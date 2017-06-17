<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewWorkers.aspx.cs" Inherits="NewWebClient.ViewWorkers" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">    
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">צפייה בעובדים</asp:Label>
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
PageSize = "8" style="direction: rtl"  >

<Columns>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "שם העובד">
    <ItemTemplate>
        <asp:Label ID="userName" runat="server"
                Text='<%# Eval("UserName")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "האם מנהל?">
    <ItemTemplate>
        <asp:Label ID="isManager" runat="server"
                Text='<%# Eval("IsManager")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="remove" runat="server"
            CommandArgument = '<%# Eval("UserName")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שברצונך למחוק את החברה')"
        Text = "מחק" OnClick = "deleteWorker"></asp:LinkButton>
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
