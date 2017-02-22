Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports System.Text.RegularExpressions.Regex
Imports System.Text

Public Class BennysMenu
    Inherits Script

    Public Shared lowriders As List(Of Model) = New List(Of Model) From {"banshee", "Buccaneer", "chino", "diabolus", "comet2", "faction", "faction2", "fcr", "italigtb", "minivan", "moonbeam", "nero", "primo", "sabregt",
        "slamvan", "specter", "sultan", "tornado", "tornado2", "tornado3", "virgo3", "voodoo2", "elegy2"}
    Public Shared bennysvehicle As List(Of Model) = New List(Of Model) From {"banshee2", "buccaneer2", "chino2", "diabolus2", "comet", "faction2", "faction3", "fcr2", "italigtb2", "minivan2", "moonbeam2", "nero2", "primo2",
        "sabregt2", "specter2", "sultanrs", "tornado5", "virgo2", "voodoo", "elegy"}
    Public Shared tyres As String() = New String() {"Stock", "Thin White", "White", "Fat White", "Red", "Blue", "Atomic"}
    Public Shared MainMenu, gmBodywork, gmEngine, gmInterior, gmPlate, gmLights, gmRespray, gmWheels, gmBumper, gmWheelType, gmNeonKits, gmDoor As UIMenu
    Public Shared mAerials, mSuspension, mArmor, mBrakes, mEngine, mTransmission, mFBumper, mRBumper, mSSkirt, mTrim, mEngineBlock, mAirFilter, mStruts, mColumnShifterLevers, mDashboard, mDialDesign, mOrnaments, mSeats,
        mSteeringWheels, mTrimDesign, mPlateHolder, mVanityPlates, mNumberPlate, mBikeWheels, mHighEnd, mLowrider, mMuscle, mOffroad, mSport, mSUV, mTuner, mBennysOriginals, mBespoke, mTires, mHeadlights, mNeon, mNeonColor,
    mArchCover, mExhaust, mFender, mRFender, mDoor, mFrame, mGrille, mHood, mHorn, mHydraulics, mLivery, mPlaques, mRoof, mSpeakers, mSpoilers, mTank, mTrunk, mWindow, mTurbo, mTint, mLightsColor, mTrimColor, mRimColor,
    mPrimaryClassicColor, mPrimaryChromeColor, mPrimaryMetallicColor, mPrimaryMetalsColor, mPrimaryMatteColor, mPrimaryPearlescentColor, mPrimaryColor, mSecondaryColor, mSecondaryClassicColor, mSecondaryChromeColor,
    mSecondaryMetallicColor, mSecondaryMetalsColor, mSecondaryMatteColor, mTireSmoke As UIMenu
    Public Shared iRepair, iHorn, iArmor, iBrakes, iFBumper, iExhaust, iFender, iRollcage, iRoof, iTransmission, iEngine, iPlate, iLights, iTint, iTurbo, iRespray, iWheels, iSuspension, iEngineBlock, iAerials, iAirFilter,
        iArchCover, iDoor, iFrame, iGrille, iHood, iHydraulics, iLivery, iPlaques, iRFender, iSpeaker, iSpoilers, iTank, iTrunk, iWindows, iTrim, iUpgrade, iStruts, iTrimColor, iColumnShifterLevers, iDashboard, iDialDesign,
        iOrnaments, iSeats, iSteeringWheels, iTrimDesign, iRBumper, iSideSkirt, iRimColor, iPlateHolder, iVanityPlates, iHeadlights, iDashboardColor, iNumberPlate, iBikeWheels, iHighEnd, iLowrider, iMuscle, iOffroad,
        iSport, iSUV, iTuner, iBennys, iBespoke, iTires, iNeon, iTireSmoke, iNeonColor, iLightsColor, iPrimaryCol, iSecondaryCol, iPrimaryChromeColor, iPrimaryClassicColor, iPrimaryMetallicColor, iPrimaryMetalsColor,
        iPrimaryMatteColor, iPrimaryPearlescentColor, iSecondaryChromeColor, iSecondaryClassicColor, iSecondaryMetallicColor, iSecondaryMetalsColor, iSecondaryMatteColor, iSecondaryPearlescentColor As UIMenuItem
    Public Shared giBodywork, giEngine, giInterior, giPlate, giLights, giRespray, giWheels, giBumper, giWheelType, giTires, giNeonKits, giPrimaryCol, giSecondaryCol, giBikeWheels, giHighEndWheels, giDoor,
        giLowriderWheels, giMuscleWheels, giOffroadWheels, giSportWheels, giSUVWheels, giTunerWheels, giBennysWheels, giBespokeWheels, giFBumper, giRBumper, giSSkirt, giNumberPlate, giVanityPlate, giPlateHolder,
        giExhaust, giBrakes, giGrille, giHood, giHydraulics, giPlaques, giSpoilers, giTank As UIMenuItem
    Public Shared _menuPool As MenuPool
    Public Shared camera As WorkshopCamera

    Public Shared Sub CreateMainMenu()
        Try
            MainMenu = New UIMenu("", Helper.LocalizeModTitleName("CATEGORIES"))
            MainMenu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            MainMenu.MouseEdgeEnabled = False
            _menuPool.Add(MainMenu)
            MainMenu.AddItem(New UIMenuItem("Noting"))
            MainMenu.RefreshIndex()
            AddHandler MainMenu.OnMenuClose, AddressOf MainMenuCloseHandler
            AddHandler MainMenu.OnItemSelect, AddressOf MainMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshMainMenu()
        Try
            MainMenu.MenuItems.Clear()

            If Bennys.veh.IsDamaged Then
                iRepair = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Repair), Game.GetGXTEntry("CMOD_MOD_0_D")) 'Repair
                MainMenu.AddItem(iRepair)
                MainMenu.RefreshIndex()
            Else
                'Specials
                If lowriders.Contains(Bennys.veh.Model) Then
                    iUpgrade = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Upgrade), Game.GetGXTEntry("CMOD_MOD_100_D")) 'Upgrade
                    MainMenu.AddItem(iUpgrade)
                End If

                'Groups
                If (Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Windows) <> 0 Or Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0) Then
                    giBodywork = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Bodyworks), Game.GetGXTEntry("IE_BO_DT1")) 'bodywork
                    MainMenu.AddItem(giBodywork)
                    MainMenu.BindMenuToItem(gmBodywork, giBodywork)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Or Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Struts) <> 0) Then
                    giEngine = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Engine), Game.GetGXTEntry("CMOD_SMOD_2_D")) 'engine
                    MainMenu.AddItem(giEngine)
                    MainMenu.BindMenuToItem(gmEngine, giEngine)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Or Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 _
                        Or Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Or Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Or Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 _
                        Or Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0) Then
                    giInterior = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Interior), Game.GetGXTEntry("SMOD_IN_1")) 'interior
                    MainMenu.AddItem(giInterior)
                    MainMenu.BindMenuToItem(gmInterior, giInterior)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0) Then
                    giBumper = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Bumpers), Game.GetGXTEntry("CMOD_MOD_4_D")) 'bumper
                    MainMenu.AddItem(giBumper)
                    MainMenu.BindMenuToItem(gmBumper, giBumper)
                End If
                giPlate = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Plate), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Plate
                MainMenu.AddItem(giPlate)
                MainMenu.BindMenuToItem(gmPlate, giPlate)
                giWheels = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Wheels), Game.GetGXTEntry("CMOD_MOD_60_D"))
                MainMenu.AddItem(giWheels)
                MainMenu.BindMenuToItem(gmWheels, giWheels)
                giLights = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Lights), Game.GetGXTEntry("CMOD_MOD_15_D"))  'CMOD_MOD_47_D
                MainMenu.AddItem(giLights)
                MainMenu.BindMenuToItem(gmLights, giLights)
                giRespray = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Respray), Game.GetGXTEntry("CMOD_MOD_6_D"))
                MainMenu.AddItem(giRespray)
                MainMenu.BindMenuToItem(gmRespray, giRespray)

                'Single Item
                If Bennys.veh.GetModCount(VehicleMod.Armor) <> 0 Then
                    iArmor = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Armor), Game.GetGXTEntry("CMOD_MOD_1_D"))
                    MainMenu.AddItem(iArmor)
                    MainMenu.BindMenuToItem(mArmor, iArmor)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Brakes) <> 0 Then
                    giBrakes = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Brakes), Game.GetGXTEntry("CMOD_MOD_3_D"))
                    MainMenu.AddItem(giBrakes)
                    MainMenu.BindMenuToItem(mBrakes, giBrakes)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Exhaust) <> 0 Then
                    giExhaust = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Exhaust), Game.GetGXTEntry("CMOD_MOD_16_D"))
                    MainMenu.AddItem(giExhaust)
                    MainMenu.BindMenuToItem(mExhaust, giExhaust)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Fender) <> 0 Then
                    iFender = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Fender), Game.GetGXTEntry("CMOD_MOD_9_D"))
                    MainMenu.AddItem(iFender)
                    MainMenu.BindMenuToItem(mFender, iFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Grille) <> 0 Then
                    giGrille = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Grille), Game.GetGXTEntry("SMOD_CHASS_2c"))
                    MainMenu.AddItem(giGrille)
                    MainMenu.BindMenuToItem(mGrille, giGrille)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hood) <> 0 Then
                    giHood = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Hood), Game.GetGXTEntry("CMOD_MOD_72_D"))
                    MainMenu.AddItem(giHood)
                    MainMenu.BindMenuToItem(mHood, giHood)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Horns) <> 0 Then
                    iHorn = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Horns), Game.GetGXTEntry("CMOD_MOD_14_D"))
                    MainMenu.AddItem(iHorn)
                    MainMenu.BindMenuToItem(mHorn, iHorn)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hydraulics) <> 0 Then
                    giHydraulics = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Hydraulics), Game.GetGXTEntry("CMOD_SMOD_5_D"))
                    MainMenu.AddItem(giHydraulics)
                    MainMenu.BindMenuToItem(mHydraulics, giHydraulics)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Livery) <> 0 Then
                    iLivery = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Livery), Game.GetGXTEntry("CMOD_SMOD_6_D"))
                    MainMenu.AddItem(iLivery)
                    MainMenu.BindMenuToItem(mLivery, iLivery)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                    giPlaques = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Plaques), Game.GetGXTEntry("SMOD_IN_PLAQUE"))
                    MainMenu.AddItem(giPlaques)
                    MainMenu.BindMenuToItem(mPlaques, giPlaques)
                End If
                If Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Then
                    iRFender = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.RightFender), Game.GetGXTEntry("CMOD_MOD_9_D"))
                    MainMenu.AddItem(iRFender)
                    MainMenu.BindMenuToItem(mRFender, iRFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Then
                    iRoof = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Roof), Game.GetGXTEntry("CMOD_MOD_73_D"))
                    MainMenu.AddItem(iRoof)
                    MainMenu.BindMenuToItem(mRoof, iRoof)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Spoilers) <> 0 Then
                    giSpoilers = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Spoilers), Game.GetGXTEntry("CMOD_MOD_37_D"))
                    MainMenu.AddItem(giSpoilers)
                    MainMenu.BindMenuToItem(mSpoilers, giSpoilers)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Suspension) <> 0 Then
                    iSuspension = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Suspension), Game.GetGXTEntry("CMOD_MOD_24_D"))
                    MainMenu.AddItem(iSuspension)
                    MainMenu.BindMenuToItem(mSuspension, iSuspension)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                    giTank = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Tank), Game.GetGXTEntry("CMOD_MOD_45_D"))
                    MainMenu.AddItem(giTank)
                    MainMenu.BindMenuToItem(mTank, giTank)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Transmission) <> 0 Then
                    iTransmission = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Transmission), Game.GetGXTEntry("CMOD_MOD_26_D"))
                    MainMenu.AddItem(iTransmission)
                    MainMenu.BindMenuToItem(mTransmission, iTransmission)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trunk) <> 0 Then
                    iTrunk = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Trunk), Game.GetGXTEntry("CMOD_MOD_62_D"))
                    MainMenu.AddItem(iTrunk)
                    MainMenu.BindMenuToItem(mTrunk, iTrunk)
                End If
                iTurbo = New UIMenuItem(Helper.LocalizedModTypeName(VehicleToggleMod.Turbo), Game.GetGXTEntry("CMOD_MOD_27_D"))
                MainMenu.AddItem(iTurbo)
                MainMenu.BindMenuToItem(mTurbo, iTurbo)
                iTint = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Windows), Game.GetGXTEntry("CMOD_MOD_29_D"))
                MainMenu.AddItem(iTint)
                MainMenu.BindMenuToItem(mTint, iTint)
                MainMenu.RefreshIndex()
            End If

            MainMenu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuCloseHandler(sender As UIMenu)
        Try
            Game.FadeScreenOut(500)
            Wait(500)
            Bennys.isExiting = True
            camera.Stop()
            Bennys.veh.Position = New Vector3(-205.8678, -1321.805, 30.41191)
            Bennys.ply.Task.DriveTo(Bennys.veh, New Vector3(-205.743, -1303.657, 30.84998), 0.5, 5)
            Wait(500)
            Game.FadeScreenIn(500)
            Wait(5000)
            Bennys.ply.Task.ClearAll()
            Bennys.isExiting = False
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If sender Is MainMenu Then
                If selectedItem Is iRepair Then
                    Bennys.veh.Repair()
                    RefreshMenus()
                ElseIf selectedItem Is iUpgrade Then
                    Game.FadeScreenOut(500)
                    Wait(500)
                    Dim veh As Vehicle = World.CreateVehicle(Helper.LowriderUpgrade(Bennys.veh.Model), Bennys.veh.Position, Bennys.veh.Heading)
                    veh.IsPersistent = False
                    veh.PrimaryColor = Bennys.lastVehMemory.PrimaryColor
                    veh.SecondaryColor = Bennys.lastVehMemory.SecondaryColor
                    veh.DashboardColor = Bennys.lastVehMemory.LightsColor
                    veh.PearlescentColor = Bennys.lastVehMemory.PearlescentColor
                    veh.TrimColor = Bennys.lastVehMemory.TrimColor
                    veh.RimColor = Bennys.lastVehMemory.RimColor
                    veh.NeonLightsColor = Bennys.lastVehMemory.NeonLightsColor
                    veh.TireSmokeColor = Bennys.lastVehMemory.TireSmokeColor
                    veh.InstallModKit()
                    veh.WheelType = Bennys.lastVehMemory.WheelType
                    veh.SetMod(VehicleMod.Aerials, Bennys.lastVehMemory.Aerials, False)
                    veh.SetMod(VehicleMod.AirFilter, Bennys.lastVehMemory.AirFilter, False)
                    veh.SetMod(VehicleMod.ArchCover, Bennys.lastVehMemory.ArchCover, False)
                    veh.SetMod(VehicleMod.Armor, Bennys.lastVehMemory.Armor, False)
                    veh.SetMod(VehicleMod.BackWheels, Bennys.lastVehMemory.BackWheels, False)
                    veh.SetMod(VehicleMod.Brakes, Bennys.lastVehMemory.Brakes, False)
                    veh.SetMod(VehicleMod.ColumnShifterLevers, Bennys.lastVehMemory.ColumnShifterLevers, False)
                    veh.SetMod(VehicleMod.Dashboard, Bennys.lastVehMemory.Dashboard, False)
                    veh.SetMod(VehicleMod.DialDesign, Bennys.lastVehMemory.DialDesign, False)
                    veh.SetMod(VehicleMod.DoorSpeakers, Bennys.lastVehMemory.DoorSpeakers, False)
                    veh.SetMod(VehicleMod.Engine, Bennys.lastVehMemory.Engine, False)
                    veh.SetMod(VehicleMod.EngineBlock, Bennys.lastVehMemory.EngineBlock, False)
                    veh.SetMod(VehicleMod.Exhaust, Bennys.lastVehMemory.Exhaust, False)
                    veh.SetMod(VehicleMod.Fender, Bennys.lastVehMemory.Fender, False)
                    veh.SetMod(VehicleMod.Frame, Bennys.lastVehMemory.Frame, False)
                    veh.SetMod(VehicleMod.FrontBumper, Bennys.lastVehMemory.FrontBumper, False)
                    veh.SetMod(VehicleMod.FrontWheels, Bennys.lastVehMemory.FrontWheels, False)
                    veh.SetMod(VehicleMod.Grille, Bennys.lastVehMemory.Grille, False)
                    veh.SetMod(VehicleMod.Hood, Bennys.lastVehMemory.Hood, False)
                    veh.SetMod(VehicleMod.Horns, Bennys.lastVehMemory.Horns, False)
                    veh.SetMod(VehicleMod.Hydraulics, Bennys.lastVehMemory.Hydraulics, False)
                    veh.SetMod(VehicleMod.Livery, Bennys.lastVehMemory.Livery, False)
                    veh.SetMod(VehicleMod.Ornaments, Bennys.lastVehMemory.Ornaments, False)
                    veh.SetMod(VehicleMod.Plaques, Bennys.lastVehMemory.Plaques, False)
                    veh.SetMod(VehicleMod.PlateHolder, Bennys.lastVehMemory.PlateHolder, False)
                    veh.SetMod(VehicleMod.RearBumper, Bennys.lastVehMemory.RearBumper, False)
                    veh.SetMod(VehicleMod.RightFender, Bennys.lastVehMemory.RightFender, False)
                    veh.SetMod(VehicleMod.Roof, Bennys.lastVehMemory.Roof, False)
                    veh.SetMod(VehicleMod.Seats, Bennys.lastVehMemory.Seats, False)
                    veh.SetMod(VehicleMod.SideSkirt, Bennys.lastVehMemory.SideSkirt, False)
                    veh.SetMod(VehicleMod.Speakers, Bennys.lastVehMemory.Speakers, False)
                    veh.SetMod(VehicleMod.Spoilers, Bennys.lastVehMemory.Spoilers, False)
                    veh.SetMod(VehicleMod.SteeringWheels, Bennys.lastVehMemory.SteeringWheels, False)
                    veh.SetMod(VehicleMod.Struts, Bennys.lastVehMemory.Struts, False)
                    veh.SetMod(VehicleMod.Suspension, Bennys.lastVehMemory.Suspension, False)
                    veh.SetMod(VehicleMod.Tank, Bennys.lastVehMemory.Tank, False)
                    veh.SetMod(VehicleMod.Transmission, Bennys.lastVehMemory.Transmission, False)
                    veh.SetMod(VehicleMod.Trim, Bennys.lastVehMemory.Trim, False)
                    veh.SetMod(VehicleMod.TrimDesign, Bennys.lastVehMemory.TrimDesign, False)
                    veh.SetMod(VehicleMod.Trunk, Bennys.lastVehMemory.Trunk, False)
                    veh.SetMod(VehicleMod.VanityPlates, Bennys.lastVehMemory.VanityPlates, False)
                    veh.SetMod(VehicleMod.Windows, Bennys.lastVehMemory.Windows, False)
                    veh.ToggleMod(VehicleToggleMod.TireSmoke, True)
                    veh.ToggleMod(VehicleToggleMod.Turbo, Bennys.lastVehMemory.Turbo)
                    veh.ToggleMod(VehicleToggleMod.XenonHeadlights, Bennys.lastVehMemory.Headlights)
                    veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
                    veh.NumberPlate = Bennys.lastVehMemory.PlateNumbers
                    Bennys.veh.Delete()
                    Bennys.ply.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                    Bennys.veh = veh
                    veh.InstallModKit()
                    MainMenu.MenuItems.Remove(selectedItem)
                    RefreshMenus()
                    camera.RepositionFor(veh)
                    Wait(500)
                    Game.FadeScreenIn(500)
                    Helper.ScreenEffectStart(Helper.ScreenEffect.RaceTurbo, 1000)
                ElseIf selectedItem Is giEngine Then
                    Bennys.veh.OpenDoor(VehicleDoor.Hood, False, False)
                    camera.MainCameraPosition = CameraPosition.Engine
                ElseIf selectedItem Is giInterior Then
                    camera.MainCameraPosition = CameraPosition.Interior
                ElseIf selectedItem Is giWheels Then
                    camera.MainCameraPosition = CameraPosition.Wheels
                ElseIf selectedItem Is giLights Then
                    Bennys.veh.HighBeamsOn = True
                ElseIf selectedItem Is giExhaust Then
                    Select Case Bennys.veh.Model
                        Case VehicleHash.SultanRS
                            camera.MainCameraPosition = CameraPosition.Wheels
                        Case Else
                            camera.MainCameraPosition = CameraPosition.RearBumper
                    End Select
                ElseIf selectedItem Is giBrakes Then
                    camera.MainCameraPosition = CameraPosition.Wheels
                ElseIf selectedItem Is giGrille Then
                    camera.MainCameraPosition = CameraPosition.Grille
                ElseIf selectedItem Is giHood Then
                    camera.MainCameraPosition = CameraPosition.Engine
                ElseIf selectedItem Is giHydraulics Then
                    Bennys.veh.OpenDoor(VehicleDoor.Trunk, False, False)
                    camera.MainCameraPosition = CameraPosition.Trunk
                ElseIf selectedItem Is giPlaques Then
                    Select Case Bennys.veh.Model
                        Case VehicleHash.SlamVan3, VehicleHash.Buccaneer2
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Case Else
                            camera.MainCameraPosition = CameraPosition.Plaque
                    End Select
                ElseIf selectedItem Is giSpoilers Then
                    Select Case Bennys.veh.Model
                        Case Game.GenerateHash("prototipo")
                            camera.MainCameraPosition = CameraPosition.RearBumper
                        Case Else
                            camera.MainCameraPosition = CameraPosition.Trunk
                    End Select
                ElseIf selectedItem Is giTank Then
                    Select Case Bennys.veh.Model
                        Case VehicleHash.SlamVan3
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Case Game.GenerateHash("elegy")
                            camera.MainCameraPosition = CameraPosition.FrontPlate
                        Case Else
                            camera.MainCameraPosition = CameraPosition.Tank
                    End Select
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBodyworkMenu()
        Try
            gmBodywork = New UIMenu("", Helper.LocalizeModTitleName("BODYWORK")) 'BODYWORK
            gmBodywork.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBodywork.MouseEdgeEnabled = False
            _menuPool.Add(gmBodywork)
            gmBodywork.AddItem(New UIMenuItem("Nothing"))
            gmBodywork.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBodyworkMenu()
        Try
            gmBodywork.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Then
                iAerials = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Aerials), Game.GetGXTEntry("SMOD_CHASS_6"))
                gmBodywork.AddItem(iAerials)
                gmBodywork.BindMenuToItem(mAerials, iAerials)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                iTrim = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Trim), Game.GetGXTEntry("SMOD_CHASS_1b"))
                gmBodywork.AddItem(iTrim)
                gmBodywork.BindMenuToItem(mTrim, iTrim)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Windows) <> 0 Then
                iWindows = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Windows), Game.GetGXTEntry("SMOD_CHASS_5"))
                gmBodywork.AddItem(iWindows)
                gmBodywork.BindMenuToItem(mWindow, iWindows)
            End If
            If Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Then
                iArchCover = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.ArchCover), Game.GetGXTEntry("SMOD_CHASS_1c")) 'Arch Covers
                gmBodywork.AddItem(iArchCover)
                gmBodywork.BindMenuToItem(mArchCover, iArchCover)
            End If
            gmBodywork.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateEngineMenu()
        Try
            gmEngine = New UIMenu("", Helper.LocalizeModTitleName("ENGINE")) 'ENGINE
            gmEngine.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmEngine.MouseEdgeEnabled = False
            _menuPool.Add(gmEngine)
            gmEngine.AddItem(New UIMenuItem("Nothing"))
            gmEngine.RefreshIndex()
            AddHandler gmEngine.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshEngineMenu()
        Try
            gmEngine.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Then
                iEngine = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Engine), Game.GetGXTEntry("SMOD_ENGINE_4"))
                gmEngine.AddItem(iEngine)
                gmEngine.BindMenuToItem(mEngine, iEngine)
            End If
            If Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Then
                iEngineBlock = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.EngineBlock), Game.GetGXTEntry("SMOD_ENGINE_1"))
                gmEngine.AddItem(iEngineBlock)
                gmEngine.BindMenuToItem(mEngineBlock, iEngineBlock)
            End If
            If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                iAirFilter = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.AirFilter), Game.GetGXTEntry("SMOD_ENGINE_2"))
                gmEngine.AddItem(iAirFilter)
                gmEngine.BindMenuToItem(mAirFilter, iAirFilter)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                iStruts = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Struts), Game.GetGXTEntry("SMOD_ENGINE_3b"))
                gmEngine.AddItem(iStruts)
                gmEngine.BindMenuToItem(mStruts, iStruts)
            End If
            gmEngine.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateInteriorMenu()
        Try
            gmInterior = New UIMenu("", Helper.LocalizeModTitleName("INTERIOR")) 'INTERIOR
            gmInterior.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmInterior.MouseEdgeEnabled = False
            _menuPool.Add(gmInterior)
            gmInterior.AddItem(New UIMenuItem("Nothing"))
            gmInterior.RefreshIndex()
            AddHandler gmInterior.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler gmInterior.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshInteriorMenu()
        Try
            gmInterior.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Then
                iColumnShifterLevers = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.ColumnShifterLevers), Game.GetGXTEntry("SMOD_IN_KNOB"))
                gmInterior.AddItem(iColumnShifterLevers)
                gmInterior.BindMenuToItem(mColumnShifterLevers, iColumnShifterLevers)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Then
                iDashboard = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Dashboard), Game.GetGXTEntry("SMOD_IN_2"))
                gmInterior.AddItem(iDashboard)
                gmInterior.BindMenuToItem(mDashboard, iDashboard)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Then
                iDialDesign = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.DialDesign), Game.GetGXTEntry("SMOD_IN_4"))
                gmInterior.AddItem(iDialDesign)
                gmInterior.BindMenuToItem(mDialDesign, iDialDesign)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Then
                iOrnaments = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Ornaments), Game.GetGXTEntry("CMOD_MOD_64_D"))
                gmInterior.AddItem(iOrnaments)
                gmInterior.BindMenuToItem(mOrnaments, iOrnaments)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Then
                iSeats = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Seats), Game.GetGXTEntry("SMOD_IN_SEAT"))
                gmInterior.AddItem(iSeats)
                gmInterior.BindMenuToItem(mSeats, iSeats)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Then
                iSteeringWheels = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.SteeringWheels), Game.GetGXTEntry("SMOD_IN_STEER"))
                gmInterior.AddItem(iSteeringWheels)
                gmInterior.BindMenuToItem(mSteeringWheels, iSteeringWheels)
            End If
            If Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Then
                iTrimDesign = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.TrimDesign), Game.GetGXTEntry("SMOD_IN_3"))
                gmInterior.AddItem(iTrimDesign)
                gmInterior.BindMenuToItem(mTrimDesign, iTrimDesign)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 Then
                giDoor = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.DoorSpeakers), Game.GetGXTEntry("SMOD_IN_5b"))
                gmInterior.AddItem(giDoor)
                gmInterior.BindMenuToItem(mDoor, giDoor)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                iFrame = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Frame), Game.GetGXTEntry("SMOD_ROLLCAGE_1"))
                gmInterior.AddItem(iFrame)
                gmInterior.BindMenuToItem(mFrame, iFrame)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0 Then
                iSpeaker = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Speakers), Game.GetGXTEntry("CMOD_MOD_23_D"))
                gmInterior.AddItem(iSpeaker)
                gmInterior.BindMenuToItem(mSpeakers, iSpeaker)
            End If
            If bennysvehicle.Contains(Bennys.veh.Model) Then
                iDashboardColor = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.LightColor), Game.GetGXTEntry("SMOD_LIGHT_COLb"))
                gmInterior.AddItem(iDashboardColor)
                gmInterior.BindMenuToItem(mLightsColor, iDashboardColor)
                iTrimColor = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.TrimColor), Game.GetGXTEntry("CMOD_MOD_6_D")) 'trim color
                gmInterior.AddItem(iTrimColor)
                gmInterior.BindMenuToItem(mTrimColor, iTrimColor)
            End If
            gmInterior.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBumperMenu()
        Try
            gmBumper = New UIMenu("", Helper.LocalizeModTitleName("BUMPERS")) 'BUMPERS
            gmBumper.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBumper.MouseEdgeEnabled = False
            _menuPool.Add(gmBumper)
            gmBumper.AddItem(New UIMenuItem("Nothing"))
            gmBumper.RefreshIndex()
            AddHandler gmBumper.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBumperMenu()
        Try
            gmBumper.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Then
                giFBumper = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.FrontBumper), Game.GetGXTEntry("CMOD_MOD_71_D"))
                gmBumper.AddItem(giFBumper)
                gmBumper.BindMenuToItem(mFBumper, giFBumper)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0 Then
                giSSkirt = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.SideSkirt), Game.GetGXTEntry("CMOD_MOD_21_D"))
                gmBumper.AddItem(giSSkirt)
                gmBumper.BindMenuToItem(mSSkirt, giSSkirt)
            End If
            If Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Then
                giRBumper = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.RearBumper), Game.GetGXTEntry("CMOD_MOD_71_D"))
                gmBumper.AddItem(giRBumper)
                gmBumper.BindMenuToItem(mRBumper, giRBumper)
            End If
            gmBumper.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelsMenu()
        Try
            gmWheels = New UIMenu("", Helper.LocalizeModTitleName("WHEELS")) 'WHEELS
            gmWheels.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmWheels.MouseEdgeEnabled = False
            _menuPool.Add(gmWheels)
            gmWheels.AddItem(New UIMenuItem("Nothing"))
            gmWheels.RefreshIndex()
            AddHandler gmWheels.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler gmWheels.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelsMenu()
        Try
            gmWheels.MenuItems.Clear()
            giWheelType = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.WheelType), Game.GetGXTEntry("CMOD_MOD_28_D")) 'Wheel Type
            gmWheels.AddItem(giWheelType)
            gmWheels.BindMenuToItem(gmWheelType, giWheelType)
            iRimColor = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.WheelColor), Game.GetGXTEntry("CMOD_MOD_59_D"))
            gmWheels.AddItem(iRimColor)
            gmWheels.BindMenuToItem(mRimColor, iRimColor)
            giTires = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Tires), Game.GetGXTEntry("CMOD_IE_25_D"))
            gmWheels.AddItem(giTires)
            gmWheels.BindMenuToItem(mTires, giTires)
            iTireSmoke = New UIMenuItem(Helper.LocalizedModTypeName(VehicleToggleMod.TireSmoke), Game.GetGXTEntry("CMOD_IE_25_D"))
            gmWheels.AddItem(iTireSmoke)
            gmWheels.BindMenuToItem(mTireSmoke, iTireSmoke)
            gmWheels.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelTypeMenu()
        Try
            gmWheelType = New UIMenu("", Helper.LocalizeModTitleName("WHEELTYPE")) 'WHEEL TYPE
            gmWheelType.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmWheelType.MouseEdgeEnabled = False
            _menuPool.Add(gmWheelType)
            gmWheelType.AddItem(New UIMenuItem("Nothing"))
            gmWheelType.RefreshIndex()
            AddHandler gmWheelType.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelTypeMenu()
        Try
            gmWheelType.MenuItems.Clear()

            Select Case Bennys.veh.ClassType
                Case VehicleClass.Motorcycles, VehicleClass.Cycles
                    giBikeWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels))
                    gmWheelType.AddItem(giBikeWheels)
                    gmWheelType.BindMenuToItem(mBikeWheels, giBikeWheels)
                Case Else
                    giHighEndWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd))
                    gmWheelType.AddItem(giHighEndWheels)
                    gmWheelType.BindMenuToItem(mHighEnd, giHighEndWheels)
                    giLowriderWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider))
                    gmWheelType.AddItem(giLowriderWheels)
                    gmWheelType.BindMenuToItem(mLowrider, giLowriderWheels)
                    giMuscleWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle))
                    gmWheelType.AddItem(giMuscleWheels)
                    gmWheelType.BindMenuToItem(mMuscle, giMuscleWheels)
                    giOffroadWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad))
                    gmWheelType.AddItem(giOffroadWheels)
                    gmWheelType.BindMenuToItem(mOffroad, giOffroadWheels)
                    giSportWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport))
                    gmWheelType.AddItem(giSportWheels)
                    gmWheelType.BindMenuToItem(mSport, giSportWheels)
                    giSUVWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV))
                    gmWheelType.AddItem(giSUVWheels)
                    gmWheelType.BindMenuToItem(mSUV, giSUVWheels)
                    giTunerWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner))
                    gmWheelType.AddItem(giTunerWheels)
                    gmWheelType.BindMenuToItem(mTuner, giTunerWheels)
                    giBennysWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(8)) 'Benny's Original
                    gmWheelType.AddItem(giBennysWheels)
                    gmWheelType.BindMenuToItem(mBennysOriginals, giBennysWheels)
                    giBespokeWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(9)) 'Benny's Bespoke
                    gmWheelType.AddItem(giBespokeWheels)
                    gmWheelType.BindMenuToItem(mBespoke, giBespokeWheels)
            End Select

            gmWheelType.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateTyresMenu()
        Try
            mTires = New UIMenu("", Helper.LocalizeModTitleName("TIRES")) 'TIRES
            mTires.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mTires.MouseEdgeEnabled = False
            _menuPool.Add(mTires)
            mTires.AddItem(New UIMenuItem("Nothing"))
            mTires.RefreshIndex()
            AddHandler mTires.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mTires.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshTyresMenu()
        Try
            mTires.MenuItems.Clear()

            Select Case Bennys.veh.WheelType
                Case 8, 9
                    iTires = New UIMenuItem(tyres(0))
                    With iTires
                        .SubInteger1 = 0
                        If Not Helper.IsCustomWheels Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End With
                    mTires.AddItem(iTires)
                Case Else
                    iTires = New UIMenuItem(tyres(0))
                    With iTires
                        .SubInteger1 = 0
                        If Not Helper.IsCustomWheels Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End With
                    mTires.AddItem(iTires)
                    iTires = New UIMenuItem(tyres(6))
                    With iTires
                        .SubInteger1 = 6
                        If Helper.IsCustomWheels Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End With
                    mTires.AddItem(iTires)
            End Select

            mTires.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateMenu()
        Try
            gmPlate = New UIMenu("", Helper.LocalizeModTitleName("PLATES")) 'PLATES
            gmPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmPlate.MouseEdgeEnabled = False
            _menuPool.Add(gmPlate)
            gmPlate.AddItem(New UIMenuItem("Nothing"))
            gmPlate.RefreshIndex()
            AddHandler gmPlate.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler gmPlate.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPlateMenu()
        Try
            gmPlate.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Then
                giPlateHolder = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.PlateHolder), Game.GetGXTEntry("CMOD_MOD_49_D"))
                gmPlate.AddItem(giPlateHolder)
                gmPlate.BindMenuToItem(mPlateHolder, giPlateHolder)
            End If
            If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                giVanityPlate = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.VanityPlates), Game.GetGXTEntry("CMOD_SMOD_4_D"))
                gmPlate.AddItem(giVanityPlate)
                gmPlate.BindMenuToItem(mVanityPlates, giVanityPlate)
            End If
            giNumberPlate = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.License), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Number Plate
            gmPlate.AddItem(giNumberPlate)
            gmPlate.BindMenuToItem(mNumberPlate, giNumberPlate)
            gmPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateNumberMenu()
        Try
            mNumberPlate = New UIMenu("", Helper.LocalizeModTitleName("LICENSE")) 'LICENSE PLATE
            mNumberPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNumberPlate.MouseEdgeEnabled = False
            _menuPool.Add(mNumberPlate)
            mNumberPlate.AddItem(New UIMenuItem("Nothing"))
            mNumberPlate.RefreshIndex()
            AddHandler mNumberPlate.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mNumberPlate.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mNumberPlate.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateTintMenu()
        Try
            mTint = New UIMenu("", Helper.LocalizeModTitleName("TINTS")) 'TINTS
            mTint.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mTint.MouseEdgeEnabled = False
            _menuPool.Add(mTint)
            mTint.AddItem(New UIMenuItem("Nothing"))
            mTint.RefreshIndex()
            AddHandler mTint.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mTint.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mTint.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateColorMenuFor(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            AddHandler menu.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler menu.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshEnumModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef enumType As Helper.EnumTypes)
        Try
            menu.MenuItems.Clear()

            Dim enumArray As Array = Nothing
            Select Case enumType
                Case Helper.EnumTypes.NumberPlateType
                    enumArray = System.Enum.GetValues(GetType(NumberPlateType))
                    For Each enumItem As NumberPlateType In enumArray
                        item = New UIMenuItem(Helper.LocalizedLicensePlate(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.NumberPlateType = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleWindowTint
                    enumArray = System.Enum.GetValues(GetType(VehicleWindowTint))
                    For Each enumItem As VehicleWindowTint In enumArray
                        item = New UIMenuItem(Helper.LocalizedWindowsTint(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.WindowTint = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorPrimary
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(Helper.GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.PrimaryColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorSecondary
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(Helper.GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.SecondaryColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.vehicleColorPearlescent
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(Helper.GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.PearlescentColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorTrim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(Helper.GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.TrimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorDashboard
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(Helper.GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.DashboardColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorRim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(Helper.GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.RimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
            End Select
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateLightsMenu()
        Try
            gmLights = New UIMenu("", Helper.LocalizeModTitleName("LIGHTS")) 'LIGHTS
            gmLights.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmLights.MouseEdgeEnabled = False
            _menuPool.Add(gmLights)
            gmLights.AddItem(New UIMenuItem("Nothing"))
            gmLights.RefreshIndex()
            AddHandler gmLights.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshLightsMenu()
        Try
            gmLights.MenuItems.Clear()
            iHeadlights = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.Headlights), Game.GetGXTEntry("CMOD_MOD_47_D"))
            gmLights.AddItem(iHeadlights)
            gmLights.BindMenuToItem(mHeadlights, iHeadlights)
            giNeonKits = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.NeonKits), Game.GetGXTEntry("CMOD_MOD_6_D"))
            gmLights.AddItem(giNeonKits)
            gmLights.BindMenuToItem(gmNeonKits, giNeonKits)
            gmLights.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNeonKitsMenu()
        Try
            gmNeonKits = New UIMenu("", Helper.LocalizeModTitleName("NEONKITS")) 'NEON KITS
            gmNeonKits.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmNeonKits.MouseEdgeEnabled = False
            _menuPool.Add(gmNeonKits)
            gmNeonKits.AddItem(New UIMenuItem("Nothing"))
            gmNeonKits.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshNeonKitsMenu()
        Try
            gmNeonKits.MenuItems.Clear()
            iNeon = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.NeonLayout))
            gmNeonKits.AddItem(iNeon)
            gmNeonKits.BindMenuToItem(mNeon, iNeon)
            iNeonColor = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.NeonColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
            gmNeonKits.AddItem(iNeonColor)
            gmNeonKits.BindMenuToItem(mNeonColor, iNeonColor)
            'gmNeonKits.BindMenuToItem(mNeonColor, iNeonColor)
            gmNeonKits.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNeonMenu()
        Try
            mNeon = New UIMenu("", Helper.LocalizeModTitleName("NEONLAYOUT")) 'NEON LAYOUT
            mNeon.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNeon.MouseEdgeEnabled = False
            _menuPool.Add(mNeon)
            mNeon.AddItem(New UIMenuItem("Nothing"))
            mNeon.RefreshIndex()
            AddHandler mNeon.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mNeon.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mNeon.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshNeonMenu()
        Try
            mNeon.MenuItems.Clear()

            iNeon = New UIMenuItem("None")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.None
                If Helper.NeonLayout = Helper.NeonLayouts.None Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.Front
                If Helper.NeonLayout = Helper.NeonLayouts.Front Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Back")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.Back
                If Helper.NeonLayout = Helper.NeonLayouts.Back Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.Sides
                If Helper.NeonLayout = Helper.NeonLayouts.Sides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front and Back")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.FrontAndBack
                If Helper.NeonLayout = Helper.NeonLayouts.FrontAndBack Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front and Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.FrontAndSides
                If Helper.NeonLayout = Helper.NeonLayouts.FrontAndSides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Back and Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.BackAndSides
                If Helper.NeonLayout = Helper.NeonLayouts.BackAndSides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front, Back and Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.FrontBackAndSides
                If Helper.NeonLayout = Helper.NeonLayouts.FrontBackAndSides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)

            mNeon.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateResprayMenu()
        Try
            gmRespray = New UIMenu("", Helper.LocalizeModTitleName("RESPRAY")) 'RESPRAY
            gmRespray.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmRespray.MouseEdgeEnabled = False
            _menuPool.Add(gmRespray)
            gmRespray.AddItem(New UIMenuItem("Nothing"))
            gmRespray.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshResprayMenu()
        Try
            gmRespray.MenuItems.Clear()
            giPrimaryCol = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.PrimaryColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
            gmRespray.AddItem(giPrimaryCol)
            gmRespray.BindMenuToItem(mPrimaryColor, giPrimaryCol)
            giSecondaryCol = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.SecondaryColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
            gmRespray.AddItem(giSecondaryCol)
            gmRespray.BindMenuToItem(mSecondaryColor, giSecondaryCol)
            If Not bennysvehicle.Contains(Bennys.veh.Model) Then
                iDashboardColor = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.AccentColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
                gmRespray.AddItem(iDashboardColor)
                gmRespray.BindMenuToItem(mLightsColor, iDashboardColor)
                iTrimColor = New UIMenuItem(Helper.LocalizedModGroupName(Helper.GroupName.TrimColor), Game.GetGXTEntry("CMOD_MOD_6_D")) 'trim color
                gmRespray.AddItem(iTrimColor)
                gmRespray.BindMenuToItem(mTrimColor, iTrimColor)
            End If
            gmRespray.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPrimaryColorMenu()
        Try
            mPrimaryColor.MenuItems.Clear()
            iPrimaryChromeColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Chrome), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryChromeColor)
            mPrimaryColor.BindMenuToItem(mPrimaryChromeColor, iPrimaryChromeColor)
            iPrimaryClassicColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Classic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryClassicColor)
            mPrimaryColor.BindMenuToItem(mPrimaryClassicColor, iPrimaryClassicColor)
            iPrimaryMatteColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Matte), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryMatteColor)
            mPrimaryColor.BindMenuToItem(mPrimaryMatteColor, iPrimaryMatteColor)
            iPrimaryMetallicColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Metallic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryMetallicColor)
            mPrimaryColor.BindMenuToItem(mPrimaryMetallicColor, iPrimaryMetallicColor)
            iPrimaryMetalsColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Metals), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryMetalsColor)
            mPrimaryColor.BindMenuToItem(mPrimaryMetalsColor, iPrimaryMetalsColor)
            iPrimaryPearlescentColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Pearlescent), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryPearlescentColor)
            mPrimaryColor.BindMenuToItem(mPrimaryPearlescentColor, iPrimaryPearlescentColor)
            mPrimaryColor.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshSecondaryColorMenu()
        Try
            mSecondaryColor.MenuItems.Clear()
            iSecondaryChromeColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Chrome), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryChromeColor)
            mSecondaryColor.BindMenuToItem(mSecondaryChromeColor, iSecondaryChromeColor)
            iSecondaryClassicColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Classic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryClassicColor)
            mSecondaryColor.BindMenuToItem(mSecondaryClassicColor, iSecondaryClassicColor)
            iSecondaryMatteColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Matte), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryMatteColor)
            mSecondaryColor.BindMenuToItem(mSecondaryMatteColor, iSecondaryMatteColor)
            iSecondaryMetallicColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Metallic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryMetallicColor)
            mSecondaryColor.BindMenuToItem(mSecondaryMetallicColor, iSecondaryMetallicColor)
            iSecondaryMetalsColor = New UIMenuItem(Helper.LocalizedColorGroupName(Helper.ColorType.Metals), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryMetalsColor)
            mSecondaryColor.BindMenuToItem(mSecondaryMetalsColor, iSecondaryMetalsColor)
            mSecondaryColor.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshColorMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef colorList As List(Of VehicleColor), prisecpear As String)
        Try
            menu.MenuItems.Clear()
            For Each col As VehicleColor In colorList
                item = New UIMenuItem(Helper.GetLocalizedColorName(col))
                With item
                    .SubInteger1 = col
                    If prisecpear = "Primary" Then
                        If Bennys.veh.PrimaryColor = col Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    ElseIf prisecpear = "Secondary" Then
                        If Bennys.veh.SecondaryColor = col Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    ElseIf prisecpear = "Pearlescent" Then
                        If Bennys.veh.PearlescentColor = col Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End If
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshRGBColorMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, neonsmoke As String)
        Try
            menu.MenuItems.Clear()
            Dim removeList As New List(Of String) From {"R", "G", "B", "A", "IsKnownColor", "IsEmpty", "IsNamedColor", "IsSystemColor", "Name", "Transparent"}
            Dim index As Integer = 0
            For Each col As Reflection.PropertyInfo In GetType(Drawing.Color).GetProperties()
                If Not removeList.Contains(col.Name) Then
                    item = New UIMenuItem(Trim(RegularExpressions.Regex.Replace(col.Name, "[A-Z]", " ${0}")))
                    With item
                        .SubInteger1 = Drawing.Color.FromName(col.Name).R
                        .SubInteger2 = Drawing.Color.FromName(col.Name).G
                        .SubInteger3 = Drawing.Color.FromName(col.Name).B
                        If neonsmoke = "Neon" Then
                            If Bennys.veh.NeonLightsColor = Drawing.Color.FromName(col.Name) Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        ElseIf neonsmoke = "Smoke" Then
                            If Bennys.veh.TireSmokeColor = Drawing.Color.FromName(col.Name) Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End If
                    End With
                    menu.AddItem(item)
                End If
            Next

            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePerformanceMenuFor(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            AddHandler menu.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler menu.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPerformanceMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod, ByRef gxt As String)
        Try
            menu.MenuItems.Clear()

            For i As Integer = -1 To Bennys.veh.GetModCount(vehmod) - 1
                Select Case vehmod
                    Case VehicleMod.Engine
                        item = New UIMenuItem(Game.GetGXTEntry(gxt & i + 2))
                    Case Else
                        item = New UIMenuItem(Game.GetGXTEntry(gxt & i + 1))
                End Select

                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateModMenuFor(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            AddHandler menu.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler menu.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod)
        Try
            menu.MenuItems.Clear()
            For i As Integer = -1 To Bennys.veh.GetModCount(vehmod) - 1
                item = New UIMenuItem(Helper.GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleToggleMod)
        Try
            menu.MenuItems.Clear()

            item = New UIMenuItem(Helper.LocalizedModTypeName(vehmod, True))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 0
                If Not Bennys.veh.IsToggleModOn(vehmod) Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Helper.LocalizedModTypeName(vehmod))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 1
                If Bennys.veh.IsToggleModOn(vehmod) Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            menu.AddItem(item)

            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuCloseHandler(sender As UIMenu)
        Try
            'Performance Mods
            Bennys.veh.SetMod(VehicleMod.Suspension, Bennys.lastVehMemory.Suspension, False)
            Bennys.veh.SetMod(VehicleMod.Armor, Bennys.lastVehMemory.Armor, False)
            Bennys.veh.SetMod(VehicleMod.Brakes, Bennys.lastVehMemory.Brakes, False)
            Bennys.veh.SetMod(VehicleMod.Transmission, Bennys.lastVehMemory.Transmission, False)
            Bennys.veh.SetMod(VehicleMod.Engine, Bennys.lastVehMemory.Engine, False)

            'Mods
            Bennys.veh.SetMod(VehicleMod.FrontBumper, Bennys.lastVehMemory.FrontBumper, False)
            Bennys.veh.SetMod(VehicleMod.RearBumper, Bennys.lastVehMemory.RearBumper, False)
            Bennys.veh.SetMod(VehicleMod.SideSkirt, Bennys.lastVehMemory.SideSkirt, False)
            Bennys.veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
            Bennys.veh.WheelType = Bennys.lastVehMemory.WheelType
            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.lastVehMemory.FrontWheels, Bennys.lastVehMemory.WheelsVariation)
            Bennys.veh.SetMod(VehicleMod.BackWheels, Bennys.lastVehMemory.BackWheels, Bennys.lastVehMemory.WheelsVariation)
            Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, Bennys.lastVehMemory.Headlights)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, Bennys.lastVehMemory.BackNeon)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, Bennys.lastVehMemory.FrontNeon)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, Bennys.lastVehMemory.LeftNeon)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, Bennys.lastVehMemory.RightNeon)
            Bennys.veh.SetMod(VehicleMod.ArchCover, Bennys.lastVehMemory.ArchCover, False)
            Bennys.veh.SetMod(VehicleMod.Exhaust, Bennys.lastVehMemory.Exhaust, False)
            Bennys.veh.SetMod(VehicleMod.Fender, Bennys.lastVehMemory.Fender, False)
            Bennys.veh.SetMod(VehicleMod.RightFender, Bennys.lastVehMemory.RightFender, False)
            Bennys.veh.SetMod(VehicleMod.DoorSpeakers, Bennys.lastVehMemory.DoorSpeakers, False)
            Bennys.veh.SetMod(VehicleMod.Frame, Bennys.lastVehMemory.Frame, False)
            Bennys.veh.SetMod(VehicleMod.Grille, Bennys.lastVehMemory.Grille, False)
            Bennys.veh.SetMod(VehicleMod.Hood, Bennys.lastVehMemory.Hood, False)
            Bennys.veh.SetMod(VehicleMod.Horns, Bennys.lastVehMemory.Horns, False)
            Bennys.veh.SetMod(VehicleMod.Hydraulics, Bennys.lastVehMemory.Hydraulics, False)
            Bennys.veh.SetMod(VehicleMod.Livery, Bennys.lastVehMemory.Livery, False)
            Bennys.veh.SetMod(VehicleMod.Plaques, Bennys.lastVehMemory.Plaques, False)
            Bennys.veh.SetMod(VehicleMod.Roof, Bennys.lastVehMemory.Roof, False)
            Bennys.veh.SetMod(VehicleMod.Speakers, Bennys.lastVehMemory.Speakers, False)
            Bennys.veh.SetMod(VehicleMod.Spoilers, Bennys.lastVehMemory.Spoilers, False)
            Bennys.veh.SetMod(VehicleMod.Aerials, Bennys.lastVehMemory.Aerials, False)
            Bennys.veh.SetMod(VehicleMod.Trim, Bennys.lastVehMemory.Trim, False)
            Bennys.veh.SetMod(VehicleMod.EngineBlock, Bennys.lastVehMemory.EngineBlock, False)
            Bennys.veh.SetMod(VehicleMod.AirFilter, Bennys.lastVehMemory.AirFilter, False)
            Bennys.veh.SetMod(VehicleMod.Struts, Bennys.lastVehMemory.Struts, False)
            Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, Bennys.lastVehMemory.ColumnShifterLevers, False)
            Bennys.veh.SetMod(VehicleMod.Dashboard, Bennys.lastVehMemory.Dashboard, False)
            Bennys.veh.SetMod(VehicleMod.DialDesign, Bennys.lastVehMemory.DialDesign, False)
            Bennys.veh.SetMod(VehicleMod.Ornaments, Bennys.lastVehMemory.Ornaments, False)
            Bennys.veh.SetMod(VehicleMod.Seats, Bennys.lastVehMemory.Seats, False)
            Bennys.veh.SetMod(VehicleMod.SteeringWheels, Bennys.lastVehMemory.SteeringWheels, False)
            Bennys.veh.SetMod(VehicleMod.TrimDesign, Bennys.lastVehMemory.TrimDesign, False)
            Bennys.veh.SetMod(VehicleMod.PlateHolder, Bennys.lastVehMemory.PlateHolder, False)
            Bennys.veh.SetMod(VehicleMod.VanityPlates, Bennys.lastVehMemory.VanityPlates, False)
            Bennys.veh.SetMod(VehicleMod.Tank, Bennys.lastVehMemory.Tank, False)
            Bennys.veh.SetMod(VehicleMod.Trunk, Bennys.lastVehMemory.Trunk, False)
            Bennys.veh.SetMod(VehicleMod.Windows, Bennys.lastVehMemory.Windows, False)
            Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, Bennys.lastVehMemory.Turbo)
            Bennys.veh.WindowTint = Bennys.lastVehMemory.Tint

            'Color
            Bennys.veh.DashboardColor = Bennys.lastVehMemory.LightsColor
            Bennys.veh.TrimColor = Bennys.lastVehMemory.TrimColor
            Bennys.veh.PrimaryColor = Bennys.lastVehMemory.PrimaryColor
            Bennys.veh.SecondaryColor = Bennys.lastVehMemory.SecondaryColor
            Bennys.veh.PearlescentColor = Bennys.lastVehMemory.PearlescentColor
            Bennys.veh.RimColor = Bennys.lastVehMemory.RimColor
            Bennys.veh.NeonLightsColor = Bennys.lastVehMemory.NeonLightsColor
            Bennys.veh.TireSmokeColor = Bennys.lastVehMemory.TireSmokeColor

            'Close Doors
            If sender Is gmEngine Then Bennys.veh.CloseDoor(VehicleDoor.Hood, False)
            If sender Is mDoor Then
                Bennys.veh.CloseDoor(VehicleDoor.FrontLeftDoor, False)
                Bennys.veh.CloseDoor(VehicleDoor.FrontRightDoor, False)
            End If
            If sender Is mHydraulics Then Bennys.veh.CloseDoor(VehicleDoor.Trunk, False)
            If sender Is gmLights Then Bennys.veh.HighBeamsOn = False
            If sender Is mHorn Then Bennys.ply.Task.WarpIntoVehicle(Bennys.veh, VehicleSeat.Driver)

            'Reset Camera Position
            If (sender Is gmInterior) Or (sender Is gmEngine) Or (sender Is mFBumper) Or (sender Is mRBumper) Or (sender Is mSSkirt) Or (sender Is mNumberPlate) Or (sender Is mPlateHolder) Or (sender Is mSpoilers) Or
                (sender Is mVanityPlates) Or (sender Is gmWheels) Or (sender Is mExhaust) Or (sender Is mBrakes) Or (sender Is mGrille) Or (sender Is mHood) Or (sender Is mHydraulics) Or (sender Is mPlaques) Or (sender Is mTank) Then
                camera.MainCameraPosition = CameraPosition.Car
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            For Each i As UIMenuItem In sender.MenuItems
                i.SetRightBadge(UIMenuItem.BadgeStyle.None)
            Next

            'Performance Mods
            If sender Is mSuspension Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Suspension, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Suspension = selectedItem.SubInteger1
                End If
            ElseIf sender Is mArmor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Armor, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Armor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mBrakes Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Brakes, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Brakes = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTransmission Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Transmission, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Transmission = selectedItem.SubInteger1
                End If
            ElseIf sender Is mEngine Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Engine, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Engine = selectedItem.SubInteger1
                End If
            End If

            'Mods
            If sender Is mFBumper Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.FrontBumper = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRBumper Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RearBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.RearBumper = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSSkirt Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SideSkirt, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.SideSkirt = selectedItem.SubInteger1
                End If
            ElseIf sender Is mNumberPlate Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.NumberPlateType = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.NumberPlate = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHeadlights Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, CBool(selectedItem.SubInteger1))
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Headlights = CBool(selectedItem.SubInteger1)
                End If
            ElseIf sender Is mArchCover Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.ArchCover, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.ArchCover = selectedItem.SubInteger1
                End If
            ElseIf sender Is mExhaust Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Exhaust, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Exhaust = selectedItem.SubInteger1
                End If
            ElseIf sender Is mFender Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Fender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Fender = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRFender Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RightFender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.RightFender = selectedItem.SubInteger1
                End If
            ElseIf sender Is mDoor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.DoorSpeakers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.DoorSpeakers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mFrame Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Frame, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Frame = selectedItem.SubInteger1
                End If
            ElseIf sender Is mAerials Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Aerials, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Aerials = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrim Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Trim, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Trim = selectedItem.SubInteger1
                End If
            ElseIf sender Is mEngineBlock Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.EngineBlock, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.EngineBlock = selectedItem.SubInteger1
                End If
            ElseIf sender Is mAirFilter Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.AirFilter, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.AirFilter = selectedItem.SubInteger1
                End If
            ElseIf sender Is mStruts Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Struts, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Struts = selectedItem.SubInteger1
                End If
            ElseIf sender Is mColumnShifterLevers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.ColumnShifterLevers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mDashboard Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Dashboard, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Dashboard = selectedItem.SubInteger1
                End If
            ElseIf sender Is mDialDesign Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.DialDesign, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.DialDesign = selectedItem.SubInteger1
                End If
            ElseIf sender Is mOrnaments Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Ornaments, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Ornaments = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSeats Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Seats, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Seats = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSteeringWheels Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SteeringWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.SteeringWheels = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrimDesign Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.TrimDesign, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.TrimDesign = selectedItem.SubInteger1
                End If
            ElseIf sender Is mPlateHolder Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.PlateHolder, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.PlateHolder = selectedItem.SubInteger1
                End If
            ElseIf sender Is mVanityPlates Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.VanityPlates, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.VanityPlates = selectedItem.SubInteger1
                End If
            ElseIf sender Is mGrille Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Grille, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Grille = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHood Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hood, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Hood = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHorn Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Horns, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Horns = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHydraulics Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hydraulics, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Hydraulics = selectedItem.SubInteger1
                End If
            ElseIf sender Is mLivery Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Livery, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Livery = selectedItem.SubInteger1
                End If
            ElseIf sender Is mPlaques Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Plaques, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Plaques = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRoof Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Roof, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Roof = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSpeakers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Speakers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Speakers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSpoilers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Spoilers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Spoilers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTank Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Tank, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Tank = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrunk Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Trunk, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Trunk = selectedItem.SubInteger1
                End If
            ElseIf sender Is mWindow Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Windows, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Windows = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTurbo Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, CBool(selectedItem.SubInteger1))
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Turbo = CBool(selectedItem.SubInteger1)
                End If
            ElseIf sender Is mTint Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.WindowTint = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Tint = selectedItem.SubInteger1
                End If
            End If

            'Neons Mods
            If sender Is mNeon Then
                Select Case selectedItem.SubInteger1
                    Case Helper.NeonLayouts.None
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.Front
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.Back
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.Sides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case Helper.NeonLayouts.FrontAndBack
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.FrontAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case Helper.NeonLayouts.BackAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case Helper.NeonLayouts.FrontBackAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                End Select
            End If

            'Wheels Mods
            If sender Is gmWheels Then
                If selectedItem Is giTires Then RefreshTyresMenu()
            End If
            If sender Is mBikeWheels Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                    Bennys.veh.SetMod(VehicleMod.BackWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.WheelType = Bennys.veh.WheelType
                    Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                    Bennys.lastVehMemory.BackWheels = selectedItem.SubInteger1
                End If
            ElseIf (sender Is mHighEnd) Or (sender Is mLowrider) Or (sender Is mMuscle) Or (sender Is mOffroad) Or (sender Is mSport) Or (sender Is mSUV) Or (sender Is mTuner) Or (sender Is mBennysOriginals) Or (sender Is mBespoke) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.WheelType = Bennys.veh.WheelType
                    Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                End If
            End If
            If sender Is mTires Then
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                    Case Else
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            If selectedItem.SubInteger1 = 0 Then
                                Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), False)
                                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), False)
                                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                                Bennys.lastVehMemory.WheelsVariation = False
                            ElseIf selectedItem.SubInteger1 = 6 Then
                                Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), True)
                                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), True)
                                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                                Bennys.lastVehMemory.WheelsVariation = True
                            End If
                        End If
                End Select
            End If

            'Wheel Type
            If sender Is gmWheelType Then
                If selectedItem Is giBikeWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.BikeWheels
                    RefreshModMenuFor(mBikeWheels, iBikeWheels, VehicleMod.BackWheels)
                ElseIf selectedItem Is giHighEndWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.HighEnd
                    RefreshModMenuFor(mHighEnd, iHighEnd, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giLowriderWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Lowrider
                    RefreshModMenuFor(mLowrider, iLowrider, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giMuscleWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Muscle
                    RefreshModMenuFor(mMuscle, iMuscle, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giOffroadWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Offroad
                    RefreshModMenuFor(mOffroad, iOffroad, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giSportWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Sport
                    RefreshModMenuFor(mSport, iSport, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giSUVWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.SUV
                    RefreshModMenuFor(mSUV, iSUV, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giTunerWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Tuner
                    RefreshModMenuFor(mTuner, iTuner, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giBennysWheels Then
                    Bennys.veh.WheelType = 8
                    RefreshModMenuFor(mBennysOriginals, iBennys, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giBespokeWheels Then
                    Bennys.veh.WheelType = 9
                    RefreshModMenuFor(mBespoke, iBespoke, VehicleMod.FrontWheels)
                End If
            End If

            'Color
            If sender Is mLightsColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.DashboardColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.LightsColor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrimColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.TrimColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.TrimColor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRimColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.RimColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.RimColor = selectedItem.SubInteger1
                End If
            ElseIf (sender Is mPrimaryChromeColor) Or (sender Is mPrimaryClassicColor) Or (sender Is mPrimaryMatteColor) Or (sender Is mPrimaryMetalsColor) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.PrimaryColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.PrimaryColor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mPrimaryMetallicColor
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.PrimaryColor = selectedItem.SubInteger1
                    Bennys.veh.PearlescentColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.PrimaryColor = selectedItem.SubInteger1
                    Bennys.lastVehMemory.PearlescentColor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mPrimaryPearlescentColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.PearlescentColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.PearlescentColor = selectedItem.SubInteger1
                End If
            ElseIf (sender Is mSecondaryChromeColor) Or (sender Is mSecondaryClassicColor) Or (sender Is mSecondaryMatteColor) Or (sender Is mSecondaryMetallicColor) Or (sender Is mSecondaryMetalsColor) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SecondaryColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.SecondaryColor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mNeonColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.NeonLightsColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.NeonLightsColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                End If
            ElseIf sender Is mTireSmoke Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.TireSmokeColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                    Bennys.veh.ToggleMod(VehicleToggleMod.TireSmoke, True)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.TireSmokeColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                End If
            End If

            'Camera
            If sender Is gmBumper Then
                If selectedItem Is giFBumper Then
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                ElseIf selectedItem Is giRBumper Then
                    camera.MainCameraPosition = CameraPosition.RearBumper
                ElseIf selectedItem Is giSSkirt
                    camera.MainCameraPosition = CameraPosition.Wheels
                End If
            ElseIf sender Is gmPlate Then
                If selectedItem Is giNumberPlate Then
                    camera.MainCameraPosition = CameraPosition.BackPlate
                ElseIf selectedItem Is giPlateHolder Then
                    Select Case Bennys.veh.Model
                        Case VehicleHash.SlamVan3
                            camera.MainCameraPosition = CameraPosition.RearBumper
                        Case Else
                            camera.MainCameraPosition = CameraPosition.FrontBumper
                    End Select

                ElseIf selectedItem Is giVanityPlate Then
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                End If
            ElseIf sender Is gmInterior Then
                If selectedItem Is giDoor Then
                    Bennys.veh.OpenDoor(VehicleDoor.FrontLeftDoor, False, False)
                    Bennys.veh.OpenDoor(VehicleDoor.FrontRightDoor, False, False)
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuIndexChangedHandler(sender As UIMenu, index As Integer)
        Try
            'Performance
            If sender Is mSuspension Then
                Bennys.veh.SetMod(VehicleMod.Suspension, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mArmor Then
                Bennys.veh.SetMod(VehicleMod.Armor, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBrakes Then
                Bennys.veh.SetMod(VehicleMod.Brakes, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTransmission Then
                Bennys.veh.SetMod(VehicleMod.Transmission, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mEngine Then
                Bennys.veh.SetMod(VehicleMod.Engine, sender.MenuItems(index).SubInteger1, False)
            End If

            'Mod
            If sender Is mFBumper Then
                Bennys.veh.SetMod(VehicleMod.FrontBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRBumper Then
                Bennys.veh.SetMod(VehicleMod.RearBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSSkirt Then
                Bennys.veh.SetMod(VehicleMod.SideSkirt, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mNumberPlate Then
                Bennys.veh.NumberPlateType = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mHeadlights Then
                Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, CBool(sender.MenuItems(index).SubInteger1))
            ElseIf sender Is mArchCover Then
                Bennys.veh.SetMod(VehicleMod.ArchCover, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mExhaust Then
                Bennys.veh.SetMod(VehicleMod.Exhaust, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mFender Then
                Bennys.veh.SetMod(VehicleMod.Fender, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRFender Then
                Bennys.veh.SetMod(VehicleMod.RightFender, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mDoor Then
                Bennys.veh.SetMod(VehicleMod.DoorSpeakers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mFrame Then
                Bennys.veh.SetMod(VehicleMod.Frame, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mAerials Then
                Bennys.veh.SetMod(VehicleMod.Aerials, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTrim Then
                Bennys.veh.SetMod(VehicleMod.Trim, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mEngineBlock Then
                Bennys.veh.SetMod(VehicleMod.EngineBlock, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mAirFilter Then
                Bennys.veh.SetMod(VehicleMod.AirFilter, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mStruts Then
                Bennys.veh.SetMod(VehicleMod.Struts, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mColumnShifterLevers Then
                Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mDashboard Then
                Bennys.veh.SetMod(VehicleMod.Dashboard, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mDialDesign Then
                Bennys.veh.SetMod(VehicleMod.DialDesign, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mOrnaments Then
                Bennys.veh.SetMod(VehicleMod.Ornaments, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSeats Then
                Bennys.veh.SetMod(VehicleMod.Seats, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSteeringWheels Then
                Bennys.veh.SetMod(VehicleMod.SteeringWheels, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTrimDesign Then
                Bennys.veh.SetMod(VehicleMod.TrimDesign, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mPlateHolder Then
                Bennys.veh.SetMod(VehicleMod.PlateHolder, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mVanityPlates Then
                Bennys.veh.SetMod(VehicleMod.VanityPlates, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mGrille Then
                Bennys.veh.SetMod(VehicleMod.Grille, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mHood Then
                Bennys.veh.SetMod(VehicleMod.Hood, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mHorn Then
                Bennys.veh.SetMod(VehicleMod.Horns, sender.MenuItems(index).SubInteger1, False)
                Bennys.ply.Task.WarpIntoVehicle(Bennys.veh, VehicleSeat.Passenger)
                Bennys.veh.SoundHorn(3000)
            ElseIf sender Is mHydraulics Then
                Bennys.veh.SetMod(VehicleMod.Hydraulics, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mLivery Then
                Bennys.veh.SetMod(VehicleMod.Livery, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mPlaques Then
                Bennys.veh.SetMod(VehicleMod.Plaques, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRoof Then
                Bennys.veh.SetMod(VehicleMod.Roof, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSpeakers Then
                Bennys.veh.SetMod(VehicleMod.Speakers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSpoilers Then
                Bennys.veh.SetMod(VehicleMod.Spoilers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTank Then
                Bennys.veh.SetMod(VehicleMod.Tank, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTrunk Then
                Bennys.veh.SetMod(VehicleMod.Trunk, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mWindow Then
                Bennys.veh.SetMod(VehicleMod.Windows, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTurbo Then
                Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, CBool(sender.MenuItems(index).SubInteger1))
            ElseIf sender Is mTint
                Bennys.veh.WindowTint = sender.MenuItems(index).SubInteger1
            End If

            'Neons Mods
            If sender Is mNeon Then
                Select Case sender.MenuItems(index).SubInteger1
                    Case Helper.NeonLayouts.None
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.Front
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.Back
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.Sides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case Helper.NeonLayouts.FrontAndBack
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.FrontAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case Helper.NeonLayouts.BackAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case Helper.NeonLayouts.FrontBackAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                End Select
            End If

            'Wheels Mods
            If sender Is mBikeWheels Then
                Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
                Bennys.veh.SetMod(VehicleMod.BackWheels, sender.MenuItems(index).SubInteger1, False)
            ElseIf (sender Is mHighEnd) Or (sender Is mLowrider) Or (sender Is mMuscle) Or (sender Is mOffroad) Or (sender Is mSport) Or (sender Is mSUV) Or (sender Is mTuner) Or (sender Is mBennysOriginals) Or (sender Is mBespoke) Then
                Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
            End If
            If sender Is mTires Then
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                    Case Else
                        If sender.MenuItems(index).SubInteger1 = 0 Then
                            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), False)
                            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), False)
                        ElseIf sender.MenuItems(index).SubInteger1 = 6 Then
                            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), True)
                            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), True)
                        End If
                End Select
            End If

            'Color
            If sender Is mLightsColor Then
                Bennys.veh.DashboardColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mTrimColor Then
                Bennys.veh.TrimColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mRimColor Then
                Bennys.veh.RimColor = sender.MenuItems(index).SubInteger1
            ElseIf (sender Is mPrimaryChromeColor) Or (sender Is mPrimaryClassicColor) Or (sender Is mPrimaryMatteColor) Or (sender Is mPrimaryMetalsColor) Then
                Bennys.veh.PrimaryColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mPrimaryMetallicColor
                Bennys.veh.PrimaryColor = sender.MenuItems(index).SubInteger1
                Bennys.veh.PearlescentColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mPrimaryPearlescentColor Then
                Bennys.veh.PearlescentColor = sender.MenuItems(index).SubInteger1
            ElseIf (sender Is mSecondaryChromeColor) Or (sender Is mSecondaryClassicColor) Or (sender Is mSecondaryMatteColor) Or (sender Is mSecondaryMetallicColor) Or (sender Is mSecondaryMetalsColor) Then
                Bennys.veh.SecondaryColor = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mNeonColor Then
                Bennys.veh.NeonLightsColor = Drawing.Color.FromArgb(sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubInteger2, sender.MenuItems(index).SubInteger3)
            ElseIf sender Is mTireSmoke Then
                Bennys.veh.TireSmokeColor = Drawing.Color.FromArgb(sender.MenuItems(index).SubInteger1, sender.MenuItems(index).SubInteger2, sender.MenuItems(index).SubInteger3)
                Bennys.veh.ToggleMod(VehicleToggleMod.TireSmoke, True)
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub New()
        _menuPool = New MenuPool()
        camera = New WorkshopCamera
        CreateMenus()
    End Sub

    Public Shared Sub CreateMenus()
        CreateMainMenu()
        CreateBodyworkMenu()
        CreateModMenuFor(mAerials, Helper.LocalizeModTitleName("AERIALS")) 'AERIALS
        CreateModMenuFor(mTrim, Helper.LocalizeModTitleName("TRIM")) 'TRIM
        CreateModMenuFor(mWindow, Helper.LocalizeModTitleName("WINDOWS")) 'WINDOWS
        CreateEngineMenu()
        CreatePerformanceMenuFor(mEngine, Helper.LocalizeModTitleName("ENGINE")) 'ENGINE
        CreateModMenuFor(mEngineBlock, Helper.LocalizeModTitleName("ENGINEBLOCK")) 'ENGINE BLOCK
        CreateModMenuFor(mAirFilter, Helper.LocalizeModTitleName("AIRFILTER")) 'AIR FILTER
        CreateModMenuFor(mStruts, Helper.LocalizeModTitleName("STRUTS")) 'STRUTS
        CreateInteriorMenu()
        CreateModMenuFor(mColumnShifterLevers, Helper.LocalizeModTitleName("COLUMNSHIFTERLEVERS")) 'COLUMN SHIFTER LEVERS
        CreateModMenuFor(mDashboard, Helper.LocalizeModTitleName("DASHBOARD")) 'DASHBOARD
        CreateColorMenuFor(mLightsColor, Helper.LocalizeModTitleName("LIGHTCOLOR"))
        CreateModMenuFor(mDialDesign, Helper.LocalizeModTitleName("DIALDESIGN")) 'DIAL DESIGN
        CreateModMenuFor(mOrnaments, Helper.LocalizeModTitleName("ORNAMENTS")) 'ORNAMENTS
        CreateModMenuFor(mSeats, Helper.LocalizeModTitleName("SEATS")) 'SEATS
        CreateModMenuFor(mSteeringWheels, Helper.LocalizeModTitleName("STEERINGWHEELS")) 'STEERING WHEELS
        CreateModMenuFor(mTrimDesign, Helper.LocalizeModTitleName("TRIMDESIGN")) 'TRIM DESIGN
        CreateColorMenuFor(mTrimColor, Helper.LocalizeModTitleName("TRIMCOLOR"))
        CreateModMenuFor(mDoor, Helper.LocalizeModTitleName("DOORS")) 'DOORS
        CreateBumperMenu()
        CreateModMenuFor(mFBumper, Helper.LocalizeModTitleName("FRONTBUMPERS")) 'FRONT BUMPERS
        CreateModMenuFor(mRBumper, Helper.LocalizeModTitleName("REARBUMPERS")) 'REAR BUMPERS
        CreateModMenuFor(mSSkirt, Helper.LocalizeModTitleName("SIDESKIRT")) 'SIDE SKIRT
        CreateWheelsMenu()
        CreateWheelTypeMenu()
        CreateModMenuFor(mBikeWheels, Helper.LocalizeModTitleName("BIKEWHEELS")) 'BIKE WHEELS
        CreateModMenuFor(mHighEnd, Helper.LocalizeModTitleName("HIGHEND")) 'HIGH END
        CreateModMenuFor(mLowrider, Helper.LocalizeModTitleName("LOWRIDER")) 'LOWRIDER
        CreateModMenuFor(mMuscle, Helper.LocalizeModTitleName("MUSCLE")) 'MUSCLE
        CreateModMenuFor(mOffroad, Helper.LocalizeModTitleName("OFFROAD")) 'OFFROAD
        CreateModMenuFor(mSport, Helper.LocalizeModTitleName("SPORT")) 'SPORT
        CreateModMenuFor(mSUV, Helper.LocalizeModTitleName("SUV")) 'SUV
        CreateModMenuFor(mTuner, Helper.LocalizeModTitleName("TUNER")) 'TUNER
        CreateModMenuFor(mBennysOriginals, Helper.LocalizeModTitleName("BENNYS")) 'BENNY'S ORIGINALS
        CreateModMenuFor(mBespoke, Helper.LocalizeModTitleName("BESPOKE")) 'BENNY'S BESPOKE
        CreateColorMenuFor(mRimColor, Helper.LocalizedModGroupName(Helper.GroupName.WheelColor))
        CreateTyresMenu()
        CreateModMenuFor(mTireSmoke, Helper.LocalizeModTitleName("TIRESMOKE"))
        CreatePlateMenu()
        CreateModMenuFor(mPlateHolder, Helper.LocalizeModTitleName("PLATEHOLDERS")) 'PLATE HOLDERS
        CreateModMenuFor(mVanityPlates, Helper.LocalizeModTitleName("VANITYPLATES")) 'VANITY PLATES
        CreatePlateNumberMenu()
        CreateLightsMenu()
        CreateModMenuFor(mHeadlights, Helper.LocalizeModTitleName("HEADLIGHTS")) 'HEADLIGHTS
        CreateNeonKitsMenu()
        CreateNeonMenu()
        CreateModMenuFor(mNeonColor, Helper.LocalizeModTitleName("NEONCOLOR"))
        CreateResprayMenu()
        CreateModMenuFor(mPrimaryColor, Helper.LocalizeModTitleName("COLORGROUPS"))
        CreateModMenuFor(mPrimaryClassicColor, Helper.LocalizeModTitleName("PRIMARYCOLOR"))
        CreateModMenuFor(mPrimaryChromeColor, Helper.LocalizeModTitleName("PRIMARYCOLOR"))
        CreateModMenuFor(mPrimaryMetallicColor, Helper.LocalizeModTitleName("PRIMARYCOLOR"))
        CreateModMenuFor(mPrimaryMetalsColor, Helper.LocalizeModTitleName("PRIMARYCOLOR"))
        CreateModMenuFor(mPrimaryMatteColor, Helper.LocalizeModTitleName("PRIMARYCOLOR"))
        CreateModMenuFor(mPrimaryPearlescentColor, Helper.LocalizeModTitleName("PRIMARYCOLOR"))
        CreateModMenuFor(mSecondaryColor, Helper.LocalizeModTitleName("COLORGROUPS"))
        CreateModMenuFor(mSecondaryClassicColor, Helper.LocalizeModTitleName("SECONDARYCOLOR"))
        CreateModMenuFor(mSecondaryChromeColor, Helper.LocalizeModTitleName("SECONDARYCOLOR"))
        CreateModMenuFor(mSecondaryMetallicColor, Helper.LocalizeModTitleName("SECONDARYCOLOR"))
        CreateModMenuFor(mSecondaryMetalsColor, Helper.LocalizeModTitleName("SECONDARYCOLOR"))
        CreateModMenuFor(mSecondaryMatteColor, Helper.LocalizeModTitleName("SECONDARYCOLOR"))
        CreateModMenuFor(mArchCover, Helper.LocalizeModTitleName("ARCHCOVERS")) 'ARCH COVERS
        CreateModMenuFor(mExhaust, Helper.LocalizeModTitleName("EXHAUST")) 'EXHAUST
        CreateModMenuFor(mFender, Helper.LocalizeModTitleName("FENDER")) 'FENDER
        CreateModMenuFor(mRFender, Helper.LocalizeModTitleName("RIGHTFENDER")) 'RIGHT FENDER
        CreateModMenuFor(mFrame, Helper.LocalizeModTitleName("ROLLCAGE")) 'ROLL CAGE
        CreateModMenuFor(mGrille, Helper.LocalizeModTitleName("GRILLES")) 'GRILLES
        CreateModMenuFor(mHood, Helper.LocalizeModTitleName("HOOD")) 'HOOD
        CreateModMenuFor(mHorn, Helper.LocalizeModTitleName("HORN")) 'HORN
        CreateModMenuFor(mHydraulics, Helper.LocalizeModTitleName("HYDRAULICS")) 'HYDRAULICS
        CreateModMenuFor(mLivery, Helper.LocalizeModTitleName("LIVERY")) 'LIVERY
        CreateModMenuFor(mPlaques, Helper.LocalizeModTitleName("PLAQUES")) 'PLAQUES
        CreateModMenuFor(mRoof, Helper.LocalizeModTitleName("ROOF")) 'ROOF
        CreateModMenuFor(mSpeakers, Helper.LocalizeModTitleName("SPEAKERS")) 'SPEAKERS
        CreateModMenuFor(mSpoilers, Helper.LocalizeModTitleName("SPOILER")) 'SPOILER
        CreateModMenuFor(mTank, Helper.LocalizeModTitleName("TANK")) 'TANK
        CreateModMenuFor(mTrunk, Helper.LocalizeModTitleName("TRUNKS")) 'TRUNKS
        CreateModMenuFor(mTurbo, Helper.LocalizeModTitleName("TURBO")) 'TURBO
        CreatePerformanceMenuFor(mSuspension, Helper.LocalizeModTitleName("SUSPENSIONS")) 'SUSPENSION
        CreatePerformanceMenuFor(mArmor, Helper.LocalizeModTitleName("ARMOR")) 'ARMOR
        CreatePerformanceMenuFor(mBrakes, Helper.LocalizeModTitleName("BRAKES")) 'BRAKES
        CreatePerformanceMenuFor(mTransmission, Helper.LocalizeModTitleName("TRANSMISSION")) 'TRANSMISSION
        CreateTintMenu()
    End Sub

    Public Shared Sub RefreshMenus()
        RefreshMainMenu()
        RefreshBodyworkMenu()
        RefreshModMenuFor(mAerials, iAerials, VehicleMod.Aerials)
        RefreshModMenuFor(mTrim, iTrim, VehicleMod.Trim)
        RefreshModMenuFor(mWindow, iWindows, VehicleMod.Windows)
        RefreshEngineMenu()
        RefreshPerformanceMenuFor(mEngine, iEngine, VehicleMod.Engine, "CMOD_ENG_")
        RefreshModMenuFor(mEngineBlock, iEngineBlock, VehicleMod.EngineBlock)
        RefreshModMenuFor(mAirFilter, iAirFilter, VehicleMod.AirFilter)
        RefreshModMenuFor(mStruts, iStruts, VehicleMod.Struts)
        RefreshInteriorMenu()
        RefreshModMenuFor(mColumnShifterLevers, iColumnShifterLevers, VehicleMod.ColumnShifterLevers)
        RefreshModMenuFor(mDashboard, iDashboard, VehicleMod.Dashboard)
        RefreshEnumModMenuFor(mLightsColor, iLightsColor, Helper.EnumTypes.VehicleColorDashboard)
        RefreshModMenuFor(mDialDesign, iDialDesign, VehicleMod.DialDesign)
        RefreshModMenuFor(mOrnaments, iOrnaments, VehicleMod.Ornaments)
        RefreshModMenuFor(mSeats, iSeats, VehicleMod.Seats)
        RefreshModMenuFor(mSteeringWheels, iSteeringWheels, VehicleMod.SteeringWheels)
        RefreshModMenuFor(mTrimDesign, iTrimDesign, VehicleMod.TrimDesign)
        RefreshEnumModMenuFor(mTrimColor, iTrimColor, Helper.EnumTypes.VehicleColorTrim)
        RefreshModMenuFor(mDoor, iDoor, VehicleMod.DoorSpeakers)
        RefreshBumperMenu()
        RefreshModMenuFor(mFBumper, iFBumper, VehicleMod.FrontBumper)
        RefreshModMenuFor(mRBumper, iRBumper, VehicleMod.RearBumper)
        RefreshModMenuFor(mSSkirt, iSideSkirt, VehicleMod.SideSkirt)
        RefreshWheelsMenu()
        RefreshWheelTypeMenu()
        RefreshModMenuFor(mBikeWheels, iBikeWheels, VehicleMod.BackWheels)
        RefreshModMenuFor(mHighEnd, iHighEnd, VehicleMod.FrontWheels)
        RefreshModMenuFor(mLowrider, iLowrider, VehicleMod.FrontWheels)
        RefreshModMenuFor(mMuscle, iMuscle, VehicleMod.FrontWheels)
        RefreshModMenuFor(mOffroad, iOffroad, VehicleMod.FrontWheels)
        RefreshModMenuFor(mSport, iSport, VehicleMod.FrontWheels)
        RefreshModMenuFor(mSUV, iSUV, VehicleMod.FrontWheels)
        RefreshModMenuFor(mTuner, iTuner, VehicleMod.FrontWheels)
        RefreshModMenuFor(mBennysOriginals, iBennys, VehicleMod.FrontWheels)
        RefreshModMenuFor(mBespoke, iBespoke, VehicleMod.FrontWheels)
        RefreshEnumModMenuFor(mRimColor, iRimColor, Helper.EnumTypes.VehicleColorRim)
        RefreshTyresMenu()
        RefreshRGBColorMenuFor(mTireSmoke, iTireSmoke, "Smoke")
        RefreshPlateMenu()
        RefreshModMenuFor(mPlateHolder, iPlateHolder, VehicleMod.PlateHolder)
        RefreshModMenuFor(mVanityPlates, iVanityPlates, VehicleMod.VanityPlates)
        RefreshEnumModMenuFor(mNumberPlate, iNumberPlate, Helper.EnumTypes.NumberPlateType)
        RefreshLightsMenu()
        RefreshModMenuFor(mHeadlights, iHeadlights, VehicleToggleMod.XenonHeadlights)
        RefreshNeonKitsMenu()
        RefreshNeonMenu()
        RefreshRGBColorMenuFor(mNeonColor, iNeonColor, "Neon")
        RefreshResprayMenu()
        RefreshPrimaryColorMenu()
        RefreshColorMenuFor(mPrimaryChromeColor, iPrimaryChromeColor, Helper.ChromeColor, "Primary")
        RefreshColorMenuFor(mPrimaryClassicColor, iPrimaryClassicColor, Helper.ClassicColor, "Primary")
        RefreshColorMenuFor(mPrimaryMetallicColor, iPrimaryMetallicColor, Helper.ClassicColor, "Primary")
        RefreshColorMenuFor(mPrimaryMetalsColor, iPrimaryMetalsColor, Helper.MetalColor, "Primary")
        RefreshColorMenuFor(mPrimaryMatteColor, iPrimaryMatteColor, Helper.MatteColor, "Primary")
        RefreshColorMenuFor(mPrimaryPearlescentColor, iPrimaryPearlescentColor, Helper.PearlescentColor, "Pearlescent")
        RefreshSecondaryColorMenu()
        RefreshColorMenuFor(mSecondaryChromeColor, iSecondaryChromeColor, Helper.ChromeColor, "Secondary")
        RefreshColorMenuFor(mSecondaryClassicColor, iSecondaryClassicColor, Helper.ClassicColor, "Secondary")
        RefreshColorMenuFor(mSecondaryMetallicColor, iSecondaryMetallicColor, Helper.ClassicColor, "Secondary")
        RefreshColorMenuFor(mSecondaryMetalsColor, iSecondaryMetalsColor, Helper.MetalColor, "Secondary")
        RefreshColorMenuFor(mSecondaryMatteColor, iSecondaryMatteColor, Helper.MatteColor, "Secondary")
        RefreshModMenuFor(mArchCover, iArchCover, VehicleMod.ArchCover)
        RefreshModMenuFor(mExhaust, iExhaust, VehicleMod.Exhaust)
        RefreshModMenuFor(mFender, iFender, VehicleMod.Fender)
        RefreshModMenuFor(mRFender, iRFender, VehicleMod.RightFender)
        RefreshModMenuFor(mFrame, iFrame, VehicleMod.Frame)
        RefreshModMenuFor(mGrille, iGrille, VehicleMod.Grille)
        RefreshModMenuFor(mHood, iHood, VehicleMod.Hood)
        RefreshModMenuFor(mHorn, iHorn, VehicleMod.Horns)
        RefreshModMenuFor(mHydraulics, iHydraulics, VehicleMod.Hydraulics)
        RefreshModMenuFor(mLivery, iLivery, VehicleMod.Livery)
        RefreshModMenuFor(mPlaques, iPlaques, VehicleMod.Plaques)
        RefreshModMenuFor(mRoof, iRoof, VehicleMod.Roof)
        RefreshModMenuFor(mSpeakers, iSpeaker, VehicleMod.Speakers)
        RefreshModMenuFor(mSpoilers, iSpoilers, VehicleMod.Spoilers)
        RefreshModMenuFor(mTank, iTank, VehicleMod.Tank)
        RefreshModMenuFor(mTrunk, iTrunk, VehicleMod.Trunk)
        RefreshModMenuFor(mTurbo, iTurbo, VehicleToggleMod.Turbo)
        RefreshPerformanceMenuFor(mSuspension, iSuspension, VehicleMod.Suspension, "CMOD_SUS_")
        RefreshPerformanceMenuFor(mArmor, iArmor, VehicleMod.Armor, "CMOD_ARM_")
        RefreshPerformanceMenuFor(mBrakes, iBrakes, VehicleMod.Brakes, "CMOD_BRA_")
        RefreshPerformanceMenuFor(mTransmission, iTransmission, VehicleMod.Transmission, "CMOD_GBX_")
        RefreshEnumModMenuFor(mTint, iTint, Helper.EnumTypes.VehicleWindowTint)
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            _menuPool.ProcessMenus()

            If Bennys.isCutscene Then
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                Select Case Game.Language
                    Case Language.Chinese, Language.Japanese, Language.Korean
                        Helper.DrawText(Bennys.veh.FriendlyName, New Drawing.Point(0, 550), 2.0, Drawing.Color.White, Helper.GTAFont.UIDefault, Helper.GTAFontAlign.Right, Helper.GTAFontStyleOptions.DropShadow)
                        Helper.DrawText(Helper.GetClassDisplayName(Bennys.veh.ClassType), New Drawing.Point(0, 600), 2.0, Drawing.Color.DodgerBlue, Helper.GTAFont.Script, Helper.GTAFontAlign.Right, Helper.GTAFontStyleOptions.DropShadow)
                    Case Else
                        Helper.DrawText(Bennys.veh.FriendlyName, New Drawing.Point(0, 550), 2.0, Drawing.Color.White, Helper.GTAFont.Title, Helper.GTAFontAlign.Right, Helper.GTAFontStyleOptions.DropShadow)
                        Helper.DrawText(Helper.GetClassDisplayName(Bennys.veh.ClassType), New Drawing.Point(0, 600), 2.0, Drawing.Color.DodgerBlue, Helper.GTAFont.Script, Helper.GTAFontAlign.Right, Helper.GTAFontStyleOptions.DropShadow)
                End Select
            End If

            Select Case True
                Case _menuPool.IsAnyMenuOpen()
                    Game.DisableControlThisFrame(0, Control.VehicleAccelerate)
                    Game.DisableControlThisFrame(0, Control.VehicleAim)
                    Game.DisableControlThisFrame(0, Control.VehicleAttack)
                    Game.DisableControlThisFrame(0, Control.VehicleAttack2)
                    Game.DisableControlThisFrame(0, Control.VehicleBrake)
                    Game.DisableControlThisFrame(0, Control.VehicleCinCam)
                    Game.DisableControlThisFrame(0, Control.VehicleDuck)
                    Game.DisableControlThisFrame(0, Control.VehicleExit)
                    Game.DisableControlThisFrame(0, Control.VehicleHeadlight)
                    Game.DisableControlThisFrame(0, Control.VehicleHorn)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveLeftOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveRightOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveRight)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnLeftRight)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnLeftOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnRightOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnHardLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnHardRight)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveLeftRight)
                    Game.DisableControlThisFrame(0, Control.VehicleLookLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleLookRight)
                    Game.DisableControlThisFrame(0, Control.VehicleHotwireLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleHotwireRight)
                    Game.DisableControlThisFrame(0, Control.VehicleGunLeftRight)
                    Game.DisableControlThisFrame(0, Control.VehicleGunLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleGunRight)
                    Game.DisableControlThisFrame(0, Control.VehicleCinematicLeftRight)
            End Select
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub
End Class
