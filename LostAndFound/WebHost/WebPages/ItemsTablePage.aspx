﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemsTablePage.aspx.cs" Inherits="WebHost.WebPages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManger1" runat="Server">
            </asp:ScriptManager>
        <div id = "dvGrid" style ="padding:10px;width:1108px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:GridView ID="GridView1" runat="server"  Width = "550px"
AutoGenerateColumns = "false" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "#6699ff" AllowPaging ="true"  ShowFooter = "false" 
OnPageIndexChanging = "OnPaging" onrowediting="EditItem"
onrowupdating="UpdateItem"  onrowcancelingedit="CancelEdit"
PageSize = "10" style="direction: rtl" >
<Columns>

<asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "מזהה פריט">
    <ItemTemplate>
        <asp:Label ID="ItemID" runat="server"
        Text='<%# Eval("CustomerID+++++")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>


<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "סוג">
    <ItemTemplate>
        <asp:Label ID="itemType" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "צבע">
    <ItemTemplate>
        <asp:Label ID="itemColors" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "תאריך">
    <ItemTemplate>
        <asp:Label ID="itemDate" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מיקום">
    <ItemTemplate>
        <asp:Label ID="itemLocation" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מספר סידורי">
    <ItemTemplate>
        <asp:Label ID="itemSerial" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "שם מדווח">
    <ItemTemplate>
        <asp:Label ID="contactName" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "טלפון מדווח">
    <ItemTemplate>
        <asp:Label ID="contactPhone" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>





<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="itemRemove" runat="server"
            CommandArgument = '<%# Eval("CustomerID++")%>'
         OnClientClick = "return confirm('Do you want to delete?')"
        Text = "מחיקה" OnClick = "deleteItem"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>
<asp:CommandField  ShowEditButton="True" EditText="עריכה"/>

</Columns>
<AlternatingRowStyle BackColor="#99ccff"  />
</asp:GridView>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "GridView1" />
</Triggers>
</asp:UpdatePanel>
</div>
</form>
</body>
</html>
