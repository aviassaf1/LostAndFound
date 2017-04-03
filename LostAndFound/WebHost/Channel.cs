using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WorkerHost;

namespace WebHost
{
    public class Channel
    {
        private static Channel singleton;
        private IServerService  serverService;
        //private  IAdminController adminChannel;
        //private  ICompanyController companyChannel;
        //private  IItemController itemChannel;
        //private  IMatchController matchChannel;

        public static Channel getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton= new Channel();
                }
                return singleton;
            }
        }

        public IServerService ServerService
        {
            get
            {
                return serverService;
            }
        }

        private Channel()
        {
            var channelFactory = new ChannelFactory<IServerService>(new NetTcpBinding(SecurityMode.None));

            EndpointAddress ea = GetRandomEndPoint();
            serverService = channelFactory.CreateChannel(ea);
        }


        private static EndpointAddress GetRandomEndPoint()
        {
            var endpoints = RoleEnvironment.Roles["WorkerHost"].Instances.Select(i => i.InstanceEndpoints["ServerService"]).ToArray();
            var r = new Random(DateTime.Now.Millisecond);
            return new EndpointAddress(String.Format("net.tcp://{0}/server", endpoints[r.Next(endpoints.Count() - 1)].IPEndpoint));
        }

        /*public  IAdminController AdminChannel
        {
            get
            {
                return adminChannel;
            }
        }

        public ICompanyController CompanyChannel
        {
            get
            {
                return companyChannel;
            }
        }

        public IItemController ItemChannel
        {
            get
            {
                return itemChannel;
            }
        }

        public IMatchController MatchChannel
        {
            get
            {
                return matchChannel;
            }
        }

        private Channel()
        {
            var adminChannelFactory = new ChannelFactory<IAdminController>(new NetTcpBinding(SecurityMode.None));
            var companyChannelFactory = new ChannelFactory<ICompanyController>(new NetTcpBinding(SecurityMode.None));
            var itemChannelFactory = new ChannelFactory<IItemController>(new NetTcpBinding(SecurityMode.None));
            var matchChannelFactory = new ChannelFactory<IMatchController>(new NetTcpBinding(SecurityMode.None));

            EndpointAddress ea= GetRandomEndPoint();
            adminChannel = adminChannelFactory.CreateChannel(ea);
            companyChannel = companyChannelFactory.CreateChannel(ea);
            itemChannel = itemChannelFactory.CreateChannel(ea);
            matchChannel = matchChannelFactory.CreateChannel(ea);
        }


        private static EndpointAddress GetRandomEndPoint()
        {
            var endpoints = RoleEnvironment.Roles["WorkerHost"].Instances.Select(i => i.InstanceEndpoints["ServerService"]).ToArray();
            var r = new Random(DateTime.Now.Millisecond);
            return new EndpointAddress(String.Format("net.tcp://{0}/server", endpoints[r.Next(endpoints.Count() - 1)].IPEndpoint));
        }
        */
    }
}