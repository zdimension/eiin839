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
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 30, invocationCount: 10)]
public class Bench
{
    private readonly IBikeRoutingService client = new BikeRoutingServiceClient();
    
    [Benchmark(OperationsPerInvoke = 10)]
    public void GetStations()
    {
        client.GetStations();
    }

    [Benchmark(OperationsPerInvoke = 10)]
    public void GetStation()
    {
        client.GetStation("9087");
    }
}


