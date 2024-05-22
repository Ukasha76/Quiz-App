Imports System.Data.SqlClient

Partial Class UserCntrl_signup
    Inherits System.Web.UI.Page
    Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("conn_string").ConnectionString
    Dim connection As New SqlConnection(ConnectionString)
    Private Sub page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.Clear()
        End If
    End Sub
    Protected Sub signupButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles signupButton.Click
        Dim userEmail As String = Me.userEmail.Text
        Dim password As String = Me.password.Text

        ' Check if the user_email is unique
        If Not IsUserEmailUnique(userEmail) Then
            ' Show error message and exit the method
            Dim script As String = "alert('Email already exists. Please use a different email.');"
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", script, True)
            Return
        End If

        Using connection As New SqlConnection(ConnectionString)
            connection.Open()
            Dim commandText As String = "INSERT INTO users (user_email, password) VALUES (@user_email, @password)"
            Using command As New SqlCommand(commandText, connection)
                command.Parameters.AddWithValue("@user_email", userEmail)
                command.Parameters.AddWithValue("@password", password)
                command.ExecuteNonQuery()
            End Using
        End Using

        Session("isLogin") = True
        Session("user_email") = userEmail

        Response.Redirect("quiz.aspx")
    End Sub

    Private Function IsUserEmailUnique(ByVal userEmail As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM users WHERE user_email = @user_email"
        Using connection As New SqlConnection(ConnectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@user_email", userEmail)
                connection.Open()
                Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
                Return result = 0 ' If result is 0, the email is unique
            End Using
        End Using
    End Function



End Class
