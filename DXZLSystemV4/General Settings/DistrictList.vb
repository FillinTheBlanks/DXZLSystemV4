﻿Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.Utils
Public Class DistrictList
    Dim bsd As New BindingSource
    Dim dtView As New DataSet
    Dim isEdit As Boolean


    Private Sub SaveChanges()

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            If isEdit Then
                Dim i As Integer = dvList.FocusedRowHandle
                If isAllowed(Me.Text, "Edit") = "" And dvList.GetRowCellValue(i, "DistrictID").ToString <> "" Then MsgBox("Editing Features Disabled.") : Exit Sub

                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spInsert_District"
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
                        .SelectCommand.CommandText = "spInsert_District"
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
                PublicFilterData("", "DistrictID", cmbColumn, "spSelect_DistrictbyFilter" _
                                     , dtView, bsd, dgList, dvList)
                isEdit = False
                Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub

    Private Sub AddCompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCompanyToolStripMenuItem.Click
        dvList.OptionsBehavior.ReadOnly = False
        With dvList
            .Columns(0).OptionsColumn.ReadOnly = True

        End With

        dvList.AddNewRow()
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
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_DistrictbyFilter" _
                                  , dtView, bsd, dgList, dvList, False)
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub CompanyList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData("", "DistrictID", cmbColumn, "spSelect_DistrictbyFilter" _
                             , dtView, bsd, dgList, dvList)
        isEdit = False
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData("", "DistrictID", cmbColumn, "spSelect_DistrictbyFilter" _
                             , dtView, bsd, dgList, dvList)
        isEdit = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If isAllowed(Me.Text, "Print") = "" Then Exit Sub
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then Exit Sub
        Dim i As Integer = dvList.FocusedRowHandle
        DeleteMasterFileTransaction("tblDistrictMasterFile", "DistrictID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("DistrictID")).ToString()))
    End Sub
End Class