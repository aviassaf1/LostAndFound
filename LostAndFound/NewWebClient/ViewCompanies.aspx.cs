﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkerHost.ServiceLayer.DataContracts;

namespace NewWebClient
{
    public partial class ViewCompanies : System.Web.UI.Page
    {
        private List<CompanyData> companies;

        private string item1ID;
        public void showAlert(string content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
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
            item1ID = Request.QueryString["ID"];
            companies = channel.ServerService.getAllCompanies(token);
            GridView1.DataSource = companies;
            GridView1.DataBind();
        }

        protected void deleteCompany(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            string ans = Channel.getInstance.ServerService.deleteCompany(argument, (int)Session["token"]);
            BindData();
        }

        protected void editCompany(object sender, EventArgs e)
        {
            var argument = ((LinkButton)sender).CommandArgument;
            Response.Redirect("/EditCompany.aspx?ID=" + argument);
        }
    }
}