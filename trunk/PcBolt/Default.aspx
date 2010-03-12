<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PcBolt._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AAR PC Bolt Projekt </title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="szoveg">
    <asp:Label Text="szoveg" runat="server" ID="cimke" onload="szovegCsere"></asp:Label>
    
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
    </div>
    <p>
        <asp:TextBox ID="TextBoxKonzol" runat="server" Height="190px" 
            TextMode="MultiLine" Width="490px"></asp:TextBox>
    </p>
    </form>
</body>
</html>
