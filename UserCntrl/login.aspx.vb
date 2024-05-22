Imports System.Data.SqlClient

Partial Class UserCntrl_login
    Inherits System.Web.UI.Page
    Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("conn_string").ConnectionString
    Dim connection As New SqlConnection(ConnectionString)
    Private Sub page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.Clear()
        End If
    End Sub
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click

        Dim userEmail As String = Me.userEmail.Text
        Dim password As String = Me.txtPassword.Text

        Dim query As String = "SELECT COUNT(*) FROM users WHERE user_email = @userEmail AND password = @password"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@userEmail", userEmail)
        command.Parameters.AddWithValue("@password", password)

        connection.Open()
        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
        connection.Close()

        If count > 0 Then
            Session("isLogin") = True
            Session("user_email") = userEmail
            Response.Redirect("quiz.aspx")
        Else
            Response.Redirect("login.aspx")
        End If
    End Sub

    Protected Sub cmdSignUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSignUp.Click
        Response.Redirect("signup.aspx")

    End Sub
End Class
