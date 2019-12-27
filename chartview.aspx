<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chartview.aspx.cs" Inherits="empreview.chartview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chart view</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form-group">
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
      <li runat="server" ID="viewreport" visible="true" font-size="medium"><a href="viewreport.aspx">View report</a></li>
      <li runat="server" ID="chartviewid" visible="true" font-size="medium" class="active"><a href="chartview.aspx">Chart view</a></li>      
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
                Chart view</h1>
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownListemp" class="form-control" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownListemp_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Labelmsg" runat="server" Text=""></asp:Label><br />
            <asp:Chart ID="Chart1" runat="server" Width="914px" Height="340px">
                <Titles>
                    <asp:Title Text="Employee review chart">
                    </asp:Title>
                </Titles>
                <Series>
                    <asp:Series Name="Series1" ChartArea="ChartArea1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title="QID (Questions)">
                        </AxisX>
                        <AxisY Title="Marks">
                        </AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <br />
            <hr />
            <i>
                Questions assigned for IDs</i><br />       

            <asp:Table ID="Table1" runat="server" style="background-color:lightblue;" class="table">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><b>QID</b></asp:TableCell>
                    <asp:TableCell runat="server"><b>Questions</b></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div class="footer">
                <hr />
                <i>Employee review track, developed by SIT-CSE.</i>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
