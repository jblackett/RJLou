﻿<%@ Page Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="UOR.aspx.cs" Inherits="RJLou.Admin.UOR" %>



<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat='server' ID='workingMan' />
    <div id="container_left" class="container left">
        <table class="changes" cellspacing="0" border="0">
        </table>
        <asp:Repeater runat="server" ID="ChargesRepeater" OnItemDataBound="ChargesRepeater_Databind">
            <HeaderTemplate>
                <table cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th>ChargeID</th>
                            <th>UOR</th>
                            <th>Desc.</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><asp:LinkButton runat="server" ID="ChargeButton" OnClick="LoadCharge" Text='<%# Eval("ChargeID") %>' CommandArgument='<%# Eval("ChargeID") %>' /></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "UORCode")%></td>
                    <td><asp:Label ID="ChargeID" runat="server"></asp:Label></td>
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
                <asp:Panel ID="ChargeUpdatedPanel" runat="server" CssClass="updatepanel">
                    <p>
                        This charge update was successfully saved!
                    </p>
                    <span class="x alert" onclick="CloseUpdatePanel">X</span>
                </asp:Panel>
                <h1 id="case_info">Charge Info</h1>
                <table class="nothing">
                    <tr>
                        <td>Charge ID:</td>
                        <td><asp:TextBox ID="ChargeID" runat="server" /></td>
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
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removeClass('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        });

        $('#charges').addClass('active');
    </script>
</asp:content>
