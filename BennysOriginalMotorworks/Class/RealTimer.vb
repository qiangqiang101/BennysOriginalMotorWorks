Public Class RealTimer

    Public Start As Date

    Public Sub New(_start As Date)
        Start = _start
    End Sub

    Public Sub New()
        Start = Now
    End Sub

    Public Sub Reset(Optional _start As Date = Nothing)
        If _start = Nothing Then _start = Now
        Start = _start
    End Sub

    Public Function TotalSeconds(Span As Integer) As Boolean
        Return (Now - Start).TotalSeconds > Span
    End Function

    Public Function TotalMilliseconds(Span As Integer) As Boolean
        Return (Now - Start).TotalMilliseconds > Span
    End Function

    Public Function TotalMinutes(Span As Integer) As Boolean
        Return (Now - Start).TotalMinutes > Span
    End Function

    Public Function TotalHours(Span As Integer) As Boolean
        Return (Now - Start).TotalHours > Span
    End Function

End Class
