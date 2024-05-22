Imports System.Data.SqlClient
Partial Class AdminCntrl_input
    Inherits System.Web.UI.Page

    Dim connectionString As String = ConfigurationManager.ConnectionStrings("conn_string").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("isAuth") Then


            Dim totalQuestionValue As Integer = Convert.ToInt32(Request.QueryString("totalquestions"))

            ViewState("TotalQuestions") = totalQuestionValue
            ViewState("QuestionCounter") = 0

        Else
            Response.Redirect("login.aspx")

        End If
    End Sub
    Protected Sub OptionTextBox_TextChanged(sender As Object, e As EventArgs)
        If Session("isAuth") Then

            Dim crctoptn As DropDownList = DirectCast(form1.FindControl("crctoptn"), DropDownList)
            crctoptn.Items.Clear()

            If Not String.IsNullOrEmpty(opt1.Text) Then
                crctoptn.Items.Add(New ListItem(opt1.Text, opt1.Text))
            End If
            If Not String.IsNullOrEmpty(opt2.Text) Then
                crctoptn.Items.Add(New ListItem(opt2.Text, opt2.Text))
            End If
            If Not String.IsNullOrEmpty(opt3.Text) Then
                crctoptn.Items.Add(New ListItem(opt3.Text, opt3.Text))
            End If
            If Not String.IsNullOrEmpty(opt4.Text) Then
                crctoptn.Items.Add(New ListItem(opt4.Text, opt4.Text))
            End If
        Else
            Response.Redirect("login.aspx")

        End If
    End Sub



    Protected Sub submit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles submit.Click
        If Not Session("isAuth") Then

            Dim totalQuestions As Integer = Convert.ToInt32(ViewState("TotalQuestions"))
            Dim questionCounter As Integer = Convert.ToInt32(ViewState("QuestionCounter"))

            If (Not Duplicate(qstmnt.Text)) Then
                lblMessage.Text = "Question already exist"

                If questionCounter < totalQuestions Then
                    Dim insertSQL As String
                    insertSQL = "INSERT INTO quiz_t (qstmnt, optn1, optn2, optn3, optn4, crct_optn) "
                    insertSQL &= "VALUES ('"
                    insertSQL &= qstmnt.Text & "', '"
                    insertSQL &= opt1.Text & "', '"
                    insertSQL &= opt2.Text & "', '"
                    insertSQL &= opt3.Text & "', '"
                    insertSQL &= opt4.Text & "', '"
                    insertSQL &= crctoptn.Text & "')"

                    Dim con As New SqlConnection(connectionString)
                    Dim cmd As New SqlCommand(insertSQL, con)

                    Dim added As Integer = 0
                    Try
                        con.Open()
                        added = cmd.ExecuteNonQuery()
                        lblResults.Text = added.ToString() & " records inserted."
                    Catch err As Exception
                        lblResults.Text = "Error inserting record. "
                        lblResults.Text &= err.Message
                    Finally
                        con.Close()
                    End Try

                    questionCounter += 1

                    ViewState("QuestionCounter") = questionCounter

                    qstmnt.Text = ""
                    opt1.Text = ""
                    opt2.Text = ""
                    opt3.Text = ""
                    opt4.Text = ""

                    crctoptn.Items.Clear()

                    Dim questionsRemaining As Integer = totalQuestions - questionCounter

                    If questionsRemaining > 0 Then
                        lblMessage.Text = "Enter the next question. " & questionsRemaining & " question(s) remaining."
                    Else
                        lblMessage.Text = "All questions entered."
                        submit.Enabled = False
                        btnlogout.Visible = True
                    End If
                Else

                    lblMessage.Text = "All questions have been entered."
                    submit.Enabled = False
                    btnlogout.Visible = True
                End If
            Else
                submit.Enabled = False
                lblMessage.Text = "Question already exist"
                qstmnt.Text = ""
                opt1.Text = ""
                opt2.Text = ""
                opt3.Text = ""
                opt4.Text = ""
                crctoptn.Items.Clear()
            End If
        Else
            Response.Redirect("login.aspx")

        End If
    End Sub

    Protected Sub btnlogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        If Session("isAuth") Then
            Session.Remove("isAuth")
            Response.Redirect("login.aspx")
        End If

    End Sub
    Public Function Duplicate(ByVal qstmnt As String) As Boolean

        Dim query As String = "SELECT COUNT(*) FROM quiz_t WHERE qstmnt = @Qstmnt"
        Dim result As Integer = 0

        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Qstmnt", qstmnt)
                connection.Open()
                result = Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using

        Return result > 0
    End Function

End Class
