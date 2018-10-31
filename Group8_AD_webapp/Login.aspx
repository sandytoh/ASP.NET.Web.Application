<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Group8_AD_webapp.Login" %>

<!DOCTYPE html>

<html>
<head>
    <link rel="icon" 
      type="image/png" 
      href="img/favicon.png">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Logic University Login</title>

        <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/datepicker3.css" rel="stylesheet">
    <link href="css/styles.css" rel="stylesheet">
    <link href="css/login-style.css" rel="stylesheet">

</head>

<body>
    <nav class="navbar navbar-custom navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">

                <a class="navbar-brand desktop" runat="server" href="#"><span><strong>Logic</strong></span>University</a><span class="tinylogo desktop"><img style="width: 25px;height: 40px;" src="img/leaf.png"/></span>
                <a class="navbar-brand mobile" runat="server" href="#"><span>L</span>U</a>
            </div>
        </div>
    </nav>

    <div class="row">
        <div class="col-xs-10 col-xs-offset-1 col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4" style="margin-top:10%;">
            <div class="login-panel panel panel-default opacitylow">
                <%--<div class="panel-heading"><strong>Log in</strong></div>--%>
                <div class="panel-body">

<%--                        <fieldset>
                            <div class="form-group">
                                <asp:TextBox ID="txtLoginId" class="form-control" runat="server" placeholder="Employee Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLoginId" Display="Dynamic" CssClass="error" ID="rfv1" runat="server" ErrorMessage="Please enter an Employee Number"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="txtPwd" class="form-control" Display="Dynamic" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPwd" ID="rfv2" runat="server" CssClass="error" ErrorMessage="Please enter a Password"></asp:RequiredFieldValidator>
                                
                            </div>
                            <div class="text-right">
                            <asp:Button ID="btnLogin" class="btn btn-primary btn-lg" runat="server" Text="Login" CausesValidation="true" OnClientClick="return BtnClick();" OnClick="btnLogin_Click" />

                            </div>
                        </fieldset>--%>
                    <form ID="frmLogin" role="form" runat="server">
                        <asp:Login ID="Login1" runat="server" CssClass="logstyle" PasswordLabelText="" UserNameLabelText="" DestinationPageUrl="Login.aspx" DisplayRememberMe="False">
                            <LayoutTemplate>
                                
                        <fieldset>
                            <div class="form-group">
                                                        <asp:TextBox ID="UserName"  runat="server" ClientIDMode="Static"  placeholder="Employee Number" CssClass="form-control" ToolTip="Employee ID should be a number"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvUsername" ClientIDMode="Static" runat="server" ControlToValidate="UserName" ErrorMessage="Employee Number is required." ToolTip="Employee ID is required." ValidationGroup="Login1" ForeColor="Red" Font-Bold="True"></asp:RequiredFieldValidator>
                            
                            </div>
                            <div class="form-group">
                                                        <asp:TextBox ID="Password"  runat="server" ClientIDMode="Static" placeholder="Password" CssClass="form-control" TextMode="Password" ToolTip="Password should be >8 chars, with at least one number and special character"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPassword" ClientIDMode="Static" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1" ForeColor="Red" Font-Bold="True">Password is required.</asp:RequiredFieldValidator>
                            </div>
                            <div class="text-right">
                                                        <asp:Label ID="FailureText" CssClass="errortext" runat="server" EnableViewState="False"></asp:Label>
                            </div>
                            <div class="text-right">
                                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-primary btn-lg" Text="Log In" ValidationGroup="Login1" />
                            </div>
                            
                            </LayoutTemplate>
                        </asp:Login></form>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                </div>
            </div>
        </div>
        <!-- /.col-->
    </div>
    <!-- /.row -->

<%--    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>--%>
    <script type="text/javascript">

        ChangeIt();
        
        function ChangeIt() 
        {
            var totalCount = 6;
            var num = Math.ceil( Math.random() * totalCount );
            document.body.background = 'images/background'+num+'.jpg';
            }
    </script>

</body>
</html>
