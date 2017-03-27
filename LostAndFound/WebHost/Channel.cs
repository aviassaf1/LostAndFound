using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WebHost
{
    public class Channel
    {
        private static WorkerHost.IServerService channel;
        public static WorkerHost.IServerService getInstance
        {
            get
            {
                if (channel == null)
                {
                    new Channel();
                }
                return channel;
            }
        }

        private Channel()
        {
            var factory = new ChannelFactory<WorkerHost.IServerService>(new NetTcpBinding(SecurityMode.None));

            channel = factory.CreateChannel(GetRandomEndPoint());
        }


        private static EndpointAddress GetRandomEndPoint()
        {
            var endpoints = RoleEnvironment.Roles["WorkerHost"].Instances.Select(i => i.InstanceEndpoints["ServerService"]).ToArray();
            var r = new Random(DateTime.Now.Millisecond);
            return new EndpointAddress(String.Format("net.tcp://{0}/server", endpoints[r.Next(endpoints.Count() - 1)].IPEndpoint));
        }
    }
}