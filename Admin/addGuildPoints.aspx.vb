
Partial Class Admin_addGuildPoints
    Inherits System.Web.UI.Page

    Public irisFunc As New irisFunctions

    Protected Sub addGuildPointsButton_Click(sender As Object, e As EventArgs) Handles addGuildPointsButton.Click

        irisFunc.addGuildPoints(Integer.Parse(guildLevelsTextbox.Text), Integer.Parse(guildPointsTextbox.Text))

    End Sub
End Class
