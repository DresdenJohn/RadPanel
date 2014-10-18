
Partial Class Admin_irisBanForm
    Inherits System.Web.UI.Page

    Public irisFunc As New irisFunctions

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        actionDropdown.Items.Clear()

        actionDropdown.Items.Add("Ban")
        actionDropdown.Items.Add("Unban")
    End Sub

    Protected Sub banButton_Click(sender As Object, e As EventArgs) Handles banButton.Click

        If irisFunc.isCharValid(characterNameTextbox.Text) = False Then
            resultLabel.Text = "Result: Invalid Character."
            resultLabel.Visible = True
        Else
            If actionDropdown.SelectedIndex = 0 Then
                irisFunc.adjustSLevel(characterNameTextbox.Text, "2")
                resultLabel.Text = "Result: Banned Account."
                resultLabel.Visible = True
            Else
                irisFunc.adjustSLevel(characterNameTextbox.Text, "5")
                resultLabel.Text = "Result: Unbanned Account."
                resultLabel.Visible = True
            End If
        End If

    End Sub
End Class
