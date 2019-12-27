<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="empreview.homepage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home page</title>
    <link rel="stylesheet" href="bootstrap.min.css" />
    <script type="" src="jquery.min.js"></script>
    <script type="" src="bootstrap.min.js"></script>
    <link rel="Stylesheet" href="main.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="jumbotron">
            <h1>
                Employee Review Track</h1>
        </div>
        <nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <ul class="nav navbar-nav">
      <li runat="server" ID="homedemo" visible="true" font-size="medium" class="active"><a href="homepage.aspx">Home</a></li>
      <li runat="server" ID="register" visible="true" font-size="medium"><a href="registration.aspx">Register</a></li>
      <li runat="server" ID="review" visible="true" font-size="medium"><a href="reviewpage.aspx">Review</a></li>
      <li runat="server" ID="viewreport" visible="true" font-size="medium"><a href="viewreport.aspx">View report</a></li>
      <li runat="server" ID="chartview" visible="true" font-size="medium"><a href="chartview.aspx">Chart view</a></li>      
       </ul>
      <ul class="nav navbar-nav navbar-right">
      <div class="dropdown">
     <button class="btnn dropdown-toggle" type="button" data-toggle="dropdown" 
              style="border-style: hidden; color: #FFFFFF;">Profile
    <span class="caret"></span></button>
    
    <ul class="dropdown-menu menuu">
      <li><center><h3 class="namelabel">Welcome, 
         <asp:Label ID="Labelname" runat="server" Text=""></asp:Label></h3></center>
          </li>
          <li class="divider"></li>
            <p class="namelabel"> Your role : <asp:Label ID="Labelrole" runat="server" Text=""></asp:Label></p>
          <li class="divider"></li>
      <li><asp:LinkButton  Font-Size="Large" ID="LinkBtnresetpassword" runat="server" 
            data-toggle="modal" data-target="#myModal">Reset password
      </asp:LinkButton></li>
      <li>
    <asp:LinkButton ID="LinkBtnlogout" Font-Size="Large" runat="server" onclick="LinkBtnlogout_Click">    
         Logout</asp:LinkButton></li>
    </ul>
  </div>
    </ul>
     </div>           
</nav>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <center>
                            <h2 class="modal-title">
                                Reset password</h2>
                        </center>
                    </div>
                    <div class="modal-body">
                        <center>
                            <table class="table text-center table-hover table-borderless">
                                <tr>
                                    <td>
                                        New password
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Textnewpassword" class="form-control" runat="server" required=""
                                            Height="30px" Width="167px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Confirm password
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Textconfirmpassword" class="form-control" runat="server" required=""
                                            Height="30px" Width="167px" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password mismatched!"
                                ControlToCompare="Textnewpassword" ControlToValidate="Textconfirmpassword" ForeColor="Red"></asp:CompareValidator>
                            <br />
                        </center>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <asp:Button ID="Btnnewpassword" runat="server" Text="Save and Logout" Height="37px"
                                Width="150px" class="btn btn-success" OnClick="Btnnewpassword_Click" />
                        </center>
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <p class="para1">
            Employee Review Track is a web application used to track employee's review by logging
            in to their account. It is used by the admin or team leader to give their reviews
            of employees working under them. At any time employees can check their reviews given
            to them by their admin or teamleader. Chart view of reviews are also available.
            Reset password option is available in profile button on homepage. Have a nice day.</p>
        <br />
        <br />
        <br />
        <br />
        <div class="footer">
            <hr />
            <i>Employee review track, developed by SIT-CSE</i>
        </div>
    </div>
    </form>
</body>
</html>
