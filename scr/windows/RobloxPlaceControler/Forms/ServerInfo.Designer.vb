<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerInfo))
        StatsListBox = New ListBox()
        SuspendLayout()
        ' 
        ' StatsListBox
        ' 
        StatsListBox.BackColor = SystemColors.InfoText
        StatsListBox.ForeColor = SystemColors.Highlight
        StatsListBox.FormattingEnabled = True
        StatsListBox.ItemHeight = 15
        StatsListBox.Location = New Point(12, 12)
        StatsListBox.Name = "StatsListBox"
        StatsListBox.Size = New Size(198, 154)
        StatsListBox.TabIndex = 0
        ' 
        ' ServerInfo
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaptionText
        ClientSize = New Size(221, 177)
        Controls.Add(StatsListBox)
        ForeColor = SystemColors.Highlight
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "ServerInfo"
        Text = "Info"
        ResumeLayout(False)
    End Sub

    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents StatsListBox As ListBox
End Class
