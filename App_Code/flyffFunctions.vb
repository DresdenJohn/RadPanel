Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Data

Public Class flyffFunctions

    '================================================'
    '-------- Eternal Control Panel UserDB Class -------'
    '================================================'
    ' - All Functions/Sub-Procedures concerning Flyff
    '   user accounts are located here.
    '================================================'
    ' - Initialize Control Panel Functions for use
    Dim cpFunctions As New cpFunctions
    Dim csLibrary As New webVars
    '================================================'
    ' - Flyff Database Connection Strings
    Private accountConnectionString As String = csLibrary.flyffAccountCS
    Private accountDetailsConnectionString As String = accountConnectionString
    Private characterConnectionString As String = csLibrary.flyffCharacterCS
    '==============================================='
    ' - Begin Functions/Sub-Procedures
    '==============================================='
    Public Function getConnectionString(ByVal connection As String) As String

        If connection = "accountConnect" Then
            Return accountConnectionString
        ElseIf connection = "accountDetails" Then
            Return accountDetailsConnectionString
        ElseIf connection = "characterConnect" Then
            Return characterConnectionString
        Else : Return "No Connection"
        End If

    End Function

    Public Function getEncryptedPassword(ByVal passwordText As String) As String

        Dim passwordSource As String = "L16vobDwmk" + passwordText

        Using md5Hash As MD5 = MD5.Create()
            Return cpFunctions.GetMd5Hash(md5Hash, passwordSource)
        End Using

    End Function

    Public Function registerUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal ip As String) As String

        Dim passwordHash As String = getEncryptedPassword(password)

        Dim con As New SqlConnection(accountConnectionString)
        Dim cmd As New SqlCommand("usp_ASP_Registration", con)
        cmd.CommandType = Data.CommandType.StoredProcedure

        With cmd.Parameters
            .AddWithValue("@account", username.ToString().ToLower())
            .AddWithValue("@password", passwordHash)
            .AddWithValue("@cash", 0)
        End With

        Try
            con.Open()
            cmd.ExecuteNonQuery()
        Catch ex As SqlException
            Return "Registration Failed: Account Already Exists"
        Finally
            con.Close()
        End Try

        '==============================================================================================='

        Dim con2 As New SqlConnection(accountDetailsConnectionString)
        Dim cmd2 As New SqlCommand("usp_ASP_Registration2", con2)
        cmd2.CommandType = Data.CommandType.StoredProcedure

        With cmd2.Parameters
            .AddWithValue("@account", username.ToString().ToLower())
            .AddWithValue("@email", email)
        End With

        Try
            con2.Open()
            cmd2.ExecuteNonQuery()
        Catch ex As SqlException
            Return "Registration Failed: Account Already Exists"

        Finally
            con2.Close()

        End Try

        Return "Successful Registration for Username: " + username


    End Function

    Public Function isBanned(ByVal username As String) As Boolean
        Dim isBannedBool As Boolean = True
        Dim BlockTime As String = ""
        Dim WebTime As String = ""

        Dim queryString As String =
            "SELECT * FROM ACCOUNT_TBL_DETAIL WHERE account = '" & username.ToLower & "';"

        Using connection As New SqlConnection(accountDetailsConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                BlockTime = reader("BlockTime")
                WebTime = reader("WebTime")
            End While

            If BlockTime = "21000101" And WebTime = "21000101" Then
                Return True
            End If
        End Using

        Return False
    End Function

    Public Function banUser(ByVal username As String) As String

        Dim queryString As String =
            "UPDATE ACCOUNT_TBL_DETAIL SET BlockTime = '21000101', WebTime = '21000101' WHERE account = '" & username.ToLower & "';"

        Using connection As New SqlConnection(accountDetailsConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim rowsEffected As Integer = command.ExecuteNonQuery()

            connection.Close()

            If rowsEffected = 0 Then
                Return "User does not exist!"
            Else
                Return "Banned: " & StrConv(username, vbProperCase) & vbNewLine
            End If

        End Using

    End Function

    Public Function unbanUser(ByVal username As String) As String

        Dim thisDate As Date = DateValue(Now)

        Dim nowDate As String = thisDate.ToString("yyyyMMdd")

        Dim queryString As String =
            "UPDATE ACCOUNT_TBL_DETAIL SET BlockTime = " & nowDate & ", WebTime = " & nowDate & " WHERE account = '" & username & "';"

        Using connection As New SqlConnection(accountDetailsConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim rowsEffected As Integer = command.ExecuteNonQuery()

            connection.Close()

            If rowsEffected = 0 Then
                Return "User does not exist!"
            Else
                Return "Unbanned: " & StrConv(username, vbProperCase) & vbNewLine
            End If

        End Using
    End Function

    Public Function checkAccount(ByVal textUsername As String, ByVal textPassword As String) As String
        Dim username As String = ""
        Dim password As String = ""

        Dim queryString As String =
            "SELECT * FROM ACCOUNT_TBL WHERE account = '" & textUsername.ToLower & "';"

        Using connection As New SqlConnection(accountConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                username = reader("account")
                password = reader("password")
            End While

            If Not password = textPassword Then
                Return "Invalid Login"
            End If

            reader.Close()
        End Using

        Return "Success"
    End Function

    Public Function getUserAuth(ByVal username As String) As String
        Dim userAuth As String = "N"

        Dim queryString As String =
            "SELECT * FROM ACCOUNT_TBL_DETAIL WHERE account = '" & username & "';"

        Using connection As New SqlConnection(accountDetailsConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader

            While reader.Read()
                userAuth = reader("m_chLoginAuthority")
            End While

            reader.Close()
        End Using

        Return userAuth
    End Function

    Public Function changePassword(ByVal username As String, ByVal currentPassword As String, ByVal newPassword As String) As Boolean

        If checkAccount(username.ToLower, currentPassword) = "Success" Then
            Dim queryString As String =
                        "UPDATE ACCOUNT_TBL SET password = '" & newPassword & "', id_no2 = '" & newPassword & "' WHERE account = '" & username.ToLower & "';"

            Using connection As New SqlConnection(accountConnectionString)
                Dim command As New SqlCommand(queryString, connection)

                connection.Open()

                command.ExecuteNonQuery()

                connection.Close()
            End Using
        Else
            Return False
        End If

        Return True

    End Function

    Public Function getWarningCount(ByVal username As String) As Integer
        Dim warningCount As Integer = 0


        Return warningCount
    End Function

    Public Sub checkAccountStatus(ByVal username As String)
        Dim warningCount As Integer = getWarningCount(username)

        If warningCount >= 3 Then
            banUser(username)

            'cpFunctions.logAction("SYSTEM", "Banned: " + StrConv(username, vbProperCase) + " for reaching 3 warnings", "127.0.0.1")
        End If

    End Sub

    Public Function countFlyffAccounts() As Integer

        Using connection As New SqlConnection(accountConnectionString)
            Dim cmd As New SqlCommand
            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            connection.Open()

            cmd = connection.CreateCommand

            cmd.CommandText = "SELECT * FROM ACCOUNT_TBL"
            da.SelectCommand = cmd
            da.Fill(ds, "Accounts")
            dt = ds.Tables("Accounts")

            connection.Close()

            Return dt.Rows.Count

        End Using

    End Function

    Public Function countFlyffChars() As Integer

        Dim queryString As String = "SELECT * FROM CHARACTER_TBL"

        Using connection As New SqlConnection(characterConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dt As New DataTable

            da.SelectCommand = command
            da.Fill(ds, "Chars")
            dt = ds.Tables("Chars")

            connection.Close()

            Return dt.Rows.Count
        End Using
    End Function

    Public Function countFlyffGuilds() As Integer
        Dim queryString As String = "SELECT * FROM GUILD_TBL"

        Using connection As New SqlConnection(characterConnectionString)
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

    Public Function getFrontRanking(ByVal amount As Integer) As String

        Dim finalString As String = ""

        ' So I heard you like long queries
        Dim queryString As String = "SELECT m_szName, m_dwSex, m_nJob, m_nLevel, m_nExp1, m_nExp2, TotalPlayTime, m_chAuthority FROM CHARACTER_TBL WHERE (m_chAuthority = 'F') ORDER BY m_nLevel DESC, m_nExp1 DESC, m_nExp2 DESC, m_nJob DESC"

        Using connection As New SqlConnection(csLibrary.flyffCharacterCS)
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
                    "<td width='150px'>Pos.</td>" +
                    "<td width='250px'>Class</td>" +
                    "<td width='250px' style='text-align:left !important;'>Character Name</td>" +
                    "<td width='100'>Level</td></tr>"

                For i As Integer = 1 To amount Step 1

                    If dt.Rows.Count < i Then
                        Exit For
                    End If

                    reader.Read()

                    finalString += "<tr>" + "<td style='text-align:center;'>" + i.ToString + "</td>" + "<td style='text-align:center;'><div class='imgTooltip'><img class='raceIcon' src='./Assets/Icons/"

                    finalString += "g" + reader("m_dwSex").ToString + ".png' style='margin-right:5px;' /><span class='classic'>" + getRaceText(reader("m_dwSex")) + "</span></div>"

                    finalString += "<div class='imgTooltip'><img class='classIcon' src='./Assets/Icons/c" + reader("m_nJob").ToString + ".bmp' /><span class='classic'>" + getJobText(reader("m_nJob"))

                    finalString += "</span></div></td>" + "<td>" + reader("m_szName").ToString + "</td>" + "<td style='text-align:center;'>" + reader("m_nLevel").ToString + "</td></tr>"


                Next

                finalString += "</table>"
            Else
                finalString += "<p>Sorry, no characters have gotten here yet! Why haven't you started?</p>"
            End If

            connection.Close()

        End Using

        Return finalString

    End Function

    Public Function getRaceText(ByRef gender As Integer) As String

        Dim finalString As String = ""

        Select Case gender
            Case 0
                finalString += "Male "
            Case Else
                finalString += "Female "
        End Select

        Return finalString

    End Function

    Public Function getJobText(ByRef job As Integer) As String

        Dim jobText As String = ""

        Select Case job
            Case 0
                jobText = "Vagrant" 'First'
            Case 1
                jobText = "Mercenary" 'Second'
            Case 2
                jobText = "Acrobat" 'Second'
            Case 3
                jobText = "Assist" 'Second'
            Case 4
                jobText = "Magician" 'First'
            Case 6
                jobText = "Knight" 'Second'
            Case 7
                jobText = "Blade" 'Second'
            Case 8
                jobText = "Jester" 'First'
            Case 9
                jobText = "Ranger" 'Second'
            Case 10
                jobText = "Ringmaster" 'Second'
            Case 11
                jobText = "Billposter" 'First'
            Case 12
                jobText = "Psykeeper" 'Second'
            Case 13
                jobText = "Elementor" 'Second'
            Case 16
                jobText = "Master Knight" 'First'
            Case 17
                jobText = "Master Blade" 'Second'
            Case 18
                jobText = "Master Jester" 'Second'
            Case 19
                jobText = "Master Ranger" 'Second'
            Case 20
                jobText = "Master Ringmaster" 'First'
            Case 21
                jobText = "Master Billposter" 'Second'
            Case 22
                jobText = "Master Psykeeper" 'Second'
            Case 23
                jobText = "Master Elementor" 'Third'
            Case 24
                jobText = "Hero Knight" 'Third'
            Case 25
                jobText = "Hero Blade" 'Third'
            Case 26
                jobText = "Hero Jester" 'Third'
            Case 27
                jobText = "Hero Ranger" 'Third'
            Case 28
                jobText = "Hero Ringmaster" 'Third'
            Case 29
                jobText = "Hero Billposter" 'Third'
            Case 30
                jobText = "Hero Psykeeper" 'Third'
            Case 31
                jobText = "Hero Elementor" 'Third'
            Case 32
                jobText = "Templar" 'Third'
            Case 33
                jobText = "Slayer" 'Third'
            Case 34
                jobText = "Harlequin" 'Third'
            Case 35
                jobText = "Crackshooter" 'Third'
            Case 36
                jobText = "Seraph" 'Third'
            Case 37
                jobText = "ForceMaster" 'Third'
            Case 38
                jobText = "Arcanist" 'Third'
            Case 39
                jobText = "Mentalist" 'Third'
        End Select

        Return jobText

    End Function

    Public Function getAccountNames(ByVal webID As String) As List(Of String)
        Dim allAccounts As New List(Of String)

        Dim query As String = "SELECT * FROM WEB_GAMEACCOUNTS_TBL WHERE g_Game = 'Flyff' AND g_Account = '" + webID + "'"

        Using connection As New SqlConnection(csLibrary.webPanelCS)
            Dim command As New SqlCommand(query, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read
                allAccounts.Add(reader("g_IngameUser"))
            End While

        End Using

        Return allAccounts
    End Function

    Public Function getCharacterNames(ByVal flyffAccount As String) As List(Of String)
        Dim allCharacters As New List(Of String)

        Dim query As String = "SELECT * FROM CHARACTER_TBL WHERE account = '" + flyffAccount + "' AND isBlock = 'F'"

        Using connection As New SqlConnection(csLibrary.flyffCharacterCS)
            Dim command As New SqlCommand(query, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read
                allCharacters.Add(reader("m_szName"))
            End While

        End Using

        Return allCharacters

    End Function

    Public Function getCharInfo(ByVal flyffCharacter As String) As List(Of String)
        Dim charInfo As New List(Of String)

        Dim query As String = "SELECT * FROM CHARACTER_TBL WHERE m_szName = '" + flyffCharacter + "' AND isBlock ='F'"

        Using connection As New SqlConnection(csLibrary.flyffCharacterCS)
            Dim command As New SqlCommand(query, connection)

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read



            End While
        End Using

        Return charInfo

    End Function

End Class
