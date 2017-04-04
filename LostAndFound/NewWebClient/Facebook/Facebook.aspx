<%@ Page Title="דף כניסה" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Facebook.aspx.cs" Inherits="NewWebClient.Facebook.Facebook" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >

    <div id="fb-root"></div>
    <a href="#" onclick="loginByFacebook();">לחץ על מנת להתחבר למערכת</a>

    <%-- now this is some required facebook's JS, two things to pay attention to
    1. setting the ApplicationID, To make this project work you have to edit "callback.aspx.cs" and put your facebook-app-key there
    2. Adjust the permissions you want to get from user, set that in scope options below. --%>
    <script type="text/javascript">
        window.fbAsyncInit = function () {
            FB.init({
                appId: '1332752206764172',
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true, // parse XFBML
                oauth: true // enable OAuth 2.0
            });
        };
        (function () {
            var e = document.createElement('script'); e.async = true;
            e.src = document.location.protocol +
            '//connect.facebook.net/en_US/all.js';
            document.getElementById('fb-root').appendChild(e);
        }());

        function loginByFacebook() {
            FB.login(function (response) {
                if (response.authResponse) {
                    FacebookLoggedIn(response);
                } else {
                    console.log('User cancelled login or did not fully authorize.');
                }
            }, { scope: 'email, public_profile, user_friends' });
        }

        function FacebookLoggedIn(response) {
            var loc = '/Account/Login.aspx';
            if (loc.indexOf('?') > -1)
                window.location = loc + '&authprv=facebook&access_token=' + response.authResponse.accessToken;
            else
                window.location = loc + '?authprv=facebook&access_token=' + response.authResponse.accessToken;
        }
    </script>
</asp:Content>
