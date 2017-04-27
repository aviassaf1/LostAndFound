using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class ViewMatches : System.Web.UI.Page
    {
        private List<MatchData> matches;

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
            matches = channel.ServerService.getMatchesByItemID(Int32.Parse(item1ID),token);
            /*List<ItemsForMatchesData> items = new List<ItemsForMatchesData>();
            foreach(MatchData match in matches)
            {

                items.stasus =;
            }
            GridView1.DataSource = items;*/
            GridView1.DataBind();
            
            CompanyItemData cid= channel.ServerService.getCompanyItem(Int32.Parse(item1ID), token);
            item1.Text = "פריט מספר" + cid.ItemID + "בצבע " + cid.Colors + "ותיאורו " + cid.Description;

        }
    }
}


