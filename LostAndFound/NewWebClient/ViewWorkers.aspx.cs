using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class ViewWorkers : System.Web.UI.Page
    {
        private List<WorkerData> workers;

        private string item1ID;
        public void showAlert(string content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        private void BindData()
        {
            var channel = Channel.getInstance;
            int token = (int)(Session["token"]);
            item1ID = Request.QueryString["ID"];
            workers = channel.ServerService.getCompanyWorkers(token);
            GridView1.DataSource = workers;
            GridView1.DataBind();
        }

        protected void deleteWorker(object sender, EventArgs e)
        {
            bool isOKToDelete = false;
            var argument = ((LinkButton)sender).CommandArgument;
            foreach (WorkerData wd in workers)
            {
                if (wd.UserName.Equals(argument))
                {
                    if (wd.IsAdmin)
                        break;
                    else
                    {
                        isOKToDelete = true;
                    }
                }
            }
            if (isOKToDelete)
            {
                string ans = Channel.getInstance.ServerService.removeWorker(argument, (int)Session["token"]);
                BindData();
            }
        }
    }
}