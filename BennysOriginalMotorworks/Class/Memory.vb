Public Class Memory

    Public Sub New()
    End Sub

    Private _aerials As Integer
    Public Property Aerials() As Integer
        Get
            Return _aerials
        End Get
        Set(value As Integer)
            _aerials = value
        End Set
    End Property

    Private _suspension As Integer
    Public Property Suspension() As Integer
        Get
            Return _suspension
        End Get
        Set(value As Integer)
            _suspension = value
        End Set
    End Property

    Private _armor As Integer
    Public Property Armor() As Integer
        Get
            Return _armor
        End Get
        Set(value As Integer)
            _armor = value
        End Set
    End Property

    Private _brakes As Integer
    Public Property Brakes() As Integer
        Get
            Return _brakes
        End Get
        Set(value As Integer)
            _brakes = value
        End Set
    End Property

    Private _engine As Integer
    Public Property Engine() As Integer
        Get
            Return _engine
        End Get
        Set(value As Integer)
            _engine = value
        End Set
    End Property

    Private _transmission As Integer
    Public Property Transmission() As Integer
        Get
            Return _transmission
        End Get
        Set(value As Integer)
            _transmission = value
        End Set
    End Property

    Private _frontbumper As Integer
    Public Property FrontBumper() As Integer
        Get
            Return _frontbumper
        End Get
        Set(value As Integer)
            _frontbumper = value
        End Set
    End Property

    Private _rearbumper As Integer
    Public Property RearBumper() As Integer
        Get
            Return _rearbumper
        End Get
        Set(value As Integer)
            _rearbumper = value
        End Set
    End Property

    Private _sideskirt As Integer
    Public Property SideSkirt() As Integer
        Get
            Return _sideskirt
        End Get
        Set(value As Integer)
            _sideskirt = value
        End Set
    End Property

    Private _trim As Integer
    Public Property Trim() As Integer
        Get
            Return _trim
        End Get
        Set(value As Integer)
            _trim = value
        End Set
    End Property

    Private _engineblock As Integer
    Public Property EngineBlock() As Integer
        Get
            Return _engineblock
        End Get
        Set(value As Integer)
            _engineblock = value
        End Set
    End Property

    Private _airfilter As Integer
    Public Property AirFilter() As Integer
        Get
            Return _airfilter
        End Get
        Set(value As Integer)
            _airfilter = value
        End Set
    End Property

    Private _struts As Integer
    Public Property Struts() As Integer
        Get
            Return _struts
        End Get
        Set(value As Integer)
            _struts = value
        End Set
    End Property

    Private _columnshifterlevers As Integer
    Public Property ColumnShifterLevers() As Integer
        Get
            Return _columnshifterlevers
        End Get
        Set(value As Integer)
            _columnshifterlevers = value
        End Set
    End Property

    Private _dashboard As Integer
    Public Property Dashboard() As Integer
        Get
            Return _dashboard
        End Get
        Set(value As Integer)
            _dashboard = value
        End Set
    End Property

    Private _dialdesign As Integer
    Public Property DialDesign() As Integer
        Get
            Return _dialdesign
        End Get
        Set(value As Integer)
            _dialdesign = value
        End Set
    End Property

    Private _ornaments As Integer
    Public Property Ornaments() As Integer
        Get
            Return _ornaments
        End Get
        Set(value As Integer)
            _ornaments = value
        End Set
    End Property

    Private _seats As Integer
    Public Property Seats() As Integer
        Get
            Return _seats
        End Get
        Set(value As Integer)
            _seats = value
        End Set
    End Property

    Private _steeringwheels As Integer
    Public Property SteeringWheels() As Integer
        Get
            Return _steeringwheels
        End Get
        Set(value As Integer)
            _steeringwheels = value
        End Set
    End Property

    Private _trimdesign As Integer
    Public Property TrimDesign() As Integer
        Get
            Return _trimdesign
        End Get
        Set(value As Integer)
            _trimdesign = value
        End Set
    End Property

    Private _trimcol As GTA.VehicleColor
    Public Property TrimColor() As GTA.VehicleColor
        Get
            Return _trimcol
        End Get
        Set(value As GTA.VehicleColor)
            _trimcol = value
        End Set
    End Property

    Private _plateholder As Integer
    Public Property PlateHolder() As Integer
        Get
            Return _plateholder
        End Get
        Set(value As Integer)
            _plateholder = value
        End Set
    End Property

    Private _vanityplates As Integer
    Public Property VanityPlates() As Integer
        Get
            Return _vanityplates
        End Get
        Set(value As Integer)
            _vanityplates = value
        End Set
    End Property

    Private _numberplate As GTA.NumberPlateType
    Public Property NumberPlate() As GTA.NumberPlateType
        Get
            Return _numberplate
        End Get
        Set(value As GTA.NumberPlateType)
            _numberplate = value
        End Set
    End Property

    Private _platenumbers As String
    Public Property PlateNumbers() As String
        Get
            Return _platenumbers
        End Get
        Set(value As String)
            _platenumbers = value
        End Set
    End Property

    Private _wheeltype As GTA.VehicleWheelType
    Public Property WheelType() As GTA.VehicleWheelType
        Get
            Return _wheeltype
        End Get
        Set(value As GTA.VehicleWheelType)
            _wheeltype = value
        End Set
    End Property

    Private _frontwheels As Integer
    Public Property FrontWheels() As Integer
        Get
            Return _frontwheels
        End Get
        Set(value As Integer)
            _frontwheels = value
        End Set
    End Property

    Private _backwheels As Integer
    Public Property BackWheels() As Integer
        Get
            Return _backwheels
        End Get
        Set(value As Integer)
            _backwheels = value
        End Set
    End Property

    Private _wheelsvariation As Integer
    Public Property WheelsVariation() As Boolean
        Get
            Return _wheelsvariation
        End Get
        Set(value As Boolean)
            _wheelsvariation = value
        End Set
    End Property

    Private _headlights As Boolean
    Public Property Headlights() As Boolean
        Get
            Return _headlights
        End Get
        Set(value As Boolean)
            _headlights = value
        End Set
    End Property

    Private _frontneon As Boolean
    Public Property FrontNeon() As Boolean
        Get
            Return _frontneon
        End Get
        Set(value As Boolean)
            _frontneon = value
        End Set
    End Property

    Private _backneon As Boolean
    Public Property BackNeon() As Boolean
        Get
            Return _backneon
        End Get
        Set(value As Boolean)
            _backneon = value
        End Set
    End Property

    Private _leftneon As Boolean
    Public Property LeftNeon() As Boolean
        Get
            Return _leftneon
        End Get
        Set(value As Boolean)
            _leftneon = value
        End Set
    End Property

    Private _rightneon As Boolean
    Public Property RightNeon() As Boolean
        Get
            Return _rightneon
        End Get
        Set(value As Boolean)
            _rightneon = value
        End Set
    End Property

    Private _archcover As Integer
    Public Property ArchCover() As Integer
        Get
            Return _archcover
        End Get
        Set(value As Integer)
            _archcover = value
        End Set
    End Property

    Private _exhaust As Integer
    Public Property Exhaust() As Integer
        Get
            Return _exhaust
        End Get
        Set(value As Integer)
            _exhaust = value
        End Set
    End Property

    Private _fender As Integer
    Public Property Fender() As Integer
        Get
            Return _fender
        End Get
        Set(value As Integer)
            _fender = value
        End Set
    End Property

    Private _rightfender As Integer
    Public Property RightFender() As Integer
        Get
            Return _rightfender
        End Get
        Set(value As Integer)
            _rightfender = value
        End Set
    End Property

    Private _doorspeakers As Integer
    Public Property DoorSpeakers() As Integer
        Get
            Return _doorspeakers
        End Get
        Set(value As Integer)
            _doorspeakers = value
        End Set
    End Property

    Private _frame As Integer
    Public Property Frame() As Integer
        Get
            Return _frame
        End Get
        Set(value As Integer)
            _frame = value
        End Set
    End Property

    Private _grille As Integer
    Public Property Grille() As Integer
        Get
            Return _grille
        End Get
        Set(value As Integer)
            _grille = value
        End Set
    End Property

    Private _hood As Integer
    Public Property Hood() As Integer
        Get
            Return _hood
        End Get
        Set(value As Integer)
            _hood = value
        End Set
    End Property

    Private _horns As Integer
    Public Property Horns() As Integer
        Get
            Return _horns
        End Get
        Set(value As Integer)
            _horns = value
        End Set
    End Property

    Private _hydraulics As Integer
    Public Property Hydraulics() As Integer
        Get
            Return _hydraulics
        End Get
        Set(value As Integer)
            _hydraulics = value
        End Set
    End Property

    Private _livery As Integer
    Public Property Livery() As Integer
        Get
            Return _livery
        End Get
        Set(value As Integer)
            _livery = value
        End Set
    End Property

    Private _livery2 As Integer
    Public Property Livery2() As Integer
        Get
            Return _livery2
        End Get
        Set(value As Integer)
            _livery2 = value
        End Set
    End Property

    Private _plaques As Integer
    Public Property Plaques() As Integer
        Get
            Return _plaques
        End Get
        Set(value As Integer)
            _plaques = value
        End Set
    End Property

    Private _roof As Integer
    Public Property Roof() As Integer
        Get
            Return _roof
        End Get
        Set(value As Integer)
            _roof = value
        End Set
    End Property

    Private _speakers As Integer
    Public Property Speakers() As Integer
        Get
            Return _speakers
        End Get
        Set(value As Integer)
            _speakers = value
        End Set
    End Property

    Private _spoilers As Integer
    Public Property Spoilers() As Integer
        Get
            Return _spoilers
        End Get
        Set(value As Integer)
            _spoilers = value
        End Set
    End Property

    Private _tank As Integer
    Public Property Tank() As Integer
        Get
            Return _tank
        End Get
        Set(value As Integer)
            _tank = value
        End Set
    End Property

    Private _trunk As Integer
    Public Property Trunk() As Integer
        Get
            Return _trunk
        End Get
        Set(value As Integer)
            _trunk = value
        End Set
    End Property

    Private _windows As Integer
    Public Property Windows() As Integer
        Get
            Return _windows
        End Get
        Set(value As Integer)
            _windows = value
        End Set
    End Property

    Private _turbo As Boolean
    Public Property Turbo() As Boolean
        Get
            Return _turbo
        End Get
        Set(value As Boolean)
            _turbo = value
        End Set
    End Property

    Private _tint As GTA.VehicleWindowTint
    Public Property Tint() As GTA.VehicleWindowTint
        Get
            Return _tint
        End Get
        Set(value As GTA.VehicleWindowTint)
            _tint = value
        End Set
    End Property

    Private _primarycolor As GTA.VehicleColor
    Public Property PrimaryColor() As GTA.VehicleColor
        Get
            Return _primarycolor
        End Get
        Set(value As GTA.VehicleColor)
            _primarycolor = value
        End Set
    End Property

    Private _secondarycolor As GTA.VehicleColor
    Public Property SecondaryColor() As GTA.VehicleColor
        Get
            Return _secondarycolor
        End Get
        Set(value As GTA.VehicleColor)
            _secondarycolor = value
        End Set
    End Property

    Private _pearlescentcolor As GTA.VehicleColor
    Public Property PearlescentColor() As GTA.VehicleColor
        Get
            Return _pearlescentcolor
        End Get
        Set(value As GTA.VehicleColor)
            _pearlescentcolor = value
        End Set
    End Property

    Private _rimcolor As GTA.VehicleColor
    Public Property RimColor() As GTA.VehicleColor
        Get
            Return _rimcolor
        End Get
        Set(value As GTA.VehicleColor)
            _rimcolor = value
        End Set
    End Property

    Private _lightscolor As GTA.VehicleColor
    Public Property LightsColor() As Integer
        Get
            Return _lightscolor
        End Get
        Set(value As Integer)
            _lightscolor = value
        End Set
    End Property

    Private _neonlightscolor As Drawing.Color
    Public Property NeonLightsColor() As Drawing.Color
        Get
            Return _neonlightscolor
        End Get
        Set(value As Drawing.Color)
            _neonlightscolor = value
        End Set
    End Property

    Private _tiresmoke As Drawing.Color
    Public Property TireSmokeColor() As Drawing.Color
        Get
            Return _tiresmoke
        End Get
        Set(value As Drawing.Color)
            _tiresmoke = value
        End Set
    End Property
End Class
