using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        //
        // Set up the process with the ProcessStartInfo class.
        // https://www.dotnetperls.com/process
        //
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = @"D:\ENSEIGNEMENTS\SoCWS SI4\TD2\BasicExamplesTD2\ExecTest\bin\Debug\ExecTest.exe"; // Specify exe name.
        start.Arguments = "Argument1"; // Specify arguments.
        start.UseShellExecute = false; 
        start.RedirectStandardOutput = true;
        //
        // Start the process.
        //
        using (Process process = Process.Start(start))
        {
            //
            // Read in all the text from the process with the StreamReader.
            //
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
    }
}