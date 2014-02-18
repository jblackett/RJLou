<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RJLou.Admin.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="/content/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="section admin_header">
        <div class="container admin_header">
            <a class="admin_nav_link active" href="#">Case</a>
            <a class="admin_nav_link" href="#">Contacts</a>
            <a class="admin_nav_link" href="#">Donations</a>
            <a class="admin_nav_link" href="#">Events</a>
            <a class="admin_nav_link" href="#">Reports</a>
            <div class="float-right">
                <input type="search" name="searchTxt" placeholder="Start searching..." />
                <a class="button" href="#">+</a>
            </div>
        </div>
    </div>
    <div class="section admin_body">
        <div class="container left">
            <table class="changes" cellspacing="0" border="0">
                <tr>
                    <td>All</td>
                    <td>Open</td>
                    <td>Pending Approval</td>
                    <td>Closed</td>
                </tr>
            </table>
            <table cellspacing="0" border="0">
                <thead>
                    <tr>
                        <th>Case ID</th>
                        <th>Name</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>AB5463</td>
                        <td>Jones</td>
                        <td>Open</td>
                    </tr>
                    <tr>
                        <td>HJ7984</td>
                        <td>Smith</td>
                        <td>Open</td>
                    </tr>
                    <tr>
                        <td>JU6574</td>
                        <td>Mathers</td>
                        <td>Open</td>
                    </tr>
                    <tr>
                        <td>TG4563</td>
                        <td>Blackett</td>
                        <td>Open</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="container right">
            <div class="scroll-stick">
                <a class="admin_nav_link" href="#">Case Info</a>
                <a class="admin_nav_link" href="#">Notes</a>
                <a class="admin_nav_link" href="#">Documents</a>
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
        <div style="margin: 0; padding: 0; clear: both;"
    </div>
    </form>
</body>
</html>
