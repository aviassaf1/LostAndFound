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
        private string _companyId; 
        protected void Page_Load(object sender, EventArgs e)
        {
            _companyId = Request.QueryString["ID"];
            
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

            
            int key = (int)Session["token"];
            var channel = Channel.getInstance;
            string ret = channel.ServerService.editCompany(companyNameTextBox.Text,null, PhoneTextBox.Text, key);
            int i = 0;
            companyNameTextBox.Text = ret;
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}