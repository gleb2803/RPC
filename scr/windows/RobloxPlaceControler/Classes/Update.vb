Public Class Update
    Public Property Type As String
    Public Property Commands As List(Of String)
    Public Property Daemon As Boolean
    Public Property Delay As Integer

    Public Sub New(Type As String)
        Me.Type = Type
        Me.Commands = New List(Of String)()
    End Sub

    Public Sub AddCommand(Command As String)
        If Not Commands.Contains(Command) Then
            Commands.Add(Command)
        End If
    End Sub

    Public Sub RemoveCommand(Command As String)
        Commands.Remove(Command)
    End Sub
End Class