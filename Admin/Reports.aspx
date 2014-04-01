<%--@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RJLou.Reports" --%>
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
        <asp:Repeater ID="ReportsRepeater" runat="server" DataSourceID="">
            <HeaderTemplate>
                <table border="0">
                    <thead>
                        <tr>
                            <th>FilterItem</th>
                            <th>CaseID</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Label ID="FilterItem" runat="server" Text='<%# Eval("FilterItem") %>'></asp:Label></td>
                    <td><asp:Label ID="CaseID" runat="server" Text='<%# Eval("Case_ID") %>'></asp:Label></td>
                    <td><asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:SqlDataSource ID="StatusSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Status AS [FilterItem], [Case_ID], [Status] FROM [RJL_Case] ORDER BY [FilterItem] desc">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="OffenseSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT [Description] AS [FilterItem], RJL_Case.Status AS [Status], RJL_Case.Case_ID AS [Case_ID] FROM RJL_Case JOIN CASE_CHARGE ON RJL_Case.Case_ID = CASE_CHARGE.Case_ID JOIN CHARGE ON CHARGE.Charge_ID = CASE_CHARGE.Charge_ID ORDER BY RJL_Case.Status desc, Description">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="EthnicitySqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Race AS [FilterItem], C.Status AS [Status], C.Case_ID AS [Case_ID] FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY C.Status desc, Race">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="GenderSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Gender AS [FilterItem], C.Status AS [Status], C.Case_ID AS [Case_ID] FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY C.Status desc, Gender">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="AgeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Date_Of_Birth AS [FilterItem], C.Status AS [Status], C.Case_ID AS [Case_ID] FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY C.Status desc, Date_Of_Birth DESC">
        </asp:SqlDataSource>

    </div>
</asp:Content>
