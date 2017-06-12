using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class UpdateToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void updateTokenButton(object sender, EventArgs e)
        {
            if (TokenTextBox.Text.Equals(""))
            {
                showAlert("מפתח לא הוזן");
                return;
            }
            if (companyNameTextBox.Text.Equals(""))
            {
                showAlert("שם חברה לא הוזן");
                return;
            }
            try
            {
                int key = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.updateToken(TokenTextBox.Text, companyNameTextBox.Text, key);
                showAlert(ret);

            }
            catch (Exception exc)
            {
                showAlert(exc + "לא הצלחנו להוסיף את החברה אנא נסה שנית. שגיאה:");
            }
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}