Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Web.UI.Page

Partial Class myAccount
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

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        refreshCookie()

        If checkLogin() = True Then
            loggedOutPlaceholder.Visible = False
            loggedInPlaceholder.Visible = True

            Dim accounts As String() = flyffFunc.getAccountNames(g_username).ToArray


            flyffAccountsLabel.Text += "<ul>"

            For i As Integer = 0 To accounts.Length - 1 Step 1
                flyffAccountsLabel.Text += "<li>" + accounts(i) + "<ul>"
                Dim characters As String() = flyffFunc.getCharacterNames(accounts(i)).ToArray
                For g As Integer = 0 To characters.Length - 1 Step 1
                    flyffAccountsLabel.Text += "<li>" + characters(g) + "</li>"
                Next
                flyffAccountsLabel.Text += "</ul></li>"
            Next

            flyffAccountsLabel.Text += "</ul>"

        End If
        
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

End Class
