using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class ViewItems : System.Web.UI.Page
    {
        private List<CompanyItemData> items;
        private string _path;
        private bool _wait;
        private IimageProcessing _iip;

        public void showAlert(string content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            _path = "";
            _wait = true;
            //_iip = new ImageProccessingGoogle();
            _iip = new imageProcessingMicrosoft();
        }
        protected void deleteItem2(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans=Channel.getInstance.ServerService.deleteItem(int.Parse(argument), (int)(Session["token"]));
            if(ans.Equals("Item Removed"))
            {
                showAlert("הפריט נמחק בהצלחה");
                items = Channel.getInstance.ServerService.getAllCompanyItems((int)(Session["token"]));
                Response.Redirect("/ViewItems.aspx");
            }
            else
            {
                showAlert(ans);
            }
        }
        protected void deleteItem(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void viewItemMatches(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            Response.Redirect("/ViewMatches.aspx?ID="+argument);
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            BindData();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        private void BindData()
        {
            var channel = Channel.getInstance;
            int token = (int)(Session["token"]);
            items= channel.ServerService.getAllCompanyItems(token);
            GridView1.DataSource = items;
            GridView1.DataBind();
        }

        protected void EditItem2(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;

            Response.Redirect("/EditItem.aspx?ID=" + argument);
        }

        protected void EditItem(object sender, GridViewEditEventArgs e)
        {
        }

        protected void publishButton_Click(object sender, EventArgs e)
        {
            try
            {
                var channel = Channel.getInstance;
                int key = (int)Session["token"];
                string ret = channel.ServerService.publishInventory("", 3, key);
                if (ret.Equals("true"))
                {
                    showAlert("פרסום הפריטים הצליח");
                }
                else
                {
                    showAlert(ret);
                }
            }
            catch (Exception exc)
            {
                showAlert(exc + "לא הצלחנו לפרסם את הפריטים אנא נסה שנית. שגיאה:");
            }
        }

        protected void ViewWorkers(object sender, EventArgs e)
        {
            Response.Redirect("/ViewWorkers.aspx");
        }


        /////////////////////////////////////from add item page///////////////////////////////////

        protected void picFoundItem_Click(object sender, EventArgs e)
        {
            imageChoosingButton_Click(null, null);
            while (_wait) ;
            _wait = true;
            Response.Redirect("/AddFoundItem.aspx?path=" + _path);
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
            if (!_path.Equals(""))
            {
                setDataFromPic();
            }
            _wait = false;
        }



        private void setDataFromPic()
        {
            List<string> types = new List<string>();
            List<int> colorsIndex = new List<int>();
            _iip.processImage(_path, types, colorsIndex);
            Session["types"] = types;
            Session["colorsIndex"] = colorsIndex;


        }

    }
}
