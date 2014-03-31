<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RJLou.Reports" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <asp:LinkButton runat="server" ID="Report1" OnClick="ReportClick" Text="Case By Status" CommandArgument="1"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="Report2" OnClick="ReportClick" Text="Case By Offense" CommandArgument="2"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="Report3" OnClick="ReportClick" Text="Case By Ethnicity" CommandArgument="3"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="Report4" OnClick="ReportClick" Text="Case By Gender" CommandArgument="4"></asp:LinkButton>
        <asp:LinkButton runat="server" ID="Report5" OnClick="ReportClick" Text="Case By Age" CommandArgument="5"></asp:LinkButton>
    </div>
    <div class="container right">
        <asp:Repeater ID="ReportsRepeater" runat="server">
            <HeaderTemplate>
                <table border="0">
                    <thead>
                        <tr>
                            <th>FilterItem</th>
                            <th>Case ID</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#DataBinder.Eval(Container.DataItem, "FilterItem") %>
                        <%#DataBinder.Eval(Container.DataItem, "Case ID") %>
                        <%#DataBinder.Eval(Container.DataItem, "Status") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
