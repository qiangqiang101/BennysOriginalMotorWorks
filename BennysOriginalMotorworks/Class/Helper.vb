Imports System.Runtime.InteropServices
Imports System.Text
Imports GTA
Imports GTA.Math
Imports GTA.Native

Public Class Helper

    Public Shared Function CreateVehicle(VehicleModel As String, VehicleHash As Integer, Position As Vector3, Optional Heading As Single = 0) As Vehicle
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

    Public Shared Function WorldCreateVehicle(model As Model, position As Vector3, Optional heading As Single = 0F) As Vehicle
        If Not model.IsVehicle OrElse Not model.Request(1000) Then
            Return Nothing
        End If

        Return New Vehicle(Native.Function.Call(Of Integer)(Hash.CREATE_VEHICLE, model.Hash, position.X, position.Y, position.Z, heading,
        False, False))
    End Function

    Public Shared Sub LoadMPDLCMap()
        Native.Function.Call(Hash._LOAD_MP_DLC_MAPS)
        LoadMPDLCMapMissingObjects()
    End Sub

    Public Shared Sub LoadMPDLCMapMissingObjects()
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

        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID1, "V_57_FranklinStuff")

        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "swap_clean_apt")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "layer_whiskey")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "layer_sextoys_a")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "swap_mrJam_A")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "swap_sofa_A")

        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_bed_tidy")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_L_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_S_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_D_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_M_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "Michael_premier")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_plane_ticket")

        'Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "showhome_only")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "franklin_settled")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "franklin_unpacking")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "bong_and_wine")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "progress_flyer")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "progress_tshirt")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "progress_tux")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "unlocked")

        Native.Function.Call(Hash._0x55E86AF2712B36A1, WODID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, NCAID1, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, NCAID2, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, HCAID1, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, HCAID2, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, HCAID3, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MRID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, WMDID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MWTDID, "Stilts_Kitchen_Window")

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

    Public Shared Sub DisplayHelpTextThisFrame(helpText As String, Optional Shape As Integer = -1)
        Native.Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "CELL_EMAIL_BCON")
        Const maxStringLength As Integer = 99

        Dim i As Integer = 0
        While i < helpText.Length
            Native.Function.Call(Hash._0x6C188BE134E074AA, helpText.Substring(i, System.Math.Min(maxStringLength, helpText.Length - i)))
            i += maxStringLength
        End While
        Native.Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, 0, 1, Shape)
    End Sub

    Public Shared Function GetInteriorID(interior As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, interior.X, interior.Y, interior.Z)
    End Function

    Public Shared Function LowriderUpgrade(model As Model) As Model
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
            Case "slamvan"
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

    Public Shared Sub ScreenEffectStart(effectName As ScreenEffect, Optional duration As Integer = 0, Optional looped As Boolean = False)
        Native.Function.Call(Hash._START_SCREEN_EFFECT, New InputArgument() {[Enum].GetName(GetType(ScreenEffect), effectName), duration, looped})
    End Sub

    Public Shared Function LocalizedModTypeName(modType As VehicleMod) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String = Nothing
        Select Case modType
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
                If Not Bennys.veh.Model.IsBike AndAlso Bennys.veh.Model.IsBicycle Then
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
                If Bennys.veh.Model = "elegy" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S40")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S1")
                End If
                Exit Select
            Case VehicleMod.TrimDesign
                If Bennys.veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S2b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S2")
                End If
                Exit Select
            Case VehicleMod.Ornaments
                cur = Game.GetGXTEntry("CMM_MOD_S3")
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
                cur = Game.GetGXTEntry("CMM_MOD_S10")
                Exit Select
            Case VehicleMod.Speakers
                cur = Game.GetGXTEntry("CMM_MOD_S11")
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
                Select Case Bennys.veh.Model
                    Case "sultanrs", "elegy"
                        cur = Game.GetGXTEntry("CMM_MOD_S15b")
                    Case Else
                        cur = Game.GetGXTEntry("CMM_MOD_S15")
                End Select
                Exit Select
            Case VehicleMod.Struts
                Select Case Bennys.veh.Model
                    Case "sultanrs", "banshee2"
                        cur = Game.GetGXTEntry("CMM_MOD_S16b")
                    Case Else
                        cur = Game.GetGXTEntry("CMM_MOD_S16")
                End Select
                Exit Select
            Case VehicleMod.ArchCover
                If Bennys.veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S17b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S17")
                End If
                Exit Select
            Case VehicleMod.Aerials
                If Bennys.veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S18b")
                ElseIf Bennys.veh.Model = "btype3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S18c")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S18")
                End If
                Exit Select
            Case VehicleMod.Trim
                If Bennys.veh.Model = "sultanrs" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19b")
                ElseIf Bennys.veh.Model = "btype3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19c")
                ElseIf Bennys.veh.Model = "virgo2" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19d")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S19")
                End If
                Exit Select
            Case VehicleMod.Tank
                If Bennys.veh.Model = "slamvan3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S27")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S20")
                End If
                Exit Select

            Case VehicleMod.Windows
                If Bennys.veh.Model = "btype3" Then
                    cur = Game.GetGXTEntry("CMM_MOD_S21b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S21")
                End If
                Exit Select
            Case DirectCast(47, VehicleMod)
                If Bennys.veh.Model = "slamvan3" Then
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
                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                    cur = Game.GetGXTEntry("CMOD_SHIFTER_0")
                Else
                    cur = Game.GetGXTEntry("CMOD_MOD_FEN")
                End If
                Exit Select
            Case VehicleMod.Spoilers
                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                    If Bennys.veh.Model = "faggio3" Then
                        cur = Game.GetGXTEntry("TOP_ANTENNA")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_BLT")
                    End If
                Else
                    If Bennys.veh.Model = "btype3" Then
                        cur = Game.GetGXTEntry("BT_SPARE2")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_SPO")
                    End If
                End If
                Exit Select
            Case VehicleMod.Frame
                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                    cur = Game.GetGXTEntry("CMM_MOD_S14")
                Else
                    If Bennys.veh.Model = "sultanrs" Then
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
                Select Case Bennys.veh.Model
                    Case "avarus"
                        cur = Game.GetGXTEntry("TOP_OIL")
                    Case Else
                        cur = Game.GetGXTEntry("CMOD_MOD_GRL")
                End Select

                Exit Select
            Case VehicleMod.Hood
                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                    cur = Game.GetGXTEntry("CMM_MOD_S7")
                Else
                    cur = Game.GetGXTEntry("CMOD_MOD_HOD")
                End If
                Exit Select
            Case VehicleMod.Roof
                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                    If Bennys.veh.Model = "penetrator" Then
                        cur = Game.GetGXTEntry("CMM_MOD_S43")
                    ElseIf Bennys.veh.Model = "blazer4" Then
                        cur = Game.GetGXTEntry("CMM_MOD_S17")
                    ElseIf Bennys.veh.Model = "faggio3" Then
                        cur = Game.GetGXTEntry("TOP_ANTENNAR")
                    Else
                        cur = Game.GetGXTEntry("CMOD_MOD_TNK")
                    End If
                Else
                    cur = Game.GetGXTEntry("CMOD_MOD_ROF")
                End If
                Exit Select
            Case Else

                cur = Native.Function.Call(Of String)(Hash.GET_MOD_SLOT_NAME, Bennys.veh.Handle, modType)
                If DoesGXTEntryExist(cur) Then
                    cur = Game.GetGXTEntry(cur)
                End If
                Exit Select
        End Select
        If cur = "" Then
            'would only happen if the text isnt loaded
            cur = [Enum].GetName(GetType(VehicleMod), modType)
        End If

        Return cur
    End Function

    Public Shared Function LocalizeModTitleName(title As String) As String
        Dim langConf As ScriptSettings = ScriptSettings.Load("scripts\bennyslang.ini")
        Return langConf.GetValue("TITLE", title, "NULL")
    End Function

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
    End Enum

    Public Shared Function LocalizedModGroupName(groupName As GroupName) As String
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
                If Bennys.veh.Model = "blazer4" Then
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

    Public Shared Function LocalizedColorGroupName(colorTypeName As ColorType) As String
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

    Public Shared Function LocalizedModTypeName(toggleModType As VehicleToggleMod, Optional stock As Boolean = False) As String
        Dim result As String = Nothing
        If stock = True Then
            result = Game.GetGXTEntry("CMOD_ARM_0")
        Else
            'result = Native.Function.Call(Of String)(Hash.GET_MOD_SLOT_NAME, Bennys.veh.Handle, toggleModType)
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

    Public Shared Function DoesGXTEntryExist(entry As String) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.DOES_TEXT_LABEL_EXIST, entry)
    End Function

    Public Shared Function GetLocalizedModName(index As Integer, modCount As Integer, modType As VehicleMod) As String
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
                If Not Bennys.veh.Model.IsBike AndAlso Bennys.veh.Model.IsBicycle Then
                    Return Game.GetGXTEntry("CMOD_WHE_0")
                Else
                    Return Game.GetGXTEntry("CMOD_WHE_B_0")
                End If
            End If
            If index >= modCount / 2 Then
                Return Game.GetGXTEntry("CHROME") + " " + Game.GetGXTEntry(Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, Bennys.veh.Handle, modType, index))
            Else
                Return Game.GetGXTEntry(Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, Bennys.veh.Handle, modType, index))
            End If
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
            cur = Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, Bennys.veh.Handle, modType, index)
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
                    If Bennys.veh.Model = VehicleHash.Tornado Then
                    End If
                    Exit Select
                Case VehicleMod.Struts
                    Select Case Bennys.veh.Model
                        Case VehicleHash.Banshee, VehicleHash.Banshee2, VehicleHash.SultanRS
                            Return Game.GetGXTEntry("CMOD_COL5_41")
                    End Select
                    Exit Select

            End Select
            Return Game.GetGXTEntry("CMOD_DEF_0")
        End If
    End Function

    Public Shared Function LocalizedLicensePlate(plateType As GTA.NumberPlateType) As String
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

    Public Shared Function LocalizedWindowsTint(tint As GTA.VehicleWindowTint) As String
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

    Public Shared Function GetLocalizedWheelTypeName(wheelType As VehicleWheelType) As String
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

    Public Shared Function GetLocalizedColorName(vehColor As VehicleColor) As String
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

    Public Shared ClassicColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89,
        91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112}
    Public Shared MatteColor As List(Of VehicleColor) = New List(Of VehicleColor) From {12, 13, 14, 131, 83, 82, 84, 149, 148, 39, 40, 41, 42, 55, 128, 151, 155, 152, 153, 154}
    Public Shared MetalColor As List(Of VehicleColor) = New List(Of VehicleColor) From {117, 118, 119, 158, 159, 160}
    Public Shared ChromeColor As List(Of VehicleColor) = New List(Of VehicleColor) From {120}
    Public Shared PearlescentColor As List(Of VehicleColor) = New List(Of VehicleColor) From {0, 147, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 27, 28, 29, 150, 30, 31, 32, 33, 34, 143, 35, 135, 137, 136, 36, 38, 138, 99, 90, 88, 89, 91, 49, 50, 51, 52, 53, 54, 92, 141, 61, 62, 63, 64, 65, 66, 67, 68, 69, 73, 70, 74, 96, 101, 95, 94, 97, 103, 104, 98, 100, 102, 99, 105, 106, 71, 72, 142, 145, 107, 111, 112, 117, 118, 119, 158, 159, 160}

    Private Shared ReadOnly _colorNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
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

    Private Shared ReadOnly _hornNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
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
    {47, New Tuple(Of String, String)("HORN_XM15_2", "Festive Loop 2")},
    {48, New Tuple(Of String, String)("HORN_XM15_3", "Festive Loop 3")}
})

    Private Shared ReadOnly _wheelNames As New Dictionary(Of VehicleWheelType, Tuple(Of String, String))(New Dictionary(Of VehicleWheelType, Tuple(Of String, String))() From {
    {VehicleWheelType.BikeWheels, New Tuple(Of String, String)("CMOD_WHE1_0", "Bike")},
    {VehicleWheelType.HighEnd, New Tuple(Of String, String)("CMOD_WHE1_1", "High End")},
    {VehicleWheelType.Lowrider, New Tuple(Of String, String)("CMOD_WHE1_2", "Lowrider")},
    {VehicleWheelType.Muscle, New Tuple(Of String, String)("CMOD_WHE1_3", "Muscle")},
    {VehicleWheelType.Offroad, New Tuple(Of String, String)("CMOD_WHE1_4", "Offroad")},
    {VehicleWheelType.Sport, New Tuple(Of String, String)("CMOD_WHE1_5", "Sport")},
    {VehicleWheelType.SUV, New Tuple(Of String, String)("CMOD_WHE1_6", "SUV")},
    {VehicleWheelType.Tuner, New Tuple(Of String, String)("CMOD_WHE1_7", "Tuner")},
    {8, New Tuple(Of String, String)("CMOD_WHE1_8", "Benny's Originals")},
    {9, New Tuple(Of String, String)("CMOD_WHE1_9", "Benny's Bespoke")}
})

    Public Shared Function IsCustomWheels() As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_MOD_VARIATION, Bennys.veh, VehicleMod.FrontWheels)
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

    Public Shared Function NeonLayout() As NeonLayouts
        Dim v As Vehicle = Bennys.veh
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

    Public Enum GTAFont
        ' Fields
        Pricedown = 7
        Script = 1
        Symbols = 3
        Symbols2 = 5
        Title = 4
        Title2 = 6
        TitleWSymbols = 2
        UIDefault = 0
    End Enum

    Public Enum GTAFontAlign
        ' Fields
        Center = 1
        Left = 0
        Right = 2
    End Enum

    Public Enum GTAFontStyleOptions
        ' Fields
        DropShadow = 1
        None = 0
        Outline = 2
    End Enum

    Public Shared Sub DrawText(ByVal [Text] As String, ByVal Position As Drawing.PointF, ByVal Scale As Single, ByVal color As Drawing.Color, ByVal Font As GTAFont, ByVal Alignment As GTAFontAlign, ByVal Options As GTAFontStyleOptions)
        Dim arguments As InputArgument() = New InputArgument() {Font}
        Native.Function.Call(Hash._0x66E0276CC5F6B9DA, arguments)
        Dim argumentArray2 As InputArgument() = New InputArgument() {1.0!, Scale}
        Native.Function.Call(Hash._0x07C837F9A01C34C9, argumentArray2)
        Dim argumentArray3 As InputArgument() = New InputArgument() {color.R, color.G, color.B, color.A}
        Native.Function.Call(Hash._0xBE6B23FFA53FB442, argumentArray3)
        If Options.HasFlag(GTAFontStyleOptions.DropShadow) Then
            Native.Function.Call(Hash._0x1CA3E9EAC9D93E5E, New InputArgument(0 - 1) {})
        End If
        If Options.HasFlag(GTAFontStyleOptions.Outline) Then
            Native.Function.Call(Hash._0x2513DFB0FB8400FE, New InputArgument(0 - 1) {})
        End If
        If Alignment.HasFlag(GTAFontAlign.Center) Then
            Dim argumentArray4 As InputArgument() = New InputArgument() {1}
            Native.Function.Call(Hash._0xC02F4DBFB51D988B, argumentArray4)
        ElseIf Alignment.HasFlag(GTAFontAlign.Right) Then
            Dim argumentArray5 As InputArgument() = New InputArgument() {1}
            Native.Function.Call(Hash._0x6B3C4650BC8BEE47, argumentArray5)
        End If
        Dim argumentArray6 As InputArgument() = New InputArgument() {"jamyfafi"}
        Native.Function.Call(Hash._0x25FBB336DF1804CB, argumentArray6)
        PushBigString([Text])
        Dim argumentArray7 As InputArgument() = New InputArgument() {(Position.X / 1280.0!), (Position.Y / 720.0!)}
        Native.Function.Call(Hash._0xCD015E5BB0D96A57, argumentArray7)
    End Sub

    Public Shared Sub PushBigString(ByVal [Text] As String)
        Dim strArray As String() = SplitStringEveryNth([Text], &H63)
        Dim i As Integer
        For i = 0 To strArray.Length - 1
            Dim arguments As InputArgument() = New InputArgument() {[Text].Substring((i * &H63), strArray(i).Length)}
            Native.Function.Call(Hash._0x6C188BE134E074AA, arguments)
        Next i
    End Sub

    Private Shared Function SplitStringEveryNth(ByVal [text] As String, ByVal Nth As Integer) As String()
        Dim list As New List(Of String)
        Dim item As String = ""
        Dim num As Integer = 0
        Dim i As Integer
        For i = 0 To [text].Length - 1
            item = (item & [text].Chars(i).ToString)
            num += 1
            If ((i <> 0) AndAlso (num = Nth)) Then
                list.Add(item)
                item = ""
                num = 0
            End If
        Next i
        If (item <> "") Then
            list.Add(item)
        End If
        Return list.ToArray
    End Function

    Public Shared Function GetClassDisplayName(vehicleClass As VehicleClass) As String
        Return Game.GetGXTEntry("VEH_CLASS_" + CInt(vehicleClass).ToString())
    End Function

    Public Shared Function GetPrice(itemhash As Integer, vehhash As Integer) As Integer
        Return Native.Function.Call(Of Integer)(Hash._NETWORK_SHOP_GET_PRICE, vehhash, itemhash, True)
    End Function

    Public Shared Sub CreateTitleNames()
        Try
            Dim langConf As ScriptSettings = ScriptSettings.Load("scripts\BennysLang.ini")
            langConf.SetValue(Of String)("TITLE", "AERIALS", Game.GetGXTEntry("CMM_MOD_ST18"))
            langConf.SetValue(Of String)("TITLE", "BODYWORK", Game.GetGXTEntry("CMOD_BW_T"))
            langConf.SetValue(Of String)("TITLE", "DOORS", Game.GetGXTEntry("CMM_MOD_ST6"))
            langConf.SetValue(Of String)("TITLE", "ENGINE", Game.GetGXTEntry("CMM_MOD_GT3"))
            langConf.SetValue(Of String)("TITLE", "INTERIOR", Game.GetGXTEntry("CMM_MOD_GT1"))
            langConf.SetValue(Of String)("TITLE", "BUMPERS", Game.GetGXTEntry("CMOD_BUM_T"))
            langConf.SetValue(Of String)("TITLE", "WHEELS", Game.GetGXTEntry("CMOD_WHE0_T"))
            langConf.SetValue(Of String)("TITLE", "WHEELTYPE", Game.GetGXTEntry("CMOD_WHE1_T"))
            langConf.SetValue(Of String)("TITLE", "TIRES", Game.GetGXTEntry("CMOD_TYR_T"))
            langConf.SetValue(Of String)("TITLE", "PLATES", Game.GetGXTEntry("CMM_MOD_GT2"))
            langConf.SetValue(Of String)("TITLE", "TRIM", Game.GetGXTEntry("CMM_MOD_ST19"))
            langConf.SetValue(Of String)("TITLE", "LIGHTS", Game.GetGXTEntry("CMOD_LGT_T"))
            langConf.SetValue(Of String)("TITLE", "ENGINEBLOCK", Game.GetGXTEntry("CMOD_EB_T"))
            langConf.SetValue(Of String)("TITLE", "AIRFILTER", Game.GetGXTEntry("CMM_MOD_ST15"))
            langConf.SetValue(Of String)("TITLE", "RESPRAY", Game.GetGXTEntry("CMOD_COL0_T"))
            langConf.SetValue(Of String)("TITLE", "STRUTS", Game.GetGXTEntry("CMM_MOD_ST16"))
            langConf.SetValue(Of String)("TITLE", "COLUMNSHIFTERLEVERS", Game.GetGXTEntry("CMM_MOD_ST9"))
            langConf.SetValue(Of String)("TITLE", "DASHBOARD", Game.GetGXTEntry("CMM_MOD_ST4"))
            langConf.SetValue(Of String)("TITLE", "DIALDESIGN", Game.GetGXTEntry("CMM_MOD_ST5"))
            langConf.SetValue(Of String)("TITLE", "ORNAMENTS", Game.GetGXTEntry("CMM_MOD_ST3"))
            langConf.SetValue(Of String)("TITLE", "SEATS", Game.GetGXTEntry("CMM_MOD_ST7"))
            langConf.SetValue(Of String)("TITLE", "STEERINGWHEELS", Game.GetGXTEntry("CMM_MOD_ST8"))
            langConf.SetValue(Of String)("TITLE", "TRIMDESIGN", Game.GetGXTEntry("CMM_MOD_ST2"))
            langConf.SetValue(Of String)("TITLE", "DOORS2", Game.GetGXTEntry("CMM_MOD_ST6"))
            langConf.SetValue(Of String)("TITLE", "WINDOWS", Game.GetGXTEntry("CMM_MOD_ST21"))
            langConf.SetValue(Of String)("TITLE", "FRONTBUMPERS", Game.GetGXTEntry("CMOD_BUMF_T"))
            langConf.SetValue(Of String)("TITLE", "REARBUMPERS", Game.GetGXTEntry("CMOD_BUMR_T"))
            langConf.SetValue(Of String)("TITLE", "SIDESKIRT", Game.GetGXTEntry("CMOD_SS_T"))
            langConf.SetValue(Of String)("TITLE", "PLATEHOLDERS", Game.GetGXTEntry("CMOD_PLH_T"))
            langConf.SetValue(Of String)("TITLE", "VANITYPLATES", Game.GetGXTEntry("CMM_MOD_ST1"))
            langConf.SetValue(Of String)("TITLE", "HEADLIGHTS", Game.GetGXTEntry("CMOD_HED_T"))
            langConf.SetValue(Of String)("TITLE", "ARCHCOVERS", Game.GetGXTEntry("CMM_MOD_ST17"))
            langConf.SetValue(Of String)("TITLE", "EXHAUST", Game.GetGXTEntry("CMOD_EXH_T"))
            langConf.SetValue(Of String)("TITLE", "FENDER", Game.GetGXTEntry("CMOD_WNG_T"))
            langConf.SetValue(Of String)("TITLE", "RIGHTFENDER", Game.GetGXTEntry("CMOD_WNG_T"))
            langConf.SetValue(Of String)("TITLE", "ROLLCAGE", Game.GetGXTEntry("CMOD_RC_T"))
            langConf.SetValue(Of String)("TITLE", "GRILLES", Game.GetGXTEntry("CMOD_GRL_T"))
            langConf.SetValue(Of String)("TITLE", "HOOD", Game.GetGXTEntry("CMOD_BON_T"))
            langConf.SetValue(Of String)("TITLE", "HORN", Game.GetGXTEntry("CMOD_HRN_T"))
            langConf.SetValue(Of String)("TITLE", "HYDRAULICS", Game.GetGXTEntry("CMM_MOD_ST13"))
            langConf.SetValue(Of String)("TITLE", "LIVERY", Game.GetGXTEntry("CMM_MOD_ST23"))
            langConf.SetValue(Of String)("TITLE", "PLAQUES", Game.GetGXTEntry("CMM_MOD_ST10"))
            langConf.SetValue(Of String)("TITLE", "ROOF", Game.GetGXTEntry("CMOD_ROF_T"))
            langConf.SetValue(Of String)("TITLE", "SPEAKERS", Game.GetGXTEntry("CMM_MOD_S11"))
            langConf.SetValue(Of String)("TITLE", "SPOILER", Game.GetGXTEntry("CMOD_SPO_T"))
            langConf.SetValue(Of String)("TITLE", "TANK", Game.GetGXTEntry("CMM_MOD_ST20"))
            langConf.SetValue(Of String)("TITLE", "TRUNKS", Game.GetGXTEntry("CMOD_TR_T"))
            langConf.SetValue(Of String)("TITLE", "TURBO", Game.GetGXTEntry("CMOD_TUR_T"))
            langConf.SetValue(Of String)("TITLE", "SUSPENSIONS", Game.GetGXTEntry("CMOD_SUS_T"))
            langConf.SetValue(Of String)("TITLE", "ARMOR", Game.GetGXTEntry("CMOD_ARM_T"))
            langConf.SetValue(Of String)("TITLE", "BRAKES", Game.GetGXTEntry("CMOD_BRA_T"))
            langConf.SetValue(Of String)("TITLE", "TRANSMISSION", Game.GetGXTEntry("CMOD_GBX_T"))
            langConf.SetValue(Of String)("TITLE", "LICENSE", Game.GetGXTEntry("CMOD_MOD_PLA2").ToUpper)
            langConf.SetValue(Of String)("TITLE", "NEONKITS", Game.GetGXTEntry("CMOD_MOD_LGT_N").ToUpper)
            langConf.SetValue(Of String)("TITLE", "NEONLAYOUT", Game.GetGXTEntry("CMOD_NEON_0").ToUpper)
            langConf.SetValue(Of String)("TITLE", "BIKEWHEELS", Helper.GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper)
            langConf.SetValue(Of String)("TITLE", "HIGHEND", Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper)
            langConf.SetValue(Of String)("TITLE", "LOWRIDER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper)
            langConf.SetValue(Of String)("TITLE", "MUSCLE", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper)
            langConf.SetValue(Of String)("TITLE", "OFFROAD", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper)
            langConf.SetValue(Of String)("TITLE", "SPORT", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper)
            langConf.SetValue(Of String)("TITLE", "SUV", Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper)
            langConf.SetValue(Of String)("TITLE", "TUNER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper)
            langConf.SetValue(Of String)("TITLE", "BENNYS", Helper.GetLocalizedWheelTypeName(8).ToUpper)
            langConf.SetValue(Of String)("TITLE", "BESPOKE", Helper.GetLocalizedWheelTypeName(9).ToUpper)
            langConf.SetValue(Of String)("TITLE", "LIGHTCOLOR", Game.GetGXTEntry("CMM_MOD_ST26").ToUpper)
            langConf.SetValue(Of String)("TITLE", "PRIMARYCOLOR", Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
            langConf.SetValue(Of String)("TITLE", "SECONDARYCOLOR", Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
            langConf.SetValue(Of String)("TITLE", "LIVERYCOLOR", Game.GetGXTEntry("CMOD_COL0_4").ToUpper)
            langConf.SetValue(Of String)("TITLE", "COLORGROUPS", Game.GetGXTEntry("CMOD_COL1_T"))
            langConf.SetValue(Of String)("TITLE", "WHEELCOLORS", Game.GetGXTEntry("CMOD_COL5_T"))
            langConf.SetValue(Of String)("TITLE", "TINTS", Game.GetGXTEntry("CMOD_WIN_T"))
            langConf.SetValue(Of String)("TITLE", "TRIMCOLOR", Game.GetGXTEntry("CMOD_MOD_TRIM2").ToUpper)
            langConf.SetValue(Of String)("TITLE", "NEONCOLOR", Game.GetGXTEntry("CMOD_NEON_1").ToUpper)
            langConf.SetValue(Of String)("TITLE", "TIRESMOKE", Game.GetGXTEntry("CMOD_MOD_TYR3").ToUpper)
            langConf.SetValue(Of String)("TITLE", "CATEGORIES", Game.GetGXTEntry("CMOD_MOD_T"))
            'Biker
            langConf.SetValue(Of String)("TITLE", "SHIFTER", Game.GetGXTEntry("CMOD_SHIFTER_T"))
            langConf.SetValue(Of String)("TITLE", "FRONTMUDGUARD", Game.GetGXTEntry("CMOD_FMUD_T"))
            langConf.SetValue(Of String)("TITLE", "REARMUDGUARD", Game.GetGXTEntry("CMOD_RMUD_T"))
            langConf.SetValue(Of String)("TITLE", "OILTANK", Game.GetGXTEntry("CMM_MOD_ST29"))
            langConf.SetValue(Of String)("TITLE", "FUELTANK", Game.GetGXTEntry("CMOD_FUL_T"))
            langConf.SetValue(Of String)("TITLE", "BELTDRIVECOVER", Game.GetGXTEntry("CMOD_MOD_BLT").ToUpper)
            langConf.Save()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub SaveTitleNames()
        Dim langConf As ScriptSettings = ScriptSettings.Load("scripts\BennysLang.ini")
        If langConf.GetValue("TITLE", "AERIALS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "AERIALS", Game.GetGXTEntry("CMM_MOD_ST18"))
        If langConf.GetValue("TITLE", "BODYWORK") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BODYWORK", Game.GetGXTEntry("CMOD_BW_T"))
        If langConf.GetValue("TITLE", "DOORS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "DOORS", Game.GetGXTEntry("CMM_MOD_ST6"))
        If langConf.GetValue("TITLE", "ENGINE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ENGINE", Game.GetGXTEntry("CMM_MOD_GT3"))
        If langConf.GetValue("TITLE", "INTERIOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "INTERIOR", Game.GetGXTEntry("CMM_MOD_GT1"))
        If langConf.GetValue("TITLE", "BUMPERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BUMPERS", Game.GetGXTEntry("CMOD_BUM_T"))
        If langConf.GetValue("TITLE", "WHEELS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "WHEELS", Game.GetGXTEntry("CMOD_WHE0_T"))
        If langConf.GetValue("TITLE", "WHEELTYPE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "WHEELTYPE", Game.GetGXTEntry("CMOD_WHE1_T"))
        If langConf.GetValue("TITLE", "TIRES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TIRES", Game.GetGXTEntry("CMOD_TYR_T"))
        If langConf.GetValue("TITLE", "PLATES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "PLATES", Game.GetGXTEntry("CMM_MOD_GT2"))
        If langConf.GetValue("TITLE", "TRIM") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TRIM", Game.GetGXTEntry("CMM_MOD_ST19"))
        If langConf.GetValue("TITLE", "LIGHTS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "LIGHTS", Game.GetGXTEntry("CMOD_LGT_T"))
        If langConf.GetValue("TITLE", "ENGINEBLOCK") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ENGINEBLOCK", Game.GetGXTEntry("CMOD_EB_T"))
        If langConf.GetValue("TITLE", "AIRFILTER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "AIRFILTER", Game.GetGXTEntry("CMM_MOD_ST15"))
        If langConf.GetValue("TITLE", "RESPRAY") = "NULL" Then langConf.SetValue(Of String)("TITLE", "RESPRAY", Game.GetGXTEntry("CMOD_COL0_T"))
        If langConf.GetValue("TITLE", "STRUTS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "STRUTS", Game.GetGXTEntry("CMM_MOD_ST16"))
        If langConf.GetValue("TITLE", "COLUMNSHIFTERLEVERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "COLUMNSHIFTERLEVERS", Game.GetGXTEntry("CMM_MOD_ST9"))
        If langConf.GetValue("TITLE", "DASHBOARD") = "NULL" Then langConf.SetValue(Of String)("TITLE", "DASHBOARD", Game.GetGXTEntry("CMM_MOD_ST4"))
        If langConf.GetValue("TITLE", "DIALDESIGN") = "NULL" Then langConf.SetValue(Of String)("TITLE", "DIALDESIGN", Game.GetGXTEntry("CMM_MOD_ST5"))
        If langConf.GetValue("TITLE", "ORNAMENTS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ORNAMENTS", Game.GetGXTEntry("CMM_MOD_ST3"))
        If langConf.GetValue("TITLE", "SEATS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SEATS", Game.GetGXTEntry("CMM_MOD_ST7"))
        If langConf.GetValue("TITLE", "STEERINGWHEELS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "STEERINGWHEELS", Game.GetGXTEntry("CMM_MOD_ST8"))
        If langConf.GetValue("TITLE", "TRIMDESIGN") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TRIMDESIGN", Game.GetGXTEntry("CMM_MOD_ST2"))
        If langConf.GetValue("TITLE", "DOORS2") = "NULL" Then langConf.SetValue(Of String)("TITLE", "DOORS2", Game.GetGXTEntry("CMM_MOD_ST6"))
        If langConf.GetValue("TITLE", "WINDOWS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "WINDOWS", Game.GetGXTEntry("CMM_MOD_ST21"))
        If langConf.GetValue("TITLE", "FRONTBUMPERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "FRONTBUMPERS", Game.GetGXTEntry("CMOD_BUMF_T"))
        If langConf.GetValue("TITLE", "REARBUMPERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "REARBUMPERS", Game.GetGXTEntry("CMOD_BUMR_T"))
        If langConf.GetValue("TITLE", "SIDESKIRT") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SIDESKIRT", Game.GetGXTEntry("CMOD_SS_T"))
        If langConf.GetValue("TITLE", "PLATEHOLDERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "PLATEHOLDERS", Game.GetGXTEntry("CMOD_PLH_T"))
        If langConf.GetValue("TITLE", "VANITYPLATES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "VANITYPLATES", Game.GetGXTEntry("CMM_MOD_ST1"))
        If langConf.GetValue("TITLE", "HEADLIGHTS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "HEADLIGHTS", Game.GetGXTEntry("CMOD_HED_T"))
        If langConf.GetValue("TITLE", "ARCHCOVERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ARCHCOVERS", Game.GetGXTEntry("CMM_MOD_ST17"))
        If langConf.GetValue("TITLE", "EXHAUST") = "NULL" Then langConf.SetValue(Of String)("TITLE", "EXHAUST", Game.GetGXTEntry("CMOD_EXH_T"))
        If langConf.GetValue("TITLE", "FENDER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "FENDER", Game.GetGXTEntry("CMOD_WNG_T"))
        If langConf.GetValue("TITLE", "RIGHTFENDER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "RIGHTFENDER", Game.GetGXTEntry("CMOD_WNG_T"))
        If langConf.GetValue("TITLE", "ROLLCAGE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ROLLCAGE", Game.GetGXTEntry("CMOD_RC_T"))
        If langConf.GetValue("TITLE", "GRILLES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "GRILLES", Game.GetGXTEntry("CMOD_GRL_T"))
        If langConf.GetValue("TITLE", "HOOD") = "NULL" Then langConf.SetValue(Of String)("TITLE", "HOOD", Game.GetGXTEntry("CMOD_BON_T"))
        If langConf.GetValue("TITLE", "HORN") = "NULL" Then langConf.SetValue(Of String)("TITLE", "HORN", Game.GetGXTEntry("CMOD_HRN_T"))
        If langConf.GetValue("TITLE", "HYDRAULICS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "HYDRAULICS", Game.GetGXTEntry("CMM_MOD_ST13"))
        If langConf.GetValue("TITLE", "LIVERY") = "NULL" Then langConf.SetValue(Of String)("TITLE", "LIVERY", Game.GetGXTEntry("CMM_MOD_ST23"))
        If langConf.GetValue("TITLE", "PLAQUES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "PLAQUES", Game.GetGXTEntry("CMM_MOD_ST10"))
        If langConf.GetValue("TITLE", "ROOF") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ROOF", Game.GetGXTEntry("CMOD_ROF_T"))
        If langConf.GetValue("TITLE", "SPEAKERS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SPEAKERS", Game.GetGXTEntry("CMM_MOD_S11"))
        If langConf.GetValue("TITLE", "SPOILER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SPOILER", Game.GetGXTEntry("CMOD_SPO_T"))
        If langConf.GetValue("TITLE", "TANK") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TANK", Game.GetGXTEntry("CMM_MOD_ST20"))
        If langConf.GetValue("TITLE", "TRUNKS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TRUNKS", Game.GetGXTEntry("CMOD_TR_T"))
        If langConf.GetValue("TITLE", "TURBO") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TURBO", Game.GetGXTEntry("CMOD_TUR_T"))
        If langConf.GetValue("TITLE", "SUSPENSIONS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SUSPENSIONS", Game.GetGXTEntry("CMOD_SUS_T"))
        If langConf.GetValue("TITLE", "ARMOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "ARMOR", Game.GetGXTEntry("CMOD_ARM_T"))
        If langConf.GetValue("TITLE", "BRAKES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BRAKES", Game.GetGXTEntry("CMOD_BRA_T"))
        If langConf.GetValue("TITLE", "TRANSMISSION") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TRANSMISSION", Game.GetGXTEntry("CMOD_GBX_T"))
        If langConf.GetValue("TITLE", "LICENSE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "LICENSE", Game.GetGXTEntry("CMOD_MOD_PLA2").ToUpper)
        If langConf.GetValue("TITLE", "NEONKITS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "NEONKITS", Game.GetGXTEntry("CMOD_MOD_LGT_N").ToUpper)
        If langConf.GetValue("TITLE", "NEONLAYOUT") = "NULL" Then langConf.SetValue(Of String)("TITLE", "NEONLAYOUT", Game.GetGXTEntry("CMOD_NEON_0").ToUpper)
        If langConf.GetValue("TITLE", "BIKEWHEELS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BIKEWHEELS", Helper.GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper)
        If langConf.GetValue("TITLE", "HIGHEND") = "NULL" Then langConf.SetValue(Of String)("TITLE", "HIGHEND", Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper)
        If langConf.GetValue("TITLE", "LOWRIDER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "LOWRIDER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper)
        If langConf.GetValue("TITLE", "MUSCLE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "MUSCLE", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper)
        If langConf.GetValue("TITLE", "OFFROAD") = "NULL" Then langConf.SetValue(Of String)("TITLE", "OFFROAD", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper)
        If langConf.GetValue("TITLE", "SPORT") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SPORT", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper)
        If langConf.GetValue("TITLE", "SUV") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SUV", Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper)
        If langConf.GetValue("TITLE", "TUNER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TUNER", Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper)
        If langConf.GetValue("TITLE", "BENNYS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BENNYS", Helper.GetLocalizedWheelTypeName(8).ToUpper)
        If langConf.GetValue("TITLE", "BESPOKE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BESPOKE", Helper.GetLocalizedWheelTypeName(9).ToUpper)
        If langConf.GetValue("TITLE", "LIGHTCOLOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "LIGHTCOLOR", Game.GetGXTEntry("CMM_MOD_ST26").ToUpper)
        If langConf.GetValue("TITLE", "PRIMARYCOLOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "PRIMARYCOLOR", Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        If langConf.GetValue("TITLE", "SECONDARYCOLOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SECONDARYCOLOR", Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        If langConf.GetValue("TITLE", "LIVERYCOLOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "LIVERYCOLOR", Game.GetGXTEntry("CMOD_COL0_4").ToUpper)
        If langConf.GetValue("TITLE", "COLORGROUPS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "COLORGROUPS", Game.GetGXTEntry("CMOD_COL1_T"))
        If langConf.GetValue("TITLE", "WHEELCOLORS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "WHEELCOLORS", Game.GetGXTEntry("CMOD_COL5_T"))
        If langConf.GetValue("TITLE", "TINTS") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TINTS", Game.GetGXTEntry("CMOD_WIN_T"))
        If langConf.GetValue("TITLE", "TRIMCOLOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TRIMCOLOR", Game.GetGXTEntry("CMOD_MOD_TRIM2").ToUpper)
        If langConf.GetValue("TITLE", "NEONCOLOR") = "NULL" Then langConf.SetValue(Of String)("TITLE", "NEONCOLOR", Game.GetGXTEntry("CMOD_NEON_1").ToUpper)
        If langConf.GetValue("TITLE", "TIRESMOKE") = "NULL" Then langConf.SetValue(Of String)("TITLE", "TIRESMOKE", Game.GetGXTEntry("CMOD_MOD_TYR3").ToUpper)
        If langConf.GetValue("TITLE", "CATEGORIES") = "NULL" Then langConf.SetValue(Of String)("TITLE", "CATEGORIES", Game.GetGXTEntry("CMOD_MOD_T"))
        'Bikers
        If langConf.GetValue("TITLE", "SHIFTER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "SHIFTER", Game.GetGXTEntry("CMOD_SHIFTER_T"))
        If langConf.GetValue("TITLE", "FRONTMUDGUARD") = "NULL" Then langConf.SetValue(Of String)("TITLE", "FRONTMUDGUARD", Game.GetGXTEntry("CMOD_FMUD_T"))
        If langConf.GetValue("TITLE", "REARMUDGUARD") = "NULL" Then langConf.SetValue(Of String)("TITLE", "REARMUDGUARD", Game.GetGXTEntry("CMOD_RMUD_T"))
        If langConf.GetValue("TITLE", "OILTANK") = "NULL" Then langConf.SetValue(Of String)("TITLE", "OILTANK", Game.GetGXTEntry("CMM_MOD_ST29"))
        If langConf.GetValue("TITLE", "FUELTANK") = "NULL" Then langConf.SetValue(Of String)("TITLE", "FUELTANK", Game.GetGXTEntry("CMOD_FUL_T"))
        If langConf.GetValue("TITLE", "BELTDRIVECOVER") = "NULL" Then langConf.SetValue(Of String)("TITLE", "BELTDRIVECOVER", Game.GetGXTEntry("CMOD_MOD_BLT").ToUpper)
        'If langConf.GetValue("TITLE", "") = "NULL"  Then langConf.SetValue(Of String)("TITLE", "", Game.GetGXTEntry(""))
        'If langConf.GetValue("TITLE", "") = "NULL"  Then langConf.SetValue(Of String)("TITLE", "", Game.GetGXTEntry(""))
        langConf.Save()
    End Sub
End Class
