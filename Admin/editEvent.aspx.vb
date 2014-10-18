Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_editEvent
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

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        refreshCookie()

        usergroupDropdown.Items.Clear()

        usergroupDropdown.Items.Add("Owners")
        usergroupDropdown.Items.Add("Administrators")
        usergroupDropdown.Items.Add("Moderators/GMs")
        usergroupDropdown.Items.Add("Everyone")

        Dim editIDString As String = Request.QueryString("ID")
        Dim editID As Integer

        If Integer.TryParse(editIDString, editID) = True Then
            loadNewsEdit(editID)
        End If


    End Sub

    Public Sub loadNewsEdit(ByVal ID As Integer)

        Dim queryString As String = "SELECT * FROM WEB_EVENTS_TBL WHERE ID = " + ID.ToString

        Using connection As New SqlConnection(webFunc.getConn)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            connection.Close()

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            reader.Read()

            If Not dt.Rows.Count <= 0 Then
                errorBox.Visible = False
                editBox.Visible = True
            End If

            newsTitleTextBox.Text = reader("n_Title")

            Select Case reader("n_Authority")
                Case Is = admin
                    usergroupDropdown.SelectedIndex = 1
                Case Is = moderator
                    usergroupDropdown.SelectedIndex = 2
                Case Is = everyone
                    usergroupDropdown.SelectedIndex = 3
                Case Else
                    usergroupDropdown.SelectedIndex = 0
            End Select

            NewsPosting.Text = Server.HtmlDecode(reader("n_News"))

            reader.Close()

            connection.Close()

        End Using

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



    Public Sub updateNews(ByVal id As String)

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

        Dim queryString As String = "UPDATE WEB_EVENTS_TBL SET n_Title = '" + newsTitleTextBox.Text + "', n_Authority = '" + auth + "', n_News = '" + Server.HtmlEncode(NewsPosting.Text) + "' WHERE ID = " + id

        Using connection As New SqlConnection(webFunc.getConn)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()

        End Using

    End Sub

    Protected Sub newsPostButton_Click(sender As Object, e As EventArgs) Handles newsPostButton.Click
        Dim id As String = Request.QueryString("ID")

        updateNews(id)

    End Sub

End Class
