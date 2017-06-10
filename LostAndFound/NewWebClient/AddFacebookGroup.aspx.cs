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

            try
            {
                int key = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.addFBGroup(FbIdTextBox.Text, key);
                if (ret.Equals("Add facebook group worked"))
                {
                    showAlert("הקבוצה נוספה בהצלחה");
                    Response.Redirect("/AddCompany.aspx");
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