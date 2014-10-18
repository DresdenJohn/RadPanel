
Partial Class register
    Inherits System.Web.UI.Page

    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions
    Public flyffFunc As New flyffFunctions

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        If checkLogin() = True Then
            Response.Redirect("registerFlyffAccount.aspx")
        End If

        Randomize()
        resetVerifyCode()
    End Sub

    Public Function Rand(ByVal Low As Long, ByVal High As Long) As Long
        Rand = Int((High - Low + 1) * Rnd()) + Low
    End Function

    Public Sub resetVerifyCode()
        Dim randomNumber1 As Integer = Rand(0, 30)
        Dim randomNumber2 As Integer = Rand(0, 30)

        Dim verifyString As String = "<abbr class='tooltip'>What is " + randomNumber1.ToString + " + " + randomNumber2.ToString + "?<span class='classic'>Captchas are dull and boring, lets use the 'ol brain for a bit with a simple math question.</span></abbr>"

        registerVerifyNumberLabel.Text = verifyString

        registerVerifyNumberLabel.Visible = True

        verifyNumberAnswer.Text = randomNumber1 + randomNumber2
    End Sub

    Protected Sub registerSubmitButton_Click(sender As Object, e As EventArgs) Handles registerSubmitButton.Click
        registerResultLabel.Text = "<h2 style='color: rgb(217,163,0)'>" + webFunc.registerSkyeAccount(registerUsernameTextbox.Text, registerEmailTextBox.Text, registerPinTextBox.Text, registerPasswordTextbox.Text, Request.ServerVariables("REMOTE_ADDR").ToString) + "</h2>"

            If createIrisAccountCheck.Checked Then
            registerResultLabel.Text += "<br/><h2 style='color: rgb(217,163,0)'>" + flyffFunc.registerUser(registerUsernameTextbox.Text, registerPasswordTextbox.Text, registerEmailTextBox.Text, Request.ServerVariables("REMOTE_ADDR").ToString) + "</h2>"
            irisFunc.addGameAccount(registerUsernameTextbox.Text, "Flyff", registerUsernameTextbox.Text)
            End If


            If registerResultLabel.Text.ToLower.Contains("success") Then

                Dim accountCookie As New HttpCookie("accountInfo")

                accountCookie.Values("g_email") = registerEmailTextBox.Text
                accountCookie.Values("g_password") = flyffFunc.getEncryptedPassword(registerPasswordTextbox.Text)
                accountCookie.Values("g_username") = webFunc.getUsername(registerEmailTextBox.Text)
                accountCookie.Values("g_auth") = webFunc.getAccountAuth(registerEmailTextBox.Text)

                Response.Cookies.Add(accountCookie)

                If createIrisAccountCheck.Checked Then
                    Response.Redirect("~/default.aspx?actn=firstLoginWIris")
                Else
                    Response.Redirect("~/default.aspx?actn=firstLogin")
                End If

            End If
        
    End Sub

    Public Function checkLogin() As Boolean

        If Not Request.Cookies("accountInfo") Is Nothing Then

            Dim accountCookie As HttpCookie = Request.Cookies("accountInfo")

            g_username = Server.HtmlEncode(accountCookie.Values("g_username"))
            g_email = Server.HtmlEncode(accountCookie.Values("g_email"))
            g_password = Server.HtmlEncode(accountCookie.Values("g_password"))
            g_auth = Server.HtmlEncode(accountCookie.Values("g_auth"))
            g_ip = Request.ServerVariables("REMOTE_ADDR").ToString

            Dim emailRegCheck As New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            Dim passwordRegCheck As New Regex("[a-zA-Z0-9]*")

            If emailRegCheck.IsMatch(g_email) And passwordRegCheck.IsMatch(g_password) Then
                Return webFunc.checkAccount(g_email, g_password, True)
            Else : Return False
            End If

        Else
            Return False
        End If

    End Function

End Class
