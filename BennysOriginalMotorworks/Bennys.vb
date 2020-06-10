Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports BennysOriginalMotorworks.BennysMenu
Imports System.Windows.Forms
Imports System.Drawing
Imports Metadata

Public Class Bennys
    Inherits Script

    Public Sub New()
        LoadSettings()
        bennyIntID = Helper.GetInteriorID(New Vector3(-211.798, -1324.292, 30.37535))
        CreateBlip()
        Game.Globals(GetGlobalValue).SetInt(1)
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
                            camera.Update()
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
                    Game.DisableAllControlsThisFrame(0)
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try

        If _menuPool.IsAnyMenuOpen Then
            If Game.IsControlPressed(0, zinKey) AndAlso camera.MainCameraPosition <> CameraPosition.Interior Then
                Dim max As New PointF(6.0F + camera.Dimension, 3.0F + camera.Dimension)
                If Not camera.CameraZoom <= max.Y Then
                    camera.CameraZoom -= 0.1
                Else
                    camera.CameraZoom = max.Y
                End If
            End If
            If Game.IsControlPressed(0, zoutKey) AndAlso camera.MainCameraPosition <> CameraPosition.Interior Then
                Dim max As New PointF(6.0F + camera.Dimension, 3.0F + camera.Dimension)
                If Not camera.CameraZoom >= max.X Then
                    camera.CameraZoom += 0.1
                Else
                    camera.CameraZoom = max.X
                End If
            End If

            If Game.IsControlJustReleased(0, fpcKey) Then
                lastCameraPos = camera.MainCameraPosition
                If camera.MainCameraPosition = CameraPosition.Interior Then
                    If lastCameraPos = CameraPosition.Interior Then
                        camera.MainCameraPosition = CameraPosition.Car
                    Else
                        camera.MainCameraPosition = lastCameraPos
                    End If
                Else
                    camera.MainCameraPosition = CameraPosition.Interior
                End If
            End If
        End If
    End Sub

    Public Sub OnAborted() Handles MyBase.Aborted
        BennysBlip.Remove()
        Game.FadeScreenIn(1000)
        If Not bennyPed = Nothing Then bennyPed.Delete()
    End Sub
End Class
