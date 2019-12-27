<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewreport.aspx.cs" Inherits="empreview.viewreport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View report</title>
    <link rel="stylesheet" href="bootstrap.min.css" />
    <script type="" src="jquery.min.js"></script>
    <script type="" src="bootstrap.min.js"></script>
    <link rel="Stylesheet" href="main.css" />
    <style type="text/css">
        body
        {
            font-family: Georgia;
            font-size: 20px;
        }
        .footer
        {
            text-align: center;
            font-size: 11px;
            left: 0;
            bottom: 0;
            width: 100%;
        }
        .jumbotron
        {
            background-color: #7aceef;
            font-weight: normal;
            width: 100%;
            margin-bottom: 0;
        }
        #DropDownList1
        {
            color: black;
        }
        .style1
        {
            width: 510px;
        }
        .style2
        {
            width: 105px;
        }
        .style3
        {
            width: 373px;
        }
    </style>
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
      <li runat="server" ID="homedemo" visible="true" font-size="medium"><a href="homepage.aspx">Home</a></li>
      <li runat="server" ID="register" visible="true" font-size="medium"><a href="registration.aspx">Register</a></li>
      <li runat="server" ID="review" visible="true" font-size="medium"><a href="reviewpage.aspx">Review</a></li>
      <li runat="server" ID="viewreport1" visible="true" font-size="medium" class="active"><a href="viewreport.aspx">View report</a></li>
      <li runat="server" ID="chartview" visible="true" font-size="medium"><a href="chartview.aspx">Chart view</a></li>
      </ul>
      <ul class="nav navbar-nav navbar-right">
      <div class="dropdown">
     <button class="btnn dropdown-toggle" type="button" data-toggle="dropdown" 
              style="border-style: hidden; color: #FFFFFF;">Profile
    <span class="caret"></span></button>
    
    <ul class="dropdown-menu menuu">
      <li><center><h3 class="namelabel">Welcome, 
         <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label></h3></center>
          </li>
          <li class="divider"></li>
            <p class="namelabel"> Your role : <asp:Label ID="roleLabel" runat="server" Text=""></asp:Label></p>
          <li class="divider"></li>     
      <li>
    <asp:LinkButton ID="LinkBtnlogout" Font-Size="Large" runat="server" OnClick="LinkBtnlogout_Click">    
         Logout</asp:LinkButton></li>
    </ul>
  </div>
    </ul> 
  </div>
</nav>
        <center>
            <h2>
                View report</h2>
            <asp:Label ID="Labelselectemp" runat="server" Text=""></asp:Label>
            &nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server" Visible="false" AutoPostBack="True"
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <asp:Label ID="Labelerror" runat="server" Text=""></asp:Label>
            <br />
            <div class="container">
                <table class="table table-hover">
                    <tr>
                        <th class="style1">
                            Employee ID
                        </th>
                        <td>
                            <asp:Label ID="Labelempid" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th class="style1">
                            Name
                        </th>
                        <td>
                            <asp:Label ID="Labelname" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th class="style1">
                            Role
                        </th>
                        <td>
                            <asp:Label ID="Labelrole" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th class="style1">
                            Given by
                        </th>
                        <td>
                            <asp:Label ID="Labelgivenby" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th class="style1">
                            Date
                        </th>
                        <td>
                            <asp:Label ID="Labeldate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <center>
                    <i>Marks and reviews</i></center>
                <br />
                <asp:Table ID="Table1" runat="server" class="table table-hover" GridLines="Vertical">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server"><b>Sno</b></asp:TableCell>
                        <asp:TableCell runat="server"><b>Questions</b></asp:TableCell>
                        <asp:TableCell runat="server"><b>Marks</b></asp:TableCell>
                        <asp:TableCell runat="server"><b>Comments</b></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
            <br />
            <div class="form-group">
                <p>
                    Overall comment</p>
                <textarea class="form-control" rows="4" cols="100" id="ta" runat="server" disabled="disabled">
</textarea></div>
            <br />
            <div class="footer">
                <br />
                <br />
                <hr />
                <i>Employee review track, developed by SIT-CSE</i>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
