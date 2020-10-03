﻿Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports INMNativeUI
Imports Metadata

Public Module Helper

    Public nitroMod As String = "inm_nitro_active"

    Public lowriders As New List(Of Model) From {"banshee", "Buccaneer", "chino", "diablous", "comet2", "faction", "faction2", "fcr", "italigtb", "minivan", "moonbeam", "nero", "primo", "sabregt",
        "slamvan", "specter", "sultan", "tornado", "tornado2", "tornado3", "virgo3", "voodoo2", "elegy2", "technical", "insurgent", "youga2", "yosemite", "peyote", "manana", "glendale", "gauntlet3"}
    Public arenawar As New List(Of Model) From {"glendale", "gargoyle", "dominator", "dominator2", "impaler", "issi3", "ratloader", "ratloader2", "slamvan", "slamvan2", "slamvan3"}
    Public bennysvehicle As New List(Of Model) From {"banshee2", "buccaneer2", "chino2", "diabolus2", "comet3", "faction2", "faction3", "fcr2", "italigtb2", "minivan2", "moonbeam2", "nero2", "primo2",
        "sabregt2", "specter2", "sultanrs", "tornado5", "virgo2", "voodoo", "elegy", "technical3", "insurgent3", "youga3", "yosemite3", "peyote3", "manana2", "glendale2", "gauntlet5"}
    Public arenavehicle As New List(Of Model) From {"bruiser", "bruiser2", "bruiser3", "cerberus", "cerberus2", "cerberus3", "deathbike", "deathbike2", "deathbike3", "dominator4", "dominator5",
        "dominator6", "impaler2", "impaler3", "impaler4", "imperator", "imperator2", "imperator3", "issi4", "issi5", "issi6", "monster3", "monster4", "monster5", "slamvan4", "slamvan5", "slamvan6", "brutus", "brutus2", "brutus3", "scarab", "scarab2",
        "scarab3", "zr380", "zr3802", "zr3803"}

    Public veh, tra As Vehicle
    Public ply As Ped
    Public onlineMap As Integer = 1
    Public fixDoor As Integer = 1
    Public bennyIntID As Integer
    Public isExiting As Boolean = False
    Public lastVehMemory As Memory
    Public BennysBlip As Blip
    Public bennyPed As Ped
    Public isCutscene As Boolean = False
    Public scriptCam As Camera 'ScriptedCamera
    Public unWelcome As List(Of VehicleClass) = New List(Of VehicleClass) From {VehicleClass.Boats, VehicleClass.Cycles, VehicleClass.Helicopters, VehicleClass.Planes}
    Public fpcKey, zoutKey, zinKey As GTA.Control
    Public lastCameraPos As CameraPosition

    Public BtnZoom, BtnZoomOut, BtnFirstPerson As InstructionalButton
    Public _menuPool As MenuPool
    Public camera As WorkshopCamera
    Public isRepairing As Boolean = False
    Public vehStats As VehicleStats
    Public arenaVehImage As String = "brusier_apoc"

    Public Function CreateVehicle(VehicleModel As String, VehicleHash As Integer, Position As Vector3, Optional Heading As Single = 0) As Vehicle
        Dim Result As Vehicle = Nothing
        If VehicleModel = "" Then
            Dim model = New Model(VehicleHash)
            model.Request(250)
            If model.IsInCdImage AndAlso model.IsValid Then
                While Not model.IsLoaded
                    Script.Wait(50)
                End While
                Result = WorldCreateVehicle(model, Position, Heading)
            End If
            model.MarkAsNoLongerNeeded()
        Else
            Dim model = New Model(VehicleModel)
            model.Request(250)
            If model.IsInCdImage AndAlso model.IsValid Then
                While Not model.IsLoaded
                    Script.Wait(50)
                End While
                Result = WorldCreateVehicle(model, Position, Heading)
            End If
            model.MarkAsNoLongerNeeded()
        End If
        Return Result
    End Function

    Public Function WorldCreateVehicle(model As Model, position As Vector3, Optional heading As Single = 0F) As Vehicle
        If Not model.IsVehicle OrElse Not model.Request(1000) Then
            Return Nothing
        End If

        Return New Vehicle(Native.Function.Call(Of Integer)(Hash.CREATE_VEHICLE, model.Hash, position.X, position.Y, position.Z, heading,
        False, False))
    End Function

    Public Sub LoadMPDLCMap()
        Native.Function.Call(Hash._LOAD_MP_DLC_MAPS)
        Native.Function.Call(Hash._LOWER_MAP_PROP_DENSITY, True)
        LoadMPDLCMapMissingObjects()
    End Sub

    Public Sub LoadMPDLCMapMissingObjects()
        Dim TID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -1155.31005, -1518.5699, 10.6300001) 'Floyd Apartment
        Dim MID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -802.31097, 175.05599, 72.84459) 'Michael House
        Dim FID1 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -9.96562, -1438.54003, 31.101499) 'Franklin Aunt House
        Dim FID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 0.91675, 528.48498, 174.628005) 'Franklin House

        Dim WODID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -172.983001, 494.032989, 137.654006) '3655 Wild Oats
        Dim NCAID1 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 340.941009, 437.17999, 149.389999) '2044 North Conker
        Dim NCAID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 373.0230102, 416.1050109, 145.70100402) '2045 North Conker
        Dim HCAID1 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -676.1270141, 588.6119995, 145.16999816) '2862 Hillcrest Avenue
        Dim HCAID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -763.10699462, 615.90600585, 144.139999) '2868 Hillcrest Avenue
        Dim HCAID3 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -857.79797363, 682.56298828, 152.6529998) '2874 Hillcrest Avenue
        Dim MRID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -572.60998535, 653.13000488, 145.63000488) '2117 Milton Road
        Dim WMDID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 120.5, 549.952026367, 184.09700012207) '3677 Whispymound Drive
        Dim MWTDID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -1288, 440.74798583, 97.694602966) '2113 Mad Wayne Thunder Drive

        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID1, "V_57_FranklinStuff")

        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, TID2, "swap_clean_apt")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, TID2, "layer_whiskey")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, TID2, "layer_sextoys_a")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, TID2, "swap_mrJam_A")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, TID2, "swap_sofa_A")

        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "V_Michael_bed_tidy")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "V_Michael_L_Items")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "V_Michael_S_Items")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "V_Michael_D_Items")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "V_Michael_M_Items")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "Michael_premier")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MID, "V_Michael_plane_ticket")

        'Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "showhome_only")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "franklin_settled")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "franklin_unpacking")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "bong_and_wine")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "progress_flyer")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "progress_tshirt")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "progress_tux")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, FID2, "unlocked")

        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, WODID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, NCAID1, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, NCAID2, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, HCAID1, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, HCAID2, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, HCAID3, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MRID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, WMDID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._ENABLE_INTERIOR_PROP, MWTDID, "Stilts_Kitchen_Window")

        Native.Function.Call(Hash.REFRESH_INTERIOR, FID1)
        Native.Function.Call(Hash.REFRESH_INTERIOR, TID2)
        Native.Function.Call(Hash.REFRESH_INTERIOR, MID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, FID2)

        Native.Function.Call(Hash.REFRESH_INTERIOR, WODID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, NCAID1)
        Native.Function.Call(Hash.REFRESH_INTERIOR, NCAID2)
        Native.Function.Call(Hash.REFRESH_INTERIOR, HCAID1)
        Native.Function.Call(Hash.REFRESH_INTERIOR, HCAID2)
        Native.Function.Call(Hash.REFRESH_INTERIOR, HCAID3)
        Native.Function.Call(Hash.REFRESH_INTERIOR, MRID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, WMDID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, MWTDID)
    End Sub

    Public Sub DisplayHelpTextThisFrame(helpText As String, Optional Shape As Integer = -1)
        Native.Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "CELL_EMAIL_BCON")
        Const maxStringLength As Integer = 99

        Dim i As Integer = 0
        While i < helpText.Length
            Native.Function.Call(Hash._0x6C188BE134E074AA, helpText.Substring(i, System.Math.Min(maxStringLength, helpText.Length - i)))
            i += maxStringLength
        End While
        Native.Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, 0, 1, Shape)
    End Sub

    Public Function GetInteriorID(interior As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, interior.X, interior.Y, interior.Z)
    End Function

    Public Function LowriderUpgrade(model As Model) As Model
        Dim result As Model = model
        Select Case model
            Case "banshee"
                result = "banshee2"
            Case "buccaneer"
                result = "buccaneer2"
            Case "chino"
                result = "chino2"
            Case "diablous"
                result = "diablous2"
            Case "comet2"
                result = "comet3"
            Case "faction"
                result = "faction2"
            Case "faction2"
                result = "faction3"
            Case "fcr"
                result = "fcr2"
            Case "italigtb"
                result = "italigtb2"
            Case "minivan"
                result = "minivan2"
            Case "moonbeam"
                result = "moonbeam2"
            Case "nero"
                result = "nero2"
            Case "primo"
                result = "primo2"
            Case "sabregt"
                result = "sabregt2"
            Case "slamvan", "slamvan2"
                result = "slamvan3"
            Case "specter"
                result = "specter2"
            Case "sultan"
                result = "sultanrs"
            Case "tornado", "tornado2", "tornado3"
                result = "tornado5"
            Case "virgo3"
                result = "virgo2"
            Case "voodoo2"
                result = "voodoo"
            Case "elegy2"
                result = "elegy"
            Case "technical"
                result = "technical3"
            Case "insurgent"
                result = "insurgent3"
            Case "youga2"
                result = "youga3"
            Case "yosemite"
                result = "yosemite3"
            Case "peyote"
                result = "peyote3"
            Case "manana"
                result = "manana2"
            Case "glendale"
                result = "glendale2"
            Case "gauntlet3"
                result = "gauntlet5"
            Case Else
                result = model
        End Select
        Return result
    End Function

    Public Enum ScreenEffect
        SwitchHudIn
        SwitchHudOut
        FocusIn
        FocusOut
        MinigameEndNeutral
        MinigameEndTrevor
        MinigameEndFranklin
        MinigameEndMichael
        MinigameTransitionOut
        MinigameTransitionIn
        SwitchShortNeutralIn
        SwitchShortFranklinIn
        SwitchShortTrevorIn
        SwitchShortMichaelIn
        SwitchOpenMichaelIn
        SwitchOpenFranklinIn
        SwitchOpenTrevorIn
        SwitchHudMichaelOut
        SwitchHudFranklinOut
        SwitchHudTrevorOut
        SwitchShortFranklinMid
        SwitchShortMichaelMid
        SwitchShortTrevorMid
        DeathFailOut
        CamPushInNeutral
        CamPushInFranklin
        CamPushInMichael
        CamPushInTrevor
        SwitchSceneFranklin
        SwitchSceneTrevor
        SwitchSceneMichael
        SwitchSceneNeutral
        MpCelebWin
        MpCelebWinOut
        MpCelebLose
        MpCelebLoseOut
        DeathFailNeutralIn
        DeathFailMpDark
        DeathFailMpIn
        MpCelebPreloadFade
        PeyoteEndOut
        PeyoteEndIn
        PeyoteIn
        PeyoteOut
        MpRaceCrash
        SuccessFranklin
        SuccessTrevor
        SuccessMichael
        DrugsMichaelAliensFightIn
        DrugsMichaelAliensFight
        DrugsMichaelAliensFightOut
        DrugsTrevorClownsFightIn
        DrugsTrevorClownsFight
        DrugsTrevorClownsFightOut
        HeistCelebPass
        HeistCelebPassBw
        HeistCelebEnd
        HeistCelebToast
        MenuMgHeistIn
        MenuMgTournamentIn
        MenuMgSelectionIn
        ChopVision
        DmtFlightIntro
        DmtFlight
        DrugsDrivingIn
        DrugsDrivingOut
        SwitchOpenNeutralFib5
        HeistLocate
        MpJobLoad
        RaceTurbo
        MpIntroLogo
        HeistTripSkipFade
        MenuMgHeistOut
        MpCoronaSwitch
        MenuMgSelectionTint
        SuccessNeutral
        ExplosionJosh3
        SniperOverlay
        RampageOut
        Rampage
        DontTazemeBro
    End Enum

    Public Sub ScreenEffectStart(effectName As ScreenEffect, Optional duration As Integer = 0, Optional looped As Boolean = False)
        Native.Function.Call(Hash._START_SCREEN_EFFECT, New InputArgument() {[Enum].GetName(GetType(ScreenEffect), effectName), duration, looped})
    End Sub

    Public Function LocalizedModTypeName(modType As VehicleMod) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String = Nothing
        Select Case modType
            Case VehicleMod.FrontBumper
                cur = Game.GetGXTEntry("CMOD_MOD_BUMF")
                Exit Select
            Case VehicleMod.RearBumper
                cur = Game.GetGXTEntry("CMOD_MOD_BUMR")
                Exit Select
            Case VehicleMod.SideSkirt
                cur = Game.GetGXTEntry("CMOD_MOD_SKI")
                Exit Select
            Case VehicleMod.Armor
                cur = Game.GetGXTEntry("CMOD_MOD_ARM")
                Exit Select
            Case VehicleMod.Brakes
                cur = Game.GetGXTEntry("CMOD_MOD_BRA")
                Exit Select
            Case VehicleMod.Engine
                cur = Game.GetGXTEntry("CMOD_MOD_ENG")
                Exit Select
            Case VehicleMod.Suspension
                cur = Game.GetGXTEntry("CMOD_MOD_SUS")
                Exit Select
            Case VehicleMod.Transmission
                cur = Game.GetGXTEntry("CMOD_MOD_TRN")
                Exit Select
            Case VehicleMod.Horns
                cur = Game.GetGXTEntry("CMOD_MOD_HRN")
                Exit Select
            Case VehicleMod.FrontWheels
                If Not veh.Model.IsBike AndAlso veh.Model.IsBicycle Then
                    cur = Game.GetGXTEntry("CMOD_MOD_WHEM")
                    If cur = "" Then
                        Return "Wheels"
                    End If
                Else
                    cur = Game.GetGXTEntry("CMOD_WHE0_0")
                End If
                Exit Select
            Case VehicleMod.BackWheels
                cur = Game.GetGXTEntry("CMOD_WHE0_1")
                Exit Select

            'Bennys
            Case VehicleMod.PlateHolder
                cur = Game.GetGXTEntry("CMM_MOD_S0")
                Exit Select
            Case VehicleMod.VanityPlates
                If veh.Model = "elegy" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S40")
                ElseIf arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("collision_yzkcrh").ToLower.UppercaseFirstLetter 'Rear Wibbles
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S1")
                End If
                Exit Select
            Case VehicleMod.TrimDesign
                If veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S2b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S2")
                End If
                Exit Select
            Case VehicleMod.Ornaments
                If arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("CMM_MOD_S27")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S3")
                End If
                Exit Select
            Case VehicleMod.Dashboard
                cur = Game.GetGXTEntry("CMM_MOD_S4")
                Exit Select
            Case VehicleMod.DialDesign
                cur = Game.GetGXTEntry("CMM_MOD_S5")
                Exit Select
            Case VehicleMod.DoorSpeakers
                cur = Game.GetGXTEntry("CMM_MOD_S6")
                Exit Select
            Case VehicleMod.Seats
                cur = Game.GetGXTEntry("CMM_MOD_S7")
                Exit Select
            Case VehicleMod.SteeringWheels
                cur = Game.GetGXTEntry("CMM_MOD_S8")
                Exit Select
            Case VehicleMod.ColumnShifterLevers
                cur = Game.GetGXTEntry("CMM_MOD_S9")
                Exit Select
            Case VehicleMod.Plaques
                If arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("collision_8itszix").ToLower.UppercaseFirstLetter 'Decorations
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S10")
                    Exit Select
                End If
            Case VehicleMod.Speakers
                If arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("MNU_WBAR") 'Wheelie Bar
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S11")
                End If
                Exit Select
            Case VehicleMod.Trunk
                cur = Game.GetGXTEntry("CMM_MOD_S12")
                Exit Select
            Case VehicleMod.Hydraulics
                cur = Game.GetGXTEntry("CMM_MOD_S13")
                Exit Select
            Case VehicleMod.EngineBlock
                cur = Game.GetGXTEntry("CMM_MOD_S14")
                Exit Select
            Case VehicleMod.AirFilter
                If arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("WT_BOOST")
                Else
                    Select Case veh.Model
                        Case "sultanrs", "elegy"
                            cur = Game.GetGXTEntry("CMM_MOD_S15b")
                        Case Else
                            cur = Game.GetGXTEntry("CMM_MOD_S15")
                    End Select
                End If
                Exit Select
            Case VehicleMod.Struts
                If arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("collision_64bkrs4") 'vertical jump
                Else
                    Select Case veh.Model
                        Case "sultanrs", "banshee2"
                            cur = Game.GetGXTEntry("CMM_MOD_S16b")
                        Case Else
                            cur = Game.GetGXTEntry("CMM_MOD_S16")
                    End Select
                End If
                Exit Select
            Case VehicleMod.ArchCover
                If veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S17b")
                ElseIf arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("collision_h1pzbg").ToLower.UppercaseFirstLetter
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S17")
                End If
                Exit Select
            Case VehicleMod.Aerials
                If veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S18b")
                ElseIf veh.Model = "btype3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S18c")
                ElseIf arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("BLIP_320") 'spikes
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S18")
                End If
                Exit Select
            Case VehicleMod.Trim
                If veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19b")
                ElseIf veh.Model = "btype3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19c")
                ElseIf veh.Model = "virgo2" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19d")
                ElseIf arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("collision_84p91l0").ToLower.UppercaseFirstLetter 'blades
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S19")
                End If
                Exit Select
            Case VehicleMod.Tank
                If veh.Model = "slamvan3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S27")
                ElseIf arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("collision_6w0cd59")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S20")
                End If
                Exit Select
            Case VehicleMod.Windows
                If veh.Model = "btype3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S21b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S21")
                End If
                Exit Select
            Case DirectCast(47, VehicleMod)
                If veh.Model = "slamvan3" Then
                    cur = Game.GetGXTEntry("SLVAN3_RDOOR")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S22")
                End If
                Exit Select
            Case VehicleMod.Livery
                cur = Game.GetGXTEntry("CMM_MOD_S23")
                Exit Select

            'I'm Not MentaL
            Case VehicleMod.Fender
                If veh.ClassType = VehicleClass.Motorcycles Then
                    cur = Game.GetGXTEntry("CMOD_SHIFTER_0")
                Else
                    cur = Game.GetGXTEntry("CMOD_MOD_FEN")
                End If
                Exit Select
            Case VehicleMod.Spoilers
                If veh.ClassType = VehicleClass.Motorcycles Then
                    If veh.Model = "faggio3" Then
                        cur = Game.GetGXTEntry("TOP_ANTENNA")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_BLT")
                    End If
                Else
                    If veh.Model = "btype3" Then
                        cur = Game.GetGXTEntry("BT_SPARE2")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_SPO")
                    End If
                End If
                Exit Select
            Case VehicleMod.Frame
                If veh.ClassType = VehicleClass.Motorcycles Then
                    If arenavehicle.Contains(veh.Model) Then
                        cur = Game.GetGXTEntry("CMOD_ARMPL_N") 'Armor Plating
                    Else
                        cur = Game.GetGXTEntry("CMM_MOD_S14")
                    End If
                ElseIf arenavehicle.Contains(veh.Model) Then
                    cur = Game.GetGXTEntry("CMOD_ARMPL_N") 'Armor Plating
                Else
                    If veh.Model = "sultanrs" Then
                        cur = Game.GetGXTEntry("TOP_CAGE")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_CHA")
                    End If
                End If
                Exit Select
            Case VehicleMod.Exhaust
                cur = Game.GetGXTEntry("CMOD_MOD_MUF")
                Exit Select
            Case VehicleMod.Grille
                Select Case veh.Model
                    Case "avarus"
                        cur = Game.GetGXTEntry("TOP_OIL")
                    Case "zr3802"
                        cur = Game.GetGXTEntry("collision_832uimd") 'rear windshield
                    Case Else
                        cur = Game.GetGXTEntry("CMOD_MOD_GRL")
                End Select

                Exit Select
            Case VehicleMod.Hood
                If veh.ClassType = VehicleClass.Motorcycles Then
                    cur = Game.GetGXTEntry("CMM_MOD_S7")
                Else
                    cur = Game.GetGXTEntry("CMOD_MOD_HOD")
                End If
                Exit Select
            Case VehicleMod.Roof
                If veh.ClassType = VehicleClass.Motorcycles Then
                    If veh.Model = "faggio3" Then
                        cur = Game.GetGXTEntry("TOP_ANTENNAR")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_TNK")
                    End If
                Else
                    If veh.Model = "penetrator" Then
                        cur = Game.GetGXTEntry("CMM_MOD_S43")
                    ElseIf veh.Model = "blazer4" Then
                        cur = Game.GetGXTEntry("CMM_MOD_S17")
                    ElseIf arenavehicle.Contains(veh.Model) Then
                        cur = Game.GetGXTEntry("CMOD_SEWEAP_N")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_ROF")
                    End If
                End If
                Exit Select
            Case Else
                cur = Native.Function.Call(Of String)(Hash.GET_MOD_SLOT_NAME, veh.Handle, modType)
                If DoesGXTEntryExist(cur) Then
                    cur = Game.GetGXTEntry(cur)
                End If
                Exit Select
        End Select
        If cur = "" Then
            'would only happen if the text isnt loaded
            cur = $"*{[Enum].GetName(GetType(VehicleMod), modType)}"
        End If

        Return cur
    End Function

    'Public Function LocalizeModTitleName(title As String) As String
    '    Dim langConf As ScriptSettings = ScriptSettings.Load("scripts\BennysLang-" & Game.Language.ToString & ".ini")
    '    Return langConf.GetValue("TITLE", title, "NULL")
    'End Function

    Enum GroupName
        NeonKits
        NeonLayout
        NeonColor
        Headlights
        Lights
        Bumpers
        Respray
        Extras
        Plate
        License
        Tires
        WheelColor
        WheelType
        Turbo
        Wheels
        Windows
        Upgrade
        Upgrade2
        Door
        Bodyworks
        Interior
        Plates
        Engine
        PrimaryColor
        SecondaryColor
        LightColor
        TertiaryColor
        TrimColor
        AccentColor
        Repair
        Livery
        Weapons
    End Enum

    Public Function LocalizedModGroupName(groupName As GroupName) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String = Nothing
        Select Case groupName
            Case GroupName.NeonKits
                cur = Game.GetGXTEntry("CMOD_MOD_LGT_N")
                Exit Select
            Case GroupName.Headlights
                cur = Game.GetGXTEntry("CMOD_MOD_LGT_H")
                Exit Select
            Case GroupName.Lights
                cur = Game.GetGXTEntry("CMOD_MOD_LGT")
                Exit Select
            Case GroupName.Bumpers
                If veh.Model = "blazer4" Then
                    cur = Game.GetGXTEntry("TOP_MUDFR")
                Else
                    cur = Game.GetGXTEntry("CMOD_MOD_BUM")
                End If
                Exit Select
            Case GroupName.Respray
                cur = Game.GetGXTEntry("CMOD_MOD_COL")
                Exit Select
            Case GroupName.Extras
                cur = Game.GetGXTEntry("CMOD_MOD_GLD2")
                Exit Select
            Case GroupName.Plate
                cur = Game.GetGXTEntry("CMOD_MOD_PLA")
                Exit Select
            Case GroupName.License
                cur = Game.GetGXTEntry("CMOD_MOD_PLA2")
                Exit Select
            Case GroupName.Tires
                cur = Game.GetGXTEntry("CMOD_MOD_TYR")
                Exit Select
            Case GroupName.WheelColor
                cur = Game.GetGXTEntry("CMOD_MOD_WCL")
                Exit Select
            Case GroupName.Turbo
                cur = Game.GetGXTEntry("CMOD_MOD_TUR")
                Exit Select
            Case GroupName.Wheels
                cur = Game.GetGXTEntry("CMOD_MOD_WHEM")
                Exit Select
            Case GroupName.Windows
                cur = Game.GetGXTEntry("CMOD_MOD_WIN")
                Exit Select
            Case GroupName.Upgrade
                cur = Game.GetGXTEntry("CMM_MOD_LOW")
                Exit Select
            Case GroupName.Upgrade2
                cur = Game.GetGXTEntry("collision_85z9vzf")
            Case GroupName.Door
                cur = Game.GetGXTEntry("CMM_MOD_S21")
                Exit Select
            Case GroupName.NeonLayout
                cur = Game.GetGXTEntry("CMOD_NEON_0")
                Exit Select
            Case GroupName.NeonColor
                cur = Game.GetGXTEntry("CMOD_NEON_1")
                Exit Select
            Case GroupName.Bodyworks
                cur = Game.GetGXTEntry("CMM_MOD_BODY_W")
                Exit Select
            Case GroupName.Interior
                cur = Game.GetGXTEntry("CMM_MOD_G1")
                Exit Select
            Case GroupName.Plates
                cur = Game.GetGXTEntry("CMM_MOD_G2")
                Exit Select
            Case GroupName.Engine
                cur = Game.GetGXTEntry("CMM_MOD_G3")
                Exit Select
            Case GroupName.PrimaryColor
                cur = Game.GetGXTEntry("CMOD_COL0_0")
                Exit Select
            Case GroupName.SecondaryColor
                cur = Game.GetGXTEntry("CMOD_COL0_1")
                Exit Select
            Case GroupName.LightColor
                cur = Game.GetGXTEntry("CMM_MOD_S26")
                Exit Select
            Case GroupName.Repair
                cur = Game.GetGXTEntry("CMOD_MOD_MNT")
                Exit Select
            Case GroupName.TertiaryColor
                cur = Game.GetGXTEntry("CMOD_COL0_5")
                Exit Select
            Case GroupName.TrimColor
                cur = Game.GetGXTEntry("CMOD_MOD_TRIM2")
                Exit Select
            Case GroupName.AccentColor
                cur = Game.GetGXTEntry("CMOD_MOD_TRIM3")
                Exit Select
            Case GroupName.WheelType
                cur = Game.GetGXTEntry("CMOD_MOD_WHE")
                Exit Select
            Case GroupName.Livery
                cur = Game.GetGXTEntry("CMM_MOD_S23")
                Exit Select
            Case GroupName.Weapons
                cur = Game.GetGXTEntry("PM_INF_WEPT")
                Exit Select
        End Select

        Return cur
    End Function

    Enum ColorType
        Chrome
        Classic
        Metallic
        Metals
        Matte
        Pearlescent
        Crew
    End Enum

    Public Function LocalizedColorGroupName(colorTypeName As ColorType) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String = Nothing
        Select Case colorTypeName
            Case ColorType.Chrome
                cur = Game.GetGXTEntry("CMOD_COL1_0")
                Exit Select
            Case ColorType.Classic
                cur = Game.GetGXTEntry("CMOD_COL1_1")
                Exit Select
            Case ColorType.Crew
                cur = Game.GetGXTEntry("CMOD_COL1_2")
                Exit Select
            Case ColorType.Metallic
                cur = Game.GetGXTEntry("CMOD_COL1_3")
                Exit Select
            Case ColorType.Metals
                cur = Game.GetGXTEntry("CMOD_COL1_4")
                Exit Select
            Case ColorType.Matte
                cur = Game.GetGXTEntry("CMOD_COL1_5")
                Exit Select
            Case ColorType.Pearlescent
                cur = Game.GetGXTEntry("CMOD_COL1_6")
                Exit Select
        End Select
        Return cur
    End Function

    Public Function LocalizedModTypeName(toggleModType As VehicleToggleMod, Optional stock As Boolean = False) As String
        Dim result As String = Nothing
        If stock = True Then
            result = Game.GetGXTEntry("CMOD_ARM_0")
        Else
            'result = Native.Function.Call(Of String)(Hash.GET_MOD_SLOT_NAME, veh.Handle, toggleModType)
            Select Case toggleModType
                Case VehicleToggleMod.Turbo
                    result = Game.GetGXTEntry("CMOD_MOD_TUR")
                    Exit Select
                Case VehicleToggleMod.XenonHeadlights
                    result = Game.GetGXTEntry("CMOD_LGT_1")
                    Exit Select
                Case VehicleToggleMod.TireSmoke
                    result = Game.GetGXTEntry("CMOD_MOD_TYR3")
                    Exit Select
            End Select
            If result = "" Then
                'would only happen if the text isnt loaded
                result = [Enum].GetName(GetType(VehicleToggleMod), toggleModType)
            End If
        End If
        Return result
    End Function

    Public Function DoesGXTEntryExist(entry As String) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.DOES_TEXT_LABEL_EXIST, entry)
    End Function

    Public Function GetLocalizedModName(index As Integer, modCount As Integer, modType As VehicleMod) As String
        'this still needs a little more work, but its better than what it used to be
        If modCount = 0 Then
            Return ""
        End If
        If index < -1 OrElse index >= modCount Then
            Return ""
        End If
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String
        If modType = VehicleMod.Horns Then
            If _hornNames.ContainsKey(index) Then
                If DoesGXTEntryExist(_hornNames(index).Item1) Then
                    Return Game.GetGXTEntry(_hornNames(index).Item1)
                End If
                Return _hornNames(index).Item2
            End If
            Return ""
        End If
        If modType = VehicleMod.FrontWheels OrElse modType = VehicleMod.BackWheels Then
            If index = -1 Then
                If Not veh.Model.IsBike AndAlso veh.Model.IsBicycle Then
                    Return Game.GetGXTEntry("CMOD_WHE_0")
                Else
                    Return Game.GetGXTEntry("CMOD_WHE_B_0")
                End If
            End If

            Return Game.GetGXTEntry(Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, veh.Handle, modType, index))
        End If

        Select Case modType
            Case VehicleMod.Armor
                Return Game.GetGXTEntry("CMOD_ARM_" + (index + 1).ToString())
            Case VehicleMod.Brakes
                Return Game.GetGXTEntry("CMOD_BRA_" + (index + 1).ToString())
            Case VehicleMod.Engine
                If index = -1 Then
                    'Engine doesn't list anything in LSC for no parts, but there is a setting with no part. so just use armours none
                    Return Game.GetGXTEntry("CMOD_ARM_0")
                End If
                Return Game.GetGXTEntry("CMOD_ENG_" + (index + 2).ToString())
            Case VehicleMod.Suspension
                Return Game.GetGXTEntry("CMOD_SUS_" + (index + 1).ToString())
            Case VehicleMod.Transmission
                Return Game.GetGXTEntry("CMOD_GBX_" + (index + 1).ToString())
        End Select
        If index > -1 Then
            cur = Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, veh.Handle, modType, index)
            If DoesGXTEntryExist(cur) Then
                cur = Game.GetGXTEntry(cur)
                If cur = "" OrElse cur = "NULL" Then
                    Return LocalizedModTypeName(modType) + " " + (index + 1).ToString()
                End If
                Return cur
            End If
            Return LocalizedModTypeName(modType) + " " + (index + 1).ToString()
        Else
            Select Case modType
                Case VehicleMod.AirFilter
                    If veh.Model = VehicleHash.Tornado Then
                    End If
                    Exit Select
                Case VehicleMod.Struts
                    Select Case veh.Model
                        Case VehicleHash.Banshee, VehicleHash.Banshee2, VehicleHash.SultanRS
                            Return Game.GetGXTEntry("CMOD_COL5_41")
                    End Select
                    Exit Select

            End Select
            Return Game.GetGXTEntry("CMOD_DEF_0")
        End If
    End Function

    Public Function LocalizedLicensePlate(plateType As GTA.NumberPlateType) As String
        Dim result As String = Nothing

        Select Case plateType
            Case NumberPlateType.BlueOnWhite1
                result = Game.GetGXTEntry("CMOD_PLA_0")
                Exit Select
            Case NumberPlateType.BlueOnWhite2
                result = Game.GetGXTEntry("CMOD_PLA_1")
                Exit Select
            Case NumberPlateType.BlueOnWhite3
                result = Game.GetGXTEntry("CMOD_PLA_2")
                Exit Select
            Case NumberPlateType.NorthYankton
                result = Game.GetGXTEntry("CMOD_MOD_GLD2")
                Exit Select
            Case NumberPlateType.YellowOnBlack
                result = Game.GetGXTEntry("CMOD_PLA_4")
                Exit Select
            Case NumberPlateType.YellowOnBlue
                result = Game.GetGXTEntry("CMOD_PLA_3")
                Exit Select
        End Select

        Return result
    End Function

    Public Function LocalizedT5RoofName(roofID As Integer) As String
        Return Game.GetGXTEntry("T5_ROOF" & roofID)
    End Function

    Public Function LocalizedWindowsTint(tint As GTA.VehicleWindowTint) As String
        Dim result As String = Nothing

        Select Case tint
            Case VehicleWindowTint.DarkSmoke
                result = Game.GetGXTEntry("CMOD_WIN_2")
                Exit Select
            Case VehicleWindowTint.Green
                result = Game.GetGXTEntry("GREEN")
                Exit Select
            Case VehicleWindowTint.LightSmoke
                result = Game.GetGXTEntry("CMOD_WIN_1")
                Exit Select
            Case VehicleWindowTint.Limo
                result = Game.GetGXTEntry("CMOD_WIN_3")
                Exit Select
            Case VehicleWindowTint.None
                result = Game.GetGXTEntry("CMOD_WIN_0")
                Exit Select
            Case VehicleWindowTint.PureBlack
                result = Game.GetGXTEntry("CMOD_WIN_5")
                Exit Select
            Case VehicleWindowTint.Stock
                result = Game.GetGXTEntry("CMOD_WIN_4")
                Exit Select
        End Select

        Return result
    End Function

    Public Function GetLocalizedWheelTypeName(wheelType As VehicleWheelType) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        If _wheelNames.ContainsKey(wheelType) Then
            If DoesGXTEntryExist(_wheelNames(wheelType).Item1) Then
                Return Game.GetGXTEntry(_wheelNames(wheelType).Item1)
            End If
            Return _wheelNames(wheelType).Item2
        End If
        Throw New ArgumentException("Wheel Type Is undefined", "wheelType")
    End Function

    Public Function GetLocalizedColorName(vehColor As VehicleColor) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        If _colorNames.ContainsKey(vehColor) Then
            If DoesGXTEntryExist(_colorNames(vehColor).Item1) Then
                Return Game.GetGXTEntry(_colorNames(vehColor).Item1)
            End If
            Return Trim(RegularExpressions.Regex.Replace(_colorNames(vehColor).Item2, "[A-Z]", " ${0}"))
        End If
        Throw New ArgumentException("Vehicle Color Is undefined", "Vehicle Color")
    End Function

    Public ClassicColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89,
        91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112}
    Public MatteColor As List(Of VehicleColor) = New List(Of VehicleColor) From {12, 13, 14, 131, 83, 82, 84, 149, 148, 39, 40, 41, 42, 55, 128, 151, 155, 152, 153, 154}
    Public MetalColor As List(Of VehicleColor) = New List(Of VehicleColor) From {117, 118, 119, 158, 159, 160}
    Public ChromeColor As List(Of VehicleColor) = New List(Of VehicleColor) From {120}
    Public PearlescentColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89, 91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112, 117, 118, 119, 158, 159, 160}

    Private ReadOnly _colorNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
    {0, New Tuple(Of String, String)("BLACK", "MetallicBlack")},
    {1, New Tuple(Of String, String)("GRAPHITE", "MetallicGraphiteBlack")},
    {2, New Tuple(Of String, String)("BLACK_STEEL", "MetallicBlackSteel")},
    {3, New Tuple(Of String, String)("DARK_SILVER", "MetallicDarkSilver")},
    {4, New Tuple(Of String, String)("SILVER", "MetallicSilver")},
    {5, New Tuple(Of String, String)("BLUE_SILVER", "MetallicBlueSilver")},
    {6, New Tuple(Of String, String)("ROLLED_STEEL", "MetallicSteelGray")},
    {7, New Tuple(Of String, String)("SHADOW_SILVER", "MetallicShadowSilver")},
    {8, New Tuple(Of String, String)("STONE_SILVER", "MetallicStoneSilver")},
    {9, New Tuple(Of String, String)("MIDNIGHT_SILVER", "MetallicMidnightSilver")},
    {10, New Tuple(Of String, String)("CAST_IRON_SIL", "MetallicGunMetal")},
    {11, New Tuple(Of String, String)("ANTHR_BLACK", "MetallicAnthraciteGray")},
    {12, New Tuple(Of String, String)("BLACK", "MatteBlack")},
    {13, New Tuple(Of String, String)("GREY", "MatteGray")},
    {14, New Tuple(Of String, String)("LIGHT_GREY", "MatteLightGray")},
    {15, New Tuple(Of String, String)("BLACK", "UtilBlack")},
    {16, New Tuple(Of String, String)("BLACK", "UtilBlackPoly")},
    {17, New Tuple(Of String, String)("DARK_SILVER", "UtilDarksilver")},
    {18, New Tuple(Of String, String)("SILVER", "UtilSilver")},
    {19, New Tuple(Of String, String)("CAST_IRON_SIL", "UtilGunMetal")},
    {20, New Tuple(Of String, String)("SHADOW_SILVER", "UtilShadowSilver")},
    {21, New Tuple(Of String, String)("BLACK", "WornBlack")},
    {22, New Tuple(Of String, String)("GRAPHITE", "WornGraphite")},
    {23, New Tuple(Of String, String)("ROLLED_STEEL", "WornSilverGray")},
    {24, New Tuple(Of String, String)("SILVER", "WornSilver")},
    {25, New Tuple(Of String, String)("BLUE_SILVER", "WornBlueSilver")},
    {26, New Tuple(Of String, String)("SHADOW_SILVER", "WornShadowSilver")},
    {27, New Tuple(Of String, String)("RED", "MetallicRed")},
    {28, New Tuple(Of String, String)("TORINO_RED", "MetallicTorinoRed")},
    {29, New Tuple(Of String, String)("FORMULA_RED", "MetallicFormulaRed")},
    {30, New Tuple(Of String, String)("BLAZE_RED", "MetallicBlazeRed")},
    {31, New Tuple(Of String, String)("GRACE_RED", "MetallicGracefulRed")},
    {32, New Tuple(Of String, String)("GARNET_RED", "MetallicGarnetRed")},
    {33, New Tuple(Of String, String)("SUNSET_RED", "MetallicDesertRed")},
    {34, New Tuple(Of String, String)("CABERNET_RED", "MetallicCabernetRed")},
    {35, New Tuple(Of String, String)("CANDY_RED", "MetallicCandyRed")},
    {36, New Tuple(Of String, String)("SUNRISE_ORANGE", "MetallicSunriseOrange")},
    {37, New Tuple(Of String, String)("GOLD", "MetallicClassicGold")},
    {38, New Tuple(Of String, String)("ORANGE", "MetallicOrange")},
    {39, New Tuple(Of String, String)("RED", "MatteRed")},
    {40, New Tuple(Of String, String)("DARK_RED", "MatteDarkRed")},
    {41, New Tuple(Of String, String)("ORANGE", "MatteOrange")},
    {42, New Tuple(Of String, String)("YELLOW", "MatteYellow")},
    {43, New Tuple(Of String, String)("RED", "UtilRed")},
    {44, New Tuple(Of String, String)("NULL", "UtilBrightRed")},
    {45, New Tuple(Of String, String)("GARNET_RED", "UtilGarnetRed")},
    {46, New Tuple(Of String, String)("RED", "WornRed")},
    {47, New Tuple(Of String, String)("NULL", "WornGoldenRed")},
    {48, New Tuple(Of String, String)("DARK_RED", "WornDarkRed")},
    {49, New Tuple(Of String, String)("DARK_GREEN", "MetallicDarkGreen")},
    {50, New Tuple(Of String, String)("RACING_GREEN", "MetallicRacingGreen")},
    {51, New Tuple(Of String, String)("SEA_GREEN", "MetallicSeaGreen")},
    {52, New Tuple(Of String, String)("OLIVE_GREEN", "MetallicOliveGreen")},
    {53, New Tuple(Of String, String)("BRIGHT_GREEN", "MetallicGreen")},
    {54, New Tuple(Of String, String)("PETROL_GREEN", "MetallicGasolineBlueGreen")},
    {55, New Tuple(Of String, String)("LIME_GREEN", "MatteLimeGreen")},
    {56, New Tuple(Of String, String)("DARK_GREEN", "UtilDarkGreen")},
    {57, New Tuple(Of String, String)("GREEN", "UtilGreen")},
    {58, New Tuple(Of String, String)("DARK_GREEN", "WornDarkGreen")},
    {59, New Tuple(Of String, String)("GREEN", "WornGreen")},
    {60, New Tuple(Of String, String)("NULL", "WornSeaWash")},
    {61, New Tuple(Of String, String)("GALAXY_BLUE", "MetallicMidnightBlue")},
    {62, New Tuple(Of String, String)("DARK_BLUE", "MetallicDarkBlue")},
    {63, New Tuple(Of String, String)("SAXON_BLUE", "MetallicSaxonyBlue")},
    {64, New Tuple(Of String, String)("BLUE", "MetallicBlue")},
    {65, New Tuple(Of String, String)("MARINER_BLUE", "MetallicMarinerBlue")},
    {66, New Tuple(Of String, String)("HARBOR_BLUE", "MetallicHarborBlue")},
    {67, New Tuple(Of String, String)("DIAMOND_BLUE", "MetallicDiamondBlue")},
    {68, New Tuple(Of String, String)("SURF_BLUE", "MetallicSurfBlue")},
    {69, New Tuple(Of String, String)("NAUTICAL_BLUE", "MetallicNauticalBlue")},
    {70, New Tuple(Of String, String)("ULTRA_BLUE", "MetallicBrightBlue")},
    {71, New Tuple(Of String, String)("PURPLE", "MetallicPurpleBlue")},
    {72, New Tuple(Of String, String)("SPIN_PURPLE", "MetallicSpinnakerBlue")},
    {73, New Tuple(Of String, String)("RACING_BLUE", "MetallicUltraBlue")},
    {74, New Tuple(Of String, String)("LIGHT_BLUE", "MetallicLightBlue")},
    {75, New Tuple(Of String, String)("DARK_BLUE", "UtilDarkBlue")},
    {76, New Tuple(Of String, String)("MIDNIGHT_BLUE", "UtilMidnightBlue")},
    {77, New Tuple(Of String, String)("BLUE", "UtilBlue")},
    {78, New Tuple(Of String, String)("NULL", "UtilSeaFoamBlue")},
    {79, New Tuple(Of String, String)("LIGHT_BLUE", "UtilLightningBlue")},
    {80, New Tuple(Of String, String)("NULL", "UtilMauiBluePoly")},
    {81, New Tuple(Of String, String)("NULL", "UtilBrightBlue")},
    {82, New Tuple(Of String, String)("DARK_BLUE", "MatteDarkBlue")},
    {83, New Tuple(Of String, String)("BLUE", "MatteBlue")},
    {84, New Tuple(Of String, String)("MIDNIGHT_BLUE", "MatteMidnightBlue")},
    {85, New Tuple(Of String, String)("DARK_BLUE", "WornDarkBlue")},
    {86, New Tuple(Of String, String)("BLUE", "WornBlue")},
    {87, New Tuple(Of String, String)("LIGHT_BLUE", "WornLightBlue")},
    {88, New Tuple(Of String, String)("YELLOW", "MetallicTaxiYellow")},
    {89, New Tuple(Of String, String)("RACE_YELLOW", "MetallicRaceYellow")},
    {90, New Tuple(Of String, String)("BRONZE", "MetallicBronze")},
    {91, New Tuple(Of String, String)("FLUR_YELLOW", "MetallicYellowBird")},
    {92, New Tuple(Of String, String)("LIME_GREEN", "MetallicLime")},
    {93, New Tuple(Of String, String)("NULL", "MetallicChampagne")},
    {94, New Tuple(Of String, String)("UMBER_BROWN", "MetallicPuebloBeige")},
    {95, New Tuple(Of String, String)("CREEK_BROWN", "MetallicDarkIvory")},
    {96, New Tuple(Of String, String)("CHOCOLATE_BROWN", "MetallicChocoBrown")},
    {97, New Tuple(Of String, String)("MAPLE_BROWN", "MetallicGoldenBrown")},
    {98, New Tuple(Of String, String)("SADDLE_BROWN", "MetallicLightBrown")},
    {99, New Tuple(Of String, String)("STRAW_BROWN", "MetallicStrawBeige")},
    {100, New Tuple(Of String, String)("MOSS_BROWN", "MetallicMossBrown")},
    {101, New Tuple(Of String, String)("BISON_BROWN", "MetallicBistonBrown")},
    {102, New Tuple(Of String, String)("WOODBEECH_BROWN", "MetallicBeechwood")},
    {103, New Tuple(Of String, String)("NULL", "MetallicDarkBeechwood")},
    {104, New Tuple(Of String, String)("SIENNA_BROWN", "MetallicChocoOrange")},
    {105, New Tuple(Of String, String)("SANDY_BROWN", "MetallicBeachSand")},
    {106, New Tuple(Of String, String)("BLEECHED_BROWN", "MetallicSunBleechedSand")},
    {107, New Tuple(Of String, String)("CREAM", "MetallicCream")},
    {108, New Tuple(Of String, String)("BROWN", "UtilBrown")},
    {109, New Tuple(Of String, String)("NULL", "UtilMediumBrown")},
    {110, New Tuple(Of String, String)("NULL", "UtilLightBrown")},
    {111, New Tuple(Of String, String)("WHITE", "MetallicWhite")},
    {112, New Tuple(Of String, String)("FROST_WHITE", "MetallicFrostWhite")},
    {113, New Tuple(Of String, String)("NULL", "WornHoneyBeige")},
    {114, New Tuple(Of String, String)("BROWN", "WornBrown")},
    {115, New Tuple(Of String, String)("DARK_BROWN", "WornDarkBrown")},
    {116, New Tuple(Of String, String)("STRAW_BROWN", "WornStrawBeige")},
    {117, New Tuple(Of String, String)("BR_STEEL", "BrushedSteel")},
    {118, New Tuple(Of String, String)("BR BLACK_STEEL", "BrushedBlackSteel")},
    {119, New Tuple(Of String, String)("BR_ALUMINIUM", "BrushedAluminium")},
    {120, New Tuple(Of String, String)("CHROME", "Chrome")},
    {121, New Tuple(Of String, String)("NULL", "WornOffWhite")},
    {122, New Tuple(Of String, String)("NULL", "UtilOffWhite")},
    {123, New Tuple(Of String, String)("ORANGE", "WornOrange")},
    {124, New Tuple(Of String, String)("NULL", "WornLightOrange")},
    {125, New Tuple(Of String, String)("NULL", "MetallicSecuricorGreen")},
    {126, New Tuple(Of String, String)("YELLOW", "WornTaxiYellow")},
    {127, New Tuple(Of String, String)("NULL", "PoliceCarBlue")},
    {128, New Tuple(Of String, String)("GREEN", "MatteGreen")},
    {129, New Tuple(Of String, String)("BROWN", "MatteBrown")},
    {130, New Tuple(Of String, String)("NULL", "SteelBlue")},
    {131, New Tuple(Of String, String)("WHITE", "MatteWhite")},
    {132, New Tuple(Of String, String)("WHITE", "WornWhite")},
    {133, New Tuple(Of String, String)("OLIVE_GREEN", "WornOliveArmyGreen")},
    {134, New Tuple(Of String, String)("WHITE", "PureWhite")},
    {135, New Tuple(Of String, String)("HOT PINK", "HotPink")},
    {136, New Tuple(Of String, String)("SALMON_PINK", "Salmonpink")},
    {137, New Tuple(Of String, String)("PINK", "MetallicVermillionPink")},
    {138, New Tuple(Of String, String)("BRIGHT_ORANGE", "Orange")},
    {139, New Tuple(Of String, String)("GREEN", "Green")},
    {140, New Tuple(Of String, String)("BLUE", "Blue")},
    {141, New Tuple(Of String, String)("MIDNIGHT_BLUE", "MettalicBlackBlue")},
    {142, New Tuple(Of String, String)("MIGHT_PURPLE", "MetallicBlackPurple")},
    {143, New Tuple(Of String, String)("WINE_RED", "MetallicBlackRed")},
    {144, New Tuple(Of String, String)("NULL", "HunterGreen")},
    {145, New Tuple(Of String, String)("BRIGHT_PURPLE", "MetallicPurple")},
    {146, New Tuple(Of String, String)("MIGHT_PURPLE", "MetaillicVDarkBlue")},
    {147, New Tuple(Of String, String)("BLACK_GRAPHITE", "ModshopBlack1")},
    {148, New Tuple(Of String, String)("PURPLE", "MattePurple")},
    {149, New Tuple(Of String, String)("MIGHT_PURPLE", "MatteDarkPurple")},
    {150, New Tuple(Of String, String)("LAVA_RED", "MetallicLavaRed")},
    {151, New Tuple(Of String, String)("MATTE_FOR", "MatteForestGreen")},
    {152, New Tuple(Of String, String)("MATTE_OD", "MatteOliveDrab")},
    {153, New Tuple(Of String, String)("MATTE_DIRT", "MatteDesertBrown")},
    {154, New Tuple(Of String, String)("MATTE_DESERT", "MatteDesertTan")},
    {155, New Tuple(Of String, String)("MATTE_FOIL", "MatteFoliageGreen")},
    {156, New Tuple(Of String, String)("NULL", "DefaultAlloyColor")},
    {157, New Tuple(Of String, String)("NULL", "EpsilonBlue")},
    {158, New Tuple(Of String, String)("GOLD_P", "PureGold")},
    {159, New Tuple(Of String, String)("GOLD_S", "BrushedGold")},
    {160, New Tuple(Of String, String)("NULL", "SecretGold")}
    })

    Private ReadOnly _hornNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
    {-1, New Tuple(Of String, String)("CMOD_HRN_0", "Stock Horn")},
    {0, New Tuple(Of String, String)("CMOD_HRN_TRK", "Truck Horn")},
    {1, New Tuple(Of String, String)("CMOD_HRN_COP", "Cop Horn")},
    {2, New Tuple(Of String, String)("CMOD_HRN_CLO", "Clown Horn")},
    {3, New Tuple(Of String, String)("CMOD_HRN_MUS1", "Musical Horn 1")},
    {4, New Tuple(Of String, String)("CMOD_HRN_MUS2", "Musical Horn 2")},
    {5, New Tuple(Of String, String)("CMOD_HRN_MUS3", "Musical Horn 3")},
    {6, New Tuple(Of String, String)("CMOD_HRN_MUS4", "Musical Horn 4")},
    {7, New Tuple(Of String, String)("CMOD_HRN_MUS5", "Musical Horn 5")},
    {8, New Tuple(Of String, String)("CMOD_HRN_SAD", "Sad Trombone")},
    {9, New Tuple(Of String, String)("HORN_CLAS1", "Classical Horn 1")},
    {10, New Tuple(Of String, String)("HORN_CLAS2", "Classical Horn 2")},
    {11, New Tuple(Of String, String)("HORN_CLAS3", "Classical Horn 3")},
    {12, New Tuple(Of String, String)("HORN_CLAS4", "Classical Horn 4")},
    {13, New Tuple(Of String, String)("HORN_CLAS5", "Classical Horn 5")},
    {14, New Tuple(Of String, String)("HORN_CLAS6", "Classical Horn 6")},
    {15, New Tuple(Of String, String)("HORN_CLAS7", "Classical Horn 7")},
    {16, New Tuple(Of String, String)("HORN_CNOTE_C0", "Scale Do")},
    {17, New Tuple(Of String, String)("HORN_CNOTE_D0", "Scale Re")},
    {18, New Tuple(Of String, String)("HORN_CNOTE_E0", "Scale Mi")},
    {19, New Tuple(Of String, String)("HORN_CNOTE_F0", "Scale Fa")},
    {20, New Tuple(Of String, String)("HORN_CNOTE_G0", "Scale Sol")},
    {21, New Tuple(Of String, String)("HORN_CNOTE_A0", "Scale La")},
    {22, New Tuple(Of String, String)("HORN_CNOTE_B0", "Scale Ti")},
    {23, New Tuple(Of String, String)("HORN_CNOTE_C1", "Scale Do (High)")},
    {24, New Tuple(Of String, String)("HORN_HIPS1", "Jazz Horn 1")},
    {25, New Tuple(Of String, String)("HORN_HIPS2", "Jazz Horn 2")},
    {26, New Tuple(Of String, String)("HORN_HIPS3", "Jazz Horn 3")},
    {27, New Tuple(Of String, String)("HORN_HIPS4", "Jazz Horn Loop")},
    {28, New Tuple(Of String, String)("HORN_INDI_1", "Star Spangled Banner 1")},
    {29, New Tuple(Of String, String)("HORN_INDI_2", "Star Spangled Banner 2")},
    {30, New Tuple(Of String, String)("HORN_INDI_3", "Star Spangled Banner 3")},
    {31, New Tuple(Of String, String)("HORN_INDI_4", "Star Spangled Banner 4")},
    {32, New Tuple(Of String, String)("HORN_LUXE2", "Classical Horn Loop 1")},
    {33, New Tuple(Of String, String)("HORN_LUXE1", "Classical Horn 8")},
    {34, New Tuple(Of String, String)("HORN_LUXE3", "Classical Horn Loop 2")},
    {35, New Tuple(Of String, String)("HORN_LUXE2", "Classical Horn Loop 1")},
    {36, New Tuple(Of String, String)("HORN_LUXE1", "Classical Horn 8")},
    {37, New Tuple(Of String, String)("HORN_LUXE3", "Classical Horn Loop 2")},
    {38, New Tuple(Of String, String)("HORN_HWEEN1", "Halloween Loop 1")},
    {39, New Tuple(Of String, String)("HORN_HWEEN1", "Halloween Loop 1")},
    {40, New Tuple(Of String, String)("HORN_HWEEN2", "Halloween Loop 2")},
    {41, New Tuple(Of String, String)("HORN_HWEEN2", "Halloween Loop 2")},
    {42, New Tuple(Of String, String)("HORN_LOWRDER1", "San Andreas Loop")},
    {43, New Tuple(Of String, String)("HORN_LOWRDER1", "San Andreas Loop")},
    {44, New Tuple(Of String, String)("HORN_LOWRDER2", "Liberty City Loop")},
    {45, New Tuple(Of String, String)("HORN_LOWRDER2", "Liberty City Loop")},
    {46, New Tuple(Of String, String)("HORN_XM15_1", "Festive Loop 1")},
    {47, New Tuple(Of String, String)("HORN_XM15_1", "Festive Loop 1")},
    {48, New Tuple(Of String, String)("HORN_XM15_2", "Festive Loop 2")},
    {49, New Tuple(Of String, String)("HORN_XM15_2", "Festive Loop 2")},
    {50, New Tuple(Of String, String)("HORN_XM15_3", "Festive Loop 3")},
    {51, New Tuple(Of String, String)("HORN_XM15_3", "Festive Loop 3")},
    {52, New Tuple(Of String, String)("CMOD_AIRHORN_01", "Airhorn 1")},
    {53, New Tuple(Of String, String)("CMOD_AIRHORN_01", "Airhorn 1")},
    {54, New Tuple(Of String, String)("CMOD_AIRHORN_02", "Airhorn 2")},
    {55, New Tuple(Of String, String)("CMOD_AIRHORN_02", "Airhorn 2")},
    {56, New Tuple(Of String, String)("CMOD_AIRHORN_03", "Airhorn 3")},
    {57, New Tuple(Of String, String)("CMOD_AIRHORN_03", "Airhorn 3")}
})

    Private ReadOnly _wheelNames As New Dictionary(Of VehicleWheelType, Tuple(Of String, String))(New Dictionary(Of VehicleWheelType, Tuple(Of String, String))() From {
    {VehicleWheelType.BikeWheels, New Tuple(Of String, String)("CMOD_WHE1_0", "Bike")},
    {VehicleWheelType.HighEnd, New Tuple(Of String, String)("CMOD_WHE1_1", "High End")},
    {VehicleWheelType.Lowrider, New Tuple(Of String, String)("CMOD_WHE1_2", "Lowrider")},
    {VehicleWheelType.Muscle, New Tuple(Of String, String)("CMOD_WHE1_3", "Muscle")},
    {VehicleWheelType.Offroad, New Tuple(Of String, String)("CMOD_WHE1_4", "Offroad")},
    {VehicleWheelType.Sport, New Tuple(Of String, String)("CMOD_WHE1_5", "Sport")},
    {VehicleWheelType.SUV, New Tuple(Of String, String)("CMOD_WHE1_6", "SUV")},
    {VehicleWheelType.Tuner, New Tuple(Of String, String)("CMOD_WHE1_7", "Tuner")},
    {8, New Tuple(Of String, String)("CMOD_WHE1_8", "Benny's Originals")},
    {9, New Tuple(Of String, String)("CMOD_WHE1_9", "Benny's Bespoke")},
    {10, New Tuple(Of String, String)("CMOD_WHE1_10", "Racing")},
    {11, New Tuple(Of String, String)("CMOD_WHE1_11", "Street")}
})

    Public Function IsCustomWheels() As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_MOD_VARIATION, veh, VehicleMod.FrontWheels)
    End Function

    Enum EnumTypes
        NumberPlateType
        VehicleColorPrimary
        VehicleColorSecondary
        VehicleColorTrim
        VehicleColorDashboard
        VehicleColorRim
        VehicleColorAccent
        vehicleColorPearlescent
        VehicleWindowTint
    End Enum

    Enum NeonLayouts
        None
        Sides = 3
        Front
        FrontAndSides = 7
        Back
        BackAndSides = 11
        FrontAndBack
        FrontBackAndSides = 15
    End Enum

    Public Function NeonLayout() As NeonLayouts
        Dim v As Vehicle = veh
        Dim back As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Back)
        Dim front As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Front)
        Dim left As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Left)
        Dim right As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Right)
        Dim result As Integer = -1

        If Not back AndAlso Not front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.None
        ElseIf Not back AndAlso front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.Front
        ElseIf back AndAlso Not front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.Back
        ElseIf Not back AndAlso Not front AndAlso left AndAlso right Then
            result = NeonLayouts.Sides
        ElseIf back AndAlso front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.FrontAndBack
        ElseIf Not back AndAlso front AndAlso left AndAlso right Then
            result = NeonLayouts.FrontAndSides
        ElseIf back AndAlso Not front AndAlso left AndAlso right Then
            result = NeonLayouts.BackAndSides
        ElseIf back AndAlso front AndAlso left AndAlso right Then
            result = NeonLayouts.FrontBackAndSides
        End If

        Return result
    End Function

    Public Function GetClassDisplayName(vehicleClass As VehicleClass) As String
        Return Game.GetGXTEntry("VEH_CLASS_" + CInt(vehicleClass).ToString())
    End Function

    <Extension()>
    Public Function IsUpgradeModExist(vehDispName As String) As Boolean
        Dim result As Boolean = False
        Dim config As ScriptSettings = ScriptSettings.Load("scripts\BennysOriginalMotorWorks.ini")
        Dim v As String = config.GetValue(Of String)("UPGRADE", vehDispName.ToString.ToLower & "_Model", Nothing)
        If v = Nothing Then
            result = False
        Else
            result = True
        End If
        Return result
    End Function

    <Extension()>
    Public Function GetUpgradeModVehicleInfo(vehDispName As String) As Tuple(Of String, Integer)
        Dim config As ScriptSettings = ScriptSettings.Load("scripts\BennysOriginalMotorWorks.ini")
        Dim newModel As String = config.GetValue(Of String)("UPGRADE", vehDispName.ToString.ToLower & "_Model", Nothing)
        Dim newPrice As Integer = config.GetValue(Of Integer)("UPGRADE", vehDispName.ToString.ToLower & "_Price", 0)
        Return New Tuple(Of String, Integer)(newModel, newPrice)
    End Function

    <Extension()>
    Public Function GetRepairPrice(vehicle As Vehicle) As Integer
        Dim price As Integer = System.Math.Round(vehicle.MaxHealth - vehicle.Health) * 10
        If price = 0 Then price = 1
        Return price
    End Function

    <Extension()>
    Public Function GetUpgradePrice(vehicleModel As Model) As Integer
        Dim result As Integer = 0
        Select Case vehicleModel
            Case "banshee"
                result = 565000
            Case "buccaneer"
                result = 390000
            Case "chino"
                result = 180000
            Case "diablous"
                result = 245000
            Case "comet2"
                result = 645000
            Case "faction"
                result = 335000
            Case "faction2"
                result = 695000
            Case "fcr"
                result = 196000
            Case "italigtb"
                result = 495000
            Case "minivan"
                result = 330000
            Case "moonbeam"
                result = 370000
            Case "nero"
                result = 605000
            Case "primo"
                result = 400000
            Case "sabregt"
                result = 490000
            Case "slamvan", "slamvan2"
                result = 394250
            Case "specter"
                result = 252000
            Case "sultan"
                result = 795000
            Case "tornado", "tornado2", "tornado3"
                result = 375000
            Case "virgo3"
                result = 240000
            Case "voodoo2"
                result = 420000
            Case "elegy2"
                result = 904000
            Case "technical"
                result = 142500
            Case "insurgent"
                result = 202500
            Case "youga2"
                result = 1288000
            Case "yosemite"
                result = 700000
            Case "peyote"
                result = 620000
            Case "manana"
                result = 925000
            Case "glendale"
                result = 520000
            Case "gauntlet3"
                result = 815000
            Case Else
                result = 0
        End Select
        Return result
    End Function

    Public Sub PlaySpeech(speechName As String)
        If speechName = "" Then speechName = "LR_UPGRADE_GENERIC"
        Native.Function.Call(Hash._PLAY_AMBIENT_SPEECH_WITH_VOICE, bennyPed, speechName, "BENNY", "SPEECH_PARAMS_FORCE_SHOUTED", 0)
    End Sub

    <Extension()>
    Public Sub SetLivery2(veh As Vehicle, liv As Integer)
        Native.Function.Call(DirectCast(&HA6D3A8750DC73270UL, Hash), veh.Handle, liv)
    End Sub

    <Extension()>
    Public Function GetLivery2(veh As Vehicle) As Integer
        Return Native.Function.Call(Of Integer)(DirectCast(&H60190048C0764A26UL, Hash), veh.Handle)
    End Function

    <Extension()>
    Public Function Livery2Count(veh As Vehicle) As Integer
        Return Native.Function.Call(Of Integer)(DirectCast(&H5ECB40269053C0D4UL, Hash), veh.Handle)
    End Function

    Public Function GetBennysOriginalRim(curRim As Integer) As Integer
        Dim result As Integer = 0

        Dim totalWheelsCount As Integer = veh.GetModCount(VehicleMod.FrontWheels) '217
        Dim howMany As Integer = (totalWheelsCount / 7) '31

        If curRim <= howMany Then
            result = curRim
        Else
            result = curRim Mod 31
            '                ^ thanks to ikt
        End If

        Return result
    End Function

    Public Function CanEnterBennysMotorwork(veh As Vehicle) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash._0x8D474C8FAEFF6CDE, veh)
    End Function

    <Extension>
    Public Function IsVehicleAttachedToTrailer(veh As Vehicle) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.IS_VEHICLE_ATTACHED_TO_TRAILER, veh)
    End Function

    <Extension()>
    Public Function GetVehicleTrailerVehicle(veh As Vehicle) As Vehicle
        Dim arg As New OutputArgument()
        Native.Function.Call(Hash.GET_VEHICLE_TRAILER_VEHICLE, veh, arg)
        Return arg.GetResult(Of Vehicle)()
    End Function

    Public Enum EngineLoc
        front
        rear
        unk
    End Enum

    Public Function GetVehicleEnginePositionSingle(veh As Vehicle) As Single
        Dim lfwheel As Vector3 = veh.GetBoneCoord("wheel_lf")
        Dim engine As Vector3 = veh.GetBoneCoord("engine")
        Return Vector3.Distance(lfwheel, engine)
    End Function

    Public Function GetVehicleHoodPositionSingle(veh As Vehicle) As Single
        Dim lfwheel As Vector3 = veh.GetBoneCoord("wheel_lf")
        Dim bonnet As Vector3 = veh.GetBoneCoord("bonnet")
        Return Vector3.Distance(lfwheel, bonnet)
    End Function

    Public Function GetVehicleTrunkPositionSingle(veh As Vehicle) As Single
        Dim lfwheel As Vector3 = veh.GetBoneCoord("wheel_lf")
        Dim boot As Vector3 = veh.GetBoneCoord("boot")
        Return Vector3.Distance(lfwheel, boot)
    End Function

    <Extension()>
    Public Function GetVehEnginePos(veh As Vehicle) As EngineLoc
        Dim lfwheel As Vector3 = veh.GetBoneCoord("wheel_lf")
        Dim engine As Vector3 = veh.GetBoneCoord("engine")
        Dim result As EngineLoc = EngineLoc.unk
        Select Case Vector3.Distance(lfwheel, engine)
            Case 0.0 To 1.9
                result = EngineLoc.front
            Case 2.0 To 5.0
                result = EngineLoc.rear
            Case Else
                result = EngineLoc.unk
        End Select

        Return result
    End Function

    <Extension()>
    Public Function GetVehHoodPos(veh As Vehicle) As EngineLoc
        Dim bonnet As Vector3 = veh.GetBoneCoord("bonnet")
        Dim lfwheel As Vector3 = veh.GetBoneCoord("wheel_lf")
        Dim result As EngineLoc = EngineLoc.unk
        Select Case Vector3.Distance(bonnet, lfwheel)
            Case 0.0 To 1.69
                result = EngineLoc.front
            Case 1.7 To 5.0
                result = EngineLoc.rear
            Case Else
                result = EngineLoc.unk
        End Select

        Return result
    End Function

    <Extension()>
    Public Function GetVehTrunkPos(veh As Vehicle) As EngineLoc
        Dim boot As Vector3 = veh.GetBoneCoord("boot")
        Dim lfwheel As Vector3 = veh.GetBoneCoord("wheel_lf")
        Dim result As EngineLoc = EngineLoc.unk
        Select Case Vector3.Distance(boot, lfwheel)
            Case 0.0 To 1.99
                result = EngineLoc.front
            Case 2.0 To 5.0
                result = EngineLoc.rear
            Case Else
                result = EngineLoc.unk
        End Select

        Return result
    End Function

    Public Function GetVehicleStats(ByVal veh As Vehicle) As VehicleStats
        Dim stats As New VehicleStats
        stats.TopSpeed = ((Native.Function.Call(Of Single)(Hash._0x53AF99BAA671CA47, veh) * 3600) / 1609.344) * 1.9
        stats.Braking = Native.Function.Call(Of Single)(Hash.GET_VEHICLE_MAX_BRAKING, veh) * 70
        stats.Acceleration = (Native.Function.Call(Of Single)(Hash.GET_VEHICLE_ACCELERATION, veh) * 100) * 4.4
        stats.Traction = Native.Function.Call(Of Single)(Hash.GET_VEHICLE_MAX_TRACTION, veh) * 6.5
        If stats.TopSpeed >= 200 Then stats.TopSpeed = 200
        If stats.Braking >= 200 Then stats.Braking = 200
        If stats.Acceleration >= 200 Then stats.Acceleration = 200
        If stats.Traction >= 200 Then stats.Traction = 200
        Return stats
    End Function

    <Extension()>
    Public Sub SetXenonHeadlightsColor(ByVal veh As Vehicle, colorID As Integer, toggleXenon As Boolean)
        If toggleXenon Then veh.ToggleMod(VehicleToggleMod.XenonHeadlights, True)
        Native.Function.Call(&HE41033B25D003A07UL, veh.Handle, colorID)
    End Sub

    <Extension()>
    Public Function GetXenonHeadlightsColor(ByVal veh As Vehicle) As Integer
        Return Native.Function.Call(Of Integer)(&H3DFF319A831E0CDB, veh.Handle)
    End Function

    <Extension()>
    Public Function Brand(ByVal veh As Vehicle) As String
        Return Game.GetGXTEntry(Native.Function.Call(Of String)(&HF7AF4F159FF99F97UL, veh.Model.Hash))
    End Function

    <Extension()>
    Public Function GetHashKey(str As String) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_HASH_KEY, str)
    End Function

    Public Function IsArenaWarDLCInstalled() As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.IS_DLC_PRESENT, "mpchristmas2018".GetHashKey)
    End Function

    <Extension()>
    Public Sub UpdateTitleCaption(ByVal menu As UIMenu, newCaption As String, Optional upper As Boolean = False)
        Try
            Dim result As String = Game.GetGXTEntry(newCaption)
            If upper Then result = Game.GetGXTEntry(newCaption).ToUpper()
            If menu.Subtitle.Caption = "NULL" Then menu.Subtitle.Caption = result
        Catch ex As Exception
            Logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    <Extension()>
    Public Sub UpdateTitleCaption(ByVal menu As UIMenu, wheeltype As VehicleWheelType, Optional upper As Boolean = False)
        Dim result As String = GetLocalizedWheelTypeName(wheeltype)
        If upper Then result = GetLocalizedWheelTypeName(wheeltype).ToUpper()
        If menu.Subtitle.Caption = "NULL" Then menu.Subtitle.Caption = result
    End Sub

    <Extension()>
    Public Function GetUIMenuOffset(ByVal menu As UIMenu) As Point
        Dim banner = 107, subtitle = 37, stat = 9
        Dim sizeHeight = (38 * menu.Size) + banner + subtitle + stat
        Return New Point(UIMenu.GetSafezoneBounds.X, UIMenu.GetSafezoneBounds.Y + sizeHeight)
    End Function

    <Extension()>
    Public Function UppercaseFirstLetter(ByVal val As String) As String
        If String.IsNullOrEmpty(val) Then Return val
        Dim array() As Char = val.ToCharArray
        array(0) = Char.ToUpper(array(0))
        Return New String(array)
    End Function

    Public Sub HoodCamera()
        If veh.HasBone("bonnet") AndAlso veh.HasBone("boot") Then 'has hood and trunk
            If veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                    camera.MainCameraPosition = CameraPosition.Hood
                ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                    camera.MainCameraPosition = CameraPosition.RearHood
                End If
            ElseIf veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                    camera.MainCameraPosition = CameraPosition.Hood
                ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                    If veh.GetVehTrunkPos = EngineLoc.front Then
                        camera.MainCameraPosition = CameraPosition.FrontTrunk
                    Else
                        camera.MainCameraPosition = CameraPosition.FrontBumper
                    End If
                End If
            End If
        ElseIf veh.HasBone("bonnet") Then 'has hood only
            If veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                    camera.MainCameraPosition = CameraPosition.Hood
                ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                    camera.MainCameraPosition = CameraPosition.RearHood
                End If
            ElseIf veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                    camera.MainCameraPosition = CameraPosition.Hood
                ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                End If
            End If

        ElseIf veh.HasBone("boot") Then 'has trunk only
            If veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                If veh.GetVehTrunkPos = EngineLoc.front Then 'front trunk
                    camera.MainCameraPosition = CameraPosition.FrontTrunk
                ElseIf veh.GetVehTrunkPos = EngineLoc.rear Then 'rear trunk
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                End If
            ElseIf veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                If veh.GetVehTrunkPos = EngineLoc.front Then 'front trunk
                    camera.MainCameraPosition = CameraPosition.FrontTrunk
                ElseIf veh.GetVehTrunkPos = EngineLoc.rear Then 'rear trunk
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                End If
            End If
        Else 'no hood and trunk
            If veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                camera.MainCameraPosition = CameraPosition.FrontBumper
            ElseIf veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                camera.MainCameraPosition = CameraPosition.Engine
            End If
        End If
    End Sub

    Public Sub HoodCamera(opendoor As Boolean)
        Select Case veh.Model
            Case "monster3", "monster4", "monster5"
                camera.MainCameraPosition = CameraPosition.Car
            Case Else
                If Not opendoor Then
                    HoodCamera()
                    Exit Sub
                End If
                If veh.HasBone("bonnet") AndAlso veh.HasBone("boot") Then 'has hood and trunk
                    If veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                        If opendoor Then veh.OpenDoor(VehicleDoor.Hood, False, False)
                        If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                            camera.MainCameraPosition = CameraPosition.Hood
                        ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                            camera.MainCameraPosition = CameraPosition.RearHood
                        End If
                    ElseIf veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                        If veh.GetVehHoodPos = EngineLoc.rear AndAlso veh.GetVehTrunkPos = EngineLoc.front Then
                            'eg: comet3
                            If opendoor Then veh.OpenDoor(VehicleDoor.Hood, False, False)
                            camera.MainCameraPosition = CameraPosition.RearEngine
                        ElseIf veh.GetVehHoodPos = EngineLoc.front AndAlso veh.GetVehTrunkPos = EngineLoc.rear Then
                            If opendoor Then veh.OpenDoor(VehicleDoor.Trunk, False, False)
                            If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                                camera.MainCameraPosition = CameraPosition.Trunk
                            ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                                If veh.GetVehTrunkPos = EngineLoc.front Then
                                    camera.MainCameraPosition = CameraPosition.FrontTrunk
                                Else
                                    camera.MainCameraPosition = CameraPosition.FrontBumper
                                End If
                            End If
                        End If
                    End If
                ElseIf veh.HasBone("bonnet") Then 'has hood only
                    If opendoor Then veh.OpenDoor(VehicleDoor.Hood, False, False)
                    If veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                        If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                            camera.MainCameraPosition = CameraPosition.Hood
                        ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                            camera.MainCameraPosition = CameraPosition.RearHood
                        End If
                    ElseIf veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                        If veh.GetVehHoodPos = EngineLoc.front Then 'front hood
                            camera.MainCameraPosition = CameraPosition.Hood
                        ElseIf veh.GetVehHoodPos = EngineLoc.rear Then 'rear hood
                            camera.MainCameraPosition = CameraPosition.FrontBumper
                        End If
                    End If

                ElseIf veh.HasBone("boot") Then 'has trunk only
                    If opendoor Then veh.OpenDoor(VehicleDoor.Trunk, False, False)
                    If veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                        If veh.GetVehTrunkPos = EngineLoc.front Then 'front trunk
                            camera.MainCameraPosition = CameraPosition.FrontTrunk
                        ElseIf veh.GetVehTrunkPos = EngineLoc.rear Then 'rear trunk
                            camera.MainCameraPosition = CameraPosition.FrontBumper
                        End If
                    ElseIf veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                        If veh.GetVehTrunkPos = EngineLoc.front Then 'front trunk
                            camera.MainCameraPosition = CameraPosition.FrontTrunk
                        ElseIf veh.GetVehTrunkPos = EngineLoc.rear Then 'rear trunk
                            camera.MainCameraPosition = CameraPosition.FrontBumper
                        End If
                    End If
                Else 'no hood and trunk
                    If veh.GetVehEnginePos = EngineLoc.rear Then 'rear engine
                        camera.MainCameraPosition = CameraPosition.FrontBumper
                    ElseIf veh.GetVehEnginePos = EngineLoc.front Then 'front engine
                        camera.MainCameraPosition = CameraPosition.Engine
                    End If
                End If
        End Select
    End Sub

    'open shop_controller.ysc and search for "!= 999"
    Public Enum GlobalValue
        b1_0_757_4 = &H271803
        b1_0_791_2 = &H272A34
        b1_0_877_1 = &H2750BD
        b1_0_944_2 = &H279476
        'b1_0_1011_1 = ?
        b1_0_1032_1 = 2593970
        b1_0_1103_2 = 2599337
        b1_0_1180_2 = 2606794
        'b1_0_1290_1 = ?
        b1_0_1365_1 = 4265719
        'b1_0_1493_0 = ?
        b1_0_1493_1 = 4266042
        b1_0_1604_1 = 4266905
        b1_0_1737_0 = 4267883
        b1_0_1868_0 = 4268190
        b1_0_2060_0 = 4268340
        b1_0_2060_1 = 4268341
    End Enum

    Public Function GetGlobalValue() As GlobalValue
        Select Case Game.Version
            Case GameVersion.VER_1_0_757_4_NOSTEAM
                Return GlobalValue.b1_0_757_4
            Case GameVersion.VER_1_0_791_2_NOSTEAM, GameVersion.VER_1_0_791_2_STEAM
                Return GlobalValue.b1_0_791_2
            Case GameVersion.VER_1_0_877_1_NOSTEAM, GameVersion.VER_1_0_877_1_STEAM
                Return GlobalValue.b1_0_877_1
            Case GameVersion.VER_1_0_944_2_NOSTEAM, GameVersion.VER_1_0_944_2_STEAM
                Return GlobalValue.b1_0_944_2
            Case GameVersion.VER_1_0_1032_1_NOSTEAM, GameVersion.VER_1_0_1032_1_STEAM
                Return GlobalValue.b1_0_1032_1
            Case GameVersion.VER_1_0_1103_2_NOSTEAM, GameVersion.VER_1_0_1103_2_STEAM
                Return GlobalValue.b1_0_1103_2
            Case GameVersion.VER_1_0_1180_2_NOSTEAM, GameVersion.VER_1_0_1180_2_STEAM
                Return GlobalValue.b1_0_1180_2
            Case GameVersion.VER_1_0_1365_1_NOSTEAM, GameVersion.VER_1_0_1365_1_STEAM
                Return GlobalValue.b1_0_1365_1
            Case GameVersion.VER_1_0_1493_1_NOSTEAM, GameVersion.VER_1_0_1493_1_STEAM
                Return GlobalValue.b1_0_1493_1
            Case GameVersion.VER_1_0_1604_0_NOSTEAM, GameVersion.VER_1_0_1604_0_STEAM, GameVersion.VER_1_0_1604_1_NOSTEAM, GameVersion.VER_1_0_1604_1_STEAM
                Return GlobalValue.b1_0_1604_1
            Case GameVersion.VER_1_0_1737_0_NOSTEAM, GameVersion.VER_1_0_1737_0_STEAM, GameVersion.VER_1_0_1737_6_NOSTEAM, GameVersion.VER_1_0_1737_6_STEAM
                Return GlobalValue.b1_0_1737_0
            Case GameVersion.VER_1_0_1868_0_NOSTEAM, GameVersion.VER_1_0_1868_0_STEAM, 57, 58, 59 'GameVersion.VER_1_0_1868_1_STEAM, GameVersion.VER_1_0_1868_1_NOSTEAM, GameVersion.VER_1_0_1868_4_EGS
                Return GlobalValue.b1_0_1868_0
            Case 60, 61 'GameVersion.VER_1_0_2060_0_STEAM, GameVersion.VER_1_0_2060_0_NOSTEAM
                Return GlobalValue.b1_0_2060_0
            Case 62, 63 'GameVersion.VER_1_0_2060_1_STEAM, GameVersion.VER_1_0_2060_1_NOSTEAM
                Return GlobalValue.b1_0_2060_1
            Case Else
                Return GlobalValue.b1_0_2060_1
        End Select
    End Function

    Public Sub SuspendKeys()
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAccelerate)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAim)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAttack)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleAttack2)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleBrake)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleCinCam)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleDuck)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleExit)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHeadlight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHorn)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveLeftOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveRightOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnLeftOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnRightOnly)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnHardLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleSubTurnHardRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleMoveLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleLookLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleLookRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHotwireLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleHotwireRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleGunLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleGunLeft)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleGunRight)
        Game.DisableControlThisFrame(0, GTA.Control.VehicleCinematicLeftRight)
        Game.DisableControlThisFrame(0, GTA.Control.NextCamera)
        Game.DisableControlThisFrame(0, Control.VehicleRocketBoost)
        Game.DisableControlThisFrame(0, Control.VehicleJump)
        Game.DisableControlThisFrame(0, Control.VehicleCarJump)
    End Sub

    Public Sub PlayerVehicleHalt(Optional distance As Single = 1.0F)
        Native.Function.Call(Hash._TASK_BRING_VEHICLE_TO_HALT, Game.Player.LastVehicle, distance, 1, 0)
    End Sub

    Public Function IsNitroModInstalled() As Boolean
        Return Decor.Registered(nitroMod, Decor.eDecorType.Int)
    End Function

    <Extension>
    Public Function CanInstallNitroMod(v As Vehicle) As Boolean
        Dim result As Boolean = True
        If v.HasRocketBoost Then result = False
        If v.HasRam Then result = False
        If v.HasScoop Then result = False
        If v.HasSpike Then result = False
        If v.GetModCount(VehicleMod.AirFilter) >= 2 AndAlso Not bennysvehicle.Contains(v.Model) Then result = False
        If Not IsNitroModInstalled() Then result = False
        Return result
    End Function

    Public Sub UpdateTitleName()
        QuitMenu.UpdateTitleCaption("CMOD_MOD_E")
        MainMenu.UpdateTitleCaption("CMOD_MOD_T")
        gmBodywork.UpdateTitleCaption("CMOD_BW_T")
        gmEngine.UpdateTitleCaption("CMM_MOD_GT3")
        gmInterior.UpdateTitleCaption("CMM_MOD_GT1")
        gmPlate.UpdateTitleCaption("CMM_MOD_GT2")
        gmRespray.UpdateTitleCaption("CMOD_COL0_T")
        gmWheels.UpdateTitleCaption("CMOD_WHE0_T")
        gmBumper.UpdateTitleCaption("CMOD_BUM_T")
        gmWheelType.UpdateTitleCaption("CMOD_WHE1_T")
        gmNeonKits.UpdateTitleCaption("CMOD_MOD_LGT_N", True)
        mAerials.UpdateTitleCaption("CMM_MOD_ST18")
        mSuspension.UpdateTitleCaption("CMOD_SUS_T")
        mArmor.UpdateTitleCaption("CMOD_ARM_T")
        mBrakes.UpdateTitleCaption("CMOD_BRA_T")
        mEngine.UpdateTitleCaption("CMM_MOD_GT3")
        mNitro.UpdateTitleCaption("CMM_MOD_TBOS")
        mTransmission.UpdateTitleCaption("CMOD_GBX_T")
        mFBumper.UpdateTitleCaption("CMOD_BUMF_T")
        mRBumper.UpdateTitleCaption("CMOD_BUMR_T")
        mSSkirt.UpdateTitleCaption("CMOD_SS_T")
        mTrim.UpdateTitleCaption("CMM_MOD_ST19")
        mEngineBlock.UpdateTitleCaption("CMOD_EB_T")
        mAirFilter.UpdateTitleCaption("CMM_MOD_ST15")
        mStruts.UpdateTitleCaption("CMM_MOD_ST16")
        mColumnShifterLevers.UpdateTitleCaption("CMM_MOD_ST9")
        mDashboard.UpdateTitleCaption("CMM_MOD_ST4")
        mDialDesign.UpdateTitleCaption("CMM_MOD_ST5")
        mOrnaments.UpdateTitleCaption("CMM_MOD_ST3")
        mSeats.UpdateTitleCaption("CMM_MOD_ST7")
        mSteeringWheels.UpdateTitleCaption("CMM_MOD_ST8")
        mTrimDesign.UpdateTitleCaption("CMM_MOD_ST2")
        mPlateHolder.UpdateTitleCaption("CMOD_PLH_T")
        mVanityPlates.UpdateTitleCaption("CMM_MOD_ST1")
        mNumberPlate.UpdateTitleCaption("CMM_MOD_GT2")
        gmBikeWheels.UpdateTitleCaption(VehicleWheelType.BikeWheels, True)
        gmHighEnd.UpdateTitleCaption(VehicleWheelType.HighEnd, True)
        gmLowrider.UpdateTitleCaption(VehicleWheelType.Lowrider, True)
        gmMuscle.UpdateTitleCaption(VehicleWheelType.Muscle, True)
        gmOffroad.UpdateTitleCaption(VehicleWheelType.Offroad, True)
        gmSport.UpdateTitleCaption(VehicleWheelType.Sport, True)
        gmSUV.UpdateTitleCaption(VehicleWheelType.SUV, True)
        gmTuner.UpdateTitleCaption(VehicleWheelType.Tuner, True)
        mBennysOriginals.UpdateTitleCaption(CType(8, VehicleWheelType), True)
        mBespoke.UpdateTitleCaption(CType(9, VehicleWheelType))
        mTires.UpdateTitleCaption("CMOD_TYR_T")
        mHeadlights.UpdateTitleCaption("CMOD_HED_T")
        mNeon.UpdateTitleCaption("CMOD_MOD_LGT_N", True)
        mNeonColor.UpdateTitleCaption("CMOD_NEON_1", True)
        mArchCover.UpdateTitleCaption("CMM_MOD_ST17")
        mExhaust.UpdateTitleCaption("CMOD_EXH_T")
        mFender.UpdateTitleCaption("CMOD_WNG_T")
        mRFender.UpdateTitleCaption("CMOD_WNG_T")
        mDoor.UpdateTitleCaption("CMM_MOD_ST6")
        mFrame.UpdateTitleCaption("CMOD_RC_T")
        mGrille.UpdateTitleCaption("CMOD_GRL_T")
        mHood.UpdateTitleCaption("CMOD_BON_T")
        mHorn.UpdateTitleCaption("CMOD_HRN_T")
        mHydraulics.UpdateTitleCaption("CMM_MOD_ST13")
        mLivery.UpdateTitleCaption("CMM_MOD_ST23")
        mPlaques.UpdateTitleCaption("CMM_MOD_ST10")
        mRoof.UpdateTitleCaption("CMOD_ROF_T")
        mSpeakers.UpdateTitleCaption("CMM_MOD_S11")
        mSpoilers.UpdateTitleCaption("CMOD_SPO_T")
        mTank.UpdateTitleCaption("CMM_MOD_ST20")
        mTrunk.UpdateTitleCaption("CMOD_TR_T")
        mWindow.UpdateTitleCaption("CMM_MOD_ST21")
        mTurbo.UpdateTitleCaption("CMOD_TUR_T")
        mTint.UpdateTitleCaption("CMOD_WIN_T")
        mLightsColor.UpdateTitleCaption("CMM_MOD_ST26")
        mTrimColor.UpdateTitleCaption("CMOD_MOD_TRIM2", True)
        mRimColor.UpdateTitleCaption("CMOD_COL5_T")
        mPrimaryClassicColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mPrimaryChromeColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mPrimaryMetallicColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mPrimaryMetalsColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mPrimaryMatteColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mPrimaryPearlescentColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mPrimaryColor.UpdateTitleCaption("CMOD_COL0_0", True)
        mSecondaryColor.UpdateTitleCaption("CMOD_COL0_1", True)
        mSecondaryClassicColor.UpdateTitleCaption("CMOD_COL0_1", True)
        mSecondaryChromeColor.UpdateTitleCaption("CMOD_COL0_1", True)
        mSecondaryMetallicColor.UpdateTitleCaption("CMOD_COL0_1", True)
        mSecondaryMetalsColor.UpdateTitleCaption("CMOD_COL0_1", True)
        mSecondaryMatteColor.UpdateTitleCaption("CMOD_COL0_1", True)
        mTireSmoke.UpdateTitleCaption("CMOD_MOD_TYR3", True)
        mTornadoC.UpdateTitleCaption("CMOD_SMOD_6_D")
        mSBikeWheels.UpdateTitleCaption(VehicleWheelType.BikeWheels, True)
        mCBikeWheels.UpdateTitleCaption(VehicleWheelType.BikeWheels, True)
        mSHighEnd.UpdateTitleCaption(VehicleWheelType.HighEnd, True)
        mCHighEnd.UpdateTitleCaption(VehicleWheelType.HighEnd, True)
        mSLowrider.UpdateTitleCaption(VehicleWheelType.Lowrider, True)
        mCLowrider.UpdateTitleCaption(VehicleWheelType.Lowrider, True)
        mSMuscle.UpdateTitleCaption(VehicleWheelType.Muscle, True)
        mCMuscle.UpdateTitleCaption(VehicleWheelType.Muscle, True)
        mSOffroad.UpdateTitleCaption(VehicleWheelType.Offroad, True)
        mCOffroad.UpdateTitleCaption(VehicleWheelType.Offroad, True)
        mSSport.UpdateTitleCaption(VehicleWheelType.Sport, True)
        mSSport.UpdateTitleCaption(VehicleWheelType.Sport, True)
        mSSUV.UpdateTitleCaption(VehicleWheelType.SUV, True)
        mCSUV.UpdateTitleCaption(VehicleWheelType.SUV, True)
        mSTuner.UpdateTitleCaption(VehicleWheelType.Tuner, True)
        mCTuner.UpdateTitleCaption(VehicleWheelType.Tuner, True)
        mUpgradeAW.UpdateTitleCaption("collision_9znude7")
        gmWeapon.UpdateTitleCaption("PM_SCR_WEA")
        gmBodyworkArena.UpdateTitleCaption("CMOD_BW_T")
    End Sub

    Public Sub LoadSettings()
        Dim config As ScriptSettings = ScriptSettings.Load("scripts\BennysOriginalMotorWorks.ini")
        onlineMap = config.GetValue(Of Integer)("SETTINGS", "OnlineMap", 1)
        fixDoor = config.GetValue(Of Integer)("SETTINGS", "FixDoor", 1)
        fpcKey = config.GetValue(Of GTA.Control)("CONTROLS", "FirstPerson", GTA.Control.NextCamera)
        zoutKey = config.GetValue(Of GTA.Control)("CONTROLS", "ZoomOut", GTA.Control.FrontendLt)
        zinKey = config.GetValue(Of GTA.Control)("CONTROLS", "ZoomIn", GTA.Control.FrontendRt)
        If onlineMap = 1 Then LoadMPDLCMap()
    End Sub

    Public Sub CreateBlip()
        BennysBlip = World.CreateBlip(New Vector3(-205.5417, -1307.118, 30.26981))
        BennysBlip.Sprite = (BlipSprite.DollarSignSquared Or BlipSprite.ArrowDownOutlined)
        BennysBlip.Color = BlipColor.Yellow
        BennysBlip.IsShortRange = True
        BennysBlip.Name = Game.GetGXTEntry("S_MO_09")
    End Sub

    Public Sub PlayEnterCutScene()
        Try
            Dim rt As New RealTimer()
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
            Game.FadeScreenOut(1000)
            Script.Wait(1000)
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
            Script.Wait(1000)
            Game.FadeScreenIn(1000)
            scriptCam = World.CreateCamera(New Vector3(-201.1808, -1321.512, 32.20821), Vector3.Zero, GameplayCamera.FieldOfView)
            Dim interpCam As Camera = World.CreateCamera(New Vector3(-200.7804, -1316.474, 32.08001), Vector3.Zero, GameplayCamera.FieldOfView)
            World.RenderingCamera = scriptCam
            scriptCam.InterpTo(interpCam, 5000, True, True)
            World.RenderingCamera = interpCam
            interpCam.Shake(CameraShake.Hand, 0.4F)
            interpCam.PointAt(veh)

            While Not rt.TotalSeconds(2)
                Script.Wait(50)
            End While
            ply.Task.DriveTo(veh, New Vector3(-207.155, -1320.521, 30.8904), 0.1, 2.3)
            Helper.PlaySpeech("SHOP_NICE_VEHICLE")

            While Not rt.TotalSeconds(4)
                Script.Wait(50)
            End While
            ply.Task.DriveTo(veh, New Vector3(-211.798, -1324.292, 30.37535), 0.1, 2)

            While Not rt.TotalSeconds(8)
                Script.Wait(50)
            End While

            ply.Task.ClearAll()
            Game.FadeScreenOut(1000)
            Script.Wait(1000)
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
            isExiting = False
            isCutscene = False
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub PlayExitCutScene()
        Try
            Dim rt As New RealTimer
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
            isExiting = True

            Game.FadeScreenOut(1000)
            Script.Wait(1000)
            Game.Player.Character.Alpha = 255
            camera.Stop()
            veh.Position = New Vector3(-205.8678, -1321.805, 30.41191)
            veh.Heading = 358.6677
            ply.Task.DriveTo(veh, New Vector3(-205.743, -1303.657, 30.84998), 0.5, 5)
            Script.Wait(1000)
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
            While Not rt.TotalSeconds(5)
                Script.Wait(50)
            End While
            ply.Task.DriveTo(veh, New Vector3(-200.2561, -1303.021, 30.66544), 0.1, 2)
            While Not rt.TotalSeconds(9)
                Script.Wait(50)
            End While
            ply.Task.ClearAll()
            World.DestroyAllCameras()
            World.RenderingCamera = Nothing
            ply.Task.ClearAll()
            If Not veh.Position.DistanceToSquared(New Vector3(-200.2561, -1303.021, 30.66544)) <= 4.0F Then
                Game.FadeScreenOut(1000)
                Script.Wait(1000)
                veh.Position = New Vector3(-200.2561, -1303.021, 30.66544)
                veh.Heading = 312.8701
                Script.Wait(1000)
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

    Public Sub PutVehIntoShop()
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
                .Suspension = veh.GetMod(VehicleMod.Suspension),
                .Nitro = veh.GetInt(nitroMod),
                .BulletProofTires = veh.CanTiresBurst}
            veh.Position = New Vector3(-211.798, -1324.292, 30.37535)
            veh.Heading = 150.2801 '358.6677
            MainMenu.Visible = Not MainMenu.Visible
            camera.RepositionFor(veh)
            Script.Wait(1000)
            Game.FadeScreenIn(1000)
            If Not Native.Function.Call(Of Boolean)(Hash.IS_AUDIO_SCENE_ACTIVE, "CAR_MOD_RADIO_MUTE_SCENE") Then Native.Function.Call(Hash.START_AUDIO_SCENE, "CAR_MOD_RADIO_MUTE_SCENE")
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

End Module
