﻿Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Camera
Imports DevExpress.Utils
Imports System.Drawing.Imaging
Imports DevExpress.XtraEditors.Controls
Public Class LoanVoucher

    Public bsd, bsSchool, bsCategory, bsPaidBy As New BindingSource
    Public dtView, dsSchool, dsCategory, dsPaidBy As New DataSet
    Dim isEdit As Boolean


    Public Sub HideColumn()

        'If dvList.RowCount > 0 Then

        dvList.OptionsView.ShowFooter = True
        dvList.Columns("LoanAmount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        dvList.Columns("CustomerID").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        lblMessage1.Text = "Total Record/s:"
        lblMessage2.Text = "Total Loan/s:"
        lblResult1.Text = Double.Parse(dvList.Columns("CustomerID").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        lblResult2.Text = Double.Parse(dvList.Columns("LoanAmount").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        'End If
        With dvList
            .Columns("RefBHID").Visible = False
            .Columns("CustomerID").Visible = False
            .Columns("PaidByDescription").Visible = False
            .Columns("TranStatusDescription").Visible = False
            .Columns("TransactionID").Visible = False
            .Columns("TransactionCode").Visible = False
            .Columns("TransactionName").Visible = False
            .Columns("TranStatus").Visible = False
            .Columns("TranStatus").Visible = False
            .Columns("PaidBit").Visible = False
            .Columns("CreatedDateTime").Visible = False
            .Columns("ModifiedBy").Visible = False
            .Columns("ModifiedDateTime").Visible = False
            .Columns("RefNum").BestFit()
            .Columns("RefDate").BestFit()
            '.Columns("Amount").BestFit()
            .Columns("LoanAmount").BestFit()
            '.Columns("InterestAmount").BestFit()
            '.Columns("AmortizationAmount").BestFit()
            '.Columns("LoanAmountGranted").BestFit()
            .Columns("PaidStatus").Visible = False
        End With
    End Sub

    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbColumn.Text.ToString = String.Empty Then Exit Sub

            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_LoanVoucherbyFilter" _
                                  , dtView, bsd, dgList, dvList, False)
            HideColumn()
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub cmbColumn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColumn.SelectedIndexChanged
        If cmbColumn.SelectedItem.ToString <> "" Then
            'If cmbColumn.Text.ToString = String.Empty Then Exit Sub

            'Me.Cursor = Cursors.WaitCursor
            'PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_CustomerbyFilter" _
            '                      , dtView, bsd, dgList, dvList, False)
            'isEdit = False
            'Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnRefresh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_LoanVoucherbyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnNewItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNewItem.ItemClick
        If isAllowed(Me.Text, "Add") = "" Then MsgBox("Account not Allowed") : Exit Sub
        Dim bnedit As New LoanVoucherEdit
        With bnedit
            .StartPosition = FormStartPosition.CenterScreen
            .Show()

            .frmList = Me
            .bsd = New BindingSource
            .bsd.DataSource = GetType(MyLoanVoucherRecord)
            Dim srec As MyLoanVoucherRecord = New MyLoanVoucherRecord
            'srec.CustomerID = 0
            srec.InterestRate = 0
            srec.Amount = 0
            .bsd.Add(srec)
            .txtInterestAmt.Text = "0.00"
            .txtAmmortizationAmt.Text = "0.00"
            .txtLoanGrantedAmt.Text = "0.00"
            '.txtCustomerID.DataBindings.Add(New Binding("EditValue", .bsd, "CustomerID", True))
            '.txtInterest1.DataBindings.Add(New Binding("EditValue", .bsd, "InterestRate", True))
            .txtAmount.DataBindings.Add(New Binding("EditValue", .bsd, "Amount", True))
            .DxErrorProvider1.DataSource = .bsd
            .DxErrorProvider1.ContainerControl = bnedit

            .TopLevel = True
            .Refresh()
            .txtCustomerName.Focus()
            .txtCustomerName.SelectAll()
        End With
    End Sub

    Private Sub LoanVoucher_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_LoanVoucherbyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub btnFirst_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnFirst.ItemClick
        dvList.MoveFirst()
    End Sub

    Private Sub btnPrev_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrev.ItemClick
        dvList.MovePrev()
    End Sub

    Private Sub btnNext_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNext.ItemClick
        dvList.MoveNext()
    End Sub

    Private Sub btnLast_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLast.ItemClick
        dvList.MoveLast()
    End Sub

    Private Sub dvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvList.DoubleClick
        If isAllowed(Me.Text, "Edit") = "" Then MsgBox("Account not Allowed") : Exit Sub
        If IsNothing(dvList.GetRowCellValue(dvList.FocusedRowHandle, dvList.Columns("RefBHID"))) = True Then MsgBox("No Record Selected.") : Exit Sub
        Dim bnedit As New LoanVoucherEdit
        With bnedit
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .frmList = Me
            .isEdit = 1

            Dim i As Integer = dvList.FocusedRowHandle
            .bsd = New BindingSource
            .bsd.DataSource = GetType(MyLoanVoucherRecord)
            Dim srec As MyLoanVoucherRecord = New MyLoanVoucherRecord
            .refbhid = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString())

            srec.CustomerID = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("CustomerID")).ToString())
            srec.InterestRate = Double.Parse(dvList.GetRowCellValue(i, dvList.Columns("InterestRate1")).ToString())
            srec.Amount = Double.Parse(dvList.GetRowCellValue(i, dvList.Columns("LoanAmount")).ToString())
            .txtRefNum.Text = dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString()
            .txtCustomerName.Text = dvList.GetRowCellValue(i, dvList.Columns("FullName")).ToString()
            .txtRemarks.Text = dvList.GetRowCellValue(i, dvList.Columns("Remarks")).ToString()
            .txtInterest2.Text = dvList.GetRowCellValue(i, dvList.Columns("InterestRate2")).ToString()
            .txtInterestAmt.Text = Double.Parse(dvList.GetRowCellValue(i, dvList.Columns("InterestAmount")).ToString).ToString("#,##0.00")
            .txtAmmortizationAmt.Text = Double.Parse(dvList.GetRowCellValue(i, dvList.Columns("AmortizationAmount")).ToString).ToString("#,##0.00")
            .txtLoanGrantedAmt.Text = Double.Parse(dvList.GetRowCellValue(i, dvList.Columns("LoanAmountGranted")).ToString).ToString("#,##0.00")
            .bsd.Add(srec)

            '.txtRefNum.DataBindings.Add(New Binding("EditValue", .bsd, "[RefNum]", True))
            .txtCustomerID.DataBindings.Add(New Binding("EditValue", .bsd, "CustomerID", True))
            '.txtInterest1.DataBindings.Add(New Binding("EditValue", .bsd, "InterestRate", True))
            .txtAmount.DataBindings.Add(New Binding("EditValue", .bsd, "Amount", True))
            .DxErrorProvider1.DataSource = .bsd
            .DxErrorProvider1.ContainerControl = bnedit

            .TopLevel = True
            .Refresh()
            .txtCustomerName.Focus()
            .txtCustomerName.SelectAll()
        End With
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        'Dim printform As New PrintReportForm
        'With printform
        '    .dsTrans = dtView
        '    .bs = bsd
        '    .Show()
        '    .PrintReport()
        'End With
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                              , "", "END OF REPORT")
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_LoanVoucherbyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub btnOutstandingReport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnOutstandingReport.ItemClick
        Dim rps As New ReportParameterSelector
        rps.rpttype = Me.Text
        rps.RptName = "OutstandingReport"
        showChildFormDLG(rps, Windows.Forms.FormBorderStyle.FixedToolWindow)
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then MsgBox("Not Allowed to Delete") : Exit Sub
        If IsNothing(dvList.GetRowCellValue(dvList.FocusedRowHandle, dvList.Columns("RefBHID"))) = True Then MsgBox("No Record Selected.") : Exit Sub
        Dim rbd As New RecordbeforeDelete
        Dim i As Integer = dvList.FocusedRowHandle
        rbd.refnum = dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString()
        rbd.refbhid = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString())
        rbd.customername = dvList.GetRowCellValue(i, dvList.Columns("FullName")).ToString()
        rbd.tblName = "tbl" & Me.Name & "File"
        showChildFormDLG(rbd, Windows.Forms.FormBorderStyle.FixedToolWindow)
    End Sub
End Class
