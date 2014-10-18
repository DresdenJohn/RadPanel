Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Web.UI.Page

Partial Class WebMaster
    Inherits System.Web.UI.MasterPage

#Region "Public Declarations"
    Public cpFunc As New cpFunctions
    Public flyffFunc As New flyffFunctions
    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""



#End Region

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        runStartupFunctions(checkLogin())
        verLabel.Text = webFunc.versionName

        Dim refreshAction As String = Request.QueryString("actn")

        Dim newsAction As String = Request.QueryString("newsAction")

        Select Case (newsAction)
            Case "trun"
                sendNotification("News posting has been deleted.")
            Case "newsUpdate"
                sendNotification("News has been updated.")
        End Select

        Select Case (refreshAction)
            Case "logout"
                sendNotification("You are now logged out!")
            Case "DoLogout"
                doLogout()
                Response.Redirect("default.aspx?actn=logout")
            Case "login"
                sendNotification("Welcome back, " + g_username)
            Case "firstLogin"
                sendNotification("Welcome to Eternal Gaming! Your Network account is now ready for use.<br/>You can now register for in-game accounts.")
            Case "firstLoginWIris"
                sendNotification("Thanks for joining Eternal Games!<br/>Both your Network and Iris accounts are ready for use!")
            Case "irisRegister"
                sendNotification("Iris Account Registered!")
            Case "voted"
                sendNotification("Thanks for Voting! Your chosen character has been rewarded.")
            Case "invalidVote"
                sendNotification("Sorry! It looks like you already recieved your reward! Try again later.")
            Case "sentIrisItem"
                sendNotification("Send Item success!")

        End Select

    End Sub

    Public Sub sendNotification(ByVal actionText As String)

        notifyLabel.Text = "<script>$(document).ready(function(){$.sticky('" + actionText + "');});</script>"

    End Sub

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


    Public Sub runStartupFunctions(ByVal isLoggedIn As Boolean)
		refreshCookie()
        If isLoggedIn = False Then
            usernameTopBoxLabel.Text = "<span style='text-shadow: 2px 2px 3px #000;'>Welcome, Guest!</span>"
            topBarConsoleLabel.Text = "<a href='../default.aspx'>Home</a> | <a href='../login.aspx'>Login</a>"
        Else

            Dim auth As String = webFunc.getAccountAuth(g_email)

            If auth = "S" Or auth = "A" Then
                defaultPlace.Visible = False
                adminPlaceholder.Visible = True
            End If
            If auth = "S" Then
                ownerLinks.Visible = True
            End If
            g_username = g_username.Remove(g_username.Length - 1, 1)
            usernameTopBoxLabel.Text = "<span style='text-shadow: 2px 2px 3px #000;'>Welcome, " & g_username & "</span>"
            topBarConsoleLabel.Text = "<a href='../default.aspx'>Home</a> | <a href='../default.aspx?actn=DoLogout'>Logout</a>"

        End If

        Randomize()

        resetVerifyCode()

    End Sub

#Region "Unchanging Subs"

    Public Sub applySiteTheme(ByVal themeColor As String)
        'If themeColor.ToLower = "blue" Then
        '    siteThemeCSS.Text = "<link rel='stylesheet' media='screen' href='../Styles/blueStyle.css' type='text/css'/>"
        'ElseIf themeColor.ToLower = "yellow" Then
        '    siteThemeCSS.Text = "<link rel='stylesheet' media='screen' href='../Styles/yellowStyle.css' type='text/css'/>"
        'ElseIf themeColor.ToLower = "purple" Then
        '    siteThemeCSS.Text = "<link rel='stylesheet' media='screen' href='../Styles/purpleStyle.css' type='text/css'/>"
        'Else
        '    siteThemeCSS.Text = "<link rel='stylesheet' media='screen' href='../Styles/blueStyle.css' type='text/css'/>"
        'End If
    End Sub

    Public Function Rand(ByVal Low As Long, ByVal High As Long) As Long
        Rand = Int((High - Low + 1) * Rnd()) + Low
    End Function

    Public Sub resetVerifyCode()
        Dim randomNumber1 As Integer = Rand(0, 30)
        Dim randomNumber2 As Integer = Rand(0, 30)

    End Sub

#End Region

Public Sub doLogout()
        Dim accountCookie As New HttpCookie("accountInfo")

        accountCookie.Expires = DateTime.Now.AddDays(-1)

        accountCookie.Values("g_email") = ""
        accountCookie.Values("g_password") = ""
        accountCookie.Values("g_username") = ""
        accountCookie.Values("g_auth") = ""

        Response.Cookies.Add(accountCookie)

        Response.Redirect("~/default.aspx?actn=logout")
    End Sub


    Protected Sub signUpButton_Click(sender As Object, e As EventArgs)
        Response.Redirect("register.aspx")
    End Sub

    Protected Sub loginButton_Click(sender As Object, e As EventArgs)
        Session("usingSession") = True

        Response.Redirect("login.aspx")
    End Sub

End Class




