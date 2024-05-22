<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="UserCntrl_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login</title>
    
    <link href="../styles/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>User Login</h1>
            <div>
                <label for="userEmail">Email:</label>
                <asp:TextBox ID="userEmail" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" />
            </div>
             <div>
                <asp:Button ID="cmdSignUp" runat="server" Text="SignUp" />
            </div>
        </div>
    </form>
</body>
</html>
