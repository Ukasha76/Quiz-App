Imports System.Data.SqlClient

Partial Class AdminCntrl_login
    Inherits System.Web.UI.Page
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("conn_string").ConnectionString
    Dim connection As New SqlConnection(connectionString)

    Protected Sub cmdlogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdlogin.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        connection.Open()
        Dim command As New SqlCommand("SELECT COUNT(1) FROM adminauth WHERE username = @Username AND password = @Password", connection)
        command.Parameters.AddWithValue("@Username", username)
        command.Parameters.AddWithValue("@Password", password)
        Dim count As Integer = command.ExecuteScalar()
        connection.Close()

        If count > 0 Then

            Session("isAuth") = True

            Response.Redirect("questionInput.aspx")

        Else
            Response.Redirect("login.aspx")

        End If
    End Sub
End Class
