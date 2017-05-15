using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class AddWorker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void add_worker_button(object sender, EventArgs e)
        {
            if (workerNameTextBox.Text.Equals(""))
            {
                showAlert("שם העובד לא הוזן");
                return;
            }
            if (password.Text.Equals(""))
            {
                showAlert("סיסמת העובד לא הוזנה");
                return;
            }
            
            try
            {
                int key = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.addWorker(workerNameTextBox.Text, password.Text, isManager.Checked, key);
                workerNameTextBox.Text = ret;
                Response.Redirect("/ViewWorkers.aspx");
            }
            catch (Exception exc)
            {
                showAlert(exc + "לא הצלחנו להוסיף את העובד אנא נסה שנית. שגיאה:");
            }
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}