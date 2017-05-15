<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewCompanies.aspx.cs" Inherits="NewWebClient.ViewCompanies" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">צפייה בחברות</asp:Label>
    </div>
    <div id = "dvGrid" style ="padding:10px;width:1108px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Label ID="item1" runat="server" ></asp:Label>

<asp:GridView ID="GridView1" runat="server"  Width = "550px"
AutoGenerateColumns = "false" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "#6699ff" AllowPaging ="true"  ShowFooter = "false" 
OnPageIndexChanging = "OnPaging" 
PageSize = "5" style="direction: rtl"  >

<Columns>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "שם חברה">
    <ItemTemplate>
        <asp:Label ID="companyName" runat="server"
                Text='<%# Eval("CompanyName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "טלפון">
    <ItemTemplate>
        <asp:Label ID="phone" runat="server"
                Text='<%# Eval("PhoneNumber")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="edit" runat="server"
            CommandArgument = '<%# Eval("CompanyName")%>'
        Text = "ערוך" OnClick = "editCompany"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="remove" runat="server"
            CommandArgument = '<%# Eval("CompanyName")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שברצונך למחוק את החברה')"
        Text = "מחק" OnClick = "deleteCompany"></asp:LinkButton>
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
