Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Data

Public Class irisFunctions

    '================================================'
    '-------- Iris Online Control Functions ---------'
    '================================================'
    ' - All Functions/Sub-Procedures concerning Iris
    '   user accounts are located here.
    '================================================'
    ' - Initialize Control Panel Functions for use
    Dim cpFunctions As New cpFunctions
    Dim webFunc As New webFunctions
    Dim csLibrary As New webVars
    ' - Borrow some of flyff's functions
    Dim flyffFunc As New flyffFunctions
    '================================================'
    ' - Website Database Connection String
    Dim webConnectionString As String = csLibrary.webPanelCS
    ' - Iris Database Connection Strings
    Dim irisMemberConnectionString As String = csLibrary.irisMemberCS
    Dim irisAccountConnectionString As String = csLibrary.irisWebMemberCS
    Dim irisGameConnectionString As String = csLibrary.irisGameCS
    Dim irisLogConnectionString As String = csLibrary.irisLogCS
    '==============================================='
    ' - Begin Functions/Sub-Procedures
    '==============================================='

    Public Function getFrontRanking() As String

        Dim finalString As String = ""

        ' So I heard you like long queries
        Dim queryString As String = "SELECT WEB_MEMBERDB.dbo.member_info.id_idx,dbo.TB_CHARACTER.NAME,WEB_MEMBERDB.dbo.member_info.Slevel,dbo.TB_CHARACTER.[LEVEL],dbo.TB_CHARACTER.RACE,dbo.TB_CHARACTER.GENDER,dbo.TB_CHARACTER.JOB,dbo.TB_CHARACTER.EXP,dbo.TB_CHARACTER.USER_IDX FROM dbo.TB_CHARACTER INNER JOIN WEB_MEMBERDB.dbo.member_info ON dbo.TB_CHARACTER.USER_IDX=WEB_MEMBERDB.dbo.member_info.id_idx WHERE WEB_MEMBERDB.dbo.member_info.Slevel='5'AND dbo.TB_CHARACTER.APPLY='0'GROUP BY WEB_MEMBERDB.dbo.member_info.id_idx,dbo.TB_CHARACTER.NAME,WEB_MEMBERDB.dbo.member_info.Slevel,dbo.TB_CHARACTER.[LEVEL],dbo.TB_CHARACTER.RACE,dbo.TB_CHARACTER.GENDER,dbo.TB_CHARACTER.JOB,dbo.TB_CHARACTER.EXP,dbo.TB_CHARACTER.USER_IDX ORDER BY LEVEL DESC,EXP DESC"

        Using connection As New SqlConnection(irisAccountConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            Dim reader As SqlDataReader = command.ExecuteReader()

            If dt.Rows.Count > 0 Then

                finalString += "<table id='rankFrontTable'>" +
                    "<tr class='headerRow'>" +
                    "<td width='50px'>Pos.</td>" +
                    "<td width='150px'>Race &amp; Class</td>" +
                    "<td width='200px' style='text-align:left !important;'>Character Name</td>" +
                    "<td width='60'>Level</td></tr>"

                For i As Integer = 1 To 10 Step 1

                    If dt.Rows.Count < i Then
                        Exit For
                    End If

                    reader.Read()

                    finalString += "<tr>" + "<td style='text-align:center;'>" + i.ToString + "</td>" + "<td style='text-align:center;'><div class='imgTooltip'><img class='raceIcon' src='./Assets/Icons/r"

                    finalString += reader("RACE").ToString + "g" + reader("GENDER").ToString + ".png' /><span class='classic'>" + getRaceText(reader("RACE"), reader("GENDER")) + "</span></div>"

                    finalString += "<div class='imgTooltip'><img class='classIcon' src='./Assets/Icons/" + reader("JOB").ToString + ".png' /><span class='classic'>" + getJobText(reader("JOB"))

                    finalString += "</span></div></td>" + "<td>" + reader("NAME").ToString + "</td>" + "<td style='text-align:center;'>" + reader("LEVEL").ToString + "</td></tr>"


                Next

                finalString += "</table>"
            Else
                finalString += "<p>Sorry, Ranking isn't available.</p>"
            End If

            connection.Close()

        End Using

        Return finalString

    End Function

    Public Function getRaceText(ByRef race As Integer, ByRef gender As Integer) As String

        Dim finalString As String = ""

        Select Case gender
            Case 0
                finalString += "Male "
            Case Else
                finalString += "Female "
        End Select

        Select Case race
            Case 0
                finalString += "Human"
            Case 1
                finalString += "Hybrid"
            Case Else
                finalString += "Elf"
        End Select

        Return finalString

    End Function

    Public Function getJobText(ByRef job As Integer) As String

        Dim jobText As String = ""

        Select Case job
            Case 1000
                jobText = "Fighter" 'First'
            Case 1101
                jobText = "Knight" 'Second'
            Case 1102
                jobText = "Gladiator" 'Second'
            Case 1103
                jobText = "Mercenary" 'Second'
            Case 2000
                jobText = "Mage" 'First'
            Case 2101
                jobText = "Magician" 'Second'
            Case 2102
                jobText = "Priest" 'Second'
            Case 3000
                jobText = "Ranger" 'First'
            Case 3101
                jobText = "Adventurer" 'Second'
            Case 3102
                jobText = "Scout" 'Second'
            Case 4000
                jobText = "Shaman" 'First'
            Case 4101
                jobText = "Warlock" 'Second'
            Case 4102
                jobText = "Sage" 'Second'
            Case 5000
                jobText = "Warrior" 'First'
            Case 5101
                jobText = "Guardian" 'Second'
            Case 5102
                jobText = "Beserker" 'Second'
            Case 5103
                jobText = "Barbarian" 'Second'
            Case 6000
                jobText = "Rogue" 'First'
            Case 6101
                jobText = "Shadow Walker" 'Second'
            Case 6102
                jobText = "Hunter" 'Second'
            Case 4201
                jobText = "Sorcerer" 'Third'
            Case 3202
                jobText = "Sniper" 'Third'
            Case 2202
                jobText = "High Priest" 'Third'
            Case 1202
                jobText = "Templar" 'Third'
            Case 1201
                jobText = "Crusader" 'Third'
            Case 1203
                jobText = "Soul Blader" 'Third'
            Case 5203
                jobText = "Highlander" 'Third'
            Case 4202
                jobText = "Mentalist" 'Third'
            Case 2201
                jobText = "Wizard" 'Third'
            Case 3202
                jobText = "Assassin" 'Third'
            Case 5201
                jobText = "Champion" 'Third'
            Case 5202
                jobText = "Slayer" 'Third'
            Case 6201
                jobText = "Avenger" 'Third'
            Case 6202
                jobText = "Sharpshooter" 'Third'
        End Select

        Return jobText

    End Function

    Public Function sendIrisItem(ByVal charIDX As String, ByVal indexNum As Integer, ByVal count As Integer) As String

        Dim queryString As String = "INSERT INTO [dbo].[TB_INVENTORY] ([CHARACTER_IDX], [ITEM_DEFINE_INDEX], [NUMBER], [COUNT], [SEAL])SELECT " + charIDX + " , " + indexNum.ToString + ",-90 , " + count.ToString + " , [LICENSE_TYPE] FROM [dbo].[TB_ITEM_DEFINE] WHERE [INDEX] = 809022 AND [APPLY] = 0"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            Try
                connection.Open()

                command.ExecuteNonQuery()

                connection.Close()

            Catch ex As SqlException
                Return ex.Message
            End Try

            Return "Package Sent!"
            End Using

    End Function

    Public Function isCharValid(ByVal charName As String) As Boolean

        Dim isValid As Boolean = False

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            cmd = connection.CreateCommand

            cmd.CommandText = "SELECT * FROM TB_CHARACTER WHERE NAME = '" + charName + "' AND NOT APPLY = '1'"
            da.SelectCommand = cmd
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            connection.Close()

            If dt.Rows.Count = 1 Then
                isValid = True
            End If
        End Using

        Return isValid

    End Function

    Public Sub addGuildPoints(ByVal levelsToAdd As Integer, ByVal pointsToAdd As Integer)

        Dim getGuildsQuery As String = "SELECT * FROM TB_GUILD WHERE MASTER_NAME != 'Spanky'"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(getGuildsQuery, connection)

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                Dim guildIdx As String = reader("IDX")
                Dim curLevel As Integer = Integer.Parse(reader("LEVEL"))
                Dim remainPoints As Integer = Integer.Parse(reader("REMAIN_POINT"))
                Dim totalPoints As Integer = Integer.Parse(reader("TOTAL_POINT"))

                curLevel += levelsToAdd
                remainPoints += pointsToAdd
                totalPoints += pointsToAdd

                updateGuildStat(curLevel.ToString, remainPoints.ToString, totalPoints.ToString, guildIdx.ToString)

            End While

            connection.Close()

        End Using

    End Sub

    Public Sub updateGuildStat(ByRef levels As String, ByRef pointsRemain As String, totalPoints As String, ByRef guildIdx As String)

        Dim getGuildsQuery As String =
                "UPDATE TB_GUILD SET LEVEL = " + levels +
                ", REMAIN_POINT = " + pointsRemain +
                ", TOTAL_POINT = " + totalPoints +
                " WHERE IDX = " + guildIdx

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(getGuildsQuery, connection)
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()

        End Using

    End Sub

    Public Sub adjustSLevel(ByVal charName As String, ByVal sLevel As String)
        Dim userIDX As String = ""

        Dim getUserIDXQS As String = "SELECT USER_IDX FROM TB_CHARACTER WHERE NAME = '" + charName + "'"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(getUserIDXQS, connection)

            connection.Open()

            userIDX = command.ExecuteScalar()

            connection.Close()
        End Using

        Dim banUserQS As String = "UPDATE member_info SET SLEVEL = '" + sLevel + "' WHERE id_idx = '" + userIDX + "'"

        Using connection As New SqlConnection(irisAccountConnectionString)
            Dim command As New SqlCommand(banUserQS, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()

        End Using
    End Sub

    Public Function getCharIDX(ByVal charName As String) As String

        If isCharValid(charName) = False Then
            Return "invalidChar"
        End If

        Dim IDX As String = ""

        Dim queryString As String = "SELECT * FROM TB_CHARACTER WHERE NAME = '" + charName + "'"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                IDX = reader("IDX").ToString
            End While

            Return IDX
        End Using

    End Function

    Public Function getUserIDX(ByVal irisUsername As String) As String

        Dim IDX As String = ""

        Dim queryString As String = "SELECT id_idx FROM member_info WHERE id_loginid = '" + irisUsername + "'"

        Using connection As New SqlConnection(irisAccountConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            IDX = command.ExecuteScalar

            connection.Close()

        End Using

        Return IDX

    End Function

    Public Function registerIrisUser(ByVal account As String, ByVal username As String, ByVal password As String, ByVal email As String, ByVal ip As String, ByVal pin As String) As String
        Dim con As New SqlConnection(irisAccountConnectionString)
        Dim cmd As New SqlCommand("AC_sp_CreateAccount", con)
        cmd.CommandType = Data.CommandType.StoredProcedure

        With cmd.Parameters
            .AddWithValue("@strGameAccount", username.ToString().ToLower())
            .AddWithValue("@strGamePWD", password.ToString())
            .AddWithValue("@strToGamePWD", pin.ToString)
            .AddWithValue("@strTjUser", username.ToString().ToLower())
            .AddWithValue("@strSex", 0)
            .AddWithValue("@strage", 18)
            .AddWithValue("@strBirthday", "1992-1-1")
            .AddWithValue("@strTrueId", username.ToString().ToLower())
            .AddWithValue("@strEmail", email.ToString())
            .AddWithValue("@strErrInfo", 0)
        End With

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Catch ex As SqlException
            Return "<h2>An Error has Occured!</h2>" +
                "<h3 style='font-size: 10px;color:black;padding: 10px; border: 1px solid #666; background-color:rgba(255,0,0,0.5); border-radius: 10px;'>" +
                ex.Message.ToString +
                "</h3>"
        End Try
        addGameAccount(account, "Iris", username)
        Return "Successful Registration for username " + StrConv(username, vbProperCase)

    End Function

    Public Sub addGameAccount(ByVal account As String, ByVal game As String, ByVal username As String)
        Dim queryString As String =
            "INSERT INTO WEB_GAMEACCOUNTS_TBL (g_Game, g_Account, g_IngameUser, g_DateCreated) VALUES ('" + game + "', '" + account + "', '" + username + "', GETDATE())"

        Using connection As New SqlConnection(webConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()

        End Using
    End Sub

    Public Function countIrisAccounts() As Integer

        Using connection As New SqlConnection(irisAccountConnectionString)
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            cmd = connection.CreateCommand

            cmd.CommandText = "SELECT * FROM member_info"
            da.SelectCommand = cmd
            da.Fill(ds, "Accounts")
            dt = ds.Tables("Accounts")

            connection.Close()

            Return dt.Rows.Count
        End Using
    End Function

    Public Function countIrisChars() As Integer
        Dim queryString As String = "SELECT * FROM TB_CHARACTER"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "chars")
            dt = ds.Tables("chars")

            connection.Close()

            Return dt.Rows.Count
        End Using
    End Function

    Public Function countIrisGuilds() As Integer
        Dim queryString As String = "SELECT * FROM TB_GUILD"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "Guilds")
            dt = ds.Tables("Guilds")

            connection.Close()

            Return dt.Rows.Count
        End Using
    End Function

    Public Function countOnlineUsers() As Integer
        Dim queryString As String = "SELECT * FROM TB_CONCURRENT_EVENT"

        Using connection As New SqlConnection(irisLogConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            Dim count As Integer = 0

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                count = reader("MAXIMUM")
            End While

            connection.Close()

            Return count

        End Using
    End Function

    Public Function getCharList(ByVal username As String) As String

        Dim result As String = ""

        Dim queryString As String = "SELECT * FROM WEB_GAMEACCOUNTS_TBL WHERE g_Account = '" + username + "' AND g_Game = 'Iris' ORDER BY g_DateCreated DESC"

        Using connection As New SqlConnection(webFunc.getConn())
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            result += "<ul>"

            While reader.Read()
                result += _
                    "<li><p>" + reader("g_IngameUser") + "</p></li>"
            End While
            result += "</ul>"

            Return result

        End Using

    End Function

    Public Function getIrisAccounts(ByVal username As String) As String

        Dim result As String = ""

        Dim rowCount As Integer = 0

        Dim queryString As String = "SELECT g_IngameUser FROM WEB_GAMEACCOUNTS_TBL WHERE g_Account = '" + username + "' AND g_Game = 'Iris' ORDER BY g_DateCreated DESC"

        Using connection As New SqlConnection(webFunc.getConn())
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            rowCount = dt.Rows.Count

            connection.Close()

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            For i As Integer = 1 To rowCount Step 1
                reader.Read()

                If i = rowCount Then
                    result += reader(0)
                Else
                    result += reader(0) + ","
                End If
            Next

            Return result

        End Using

    End Function

    Public Function getIrisCharacters(ByVal irisUsername As String) As String
        Dim result As String = ""

        ' Get User IDX first

        Dim accountIDX As String = getUserIDX(irisUsername)

        Dim rowCount As Integer = 0

        Dim queryString As String = "SELECT NAME FROM TB_CHARACTER WHERE USER_IDX = '" + accountIDX + "' ORDER BY NAME ASC"

        Using connection As New SqlConnection(irisGameConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            rowCount = dt.Rows.Count

            connection.Close()

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            For i As Integer = 1 To rowCount Step 1
                reader.Read()

                If i = rowCount Then
                    result += reader(0)
                Else
                    result += reader(0) + ","
                End If
            Next

            Return result

        End Using
    End Function

    Public Function attachIrisAccount(ByVal egEmail As String, ByVal irisAccount As String, ByVal irisPassword As String) As String

        Dim queryString As String = "SELECT * FROM member_info WHERE id_loginid = '" + irisAccount.ToLower + "' AND id_passwd = '" + getUpperBase64(irisPassword) + "'"

        Using connection As New SqlConnection(irisAccountConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If dt.Rows.Count > 0 Then
                addGameAccount(webFunc.getUsername(egEmail), "Iris", irisAccount)
                Return "Success"
            End If

            Return "Error: Iris Account Does Not Exist Or Password Is Invalid"

            connection.Close()
        End Using

    End Function

    Public Function getUpperBase64(ByVal text As String) As String

        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(text.ToUpper)

        Return Convert.ToBase64String(byt)

    End Function

    Public Sub logVote(ByVal egUsername As String, ByVal irisChar As String, ByVal ip As String)

        Dim result As String = ""

        Dim queryString As String = "INSERT INTO IRIS_VOTE_TBL (v_Account, v_IrisCharacter, v_IP, v_Timestamp) VALUES('" + egUsername + "', '" + irisChar + "', '" + ip + "', (SELECT DATEDIFF(s, '19700101', GETDATE())))"

        Using connection As New SqlConnection(webConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()
        End Using

    End Sub

    Public Function getVoteTimer(ByVal egUsername As String) As Integer

        Dim queryString As String = "SELECT v_TimeStamp FROM IRIS_VOTE_TBL WHERE v_Account = '" + egUsername + "' ORDER BY v_TimeStamp DESC"

        Dim currentTime As Double = DateDiff("S", "1/1/1970", Now())

        Dim lastVoteTime As Double

        Using connection As New SqlConnection(webConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            lastVoteTime = command.ExecuteScalar()

            connection.Close()

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If dt.Rows.Count = 0 Then
                Return 0
            End If

            connection.Close()

        End Using

        Return (currentTime - lastVoteTime)

    End Function

    Public Function checkVote(ByVal egUsername As String) As Boolean

        Dim queryString As String = "SELECT v_TimeStamp FROM IRIS_VOTE_TBL WHERE v_Account = '" + egUsername + "' ORDER BY v_TimeStamp DESC"

        Dim currentTime As Double = DateDiff("S", "1/1/1970", Now())

        Dim lastVoteTime As Double

        Using connection As New SqlConnection(webConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            lastVoteTime = command.ExecuteScalar()

            connection.Close()

            connection.Open()


            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "temp")
            dt = ds.Tables("temp")

            If dt.Rows.Count = 0 Then
                Return True
            End If

            connection.Close()

        End Using

        If (currentTime - lastVoteTime) > 43200 Then
            Return True
        End If

        Return False

    End Function

End Class
