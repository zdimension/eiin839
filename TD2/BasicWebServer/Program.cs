using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicWebServerUrlParser;

internal class Program
{
    private static readonly string
        HttpRoot = Path.GetFullPath(Environment.GetEnvironmentVariable("HTTP_ROOT") ?? "root");

    private static async Task Main(string[] args)
    {
        //if HttpListener is not supported by the Framework
        if (!HttpListener.IsSupported)
        {
            Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
            return;
        }


        // Create a listener.
        var listener = new HttpListener();

        // Add the prefixes.
        if (args.Length != 0)
            foreach (var s in args)
                listener.Prefixes.Add(s);
        // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
        // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
        // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
        // user="Tout le monde" is language dependent, use user=Everyone in english 
        else
            Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
        listener.Start();

        // get args 
        foreach (var s in args)
            Console.WriteLine("Listening for connections on " + s);

        // Trap Ctrl-C on console to exit 
        Console.CancelKeyPress += delegate
        {
            // call methods to close socket and exit
            listener.Stop();
            listener.Close();
            Environment.Exit(0);
        };


        while (true)
        {
            // Note: The GetContext method blocks while waiting for a request.
            var context = await listener.GetContextAsync();
            var request = context.Request;

            // get url 
            Console.WriteLine($"Received request for {request.Url}");
            var response = context.Response;
            await using var stream = response.OutputStream;
            await using var sw = new StreamWriter(stream);
            try
            {
                await HandleRequest(request, response);
                response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                var http = e switch
                {
                    HttpException httpException => httpException,
                    _ => new HttpException(HttpStatusCode.InternalServerError, e.Message)
                };

                response.StatusCode = (int)http.Code;
                await sw.WriteLineAsync(
                    $"<html><body><h1>{(int)http.Code} {http.Code}</h1>{http.Message}</body></html>");
            }

            //get url protocol
            Console.WriteLine("Scheme " + request.Url!.Scheme);
            //get user in url
            Console.WriteLine("UserInfo " + request.Url.UserInfo);
            //get host in url
            Console.WriteLine("Host" + request.Url.Host);
            //get port in url
            Console.WriteLine("Port " + request.Url.Port);
            //get path in url 
            Console.WriteLine("LocalPath " + request.Url.LocalPath);

            // parse path in url 
            Console.WriteLine("Segments :");
            foreach (var str in request.Url.Segments)
                Console.WriteLine(str);
            
            Console.WriteLine("Query " + request.Url.Query);
        }
        // Httplistener neither stop ... But Ctrl-C do that ...
        // listener.Stop();
    }

    private class HttpException : Exception
    {
        public HttpException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }

        public HttpStatusCode Code { get; }
    }

    private static async Task HandleFileRequest(HttpListenerRequest request, HttpListenerResponse response)
    {
        var filePath = Path.Combine(HttpRoot, request.Url!.LocalPath[1..]);

        if (!File.Exists(filePath))
            throw new HttpException(HttpStatusCode.NotFound, "File not found");

        await using var output = response.OutputStream;

        var fileInfo = new FileInfo(filePath);
        if (fileInfo.Extension.ToUpperInvariant() == ".PHP")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "php-cgi.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    EnvironmentVariables =
                    {
                        ["REQUEST_METHOD"] = request.HttpMethod,
                        ["REQUEST_URI"] = request.Url.LocalPath,
                        ["SCRIPT_FILENAME"] = filePath,
                        ["QUERY_STRING"] = request.Url.Query.TrimStart('?'),
                        ["REDIRECT_STATUS"] = "CGI"
                    }
                }
            };
            process.StartInfo.RedirectStandardInput = true;
            process.Start();
            await request.InputStream.CopyToAsync(process.StandardInput.BaseStream);
            process.StandardInput.Close();
            using var br = new BinaryReader(process.StandardOutput.BaseStream);
            var sb = new StringBuilder();
            var val = new StringBuilder();
            var readingValue = false;
            var last = '\0';
            while (true)
            {
                var c = br.ReadChar();
                if (!readingValue && c == ':')
                {
                    readingValue = true;
                    if (br.PeekChar() == ' ')
                        br.ReadChar();
                    continue;
                }

                if (c == '\n' && last == '\r')
                {
                    // parse header
                    if (sb.Length == 0)
                        break;
                    var headerName = sb.ToString();
                    var headerValue = val.ToString();
                    response.Headers[headerName] = headerValue;
                    sb.Clear();
                    val.Clear();
                    readingValue = false;
                    continue;
                }

                if (c != '\r')
                    (readingValue ? val : sb).Append(c);
                last = c;
            }

            await process.StandardOutput.BaseStream.CopyToAsync(output);
            return;
        }

        var contentType = fileInfo.Extension switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".gif" => "image/gif",
            _ => "text/plain"
        };

        response.ContentType = contentType;
        await using var fileStream = File.OpenRead(filePath);
        await fileStream.CopyToAsync(output);
    }

    private static async Task HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
    {
        if (request.Url!.Segments.Length < 2)
            throw new HttpException(HttpStatusCode.BadRequest, "Missing controller name");

        var controller = request.Url.Segments[1][..^1];
        var controllerClass = typeof(Program).Assembly.ExportedTypes.FirstOrDefault(t => t.Name == controller);

        if (controllerClass is null)
        {
            await HandleFileRequest(request, response);
            return;
        }

        var controllerInstance = Activator.CreateInstance(controllerClass);

        if (controllerInstance is null)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, "Unable to instanciate controller");
        }

        if (request.Url.Segments.Length < 3)
            throw new HttpException(HttpStatusCode.BadRequest, "Missing action name");

        var action = request.Url.Segments[2];
        var actionMethod = controllerClass.GetMethod(action);

        if (actionMethod is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Action not found");
        }

        var query = HttpUtility.ParseQueryString(request.Url.Query);
        var parameters = actionMethod.GetParameters();
        var args = new object[parameters.Length];
        for (var i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            var parameterName = parameter.Name;
            // get param
            string parameterValue;
            try
            {
                parameterValue = query.Get(parameterName);
            }
            catch (Exception)
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Missing parameter {parameterName}");
            }

            // convert
            var parameterType = parameter.ParameterType;
            try
            {
                args[i] = Convert.ChangeType(parameterValue, parameterType);
            }
            catch (Exception)
            {
                throw new HttpException(HttpStatusCode.BadRequest,
                    $"Unable to convert {parameterValue} to {parameterType}");
            }
        }

        var result = actionMethod.Invoke(controllerInstance, args);
        await using var output = response.OutputStream;
        await using var sw = new StreamWriter(output);
        await sw.WriteAsync(result?.ToString() ?? "");
    }
}