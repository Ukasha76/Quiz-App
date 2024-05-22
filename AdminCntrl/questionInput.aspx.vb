Partial Class AdminCntrl_questionInput
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Session("isAuth") Then
            Response.Redirect("login.aspx")
        End If
    End Sub

    Protected Sub cmdsumbit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdsumbit.Click
        Response.Redirect("input.aspx?totalquestions=" + totalquestions.Text)

    End Sub
    Protected Sub btnlogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnlogout.Click
        If Session("isAuth") Then
            Session.Remove("isAuth")
            Response.Redirect("login.aspx")
        End If

    End Sub

End Class
