Imports System.IO
Imports System.Net.Http
Imports System.Text.Json
Imports System.Threading

Public Class PlaceControl

    Public Property Ips As List(Of String) = New List(Of String)

    Public MainForm As Form1

    Public Sub RefreshList()
        If Ips.Count = 0 Then
            My.Forms.Explorer.Close()
            My.Forms.ServerInfo.Close()
            My.Forms.Fun.Close()
            MyBase.Hide()
            My.Forms.Form1.Show()
            Return
        End If
        Dim selected = ServesListBox.SelectedIndex

        ServesListBox.Items.Clear()

        For Each ip As String In Ips
            ServesListBox.Items.Add(ip)
        Next

        ServesListBox.Items.Add("All")

        If ServesListBox.Items.Count >= selected Then
            ServesListBox.SelectedIndex = selected
        End If
    End Sub

    Private Sub PlaceControl_VC(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Not MyBase.Visible Then
            Return
        End If

        MainForm.PendingUpdates.Add(New Update("GetData"))
        Logger.Log(Logger.LogType.AdminAction, $"Refresed Data Of {My.Forms.Form1.SelectedIp}")

        PlayersListBox.Items.Clear()
        ChatListBox.Items.Clear()

        If Ips Is Nothing Then
            Ips = New List(Of String)
        End If

        ServesListBox.Items.Clear()

        For Each ip As String In Ips
            ServesListBox.Items.Add(ip)
        Next

        ServesListBox.Items.Add("All")

        ServesListBox.SelectedIndex = 0
    End Sub

    Private Sub PlaceControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' ContextMenus

        Dim serverMenu As New ContextMenuStrip()

        Dim itemShutdown = serverMenu.Items.Add("Shutdown")
        Dim itemHint = serverMenu.Items.Add("Hint For All Players")
        Dim itemSetClockTime = serverMenu.Items.Add("Set Clock Time")
        serverMenu.Items.Add(New ToolStripSeparator())
        Dim itemDeleteServer = serverMenu.Items.Add("Delete Server")

        AddHandler itemShutdown.Click, Sub(s, ee)
                                           If ServesListBox.SelectedItem IsNot Nothing Then
                                               Dim update As Update = New Update("executeTheCommands")
                                               update.AddCommand($"shutdown;;")
                                               MainForm.PendingUpdates.Add(update)
                                               Logger.Log(Logger.LogType.AdminAction, $"Sent Shutdown To {My.Forms.Form1.SelectedIp}")
                                           End If
                                       End Sub

        AddHandler itemHint.Click, Sub(s, ee)
                                       If ServesListBox.SelectedItem IsNot Nothing Then
                                           Dim result = InputBox("Hint Message:", "Hint For All Players")

                                           Dim update As Update = New Update("executeTheCommands")
                                           update.AddCommand($"hint;{result};")
                                           MainForm.PendingUpdates.Add(update)
                                           Logger.Log(Logger.LogType.AdminAction, $"Sent Hint To {My.Forms.Form1.SelectedIp} Text : {result}")
                                       End If
                                   End Sub

        AddHandler itemSetClockTime.Click, Sub(s, ee)
                                               If ServesListBox.SelectedItem IsNot Nothing Then
                                                   Dim result = InputBox("Clock Time:", "Set Clock Time")

                                                   If result <> "" Then
                                                       Dim update As Update = New Update("executeTheCommands")
                                                       update.AddCommand($"setTime;{result};")
                                                       MainForm.PendingUpdates.Add(update)
                                                       Logger.Log(Logger.LogType.AdminAction, $"Set Time To {My.Forms.Form1.SelectedIp} Time : {result}")
                                                   End If
                                               End If
                                           End Sub

        AddHandler itemDeleteServer.Click, Sub(s, ee)
                                               Dim selectedIp = ServesListBox.SelectedItem?.ToString()
                                               If selectedIp Is Nothing Then Return

                                               Dim result = MessageBox.Show($"Удалить сервер {selectedIp}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                                               If result = DialogResult.Yes Then
                                                   Dim place = MainForm.places.FirstOrDefault(Function(p) p.IpList.Contains(selectedIp))
                                                   place?.RemoveIp(selectedIp)
                                                   ServesListBox.Items.Remove(selectedIp)

                                                   ' если серверов не осталось — удаляем place и возвращаемся
                                                   If place IsNot Nothing AndAlso place.IpList.Count = 0 Then
                                                       MainForm.places.Remove(place)
                                                       MainForm.Update_Btns()
                                                       MainForm.Show()
                                                       Me.Hide()
                                                   Else
                                                       MainForm.Update_Btns()
                                                   End If
                                               End If
                                           End Sub

        ServesListBox.ContextMenuStrip = serverMenu

        Dim plrMenu As New ContextMenuStrip()

        Dim itemProfile = plrMenu.Items.Add("Open Profile")
        plrMenu.Items.Add(New ToolStripSeparator())
        Dim itemKill = plrMenu.Items.Add("Kill")
        Dim itemKick = plrMenu.Items.Add("Kick")
        Dim itemLag = plrMenu.Items.Add("Lag")
        plrMenu.Items.Add(New ToolStripSeparator())

        AddHandler itemProfile.Click, Sub(s, ee)
                                          If PlayersListBox.SelectedItem IsNot Nothing Then
                                              Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem?.ToString())
                                              If plr IsNot Nothing Then
                                                  Process.Start(New ProcessStartInfo("https://www.roblox.com/users/" & plr.UserId & "/profile") With {
                                                        .UseShellExecute = True
                                                    })
                                                  Logger.Log(Logger.LogType.AdminAction, $"Opened Profile Of {plr.Name} ({plr.UserId})")
                                              End If
                                          End If
                                      End Sub

        AddHandler itemKill.Click, Sub(s, ee)
                                       If PlayersListBox.SelectedItem IsNot Nothing Then
                                           Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem?.ToString())
                                           If plr IsNot Nothing Then
                                               Dim update As Update = New Update("executeTheCommands")
                                               update.AddCommand($"kill;;{plr.Name}")
                                               MainForm.PendingUpdates.Add(update)
                                               Logger.Log(Logger.LogType.AdminAction, $"Killed {plr.Name}")
                                           End If
                                       End If
                                   End Sub

        AddHandler itemKick.Click, Sub(s, ee)
                                       If PlayersListBox.SelectedItem IsNot Nothing Then
                                           Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem?.ToString())
                                           If plr IsNot Nothing Then
                                               Dim update As Update = New Update("executeTheCommands")
                                               update.AddCommand($"kick;;{plr.Name}")
                                               MainForm.PendingUpdates.Add(update)
                                               Logger.Log(Logger.LogType.AdminAction, $"Kicked {plr.Name}")
                                           End If
                                       End If
                                   End Sub

        AddHandler itemLag.Click, Sub(s, ee)
                                      If PlayersListBox.SelectedItem IsNot Nothing Then
                                          Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem?.ToString())
                                          If plr IsNot Nothing Then
                                              Dim update As Update = New Update("executeTheCommands")
                                              update.AddCommand($"lag;;{plr.Name}")
                                              MainForm.PendingUpdates.Add(update)
                                              Logger.Log(Logger.LogType.AdminAction, $"Laged {plr.Name}")
                                          End If
                                      End If
                                  End Sub

        PlayersListBox.ContextMenuStrip = plrMenu
    End Sub

    Public Function GetFlagImage(countryCode As String) As Image
        Using client As New System.Net.WebClient()
            Dim url = $"https://flagcdn.com/64x48/{countryCode.ToLower()}.png"
            Dim bytes = client.DownloadData(url)
            Using ms As New IO.MemoryStream(bytes)
                Return Image.FromStream(ms)
            End Using
        End Using
    End Function


    Public Sub NewDataGet()
        Dim selected = PlayersListBox.SelectedIndex
        PlayersListBox.Items.Clear()
        For Each plr In MainForm.receivedData.Players
            PlayersListBox.Items.Add(plr.Name)
        Next
        If PlayersListBox.Items.Count - 1 >= selected Then
            PlayersListBox.SelectedIndex = selected
        End If
    End Sub

    Private Sub selectChanged_Connect(sender As Object, e As EventArgs) Handles ServesListBox.SelectedIndexChanged
        If ServesListBox.SelectedItem Is Nothing Then
            MessageBox.Show("Выберите IP")
            Return
        End If

        If ServesListBox.SelectedItem.ToString = MainForm.SelectedIp Then
            Return
        End If

        If ServesListBox.SelectedItem.ToString <> "All" Then
            MainForm.receivedData = New ServerData
            MainForm.PendingUpdates.Add(New Update("GetData"))
            Logger.Log(Logger.LogType.AdminAction, $"Get Data Of {My.Forms.Form1.SelectedIp}")
        End If

        PlayersListBox.Items.Clear()
        MainForm.SelectedIp = ServesListBox.SelectedItem.ToString
        ChatListBox.Items.Clear()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        MyBase.Hide()
        My.Forms.Form1.Show()
    End Sub

    Private Sub RefreshButton_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click
        MainForm.PendingUpdates.Add(New Update("GetData"))
        Logger.Log(Logger.LogType.AdminAction, $"Refresed Data Of {My.Forms.Form1.SelectedIp}")
    End Sub

    Private Sub Closed_Form(sender As Object, e As EventArgs) Handles MyBase.Closed
        My.Forms.Form1.Close()
    End Sub

    Private Sub PlayersListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PlayersListBox.SelectedIndexChanged
        If PlayersListBox.SelectedItem IsNot Nothing Then
            Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem)

            If plr IsNot Nothing Then
                FlagImage.Image = GetFlagImage(plr.countryCode)
                UsernameLabel.Text = "@" & plr.Name
                DisplaynameLabel.Text = plr.DisplayName
                AccountAgeLabel.Text = $"Account Age : {plr.AccountAge} Days"
            End If
        End If
    End Sub

    Private Sub GiveGearButton_Click(sender As Object, e As EventArgs) Handles GiveGearButton.Click
        If PlayersListBox.SelectedItem IsNot Nothing Then
            Dim gearId = GearIdTextBox.Text
            Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem)

            If plr IsNot Nothing Then
                Dim update = New Update("executeTheCommands")
                update.AddCommand($"giveGear;{gearId};{plr.Name}")
                MainForm.PendingUpdates.Add(update)
                Logger.Log(Logger.LogType.AdminAction, $"Gived Gear ({gearId}) To {plr.Name}")
            End If
        End If
    End Sub

    Private Async Sub ExecuteCodeButton_Click(sender As Object, e As EventArgs) Handles ExecuteCodeButton.Click
        If MainForm.receivedData.LoadStringEnabled = True Then
            Dim update = New Update("loadstringCode")
            update.AddCommand(CodeRichTextBox.Text)
            MainForm.PendingUpdates.Add(update)
            Logger.Log(Logger.LogType.AdminAction, $"Executed Script At {My.Forms.Form1.SelectedIp}: {CodeRichTextBox.Text}")
        Else
            ExecuteCodeButton.Text = "Loadstring Disabled"
            Await Task.Delay(1000)
            ExecuteCodeButton.Text = "Execute"
        End If
    End Sub

    Private Sub SendImageButton_Click(sender As Object, e As EventArgs) Handles SendImageButton.Click
        If PlayersListBox.SelectedItem IsNot Nothing Then
            Dim ImageURL = ImageJsonUrlTextBox.Text
            Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem)

            If plr IsNot Nothing Then
                Dim update = New Update("executeTheCommands")
                update.AddCommand($"sendImage;{ImageURL};{plr.Name}")
                MainForm.PendingUpdates.Add(update)
                Logger.Log(Logger.LogType.AdminAction, $"Sended Image ({ImageURL}) To {plr.Name}")
            End If
        End If
    End Sub

    Private Sub DeleteImageButton_Click(sender As Object, e As EventArgs) Handles DeleteImageButton.Click
        If PlayersListBox.SelectedItem IsNot Nothing Then
            Dim plr = MainForm.receivedData.Players.FirstOrDefault(Function(p) p.Name = PlayersListBox.SelectedItem)
            Dim update = New Update("executeTheCommands")
            update.AddCommand($"clearImage;;{plr.Name}")
            MainForm.PendingUpdates.Add(update)
            Logger.Log(Logger.LogType.AdminAction, $"Deleted Image From {plr.Name}")
        End If
    End Sub

    Private Sub SendMessageButton_Click(sender As Object, e As EventArgs) Handles SendMessageButton.Click
        Dim message = MessageTextBox.Text

        If message <> "" Then
            Dim update = New Update("executeTheCommands")
            update.AddCommand($"sendChatMessage;{message};")
            MainForm.PendingUpdates.Add(update)
            ChatListBox.Items.Add($"[{DateTime.Now.ToString("HH:mm:ss")}] SERVER: {message}")
            Logger.Log(Logger.LogType.AdminAction, $"Sended Message To {My.Forms.Form1.SelectedIp} Text : {message}")
        End If
    End Sub

    Public Sub AddChatMessage(msg As ChatMessage)
        ChatListBox.Items.Add($"[{msg.Time}] {msg.Player}: {msg.Message}")
        ChatListBox.TopIndex = ChatListBox.Items.Count - 1  ' прокрутка вниз
    End Sub

    Private Sub ExplorerButton_Click(sender As Object, e As EventArgs) Handles ExplorerButton.Click
        MainForm.PendingUpdates.Add(New Update("ExplorerUpdate"))
        My.Forms.Explorer.MainForm = MainForm
        My.Forms.Explorer.Show()
    End Sub

    Private Sub InfoButton_Click(sender As Object, e As EventArgs) Handles InfoButton.Click
        My.Forms.ServerInfo.MainForm = MainForm
        My.Forms.ServerInfo.Show()
    End Sub

    Private Sub FunButton_Click(sender As Object, e As EventArgs) Handles FunButton.Click
        My.Forms.Fun.MainForm = MainForm
        My.Forms.Fun.Show()
    End Sub
End Class