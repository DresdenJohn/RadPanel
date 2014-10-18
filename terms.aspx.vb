
Partial Class terms
    Inherits System.Web.UI.Page

    Dim webFunc As New webFunctions()

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        termsLiteral.Text = Server.HtmlDecode(webFunc.getPageText("Terms of Service"))

    End Sub

End Class
