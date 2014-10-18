Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Web.UI.Page
Imports System.Data

Public Class paymentWall

    ' Bring in other classes for use '
    Public webVars As New webVars
    Public flyffFunc As New flyffFunctions
    Public webFunc As New webFunctions
    Public cpFunc As New cpFunctions

    ' Declare MSSQL Login info '
    Private SQLUsername As String = "sa"
    Private SQLPassword As String = "Fluffy<3Kitty"
    Private SQLDataSource As String = "localhost"

    ' PaymentWall secret Key '
    Private PWSecretKey As String = "3eee5d4f333ad1c660153f95060ee1a8"

    ' PaymentWall server IPs '
    Private IPAllowed As String() = New String() {"66.220.10.2", "66.220.10.3", "174.36.92.186", "174.36.96.66", "174.36.92.187", "174.36.92.192", "174.37.14.28"}

    Private connectionStringStart As String = "Data Source=" + SQLDataSource + ";Initial Catalog="
    Private connectionStringEnd As String = ";Integrated Security=False;User ID=" + SQLUsername + ";Password=" + SQLPassword

    Private flyffAccountDB As String = "ACCOUNT_DBF"

    Public Sub addPurchase(ByVal accountName As String, ByVal currency As String, ByVal type As String, ByVal insertDate As String, ByVal curIP As String)

        Dim convertDate As Date = Date.Parse(insertDate)

        Dim queryInsertPayment As String =
            "INSERT INTO PaymentWall (UserID, Currency, Type, Date) VALUES ('" +
            accountName + "', '" +
            currency + "', 'Payment', '" +
            convertDate.ToString("dd-MM-yyy hh:mm:ss") + "')" + vbNewLine

        Dim queryUpdateCoins As String =
            "UPDATE Account SET Coins = Coins + " + currency +
            " WHERE UserID = '" + accountName + "'"

        Dim queryChargeback As String =
             "INSERT INTO PaymentWall (UserID, Currency, Type, Date) VALUES ('" +
            accountName + "', '" +
            currency + "', 'Chargeback', '" +
            convertDate.ToString("dd-MM-yyy hh:mm:ss") + "')" + vbNewLine

        Dim queryUpdateGrade As String =
            "UPDATE Account SET UGradeID = 253, PGradeID = 253 WHERE UserID = '" +
            accountName + "'"

        Using connection As New SqlConnection(webVars.flyffAccountCS)
            Dim commandToUse As String = ""

            Dim connectingFromPW As Boolean = False

            For i As Integer = 0 To IPAllowed.Length Step 1
                If curIP = IPAllowed(i) Then
                    connectingFromPW = True
                End If
            Next

            Select Case type
                Case "0" Or "1"
                    commandToUse += queryInsertPayment + queryUpdateCoins
                Case 2
                    commandToUse += queryChargeback + queryUpdateGrade
            End Select

            If connectingFromPW Then
                Dim command As New SqlCommand(commandToUse, connection)

                connection.Open()

                Dim rowsEffected As Integer = command.ExecuteNonQuery()

                connection.Close()
            End If

        End Using

    End Sub

End Class
