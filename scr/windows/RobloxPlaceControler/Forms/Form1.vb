Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Security.Principal
Imports System.Text
Imports System.Text.Json

Public Class Form1

    Public places As New List(Of Place) ' From {
    '    New Place("123", "Wi"),
    '  New Place("123", "Penguin Test")
    '}
    Dim buttons As New List(Of Button) From {}
    Public Port As Integer
    Public Password As Integer
    Dim localAddr As IPAddress = IPAddress.Any
    Dim listener As HttpListener
    Public PendingUpdates As New List(Of Update)
    Public SelectedIp As String
    Public receivedData As ServerData = New ServerData
    Public ChatMessages As New List(Of ChatMessage)
    Dim trayIcon As New NotifyIcon()


    Public Function IsAdmin() As Boolean
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Public Sub Update_Btns()
        For Each btn As Button In buttons
            Me.Controls.Remove(btn)
        Next
        buttons.Clear()
        For Each place As Place In places
            Dim btn As New Button()
            buttons.Add(btn)
            btn.Name = place.Id
            btn.Text = place.Name & $" ({place.IpList.Count} Серверов)"
            btn.Location = New Point(12, buttons.Count * 50 + 40)
            btn.Size = New Size(776, 46)
            btn.Font = New Font("Segoe UI", 16)
            AddHandler btn.Click, AddressOf DynamicButtons_Click
            Me.Controls.Add(btn)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not IsAdmin() Then
            Dim info As New ProcessStartInfo()
            info.FileName = Application.ExecutablePath
            info.Verb = "runas"
            info.UseShellExecute = True
            Try
                Process.Start(info)
                Application.Exit()
            Catch
                Application.Exit()
            End Try
        End If

        If Not Port Or Not Password Then
            My.Forms.InitSettings.Show()
            Me.Enabled = False
        End If

        Update_Btns()
    End Sub


    Public Sub StartServer()
        listener = New HttpListener()
        listener.Prefixes.Add("http://*:" & Port & "/")
        listener.Start()

        Task.Run(Async Sub()
                     While listener.IsListening
                         Try
                             Dim context As HttpListenerContext = Await listener.GetContextAsync()
                             Task.Run(Sub() HandleRequest(context))
                         Catch ex As Exception
                             ' сервер остановлен
                         End Try
                     End While
                 End Sub)
    End Sub

    Private Sub HandleRequest(context As HttpListenerContext)
        Dim request As HttpListenerRequest = context.Request
        Dim response As HttpListenerResponse = context.Response
        Try
            If request.Url.AbsolutePath = "/poll" AndAlso request.HttpMethod = "GET" Then
                response.StatusCode = 200
                response.ContentType = "application/json"
                response.Headers.Add("Access-Control-Allow-Origin", "*")

                Dim snapshot As List(Of Update)

                SyncLock PendingUpdates
                    snapshot = New List(Of Update)(PendingUpdates)
                    PendingUpdates.Clear()
                End SyncLock

                Dim updatesData = New With {
                .updates = snapshot,
                .targetIp = SelectedIp
            }

                Dim json = JsonSerializer.Serialize(updatesData)
                Dim bytes = Encoding.UTF8.GetBytes(json)
                response.ContentLength64 = bytes.Length
                response.OutputStream.Write(bytes, 0, bytes.Length)

            ElseIf request.Url.AbsolutePath = "/send" AndAlso request.HttpMethod = "POST" Then
                Dim body As String = ""
                Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                    body = reader.ReadToEnd()
                End Using

                Dim options As New JsonSerializerOptions With {
                    .PropertyNameCaseInsensitive = True
                }

                Dim serverData = JsonSerializer.Deserialize(Of ServerData)(body, options)

                Me.Invoke(Sub()
                              receivedData = serverData
                              My.Forms.PlaceControl.MainForm = Me
                              My.Forms.PlaceControl.NewDataGet()
                          End Sub)


                SendResponse(response, 200, "ok")
            ElseIf request.Url.AbsolutePath = "/explorer" AndAlso request.HttpMethod = "POST" Then
                Dim body As String = ""
                Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                    body = reader.ReadToEnd()
                End Using

                Dim options As New JsonSerializerOptions With {
                    .PropertyNameCaseInsensitive = True
                }

                Dim explorerData = JsonSerializer.Deserialize(Of ExplorerData)(body, options)

                Me.Invoke(Sub()
                              My.Forms.Explorer.MainForm = Me
                              My.Forms.Explorer.LoadTree(explorerData.Tree)
                          End Sub)

                SendResponse(response, 200, "ok")
            ElseIf request.Url.AbsolutePath = "/properties" AndAlso request.HttpMethod = "POST" Then
                Dim body As String = ""
                Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                    body = reader.ReadToEnd()
                End Using

                Dim options As New JsonSerializerOptions With {
                    .PropertyNameCaseInsensitive = True
                }

                Dim propsData = JsonSerializer.Deserialize(Of PropertiesData)(body, options)

                Me.Invoke(Sub()
                              My.Forms.Explorer.LoadProperties(propsData.Properties)
                          End Sub)

                SendResponse(response, 200, "ok")
            ElseIf request.Url.AbsolutePath = "/chat" AndAlso request.HttpMethod = "POST" Then
                Dim body As String = ""
                Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                    body = reader.ReadToEnd()
                End Using

                Dim options As New JsonSerializerOptions With {
                    .PropertyNameCaseInsensitive = True
                }

                Dim msg = JsonSerializer.Deserialize(Of ChatMessage)(body, options)
                msg.Time = DateTime.Now.ToString("HH:mm:ss")

                Me.Invoke(Sub()
                              ChatMessages.Add(msg)
                              My.Forms.PlaceControl.AddChatMessage(msg)
                              Logger.Log(Logger.LogType.MessageRecived, $"[Chat] {msg.Player}: {msg.Message}")
                          End Sub)

                SendResponse(response, 200, "ok")
            ElseIf request.Url.AbsolutePath = "/" AndAlso request.HttpMethod = "POST" Then
                Dim body As String = ""
                Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                    body = reader.ReadToEnd()
                End Using


                Dim json As JsonDocument = JsonDocument.Parse(body)
                Dim root As JsonElement = json.RootElement

                Dim statusEl, idEl, nameEl, ipEl, passEl As JsonElement
                If Not root.TryGetProperty("Status", statusEl) OrElse
                Not root.TryGetProperty("PlaceId", idEl) OrElse
                Not root.TryGetProperty("PlaceName", nameEl) OrElse
                Not root.TryGetProperty("Ip", ipEl) OrElse
                Not root.TryGetProperty("Pass", passEl) Then
                    SendResponse(response, 400, "отсутствуют поля")
                    Return
                End If

                Dim status As String = statusEl.GetString()
                Dim id As String = idEl.GetString()
                Dim name As String = nameEl.GetString()
                Dim ip As String = ipEl.GetString()
                Dim pass As String = passEl.GetInt32()

                If pass <> Password Then
                    Logger.Log(Logger.LogType.ServerTryingToConnect, $"Password Is Incorect: Ip({ip}) password({pass})")
                    SendResponse(response, 403, "Forbidden")
                    Return
                End If
                If status = "Created" OrElse status = "Working" Then
                    Me.Invoke(Sub()
                                  Dim existing = places.FirstOrDefault(Function(p) p.Id = id)
                                  If existing Is Nothing Then
                                      Dim newPlace As New Place(id, name)
                                      newPlace.AddIp(ip)
                                      places.Add(newPlace)
                                      Logger.Log(Logger.LogType.ServerConnect, $"Server Connected: {ip} | Place: {name} ({id})")
                                  Else
                                      ' place уже есть — просто добавляем IP
                                      existing.AddIp(ip)
                                  End If
                                  Update_Btns()
                              End Sub)

                ElseIf status = "Closing" Then
                    Me.Invoke(Sub()
                                  Dim existing = places.FirstOrDefault(Function(p) p.Id = id)
                                  If existing IsNot Nothing Then
                                      existing.RemoveIp(ip)
                                      Logger.Log(Logger.LogType.ServerDisconnect, $"Server Disconnected: {ip} | Place: {name} ({id})")
                                      ' удаляем place только если не осталось IP
                                      If existing.IpList.Count = 0 Then
                                          places.Remove(existing)
                                      End If
                                  End If
                                  Update_Btns()
                              End Sub)
                End If

                Me.Invoke(Sub()
                              My.Forms.PlaceControl.RefreshList()
                          End Sub)

                SendResponse(response, 200, "ok")
            Else
                SendResponse(response, 405, "метод не поддерживается")
            End If
        Catch ex As Exception
            Logger.Log(Logger.LogType.ServerError, ex.Message)
        Finally
            If request.Url.AbsolutePath <> "/stream" Then
                response.OutputStream.Close()
            End If
        End Try
    End Sub
    Private Sub SendResponse(response As HttpListenerResponse, code As Integer, message As String)
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(message)
        response.StatusCode = code
        response.ContentLength64 = bytes.Length
        response.OutputStream.Write(bytes, 0, bytes.Length)
    End Sub

    Private Sub DynamicButtons_Click(sender As Object, e As EventArgs)
        Dim btn = DirectCast(sender, Button)
        Dim existing = places.FirstOrDefault(Function(p) p.Id = btn.Name)
        If existing IsNot Nothing Then
            If existing.IpList.Count > 0 Then
                My.Forms.PlaceControl.Ips = existing.IpList
                My.Forms.PlaceControl.MainForm = Me
                My.Forms.PlaceControl.Show()
                MyBase.Hide()
            End If
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If listener IsNot Nothing Then
            listener.Stop()
        End If
    End Sub

    Private Sub CreditsLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles CreditsLinkLabel.LinkClicked
        My.Forms.Сredits.Show()
    End Sub
End Class
