<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fun
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fun))
        ScreamerButton = New Button()
        SuspendLayout()
        ' 
        ' ScreamerButton
        ' 
        ScreamerButton.BackColor = SystemColors.ActiveCaptionText
        ScreamerButton.Location = New Point(12, 12)
        ScreamerButton.Name = "ScreamerButton"
        ScreamerButton.Size = New Size(319, 40)
        ScreamerButton.TabIndex = 0
        ScreamerButton.Text = "Screamer"
        ScreamerButton.UseVisualStyleBackColor = False
        ' 
        ' Fun
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaptionText
        ClientSize = New Size(348, 68)
        Controls.Add(ScreamerButton)
        ForeColor = SystemColors.Highlight
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Fun"
        Text = "Fun"
        ResumeLayout(False)
    End Sub

    Friend WithEvents ScreamerButton As Button
End Class
