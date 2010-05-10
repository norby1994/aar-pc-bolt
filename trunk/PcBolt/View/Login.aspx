<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PcBolt.View.Login"
         MasterPageFile="~/View/MasterPage.Master" Title="PcBolt belépés"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <br />
    <p>Adja meg a felhasználónevét, illetve a jelszavát.</p>
    <asp:Panel ID="Panel1" runat="server" Width="265px" GroupingText="Belépés">
    <table>
        <tr>
            <td>
                Felhasználó név:
            </td>
            <td>
                <asp:TextBox ID="tb_felhasznaloNev" runat="server" Width="136px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Jelszó:
            </td>
            <td>
                <asp:TextBox  ID="tb_password" runat="server" Width="136px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:Button runat="server" ID="btn_ok" Text="OK" OnClick="btn_ok_Click" />
            </td>
            <td>
                <asp:Button runat="server" ID="btn_cancel" Text="Mégsem" PostBackUrl="~/View/Default.aspx" />
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
