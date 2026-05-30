Public Class Place
    Public Property Id As String
    Public Property Name As String
    Public Property IpList As New List(Of String)

    Public Sub New(id As String, name As String)
        Me.Id = id
        Me.Name = name
    End Sub

    Public Sub AddIp(ip As String)
        If Not IpList.Contains(ip) Then
            IpList.Add(ip)
        End If
    End Sub

    Public Sub RemoveIp(ip As String)
        IpList.Remove(ip)
    End Sub
End Class