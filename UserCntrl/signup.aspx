<%@ Page Language="VB" AutoEventWireup="false" CodeFile="signup.aspx.vb" Inherits="UserCntrl_signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link href="../styles/signup.css" rel="stylesheet" />
</head>
<body>
   
     <h1>User Signup</h1>
    <form id="signupForm" runat="server">
        <asp:Label runat="server">Email:</asp:Label>
        <asp:TextBox runat="server" ID="userEmail" ></asp:TextBox>

        <asp:Label runat="server" >Password:</asp:Label>
        <asp:TextBox runat="server" ID="password" TextMode="Password" ></asp:TextBox>

        <asp:Button runat="server" ID="signupButton" Text="Signup" />
    </form>
</body>
</html>
