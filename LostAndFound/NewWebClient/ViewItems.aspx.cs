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
        protected void deleteItem(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            string ans=Channel.getInstance.ServerService.deleteItem(items.ElementAt(index).ItemID, (int)(Session["token"]));
            items = Channel.getInstance.ServerService.getAllCompanyItems((int)(Session["token"]));
            GridView1.DataSource = items;
            GridView1.DataBind();
            showAlert(ans);
        }

        protected void viewItemMatches(object sender, GridViewPageEventArgs e)
        {

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

        protected void EditItem(object sender, GridViewEditEventArgs e)
        {
            //int index = e.NewEditIndex;
            //GridView1.EditIndex = index;
            //Channel.getInstance.ServerService.editItem(items.ElementAt(index).ItemID, DateTime.Today, "loc", "des", 0, "con", "pho", (int)(Session["token"]));
            //BindData();
        }
        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
        }
        protected void UpdateItem(object sender, GridViewUpdateEventArgs e)
        {

            GridView1.EditIndex = -1;

            GridView1.DataBind();
        }
    }
}
