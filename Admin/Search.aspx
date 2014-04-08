<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="RJLou.Admin.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 <form id="form1" runat="server">
    <div>  
   <table>
    <tr>
    <td> 
       Search
        </td>
        <td>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
        <td> 
        <asp:Button ID="Button1" runat="server" Text="Go" OnClick="search1" />
        <asp:Label ID="errorlabel" runat="server" Text="" />
        </td>
        
        </tr>
 
</table>
<table><tr><td><p><asp:Label ID="Label2" runat="server" Text=""></asp:Label>  </p></td></tr></table>
 
        <asp:GridView ID="GridView1" runat="server" ></asp:GridView> 
        <asp:GridView ID="GridView2" runat="server" ></asp:GridView> 
        <asp:GridView ID="GridView3" runat="server" ></asp:GridView> 
        <asp:GridView ID="GridView4" runat="server" ></asp:GridView> 
        <asp:GridView ID="GridView5" runat="server" ></asp:GridView> 
        <asp:GridView ID="GridView6" runat="server" ></asp:GridView> 
        <asp:GridView ID="GridView7" runat="server" ></asp:GridView>   
    </div>
 </form>
</body>
</html>
