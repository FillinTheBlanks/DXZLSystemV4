Imports DevExpress.XtraEditors.Camera
Imports System.IO

Public Class TakePicture
    Dim imgArray As Byte()

    Public Sub GetImagefromFile()
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT CustomerImage,ImageLength FROM vw_CustomerImage WHERE CustomerID=" & txtCustomerID.Text & ""
                With .SelectCommand.Parameters
                    .Clear()

                End With
                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    If dr.HasRows Then
                        If dr.GetInt32(1) > 0 Then


                            imgArray = Nothing
                            pbImage.BackgroundImage = Nothing
                            pbImage.Image = Nothing
                            pbImage.Invalidate()
                            pbImage.BackgroundImage = My.Resources.defaultimg
                            pbImage.Invalidate()
                            System.GC.Collect()
                            System.GC.WaitForPendingFinalizers()
                            System.Threading.Thread.Sleep(100)
                            Dim fileSize As Integer = dr.GetInt32(dr.GetOrdinal("ImageLength"))
                            Dim name As String = "TempImage1" & Now.ToString("yyyy-MM-dd hh-mm-ss")
                            Dim rawData() As Byte = New Byte((fileSize) - 1) {}
                            If System.IO.File.Exists("TempImage1*") Then
                                System.IO.File.Delete("TempImage1*")
                            End If

                            dr.GetBytes(dr.GetOrdinal("CustomerImage"), 0, rawData, 0, fileSize)
                            Dim fs As FileStream = New FileStream(name, FileMode.Create, FileAccess.Write)
                            fs.Write(rawData, 0, fileSize)
                            fs.Close()
                            pbImage.BackgroundImage = New Bitmap(name)
                            Dim converter As ImageConverter = New ImageConverter
                            imgArray = CType(converter.ConvertTo(pbImage.Image, GetType(System.Byte())), Byte())
                        Else
                            pbImage.BackgroundImage = My.Resources.defaultimg
                        End If
                    Else
                        pbImage.BackgroundImage = My.Resources.defaultimg
                    End If
                    Else
                    pbImage.BackgroundImage = My.Resources.defaultimg
                End If
                dr.Close()

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SaveChanges()
        Dim cnt As Integer = 0

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim converter As ImageConverter = New ImageConverter
            imgArray = CType(converter.ConvertTo(pbImage.BackgroundImage, GetType(System.Byte())), Byte())

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spUpdate_CustomerImage"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@CustomerImage", imgArray)
                End With
                cnt = .SelectCommand.ExecuteNonQuery()

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                GetImagefromFile()
                System.GC.Collect()
                System.GC.WaitForPendingFinalizers()
                System.Threading.Thread.Sleep(1000)
                Me.Close()
                Me.Dispose()
            End If
        End Try
    End Sub

    Private Sub pbImage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbImage.DoubleClick
        Dim d As New TakePictureDialog()

        If d.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim images As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
            images.ImageSize = New Size(200, 200)
            images.AddImage(d.Image)
            'images.Save("D:\snapshot.bmp")
            pbImage.BackgroundImage = images.Images(0)
            'pbImage.BackgroundImageLayout = ImageLayout.Zoom
            'myLookCustomerImage.SmallImages = images
            'myLookCustomerImage.Items.Add(New ImageComboBoxItem("", 1, 0))
            'myLookCustomerImage.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center

        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges()
        End If
    End Sub

    Private Sub TakePicture_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pbImage.BackgroundImageLayout = ImageLayout.Zoom
        txtCustomerID.ReadOnly = True
        txtDistrict.ReadOnly = True
        txtFullName.ReadOnly = True
        txtSchool.ReadOnly = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub
End Class