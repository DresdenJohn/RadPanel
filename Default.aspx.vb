Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration

Partial Class _Default
    Inherits System.Web.UI.Page

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions
    Public flyffFunc As New flyffFunctions

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not Request.Cookies("accountInfo") Is Nothing Then

            Dim accountCookie As HttpCookie = Request.Cookies("accountInfo")

            g_username = Server.HtmlEncode(accountCookie.Values("g_username"))
            g_email = Server.HtmlEncode(accountCookie.Values("g_email"))
            g_password = Server.HtmlEncode(accountCookie.Values("g_password"))
            g_auth = Server.HtmlEncode(accountCookie.Values("g_auth"))
            g_ip = Request.ServerVariables("REMOTE_ADDR").ToString

        End If

        newsStoriesPlaceHolder.Text = webFunc.loadNewsTable(webFunc.getAccountAuth(g_email), 0, False, False, False, True)

        eventsStoriesPlaceholder.Text = webFunc.loadEventsTable(webFunc.getAccountAuth(g_email), 0, False, False, False, True)

        rankingPlaceholder.Text = flyffFunc.getFrontRanking(10)

        'rankingPlaceholder.Text = irisFunc.getFrontRanking()

    End Sub

End Class
