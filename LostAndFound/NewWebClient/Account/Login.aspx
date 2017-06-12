<%@ Page Title="התחברות למערכת" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NewWebClient.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
    <h2 dir="rtl"><%: Title %></h2>

    <div class="row" dir="rtl">
        <div class="col-md-12" dir="rtl">
            <section id="loginForm" dir="rtl">
                <div class="form-horizontal" dir="rtl">
                    
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Username" CssClass="form-control" TextMode="SingleLine" style="direction:ltr"/>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                                CssClass="text-danger" ErrorMessage="אנא הזן שם משתשמש בבקשה" />
                        </div>
                        <asp:Label runat="server" AssociatedControlID="Username" CssClass="col-md-2 control-label">שם משתמש</asp:Label>
                    </div>
                    <div class="form-group" dir="rtl">
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" style="direction:ltr" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="אנא הזן סיסמה בבקשה" />
                        </div>
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">סיסמה</asp:Label>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-10 col-md-2">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="IsAdmin" />
                                <asp:Label runat="server" AssociatedControlID="IsAdmin"> מנהל מערכת? </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="התחבר" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
                <p>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    --%>
                </p>
            </section>
        </div>

      
    </div>
</asp:Content>
