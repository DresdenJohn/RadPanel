Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Web.UI.Page
Imports System.Data

Public Class webVars

    Public SQLUsername As String = "DresdenJohn"
    Public SQLPassword As String = "k&cr_@Tewe2ew9t"
    Public SQLDataSource As String = "198.204.232.58"
    Public LocalDataSource As String = "LOCALHOST\MSSQL2008"
    Public BetaDataSource As String = "LOCALHOST\MSSQL2008FB"
    Public useIntegratedLogin As String = "False"

    '======================================================================================================================='

    Private connectionStringStart As String = "Data Source=" + SQLDataSource + ";Initial Catalog="
    Private connectionStringEnd As String = ";Integrated Security=" + useIntegratedLogin + ";User ID=" + SQLUsername + ";Password=" + SQLPassword

    'Private connectionStringStart As String = "Data Source=" + "." + ";Initial Catalog="
    'Private connectionStringEnd As String = ";Integrated Security=" + useIntegratedLogin + ";"

    Private flyffAccountDB As String = "ACCOUNT_DBF"
    Private flyffCharacterDB As String = "CHARACTER_01_DBF"

    Private webPanelDB As String = "WEB_PANEL_DBF"
    Private webClockDB As String = "WEB_CLOCK_DBF"

    Private irisMemberDB As String = "IRIS_MEMBERDB"
    Private irisGameDB As String = "IRIS_GAMEDB"
    Private irisLogDB As String = "IRIS_LOGDB"
    Private irisWebMemberDB As String = "WEB_MEMBERDB"

    '======================================================================================================================='

    Public flyffAccountCS As String = connectionStringStart + flyffAccountDB + connectionStringEnd
    Public flyffCharacterCS As String = connectionStringStart + flyffCharacterDB + connectionStringEnd

    Public webPanelCS As String = connectionStringStart + webPanelDB + connectionStringEnd
    Public webClockCS As String = connectionStringStart + webClockDB + connectionStringEnd

    Public irisMemberCS As String = connectionStringStart + irisMemberDB + connectionStringEnd
    Public irisGameCS As String = connectionStringStart + irisGameDB + connectionStringEnd
    Public irisLogCS As String = connectionStringStart + irisLogDB + connectionStringEnd
    Public irisWebMemberCS As String = connectionStringStart + irisWebMemberDB + connectionStringEnd

End Class
