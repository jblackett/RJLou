<%@ Page Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RJLou.Admin.Login1" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="section">
        <div class="container">
            <asp:Panel runat="server" DefaultButton="submitButton">
            <h1>Please Log In</h1>
            <asp:Label runat="server" ID="ErrorText" Text="Unfortunately, your login attempt has failed." Visible="false" /><br />
            
            Email Address:<br /> 
            <asp:TextBox runat="server" ID="Email" /><br />
            
            Password: <br />
            <asp:TextBox runat="server" ID="Password" TextMode="Password" /><br />
            
            <asp:LinkButton runat="server" ID="submitButton" CssClass="button" Text="Log In" OnClick="verifyInfo" />
                </asp:Panel>
        </div>
    </div>
</asp:Content>