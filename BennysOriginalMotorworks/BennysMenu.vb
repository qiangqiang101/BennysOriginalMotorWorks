Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports System.Text
Imports System.Drawing
Imports Metadata

Public Class BennysMenu
    Inherits Script

    Public IsScriptLoaded As Boolean = False

    Public Sub New()
        _menuPool = New MenuPool()
        camera = New WorkshopCamera
        BtnFirstPerson = New InstructionalButton(fpcKey, Game.GetGXTEntry("MO_ZOOM_FIRST")) 'MO_ZOOM_FIRST   LOB_FCP_1
        BtnZoom = New InstructionalButton(zinKey, Game.GetGXTEntry("INPUT_CREATOR_ZOOM_IN_DISPLAYONLY")) 'CELL_284
        BtnZoomOut = New InstructionalButton(zoutKey, Game.GetGXTEntry("INPUT_CREATOR_ZOOM_OUT_DISPLAYONLY"))

        Native.Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_1", False, -1)
        Native.Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_2", False, -1)
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        If Not IsScriptLoaded Then
            If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 19) Then
                Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 19, True)
                Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 19)
            End If
            CreateMenus()
            IsScriptLoaded = True
        End If

        Try
            _menuPool.ProcessMenus()
            If Not veh = Nothing Then vehStats = GetVehicleStats(veh)
            _menuPool.UpdateStats(vehStats.TopSpeed, vehStats.Acceleration, vehStats.Braking, vehStats.Traction)

            If mUpgradeAW.Visible Then
                Dim spr As New Sprite("aw_upg_vehs", arenaVehImage, mUpgradeAW.GetUIMenuOffset, New Size(431, 216), 0F, Color.White) : spr.Draw()
            End If

            If _menuPool.IsAnyMenuOpen Then
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                If BtnZoom.Text = "NULL" Then BtnZoom.Text = Game.GetGXTEntry("INPUT_CREATOR_ZOOM_IN_DISPLAYONLY")
                If BtnZoomOut.Text = "NULL" Then BtnZoom.Text = Game.GetGXTEntry("INPUT_CREATOR_ZOOM_OUT_DISPLAYONLY")
                If BtnFirstPerson.Text = "NULL" Then BtnFirstPerson.Text = Game.GetGXTEntry("MO_ZOOM_FIRST")
            End If

            If isCutscene Then
                Game.DisableAllControlsThisFrame(0)
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                Dim sr = Size.Round(UIMenu.GetScreenResolutionMaintainRatio)
                Dim sz = UIMenu.GetSafezoneBounds
                Dim vname As String = $"{veh.Brand} {veh.FriendlyName}"
                Dim vclass As String = GetClassDisplayName(veh.ClassType)

                Select Case Game.Language
                    Case Language.Chinese, Language.Japanese, Language.Korean, Language.ChineseSimplified
                        Dim vn As New UIResText(vname, New Point(sr.Width - sz.X - 100, sr.Height - sz.Y - 240), 2.0F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Right) With {.DropShadow = True} : vn.Draw()
                        Dim vc As New UIResText(vclass, New Point(sr.Width - sz.X, sr.Height - sz.Y - 170), 2.0F, Color.DodgerBlue, GTA.Font.HouseScript, UIResText.Alignment.Right) With {.DropShadow = True} : vc.Draw()
                    Case Else
                        Dim vn As New UIResText(vname, New Point(sr.Width - sz.X - 100, sr.Height - sz.Y - 240), 2.0F, Color.White, GTA.Font.ChaletComprimeCologne, UIResText.Alignment.Right) With {.DropShadow = True} : vn.Draw()
                        Dim vc As New UIResText(vclass, New Point(sr.Width - sz.X, sr.Height - sz.Y - 170), 2.0F, Color.DodgerBlue, GTA.Font.HouseScript, UIResText.Alignment.Right) With {.DropShadow = True} : vc.Draw()
                End Select
            End If

            If isRepairing Then
                _menuPool.CloseAllMenus()
                MainMenu.Visible = True
                isRepairing = False
            End If

            Select Case True
                Case _menuPool.IsAnyMenuOpen()
                    SuspendKeys()
            End Select
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

End Class
