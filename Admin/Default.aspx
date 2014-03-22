<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RJLou.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
            <tr>
                <td><asp:LinkButton runat="server" ID="CaseSwitchAll" OnClick="SwitchCaseList" Text="All" CommandArgument="all" /></td>
                <td><asp:LinkButton runat="server" ID="CaseSwitchOpen" OnClick="SwitchCaseList" Text="Open" CommandArgument="open" /></td>
                <td><asp:LinkButton runat="server" ID="CaseSwitchPending" OnClick="SwitchCaseList" Text="Pending Approval" CommandArgument="pending" /></td>
                <td><asp:LinkButton runat="server" ID="CaseSwitchClosed" OnClick="SwitchCaseList" Text="Closed" CommandArgument="closed" /></td>
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
                <asp:Panel ID="CaseUpdatedPanel" runat="server" CssClass="updatepanel">
                    <p>
                        This case was successfully saved!
                    </p>
                    <span class="x alert">X</span>
                </asp:Panel>
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
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" Text="Save Case" OnClick="SaveCase" />
                <%--<asp:LinkButton runat="server" CssClass="button" Text="Save Case" OnClick="SaveCase" />--%>
                <asp:LinkButton ID="AddCaseManagerBtn" runat="server" CssClass="button" Text="Add Case Manager" OnClick="OpenManagerModalPanel" />
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
                            <asp:LinkButton runat="server" ID="VictimAddButton" OnClick="AddVictim" CssClass="button float-right" Text="Add Victim" />
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
                            <asp:LinkButton runat="server" ID="OffenderAddButton" OnClick="AddOffender" CssClass="button float-right" Text="Add Offender" />
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
                            <asp:LinkButton runat="server" ID="AffiliateAddButton" OnClick="AddAffiliate" CssClass="button float-right" Text="Add Affiliate" />
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
                                            <th>UOR Code</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#DataBinder.Eval(Container.DataItem, "ChargeID")%></td>
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
    <asp:Panel ID="ViewPersonModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1 id="ModalName" runat="server"></h1>
            <table class="nothing">
                <tr>
                    <td>Date of Birth:</td>
                    <td><asp:TextBox ID="ModalDateOfBirth" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Gender:</td>
                    <td><asp:TextBox ID="ModalGender" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Race:</td>
                    <td><asp:TextBox ID="ModalRace" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Phone Numbers:</td>
                    <td>
                        <asp:Repeater ID="ModalPhoneNumbers" runat="server" OnItemDataBound="ModalPhoneNumbers_ItemDataBound">
                            <ItemTemplate>
                                <asp:TextBox ID="ModalPhoneNum" runat="server" ReadOnly="true" /> <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td>Addresses:</td>
                    <td>
                        <asp:Repeater ID="ModalAddresses" runat="server" OnItemDataBound="ModalAddresses_ItemDataBound">
                            <ItemTemplate>
                                <asp:TextBox ID="ModalAddress" runat="server" ReadOnly="true" Width="300" /> <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <a class="button" href="#">Edit Case</a>
            <span class="x popup" runat="server">X</span>
        </div>
    </asp:Panel>
    <asp:Panel ID="AddPersonModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1 id="ModalType" runat="server"></h1>
            <asp:GridView ID="NewCasePersonList" AllowPaging="true" PageSize="6" OnPageIndexChanging="NewCasePersonList_PageIndexChanging" AutoGenerateColumns="false" runat="server" OnRowDataBound="NewCasePersonList_RowDataBound" CellSpacing="0">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="PersonName" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:BoundField DataField="Race" HeaderText="Race" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="NewPersonAddButton" OnClick="AddPersonToCaseList" Text="Add" CssClass="button" CommandArgument='<%# Eval("PersonID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel ID="addManagerPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h2 id="H1" runat="server">Add a case manager </h2>
            <table class="nothing">
                <tr>
                    <td>Manager to Add:</td>
                    <td>
                            <asp:DropDownList ID="ManagerDropDown" runat="server" width ="200px">
                            </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Current Managers:</td>
                    <td><asp:DropDownList ID="ddlCurrentManagers" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                </table>
            <asp:LinkButton ID="LinkButton2" runat="server" Text="Add" OnClick="AddManager"  CssClass="button"/>
            <asp:LinkButton ID="LinkButton3" runat="server" Text="Close" OnClick="CloseManagerModalPanel" CssClass="button" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removeClass('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        })
    </script>
</asp:Content>