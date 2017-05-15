<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddWorker.aspx.cs" Inherits="NewWebClient.AddWorker" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">הוספת עובד לחברה</asp:Label>
    
    
    </div>
    <div class="row" dir="rtl">
        <div>
            <p style="direction: rtl">
                    
                    &nbsp;</p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true"> שם העובד:</asp:Label>
                    <asp:TextBox ID="workerNameTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">סיסמת העובד:</asp:Label>
                    <asp:TextBox ID="password" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p>
                    <asp:CheckBox runat="server" ID="isManager" />
                    <asp:Label runat="server" AssociatedControlID="isManager"> מנהל ? </asp:Label>
                </p>
            <asp:Button ID="OK" runat="server" Text="הוסף עובד לחברה" OnClick="add_worker_button" style="margin-left: 0px" Width="261px" />
        </div>
        
    </div>       
</asp:Content>
