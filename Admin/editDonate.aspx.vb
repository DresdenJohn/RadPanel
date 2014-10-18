
Partial Class Admin_editDonate
    Inherits System.Web.UI.Page

    Public webFunc As New webFunctions

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        pageEditor.Text = Server.HtmlDecode(webFunc.getPageText("Donate"))

    End Sub

    Protected Sub savePageTextButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles savePageTextButton.Click

        webFunc.updatePageText("Donate", Server.HtmlEncode(pageEditor.Text))

        Response.Redirect("./editDonate.aspx?actn=updatePage")

    End Sub
End Class
