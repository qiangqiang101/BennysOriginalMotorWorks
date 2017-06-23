Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports BennysOriginalMotorworks.BennysMenu

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
    Public Shared scriptCam As ScriptedCamera
    Public Shared unWelcome As List(Of VehicleClass) = New List(Of VehicleClass) From {VehicleClass.Commercial, VehicleClass.Boats, VehicleClass.Cycles, VehicleClass.Helicopters, VehicleClass.Military, VehicleClass.Planes}
    Public Shared unWelcomeV As List(Of Model) = New List(Of Model) From {"firetruck", "pbus", "policeb", "riot", "dump", "cutter", "bulldozer", "flatbed", "handler", "mixer", "mixer2", "rubble", "tiptruck", "tiptruck2",
        "ruiner3", "dune2", "marshall", "monster", "brickade", "airbus", "bus", "coach", "rallytruck", "rentalbus", "tourbus", "trash", "trash2", "wastelander", "airtug", "caddy", "caddy2", "docktug", "ripley", "mower",
        "forklift", "scrap", "towtruck", "towtruck2", "tractor", "tractor2", "tractor3", "utillitruck", "utilitytruck2", "utillitruck3", "camper", "journey", "taco"}

    Public Sub New()
        LoadSettings()
        bennyIntID = Helper.GetInteriorID(New Vector3(-211.798, -1324.292, 30.37535))
        CreateBlip()
        If Not IO.File.Exists(".\scripts\BennysLang-" & Game.Language.ToString & ".ini") Then Helper.CreateTitleNames()
    End Sub

    Public Sub LoadSettings()
        Dim config As ScriptSettings = ScriptSettings.Load("scripts\BennysOriginalMotorWorks.ini")
        onlineMap = config.GetValue(Of Integer)("SETTINGS", "OnlineMap", 1)
        fixDoor = config.GetValue(Of Integer)("SETTINGS", "FixDoor", 1)

        If onlineMap = 1 Then
            Helper.LoadMPDLCMap()
        End If
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            veh = Game.Player.Character.LastVehicle
            ply = Game.Player.Character
            If Helper.IsVehicleAttachedToTrailer(veh) Then tra = Helper.GetVehicleTrailerVehicle(veh)

            If Game.Version < GameVersion.VER_1_0_877_1_STEAM Then
                Helper.DisplayHelpTextThisFrame("Un-supported GTA V version detected! SPB may not work properly on this version.")
            End If

            If Game.IsControlPressed(0, Control.Jump) AndAlso Game.IsControlPressed(0, Control.Reload) Then
                Dim s As String = Game.GetUserInput(System.Windows.Forms.Clipboard.GetText(), 99)
                UI.Notify(Game.GetGXTEntry(s))

                ''For Each line As String In IO.File.ReadLines("C:\New.txt")
                ''    Logger.Log(Game.GetGXTEntry(line) & ", " & line)
                ''Next

                ''For i As Integer = 0 To 500
                ''    Logger.Write("CMOD_MOD_" & i & "_D = " & Game.GetGXTEntry("CMOD_MOD_" & i & "_D"))
                ''Next
                ''For i As Integer = 0 To 500
                ''    Logger.Write("CMOD_SMOD_" & i & "_D = " & Game.GetGXTEntry("CMOD_SMOD_" & i & "_D"))
                ''Next
            End If

            If fixDoor = 1 Then
                If Not unWelcome.Contains(veh.ClassType) AndAlso Not unWelcomeV.Contains(veh.Model) Then
                    If veh.Position.DistanceTo(New Vector3(-205.6828, -1310.683, 30.29572)) <= 15 Then
                        Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 0, 0.0, 50.0, 0)
                    Else
                        Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 1, 0.0, 50.0, 0)
                    End If
                End If
            End If

            If Helper.GetInteriorID(ply.Position) = bennyIntID AndAlso (Not unWelcome.Contains(veh.ClassType) AndAlso Not unWelcomeV.Contains(veh.Model)) Then
                If Not isExiting Then
                    If veh.Position.DistanceTo(New Vector3(-205.6165, -1312.976, 31.1331)) <= 5 Then
                        Helper.SaveTitleNames()
                        PlayCutScene()
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
                If isExiting Then Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBlip()
        BennysBlip = World.CreateBlip(New Vector3(-205.5417, -1307.118, 30.26981))
        BennysBlip.Sprite = (BlipSprite.DollarSignSquared Or BlipSprite.ArrowDownOutlined)
        BennysBlip.Color = BlipColor.Yellow
        BennysBlip.IsShortRange = True
        BennysBlip.Name = Game.GetGXTEntry("S_MO_09")
    End Sub

    Public Shared Sub PlayCutScene()
        Try
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
            Game.FadeScreenOut(500)
            Wait(500)
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
            Wait(500)
            Game.FadeScreenIn(500)
            scriptCam = New ScriptedCamera()
            ScriptedCamera.EnableCamera()
            ScriptedCamera.prevPos = New Vector4(New Vector3(-201.1808, -1321.512, 32.20821))
            ScriptedCamera.TransitionToPoint(New Vector4(New Vector3(-200.7804, -1316.474, 32.08001)), 5000)
            ScriptedCamera.CameraShake(CameraShake.Hand, 0.4)
            ScriptedCamera.PointAt(veh)
            ply.Task.DriveTo(veh, New Vector3(-207.155, -1320.521, 30.8904), 0.1, 2.3)
            Wait(2000)
            Helper.PlaySpeech("SHOP_NICE_VEHICLE")
            ply.Task.DriveTo(veh, New Vector3(-211.798, -1324.292, 30.37535), 0.1, 2)
            Wait(4000)
            ply.Task.ClearAll()
            Game.FadeScreenOut(500)
            Wait(500)
            World.DestroyAllCameras()
            ScriptedCamera.DisableCamera()
            isExiting = False
            isCutscene = False
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
                .Livery2 = Helper.GetTornadoCustomRoof(veh),
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
                .Suspension = veh.GetMod(VehicleMod.Suspension)}
            veh.Position = New Vector3(-211.798, -1324.292, 30.37535)
            veh.Heading = 150.2801 '358.6677
            MainMenu.Visible = Not MainMenu.Visible
            BennysMenu.camera.RepositionFor(veh)
            Wait(500)
            Game.FadeScreenIn(500)
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        BennysBlip.Remove()
        Game.FadeScreenIn(500)
        If Not bennyPed = Nothing Then bennyPed.Delete()
    End Sub
End Class
