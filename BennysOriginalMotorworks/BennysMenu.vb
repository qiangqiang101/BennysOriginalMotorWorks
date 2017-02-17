Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math

Public Class BennysMenu
    Inherits Script

    Public Shared lowriders As List(Of Model) = New List(Of Model) From {"banshee", "Buccaneer", "chino", "diabolus", "comet2", "faction", "faction2", "fcr", "italigtb", "minivan", "moonbeam", "nero", "primo", "sabregt",
        "slamvan", "specter", "sultan", "tornado", "tornado2", "tornado3", "virgo3", "voodoo2", "elegy2"}
    Public Shared MainMenu, gmBodywork, gmEngine, gmInterior, gmPlate, gmLights, gmRespray, gmWheels, gmBumper, gmWheelType As UIMenu
    Public Shared mAerials, mSuspension, mArmor, mBrakes, mEngine, mTransmission, mFBumper, mRBumper, mSSkirt, mTrim, mEngineBlock, mAirFilter, mStruts, mColumnShifterLevers, mDashboard, mDialDesign, mOrnaments, mSeats,
        mSteeringWheels, mTrimDesign, mPlateHolder, mVanityPlates, mNumberPlate As UIMenu
    Public Shared iRepair, iHorn, iArmor, iBrakes, iFBumper, iExhaust, iFender, iRollcage, iRoof, iTransmission, iEngine, iPlate, iLights, iTint, iTurbo, iRespray, iWheels, iSuspension, iEngineBlock, iAerials, iAirFilter,
        iArchCover, iDoor, iFrame, iGrille, iHood, iHydraulics, iLivery, iPlaques, iRFender, iSpeaker, iSpoilers, iTank, iTrunk, iWindows, iTrim, iUpgrade, iStruts, iTrimColor, iColumnShifterLevers, iDashboard, iDialDesign,
        iOrnaments, iSeats, iSteeringWheels, iTrimDesign, iRBumper, iSideSkirt, iRimColor, iPlateHolder, iVanityPlates, iHeadlights, iDashboardColor, iNumberPlate As UIMenuItem
    Public Shared giBodywork, giEngine, giInterior, giPlate, giLights, giRespray, giWheels, giBumper, giWheelType, giTires, giNeonKits, giPrimaryCol, giSecondaryCol, giAccentCol, giBikeWheels, giHighEndWheels,
        giLowriderWheels, giMuscleWheels, giOffroadWheels, giSportWheels, giSUVWheels, giTunerWheels, giBennysWheels, giBespokeWheels As UIMenuItem
    Public Shared _menuPool As MenuPool

    Public Shared Sub CreateMainMenu()
        Try
            MainMenu = New UIMenu("", "MAIN MENU")
            MainMenu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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
                iRepair = New UIMenuItem("Repair")
                MainMenu.AddItem(iRepair)
            Else
                'Specials
                If lowriders.Contains(Bennys.veh.Model) Then
                    iUpgrade = New UIMenuItem("Upgrade")
                    MainMenu.AddItem(iUpgrade)
                End If

                'Groups
                If (Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Trim) <> 0) Then
                    giBodywork = New UIMenuItem("Bodywork")
                    MainMenu.AddItem(giBodywork)
                    MainMenu.BindMenuToItem(gmBodywork, giBodywork)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Or Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Struts) <> 0) Then
                    giEngine = New UIMenuItem("Engine")
                    MainMenu.AddItem(giEngine)
                    MainMenu.BindMenuToItem(gmEngine, giEngine)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Or Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Or Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0) Then
                    giInterior = New UIMenuItem("Interior")
                    MainMenu.AddItem(giInterior)
                    MainMenu.BindMenuToItem(gmInterior, giInterior)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0) Then
                    giBumper = New UIMenuItem("Bumper")
                    MainMenu.AddItem(giBumper)
                    MainMenu.BindMenuToItem(gmBumper, giBumper)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Or Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0) Then
                    giPlate = New UIMenuItem("Number Plate")
                    MainMenu.AddItem(giPlate)
                    MainMenu.BindMenuToItem(gmPlate, giPlate)
                End If
                giWheels = New UIMenuItem("Wheels")
                MainMenu.AddItem(giWheels)
                MainMenu.BindMenuToItem(gmWheels, giWheels)
                giLights = New UIMenuItem("Lights")
                MainMenu.AddItem(giLights)
                MainMenu.BindMenuToItem(gmLights, giLights)
                giRespray = New UIMenuItem("Respray")
                MainMenu.AddItem(giRespray)
                MainMenu.BindMenuToItem(gmRespray, giRespray)

                'Single Item
                If Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Then
                    iArchCover = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.ArchCover)) 'Arch Covers
                    MainMenu.AddItem(iArchCover)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Armor) <> 0 Then
                    iArmor = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Armor))
                    MainMenu.AddItem(iArmor)
                    MainMenu.BindMenuToItem(mArmor, iArmor)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Brakes) <> 0 Then
                    iBrakes = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Brakes))
                    MainMenu.AddItem(iBrakes)
                    MainMenu.BindMenuToItem(mBrakes, iBrakes)
                End If
                If Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 Then
                    iDoor = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.DoorSpeakers))
                    MainMenu.AddItem(iDoor)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Exhaust) <> 0 Then
                    iExhaust = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Exhaust))
                    MainMenu.AddItem(iExhaust)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Fender) <> 0 Then
                    iFender = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Fender))
                    MainMenu.AddItem(iFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                    iFrame = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Frame))
                    MainMenu.AddItem(iFrame)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Grille) <> 0 Then
                    iGrille = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Grille))
                    MainMenu.AddItem(iGrille)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hood) <> 0 Then
                    iHood = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Hood))
                    MainMenu.AddItem(iHood)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Horns) <> 0 Then
                    iHorn = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Horns))
                    MainMenu.AddItem(iHorn)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hydraulics) <> 0 Then
                    iHydraulics = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Hydraulics))
                    MainMenu.AddItem(iHydraulics)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Livery) <> 0 Then
                    iLivery = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Livery))
                    MainMenu.AddItem(iLivery)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                    iPlaques = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Plaques))
                    MainMenu.AddItem(iPlaques)
                End If
                If Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Then
                    iRFender = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.RightFender))
                    MainMenu.AddItem(iRFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Then
                    iRoof = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Roof))
                    MainMenu.AddItem(iRoof)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0 Then
                    iSpeaker = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Speakers))
                    MainMenu.AddItem(iSpeaker)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Spoilers) <> 0 Then
                    iSpoilers = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Spoilers))
                    MainMenu.AddItem(iSpoilers)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Suspension) <> 0 Then
                    iSuspension = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Suspension))
                    MainMenu.AddItem(iSuspension)
                    MainMenu.BindMenuToItem(mSuspension, iSuspension)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                    iTank = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Tank))
                    MainMenu.AddItem(iTank)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Transmission) <> 0 Then
                    iTransmission = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Transmission))
                    MainMenu.AddItem(iTransmission)
                    MainMenu.BindMenuToItem(mTransmission, iTransmission)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trunk) <> 0 Then
                    iTrunk = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Trunk))
                    MainMenu.AddItem(iTrunk)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Windows) <> 0 Then
                    iWindows = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Windows))
                    MainMenu.AddItem(iWindows)
                End If
                iTurbo = New UIMenuItem(Helper.LocalizedModTypeName(VehicleToggleMod.Turbo))
                MainMenu.AddItem(iTurbo)
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
            Bennys.veh.Position = New Vector3(-205.8678, -1321.805, 30.41191)
            Bennys.ply.Task.DriveTo(Bennys.veh, New Vector3(-205.743, -1303.657, 30.84998), 0.5, 5)
            Wait(500)
            Game.FadeScreenIn(500)
            Wait(10000)
            Bennys.ply.Task.ClearAll()
            Bennys.isExiting = False
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If selectedItem Is iRepair Then
                Bennys.veh.Repair()
                RefreshMenus()
            ElseIf selectedItem Is iUpgrade Then
                Game.FadeScreenOut(500)
                Wait(500)
                Dim veh As Vehicle = World.CreateVehicle(Helper.LowriderUpgrade(Bennys.veh.Model), Bennys.veh.Position, Bennys.veh.Heading)
                veh.IsPersistent = False
                veh.PrimaryColor = Bennys.veh.PrimaryColor
                veh.SecondaryColor = Bennys.veh.SecondaryColor
                veh.DashboardColor = Bennys.veh.DashboardColor
                veh.PearlescentColor = Bennys.veh.PearlescentColor
                veh.TrimColor = Bennys.veh.TrimColor
                veh.RimColor = Bennys.veh.RimColor
                veh.NumberPlate = Bennys.veh.NumberPlate
                Bennys.veh.Delete()
                Bennys.ply.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                veh.InstallModKit()
                MainMenu.MenuItems.Remove(selectedItem)
                RefreshMenus()
                Wait(500)
                Game.FadeScreenIn(500)
                Helper.ScreenEffectStart(Helper.ScreenEffect.RaceTurbo, 1000)
            ElseIf selectedItem Is giEngine Then
                'Open Doors
                Bennys.veh.OpenDoor(VehicleDoor.Hood, False, False)
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBodyworkMenu()
        Try
            gmBodywork = New UIMenu("", "BODYWORK")
            gmBodywork.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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
                iAerials = New UIMenuItem("Aerials")
                gmBodywork.AddItem(iAerials)
                gmBodywork.BindMenuToItem(mAerials, iAerials)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                iTrim = New UIMenuItem("Trim")
                gmBodywork.AddItem(iTrim)
                gmBodywork.BindMenuToItem(mTrim, iTrim)
            End If
            gmBodywork.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateEngineMenu()
        Try
            gmEngine = New UIMenu("", "ENGINE")
            gmEngine.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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
                iEngine = New UIMenuItem("EMS Engine Upgrade")
                gmEngine.AddItem(iEngine)
                gmEngine.BindMenuToItem(mEngine, iEngine)
            End If
            If Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Then
                iEngineBlock = New UIMenuItem("Engine Block")
                gmEngine.AddItem(iEngineBlock)
                gmEngine.BindMenuToItem(mEngineBlock, iEngineBlock)
            End If
            If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                iAirFilter = New UIMenuItem("Air Filter")
                gmEngine.AddItem(iAirFilter)
                gmEngine.BindMenuToItem(mAirFilter, iAirFilter)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                iStruts = New UIMenuItem("Struts")
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
            gmInterior = New UIMenu("", "INTERIOR")
            gmInterior.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            _menuPool.Add(gmInterior)
            gmInterior.AddItem(New UIMenuItem("Nothing"))
            gmInterior.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshInteriorMenu()
        Try
            gmInterior.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Then
                iColumnShifterLevers = New UIMenuItem("Column Shifter Levers")
                gmInterior.AddItem(iColumnShifterLevers)
                gmInterior.BindMenuToItem(mColumnShifterLevers, iColumnShifterLevers)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Then
                iDashboard = New UIMenuItem("Dashboard")
                gmInterior.AddItem(iDashboard)
                gmInterior.BindMenuToItem(mDashboard, iDashboard)
                iDashboardColor = New UIMenuItem("Dashboard Color")
                gmInterior.AddItem(idashboardColor)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Then
                iDialDesign = New UIMenuItem("Dial Design")
                gmInterior.AddItem(iDialDesign)
                gmInterior.BindMenuToItem(mDialDesign, iDialDesign)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Then
                iOrnaments = New UIMenuItem("Ornaments")
                gmInterior.AddItem(iOrnaments)
                gmInterior.BindMenuToItem(mOrnaments, iOrnaments)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Then
                iSeats = New UIMenuItem("Seats")
                gmInterior.AddItem(iSeats)
                gmInterior.BindMenuToItem(mSeats, iSeats)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Then
                iSteeringWheels = New UIMenuItem("Steering Wheel")
                gmInterior.AddItem(iSteeringWheels)
                gmInterior.BindMenuToItem(mSteeringWheels, iSteeringWheels)
            End If
            If Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Then
                iTrimDesign = New UIMenuItem("Trim Design")
                gmInterior.AddItem(iTrimDesign)
                gmInterior.BindMenuToItem(mTrimDesign, iTrimDesign)
                iTrimColor = New UIMenuItem("Trim Color")
                gmInterior.AddItem(iTrimColor)
            End If
            gmInterior.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBumperMenu()
        Try
            gmBumper = New UIMenu("", "BUMPER")
            gmBumper.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            _menuPool.Add(gmBumper)
            gmBumper.AddItem(New UIMenuItem("Nothing"))
            gmBumper.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBumperMenu()
        Try
            gmBumper.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Then
                iFBumper = New UIMenuItem("Front Bumper")
                gmBumper.AddItem(iFBumper)
                gmBumper.BindMenuToItem(mFBumper, iFBumper)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0 Then
                iSideSkirt = New UIMenuItem("Side Skirt")
                gmBumper.AddItem(iSideSkirt)
                gmBumper.BindMenuToItem(mSSkirt, iSideSkirt)
            End If
            If Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Then
                iRBumper = New UIMenuItem("Rear Bumper")
                gmBumper.AddItem(iRBumper)
                gmBumper.BindMenuToItem(mRBumper, iRBumper)
            End If
            gmBumper.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelsMenu()
        Try
            gmWheels = New UIMenu("", "WHEELS")
            gmWheels.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            _menuPool.Add(gmWheels)
            gmWheels.AddItem(New UIMenuItem("Nothing"))
            gmWheels.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelsMenu()
        Try
            gmWheels.MenuItems.Clear()
            giWheelType = New UIMenuItem("Wheel Type")
            gmWheels.AddItem(giWheelType)
            gmWheels.BindMenuToItem(gmWheelType, giWheelType)
            iRimColor = New UIMenuItem("Wheel Color")
            gmWheels.AddItem(iRimColor)
            giTires = New UIMenuItem("Tires")
            gmWheels.AddItem(giTires)
            gmWheels.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelTypeMenu()
        Try
            gmWheelType = New UIMenu("", "WHEEL TYPE")
            gmWheelType.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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
                Case Else
                    giHighEndWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd))
                    gmWheelType.AddItem(giHighEndWheels)
                    giLowriderWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider))
                    gmWheelType.AddItem(giLowriderWheels)
                    giMuscleWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle))
                    gmWheelType.AddItem(giMuscleWheels)
                    giOffroadWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad))
                    gmWheelType.AddItem(giOffroadWheels)
                    giSportWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport))
                    gmWheelType.AddItem(giSportWheels)
                    giSUVWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV))
                    gmWheelType.AddItem(giSUVWheels)
                    giTunerWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner))
                    gmWheelType.AddItem(giTunerWheels)
                    giBennysWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(8)) 'Benny's Original
                    gmWheelType.AddItem(giBennysWheels)
                    giBespokeWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(9)) 'Benny's Bespoke
                    gmWheelType.AddItem(giBespokeWheels)
            End Select

            gmWheelType.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateMenu()
        Try
            gmPlate = New UIMenu("", "NUMBER PLATE")
            gmPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            _menuPool.Add(gmPlate)
            gmPlate.AddItem(New UIMenuItem("Nothing"))
            gmPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPlateMenu()
        Try
            gmPlate.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Then
                iPlateHolder = New UIMenuItem("Plate Holder")
                gmPlate.AddItem(iPlateHolder)
                gmPlate.BindMenuToItem(mPlateHolder, iPlateHolder)
            End If
            If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                iVanityPlates = New UIMenuItem("Vanity Plates")
                gmPlate.AddItem(iVanityPlates)
                gmPlate.BindMenuToItem(mVanityPlates, iVanityPlates)
            End If
            iPlate = New UIMenuItem("Number Plate")
            gmPlate.AddItem(iPlate)
            gmPlate.BindMenuToItem(mNumberPlate, iNumberPlate)
            gmPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateLightsMenu()
        Try
            gmLights = New UIMenu("", "LIGHTS")
            gmLights.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            _menuPool.Add(gmLights)
            gmLights.AddItem(New UIMenuItem("Nothing"))
            gmLights.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshLightsMenu()
        Try
            gmLights.MenuItems.Clear()
            iHeadlights = New UIMenuItem("Headlights")
            gmLights.AddItem(iHeadlights)
            giNeonKits = New UIMenuItem("Neon Kits")
            gmLights.AddItem(giNeonKits)
            gmLights.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateResprayMenu()
        Try
            gmRespray = New UIMenu("", "RESPRAY")
            gmRespray.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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
            giPrimaryCol = New UIMenuItem("Primary Color")
            gmRespray.AddItem(giPrimaryCol)
            giSecondaryCol = New UIMenuItem("Secondary Color")
            gmRespray.AddItem(giSecondaryCol)
            giAccentCol = New UIMenuItem("Accent Color")
            gmRespray.AddItem(giAccentCol)
            gmRespray.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePerformanceMenuFor(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title.ToUpper)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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
            menu = New UIMenu("", title.ToUpper)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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

            'Super Mods
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
            Bennys.veh.SetMod(VehicleMod.PlateHolder, Bennys.lastVehMemory.TrimDesign, False)
            Bennys.veh.SetMod(VehicleMod.VanityPlates, Bennys.lastVehMemory.TrimDesign, False)

            'Color
            Bennys.veh.DashboardColor = Bennys.lastVehMemory.DashboardColor
            Bennys.veh.TrimColor = Bennys.lastVehMemory.TrimColor

            'Close Doors
            If sender Is gmEngine Then Bennys.veh.CloseDoor(VehicleDoor.Hood, False)
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
            End If

            'Super Mods
            If sender Is mAerials Then
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
            End If

            'Wheel Type
            If sender Is gmWheelType Then
                If selectedItem Is giBikeWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.BikeWheels
                ElseIf selectedItem Is giHighEndWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.HighEnd
                ElseIf selectedItem Is giLowriderWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Lowrider
                ElseIf selectedItem Is giMuscleWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Muscle
                ElseIf selectedItem Is giOffroadWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Offroad
                ElseIf selectedItem Is giSportWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Sport
                ElseIf selectedItem Is giSUVWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.SUV
                ElseIf selectedItem Is giTunerWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Tuner
                ElseIf selectedItem Is giBennysWheels Then
                    Bennys.veh.WheelType = 8
                ElseIf selectedItem Is giBennysWheels Then
                    Bennys.veh.WheelType = 9
                End If
            End If

            'Color
            'If sender Is mDashboardColor Then
            '    If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
            '        Bennys.veh.DashboardColor = selectedItem.SubInteger1
            '        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            '        Bennys.lastVehMemory.DashboardColor = selectedItem.SubInteger1
            '    End If
            'ElseIf sender Is mTrimColor Then
            '    If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
            '        Bennys.veh.TrimColor = selectedItem.SubInteger1
            '        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            '        Bennys.lastVehMemory.TrimColor = selectedItem.SubInteger1
            '    End If
            'End If
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
            ElseIf sender Is mrbumper Then
                Bennys.veh.SetMod(VehicleMod.RearBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is msskirt Then
                Bennys.veh.SetMod(VehicleMod.SideSkirt, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mNumberPlate Then
                Bennys.veh.NumberPlateType = sender.MenuItems(index).SubInteger1
            End If

            'Super Mod
            If sender Is mAerials Then
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
            End If

            'Color
            'If sender Is mDashboardColor Then
            '    Bennys.veh.DashboardColor = sender.MenuItems(index).SubInteger1
            'ElseIf sender Is mTrimColor Then
            '    Bennys.veh.TrimColor = sender.MenuItems(index).SubInteger1
            'End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateNumberMenu()
        Try
            mNumberPlate = New UIMenu("", "NUMBER PLATE")
            mNumberPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
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

    Public Shared Sub RefreshPlateNumberMenu()
        Try
            mNumberPlate.MenuItems.Clear()

            Dim plateType As Array = System.Enum.GetValues(GetType(NumberPlateType))
            For Each plate As NumberPlateType In plateType
                iNumberPlate = New UIMenuItem([Enum].GetName(GetType(NumberPlateType), plate))
                With iNumberPlate
                    .SubInteger1 = plate
                    If Bennys.veh.NumberPlateType = plate Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                mNumberPlate.AddItem(iNumberPlate)
            Next
            mNumberPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub New()
        _menuPool = New MenuPool()
        CreateMainMenu()
        CreateBodyworkMenu()
        CreateEngineMenu()
        CreatePerformanceMenuFor(mEngine, "ENGINE")
        CreateModMenuFor(mEngineBlock, "ENGINE BLOCK")
        CreateModMenuFor(mAirFilter, "AIR FILTER")
        CreateModMenuFor(mStruts, "STRUTS")
        CreateInteriorMenu()
        CreateModMenuFor(mColumnShifterLevers, "COLUMN SHIFTER LEVERS")
        CreateModMenuFor(mDashboard, "DASHBOARD")
        CreateModMenuFor(mDialDesign, "DIAL DESIGN")
        CreateModMenuFor(mOrnaments, "ORNAMENTS")
        CreateModMenuFor(mSeats, "SEATS")
        CreateModMenuFor(mSteeringWheels, "STEERING WHEEL")
        CreateModMenuFor(mTrimDesign, "TRIM DESIGN")
        CreateBumperMenu()
        CreateModMenuFor(mFBumper, "FRONT BUMPER")
        CreateModMenuFor(mRBumper, "REAR BUMPER")
        CreateModMenuFor(mSSkirt, "SIDE SKIRT")
        CreateModMenuFor(mAerials, "AERIALS")
        CreateModMenuFor(mTrim, "TRIM")
        CreateWheelsMenu()
        CreateWheelTypeMenu()
        CreatePlateMenu()
        CreateModMenuFor(mPlateHolder, "PLATE HOLDER")
        CreateModMenuFor(mVanityPlates, "VANITY PLATE")
        CreatePlateNumberMenu()
        CreateLightsMenu()
        CreateResprayMenu()
        CreatePerformanceMenuFor(mSuspension, "SUSPENSION")
        CreatePerformanceMenuFor(mArmor, "ARMOR")
        CreatePerformanceMenuFor(mBrakes, "BRAKES")
        CreatePerformanceMenuFor(mTransmission, "TRANSMISSION")
    End Sub

    Public Shared Sub RefreshMenus()
        RefreshMainMenu()
        RefreshBodyworkMenu()
        RefreshModMenuFor(mAerials, iAerials, VehicleMod.Aerials)
        RefreshModMenuFor(mTrim, iTrim, VehicleMod.Trim)
        RefreshEngineMenu()
        RefreshPerformanceMenuFor(mEngine, iEngine, VehicleMod.Engine, "CMOD_ENG_")
        RefreshModMenuFor(mEngineBlock, iEngineBlock, VehicleMod.EngineBlock)
        RefreshModMenuFor(mAirFilter, iAirFilter, VehicleMod.AirFilter)
        RefreshModMenuFor(mStruts, iStruts, VehicleMod.Struts)
        RefreshInteriorMenu()
        RefreshModMenuFor(mColumnShifterLevers, iColumnShifterLevers, VehicleMod.ColumnShifterLevers)
        RefreshModMenuFor(mDashboard, iDashboard, VehicleMod.Dashboard)
        RefreshModMenuFor(mDialDesign, iDialDesign, VehicleMod.DialDesign)
        RefreshModMenuFor(mOrnaments, iOrnaments, VehicleMod.Ornaments)
        RefreshModMenuFor(mSeats, iSeats, VehicleMod.Seats)
        RefreshModMenuFor(mSteeringWheels, iSteeringWheels, VehicleMod.SteeringWheels)
        RefreshModMenuFor(mTrimDesign, iTrimDesign, VehicleMod.TrimDesign)
        RefreshBumperMenu()
        RefreshModMenuFor(mFBumper, iFBumper, VehicleMod.FrontBumper)
        RefreshModMenuFor(mRBumper, iRBumper, VehicleMod.RearBumper)
        RefreshModMenuFor(mSSkirt, iSideSkirt, VehicleMod.SideSkirt)
        RefreshWheelsMenu()
        RefreshWheelTypeMenu()
        RefreshPlateMenu()
        RefreshModMenuFor(mPlateHolder, iPlateHolder, VehicleMod.PlateHolder)
        RefreshModMenuFor(mVanityPlates, iVanityPlates, VehicleMod.VanityPlates)
        RefreshPlateNumberMenu()
        RefreshLightsMenu()
        RefreshResprayMenu()
        RefreshPerformanceMenuFor(mSuspension, iSuspension, VehicleMod.Suspension, "CMOD_SUS_")
        RefreshPerformanceMenuFor(mArmor, iArmor, VehicleMod.Armor, "CMOD_ARM_")
        RefreshPerformanceMenuFor(mBrakes, iBrakes, VehicleMod.Brakes, "CMOD_BRA_")
        RefreshPerformanceMenuFor(mTransmission, iTransmission, VehicleMod.Transmission, "CMOD_GBX_")
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            _menuPool.ProcessMenus()

            Select Case True
                Case MainMenu.Visible, gmBodywork.Visible
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
