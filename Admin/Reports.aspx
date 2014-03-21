<%@ Page Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RJLou.Admin.Reports" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
        </table>
            <HeaderTemplate>
                <table cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th>Report Type</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>Case Report</td>
                    <td>Person Report</td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
    </div>
    <div class="container right">
        <asp:UpdatePanel ID="MainContainer" runat="server" Visible="true">
            <ContentTemplate>
                <div style="clear: both;"></div>
                <asp:Panel ID="CaseUpdatedPanel" runat="server" CssClass="updatepanel">
                    <p>
                        This case was successfully saved!
                    </p>
                    <span class="x alert">X</span>
                </asp:Panel>
                <h1 id="case_info">Report Options</h1>
                <table class="nothing">
                    <tr><td>SORT BY</td></tr>
                        <td>Case Status</td>
                        <td><asp:CheckBox ID="CaseStatus" runat="server" /></td>
                        <td>Case Charge</td>
                        <td><asp:CheckBox ID="CaseCharge" runat="server" /></td>
                        <td>Case District</td>
                        <td><asp:CheckBox ID="CaseDistrict" runat="server" /></td>
                        <td>Offender Race</td>
                        <td><asp:CheckBox ID="CaseRace" runat="server" /></td>
                        <td>Offender Gender</td>
                        <td><asp:CheckBox ID="OffenderGender" runat="server" /></td>
                        <td>Offender Age</td>
                        <td><asp:CheckBox ID="OffenderAge" runat="server" /></td>
                        <td>Offender Zip</td>
                        <td><asp:CheckBox ID="OffenderZip" runat="server" /></td>

                     <tr><td>INFORMATION INCLUDED</td></tr>
                        <td>Case ID</td>
                        <td><asp:CheckBox ID="CaseID" runat="server" /></td>
                        <td>Offender Name</td>
                        <td><asp:CheckBox ID="TOffenderName" runat="server" /></td>
                        <td>Case Status</td>
                        <td><asp:CheckBox ID="Status" runat="server" /></td>
                        <td>Case Charge</td>
                        <td><asp:CheckBox ID="Charge" runat="server" /></td>
                        <td>Case District</td>
                        <td><asp:CheckBox ID="District" runat="server" /></td>
                        <td>Offender Race</td>
                        <td><asp:CheckBox ID="Race" runat="server" /></td>
                        <td>Offender Gender</td>
                        <td><asp:CheckBox ID="Gender" runat="server" /></td>
                        <tr><td>Offender Age</td>
                        <td><asp:CheckBox ID="Age" runat="server" /></td>
                        <td>Offender Zip</td>
                        <td><asp:CheckBox ID="Zip" runat="server" /></td></tr>

                      <tr><td>SORT BY</td></tr>
                        <td>Offender</td>
                        <td><asp:CheckBox ID="OffenderName" runat="server" /></td>
                        <td>Victim</td>
                        <td><asp:CheckBox ID="VictimName" runat="server" /></td>
                    
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removeClass('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        })
    </script>
</asp:content>
