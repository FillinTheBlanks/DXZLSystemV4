Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.Utils
Public Class SchoolList
    Dim bsd, bsDistrict As New BindingSource
    Dim dtView, dsDistrict As New DataSet
    Dim isEdit As Boolean
    Friend WithEvents myLookDistrict As New RepositoryItemGridLookUpEdit

    Private Sub RefreshDataDistrict()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsDistrict.Clear()
            myLookDistrict.DataSource = Nothing
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_DistrictbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "")
                    .AddWithValue("@ColumnName", "DistrictID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsDistrict)
            End With
            bsDistrict.DataSource = dsDistrict.Tables(0)

            dsDistrict.Tables(0).Columns(0).ColumnMapping = MappingType.Hidden
            For x As Integer = 3 To 6
                dsDistrict.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next

            Dim ColumnName As String = "DistrictName"
            myLookDistrict.DisplayMember = ColumnName
            myLookDistrict.ValueMember = ColumnName
            myLookDistrict.DataSource = bsDistrict.DataSource
            dvList.Columns(ColumnName).ColumnEdit = myLookDistrict
            dvList.Columns(ColumnName).ColumnEdit.NullText = "<Select " & dvList.Columns(ColumnName).FieldName.ToString & ">"

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

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            If isEdit Then
                Dim i As Integer = dvList.FocusedRowHandle
                If isAllowed(Me.Text, "Edit") = "" And dvList.GetRowCellValue(i, "SchoolID").ToString <> "" Then MsgBox("Editing Features Disabled.") : Exit Sub

                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spInsert_School"
                    With .SelectCommand.Parameters
                        .Clear()
                        For Each col In dtView.Tables(0).Columns

                            .AddWithValue("@" & col.ToString, dvList.GetRowCellValue(i, col.ToString))
                        Next
                        '.AddWithValue("@CompanyID", dvCompanyList.GetRowCellValue(i, "CompanyID"))
                        '.AddWithValue("@CompanyName", dvCompanyList.GetRowCellValue(i, "CompanyName"))
                        '.AddWithValue("@CompanyStatus", dvCompanyList.GetRowCellValue(i, "CompanyStatus"))
                        '.AddWithValue("@CompanyInitial", dvCompanyList.GetRowCellValue(i, "CompanyInitial"))
                        '.AddWithValue("@CompanyAddress", dvCompanyList.GetRowCellValue(i, "CompanyAddress"))
                        '.AddWithValue("@CompanyTelNo", dvCompanyList.GetRowCellValue(i, "CompanyTelNo"))
                        '.AddWithValue("@CompanyTinNo", dvCompanyList.GetRowCellValue(i, "CompanyTinNo"))
                        '.AddWithValue("@CompanyEmailAddress", dvCompanyList.GetRowCellValue(i, "CompanyEmailAddress"))
                    End With
                    cnt = .SelectCommand.ExecuteNonQuery()

                End With
            Else
                For i As Integer = 0 To dvList.DataRowCount - 1
                    With dacp
                        .SelectCommand.CommandType = CommandType.StoredProcedure
                        .SelectCommand.CommandText = "spInsert_School"
                        With .SelectCommand.Parameters
                            .Clear()
                            For Each col In dtView.Tables(0).Columns

                                .AddWithValue("@" & col.ToString, dvList.GetRowCellValue(i, col.ToString))
                            Next
                            '.AddWithValue("@CompanyID", dvCompanyList.GetRowCellValue(i, "CompanyID"))
                            '.AddWithValue("@CompanyName", dvCompanyList.GetRowCellValue(i, "CompanyName"))
                            '.AddWithValue("@CompanyStatus", dvCompanyList.GetRowCellValue(i, "CompanyStatus"))
                            '.AddWithValue("@CompanyInitial", dvCompanyList.GetRowCellValue(i, "CompanyInitial"))
                            '.AddWithValue("@CompanyAddress", dvCompanyList.GetRowCellValue(i, "CompanyAddress"))
                            '.AddWithValue("@CompanyTelNo", dvCompanyList.GetRowCellValue(i, "CompanyTelNo"))
                            '.AddWithValue("@CompanyTinNo", dvCompanyList.GetRowCellValue(i, "CompanyTinNo"))
                            '.AddWithValue("@CompanyEmailAddress", dvCompanyList.GetRowCellValue(i, "CompanyEmailAddress"))
                        End With
                        cnt = .SelectCommand.ExecuteNonQuery()

                    End With
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MsgBox("Successfully Saved.")
                Me.Cursor = Cursors.WaitCursor
                PublicFilterData("", "SchoolID", cmbColumn, "spSelect_SchoolbyFilter" _
                                     , dtView, bsd, dgList, dvList)
                isEdit = False
                Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub

    Private Sub AddCompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCompanyToolStripMenuItem.Click
        dvList.OptionsBehavior.ReadOnly = False
        dvList.AddNewRow()
        With dvList
            .Columns(0).OptionsColumn.ReadOnly = True
            .SetRowCellValue(.FocusedRowHandle, "CreatedBy", global_PerName)
            .SetRowCellValue(.FocusedRowHandle, "CreatedDateTime", Now())
            .FocusedColumn = .VisibleColumns(0)
        End With
    End Sub

    Private Sub EditCompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditCompanyToolStripMenuItem.Click
        dvList.OptionsBehavior.ReadOnly = False
        With dvList
            .Columns(0).OptionsColumn.ReadOnly = True
        End With
        isEdit = True
    End Sub

    Private Sub SaveChangesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveChangesToolStripMenuItem.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges()
        End If
    End Sub

    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbColumn.Text.ToString = String.Empty Then Exit Sub

            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_SchoolbyFilter" _
                                  , dtView, bsd, dgList, dvList, False)
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub SchoolList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData("", "SchoolID", cmbColumn, "spSelect_SchoolbyFilter" _
                             , dtView, bsd, dgList, dvList)
        RefreshDataDistrict()
        isEdit = False
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData("", "SchoolID", cmbColumn, "spSelect_SchoolbyFilter" _
                             , dtView, bsd, dgList, dvList)
        isEdit = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If isAllowed(Me.Text, "Print") = "" Then Exit Sub
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
    End Sub

    Private Sub myLookDistrict_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookDistrict.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsDistrict.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(2).ToString Then
                With dvList
                    .SetRowCellValue(.FocusedRowHandle, "DistrictID", rw(0))
                    .SetRowCellValue(.FocusedRowHandle, "DistrictCode", rw(1))
                    .FocusedColumn = .VisibleColumns(0)
                End With
                Exit For
            End If
        Next
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then Exit Sub
        Dim i As Integer = dvList.FocusedRowHandle
        DeleteMasterFileTransaction("tblSchoolMasterFile", "SchoolID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("SchoolID")).ToString()))
    End Sub
End Class