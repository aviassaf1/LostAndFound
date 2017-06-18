using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class ViewGroups : System.Web.UI.Page
    {
        private List<GroupData> groups;

        public void showAlert(string content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                BindData();
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
            groups = channel.ServerService.getSystemCompanyFBGroup(token);
            GridView1.DataSource = groups;
            GridView1.DataBind();
        }

        protected void deleteGroup(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans = Channel.getInstance.ServerService.removeFBGroup(argument, (int)Session["token"]);
            Response.Redirect("/ViewGroups.aspx");
        }
    }
}