using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using HeavyClient.RoutingService;

Console.WriteLine("Starting benchmark...");

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

[MinColumn, MaxColumn]
public class Bench
{
    private readonly IBikeRoutingService client = new BikeRoutingServiceClient();
    
    [Benchmark]
    public void GetStations()
    {
        client.GetStations();
    }

    [Benchmark]
    public void GetStation()
    {
        client.GetStation("9087");
    }
}


