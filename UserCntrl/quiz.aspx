<%@ Page Language="VB" AutoEventWireup="false" CodeFile="quiz.aspx.vb" Inherits="UserCntrl_quiz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quiz</title>
    <link href="../styles/quiz.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="q_stmnt" runat="server" Text=""></asp:Label>
            <asp:RadioButtonList ID="optnlst" runat="server"></asp:RadioButtonList>
            <asp:Button ID="cmdNext" runat="server" Text="Next" />
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
     

        </div>
    </form>
</body>
</html>
