Imports System.Data.SqlClient

Partial Class downloads
    Inherits System.Web.UI.Page

    Public webFunc As New webFunctions

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        downloadLiteral.Text = Server.HtmlDecode(webFunc.getPageText("Downloads"))

    End Sub
    
End Class
