Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports BennysOriginalMotorworks.BennysMenu
Imports System.Windows.Forms
Imports System.Drawing

Public Class Bennys
    Inherits Script

    Public Shared veh, tra As Vehicle
    Public Shared ply As Ped
    Public Shared onlineMap As Integer = 1
    Public Shared fixDoor As Integer = 1
    Public Shared bennyIntID As Integer
    Public Shared isExiting As Boolean = False
    Public Shared lastVehMemory As Memory
    Public Shared BennysBlip As Blip
    Public Shared bennyPed As Ped
    Public Shared isCutscene As Boolean = False
    Public Shared scriptCam As Camera 'ScriptedCamera
    Public Shared unWelcome As List(Of VehicleClass) = New List(Of VehicleClass) From {VehicleClass.Boats, VehicleClass.Cycles, VehicleClass.Helicopters, VehicleClass.Planes}
    Public Shared fpcKey, zoutKey, zinKey As GTA.Control
    'Public Shared unWelcomeV As List(Of Model) = New List(Of Model) From {"firetruck", "pbus", "policeb", "riot", "dump", "cutter", "bulldozer", "flatbed", "handler", "mixer", "mixer2", "rubble", "tiptruck", "tiptruck2",
    '    "ruiner3", "dune2", "marshall", "monster", "brickade", "airbus", "bus", "coach", "rallytruck", "rentalbus", "tourbus", "trash", "trash2", "wastelander", "airtug", "caddy", "caddy2", "docktug", "ripley", "mower",
    '    "forklift", "scrap", "towtruck", "towtruck2", "tractor", "tractor2", "tractor3", "utillitruck", "utilitytruck2", "utillitruck3", "camper", "journey", "taco", "rhino", "barracks3", "barracks2", "barracks",
    '    "chernobog", "riot2", "khanjari", "Thruster"}
    Public Shared lastCameraPos As CameraPosition

    Public Sub New()
        LoadSettings()
        bennyIntID = Helper.GetInteriorID(New Vector3(-211.798, -1324.292, 30.37535))
        CreateBlip()
        Game.Globals(GetGlobalValue).SetInt(1)
    End Sub

    Public Sub LoadSettings()
        Dim config As ScriptSettings = ScriptSettings.Load("scripts\BennysOriginalMotorWorks.ini")
        onlineMap = config.GetValue(Of Integer)("SETTINGS", "OnlineMap", 1)
        fixDoor = config.GetValue(Of Integer)("SETTINGS", "FixDoor", 1)
        fpcKey = config.GetValue(Of GTA.Control)("CONTROLS", "FirstPerson", GTA.Control.NextCamera)
        zoutKey = config.GetValue(Of GTA.Control)("CONTROLS", "ZoomOut", GTA.Control.VehicleSubDescend)
        zinKey = config.GetValue(Of GTA.Control)("CONTROLS", "ZoomIn", GTA.Control.VehicleSubAscend)
        If onlineMap = 1 Then LoadMPDLCMap()
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            veh = Game.Player.Character.LastVehicle
            ply = Game.Player.Character
            If veh.IsVehicleAttachedToTrailer Then tra = veh.GetVehicleTrailerVehicle

            If fixDoor = 1 Then
                If Not unWelcome.Contains(veh.ClassType) Then 'AndAlso Not unWelcomeV.Contains(veh.Model) Then
                    If veh.Position.DistanceTo(New Vector3(-205.6828, -1310.683, 30.29572)) <= 15 Then
                        Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 0, 0.0, 50.0, 0)
                    Else
                        Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 1, 0.0, 50.0, 0)
                    End If
                End If
            End If

            If GetInteriorID(ply.Position) = bennyIntID Then
                If Not IsArenaWarDLCInstalled() Then
                    Helper.DisplayHelpTextThisFrame("Un-supported GTA V version detected! SPB may not work properly on this version.")
                End If
            End If

            If Helper.GetInteriorID(ply.Position) = bennyIntID AndAlso Not unWelcome.Contains(veh.ClassType) Then 'AndAlso Not unWelcomeV.Contains(veh.Model)) Then
                If Not isExiting Then
                    If veh.Position.DistanceTo(New Vector3(-205.6165, -1312.976, 31.1331)) <= 5 Then
                        PlayerVehicleHalt()
                        UpdateTitleName()
                        PlayEnterCutScene()
                        PutVehIntoShop()
                    Else
                        If veh.Position.DistanceTo(New Vector3(-211.798, -1324.292, 30.37535)) <= 5 Then
                            BennysMenu.camera.Update()
                            Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 3)
                            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 4)
                            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 5)
                            Native.Function.Call(Hash.SHOW_HUD_COMPONENT_THIS_FRAME, 13)
                        End If
                    End If
                End If
                If isExiting Then
                    Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                    SuspendKeys()
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try

        'If Game.IsControlPressed(0, GTA.Control.Jump) AndAlso Game.IsControlPressed(0, GTA.Control.Reload) Then
        '    '    Dim s As String = Game.GetUserInput(System.Windows.Forms.Clipboard.GetText(), 99)
        '    '    UI.Notify(Game.GetGXTEntry(s))

        '    '    ''For Each line As String In IO.File.ReadLines("C:\New.txt")
        '    '    ''    Logger.Log(Game.GetGXTEntry(line) & ", " & line)
        '    '    ''Next

        '    '    ''For i As Integer = 0 To 500
        '    '    ''    Logger.Write("CMOD_MOD_" & i & "_D = " & Game.GetGXTEntry("CMOD_MOD_" & i & "_D"))
        '    '    ''Next
        '    '    ''For i As Integer = 0 To 500
        '    '    ''    Logger.Write("CMOD_SMOD_" & i & "_D = " & Game.GetGXTEntry("CMOD_SMOD_" & i & "_D"))
        '    '    ''Next
        '    Dim engine As Single = Helper.GetVehicleEnginePositionSingle(Game.Player.LastVehicle)
        '    Dim hood As Single = Helper.GetVehicleHoodPositionSingle(Game.Player.LastVehicle)
        '    Dim trunk As Single = Helper.GetVehicleTrunkPositionSingle(Game.Player.LastVehicle)
        '    Dim brand As String = GetVehicleMakeName(Game.Player.LastVehicle.Model.Hash)
        '    UI.ShowSubtitle(String.Format("Engine: {0}, Hood: {1}, Trunk: {2}, Brand: {3}", engine, hood, trunk, brand))
        'End If

        If _menuPool.IsAnyMenuOpen Then
            'If Game.IsControlJustReleased(0, GTA.Control.VehicleSubAscend) Then
            '    If BennysMenu.camera.MainCameraPosition = CameraPosition.Car Then
            '        If BennysMenu.camera.CameraZoom = (5.0 + BennysMenu.camera.Dimension) Then
            '            Do While BennysMenu.camera.CameraZoom > (3.5 + BennysMenu.camera.Dimension)
            '                Wait(1)
            '                BennysMenu.camera.CameraZoom -= 0.1
            '            Loop
            '        Else
            '            Do While BennysMenu.camera.CameraZoom < (5.0 + BennysMenu.camera.Dimension)
            '                Wait(1)
            '                BennysMenu.camera.CameraZoom += 0.1
            '            Loop
            '        End If
            '    End If
            'End If

            'UI.ShowSubtitle($"E: {veh.GetVehEnginePos} H: {veh.GetVehHoodPos} T: {veh.GetVehTrunkPos} | Has Hood: {Bennys.veh.HasBone("bonnet")} Has Trunk: {Bennys.veh.HasBone("boot")}") 'Bennys.veh.HasBone("bonnet") AndAlso Bennys.veh.HasBone("boot")

            If Game.IsControlPressed(0, zinKey) Then
                Dim max As New PointF(6.0F + BennysMenu.camera.Dimension, 3.0F + BennysMenu.camera.Dimension)
                If Not BennysMenu.camera.CameraZoom <= max.Y Then
                    BennysMenu.camera.CameraZoom -= 0.1
                Else
                    BennysMenu.camera.CameraZoom = max.Y
                End If
            End If
            If Game.IsControlPressed(0, zoutKey) Then
                Dim max As New PointF(6.0F + BennysMenu.camera.Dimension, 3.0F + BennysMenu.camera.Dimension)
                If Not BennysMenu.camera.CameraZoom >= max.X Then
                    BennysMenu.camera.CameraZoom += 0.1
                Else
                    BennysMenu.camera.CameraZoom = max.X
                End If
            End If

            If Game.IsControlJustReleased(0, fpcKey) Then
                lastCameraPos = BennysMenu.camera.MainCameraPosition
                If BennysMenu.camera.MainCameraPosition = CameraPosition.Interior Then
                    If lastCameraPos = CameraPosition.Interior Then
                        BennysMenu.camera.MainCameraPosition = CameraPosition.Car
                    Else
                        BennysMenu.camera.MainCameraPosition = lastCameraPos
                    End If
                Else
                    BennysMenu.camera.MainCameraPosition = CameraPosition.Interior
                End If
            End If
        End If
    End Sub

    Public Shared Sub CreateBlip()
        BennysBlip = World.CreateBlip(New Vector3(-205.5417, -1307.118, 30.26981))
        BennysBlip.Sprite = (BlipSprite.DollarSignSquared Or BlipSprite.ArrowDownOutlined)
        BennysBlip.Color = BlipColor.Yellow
        BennysBlip.IsShortRange = True
        BennysBlip.Name = Game.GetGXTEntry("S_MO_09")
    End Sub

    Public Shared Sub PlayEnterCutScene()
        Try
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
            Game.FadeScreenOut(1000)
            Wait(1000)
            isExiting = True
            isCutscene = True
            If bennyPed = Nothing Then
                bennyPed = World.CreatePed(PedHash.Benny, New Vector3(-216.0945, -1319.185, 30.89038), 219.5891)
            Else
                bennyPed.Delete()
                bennyPed = World.CreatePed(PedHash.Benny, New Vector3(-216.0945, -1319.185, 30.89038), 219.5891)
            End If
            bennyPed.Task.LookAt(veh)
            bennyPed.AlwaysKeepTask = True
            veh.Position = New Vector3(-205.4903, -1313.958, 31.02291)
            veh.Heading = 180.3224
            Wait(1000)
            Game.FadeScreenIn(1000)
            scriptCam = World.CreateCamera(New Vector3(-201.1808, -1321.512, 32.20821), Vector3.Zero, GameplayCamera.FieldOfView)
            Dim interpCam As Camera = World.CreateCamera(New Vector3(-200.7804, -1316.474, 32.08001), Vector3.Zero, GameplayCamera.FieldOfView)
            World.RenderingCamera = scriptCam
            scriptCam.InterpTo(interpCam, 5000, True, True)
            World.RenderingCamera = interpCam
            interpCam.Shake(CameraShake.Hand, 0.4F)
            interpCam.PointAt(veh)

            ply.Task.DriveTo(veh, New Vector3(-207.155, -1320.521, 30.8904), 0.1, 2.3)
            Wait(2000)
            Helper.PlaySpeech("SHOP_NICE_VEHICLE")
            ply.Task.DriveTo(veh, New Vector3(-211.798, -1324.292, 30.37535), 0.1, 2)
            Wait(4000)
            ply.Task.ClearAll()
            Game.FadeScreenOut(1000)
            Wait(1000)
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
            isExiting = False
            isCutscene = False
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub PlayExitCutScene()
        Try
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
            isExiting = True

            Game.FadeScreenOut(1000)
            Wait(1000)
            Game.Player.Character.Alpha = 255
            BennysMenu.camera.Stop()
            veh.Position = New Vector3(-205.8678, -1321.805, 30.41191)
            veh.Heading = 358.6677
            ply.Task.DriveTo(veh, New Vector3(-205.743, -1303.657, 30.84998), 0.5, 5)
            Wait(1000)
            Game.FadeScreenIn(1000)
            PlaySpeech("SHOP_GOODBYE")
            scriptCam = World.CreateCamera(New Vector3(-201.1865, -1299.761, 31.41244), Vector3.Zero, GameplayCamera.FieldOfView)
            Dim interpCam As Camera = World.CreateCamera(New Vector3(-197.5533, -1297.754, 32.29234), Vector3.Zero, GameplayCamera.FieldOfView)
            World.RenderingCamera = scriptCam
            scriptCam.PointAt(veh)
            scriptCam.InterpTo(interpCam, 5000, True, True)
            World.RenderingCamera = interpCam
            interpCam.Shake(CameraShake.Hand, 0.4F)
            interpCam.PointAt(veh)
            Wait(2800)
            ply.Task.DriveTo(veh, New Vector3(-200.2561, -1303.021, 30.66544), 0.1, 2)
            Wait(4000)
            ply.Task.ClearAll()
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
            ply.Task.ClearAll()
            If Not veh.Position.DistanceTo2D(New Vector3(-200.2561, -1303.021, 30.66544)) <= 4.0F Then
                Game.FadeScreenOut(1000)
                Wait(1000)
                veh.Position = New Vector3(-200.2561, -1303.021, 30.66544)
                veh.Heading = 312.8701
                Wait(1000)
                veh.Repair()
                Game.FadeScreenIn(1000)
            End If
            isExiting = False
            If Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then
                Native.Function.Call(Hash.STOP_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub PutVehIntoShop()
        Try
            veh.InstallModKit()
            RefreshMenus()
            lastVehMemory = New Memory() With {
                .Aerials = veh.GetMod(VehicleMod.Aerials),
                .Trim = veh.GetMod(VehicleMod.Trim),
                .FrontBumper = veh.GetMod(VehicleMod.FrontBumper),
                .RearBumper = veh.GetMod(VehicleMod.RearBumper),
                .SideSkirt = veh.GetMod(VehicleMod.SideSkirt),
                .ColumnShifterLevers = veh.GetMod(VehicleMod.ColumnShifterLevers),
                .Dashboard = veh.GetMod(VehicleMod.Dashboard),
                .DialDesign = veh.GetMod(VehicleMod.DialDesign),
                .Ornaments = veh.GetMod(VehicleMod.Ornaments),
                .Seats = veh.GetMod(VehicleMod.Seats),
                .SteeringWheels = veh.GetMod(VehicleMod.SteeringWheels),
                .TrimDesign = veh.GetMod(VehicleMod.TrimDesign),
                .LightsColor = veh.DashboardColor,
                .TrimColor = veh.TrimColor,
                .WheelType = veh.WheelType,
                .AirFilter = veh.GetMod(VehicleMod.AirFilter),
                .EngineBlock = veh.GetMod(VehicleMod.EngineBlock),
                .Struts = veh.GetMod(VehicleMod.Struts),
                .NumberPlate = veh.NumberPlateType,
                .PlateHolder = veh.GetMod(VehicleMod.PlateHolder),
                .VanityPlates = veh.GetMod(VehicleMod.VanityPlates),
                .Armor = veh.GetMod(VehicleMod.Armor),
                .Brakes = veh.GetMod(VehicleMod.Brakes),
                .Engine = veh.GetMod(VehicleMod.Engine),
                .Transmission = veh.GetMod(VehicleMod.Transmission),
                .BackNeon = veh.IsNeonLightsOn(VehicleNeonLight.Back),
                .FrontNeon = veh.IsNeonLightsOn(VehicleNeonLight.Front),
                .LeftNeon = veh.IsNeonLightsOn(VehicleNeonLight.Left),
                .RightNeon = veh.IsNeonLightsOn(VehicleNeonLight.Right),
                .BackWheels = veh.GetMod(VehicleMod.BackWheels),
                .FrontWheels = veh.GetMod(VehicleMod.FrontWheels),
                .Headlights = veh.IsToggleModOn(VehicleToggleMod.XenonHeadlights),
                .WheelsVariation = Helper.IsCustomWheels(),
                .ArchCover = veh.GetMod(VehicleMod.ArchCover),
                .Exhaust = veh.GetMod(VehicleMod.Exhaust),
                .Fender = veh.GetMod(VehicleMod.Fender),
                .RightFender = veh.GetMod(VehicleMod.RightFender),
                .DoorSpeakers = veh.GetMod(VehicleMod.DoorSpeakers),
                .Frame = veh.GetMod(VehicleMod.Frame),
                .Grille = veh.GetMod(VehicleMod.Grille),
                .Hood = veh.GetMod(VehicleMod.Hood),
                .Horns = veh.GetMod(VehicleMod.Horns),
                .Hydraulics = veh.GetMod(VehicleMod.Hydraulics),
                .Livery = veh.GetMod(VehicleMod.Livery),
                .Livery2 = veh.GetLivery2,
                .Plaques = veh.GetMod(VehicleMod.Plaques),
                .Roof = veh.GetMod(VehicleMod.Roof),
                .Speakers = veh.GetMod(VehicleMod.Speakers),
                .Spoilers = veh.GetMod(VehicleMod.Spoilers),
                .Tank = veh.GetMod(VehicleMod.Tank),
                .Trunk = veh.GetMod(VehicleMod.Trunk),
                .Turbo = veh.IsToggleModOn(VehicleToggleMod.Turbo),
                .Windows = veh.GetMod(VehicleMod.Windows),
                .Tint = veh.WindowTint,
                .PearlescentColor = veh.PearlescentColor,
                .PrimaryColor = veh.PrimaryColor,
                .RimColor = veh.RimColor,
                .SecondaryColor = veh.SecondaryColor,
                .TireSmokeColor = veh.TireSmokeColor,
                .NeonLightsColor = veh.NeonLightsColor,
                .PlateNumbers = veh.NumberPlate,
                .HeadlightsColor = veh.GetXenonHeadlightsColor,
                .Suspension = veh.GetMod(VehicleMod.Suspension)}
            veh.Position = New Vector3(-211.798, -1324.292, 30.37535)
            veh.Heading = 150.2801 '358.6677
            BennysMenu.MainMenu.Visible = Not BennysMenu.MainMenu.Visible
            BennysMenu.camera.RepositionFor(veh)
            Wait(1000)
            Game.FadeScreenIn(1000)
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    'Public Sub OnKeyDown(o As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If Game.Player.Character.IsInVehicle Then
    '        If e.KeyCode = Keys.Up Then
    '            If Game.Player.Character.LastVehicle.IsDoorOpen(VehicleDoor.Hood) Then
    '                Game.Player.Character.LastVehicle.CloseDoor(VehicleDoor.Hood, False)
    '            Else
    '                Game.Player.Character.LastVehicle.OpenDoor(VehicleDoor.Hood, False, False)
    '            End If
    '        End If

    '        If e.KeyCode = Keys.Down Then
    '            If Game.Player.Character.LastVehicle.IsDoorOpen(VehicleDoor.Trunk) Then
    '                Game.Player.Character.LastVehicle.CloseDoor(VehicleDoor.Trunk, False)
    '            Else
    '                Game.Player.Character.LastVehicle.OpenDoor(VehicleDoor.Trunk, False, False)
    '            End If
    '        End If
    '    End If
    'End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        BennysBlip.Remove()
        Game.FadeScreenIn(1000)
        If Not bennyPed = Nothing Then bennyPed.Delete()
    End Sub
End Class
