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

    Private _dashboardcol As Integer
    Public Property DashboardColor() As Integer
        Get
            Return _dashboardcol
        End Get
        Set(value As Integer)
            _dashboardcol = value
        End Set
    End Property

    Private _trimcol As Integer
    Public Property TrimColor() As Integer
        Get
            Return _trimcol
        End Get
        Set(value As Integer)
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
End Class
