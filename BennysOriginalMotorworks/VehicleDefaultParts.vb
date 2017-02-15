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

End Class
