Public Class ServerData
    Public Property Players As List(Of PlayerData)
    Public Property MemoryUsageMb As Double
    Public Property PlayerCount As Integer
    Public Property MaxPlayers As Integer
    Public Property Teams As List(Of Object)
    Public Property ContactsCount As Integer
    Public Property PlaceId As Long
    Public Property PlaceVersion As Integer
    Public Property InstanceCount As Integer
    Public Property MovingPrimitivesCount As Integer
    Public Property HeartbeatTime As Double
    Public Property ServerTime As Double
    Public Property DataReceiveKbps As Double
    Public Property DataSendKbps As Double
    Public Property PhysicsReceiveKbps As Double
    Public Property PhysicsSendKbps As Double
    Public Property LoadStringEnabled As Boolean
End Class

Public Class PlayerData
    Public Property Name As String
    Public Property DisplayName As String
    Public Property UserId As Long
    Public Property AccountAge As Integer
    Public Property countryCode As String
End Class