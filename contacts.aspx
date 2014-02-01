<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contacts.aspx.cs" Inherits="RJLou.contacts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="contactForm" runat="server">
    <div id="contactFormWrapper" class="container">
        
        <asp:Label ID="lbl_name" runat="server" Text="Label">Name:</asp:Label>
        <br />
        <asp:TextBox ID="txtBx_name" runat="server">Name:</asp:TextBox>
        
    </div>
    </form>
</body>
</html>
