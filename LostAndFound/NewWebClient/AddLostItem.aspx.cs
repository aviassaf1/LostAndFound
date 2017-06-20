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
            _path = (string)(Request.QueryString["path"]);
            if (!_path.Equals(""))
            {
                List<string> types = (List<string>)Session["types"];
                List<int> colorsIndex = (List<int>)Session["colorsIndex"];
                int i = 0;
                foreach (ListItem item in _ColorsCheckBox.Items)
                {
                    if (colorsIndex.Contains(i))
                    {
                        item.Selected = true;
                    }
                    i++;
                }
                string hebrewFinalType = "";
                string englishFinalType = "";
                foreach (string type in types)
                {
                    foreach (string t in EditItem.EnglishTypes2Hebrew.Keys)
                    {
                        if ((type.ToUpper().Contains(t.ToUpper()) || t.ToUpper().Contains(type.ToUpper())) && hebrewFinalType.Equals(""))
                        {
                            hebrewFinalType = EditItem.EnglishTypes2Hebrew[t];
                            englishFinalType = type;
                        }
                    }
                }
                if (!hebrewFinalType.Equals(""))
                {
                    foreach (ListItem li in _TypeList.Items)
                    {
                        if (li.Text.Equals(hebrewFinalType) || englishFinalType.ToUpper().Contains(li.Value.ToUpper()) ||
                            (li.Text.Contains(hebrewFinalType) && !(li.Text.Contains(" "))) || (li.Value.ToUpper().Contains(englishFinalType.ToUpper()) && !(li.Value.ToUpper().Contains(" ")))
                            || (hebrewFinalType.Contains(li.Text) && !(hebrewFinalType.Contains(" "))) || (englishFinalType.ToUpper().Contains(li.Value.ToUpper())) && !(englishFinalType.ToUpper().Contains(" ")))
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }
            }
            _dateCalendar.SelectedDate = DateTime.Today;
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
            if (date > DateTime.Today || date < new DateTime(2005, 1, 1))
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
                if (ret.Equals("add lost item: item was added successfully"))
                {
                    showAlert("הפריט נוסף בהצלחה");
                    Response.Redirect("/ViewItems.aspx");
                }
                else
                {
                    showAlert(ret);
                }
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

    }
}