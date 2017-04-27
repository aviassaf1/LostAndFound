using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewWebClient
{
    public partial class AddFoundItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<string> sColors = new List<string>();//בעיה
            foreach (ListItem item in _ColorsCheckBox.Items)
            {
                if (item.Selected)
                {
                    sColors.Add(item.Value);
                }
            }
            if (sColors.Count == 0)
            {
                showAlert("בחר בבקשה צבע אחד לפחות");
            }
            string sType = _TypeList.SelectedValue;
            if (sType == "NONE")
            {
                showAlert("לא נבחר סוג פריט");
            }
            DateTime date = _dateCalendar.SelectedDate;
            if (date > DateTime.Today)
            {
                showAlert("תאריך לא תקין, נא לבחור תאריך אמיתי");
            }
            string location = _location.Text;
            string description = _description.Text;
            int serialNumber = 0;
            try
            {
                serialNumber = Int32.Parse(_serial.Text);
                if (serialNumber < 0)
                {
                    showAlert("בבקשה להזין מספר סידורי תקין, במידה ואין לך, הזן 0");
                }
            }
            catch (Exception)
            {
                showAlert("בבקשה להזין מספר סידורי תקין, במידה ואין לך, הזן 0");
            }
            string contactName = _contactName.Text;
            if (contactName == "")
            {
                showAlert("בבקשה להזין שם של מדווח האבידה");
            }
            string contactPhone = _contactPhone.Text;
            if (contactPhone == "")//עוד בדיקות
            {
                showAlert("בבקשה להזין טלפון של מדווח האבידה");
            }
            int token = (int)Session["token"];
            var channel = Channel.getInstance;
            string ret = channel.ServerService.addFoundItem(sColors, sType, date, location, description,
            serialNumber, contactName, contactPhone, "D", token);
            showAlert(ret);
            Response.Redirect("/ViewItems.aspx");
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}