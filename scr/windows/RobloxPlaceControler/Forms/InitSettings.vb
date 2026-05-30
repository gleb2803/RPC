Public Class InitSettings
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Forms.Form1.Port = Convert.ToInt32(PortTextBox.Text)
        My.Forms.Form1.Password = Convert.ToInt32(PasswordTextBox.Text)
        My.Forms.Form1.Enabled = True
        Me.Hide()

        My.Forms.Form1.StartServer()
    End Sub

    Private Sub Closed_Form(sender As Object, e As EventArgs) Handles MyBase.Closed
        My.Forms.Form1.Close()
    End Sub

    Private Sub InitSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class