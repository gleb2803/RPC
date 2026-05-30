Public Class ServerInfo
    Public MainForm As Form1

    Private WithEvents UpdateTimer As New Timer

    Sub UpdateStats()
        MainForm.PendingUpdates.Add(New Update("GetData"))
        Dim Selected = StatsListBox.SelectedIndex
        StatsListBox.Items.Clear()
        StatsListBox.Items.Add($"InstanceCount : {MainForm.receivedData.InstanceCount}")
        StatsListBox.Items.Add($"MemoryUsage : {MainForm.receivedData.MemoryUsageMb}")
        StatsListBox.Items.Add($"ContactsCount : {MainForm.receivedData.ContactsCount}")
        StatsListBox.Items.Add($"MovingPrimitivesCount : {MainForm.receivedData.MovingPrimitivesCount}")
        StatsListBox.Items.Add($"DataSendKbps : {MainForm.receivedData.DataSendKbps}")
        StatsListBox.Items.Add($"DataReceiveKbps : {MainForm.receivedData.DataReceiveKbps}")
        StatsListBox.Items.Add($"HeartbeatTime : {MainForm.receivedData.HeartbeatTime}")
        StatsListBox.Items.Add($"ServerTime : {MainForm.receivedData.ServerTime}")
        StatsListBox.Items.Add($"PlaceVersion : {MainForm.receivedData.PlaceVersion}")
        StatsListBox.SelectedIndex = Selected
    End Sub

    Sub FormClosede() Handles MyBase.Closed
        UpdateTimer.Stop()
    End Sub

    Sub OnTick() Handles UpdateTimer.Tick
        UpdateStats()
    End Sub

    Private Sub ServerInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim infoMenu As New ContextMenuStrip()
        Dim itemUpdate = infoMenu.Items.Add("Update")
        AddHandler itemUpdate.Click, Sub()
                                         UpdateStats()
                                     End Sub
        StatsListBox.ContextMenuStrip = infoMenu

        UpdateTimer.Interval = 3000
        UpdateTimer.Start()
    End Sub
End Class