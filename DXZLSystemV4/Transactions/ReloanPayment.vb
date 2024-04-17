Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Camera
Imports DevExpress.Utils
Imports System.Drawing.Imaging
Imports DevExpress.XtraEditors.Controls
Public Class ReloanPayment
    Private WithEvents myLookAddCustomerButton As New RepositoryItemButtonEdit
    Private WithEvents myLookAddUnpaidBalance As New RepositoryItemButtonEdit
    Public bsd, bsSchool, bsCategory, bsPaidBy As New BindingSource
    Public dtView, dsSchool, dsCategory, dsPaidBy As New DataSet
    Dim isEdit As Boolean

    Public Sub QuickComputationBalance(ByVal CustomerID As String)

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()


            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspQuickComputationBalance"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@SearchCustomerID", CustomerID)
                    .AddWithValue("@CustomerPaymentDate", MDIParent1.tsDateTimeChanger.Text)

                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub HideColumn()

        'If dvList.RowCount > 0 Then

        dvList.OptionsView.ShowFooter = True
        'dvList.Columns("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        dvList.Columns("CustomerID").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        lblMessage1.Text = "Total Record/s:"
        'lblMessage2.Text = "Total Payment/s:"
        lblResult1.Text = Double.Parse(dvList.Columns("CustomerID").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        'lblResult2.Text = Double.Parse(dvList.Columns("Amount").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        'End If
        With dvList
            .Columns("RefBHID").Visible = False
            .Columns("CustomerID").Visible = False
            .Columns("LoanVoucherRefBHID").Visible = False
            '.Columns("TranStatusDescription").Visible = False
            '.Columns("TranStatus").Visible = False
            .Columns("CreatedBy").Visible = False
            .Columns("CreatedDateTime").Visible = False
            .Columns("ModifiedBy").Visible = False
            .Columns("ModifiedDateTime").Visible = False
            .Columns("RefNum").BestFit()
            .Columns("RefDate").BestFit()
            '.Columns("Amount").BestFit()
            '.Columns("LoanAmount").BestFit()
            '.Columns("InterestAmount").BestFit()
            '.Columns("AmortizationAmount").BestFit()
            '.Columns("LoanAmountGranted").BestFit()

        End With
    End Sub

    Public Sub PrintReport(ByVal customerid As String, ByVal refbhid As Integer)
        Try
            Dim dt As New DataTable
            Dim ds As New DataSet

            dt.Clear()
            dt.TableName = "ReloanPaymentTbl"

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spReport_ReloanPayment"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@CustomerID", customerid)
                    .AddWithValue("@RefNum", refbhid)
                End With
                dr = .SelectCommand.ExecuteReader
                ds.Tables.Add(dt)
                dt.Load(dr)

                dr.Close()
            End With
            If ds.Tables(0).Rows.Count > 0 Then
                QuickComputationBalance(customerid)

                Dim totalcv As Double = 0
                Dim totalcvintrst As Double = 0
                Dim totalbn As Double = 0
                Dim totallv As Double = 0
                Dim totalca As Double = 0
                Dim totalcvamt As Double = 0
                Dim totalotheramt As Double = 0
                Dim intrst As Double = 0
                Dim amt As Double = 0
                Dim amtgranted As Double = 0
                Dim refnum As String = dt.Rows(0)("RefNum").ToString
                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spReport_LoanBalance"
                    With .SelectCommand.Parameters
                        .Clear()
                        .AddWithValue("@CustomerID", customerid)
                    End With
                    dr = .SelectCommand.ExecuteReader
                    While dr.Read
                        If dr(0).ToString = "LV-RS" Then
                            totallv += Double.Parse(dr(1).ToString)
                        ElseIf dr(0).ToString = "CV-RS" Then
                            totalcv += Double.Parse(dr(1).ToString)
                            totalcvintrst += Double.Parse(dr(2).ToString)
                        ElseIf dr(0).ToString = "BN-RS" Then
                            totalbn += Double.Parse(dr(1).ToString) + Double.Parse(dr(2).ToString)
                        ElseIf dr(0).ToString = "CA-RS" Then
                            totalca += Double.Parse(dr(1).ToString) + Double.Parse(dr(2).ToString)
                        End If
                    End While

                    dr.Close()
                End With

                For x As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(x)("TransactionCode").ToString = "CV" Or dt.Rows(x)("TransactionCode").ToString = "PC" Then
                        totalcvamt += Double.Parse(dt.Rows(x)("Amount").ToString)
                    Else
                        totalotheramt += Double.Parse(dt.Rows(x)("Amount").ToString)
                    End If
                Next

                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spSelect_LoanVoucherbyFilter"
                    With .SelectCommand.Parameters
                        .Clear()
                        .AddWithValue("@TextFilter", refnum)
                        .AddWithValue("@ColumnName", "RefNum")
                    End With
                    dr = .SelectCommand.ExecuteReader

                    If dr.Read Then
                        amtgranted = Double.Parse(dr(20).ToString)
                        intrst = Double.Parse(dr(9).ToString)
                        amt = Double.Parse(dr(17).ToString)
                    End If
                End With

                dr.Close()


                Dim params As New List(Of Object)
                params.Add(totalcv + totalcvintrst) 'ParamCVBalance
                params.Add(totalcvintrst) 'ParamCVInterest
                params.Add(totalcv) 'ParamCV
                params.Add(totalbn + totalca) 'ParamOtherPayable

                params.Add(totalcvamt) 'ParamCVAmount
                params.Add(totalotheramt) 'ParamOtherAmount
                params.Add(totalcvamt + totalotheramt) 'ParamTotalPayment
                params.Add(totalcv + totalcvintrst + totalbn + totalca) 'ParamTotalDue
                params.Add(amt) 'ParamLoanAmt
                params.Add(intrst) 'ParamInterestAmt
                params.Add(amtgranted) 'ParamAmtGranted
                params.Add((totalcvamt + totalotheramt) - amtgranted) 'ParamNetProceed

                If System.IO.File.Exists(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xml") Then
                    System.IO.File.Delete(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xml")
                End If

                If System.IO.File.Exists(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xsd") Then
                    System.IO.File.Delete(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xsd")
                End If

                dt.WriteXml(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xml")
                dt.WriteXmlSchema(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xsd")
                Dim rviewer As New ReportViewer

                rviewer.LoadReloanPaymentReport(Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xsd", _
                                               Application.StartupPath + "\DXZLS_ReloanPaymentRpt.xml", _
                                               params)
            Else
                MsgBox("No records available.")
            End If
        Catch ex As Exception
            dr.Close()
            MsgBox(ex.ToString)
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

    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbColumn.Text.ToString = String.Empty Then Exit Sub

            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_ReloanPaymentbyFilter" _
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
        PublicFilterData(Date.Parse(MDIParent1.tsDateTimeChanger.Text).Year.ToString, "RefDate", cmbColumn, "spSelect_ReloanPaymentbyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnNewItem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNewItem.ItemClick
        If isAllowed(Me.Text, "Add") = "" Then MsgBox("Account not Allowed") : Exit Sub
        Dim pyedit As New ReloanPaymentEdit1
        With pyedit
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .refbhid = 0
            .frmList = Me
            .bsd = New BindingSource
            .bsd.DataSource = GetType(MyReloanVoucherRecord)
            Dim srec As MyReloanVoucherRecord = New MyReloanVoucherRecord
            'srec.CustomerID = 0
            srec.InterestRate = 0
            srec.Amount = 0
            .bsd.Add(srec)


            .DxErrorProvider1.DataSource = .bsd
            .DxErrorProvider1.ContainerControl = pyedit

            .TopLevel = True
            .Refresh()
            .txtCustomerName.Focus()
            .txtCustomerName.SelectAll()
        End With
    End Sub

    Private Sub Payment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Date.Parse(MDIParent1.tsDateTimeChanger.Text).Year.ToString, "RefDate", cmbColumn, "spSelect_ReloanPaymentbyFilter" _
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
        If dvList.SelectedRowsCount <= 0 Then Exit Sub

        Dim pyedit As New ReloanPaymentEdit1

        With pyedit
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .frmList = Me
            .isEdit = 1

            Dim i As Integer = dvList.FocusedRowHandle
            .bsd = New BindingSource
            .bsd.DataSource = GetType(MyReloanVoucherRecord)
            Dim srec As MyReloanVoucherRecord = New MyReloanVoucherRecord
            .refbhid = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("LoanVoucherRefBHID")).ToString())
            .txtRefDate.Text = dvList.GetRowCellValue(i, dvList.Columns("RefDate")).ToString()
            srec.CustomerID = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("CustomerID")).ToString())
            '.txtRefNum.Text = dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString()
            .txtCustomerName.Text = dvList.GetRowCellValue(i, dvList.Columns("FullName")).ToString()
            .txtRemarks.Text = dvList.GetRowCellValue(i, dvList.Columns("Remarks")).ToString()
            .rprefbhid = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString())
            .bsd.Add(srec)
            .txtbtnRefNo.Text = dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString()
            .txtRefID.Text = dvList.GetRowCellValue(i, dvList.Columns("LoanVoucherRefBHID")).ToString()
            '.txtRefNum.DataBindings.Add(New Binding("EditValue", .bsd, "[RefNum]", True))
            .txtCustomerID.DataBindings.Add(New Binding("EditValue", .bsd, "CustomerID", True))
            '.txtInterest1.DataBindings.Add(New Binding("EditValue", .bsd, "InterestRate", True))
            
            .LayoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .LayoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .LayoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .LayoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            .LayoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            .DxErrorProvider1.DataSource = .bsd
            .DxErrorProvider1.ContainerControl = pyedit

            .TopLevel = True
            .Refresh()
            .txtCustomerName.Focus()
            .txtCustomerName.SelectAll()
        End With
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Date.Parse(MDIParent1.tsDateTimeChanger.Text).Year.ToString, "RefDate", cmbColumn, "spSelect_ReloanPaymentbyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub AddRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dvList.AddNewRow()
        isEdit = False
        dvList.FocusedColumn = dvList.VisibleColumns(0)

    End Sub

    Private Sub PrintReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintReportToolStripMenuItem.Click
        PrintReport(dvList.GetFocusedRowCellDisplayText("CustomerID").ToString, Integer.Parse(dvList.GetFocusedRowCellDisplayText("RefBHID").ToString))
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then MsgBox("Not Allowed to Delete") : Exit Sub
        If MsgBox("Are you sure you want to delete all the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Dim i As Integer = dvList.FocusedRowHandle
            DeleteRecord("tbl" & Me.Name & "BHFile", "RefBHID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString()))
            DeleteRecord("tbl" & Me.Name & "DTFile", "RefBHID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString()))
            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(Date.Parse(MDIParent1.tsDateTimeChanger.Text).Year.ToString, "RefDate", cmbColumn, "spSelect_ReloanPaymentbyFilter" _
                             , dtView, bsd, dgList, dvList)
            HideColumn()
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnRPReport_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRPReport.ItemClick
        If dvList.SelectedRowsCount > 0 Then Exit Sub
        Dim i As Integer = dvList.FocusedRowHandle
        PrintReport(dvList.GetRowCellValue(i, "CustomerID").ToString, Integer.Parse(dvList.GetRowCellValue(i, "RefBHID").ToString))
    End Sub
End Class
