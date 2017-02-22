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
    Public Shared fixDoor As Integer = 1
    Public Shared bennyIntID As Integer
    Public Shared isExiting As Boolean = False
    Public Shared lastVehMemory As Memory
    Public Shared BennysBlip As Blip
    Public Shared bennyPed As Ped
    Public Shared isCutscene As Boolean = False
    Public Shared scriptCam As ScriptedCamera

    Public Sub New()
        LoadSettings()
        bennyIntID = Helper.GetInteriorID(New Vector3(-211.798, -1324.292, 30.37535))
        CreateBlip()
        If Not IO.File.Exists("scripts\BennysOriginalMotorworks.ini") Then CreateTitleNames()
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

            'If Game.IsControlPressed(0, Control.Jump) AndAlso Game.IsControlPressed(0, Control.Reload) Then
            '    Dim s As String = Game.GetUserInput(System.Windows.Forms.Clipboard.GetText(), 99)
            '    UI.Notify(Game.GetGXTEntry(s))

            '    'For Each line As String In IO.File.ReadLines("C:\New.txt")
            '    '    Logger.Log(Game.GetGXTEntry(line) & ", " & line)
            '    'Next

            '    'For i As Integer = 0 To 500
            '    '    Logger.Write("CMOD_MOD_" & i & "_D = " & Game.GetGXTEntry("CMOD_MOD_" & i & "_D"))
            '    'Next
            '    'For i As Integer = 0 To 500
            '    '    Logger.Write("CMOD_SMOD_" & i & "_D = " & Game.GetGXTEntry("CMOD_SMOD_" & i & "_D"))
            '    'Next
            'End If

            If fixDoor = 1 Then
                If veh.Position.DistanceTo(New Vector3(-205.6828, -1310.683, 30.29572)) <= 10 Then
                    Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 0, 0.0, 50.0, 0)
                Else
                    Native.Function.Call(Hash._DOOR_CONTROL, -427498890, -205.6828, -1310.683, 30.29572, 1, 0.0, 50.0, 0)
                End If
            End If

            If Helper.GetInteriorID(ply.Position) = bennyIntID Then
                If Not isExiting Then
                    If veh.Position.DistanceTo(New Vector3(-205.6165, -1312.976, 31.1331)) <= 5 Then
                        SaveTitleNames()
                        PlayCutScene()
                        PutVehIntoShop()
                    Else
                        If veh.Position.DistanceTo(New Vector3(-211.798, -1324.292, 30.37535)) <= 5 Then
                            BennysMenu.camera.Update()
                            Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
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

    Public Shared Sub SaveTitleNames()
        Dim langConf As ScriptSettings = ScriptSettings.Load("scripts\BennysLang.ini")
        If langConf.GetValue("TITLE", "AERIALS") = "NULL" Then langConf.SetValue("TITLE", "AERIALS", Game.GetGXTEntry("CMM_MOD_ST18"))
        If langConf.GetValue("TITLE", "BODYWORK") = "NULL" Then langConf.SetValue("TITLE", "BODYWORK", Game.GetGXTEntry("CMOD_BW_T"))
        If langConf.GetValue("TITLE", "DOORS") = "NULL" Then langConf.SetValue("TITLE", "DOORS", Game.GetGXTEntry("CMM_MOD_ST6"))
        If langConf.GetValue("TITLE", "ENGINE") = "NULL" Then langConf.SetValue("TITLE", "ENGINE", Game.GetGXTEntry("CMM_MOD_GT3"))
        If langConf.GetValue("TITLE", "INTERIOR") = "NULL" Then langConf.SetValue("TITLE", "INTERIOR", Game.GetGXTEntry("CMM_MOD_GT1"))
        If langConf.GetValue("TITLE", "BUMPERS") = "NULL" Then langConf.SetValue("TITLE", "BUMPERS", Game.GetGXTEntry("CMOD_BUM_T"))
        If langConf.GetValue("TITLE", "WHEELS") = "NULL" Then langConf.SetValue("TITLE", "WHEELS", Game.GetGXTEntry("CMOD_WHE0_T"))
        If langConf.GetValue("TITLE", "WHEELTYPE") = "NULL" Then langConf.SetValue("TITLE", "WHEELTYPE", Game.GetGXTEntry("CMOD_WHE1_T"))
        If langConf.GetValue("TITLE", "TIRES") = "NULL" Then langConf.SetValue("TITLE", "TIRES", Game.GetGXTEntry("CMOD_TYR_T"))
        If langConf.GetValue("TITLE", "PLATES") = "NULL" Then langConf.SetValue("TITLE", "PLATES", Game.GetGXTEntry("CMM_MOD_GT2"))
        If langConf.GetValue("TITLE", "TRIM") = "NULL" Then langConf.SetValue("TITLE", "TRIM", Game.GetGXTEntry("CMM_MOD_ST19"))
        If langConf.GetValue("TITLE", "LIGHTS") = "NULL" Then langConf.SetValue("TITLE", "LIGHTS", Game.GetGXTEntry("CMOD_LGT_T"))
        If langConf.GetValue("TITLE", "ENGINEBLOCK") = "NULL" Then langConf.SetValue("TITLE", "ENGINEBLOCK", Game.GetGXTEntry("CMOD_EB_T"))
        If langConf.GetValue("TITLE", "AIRFILTER") = "NULL" Then langConf.SetValue("TITLE", "AIRFILTER", Game.GetGXTEntry("CMM_MOD_ST15"))
        If langConf.GetValue("TITLE", "RESPRAY") = "NULL" Then langConf.SetValue("TITLE", "RESPRAY", Game.GetGXTEntry("CMOD_COL0_T"))
        If langConf.GetValue("TITLE", "STRUTS") = "NULL" Then langConf.SetValue("TITLE", "STRUTS", Game.GetGXTEntry("CMM_MOD_ST16"))
        If langConf.GetValue("TITLE", "COLUMNSHIFTERLEVERS") = "NULL" Then langConf.SetValue("TITLE", "COLUMNSHIFTERLEVERS", Game.GetGXTEntry("CMM_MOD_ST9"))
        If langConf.GetValue("TITLE", "DASHBOARD") = "NULL" Then langConf.SetValue("TITLE", "DASHBOARD", Game.GetGXTEntry("CMM_MOD_ST4"))
        If langConf.GetValue("TITLE", "DIALDESIGN") = "NULL" Then langConf.SetValue("TITLE", "DIALDESIGN", Game.GetGXTEntry("CMM_MOD_ST5"))
        If langConf.GetValue("TITLE", "ORNAMENTS") = "NULL" Then langConf.SetValue("TITLE", "ORNAMENTS", Game.GetGXTEntry("CMM_MOD_ST3"))
        If langConf.GetValue("TITLE", "SEATS") = "NULL" Then langConf.SetValue("TITLE", "SEATS", Game.GetGXTEntry("CMM_MOD_ST7"))
        If langConf.GetValue("TITLE", "STEERINGWHEELS") = "NULL" Then langConf.SetValue("TITLE", "STEERINGWHEELS", Game.GetGXTEntry("CMM_MOD_ST8"))
        If langConf.GetValue("TITLE", "TRIMDESIGN") = "NULL" Then langConf.SetValue("TITLE", "TRIMDESIGN", Game.GetGXTEntry("CMM_MOD_ST2"))
        If langConf.GetValue("TITLE", "DOORS2") = "NULL" Then langConf.SetValue("TITLE", "DOORS2", Game.GetGXTEntry("CMM_MOD_ST6"))
        If langConf.GetValue("TITLE", "WINDOWS") = "NULL" Then langConf.SetValue("TITLE", "WINDOWS", Game.GetGXTEntry("CMM_MOD_ST21"))
        If langConf.GetValue("TITLE", "FRONTBUMPERS") = "NULL" Then langConf.SetValue("TITLE", "FRONTBUMPERS", Game.GetGXTEntry("CMOD_BUMF_T"))
        If langConf.GetValue("TITLE", "REARBUMPERS") = "NULL" Then langConf.SetValue("TITLE", "REARBUMPERS", Game.GetGXTEntry("CMOD_BUMR_T"))
        If langConf.GetValue("TITLE", "SIDESKIRT") = "NULL" Then langConf.SetValue("TITLE", "SIDESKIRT", Game.GetGXTEntry("CMOD_SS_T"))
        If langConf.GetValue("TITLE", "PLATEHOLDERS") = "NULL" Then langConf.SetValue("TITLE", "PLATEHOLDERS", Game.GetGXTEntry("CMOD_PLH_T"))
        If langConf.GetValue("TITLE", "VANITYPLATES") = "NULL" Then langConf.SetValue("TITLE", "VANITYPLATES", Game.GetGXTEntry("CMM_MOD_ST1"))
        If langConf.GetValue("TITLE", "HEADLIGHTS") = "NULL" Then langConf.SetValue("TITLE", "HEADLIGHTS", Game.GetGXTEntry("CMOD_HED_T"))
        If langConf.GetValue("TITLE", "ARCHCOVERS") = "NULL" Then langConf.SetValue("TITLE", "ARCHCOVERS", Game.GetGXTEntry("CMM_MOD_ST17"))
        If langConf.GetValue("TITLE", "EXHAUST") = "NULL" Then langConf.SetValue("TITLE", "EXHAUST", Game.GetGXTEntry("CMOD_EXH_T"))
        If langConf.GetValue("TITLE", "FENDER") = "NULL" Then langConf.SetValue("TITLE", "FENDER", Game.GetGXTEntry("CMOD_WNG_T"))
        If langConf.GetValue("TITLE", "RIGHTFENDER") = "NULL" Then langConf.SetValue("TITLE", "RIGHTFENDER", Game.GetGXTEntry("CMOD_WNG_T"))
        If langConf.GetValue("TITLE", "ROLLCAGE") = "NULL" Then langConf.SetValue("TITLE", "ROLLCAGE", Game.GetGXTEntry("CMOD_RC_T"))
        If langConf.GetValue("TITLE", "GRILLES") = "NULL" Then langConf.SetValue("TITLE", "GRILLES", Game.GetGXTEntry("CMOD_GRL_T"))
        If langConf.GetValue("TITLE", "HOOD") = "NULL" Then langConf.SetValue("TITLE", "HOOD", Game.GetGXTEntry("CMOD_BON_T"))
        If langConf.GetValue("TITLE", "HORN") = "NULL" Then langConf.SetValue("TITLE", "HORN", Game.GetGXTEntry("CMOD_HRN_T"))
        If langConf.GetValue("TITLE", "HYDRAULICS") = "NULL" Then langConf.SetValue("TITLE", "HYDRAULICS", Game.GetGXTEntry("CMM_MOD_ST13"))
        If langConf.GetValue("TITLE", "LIVERY") = "NULL" Then langConf.SetValue("TITLE", "LIVERY", Game.GetGXTEntry("CMM_MOD_ST23"))
        If langConf.GetValue("TITLE", "PLAQUES") = "NULL" Then langConf.SetValue("TITLE", "PLAQUES", Game.GetGXTEntry("CMM_MOD_ST10"))
        If langConf.GetValue("TITLE", "ROOF") = "NULL" Then langConf.SetValue("TITLE", "ROOF", Game.GetGXTEntry("CMOD_ROF_T"))
        If langConf.GetValue("TITLE", "SPEAKERS") = "NULL" Then langConf.SetValue("TITLE", "SPEAKERS", Game.GetGXTEntry("CMM_MOD_S11"))
        If langConf.GetValue("TITLE", "SPOILER") = "NULL" Then langConf.SetValue("TITLE", "SPOILER", Game.GetGXTEntry("CMOD_SPO_T"))
        If langConf.GetValue("TITLE", "TANK") = "NULL" Then langConf.SetValue("TITLE", "TANK", Game.GetGXTEntry("CMM_MOD_ST20"))
        If langConf.GetValue("TITLE", "TRUNKS") = "NULL" Then langConf.SetValue("TITLE", "TRUNKS", Game.GetGXTEntry("CMOD_TR_T"))
        If langConf.GetValue("TITLE", "TURBO") = "NULL" Then langConf.SetValue("TITLE", "TURBO", Game.GetGXTEntry("CMOD_TUR_T"))
        If langConf.GetValue("TITLE", "SUSPENSIONS") = "NULL" Then langConf.SetValue("TITLE", "SUSPENSIONS", Game.GetGXTEntry("CMOD_SUS_T"))
        If langConf.GetValue("TITLE", "ARMOR") = "NULL" Then langConf.SetValue("TITLE", "ARMOR", Game.GetGXTEntry("CMOD_ARM_T"))
        If langConf.GetValue("TITLE", "BRAKES") = "NULL" Then langConf.SetValue("TITLE", "BRAKES", Game.GetGXTEntry("CMOD_BRA_T"))
        If langConf.GetValue("TITLE", "TRANSMISSION") = "NULL" Then langConf.SetValue("TITLE", "TRANSMISSION", Game.GetGXTEntry("CMOD_GBX_T"))
        If langConf.GetValue("TITLE", "LICENSE") = "NULL" Then langConf.SetValue("TITLE", "LICENSE", Game.GetGXTEntry("CMOD_MOD_PLA2").ToUpper)
        If langConf.GetValue("TITLE", "NEONKITS") = "NULL" Then langConf.SetValue("TITLE", "NEONKITS", Game.GetGXTEntry("CMOD_MOD_LGT_N").ToUpper)
        If langConf.GetValue("TITLE", "NEONLAYOUT") = "NULL" Then langConf.SetValue("TITLE", "NEONLAYOUT", Game.GetGXTEntry("CMOD_NEON_0").ToUpper)
        If langConf.GetValue("TITLE", "BIKEWHEELS") = "NULL" Then langConf.SetValue("TITLE", "BIKEWHEELS", Helper.GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper)
        If langConf.GetValue("TITLE", "HIGHEND") = "NULL" Then langConf.SetValue("TITLE", "HIGHEND", Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper)
        If langConf.GetValue("TITLE", "LOWRIDER") = "NULL" Then langConf.SetValue("TITLE", "LOWRIDER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper)
        If langConf.GetValue("TITLE", "MUSCLE") = "NULL" Then langConf.SetValue("TITLE", "MUSCLE", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper)
        If langConf.GetValue("TITLE", "OFFROAD") = "NULL" Then langConf.SetValue("TITLE", "OFFROAD", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper)
        If langConf.GetValue("TITLE", "SPORT") = "NULL" Then langConf.SetValue("TITLE", "SPORT", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper)
        If langConf.GetValue("TITLE", "SUV") = "NULL" Then langConf.SetValue("TITLE", "SUV", Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper)
        If langConf.GetValue("TITLE", "TUNER") = "NULL" Then langConf.SetValue("TITLE", "TUNER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper)
        If langConf.GetValue("TITLE", "BENNYS") = "NULL" Then langConf.SetValue("TITLE", "BENNYS", Helper.GetLocalizedWheelTypeName(8).ToUpper)
        If langConf.GetValue("TITLE", "BESPOKE") = "NULL" Then langConf.SetValue("TITLE", "BESPOKE", Helper.GetLocalizedWheelTypeName(9).ToUpper)
        If langConf.GetValue("TITLE", "LIGHTCOLOR") = "NULL" Then langConf.SetValue("TITLE", "LIGHTCOLOR", Game.GetGXTEntry("CMM_MOD_ST26").ToUpper)
        If langConf.GetValue("TITLE", "PRIMARYCOLOR") = "NULL" Then langConf.SetValue("TITLE", "PRIMARYCOLOR", Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        If langConf.GetValue("TITLE", "SECONDARYCOLOR") = "NULL" Then langConf.SetValue("TITLE", "SECONDARYCOLOR", Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        If langConf.GetValue("TITLE", "LIVERYCOLOR") = "NULL" Then langConf.SetValue("TITLE", "LIVERYCOLOR", Game.GetGXTEntry("CMOD_COL0_4").ToUpper)
        If langConf.GetValue("TITLE", "COLORGROUPS") = "NULL" Then langConf.SetValue("TITLE", "COLORGROUPS", Game.GetGXTEntry("CMOD_COL1_T"))
        If langConf.GetValue("TITLE", "WHEELCOLORS") = "NULL" Then langConf.SetValue("TITLE", "WHEELCOLORS", Game.GetGXTEntry("CMOD_COL5_T"))
        If langConf.GetValue("TITLE", "TINTS") = "NULL" Then langConf.SetValue("TITLE", "TINTS", Game.GetGXTEntry("CMOD_WIN_T"))
        If langConf.GetValue("TITLE", "TRIMCOLOR") = "NULL" Then langConf.SetValue("TITLE", "TRIMCOLOR", Game.GetGXTEntry("CMOD_MOD_TRIM2").ToUpper)
        If langConf.GetValue("TITLE", "NEONCOLOR") = "NULL" Then langConf.SetValue("TITLE", "NEONCOLOR", Game.GetGXTEntry("CMOD_NEON_1").ToUpper)
        If langConf.GetValue("TITLE", "TIRESMOKE") = "NULL" Then langConf.SetValue("TITLE", "TIRESMOKE", Game.GetGXTEntry("CMOD_MOD_TYR3").ToUpper)
        If langConf.GetValue("TITLE", "CATEGORIES") = "NULL" Then langConf.SetValue("TITLE", "CATEGORIES", Game.GetGXTEntry("CMOD_MOD_T"))
        'If langConf.GetValue("TITLE", "") = "NULL"  Then langConf.SetValue("TITLE", "", Game.GetGXTEntry(""))
        'If langConf.GetValue("TITLE", "") = "NULL"  Then langConf.SetValue("TITLE", "", Game.GetGXTEntry(""))
        langConf.Save()
    End Sub

    Public Shared Sub CreateTitleNames()
        Dim langConf As ScriptSettings = ScriptSettings.Load("scripts\BennysLang.ini")
        langConf.SetValue("TITLE", "AERIALS", Game.GetGXTEntry("CMM_MOD_ST18"))
        langConf.SetValue("TITLE", "BODYWORK", Game.GetGXTEntry("CMOD_BW_T"))
        langConf.SetValue("TITLE", "DOORS", Game.GetGXTEntry("CMM_MOD_ST6"))
        langConf.SetValue("TITLE", "ENGINE", Game.GetGXTEntry("CMM_MOD_GT3"))
        langConf.SetValue("TITLE", "INTERIOR", Game.GetGXTEntry("CMM_MOD_GT1"))
        langConf.SetValue("TITLE", "BUMPERS", Game.GetGXTEntry("CMOD_BUM_T"))
        langConf.SetValue("TITLE", "WHEELS", Game.GetGXTEntry("CMOD_WHE0_T"))
        langConf.SetValue("TITLE", "WHEELTYPE", Game.GetGXTEntry("CMOD_WHE1_T"))
        langConf.SetValue("TITLE", "TIRES", Game.GetGXTEntry("CMOD_TYR_T"))
        langConf.SetValue("TITLE", "PLATES", Game.GetGXTEntry("CMM_MOD_GT2"))
        langConf.SetValue("TITLE", "TRIM", Game.GetGXTEntry("CMM_MOD_ST19"))
        langConf.SetValue("TITLE", "LIGHTS", Game.GetGXTEntry("CMOD_LGT_T"))
        langConf.SetValue("TITLE", "ENGINEBLOCK", Game.GetGXTEntry("CMOD_EB_T"))
        langConf.SetValue("TITLE", "AIRFILTER", Game.GetGXTEntry("CMM_MOD_ST15"))
        langConf.SetValue("TITLE", "RESPRAY", Game.GetGXTEntry("CMOD_COL0_T"))
        langConf.SetValue("TITLE", "STRUTS", Game.GetGXTEntry("CMM_MOD_ST16"))
        langConf.SetValue("TITLE", "COLUMNSHIFTERLEVERS", Game.GetGXTEntry("CMM_MOD_ST9"))
        langConf.SetValue("TITLE", "DASHBOARD", Game.GetGXTEntry("CMM_MOD_ST4"))
        langConf.SetValue("TITLE", "DIALDESIGN", Game.GetGXTEntry("CMM_MOD_ST5"))
        langConf.SetValue("TITLE", "ORNAMENTS", Game.GetGXTEntry("CMM_MOD_ST3"))
        langConf.SetValue("TITLE", "SEATS", Game.GetGXTEntry("CMM_MOD_ST7"))
        langConf.SetValue("TITLE", "STEERINGWHEELS", Game.GetGXTEntry("CMM_MOD_ST8"))
        langConf.SetValue("TITLE", "TRIMDESIGN", Game.GetGXTEntry("CMM_MOD_ST2"))
        langConf.SetValue("TITLE", "DOORS2", Game.GetGXTEntry("CMM_MOD_ST6"))
        langConf.SetValue("TITLE", "WINDOWS", Game.GetGXTEntry("CMM_MOD_ST21"))
        langConf.SetValue("TITLE", "FRONTBUMPERS", Game.GetGXTEntry("CMOD_BUMF_T"))
        langConf.SetValue("TITLE", "REARBUMPERS", Game.GetGXTEntry("CMOD_BUMR_T"))
        langConf.SetValue("TITLE", "SIDESKIRT", Game.GetGXTEntry("CMOD_SS_T"))
        langConf.SetValue("TITLE", "PLATEHOLDERS", Game.GetGXTEntry("CMOD_PLH_T"))
        langConf.SetValue("TITLE", "VANITYPLATES", Game.GetGXTEntry("CMM_MOD_ST1"))
        langConf.SetValue("TITLE", "HEADLIGHTS", Game.GetGXTEntry("CMOD_HED_T"))
        langConf.SetValue("TITLE", "ARCHCOVERS", Game.GetGXTEntry("CMM_MOD_ST17"))
        langConf.SetValue("TITLE", "EXHAUST", Game.GetGXTEntry("CMOD_EXH_T"))
        langConf.SetValue("TITLE", "FENDER", Game.GetGXTEntry("CMOD_WNG_T"))
        langConf.SetValue("TITLE", "RIGHTFENDER", Game.GetGXTEntry("CMOD_WNG_T"))
        langConf.SetValue("TITLE", "ROLLCAGE", Game.GetGXTEntry("CMOD_RC_T"))
        langConf.SetValue("TITLE", "GRILLES", Game.GetGXTEntry("CMOD_GRL_T"))
        langConf.SetValue("TITLE", "HOOD", Game.GetGXTEntry("CMOD_BON_T"))
        langConf.SetValue("TITLE", "HORN", Game.GetGXTEntry("CMOD_HRN_T"))
        langConf.SetValue("TITLE", "HYDRAULICS", Game.GetGXTEntry("CMM_MOD_ST13"))
        langConf.SetValue("TITLE", "LIVERY", Game.GetGXTEntry("CMM_MOD_ST23"))
        langConf.SetValue("TITLE", "PLAQUES", Game.GetGXTEntry("CMM_MOD_ST10"))
        langConf.SetValue("TITLE", "ROOF", Game.GetGXTEntry("CMOD_ROF_T"))
        langConf.SetValue("TITLE", "SPEAKERS", Game.GetGXTEntry("CMM_MOD_S11"))
        langConf.SetValue("TITLE", "SPOILER", Game.GetGXTEntry("CMOD_SPO_T"))
        langConf.SetValue("TITLE", "TANK", Game.GetGXTEntry("CMM_MOD_ST20"))
        langConf.SetValue("TITLE", "TRUNKS", Game.GetGXTEntry("CMOD_TR_T"))
        langConf.SetValue("TITLE", "TURBO", Game.GetGXTEntry("CMOD_TUR_T"))
        langConf.SetValue("TITLE", "SUSPENSIONS", Game.GetGXTEntry("CMOD_SUS_T"))
        langConf.SetValue("TITLE", "ARMOR", Game.GetGXTEntry("CMOD_ARM_T"))
        langConf.SetValue("TITLE", "BRAKES", Game.GetGXTEntry("CMOD_BRA_T"))
        langConf.SetValue("TITLE", "TRANSMISSION", Game.GetGXTEntry("CMOD_GBX_T"))
        langConf.SetValue("TITLE", "LICENSE", Game.GetGXTEntry("CMOD_MOD_PLA2").ToUpper)
        langConf.SetValue("TITLE", "NEONKITS", Game.GetGXTEntry("CMOD_MOD_LGT_N").ToUpper)
        langConf.SetValue("TITLE", "NEONLAYOUT", Game.GetGXTEntry("CMOD_NEON_0").ToUpper)
        langConf.SetValue("TITLE", "BIKEWHEELS", Helper.GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper)
        langConf.SetValue("TITLE", "HIGHEND", Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper)
        langConf.SetValue("TITLE", "LOWRIDER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper)
        langConf.SetValue("TITLE", "MUSCLE", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper)
        langConf.SetValue("TITLE", "OFFROAD", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper)
        langConf.SetValue("TITLE", "SPORT", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper)
        langConf.SetValue("TITLE", "SUV", Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper)
        langConf.SetValue("TITLE", "TUNER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper)
        langConf.SetValue("TITLE", "BENNYS", Helper.GetLocalizedWheelTypeName(8).ToUpper)
        langConf.SetValue("TITLE", "BESPOKE", Helper.GetLocalizedWheelTypeName(9).ToUpper)
        langConf.SetValue("TITLE", "LIGHTCOLOR", Game.GetGXTEntry("CMM_MOD_ST26").ToUpper)
        langConf.SetValue("TITLE", "PRIMARYCOLOR", Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        langConf.SetValue("TITLE", "SECONDARYCOLOR", Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        langConf.SetValue("TITLE", "LIVERYCOLOR", Game.GetGXTEntry("CMOD_COL0_4").ToUpper)
        langConf.SetValue("TITLE", "COLORGROUPS", Game.GetGXTEntry("CMOD_COL1_T"))
        langConf.SetValue("TITLE", "WHEELCOLORS", Game.GetGXTEntry("CMOD_COL5_T"))
        langConf.SetValue("TITLE", "TINTS", Game.GetGXTEntry("CMOD_WIN_T"))
        langConf.SetValue("TITLE", "TRIMCOLOR", Game.GetGXTEntry("CMOD_MOD_TRIM2").ToUpper)
        langConf.SetValue("TITLE", "NEONCOLOR", Game.GetGXTEntry("CMOD_NEON_1").ToUpper)
        langConf.SetValue("TITLE", "TIRESMOKE", Game.GetGXTEntry("CMOD_MOD_TYR3").ToUpper)
        langConf.SetValue("TITLE", "CATEGORIES", Game.GetGXTEntry("CMOD_MOD_T"))
        langConf.Save()
    End Sub

    Public Shared Sub PlayCutScene()
        Try
            Game.FadeScreenOut(500)
            Wait(500)
            isExiting = True
            isCutscene = True
            Native.Function.Call(Hash._0x260BE8F09E326A20, veh, 3.0, 0, False)
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
            ply.Task.DriveTo(veh, New Vector3(-211.798, -1324.292, 30.37535), 0.1, 2)
            Wait(7000)
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
        If Not bennyPed = Nothing Then bennyPed.Delete()
    End Sub
End Class
