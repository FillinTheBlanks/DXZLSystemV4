Public Class DateTimeChanger 
    Public frmList As MDIParent1


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        frmList.tsDateTimeChanger.Text = dtDateTime.DateTime.ToString
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub dtDateTime_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtDateTime.KeyDown
        If e.KeyData = Keys.Enter Then
            frmList.tsDateTimeChanger.Text = dtDateTime.DateTime.ToString
            Me.Close()
            Me.Dispose()
        End If
    End Sub
End Class