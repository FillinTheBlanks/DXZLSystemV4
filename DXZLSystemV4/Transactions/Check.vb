Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors

Public Class Check
    Dim bsd, bsCheck, bsBank As New BindingSource
    Dim dtCompView, dsCheck, dsBank As New DataSet
    Dim isEdit As Integer = 0
    Friend WithEvents myLookBank As New RepositoryItemGridLookUpEdit
    Public pyedit As PaymentEdit
    Public focusedRow As Integer
    Public Sub RefreshCheckFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsCheck.Clear()
            dgList.DataSource = Nothing


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspCheckAvailableRecord"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@RefDate", Now.Year.ToString)
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsCheck)
            End With
            'MsgBox(dsCustomer.Tables(0).Columns.Count)
            For x As Integer = 3 To 8
                dsCheck.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next
            For x As Integer = 12 To 13
                dsCheck.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next
            For x As Integer = 16 To 19
                dsCheck.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next
            'Dim dv As DataView = dsPaymentd.Tables(0).DefaultView

            'dv.RowFilter = "InUsed = 1"
            'bsPaymentd.DataSource = dv
            bsCheck.DataSource = dsCheck.Tables(0)
            dgList.DataSource = bsCheck
            dvList.OptionsBehavior.ReadOnly = False
            DxErrorProvider1.DataSource = bsCheck.DataSource
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            HideColumn()
        End Try
    End Sub

    Private Sub RefreshDataBank()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsBank.Clear()
            myLookBank.DataSource = Nothing
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_BankbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "")
                    .AddWithValue("@ColumnName", "BankID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsBank)
            End With
            bsBank.DataSource = dsBank.Tables(0)

            'dsBank.Tables(0).Columns(0).ColumnMapping = MappingType.Hidden
            For x As Integer = 3 To 8
                dsBank.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next

            Dim ColumnName As String = "BankName"
            myLookBank.DisplayMember = ColumnName
            myLookBank.ValueMember = ColumnName
            myLookBank.DataSource = bsBank.DataSource
            dvList.Columns(ColumnName).ColumnEdit = myLookBank
            dvList.Columns(ColumnName).ColumnEdit.NullText = "<Select " & dvList.Columns(ColumnName).FieldName.ToString & ">"

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub HideColumn()

        If dvList.RowCount > 0 Then

            dvList.OptionsView.ShowFooter = True
            dvList.Columns("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            dvList.Columns("RefBHID").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            'lblMessage1.Text = "Total Record/s:"
            'lblMessage2.Text = "Total Payment/s:"
            'lblResult1.Text = Double.Parse(dvList.Columns("CustomerID").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
            'lblResult2.Text = Double.Parse(dvList.Columns("TotalAmount").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        End If
        With dvList
            .OptionsBehavior.ReadOnly = True
            .Columns("RefDate").Visible = False
            '.Columns("RefTDID").Visible = False
            '.Columns("RefNum").Visible = False
            '.Columns("CreatedBy").Visible = False
            '.Columns("CreatedDateTime").Visible = False
            '.Columns("ModifiedBy").Visible = False
            '.Columns("ModifiedDateTime").Visible = False
            '.Columns("ReferenceNumber").BestFit()
            .Columns("Amount").BestFit()
            .Columns("RefBHID").OptionsColumn.ReadOnly = True
            .Columns("BankID").OptionsColumn.ReadOnly = True
            .Columns("BankCode").OptionsColumn.ReadOnly = True
        End With
    End Sub

    Private Sub SaveChanges(ByVal status As Integer)

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            Dim i As Integer = dvList.FocusedRowHandle
            If dvList.GetRowCellValue(i, "CustomerID").ToString <> "" Then MsgBox("No Customer Assigned.") : Exit Sub

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspCheckAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@Option", status)
                    .AddWithValue("@RefBHID", dvList.GetRowCellValue(i, "RefBHID"))
                    .AddWithValue("@RefNum", dvList.GetRowCellValue(i, "RefNum"))
                    .AddWithValue("@RefDate", My.Computer.Clock.LocalTime.ToString)
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@BankID", dvList.GetRowCellValue(i, "BankID"))
                    .AddWithValue("@Amount", dvList.GetRowCellValue(i, "Amount"))
                    .AddWithValue("@Remarks", dvList.GetRowCellValue(i, "Remarks").ToString.Replace(" ", ""))
                    .AddWithValue("@CreatedBy", global_PerName)
                    .AddWithValue("@CreatedDateTime", My.Computer.Clock.LocalTime.ToString)
                    .AddWithValue("@ModifiedBy", global_PerName)
                    .AddWithValue("@ModifiedDateTime", My.Computer.Clock.LocalTime.ToString)
                End With
                cnt = .SelectCommand.ExecuteNonQuery()

            End With


        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MsgBox("Successfully Saved.")
                Me.Cursor = Cursors.WaitCursor
                RefreshCheckFile()
                isEdit = 0
                Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub

    Private Sub DeleteRecord(ByVal tableName As String, ByVal fieldname As String, ByVal bhid As Integer)
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

        End Try
    End Sub


    Private Sub SaveChangesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges(isEdit)
        End If
    End Sub

    Private Sub Check_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, txtCustomerName.KeyDown, dvList.KeyDown, txtCustomerID.KeyDown, txtCustomerName.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If

    End Sub


    Private Sub Check_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        RefreshCheckFile()
        RefreshDataBank()
        isEdit = 0
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub


    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        RefreshCheckFile()
        RefreshDataBank()
        isEdit = 0
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
    End Sub

    Private Sub AddRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRowsToolStripMenuItem.Click
        dvList.AddNewRow()
        dvList.OptionsBehavior.ReadOnly = False
    End Sub

    Private Sub myLookBank_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookBank.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsBank.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(2).ToString Then
                With dvList
                    .SetRowCellValue(.FocusedRowHandle, "BankID", rw(0))
                    .SetRowCellValue(.FocusedRowHandle, "BankCode", rw(1))
                    .FocusedColumn = .VisibleColumns(0)
                End With
                Exit For
            End If
        Next
    End Sub

    Private Sub EditRowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditRowToolStripMenuItem.Click
        isEdit = 1
        dvList.OptionsBehavior.ReadOnly = False
    End Sub

    Private Sub dvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvList.DoubleClick
        Dim i As Integer = dvList.FocusedRowHandle

        If dvList.GetRowCellValue(i, "RefBHID").ToString <> "" Then

            With pyedit
               
                Dim rh As Integer = .dvList.FocusedRowHandle
                '.dvList.SetRowCellValue(rh, "CheckRefBHID", dvList.GetRowCellValue(i, "RefBHID").ToString)
                '.dvList.SetRowCellValue(rh, "CheckNumber", dvList.GetRowCellValue(i, "RefNum").ToString)
                '.dvList.SetRowCellValue(rh, "CheckDate", dvList.GetRowCellValue(i, "RefDate").ToString)
                '.dvList.SetRowCellValue(rh, "Amount", dvList.GetRowCellValue(i, "Amount").ToString)
                .txtbtnCheckEdit.Text = dvList.GetRowCellValue(i, "RefBHID").ToString
                .txtAmount.Text = dvList.GetRowCellValue(i, "Amount").ToString
                .txtAmount.ReadOnly = True
                '.dvList.Columns("Amount").OptionsColumn.ReadOnly = True
            End With
            Me.Close()
            Me.Dispose()

        End If
    End Sub

    Private Sub dvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dvList.KeyDown
        If e.KeyData = Keys.Enter Then
            Me.Validate()
            If Me.DxErrorProvider1.HasErrors Then Exit Sub
            SaveChanges(isEdit)
        End If
    End Sub

    Private Sub DeleteRowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteRowToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then MsgBox("Not Allowed to Delete") : Exit Sub
        If IsNothing(dvList.GetRowCellValue(dvList.FocusedRowHandle, dvList.Columns("RefBHID"))) = True Then MsgBox("No Record Selected.") : Exit Sub
        If MsgBox("Are you sure you want to delete all the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Dim i As Integer = dvList.FocusedRowHandle
            DeleteRecord("tbl" & Me.Name & "File", "RefBHID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString()))
            Me.Cursor = Cursors.WaitCursor
            RefreshCheckFile()
            HideColumn()

            Me.Cursor = Cursors.Default
        End If
    End Sub
End Class