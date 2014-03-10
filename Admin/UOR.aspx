<%@ Page Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="UOR.aspx.cs" Inherits="RJLou.Admin.UOR" %>



<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
            <tr>
                <td><asp:LinkButton runat="server" ID="CodeSwitchAll" OnClick="SwitchCodeList" Text="All" CommandArgument="all" /></td>
                <td><asp:LinkButton runat="server" ID="CodeSwitchOpen" OnClick="SwitchCodeList" Text="KRS" CommandArgument="krs" /></td>
                <td><asp:LinkButton runat="server" ID="CodeSwitchPending" OnClick="SwitchCodeList" Text="UOR" CommandArgument="uor" /></td>
            </tr>
        </table>
        <asp:Repeater runat="server" ID="ChargesRepeater" OnItemDataBound="ChargesRepeater_Databind">
            <HeaderTemplate>
                <table cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th>ChargeID</th>
                            <th>KRS</th>
                            <th>UOR</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:LinkButton runat="server" ID="ChargeButton" OnClick="LoadCharge" Text='<%# Eval("ChargeID") %>' CommandArgument='<%# Eval("ChargeID") %>' /></td>
                    <td><asp:Label ID="Name" runat="server"></asp:Label></td>
                    <!--<td><%#DataBinder.Eval(Container.DataItem, "Status")%></td>-->
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
                        <td>Charge ID:</td>
                        <td><asp:TextBox ID="ChargeID" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>KRS Code:</td>
                        <td><asp:TextBox ID="KRS_Code" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>UOR Code:</td>
                        <td><asp:TextBox ID="UOR_Code" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Description:</td>
                        <td><asp:TextBox ID="Description" runat="server" /></td>
                    </tr>
                </table>
                <asp:LinkButton runat="server" CssClass="button" Text="Save Charge" OnClick="SaveCharge" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
    <!--<asp:Panel ID="ViewPersonModalPanel" runat="server" CssClass="modal-background">
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
    </asp:Panel>-->
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removeClass('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        })
    </script>
</asp:content>
