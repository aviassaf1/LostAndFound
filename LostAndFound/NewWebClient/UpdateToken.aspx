﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UpdateToken.aspx.cs" Inherits="NewWebClient.UpdateToken" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">עדכון מפתח בפייסבוק</asp:Label>
    
    
    </div>
    <div class="row" dir="rtl">
        <div>
            <p style="direction: rtl">
                    
                    &nbsp;</p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true"> מפתח:</asp:Label>
                    <asp:TextBox ID="TokenTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
            <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true"> שם החברה:</asp:Label>
                    <asp:TextBox ID="companyNameTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
            <asp:Button ID="OK" runat="server" Text="עדכן מפתח" OnClick="updateTokenButton" style="margin-left: 0px" Width="261px" />
        </div>
        
    </div>       
</asp:Content>
