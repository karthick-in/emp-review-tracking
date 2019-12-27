<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="empreview.registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <link rel="stylesheet" href="bootstrap.min.css" />
    <script type="" src="jquery.min.js"></script>
    <script type="" src="bootstrap.min.js"></script>
    <link href="main.css" rel="stylesheet" type="text/css" />
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
            height: 150px;
            margin-bottom: 0;
        }
        .style1
        {
            width: 499px;
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
      <li runat="server" ID="register" visible="true" font-size="medium" class="active"><a href="registration.aspx">Register</a></li>
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
                Registration page</h1>
            <br />
            <asp:Label ID="Labelmsg" runat="server" Text=""></asp:Label>
            <table cellpadding="30" class="table table-hover text-center">
                <tr>
                    <td class="style1">
                        Employee ID
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="Textempid" class="form-control" runat="server" required="" Height="30px"
                            Width="197px" AutoPostBack="True" OnTextChanged="Textempid_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Role
                    </td>
                    <td class="style4">
                        <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" Height="30px"
                            Width="197px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>Admin</asp:ListItem>
                            <asp:ListItem>Team leader</asp:ListItem>
                            <asp:ListItem>Employee</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Name
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="Textname" class="form-control" runat="server" required="" Height="30px"
                            Width="197px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Email
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="Textemail" runat="server" class="form-control" type="email" Height="30px"
                            Width="197px" required=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Password
                    </td>
                    <td class="style4">
                        <asp:TextBox ID="Textpassword" class="form-control" runat="server" type="password"
                            Height="30px" Width="197px" required=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Designation
                    </td>
                    <td>
                        <asp:DropDownList class="form-control" ID="DropDownList2" runat="server" Height="30px"
                            Width="197px">
                            <asp:ListItem>developer</asp:ListItem>
                            <asp:ListItem>tester</asp:ListItem>
                            <asp:ListItem>teamleader</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        Date of joining
                    </td>
                    <td>
                        <asp:TextBox ID="Textdatepick" class="form-control" runat="server" placeholder="mm/dd/yyyy"
                            required="" Height="30px" Width="197px" TextMode="Date" AutoPostBack="True" OnTextChanged="Textdatepick_TextChanged"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" Height="56px" Style="margin-left: 0px" Width="387px"
                Visible="false">
                <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Height="74px"
                    Width="106px"></asp:ListBox>
                &nbsp;<asp:Button ID="Btnadd" runat="server" Height="29px" Text=">>" Width="34px"
                    OnClick="Btnadd_Click" formnovalidate="" Style="margin-top: 0px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Btnremove" runat="server" Text="<<" OnClick="Btnremove_Click" formnovalidate=""
                    Height="29px" Width="34px" Style="margin-top: 0px" />
                &nbsp;<asp:ListBox ID="ListBox2" runat="server" Height="74px" Width="106px" SelectionMode="Multiple">
                </asp:ListBox>
            </asp:Panel>
            <br />
            <br />
            <asp:Button ID="Btnsubmit" runat="server" Text="Submit" Height="31px" Width="73px"
                OnClick="Btnsubmit_Click" class="btn btn-info" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnupdate" runat="server" Text="Update" Height="31px" Width="73px"
                OnClick="Btnupdate_Click" class="btn btn-info" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btndelete" runat="server" Text="Delete" Height="31px" Width="73px"
                formnovalidate="" class="btn btn-info" OnClick="Btndelete_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnreset" runat="server" Text="Clear" Height="31px" Width="73px"
                formnovalidate="" OnClick="Btnreset_Click" class="btn btn-info" />
            <br />
            <br />
            <div class="footer">
                <hr />
                <i>Employee review track, developed by SIT-CSE.</i>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
