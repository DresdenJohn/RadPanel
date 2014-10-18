
Partial Class Admin_redemptionIris
    Inherits System.Web.UI.Page

    Dim irisFunc As New irisFunctions

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

        For i As Integer = 0 To 5 Step 1
            Dim letters() As String = {"A", "B", "C", "D", "E", "F"}

            Dim price As String = "0"
            Dim tokenAmount As String = "0"

            Select Case i
                Case 0
                    price = "10"
                    tokenAmount = "30"
                Case 1
                    price = "25"
                    tokenAmount = "76"
                Case 2
                    price = "50"
                    tokenAmount = "156"
                Case 3
                    price = "75"
                    tokenAmount = "240"
                Case 4
                    price = "100"
                    tokenAmount = "330"
                Case 5
                    price = "150"
                    tokenAmount = "500"
            End Select

            Dim temp As String = "Package " + letters.GetValue(i) + " - " + "$" + price + " - " + tokenAmount + " egTokens"

            packageDropdown.Items.Add(temp)
            packageDropdown.Items(i).Value = tokenAmount
        Next

        characterRequired.IsValid = False

    End Sub

    Protected Sub sendCharItemButton_Click(sender As Object, e As EventArgs) Handles sendCharItemButton.Click

        Dim result As String = ""

        If irisFunc.isCharValid(characterNameTextbox.Text) Then
            result = irisFunc.sendIrisItem(irisFunc.getCharIDX(characterNameTextbox.Text), 809022, CInt(packageDropdown.SelectedValue))
        Else
            result = "Invalid Char"
        End If

        Response.Redirect("redemptionIris.aspx?actn=sentIrisItem&code=" + result)

    End Sub
End Class
