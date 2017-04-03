using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class ViewItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void deleteItem(object sender, EventArgs e)
        {

        }

        protected void AddNewItem(object sender, EventArgs e)
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
            //var channel = Channel.getInstance;
            //string ret = channel.getCompanyItems();
            //List<List<string>> 
            //GridView1.DataSource =
            //GridView1.DataBind();
        }

        protected void EditItem(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

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