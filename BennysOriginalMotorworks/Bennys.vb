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
    Public Shared lastVehMemory As VehicleDefaultParts

    Public Sub New()
        LoadSettings()
        bennyIntID = Helper.GetInteriorID(New Vector3(-211.798, -1324.292, 30.37535))
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
                    End If
                End If
            End If

        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub PutVehIntoShop()
        Try
            Game.FadeScreenOut(500)
            Wait(500)
            veh.InstallModKit()
            'Refresh Menus
            RefreshMainMenu()
            RefreshBodyworkMenu()
            RefreshEngineMenu()
            RefreshInteriorMenu()
            RefreshBumperMenu()
            RefreshWheelsMenu()
            RefreshPlateMenu()
            RefreshLightsMenu()
            RefreshResprayMenu()
            RefreshSuspensionMenu()
            lastVehMemory = New VehicleDefaultParts() With {.Aerials = veh.GetMod(VehicleMod.Aerials), .Suspension = veh.GetMod(VehicleMod.Suspension)}
            veh.Position = New Vector3(-211.798, -1324.292, 30.37535)
            veh.Heading = 358.6677
            MainMenu.Visible = Not MainMenu.Visible
            Wait(500)
            Game.FadeScreenIn(500)
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        Wait(500)
        Game.FadeScreenIn(500)
    End Sub
End Class
