<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RJLou.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
            <tr>
                <td>All</td>
                <td>Open</td>
                <td>Pending Approval</td>
                <td>Closed</td>
            </tr>
        </table>
        <asp:Repeater runat="server" ID="CasesRepeater" OnItemDataBound="CasesRepeater_Databind">
            <HeaderTemplate>
                <table cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th>Case ID</th>
                            <th>Name</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:LinkButton runat="server" ID="CaseButton" OnClick="LoadCase" Text='<%# Eval("CaseID") %>' CommandArgument='<%# Eval("CaseID") %>' /></td>
                    <td><asp:Label ID="Name" runat="server"></asp:Label></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "Status")%></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="container right">
        <asp:UpdatePanel ID="MainContainer" runat="server" Visible="false">
            <ContentTemplate>
                <div class="scroll-stick">
                    <a class="smaller" href="#case_info">Case Info</a>
                    <a class="smaller" href="#victims">Victims</a>
                    <a class="smaller" href="#offenders">Offenders</a>
                    <a class="smaller" href="#affiliates">Affiliates</a>
                    <a class="smaller" href="#notes">Notes</a>
                    <a class="smaller" href="#charges">Charges</a>
                    <a class="smaller" href="#documents">Documents</a>
                </div>
                <div style="clear: both;"></div>
                <h1 id="case_info">Case Info</h1>
                <table class="nothing">
                    <tr>
                        <td>Case ID:</td>
                        <td><asp:TextBox ID="CaseID" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Court ID:</td>
                        <td><asp:TextBox ID="CourtID" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Referral Date:</td>
                        <td><asp:TextBox ID="ReferralDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Referral Number:</td>
                        <td><asp:TextBox ID="ReferralNumber" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Court Date:</td>
                        <td><asp:TextBox ID="CourtDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Date of Final Conference:</td>
                        <td><asp:TextBox ID="DateFinalConference" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Date of Completion:</td>
                        <td><asp:TextBox ID="DateCompletion" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Status:</td>
                        <td><asp:TextBox ID="Status" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>District:</td>
                        <td><asp:TextBox ID="District" runat="server" /></td>
                    </tr>
                </table>
                <h1 id="victims">Victims</h1>
                <div class="inner">
                    <asp:Repeater ID="VictimsRepeater" runat="server" OnItemDataBound="VictimsRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Gender</th>
                                        <th>Race</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="VictimName" runat="server" /></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Gender")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Race")%></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="VictimDeleteButton" OnClick="DeleteVictim" Text="Delete" CommandArgument='<%# Eval("PersonID") %>' /> &nbsp;
                                    <asp:LinkButton runat="server" ID="VictimViewButton" OnClick="ViewVictim" Text="View" CommandArgument='<%# Eval("PersonID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                            <a class="button float-right" href="#">Add Victim</a>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <h1 id="offenders">Offenders</h1>
                <div class="inner">
                    <asp:Repeater ID="OffendersRepeater" runat="server" OnItemDataBound="OffendersRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Gender</th>
                                        <th>Race</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="OffenderName" runat="server" /></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Gender")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Race")%></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="OffenderDeleteButton" OnClick="DeleteOffender" Text="Delete" CommandArgument='<%# Eval("PersonID") %>' /> &nbsp;
                                    <asp:LinkButton runat="server" ID="OffenderViewButton" OnClick="ViewOffender" Text="View" CommandArgument='<%# Eval("PersonID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                            <a class="button float-right" href="#">Add Offender</a>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <h1 id="affiliates">Affiliates</h1>
                <div class="inner">
                    <asp:Repeater ID="AffiliatesRepeater" runat="server" OnItemDataBound="AffiliatesRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Gender</th>
                                        <th>Race</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="AffiliateName" runat="server" /></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Gender")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Race")%></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="AffiliateDeleteButton" OnClick="DeleteAffiliate" Text="Delete" CommandArgument='<%# Eval("PersonID") %>' /> &nbsp;
                                    <asp:LinkButton runat="server" ID="AffiliateViewButton" OnClick="ViewAffiliate" Text="View" CommandArgument='<%# Eval("PersonID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                            <a class="button float-right" href="#">Add Affiliate</a>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <h1 id="notes">Notes</h1>
                <asp:Repeater ID="NotesRepeater" runat="server" OnItemDataBound="NotesRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="inner">
                            <asp:Label ID="NoteAuthorAndDate" runat="server" />
                            <asp:TextBox TextMode="MultiLine" ID="NoteText" runat="server" />
                            <a class="button float-right" href="#">Edit Note</a>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div class="inner">
                            <asp:TextBox TextMode="MultiLine" ID="NewNote" runat="server" />
                            <a class="button float-right" href="#">New Note</a>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
                <h1 id="charges">Charges</h1>
                <div class="inner">
                    <asp:Repeater ID="ChargesRepeater" runat="server" OnItemDataBound="ChargesRepeater_ItemDataBound">
                        <HeaderTemplate>
                                <table cellspacing="0" border="0">
                                    <thead>
                                        <tr>
                                            <th>Charge ID</th>
                                            <th>KRS Code</th>
                                            <th>UOR Code</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#DataBinder.Eval(Container.DataItem, "ChargeID")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "KRSCode")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "UORCode")%></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="ChargeDeleteButton" OnClick="DeleteCharge" Text="Delete" CommandArgument='<%# Eval("ChargeID") %>' /> &nbsp;
                                        <asp:LinkButton runat="server" ID="AffiliateViewButton" OnClick="ViewCharge" Text="View" CommandArgument='<%# Eval("ChargeID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                </table>
                                <a class="button float-right" href="#">Add Charge</a>
                            </FooterTemplate>
                    </asp:Repeater>
                </div>
                <h1 id="documents">Documents</h1>
                <asp:Repeater ID="DocumentsRepeater" runat="server" OnItemDataBound="DocumentsRepeater_ItemDataBound">
                    <ItemTemplate>
                        <p>A FileUploader would go here.</p>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
</asp:Content>