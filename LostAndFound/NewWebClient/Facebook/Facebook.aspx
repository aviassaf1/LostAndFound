<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Facebook.aspx.cs" Inherits="NewWebClient.Facebook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="fb-root"></div>
    <a href="#" onclick="loginByFacebook();">Login with Facebook</a>

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
            var loc = '/Facebook/callback.aspx';
            if (loc.indexOf('?') > -1)
                window.location = loc + '&authprv=facebook&access_token=' + response.authResponse.accessToken;
            else
                window.location = loc + '?authprv=facebook&access_token=' + response.authResponse.accessToken;
        }
    </script>
</body>
</html>
