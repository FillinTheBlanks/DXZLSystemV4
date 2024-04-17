Public Class UnpaidBalanceList 
    Public pyedit As PaymentEdit
    Public dbmedit As DebitCreditMemoEdit
    Public reloan As ReloanPaymentEdit
    Public rlvedit As ReloanPaymentEdit1
    Dim bsCheck, bsLV As New BindingSource
    Dim dsCheck, dsLV As New DataSet
    Public customerid As Integer
    Public customername As String
    Public trantype As String = ""
    Public frmList As String = ""
    Public school, district, dateTrans As String


    Public Sub RefreshCheckFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsCheck.Clear()
            dgList.DataSource = Nothing


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_UnpaidBalanceforPaymentbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", customerid)
                    .AddWithValue("@ColumnName", "CustomerID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsCheck)
            End With

            For x As Integer = 3 To 5
                dsCheck.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next
          
            Dim dv As DataView = dsCheck.Tables(0).DefaultView

            dv.RowFilter = "TransactionCode LIKE '%" & trantype & "%'"
            bsCheck.DataSource = dv
            'bsCheck.DataSource = dsCheck.Tables(0)
            dgList.DataSource = bsCheck
            dvList.OptionsBehavior.ReadOnly = False

        Catch ex As Exception
            'MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub GetLVDetailsbyRefBHID(ByVal refbhid As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            dsLV.Clear()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_LoanVoucherbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", refbhid)
                    .AddWithValue("@ColumnName", "RefBHID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsLV)
            End With

            bsLV.DataSource = dsLV.Tables(0)
           
        Catch ex As Exception
            'MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SelectItemfromGridView()
        If dvList.SelectedRowsCount > 0 And dvList.GetFocusedRowCellValue("RefBHID").ToString <> "" Then
            Dim i As Integer = dvList.FocusedRowHandle
            If frmList = "PaymentEdit" Then
                With pyedit
                    Dim rh As Integer = .dvList.FocusedRowHandle
                    .dvList.SetRowCellValue(rh, "ReferenceID", dvList.GetRowCellValue(i, "RefBHID").ToString)
                    .dvList.SetRowCellValue(rh, "ReferenceNumber", dvList.GetRowCellValue(i, "RefNum").ToString)
                    .dvList.SetRowCellValue(rh, "TransactionID", dvList.GetRowCellValue(i, "TransactionID").ToString)
                    .dvList.SetRowCellValue(rh, "TransactionCode", dvList.GetRowCellValue(i, "TransactionCode").ToString.Replace("-RS", ""))
                End With
                Me.Close()
                Me.Dispose()
            ElseIf frmList = "ReloanPaymentEdit1" Then
                Dim dt As DataTable = dsCheck.Tables(0).Copy
                Dim dv As DataView = dt.DefaultView
                GetLVDetailsbyRefBHID(dvList.GetRowCellValue(i, "RefBHID").ToString)
                dv.RowFilter = "TransactionCode LIKE 'CV%'"
                Dim lvbalance As Double = Double.Parse(dvList.GetRowCellValue(i, "TotalBalance").ToString)
                Dim cvbalance As Double = Double.Parse(dt.Compute("Sum(TotalBalance)", dv.RowFilter).ToString)
                Dim loangranted As Double = Double.Parse(dsLV.Tables(0).Rows(0)("LoanAmountGranted").ToString)
                Dim loanamount As Double = Double.Parse(dsLV.Tables(0).Rows(0)("LoanAmount").ToString)
                Dim interestamount As Double = Double.Parse(dsLV.Tables(0).Rows(0)("InterestAmount").ToString)
                Dim amortization As Double = Double.Parse(dsLV.Tables(0).Rows(0)("AmortizationAmount").ToString)
                Dim netproceed As Double = loangranted - (lvbalance + cvbalance)
                If netproceed < 0 Then
                    netproceed = 0
                End If
                With rlvedit
                    .refbhid = Integer.Parse(dvList.GetRowCellValue(i, "RefBHID").ToString)
                    .txtRefID.Text = dvList.GetRowCellValue(i, "RefBHID").ToString
                    .txtbtnRefNo.Text = dvList.GetRowCellValue(i, "RefNum").ToString
                    .txtAmortization.Text = amortization.ToString("#,##0.00")
                    .txtRefDate.Text = dvList.GetRowCellValue(i, "RefDate").ToString
                    .txtLVBalance.Text = lvbalance.ToString("#,##0.00")
                    .txtCVBalance.Text = cvbalance.ToString("#,##0.00")
                    .txtInterestAmount.Text = interestamount.ToString("#,##0.00")
                    .txtLoanAmount.Text = loanamount.ToString("#,##0.00")
                    .txtLoanAmountGranted.Text = loangranted.ToString("#,##0.00")
                    .txtNetProceed.Text = netproceed.ToString("#,##0.00")
                    .dsCheck = dsCheck
                    Dim dv1 As New DataView(dsCheck.Tables(0))
                    dv1.Sort = "TotalBalance ASC"
                    .bsd.DataSource = dv1
                    .dgList.DataSource = .bsd

                End With


                Me.Close()
                Me.Dispose()
            ElseIf frmList = "DebitCreditMemoEdit" Then
                With dbmedit
                    'Dim rh As Integer = .dvList.FocusedRowHandle
                    .txtRefID.Text = dvList.GetRowCellValue(i, "RefBHID").ToString
                    .txtbtnRefNo.Text = dvList.GetRowCellValue(i, "RefNum").ToString
                    '.txtTotalBal.Text = Double.Parse(dvList.GetRowCellValue(i, "TotalBalance").ToString).ToString("#,##0.00")
                    .txtTransID.Text = dvList.GetRowCellValue(i, "TransactionID").ToString
                    '.txtRefDate.Text = dvList.GetRowCellValue(i, "RefDate").ToString
                End With
                Me.Close()
                Me.Dispose()
            End If

        End If
    End Sub

    Private Sub UnpaidBalanceList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, dvList.KeyDown, dgList.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        ElseIf e.KeyData = Keys.Enter Then
            SelectItemfromGridView()
        End If
    End Sub

    Private Sub UnpaidBalanceList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Unpaid Balance List of " & customername
        RefreshCheckFile()
        dvList.OptionsBehavior.ReadOnly = True
        dvList.OptionsBehavior.Editable = False
    End Sub

    Private Sub dvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvList.DoubleClick
        SelectItemfromGridView()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
        'Dim params As New List(Of Object)
        'With params
        '    .Add(customername)
        '    .Add(school)
        '    .Add(district)
        '    .Add(dateTrans)
        'End With
        'Dim dt As DataTable = dsCheck.Tables(0).Clone

        'PrintOutstandingReport(dt, params)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Text = "Unpaid Balance List of " & customername
        RefreshCheckFile()
        dvList.OptionsBehavior.ReadOnly = True
        dvList.OptionsBehavior.Editable = False
    End Sub
End Class