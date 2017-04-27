<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFacebookGroup.aspx.cs" Inherits="NewWebClient.AddFacebookGroup" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">הוספת קבוצת פייסבוק למערכת</asp:Label>
    
    
    </div>
    <div class="row" dir="rtl">
        <div>
            <p style="direction: rtl">
                    
                    &nbsp;</p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true"> מספר קבוצת פייסבוק:</asp:Label>
                    <asp:TextBox ID="FbIdTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    &nbsp;</p>
                <p style="direction: rtl">
                    &nbsp;</p>
                <p style="direction: rtl">
            <asp:Button ID="OK" runat="server" Text="הוסף קבוצת פייסבוק למערכת" OnClick="Button1_Click" style="margin-left: 0px" Width="261px" />
            </p>
                <p style="direction: rtl">
                    &nbsp;</p>
              <p style="direction: rtl">
                   &nbsp;</p>
        </div>
        
    </div>       
</asp:Content>
