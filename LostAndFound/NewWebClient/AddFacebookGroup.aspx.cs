using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class AddFacebookGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FbIdTextBox.Text.Equals(""))
            {
                showAlert("מספר קבוצת פייסבוק לא הוזן");
                return;
            }
            int fbId = -1;
            try
            {
                fbId = int.Parse(FbIdTextBox.Text);
            }
            catch
            {
                showAlert("שם קבוצת פייסבוק לא הוזן");
                return;
            }


            int key = (int)Session["token"];
            var channel = Channel.getInstance;
            string ret = channel.ServerService.addFBGroup(FbIdTextBox.Text, key);
            int i = 0;
            showAlert("הקבוצה נוספה");
            Response.Redirect("/AddCompany.aspx");
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}