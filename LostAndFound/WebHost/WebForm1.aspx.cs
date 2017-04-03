using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;

namespace WorkerHost.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var factory = new ChannelFactory<WorkerHost.IServerService>(new NetTcpBinding(SecurityMode.None));

            var channel = factory.CreateChannel(GetTandomEndPoint());

            string colorList = TextBox1.Text;
            string itemType = TextBox2.Text;
            string cname = TextBox3.Text;
            string cphone = TextBox4.Text;

            string ret = "";//channel.testClass1(colorList, itemType, cname, cphone);
            TextBox1.Text = ret;
        }

        private EndpointAddress GetTandomEndPoint()
        {
            var endpoints = RoleEnvironment.Roles["WorkerHost"].Instances.Select(i => i.InstanceEndpoints["ServerService"]).ToArray();
            var r = new Random(DateTime.Now.Millisecond);
            return new EndpointAddress(String.Format("net.tcp://{0}/server", endpoints[r.Next(endpoints.Count() - 1)].IPEndpoint));
        }

        public static List<string> stringToListOfColors(string colors)
        {
            string color = "";
            List<string> colorList = new List<string>();
            for (int i = 0; i < colors.Length; i++)
            {
                if ((i == colors.Length - 1) || colors.ElementAt(i).Equals(","))
                {
                    if (i == colors.Length - 1)
                    {
                        color += colors.ElementAt(i);
                    }
                    colorList.Add(color);
                    color = "";
                }
                else
                {
                    color += colors.ElementAt(i);
                }
            }
            return colorList;
        }
    }
}