<%@ Page Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" 
         Inherits="PcBolt.View.Basket" Title="PcBolt kosár" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label runat="server" ID="label_nev"></asp:Label>
    </p>
   <asp:PlaceHolder ID="placeholder1" runat="server">
    
    </asp:PlaceHolder> 
    <p>
        <asp:Label runat="server" ID="label_ar"></asp:Label>
    </p>
    <asp:Button runat="server" ID="btn_order" Text="Rendel" OnClick="btn_order_Click"/>
</asp:Content>
