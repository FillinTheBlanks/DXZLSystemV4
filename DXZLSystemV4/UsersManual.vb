Public Class UsersManual
    Sub New
        InitializeComponent()
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum

    Private Sub UsersManual_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, PdfViewer1.KeyDown, LayoutControl1.KeyDown, _
        LayoutControl2.KeyDown, btnClose.KeyDown
        If e.KeyData = Keys.Escape Then
            btnClose.PerformClick()
        End If
    End Sub

    Private Sub UsersManual_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            PdfViewer1.LoadDocument("C:\Users Manual.pdf")
            PdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible
        Catch ex As Exception
            MsgBox(ex.ToString & ". Contact System Administrator.")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class
