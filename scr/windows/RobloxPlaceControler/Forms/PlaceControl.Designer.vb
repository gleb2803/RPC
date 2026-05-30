<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlaceControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlaceControl))
        ServesListBox = New ListBox()
        BackButton = New Button()
        PlayersListBox = New ListBox()
        RefreshButton = New Button()
        FlagImage = New PictureBox()
        UsernameLabel = New Label()
        DisplaynameLabel = New Label()
        GiveGearButton = New Button()
        GiveGearBox = New GroupBox()
        GearIdTextBox = New TextBox()
        LoadStringBox = New GroupBox()
        ExecuteCodeButton = New Button()
        CodeRichTextBox = New RichTextBox()
        AccountAgeLabel = New Label()
        ImageGroupBox = New GroupBox()
        ImageJsonUrlTextBox = New TextBox()
        DeleteImageButton = New Button()
        SendImageButton = New Button()
        OtherToolsBox = New GroupBox()
        FunButton = New Button()
        InfoButton = New Button()
        ExplorerButton = New Button()
        ChatBox = New GroupBox()
        ChatListBox = New ListBox()
        SendMessageButton = New Button()
        MessageTextBox = New TextBox()
        CType(FlagImage, ComponentModel.ISupportInitialize).BeginInit()
        GiveGearBox.SuspendLayout()
        LoadStringBox.SuspendLayout()
        ImageGroupBox.SuspendLayout()
        OtherToolsBox.SuspendLayout()
        ChatBox.SuspendLayout()
        SuspendLayout()
        ' 
        ' ServesListBox
        ' 
        ServesListBox.BackColor = SystemColors.InfoText
        ServesListBox.ForeColor = SystemColors.MenuHighlight
        ServesListBox.FormattingEnabled = True
        ServesListBox.ItemHeight = 15
        ServesListBox.Location = New Point(12, 53)
        ServesListBox.Name = "ServesListBox"
        ServesListBox.Size = New Size(120, 154)
        ServesListBox.TabIndex = 0
        ' 
        ' BackButton
        ' 
        BackButton.BackColor = SystemColors.ActiveCaptionText
        BackButton.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        BackButton.Location = New Point(12, 12)
        BackButton.Name = "BackButton"
        BackButton.Size = New Size(120, 35)
        BackButton.TabIndex = 1
        BackButton.Text = "Back"
        BackButton.UseVisualStyleBackColor = False
        ' 
        ' PlayersListBox
        ' 
        PlayersListBox.BackColor = SystemColors.InfoText
        PlayersListBox.Font = New Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        PlayersListBox.ForeColor = SystemColors.MenuHighlight
        PlayersListBox.FormattingEnabled = True
        PlayersListBox.Location = New Point(12, 254)
        PlayersListBox.Name = "PlayersListBox"
        PlayersListBox.Size = New Size(120, 148)
        PlayersListBox.TabIndex = 2
        ' 
        ' RefreshButton
        ' 
        RefreshButton.BackColor = SystemColors.ActiveCaptionText
        RefreshButton.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        RefreshButton.Location = New Point(12, 213)
        RefreshButton.Name = "RefreshButton"
        RefreshButton.Size = New Size(120, 35)
        RefreshButton.TabIndex = 3
        RefreshButton.Text = "Refresh"
        RefreshButton.UseVisualStyleBackColor = False
        ' 
        ' FlagImage
        ' 
        FlagImage.Location = New Point(12, 458)
        FlagImage.Name = "FlagImage"
        FlagImage.Size = New Size(64, 48)
        FlagImage.TabIndex = 4
        FlagImage.TabStop = False
        ' 
        ' UsernameLabel
        ' 
        UsernameLabel.AutoSize = True
        UsernameLabel.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        UsernameLabel.Location = New Point(12, 430)
        UsernameLabel.Name = "UsernameLabel"
        UsernameLabel.Size = New Size(170, 25)
        UsernameLabel.TabIndex = 5
        UsernameLabel.Text = "@glebmalish_2000"
        ' 
        ' DisplaynameLabel
        ' 
        DisplaynameLabel.AutoSize = True
        DisplaynameLabel.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        DisplaynameLabel.Location = New Point(12, 405)
        DisplaynameLabel.Name = "DisplaynameLabel"
        DisplaynameLabel.Size = New Size(101, 25)
        DisplaynameLabel.TabIndex = 6
        DisplaynameLabel.Text = "Penguin28"
        ' 
        ' GiveGearButton
        ' 
        GiveGearButton.BackColor = SystemColors.ActiveCaptionText
        GiveGearButton.Location = New Point(67, 60)
        GiveGearButton.Name = "GiveGearButton"
        GiveGearButton.Size = New Size(88, 23)
        GiveGearButton.TabIndex = 7
        GiveGearButton.Text = "Give Gear"
        GiveGearButton.UseVisualStyleBackColor = False
        ' 
        ' GiveGearBox
        ' 
        GiveGearBox.Controls.Add(GearIdTextBox)
        GiveGearBox.Controls.Add(GiveGearButton)
        GiveGearBox.ForeColor = SystemColors.Highlight
        GiveGearBox.Location = New Point(138, 12)
        GiveGearBox.Name = "GiveGearBox"
        GiveGearBox.Size = New Size(229, 100)
        GiveGearBox.TabIndex = 8
        GiveGearBox.TabStop = False
        GiveGearBox.Text = "GiveGear"
        ' 
        ' GearIdTextBox
        ' 
        GearIdTextBox.BackColor = SystemColors.InfoText
        GearIdTextBox.ForeColor = SystemColors.MenuHighlight
        GearIdTextBox.Location = New Point(6, 22)
        GearIdTextBox.Name = "GearIdTextBox"
        GearIdTextBox.Size = New Size(217, 23)
        GearIdTextBox.TabIndex = 8
        GearIdTextBox.Text = "212641536"
        ' 
        ' LoadStringBox
        ' 
        LoadStringBox.Controls.Add(ExecuteCodeButton)
        LoadStringBox.Controls.Add(CodeRichTextBox)
        LoadStringBox.ForeColor = SystemColors.Highlight
        LoadStringBox.Location = New Point(138, 118)
        LoadStringBox.Name = "LoadStringBox"
        LoadStringBox.Size = New Size(464, 284)
        LoadStringBox.TabIndex = 9
        LoadStringBox.TabStop = False
        LoadStringBox.Text = "LoadString"
        ' 
        ' ExecuteCodeButton
        ' 
        ExecuteCodeButton.BackColor = SystemColors.ActiveCaptionText
        ExecuteCodeButton.Location = New Point(169, 242)
        ExecuteCodeButton.Name = "ExecuteCodeButton"
        ExecuteCodeButton.Size = New Size(124, 35)
        ExecuteCodeButton.TabIndex = 1
        ExecuteCodeButton.Text = "Execute"
        ExecuteCodeButton.UseVisualStyleBackColor = False
        ' 
        ' CodeRichTextBox
        ' 
        CodeRichTextBox.BackColor = SystemColors.InfoText
        CodeRichTextBox.ForeColor = SystemColors.Highlight
        CodeRichTextBox.Location = New Point(6, 22)
        CodeRichTextBox.Name = "CodeRichTextBox"
        CodeRichTextBox.Size = New Size(452, 214)
        CodeRichTextBox.TabIndex = 0
        CodeRichTextBox.Text = "print(""Hello World!"")"
        ' 
        ' AccountAgeLabel
        ' 
        AccountAgeLabel.AutoSize = True
        AccountAgeLabel.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(204))
        AccountAgeLabel.Location = New Point(12, 520)
        AccountAgeLabel.Name = "AccountAgeLabel"
        AccountAgeLabel.Size = New Size(228, 25)
        AccountAgeLabel.TabIndex = 10
        AccountAgeLabel.Text = "Account Age : 12343 Days"
        ' 
        ' ImageGroupBox
        ' 
        ImageGroupBox.Controls.Add(ImageJsonUrlTextBox)
        ImageGroupBox.Controls.Add(DeleteImageButton)
        ImageGroupBox.Controls.Add(SendImageButton)
        ImageGroupBox.ForeColor = SystemColors.Highlight
        ImageGroupBox.Location = New Point(373, 12)
        ImageGroupBox.Name = "ImageGroupBox"
        ImageGroupBox.Size = New Size(229, 100)
        ImageGroupBox.TabIndex = 9
        ImageGroupBox.TabStop = False
        ImageGroupBox.Text = "SendImage"
        ' 
        ' ImageJsonUrlTextBox
        ' 
        ImageJsonUrlTextBox.BackColor = SystemColors.InfoText
        ImageJsonUrlTextBox.ForeColor = SystemColors.MenuHighlight
        ImageJsonUrlTextBox.Location = New Point(6, 22)
        ImageJsonUrlTextBox.Name = "ImageJsonUrlTextBox"
        ImageJsonUrlTextBox.Size = New Size(217, 23)
        ImageJsonUrlTextBox.TabIndex = 8
        ImageJsonUrlTextBox.Text = "https://raw.githubusercontent.com/gleb2803/RobloxJson/refs/heads/main/images/banka.json"
        ' 
        ' DeleteImageButton
        ' 
        DeleteImageButton.BackColor = SystemColors.ActiveCaptionText
        DeleteImageButton.Location = New Point(121, 60)
        DeleteImageButton.Name = "DeleteImageButton"
        DeleteImageButton.Size = New Size(88, 23)
        DeleteImageButton.TabIndex = 7
        DeleteImageButton.Text = "Delete Image"
        DeleteImageButton.UseVisualStyleBackColor = False
        ' 
        ' SendImageButton
        ' 
        SendImageButton.BackColor = SystemColors.ActiveCaptionText
        SendImageButton.Location = New Point(17, 60)
        SendImageButton.Name = "SendImageButton"
        SendImageButton.Size = New Size(88, 23)
        SendImageButton.TabIndex = 7
        SendImageButton.Text = "Sent Image"
        SendImageButton.UseVisualStyleBackColor = False
        ' 
        ' OtherToolsBox
        ' 
        OtherToolsBox.Controls.Add(FunButton)
        OtherToolsBox.Controls.Add(InfoButton)
        OtherToolsBox.Controls.Add(ExplorerButton)
        OtherToolsBox.ForeColor = SystemColors.Highlight
        OtherToolsBox.Location = New Point(246, 408)
        OtherToolsBox.Name = "OtherToolsBox"
        OtherToolsBox.Size = New Size(356, 157)
        OtherToolsBox.TabIndex = 10
        OtherToolsBox.TabStop = False
        OtherToolsBox.Text = "OtherTools"
        ' 
        ' FunButton
        ' 
        FunButton.BackColor = SystemColors.ActiveCaptionText
        FunButton.Font = New Font("Segoe UI", 14.25F)
        FunButton.Location = New Point(6, 112)
        FunButton.Name = "FunButton"
        FunButton.Size = New Size(344, 39)
        FunButton.TabIndex = 2
        FunButton.Text = "Fun"
        FunButton.UseVisualStyleBackColor = False
        ' 
        ' InfoButton
        ' 
        InfoButton.BackColor = SystemColors.ActiveCaptionText
        InfoButton.Font = New Font("Segoe UI", 14.25F)
        InfoButton.Location = New Point(6, 67)
        InfoButton.Name = "InfoButton"
        InfoButton.Size = New Size(344, 39)
        InfoButton.TabIndex = 1
        InfoButton.Text = "Server Info"
        InfoButton.UseVisualStyleBackColor = False
        ' 
        ' ExplorerButton
        ' 
        ExplorerButton.BackColor = SystemColors.ActiveCaptionText
        ExplorerButton.Font = New Font("Segoe UI", 14.25F)
        ExplorerButton.Location = New Point(6, 22)
        ExplorerButton.Name = "ExplorerButton"
        ExplorerButton.Size = New Size(344, 39)
        ExplorerButton.TabIndex = 0
        ExplorerButton.Text = "Explorer"
        ExplorerButton.UseVisualStyleBackColor = False
        ' 
        ' ChatBox
        ' 
        ChatBox.Controls.Add(ChatListBox)
        ChatBox.Controls.Add(SendMessageButton)
        ChatBox.Controls.Add(MessageTextBox)
        ChatBox.ForeColor = SystemColors.Highlight
        ChatBox.Location = New Point(608, 12)
        ChatBox.Name = "ChatBox"
        ChatBox.Size = New Size(413, 553)
        ChatBox.TabIndex = 11
        ChatBox.TabStop = False
        ChatBox.Text = "Chat"
        ' 
        ' ChatListBox
        ' 
        ChatListBox.BackColor = SystemColors.InfoText
        ChatListBox.ForeColor = SystemColors.MenuHighlight
        ChatListBox.FormattingEnabled = True
        ChatListBox.ItemHeight = 15
        ChatListBox.Location = New Point(6, 22)
        ChatListBox.Name = "ChatListBox"
        ChatListBox.Size = New Size(401, 499)
        ChatListBox.TabIndex = 11
        ' 
        ' SendMessageButton
        ' 
        SendMessageButton.BackColor = SystemColors.ActiveCaptionText
        SendMessageButton.Location = New Point(316, 524)
        SendMessageButton.Name = "SendMessageButton"
        SendMessageButton.Size = New Size(91, 23)
        SendMessageButton.TabIndex = 10
        SendMessageButton.Text = "Send Message"
        SendMessageButton.UseVisualStyleBackColor = False
        ' 
        ' MessageTextBox
        ' 
        MessageTextBox.BackColor = SystemColors.InfoText
        MessageTextBox.ForeColor = SystemColors.MenuHighlight
        MessageTextBox.Location = New Point(6, 524)
        MessageTextBox.Name = "MessageTextBox"
        MessageTextBox.Size = New Size(304, 23)
        MessageTextBox.TabIndex = 9
        MessageTextBox.Text = "Hello"
        ' 
        ' PlaceControl
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ActiveCaptionText
        ClientSize = New Size(1033, 577)
        Controls.Add(ChatBox)
        Controls.Add(OtherToolsBox)
        Controls.Add(ImageGroupBox)
        Controls.Add(AccountAgeLabel)
        Controls.Add(LoadStringBox)
        Controls.Add(GiveGearBox)
        Controls.Add(DisplaynameLabel)
        Controls.Add(UsernameLabel)
        Controls.Add(FlagImage)
        Controls.Add(RefreshButton)
        Controls.Add(PlayersListBox)
        Controls.Add(BackButton)
        Controls.Add(ServesListBox)
        ForeColor = SystemColors.Highlight
        FormBorderStyle = FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "PlaceControl"
        Text = "PlaceControl"
        CType(FlagImage, ComponentModel.ISupportInitialize).EndInit()
        GiveGearBox.ResumeLayout(False)
        GiveGearBox.PerformLayout()
        LoadStringBox.ResumeLayout(False)
        ImageGroupBox.ResumeLayout(False)
        ImageGroupBox.PerformLayout()
        OtherToolsBox.ResumeLayout(False)
        ChatBox.ResumeLayout(False)
        ChatBox.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ServesListBox As ListBox
    Friend WithEvents BackButton As Button
    Friend WithEvents PlayersListBox As ListBox
    Friend WithEvents RefreshButton As Button
    Friend WithEvents FlagImage As PictureBox
    Friend WithEvents UsernameLabel As Label
    Friend WithEvents DisplaynameLabel As Label
    Friend WithEvents GiveGearButton As Button
    Friend WithEvents GiveGearBox As GroupBox
    Friend WithEvents GearIdTextBox As TextBox
    Friend WithEvents LoadStringBox As GroupBox
    Friend WithEvents CodeRichTextBox As RichTextBox
    Friend WithEvents ExecuteCodeButton As Button
    Friend WithEvents AccountAgeLabel As Label
    Friend WithEvents ImageGroupBox As GroupBox
    Friend WithEvents ImageJsonUrlTextBox As TextBox
    Friend WithEvents SendImageButton As Button
    Friend WithEvents DeleteImageButton As Button
    Friend WithEvents OtherToolsBox As GroupBox
    Friend WithEvents ChatBox As GroupBox
    Friend WithEvents SendMessageButton As Button
    Friend WithEvents MessageTextBox As TextBox
    Friend WithEvents ChatListBox As ListBox
    Friend WithEvents FunButton As Button
    Friend WithEvents InfoButton As Button
    Friend WithEvents ExplorerButton As Button
End Class
