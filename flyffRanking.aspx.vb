
Partial Class flyffRanking
    Inherits System.Web.UI.Page

    Public g_username As String = ""
    Public g_email As String = ""
    Public g_password As String = ""
    Public g_auth As String = ""
    Public g_ip As String = ""

    Public webFunc As New webFunctions
    Public irisFunc As New irisFunctions
    Public flyffFunc As New flyffFunctions

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        rankingPlaceholder.Text = flyffFunc.getFrontRanking(50)
    End Sub
End Class
