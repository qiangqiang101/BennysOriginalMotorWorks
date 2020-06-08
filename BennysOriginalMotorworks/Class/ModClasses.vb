Imports System.Drawing

Public Class ArenaWarVehicle

    Public Model As String
    Public Image As String
    Public Price As Integer

    Public Sub New(m As String, i As String, p As Integer)
        Model = m
        Image = i
        Price = p
    End Sub

End Class

Public Class ModClass

    Public ModID As Integer
    Public Price As Integer

    Public Sub New(m As Integer, p As Integer)
        ModID = m
        Price = p
    End Sub

    Public Sub New(m As Boolean, p As Integer)
        ModID = CInt(m)
        Price = p
    End Sub

    Public Function ModIDBool() As Boolean
        Return CBool(ModID)
    End Function

End Class

Public Class ToggleModClass

    Public ModToggle As Boolean
    Public ModID As Integer
    Public Price As Integer

    Public Sub New(mt As Boolean, m As Integer, p As Integer)
        ModToggle = mt
        ModID = m
        Price = p
    End Sub

End Class

Public Class RGBModClass

    Public Alpha As Integer
    Public Red As Integer
    Public Green As Integer
    Public Blue As Integer
    Public Price As Integer

    Public Sub New(col As Color, p As Integer)
        Alpha = col.A
        Red = col.R
        Green = col.G
        Blue = col.B
        Price = p
    End Sub

    Public Sub New(a As Integer, r As Integer, g As Integer, b As Integer, p As Integer)
        Alpha = a
        Red = r
        Green = g
        Blue = b
        Price = p
    End Sub

    Public Sub New(a As Integer, col As Color, p As Integer)
        Alpha = a
        Red = col.R
        Green = col.G
        Blue = col.B
        Price = p
    End Sub

    Public Sub New(r As Integer, g As Integer, b As Integer, p As Integer)
        Alpha = 255
        Red = r
        Green = g
        Blue = b
        Price = p
    End Sub

    Public Function Color() As Color
        Return Color.FromArgb(Alpha, Red, Green, Blue)
    End Function

End Class