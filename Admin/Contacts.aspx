<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="RJLou.Admin.Contacts" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
            <tr>
                <td colspan="2">
                    <asp:LinkButton runat="server" ID="PersonSwitchEmployees" OnClick="SwitchPersonList" Text="Employees" CommandArgument="employees" Width="346px"/>
                </td>
            </tr>
            <tr>
                <td><asp:LinkButton runat="server" ID="PersonSwitchOffenders" OnClick="SwitchPersonList" Text="Offenders" CommandArgument="offenders" Width="173px" /></td>
                <td><asp:LinkButton runat="server" ID="PersonSwitchVictims" OnClick="SwitchPersonList" Text="Victims" CommandArgument="victims" Width="173px" /></td>
            </tr>
            <tr>
                <td><asp:LinkButton runat="server" ID="PersonSwitchAffiliates" OnClick="SwitchPersonList" Text="Affiliates" CommandArgument="affiliates" Width="173px" /></td>
                <td><asp:LinkButton runat="server" ID="PersonSwitchGuardians" OnClick="SwitchPersonList" Text="Guardians" CommandArgument="guardians" Width="173px" /></td>
            </tr>
        </table>
        <asp:Repeater runat="server" ID="PersonsRepeater" OnItemDataBound="PersonsRepeater_ItemDataBound">
            <HeaderTemplate>
                <table cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Phone</th>
                            <th>Address</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:LinkButton runat="server" ID="PersonButton" OnClick="LoadPerson" CommandArgument='<%# Eval("PersonID") %>' /></td>
                    <td><asp:Label ID="Phone" runat="server"></asp:Label></td>
                    <td><asp:Label ID="Address" runat="server"></asp:Label></td>
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
                    <a class="smaller" href="#info">Person Info</a>
                    <a class="smaller" href="#phonenums">Phone Numbers</a>
                    <a class="smaller" href="#addresses">Addresses</a>
                    <a class="smaller" href="#guardians">Guardians</a>
                    <a class="smaller" href="#cases">Cases</a>
                </div>
                <div style="clear: both;"></div>
                <asp:Panel ID="PersonUpdatedPanel" runat="server" CssClass="updatepanel">
                    <p>
                        This person was successfully saved!
                    </p>
                    <span class="x alert">X</span>
                </asp:Panel>
                <h1 id="contact">Contact</h1>
                <asp:Label ID="PersonID" runat="server" Visible="false" />
                <table class="nothing">
                    <tr>
                        <td>First Name:</td>
                        <td><asp:TextBox ID="FirstName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Last Name:</td>
                        <td><asp:TextBox ID="LastName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Date of Birth:</td>
                        <td><asp:TextBox ID="DateOfBirth" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Gender:</td>
                        <td><asp:TextBox ID="Gender" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td><asp:TextBox ID="Email" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Race:</td>
                        <td><asp:TextBox ID="Race" runat="server" /></td>
                    </tr>
                    <tr id="HeaderRelationship" runat="server">
                        <td>Relationship:</td>
                        <td><asp:TextBox ID="Relationship" runat="server" /></td>
                    </tr>
                    <tr id="HeaderPassword" runat="server">
                        <td>New Password:</td>
                        <td><asp:TextBox ID="NewPassword" runat="server" /></td>
                    </tr>
                    <%--<tr id="HeaderUserType" runat="server">
                        <td>Employee Type</td>
                        <td>
                            <asp:DropDownList ID="UserType" runat="server">
                                <asp:ListItem Text="Admin" Value="admin" />
                                <asp:ListItem Text="Case Manager" Value="case manager" />
                                <asp:ListItem Text="Facilitator" Value="facilitator" />
                                <asp:ListItem Text="Volunteer" Value="volunteer" />
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr id="HeaderUserType" runat="server">
                        <td>Employee Type</td>
                        <td><asp:TextBox ID="UserType" runat="server" /></td>
                    </tr>
                    <tr id="HeaderOffenderNumber" runat="server">
                        <td>Offender Number:</td>
                        <td><asp:TextBox ID="OffenderNumber" runat="server" /></td>
                    </tr>
                </table>
                <asp:LinkButton runat="server" CssClass="button" Text="Save Person" OnClick="SavePerson" />
                <h1 id="phonenums">Phone Numbers</h1>
                <div class="inner">
                    <asp:Repeater ID="PhoneNumbersRepeater" runat="server" OnItemDataBound="PhoneNumbersRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Phone Number</th>
                                        <th>Type</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#DataBinder.Eval(Container.DataItem, "Number")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "PType")%></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="PhoneDeleteButton" OnClick="DeletePhone" Text="Delete" CommandArgument='<%# Eval("PhoneID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                            <asp:LinkButton runat="server" ID="PhoneAddButton" OnClick="AddPhone" CssClass="button float-right" Text="Add Phone Number" />
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <h1 id="addresses">Addresses</h1>
                <div class="inner">
                    <asp:Repeater ID="AddressesRepeater" runat="server" OnItemDataBound="AddressesRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Street Address</th>
                                        <th>City</th>
                                        <th>State</th>
                                        <th>Zip</th>
                                        <th>Type</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#DataBinder.Eval(Container.DataItem, "streetAddress")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "city")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "state")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "zip")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "type")%></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="AddressDeleteButton" OnClick="DeleteAddress" Text="Delete" CommandArgument='<%# Eval("AddressID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                            <asp:LinkButton runat="server" ID="AddressAddButton" OnClick="AddAddress" CssClass="button float-right" Text="Add Address" />
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <asp:Panel ID="GuardiansPanel" runat="server">
                    <h1 id="guardians">Guardians</h1>
                    <div class="inner">
                        <asp:Repeater ID="GuardiansRepeater" runat="server" OnItemDataBound="GuardiansRepeater_ItemDataBound">
                            <HeaderTemplate>
                                <table cellspacing="0" border="0">
                                    <thead>
                                        <tr>
                                            <th>Name</th>
                                            <th>Gender</th>
                                            <th>Relationship</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Label ID="GuardianName" runat="server" /></td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "Gender")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "Relationship")%></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="GuardianDeleteButton" OnClick="DeleteGuardian" Text="Delete" CommandArgument='<%# Eval("GuardianID") %>' /> &nbsp;
                                        <asp:LinkButton runat="server" ID="GuardianViewButton" OnClick="ViewGuardian" Text="View" CommandArgument='<%# Eval("PersonID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                </table>
                                <asp:LinkButton runat="server" ID="GuardianAddButton" OnClick="AddGuardian" CssClass="button float-right" Text="Add Guardian" />
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>
                <h1 id="cases">Cases</h1>
                <div class="inner">
                    <asp:Repeater ID="CasesRepeater" runat="server" OnItemDataBound="CasesRepeater_ItemDataBound">
                        <HeaderTemplate>
                                <table cellspacing="0" border="0">
                                    <thead>
                                        <tr>
                                            <th>Court ID</th>
                                            <th>Status</th>
                                            <th>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#DataBinder.Eval(Container.DataItem, "CourtID")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "Status")%></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="CaseViewButton" OnClick="ViewCase" Text="View" CommandArgument='<%# Eval("CaseID") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                </table>
                            </FooterTemplate>
                    </asp:Repeater>
                </div>
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
            <asp:LinkButton ID="ModalClosePerson" runat="server" OnClick="CloseModal" CssClass="button" Text="Close" />
        </div>
    </asp:Panel>
    <asp:Panel ID="ViewCaseModalPanel" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1 id="ModalCaseNumber" runat="server"></h1>
            <table class="nothing">
                <tr>
                    <td>Case ID:</td>
                    <td><asp:TextBox ID="ModalCaseID" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Referral Date:</td>
                    <td><asp:TextBox ID="ModalReferralDate" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Referral Number:</td>
                    <td><asp:TextBox ID="ModalReferralNumber" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Court Date:</td>
                    <td><asp:TextBox ID="ModalCourtDate" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Date of Final Conference:</td>
                    <td><asp:TextBox ID="ModalDateFinalConference" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Date of Completion:</td>
                    <td><asp:TextBox ID="ModalDateCompletion" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>Status:</td>
                    <td><asp:TextBox ID="ModalStatus" runat="server" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td>District:</td>
                    <td><asp:TextBox ID="ModalDistrict" runat="server" ReadOnly="true" /></td>
                </tr>
            </table>
            <asp:LinkButton ID="ModalCloseCase" runat="server" OnClick="CloseModal" CssClass="button" Text="Close" />
        </div>
    </asp:Panel>
    <asp:Panel ID="NewPhoneNumber" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1>New Phone Number</h1>
            <table class="nothing">
                <tr>
                    <td>Phone Number (numbers only):</td>
                    <td><asp:TextBox ID="NewNumber" runat="server" /></td>
                </tr>
                <tr>
                    <td>Type:</td>
                    <td><asp:TextBox ID="NewType" runat="server" /></td>
                </tr>
            </table>
            <asp:LinkButton runat="server" ID="SubmitNewPhoneNumber" OnClick="SavePhone" CssClass="button" Text="Submit" />
        </div>
    </asp:Panel>
    <asp:Panel ID="NewAddressModal" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1>New Address</h1>
            <table class="nothing">
                <tr>
                    <td>Street Address</td>
                    <td><asp:TextBox ID="NewStreetAddress" runat="server" /></td>
                </tr>
                <tr>
                    <td>City</td>
                    <td><asp:TextBox ID="NewCity" runat="server" /></td>
                </tr>
                <tr>
                    <td>State</td>
                    <td><asp:TextBox ID="NewState" runat="server" /></td>
                </tr>
                <tr>
                    <td>Zip</td>
                    <td><asp:TextBox ID="NewZipCode" runat="server" /></td>
                </tr>
                <tr>
                    <td>Type</td>
                    <td><asp:TextBox ID="NewAddressType" runat="server" /></td>
                </tr>
            </table>
            <asp:LinkButton runat="server" ID="SubmitNewAddress" OnClick="SaveAddress" CssClass="button" Text="Submit" />
        </div>
    </asp:Panel>
    <asp:Panel ID="NewGuardianModal" runat="server" CssClass="modal-background">
        <div class="modal">
            <h1>New Guardian</h1>
            <p>Select a guardian from the list below:</p>
            <asp:DropDownList ID="NewGuardian" runat="server"></asp:DropDownList>
            <br />
            <asp:LinkButton runat="server" ID="SubmitNewGuardian" OnClick="SaveGuardian" CssClass="button" Text="Submit" />
            <asp:LinkButton runat="server" ID="CancelNewGuardian" OnClick="CancelGuardian" CssClass="button" Text="Cancel" />
        </div>
    </asp:Panel>
</asp:Content>