Public Class Explorer
    Public MainForm As Form1

    Public Sub LoadTree(node As ExplorerNode)
        ExplorerTreeView.Nodes.Clear()
        Dim rootNode = BuildNode(node)
        ExplorerTreeView.Nodes.Add(rootNode)
        rootNode.Expand()
    End Sub

    Private Function BuildNode(node As ExplorerNode) As TreeNode
        Dim treeNode As New TreeNode($"{node.Name} [{node.ClassName}]")
        treeNode.Tag = node

        If node.Children IsNot Nothing Then
            For Each child In node.Children
                treeNode.Nodes.Add(BuildNode(child))
            Next
        End If

        Return treeNode
    End Function

    Private Sub ExplorerTreeView_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles ExplorerTreeView.NodeMouseClick
        If e.Button = MouseButtons.Left Then
            Dim node = TryCast(ExplorerTreeView.SelectedNode?.Tag, ExplorerNode)
            If node IsNot Nothing Then
                ' запрашиваем свойства с сервера
                MainForm.PendingUpdates.Add(New Update("GetParams") With {
                .Commands = New List(Of String) From {node.Path}
            })
            End If
        End If
    End Sub

    Public Sub LoadProperties(node As ExplorerNodeWithProperties)
        PropertiesGrid.Rows.Clear()
        PropertiesGrid.Columns.Clear()

        PropertiesGrid.Columns.Add("Property", "Property")
        PropertiesGrid.Columns.Add("Value", "Value")
        PropertiesGrid.Columns.Add("Type", "Type")

        PropertiesGrid.Columns(0).Width = 150
        PropertiesGrid.Columns(1).Width = 200
        PropertiesGrid.Columns(2).Width = 100
        PropertiesGrid.Columns(0).ReadOnly = True
        PropertiesGrid.Columns(2).ReadOnly = True
        PropertiesGrid.Tag = node.Path

        If node.Parameters IsNot Nothing Then
            For Each kvp In node.Parameters
                PropertiesGrid.Rows.Add(kvp.Key, kvp.Value.Value, kvp.Value.Type)
            Next
        End If
    End Sub

    Private Sub PropertiesGrid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles PropertiesGrid.CellEndEdit
        If e.ColumnIndex <> 1 Then Return

        Dim path = PropertiesGrid.Tag?.ToString()
        If path Is Nothing Then Return

        Dim propName = PropertiesGrid.Rows(e.RowIndex).Cells(0).Value?.ToString()
        Dim newValue = PropertiesGrid.Rows(e.RowIndex).Cells(1).Value?.ToString()

        If propName IsNot Nothing AndAlso newValue IsNot Nothing Then
            MainForm.PendingUpdates.Add(New Update("SetProperty") With {
            .Commands = New List(Of String) From {path, propName, newValue}
        })
            Logger.Log(Logger.LogType.PropertyChanged, $"ObjPath : {path}, Name : {propName}, Value : {newValue}")
        End If
    End Sub

    Private Sub ExplorerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim menu As New ContextMenuStrip()
        Dim itemDelete = menu.Items.Add("Delete")
        Dim itemRename = menu.Items.Add("Rename")
        Dim itemCreate = menu.Items.Add("Create Object")
        menu.Items.Add(New ToolStripSeparator())
        Dim itemUpdate = menu.Items.Add("Update")
        ExplorerTreeView.ContextMenuStrip = menu

        AddHandler ExplorerTreeView.MouseDown, Sub(s, ee)
                                                   If ee.Button = MouseButtons.Right Then
                                                       Dim node = ExplorerTreeView.GetNodeAt(ee.Location)
                                                       If node IsNot Nothing Then
                                                           ExplorerTreeView.SelectedNode = node
                                                       End If
                                                   End If
                                               End Sub

        AddHandler itemDelete.Click, Sub(s, ee)
                                         Dim node = ExplorerTreeView.SelectedNode
                                         If node Is Nothing Then Return
                                         Dim explorerNode = TryCast(node.Tag, ExplorerNode)
                                         If explorerNode Is Nothing Then Return
                                         Dim path = explorerNode.Path

                                         Dim result = MessageBox.Show($"Delete {path}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                                         If result = DialogResult.Yes Then
                                             MainForm.PendingUpdates.Add(New Update("ExplorerDelete") With {
                                                 .Commands = New List(Of String) From {path}
                                             })
                                             node.Remove()
                                         End If
                                     End Sub

        AddHandler itemRename.Click, Sub(s, ee)
                                         Dim node = ExplorerTreeView.SelectedNode
                                         If node Is Nothing Then Return
                                         Dim explorerNode = TryCast(node.Tag, ExplorerNode)
                                         If explorerNode Is Nothing Then Return
                                         Dim path = explorerNode.Path

                                         Dim newName = InputBox("New Name:", "Rename", path.Split(".").Last())
                                         If newName <> "" Then
                                             MainForm.PendingUpdates.Add(New Update("ExplorerRename") With {
                                                 .Commands = New List(Of String) From {path, newName}
                                             })
                                             node.Text = $"{newName} [{node.Text.Split("[")(1)}"
                                         End If
                                     End Sub

        AddHandler itemCreate.Click, Sub(s, ee)
                                         Dim node = ExplorerTreeView.SelectedNode
                                         If node Is Nothing Then Return
                                         Dim explorerNode = TryCast(node.Tag, ExplorerNode)
                                         If explorerNode Is Nothing Then Return
                                         Dim path = explorerNode.Path

                                         Dim className = InputBox("ClassName New Object:", "Create Object", "Part")
                                         If className <> "" Then
                                             MainForm.PendingUpdates.Add(New Update("ExplorerCreate") With {
                                                 .Commands = New List(Of String) From {path, className}
                                             })
                                         End If
                                     End Sub

        AddHandler itemUpdate.Click, Sub(s, ee)
                                         Dim update = New Update("ExplorerUpdate")
                                         MainForm.PendingUpdates.Add(update)
                                     End Sub
    End Sub
End Class