using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class ViewItems : System.Web.UI.Page
    {
        private List<CompanyItemData> items;

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
        protected void deleteItem2(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans=Channel.getInstance.ServerService.deleteItem(int.Parse(argument), (int)(Session["token"]));
            items = Channel.getInstance.ServerService.getAllCompanyItems((int)(Session["token"]));
            GridView1.DataSource = items;
            GridView1.DataBind();
            showAlert(ans);
        }
        protected void deleteItem(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void viewItemMatches(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            Response.Redirect("/ViewMatches.aspx?ID="+argument);
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
            items= channel.ServerService.getAllCompanyItems(token);
            GridView1.DataSource = items;
            GridView1.DataBind();
        }

        protected void EditItem2(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;

            Response.Redirect("/EditItem.aspx?ID=" + argument);
        }

        protected void EditItem(object sender, GridViewEditEventArgs e)
        {
        }

        protected void publishButton_Click(object sender, EventArgs e)
        {
            var channel = Channel.getInstance;
            int key = (int)Session["token"];
            channel.ServerService.publishInventory(/*should be a group id*/ "0", 0, key);
        }
    }
}
