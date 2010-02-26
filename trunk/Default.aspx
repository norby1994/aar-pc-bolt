<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplicationElsoProba._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script runat="server">
        public void gombClick(object forras, EventArgs e)
        {
            cimke.Text = "megnyomta a gombot";
            
            
        }
    </script>
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
   
        <asp:Button ID="gomb"
        runat="server"
        Text="gomb felirat"
        OnClick="gombClick" />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="cimke"
        runat="server" />
    </form>
</body>
</html>
