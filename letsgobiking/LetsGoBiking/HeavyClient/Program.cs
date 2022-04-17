using System;
using HeavyClient.RoutingService;

var route = new BikeRoutingServiceClient();
var s = await route.GetStationsAsync();
Console.WriteLine(s[0]);
Console.ReadLine();
