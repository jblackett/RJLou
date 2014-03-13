<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="RJLou.Admin.Contacts" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <asp:UpdatePanel ID="ContainerLeftUpdatePanel" runat="server">
            <ContentTemplate>
                <table class="changes" cellspacing="0" border="0">
                    <tr>
                        <td colspan="2">
                            <asp:LinkButton runat="server" ID="PersonSwitchEmployees" OnClick="SwitchPersonList" Text="Employees" CommandArgument="employees" />
                        </td>
                    </tr>
                    <tr>
                        <td><asp:LinkButton runat="server" ID="PersonSwitchOffenders" OnClick="SwitchPersonList" Text="Offenders" CommandArgument="offenders" /></td>
                        <td><asp:LinkButton runat="server" ID="PersonSwitchVictims" OnClick="SwitchPersonList" Text="Victims" CommandArgument="victims" /></td>
                    </tr>
                    <tr>
                        <td><asp:LinkButton runat="server" ID="PersonSwitchAffiliates" OnClick="SwitchPersonList" Text="Affiliates" CommandArgument="affiliates" /></td>
                        <td><asp:LinkButton runat="server" ID="PersonSwitchGuardians" OnClick="SwitchPersonList" Text="Guardians" CommandArgument="guardians" /></td>
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
            </ContentTemplate>
        </asp:UpdatePanel>
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
                                        <asp:LinkButton runat="server" ID="GuardianDeleteButton" OnClick="DeleteGuardian" Text="Delete" CommandArgument='<%# Eval("PersonID") %>' /> &nbsp;
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
</asp:Content>