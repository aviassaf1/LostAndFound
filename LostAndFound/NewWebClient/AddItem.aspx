<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="NewWebClient.AddItem" %>

<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl">
                   
            
        <asp:Label runat="server" Font-Size="Larger" Font-Bold="true">הוספת מציאה למערכת</asp:Label>
    
    </div>
    <div class="row" dir="rtl">
        <asp:Button ID="picFoundItem" runat="server" Text="הוספת מציאה עם תמונה" OnClick="picFoundItem_Click" />
        <asp:Button ID="noPicFoundItem" runat="server" Text="הוספת מציאה בלי תמונה" OnClick="noPicFoundItem_Click" />
        <br />
        <asp:Button ID="picLostItem" runat="server" Text="הוספת אבידה עם תמונה" OnClick="picLostItem_Click" />
        <asp:Button ID="noPicLostItem" runat="server" Text="הוספת אבידה בלי תמונה" OnClick="noPicLostItem_Click" />
    </div>       
</asp:Content>
