Public Class Fun
    Public MainForm As Form1

    Private Sub Fun_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ScreamerButton_Click(sender As Object, e As EventArgs) Handles ScreamerButton.Click
        Dim update = New Update("executeTheCommands")
        update.AddCommand("screamer;;")
        update.Daemon = True
        MainForm.PendingUpdates.Add(update)
    End Sub
End Class