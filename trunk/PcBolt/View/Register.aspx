<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="PcBolt.View.Register" 
         MasterPageFile="~/View/MasterPage.Master" Title="PcBolt regisztráció"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <br />
    <p>A regisztráláshoz adja meg az alábbi adatokat. <br />
       A felhasználó név, teljes név, valamint a jelszó mezők kitöltése kötelező.
    </p>
    <asp:Panel ID="Panel1" runat="server" Width="265px" GroupingText="Regisztráció">
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
                Teljes név:
            </td>
            <td>
                <asp:TextBox ID="tb_teljesNev" runat="server" Width="136px"></asp:TextBox>
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
        <tr>
            <td>
                Jelszó újra:
            </td>
            <td>
                <asp:TextBox ID="tb_password2" runat="server" Width="136px" TextMode="Password"></asp:TextBox>
            </td>
        </tr>     
        <tr>
            <td>
                Irányítószám:
            </td>
            <td>
                <asp:TextBox ID="tb_irszam" runat="server" Width="136px"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td>
                Város:
            </td>
            <td>
                <asp:TextBox ID="tb_varos" runat="server" Width="136px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>
                Utca:
            </td>
            <td>
                <asp:TextBox ID="tb_utca" runat="server" Width="136px"></asp:TextBox>
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
