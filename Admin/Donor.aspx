<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Donor.aspx.cs" Inherits="RJLou.Admin.Donor" %>

<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/content/style.css" />
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
</head>
<body>
    <form method="post" action="Default.aspx" id="ctl00">
<div class="aspNetHidden">
<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="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" />
</div>

<script type="text/javascript">
    //<![CDATA[
    var theForm = document.forms['ctl00'];
    if (!theForm) {
        theForm = document.ctl00;
    }
    function __doPostBack(eventTarget, eventArgument) {
        if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
            theForm.__EVENTTARGET.value = eventTarget;
            theForm.__EVENTARGUMENT.value = eventArgument;
            theForm.submit();
        }
    }
    //]]>
</script>


<script src="/WebResource.axd?d=pynGkmcFUV13He1Qd6_TZPkkJNB_JlYks_5cFPRfEjOEHvCxOYCn64gAK7KBHV_V74H_5rhjjS8y7hLED-Qq7Q2&amp;t=635145356180000000" type="text/javascript"></script>


<script src="../Scripts/WebForms/MsAjax/MicrosoftAjax.js" type="text/javascript"></script>
<script type="text/javascript">
    //<![CDATA[
    if (typeof (Sys) === 'undefined') throw new Error('ASP.NET Ajax client-side framework failed to load.');
    //]]>
</script>

<script src="../Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js" type="text/javascript"></script>
<div class="aspNetHidden">

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="1Y/QI7S14Pju96Q5q70z2ZIyJVlsa9jM1jS1vlf+zIwx2bUc+QMiFU1mGHwFkNs0P/8ybFT6FSLlJaeiRk8jng/9z4dXsMlz7dT/y2m1kEf0u09nw8r5o77c+giRP/M8aOBT9nvTY2wgLGylKSiHUHIvjlIaSHRqsNAYp3rtfoLRIq+tuJdxg/2n/et1+0orWw6ECKrJAM7nyYTxq2wfboLOsjDCfvJFkPyeuXvzCeNVv06lJs0k8sJGaffu4zOS12mzU27af2jmv+/Z83rlv/sRCfbkfRHEYJ+uPapZSCAW/iAP60XH2aFbNnRh9xCHnv2H2tzSxtH1agjCy478fs96plnbeA6GAQFeYRkl/Xks1NqcUks9Gd4thIqyGQkr/NsoGH6/GKDR7fsQGtXmMq9uMY6Of1pjSDvfyVJE1SVG4EsTa+fJhYtSwcpMkpjAfmu+eShUdzr28Y/M3H2pv3KwHNahDO6g30iRp3FVtzLebxa18oNLXX/lvOvLgzU4WwoQbghb5GgLjMgtpbRjEW1hvUiXhSQLvD5mvGPjgL1JTsW67vbcTCvq1Ruhqthw9m36rxKSQKhEOr2BsYHVBCx9C9iYD20snPwknNzkRHDiNOVSkSLlwfIY23Tjs5fSdyMs5Yh3brPMkmlDvSu6WBUYfAzCJde565xvzcxSWo/G9nVBLpzm3kAqe1/RmKTbthNbIbRV0YUpbCbWdiShywntra8GwxNiMjyxs2nMp9Xi2Zg8DOHMbcspOm9nDm87QRm4RKZi+V8uKAbHJcyeJQKRjvbTjEqCzXjzt99Amu7a7lFEptvmQDCgnfHKPtJdHOkknLrLiRNzZa6UXAbsXCm51v+3oQrWLyk4UBAO4AjtzqI5LYZNWkmMbymjbWgulnpAvDCRNf0cnWRIBexH9WwXtOyUeGqt+SCRqiKTlACOLHYEVg6Ad0NeGf4VyxBmmHBEhCHxnp3i25xsbfHVOMCjyl3yy8Rxsj4LFejuyf6WQ3nuhMgRNzAK6+b1DiRG/pJnfdjl+PGWAkwT6wdrDBIAfVN0T/ia1ZMkMHJu/P6NvXkAznx338DlYtBvgt7mnKjPjk3m9DUdWpveYI9DBA==" />
</div>
    <div class="section admin_header">
        <div class="container admin_header">
            <a class="admin_nav_link active" href="Default.aspx">Case</a>
            <a class="admin_nav_link" href="contacts.html">Contacts</a>
            <a class="admin_nav_link" href="donations.html">Donations</a>
            <a class="admin_nav_link" href="events.html">Events</a>
            <a class="admin_nav_link" href="reports.html">Reports</a>
            <div class="float-right">
                <input type="search" name="searchTxt" placeholder="Start searching..." />
                <a class="button" href="#">+</a>
            </div>
        </div>
    </div>
    <div class="section admin_body">
        
    <script type="text/javascript">
        //<![CDATA[
        Sys.WebForms.PageRequestManager._initialize('ctl00$MainContent$workingMan', 'ctl00', ['tctl00$MainContent$MainContainer', 'MainContent_MainContainer'], [], [], 90, 'ctl00');
        //]]>
</script>

    <div id="container_left" class="container left">
       <%-- <table class="changes" cellspacing="0" border="0">
            <tr>
                <td><a id="MainContent_CaseSwitchAll" href="javascript:__doPostBack(&#39;ctl00$MainContent$CaseSwitchAll&#39;,&#39;&#39;)">All</a></td>
                <td><a id="MainContent_CaseSwitchOpen" href="javascript:__doPostBack(&#39;ctl00$MainContent$CaseSwitchOpen&#39;,&#39;&#39;)">Open</a></td>
                <td><a id="MainContent_CaseSwitchPending" href="javascript:__doPostBack(&#39;ctl00$MainContent$CaseSwitchPending&#39;,&#39;&#39;)">Pending Approval</a></td>
                <td><a id="MainContent_CaseSwitchClosed" href="javascript:__doPostBack(&#39;ctl00$MainContent$CaseSwitchClosed&#39;,&#39;&#39;)">Closed</a></td>
            </tr>
        </table>--%>
        
                <table cellspacing="0" border="0">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Phone</th>
                            <%--<th>Address</th>--%>
                        </tr>
                    </thead>
                    <tbody>
            
               
            
               
            
                
                
            
                
            
                
            
                
            
                <tr>
                    <td><a id="MainContent_CasesRepeater_CaseButton_6" href="javascript:__doPostBack(&#39;ctl00$MainContent$CasesRepeater$ctl07$CaseButton&#39;,&#39;&#39;)"> </a></td>
                    <td><span id="MainContent_CasesRepeater_Name_6"> </span></td>
                <%--    <td> </td>--%>
                </tr>
            
                <tr>
                    <td><a id="MainContent_CasesRepeater_CaseButton_7" href="javascript:__doPostBack(&#39;ctl00$MainContent$CasesRepeater$ctl08$CaseButton&#39;,&#39;&#39;)"> </a></td>
                    <td><span id="MainContent_CasesRepeater_Name_7"> </span></td>
                 <%--   <td> </td>--%>
                </tr>
            
                <tr>
                    <td><a id="MainContent_CasesRepeater_CaseButton_8" href="javascript:__doPostBack(&#39;ctl00$MainContent$CasesRepeater$ctl09$CaseButton&#39;,&#39;&#39;)"> </a></td>
                    <td><span id="MainContent_CasesRepeater_Name_8"> </span></td>
                   <%-- <td> </td>--%>
                </tr>
            
                <tr>
                    <td><a id="MainContent_CasesRepeater_CaseButton_9" href="javascript:__doPostBack(&#39;ctl00$MainContent$CasesRepeater$ctl10$CaseButton&#39;,&#39;&#39;)"> </a></td>
                    <td><span id="MainContent_CasesRepeater_Name_9"> </span></td>
                 <%--   <td> </td>--%>
                </tr>
            
                    </tbody>
                </table>
            
    </div>
    <div class="container right">
        <div id="MainContent_MainContainer">

                <div class="scroll-stick">
                    <a class="smaller" href="#Donor">Donor</a>
                    <a class="smaller" href="#Donations">Donations</a>
                    <a class="smaller" href="#Events_Attended">Events Attended</a>
                </div>
                <div style="clear: both;"></div>
                <div id="MainContent_CaseUpdatedPanel" class="updatepanel">

                    <p>
                        This case was successfully saved!
                    </p>
                    <span class="x alert">X</span>
                
	</div>
                <h1 id="case_info">Donor Info</h1>
                <table class="nothing">
                    <tr>
                        <td>Name:</td>
                        <td><input name="ctl00$MainContent$CaseID" type="text" value=" " readonly="readonly" id="MainContent_CaseID" /></td>
                    </tr>
                    <tr>
                        <td>Address:</td>
                        <td><input name="ctl00$MainContent$CourtID" type="text" value=" " id="MainContent_CourtID" /></td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td><input name="ctl00$MainContent$ReferralDate" type="text" value=" " id="MainContent_ReferralDate" /></td>
                    </tr>
                   
                </table>
                <a class="button" href="javascript:__doPostBack(&#39;ctl00$MainContent$ctl01&#39;,&#39;&#39;)">Save Donor</a>
                <h1 id="victims">Donations</h1>
                <div class="inner">
                    
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Amount</th>
                                        <th>Event</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        
                            <tr>
                                <td><span id="MainContent_VictimsRepeater_VictimName_0"> </span></td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <a id="MainContent_VictimsRepeater_VictimDeleteButton_0" href="javascript:__doPostBack(&#39;ctl00$MainContent$VictimsRepeater$ctl01$VictimDeleteButton&#39;,&#39;&#39;)">Delete</a> &nbsp;
                                    
                                </td>
                            </tr>
                        
                            <tr>
                                <td><span id="MainContent_VictimsRepeater_VictimName_1"> </span></td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <a id="MainContent_VictimsRepeater_VictimDeleteButton_1" href="javascript:__doPostBack(&#39;ctl00$MainContent$VictimsRepeater$ctl02$VictimDeleteButton&#39;,&#39;&#39;)">Delete</a> &nbsp;
                                    
                                </td>
                            </tr>
                        
                                </tbody>
                            </table>
                            <a class="button float-right" href="#">Add Donation</a>
                        
                </div>
                <h1 id="offenders">Events Attended</h1>
                <div class="inner">
                    
                            <table cellspacing="0" border="0">
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Event</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                        
                            <tr>
                                <td><span id="MainContent_OffendersRepeater_OffenderName_0"> </span></td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <a id="MainContent_OffendersRepeater_OffenderDeleteButton_0" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl01$OffenderDeleteButton&#39;,&#39;&#39;)">Delete</a> &nbsp;
                                    <a id="MainContent_OffendersRepeater_OffenderViewButton_0" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl01$OffenderViewButton&#39;,&#39;&#39;)">View</a>
                                </td>
                            </tr>
                        
                            <tr>
                                <td><span id="MainContent_OffendersRepeater_OffenderName_1"> </span></td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <a id="MainContent_OffendersRepeater_OffenderDeleteButton_1" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl02$OffenderDeleteButton&#39;,&#39;&#39;)">Delete</a> &nbsp;
                                    <a id="MainContent_OffendersRepeater_OffenderViewButton_1" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl02$OffenderViewButton&#39;,&#39;&#39;)">View</a>
                                </td>
                            </tr>
                        
                            <tr>
                                <td><span id="MainContent_OffendersRepeater_OffenderName_2"> </span></td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <a id="MainContent_OffendersRepeater_OffenderDeleteButton_2" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl03$OffenderDeleteButton&#39;,&#39;&#39;)">Delete</a> &nbsp;
                                    <a id="MainContent_OffendersRepeater_OffenderViewButton_2" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl03$OffenderViewButton&#39;,&#39;&#39;)">View</a>
                                </td>
                            </tr>
                        
                            <tr>
                                <td><span id="MainContent_OffendersRepeater_OffenderName_3"> </span></td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <a id="MainContent_OffendersRepeater_OffenderDeleteButton_3" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl04$OffenderDeleteButton&#39;,&#39;&#39;)">Delete</a> &nbsp;
                                    <a id="MainContent_OffendersRepeater_OffenderViewButton_3" href="javascript:__doPostBack(&#39;ctl00$MainContent$OffendersRepeater$ctl04$OffenderViewButton&#39;,&#39;&#39;)">View</a>
                                </td>
                            </tr>
                        
                                </tbody>
                            </table>
                            <a class="button float-right" href="#">Add Event</a>
                        
                </div>
                    
            
</div>
    </div>
    <div style="margin: 0; padding: 0; clear: both;"></div>
    <div id="MainContent_ViewPersonModalPanel" class="modal-background">

        <div class="modal">
            <h1 id="MainContent_ModalName"></h1>
            <table class="nothing">
                <tr>
                    <td>Date of Event:</td>
                    <td><input name="ctl00$MainContent$ModalDateOfBirth" type="text" readonly="readonly" id="MainContent_ModalDateOfBirth" /></td>
                </tr>
                <tr>
                    <td>Address of Event:</td>
                    <td><input name="ctl00$MainContent$ModalGender" type="text" readonly="readonly" id="MainContent_ModalGender" /></td>
                </tr>
                <tr>
                    <td>Time:</td>
                    <td><input name="ctl00$MainContent$ModalRace" type="text" readonly="readonly" id="MainContent_ModalRace" /></td>
                </tr>
             
            </table>
            <a class="button" href="#">Edit Case</a>
            <span class="x popup">X</span>
        </div>
    
</div>
    <script type="text/javascript">
        $('span.x.alert').click(function () {
            $('.updatepanel').removeClass('visible');
        });

        $('span.x.popup').click(function () {
            $('.modal-background').removeClass('visible');
        })
    </script>

    </div>
    <script type="text/javascript">
        $('.scroll-stick > .smaller').click(function () {
            var href = $(this).attr('href').toString();
            var top = $(href).offset();

            $('html, body').animate({
                scrollTop: top.top
            }, 2000);
        });

        $(window).scroll(function () {
            var scrollTop = $(window).scrollTop();
            var minScrolled = $('#MainContent_MainContainer').offset();

            if (scrollTop > minScrolled.top) {
                $('#container_left').addClass('fixed');
                $('.scroll-stick').addClass('fixed');
            }
            else {
                $('#container_left').removeClass('fixed');
                $('.scroll-stick').removeClass('fixed');
            }
        });
    </script>
    </form>
</body>
</html>
