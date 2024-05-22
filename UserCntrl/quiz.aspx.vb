Imports System.Data.SqlClient

Partial Class UserCntrl_quiz
    Inherits System.Web.UI.Page

    Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("conn_string").ConnectionString
    Dim connection As New SqlConnection(ConnectionString)

    Dim questions As New List(Of Question)()

    Dim questionIndex As Integer = 0
    Dim totalQuestions As Integer = 0
    Dim correctAnswers As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Session("islogin") Is Nothing AndAlso Session("islogin").ToString() = "True" Then

            Dim userEmail As String = Session("user_email").ToString()

            If isAttempted(userEmail) Then

                Response.Redirect("showgrades.aspx") ' Redirect to a page indicating access is denied
            Else


                If Not IsPostBack Then


                    LoadQuestions()


                    ShowQuestion(questionIndex)

                    Session("questions") = questions
                    Session("questionIndex") = questionIndex
                    Session("totalQuestions") = totalQuestions
                    Session("correctAnswers") = correctAnswers
                Else
                    questions = CType(Session("questions"), List(Of Question))
                    questionIndex = CInt(Session("questionIndex"))
                    totalQuestions = CInt(Session("totalQuestions"))
                    correctAnswers = CInt(Session("correctAnswers"))
                End If
            End If
        Else
            Response.Redirect("login.aspx")
        End If


    End Sub

    Private Sub LoadQuestions()
        connection.Open()
        Dim command As New SqlCommand("SELECT * FROM quiz_t ORDER BY NEWID()", connection)
        Dim reader As SqlDataReader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                Dim qstmnt As String = reader("qstmnt").ToString()
                Dim optn1 As String = reader("optn1").ToString()
                Dim optn2 As String = reader("optn2").ToString()
                Dim optn3 As String = reader("optn3").ToString()
                Dim optn4 As String = reader("optn4").ToString()
                Dim correctAnswer As String = reader("crct_optn").ToString()

                Dim question As New Question(qstmnt, optn1, optn2, optn3, optn4, correctAnswer)

                questions.Add(question)
            End While
            totalQuestions = questions.Count
        End If

        reader.Close()
        connection.Close()
    End Sub

    Private Sub ShowQuestion(ByVal index As Integer)
        If index >= 0 AndAlso index < totalQuestions Then
            Dim question As Question = questions(index)
            q_stmnt.Text = question.Statement
            optnlst.Items.Clear()
            For i As Integer = 0 To question.Options.Length - 1
                optnlst.Items.Add(New ListItem(question.Options(i), question.Options(i)))
            Next
            optnlst.SelectedIndex = 0
        End If
    End Sub


    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdNext.Click
        If optnlst.SelectedIndex >= 0 Then

            Dim selectedAnswer As String = optnlst.SelectedValue
            Dim correctAnswer As String = questions(questionIndex).CorrectAnswer



            InsertUserAnswer(q_stmnt.Text, optnlst.Items(0).Text, optnlst.Items(1).Text, optnlst.Items(2).Text, optnlst.Items(3).Text, selectedAnswer, correctAnswer)

            questionIndex += 1
            Session("questionIndex") = questionIndex

            If questionIndex < totalQuestions Then
                ShowQuestion(questionIndex)
            Else
                attempted()
                Response.Redirect("showgrades.aspx")

            End If
        End If
    End Sub
    Private Function isAttempted(ByVal userEmail As String) As Boolean
        Dim query As String = "SELECT attempted FROM users WHERE user_email = @UserEmail"
        Using connection As New SqlConnection(ConnectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserEmail", userEmail)
                connection.Open()
                Dim result As Object = command.ExecuteScalar()
                Return Convert.ToBoolean(result)
            End Using
        End Using
    End Function

    Private Sub Attempted()
        ' Extract user_email from session
        Dim user_email As String = Session("user_email").ToString()
        connection.Open()

        ' Update the attempted status for the user
        Dim updateCommand As New SqlCommand("UPDATE users SET attempted = 1 WHERE user_email = @userEmail", connection)
        updateCommand.Parameters.AddWithValue("@userEmail", user_email)
        updateCommand.ExecuteNonQuery()

        connection.Close()
    End Sub

    Private Sub InsertUserAnswer(ByVal qstmnt As String, ByVal opt1 As String, ByVal opt2 As String, ByVal opt3 As String, ByVal opt4 As String, ByVal selectedAnswer As String, ByVal correctAnswer As String)
        'will extract from session later 
        Dim userEmail As String = Session("user_email").ToString()
        Dim query As String = "INSERT INTO UserAnswers (user_email,qstmnt, optn1, optn2, optn3, optn4,  correctAnswer,selectedAnswer) VALUES (@userEmail,@qstmnt, @opt1, @opt2, @opt3, @opt4,@correctAnswer,@selectedAnswer)"

        Using con As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@userEmail", userEmail)

                cmd.Parameters.AddWithValue("@qstmnt", qstmnt)
                cmd.Parameters.AddWithValue("@opt1", opt1)
                cmd.Parameters.AddWithValue("@opt2", opt2)
                cmd.Parameters.AddWithValue("@opt3", opt3)
                cmd.Parameters.AddWithValue("@opt4", opt4)
                cmd.Parameters.AddWithValue("@selectedAnswer", selectedAnswer)
                cmd.Parameters.AddWithValue("@correctAnswer", correctAnswer)



                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Class Question
        Public Property Statement As String
        Public Property Options As String()
        Public Property CorrectAnswer As String

        Public Sub New(ByVal stmnt As String, ByVal opt1 As String, ByVal opt2 As String, ByVal opt3 As String, ByVal opt4 As String, ByVal crct As String)
            Statement = stmnt
            Options = {opt1, opt2, opt3, opt4}
            CorrectAnswer = crct
            ShuffleOptions()
        End Sub

        Public Sub ShuffleOptions()
            Dim rand As New Random()
            Options = Options.OrderBy(Function() rand.Next()).ToArray()
        End Sub
    End Class


End Class
