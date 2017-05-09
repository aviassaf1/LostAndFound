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
        protected void Page_Load(object sender, EventArgs e)
        {
            _companyName = Request.QueryString["ID"];

            companyNameTextBox.Text = _companyName;
            PhoneTextBox.Text = Request.QueryString["phone"];

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (companyNameTextBox.Text.Equals(""))
            {
                showAlert("שם חברה לא הוזן");
                return;
            }
            if (PhoneTextBox.Text.Equals(""))
            {
                showAlert("מספר פלאפון לא הוזן");
                return;
            }

            try
            {
                int key = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.editCompany(companyNameTextBox.Text, null, PhoneTextBox.Text, key);
                int i = 0;
                companyNameTextBox.Text = ret;
                Response.Redirect("/ViewCompanies.aspx");
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