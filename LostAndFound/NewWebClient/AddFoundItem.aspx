<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFoundItem.aspx.cs" Inherits="NewWebClient.AddFoundItem" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
    
        הוספת אבידה למערכת<br />
    
    </div>
        <p style="direction: rtl">
            &nbsp;</p>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Right" Height="948px" style="margin-left: 465px">
                <p style="direction: rtl">
                סוג הפריט:&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="_TypeList" runat="server" Height="16px" Width="133px">
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
                    <asp:ListItem Text="כפכפים" Value="FLIPLOPS"></asp:ListItem>
                    <asp:ListItem Text="קלסר" Value="FOLDER"></asp:ListItem>
                    <asp:ListItem Text="מטען" Value="CHARGER"></asp:ListItem>
                    <asp:ListItem Text="עגילים" Value="EARINGS"></asp:ListItem>
                    <asp:ListItem Text="טבעת" Value="RING"></asp:ListItem>
                    <asp:ListItem Text="שרשרת" Value="KNECKLESS"></asp:ListItem>
                    <asp:ListItem Text="צמיד"></asp:ListItem>
                    <asp:ListItem Text="אוזניות" Value="HEADPHONES"></asp:ListItem>
                    <asp:ListItem Text="סוג אחר"></asp:ListItem>
                </asp:DropDownList>
            
            </p>
                <p style="direction: rtl">
                    צבע הפריט:&nbsp;&nbsp;&nbsp;
                    <asp:CheckBoxList ID="_ColorsCheckBox" runat="server" Height="190px" Width="376px">
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
                    תאריך האבידה:<asp:Calendar ID="_dateCalendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="14pt" ForeColor="#003399" Height="200px" Width="220px">
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
                    מיקום אבדן הפריט:
                    <asp:TextBox ID="_location" runat="server" style="margin-right: 30px" Width="107px"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    תיאור האבידה:
                    <asp:TextBox ID="_description" runat="server" style="margin-right: 53px" Width="107px" ></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    מספר סידורי:
                    <asp:TextBox ID="_serial" runat="server" style="margin-right: 61px" Width="108px"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    שם מדווח האבידה:
                    <asp:TextBox ID="_contactName" runat="server" style="margin-right: 33px" Width="106px"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    טלפון מדווח האבידה:
                    <asp:TextBox ID="_contactPhone" runat="server" style="margin-right: 23px" Width="107px"></asp:TextBox>
                </p>
            <asp:Button ID="OK" runat="server" Text="הוסף אבידה למערכת" OnClick="Button1_Click" style="margin-left: 0px" Width="261px" />
        </asp:Panel>
</asp:Content>
