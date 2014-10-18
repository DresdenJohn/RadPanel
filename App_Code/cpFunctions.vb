Imports Microsoft.VisualBasic
Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Net
Imports System.IO

Public Class cpFunctions

    '================================================'
    '------------- Skye Control Panel ---------------'
    '================================================'
    ' - All functions related to the control panel
    '   is stored here.
    '================================================'
    ' - Turn cp On and Off
    '       Options:
    '           True = ON
    '           False = OFF
    Private cpIsActive As Boolean = True
    Public csLibrary As New webVars
    '================================================'
    ' - Turn Registrations On and Off
    '       Options:
    '           True = ON
    '           False = OFF
    Private allowRegistrations As Boolean = False
    '==============================================='
    ' - Filters who can login (Doesn't Work)
    '       Options:
    '           'S' = Admins Only
    '           'O' = Admins and GMs Only
    '           'F' = Allow Everyone
    Private cpAuthLevel As Char = "S"
    '================================================'
    ' - Enables control Panel Debugging Mode;
    '   This mode allows entry to the CP without
    '   having to login. Only use while in development!
    '       Options:
    '           True = ON
    '           False = OFF
    Private isDebuggingEnabled As Boolean = True
    '==============================================='
    ' - Control Panel Database Connection Strings
    Private logConnectionString As String = csLibrary.webPanelCS
    '==============================================='
    ' - Begin Functions/Sub-Procedures
    '==============================================='
    'Public Function cpActive() As Boolean

    '    Return cpIsActive

    'End Function

    Public Function cpActiveAuth() As Char

        Return cpAuthLevel

    End Function

    Public Function isDebugging() As Boolean

        Return isDebuggingEnabled

    End Function

    Public Function isRegisterAvailable() As Boolean

        Return allowRegistrations

    End Function

    Public Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String

        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

        Dim sBuilder As New StringBuilder()

        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        Return sBuilder.ToString()

    End Function

    Public Sub logAction(ByVal username As String, ByVal action As String, ByVal ip As String)

        Dim queryString As String =
            "INSERT INTO CPANEL_LOG (account, action, datetime, IP) Values('" & StrConv(username, vbProperCase) & "', '" & action & "', GETDATE(), '" & ip & "');"

        Using connection As New SqlConnection(logConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()
        End Using
    End Sub

    Public Sub clearLog()

        Dim queryString As String = "TRUNCATE TABLE CPANEL_LOG"

        Using connection As New SqlConnection(logConnectionString)
            Dim command As New SqlCommand(queryString, connection)

            connection.Open()

            command.ExecuteNonQuery()

            connection.Close()
        End Using

    End Sub

End Class
