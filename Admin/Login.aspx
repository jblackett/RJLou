<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RJLou.Admin.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restorative Justice Louisville</title>
    <link rel="stylesheet" type="text/css" href="/content/style.css" />
</head>
<body>
   <form id="form1" runat="server">
    <div class="section header">
            <div class="container header">
                <div class="float-left">
                    <a href="#"><img style="width: 3rem;" src="/images/logo-v2-whiteongreen.svg" /></a>
                </div>
                <div class="float-right" style="margin-top: 1rem;">
                    <a class="nav_link" href="#">About Us</a>
                    <a class="nav_link" href="#">Our Impact</a>
                    <a class="nav_link" href="#">Get Involved</a>
                    <a class="nav_link" href="#">Resources</a>
                    <a class="nav_link" href="#">Contact Us</a>
                    <a class="button" href="#">Login</a>
                </div>
                <div class="container header" style="text-align: center";">
                    <h1><span class="green" >Restorative Justice Louisville</span></h1>
		        </div>
            </div>
        </div>
       <div class="section content">
           <h1> Please Login Below: </h1>
            <div class="container">
            <asp:Label runat="server" ID="ErrorText" Text="Unfortunately, your login attempt has failed." Visible="false" /><br />
            Email Address: <asp:TextBox runat="server" ID="UserName" /><br />
            Password: <asp:TextBox runat="server" ID="Password" TextMode="Password" /><br />
            <asp:Button runat="server" ID="submitButton" Text="Log In" OnClick="verifyInfo" />
        
            </div>
		</div>
    </form>
</body>
</html>