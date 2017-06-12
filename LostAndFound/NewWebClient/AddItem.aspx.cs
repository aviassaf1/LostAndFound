using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace NewWebClient
{
    public partial class AddItem : System.Web.UI.Page
    {
        string _path;
        bool _wait;
        protected void Page_Load(object sender, EventArgs e)
        {
            _path = "";
            _wait = true;
        }
        

        protected void picFoundItem_Click(object sender, EventArgs e)
        {
            imageChoosingButton_Click(null, null);
            while (_wait) ;
            _wait = true;
            Response.Redirect("/AddFoundItem.aspx?path="+_path);
        }

        protected void noPicFoundItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AddFoundItem.aspx?path=" + _path);
        }

        protected void picLostItem_Click(object sender, EventArgs e)
        {
            imageChoosingButton_Click(null, null);
            while (_wait) ;
            _wait = true;
            Response.Redirect("/AddLostItem.aspx?path=" + _path);
        }

        protected void noPicLostItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("/AddLostItem.aspx?path=" + _path);
        }


        protected void imageChoosingButton_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(getPath));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void getPath()
        {
            List<String> pathes = ImageChooser.getImagePath();
            if (pathes.Count > 0)
                _path = pathes[0];
            _wait = false;

        }
    }
}