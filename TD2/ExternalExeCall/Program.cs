using System;
using System.Diagnostics;

internal class Program
{
    private static void Main()
    {
        //
        // Set up the process with the ProcessStartInfo class.
        // https://www.dotnetperls.com/process
        //
        var start = new ProcessStartInfo();
        start.FileName =
            @"D:\ENSEIGNEMENTS\SoCWS SI4\TD2\BasicExamplesTD2\ExecTest\bin\Debug\ExecTest.exe"; // Specify exe name.
        start.Arguments = "Argument1"; // Specify arguments.
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        //
        // Start the process.
        //
        using (var process = Process.Start(start))
        {
            //
            // Read in all the text from the process with the StreamReader.
            //
            using (var reader = process.StandardOutput)
            {
                var result = reader.ReadToEnd();
                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
    }
}