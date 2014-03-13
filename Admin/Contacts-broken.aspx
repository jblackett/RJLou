<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Contacts-broken.aspx.cs" Inherits="RJLou.Contacts" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />

    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
            <tr>
                <td><asp:LinkButton runat="server" ID="PersonSwitchAll" OnClick="SwitchPersonList" Text="All" CommandArgument="all" /></td>
                <td><asp:LinkButton runat="server" ID="PersonSwitchEmployees" OnClick="SwitchPersonList" Text="Employees" CommandArgument="employees" /></td>
                <td><asp:LinkButton runat="server" ID="PersonSwitchOffenders" OnClick="SwitchPersonList" Text="Offenders" CommandArgument="offenders" /></td>
                <td><asp:LinkButton runat="server" ID="PersonSwitchVictims" OnClick="SwitchPersonList" Text="Victims" CommandArgument="victims" /></td>
            </tr>
        </table>
        <asp:Repeater runat="server" ID="PersonsRepeater" OnItemDataBound="PersonsRepeater_Databind">
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
                    <a class="smaller" href="#contact">Contact</a>
                    <a class="smaller" href="#cases">Cases</a>
                    <a class="smaller" href="#guardians">Guardians</a>
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
                    <tr>
                        <td>Court ID:</td>
                        <td><asp:TextBox ID="CourtID" runat="server" /></td>
                    </tr>
                </table>
                <asp:LinkButton runat="server" CssClass="button" Text="Save Person" OnClick="SavePerson" />
                <h1 id="cases">Cases</h1>
                <div class="inner">
                    <asp:Repeater ID="PersonCasesRepeater" runat="server" OnItemDataBound="PersonCasesRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Case ID</th>
                                        <th>Court ID</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#DataBinder.Eval(Container.DataItem, "CaseID")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "CourtID")%></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Status")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <h1 id="guardians">Guardians</h1>
                <div class="inner">
                    <asp:Repeater ID="GuardiansRepeater" runat="server" OnItemDataBound="GuardiansRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Gender</th>
                                        <th>Phone Number</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><asp:Label ID="GuardianName" runat="server" /></td>
                                <td><%#DataBinder.Eval(Container.DataItem, "Gender")%></td>
                                <td><asp:Label ID="PhoneNumber" runat="server" /></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="GuardianDeleteButton" OnClick="DeleteGuardian" Text="Delete" CommandArgument='<%# Eval("PersonID") %>' /> &nbsp;
                                    <asp:LinkButton runat="server" ID="GuardianViewButton" OnClick="ViewGuardian" Text="View" CommandArgument='<%# Eval("PersonID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                            <a class="button float-right" href="#">Add Guardian</a>
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
            <a class="button" href="#">Edit Person</a>
            <span class="x popup" runat="server">X</span>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removePerson('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        })
    </script>
</asp:Content>