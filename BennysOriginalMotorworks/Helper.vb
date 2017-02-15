Imports System.Runtime.InteropServices
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

        Return New Vehicle([Function].[Call](Of Integer)(Hash.CREATE_VEHICLE, model.Hash, position.X, position.Y, position.Z, heading,
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
            Case "diabolus"
                result = "diabolus2"
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
                result = "moonbeam"
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

    Public Shared Function GetAccentColor(vehicle As Vehicle) As VehicleColor
        Dim arg As New OutputArgument()
        Native.Function.Call(&HB7635E80A5C31BFFUL, vehicle, arg)
        Return arg.GetResult(Of VehicleColor)()
    End Function

    Public Shared Sub SetAccentColor(vehicle As Vehicle, color As VehicleColor)
        Native.Function.Call(&H6089CDF6A57F326C, vehicle, color)
    End Sub
End Class
