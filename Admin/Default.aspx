<%@ Page MasterPageFile="~/Masterpages/Admin.master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RJLou.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container left">
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
                    <td><%#DataBinder.Eval(Container.DataItem, "CaseID")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "Offender.LastName")%></td>
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
        <div class="scroll-stick">
            <a class="smaller" href="#">Case Info</a>
            <a class="smaller" href="#">Notes</a>
            <a class="smaller" href="#">Documents</a>
        </div>
        <div style="clear: both;"></div>
        <h1 id="case_info">Case Info</h1>
        <table class="nothing">
            <tr>
                <td>Case ID:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Offender Name:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>UOR Code:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Case Manager:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Counselor:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Other:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Victim Name:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>Judge:</td>
                <td><input type="text" /></td>
            </tr>
            <tr>
                <td>District:</td>
                <td><input type="text" /></td>
            </tr>
        </table>
        <h1 id="notes">Notes</h1>
        <div class="inner">
            Nance, Tara; 20 Nov 2013
            <textarea>I typed stuff in here blah blah blah</textarea>
            <a class="button float-right" href="#">Edit Note</a>
        </div>
        <div class="inner">
            Nance, Tara; 20 Nov 2013
            <textarea></textarea>
            <a class="button float-right" href="#">Edit Note</a>
        </div>
        <div class="inner">
            Nance, Tara; 20 Nov 2013
            <textarea></textarea>
            <a class="button float-right" href="#">Edit Note</a>
        </div>
        <div class="inner">
            New Note
            <textarea></textarea>
            <a class="button float-right" href="#">Save Note</a>
        </div>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
</asp:Content>