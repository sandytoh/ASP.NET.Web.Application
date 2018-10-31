<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Group8_AD_webapp.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Logic University Login</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body{
            background: url('images/background2.jpg');
            background-position: center;
            background-size: cover;
            overflow: hidden;
            background-repeat: no-repeat;
            width: 100vw;
            height: 100vh;
            padding-top: 20vh;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="position:absolute; top: 40vh; left: 40vw;" class="form form-inline" >
            Employee ID: <asp:TextBox ID="txtID" CssClass="form-control" runat="server"></asp:TextBox><asp:Button ID="btnEnter" CssClass="btn btn-primary" runat="server" Text="Enter" OnClick="btnEnter_Click" /><br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
    </form>

</body>
</html>
