Imports System
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports GTA

Public Class ModPreviewInfo

    Private _cameraposition As CameraPosition
    Public Property CameraPosition As CameraPosition
        Get
            Return _cameraposition
        End Get
        Set(ByVal value As CameraPosition)
            _cameraposition = value
        End Set
    End Property

    Private _cost As Integer
    Public Property Cost As Integer
        Get
            Return _cost
        End Get
        Set(ByVal value As Integer)
            _cost = value
        End Set
    End Property

    Private _name As String
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _openParts As List(Of VehicleDoor)
    Public Property OpenParts As List(Of VehicleDoor)
        Get
            Return _openParts
        End Get
        Set(ByVal value As List(Of VehicleDoor))
            _openParts = value
        End Set
    End Property

    Public Sub New(ByVal name As String)
        Me.New(name, New List(Of VehicleDoor), CameraPosition.Car)
    End Sub

    Public Sub New(ByVal name As String, ByVal pos As CameraPosition)
        Me.New(name, New List(Of VehicleDoor), pos)
    End Sub

    Public Sub New(ByVal name As String, ByVal doors As List(Of VehicleDoor))
        Me.New(name, doors, CameraPosition.Car)
    End Sub

    Public Sub New(ByVal name As String, ByVal doors As List(Of VehicleDoor), ByVal pos As CameraPosition)
        Me.Name = name
        Me.OpenParts = doors
        Me.CameraPosition = pos
    End Sub

End Class
