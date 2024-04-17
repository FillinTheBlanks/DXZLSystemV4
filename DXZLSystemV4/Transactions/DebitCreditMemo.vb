Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Camera
Imports DevExpress.Utils
Imports System.Drawing.Imaging
Imports DevExpress.XtraEditors.Controls
Public Class DebitCreditMemo

    Public bsd, bsSchool, bsCategory, bsPaidBy As New BindingSource
    Public dtView, dsSchool, dsCategory, dsPaidBy As New DataSet
    Dim isEdit As Boolean

    Public Sub HideColumn()

        'If dvList.RowCount > 0 Then

        dvList.OptionsView.ShowFooter = True
        dvList.Columns("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        dvList.Columns("CustomerID").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        lblMessage1.Text = "Total Record/s:"
        lblMessage2.Text = "Total Payment/s:"
        lblResult1.Text = Double.Parse(dvList.Columns("CustomerID").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        lblResult2.Text = Double.Parse(dvList.Columns("Amount").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")

        'End If
        With dvList
            .Columns("RefBHID").Visible = False
            .Columns("CustomerID").Visible = False
            .Columns("PaidByDescription").Visible = False
            .Columns("TranStatusDescription").Visible = False
            .Columns("TranStatus").Visible = False
            '.Columns("PaidBit").Visible = False
            .Columns("CreatedDateTime").Visible = False
            .Columns("ModifiedBy").Visible = False
            .Columns("ModifiedDateTime").Visible = False
            .Columns("RefNum").BestFit()
            .Columns("RefDate").BestFit()
            .Columns("FullName").BestFit()
            .Columns("Amount").BestFit()

            '.Columns("LoanAmount").BestFit()
            '.Columns("InterestAmount").BestFit()
            '.Columns("AmortizationAmount").BestFit()
            '.Columns("LoanAmountGranted").BestFit()

        End With
    End Sub

    Public Sub GenerateReceipt(ByVal customerid As String, ByVal paymentdate As DateTime, ByVal refbhid As Integer, ByVal preparedby As String, ByVal savehistory As String)
        Dim cnt As Integer = 0
        Try
            Dim i As Integer = dvList.FocusedRowHandle
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspPaymentPrinting"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@CustomerID", customerid)
                    .AddWithValue("@CustomerPaymentDate", paymentdate)
                    .AddWithValue("@RefBHID", refbhid)
                    .AddWithValue("@PreparedBy", preparedby)
                    .AddWithValue("@SaveToHistory", savehistory)
                End With
                cnt += .SelectCommand.ExecuteNonQuery()
            End With


        Catch ex As Exception

        End Try
    End Sub

    Public Sub PrintReceipt(ByVal customerid As String, ByVal refbhid As Integer)
        Try
            Dim dt As New DataTable
            Dim ds As New DataSet

            dt.Clear()
            dt.TableName = "PaymentReceiptTbl"

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spReport_PaymentReceipt"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@CustomerID", customerid)
                    .AddWithValue("@RefBHID", refbhid)
                End With
                dr = .SelectCommand.ExecuteReader
                ds.Tables.Add(dt)
                dt.Load(dr)


                dr.Close()
            End With
            If ds.Tables(0).Rows.Count > 0 Then
                Dim params As New List(Of Object)
                params.Add(global_PerCompanyLong)
                params.Add(global_PerName)

                If System.IO.File.Exists(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xml") Then
                    System.IO.File.Delete(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xml")
                End If

                If System.IO.File.Exists(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xsd") Then
                    System.IO.File.Delete(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xsd")
                End If

                dt.WriteXml(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xml")
                dt.WriteXmlSchema(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xsd")
                Dim rviewer As New ReportViewer

                rviewer.LoadPaymentReceiptReport(Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xsd", _
                                               Application.StartupPath + "\DXZLS_PaymentReceiptRpt.xml", _
                                               params)
            Else
                MsgBox("No records available.")
            End If
        Catch ex As Exception
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
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_DebitCreditMemobyFilter" _
                                  , dtView, bsd, dgList, dvList, False)
            HideColumn()
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnRefresh_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_DebitCreditMemobyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnNewItem_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNewItem.ItemClick
        If isAllowed(Me.Text, "Add") = "" Then MsgBox("Account not Allowed") : Exit Sub
        Dim dcmedit As New DebitCreditMemoEdit
        With dcmedit
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .isEdit = 0
            .frmList = Me
            .bsd = New BindingSource
            .bsd.DataSource = GetType(MyDebitCreditMemoRecord)
            Dim srec As MyDebitCreditMemoRecord = New MyDebitCreditMemoRecord
            'srec.CustomerID = 0
            srec.InterestRate = 0
            srec.Amount = 0
            .bsd.Add(srec)
            .refbhid = -1
            .refdate = Date.Parse(MDIParent1.tsDateTimeChanger.Text)
            .txtbtnRefNo.DataBindings.Add(New Binding("EditValue", .bsd, "ReferenceNo", True))

            .txtAmount.DataBindings.Add(New Binding("EditValue", .bsd, "Amount", True))
            .DxErrorProvider1.DataSource = .bsd
            .DxErrorProvider1.ContainerControl = dcmedit

            .TopLevel = True
            .Refresh()
            .txtCustomerName.Focus()
            .txtCustomerName.SelectAll()
        End With
    End Sub

    Private Sub DebitCreditMemo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_DebitCreditMemobyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        RibbonPage2.Visible = False
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
        Dim dcmedit As New DebitCreditMemoEdit

        With dcmedit
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .frmList = Me
            .isEdit = 1

            Dim i As Integer = dvList.FocusedRowHandle
            .bsd = New BindingSource
            .bsd.DataSource = GetType(MyDebitCreditMemoRecord)
            Dim srec As MyDebitCreditMemoRecord = New MyDebitCreditMemoRecord
            .refbhid = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString())
            .refdate = Date.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefDate")).ToString())
            srec.CustomerID = Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("CustomerID")).ToString())
            .txtRefNum.Text = dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString()
            .txtCustomerName.Text = dvList.GetRowCellValue(i, dvList.Columns("FullName")).ToString()
            .txtRemarks.Text = dvList.GetRowCellValue(i, dvList.Columns("Remarks")).ToString()
            srec.ReferenceNo = dvList.GetRowCellValue(i, dvList.Columns("ReferenceNo")).ToString()
            .txtbtnRefNo.Text = dvList.GetRowCellValue(i, dvList.Columns("ReferenceNo")).ToString()
            .txtRefID.Text = dvList.GetRowCellValue(i, dvList.Columns("ReferenceID")).ToString()
            .txtTransID.Text = dvList.GetRowCellValue(i, dvList.Columns("ReferenceTranID")).ToString()

            srec.Amount = Double.Parse(dvList.GetRowCellValue(i, dvList.Columns("Amount")).ToString())
            MsgBox(dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString().Substring(0, 2))
           

            .bsd.Add(srec)

            .txtbtnRefNo.DataBindings.Add(New Binding("EditValue", .bsd, "ReferenceNo", True))
            .txtCustomerID.DataBindings.Add(New Binding("EditValue", .bsd, "CustomerID", True))
            .txtAmount.DataBindings.Add(New Binding("EditValue", .bsd, "Amount", True))
            If dvList.GetRowCellValue(i, dvList.Columns("RefNum")).ToString().Substring(0, 2) = "DM" Then
                .cmbMemoType.SelectedIndex = 0
            Else
                .cmbMemoType.SelectedIndex = 1
            End If
            .DxErrorProvider1.DataSource = .bsd
            .DxErrorProvider1.ContainerControl = dcmedit

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
        PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_DebitCreditMemobyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
        isEdit = False
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then MsgBox("Not Allowed to Delete") : Exit Sub
        If MsgBox("Are you sure you want to delete all the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Dim i As Integer = dvList.FocusedRowHandle
            DeleteRecord("tbl" & Me.Name & "File", "RefBHID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("RefBHID")).ToString()))
            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(Now.Year.ToString, "RefDate", cmbColumn, "spSelect_DebitCreditMemobyFilter" _
                                 , dtView, bsd, dgList, dvList)
            HideColumn()
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub btnOutstandingRpt_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnOutstandingRpt.ItemClick
        Dim rps As New ReportParameterSelector
        rps.rpttype = Me.Name.ToUpper
        rps.RptName = "OutstandingReport"
        showChildFormDLG(rps, Windows.Forms.FormBorderStyle.FixedToolWindow)
    End Sub
End Class