using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using System.ServiceModel;
using WorkerHost.ServiceLayer.Controllers;

namespace WorkerHost
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("WorkerHost is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            StartWCFHost();


            bool result = base.OnStart();

            Trace.TraceInformation("WorkerHost has been started");

            return result;
        }

        private void StartWCFHost()
        {
            var itemBaseAddress = String.Format("net.tcp://{0}", RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["ItemController"].IPEndpoint);
            var itemHost = new ServiceHost(typeof(ItemController), new Uri(itemBaseAddress));
            var companyBaseAddress = String.Format("net.tcp://{0}", RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["CompanyController"].IPEndpoint);
            var companyHost = new ServiceHost(typeof(CompanyController), new Uri(companyBaseAddress));
            var matchBaseAddress = String.Format("net.tcp://{0}", RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["MatchController"].IPEndpoint);
            var matchHost = new ServiceHost(typeof(MatchController), new Uri(matchBaseAddress));
            var adminBaseAddress = String.Format("net.tcp://{0}", RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["AdminController"].IPEndpoint);
            var adminHost = new ServiceHost(typeof(AdminController), new Uri(adminBaseAddress));

            itemHost.AddServiceEndpoint(typeof(IItemController), new NetTcpBinding(SecurityMode.None), "server");
            companyHost.AddServiceEndpoint(typeof(ICompanyController), new NetTcpBinding(SecurityMode.None), "server");
            matchHost.AddServiceEndpoint(typeof(IMatchController), new NetTcpBinding(SecurityMode.None), "server");
            adminHost.AddServiceEndpoint(typeof(IAdminController), new NetTcpBinding(SecurityMode.None), "server");

            while (true)
            {
                try
                {
                    itemHost.Open();
                    companyHost.Open();
                    matchHost.Open();
                    adminHost.Open();
                    break;
                }
                catch
                {

                }
            }
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerHost is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerHost has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
