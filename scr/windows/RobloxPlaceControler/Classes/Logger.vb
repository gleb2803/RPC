Imports System.IO

Public Class Logger
    Private Shared LogPath As String = Path.Combine(Application.StartupPath, "logs.txt")

    Public Enum LogType
        ServerConnect
        ServerDisconnect
        ServerTryingToConnect
        AdminAction
        ServerError
        MessageRecived
        PropertyChanged
    End Enum

    Public Shared Sub Log(type As LogType, message As String)
        Dim line = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{type}] {message}"
        File.AppendAllText(LogPath, line & Environment.NewLine)
    End Sub
End Class