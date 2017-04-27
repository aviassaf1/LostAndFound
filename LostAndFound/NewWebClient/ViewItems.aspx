<%@ Page Title="View Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewItems.aspx.cs" Inherits="NewWebClient.ViewItems" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
        <div id = "dvGrid" style ="padding:10px;width:1108px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:GridView ID="GridView1" runat="server"  Width = "550px"
AutoGenerateColumns = "false" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "#6699ff" AllowPaging ="true"  ShowFooter = "false" 
OnPageIndexChanging = "OnPaging" 
PageSize = "10" style="direction: rtl" >
<Columns>

<asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "מזהה פריט" runat="server" >
    <ItemTemplate>
        <asp:Label ID="ItemID" runat="server"
        Text='<%# Eval("ItemID")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>


<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "סוג">
    <ItemTemplate>
        <asp:Label ID="itemType" runat="server"
                Text='<%# Eval("ItemType")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "צבע">
    <ItemTemplate>
        <asp:Label ID="itemColors" runat="server"
                Text='<%# Eval("Colors")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "תאריך">
    <ItemTemplate>
        <asp:Label ID="itemDate" runat="server"
                Text='<%# Eval("Date")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מיקום">
    <ItemTemplate>
        <asp:Label ID="itemLocation" runat="server"
                Text='<%# Eval("Location")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מספר סידורי">
    <ItemTemplate>
        <asp:Label ID="itemSerial" runat="server"
                Text='<%# Eval("SerialNumber")%>'></asp:Label>
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
                Text='<%# Eval("ContactPhone")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "סטטוס">
    <ItemTemplate>
        <asp:Label ID="status" runat="server"
                Text='<%# Eval("Status")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "תיאור">
    <ItemTemplate>
        <asp:Label ID="description" runat="server"
                Text='<%# Eval("Description")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>







<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "התאמות">
    <ItemTemplate>
        <asp:LinkButton ID="viewItemMatches" runat="server"
            CommandArgument = '<%# Eval("ItemID")%>'
        Text = "צפה" OnClick = "viewItemMatches" ></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="itemRemove" runat="server"
            CommandArgument = '<%# Eval("ItemID")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שברצונך למחוק את הפריט')"
        Text = "מחיקה" OnClick = "deleteItem2"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "עריכה">
    <ItemTemplate>
        <asp:LinkButton ID="editItem" runat="server"
            CommandArgument = '<%# Eval("ItemID")%>'
        Text = "ערוך"  onClick="EditItem2"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

</Columns>
<AlternatingRowStyle BackColor="#99ccff"  />
</asp:GridView>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "GridView1" />
</Triggers>
</asp:UpdatePanel>
</div>
</asp:Content>
