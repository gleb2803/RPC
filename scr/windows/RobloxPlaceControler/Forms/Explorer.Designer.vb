<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Explorer
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Explorer))
        ExplorerTreeView = New TreeView()
        PropertiesGrid = New DataGridView()
        CType(PropertiesGrid, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ExplorerTreeView
        ' 
        ExplorerTreeView.BackColor = SystemColors.InfoText
        ExplorerTreeView.ForeColor = SystemColors.Highlight
        ExplorerTreeView.Location = New Point(12, 12)
        ExplorerTreeView.Name = "ExplorerTreeView"
        ExplorerTreeView.Size = New Size(409, 562)
        ExplorerTreeView.TabIndex = 0
        ' 
        ' PropertiesGrid
        ' 
        PropertiesGrid.AllowUserToAddRows = False
        PropertiesGrid.AllowUserToDeleteRows = False
        PropertiesGrid.BackgroundColor = SystemColors.AppWorkspace
        PropertiesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        PropertiesGrid.GridColor = SystemColors.InfoText
        PropertiesGrid.Location = New Point(427, 12)
        PropertiesGrid.Name = "PropertiesGrid"
        PropertiesGrid.Size = New Size(492, 562)
        PropertiesGrid.TabIndex = 1
        ' 
        ' Explorer
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaptionText
        ClientSize = New Size(931, 586)
        Controls.Add(PropertiesGrid)
        Controls.Add(ExplorerTreeView)
        ForeColor = SystemColors.Highlight
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Explorer"
        Text = "Explorer"
        CType(PropertiesGrid, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ExplorerTreeView As TreeView
    Friend WithEvents PropertiesGrid As DataGridView
End Class
