﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddCompany.aspx.cs" Inherits="NewWebClient.AddCompany" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">הוספת חברה למערכת</asp:Label>
    
    
    </div>
    <div class="row" dir="rtl">
        <div>
            <p style="direction: rtl">
                    
                    &nbsp;</p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true"> שם החברה:</asp:Label>
                    <asp:TextBox ID="companyNameTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">מספר טלפון:</asp:Label>
                    <asp:TextBox ID="PhoneTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">מזהה קבוצות פייסבוק:</asp:Label>
                    <asp:TextBox ID="GroupNamesTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                    <asp:Label runat="server" Width="120" Font-Underline="true">שם מנהל החברה:</asp:Label>
                    <asp:TextBox ID="GroupManager" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
                <p style="direction: rtl">
                   <asp:Label runat="server" Width="120" Font-Underline="true">סיסמת מנהל החברה:</asp:Label> 
                    <asp:TextBox ID="GroupManagerPass" runat="server" TextMode="Password" style="margin-right: 30px" Width="120" ControlToValidate="Password"></asp:TextBox>
                </p>
              <p style="direction: rtl">
                   <asp:Label runat="server" Width="120" Font-Underline="true">ID פייסבוק:</asp:Label> 
                    <asp:TextBox ID="FacebookIdTextBox" runat="server" style="margin-right: 30px" Width="120"></asp:TextBox>
                </p>
            <asp:Button ID="OK" runat="server" Text="הוסף חברה" OnClick="Button1_Click" style="margin-left: 0px" Width="261px" />
        </div>
        
    </div>       
</asp:Content>
