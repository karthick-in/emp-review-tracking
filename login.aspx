<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="empreview.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login page</title>
    <link rel="stylesheet" href="bootstrap.min.css" />
    <script type="" src="jquery.min.js"></script>
    <script type="" src="bootstrap.min.js"></script>
    <style type="text/css">
        body
        {
            font-family: georgia;
            font-size: 15px;
        }
        .jumbotron
        {
            background-color: #7aceef;
            height: 150px;
        }
        .footer
        {
            text-align: center;
            font-size: 11px;
            left: 0;
            bottom: 0;
            width: 100%;
        }
        
        .style1
        {
            width: 167px;
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
        <center>
            <h2>
                Login</h2>
            <div class="form-group">
                &nbsp;<table class="table-hover text-center">
                    <tr>
                        <td class="style1">
                            Username
                        </td>
                        <td>
                            <asp:TextBox ID="Textusername" runat="server" placeholder="username" required="username"
                                class="form-control" Height="31px" Width="113px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Password
                        </td>
                        <td>
                            <asp:TextBox ID="Textpassword" runat="server" class="form-control" placeholder="password"
                                required="password" Height="31px" Width="113px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:LinkButton ID="LinkButtonforgotpassword" runat="server" OnClick="LinkButtonforgotpassword_Click">Forgot password?</asp:LinkButton>
            <br />
            <br />
            <asp:Button ID="Btnlogin" runat="server" Text="Login" Height="34px" Width="61px"
                OnClick="Btnlogin_Click" class="btn btn-info" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btnreset" runat="server" Text="Clear" Height="34px" Width="61px"
                OnClick="Btnreset_Click" formnovalidate="" class="btn btn-info" />
            <br />
            <br />
        </center>
        <center>
            <b>
                <asp:Label ID="Labelmsg" runat="server" ForeColor="Red"></asp:Label></b></center>
    </div>
    <br />
    <br />
    <br />
    <div class="footer">
        <hr />
        <i>Employee review track, developed by SIT-CSE</i>
    </div>
    </form>
</body>
</html>
