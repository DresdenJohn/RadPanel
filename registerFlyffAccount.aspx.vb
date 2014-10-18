
Partial Class registerIrisAccount
    Inherits System.Web.UI.Page

    Public cpFunc As New cpFunctions
    Public flyffFunc As New flyffFunctions
    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Public Function Rand(ByVal Low As Long, ByVal High As Long) As Long
        Rand = Int((High - Low + 1) * Rnd()) + Low
    End Function

    Public Sub resetVerifyCode()
        Dim randomNumber1 As Integer = Rand(0, 30)
        Dim randomNumber2 As Integer = Rand(0, 30)

        Dim verifyString As String = "<abbr class='tooltip'>What is " + randomNumber1.ToString + " + " + randomNumber2.ToString + "?<span class='classic'>Captchas are dull and boring, lets use the 'ol brain for a bit with a simple math question.</span></abbr>"

        irisCaptchaLabel.Text = verifyString

        irisCaptchaLabel.Visible = True

        irisRealCaptchaText.Text = randomNumber1 + randomNumber2
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

    Public Sub refreshCookie()

        If Not Request.Cookies("accountInfo") Is Nothing Then

            Dim accountCookie As HttpCookie = Request.Cookies("accountInfo")

            g_username = Server.HtmlEncode(accountCookie.Values("g_username"))
            g_email = Server.HtmlEncode(accountCookie.Values("g_email"))
            g_password = Server.HtmlEncode(accountCookie.Values("g_password"))
            g_auth = Server.HtmlEncode(accountCookie.Values("g_auth"))
            g_ip = Request.ServerVariables("REMOTE_ADDR").ToString

        End If

    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If checkLogin() = True Then
            loggedOutPlaceholder.Visible = False
            loggedInPlaceholder.Visible = True
        End If

        Randomize()
        resetVerifyCode()

    End Sub

    Protected Sub irisRegisterSubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles irisRegisterSubmitButton.Click

        refreshCookie()

        irisRegisterResult.Text = "<h3>" + flyffFunc.registerUser(irisRegisterUsernameText.Text, irisRegisterPasswordTextbox.Text, g_email, g_ip) + "</h3>"

        If irisRegisterResult.Text.ToLower.Contains("success") Then
            irisFunc.addGameAccount(g_username, "Flyff", irisRegisterUsernameText.Text)
            Response.Redirect("./default.aspx?actn=flyffRegister")
        Else
            irisRegisterResult.Visible = True
        End If

    End Sub
End Class
