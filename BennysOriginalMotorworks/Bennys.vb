Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports BennysOriginalMotorworks.BennysMenu

Public Class Bennys
    Inherits Script

    Public Shared veh As Vehicle
    Public Shared ply As Ped
    Public Shared onlineMap As Integer = 1
    Public Shared fixDoor As Integer = 0
    Public Shared bennyIntID As Integer
    Public Shared isExiting As Boolean = False
    Public Shared lastVehMemory As Memory
    Public Shared BennysBlip As Blip

    Public Sub New()
        LoadSettings()
        bennyIntID = Helper.GetInteriorID(New Vector3(-211.798, -1324.292, 30.37535))
        CreateBlip()
    End Sub

    Public Sub LoadSettings()
        Dim config As ScriptSettings = ScriptSettings.Load("scripts\BennysOriginalMotorworks.ini")
        onlineMap = config.GetValue(Of Integer)("SETTINGS", "OnlineMap", 1)
        fixDoor = config.GetValue(Of Integer)("SETTINGS", "FixDoor", 0)

        If onlineMap = 1 Then
            Helper.LoadMPDLCMap()
        End If
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            veh = Game.Player.Character.LastVehicle
            ply = Game.Player.Character

            If Game.IsControlPressed(0, Control.Jump) AndAlso Game.IsControlPressed(0, Control.Reload) Then
                Dim s As String = Game.GetUserInput(99)
                UI.Notify(Game.GetGXTEntry(s))
            End If

            If fixDoor = 1 Then
                If ply.Position.DistanceTo(New Vector3(-205.6828, -1310.683, 30.29572)) <= 10 Then
                    Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 0, 0.0, 50.0, 0)
                Else
                    Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 1, 0.0, 50.0, 0)
                End If
            End If

            If Helper.GetInteriorID(ply.Position) = bennyIntID Then
                If Not isExiting Then
                    If ply.Position.DistanceTo(New Vector3(-205.8678, -1321.805, 30.41191)) <= 5 Then
                        PutVehIntoShop()
                    Else
                        If ply.Position.DistanceTo(New Vector3(-211.798, -1324.292, 30.37535)) <= 5 Then
                            BennysMenu.camera.Update()
                            Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                        End If
                    End If
                End If
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
    End Sub

    Public Shared Sub PutVehIntoShop()
        Try
            'mColumnShifterLevers, mDashboard, mDialDesign, mOrnaments, mSeats,                mSteeringWheels, mTrimDesign
            Game.FadeScreenOut(500)
            Wait(500)
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
                .DashboardColor = veh.DashboardColor,
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
                .Suspension = veh.GetMod(VehicleMod.Suspension)}
            veh.Position = New Vector3(-211.798, -1324.292, 30.37535)
            veh.Heading = 358.6677
            MainMenu.Visible = Not MainMenu.Visible
            BennysMenu.camera.RepositionFor(veh)
            Wait(500)
            Game.FadeScreenIn(500)
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        BennysBlip.Remove()
        Game.FadeScreenIn(500)
    End Sub
End Class
