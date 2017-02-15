Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports System
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class CutsceneManager

    Public Shared Function BoundRotationDeg(ByVal angleDeg As Double) As Double
        Dim num As Integer = CInt((angleDeg / 360))
        Dim num2 As Double = (angleDeg - (num * 360))
        If (num2 < 0) Then
            num2 = (num2 + 360)
        End If
        Return num2
    End Function

    Public Shared Function CrossWith(ByVal left As Vector3, ByVal right As Vector3) As Vector3
        Dim vector As Vector3
        vector.X = ((left.Y * right.Z) - (left.Z * right.Y))
        vector.Y = ((left.Z * right.X) - (left.X * right.Z))
        vector.Z = ((left.X * right.Y) - (left.Y * right.X))
        Return vector
    End Function

    Public Shared Function DegToRad(ByVal deg As Double) As Double
        Return ((deg * 3.1415926535897931) / 180)
    End Function

    Public Shared Function DirectionToRotation(ByVal direction As Vector3) As Vector3
        direction.Normalize()
        Dim deg As Double = Math.Atan2(CDbl(direction.Z), CDbl(direction.Y))
        Dim num2 As Integer = 0
        Dim num3 As Double = -Math.Atan2(CDbl(direction.X), CDbl(direction.Y))
        Return New Vector3 With {
            .X = CSng(CutsceneManager.RadToDeg(deg)),
            .Y = CSng(CutsceneManager.RadToDeg(CDbl(num2))),
            .Z = CSng(CutsceneManager.RadToDeg(num3))
        }
    End Function

    Public Shared Function ForwardVector(ByVal vector As Vector3, ByVal yaw As Single) As Vector3
        Dim vector2 As Vector3
        Dim num As Single = CSng(Math.Cos((yaw + 1.5707963267948966)))
        vector2.X = (57.29578! * num)
        vector2.Y = 0!
        Dim num2 As Single = CSng(Math.Sin((yaw + 1.5707963267948966)))
        vector2.Z = (57.29578! * num2)
        Return CutsceneManager.CrossWith(vector, vector2)
    End Function

    Public Shared Function RadToDeg(ByVal deg As Double) As Double
        Return ((deg * 180) / 3.1415926535897931)
    End Function

    Public Shared Function RaycastEntity(ByVal screenCoord As Vector2, ByVal camPos As Vector3, ByVal camRot As Vector3) As Entity
        Dim vector As Vector3 = camPos
        Dim character As Entity = Game.Player.Character
        Dim vector2 As Vector3 = (CutsceneManager.ScreenRelToWorld(camPos, camRot, screenCoord) - vector)
        vector2.Normalize()
        Dim result As RaycastResult = World.Raycast((vector + DirectCast((vector2 * 1.0!), Vector3)), (vector + DirectCast((vector2 * 100.0!), Vector3)), (IntersectOptions.Vegetation Or (IntersectOptions.Objects Or (IntersectOptions.Peds1 Or (IntersectOptions.Mission_Entities Or IntersectOptions.Map)))), character)
        If result.DitHitEntity Then
            Return result.HitEntity
        End If
        Return Nothing
    End Function

    Public Shared Function RaycastEverything(ByVal screenCoord As Vector2) As Vector3
        Dim position As Vector3 = GameplayCamera.Position
        Dim rotation As Vector3 = GameplayCamera.Rotation
        Dim vector3 As Vector3 = CutsceneManager.ScreenRelToWorld(position, rotation, screenCoord)
        Dim vector4 As Vector3 = position
        Dim character As Entity = Game.Player.Character
        If Game.Player.Character.IsInVehicle Then
            character = Game.Player.Character.CurrentVehicle
        End If
        Dim vector5 As Vector3 = (vector3 - vector4)
        vector5.Normalize()
        Dim result As RaycastResult = World.Raycast((vector4 + DirectCast((vector5 * 1.0!), Vector3)), (vector4 + DirectCast((vector5 * 100.0!), Vector3)), (IntersectOptions.Vegetation Or (IntersectOptions.Objects Or (IntersectOptions.Peds1 Or (IntersectOptions.Mission_Entities Or IntersectOptions.Map)))), character)
        If result.DitHitAnything Then
            Return result.HitCoords
        End If
        Return (position + DirectCast((vector5 * 100.0!), Vector3))
    End Function

    Public Shared Function RaycastEverything(ByVal screenCoord As Vector2, ByVal camPos As Vector3, ByVal camRot As Vector3, ByVal toIgnore As Entity) As Vector3
        Dim vector As Vector3 = camPos
        Dim ignoreEntity As Entity = toIgnore
        Dim vector2 As Vector3 = (CutsceneManager.ScreenRelToWorld(camPos, camRot, screenCoord) - vector)
        vector2.Normalize()
        Dim result As RaycastResult = World.Raycast((vector + DirectCast((vector2 * 1.0!), Vector3)), (vector + DirectCast((vector2 * 100.0!), Vector3)), (IntersectOptions.Vegetation Or (IntersectOptions.Objects Or (IntersectOptions.Peds1 Or (IntersectOptions.Mission_Entities Or IntersectOptions.Map)))), ignoreEntity)
        If result.DitHitAnything Then
            Return result.HitCoords
        End If
        Return (camPos + DirectCast((vector2 * 100.0!), Vector3))
    End Function

    Public Shared Function RotationToDirection(ByVal rotation As Vector3) As Vector3
        Dim a As Double = CutsceneManager.DegToRad(CDbl(rotation.Z))
        Dim d As Double = CutsceneManager.DegToRad(CDbl(rotation.X))
        Dim num3 As Double = Math.Abs(Math.Cos(d))
        Return New Vector3 With {
            .X = CSng((-Math.Sin(a) * num3)),
            .Y = CSng((Math.Cos(a) * num3)),
            .Z = CSng(Math.Sin(d))
        }
    End Function

    Public Shared Function ScreenRelToWorld(ByVal camPos As Vector3, ByVal camRot As Vector3, ByVal coord As Vector2) As Vector3
        Dim vector8 As Vector2
        Dim vector9 As Vector2
        Dim vector As Vector3 = CutsceneManager.RotationToDirection(camRot)
        Dim rotation As Vector3 = (camRot + New Vector3(10.0!, 0!, 0!))
        Dim vector3 As Vector3 = (camRot + New Vector3(-10.0!, 0!, 0!))
        Dim vector4 As Vector3 = (camRot + New Vector3(0!, 0!, -10.0!))
        Dim vector5 As Vector3 = (CutsceneManager.RotationToDirection(rotation) - CutsceneManager.RotationToDirection(vector3))
        Dim d As Double = -CutsceneManager.DegToRad(CDbl(camRot.Y))
        Dim vector1 As Vector3 = (CutsceneManager.RotationToDirection((camRot + New Vector3(0!, 0!, 10.0!))) - CutsceneManager.RotationToDirection(vector4))
        Dim vector6 As Vector3 = DirectCast(((vector1 * CSng(Math.Cos(d))) - (vector5 * CSng(Math.Sin(d)))), Vector3)
        Dim vector7 As Vector3 = DirectCast(((vector1 * CSng(Math.Sin(d))) + (vector5 * CSng(Math.Cos(d)))), Vector3)
        If Not CutsceneManager.WorldToScreenRel((((camPos + DirectCast((vector * 10.0!), Vector3)) + vector6) + vector7), vector8) Then
            Return (camPos + DirectCast((vector * 10.0!), Vector3))
        End If
        If Not CutsceneManager.WorldToScreenRel((camPos + DirectCast((vector * 10.0!), Vector3)), vector9) Then
            Return (camPos + DirectCast((vector * 10.0!), Vector3))
        End If
        If ((Math.Abs(CSng((vector8.X - vector9.X))) < 0.001) OrElse (Math.Abs(CSng((vector8.Y - vector9.Y))) < 0.001)) Then
            Return (camPos + DirectCast((vector * 10.0!), Vector3))
        End If
        Dim num2 As Single = ((coord.X - vector9.X) / (vector8.X - vector9.X))
        Dim num3 As Single = ((coord.Y - vector9.Y) / (vector8.Y - vector9.Y))
        Return DirectCast((((camPos + (vector * 10.0!)) + (vector6 * num2)) + (vector7 * num3)), Vector3)
    End Function

    Public Shared Function WorldToScreenRel(ByVal worldCoords As Vector3, <Out> ByRef screenCoords As Vector2) As Boolean
        Dim argument As New OutputArgument
        Dim argument2 As New OutputArgument
        Dim arguments As InputArgument() = New InputArgument() {worldCoords.X, worldCoords.Y, worldCoords.Z, argument, argument2}
        If Not Native.Function.Call(Of Boolean)(Hash._0x34E82F05DF2974F5, arguments) Then
            screenCoords = New Vector2
            Return False
        End If
        screenCoords = New Vector2(((argument.GetResult(Of Single) - 0.5!) * 2.0!), ((argument2.GetResult(Of Single) - 0.5!) * 2.0!))
        Return True
    End Function

End Class
