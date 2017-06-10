using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace NewWebClient
{
    public partial class AddLostItem : System.Web.UI.Page
    {
        private string _path;
        protected void Page_Load(object sender, EventArgs e)
        {
            _path = "";

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
            try
            {
                int token = (int)Session["token"];
                var channel = Channel.getInstance;
                string ret = channel.ServerService.addLostItem(sColors, sType, date, location, description,
                serialNumber, contactName, contactPhone, "D", token);
                showAlert(ret);
                Response.Redirect("/ViewItems.aspx");
            }
            catch (Exception exc)
            {
                showAlert(exc + "לא הצלחנו להוסיף את הפריט אנא נסה שנית. שגיאה:");
            }
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }

        protected void imageChoosingButton_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(getPath));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void getPath()
        {
            List<String> pathes= ImageChooser.getImagePath();
            if(pathes.Count>0)
                _path = pathes[0];

        }
    }
}