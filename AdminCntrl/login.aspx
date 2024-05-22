<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="AdminCntrl_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
   <link href="admin_login.css" rel="stylesheet" />
</head>
<body>
    <div class ="container">
    <form id="form1" runat="server">
        <div>
            <h2>Login</h2>
            <div>
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div >
                <asp:Button ID="cmdlogin" class="cmdlogin" runat ="server" Text ="Login" />
            </div>
        </div>
    </form>
        </div>
</body>
</html>
