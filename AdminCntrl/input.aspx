<%@ Page Language="VB" AutoEventWireup="false" CodeFile="input.aspx.vb" Inherits="AdminCntrl_input" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Questions</title>
    <link href="input.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
        
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    <asp:Label ID="qstmnt_txt" runat="server" Text="Question Statemnet"></asp:Label>
                    <br />
                    <asp:TextBox ID="qstmnt" runat="server" ></asp:TextBox>
                    <br />
                    <asp:Label ID="opt_1" runat="server" Text="First Option"></asp:Label>
                    <br />
                    <asp:TextBox ID="opt1" runat="server" AutoPostBack="True" OnTextChanged="OptionTextBox_TextChanged"></asp:TextBox>

                    <br />
                    <asp:Label ID="opt_2" runat="server" Text="Second Option"></asp:Label>
                    <br />
                    <asp:TextBox ID="opt2" runat="server" AutoPostBack="True" OnTextChanged="OptionTextBox_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="opt_3" runat="server" Text="Third Option"></asp:Label>
                    <br />
                    <asp:TextBox ID="opt3" runat="server" AutoPostBack="True" OnTextChanged="OptionTextBox_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="opt_4" runat="server" Text="Fourth Option"></asp:Label>
                    <br />
                    <asp:TextBox ID="opt4" runat="server" AutoPostBack="True" OnTextChanged="OptionTextBox_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="crct_optn" runat="server" Text="Correct Option"></asp:Label>
                    <br />
                    <asp:DropDownList ID="crctoptn" runat="server"></asp:DropDownList>

                    <br />
                    <asp:Button ID="submit" runat="server" Text="Save" OnClick="submit_Click" />
                    <asp:Button ID="btnlogout" runat="server" Text="Logout" Visible="False" />

                    <asp:Label ID="lblResults" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
            
        </asp:UpdatePanel>
    </form>
        </div>
</body>
</html>
