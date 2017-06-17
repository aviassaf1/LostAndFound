<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewGroups.aspx.cs" Inherits="NewWebClient.ViewGroups" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">צפיה בקבוצות פייסבוק</asp:Label>
    </div>
    <div id = "dvGrid" style ="padding:10px;width:1108px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<asp:GridView ID="GridView1" runat="server"  Width = "434px"
AutoGenerateColumns = "false" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "#6699ff" AllowPaging ="true"  ShowFooter = "false" 
OnPageIndexChanging = "OnPaging" 
PageSize = "8" style="direction: rtl"  >

<Columns>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "שם קבוצה">
    <ItemTemplate>
        <asp:Label ID="groupName" runat="server"
                Text='<%# Eval("GroupName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "id">
    <ItemTemplate>
        <asp:Label ID="groupID" runat="server"
                Text='<%# Eval("GroupID")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="remove" runat="server"
            CommandArgument = '<%# Eval("GroupID")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שברצונך למחוק את קבוצה זו')"
        Text = "מחק" OnClick = "deleteGroup"></asp:LinkButton>
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
