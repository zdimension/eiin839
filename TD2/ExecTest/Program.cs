using System;

namespace ExeTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 1)
                Console.WriteLine(args[0]);
            else
                Console.WriteLine("ExeTest <string parameter>");
        }
    }
}