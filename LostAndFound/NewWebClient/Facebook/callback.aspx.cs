using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace NewWebClient
{
    public partial class callback : System.Web.UI.Page
    {
        public const string FaceBookAppKey = "f2631cbbee9a9ccbcdb09558a6f1bc52";
        private char[] userInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["access_token"])) return; //ERROR! No token returned from Facebook!!
            Session["token"] = Request.QueryString["access_token"];
            //let's send an http-request to facebook using the token            
            string json = GetFacebookUserJSON(Request.QueryString["access_token"]);


            //and Deserialize the JSON response
            JavaScriptSerializer js = new JavaScriptSerializer();

            FacebookUser oUser = js.Deserialize<FacebookUser>(json);
            if (oUser != null)
            {
                Response.Write("Welcome, " + oUser.name);
                // Response.Write("<br />id, " + oUser.id);
                Response.Write("<br />Email : " + oUser.email);
                Response.Write("<br />First_name: " + oUser.first_name);
                Response.Write("<br />Last_name: " + oUser.last_name);
                Response.Write("<br />Gender: " + oUser.gender);
                Response.Write("<br />Link: " + oUser.link);
                Response.Write("<br />Token: " + Request.QueryString["access_token"]);

            }

        }
        private static string GetFacebookUserJSON(string access_token)
        {
            string url = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name,first_name,last_name,link,birthday,cover,devices,gender", access_token);

            WebClient wc = new WebClient();
            Stream data = wc.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }
    }
}