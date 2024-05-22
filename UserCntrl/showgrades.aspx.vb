Imports System.Data.SqlClient

Partial Class UserCntrl_showgrades
    Inherits System.Web.UI.Page
    Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("conn_string").ConnectionString
    Dim connection As New SqlConnection(ConnectionString)

    Dim Result As New List(Of Question)()

    Dim totalQuestions As Integer = 0
    Dim correctAnswers As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Session("islogin") Is Nothing AndAlso Session("islogin").ToString() = "True" Then


            If Not IsPostBack Then

                LoadResult()

                lblTotalQuestions.Text = "Total Questions: " & totalQuestions
                lblCorrectAnswers.Text = "Correct Answers: " & correctAnswers
                lblPercentage.Text = "Percentage: " & ((correctAnswers / totalQuestions) * 100).ToString("F2") & "%"
                For Each question As Question In Result
                    Dim questionDiv As New HtmlGenericControl("div")
                    questionDiv.Attributes("class") = "question"

                    Dim questionLabel As New Label()
                    questionLabel.Text = question.Statement
                    questionLabel.CssClass = "question-statement"
                    questionDiv.Controls.Add(questionLabel)

                    Dim optionsList As New RadioButtonList()
                    optionsList.CssClass = "options-list"
                    optionsList.Enabled = False

                    For i As Integer = 0 To 3
                        Dim optionItem As New ListItem(question.Options(i), question.Options(i))
                        optionsList.Items.Add(optionItem)
                    Next

                    ' Pre-select the option that user clicked
                    optionsList.Items.FindByValue(question.userClicked).Selected = True

                    ' Highlight the correct answer and mark the user's answer if incorrect
                    For Each item As ListItem In optionsList.Items
                        If item.Value = question.CorrectAnswer Then
                            item.Attributes("style") = "color: green;"
                        End If
                        If item.Value = question.userClicked AndAlso question.userClicked <> question.CorrectAnswer Then
                            item.Attributes("style") = "color: red; font-weight: bold;"
                        End If
                    Next

                    questionDiv.Controls.Add(optionsList)
                    pnlQuestions.Controls.Add(questionDiv)
                Next

                Session("GradesViewed") = True

            End If
        Else
            Response.Redirect("login.aspx")

        End If
    End Sub
    Protected Sub btnlogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnlogout.Click
        If Not Session("islogin") Is Nothing AndAlso Session("islogin").ToString() = "True" Then
            Session.Clear()
            Response.Redirect("login.aspx")
        End If

    End Sub

    Private Sub LoadResult()
        'will extract from session
        Dim user_email As String = Session("user_email").ToString()
        connection.Open()

        Dim command As New SqlCommand("SELECT * FROM userAnswers where user_email = @userEmail", connection)
        command.Parameters.AddWithValue("@userEmail", user_email)

        Dim reader As SqlDataReader = command.ExecuteReader()

        If reader.HasRows Then
            While reader.Read()
                Dim qstmnt As String = reader("qstmnt").ToString()
                Dim optn1 As String = reader("optn1").ToString()
                Dim optn2 As String = reader("optn2").ToString()
                Dim optn3 As String = reader("optn3").ToString()
                Dim optn4 As String = reader("optn4").ToString()
                Dim correctAnswer As String = reader("correctanswer").ToString()
                Dim userClicked As String = reader("selectedanswer").ToString()

                Dim question As New Question(qstmnt, optn1, optn2, optn3, optn4, correctAnswer, userClicked)

                Result.Add(question)
            End While
            totalQuestions = Result.Count
            correctAnswers = Result.LongCount(Function(q) q.CorrectAnswer = q.userClicked)
        End If
        reader.Close()

        connection.Close()
    End Sub



    Public Class Question
        Public Property Statement As String
        Public Property Options As String()
        Public Property userClicked As String
        Public Property CorrectAnswer As String



        Public Sub New(ByVal stmnt As String, ByVal opt1 As String, ByVal opt2 As String, ByVal opt3 As String, ByVal opt4 As String, ByVal crct As String, ByVal userclcked As String)
            Statement = stmnt
            Options = {opt1, opt2, opt3, opt4}
            CorrectAnswer = crct
            userClicked = userclcked

        End Sub
    End Class
End Class
