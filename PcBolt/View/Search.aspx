<%@ Page Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs"
         Inherits="PcBolt.View.Search" Title="PcBolt keresés" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    <asp:Label Font-Size="Larger" runat="server">Válasszon az alábbi lehetőségek közül!</asp:Label>
    </p>
    <asp:Panel runat="server" GroupingText="Keresési beállítások:" Width="532px">
        <asp:CheckBox ID="chk_alaplap" runat="server" Text="Alaplapok"/>&nbsp
        <asp:CheckBox ID="chk_hdd" runat="server" Text="Merevlemezek"/>&nbsp
        <asp:CheckBox ID="chk_cpu" runat="server" Text="Processzorok"/>&nbsp
        <asp:CheckBox ID="chk_vga" runat="server" Text="Videókártáyk"/>&nbsp
        <asp:CheckBox ID="chk_memoria" runat="server" Text="Memóriák"/>
        <asp:Button Text="Keress" runat="server"/>
    </asp:Panel>
    <p>
    <asp:Label Font-Size="Larger" runat="server">A keresés eredménye:</asp:Label>
    </p>
     <asp:PlaceHolder ID="placeholder1" runat="server">
    
    </asp:PlaceHolder> 
</asp:Content>
