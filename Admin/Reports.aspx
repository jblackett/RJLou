<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RJLou.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div class="container left">
        <asp:LinkButton ID="Button1"  runat="server" OnClick="Report1_Click" Text="Case By Status"></asp:LinkButton>
        <asp:LinkButton ID="Button2" runat="server" OnClick="Report2_Click" Text="Case By Offense"></asp:LinkButton>
        <asp:LinkButton ID="Button3" runat="server" OnClick="Report3_Click" Text="Case By Ethnicity"></asp:LinkButton>
        <asp:LinkButton ID="Button4" runat="server" OnClick="Report4_Click" Text="Case By Gender"></asp:LinkButton>
        <asp:LinkButton ID="Button5" runat="server" OnClick="Report5_Click" Text="Case By Age"></asp:LinkButton>
    </div>
    <div class="container right">
        <asp:TextBox ID="TestBox1" runat="server" />
        <asp:TextBox ID="TestBox2" runat="server" />
        <table cellspacing="0" border="0">
            <thead>
                <tr>
                    <th>Status</th>
                    <th>CaseID</th>
                </tr>
            </thead>
        </table>
    </div>
</asp:Content>
