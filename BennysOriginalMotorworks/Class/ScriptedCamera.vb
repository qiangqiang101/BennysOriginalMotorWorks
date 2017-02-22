Imports GTA
Imports GTA.Math
Imports GTA.Native

Public Class ScriptedCamera
    Inherits Script

    Public Shared prevPos As Vector4 = New Vector4
    Public Shared splineCamera As Camera = Nothing

    Public Sub New()
        World.DestroyAllCameras()
        RecreateCam()
    End Sub

    Public Shared Sub AddNode(ByVal transform As Vector4, ByVal time As Integer)
        AddNode(transform.Position, transform.Rotation, time)
    End Sub

    Public Shared Sub AddNode(ByVal position As Vector3, ByVal rotation As Vector3, ByVal time As Integer)
        Dim arguments As InputArgument() = New InputArgument() {splineCamera.Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, time, 3, 2}
        Native.Function.Call(Hash.ADD_CAM_SPLINE_NODE, arguments)
    End Sub

    Private Sub AnimatedCameraScript_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Tick
        If (World.RenderingCamera.Handle = -1) Then
            prevPos.Position = GameplayCamera.Position
            prevPos.Rotation = GameplayCamera.Rotation
        Else
            prevPos.Position = World.RenderingCamera.Position
            prevPos.Rotation = World.RenderingCamera.Rotation
        End If
    End Sub

    Public Shared Sub BeginNodes()
        RecreateCam()
        Dim arguments As InputArgument() = New InputArgument() {splineCamera.Handle, prevPos.Position.X, prevPos.Position.Y, prevPos.Position.Z, prevPos.Rotation.X, prevPos.Rotation.Y, prevPos.Rotation.Z, 0, 3, 2}
        Native.Function.Call(Hash.ADD_CAM_SPLINE_NODE, arguments)
    End Sub

    Public Shared Sub CameraShake(ByVal shake As CameraShake, ByVal intensity As Single)
        splineCamera.Shake(shake, intensity)
    End Sub

    Public Shared Sub PointAt(entity As Entity)
        splineCamera.PointAt(entity)
    End Sub

    Public Shared Sub StopPointAt()
        splineCamera.StopPointing()
    End Sub

    Public Shared Sub DisableCamera()
        World.RenderingCamera = Nothing
    End Sub

    Public Shared Sub EnableCamera()
        World.RenderingCamera = splineCamera
    End Sub

    Public Shared Sub RecreateCam()
        If (Not splineCamera Is Nothing) Then
            splineCamera.Destroy()
        End If
        Dim arguments As InputArgument() = New InputArgument() {"DEFAULT_SPLINE_CAMERA", 1}
        splineCamera = New Camera(Native.Function.Call(Of Integer)(Hash.CREATE_CAM, arguments))
        Dim argumentArray2 As InputArgument() = New InputArgument() {splineCamera.Handle, prevPos.Position.X, prevPos.Position.Y, prevPos.Position.Z, prevPos.Rotation.X, prevPos.Rotation.Y, prevPos.Rotation.Z, GameplayCamera.FieldOfView, 0, 2, 2, 2}
        Native.Function.Call(Hash.SET_CAM_PARAMS, argumentArray2)
        splineCamera.MotionBlurStrength = 1.0!
        splineCamera.FieldOfView = GameplayCamera.FieldOfView
        splineCamera.DepthOfFieldStrength = 100.0!
        splineCamera.NearDepthOfField = 1.0!
        splineCamera.FarDepthOfField = 0.01!
        Dim argumentArray3 As InputArgument() = New InputArgument() {splineCamera.Handle, 0!, 0!, 0!, 10.0!}
        Native.Function.Call(Hash.SET_CAM_DOF_PLANES, argumentArray3)
    End Sub

    Public Shared Sub SetPrevPosition(ByVal transform As Vector4)
        prevPos = transform.Clone
    End Sub

    Public Shared Sub TransitionToCamera(ByVal cam As Camera, ByVal time As Integer, ByVal Optional easeType As Integer = 3)
        Dim arguments As InputArgument() = New InputArgument() {splineCamera.Handle, cam.Handle, time, easeType}
        Native.Function.Call(Hash._0x0A9F2A468B328E74, arguments)
    End Sub

    Public Shared Sub TransitionToPoint(ByVal transform As Vector4, ByVal time As Integer)
        TransitionToPoint(transform.Position, transform.Rotation, time)
    End Sub

    Public Shared Sub TransitionToPoint(ByVal position As Vector3, ByVal rotation As Vector3, ByVal time As Integer)
        RecreateCam()
        Dim arguments As InputArgument() = New InputArgument() {splineCamera.Handle, prevPos.Position.X, prevPos.Position.Y, prevPos.Position.Z, prevPos.Rotation.X, prevPos.Rotation.Y, prevPos.Rotation.Z, 0, 3, 2}
        Native.Function.Call(Hash.ADD_CAM_SPLINE_NODE, arguments)
        Dim argumentArray2 As InputArgument() = New InputArgument() {splineCamera.Handle, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, time, 3, 2}
        Native.Function.Call(Hash.ADD_CAM_SPLINE_NODE, argumentArray2)
    End Sub

    Public Shared Sub TransitionToPointList(ByVal transforms As Vector4(), ByVal gaps As Integer, ByVal Optional transitionType As Integer = 0, ByVal Optional smoothStart As Boolean = False)
        RecreateCam()

        If smoothStart Then
            Dim arguments As InputArgument() = New InputArgument() {splineCamera.Handle, prevPos.Position.X, prevPos.Position.Y, prevPos.Position.Z, prevPos.Rotation.X, prevPos.Rotation.Y, prevPos.Rotation.Z, 0, 3, 5}
            Native.Function.Call(Hash.ADD_CAM_SPLINE_NODE, arguments)
        End If
        Dim i As Integer
        For i = 0 To transforms.Length - 1
            Dim argumentArray2 As InputArgument() = New InputArgument() {splineCamera.Handle, transforms(i).Position.X, transforms(i).Position.Y, transforms(i).Position.Z, transforms(i).Rotation.X, transforms(i).Rotation.Y, transforms(i).Rotation.Z, (gaps * i), 3, transitionType}
            Native.Function.Call(Hash.ADD_CAM_SPLINE_NODE, argumentArray2)
        Next i
    End Sub
End Class

Public Class Vector4

    Public Position As Vector3
    Public Rotation As Vector3

    Public Sub New()
    End Sub

    Public Sub New(ByVal pos As Vector3)
        Me.Rotation = New Vector3
        Me.Position = pos
    End Sub

    Public Sub New(ByVal pos As Vector3, ByVal rot As Vector3)
        Me.Position = pos
        Me.Rotation = rot
    End Sub

    Public Function Clone() As Vector4
        Return New Vector4 With {.Position = Me.Position, .Rotation = Me.Rotation}
    End Function
End Class
