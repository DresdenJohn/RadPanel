
Partial Class Admin_sendIrisItem
    Inherits System.Web.UI.Page

    Dim irisFunc As New irisFunctions

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        characterNameRequired.IsValid = False
        itemIDRequired.IsValid = False
        amountIsRequired.IsValid = False
    End Sub

    Protected Sub sendCharItemButton_Click(sender As Object, e As EventArgs) Handles sendCharItemButton.Click

        Dim result As String = ""

        If irisFunc.isCharValid(characterNameTextbox.Text) Then
            result = irisFunc.sendIrisItem(irisFunc.getCharIDX(characterNameTextbox.Text), itemIDTextbox.Text, countTextbox.Text)
        Else
            result = "Invalid Char"
        End If

        Response.Redirect("sendIrisItem.aspx?actn=sentIrisItem")

    End Sub
End Class
