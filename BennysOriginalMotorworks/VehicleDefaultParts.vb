Public Class VehicleDefaultParts

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
End Class
