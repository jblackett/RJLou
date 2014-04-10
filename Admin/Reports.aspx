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

        <asp:Panel ID="DynamicQueryPanel" runat="server">
            <asp:Label ID="invisibleSelectQuery" runat="server" Text="" Visible="false" />
            <asp:Label ID="invisibleFromQuery" runat="server" Text="" Visible ="false" />
            <asp:Label ID="invisibleGroupByQuery" runat="server" Text="" Visible="false" />



        <asp:Panel ID="listPanel1" runat="server">
            <asp:Label ID="List1Label" runat="server" Text="This report is about:"/>
            <asp:RadioButtonList ID="ReportTypeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ReportPrep">
            <asp:ListItem Text ="People" Value="Cases"></asp:ListItem>
                <asp:ListItem Text="Cases" Value="People"></asp:ListItem>
                            </asp:RadioButtonList>
            </asp:Panel>

        <asp:Panel ID="listPanel2" runat="server" Visible ="false">
            <asp:Label ID="List2Label" runat="server" Text="Regarding this Offender Attribute:" />
            <asp:RadioButtonList ID="PersonReportTypeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ReportPrep2">
                        <asp:ListItem Text="Name" Value="Name" />                        
                        <asp:ListItem Text="Age" Value="Age" />
                        <asp:ListItem Text="Race" Value="Race" />
                        <asp:ListItem Text="Gender" Value="Gender" />
                </asp:RadioButtonList>
        </asp:Panel>

                <asp:Panel ID="listPanel3" runat="server" Visible ="false">
            <asp:Label ID="List3Label" runat="server" Text="Regarding this Case attribute" />
                    <asp:RadioButtonList ID="CaseReportTypeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ReportPrep3">
                        <asp:ListItem Text="Status" Value="Status" />
                        <asp:ListItem Text="District" Value="District" />
                        <asp:ListItem Text="Case Manager" Value="CaseManager" />

                    </asp:RadioButtonList>
        </asp:Panel>

            <asp:Panel ID="listPanel4" runat="server" Visible="false">
                <asp:Label ID="List4Label" runat="server" Text="List all entries or as an aggregate?" />
                <asp:RadioButtonList ID="displayTypeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ReportPrep4">
                    <asp:ListItem Text="List all" Value="All" />
                        <asp:ListItem Text="Categorical Count" Value="Count" />                    
                </asp:RadioButtonList>
                <asp:LinkButton ID="GenerateReport" runat="server" Text="Generate" CssClass="button float-left" Visible="false" OnClick="ReportClick" CommandArgument="6" />
            </asp:Panel>
        </asp:Panel>
    </div>
    <div class="container right">
        <asp:Repeater ID="ReportsRepeater" runat="server" DataSourceID="">
            <HeaderTemplate>
                <table border="0">
                    <thead>
                        <tr>
                            <th><asp:Label ID="Header1" runat="server" Text="" /></th>
                            <th><asp:Label ID="Header2" runat="server" Text="" /></th>
                            <th><asp:Label ID="Header3" runat="server" Text="" /></th>
                        </tr>
                    </thead>
                <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:Label ID="ReportLabel1" runat="server" Text='<%# Eval("Column1") %>'></asp:Label></td>
                    <td><asp:Label ID="ReportLabel2" runat="server" Text='<%# Eval("Column2") %>'></asp:Label></td>
                    <td><asp:Label ID="ReportLabel3" runat="server" Text='<%# Eval("Column3") %>'></asp:Label></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:SqlDataSource ID="StatusSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Status AS [Column1], Case_ID AS [Column2], '' AS [Column3] FROM [RJL_Case] ORDER BY [Column1] desc">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="OffenseSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT [Description] AS [Column1], RJL_Case.Status AS [Column3], RJL_Case.Case_ID AS [Column2] FROM RJL_Case JOIN CASE_CHARGE ON RJL_Case.Case_ID = CASE_CHARGE.Case_ID JOIN CHARGE ON CHARGE.Charge_ID = CASE_CHARGE.Charge_ID ORDER BY RJL_Case.Status desc, Description">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="EthnicitySqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Race AS [Column1], C.Status AS [Column3], C.Case_ID AS [Column2] FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY C.Status desc, Race">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="GenderSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT Gender AS [Column1], C.Status AS [Column3], C.Case_ID AS [Column2] FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY C.Status desc, Gender">
        </asp:SqlDataSource>

                <asp:SqlDataSource ID="AgeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="SELECT FLOOR(DATEDIFF(MM,P.Date_Of_Birth,GETDATE())/12) AS [Column1], C.Status AS [Column3], C.Case_ID AS [Column2] FROM RJL_Case C JOIN CASE_FILE CF ON CF.Case_ID = C.Case_ID JOIN PERSON P ON P.Person_ID = CF.Person_ID JOIN OFFENDER O ON O.Person_ID = P.Person_ID ORDER BY C.Status desc, Date_Of_Birth DESC">
        </asp:SqlDataSource>

                        <asp:SqlDataSource ID="DynamicSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RJLouEntities %>" SelectCommand="">
        </asp:SqlDataSource>

    </div>
</asp:Content>
