using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoServer;

internal static class EchoServer
{
    private static void Main(string[] args)
    {
        Console.CancelKeyPress += delegate { Environment.Exit(0); };

        const int port = 5000;
            
        var serverSocket = new TcpListener(IPAddress.Loopback, port);
        serverSocket.Start();

        Console.WriteLine($"Server started: localhost:{port}");
        while (true)
        {
            var clientSocket = serverSocket.AcceptTcpClient();
            var client = new HandleClient();
            client.StartClient(clientSocket);
        }
    }
}

public class HandleClient
{
    private TcpClient _clientSocket;

    public void StartClient(TcpClient inClientSocket)
    {
        _clientSocket = inClientSocket;
        var ctThread = new Thread(Echo);
        ctThread.Start();
    }

    private static readonly string RootPath = Path.GetFullPath(Environment.GetEnvironmentVariable("HTTP_ROOT") ?? "root");

    private static MemoryStream MakeRaw(string message)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(message));
    }
    
    private static async void Response(Stream output, int code, Stream content)
    {
        await using var sw = new StreamWriter(output);

        await sw.WriteLineAsync($"HTTP/1.1 {code} OK");

        await sw.WriteLineAsync("Content-Type: text/html");
        await sw.WriteLineAsync($"Content-Length: {content.Length}");
        await sw.WriteLineAsync("Content-Encoding: UTF-8");

        await sw.WriteLineAsync();

        await sw.FlushAsync();

        await content.CopyToAsync(output);
    }

    private async void Echo()
    {
        var stream = _clientSocket.GetStream();

        try
        {
            await HandleRequest(stream);
        }
        catch (Exception e)
        {
            Response(stream, 500, MakeRaw(e.Message));
        }
    }

    private static async Task HandleRequest(NetworkStream stream)
    {
        using var sr = new StreamReader(stream);

        var req = await sr.ReadLineAsync();
        if (req is null)
            return;

        var options = req.Split(' ');
        var (method, url) = (options[0], options[1]);

        if (url[0] == '/')
            url = url[1..];

        switch (method)
        {
            case "GET":
            {
                var filePath = Path.Combine(RootPath, url);

                if (Directory.Exists(filePath))
                {
                    filePath += "/index.html";
                }
                
                if (!File.Exists(filePath))
                {
                    Response(stream, 404, MakeRaw("Page not found"));
                    return;
                }

                await using var fs = File.OpenRead(filePath);
                Response(stream, 200, fs);

                break;
            }
            default:
            {
                Response(stream, 405, MakeRaw("Method not allowed"));
                break;
            }
        }
    }
}