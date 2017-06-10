using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class EditCompany : System.Web.UI.Page
    {
        private string _companyName;
        private static string ph;
        protected void Page_Load(object sender, EventArgs e)
        {
            _companyName = Request.QueryString["ID"];

            ph = PhoneTextBox.Text;

            PhoneTextBox.Text = Request.QueryString["phone"];

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (PhoneTextBox.Text.Equals(""))
            {
                showAlert("מספר פלאפון לא הוזן");
                return;
            }

            try
            {
                int key = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.editCompany(_companyName, "", ph, key);
                if (ret.Equals("true"))
                {
                    showAlert("החברה נערכה בהצלחה");
                    Response.Redirect("/ViewCompanies.aspx");
                }
                else
                {
                    showAlert(ret);
                }
            }
            catch(Exception exc)
            {
                showAlert(exc+"לא הצלחנו לערוך את החברה אנא נסה שנית. שגיאה:");
            }
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}