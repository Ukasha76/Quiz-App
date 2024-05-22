<%@ Page Language="VB" AutoEventWireup="false" CodeFile="questionInput.aspx.vb" Inherits="AdminCntrl_questionInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question Input</title>
    <link href="questionInput.css" rel="stylesheet" />
</head>
<body>
    <div class ="container">
    <form id="form1" runat="server">
      
            <div>
            <h2>Question INPUT</h2>
            <div>
                <label for="tquestions">Number of question</label>
                <asp:TextBox ID="totalquestions" runat="server"></asp:TextBox>
            </div>
            
            <div >
                <asp:Button ID="cmdsumbit" CssClass ="btn" runat ="server" Text ="Enter" />
            </div>
                   <div >
                <asp:Button ID="btnlogout" CssClass="logout-btn" runat ="server" Text ="Logout" />
            </div>
        </div>
    </form>

</div> 
</body>
</html>
