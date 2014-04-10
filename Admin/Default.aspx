<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RJLou.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <div style="margin: 1rem;">
            <input type="search" class="search" name="searchTxt" placeholder="Start searching..." />
            <a href="#" class="button search">Go</a>
        </div>
        <table class="changes" cellspacing="0" border="0">
            <tr>
                <td id="CaseSwitchTDAll" runat="server"><asp:LinkButton runat="server" ID="CaseSwitchAll" OnClick="SwitchCaseList" Text="All" CommandArgument="all" /></td>
                <td id="CaseSwitchTDOpen" runat="server"><asp:LinkButton runat="server" ID="CaseSwitchOpen" OnClick="SwitchCaseList" Text="Open" CommandArgument="open" /></td>
                <td id="CaseSwitchTDPending" runat="server"><asp:LinkButton runat="server" ID="CaseSwitchPending" OnClick="SwitchCaseList" Text="Pending Approval" CommandArgument="pending" /></td>
                <td id="CaseSwitchTDClosed" runat="server"><asp:LinkButton runat="server" ID="CaseSwitchClosed" OnClick="SwitchCaseList" Text="Closed" CommandArgument="closed" /></td>
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
    <div class="container right" id="RightContainer" runat="server">
        <asp:Panel ID="MainContainer" runat="server" Visible="false">
            <div class="scroll-stick">
                <a class="smaller" href="#case_info">Case Info</a>
                <a class="smaller" href="#victims">Victims</a>
                <a class="smaller" href="#offenders">Offenders</a>
                <a class="smaller" href="#affiliates">Affiliates</a>
                <a class="smaller" href="#notes">Notes</a>
                <a class="smaller" href="#charges">Charges</a>
                <a class="smaller" href="#documents">Documents</a>
                <a class="smaller" href="#casemanagers">Employees</a>
            </div>
            <div style="clear: both;"></div>
            <asp:Panel ID="CaseUpdatedPanel" runat="server" CssClass="updatepanel">
                <p>
                    This case was successfully saved!
                </p>
                <span class="x alert">X</span>
            </asp:Panel>
            <h1 id="case_info" class="statusheader" runat="server"></h1>
            <asp:LinkButton ID="AddNewCase" CssClass="button add" runat="server" Text="+" OnClick="AddCase" />
            <div id="statusbar" class="status">
                <h1 id="Status" runat="server"></h1>
                <div class="status dropdown">
                    <asp:LinkButton ID="StatusOpen" runat="server" Text="Open" OnClick="SetStatus" CommandArgument="Open" />
                    <asp:LinkButton ID="StatusPending" runat="server" Text="Pending Approval" OnClick="SetStatus" CommandArgument="Pending" />
                    <asp:LinkButton ID="StatusClosedSuccess" runat="server" Text="Closed" OnClick="SetStatus" CommandArgument="Closed" />
                    <%--<asp:LinkButton ID="StatusClosedFail" runat="server" Text="Closed Unsuccessfully" OnClick="SetStatus" CommandArgument="ClosedFail" />--%>
                </div>
            </div>
            <table class="nothing main">
                <tr>
                    <td>Case ID:</td>
                    <td><asp:TextBox ID="CaseID" runat="server" /></td>
                </tr>
                <tr>
                    <td>Court ID:</td>
                    <td><asp:TextBox ID="CourtID" runat="server" /></td>
                </tr>
                <tr>
                    <td>District:</td>
                    <td><asp:TextBox ID="District" runat="server" /></td>
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
                <%--<tr>
                    <td>Status:</td>
                    <td><asp:TextBox ID="Status" runat="server" /></td>
                </tr>--%>
            </table>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" Text="Save Case" OnClick="SaveCase" />
            <%--<asp:LinkButton runat="server" CssClass="button" Text="Save Case" OnClick="SaveCase" />--%>
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
                        <asp:Label ID="NoteAuthor" runat="server" />
                        <asp:Label ID="NoteDate" runat="server" />
                        <asp:TextBox TextMode="MultiLine" ID="NoteText" runat="server" />
                        <asp:LinkButton ID="EditNoteButton" runat="server" OnClick="EditNote" Text="Edit Note" CommandArgument='<%# Eval("NoteID") %>' CssClass="button float-right" />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <div class="inner">
                        <asp:TextBox TextMode="MultiLine" ID="NewNote" runat="server" />
                        <asp:LinkButton ID="NewNoteButton" runat="server" OnClick="CreateNote" Text="New Note" CssClass="button float-right" />
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
                                    <th>Description</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#DataBinder.Eval(Container.DataItem, "ChargeID")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "UORCode")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Description") %></td>
                            <td>
                                <asp:LinkButton runat="server" ID="ChargeDeleteButton" OnClick="DeleteCharge" Text="Delete" CommandArgument='<%# Eval("ChargeID") %>' /> &nbsp;
                                <asp:LinkButton runat="server" ID="AffiliateViewButton" OnClick="ViewCharge" Text="View" CommandArgument='<%# Eval("ChargeID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                        <asp:LinkButton runat="server" ID="ChargeAddButton" OnClick="AddCharge" Text="Add Charge" CssClass="button float-right" />
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <h1 id="documents">Documents</h1>
            <div class="inner">
                <asp:Repeater ID="DocumentsRepeater" runat="server" OnItemDataBound="DocumentsRepeater_ItemDataBound">
                    <HeaderTemplate>
                        <table cellspacing="0" border="0">
                            <thead>
                                <tr>
                                    <th>Document ID</th>
                                    <th>File Name</th>
                                    <th>Person Who Added</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#DataBinder.Eval(Container.DataItem, "DocumentID")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "FileLocation")%></td>
                            <td><asp:Label ID="DocumentPersonModifier" runat="server" /></td>
                            <td>
                                <asp:HyperLink ID="DocumentViewButton" runat="server" NavigateUrl='<%# "/Documents/" + DataBinder.Eval(Container.DataItem, "FileLocation")%>' Text="Open" /> &nbsp;
                                <asp:LinkButton ID="DocumentDeleteButton" runat="server" OnClick="DeleteDocument" CommandArgument='<%# Eval("DocumentID") %>' Text="Delete" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                        <asp:LinkButton ID="DocumentAddButton" runat="server" OnClick="AddDocument" Text="Add Document" CssClass="button float-right" />
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <h1 id="casemanagers">Affiliated Employees</h1>
            <div class="inner">
                <asp:Repeater ID="CaseManagersRepeater" runat="server" OnItemDataBound="CaseManagersRepeater_ItemDataBound">
                    <HeaderTemplate>
                        <table cellspacing="0" border="0">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Gender</th>
                                    <th>Role</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><asp:Label ID="CaseManagerName" runat="server" /></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "Gender") %></td>
                            <td><asp:Label ID="CaseManagerRole" runat="server" /></td>
                            <td>
                                <asp:LinkButton runat="server" ID="CaseManagerDeleteButton" OnClick="DeleteCaseManager" Text="Delete" CommandArgument='<%# Eval("PersonID") %>' /> &nbsp;
                                <asp:LinkButton runat="server" ID="CaseManagerViewButton" OnClick="ViewCaseManager" Text="View" CommandArgument='<%# Eval("PersonID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                        <asp:LinkButton ID="AddCaseManagerBtn" runat="server" CssClass="button float-right" Text="Attach Employee" OnClick="OpenManagerModalPanel" />
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
    <asp:LinkButton ID="UnloadCaseButton" runat="server" CssClass="undo" OnClick="UnloadCase">
        <img src="/images/arrow-left.png" />
    </asp:LinkButton>
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
                                <asp:TextBox ID="ModalAddress" runat="server" ReadOnly="true" CssClass="address" /> <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <asp:LinkButton ID="ViewPersonModal" runat="server" OnClick="ViewPerson" Text="Edit Person" CssClass="button" />
            <asp:LinkButton ID="ClosePersonModal" runat="server" OnClick="ClosePerson" Text="Cancel" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:Panel ID="AddPersonModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1 id="ModalType" runat="server"></h1>
            <asp:DropDownList ID="NewCasePersonList" runat="server"></asp:DropDownList>
            <asp:LinkButton ID="AddPersonConfirm" runat="server" OnClick="AddPersonToCaseList" Text="Confirm" CssClass="button" />
            <asp:LinkButton ID="AddPersonCancel" runat="server" OnClick="CloseAddPerson" Text="Cancel" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:Panel ID="ViewChargesModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1 id="ModalChargeNumber" runat="server"></h1>
            <table class="nothing">
                <tr>
                    <td>UOR Code:</td>
                    <td><asp:TextBox ID="ModalUORCode" runat="server" /></td>
                </tr>
                <tr>
                    <td>Description:</td>
                    <td><asp:TextBox ID="ModalDescription" runat="server" /></td>
                </tr>
            </table>
            <a href="#" class="button">Edit Charge</a>
            <asp:LinkButton ID="CloseChargesModal" runat="server" OnClick="CloseCharges" Text="Cancel" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:Panel ID="AddChargeModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1>Add New Charge</h1>
            <asp:DropDownList ID="ChargesList" runat="server" /> <br />
            <asp:LinkButton ID="AddNewCharge" runat="server" OnClick="AddChargeToCase" Text="Submit" CssClass="button" />
            <asp:LinkButton ID="CloseAddChargePanel" runat="server" OnClick="CloseAddCharge" Text="Cancel" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:Panel ID="AddDocumentModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1>Upload a New Document</h1>
            <asp:FileUpload ID="ModalFileUploader" runat="server" />
            <asp:LinkButton ID="ModalNewDocument" runat="server" OnClick="UploadDocument" Text="Upload" CssClass="button" />
            <asp:LinkButton ID="ModalCancelDocument" runat="server" OnClick="CloseAddDocument" Text="Cancel" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:Panel ID="NewCaseModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1>Add New Case</h1>
            <table class="nothing">
                <tr>
                    <td>Court ID:</td>
                    <td><asp:TextBox ID="ModalCourtID" runat="server" /></td>
                </tr>
                <tr>
                    <td>District:</td>
                    <td><asp:TextBox ID="ModalDistrict" runat="server" /></td>
                </tr>
                <tr>
                    <td>Referral Date:</td>
                    <td><asp:TextBox ID="ModalReferralDate" runat="server" /></td>
                </tr>
                <tr>
                    <td>Referral Number:</td>
                    <td><asp:TextBox ID="ModalReferralNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td>Court Date:</td>
                    <td><asp:TextBox ID="ModalCourtDate" runat="server" /></td>
                </tr>
                <tr>
                    <td>Date of Final Conference:</td>
                    <td><asp:TextBox ID="ModalDateFinalConf" runat="server" /></td>
                </tr>
                <tr>
                    <td>Date of Completion:</td>
                    <td><asp:TextBox ID="ModalDateCompletion" runat="server" /></td>
                </tr>
                <tr>
                    <td>Status:</td>
                    <td><asp:TextBox ID="ModalStatus" runat="server" /></td>
                </tr>
            </table>
            <asp:LinkButton ID="CreateCase" runat="server" OnClick="CreateNewCase" Text="Save" CssClass="button" />
            <asp:LinkButton ID="CancelCase" runat="server" OnClick="CancelNewCase" Text="Cancel" CssClass="button" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removeClass('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        });

        $('.status').click(function () {
            if (!$(this).hasClass('dropdown'))
                $('.status.dropdown').slideToggle(500);
        });

        $('#cases').addClass('active');
    </script>
</asp:Content>