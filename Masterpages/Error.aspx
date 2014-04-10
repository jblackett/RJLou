<%@ Page Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="RJLou.Masterpages.Error" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Oh Noes!</h1>
        <p>
            You've encountered an error! It's possible that you just entered some information in wrong. If I
            were you, I would just go back <a href="/admin/">here</a>, and try starting over.
        </p>
    </div>
</asp:Content>