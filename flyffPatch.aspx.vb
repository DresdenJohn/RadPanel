Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration

Partial Class flyffPatch
    Inherits System.Web.UI.Page

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions
    Public flyffFunc As New flyffFunctions

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Request.Cookies("accountInfo") Is Nothing Then

            Dim accountCookie As HttpCookie = Request.Cookies("accountInfo")

            g_username = Server.HtmlEncode(accountCookie.Values("g_username"))
            g_email = Server.HtmlEncode(accountCookie.Values("g_email"))
            g_password = Server.HtmlEncode(accountCookie.Values("g_password"))
            g_auth = Server.HtmlEncode(accountCookie.Values("g_auth"))
            g_ip = Request.ServerVariables("REMOTE_ADDR").ToString

        End If

        newsList.Text = webFunc.loadNewsTable(webFunc.getAccountAuth(g_email), 0, False, False, False, True)
    End Sub

End Class
