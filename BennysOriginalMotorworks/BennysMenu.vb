Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math

Public Class BennysMenu
    Inherits Script

    Public Shared lowriders As List(Of Model) = New List(Of Model) From {"banshee", "Buccaneer", "chino", "diabolus", "comet2", "faction", "faction2", "fcr", "italigtb", "minivan", "moonbeam", "nero", "primo", "sabregt", "slamvan", "specter", "sultan", "tornado", "tornado2", "tornado3", "virgo3", "voodoo2"}
    Public Shared MainMenu, gmBodywork, gmEngine, gmInterior, gmPlate, gmLights, gmRespray, gmWheels, gmBumper As UIMenu
    Public Shared mAerials, mSuspension, mArmor, mBrakes, mEngine, mTransmission, mFBumper, mRBumper, mSSkirt As UIMenu
    Public Shared iRepair, iHorn, iArmor, iBrakes, iFBumper, iExhaust, iFender, iRollcage, iRoof, iTransmission, iEngine, iPlate, iLights, iTint, iTurbo, iRespray, iWheels, iSuspension, iEngineBlock As UIMenuItem
    Public Shared iAerials, iAirFilter, iArchCover, iDoor, iFrame, iGrille, iHood, iHydraulics, iLivery, iPlaques, iRFender, iSpeaker, iSpoilers, iTank, iTrunk, iWindows, iTrim, iUpgrade, iStruts, iTrimColor As UIMenuItem
    Public Shared iColumnShifterLevers, iDashboard, iDialDesign, iOrnaments, iSeats, iSteeringWheels, iTrimDesign, iRBumper, iSideSkirt, iRimColor, iPlateHolder, iVanityPlates, iHeadlights, iDashboardColor As UIMenuItem
    Public Shared giBodywork, giEngine, giInterior, giPlate, giLights, giRespray, giWheels, giBumper, giWheelType, giTires, giNeonKits, giPrimaryCol, giSecondaryCol, giAccentCol As UIMenuItem
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
                RefreshMainMenu()
                RefreshBodyworkMenu()
                RefreshEngineMenu()
                RefreshInteriorMenu()
                RefreshBumperMenu()
                RefreshModMenuFor(mFBumper, iFBumper, VehicleMod.FrontBumper)
                RefreshModMenuFor(mRBumper, iRBumper, VehicleMod.RearBumper)
                RefreshModMenuFor(mSSkirt, iSideSkirt, VehicleMod.SideSkirt)
                RefreshWheelsMenu()
                RefreshPlateMenu()
                RefreshLightsMenu()
                RefreshResprayMenu()
                RefreshAerialsMenu()
                RefreshPerformanceMenuFor(mSuspension, iSuspension, VehicleMod.Suspension, "CMOD_SUS_")
                RefreshPerformanceMenuFor(mArmor, iArmor, VehicleMod.Armor, "CMOD_ARM_")
                RefreshPerformanceMenuFor(mBrakes, iBrakes, VehicleMod.Brakes, "CMOD_BRA_")
                RefreshPerformanceMenuFor(mEngine, iEngine, VehicleMod.Engine, "CMOD_ENG_")
                RefreshPerformanceMenuFor(mTransmission, iTransmission, VehicleMod.Transmission, "CMOD_GBX_")
            ElseIf selectedItem Is iUpgrade Then
                Game.FadeScreenOut(500)
                Wait(500)
                Dim veh As Vehicle = World.CreateVehicle(Helper.LowriderUpgrade(Bennys.veh.Model), Bennys.veh.Position, Bennys.veh.Heading)
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
                RefreshMainMenu()
                RefreshBodyworkMenu()
                RefreshEngineMenu()
                RefreshInteriorMenu()
                RefreshBumperMenu()
                RefreshModMenuFor(mFBumper, iFBumper, VehicleMod.FrontBumper)
                RefreshModMenuFor(mRBumper, iRBumper, VehicleMod.RearBumper)
                RefreshModMenuFor(mSSkirt, iSideSkirt, VehicleMod.SideSkirt)
                RefreshWheelsMenu()
                RefreshPlateMenu()
                RefreshLightsMenu()
                RefreshResprayMenu()
                RefreshAerialsMenu()
                RefreshPerformanceMenuFor(mSuspension, iSuspension, VehicleMod.Suspension, "CMOD_SUS_")
                RefreshPerformanceMenuFor(mArmor, iArmor, VehicleMod.Armor, "CMOD_ARM_")
                RefreshPerformanceMenuFor(mBrakes, iBrakes, VehicleMod.Brakes, "CMOD_BRA_")
                RefreshPerformanceMenuFor(mEngine, iEngine, VehicleMod.Engine, "CMOD_ENG_")
                RefreshPerformanceMenuFor(mTransmission, iTransmission, VehicleMod.Transmission, "CMOD_GBX_")
                Wait(500)
                Game.FadeScreenIn(500)
                Helper.ScreenEffectStart(Helper.ScreenEffect.RaceTurbo, 1000)
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
            End If
            If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                iTrim = New UIMenuItem("Trim")
                gmBodywork.AddItem(iTrim)
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
            End If
            If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                iAirFilter = New UIMenuItem("Air Filter")
                gmEngine.AddItem(iAirFilter)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                iStruts = New UIMenuItem("Struts")
                gmEngine.AddItem(iStruts)
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
            End If
            If Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Then
                iDashboard = New UIMenuItem("Dashboard")
                gmInterior.AddItem(iDashboard)
                iDashboardColor = New UIMenuItem("Dashboard Color")
                gmInterior.AddItem(idashboardColor)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Then
                iDialDesign = New UIMenuItem("Dial Design")
                gmInterior.AddItem(iDialDesign)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Then
                iOrnaments = New UIMenuItem("Ornaments")
                gmInterior.AddItem(iOrnaments)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Then
                iSeats = New UIMenuItem("Seats")
                gmInterior.AddItem(iSeats)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Then
                iSteeringWheels = New UIMenuItem("Steering Wheel")
                gmInterior.AddItem(iSteeringWheels)
            End If
            If Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Then
                iTrimDesign = New UIMenuItem("Trim Design")
                gmInterior.AddItem(iTrimDesign)
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
            iRimColor = New UIMenuItem("Wheel Color")
            gmWheels.AddItem(iRimColor)
            giTires = New UIMenuItem("Tires")
            gmWheels.AddItem(giTires)
            gmWheels.RefreshIndex()
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
            End If
            If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                iVanityPlates = New UIMenuItem("Vanity Plates")
                gmPlate.AddItem(iVanityPlates)
            End If
            iPlate = New UIMenuItem("Number Plate")
            gmPlate.AddItem(iPlate)
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

    Public Shared Sub CreateAerialsMenu()
        Try
            mAerials = New UIMenu("", "AERIALS")
            mAerials.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            _menuPool.Add(mAerials)
            mAerials.AddItem(New UIMenuItem("Nothing"))
            mAerials.RefreshIndex()
            AddHandler mAerials.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mAerials.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mAerials.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshAerialsMenu()
        Try
            mAerials.MenuItems.Clear()

            For i As Integer = -1 To Bennys.veh.GetModCount(VehicleMod.Aerials) - 1
                iAerials = New UIMenuItem(Game.GetGXTEntry("CMM_MOD_S18" & i + 1))
                With iAerials
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(VehicleMod.Aerials) = i Then iAerials.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                mAerials.AddItem(iAerials)
            Next
            mAerials.RefreshIndex()
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

            'Super Mods
            Bennys.veh.SetMod(VehicleMod.Aerials, Bennys.lastVehMemory.Aerials, False)
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
            End If

                'Super Mods
                If sender Is mAerials Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Aerials, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Aerials = selectedItem.SubInteger1
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
            ElseIf sender Is mrbumper Then
                Bennys.veh.SetMod(VehicleMod.RearBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is msskirt Then
                Bennys.veh.SetMod(VehicleMod.SideSkirt, sender.MenuItems(index).SubInteger1, False)
            End If

            'Super Mod
            If sender Is mAerials Then
                Bennys.veh.SetMod(VehicleMod.Aerials, sender.MenuItems(index).SubInteger1, False)
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub New()
        _menuPool = New MenuPool()
        CreateMainMenu()
        CreateBodyworkMenu()
        CreateEngineMenu()
        CreateInteriorMenu()
        CreateBumperMenu()
        CreateModMenuFor(mFBumper, "FRONT BUMPER")
        CreateModMenuFor(mRBumper, "REAR BUMPER")
        CreateModMenuFor(mSSkirt, "SIDE SKIRT")
        CreateWheelsMenu()
        CreatePlateMenu()
        CreateLightsMenu()
        CreateResprayMenu()
        CreatePerformanceMenuFor(mSuspension, Helper.LocalizedModTypeName(VehicleMod.Suspension))
        CreatePerformanceMenuFor(mArmor, Helper.LocalizedModTypeName(VehicleMod.Armor))
        CreatePerformanceMenuFor(mBrakes, Helper.LocalizedModTypeName(VehicleMod.Brakes))
        CreatePerformanceMenuFor(mEngine, Helper.LocalizedModTypeName(VehicleMod.Engine))
        CreatePerformanceMenuFor(mTransmission, Helper.LocalizedModTypeName(VehicleMod.Transmission))
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
