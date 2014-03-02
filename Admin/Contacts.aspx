<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="RJLou.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />

    //Not sure if top line is quite right. 
    //What I'm doing in this and also in Contacts.aspx.cs is to just rename things so they mirror what 
    //default is basically doing (so instead of Case, I use Person; instead of Cases, I use Persons) and
    //also to attempt to use those template html pages we first used to imagine things

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
                            //Again, based it off the html mock ups we came up with. Do we really want to base
                            //it off Name, Phone, and Address, though? People can have multiple phones and
                            //addresses. Then what would you do to determine which? I propose Name, and at least their
                            //type/title (victim, offender, etc). Maybe for a third, consider email? even if they don't
                            //have one and it's empty for some people, they'll only have one.
                            <th>Name</th>
                            <th>Phone</th>
                            <th>Address</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    //the first line under this is probably okay, but the changes I made to the two lines under 
                    //it I'm really not sure about.
                    <td><asp:LinkButton runat="server" ID="PersonButton" OnClick="LoadPerson" Text='<%# Eval("PersonID") %>' CommandArgument='<%# Eval("PersonID") %>' /></td>
                    <td><asp:Label ID="Phone" runat="server"></asp:Label></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "Address")%></td>
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
                    //On here I made the option to scroll to Contact and Cases....that's what we have
                    //on our template. But what about Guardians? What do we do with them? or they on
                    //a victim or offender contacts page? I'm not sure what to do to resolve this. wouldn't 
                    //different type of contact have a bit of a different page? employees won't have guardians.
                    //Only offenders have a special ID Num given by the court that employees might need to know.
                    //For internal users, do we collect their phone numbers and addresses? Will they need sections for those?
                    //Another thing to bring up, I see we have a CASE MANAGER table in the database to connect
                    //them to their cases I guess. What about connecting facilitators to their cases?
                    <a class="smaller" href="#contact">Contact</a>
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
                    //Again, going off the Person tbale. But what about victims and offenders? They'll have guardians.
                    //Offenders will have their court offender id/num.
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
                </table>
                <asp:LinkButton runat="server" CssClass="button" Text="Save Person" OnClick="SavePerson" />
                <h1 id="cases">Cases</h1>
                <div class="inner">
                    <asp:Repeater ID="CasesRepeater" runat="server" OnItemDataBound="CasesRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        //On the html template page for this, we use Case ID and then there's a column
                                        //called Case that just seems to list persons? I'm going to take a guess based on our
                                        //presentation: I believe she would want a single offender name in that column. She
                                        //said that when you view a case, you should just see one offender of whose case it is
                                        //and that for employees identifying the case they probably just see it as that offender's
                                        //name for the most part in  theirs heads.
                                        <th>ID</th>
                                        <th>Offender</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                //I'll be honest, not sure how this is suppose to set up. Left this section for where we
                                //list cases assigned/involved in, I gues. Going off the html template, we don't need
                                //to delete or view button. Maybe hotlink the ID so you can go to the case page.
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
    
    //Again, don't konw if we really need to be able to view and see a modal box, but instead maybe just
    //hot link to the case page.
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
        //Maybe I'm stupid...this button below was originally Edit Case, but I don't see it anywhere on
        //the case page?
            <a class="button" href="#">Edit Person</a>
            <span class="x popup" runat="server">X</span>
        </div>
    </asp:Panel>
    //Not sure what to do about the second one down there since I'm unsure about the modals.
    //just changed the .removeClass to .removePerson
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removePerson('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        })
    </script>
</asp:Content>