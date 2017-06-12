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
            long fbId = -1;
            try
            {
                fbId = Int64.Parse(FbIdTextBox.Text);
            }
            catch(Exception ex)
            {
                showAlert("מספר קבוצת פייסבוק לא הוזן כראוי");
                return;
            }

            try
            {
                int key = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.addFBGroup(FbIdTextBox.Text, key);
                if (ret.Equals("Add facebook group worked"))
                {
                    Response.Redirect("/ViewGroups.aspx");
                    showAlert("הקבוצה נוספה בהצלחה");
                }
                else
                {
                    showAlert(ret);
                }
            }
            catch (Exception exc)
            {
                showAlert(exc + "לא הצלחנו להוסיף את הקבוצה אנא נסה שנית. שגיאה:");
            }
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}