using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebHost.WebPages
{
    public partial class addLostItem : System.Web.UI.Page
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
            DateTime date=_dateCalendar.SelectedDate;
            if (date > DateTime.Today)
            {
                showAlert("תאריך לא תקין, נא לבחור תאריך אמיתי");
            }
            string location = _location.Text;
            string description = _description.Text;
            int serialNumber=0;
            try
            {
                serialNumber = Int32.Parse(_serial.Text);
                if (serialNumber < 0)
                {
                    showAlert("בבקשה להזין מספר סידורי תקין, במידה ואין לך, הזן 0");
                }
            }
            catch(Exception ex)
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
            string token = "EAACEdEose0cBAPgpersMY9yjiN815tNP6IvchRcwqsFGJbUC7XZCsTpKZAnVFarhR0EQcqrhnDLk7WfGXHmxpyZA9SHTiYGmTZAZALwgKBcyJ2pVmxPefsIAvndck3vTNGxEu4JWw1vgt0cFLjn1wT5pGF7BWZA3qWEZCNU6pyo5TECqxLmCkj6";
            var channel = Channel.getInstance;
            string ret = channel.addLostItem(sColors, sType, date, location, description,
            serialNumber, "GuyCompany", contactName,contactPhone, "D",token);
            int i = 0;
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
}
}