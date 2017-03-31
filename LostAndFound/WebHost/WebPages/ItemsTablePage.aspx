<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemsTablePage.aspx.cs" Inherits="WebHost.WebPages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id = "dvGrid" style ="padding:10px;width:1108px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:GridView ID="GridView1" runat="server"  Width = "550px"
AutoGenerateColumns = "false" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "#6699ff" AllowPaging ="true"  ShowFooter = "true" 
OnPageIndexChanging = "OnPaging" onrowediting="EditItem"
onrowupdating="UpdateItem"  onrowcancelingedit="CancelEdit"
PageSize = "10" style="direction: rtl">
<Columns>

<asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "מזהה פריט">
    <ItemTemplate>
        <asp:Label ID="ItemID" runat="server"
        Text='<%# Eval("CustomerID+++++")%>'></asp:Label>
    </ItemTemplate>
    <FooterTemplate>
        

    </FooterTemplate>
</asp:TemplateField>


<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "סוג">
    <ItemTemplate>
        <asp:Label ID="itemType" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtType" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
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
    </FooterTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "צבע">
    <ItemTemplate>
        <asp:Label ID="itemColors" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtColors" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtColors" runat="server"></asp:TextBox>
    </FooterTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "תאריך">
    <ItemTemplate>
        <asp:Label ID="itemDate" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtDate" TextMode="Date" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtDate" TextMode="Date" runat="server"></asp:TextBox>
    </FooterTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מיקום">
    <ItemTemplate>
        <asp:Label ID="itemLocation" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtLocation" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
    </FooterTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מספר סידורי">
    <ItemTemplate>
        <asp:Label ID="itemSerial" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtSerial" TextMode="Number" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtSerial" TextMode="Number" runat="server"></asp:TextBox>
    </FooterTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "שם מדווח">
    <ItemTemplate>
        <asp:Label ID="contactName" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtContactName" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>
    </FooterTemplate>
</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "טלפון מדווח">
    <ItemTemplate>
        <asp:Label ID="contactPhone" runat="server"
                Text='<%# Eval("ContactName")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtContactPhone" TextMode="Phone" runat="server"
            Text='<%# Eval("ContactName")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtContacPhone" TextMode="Phone" runat="server"></asp:TextBox>
    </FooterTemplate>
</asp:TemplateField>





<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="itemRemove" runat="server"
            CommandArgument = '<%# Eval("CustomerID++")%>'
         OnClientClick = "return confirm('Do you want to delete?')"
        Text = "מחיקה" OnClick = "deleteItem"></asp:LinkButton>
    </ItemTemplate>
    <FooterTemplate>
        <asp:Button ID="btnAdd" runat="server" Text="הוסף פריט"
            OnClick = "AddNewItem" />
    </FooterTemplate>
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
