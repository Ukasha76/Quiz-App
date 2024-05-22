<%@ Page Language="VB" AutoEventWireup="false" CodeFile="showgrades.aspx.vb" Inherits="UserCntrl_showgrades" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grades</title>
    <link href="../styles/showgrades.css" rel="stylesheet" />
        <script>
            window.onload = function() {
                window.history.forward();
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <h1>Grades</h1>
        <asp:Label ID="lblTotalQuestions" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblCorrectAnswers" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblPercentage" runat="server" Text=""></asp:Label>
                    <asp:Panel ID="pnlQuestions" runat="server"></asp:Panel>
        <asp:Button ID ="btnlogout" runat ="server" Text ="Logout" />

    </form>
</body>
</html>
