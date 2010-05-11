<%@ Page Language="C#" MasterPageFile="~/View/MasterPage.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs"
         Inherits="PcBolt.View.Details" Title="PcBolt termék részletek" %>
<%@ Reference Control="~/View/AlaplapControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    <asp:Label runat="server" Font-Size="Larger">Termék részletei:</asp:Label>
    </p>
    <asp:PlaceHolder ID="placeholder1" runat="server">
    
    </asp:PlaceHolder>
    <p>
        <asp:Label runat="server" Font-Size="Larger">Hozzászólások:</asp:Label>
    </p>
    <asp:PlaceHolder ID="placeholder2" runat="server">
    
    </asp:PlaceHolder>
    <p>
        <asp:Panel runat="server" Font-Size="Larger">Új hozzászólás írása:</asp:Panel>
    </p>
    <asp:TextBox ID="tb_comment" runat="server" Rows="5" Columns="50" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Button runat="server" Text="Hozzászól" OnClick="btn_ok_Click" />
</asp:Content>
