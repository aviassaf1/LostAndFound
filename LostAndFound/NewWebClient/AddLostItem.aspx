<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLostItem.aspx.cs" Inherits="NewWebClient.AddLostItem" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">הוספת אבידה למערכת</asp:Label>
    
    
    </div>
    <div class="row" dir="rtl">
        <div class="col-md-4">
            <p style="direction: rtl">
                    
                    <asp:Label runat="server" Width="100" Font-Underline="true">תאריך האבידה:</asp:Label>


                    <asp:Calendar ID="_dateCalendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="14pt" ForeColor="#003399" Height="200px" Style="margin-left:auto" Width="278px">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" BorderColor="#FF3300" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true"> מיקום אבדן הפריט:</asp:Label>
                    <asp:TextBox ID="_location" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">תיאור האבידה:</asp:Label>
                    <asp:TextBox ID="_description" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">מספר סידורי:</asp:Label>
                    <asp:TextBox ID="_serial" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">שם מדווח האבידה:</asp:Label>
                    <asp:TextBox ID="_contactName" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                   <asp:Label runat="server" Width="120" Font-Underline="true">טלפון מדווח האבידה:</asp:Label> 
                    <asp:TextBox ID="_contactPhone" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
            <asp:Button ID="OK" runat="server" Text="הוסף אבידה למערכת" OnClick="Button1_Click" style="margin-left: 0px" Width="261px" />
        </div>
        <div class="col-md-6">
            <p style="direction: rtl" id="panel1" runat="server">
                <asp:Label runat="server" Width="60" Font-Underline="true">סוג פריט</asp:Label>
               
                <asp:DropDownList ID="_TypeList" runat="server" Height="16px" Width="133px" style="direction:rtl">
                    <asp:ListItem Text="תעודה" Value="ID"></asp:ListItem>
                    <asp:ListItem Text="ארנק" Value="WALLET"></asp:ListItem>
                    <asp:ListItem Text="עכבר מחשב" Value="PCMOUSE"></asp:ListItem>
                    <asp:ListItem Text="מחשב" Value="PC"></asp:ListItem>
                    <asp:ListItem Text="טלפון" Value="PHONE"></asp:ListItem>
                    <asp:ListItem Text="מפתחות" Value="KEYS"></asp:ListItem>
                    <asp:ListItem Text="תיק" Value="BAG"></asp:ListItem>
                    <asp:ListItem Text="מטרייה" Value="UMBRELLA"></asp:ListItem>
                    <asp:ListItem Text="סווטשרט" Value="SWEATSHIRT"></asp:ListItem>
                    <asp:ListItem Text="קלמר" Value="PENCILCASE"></asp:ListItem>
                    <asp:ListItem Text="משקפיים" Value="GLASSES"></asp:ListItem>
                    <asp:ListItem Text="נעליים" Value="SHOES"></asp:ListItem>
                    <asp:ListItem Text="כפכפים" Value="FLIPFLOPS"></asp:ListItem>
                    <asp:ListItem Text="קלסר" Value="FOLDER"></asp:ListItem>
                    <asp:ListItem Text="מטען" Value="CHARGER"></asp:ListItem>
                    <asp:ListItem Text="עגילים" Value="EARINGS"></asp:ListItem>
                    <asp:ListItem Text="טבעת" Value="RING"></asp:ListItem>
                    <asp:ListItem Text="שרשרת" Value="NECKLACE"></asp:ListItem>
                    <asp:ListItem Text="צמיד"></asp:ListItem>
                    <asp:ListItem Text="אוזניות" Value="HEADPHONES"></asp:ListItem>
                    <asp:ListItem Text="סוג אחר"></asp:ListItem>
                </asp:DropDownList>
            
            </p>
                <p style="direction: rtl">
                  <asp:Label runat="server" Width="100" Font-Underline="true">צבע הפריט:</asp:Label>

                    <asp:CheckBoxList ID="_ColorsCheckBox" Width="300" runat="server" Height="190px" Style="margin-left:auto">
                        <asp:ListItem Text="ורוד" Value="PINK"></asp:ListItem>
                        <asp:ListItem Text="שחור" Value="BLACK"></asp:ListItem>
                        <asp:ListItem Text="כחול" Value="BLUE"></asp:ListItem>
                        <asp:ListItem Text="אדום" Value="RED"></asp:ListItem>
                        <asp:ListItem Text="ירוק" Value="GREEN"></asp:ListItem>
                        <asp:ListItem Text="צהוב" Value="YELLOW"></asp:ListItem>
                        <asp:ListItem Text="לבן" Value="WHITE"></asp:ListItem>
                        <asp:ListItem Text="סגול" Value="PURPLE"></asp:ListItem>
                        <asp:ListItem Text="כתום" Value="ORANGE"></asp:ListItem>
                        <asp:ListItem Text="אפור" Value="GRAY"></asp:ListItem>
                        <asp:ListItem Text="חום" Value="BROWN"></asp:ListItem>
                        <asp:ListItem Text="זהב" Value="GOLD"></asp:ListItem>
                        <asp:ListItem Text="כסוף" Value="SILVER"></asp:ListItem>
                        <asp:ListItem Text="צבע אחר"></asp:ListItem>
                    </asp:CheckBoxList>
                </p>
            <p style="direction: rtl">
                <asp:Button ID="imageChoosingButton" runat="server" Text="בחר תמונה" OnClick="imageChoosingButton_Click" /></p>
        </div>
    </div>       
</asp:Content>
