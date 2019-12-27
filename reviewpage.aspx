<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reviewpage.aspx.cs" Inherits="empreview.reviewpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Review page</title>
    <link rel="stylesheet" href="bootstrap.min.css" />
    <script type="" src="jquery.min.js"></script>
    <script type="" src="bootstrap.min.js"></script>
    <link rel="Stylesheet" href="main.css" />
    <style type="text/css">
        body
        {
            font-family: Georgia, serif;
            font-size: 20px;
        }
        .jumbotron
        {
            background-color: #7aceef;
            margin-bottom: 0;
        }
        .footer
        {
            text-align: center;
            font-size: 11px;
            left: 0;
            bottom: 0;
            width: 100%;
        }
        .style2
        {
            width: 274px;
        }
        .style3
        {
            width: 550px
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
      <li runat="server" ID="review1" visible="true" font-size="medium" class="active"><a href="reviewpage.aspx">Review</a></li>
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
      <li>
    <asp:LinkButton ID="LinkBtnlogout" Font-Size="Large" runat="server" OnClick="LinkBtnlogout_Click">    
         Logout</asp:LinkButton></li>
    </ul>
  </div>
    </ul> 
  </div>
</nav>
        <center>
            <h1>
                Review</h1>
            <br />
            Select Employee to review :
            <asp:DropDownList ID="DropDownempid" runat="server" OnSelectedIndexChanged="DropDownempid_SelectedIndexChanged"
                autofocus="" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <asp:Label ID="Labelmsg" runat="server" Text=""></asp:Label>
            <br />
            <asp:Table ID="Table1" runat="server" Width="700px" class="table text-center">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><b>Sno</b></asp:TableCell>
                    <asp:TableCell runat="server"><b>Questions</b></asp:TableCell>
                    <asp:TableCell runat="server"><b>Mark</b></asp:TableCell>
                    <asp:TableCell runat="server"><b>Comment</b></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <hr />
            <p>
                    Overall Comment</p>
                <textarea class="form-control" rows="4" cols="100" id="ta" runat="server">
        </textarea>
        <br />
        <asp:Button ID="Btnsubmit" runat="server" Text="Submit"
                class="btn btn-info" Height="31px" Width="66px" 
                onclick="Btnsubmit_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnupdate" runat="server" Text="Update" class="btn btn-info" Height="31px"
                Width="74px" onclick="Btnupdate_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnreset" runat="server" Text="Clear" class="btn btn-info"
                Height="31px" Width="66px" onclick="Btnreset_Click" />
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
