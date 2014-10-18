Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration

Partial Class Admin_default
    Inherits System.Web.UI.Page

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        If Not Request.Cookies("accountInfo") Is Nothing Then

            Dim accountCookie As HttpCookie = Request.Cookies("accountInfo")

            g_username = Server.HtmlEncode(accountCookie.Values("g_username"))
            g_email = Server.HtmlEncode(accountCookie.Values("g_email"))
            g_password = Server.HtmlEncode(accountCookie.Values("g_password"))
            g_auth = Server.HtmlEncode(accountCookie.Values("g_auth"))
            g_ip = Request.ServerVariables("REMOTE_ADDR").ToString

        End If

        If g_auth = "S" Then
            admin_label.Text = "<p>Welcome back, Owner " + g_username + "</p>"
        ElseIf g_auth = "A" Then
            admin_label.Text = "<p>Welcome back, Admin " + g_username + "</p>"
        Else
            admin_label.Text = "<p>You are not logged in or do not have access to this page.</p>"
        End If

    End Sub
End Class
