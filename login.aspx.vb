
Partial Class login
    Inherits System.Web.UI.Page

    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions
    Public flyffFunc As New flyffFunctions

    Protected Sub loginSubmitButton_Click(sender As Object, e As EventArgs) Handles loginSubmitButton.Click
        If webFunc.checkAccount(loginEmailTextbox.Text, loginPasswordTextBox.Text, False) = True Then

            Dim accountCookie As New HttpCookie("accountInfo")

            Try
                Dim rememberMe As Boolean = DirectCast(Session("rememberMe"), Boolean)

                If rememberMe Then
                    accountCookie.Expires = DateTime.Now.AddDays(14)
                End If
            Catch ex As Exception

            End Try

            accountCookie.Values("g_email") = loginEmailTextbox.Text
            accountCookie.Values("g_password") = flyffFunc.getEncryptedPassword(loginPasswordTextBox.Text)
            accountCookie.Values("g_username") = webFunc.getUsername(loginEmailTextbox.Text)
            accountCookie.Values("g_auth") = webFunc.getAccountAuth(loginEmailTextbox.Text)

            Response.Cookies.Add(accountCookie)

            webFunc.logWebPanelAction(webFunc.getUsername(loginEmailTextbox.Text), "Logged In", Request.ServerVariables("REMOTE_ADDR").ToString)

            webFunc.updateLoginTime(loginEmailTextbox.Text)

            Response.Redirect("~/default.aspx?actn=login")
        Else
            loginResult.Text = "<h2 style='color: rgb(217,163,0)'>Incorrect Email or Password</h2>"
        End If
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        Dim curSession As String = DirectCast(Session("usingSession"), String)

        Dim usingSession As Boolean

        If Boolean.TryParse(curSession, usingSession) = True Then
            Dim email As String = DirectCast(Session("emailText"), String)
            Dim password As String = DirectCast(Session("passwordText"), String)


            Dim emailRegCheck As New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            Dim passwordRegCheck As New Regex("[a-zA-Z0-9]*")

            If emailRegCheck.IsMatch(DirectCast(Session("emailText"), String)) And passwordRegCheck.IsMatch(DirectCast(Session("passwordText"), String)) Then
                loginEmailTextbox.Text = email
                loginPasswordTextBox.Text = password

                

                loginSubmitButton_Click(sender, e)
            Else
                loginResult.Text = "<h2 style='color: rgb(217,163,0)'>Invalid Email or Password</h2>"
            End If
        End If
    End Sub
End Class
