<%@ Page Title="View Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewMatches.aspx.cs" Inherits="NewWebClient.ViewMatches" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
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

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "מזהה פריט">
    <ItemTemplate>
        <asp:Label ID="matchID" runat="server"
                Text='<%# Eval("MatchID")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField   HeaderText = "סטטוס התאמה">
    <ItemTemplate>
        <asp:Label ID="itemStatus" runat="server"
                Text='<%# Eval("MatchStatus")%>'></asp:Label>
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

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "תיאור">
    <ItemTemplate>
        <asp:Label ID="description" runat="server"
                Text='<%# Eval("Description")%>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="matchCorrect" runat="server"
            CommandArgument = '<%# Eval("MatchID")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שההתאמה נכונה')"
        Text = "ההתאמה נכונה" OnClick = "correctMatch"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="matchDone" runat="server"
            CommandArgument = '<%# Eval("MatchID")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שההתאמה נמסרה')"
        Text = "התבצעה מסירה" OnClick = "doneMatch"></asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="matchRemove" runat="server"
            CommandArgument = '<%# Eval("MatchID")%>'
         OnClientClick = "return confirm('?האם אתה בטוח שברצונך למחוק את ההתאמה')"
        Text = "מחיקה" OnClick = "deleteMatch"></asp:LinkButton>
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
