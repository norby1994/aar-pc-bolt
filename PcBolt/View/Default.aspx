<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PcBolt.View.Default"
         MasterPageFile="~/View/MasterPage.Master" Title="PcBolt főoldal"%>
<%@ Reference Control="~/View/AlaplapControl.ascx" %>
<%@ Register TagPrefix="pcb" TagName="AlaplapControl" Src="~/View/AlaplapControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <br />       
    <asp:PlaceHolder runat="server" ID="p1">
    </asp:PlaceHolder>
    <asp:Label Font-Size="Large" ID="lbl_welcome" runat="server"></asp:Label>  
</asp:Content>