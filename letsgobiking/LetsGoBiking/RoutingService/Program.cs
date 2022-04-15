using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace RoutingService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utilities.StartService<BikeRoutingService>();
        }
    }
}