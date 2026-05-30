<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InitSettings))
        Button1 = New Button()
        Label1 = New Label()
        PortTextBox = New TextBox()
        PasswordTextBox = New TextBox()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.BackColor = SystemColors.ActiveCaptionText
        Button1.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        Button1.ForeColor = SystemColors.Highlight
        Button1.Location = New Point(12, 256)
        Button1.Name = "Button1"
        Button1.Size = New Size(265, 31)
        Button1.TabIndex = 0
        Button1.Text = "Continue"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        Label1.ForeColor = SystemColors.Highlight
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(265, 36)
        Label1.TabIndex = 1
        Label1.Text = "PORT"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' PortTextBox
        ' 
        PortTextBox.Location = New Point(12, 48)
        PortTextBox.Name = "PortTextBox"
        PortTextBox.PlaceholderText = "5000"
        PortTextBox.Size = New Size(265, 23)
        PortTextBox.TabIndex = 2
        PortTextBox.Text = "5000"
        ' 
        ' PasswordTextBox
        ' 
        PasswordTextBox.Location = New Point(12, 160)
        PasswordTextBox.Name = "PasswordTextBox"
        PasswordTextBox.PlaceholderText = "1234"
        PasswordTextBox.Size = New Size(265, 23)
        PasswordTextBox.TabIndex = 4
        PasswordTextBox.Text = "1234"
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        Label2.ForeColor = SystemColors.Highlight
        Label2.Location = New Point(12, 121)
        Label2.Name = "Label2"
        Label2.Size = New Size(265, 36)
        Label2.TabIndex = 3
        Label2.Text = "PASSWORD"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' InitSettings
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaptionText
        ClientSize = New Size(289, 299)
        Controls.Add(PasswordTextBox)
        Controls.Add(Label2)
        Controls.Add(PortTextBox)
        Controls.Add(Label1)
        Controls.Add(Button1)
        ForeColor = SystemColors.ControlLightLight
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "InitSettings"
        Text = "InitSettings"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PortTextBox As TextBox
    Friend WithEvents PasswordTextBox As TextBox
    Friend WithEvents Label2 As Label
End Class
