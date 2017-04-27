using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class EditItem : System.Web.UI.Page
    {
        private string itemid;
        private CompanyItemData item;
        public static Dictionary<string, string> EnglishTypes2Hebrew = new Dictionary<string, string>(){{"ID" , "תעודה" }, {"WALLET", "ארנק"  },
                {"PCMOUSE", "עכבר"  }, { "CHARGER","מטען" },{ "PC","מחשב" }, { "PHONE","טלפון" }, { "KEYS","מפתח" }, {"BAG", "תיק" }, { "UMBRELLA","מטריה" },
                { "SWEATSHIRT","סווטשרט" }, { "GLASSES","משקפיים" }, {"SHOES", "נעל" },{ "FLIPFLOPS","כפכפים" },
                {"FOLDER" , "תיקיה/מחברת/קלסר"}, { "EARING", "עגיל" }, {  "RING" ,"טבעת"},
                { "NECKLACE","שרשרת/תליון" },{ "BRACELET", "צמיד" }, {"HEADPHONES","אוזניות" }, {"PencilCase","קלמר" } };

        protected void Page_Load(object sender, EventArgs e)
        {
            itemid = Request.QueryString["ID"];
            item = Channel.getInstance.ServerService.getCompanyItem(Int32.Parse(itemid), (int)(Session["token"]));
            string hebItem;
            foreach (ListItem litem in _TypeList.Items)
            {
                EnglishTypes2Hebrew.TryGetValue(litem.Value, out hebItem);
                if (hebItem!=null && hebItem.Equals(item.ItemType))
                {
                    litem.Selected = true;
                }
            }
           // ListItem selectedListItem = _TypeList.Items.FindByValue(item.ItemType);
           // if (selectedListItem != null)
            //    selectedListItem.Selected = true;

            ListItemCollection typelist = _TypeList.Items;
            /*foreach(ListItem type in typelist)
            {
                if (type.Value.Equals(item.ItemType))
                    _TypeList.SelectedItem.Value = type.Value;
            }*/
            _dateCalendar.SelectedDate = item.Date1;
            _location.Text = item.Location;
            _description.Text = item.Description;
            _contactName.Text = item.ContactName;
            _contactPhone.Text = item.ContactPhone;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            /*List<string> sColors = new List<string>();//בעיה
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
            }*/
            string sType = _TypeList.SelectedValue;
            if (sType == "NONE")
            {
                showAlert("לא נבחר סוג פריט");
            }
            DateTime date = _dateCalendar.SelectedDate;
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
            catch (Exception ex)
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
            var channel = Channel.getInstance;
            string ret = channel.ServerService.editItem(Int32.Parse(itemid), date, location, description,
            serialNumber, contactName, contactPhone, 564/*key*/);
            int i = 0;
            _contactName.Text = ret;
        }
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
    }
}