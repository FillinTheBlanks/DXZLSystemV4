Public Class PrintReportForm
    Public bs As New BindingSource
    Public dsTrans As New DataSet

    Public Sub PrintReport()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsTrans.Clear()
            dgList.DataSource = Nothing


            bs.DataSource = dsTrans.Tables(0)
            dgList.DataSource = bs
            dvList.OptionsBehavior.ReadOnly = False
            PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                              , "", "END OF REPORT")
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub PrintReportForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'PrintReport()
    End Sub
End Class