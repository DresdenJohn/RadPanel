
Partial Class Admin_editDownloads
    Inherits System.Web.UI.Page

    Public webFunc As New webFunctions

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        pageEditor.Text = Server.HtmlDecode(webFunc.getPageText("Downloads"))

    End Sub

    Protected Sub savePageTextButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles savePageTextButton.Click

        webFunc.updatePageText("Downloads", Server.HtmlEncode(pageEditor.Text))

        Response.Redirect("./editDownloads.aspx?actn=updatePage")

    End Sub
End Class
