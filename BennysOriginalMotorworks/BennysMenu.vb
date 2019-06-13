Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math
Imports System.Text
Imports System.Drawing
Imports Metadata

Public Class BennysMenu
    Inherits Script

    Public Shared lowriders As New List(Of Model) From {"banshee", "Buccaneer", "chino", "diablous", "comet2", "faction", "faction2", "fcr", "italigtb", "minivan", "moonbeam", "nero", "primo", "sabregt",
        "slamvan", "specter", "sultan", "tornado", "tornado2", "tornado3", "virgo3", "voodoo2", "elegy2", "technical", "insurgent"}
    Public Shared arenawar As New List(Of Model) From {"glendale", "gargoyle", "dominator", "dominator2", "impaler", "issi3", "ratloader", "ratloader2", "slamvan", "slamvan2", "slamvan3"}
    Public Shared bennysvehicle As New List(Of Model) From {"banshee2", "buccaneer2", "chino2", "diabolus2", "comet3", "faction2", "faction3", "fcr2", "italigtb2", "minivan2", "moonbeam2", "nero2", "primo2",
        "sabregt2", "specter2", "sultanrs", "tornado5", "virgo2", "voodoo", "elegy", "technical3", "insurgent3"}
    Public Shared arenavehicle As New List(Of Model) From {"bruiser", "bruiser2", "bruiser3", "cerberus", "cerberus2", "cerberus3", "deathbike", "deathbike2", "deathbike3", "dominator4", "dominator5",
        "dominator6", "impaler2", "impaler3", "impaler4", "imperator", "imperator2", "imperator3", "issi4", "issi5", "issi6", "monster3", "monster4", "monster5", "slamvan4", "slamvan5", "slamvan6", "brutus", "brutus2", "brutus3", "scarab", "scarab2",
        "scarab3", "zr380", "zr3802", "zr3803"}

    Public Shared QuitMenu, MainMenu, gmBodywork, gmBodyworkArena, gmEngine, gmInterior, gmPlate, gmLights, gmRespray, gmWheels, gmBumper, gmWheelType, gmNeonKits, gmWeapon As UIMenu
    Public Shared mAerials, mSuspension, mArmor, mBrakes, mEngine, mTransmission, mFBumper, mRBumper, mSSkirt, mTrim, mEngineBlock, mAirFilter, mStruts, mColumnShifterLevers, mDashboard, mDialDesign, mOrnaments, mSeats,
        mSteeringWheels, mTrimDesign, mPlateHolder, mVanityPlates, mNumberPlate, gmBikeWheels, gmHighEnd, gmLowrider, gmMuscle, gmOffroad, gmSport, gmSUV, gmTuner, mBennysOriginals, mBespoke, mTires, mHeadlights, mNeon, mNeonColor,
    mArchCover, mExhaust, mFender, mRFender, mDoor, mFrame, mGrille, mHood, mHorn, mHydraulics, mLivery, mPlaques, mRoof, mSpeakers, mSpoilers, mTank, mTrunk, mWindow, mTurbo, mTint, mLightsColor, mTrimColor, mRimColor,
    mPrimaryClassicColor, mPrimaryChromeColor, mPrimaryMetallicColor, mPrimaryMetalsColor, mPrimaryMatteColor, mPrimaryPearlescentColor, mPrimaryColor, mSecondaryColor, mSecondaryClassicColor, mSecondaryChromeColor,
    mSecondaryMetallicColor, mSecondaryMetalsColor, mSecondaryMatteColor, mTireSmoke, mTornadoC, mSBikeWheels, mCBikeWheels, mSHighEnd, mCHighEnd, mSLowrider, mCLowrider, mSMuscle, mCMuscle, mSOffroad, mCOffroad,
    mSSport, mCSport, mSSUV, mCSUV, mSTuner, mCTuner, mUpgradeAW, mNitro As UIMenu
    Public Shared iRepair, iHorn, iArmor, iBrakes, iFBumper, iExhaust, iFender, iRollcage, iRoof, iTransmission, iEngine, iPlate, iLights, iTint, iTurbo, iRespray, iWheels, iSuspension, iEngineBlock, iAerials, iAirFilter,
        iArchCover, iDoor, iFrame, iGrille, iHood, iHydraulics, iLivery, iPlaques, iRFender, iSpeaker, iSpoilers, iTank, iTrunk, iWindows, iTrim, iUpgrade, iUpgradeMod, iUpgradeAW, iUpgradeAWV, iStruts, iTrimColor, iColumnShifterLevers, iDashboard, iDialDesign,
        iOrnaments, iSeats, iSteeringWheels, iTrimDesign, iRBumper, iSideSkirt, iRimColor, iPlateHolder, iVanityPlates, iHeadlights, iDashboardColor, iNumberPlate, iBikeWheels, iHighEnd, iLowrider, iMuscle, iOffroad,
        iSport, iSUV, iTuner, iBennys, iBespoke, iTires, iBPTires, iNeon, iTireSmoke, iNeonColor, iLightsColor, iPrimaryCol, iSecondaryCol, iPrimaryChromeColor, iPrimaryClassicColor, iPrimaryMetallicColor, iPrimaryMetalsColor,
        iPrimaryMatteColor, iPrimaryPearlescentColor, iSecondaryChromeColor, iSecondaryClassicColor, iSecondaryMetallicColor, iSecondaryMetalsColor, iSecondaryMatteColor, iSecondaryPearlescentColor, iTornadoC, iNitro As UIMenuItem
    Public Shared giBodywork, giBodyworkArena, giEngine, giInterior, giPlate, giLights, giRespray, giWheels, giBumper, giWheelType, giTires, giNeonKits, giPrimaryCol, giSecondaryCol, giBikeWheels, giHighEndWheels, giDoor,
        giLowriderWheels, giMuscleWheels, giOffroadWheels, giSportWheels, giSUVWheels, giTunerWheels, giBennysWheels, giBespokeWheels, giFBumper, giRBumper, giSSkirt, giNumberPlate, giVanityPlate, giPlateHolder,
        giExhaust, giBrakes, giGrille, giHood, giHydraulics, giPlaques, giSpoilers, giTank, giTrunk, giStruts, iSBikeWheels, iCBikeWheels, iSHighEnd, iCHighEnd, iSLowrider, iCLowrider, iSMuscle, iCMuscle, iSOffroad, iCOffroad,
    iSSport, iCSport, iSSUV, iCSUV, iSTuner, iCTuner, giTrailer, giWeapon, giArchCover, giRoof, giAirfilter, giOrnaments As UIMenuItem
    Public Shared iShifter, iFMudguard, iBSeat, iOilTank, iRMudguard, iFuelTank, iBeltDriveCovers, iBEngineBlock, iBAirFilter, iBTank As UIMenuItem
    Public Shared giShifter, giFMudguard, giOilTank, giRMudguard, giFuelTank, giBeltDriveCovers, giBEngineBlock, giBAirFilter, giBTank As UIMenuItem
    Public Shared mShifter, mFMudguard, mBSeat, mOilTank, mRMudguard, mFuelTank, mBeltDriveCovers, mBEngineBlock, mBAirFilter, mBTank, gmTrailer As UIMenu
    Public Shared BtnZoom, BtnZoomOut, BtnFirstPerson As InstructionalButton
    Public Shared _menuPool As MenuPool
    Public Shared camera As WorkshopCamera
    Public Shared isRepairing As Boolean = False
    Public Shared vehicleStats As VehicleStats
    Public Shared arenaVehImage As String = "brusier_apoc"

    Public Shared Sub UpdateTitleName()
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

    Public Shared Sub CreateQuitMenu()
        Try
            QuitMenu = New UIMenu("", Game.GetGXTEntry("CMOD_MOD_E"))
            QuitMenu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            QuitMenu.MouseEdgeEnabled = False
            QuitMenu.AddInstructionalButton(BtnZoom)
            QuitMenu.AddInstructionalButton(BtnZoomOut)
            QuitMenu.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(QuitMenu)
            QuitMenu.AddItem(New UIMenuItem(Game.GetGXTEntry("ITEM_EXIT"), Game.GetGXTEntry("collision_6p1r1v")))
            QuitMenu.RefreshIndex()
            AddHandler QuitMenu.OnMenuClose, AddressOf MainMenuCloseHandler
            AddHandler QuitMenu.OnItemSelect, AddressOf MainMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateMainMenu()
        Try
            MainMenu = New UIMenu("", Game.GetGXTEntry("CMOD_MOD_T"), True)
            MainMenu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            MainMenu.MouseEdgeEnabled = False
            MainMenu.AddInstructionalButton(BtnZoom)
            MainMenu.AddInstructionalButton(BtnZoomOut)
            MainMenu.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(MainMenu)
            MainMenu.AddItem(New UIMenuItem("Noting"))
            MainMenu.RefreshIndex()
            AddHandler MainMenu.OnMenuClose, AddressOf MainMenuCloseHandler
            AddHandler MainMenu.OnItemSelect, AddressOf MainMenuItemSelectHandler
            'QuitMenu.BindMenuToItem(MainMenu, QuitMenu.MenuItems.FirstOrDefault)
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshMainMenu()
        Try
            MainMenu.MenuItems.Clear()

            If Bennys.veh.IsDamaged Then
                iRepair = New UIMenuItem(LocalizedModGroupName(GroupName.Repair), Game.GetGXTEntry("CMOD_MOD_0_D")) 'Repair
                With iRepair
                    .SetRightLabel("$" & Bennys.veh.GetRepairPrice)
                    .SubInteger1 = Bennys.veh.GetRepairPrice
                End With
                MainMenu.AddItem(iRepair)
                MainMenu.RefreshIndex()
                PlaySpeech("SHOP_SELL_REPAIR")
            ElseIf Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                'Specials
                If lowriders.Contains(Bennys.veh.Model) Then
                    iUpgrade = New UIMenuItem(LocalizedModGroupName(GroupName.Upgrade), Game.GetGXTEntry("CMOD_MOD_100_D")) 'Upgrade
                    With iUpgrade
                        .SetRightLabel("$" & Bennys.veh.Model.GetUpgradePrice)
                        .SubInteger1 = Bennys.veh.Model.GetUpgradePrice
                    End With
                    MainMenu.AddItem(iUpgrade)
                End If
                If Bennys.veh.DisplayName.IsUpgradeModExist Then
                    Dim upgrade2 As Tuple(Of String, Integer) = Bennys.veh.DisplayName.GetUpgradeModVehicleInfo
                    iUpgradeMod = New UIMenuItem(LocalizedModGroupName(GroupName.Upgrade), Game.GetGXTEntry("CMOD_MOD_100_D"))
                    With iUpgradeMod
                        .SetRightLabel("$" & upgrade2.Item2)
                        .SubInteger1 = upgrade2.Item2
                    End With
                    MainMenu.AddItem(iUpgradeMod)
                End If
                If arenawar.Contains(Bennys.veh.Model) Then
                    iUpgradeAW = New UIMenuItem(LocalizedModGroupName(GroupName.Upgrade2), Game.GetGXTEntry("collision_87oubis")) 'Arena War Upgrade
                    MainMenu.AddItem(iUpgradeAW)
                    MainMenu.BindMenuToItem(mUpgradeAW, iUpgradeAW)
                End If

                If arenavehicle.Contains(Bennys.veh.Model) Then 'Arena Vehicles
                    'Groups
                    If (Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Or Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Roof) <> 0) Then
                        giWeapon = New UIMenuItem(LocalizedModGroupName(GroupName.Weapons), Game.GetGXTEntry("CMOD_WEAPO_D")) 'weapons
                        MainMenu.AddItem(giWeapon)
                        MainMenu.BindMenuToItem(gmWeapon, giWeapon)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Or Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0) Then
                        giBodyworkArena = New UIMenuItem(LocalizedModGroupName(GroupName.Bodyworks), Game.GetGXTEntry("IE_BO_DT1")) 'bodywork arena
                        MainMenu.AddItem(giBodyworkArena)
                        MainMenu.BindMenuToItem(gmBodyworkArena, giBodyworkArena)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.EngineBlock)) Then
                        giEngine = New UIMenuItem(LocalizedModGroupName(GroupName.Engine), Game.GetGXTEntry("CMOD_SMOD_2_D")) 'engine
                        MainMenu.AddItem(giEngine)
                        MainMenu.BindMenuToItem(gmEngine, giEngine)
                    End If

                    'Single Item
                    If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                        giAirfilter = New UIMenuItem(LocalizedModTypeName(VehicleMod.AirFilter), Game.GetGXTEntry("SMOD_ENGINE_2"))
                        MainMenu.AddItem(giAirfilter)
                        MainMenu.BindMenuToItem(mAirFilter, giAirfilter)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                        giStruts = New UIMenuItem(LocalizedModTypeName(VehicleMod.Struts), Game.GetGXTEntry("SMOD_ENGINE_3b"))
                        MainMenu.AddItem(giStruts)
                        MainMenu.BindMenuToItem(mStruts, giStruts)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Then
                        giPlateHolder = New UIMenuItem(LocalizedModTypeName(VehicleMod.PlateHolder), Game.GetGXTEntry("CMOD_MOD_49_D"))
                        MainMenu.AddItem(giPlateHolder)
                        MainMenu.BindMenuToItem(mPlateHolder, giPlateHolder)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0 Then
                        iSpeaker = New UIMenuItem(LocalizedModTypeName(VehicleMod.Speakers), Game.GetGXTEntry("CMOD_MOD_58_D"))
                        MainMenu.AddItem(iSpeaker)
                        MainMenu.BindMenuToItem(mSpeakers, iSpeaker)
                    End If
                    giNumberPlate = New UIMenuItem(LocalizedModGroupName(GroupName.License), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Number Plate
                    MainMenu.AddItem(giNumberPlate)
                    MainMenu.BindMenuToItem(mNumberPlate, giNumberPlate)
                Else 'Bennys and regular motorcycles
                    'Groups
                    If (Bennys.veh.GetModCount(VehicleMod.Fender) <> 0 Or Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Hood) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Grille) <> 0 _
                        Or Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Spoilers) <> 0) Then
                        giBodywork = New UIMenuItem(LocalizedModGroupName(GroupName.Bodyworks), Game.GetGXTEntry("IE_BO_DT1")) 'bodywork
                        MainMenu.AddItem(giBodywork)
                        MainMenu.BindMenuToItem(gmBodywork, giBodywork)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0) Or Bennys.veh.CanInstallNitroMod Then
                        giEngine = New UIMenuItem(LocalizedModGroupName(GroupName.Engine), Game.GetGXTEntry("CMOD_SMOD_2_D")) 'engine
                        MainMenu.AddItem(giEngine)
                        MainMenu.BindMenuToItem(gmEngine, giEngine)
                    End If
                    giPlate = New UIMenuItem(LocalizedModGroupName(GroupName.Plate), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Plate
                    MainMenu.AddItem(giPlate)
                    MainMenu.BindMenuToItem(gmPlate, giPlate)
                End If

                giWheels = New UIMenuItem(LocalizedModGroupName(GroupName.Wheels), Game.GetGXTEntry("CMOD_MOD_60_D"))
                MainMenu.AddItem(giWheels)
                MainMenu.BindMenuToItem(gmWheels, giWheels)
                giLights = New UIMenuItem(LocalizedModGroupName(GroupName.Lights), Game.GetGXTEntry("CMOD_MOD_15_D"))  'CMOD_MOD_47_D
                MainMenu.AddItem(giLights)
                MainMenu.BindMenuToItem(gmLights, giLights)
                giRespray = New UIMenuItem(LocalizedModGroupName(GroupName.Respray), Game.GetGXTEntry("CMOD_MOD_6_D"))
                MainMenu.AddItem(giRespray)
                MainMenu.BindMenuToItem(gmRespray, giRespray)

                'Single Item
                If Bennys.veh.GetModCount(VehicleMod.Armor) <> 0 Then
                    iArmor = New UIMenuItem(LocalizedModTypeName(VehicleMod.Armor), Game.GetGXTEntry("CMOD_MOD_1_D"))
                    MainMenu.AddItem(iArmor)
                    MainMenu.BindMenuToItem(mArmor, iArmor)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Brakes) <> 0 Then
                    giBrakes = New UIMenuItem(LocalizedModTypeName(VehicleMod.Brakes), Game.GetGXTEntry("CMOD_MOD_3_D"))
                    MainMenu.AddItem(giBrakes)
                    MainMenu.BindMenuToItem(mBrakes, giBrakes)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Exhaust) <> 0 Then
                    giExhaust = New UIMenuItem(LocalizedModTypeName(VehicleMod.Exhaust), Game.GetGXTEntry("CMOD_MOD_16_D"))
                    MainMenu.AddItem(giExhaust)
                    MainMenu.BindMenuToItem(mExhaust, giExhaust)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Horns) <> 0 Then
                    iHorn = New UIMenuItem(LocalizedModTypeName(VehicleMod.Horns), Game.GetGXTEntry("CMOD_MOD_14_D"))
                    MainMenu.AddItem(iHorn)
                    MainMenu.BindMenuToItem(mHorn, iHorn)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hydraulics) <> 0 Then
                    giHydraulics = New UIMenuItem(LocalizedModTypeName(VehicleMod.Hydraulics), Game.GetGXTEntry("CMOD_SMOD_5_D"))
                    MainMenu.AddItem(giHydraulics)
                    MainMenu.BindMenuToItem(mHydraulics, giHydraulics)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Livery) <> 0 Then
                    iLivery = New UIMenuItem(LocalizedModTypeName(VehicleMod.Livery), Game.GetGXTEntry("CMOD_SMOD_6_D"))
                    MainMenu.AddItem(iLivery)
                    MainMenu.BindMenuToItem(mLivery, iLivery)
                End If
                If Bennys.veh.Livery2Count <> 0 Then
                    iTornadoC = New UIMenuItem(LocalizedModTypeName(VehicleMod.Roof), Game.GetGXTEntry("CMOD_SMOD_6_D"))
                    MainMenu.AddItem(iTornadoC)
                    MainMenu.BindMenuToItem(mTornadoC, iTornadoC)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                    giPlaques = New UIMenuItem(LocalizedModTypeName(VehicleMod.Plaques), Game.GetGXTEntry("SMOD_IN_PLAQUE"))
                    MainMenu.AddItem(giPlaques)
                    MainMenu.BindMenuToItem(mPlaques, giPlaques)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Suspension) <> 0 Then
                    iSuspension = New UIMenuItem(LocalizedModTypeName(VehicleMod.Suspension), Game.GetGXTEntry("CMOD_MOD_24_D"))
                    MainMenu.AddItem(iSuspension)
                    MainMenu.BindMenuToItem(mSuspension, iSuspension)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Transmission) <> 0 Then
                    iTransmission = New UIMenuItem(LocalizedModTypeName(VehicleMod.Transmission), Game.GetGXTEntry("CMOD_MOD_26_D"))
                    MainMenu.AddItem(iTransmission)
                    MainMenu.BindMenuToItem(mTransmission, iTransmission)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trunk) <> 0 Then
                    giTrunk = New UIMenuItem(LocalizedModTypeName(VehicleMod.Trunk), Game.GetGXTEntry("CMOD_MOD_62_D"))
                    MainMenu.AddItem(giTrunk)
                    MainMenu.BindMenuToItem(mTrunk, giTrunk)
                End If
                iTurbo = New UIMenuItem(LocalizedModTypeName(VehicleToggleMod.Turbo), Game.GetGXTEntry("CMOD_MOD_27_D"))
                MainMenu.AddItem(iTurbo)
                MainMenu.BindMenuToItem(mTurbo, iTurbo)
                MainMenu.RefreshIndex()
            Else
                'Specials
                If lowriders.Contains(Bennys.veh.Model) Then
                    iUpgrade = New UIMenuItem(LocalizedModGroupName(GroupName.Upgrade), Game.GetGXTEntry("CMOD_MOD_100_D")) 'Upgrade
                    With iUpgrade
                        .SetRightLabel("$" & Bennys.veh.Model.GetUpgradePrice)
                        .SubInteger1 = Bennys.veh.Model.GetUpgradePrice
                    End With
                    MainMenu.AddItem(iUpgrade)
                End If
                If Bennys.veh.DisplayName.IsUpgradeModExist Then
                    Dim upgrade2 As Tuple(Of String, Integer) = Bennys.veh.DisplayName.GetUpgradeModVehicleInfo
                    iUpgradeMod = New UIMenuItem(LocalizedModGroupName(GroupName.Upgrade), Game.GetGXTEntry("CMOD_MOD_100_D"))
                    With iUpgradeMod
                        .SetRightLabel("$" & upgrade2.Item2)
                        .SubInteger1 = upgrade2.Item2
                    End With
                    MainMenu.AddItem(iUpgradeMod)
                End If
                If arenawar.Contains(Bennys.veh.Model) Then
                    iUpgradeAW = New UIMenuItem(LocalizedModGroupName(GroupName.Upgrade2), Game.GetGXTEntry("collision_87oubis")) 'Arena War Upgrade
                    MainMenu.AddItem(iUpgradeAW)
                    MainMenu.BindMenuToItem(mUpgradeAW, iUpgradeAW)
                End If


                If arenavehicle.Contains(Bennys.veh.Model) Then 'Arena Vehicles
                    'Groups
                    If (Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Or Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Roof) <> 0) Then
                        giWeapon = New UIMenuItem(LocalizedModGroupName(GroupName.Weapons), Game.GetGXTEntry("CMOD_WEAPO_D")) 'weapons
                        MainMenu.AddItem(giWeapon)
                        MainMenu.BindMenuToItem(gmWeapon, giWeapon)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Or Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0) Then
                        giBodyworkArena = New UIMenuItem(LocalizedModGroupName(GroupName.Bodyworks), Game.GetGXTEntry("IE_BO_DT1")) 'bodywork arena
                        MainMenu.AddItem(giBodyworkArena)
                        MainMenu.BindMenuToItem(gmBodyworkArena, giBodyworkArena)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.EngineBlock)) Then
                        giEngine = New UIMenuItem(LocalizedModGroupName(GroupName.Engine), Game.GetGXTEntry("CMOD_SMOD_2_D")) 'engine
                        MainMenu.AddItem(giEngine)
                        MainMenu.BindMenuToItem(gmEngine, giEngine)
                    End If

                    'Single Item
                    If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                        giAirfilter = New UIMenuItem(LocalizedModTypeName(VehicleMod.AirFilter), Game.GetGXTEntry("SMOD_ENGINE_2"))
                        MainMenu.AddItem(giAirfilter)
                        MainMenu.BindMenuToItem(mAirFilter, giAirfilter)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                        giStruts = New UIMenuItem(LocalizedModTypeName(VehicleMod.Struts), Game.GetGXTEntry("SMOD_ENGINE_3b"))
                        MainMenu.AddItem(giStruts)
                        MainMenu.BindMenuToItem(mStruts, giStruts)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Then
                        giPlateHolder = New UIMenuItem(LocalizedModTypeName(VehicleMod.PlateHolder), Game.GetGXTEntry("CMOD_MOD_49_D"))
                        MainMenu.AddItem(giPlateHolder)
                        MainMenu.BindMenuToItem(mPlateHolder, giPlateHolder)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0 Then
                        iSpeaker = New UIMenuItem(LocalizedModTypeName(VehicleMod.Speakers), Game.GetGXTEntry("CMOD_MOD_58_D"))
                        MainMenu.AddItem(iSpeaker)
                        MainMenu.BindMenuToItem(mSpeakers, iSpeaker)
                    End If
                    giNumberPlate = New UIMenuItem(LocalizedModGroupName(GroupName.License), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Number Plate
                    MainMenu.AddItem(giNumberPlate)
                    MainMenu.BindMenuToItem(mNumberPlate, giNumberPlate)
                Else 'Bennys and regular vehicles
                    'Groups
                    If (Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Windows) <> 0 Or Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0) Then
                        giBodywork = New UIMenuItem(LocalizedModGroupName(GroupName.Bodyworks), Game.GetGXTEntry("IE_BO_DT1")) 'bodywork
                        MainMenu.AddItem(giBodywork)
                        MainMenu.BindMenuToItem(gmBodywork, giBodywork)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Or Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Struts) <> 0) Or Bennys.veh.CanInstallNitroMod Then
                        giEngine = New UIMenuItem(LocalizedModGroupName(GroupName.Engine), Game.GetGXTEntry("CMOD_SMOD_2_D")) 'engine
                        MainMenu.AddItem(giEngine)
                        MainMenu.BindMenuToItem(gmEngine, giEngine)
                    End If
                    If (Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Or Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 _
                        Or Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Or Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Or Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 _
                        Or Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0) Then
                        giInterior = New UIMenuItem(LocalizedModGroupName(GroupName.Interior), Game.GetGXTEntry("SMOD_IN_1")) 'interior
                        MainMenu.AddItem(giInterior)
                        MainMenu.BindMenuToItem(gmInterior, giInterior)
                    End If
                    giPlate = New UIMenuItem(LocalizedModGroupName(GroupName.Plate), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Plate
                    MainMenu.AddItem(giPlate)
                    MainMenu.BindMenuToItem(gmPlate, giPlate)

                    'Single Item
                    If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                        iFrame = New UIMenuItem(LocalizedModTypeName(VehicleMod.Frame), Game.GetGXTEntry("SMOD_ROLLCAGE_1"))
                        MainMenu.AddItem(iFrame)
                        MainMenu.BindMenuToItem(mFrame, iFrame)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Then
                        iRFender = New UIMenuItem(LocalizedModTypeName(VehicleMod.RightFender), Game.GetGXTEntry("CMOD_MOD_9_D"))
                        MainMenu.AddItem(iRFender)
                        MainMenu.BindMenuToItem(mRFender, iRFender)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Then
                        iRoof = New UIMenuItem(LocalizedModTypeName(VehicleMod.Roof), Game.GetGXTEntry("CMOD_MOD_73_D"))
                        MainMenu.AddItem(iRoof)
                        MainMenu.BindMenuToItem(mRoof, iRoof)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                        giTank = New UIMenuItem(LocalizedModTypeName(VehicleMod.Tank), Game.GetGXTEntry("CMOD_MOD_45_D"))
                        MainMenu.AddItem(giTank)
                        MainMenu.BindMenuToItem(mTank, giTank)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                        giPlaques = New UIMenuItem(LocalizedModTypeName(VehicleMod.Plaques), Game.GetGXTEntry("SMOD_IN_PLAQUE"))
                        MainMenu.AddItem(giPlaques)
                        MainMenu.BindMenuToItem(mPlaques, giPlaques)
                    End If
                End If

                If (Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0) Then
                    giBumper = New UIMenuItem(LocalizedModGroupName(GroupName.Bumpers), Game.GetGXTEntry("CMOD_MOD_4_D")) 'bumper
                    MainMenu.AddItem(giBumper)
                    MainMenu.BindMenuToItem(gmBumper, giBumper)
                End If

                giWheels = New UIMenuItem(LocalizedModGroupName(GroupName.Wheels), Game.GetGXTEntry("CMOD_MOD_60_D"))
                MainMenu.AddItem(giWheels)
                MainMenu.BindMenuToItem(gmWheels, giWheels)
                giLights = New UIMenuItem(LocalizedModGroupName(GroupName.Lights), Game.GetGXTEntry("CMOD_MOD_15_D"))  'CMOD_MOD_47_D
                MainMenu.AddItem(giLights)
                MainMenu.BindMenuToItem(gmLights, giLights)
                giRespray = New UIMenuItem(LocalizedModGroupName(GroupName.Respray), Game.GetGXTEntry("CMOD_MOD_6_D"))
                MainMenu.AddItem(giRespray)
                MainMenu.BindMenuToItem(gmRespray, giRespray)

                'Single Item
                If Bennys.veh.GetModCount(VehicleMod.Armor) <> 0 Then
                    iArmor = New UIMenuItem(LocalizedModTypeName(VehicleMod.Armor), Game.GetGXTEntry("CMOD_MOD_1_D"))
                    MainMenu.AddItem(iArmor)
                    MainMenu.BindMenuToItem(mArmor, iArmor)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Brakes) <> 0 Then
                    giBrakes = New UIMenuItem(LocalizedModTypeName(VehicleMod.Brakes), Game.GetGXTEntry("CMOD_MOD_3_D"))
                    MainMenu.AddItem(giBrakes)
                    MainMenu.BindMenuToItem(mBrakes, giBrakes)
                End If

                If Bennys.veh.GetModCount(VehicleMod.Exhaust) <> 0 Then
                    giExhaust = New UIMenuItem(LocalizedModTypeName(VehicleMod.Exhaust), Game.GetGXTEntry("CMOD_MOD_16_D"))
                    MainMenu.AddItem(giExhaust)
                    MainMenu.BindMenuToItem(mExhaust, giExhaust)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Fender) <> 0 Then
                    iFender = New UIMenuItem(LocalizedModTypeName(VehicleMod.Fender), Game.GetGXTEntry("CMOD_MOD_9_D"))
                    MainMenu.AddItem(iFender)
                    MainMenu.BindMenuToItem(mFender, iFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Grille) <> 0 Then
                    giGrille = New UIMenuItem(LocalizedModTypeName(VehicleMod.Grille), Game.GetGXTEntry("SMOD_CHASS_2c"))
                    MainMenu.AddItem(giGrille)
                    MainMenu.BindMenuToItem(mGrille, giGrille)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hood) <> 0 Then
                    giHood = New UIMenuItem(LocalizedModTypeName(VehicleMod.Hood), Game.GetGXTEntry("CMOD_MOD_72_D"))
                    MainMenu.AddItem(giHood)
                    MainMenu.BindMenuToItem(mHood, giHood)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Horns) <> 0 Then
                    iHorn = New UIMenuItem(LocalizedModTypeName(VehicleMod.Horns), Game.GetGXTEntry("CMOD_MOD_14_D"))
                    MainMenu.AddItem(iHorn)
                    MainMenu.BindMenuToItem(mHorn, iHorn)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hydraulics) <> 0 Then
                    giHydraulics = New UIMenuItem(LocalizedModTypeName(VehicleMod.Hydraulics), Game.GetGXTEntry("CMOD_SMOD_5_D"))
                    MainMenu.AddItem(giHydraulics)
                    MainMenu.BindMenuToItem(mHydraulics, giHydraulics)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Livery) <> 0 Then
                    iLivery = New UIMenuItem(LocalizedModTypeName(VehicleMod.Livery), Game.GetGXTEntry("CMOD_SMOD_6_D"))
                    MainMenu.AddItem(iLivery)
                    MainMenu.BindMenuToItem(mLivery, iLivery)
                End If
                If Bennys.veh.Livery2Count <> 0 Then
                    iTornadoC = New UIMenuItem(LocalizedModTypeName(VehicleMod.Roof), Game.GetGXTEntry("CMOD_MOD_73_D"))
                    MainMenu.AddItem(iTornadoC)
                    MainMenu.BindMenuToItem(mTornadoC, iTornadoC)
                End If

                If Bennys.veh.GetModCount(VehicleMod.Spoilers) <> 0 Then
                    giSpoilers = New UIMenuItem(LocalizedModTypeName(VehicleMod.Spoilers), Game.GetGXTEntry("CMOD_MOD_37_D"))
                    MainMenu.AddItem(giSpoilers)
                    MainMenu.BindMenuToItem(mSpoilers, giSpoilers)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Suspension) <> 0 Then
                    iSuspension = New UIMenuItem(LocalizedModTypeName(VehicleMod.Suspension), Game.GetGXTEntry("CMOD_MOD_24_D"))
                    MainMenu.AddItem(iSuspension)
                    MainMenu.BindMenuToItem(mSuspension, iSuspension)
                End If

                If Bennys.veh.GetModCount(VehicleMod.Transmission) <> 0 Then
                    iTransmission = New UIMenuItem(LocalizedModTypeName(VehicleMod.Transmission), Game.GetGXTEntry("CMOD_MOD_26_D"))
                    MainMenu.AddItem(iTransmission)
                    MainMenu.BindMenuToItem(mTransmission, iTransmission)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trunk) <> 0 Then
                    giTrunk = New UIMenuItem(LocalizedModTypeName(VehicleMod.Trunk), Game.GetGXTEntry("CMOD_MOD_62_D"))
                    MainMenu.AddItem(giTrunk)
                    MainMenu.BindMenuToItem(mTrunk, giTrunk)
                End If
                iTurbo = New UIMenuItem(LocalizedModTypeName(VehicleToggleMod.Turbo), Game.GetGXTEntry("CMOD_MOD_27_D"))
                MainMenu.AddItem(iTurbo)
                MainMenu.BindMenuToItem(mTurbo, iTurbo)
                If Bennys.veh.HasBone("windscreen") Then
                    iTint = New UIMenuItem(LocalizedModGroupName(GroupName.Windows), Game.GetGXTEntry("CMOD_MOD_29_D"))
                    MainMenu.AddItem(iTint)
                    MainMenu.BindMenuToItem(mTint, iTint)
                End If
                'If IsVehicleAttachedToTrailer(Bennys.veh) Then
                '    giTrailer = New UIMenuItem(Game.GetGXTEntry("TRAILER"))
                '    MainMenu.AddItem(giTrailer)
                '    MainMenu.BindMenuToItem(gmTrailer, giTrailer)
                'End If
                MainMenu.RefreshIndex()
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuCloseHandler(sender As UIMenu)
        Try
            If sender Is QuitMenu Then
                MainMenu.Visible = True
            ElseIf sender Is MainMenu Then
                QuitMenu.Visible = True
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If sender Is MainMenu Then
                If selectedItem Is iRepair Then
                    isRepairing = True
                    Bennys.veh.Repair()
                    Bennys.veh.Wash()
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger1)
                    RefreshMenus()
                ElseIf selectedItem Is iUpgrade Then
                    Game.FadeScreenOut(1000)
                    Wait(1000)
                    Dim veh As Vehicle = World.CreateVehicle(LowriderUpgrade(Bennys.veh.Model), Bennys.veh.Position, Bennys.veh.Heading)
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
                    veh.SetLivery2(Bennys.lastVehMemory.Livery2)
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
                    veh.SetXenonHeadlightsColor(Bennys.lastVehMemory.HeadlightsColor, veh.IsToggleModOn(VehicleToggleMod.XenonHeadlights))
                    veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
                    veh.NumberPlate = Bennys.lastVehMemory.PlateNumbers
                    veh.CanTiresBurst = Bennys.lastVehMemory.BulletProofTires
                    If IsNitroModInstalled() Then veh.SetBool(nitroMod, Bennys.lastVehMemory.Nitro)
                    Bennys.veh.Delete()
                    Bennys.ply.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                    Bennys.veh = veh
                    veh.InstallModKit()
                    MainMenu.MenuItems.Remove(selectedItem)
                    isRepairing = True
                    RefreshMenus()
                    camera.RepositionFor(veh)
                    Wait(1000)
                    Game.FadeScreenIn(1000)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger1)
                    Native.Function.Call(Hash._START_SCREEN_EFFECT, "MP_corona_switch_supermod", 0, 1)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "Lowrider_Upgrade", "Lowrider_Super_Mod_Garage_Sounds", 1)
                    PlaySpeech("LR_UPGRADE_SUPERMOD")
                ElseIf selectedItem Is iUpgradeMod Then
                    Game.FadeScreenOut(1000)
                    Wait(1000)
                    Dim upgrade2 As Tuple(Of String, Integer) = Bennys.veh.DisplayName.GetUpgradeModVehicleInfo
                    Dim veh As Vehicle = World.CreateVehicle(upgrade2.Item1, Bennys.veh.Position, Bennys.veh.Heading)
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
                    veh.SetLivery2(Bennys.lastVehMemory.Livery2)
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
                    veh.SetXenonHeadlightsColor(Bennys.lastVehMemory.HeadlightsColor, veh.IsToggleModOn(VehicleToggleMod.XenonHeadlights))
                    veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
                    veh.NumberPlate = Bennys.lastVehMemory.PlateNumbers
                    veh.CanTiresBurst = Bennys.lastVehMemory.BulletProofTires
                    If IsNitroModInstalled() Then veh.SetBool(nitroMod, Bennys.lastVehMemory.Nitro)
                    Bennys.veh.Delete()
                    Bennys.ply.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                    Bennys.veh = veh
                    veh.InstallModKit()
                    MainMenu.MenuItems.Remove(selectedItem)
                    isRepairing = True
                    RefreshMenus()
                    camera.RepositionFor(veh)
                    Wait(1000)
                    Game.FadeScreenIn(1000)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger1)
                    Native.Function.Call(Hash._START_SCREEN_EFFECT, "MP_corona_switch_supermod", 0, 1)
                    Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "Lowrider_Upgrade", "Lowrider_Super_Mod_Garage_Sounds", 1)
                    PlaySpeech("LR_UPGRADE_SUPERMOD")
                ElseIf selectedItem Is iUpgradeAW Then
                    Dim sitem = mUpgradeAW.MenuItems.First
                    arenaVehImage = sitem.SubString2
                ElseIf selectedItem Is giEngine Then
                    Select Case Bennys.veh.Model
                        Case "alpha"
                            Bennys.veh.OpenDoor(VehicleDoor.Hood, False, False)
                            camera.MainCameraPosition = CameraPosition.Hood
                        Case Else
                            If Not Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                                HoodCamera(True)
                            Else
                                camera.MainCameraPosition = CameraPosition.Wheels
                            End If
                    End Select
                ElseIf selectedItem Is giInterior Then
                    If Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                        camera.MainCameraPosition = CameraPosition.Car
                    Else
                        camera.MainCameraPosition = CameraPosition.Interior
                    End If
                ElseIf selectedItem Is giWheels Then
                    camera.MainCameraPosition = CameraPosition.Wheels
                ElseIf selectedItem Is giLights Then
                    Bennys.veh.HighBeamsOn = True
                    Bennys.veh.LightsOn = True
                ElseIf selectedItem Is giExhaust Then
                    Select Case Bennys.veh.Model
                        Case "sultanrs", "guardian", "ratloader", "ratloader2", "banshee", "mamba", "feltzer3", "le7b", "barrage"
                            camera.MainCameraPosition = CameraPosition.Wheels
                        Case "police3"
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Case "tornado6"
                            camera.MainCameraPosition = CameraPosition.Engine
                        Case Else
                            If Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                                camera.MainCameraPosition = CameraPosition.BikeExhaust
                            Else
                                camera.MainCameraPosition = CameraPosition.Exhaust 'CameraPosition.RearBumper
                            End If
                    End Select
                ElseIf selectedItem Is giBrakes Then
                    camera.MainCameraPosition = CameraPosition.Wheels
                ElseIf selectedItem Is giGrille Then
                    Select Case Bennys.veh.Model
                        Case "penetrator", "torero", "viseris"
                            camera.MainCameraPosition = CameraPosition.RearEngine
                        Case "banshee2"
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Case "zr3802"
                            camera.MainCameraPosition = CameraPosition.Car
                        Case Else
                            camera.MainCameraPosition = CameraPosition.Grille
                    End Select
                ElseIf selectedItem Is giHood Then
                    HoodCamera(False)
                ElseIf selectedItem Is giHydraulics Then
                    Bennys.veh.OpenDoor(VehicleDoor.Trunk, False, False)
                    camera.MainCameraPosition = CameraPosition.Trunk
                ElseIf selectedItem Is giTrunk Then
                    Bennys.veh.OpenDoor(VehicleDoor.Trunk, False, False)
                    camera.MainCameraPosition = CameraPosition.Trunk
                ElseIf selectedItem Is giPlaques Then
                    camera.MainCameraPosition = CameraPosition.Plaque
                ElseIf selectedItem Is giSpoilers Then
                    If Bennys.veh.HasBone("boot") Then
                        If Bennys.veh.GetVehTrunkPos = EngineLoc.rear Then
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Else
                            If Bennys.veh.HasBone("windscreen_r") Then
                                camera.MainCameraPosition = CameraPosition.RearWindscreen
                            Else
                                camera.MainCameraPosition = CameraPosition.RearEngine
                            End If
                        End If
                    ElseIf Bennys.veh.HasBone("windscreen_r") Then
                        camera.MainCameraPosition = CameraPosition.RearWindscreen
                    ElseIf Bennys.veh.GetVehEnginePos = EngineLoc.rear Then
                        Select Case Bennys.veh.Model
                            Case "barrage"
                                camera.MainCameraPosition = CameraPosition.Car
                            Case Else
                                camera.MainCameraPosition = CameraPosition.RearEngine
                        End Select
                    ElseIf Bennys.veh.HasBone("neon_b") Then
                        camera.MainCameraPosition = CameraPosition.RearBumper
                    Else
                        camera.MainCameraPosition = CameraPosition.Car
                    End If
                ElseIf selectedItem Is giTank Then
                    Select Case Bennys.veh.Model
                        Case "slamvan3"
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Case "elegy"
                            camera.MainCameraPosition = CameraPosition.FrontPlate
                        Case Else
                            camera.MainCameraPosition = CameraPosition.Tank
                    End Select
                ElseIf (selectedItem Is giAirfilter) Or (selectedItem Is giStruts) Then
                    Select Case Bennys.veh.Model
                        Case "zr380", "zr3802", "zr3803", "issi4", "issi5", "issi6"
                            camera.MainCameraPosition = CameraPosition.Boost
                        Case "bruiser", "bruiser2", "bruiser3", "cerberus", "cerberus2", "cerberus3", "deathbike", "deathbike2", "deathbike3", "dominator4", "dominator5", "dominator6",
                             "impaler2", "impaler3", "impaler4", "imperator", "imperator2", "imperator3", "monster3", "monster4", "monster5", "slamvan4", "slamvan5", "slamvan6",
                             "brutus", "brutus2", "brutus3", "scarab", "scarab2", "scarab3"
                            HoodCamera(False)
                        Case Else
                            HoodCamera(True)
                    End Select
                ElseIf selectedItem Is giNumberPlate Then
                    If Bennys.veh.HasBone("platelight") Then
                        If Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                            camera.MainCameraPosition = CameraPosition.Car
                        Else
                            camera.MainCameraPosition = CameraPosition.BackPlate
                        End If
                    ElseIf Bennys.veh.HasBone("neon_f") Then
                        Select Case Bennys.veh.Model
                            Case "stromberg", "z190", "comet4", "autarch"
                                camera.MainCameraPosition = CameraPosition.Car
                            Case Else
                                camera.MainCameraPosition = CameraPosition.FrontPlate
                        End Select
                    Else
                        camera.MainCameraPosition = CameraPosition.Car
                    End If
                End If
            ElseIf sender Is QuitMenu Then
                QuitMenu.Visible = False
                Bennys.PlayExitCutScene()
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateArenaWarMenu()
        Try
            mUpgradeAW = New UIMenu("", Game.GetGXTEntry("collision_9znude7"), False) 'ARENA WAR
            mUpgradeAW.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mUpgradeAW.MouseEdgeEnabled = False
            mUpgradeAW.AddInstructionalButton(BtnZoom)
            mUpgradeAW.AddInstructionalButton(BtnZoomOut)
            mUpgradeAW.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(mUpgradeAW)
            mUpgradeAW.AddItem(New UIMenuItem("Nothing"))
            mUpgradeAW.RefreshIndex()
            AddHandler mUpgradeAW.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mUpgradeAW.OnIndexChange, AddressOf ArenaWarMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshArenaWarMenu()
        Try
            mUpgradeAW.MenuItems.Clear()

            Select Case Bennys.veh.Model
                Case "glendale"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("bruiser"))
                    With iUpgradeAWV
                        .SubString1 = "bruiser"
                        .SubString2 = "bruiser_apoc"
                        .SubInteger1 = 1609000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("bruiser2"))
                    With iUpgradeAWV
                        .SubString1 = "bruiser2"
                        .SubString2 = "bruiser_scifi"
                        .SubInteger1 = 1609000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("bruiser3"))
                    With iUpgradeAWV
                        .SubString1 = "bruiser3"
                        .SubString2 = "bruiser_cons"
                        .SubInteger1 = 1609000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                Case "gargoyle"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("deathbike"))
                    With iUpgradeAWV
                        .SubString1 = "deathbike"
                        .SubString2 = "deathbike_apoc"
                        .SubInteger1 = 1269000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("deathbike2"))
                    With iUpgradeAWV
                        .SubString1 = "deathbike2"
                        .SubString2 = "deathbike_scifi"
                        .SubInteger1 = 1269000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("deathbike3"))
                    With iUpgradeAWV
                        .SubString1 = "deathbike3"
                        .SubString2 = "deathbike_cons"
                        .SubInteger1 = 1269000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                Case "dominator", "dominator2"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("dominator4"))
                    With iUpgradeAWV
                        .SubString1 = "dominator4"
                        .SubString2 = "dominator_apoc"
                        .SubInteger1 = 1132000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("dominator5"))
                    With iUpgradeAWV
                        .SubString1 = "dominator5"
                        .SubString2 = "dominator_scifi"
                        .SubInteger1 = 1132000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("dominator6"))
                    With iUpgradeAWV
                        .SubString1 = "dominator6"
                        .SubString2 = "dominator_cons"
                        .SubInteger1 = 1132000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                Case "impaler"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("impaler2"))
                    With iUpgradeAWV
                        .SubString1 = "impaler2"
                        .SubString2 = "impaler_apoc"
                        .SubInteger1 = 1209500
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("impaler3"))
                    With iUpgradeAWV
                        .SubString1 = "impaler3"
                        .SubString2 = "impaler_scifi"
                        .SubInteger1 = 1209500
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("impaler4"))
                    With iUpgradeAWV
                        .SubString1 = "impaler4"
                        .SubString2 = "impaler_cons"
                        .SubInteger1 = 1209500
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                Case "issi3"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("issi4"))
                    With iUpgradeAWV
                        .SubString1 = "issi4"
                        .SubString2 = "issi_apoc"
                        .SubInteger1 = 1089000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("issi5"))
                    With iUpgradeAWV
                        .SubString1 = "issi5"
                        .SubString2 = "issi_scifi"
                        .SubInteger1 = 1089000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("issi6"))
                    With iUpgradeAWV
                        .SubString1 = "issi6"
                        .SubString2 = "issi_cons"
                        .SubInteger1 = 1089000
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                Case "ratloader", "ratloader2"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("monster3"))
                    With iUpgradeAWV
                        .SubString1 = "monster3"
                        .SubString2 = "sasquatch_apoc"
                        .SubInteger1 = 1530875
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("monster4"))
                    With iUpgradeAWV
                        .SubString1 = "monster4"
                        .SubString2 = "sasquatch_scifi"
                        .SubInteger1 = 1530875
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("monster5"))
                    With iUpgradeAWV
                        .SubString1 = "monster5"
                        .SubString2 = "sasquatch_cons"
                        .SubInteger1 = 1530875
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                Case "slamvan", "slamvan2", "slamvan3"
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("slamvan4"))
                    With iUpgradeAWV
                        .SubString1 = "slamvan4"
                        .SubString2 = "slamvan_apoc"
                        .SubInteger1 = 1321875
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("slamvan5"))
                    With iUpgradeAWV
                        .SubString1 = "slamvan5"
                        .SubString2 = "slamvan_scifi"
                        .SubInteger1 = 1321875
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
                    iUpgradeAWV = New UIMenuItem(Game.GetGXTEntry("slamvan6"))
                    With iUpgradeAWV
                        .SubString1 = "slamvan6"
                        .SubString2 = "slamvan_cons"
                        .SubInteger1 = 1321875
                        Dim price As String = "$" & .SubInteger1
                        .SetRightLabel(price)
                    End With
                    mUpgradeAW.AddItem(iUpgradeAWV)
            End Select

            mUpgradeAW.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBodyworkMenu()
        Try
            gmBodywork = New UIMenu("", Game.GetGXTEntry("CMOD_BW_T"), True) 'BODYWORK
            gmBodywork.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBodywork.MouseEdgeEnabled = False
            gmBodywork.AddInstructionalButton(BtnZoom)
            gmBodywork.AddInstructionalButton(BtnZoomOut)
            gmBodywork.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(gmBodywork)
            gmBodywork.AddItem(New UIMenuItem("Nothing"))
            gmBodywork.RefreshIndex()
            AddHandler gmBodywork.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBodyworkMenu()
        Try
            gmBodywork.MenuItems.Clear()

            If Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                If Bennys.veh.GetModCount(VehicleMod.Fender) <> 0 Then
                    giShifter = New UIMenuItem(LocalizedModTypeName(VehicleMod.Fender), Game.GetGXTEntry("CMOD_MOD_SHI_D"))
                    gmBodywork.AddItem(giShifter)
                    gmBodywork.BindMenuToItem(mShifter, giShifter)
                End If
                If Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Then
                    giFMudguard = New UIMenuItem(LocalizedModTypeName(VehicleMod.FrontBumper), Game.GetGXTEntry("CMOD_MOD_43_D"))
                    gmBodywork.AddItem(giFMudguard)
                    gmBodywork.BindMenuToItem(mFMudguard, giFMudguard)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hood) <> 0 Then
                    iBSeat = New UIMenuItem(LocalizedModTypeName(VehicleMod.Hood), Game.GetGXTEntry("CMOD_MOD_44_D"))
                    gmBodywork.AddItem(iBSeat)
                    gmBodywork.BindMenuToItem(mBSeat, iBSeat)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Grille) <> 0 Then
                    giOilTank = New UIMenuItem(LocalizedModTypeName(VehicleMod.Grille), Game.GetGXTEntry("CMOD_MOD_OT_D"))
                    gmBodywork.AddItem(giOilTank)
                    gmBodywork.BindMenuToItem(mOilTank, giOilTank)
                End If
                If Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Then
                    giRMudguard = New UIMenuItem(LocalizedModTypeName(VehicleMod.RearBumper), Game.GetGXTEntry("CMOD_MOD_43_D"))
                    gmBodywork.AddItem(giRMudguard)
                    gmBodywork.BindMenuToItem(mRMudguard, giRMudguard)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Then
                    giFuelTank = New UIMenuItem(LocalizedModTypeName(VehicleMod.Roof), Game.GetGXTEntry("CMOD_MOD_FUT_D"))
                    gmBodywork.AddItem(giFuelTank)
                    gmBodywork.BindMenuToItem(mFuelTank, giFuelTank)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Spoilers) <> 0 Then
                    giBeltDriveCovers = New UIMenuItem(LocalizedModTypeName(VehicleMod.Spoilers), Game.GetGXTEntry("CMOD_MOD_BEC_D"))
                    gmBodywork.AddItem(giBeltDriveCovers)
                    gmBodywork.BindMenuToItem(mBeltDriveCovers, giBeltDriveCovers)
                End If
                If Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Then
                    iRFender = New UIMenuItem(LocalizedModTypeName(VehicleMod.RightFender), Game.GetGXTEntry("CMOD_MOD_41_D"))
                    gmBodywork.AddItem(iRFender)
                    gmBodywork.BindMenuToItem(mRFender, iRFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                    giBTank = New UIMenuItem(LocalizedModTypeName(VehicleMod.Tank), Game.GetGXTEntry("CMOD_MOD_45_D"))
                    gmBodywork.AddItem(giBTank)
                    gmBodywork.BindMenuToItem(mBTank, giBTank)
                End If
            Else
                If Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Then
                    iAerials = New UIMenuItem(LocalizedModTypeName(VehicleMod.Aerials), Game.GetGXTEntry("SMOD_CHASS_6"))
                    gmBodywork.AddItem(iAerials)
                    gmBodywork.BindMenuToItem(mAerials, iAerials)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                    iTrim = New UIMenuItem(LocalizedModTypeName(VehicleMod.Trim), Game.GetGXTEntry("SMOD_CHASS_1b"))
                    gmBodywork.AddItem(iTrim)
                    gmBodywork.BindMenuToItem(mTrim, iTrim)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Windows) <> 0 Then
                    iWindows = New UIMenuItem(LocalizedModTypeName(VehicleMod.Windows), Game.GetGXTEntry("SMOD_CHASS_5"))
                    gmBodywork.AddItem(iWindows)
                    gmBodywork.BindMenuToItem(mWindow, iWindows)
                End If
                If Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Then
                    iArchCover = New UIMenuItem(LocalizedModTypeName(VehicleMod.ArchCover), Game.GetGXTEntry("SMOD_CHASS_1c")) 'Arch Covers
                    gmBodywork.AddItem(iArchCover)
                    gmBodywork.BindMenuToItem(mArchCover, iArchCover)
                End If
            End If

            gmBodywork.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBodyworkArenaMenu()
        Try
            gmBodyworkArena = New UIMenu("", Game.GetGXTEntry("CMOD_BW_T"), True) 'BODYWORK
            gmBodyworkArena.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBodyworkArena.MouseEdgeEnabled = False
            gmBodyworkArena.AddInstructionalButton(BtnZoom)
            gmBodyworkArena.AddInstructionalButton(BtnZoomOut)
            gmBodyworkArena.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(gmBodyworkArena)
            gmBodyworkArena.AddItem(New UIMenuItem("Nothing"))
            gmBodyworkArena.RefreshIndex()
            AddHandler gmBodyworkArena.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBodyworkArenaMenu()
        Try
            gmBodyworkArena.MenuItems.Clear()

            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                    iPlaques = New UIMenuItem(LocalizedModTypeName(VehicleMod.Plaques), Game.GetGXTEntry("collision_di2ru")) 'Decoration
                    gmBodyworkArena.AddItem(iPlaques)
                    gmBodyworkArena.BindMenuToItem(mPlaques, iPlaques)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                    iFrame = New UIMenuItem(LocalizedModTypeName(VehicleMod.Frame), Game.GetGXTEntry("CMOD_ARMPL_D")) 'Armour plating
                    gmBodyworkArena.AddItem(iFrame)
                    gmBodyworkArena.BindMenuToItem(mFrame, iFrame)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Then
                    iAerials = New UIMenuItem(LocalizedModTypeName(VehicleMod.Aerials), Game.GetGXTEntry("collision_37l2i4l")) 'Spikes
                    gmBodyworkArena.AddItem(iAerials)
                    gmBodyworkArena.BindMenuToItem(mAerials, iAerials)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                    iTrim = New UIMenuItem(LocalizedModTypeName(VehicleMod.Trim), Game.GetGXTEntry("collision_8t77hko")) 'Blades
                    gmBodyworkArena.AddItem(iTrim)
                    gmBodyworkArena.BindMenuToItem(mTrim, iTrim)
                End If
                If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                    giVanityPlate = New UIMenuItem(LocalizedModTypeName(VehicleMod.VanityPlates), Game.GetGXTEntry("collision_7we93ne")) 'Rear Wibbles
                    gmBodyworkArena.AddItem(giVanityPlate)
                    gmBodyworkArena.BindMenuToItem(mVanityPlates, giVanityPlate)
                End If
            Else
                If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                    iPlaques = New UIMenuItem(LocalizedModTypeName(VehicleMod.Plaques), Game.GetGXTEntry("collision_di2ru")) 'Decoration
                    gmBodyworkArena.AddItem(iPlaques)
                    gmBodyworkArena.BindMenuToItem(mPlaques, iPlaques)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                    iFrame = New UIMenuItem(LocalizedModTypeName(VehicleMod.Frame), Game.GetGXTEntry("CMOD_ARMPL_D")) 'Armour plating
                    gmBodyworkArena.AddItem(iFrame)
                    gmBodyworkArena.BindMenuToItem(mFrame, iFrame)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Then
                    iAerials = New UIMenuItem(LocalizedModTypeName(VehicleMod.Aerials), Game.GetGXTEntry("collision_37l2i4l")) 'Spikes
                    gmBodyworkArena.AddItem(iAerials)
                    gmBodyworkArena.BindMenuToItem(mAerials, iAerials)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                    iTrim = New UIMenuItem(LocalizedModTypeName(VehicleMod.Trim), Game.GetGXTEntry("collision_8t77hko")) 'Blades
                    gmBodyworkArena.AddItem(iTrim)
                    gmBodyworkArena.BindMenuToItem(mTrim, iTrim)
                End If
                If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                    giVanityPlate = New UIMenuItem(LocalizedModTypeName(VehicleMod.VanityPlates), Game.GetGXTEntry("collision_7we93ne")) 'Rear Wibbles
                    gmBodyworkArena.AddItem(giVanityPlate)
                    gmBodyworkArena.BindMenuToItem(mVanityPlates, giVanityPlate)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Then
                    giOrnaments = New UIMenuItem(LocalizedModTypeName(VehicleMod.Ornaments), Game.GetGXTEntry("CMOD_MOD_53_D")) 'Roll Cage
                    gmBodyworkArena.AddItem(giOrnaments)
                    gmBodyworkArena.BindMenuToItem(mOrnaments, giOrnaments)
                End If
            End If

            gmBodyworkArena.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWeaponMenu()
        Try
            gmWeapon = New UIMenu("", Game.GetGXTEntry("PM_SCR_WEA"), True) 'WEAPONS
            gmWeapon.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmWeapon.MouseEdgeEnabled = False
            gmWeapon.AddInstructionalButton(BtnZoom)
            gmWeapon.AddInstructionalButton(BtnZoomOut)
            gmWeapon.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(gmWeapon)
            gmWeapon.AddItem(New UIMenuItem("Nothing"))
            gmWeapon.RefreshIndex()
            AddHandler gmWeapon.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWeaponMenu()
        Try
            gmWeapon.MenuItems.Clear()

            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then
                If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                    giTank = New UIMenuItem(LocalizedModTypeName(VehicleMod.Tank), Game.GetGXTEntry("collision_255bdwf")) 'Primary Weapons
                    gmWeapon.AddItem(giTank)
                    gmWeapon.BindMenuToItem(mTank, giTank)
                End If
            Else
                If Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Then
                    giArchCover = New UIMenuItem(LocalizedModTypeName(VehicleMod.ArchCover), Game.GetGXTEntry("collision_835p5rm")) 'Ram Weapons
                    gmWeapon.AddItem(giArchCover)
                    gmWeapon.BindMenuToItem(mArchCover, giArchCover)
                End If
                If Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Then
                    iRFender = New UIMenuItem(LocalizedModTypeName(VehicleMod.RightFender), Game.GetGXTEntry("CMOD_PROMI_D")) 'Proximity Mine
                    gmWeapon.AddItem(iRFender)
                    gmWeapon.BindMenuToItem(mRFender, iRFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                    giTank = New UIMenuItem(LocalizedModTypeName(VehicleMod.Tank), Game.GetGXTEntry("collision_255bdwf")) 'Primary Weapons
                    gmWeapon.AddItem(giTank)
                    gmWeapon.BindMenuToItem(mTank, giTank)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Then
                    giRoof = New UIMenuItem(LocalizedModTypeName(VehicleMod.Roof), Game.GetGXTEntry("CMOD_SEWEAP_D")) 'Secondary Weapons
                    gmWeapon.AddItem(giRoof)
                    gmWeapon.BindMenuToItem(mRoof, giRoof)
                End If
            End If

            gmWeapon.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateEngineMenu()
        Try
            gmEngine = New UIMenu("", Game.GetGXTEntry("CMM_MOD_GT3"), True) 'ENGINE
            gmEngine.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmEngine.MouseEdgeEnabled = False
            gmEngine.AddInstructionalButton(BtnZoom)
            gmEngine.AddInstructionalButton(BtnZoomOut)
            gmEngine.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(gmEngine)
            gmEngine.AddItem(New UIMenuItem("Nothing"))
            gmEngine.RefreshIndex()
            AddHandler gmEngine.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler gmEngine.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshEngineMenu()
        Try
            gmEngine.MenuItems.Clear()

            If Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                    giBEngineBlock = New UIMenuItem(LocalizedModTypeName(VehicleMod.Frame), Game.GetGXTEntry("SMOD_ENGINE_1"))
                    gmEngine.AddItem(giBEngineBlock)
                    gmEngine.BindMenuToItem(mBEngineBlock, giBEngineBlock)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Then
                    iEngine = New UIMenuItem(LocalizedModTypeName(VehicleMod.Engine), Game.GetGXTEntry("SMOD_ENGINE_4"))
                    gmEngine.AddItem(iEngine)
                    gmEngine.BindMenuToItem(mEngine, iEngine)
                End If
                If Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0 Then
                    giBAirFilter = New UIMenuItem(LocalizedModTypeName(VehicleMod.SideSkirt), Game.GetGXTEntry("CMOD_SMOD_2_D"))
                    gmEngine.AddItem(giBAirFilter)
                    gmEngine.BindMenuToItem(mBAirFilter, giBAirFilter)
                End If
                If Bennys.veh.CanInstallNitroMod Then
                    iNitro = New UIMenuItem(Game.GetGXTEntry("CMM_MOD_NJBOS"), Game.GetGXTEntry("SMOD_ENGINE_2"))
                    gmEngine.AddItem(iNitro)
                    gmEngine.BindMenuToItem(mNitro, iNitro)
                End If
            Else
                If Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Then
                    iEngine = New UIMenuItem(LocalizedModTypeName(VehicleMod.Engine), Game.GetGXTEntry("SMOD_ENGINE_4"))
                    gmEngine.AddItem(iEngine)
                    gmEngine.BindMenuToItem(mEngine, iEngine)
                End If
                If Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Then
                    iEngineBlock = New UIMenuItem(LocalizedModTypeName(VehicleMod.EngineBlock), Game.GetGXTEntry("SMOD_ENGINE_1"))
                    gmEngine.AddItem(iEngineBlock)
                    gmEngine.BindMenuToItem(mEngineBlock, iEngineBlock)
                End If
                If Bennys.veh.CanInstallNitroMod Then
                    iNitro = New UIMenuItem(Game.GetGXTEntry("CMM_MOD_NJBOS"), Game.GetGXTEntry("SMOD_ENGINE_2"))
                    gmEngine.AddItem(iNitro)
                    gmEngine.BindMenuToItem(mNitro, iNitro)
                End If
                If Not arenavehicle.Contains(Bennys.veh.Model) Then
                    If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                        giAirfilter = New UIMenuItem(LocalizedModTypeName(VehicleMod.AirFilter), Game.GetGXTEntry("SMOD_ENGINE_2"))
                        gmEngine.AddItem(giAirfilter)
                        gmEngine.BindMenuToItem(mAirFilter, giAirfilter)
                    End If
                    If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                        giStruts = New UIMenuItem(LocalizedModTypeName(VehicleMod.Struts), Game.GetGXTEntry("SMOD_ENGINE_3b"))
                        gmEngine.AddItem(giStruts)
                        gmEngine.BindMenuToItem(mStruts, giStruts)
                    End If
                End If
            End If

            gmEngine.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateInteriorMenu()
        Try
            gmInterior = New UIMenu("", Game.GetGXTEntry("CMM_MOD_GT1"), True) 'INTERIOR
            gmInterior.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmInterior.MouseEdgeEnabled = False
            gmInterior.AddInstructionalButton(BtnZoom)
            gmInterior.AddInstructionalButton(BtnZoomOut)
            gmInterior.AddInstructionalButton(BtnFirstPerson)
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
                iColumnShifterLevers = New UIMenuItem(LocalizedModTypeName(VehicleMod.ColumnShifterLevers), Game.GetGXTEntry("SMOD_IN_KNOB"))
                gmInterior.AddItem(iColumnShifterLevers)
                gmInterior.BindMenuToItem(mColumnShifterLevers, iColumnShifterLevers)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Then
                iDashboard = New UIMenuItem(LocalizedModTypeName(VehicleMod.Dashboard), Game.GetGXTEntry("SMOD_IN_2"))
                gmInterior.AddItem(iDashboard)
                gmInterior.BindMenuToItem(mDashboard, iDashboard)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Then
                iDialDesign = New UIMenuItem(LocalizedModTypeName(VehicleMod.DialDesign), Game.GetGXTEntry("SMOD_IN_4"))
                gmInterior.AddItem(iDialDesign)
                gmInterior.BindMenuToItem(mDialDesign, iDialDesign)
            End If
            If Not arenavehicle.Contains(Bennys.veh.Model) Then
                If Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Then
                    iOrnaments = New UIMenuItem(LocalizedModTypeName(VehicleMod.Ornaments), Game.GetGXTEntry("CMOD_MOD_64_D"))
                    gmInterior.AddItem(iOrnaments)
                    gmInterior.BindMenuToItem(mOrnaments, iOrnaments)
                End If
            End If
            If Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Then
                iSeats = New UIMenuItem(LocalizedModTypeName(VehicleMod.Seats), Game.GetGXTEntry("SMOD_IN_SEAT"))
                gmInterior.AddItem(iSeats)
                gmInterior.BindMenuToItem(mSeats, iSeats)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Then
                iSteeringWheels = New UIMenuItem(LocalizedModTypeName(VehicleMod.SteeringWheels), Game.GetGXTEntry("SMOD_IN_STEER"))
                gmInterior.AddItem(iSteeringWheels)
                gmInterior.BindMenuToItem(mSteeringWheels, iSteeringWheels)
            End If
            If Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Then
                iTrimDesign = New UIMenuItem(LocalizedModTypeName(VehicleMod.TrimDesign), Game.GetGXTEntry("SMOD_IN_3"))
                gmInterior.AddItem(iTrimDesign)
                gmInterior.BindMenuToItem(mTrimDesign, iTrimDesign)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 Then
                giDoor = New UIMenuItem(LocalizedModTypeName(VehicleMod.DoorSpeakers), Game.GetGXTEntry("SMOD_IN_5b"))
                gmInterior.AddItem(giDoor)
                gmInterior.BindMenuToItem(mDoor, giDoor)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0 Then
                iSpeaker = New UIMenuItem(LocalizedModTypeName(VehicleMod.Speakers), Game.GetGXTEntry("CMOD_MOD_23_D"))
                gmInterior.AddItem(iSpeaker)
                gmInterior.BindMenuToItem(mSpeakers, iSpeaker)
            End If
            If bennysvehicle.Contains(Bennys.veh.Model) Then
                iDashboardColor = New UIMenuItem(LocalizedModGroupName(GroupName.LightColor), Game.GetGXTEntry("SMOD_LIGHT_COLb"))
                gmInterior.AddItem(iDashboardColor)
                gmInterior.BindMenuToItem(mLightsColor, iDashboardColor)
                iTrimColor = New UIMenuItem(LocalizedModGroupName(GroupName.TrimColor), Game.GetGXTEntry("CMOD_MOD_6_D")) 'trim color
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
            gmBumper = New UIMenu("", Game.GetGXTEntry("CMOD_BUM_T"), True) 'BUMPERS
            gmBumper.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBumper.MouseEdgeEnabled = False
            gmBumper.AddInstructionalButton(BtnZoom)
            gmBumper.AddInstructionalButton(BtnZoomOut)
            gmBumper.AddInstructionalButton(BtnFirstPerson)
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
                giFBumper = New UIMenuItem(LocalizedModTypeName(VehicleMod.FrontBumper), Game.GetGXTEntry("CMOD_MOD_71_D"))
                gmBumper.AddItem(giFBumper)
                gmBumper.BindMenuToItem(mFBumper, giFBumper)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0 Then
                giSSkirt = New UIMenuItem(LocalizedModTypeName(VehicleMod.SideSkirt), Game.GetGXTEntry("CMOD_MOD_21_D"))
                gmBumper.AddItem(giSSkirt)
                gmBumper.BindMenuToItem(mSSkirt, giSSkirt)
            End If
            If Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Then
                giRBumper = New UIMenuItem(LocalizedModTypeName(VehicleMod.RearBumper), Game.GetGXTEntry("CMOD_MOD_71_D"))
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
            gmWheels = New UIMenu("", Game.GetGXTEntry("CMOD_WHE0_T"), True) 'WHEELS
            gmWheels.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmWheels.MouseEdgeEnabled = False
            gmWheels.AddInstructionalButton(BtnZoom)
            gmWheels.AddInstructionalButton(BtnZoomOut)
            gmWheels.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(gmWheels)
            gmWheels.AddItem(New UIMenuItem("Nothing"))
            gmWheels.RefreshIndex()
            AddHandler gmWheels.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler gmWheels.OnItemSelect, AddressOf WheelsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelsMenu()
        Try
            gmWheels.MenuItems.Clear()
            giWheelType = New UIMenuItem(LocalizedModGroupName(GroupName.WheelType), Game.GetGXTEntry("CMOD_MOD_28_D")) 'Wheel Type
            gmWheels.AddItem(giWheelType)
            gmWheels.BindMenuToItem(gmWheelType, giWheelType)
            iRimColor = New UIMenuItem(LocalizedModGroupName(GroupName.WheelColor), Game.GetGXTEntry("CMOD_MOD_59_D"))
            gmWheels.AddItem(iRimColor)
            gmWheels.BindMenuToItem(mRimColor, iRimColor)
            giTires = New UIMenuItem(LocalizedModGroupName(GroupName.Tires), Game.GetGXTEntry("CMOD_IE_25_D"))
            gmWheels.AddItem(giTires)
            gmWheels.BindMenuToItem(mTires, giTires)
            iTireSmoke = New UIMenuItem(LocalizedModTypeName(VehicleToggleMod.TireSmoke), Game.GetGXTEntry("CMOD_IE_25_D"))
            gmWheels.AddItem(iTireSmoke)
            gmWheels.BindMenuToItem(mTireSmoke, iTireSmoke)
            iBPTires = New UIMenuItem(Game.GetGXTEntry("CMOD_GLD2_1"))
            With iBPTires
                If Not Bennys.veh.CanTiresBurst Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 4000
                    Dim price As String = $"${value}"
                    .SetRightLabel(price)
                    .SubInteger2 = 4000
                End If
            End With
            gmWheels.AddItem(iBPTires)
            gmWheels.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelTypeMenu()
        Try
            gmWheelType = New UIMenu("", Game.GetGXTEntry("CMOD_WHE1_T"), True) 'WHEEL TYPE
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
                    giBikeWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels))
                    gmWheelType.AddItem(giBikeWheels)
                    gmWheelType.BindMenuToItem(gmBikeWheels, giBikeWheels)
                Case Else
                    giHighEndWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.HighEnd))
                    gmWheelType.AddItem(giHighEndWheels)
                    gmWheelType.BindMenuToItem(gmHighEnd, giHighEndWheels)
                    giLowriderWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.Lowrider))
                    gmWheelType.AddItem(giLowriderWheels)
                    gmWheelType.BindMenuToItem(gmLowrider, giLowriderWheels)
                    giMuscleWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.Muscle))
                    gmWheelType.AddItem(giMuscleWheels)
                    gmWheelType.BindMenuToItem(gmMuscle, giMuscleWheels)
                    giOffroadWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.Offroad))
                    gmWheelType.AddItem(giOffroadWheels)
                    gmWheelType.BindMenuToItem(gmOffroad, giOffroadWheels)
                    giSportWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.Sport))
                    gmWheelType.AddItem(giSportWheels)
                    gmWheelType.BindMenuToItem(gmSport, giSportWheels)
                    giSUVWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.SUV))
                    gmWheelType.AddItem(giSUVWheels)
                    gmWheelType.BindMenuToItem(gmSUV, giSUVWheels)
                    giTunerWheels = New UIMenuItem(GetLocalizedWheelTypeName(VehicleWheelType.Tuner))
                    gmWheelType.AddItem(giTunerWheels)
                    gmWheelType.BindMenuToItem(gmTuner, giTunerWheels)
                    giBennysWheels = New UIMenuItem(GetLocalizedWheelTypeName(8)) 'Benny's Original
                    gmWheelType.AddItem(giBennysWheels)
                    gmWheelType.BindMenuToItem(mBennysOriginals, giBennysWheels)
                    giBespokeWheels = New UIMenuItem(GetLocalizedWheelTypeName(9)) 'Benny's Bespoke
                    gmWheelType.AddItem(giBespokeWheels)
                    gmWheelType.BindMenuToItem(mBespoke, giBespokeWheels)
            End Select

            gmWheelType.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelRimMenu(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title, True)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            menu.AddInstructionalButton(BtnZoom)
            menu.AddInstructionalButton(BtnZoomOut)
            menu.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelRimMenu(ByRef menu As UIMenu, ByRef bindStock As UIMenu, ByRef bindChrome As UIMenu, ByRef itemStock As UIMenuItem, ByRef itemChrome As UIMenuItem)
        Try
            menu.MenuItems.Clear()
            itemStock = New UIMenuItem(Game.GetGXTEntry("CMOD_WHE4_0")) 'Stock Rims
            menu.AddItem(itemStock)
            menu.BindMenuToItem(bindStock, itemStock)
            itemChrome = New UIMenuItem(Game.GetGXTEntry("CMOD_WHE4_1")) 'Stock Rims
            menu.AddItem(itemChrome)
            menu.BindMenuToItem(bindChrome, itemChrome)
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshStockWheelsModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod)
        Try
            menu.MenuItems.Clear()
            Dim count As Integer = Bennys.veh.GetModCount(vehmod)
            Dim half As Integer = count / 2

            For i As Integer = -1 To half - 1
                item = New UIMenuItem(GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then
                        item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        If Not i = -1 Then
                            Dim ii = i + 1
                            Dim value As Integer = 200 * ii
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 200 * ii
                        End If
                    End If
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshChromeWheelsModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod)
        Try
            menu.MenuItems.Clear()
            Dim count As Integer = Bennys.veh.GetModCount(vehmod)
            Dim half As Integer = count / 2

            item = New UIMenuItem(GetLocalizedModName(-1, Bennys.veh.GetModCount(vehmod), vehmod))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = -1
                If Bennys.veh.GetMod(vehmod) = -1 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 200
                    Dim price As String = "$" & value
                    item.SetRightLabel(price)
                    .SubInteger2 = 200
                End If
            End With
            menu.AddItem(item)

            For i As Integer = half To Bennys.veh.GetModCount(vehmod) - 1
                item = New UIMenuItem(GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then
                        item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        If Not i = -1 Then
                            Dim ii = i + 1
                            Dim value As Integer = 200 * ii
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 200 * ii
                        End If
                    End If
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshLowriderDLCWheelsModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod)
        Try
            menu.MenuItems.Clear()
            Dim count As Integer = Bennys.veh.GetModCount(vehmod)
            Dim oneOver6 As Integer = count / 7

            For i As Integer = -1 To oneOver6 - 1
                item = New UIMenuItem(GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then
                        item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        If Not i = -1 Then
                            Dim ii = i + 1
                            Dim value As Integer = 200 * ii
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 200 * ii
                        End If
                    End If
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBikeWheelsModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod, ByRef chromeWheels As Boolean)
        Try
            menu.MenuItems.Clear()
            Dim count As Integer = Bennys.veh.GetModCount(vehmod)
            Dim standard As List(Of Integer) = New List(Of Integer) From {-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48}
            Dim chrome As List(Of Integer) = New List(Of Integer) From {-1, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71}

            If chromeWheels = False Then 'Standard
                For Each i As Integer In standard
                    item = New UIMenuItem(GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                    With item
                        If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                        .SubInteger1 = i
                        If Bennys.veh.GetMod(vehmod) = i Then
                            item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        Else
                            If Not i = -1 Then
                                Dim ii = i + 1
                                Dim value As Integer = 200 * ii
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 200 * ii
                            End If
                        End If
                    End With
                    menu.AddItem(item)
                Next
            ElseIf chromeWheels = True Then 'Chrome
                For Each i As Integer In chrome
                    item = New UIMenuItem(GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                    With item
                        If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                        .SubInteger1 = i
                        If Bennys.veh.GetMod(vehmod) = i Then
                            item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        Else
                            If Not i = -1 Then
                                Dim ii = i + 1
                                Dim value As Integer = 200 * ii
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 200 * ii
                            End If
                        End If
                    End With
                    menu.AddItem(item)
                Next
            End If

            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateTyresMenu()
        Try
            mTires = New UIMenu("", Game.GetGXTEntry("CMOD_TYR_T"), True) 'TIRES
            mTires.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mTires.MouseEdgeEnabled = False
            mTires.AddInstructionalButton(BtnZoom)
            mTires.AddInstructionalButton(BtnZoomOut)
            mTires.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(mTires)
            mTires.AddItem(New UIMenuItem("Nothing"))
            mTires.RefreshIndex()
            AddHandler mTires.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mTires.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mTires.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshTyresMenu()
        Try
            mTires.MenuItems.Clear()

            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = -1 Then
                iTires = New UIMenuItem(Game.GetGXTEntry("CMOD_TYR_0"))
                With iTires
                    .SubInteger1 = 1
                    If Not IsCustomWheels() Then
                        .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        Dim value As Integer = 100
                        Dim price As String = "$" & value
                        .SetRightLabel(price)
                        .SubInteger2 = 100
                    End If
                End With
                mTires.AddItem(iTires)
            Else
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                        Dim whe As Integer = GetBennysOriginalRim(Bennys.veh.GetMod(VehicleMod.FrontWheels))
                        Dim count As Integer = Bennys.veh.GetModCount(VehicleMod.FrontWheels)
                        Dim oneOver6 As Integer = count / 7
                        Dim thirtyOne As Integer = oneOver6 '31
                        iTires = New UIMenuItem(Game.GetGXTEntry("CMOD_TYR_0"))
                        With iTires
                            Dim newid As Integer = whe
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 100
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 100
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("collision_v925jg")) 'White Lines
                        With iTires
                            Dim newid As Integer = (whe + thirtyOne) ' + 1
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 200
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 200
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("collision_v925jh")) 'Classic White Wall
                        With iTires
                            Dim newid As Integer = (whe + thirtyOne * 2) ' + 2
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 300
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 300
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("collision_v925ji")) 'Retro White Wall
                        With iTires
                            Dim newid As Integer = (whe + thirtyOne * 3) ' + 3
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 400
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 400
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("collision_v925jj")) 'Red Line
                        With iTires
                            Dim newid As Integer = (whe + thirtyOne * 4) ' + 4
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim price As String = "$" & 500
                                .SetRightLabel(price)
                                .SubInteger2 = 500
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("collision_v925jk")) 'Blue Line
                        With iTires
                            Dim newid As Integer = (whe + thirtyOne * 5) ' + 5
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 600
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 600
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("CMOD_TYR_1"))
                        With iTires
                            Dim newid As Integer = (whe + thirtyOne * 6) ' + 6
                            .SubInteger1 = newid
                            If Bennys.veh.GetMod(VehicleMod.FrontWheels) = newid Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 700
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 700
                            End If
                        End With
                        mTires.AddItem(iTires)
                    Case Else
                        iTires = New UIMenuItem(Game.GetGXTEntry("CMOD_TYR_0"))
                        With iTires
                            .SubInteger1 = 1
                            If Not IsCustomWheels() Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 100
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 100
                            End If
                        End With
                        mTires.AddItem(iTires)
                        iTires = New UIMenuItem(Game.GetGXTEntry("CMOD_TYR_1"))
                        With iTires
                            .SubInteger1 = 7
                            If IsCustomWheels() Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 700
                                Dim price As String = "$" & value
                                .SetRightLabel(price)
                                .SubInteger2 = 700
                            End If
                        End With
                        mTires.AddItem(iTires)
                End Select
            End If
            mTires.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateMenu()
        Try
            gmPlate = New UIMenu("", Game.GetGXTEntry("CMM_MOD_GT2"), True) 'PLATES
            gmPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmPlate.MouseEdgeEnabled = False
            gmPlate.AddInstructionalButton(BtnZoom)
            gmPlate.AddInstructionalButton(BtnZoomOut)
            gmPlate.AddInstructionalButton(BtnFirstPerson)
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
                giPlateHolder = New UIMenuItem(LocalizedModTypeName(VehicleMod.PlateHolder), Game.GetGXTEntry("CMOD_MOD_49_D"))
                gmPlate.AddItem(giPlateHolder)
                gmPlate.BindMenuToItem(mPlateHolder, giPlateHolder)
            End If
            If Not arenavehicle.Contains(Bennys.veh.Model) Then
                If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                    giVanityPlate = New UIMenuItem(LocalizedModTypeName(VehicleMod.VanityPlates), Game.GetGXTEntry("CMOD_SMOD_4_D"))
                    gmPlate.AddItem(giVanityPlate)
                    gmPlate.BindMenuToItem(mVanityPlates, giVanityPlate)
                End If
            End If
            giNumberPlate = New UIMenuItem(LocalizedModGroupName(GroupName.License), Game.GetGXTEntry("CMOD_MOD_18_D")) 'Number Plate
            gmPlate.AddItem(giNumberPlate)
            gmPlate.BindMenuToItem(mNumberPlate, giNumberPlate)
            gmPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateNumberMenu()
        Try
            mNumberPlate = New UIMenu("", Game.GetGXTEntry("CMOD_MOD_PLA2").ToUpper, True) 'LICENSE PLATE
            mNumberPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNumberPlate.MouseEdgeEnabled = False
            mNumberPlate.AddInstructionalButton(BtnZoom)
            mNumberPlate.AddInstructionalButton(BtnZoomOut)
            mNumberPlate.AddInstructionalButton(BtnFirstPerson)
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
            mTint = New UIMenu("", Game.GetGXTEntry("CMOD_WIN_T"), True) 'TINTS
            mTint.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mTint.MouseEdgeEnabled = False
            mTint.AddInstructionalButton(BtnZoom)
            mTint.AddInstructionalButton(BtnZoomOut)
            mTint.AddInstructionalButton(BtnFirstPerson)
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
            menu = New UIMenu("", title, True)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            menu.AddInstructionalButton(BtnZoom)
            menu.AddInstructionalButton(BtnZoomOut)
            menu.AddInstructionalButton(BtnFirstPerson)
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

    Public Shared Sub RefreshEnumModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef enumType As EnumTypes)
        Try
            menu.MenuItems.Clear()

            Dim enumArray As Array = Nothing
            Select Case enumType
                Case EnumTypes.NumberPlateType
                    enumArray = System.Enum.GetValues(GetType(NumberPlateType))
                    For Each enumItem As NumberPlateType In enumArray
                        item = New UIMenuItem(LocalizedLicensePlate(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.NumberPlateType = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 200
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 200
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleWindowTint
                    enumArray = System.Enum.GetValues(GetType(VehicleWindowTint))
                    For Each enumItem As VehicleWindowTint In enumArray
                        item = New UIMenuItem(LocalizedWindowsTint(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.WindowTint = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorPrimary
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.PrimaryColor = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorSecondary
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.SecondaryColor = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.vehicleColorPearlescent
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.PearlescentColor = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorTrim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.TrimColor = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorDashboard
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.DashboardColor = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
                        End With
                        menu.AddItem(item)
                    Next
                Case EnumTypes.VehicleColorRim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem(GetLocalizedColorName(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.RimColor = enumItem Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 2000
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger2 = 2000
                            End If
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
            gmLights = New UIMenu("", Game.GetGXTEntry("CMOD_LGT_T"), True) 'LIGHTS
            gmLights.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmLights.MouseEdgeEnabled = False
            gmLights.AddInstructionalButton(BtnZoom)
            gmLights.AddInstructionalButton(BtnZoomOut)
            gmLights.AddInstructionalButton(BtnFirstPerson)
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
            iHeadlights = New UIMenuItem(LocalizedModGroupName(GroupName.Headlights), Game.GetGXTEntry("CMOD_MOD_47_D"))
            gmLights.AddItem(iHeadlights)
            gmLights.BindMenuToItem(mHeadlights, iHeadlights)
            If Bennys.veh.HasBone("neon_b") Then
                giNeonKits = New UIMenuItem(LocalizedModGroupName(GroupName.NeonKits), Game.GetGXTEntry("CMOD_MOD_6_D"))
                gmLights.AddItem(giNeonKits)
                gmLights.BindMenuToItem(gmNeonKits, giNeonKits)
            End If
            gmLights.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNitroMenu()
        Try
            mNitro = New UIMenu("", Game.GetGXTEntry("CMM_MOD_TBOS"), True) 'BOOST
            mNitro.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNitro.MouseEdgeEnabled = False
            mNitro.AddInstructionalButton(BtnZoom)
            mNitro.AddInstructionalButton(BtnZoomOut)
            mNitro.AddInstructionalButton(BtnFirstPerson)
            _menuPool.Add(mNitro)
            mNitro.AddItem(New UIMenuItem("Nothing"))
            mNitro.RefreshIndex()
            AddHandler mNitro.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mNitro.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mNitro.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshNitroMenu()
        Try
            mNitro.MenuItems.Clear()

            iNitro = New UIMenuItem(Game.GetGXTEntry("CMOD_ARM_0"))
            With iNitro
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = CInt(False)
                If Not Bennys.veh.GetBool(nitroMod) Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNitro.AddItem(iNitro)
            iNitro = New UIMenuItem(Game.GetGXTEntry("collision_57fffph")) 'Upgrade 100%
            With iNitro
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_57fffph")
                .SubInteger1 = CInt(True)
                If Bennys.veh.GetBool(nitroMod) Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 30000
                    Dim price As String = $"${value}"
                    .SetRightLabel(price)
                    .SubInteger2 = 30000
                End If
            End With
            mNitro.AddItem(iNitro)

            mNitro.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNeonKitsMenu()
        Try
            gmNeonKits = New UIMenu("", Game.GetGXTEntry("CMOD_MOD_LGT_N").ToUpper, True) 'NEON KITS
            gmNeonKits.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmNeonKits.MouseEdgeEnabled = False
            gmNeonKits.AddInstructionalButton(BtnZoom)
            gmNeonKits.AddInstructionalButton(BtnZoomOut)
            gmNeonKits.AddInstructionalButton(BtnFirstPerson)
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
            iNeon = New UIMenuItem(LocalizedModGroupName(GroupName.NeonLayout))
            gmNeonKits.AddItem(iNeon)
            gmNeonKits.BindMenuToItem(mNeon, iNeon)
            If Not Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                iNeonColor = New UIMenuItem(LocalizedModGroupName(GroupName.NeonColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
                gmNeonKits.AddItem(iNeonColor)
                gmNeonKits.BindMenuToItem(mNeonColor, iNeonColor)
            End If
            gmNeonKits.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNeonMenu()
        Try
            mNeon = New UIMenu("", Game.GetGXTEntry("CMOD_NEON_0").ToUpper, True) 'NEON LAYOUT
            mNeon.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNeon.MouseEdgeEnabled = False
            mNeon.AddInstructionalButton(BtnZoom)
            mNeon.AddInstructionalButton(BtnZoomOut)
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

            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_0"))
            With iNeon
                .SubInteger1 = NeonLayouts.None
                If NeonLayout() = NeonLayouts.None Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_1"))
            With iNeon
                .SubInteger1 = NeonLayouts.Front
                If NeonLayout() = NeonLayouts.Front Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 1000
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 1000
                End If
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_2"))
            With iNeon
                .SubInteger1 = NeonLayouts.Back
                If NeonLayout() = NeonLayouts.Back Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 1000
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 1000
                End If
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_3"))
            With iNeon
                .SubInteger1 = NeonLayouts.Sides
                If NeonLayout() = NeonLayouts.Sides Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 1250
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 1250
                End If
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_4"))
            With iNeon
                .SubInteger1 = NeonLayouts.FrontAndBack
                If NeonLayout() = NeonLayouts.FrontAndBack Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 1800
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 1800
                End If
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_5"))
            With iNeon
                .SubInteger1 = NeonLayouts.FrontAndSides
                If NeonLayout() = NeonLayouts.FrontAndSides Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 2000
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 2000
                End If
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_6"))
            With iNeon
                .SubInteger1 = NeonLayouts.BackAndSides
                If NeonLayout() = NeonLayouts.BackAndSides Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 2000
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 2000
                End If
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem(Game.GetGXTEntry("CMOD_NEONLAY_7"))
            With iNeon
                .SubInteger1 = NeonLayouts.FrontBackAndSides
                If NeonLayout() = NeonLayouts.FrontBackAndSides Then
                    .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim value As Integer = 3000
                    Dim price As String = "$" & value
                    .SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            mNeon.AddItem(iNeon)

            mNeon.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateResprayMenu()
        Try
            gmRespray = New UIMenu("", Game.GetGXTEntry("CMOD_COL0_T"), True) 'RESPRAY
            gmRespray.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmRespray.MouseEdgeEnabled = False
            gmRespray.AddInstructionalButton(BtnZoom)
            gmRespray.AddInstructionalButton(BtnZoomOut)
            gmRespray.AddInstructionalButton(BtnFirstPerson)
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
            giPrimaryCol = New UIMenuItem(LocalizedModGroupName(GroupName.PrimaryColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
            gmRespray.AddItem(giPrimaryCol)
            gmRespray.BindMenuToItem(mPrimaryColor, giPrimaryCol)
            giSecondaryCol = New UIMenuItem(LocalizedModGroupName(GroupName.SecondaryColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
            gmRespray.AddItem(giSecondaryCol)
            gmRespray.BindMenuToItem(mSecondaryColor, giSecondaryCol)
            If Not bennysvehicle.Contains(Bennys.veh.Model) Then
                iDashboardColor = New UIMenuItem(LocalizedModGroupName(GroupName.AccentColor), Game.GetGXTEntry("CMOD_MOD_6_D"))
                gmRespray.AddItem(iDashboardColor)
                gmRespray.BindMenuToItem(mLightsColor, iDashboardColor)
                iTrimColor = New UIMenuItem(LocalizedModGroupName(GroupName.TrimColor), Game.GetGXTEntry("CMOD_MOD_6_D")) 'trim color
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
            iPrimaryChromeColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Chrome), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryChromeColor)
            mPrimaryColor.BindMenuToItem(mPrimaryChromeColor, iPrimaryChromeColor)
            iPrimaryClassicColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Classic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryClassicColor)
            mPrimaryColor.BindMenuToItem(mPrimaryClassicColor, iPrimaryClassicColor)
            iPrimaryMatteColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Matte), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryMatteColor)
            mPrimaryColor.BindMenuToItem(mPrimaryMatteColor, iPrimaryMatteColor)
            iPrimaryMetallicColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Metallic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryMetallicColor)
            mPrimaryColor.BindMenuToItem(mPrimaryMetallicColor, iPrimaryMetallicColor)
            iPrimaryMetalsColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Metals), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mPrimaryColor.AddItem(iPrimaryMetalsColor)
            mPrimaryColor.BindMenuToItem(mPrimaryMetalsColor, iPrimaryMetalsColor)
            iPrimaryPearlescentColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Pearlescent), Game.GetGXTEntry("CMOD_MOD_6_D"))
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
            iSecondaryChromeColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Chrome), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryChromeColor)
            mSecondaryColor.BindMenuToItem(mSecondaryChromeColor, iSecondaryChromeColor)
            iSecondaryClassicColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Classic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryClassicColor)
            mSecondaryColor.BindMenuToItem(mSecondaryClassicColor, iSecondaryClassicColor)
            iSecondaryMatteColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Matte), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryMatteColor)
            mSecondaryColor.BindMenuToItem(mSecondaryMatteColor, iSecondaryMatteColor)
            iSecondaryMetallicColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Metallic), Game.GetGXTEntry("CMOD_MOD_6_D"))
            mSecondaryColor.AddItem(iSecondaryMetallicColor)
            mSecondaryColor.BindMenuToItem(mSecondaryMetallicColor, iSecondaryMetallicColor)
            iSecondaryMetalsColor = New UIMenuItem(LocalizedColorGroupName(ColorType.Metals), Game.GetGXTEntry("CMOD_MOD_6_D"))
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
                item = New UIMenuItem(GetLocalizedColorName(col))
                With item
                    .SubInteger1 = col
                    If prisecpear = "Primary" Then
                        If Bennys.veh.PrimaryColor = col Then
                            .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        Else
                            Dim value As Integer = 2000
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 2000
                        End If
                    ElseIf prisecpear = "Secondary" Then
                        If Bennys.veh.SecondaryColor = col Then
                            .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        Else
                            Dim value As Integer = 2000
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 2000
                        End If
                    ElseIf prisecpear = "Pearlescent" Then
                        If Bennys.veh.PearlescentColor = col Then
                            .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        Else
                            Dim value As Integer = 2000
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 2000
                        End If
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
            For Each col As Reflection.PropertyInfo In GetType(Drawing.Color).GetProperties()
                If Not removeList.Contains(col.Name) Then
                    item = New UIMenuItem(Trim(RegularExpressions.Regex.Replace(col.Name, "[A-Z]", " ${0}")))
                    With item
                        .SubInteger1 = Drawing.Color.FromName(col.Name).R
                        .SubInteger2 = Drawing.Color.FromName(col.Name).G
                        .SubInteger3 = Drawing.Color.FromName(col.Name).B
                        If neonsmoke = "Neon" Then
                            If Bennys.veh.NeonLightsColor = Drawing.Color.FromName(col.Name) Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 200
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger4 = 200
                            End If
                        ElseIf neonsmoke = "Smoke" Then
                            If Bennys.veh.TireSmokeColor = Drawing.Color.FromName(col.Name) Then
                                .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Else
                                Dim value As Integer = 200
                                Dim price As String = "$" & value
                                item.SetRightLabel(price)
                                .SubInteger4 = 200
                            End If
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
            menu = New UIMenu("", title, True)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            menu.AddInstructionalButton(BtnZoom)
            menu.AddInstructionalButton(BtnZoomOut)
            menu.AddInstructionalButton(BtnFirstPerson)
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
                    If Bennys.veh.GetMod(vehmod) = i Then
                        item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        If Not i = -1 Then
                            Dim ii = i + 1
                            Dim value As Integer = 2000 * ii
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 2000 * ii
                        End If
                    End If
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
            menu = New UIMenu("", title, True)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            menu.AddInstructionalButton(BtnZoom)
            menu.AddInstructionalButton(BtnZoomOut)
            menu.AddInstructionalButton(BtnFirstPerson)
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

    Public Shared Sub RefreshModMenuForLivery2(ByRef menu As UIMenu, ByRef item As UIMenuItem)
        Try
            menu.MenuItems.Clear()
            For i As Integer = 0 To Bennys.veh.Livery2Count - 1
                item = New UIMenuItem(LocalizedT5RoofName(i))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetLivery2 = i Then
                        item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        If Not i = -1 Then
                            Dim ii = i + 1
                            Dim value As Integer = 200 * ii
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 200 * ii
                        End If
                    End If
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod)
        Try
            menu.MenuItems.Clear()
            For i As Integer = -1 To Bennys.veh.GetModCount(vehmod) - 1
                item = New UIMenuItem(GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then
                        item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Else
                        If Not i = -1 Then
                            Dim ii = i + 1
                            Dim value As Integer = 200 * ii
                            Dim price As String = "$" & value
                            item.SetRightLabel(price)
                            .SubInteger2 = 200 * ii
                        End If
                    End If
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

            item = New UIMenuItem(LocalizedModTypeName(vehmod, True))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 0
                If Not Bennys.veh.IsToggleModOn(vehmod) Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            menu.AddItem(item)
            item = New UIMenuItem(LocalizedModTypeName(vehmod))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 1
                If Bennys.veh.IsToggleModOn(vehmod) Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 1000
                    item.SetRightLabel(price)
                    .SubInteger2 = 1000
                End If
            End With
            menu.AddItem(item)

            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshModMenuForHeadlightsColor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleToggleMod)
        Try
            menu.MenuItems.Clear()

            item = New UIMenuItem(Game.GetGXTEntry("CMOD_LGT_0"))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_LGT_0")
                .SubInteger1 = 0
                .SubInteger3 = 255
                If Not Bennys.veh.IsToggleModOn(vehmod) Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            menu.AddItem(item)
            item = New UIMenuItem(LocalizedModTypeName(vehmod))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 1
                .SubInteger3 = 255
                If Bennys.veh.IsToggleModOn(vehmod) Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 1000
                    item.SetRightLabel(price)
                    .SubInteger2 = 1000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_9vtlzex")) 'White
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_9vtlzex")
                .SubInteger1 = 1
                .SubInteger3 = 0
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_9vtlzey")) 'Blue
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_9vtlzey")
                .SubInteger1 = 1
                .SubInteger3 = 1
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_9vtlzez")) 'Electric Blue
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_9vtlzez")
                .SubInteger1 = 1
                .SubInteger3 = 2
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_8gbxm1p")) 'Mint Green
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_8gbxm1p")
                .SubInteger1 = 1
                .SubInteger3 = 3
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_8gbxm1q")) 'Lime Green
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_8gbxm1q")
                .SubInteger1 = 1
                .SubInteger3 = 4
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_8gbxm1r")) 'Yellow
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_8gbxm1r")
                .SubInteger1 = 1
                .SubInteger3 = 5
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_8gbxm1s")) 'Golden Shower
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_8gbxm1s")
                .SubInteger1 = 1
                .SubInteger3 = 6
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_8gbxm1t")) 'Orange
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_7jh67le")
                .SubInteger1 = 1
                .SubInteger3 = 7
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_7jh67le")) 'Red
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_8gbxm1t")
                .SubInteger1 = 1
                .SubInteger3 = 8
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_7jh67lg")) 'Pony Pink
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_7jh67lg")
                .SubInteger1 = 1
                .SubInteger3 = 9
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_7jh67lf")) 'Hot Pink
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_7jh67lf")
                .SubInteger1 = 1
                .SubInteger3 = 10
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_7jh67lh")) 'Blacklight
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_7jh67lh")
                .SubInteger1 = 1
                .SubInteger3 = 11
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Game.GetGXTEntry("collision_7jh67li")) 'Purple
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("collision_7jh67li")
                .SubInteger1 = 1
                .SubInteger3 = 12
                If Bennys.veh.GetXenonHeadlightsColor = 0 Then
                    item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                Else
                    Dim price As String = "$" & 3000
                    item.SetRightLabel(price)
                    .SubInteger2 = 3000
                End If
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
            If IsNitroModInstalled() Then Bennys.veh.SetBool(nitroMod, Bennys.lastVehMemory.Nitro)

            'Mods
            Bennys.veh.SetMod(VehicleMod.FrontBumper, Bennys.lastVehMemory.FrontBumper, False)
            Bennys.veh.SetMod(VehicleMod.RearBumper, Bennys.lastVehMemory.RearBumper, False)
            Bennys.veh.SetMod(VehicleMod.SideSkirt, Bennys.lastVehMemory.SideSkirt, False)
            Bennys.veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
            Bennys.veh.WheelType = Bennys.lastVehMemory.WheelType
            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.lastVehMemory.FrontWheels, Bennys.lastVehMemory.WheelsVariation)
            Bennys.veh.SetMod(VehicleMod.BackWheels, Bennys.lastVehMemory.BackWheels, Bennys.lastVehMemory.WheelsVariation)
            Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, Bennys.lastVehMemory.Headlights)
            Bennys.veh.SetXenonHeadlightsColor(Bennys.lastVehMemory.HeadlightsColor, Bennys.veh.IsToggleModOn(VehicleToggleMod.XenonHeadlights))
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
            Bennys.veh.SetLivery2(Bennys.lastVehMemory.Livery2)
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
            Bennys.veh.CanTiresBurst = Bennys.lastVehMemory.BulletProofTires

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
            If sender Is gmEngine Then
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, Bennys.veh, False)
            End If
            If sender Is mStruts Then
                Select Case Bennys.veh.Model
                    Case "comet3"
                        Bennys.veh.CloseDoor(VehicleDoor.Trunk, False)
                        camera.MainCameraPosition = CameraPosition.RearBumper
                End Select
            End If
            If sender Is mDoor Then
                Native.Function.Call(Hash.SET_VEHICLE_DOORS_SHUT, Bennys.veh, False)
            End If
            If sender Is mHydraulics Then Bennys.veh.CloseDoor(VehicleDoor.Trunk, False)
            If sender Is mTrunk Then Bennys.veh.CloseDoor(VehicleDoor.Trunk, False)
            If sender Is gmLights Then
                Bennys.veh.LightsOn = True
                Bennys.veh.HighBeamsOn = False
            End If
            If sender Is mHorn Then Bennys.ply.Task.WarpIntoVehicle(Bennys.veh, VehicleSeat.Driver)

            'Reset Camera Position
            If (sender Is gmInterior) Or (sender Is gmEngine) Or (sender Is mFBumper) Or (sender Is mRBumper) Or (sender Is mSSkirt) Or (sender Is mNumberPlate) Or (sender Is mPlateHolder) Or (sender Is mSpoilers) Or
                (sender Is mVanityPlates) Or (sender Is gmWheels) Or (sender Is mExhaust) Or (sender Is mBrakes) Or (sender Is mGrille) Or (sender Is mHood) Or (sender Is mHydraulics) Or (sender Is mPlaques) Or
                (sender Is mTank) Or (sender Is mShifter) Or (sender Is mFMudguard) Or (sender Is mOilTank) Or (sender Is mRMudguard) Or (sender Is mFuelTank) Or (sender Is mBeltDriveCovers) Or (sender Is mBTank) Or
                (sender Is mTrunk) Or (sender Is mArchCover) Or (sender Is mRoof) Then
                camera.MainCameraPosition = CameraPosition.Car
            End If
            If Not sender.ParentMenu Is gmEngine Then
                If (sender Is mStruts) Or (sender Is mAirFilter) Then
                    camera.MainCameraPosition = CameraPosition.Car
                End If
            End If
            If Not sender.ParentMenu Is gmInterior Then
                If sender Is mOrnaments Then
                    camera.MainCameraPosition = CameraPosition.Car
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub WheelsMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        If sender Is gmWheels Then
            RefreshTyresMenu()
            If selectedItem Is iBPTires Then
                If iBPTires.RightBadge = UIMenuItem.BadgeStyle.Car Then
                    Bennys.veh.CanTiresBurst = True
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.None)
                    Bennys.lastVehMemory.BulletProofTires = True
                Else
                    Bennys.veh.CanTiresBurst = False
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.BulletProofTires = False
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                End If
            End If
        End If
    End Sub

    Public Shared Sub ModsMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            For Each i As UIMenuItem In sender.MenuItems
                i.SetRightBadge(UIMenuItem.BadgeStyle.None)
            Next

            'Arena War Upgrade
            If sender Is mUpgradeAW Then
                Game.FadeScreenOut(1000)
                Wait(1000)
                Dim veh As Vehicle = World.CreateVehicle(selectedItem.SubString1, Bennys.veh.Position, Bennys.veh.Heading)
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
                veh.SetLivery2(Bennys.lastVehMemory.Livery2)
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
                veh.SetXenonHeadlightsColor(Bennys.lastVehMemory.HeadlightsColor, veh.IsToggleModOn(VehicleToggleMod.XenonHeadlights))
                veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
                veh.NumberPlate = Bennys.lastVehMemory.PlateNumbers
                veh.CanTiresBurst = Bennys.lastVehMemory.BulletProofTires
                Bennys.veh.Delete()
                Bennys.ply.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                Bennys.veh = veh
                veh.InstallModKit()
                MainMenu.MenuItems.Remove(iUpgradeAW)
                isRepairing = True
                RefreshMenus()
                camera.RepositionFor(veh)
                Wait(1000)
                Game.FadeScreenIn(1000)
                Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger1)
                Native.Function.Call(Hash._START_SCREEN_EFFECT, "MP_corona_switch_supermod", 0, 1)
                Native.Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "Lowrider_Upgrade", "Lowrider_Super_Mod_Garage_Sounds", 1)
                PlaySpeech("LR_UPGRADE_SUPERMOD")
            End If

            'Performance Mods
            If sender Is mSuspension Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Suspension, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Suspension = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_SUSPENSION")
                End If
            ElseIf sender Is mArmor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Armor, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Armor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_ARMOUR")
                End If
            ElseIf sender Is mBrakes Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Brakes, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Brakes = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_BRAKES")
                End If
            ElseIf sender Is mTransmission Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Transmission, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Transmission = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_TRANS_UPGRADE")
                End If
            ElseIf sender Is mEngine Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Engine, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Engine = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_ENGINE_UPGRADE")
                End If
            ElseIf sender Is mNitro Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetBool(nitroMod, CBool(selectedItem.SubInteger1))
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Nitro = CBool(selectedItem.SubInteger1)
                    PlaySpeech("SHOP_SELL_ENGINE_UPGRADE")
                End If
            End If

            'Mods
            If sender Is mFBumper Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.FrontBumper = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mRBumper Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RearBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.RearBumper = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mSSkirt Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SideSkirt, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.SideSkirt = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mNumberPlate Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.NumberPlateType = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.NumberPlate = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mHeadlights Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, CBool(selectedItem.SubInteger1))
                    If selectedItem.Text = Game.GetGXTEntry("CMOD_LGT_0") Then Bennys.veh.SetXenonHeadlightsColor(selectedItem.SubInteger3, False) Else Bennys.veh.SetXenonHeadlightsColor(selectedItem.SubInteger3, True)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Headlights = CBool(selectedItem.SubInteger1)
                    Bennys.lastVehMemory.HeadlightsColor = selectedItem.SubInteger3
                    PlaySpeech("")
                End If
            ElseIf sender Is mArchCover Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.ArchCover, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.ArchCover = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf sender Is mExhaust Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Exhaust, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Exhaust = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_EXHAUST")
                End If
            ElseIf sender Is mFender Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Fender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Fender = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mRFender Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RightFender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.RightFender = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mDoor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.DoorSpeakers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.DoorSpeakers = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mFrame Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Frame, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Frame = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_EXCHASSIS_MOD")
                End If
            ElseIf sender Is mAerials Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Aerials, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Aerials = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mTrim Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Trim, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Trim = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mEngineBlock Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.EngineBlock, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.EngineBlock = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_ENGINE")
                End If
            ElseIf sender Is mAirFilter Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.AirFilter, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.AirFilter = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_ENGINE")
                End If
            ElseIf sender Is mStruts Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Struts, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Struts = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mColumnShifterLevers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.ColumnShifterLevers = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_GEARKNOB")
                End If
            ElseIf sender Is mDashboard Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Dashboard, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Dashboard = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_SUPERMOD_INTERIOR")
                End If
            ElseIf sender Is mDialDesign Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.DialDesign, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.DialDesign = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_SUPERMOD_INTERIOR")
                End If
            ElseIf sender Is mOrnaments Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Ornaments, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Ornaments = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_DOLL")
                End If
            ElseIf sender Is mSeats Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Seats, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Seats = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_SUPERMOD_INTERIOR")
                End If
            ElseIf sender Is mSteeringWheels Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SteeringWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.SteeringWheels = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_SUPERMOD_INTERIOR")
                End If
            ElseIf sender Is mTrimDesign Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.TrimDesign, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.TrimDesign = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_SUPERMOD_INTERIOR")
                End If
            ElseIf sender Is mPlateHolder Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.PlateHolder, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.PlateHolder = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_PLATEHOLDER")
                End If
            ElseIf sender Is mVanityPlates Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.VanityPlates, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.VanityPlates = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_VANITYPLATE")
                End If
            ElseIf sender Is mGrille Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Grille, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Grille = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mHood Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hood, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Hood = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mHorn Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Horns, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Horns = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_HORN")
                End If
            ElseIf sender Is mHydraulics Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hydraulics, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Hydraulics = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_HYDRAULICS")
                End If
            ElseIf sender Is mLivery Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Livery, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Livery = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_LIVERY")
                End If
            ElseIf sender Is mTornadoC Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetLivery2(selectedItem.SubInteger1)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Livery2 = selectedItem.SubInteger1
                    PlaySpeech("LR_SELL_LIVERY")
                End If
            ElseIf sender Is mPlaques Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Plaques, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Plaques = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_PLAQUE")
                End If
            ElseIf sender Is mRoof Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Roof, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Roof = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mSpeakers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Speakers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Speakers = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_ICE")
                End If
            ElseIf sender Is mSpoilers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Spoilers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Spoilers = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mTank Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Tank, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Tank = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mTrunk Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Trunk, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Trunk = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_TRUNK")
                End If
            ElseIf sender Is mWindow Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Windows, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Windows = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mTurbo Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, CBool(selectedItem.SubInteger1))
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Turbo = CBool(selectedItem.SubInteger1)
                    PlaySpeech("SHOP_SELL_TURBO")
                End If
            ElseIf sender Is mTint Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.WindowTint = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Tint = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            End If

            'Bike Mods
            If sender Is mShifter Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Fender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Fender = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mFMudguard Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.FrontBumper = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mBSeat Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hood, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Hood = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mOilTank Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Grille, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Grille = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mRMudguard Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RearBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.RearBumper = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mFuelTank Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Roof, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Roof = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mBeltDriveCovers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Spoilers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Spoilers = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mBEngineBlock Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Frame, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Frame = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mBAirFilter Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SideSkirt, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.SideSkirt = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            ElseIf sender Is mBTank Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Tank, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.Tank = selectedItem.SubInteger1
                    PlaySpeech("")
                End If
            End If

            'Neons Mods
            If sender Is mNeon Then
                Select Case selectedItem.SubInteger1
                    Case NeonLayouts.None
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case NeonLayouts.Front
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case NeonLayouts.Back
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case NeonLayouts.Sides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case NeonLayouts.FrontAndBack
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case NeonLayouts.FrontAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case NeonLayouts.BackAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case NeonLayouts.FrontBackAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                End Select
                PlaySpeech("")
            End If

            'Wheels Mods           
            If (sender Is mSBikeWheels) Or (sender Is mCBikeWheels) Then 'gmBikeWheels
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                    Bennys.veh.SetMod(VehicleMod.BackWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.WheelType = Bennys.veh.WheelType
                    Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                    Bennys.lastVehMemory.BackWheels = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_WHEEL")
                End If
            ElseIf (sender Is mSHighEnd) Or (sender Is mSLowrider) Or (sender Is mSMuscle) Or (sender Is mSOffroad) Or (sender Is mSSport) Or (sender Is mSSUV) Or (sender Is mSTuner) Or (sender Is mCHighEnd) Or (sender Is mCLowrider) Or (sender Is mCMuscle) Or (sender Is mCOffroad) Or (sender Is mCSport) Or (sender Is mCSUV) Or (sender Is mCTuner) Or (sender Is mBennysOriginals) Or (sender Is mBespoke) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.WheelType = Bennys.veh.WheelType
                    Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                    PlaySpeech("LR_UPGRADE_WHEEL")
                End If
            End If
            If sender Is mTires Then
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                            selectedItem.SetRightLabel(Nothing)
                            Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                            selectedItem.SubInteger2 = 0
                        End If
                    Case Else
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            If selectedItem.SubInteger1 = 1 Then
                                Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), False)
                                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), False)
                                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                                Bennys.lastVehMemory.WheelsVariation = False
                                selectedItem.SetRightLabel(Nothing)
                                Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                                selectedItem.SubInteger2 = 0
                            ElseIf selectedItem.SubInteger1 = 7 Then
                                Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), True)
                                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), True)
                                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                                Bennys.lastVehMemory.WheelsVariation = True
                                selectedItem.SetRightLabel(Nothing)
                                Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                                selectedItem.SubInteger2 = 0
                            End If
                        End If
                End Select
                PlaySpeech("LR_UPGRADE_WHEEL")
            End If

            'Wheel Type
            If sender Is gmWheelType Then
                If selectedItem Is giBikeWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.BikeWheels
                    ''RefreshModMenuFor(gmBikeWheels, iBikeWheels, VehicleMod.BackWheels)
                    'RefreshStockWheelsModMenuFor(mSBikeWheels, iSBikeWheels, VehicleMod.FrontWheels)
                    'RefreshChromeWheelsModMenuFor(mCBikeWheels, iCBikeWheels, VehicleMod.FrontWheels)
                    RefreshBikeWheelsModMenuFor(mSBikeWheels, iSBikeWheels, VehicleMod.BackWheels, False)
                    RefreshBikeWheelsModMenuFor(mCBikeWheels, iCBikeWheels, VehicleMod.BackWheels, True)
                ElseIf selectedItem Is giHighEndWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.HighEnd
                    RefreshStockWheelsModMenuFor(mSHighEnd, iSHighEnd, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCHighEnd, iCHighEnd, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giLowriderWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Lowrider
                    RefreshStockWheelsModMenuFor(mSLowrider, iSLowrider, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCLowrider, iCLowrider, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giMuscleWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Muscle
                    RefreshStockWheelsModMenuFor(mSMuscle, iSMuscle, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCMuscle, iCMuscle, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giOffroadWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Offroad
                    RefreshStockWheelsModMenuFor(mSOffroad, iSOffroad, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCOffroad, iCOffroad, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giSportWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Sport
                    RefreshStockWheelsModMenuFor(mSSport, iSSport, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCSport, iCSport, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giSUVWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.SUV
                    RefreshStockWheelsModMenuFor(mSSUV, iSSUV, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCSUV, iCSUV, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giTunerWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Tuner
                    RefreshStockWheelsModMenuFor(mSTuner, iSTuner, VehicleMod.FrontWheels)
                    RefreshChromeWheelsModMenuFor(mCTuner, iCTuner, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giBennysWheels Then
                    Bennys.veh.WheelType = 8
                    RefreshLowriderDLCWheelsModMenuFor(mBennysOriginals, iBennys, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giBespokeWheels Then
                    Bennys.veh.WheelType = 9
                    RefreshLowriderDLCWheelsModMenuFor(mBespoke, iBespoke, VehicleMod.FrontWheels)
                End If
            End If
            If sender Is gmBikeWheels Then
                Bennys.veh.WheelType = VehicleWheelType.BikeWheels
            ElseIf sender Is gmHighEnd Then
                Bennys.veh.WheelType = VehicleWheelType.HighEnd
            ElseIf sender Is gmLowrider Then
                Bennys.veh.WheelType = VehicleWheelType.Lowrider
            ElseIf sender Is gmMuscle Then
                Bennys.veh.WheelType = VehicleWheelType.Muscle
            ElseIf sender Is gmOffroad Then
                Bennys.veh.WheelType = VehicleWheelType.Offroad
            ElseIf sender Is gmSport Then
                Bennys.veh.WheelType = VehicleWheelType.Sport
            ElseIf sender Is gmSUV Then
                Bennys.veh.WheelType = VehicleWheelType.SUV
            ElseIf sender Is gmTuner Then
                Bennys.veh.WheelType = VehicleWheelType.Tuner
            ElseIf sender Is mBennysOriginals Then
                Bennys.veh.WheelType = 8
            ElseIf sender Is mBespoke Then
                Bennys.veh.WheelType = 9
            End If

            'Color
            If sender Is mLightsColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.DashboardColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.LightsColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf sender Is mTrimColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.TrimColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.TrimColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf sender Is mRimColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.RimColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.RimColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf (sender Is mPrimaryChromeColor) Or (sender Is mPrimaryClassicColor) Or (sender Is mPrimaryMatteColor) Or (sender Is mPrimaryMetalsColor) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.PrimaryColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.PrimaryColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf sender Is mPrimaryMetallicColor Then

                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.PrimaryColor = selectedItem.SubInteger1
                    Bennys.veh.PearlescentColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.PrimaryColor = selectedItem.SubInteger1
                    Bennys.lastVehMemory.PearlescentColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf sender Is mPrimaryPearlescentColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.PearlescentColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.PearlescentColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf (sender Is mSecondaryChromeColor) Or (sender Is mSecondaryClassicColor) Or (sender Is mSecondaryMatteColor) Or (sender Is mSecondaryMetallicColor) Or (sender Is mSecondaryMetalsColor) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SecondaryColor = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger2)
                    selectedItem.SubInteger2 = 0
                    Bennys.lastVehMemory.SecondaryColor = selectedItem.SubInteger1
                    PlaySpeech("SHOP_SELL_COSMETICS")
                End If
            ElseIf sender Is mNeonColor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.NeonLightsColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger4)
                    selectedItem.SubInteger4 = 0
                    Bennys.lastVehMemory.NeonLightsColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                    PlaySpeech("")
                End If
            ElseIf sender Is mTireSmoke Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.TireSmokeColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                    Bennys.veh.ToggleMod(VehicleToggleMod.TireSmoke, True)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    selectedItem.SetRightLabel(Nothing)
                    Game.Player.Money = (Game.Player.Money - selectedItem.SubInteger4)
                    selectedItem.SubInteger4 = 0
                    Bennys.lastVehMemory.TireSmokeColor = Drawing.Color.FromArgb(selectedItem.SubInteger1, selectedItem.SubInteger2, selectedItem.SubInteger3)
                    PlaySpeech("")
                End If
            End If

            'Camera
            If sender Is gmBumper Then
                If selectedItem Is giFBumper Then
                    Select Case Bennys.veh.Model
                        Case "monster3", "monster4", "monster5"
                            camera.MainCameraPosition = CameraPosition.Car
                        Case Else
                            If Bennys.veh.HasBone("neon_f") Then
                                camera.MainCameraPosition = CameraPosition.FrontBumper
                            Else
                                camera.MainCameraPosition = CameraPosition.Hood
                            End If
                    End Select
                ElseIf selectedItem Is giRBumper Then
                    Select Case Bennys.veh.Model
                        Case "monster3", "monster4", "monster5"
                            camera.MainCameraPosition = CameraPosition.Car
                        Case Else
                            If Bennys.veh.HasBone("neon_r") Then
                                Select Case Bennys.veh.Model
                                    Case "barrage"
                                        camera.MainCameraPosition = CameraPosition.Car
                                    Case Else
                                        camera.MainCameraPosition = CameraPosition.RearBumper
                                End Select
                            Else
                                camera.MainCameraPosition = CameraPosition.Trunk
                            End If
                    End Select
                ElseIf selectedItem Is giSSkirt Then
                    Select Case Bennys.veh.Model
                        Case "barrage"
                            camera.MainCameraPosition = CameraPosition.Car
                        Case Else
                            camera.MainCameraPosition = CameraPosition.Wheels
                    End Select
                End If
            ElseIf sender Is gmPlate Then
                If selectedItem Is giNumberPlate Then
                    If Bennys.veh.HasBone("platelight") Then
                        If Bennys.veh.ClassType = VehicleClass.Motorcycles Or Bennys.veh.Model = "blazer4" Then
                            camera.MainCameraPosition = CameraPosition.Car
                        Else
                            camera.MainCameraPosition = CameraPosition.BackPlate
                        End If
                    ElseIf Bennys.veh.HasBone("neon_f") Then
                        Select Case Bennys.veh.Model
                            Case "stromberg", "z190", "comet4", "autarch"
                                camera.MainCameraPosition = CameraPosition.Car
                            Case Else
                                camera.MainCameraPosition = CameraPosition.FrontPlate
                        End Select
                    Else
                        camera.MainCameraPosition = CameraPosition.Car
                    End If
                ElseIf selectedItem Is giPlateHolder Then
                    Select Case Bennys.veh.Model
                        Case "slamvan3", "buccaneer2", "chino2", "sabregt2", "voodoo", "primo2", "tornado5", "minivan2"
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
            ElseIf sender Is gmBodywork Then
                If ((selectedItem Is giShifter) Or (selectedItem Is giFuelTank) Or (selectedItem Is giOilTank) Or (selectedItem Is giBeltDriveCovers) Or (selectedItem Is giBTank)) Then
                    camera.MainCameraPosition = CameraPosition.Wheels
                ElseIf selectedItem Is giFMudguard Then
                    Select Case Bennys.veh.Model
                        Case "blazer4"
                            camera.MainCameraPosition = CameraPosition.Engine
                        Case Else
                            camera.MainCameraPosition = CameraPosition.FrontMuguard
                    End Select

                ElseIf selectedItem Is giRMudguard Then
                    camera.MainCameraPosition = CameraPosition.RearMuguard
                End If
            ElseIf sender Is gmEngine Then
                If selectedItem Is giStruts Then
                    Select Case Bennys.veh.Model
                        Case "comet3"
                            Bennys.veh.OpenDoor(VehicleDoor.Trunk, False, False)
                            camera.MainCameraPosition = CameraPosition.FrontBumper
                    End Select
                End If
            ElseIf sender Is gmBodyworkArena Then
                If selectedItem Is giOrnaments Then
                    camera.MainCameraPosition = CameraPosition.Interior
                End If
            ElseIf sender Is gmWeapon Then
                If selectedItem Is giArchCover Then
                    Select Case Bennys.veh.Model
                        Case "monster3", "monster4", "monster5"
                            camera.MainCameraPosition = CameraPosition.Car
                        Case Else
                            camera.MainCameraPosition = CameraPosition.FrontBumper
                    End Select
                ElseIf selectedItem Is giTank Then
                    HoodCamera(False)
                ElseIf selectedItem Is giRoof Then
                    If Bennys.veh.HasBone("boot") Then
                        If Bennys.veh.GetVehTrunkPos = EngineLoc.rear Then
                            camera.MainCameraPosition = CameraPosition.Trunk
                        Else
                            If Bennys.veh.HasBone("windscreen_r") Then
                                camera.MainCameraPosition = CameraPosition.RearWindscreen
                            Else
                                camera.MainCameraPosition = CameraPosition.RearEngine
                            End If
                        End If
                    ElseIf Bennys.veh.HasBone("windscreen_r") Then
                        camera.MainCameraPosition = CameraPosition.RearWindscreen
                    ElseIf Bennys.veh.GetVehEnginePos = EngineLoc.rear Then
                        Select Case Bennys.veh.Model
                            Case "barrage"
                                camera.MainCameraPosition = CameraPosition.Car
                            Case Else
                                camera.MainCameraPosition = CameraPosition.RearEngine
                        End Select
                    ElseIf Bennys.veh.HasBone("neon_b") Then
                        camera.MainCameraPosition = CameraPosition.RearBumper
                    Else
                        camera.MainCameraPosition = CameraPosition.Car
                    End If
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ArenaWarMenuIndexChangedHandler(sender As UIMenu, index As Integer)
        Dim selecteditem As UIMenuItem = sender.MenuItems(index)
        arenaVehImage = selecteditem.SubString2
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
            ElseIf sender Is mNitro Then
                Bennys.veh.SetBool(nitroMod, CBool(sender.MenuItems(index).SubInteger1))
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
                If index = 0 Then Bennys.veh.SetXenonHeadlightsColor(sender.MenuItems(index).SubInteger3, False) Else Bennys.veh.SetXenonHeadlightsColor(sender.MenuItems(index).SubInteger3, True)
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
            ElseIf sender Is mTornadoC Then
                Bennys.veh.SetLivery2(sender.MenuItems(index).SubInteger1)
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
            ElseIf sender Is mTint Then
                Bennys.veh.WindowTint = sender.MenuItems(index).SubInteger1
            End If

            'Bike Mods
            If sender Is mShifter Then
                Bennys.veh.SetMod(VehicleMod.Fender, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mFMudguard Then
                Bennys.veh.SetMod(VehicleMod.FrontBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBSeat Then
                Bennys.veh.SetMod(VehicleMod.Hood, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mOilTank Then
                Bennys.veh.SetMod(VehicleMod.Grille, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRMudguard Then
                Bennys.veh.SetMod(VehicleMod.RearBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mFuelTank Then
                Bennys.veh.SetMod(VehicleMod.Roof, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBeltDriveCovers Then
                Bennys.veh.SetMod(VehicleMod.Spoilers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBEngineBlock Then
                Bennys.veh.SetMod(VehicleMod.Frame, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBAirFilter Then
                Bennys.veh.SetMod(VehicleMod.SideSkirt, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBTank Then
                Bennys.veh.SetMod(VehicleMod.Tank, sender.MenuItems(index).SubInteger1, False)
            End If

            'Neons Mods
            If sender Is mNeon Then
                Select Case sender.MenuItems(index).SubInteger1
                    Case NeonLayouts.None
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case NeonLayouts.Front
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case NeonLayouts.Back
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case NeonLayouts.Sides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case NeonLayouts.FrontAndBack
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case NeonLayouts.FrontAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case NeonLayouts.BackAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case NeonLayouts.FrontBackAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                End Select
            End If

            'Wheels Mods
            If (sender Is mSBikeWheels) Or (sender Is mCBikeWheels) Then 'gmBikeWheels
                Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
                Bennys.veh.SetMod(VehicleMod.BackWheels, sender.MenuItems(index).SubInteger1, False)
            ElseIf (sender Is mSHighEnd) Or (sender Is mSLowrider) Or (sender Is mSMuscle) Or (sender Is mSOffroad) Or (sender Is mSSport) Or (sender Is mSSUV) Or (sender Is mSTuner) Or (sender Is mCHighEnd) Or (sender Is mCLowrider) Or (sender Is mCMuscle) Or (sender Is mCOffroad) Or (sender Is mCSport) Or (sender Is mCSUV) Or (sender Is mCTuner) Or (sender Is mBennysOriginals) Or (sender Is mBespoke) Then
                Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
            End If
            If sender Is mTires Then
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                        Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
                    Case Else
                        If sender.MenuItems(index).SubInteger1 = 1 Then
                            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), False)
                            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), False)
                        ElseIf sender.MenuItems(index).SubInteger1 = 7 Then
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
            ElseIf sender Is mPrimaryMetallicColor Then
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
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 19) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 19, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 19)
        End If

        _menuPool = New MenuPool()
        camera = New WorkshopCamera
        BtnFirstPerson = New InstructionalButton(Bennys.fpcKey, Game.GetGXTEntry("MO_ZOOM_FIRST")) 'MO_ZOOM_FIRST   LOB_FCP_1
        BtnZoom = New InstructionalButton(Bennys.zinKey, Game.GetGXTEntry("INPUT_CREATOR_ZOOM_IN_DISPLAYONLY")) 'CELL_284
        BtnZoomOut = New InstructionalButton(Bennys.zoutKey, Game.GetGXTEntry("INPUT_CREATOR_ZOOM_OUT_DISPLAYONLY"))
        CreateMenus()
        Native.Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_1", False, -1)
        Native.Function.Call(Hash.REQUEST_SCRIPT_AUDIO_BANK, "VEHICLE_SHOP_HUD_2", False, -1)
    End Sub

    Public Shared Sub CreateMenus()
        CreateQuitMenu()
        CreateMainMenu()
        CreateArenaWarMenu()
        CreateBodyworkMenu()
        CreateBodyworkArenaMenu()
        CreateWeaponMenu()
        CreateModMenuFor(mAerials, Game.GetGXTEntry("CMM_MOD_ST18")) 'AERIALS
        CreateModMenuFor(mTrim, Game.GetGXTEntry("CMM_MOD_ST19")) 'TRIM
        CreateModMenuFor(mWindow, Game.GetGXTEntry("CMM_MOD_ST21")) 'WINDOWS
        CreateModMenuFor(mArchCover, Game.GetGXTEntry("CMM_MOD_ST17")) 'ARCH COVERS
        CreateEngineMenu()
        CreatePerformanceMenuFor(mEngine, Game.GetGXTEntry("CMM_MOD_GT3")) 'ENGINE
        CreateNitroMenu() 'NITRO
        CreateModMenuFor(mEngineBlock, Game.GetGXTEntry("CMOD_EB_T")) 'ENGINE BLOCK
        CreateModMenuFor(mAirFilter, Game.GetGXTEntry("CMM_MOD_ST15")) 'AIR FILTER
        CreateModMenuFor(mStruts, Game.GetGXTEntry("CMM_MOD_ST16")) 'STRUTS
        CreateInteriorMenu()
        CreateModMenuFor(mColumnShifterLevers, Game.GetGXTEntry("CMM_MOD_ST9")) 'COLUMN SHIFTER LEVERS
        CreateModMenuFor(mDashboard, Game.GetGXTEntry("CMM_MOD_ST4")) 'DASHBOARD
        CreateColorMenuFor(mLightsColor, Game.GetGXTEntry("CMM_MOD_ST26"))
        CreateModMenuFor(mDialDesign, Game.GetGXTEntry("CMM_MOD_ST5")) 'DIAL DESIGN
        CreateModMenuFor(mOrnaments, Game.GetGXTEntry("CMM_MOD_ST3")) 'ORNAMENTS
        CreateModMenuFor(mSeats, Game.GetGXTEntry("CMM_MOD_ST7")) 'SEATS
        CreateModMenuFor(mSteeringWheels, Game.GetGXTEntry("CMM_MOD_ST8")) 'STEERING WHEELS
        CreateModMenuFor(mTrimDesign, Game.GetGXTEntry("CMM_MOD_ST2")) 'TRIM DESIGN
        CreateColorMenuFor(mTrimColor, Game.GetGXTEntry("CMOD_MOD_TRIM2").ToUpper)
        CreateModMenuFor(mDoor, Game.GetGXTEntry("CMM_MOD_ST6")) 'DOORS
        CreateBumperMenu()
        CreateModMenuFor(mFBumper, Game.GetGXTEntry("CMOD_BUMF_T")) 'FRONT BUMPERS
        CreateModMenuFor(mRBumper, Game.GetGXTEntry("CMOD_BUMR_T")) 'REAR BUMPERS
        CreateModMenuFor(mSSkirt, Game.GetGXTEntry("CMOD_SS_T")) 'SIDE SKIRT
        CreateWheelsMenu()
        CreateWheelTypeMenu()

        CreateWheelRimMenu(gmBikeWheels, GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper) 'BIKE WHEELS
        CreateModMenuFor(mSBikeWheels, GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCBikeWheels, GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper) 'CHROME RIMS
        'CreateModMenuFor(gmBikeWheels, GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels).ToUpper) 'BIKE WHEELS

        CreateWheelRimMenu(gmHighEnd, GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper) 'HIGH END
        CreateModMenuFor(mSHighEnd, GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCHighEnd, GetLocalizedWheelTypeName(VehicleWheelType.HighEnd).ToUpper) 'CHROME RIMS

        CreateWheelRimMenu(gmLowrider, GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper) 'LOWRIDER
        CreateModMenuFor(mSLowrider, GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCLowrider, GetLocalizedWheelTypeName(VehicleWheelType.Lowrider).ToUpper) 'CHROME RIMS

        CreateWheelRimMenu(gmMuscle, GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper) 'MUSCLE
        CreateModMenuFor(mSMuscle, GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCMuscle, GetLocalizedWheelTypeName(VehicleWheelType.Muscle).ToUpper) 'CHROME RIMS

        CreateWheelRimMenu(gmOffroad, GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper) 'OFFROAD
        CreateModMenuFor(mSOffroad, GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCOffroad, GetLocalizedWheelTypeName(VehicleWheelType.Offroad).ToUpper) 'CHROME RIMS

        CreateWheelRimMenu(gmSport, GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper) 'SPORT
        CreateModMenuFor(mSSport, GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCSport, GetLocalizedWheelTypeName(VehicleWheelType.Sport).ToUpper) 'CHROME RIMS

        CreateWheelRimMenu(gmSUV, GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper) 'SUV
        CreateModMenuFor(mSSUV, GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCSUV, GetLocalizedWheelTypeName(VehicleWheelType.SUV).ToUpper) 'CHROME RIMS

        CreateWheelRimMenu(gmTuner, GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper) 'TUNER
        CreateModMenuFor(mSTuner, GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper) 'STOCK RIMS
        CreateModMenuFor(mCTuner, GetLocalizedWheelTypeName(VehicleWheelType.Tuner).ToUpper) 'CHROME RIMS

        CreateModMenuFor(mBennysOriginals, GetLocalizedWheelTypeName(8).ToUpper) 'BENNY'S ORIGINALS
        CreateModMenuFor(mBespoke, GetLocalizedWheelTypeName(9).ToUpper) 'BENNY'S BESPOKE
        CreateColorMenuFor(mRimColor, LocalizedModGroupName(GroupName.WheelColor).ToUpper)
        CreateTyresMenu()
        CreateModMenuFor(mTireSmoke, Game.GetGXTEntry("CMOD_MOD_TYR3").ToUpper)
        CreatePlateMenu()
        CreateModMenuFor(mPlateHolder, Game.GetGXTEntry("CMOD_PLH_T")) 'PLATE HOLDERS
        CreateModMenuFor(mVanityPlates, Game.GetGXTEntry("CMM_MOD_ST1")) 'VANITY PLATES
        CreatePlateNumberMenu()
        CreateLightsMenu()
        CreateModMenuFor(mHeadlights, Game.GetGXTEntry("CMOD_HED_T")) 'HEADLIGHTS
        CreateNeonKitsMenu()
        CreateNeonMenu()
        CreateModMenuFor(mNeonColor, Game.GetGXTEntry("CMOD_NEON_1").ToUpper)
        CreateResprayMenu()
        CreateModMenuFor(mPrimaryColor, Game.GetGXTEntry("CMOD_COL1_T"))
        CreateModMenuFor(mPrimaryClassicColor, Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        CreateModMenuFor(mPrimaryChromeColor, Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        CreateModMenuFor(mPrimaryMetallicColor, Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        CreateModMenuFor(mPrimaryMetalsColor, Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        CreateModMenuFor(mPrimaryMatteColor, Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        CreateModMenuFor(mPrimaryPearlescentColor, Game.GetGXTEntry("CMOD_COL0_0").ToUpper)
        CreateModMenuFor(mSecondaryColor, Game.GetGXTEntry("CMOD_COL1_T"))
        CreateModMenuFor(mSecondaryClassicColor, Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        CreateModMenuFor(mSecondaryChromeColor, Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        CreateModMenuFor(mSecondaryMetallicColor, Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        CreateModMenuFor(mSecondaryMetalsColor, Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        CreateModMenuFor(mSecondaryMatteColor, Game.GetGXTEntry("CMOD_COL0_1").ToUpper)
        CreateModMenuFor(mExhaust, Game.GetGXTEntry("CMOD_EXH_T")) 'EXHAUST
        CreateModMenuFor(mFender, Game.GetGXTEntry("CMOD_WNG_T")) 'FENDER
        CreateModMenuFor(mRFender, Game.GetGXTEntry("CMOD_WNG_T")) 'RIGHT FENDER
        CreateModMenuFor(mFrame, Game.GetGXTEntry("CMOD_RC_T")) 'ROLL CAGE
        CreateModMenuFor(mGrille, Game.GetGXTEntry("CMOD_GRL_T")) 'GRILLES
        CreateModMenuFor(mHood, Game.GetGXTEntry("CMOD_BON_T")) 'HOOD
        CreateModMenuFor(mHorn, Game.GetGXTEntry("CMOD_HRN_T")) 'HORN
        CreateModMenuFor(mHydraulics, Game.GetGXTEntry("CMM_MOD_ST13")) 'HYDRAULICS
        CreateModMenuFor(mLivery, Game.GetGXTEntry("CMM_MOD_ST23")) 'LIVERY
        CreateModMenuFor(mTornadoC, Game.GetGXTEntry("CMOD_ROF_T")) 'ROOF
        CreateModMenuFor(mPlaques, Game.GetGXTEntry("CMM_MOD_ST10")) 'PLAQUES
        CreateModMenuFor(mRoof, Game.GetGXTEntry("CMOD_ROF_T")) 'ROOF
        CreateModMenuFor(mSpeakers, Game.GetGXTEntry("CMM_MOD_S11")) 'SPEAKERS
        CreateModMenuFor(mSpoilers, Game.GetGXTEntry("CMOD_SPO_T")) 'SPOILER
        CreateModMenuFor(mTank, Game.GetGXTEntry("CMM_MOD_ST20")) 'TANK
        CreateModMenuFor(mTrunk, Game.GetGXTEntry("CMOD_TR_T")) 'TRUNKS
        CreateModMenuFor(mTurbo, Game.GetGXTEntry("CMOD_TUR_T")) 'TURBO
        CreatePerformanceMenuFor(mSuspension, Game.GetGXTEntry("CMOD_SUS_T")) 'SUSPENSION
        CreatePerformanceMenuFor(mArmor, Game.GetGXTEntry("CMOD_ARM_T")) 'ARMOR
        CreatePerformanceMenuFor(mBrakes, Game.GetGXTEntry("CMOD_BRA_T")) 'BRAKES
        CreatePerformanceMenuFor(mTransmission, Game.GetGXTEntry("CMOD_GBX_T")) 'TRANSMISSION
        CreateTintMenu()
        'Motorcycles
        CreateModMenuFor(mShifter, Game.GetGXTEntry("CMOD_SHIFTER_T"))
        CreateModMenuFor(mFMudguard, Game.GetGXTEntry("CMOD_FMUD_T"))
        CreateModMenuFor(mBSeat, Game.GetGXTEntry("CMM_MOD_ST7"))
        CreateModMenuFor(mOilTank, Game.GetGXTEntry("CMM_MOD_ST29"))
        CreateModMenuFor(mRMudguard, Game.GetGXTEntry("CMOD_RMUD_T"))
        CreateModMenuFor(mFuelTank, Game.GetGXTEntry("CMOD_FUL_T"))
        CreateModMenuFor(mBeltDriveCovers, Game.GetGXTEntry("CMOD_MOD_BLT").ToUpper)
        CreateModMenuFor(mBEngineBlock, Game.GetGXTEntry("CMOD_EB_T"))
        CreateModMenuFor(mBAirFilter, Game.GetGXTEntry("CMM_MOD_ST15"))
        CreateModMenuFor(mBTank, Game.GetGXTEntry("CMM_MOD_ST20"))
    End Sub

    Public Shared Sub RefreshMenus()
        If arenavehicle.Contains(Bennys.veh.Model) Then
            RefreshBodyworkArenaMenu()
            RefreshWeaponMenu()
        ElseIf arenawar.Contains(Bennys.veh.Model) Then
            RefreshArenaWarMenu()
        Else
            RefreshBodyworkMenu()
        End If
        RefreshModMenuFor(mAerials, iAerials, VehicleMod.Aerials)
        RefreshModMenuFor(mTrim, iTrim, VehicleMod.Trim)
        RefreshModMenuFor(mWindow, iWindows, VehicleMod.Windows)
        RefreshModMenuFor(mArchCover, iArchCover, VehicleMod.ArchCover)
        RefreshEngineMenu()
        RefreshPerformanceMenuFor(mEngine, iEngine, VehicleMod.Engine, "CMOD_ENG_")
        RefreshNitroMenu()
        RefreshModMenuFor(mEngineBlock, iEngineBlock, VehicleMod.EngineBlock)
        RefreshModMenuFor(mAirFilter, iAirFilter, VehicleMod.AirFilter)
        RefreshModMenuFor(mStruts, iStruts, VehicleMod.Struts)
        RefreshInteriorMenu()
        RefreshModMenuFor(mColumnShifterLevers, iColumnShifterLevers, VehicleMod.ColumnShifterLevers)
        RefreshModMenuFor(mDashboard, iDashboard, VehicleMod.Dashboard)
        RefreshEnumModMenuFor(mLightsColor, iLightsColor, EnumTypes.VehicleColorDashboard)
        RefreshModMenuFor(mDialDesign, iDialDesign, VehicleMod.DialDesign)
        RefreshModMenuFor(mOrnaments, iOrnaments, VehicleMod.Ornaments)
        RefreshModMenuFor(mSeats, iSeats, VehicleMod.Seats)
        RefreshModMenuFor(mSteeringWheels, iSteeringWheels, VehicleMod.SteeringWheels)
        RefreshModMenuFor(mTrimDesign, iTrimDesign, VehicleMod.TrimDesign)
        RefreshEnumModMenuFor(mTrimColor, iTrimColor, EnumTypes.VehicleColorTrim)
        RefreshModMenuFor(mDoor, iDoor, VehicleMod.DoorSpeakers)
        RefreshBumperMenu()
        RefreshModMenuFor(mFBumper, iFBumper, VehicleMod.FrontBumper)
        RefreshModMenuFor(mRBumper, iRBumper, VehicleMod.RearBumper)
        RefreshModMenuFor(mSSkirt, iSideSkirt, VehicleMod.SideSkirt)
        RefreshWheelsMenu()
        RefreshWheelTypeMenu()

        RefreshWheelRimMenu(gmBikeWheels, mSBikeWheels, mCBikeWheels, iSBikeWheels, iCBikeWheels)
        'RefreshStockWheelsModMenuFor(mSBikeWheels, iSBikeWheels, VehicleMod.BackWheels)
        'RefreshChromeWheelsModMenuFor(mCBikeWheels, iCBikeWheels, VehicleMod.BackWheels)
        ''RefreshModMenuFor(gmBikeWheels, iBikeWheels, VehicleMod.BackWheels)
        RefreshBikeWheelsModMenuFor(mSBikeWheels, iSBikeWheels, VehicleMod.BackWheels, False)
        RefreshBikeWheelsModMenuFor(mCBikeWheels, iCBikeWheels, VehicleMod.BackWheels, True)

        RefreshWheelRimMenu(gmHighEnd, mSHighEnd, mCHighEnd, iSHighEnd, iCHighEnd)
        RefreshStockWheelsModMenuFor(mSHighEnd, iSHighEnd, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCHighEnd, iCHighEnd, VehicleMod.FrontWheels)
        RefreshWheelRimMenu(gmLowrider, mSLowrider, mCLowrider, iSLowrider, iCLowrider)
        RefreshStockWheelsModMenuFor(mSLowrider, iSLowrider, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCLowrider, iCLowrider, VehicleMod.FrontWheels)
        RefreshWheelRimMenu(gmMuscle, mSMuscle, mCMuscle, iSMuscle, iCMuscle)
        RefreshStockWheelsModMenuFor(mSMuscle, iSMuscle, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCMuscle, iCMuscle, VehicleMod.FrontWheels)
        RefreshWheelRimMenu(gmOffroad, mSOffroad, mCOffroad, iSOffroad, iCOffroad)
        RefreshStockWheelsModMenuFor(mSOffroad, iSOffroad, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCOffroad, iCOffroad, VehicleMod.FrontWheels)
        RefreshWheelRimMenu(gmSport, mSSport, mCSport, iSSport, iCSport)
        RefreshStockWheelsModMenuFor(mSSport, iSSport, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCSport, iCSport, VehicleMod.FrontWheels)
        RefreshWheelRimMenu(gmSUV, mSSUV, mCSUV, iSSUV, iCSUV)
        RefreshStockWheelsModMenuFor(mSSUV, iSSUV, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCSUV, iCSUV, VehicleMod.FrontWheels)
        RefreshWheelRimMenu(gmTuner, mSTuner, mCTuner, iSTuner, iCTuner)
        RefreshStockWheelsModMenuFor(mSTuner, iSTuner, VehicleMod.FrontWheels)
        RefreshChromeWheelsModMenuFor(mCTuner, iCTuner, VehicleMod.FrontWheels)
        RefreshLowriderDLCWheelsModMenuFor(mBennysOriginals, iBennys, VehicleMod.FrontWheels)
        RefreshLowriderDLCWheelsModMenuFor(mBespoke, iBespoke, VehicleMod.FrontWheels)
        RefreshEnumModMenuFor(mRimColor, iRimColor, EnumTypes.VehicleColorRim)
        RefreshTyresMenu()
        RefreshRGBColorMenuFor(mTireSmoke, iTireSmoke, "Smoke")
        RefreshPlateMenu()
        RefreshModMenuFor(mPlateHolder, iPlateHolder, VehicleMod.PlateHolder)
        RefreshModMenuFor(mVanityPlates, iVanityPlates, VehicleMod.VanityPlates)
        RefreshEnumModMenuFor(mNumberPlate, iNumberPlate, EnumTypes.NumberPlateType)
        RefreshLightsMenu()
        RefreshModMenuForHeadlightsColor(mHeadlights, iHeadlights, VehicleToggleMod.XenonHeadlights)
        RefreshNeonKitsMenu()
        RefreshNeonMenu()
        RefreshRGBColorMenuFor(mNeonColor, iNeonColor, "Neon")
        RefreshResprayMenu()
        RefreshPrimaryColorMenu()
        RefreshColorMenuFor(mPrimaryChromeColor, iPrimaryChromeColor, ChromeColor, "Primary")
        RefreshColorMenuFor(mPrimaryClassicColor, iPrimaryClassicColor, ClassicColor, "Primary")
        RefreshColorMenuFor(mPrimaryMetallicColor, iPrimaryMetallicColor, ClassicColor, "Primary")
        RefreshColorMenuFor(mPrimaryMetalsColor, iPrimaryMetalsColor, MetalColor, "Primary")
        RefreshColorMenuFor(mPrimaryMatteColor, iPrimaryMatteColor, MatteColor, "Primary")
        RefreshColorMenuFor(mPrimaryPearlescentColor, iPrimaryPearlescentColor, PearlescentColor, "Pearlescent")
        RefreshSecondaryColorMenu()
        RefreshColorMenuFor(mSecondaryChromeColor, iSecondaryChromeColor, ChromeColor, "Secondary")
        RefreshColorMenuFor(mSecondaryClassicColor, iSecondaryClassicColor, ClassicColor, "Secondary")
        RefreshColorMenuFor(mSecondaryMetallicColor, iSecondaryMetallicColor, ClassicColor, "Secondary")
        RefreshColorMenuFor(mSecondaryMetalsColor, iSecondaryMetalsColor, MetalColor, "Secondary")
        RefreshColorMenuFor(mSecondaryMatteColor, iSecondaryMatteColor, MatteColor, "Secondary")
        RefreshModMenuFor(mExhaust, iExhaust, VehicleMod.Exhaust)
        RefreshModMenuFor(mFender, iFender, VehicleMod.Fender)
        RefreshModMenuFor(mRFender, iRFender, VehicleMod.RightFender)
        RefreshModMenuFor(mFrame, iFrame, VehicleMod.Frame)
        RefreshModMenuFor(mGrille, iGrille, VehicleMod.Grille)
        RefreshModMenuFor(mHood, iHood, VehicleMod.Hood)
        RefreshModMenuFor(mHorn, iHorn, VehicleMod.Horns)
        RefreshModMenuFor(mHydraulics, iHydraulics, VehicleMod.Hydraulics)
        RefreshModMenuFor(mLivery, iLivery, VehicleMod.Livery)
        RefreshModMenuForLivery2(mTornadoC, iTornadoC)
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
        RefreshEnumModMenuFor(mTint, iTint, EnumTypes.VehicleWindowTint)
        'Motorcycles
        RefreshModMenuFor(mShifter, iShifter, VehicleMod.Fender)
        RefreshModMenuFor(mFMudguard, iFMudguard, VehicleMod.FrontBumper)
        RefreshModMenuFor(mBSeat, iBSeat, VehicleMod.Hood)
        RefreshModMenuFor(mOilTank, iOilTank, VehicleMod.Grille)
        RefreshModMenuFor(mRMudguard, iRMudguard, VehicleMod.RearBumper)
        RefreshModMenuFor(mFuelTank, iFuelTank, VehicleMod.Roof)
        RefreshModMenuFor(mBeltDriveCovers, iBeltDriveCovers, VehicleMod.Spoilers)
        RefreshModMenuFor(mBEngineBlock, iBEngineBlock, VehicleMod.Frame)
        RefreshModMenuFor(mBAirFilter, iBAirFilter, VehicleMod.SideSkirt)
        RefreshModMenuFor(mBTank, iBTank, VehicleMod.Tank)
        RefreshMainMenu()
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            _menuPool.ProcessMenus()
            If Not Bennys.veh = Nothing Then vehicleStats = GetVehicleStats(Bennys.veh)
            _menuPool.UpdateStats(vehicleStats.TopSpeed, vehicleStats.Acceleration, vehicleStats.Braking, vehicleStats.Traction)

            If mUpgradeAW.Visible Then
                Dim spr As New Sprite("aw_upg_vehs", arenaVehImage, mUpgradeAW.GetUIMenuOffset, New Size(431, 216), 0F, Color.White) : spr.Draw()
            End If

            If _menuPool.IsAnyMenuOpen Then
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                If BtnZoom.Text = "NULL" Then BtnZoom.Text = Game.GetGXTEntry("INPUT_CREATOR_ZOOM_IN_DISPLAYONLY")
                If BtnZoomOut.Text = "NULL" Then BtnZoom.Text = Game.GetGXTEntry("INPUT_CREATOR_ZOOM_OUT_DISPLAYONLY")
                If BtnFirstPerson.Text = "NULL" Then BtnFirstPerson.Text = Game.GetGXTEntry("MO_ZOOM_FIRST")
            End If

            If Bennys.isCutscene Then
                SuspendKeys()
                Native.Function.Call(Hash.HIDE_HUD_AND_RADAR_THIS_FRAME)
                Dim sr = Size.Round(UIMenu.GetScreenResolutionMaintainRatio)
                Dim sz = UIMenu.GetSafezoneBounds
                Dim vname As String = $"{Bennys.veh.Brand} {Bennys.veh.FriendlyName}"
                Dim vclass As String = GetClassDisplayName(Bennys.veh.ClassType)

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
