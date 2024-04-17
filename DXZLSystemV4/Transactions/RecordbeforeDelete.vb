Public Class RecordbeforeDelete
    Dim bsCheck As New BindingSource
    Dim dsCheck As New DataSet
    Public refbhid As Integer
    Public refnum As String
    Public customername As String = ""
    Public tblName As String = ""

    Public Sub RefreshCheckFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsCheck.Clear()
            dgList.DataSource = Nothing


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_PaymentRecordbeforeDelete"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@RefNum", refnum)
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsCheck)
            End With

            'For x As Integer = 3 To 5
            '    dsCheck.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            'Next

            'Dim dv As DataView = dsCheck.Tables(0).DefaultView

            'dv.RowFilter = "TransactionCode LIKE '%" & trantype & "%'"
            'bsCheck.DataSource = dv
            bsCheck.DataSource = dsCheck.Tables(0)
            dgList.DataSource = bsCheck
            dvList.OptionsBehavior.ReadOnly = False

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub SaveChanges(ByVal tableName As String, ByVal fieldname As String, ByVal bhid As Integer)
        Dim cnt As Integer = 0
        Dim refnumpaybh As String = ""
        Try

            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspDeleteTransaction"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@tblName", tableName)
                    .AddWithValue("@strField", fieldname)
                    .AddWithValue("@intValue", bhid)
                End With
                'MsgBox(tableName & " " & fieldname & " " & bhid)
                cnt += .SelectCommand.ExecuteNonQuery()

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MsgBox("Successfully deleted " & cnt & " transactions.")
            End If
            Me.Close()
            Me.Dispose()
        End Try
    End Sub

    Private Sub RecordbeforeDelete_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, dvList.KeyDown, dgList.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        ElseIf e.KeyData = Keys.Enter Then
            'SelectItemfromGridView()
        End If
    End Sub

    Private Sub RecordbeforeDelete_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Affected Record w/ Ref. No. " & refnum & " of " & customername
        RefreshCheckFile()
        dvList.OptionsBehavior.ReadOnly = True
        dvList.OptionsBehavior.Editable = False
    End Sub

    Private Sub dvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvList.DoubleClick
        'SelectItemfromGridView()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Text = "Payment Record w/ Ref. No. " & refnum & " of " & customername
        RefreshCheckFile()
        dvList.OptionsBehavior.ReadOnly = True
        dvList.OptionsBehavior.Editable = False
    End Sub

    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Or refnum = "" Then Exit Sub

        If MsgBox("Are you sure you want to delete all the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges(tblName, "RefBHID", refbhid)

            If dvList.SelectedRowsCount > 0 Then
                For Each rows As DataRow In dsCheck.Tables(0).Rows
                    If rows.Item(2).ToString = "Payment" Or rows.Item(2).ToString = "ReloanPayment" Then
                        SaveChanges("tbl" & rows.Item(2).ToString & "BHFile", "RefBHID", Integer.Parse(rows.Item(0).ToString))
                        SaveChanges("tbl" & rows.Item(2).ToString & "DTFile", "RefBHID", Integer.Parse(rows.Item(0).ToString))
                    Else
                        SaveChanges("tbl" & rows.Item(2).ToString & "File", "RefBHID", Integer.Parse(rows.Item(0).ToString))
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
        Me.Dispose()
    End Sub
End Class