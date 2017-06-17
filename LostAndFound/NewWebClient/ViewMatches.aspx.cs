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
            item1ID = Request.QueryString["ID"];
            matches = channel.ServerService.getMatchesByItemID(Int32.Parse(item1ID),token);
            GridView1.DataSource = matches;
            GridView1.DataBind();
            if (matches != null)
            {
                CompanyItemData cid = channel.ServerService.getCompanyItem(Int32.Parse(item1ID), token);
                item1.Text = "פריט מספר " + cid.ItemID + "בצבע " + cid.Colors;
            }
        }

        protected void correctMatch(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans = Channel.getInstance.ServerService.changeMatchStatus(int.Parse(argument), "נכון", (int)(Session["token"]));
            showAlert(ans);
            Response.Redirect("ViewMatches.aspx?ID="+ Request.QueryString["ID"]);
        }

        protected void doneMatch(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans = Channel.getInstance.ServerService.changeMatchStatus(int.Parse(argument), "הושלם", (int)(Session["token"]));
            showAlert(ans);
            Response.Redirect("ViewMatches.aspx?ID=" + Request.QueryString["ID"]);
        }

        protected void deleteMatch(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans = Channel.getInstance.ServerService.changeMatchStatus(int.Parse(argument), "לא נכון", (int)(Session["token"]));
            showAlert(ans);
            Response.Redirect("ViewMatches.aspx?ID=" + Request.QueryString["ID"]);
        }
    }
}


