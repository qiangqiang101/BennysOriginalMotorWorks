

Public Class CameraClamp

    Private _leftHorizontalValue As Single
    Public Property LeftHorizontalValue() As Single
        Get
            Return _leftHorizontalValue
        End Get
        Set(value As Single)
            _leftHorizontalValue = value
        End Set
    End Property

    Private _maxVerticalValue As Single
    Public Property MaxVerticalValue() As Single
        Get
            Return _maxVerticalValue
        End Get
        Set(value As Single)
            _maxVerticalValue = value
        End Set
    End Property

    Private _minVerticalValue As Single
    Public Property MinVerticalValue() As Single
        Get
            Return _minVerticalValue
        End Get
        Set(value As Single)
            _minVerticalValue = value
        End Set
    End Property

    Private _rightHorizontalValue As Single
    Public Property RightHorizontalValue() As Single
        Get
            Return _rightHorizontalValue
        End Get
        Set(value As Single)
            _rightHorizontalValue = value
        End Set
    End Property
End Class
