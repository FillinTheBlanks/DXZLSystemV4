Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Utils
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports DevExpress.XtraBars
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Drawing.Helpers
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Utils.Win
Imports DevExpress.XtraEditors

Namespace DXZLSystemV4

    Public Class StickLookAndFeelForm
        Inherits CustomTopForm
        Private painter As New EmptySkinElementPainter()
        Public Sub New()
            AddHandler UserLookAndFeel.Default.StyleChanged, AddressOf Default_StyleChanged
            UpdateRegion()
        End Sub

        Private Sub Default_StyleChanged(ByVal sender As Object, ByVal e As EventArgs)
            UpdateRegion()
        End Sub
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim skin As Skin = BarSkins.GetSkin(UserLookAndFeel.Default)
            Dim cache As New GraphicsCache(e)
            DrawContent(cache, skin)
            DrawTopElement(cache, skin)
            MyBase.OnPaint(e)
        End Sub
        Private Function GetTopElementRectangle() As Rectangle
            Dim r As Rectangle = Me.ClientRectangle
            Return New Rectangle(r.X, r.Y, r.Width, 10)
        End Function
        Private Sub DrawContent(ByVal graphicsCache As GraphicsCache, ByVal skin As Skin)
            Dim element As SkinElement = skin(BarSkins.SkinAlertWindow)
            Dim eInfo As New SkinElementInfo(element, Me.ClientRectangle)
            ObjectPainter.DrawObject(graphicsCache, SkinElementPainter.Default, eInfo)
        End Sub
        Private Sub DrawTopElement(ByVal graphicsCache As GraphicsCache, ByVal skin As Skin)
            Dim element As SkinElement = skin(BarSkins.SkinAlertCaptionTop)
            Dim eInfo As New SkinElementInfo(element, GetTopElementRectangle())
            ObjectPainter.DrawObject(graphicsCache, painter, eInfo)
        End Sub
        Friend Sub UpdateRegion()
            Dim se As SkinElement = BarSkins.GetSkin(UserLookAndFeel.Default)(BarSkins.SkinAlertWindow)
            If Object.Equals(se, Nothing) Then
                Me.Region = Nothing
                Return
            End If
            Dim cornerRadius As Integer = se.Properties.GetInteger(BarSkins.SkinAlertWindowCornerRadius)
            If cornerRadius = 0 Then
                Me.Region = Nothing
            Else
                Me.Region = NativeMethods.CreateRoundRegion(New Rectangle(Point.Empty, Me.Size), cornerRadius)
            End If
        End Sub
        Protected Overrides ReadOnly Property HasSystemShadow() As Boolean
            Get
                Return True
            End Get
        End Property
        Private Class EmptySkinElementPainter
            Inherits SkinElementPainter
            Protected Overrides Sub DrawSkinForeground(ByVal ee As SkinElementInfo)
            End Sub
        End Class
    End Class

End Namespace