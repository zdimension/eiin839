using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SoapClient.MathsLibrary;

namespace SoapClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new MathsOperationsClient();
            var rng = new Random();
            while (true)
            {
                var (x, y) = (rng.Next(1, 10), rng.Next(1, 10));
                foreach (var e in client.GetType().GetMethods()
                             .Where(meth => meth.GetParameters().Length == 2 &&
                                            (meth.ReturnType == typeof(int) || meth.ReturnType == typeof(double))))
                {
                    Console.WriteLine($"{e.Name}({x}, {y}) == {e.Invoke(client, new object[] { x, y })}");
                }

                Console.ReadKey();
            }
        }
    }
}
