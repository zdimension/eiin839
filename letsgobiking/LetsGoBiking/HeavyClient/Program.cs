using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using HeavyClient.RoutingService;

Console.WriteLine("Waiting for services to start up...");
Thread.Sleep(5000);
Console.WriteLine("Starting benchmark...");

var client = new BikeRoutingServiceClient();

Bench(() => client.GetStations());
Bench(() => client.GetStation("9087"));

Console.WriteLine("Done");
Console.ReadLine();

void Bench<T>(Expression<Func<T>> code)
{
    var comp = code.Compile();
    Console.WriteLine(code.Body);
    var sw = new Stopwatch();
    for (var i = 0; i < 5; i++)
    {
        Console.Write("[{0}] ", i);
        sw.Start();
        comp();
        sw.Stop();
        Console.WriteLine("{0} ms", sw.ElapsedMilliseconds);
        sw.Reset();
    }
}