using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class AddCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            if (FacebookIdTextBox.Text.Equals(""))
            {
                showAlert("פייסבוק ID לא הוזן");
                return;
            }
            if (GroupNamesTextBox.Text.Equals(""))
            {
                showAlert("אף שם חברה לא הוזן");
                return;
            }
            if (GroupManager.Text.Equals(""))
            {
                showAlert("שם מנהל לא הוזן");
                return;
            }
            if (GroupManagerPass.Text.Equals(""))
            {
                showAlert("סיסמת מנהל לא הוזנה");
                return;
            }
            
            int key = (int)Session["token"];
            var channel = Channel.getInstance;
            string [] groupsArray = GroupNamesTextBox.Text.Split(',');
            HashSet<string> groupsSet = new HashSet<string>();
            foreach(string s in groupsArray)
            {
                groupsSet.Add(s);
            }
            string ret = channel.ServerService.addComapny(companyNameTextBox.Text, PhoneTextBox.Text, groupsSet, FacebookIdTextBox.Text, GroupManager.Text, GroupManagerPass.Text, key);
            companyNameTextBox.Text = ret;
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}