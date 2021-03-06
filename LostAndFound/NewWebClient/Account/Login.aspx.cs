﻿using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using NewWebClient.Models;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using NewWebClient.Facebook;
using Facebook;
using System.Collections.Generic;

namespace NewWebClient.Account
{
    public partial class Login : Page
    {
        public const string FaceBookAppKey = "f2631cbbee9a9ccbcdb09558a6f1bc52";
        private string fbToken;


        protected void Page_Load(object sender, EventArgs e)
        {
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);


            if (string.IsNullOrEmpty(Request.QueryString["access_token"])) return; //ERROR! No token returned from Facebook!!
            Session["token"] = Request.QueryString["access_token"];
            //Session["token"] = "EAACEdEose0cBAMKjyMnsZAsUVuws6IwTPIKWIpDqfasPHlRp5ilwpYZBxepNNm97ivGGleR3cpZApBspAYshApGDQ8LiP4bZABHd7r7XLMa0GpJFMiIvXnYDxhptazLFGBpIBy5nZCGBFesyKgmos0rp4NDGI8Mzfo93ApqeALZB5B4ToAmnsu";
            //let's send an http-request to facebook using the token            
            string json = GetFacebookUserJSON(Request.QueryString["access_token"]);


            //and Deserialize the JSON response
            JavaScriptSerializer js = new JavaScriptSerializer();

            FacebookUser oUser = js.Deserialize<FacebookUser>(json);
            if (oUser != null)
            {
                fbToken = Request.QueryString["access_token"];
                //fbToken = "EAACEdEose0cBAMKjyMnsZAsUVuws6IwTPIKWIpDqfasPHlRp5ilwpYZBxepNNm97ivGGleR3cpZApBspAYshApGDQ8LiP4bZABHd7r7XLMa0GpJFMiIvXnYDxhptazLFGBpIBy5nZCGBFesyKgmos0rp4NDGI8Mzfo93ApqeALZB5B4ToAmnsu";
            }
            string fbid = "";
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(fbToken);
                var parameters = new Dictionary<string, object>();
                parameters["fields"] = "id";
                dynamic result;
                try
                {
                    //make sure post succeeds with GID
                    result = fb.Get("me", parameters);
                    fbid = result.id;
                    Label1.Text="במידה ואתה מייצג חברה חדשה, אנא העבר מפתח זה למנהל המערכת " +
                        fbid +"."+ "  בתודה, הנהלת אבדות ומציאות";
                }
                catch (Exception)
                {
                }
            }
            catch
            {
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
        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                /*/ Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Username.Text, Password.Text, IsAdmin.Checked, shouldLockout: false);
                */
                var channel = Channel.getInstance;
                string res;
                if (IsAdmin.Checked)
                {
                    res = channel.ServerService.Adminlogin(Username.Text, Password.Text);
                    if (res.Contains("login succeeded,"))
                    {
                        char[] ar = { ',' };
                        res = res.Split(ar)[1];
                        Username.Text = res;
                        Session["token"] = int.Parse(res);
                        Session["isManager"] = false;
                        Response.Redirect("../ViewCompanies.aspx");
                    }
                    else
                    {
                        showAlert(res);
                    }
                }
                else
                { 
                     res=channel.ServerService.login(fbToken,Username.Text, Password.Text);
                    if (res.Contains("login succeeded,"))
                    {
                        char[] ar = { ',' };
                        res = res.Split(ar)[1];
                        Session["token"] = int.Parse(res);
                        Session["isManager"] = channel.ServerService.isManager((int)Session["token"]); 
                        Response.Redirect("../ViewItems.aspx");
                        //string sessionToken = Session["token"].ToString();
                    }
                    else
                    {
                        showAlert(res);
                    }
                }
                

                /*switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
                                                        Request.QueryString["ReturnUrl"],
                                                        IsAdmin.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }*/
            }
        }
    }
}