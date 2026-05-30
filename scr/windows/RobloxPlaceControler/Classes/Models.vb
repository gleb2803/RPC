Public Class PropertyItem
    Public Property Value As String
    Public Property Type As String
End Class

Public Class ExplorerNode
    Public Property Name As String
    Public Property ClassName As String
    Public Property Path As String
    Public Property Children As List(Of ExplorerNode)
End Class
Public Class ExplorerNodeWithProperties
    Public Property Name As String
    Public Property ClassName As String
    Public Property Path As String
    Public Property Parameters As Dictionary(Of String, PropertyItem)
End Class

Public Class ExplorerData
    Public Property Tree As ExplorerNode
End Class
Public Class PropertiesData
    Public Property Properties As ExplorerNodeWithProperties
End Class
