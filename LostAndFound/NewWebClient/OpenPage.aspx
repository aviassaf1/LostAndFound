<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="OpenPage.aspx.cs" Inherits="NewWebClient.OpenPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" dir="rtl">
        <h1>ברוכים הבאים למערכת אבדות ומציאות</h1>
        <p class="lead">הסבר על המערכת</p>
        <p><a href="#" onclick="loginByFacebook();" class="btn btn-primary btn-lg">התחבר למערכת &raquo;</a>

            <div id="fb-root"></div>

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
    </p>
    </div>

    <div class="row" dir="rtl">
        <div class="col-md-4">
            <h2>יצירת קשר</h2>
            <p>
                לפרטים נוספים ויצירת קשר לחברות המתעניינות במערכת ניתן ליצור קשר בטלפון 0509659522
            </p>
            <p>
                נציגנו ישמחו לעמוד לשירותכם 24/7, במיוחד בשעות הקטנות של הלילה
            </p>
        </div>
        <div class="col-md-4">
            <h2>קצת עלינו</h2>
            <p>
                המערכת נכתבה על ידי הסטודנטים: גיא אביאסף, תומר רוזנברגר ונעם ברקאי                
            </p>
            <p>
                 בהנחייתם של ד"ר גרא וייס מהמחלקה למדעי המחשב וד"ר יוסי אורן מהמחלקה למערכות מידע
            </p>
            <p>                
                לטובת פרוייקט גמר לתואר ראשון בהנדסת תוכנה באוניברסיטת בן גוריון בנגב
            </p>
        </div>
        <div class="col-md-4">
            <h2>על המערכת</h2>
            <p>
                מדובר באפליקציית אבדות ומציאות למען ארגונים וחברות לצורך ניהול מלאי הפריטים.
            </p>
            <p>
                המערכת תסייע לאיתור אנשים שאיבדו את רכושם או מצאו רכוש של אחרים ולהתאימם למציאות הנמצאים באותו הגוף.
            </p>
            <p>
                המערכת תבצע סריקות בעמודי פייסבוק רלוונטים וכן בפריטים וובפניות החברה ותבצע התאמה בעזרת אלגוריתם ייעודי.
            </p>
        </div>
    </div>

</asp:Content>