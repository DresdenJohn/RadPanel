Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Web.UI.Page
Imports System.Data

Public Class webFunctions

    '================================================'
    '------------ Eternal Web Panel -----------------'
    '================================================'
    ' - All functions related to the control panel
    '   is stored here.
    Public versionName As String = "Ver 1.705"
    Public csLibrary As New webVars
    '==============================================='
    ' - Web Panel Database Connection Strings
    Private webPanelConnectionString As String = csLibrary.webPanelCS
    '==============================================='
    ' - Begin Functions/Sub-Procedures
    '==============================================='

    Public cpFunctions As New cpFunctions
    Public flyffFunc As New flyffFunctions

    Public Function getConn() As String
        Return webPanelConnectionString
    End Function

    Public Function registerSkyeAccount(ByVal username As String, ByVal email As String, ByVal pin As String, ByVal password As String, ByVal ip As String) As String
        Dim result As String = ""

        Dim rowsEffected As Integer

        Dim queryString As String = "INSERT INTO WEB_ACCOUNTS_TBL (a_Username, a_Email, a_Password, a_GlobalPin, a_Authority, a_DateRegistered, a_LastLogin) VALUES('" + username + "', '" + email + "', '" + flyffFunc.getEncryptedPassword(password) + "', '" + pin + "', 'F', GETDATE(), GETDATE())"
        Try
            Using connection As New SqlConnection(webPanelConnectionString)

                Dim command As New SqlCommand(queryString, connection)

                connection.Open()
                rowsEffected = command.ExecuteNonQuery()
                connection.Close()

            End Using

            If rowsEffected = 1 Then
                result = "Successful Registration for user " + username + "!"
            Else
                result = "An Error has Occurred!"
            End If
        Catch ex As SqlException

            If ex.Message.IndexOf("IX_WEB_ACCOUNTS_TBL") > -1 Then
                Return "<h2 style='color: rgb(217,163,0)'>An Error has Occured!</h2>" +
                "<h3 style='font-size: 12px;color:white;padding: 10px; border: 1px solid #666; background-color:rgba(255,0,0,0.5); border-radius: 10px;'>" +
                "There is already an account registered for '" + email +
                "'</h3>"
            ElseIf ex.Message.IndexOf("PK_WEB_ACCOUNTS_TBL") > -1 Then
                Return "<h2 style='color: rgb(217,163,0)'>An Error has Occured!</h2>" +
                "<h3 style='font-size: 12px;color:white;padding: 10px; border: 1px solid #666; background-color:rgba(255,0,0,0.5); border-radius: 10px;'>" +
                "This account name has already been taken." +
                "</h3>"
            Else
                Return "<h2 style='color: rgb(217,163,0)'>An Error has Occured!</h2>" +
                "<h3 style='font-size: 12px;color:white;padding: 10px; border: 1px solid #666; background-color:rgba(255,0,0,0.5); border-radius: 10px;'>" +
                ex.Message.ToString +
                "</h3>"
            End If

        End Try

        Return result
    End Function

    Public Function getHeaders() As String

        Dim beginNewsString As String = "<div><table width='360' border='0' align='center' cellpadding='3' cellspacing='0' class='border'><tr><td><img src='./Assets/icon.gif' style='padding-right:5px;' /></td><td width='360'>"

        Dim endNewsString As String = "</td></tr></table></div>"

        Dim finalString As String = ""

        Dim queryString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'F' ORDER BY n_DateTime DESC"


        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            Dim counter As Integer = 0

            While (reader.Read())

                counter += 1

                If counter = 6 Then
                    Exit While
                End If

                finalString &= (beginNewsString + "<a href='news.aspx?ID=" + reader("ID").ToString + "' target='_blank'>" + reader("n_Title").ToString + "</a>" + endNewsString)

            End While

            reader.Close()
            connection.Close()

            Return finalString

        End Using
    End Function

    Public Sub logWebPanelAction(ByVal username As String, ByVal action As String, ByVal ip As String)

        Dim queryString As String = "INSERT INTO WEB_LOG_TBL (l_Username, l_action, l_DateTime, l_IP)" +
            "VALUES('" + username + "', '" + action + "', GETDATE(), '" + ip + "')"

        Using connection As New SqlConnection(webPanelConnectionString)

            Dim command As New SqlCommand(queryString, connection)

            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()

        End Using

    End Sub

    Public Sub updateLoginTime(ByVal email As String)
        Dim queryString As String =
            "UPDATE WEB_ACCOUNTS_TBL SET a_LastLogin = GETDATE() WHERE a_Email = '" + email + "';"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()
        End Using
    End Sub

    Public Function checkPin(ByVal email As String, ByVal inputPin As String) As Boolean

        Dim queryString As String = "SELECT * FROM WEB_ACCOUNTS_TBL WHERE a_Email = '" + email + "'"

        Dim dbPin As String = ""

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                dbPin = reader("a_GlobalPin")
            End While

            If dbPin = inputPin Then
                Return True
            Else : Return False
            End If

        End Using

    End Function

    Public Function checkAccount(ByVal email As String, ByVal password As String, ByVal isEncrypted As Boolean) As Boolean
        Dim isValid As Boolean = False
        Dim dbEmail As String = ""
        Dim dbPassword As String = ""

        Dim queryString As String =
            "SELECT * FROM WEB_ACCOUNTS_TBL WHERE a_Email = '" + email + "';"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                dbEmail = reader("a_Email")
                dbPassword = reader("a_Password")
            End While

            If isEncrypted = False Then
                If flyffFunc.getEncryptedPassword(password) = dbPassword Then
                    isValid = True
                End If
            Else
                If password = dbPassword Then
                    isValid = True
                End If
            End If


            reader.Close()

            connection.Close()

        End Using

        Return isValid
    End Function

    Public Function getUsername(ByVal email As String) As String
        Dim queryString As String = "SELECT * FROM WEB_ACCOUNTS_TBL WHERE a_Email = '" + email + "';"

        Dim username As String = ""

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                username = reader("a_Username")
            End While

            reader.Close()

            connection.Close()

            Return username
        End Using
    End Function

    Public Function getAccountAuth(ByVal email As String) As String
        Dim userAuth As String = "N"

        Dim queryString As String =
            "SELECT * FROM WEB_ACCOUNTS_TBL WHERE a_Email = '" + email + "';"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                userAuth = reader("a_Authority")
            End While

            reader.Close()
            connection.Close()


        End Using

        Return userAuth
    End Function

    Public Sub clearWebPanelLog(ByVal username As String, ByVal ip As String)

        Dim queryString As String = "TRUNCATE TABLE WEB_LOG_TBL"

        Using connection As New SqlConnection(webPanelConnectionString)

            Dim command As New SqlCommand(queryString, connection)

            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()

        End Using

        logWebPanelAction(username, "Cleared Web Panel Log", ip)

    End Sub

    Public Sub deleteNewsPost(ByVal id As Integer)

        Dim queryString As String = "DELETE FROM WEB_NEWSMAIN_TBL WHERE ID = '" + id.ToString + "';"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()


        End Using

    End Sub

    Public Sub deleteEvent(ByVal id As Integer)

        Dim queryString As String = "DELETE FROM WEB_EVENTS_TBL WHERE ID = '" + id.ToString + "';"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()


        End Using

    End Sub

    Public Sub writeStory(ByVal account As String, ByVal title As String, ByVal story As String, ByVal auth As String)

        Dim queryString As String = "INSERT INTO WEB_NEWSMAIN_TBL (n_Title, n_DateTime, n_PostedBy, n_News, n_Authority)" +
            "VALUES('" + title + "', GETDATE(), '" + account + "', '" + story + "', '" + auth + "');"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim cmd As New SqlCommand(queryString, connection)

            connection.Open()

            cmd.ExecuteNonQuery()

            connection.Close()
        End Using

    End Sub

    Public Sub writeEvent(ByVal account As String, ByVal title As String, ByVal story As String, ByVal auth As String)

        Dim queryString As String = "INSERT INTO WEB_EVENTS_TBL (n_Title, n_DateTime, n_PostedBy, n_News, n_Authority)" +
            "VALUES('" + title + "', GETDATE(), '" + account + "', '" + story + "', '" + auth + "');"

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim cmd As New SqlCommand(queryString, connection)

            connection.Open()

            cmd.ExecuteNonQuery()

            connection.Close()
        End Using

    End Sub

    Public Function getStories(ByVal auth As String, ByVal isEdit As Boolean) As String
        Dim finalString As String = ""

        Dim queryString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'F' ORDER BY n_DateTime DESC"
        Dim adminString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL ORDER BY n_DateTime DESC"
        Dim gmString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE _Authority = 'O' OR n_Authority = 'F' ORDER BY n_DateTime DESC"


        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand
            command.Connection = connection
            If auth = "S" Then
                command.CommandText = adminString
            ElseIf auth = "O" Then
                command.CommandText = gmString
            Else
                command.CommandText = queryString
            End If

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            For i As Integer = 0 To 2 Step 1

                reader.Read()

                Dim temp As String
                Dim temp2(2) As String
                Dim curDate As String
                Dim curTime As String

                temp = reader("n_DateTime")
                temp2 = temp.Split(" ")
                curDate = temp2(0)
                Dim currentDate As Date = temp2(1)
                curTime = Format(currentDate, "h:mm tt")

                If isEdit Then
                    finalString &= _
                        "<div class='box'>"
                Else
                    finalString &= _
                        "<div class='box'>"
                End If

                finalString &= _
                    "<h2>" +
                    reader("n_Title") +
                    "</h2><h3>Posted By: " +
                    reader("n_PostedBy") +
                    " on " + curDate + " at " + curTime + " EST" +
                    "</h3><hr/><p>" +
                    HttpContext.Current.Server.HtmlDecode(reader("n_News")) +
                    "</p>"

                If isEdit Then
                    finalString &= _
                        "<hr/><div class='buttonSection' style='margin: 0 auto; text-align:center;'><span class='registerButtons' style=text-align: center; margin: 0 auto;><a href=newsManage.aspx?newsAction=trun&id=" +
                    reader("ID").ToString + ">" +
                    "Delete Post</a></span></div></div>"
                Else
                    finalString &= "</div>"
                End If
            Next

            reader.Close()
            connection.Close()

            Return finalString

        End Using
    End Function

    Public Function loadNews(ByVal auth As String, ByVal id As String, ByVal loadByID As Boolean, ByVal isEdit As Boolean, Optional ByVal isMini As Boolean = False, Optional ByVal isLimited As Boolean = False) As String
        Dim finalString As String = ""

        Dim queryString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'F' ORDER BY n_DateTime DESC"
        Dim adminString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL ORDER BY n_DateTime DESC"
        Dim gmString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'O' OR n_Authority = 'F' ORDER BY n_DateTime DESC"

        Dim idString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE ID = " + id

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand
            command.Connection = connection

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            If loadByID = False Then
                If auth = "S" Then
                    command.CommandText = adminString
                ElseIf auth = "O" Then
                    command.CommandText = gmString
                Else
                    command.CommandText = queryString
                End If
            Else
                command.CommandText = idString
            End If

            connection.Open()

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If Not dt.Rows.Count <= 0 Then
                Dim reader As SqlDataReader = command.ExecuteReader

                If isLimited Then
                    For counter As Integer = 0 To 4 Step 1
                        reader.Read()

                        Dim temp As String
                        Dim temp2(2) As String
                        Dim curDate As String
                        Dim curTime As String

                        If counter >= dt.Rows.Count Then
                            Exit For
                        End If

                        temp = reader("n_DateTime")
                        temp2 = temp.Split(" ")
                        curDate = temp2(0)
                        Dim currentDate As Date = temp2(1)
                        curTime = Format(currentDate, "h:mm tt")

                        finalString &= _
                            "<div class='box'>" +
                            "<div style='float: right;'>" +
                            "<div class='fb-like' data-href='http://main.eternalgames.net/news.aspx?ID=" +
                            reader("ID").ToString +
                            "' data-send='false' data-layout='button_count' data-width='450' data-show-faces='false'></div></div>"

                        finalString &= _
                            "<h2>" +
                            reader("n_Title") +
                            "</h2><h3>Posted By: " +
                            reader("n_PostedBy") +
                            " on " + curDate + " at " + curTime + " EST" +
                            "</h3><hr/>" +
                            "<p>" +
                            HttpContext.Current.Server.HtmlDecode(reader("n_News")) +
                            "</p>"

                        finalString &= _
                            "<hr/><p style='text-align:right !important;margin-bottom: -8px;'><a href='news.aspx?ID=" + reader("ID").ToString + "'>Permalink</a>"
                        If auth = "S" Then
                            finalString &= " | <a href='./Admin/editPost.aspx?ID=" + reader("ID").ToString + "'>Edit Post</a> | <a href='./Admin/newsManage.aspx?newsAction=trun&id=" +
                        reader("ID").ToString + "'>Delete Post</a></p></div>"
                        Else
                            finalString &= "</p></div>"
                        End If
                    Next
                Else
                    While (reader.Read())

                        Dim temp As String
                        Dim temp2(2) As String
                        Dim curDate As String
                        Dim curTime As String

                        temp = reader("n_DateTime")
                        temp2 = temp.Split(" ")
                        curDate = temp2(0)
                        Dim currentDate As Date = temp2(1)
                        curTime = Format(currentDate, "h:mm tt")

                        If isEdit Then
                            finalString &= _
                                "<div class='box'>"
                        Else
                            finalString &= _
                                "<div class='box'>"
                        End If

                        finalString &= _
                            "<h2>" +
                            reader("n_Title") +
                            "</h2><h3>Posted By: " +
                            reader("n_PostedBy") +
                            " on " + curDate + " at " + curTime + " EST" +
                            "</h3><hr/><p>" +
                            HttpContext.Current.Server.HtmlDecode(reader("n_News")) +
                            "</p>"

                        finalString &= _
                            "<hr/><p style='text-align:right !important;margin-bottom: -8px;'><a href='news.aspx?ID=" + reader("ID").ToString + "'>Permalink</a>"
                        If auth = "S" Then
                            finalString &= " | <a href='./Admin/editPost.aspx?ID=" + reader("ID").ToString + "'>Edit Post</a> | <a href='./Admin/newsManage.aspx?newsAction=trun&id=" +
                        reader("ID").ToString + "'>Delete Post</a></p></div>"
                        Else
                            finalString &= "</p></div>"
                        End If
                    End While
                End If

                reader.Close()
            Else
                finalString = "<div class='box'><h2>Oops!</h2><h3>Problem Loading News</h3><hr/><p>It looks like the news posting you tried to view doesn't exist! If you feel like you reached this page in error, please contact an Administrator.</p><p><a href='default.aspx'>Back To Home</a></p></div>"
            End If

            connection.Close()

            Return finalString

        End Using
    End Function

    Public Function loadEventsTable(ByVal auth As String, ByVal id As String, ByVal loadByID As Boolean, ByVal isEdit As Boolean, Optional ByVal isMini As Boolean = False, Optional ByVal isLimited As Boolean = False) As String

        Dim finalString As String = "<div class='box'><h2 style='color: #59B200'>Planned Events:</h2><h3>Click event title to view full event details. Check back daily!</h3><hr/>"

        Dim queryString As String = "SELECT TOP 5 * FROM WEB_EVENTS_TBL WHERE n_Authority = 'F' ORDER BY n_DateTime DESC"


        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand

            command.CommandText = queryString
            command.Connection = connection

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If Not dt.Rows.Count <= 0 Then
                Dim reader As SqlDataReader = command.ExecuteReader

                While (reader.Read())

                    Dim temp As String
                    Dim temp2(2) As String
                    Dim curDate As String
                    Dim curTime As String

                    temp = reader("n_DateTime")
                    temp2 = temp.Split(" ")
                    curDate = temp2(0)
                    Dim currentDate As Date = temp2(1)
                    curTime = Format(currentDate, "h:mm tt")

                    finalString += "<li><a href='events.aspx?ID=" +
                        reader("ID").ToString + "'>" +
                        reader("n_Title") +
                        "</a> | Posted on " +
                        curDate +
                        "</li>"

                End While
            Else
                finalString += "<p>No events have been posted; check back soon!</p>"
            End If

            finalString += "</div>"

        End Using

        Return finalString

    End Function

    Public Function loadNewsTable(ByVal auth As String, ByVal id As String, ByVal loadByID As Boolean, ByVal isEdit As Boolean, Optional ByVal isMini As Boolean = False, Optional ByVal isLimited As Boolean = False) As String

        Dim finalString As String = "<div class='box'><h2>Latest News:</h2><h3>Click news title to view full news posting. Check back daily!</h3><hr/>"

        Dim queryString As String = "SELECT TOP 5 * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'F' ORDER BY n_DateTime DESC"


        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand

            command.CommandText = queryString
            command.Connection = connection

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If Not dt.Rows.Count <= 0 Then
                Dim reader As SqlDataReader = command.ExecuteReader

                While (reader.Read())

                    Dim temp As String
                    Dim temp2(2) As String
                    Dim curDate As String
                    Dim curTime As String

                    temp = reader("n_DateTime")
                    temp2 = temp.Split(" ")
                    curDate = temp2(0)
                    Dim currentDate As Date = temp2(1)
                    curTime = Format(currentDate, "h:mm tt")

                    finalString += "<li><a href='news.aspx?ID=" +
                        reader("ID").ToString + "'>" +
                        reader("n_Title") +
                        "</a> | Posted on " +
                        curDate +
                        "</li>"

                End While
            Else
                finalString += "<p>No news have been posted; check back soon!</p>"
            End If

            finalString += "</div>"

        End Using

        Return finalString

    End Function

    Public Function loadEditNews(ByVal auth As String, ByVal id As String, ByVal loadByID As Boolean, ByVal isEdit As Boolean, Optional ByVal isMini As Boolean = False, Optional ByVal isLimited As Boolean = False) As String
        Dim finalString As String = ""

        Dim queryString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'F' ORDER BY n_DateTime DESC"
        Dim adminString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL ORDER BY n_DateTime DESC"
        Dim gmString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE n_Authority = 'O' OR n_Authority = 'F' ORDER BY n_DateTime DESC"

        Dim idString As String =
            "SELECT * FROM WEB_NEWSMAIN_TBL WHERE ID = " + id

        Using connection As New SqlConnection(webPanelConnectionString)
            Dim command As New SqlCommand
            command.Connection = connection

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            If loadByID = False Then
                If auth = "S" Then
                    command.CommandText = adminString
                ElseIf auth = "O" Then
                    command.CommandText = gmString
                Else
                    command.CommandText = queryString
                End If
            Else
                command.CommandText = idString
            End If

            connection.Open()

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If Not dt.Rows.Count <= 0 Then
                Dim reader As SqlDataReader = command.ExecuteReader

                If isLimited Then
                    For counter As Integer = 0 To 4 Step 1
                        reader.Read()

                        Dim temp As String
                        Dim temp2(2) As String
                        Dim curDate As String
                        Dim curTime As String

                        If counter >= dt.Rows.Count Then
                            Exit For
                        End If

                        temp = reader("n_DateTime")
                        temp2 = temp.Split(" ")
                        curDate = temp2(0)
                        Dim currentDate As Date = temp2(1)
                        curTime = Format(currentDate, "h:mm tt")

                        finalString &= _
                            "<div class='box'>" +
                            "<div style='float: right;'>" +
                            "<div class='fb-like' data-href='http://main.eternalgames.net/news.aspx?ID=" +
                            reader("ID").ToString +
                            "' data-send='false' data-layout='button_count' data-width='450' data-show-faces='false'></div></div>"

                        finalString &= _
                            "<h2>" +
                            reader("n_Title") +
                            "</h2><h3>Posted By: " +
                            reader("n_PostedBy") +
                            " on " + curDate + " at " + curTime + " EST" +
                            "</h3><hr/>" +
                            "<p>" +
                            HttpContext.Current.Server.HtmlDecode(reader("n_News")) +
                            "</p>"

                        finalString &= _
                            "<hr/><p style='text-align:right !important;margin-bottom: -8px;'><a href='../news.aspx?ID=" + reader("ID").ToString + "'>Permalink</a>"
                        If auth = "S" Then
                            finalString &= " | <a href='./editPost.aspx?ID=" + reader("ID").ToString + "'>Edit Post</a> | <a href='./newsManage.aspx?newsAction=trun&id=" +
                        reader("ID").ToString + "'>Delete Post</a></p></div>"
                        Else
                            finalString &= "</p></div>"
                        End If
                    Next
                Else
                    While (reader.Read())

                        Dim temp As String
                        Dim temp2(2) As String
                        Dim curDate As String
                        Dim curTime As String

                        temp = reader("n_DateTime")
                        temp2 = temp.Split(" ")
                        curDate = temp2(0)
                        Dim currentDate As Date = temp2(1)
                        curTime = Format(currentDate, "h:mm tt")

                        If isEdit Then
                            finalString &= _
                                "<div class='box'>"
                        Else
                            finalString &= _
                                "<div class='box'>"
                        End If

                        finalString &= _
                            "<h2>" +
                            reader("n_Title") +
                            "</h2><h3>Posted By: " +
                            reader("n_PostedBy") +
                            " on " + curDate + " at " + curTime + " EST" +
                            "</h3><hr/><p>" +
                            HttpContext.Current.Server.HtmlDecode(reader("n_News")) +
                            "</p>"

                        finalString &= _
                            "<hr/><p style='text-align:right !important;margin-bottom: -8px;'><a href='../news.aspx?ID=" + reader("ID").ToString + "'>Permalink</a>"
                        If auth = "S" Then
                            finalString &= " | <a href='./editPost.aspx?ID=" + reader("ID").ToString + "'>Edit Post</a> | <a href='./newsManage.aspx?newsAction=trun&id=" +
                        reader("ID").ToString + "'>Delete Post</a></p></div>"
                        Else
                            finalString &= "</p></div>"
                        End If
                    End While
                End If

                reader.Close()
            Else
                finalString = "<div class='box'><h2>Oops!</h2><h3>Problem Loading News</h3><hr/><p>It looks like the news posting you tried to view doesn't exist! If you feel like you reached this page in error, please contact an Administrator.</p><p><a href='default.aspx'>Back To Home</a></p></div>"
            End If

            connection.Close()

            Return finalString

        End Using
    End Function

    Public Function countGameAccounts(ByVal game As String, ByVal account As String) As Integer

        Dim queryString As String = "SELECT * FROM WEB_GAMEACCOUNTS_TBL WHERE g_Game = '" + game + "' AND g_Account = '" + account + "';"

        Using connection As New SqlConnection(webPanelConnectionString)

            Dim cmd As New SqlCommand(queryString, connection)
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            da.SelectCommand = cmd
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            connection.Close()

            Return dt.Rows.Count

        End Using

    End Function

    Public Function getPageText(ByVal page As String) As String
        Dim queryString As String = "SELECT * FROM WEB_PAGES_TBL WHERE p_Name = '" + page + "'"

        Dim pageText As String = ""

        Using connection As New SqlConnection(getConn())
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            reader.Read()

            pageText = reader("p_Content")

            reader.Close()

            connection.Close()

        End Using

        Return pageText
    End Function

    Public Sub updatePageText(ByVal page As String, ByVal content As String)

        Dim queryString As String = "UPDATE WEB_PAGES_TBL SET p_Content = '" + content + "' WHERE p_Name = '" + page + "'"

        Using connection As New SqlConnection(getConn())
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()

        End Using

    End Sub

    Function countSwiftAccounts() As Integer

        Dim queryString As String = "SELECT * FROM WEB_ACCOUNTS_TBL"

        Using connection As New SqlConnection(csLibrary.webPanelCS)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "Accounts")
            dt = ds.Tables("Accounts")

            connection.Close()

            Return dt.Rows.Count
        End Using

    End Function

End Class
