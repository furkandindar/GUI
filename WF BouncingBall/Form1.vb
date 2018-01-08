Public Class Form1

    Dim radius As Integer = 20
    Dim velocity As Integer = 50
    Dim xC As Integer, yC As Integer, xDelta As Integer, yDelta As Integer, xSize As Integer, ySize As Integer

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        Timer1.Start()
    End Sub

    Public Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        velocity = TrackBar1.Value
    End Sub

    Private Sub TrackBar2_Scroll(sender As Object, e As EventArgs) Handles TrackBar2.Scroll
        xDelta = TrackBar2.Value
    End Sub

    Private Sub TrackBar3_Scroll(sender As Object, e As EventArgs) Handles TrackBar3.Scroll
        yDelta = TrackBar3.Value
    End Sub

    Private Sub Form1(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1_Tick_1(sender, e)
    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Start()
        Timer1.Interval = velocity
        DrawBall()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Timer1.Stop()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As System.EventArgs) Handles MyBase.Resize
        xSize = Me.ClientSize.Width
        ySize = Me.ClientSize.Height
        xC = xSize / 2
        yC = ySize / 2
        Me.Invalidate()
        DrawBall()
    End Sub

    Private Sub DrawBall()
        Dim g As Graphics = Me.CreateGraphics()
        Dim b As Brush = New SolidBrush(Me.BackColor)
        g.FillEllipse(b, xC - radius, yC - radius, 2 * radius, 2 * radius)
        xC = xC + (xDelta * 90)
        yC = yC + (yDelta * 90)

        If (xC + radius >= ClientSize.Width) OrElse (xC - radius <= 0) Then
            xDelta = -xDelta
        End If
        If (yC + radius >= ClientSize.Height) OrElse (yC - radius <= 0) Then
            yDelta = -yDelta
        End If

        b = New SolidBrush(Color.DeepSkyBlue)
        g.FillEllipse(b, xC - radius, yC - radius, 2 * radius, 2 * radius)
        b.Dispose()
        g.Dispose()
    End Sub

End Class