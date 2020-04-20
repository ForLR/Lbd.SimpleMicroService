using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBD.Api.Utility
{
    public static  class ConsulHelper
    {

        public static void ConsulRegister(this IConfiguration configuration)
        {
            var ConsulUri = configuration.GetValue<string>("Consul");
            ConsulClient consulClient = new ConsulClient(c=> 
            {
                c.Address = new Uri(ConsulUri);
                c.Datacenter = "dc1";
            });
            string ip = configuration["ip"];  //"127.0.0.1";
            int port = int.Parse(configuration["port"]);// 9000; //
            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 :int.Parse(configuration["weight"]);
            consulClient.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                Address = ip,
                Port = port,
                Tags = new string[] { weight.ToString() },
                ID = string.Format("{0}-{1}",ip,port),
                Name="LBDService",
                Check=new AgentServiceCheck 
                {
                    DeregisterCriticalServiceAfter=TimeSpan.FromSeconds(5),
                    Interval= TimeSpan.FromSeconds(12),
                    HTTP= $"http://{ip}:{port}/api/Health/Index",
                    Timeout= TimeSpan.FromSeconds(5)
                }
            });
        }

    }
}

//dotnet run  --urls="http://*:9000" --ip="127.0.0.1" --port=9000  --weight=1