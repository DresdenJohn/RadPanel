Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration

Partial Class Admin_newsManage
    Inherits System.Web.UI.Page

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Public owner As String = "S"
    Public admin As String = "D"
    Public moderator As String = "O"
    Public everyone As String = "F"


    Public webFunc As New webFunctions

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        usergroupDropdown.Items.Add("Owners")
        usergroupDropdown.Items.Add("Administrators")
        usergroupDropdown.Items.Add("Moderators/GMs")
        usergroupDropdown.Items.Add("Everyone")

        If Not Request.Cookies("accountInfo") Is Nothing Then

            Dim accountCookie As HttpCookie = Request.Cookies("accountInfo")

            g_username = Server.HtmlEncode(accountCookie.Values("g_username"))
            g_email = Server.HtmlEncode(accountCookie.Values("g_email"))
            g_password = Server.HtmlEncode(accountCookie.Values("g_password"))
            g_auth = Server.HtmlEncode(accountCookie.Values("g_auth"))
            g_ip = Request.ServerVariables("REMOTE_ADDR").ToString

        End If

        If webFunc.getAccountAuth(g_email) = "S" Then
            Dim newsAction As String = Request.QueryString("newsAction")

            If newsAction = "trun" Then
                Dim id As String = Request.QueryString("id")
                webFunc.deleteNewsPost(id)
            End If

            If newsAction = "result" Then
                resultLabel.Text = "Posted News"
            End If

        End If

        newsStoriesPlaceHolder.Text = webFunc.loadEditNews(webFunc.getAccountAuth(g_email), 0, False, False)

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

    Protected Sub newsPostButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles newsPostButton.Click

        refreshCookie()

        Dim authSelected As String = usergroupDropdown.SelectedItem.ToString

        Dim auth As String = owner

        Select Case usergroupDropdown.SelectedItem.ToString
            Case Is = "Administrators"
                auth = admin
            Case Is = "Moderators/GMs"
                auth = moderator
            Case Is = "Everyone"
                auth = everyone
            Case Else
                auth = owner
        End Select

        webFunc.writeStory(g_username, Server.HtmlEncode(newsTitleTextBox.Text), Server.HtmlEncode(NewsPosting.Text), auth)

        Response.Redirect("newsManage.aspx?newsAction=result")
    End Sub



End Class
