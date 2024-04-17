Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository

Public Class ReloanPaymentEdit
    Public bsd, bsPaymentd As New BindingSource
    Public dtView, dsPaymentd, dsTrans As New DataSet
    Public refbhid As Integer
    Dim term, amt, intrst As Double
    Public frmList As ReloanPayment
    Public isEdit As Integer = 0
    Private WithEvents myLookAddCheckButton As New RepositoryItemButtonEdit
    Private WithEvents myLookAddUnpaidBalance, myLookAddUnpaidBalance1 As New RepositoryItemButtonEdit

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
            .Columns("RefBHID").Visible = False
            .Columns("RefTDID").Visible = False
            .Columns("TranType").Visible = False
            .Columns("TranTypeDescription").Visible = False
            .Columns("InterestRate").Visible = False
            '.Columns("ModifiedBy").Visible = False
            '.Columns("ModifiedDateTime").Visible = False
            '.Columns("ReferenceNumber").BestFit()
            .Columns("Amount").BestFit()
            '.Columns("CheckRefBHID").OptionsColumn.ReadOnly = True
            '.Columns("CheckDate").OptionsColumn.ReadOnly = True
            .Columns("ReferenceID").OptionsColumn.ReadOnly = True
            .Columns("ReferenceNumber").OptionsColumn.ReadOnly = True
            .Columns("LoanAmountGranted").OptionsColumn.ReadOnly = True
            .Columns("AmortizationAmount").OptionsColumn.ReadOnly = True
        End With
    End Sub

    Public Sub RefreshLoanVoucherFile(ByVal refnum As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            dsTrans.Clear()


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
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
                    term = Double.Parse(dr(11).ToString)
                    intrst = Double.Parse(dr(9).ToString)
                    amt = Double.Parse(dr(17).ToString)
                End If
            End With

            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Dim terms As Double = term
            Dim amount As Double = amt
            Dim interest As Double = intrst / 100
            Dim interestamt As Double = Math.Round(amount * interest, 2)
            Dim amortamt As Double = Math.Round(amount / terms, 2)
            Dim loangranted As Double = Math.Round(amount - (amount * interest), 2)
            dvList.SetFocusedRowCellValue("InterestRate", intrst)
            'dvList.SetFocusedRowCellValue("Interest", 0)
            'dvList.SetFocusedRowCellValue("Amount", amt)
            dvList.SetFocusedRowCellValue("AmortizationAmount", amortamt.ToString("#,##0.00"))
            dvList.SetFocusedRowCellValue("LoanAmountGranted", loangranted.ToString("#,##0.00"))
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub RefreshPaymentDetailFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsPaymentd.Clear()
            dgList.DataSource = Nothing


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_ReloanPaymentDetailbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", txtRefNum.Text)
                    .AddWithValue("@ColumnName", "RefNum")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsPaymentd)
            End With
            'MsgBox(dsCustomer.Tables(0).Columns.Count)
            'For x As Integer = 1 To 2
            '    dsPaymentd.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            'Next
            'For x As Integer = 10 To 14
            '    dsPaymentd.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            'Next

            'Dim dv As DataView = dsPaymentd.Tables(0).DefaultView

            'dv.RowFilter = "InUsed = 1"
            'bsPaymentd.DataSource = dv
            bsPaymentd.DataSource = dsPaymentd.Tables(0)
            dgList.DataSource = bsPaymentd
            dvList.OptionsBehavior.ReadOnly = False
            'With myLookAddCheckButton.Buttons(0)
            '    .Caption = " "
            '    .Image = Global.DXZLSystemV4.My.Resources.Zoom_16x16
            '    .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            '    '.Width = 15
            'End With
            'dvList.Columns("CheckNumber").ColumnEdit = myLookAddCheckButton
            'dvList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways

            With myLookAddUnpaidBalance.Buttons(0)
                .Caption = " "
                .Image = Global.DXZLSystemV4.My.Resources.Zoom_16x16
                .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                '.Width = 15
            End With
            dvList.Columns("LoanVoucherRefBHID").ColumnEdit = myLookAddUnpaidBalance
            dvList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways


            With myLookAddUnpaidBalance1.Buttons(0)
                .Caption = " "
                .Image = Global.DXZLSystemV4.My.Resources.Zoom_16x16
                .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                '.Width = 15
            End With
            dvList.Columns("ReferenceID").ColumnEdit = myLookAddUnpaidBalance1
            dvList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            HideColumn()
        End Try
    End Sub

    Public Sub QuickComputationBalance()

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()


            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspQuickComputationBalance"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@SearchCustomerID", txtCustomerID.Text)
                    .AddWithValue("@CustomerPaymentDate", MDIParent1.tsDateTimeChanger.Text)

                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SaveChanges(ByVal status As Integer)
        Dim cnt As Integer = 0
        Dim refnumpaybh As String = ""
        Try
            Dim i As Integer = dvList.FocusedRowHandle
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspCreateReloanPayment"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LoanVoucherRefBHID", dvList.GetRowCellValue(i, "LoanVoucherRefBHID"))
                    .AddWithValue("@TranType", dvList.GetRowCellValue(i, "TranType"))
                    .AddWithValue("@ReferenceID", dvList.GetRowCellValue(i, "ReferenceID"))
                    .AddWithValue("@TransactionID", dvList.GetRowCellValue(i, "TransactionID"))
                    .AddWithValue("@Amount", dvList.GetRowCellValue(i, "Amount"))
                    .AddWithValue("@Interest", dvList.GetRowCellValue(i, "Interest"))
                   
                End With

                cnt += .SelectCommand.ExecuteNonQuery()


                .SelectCommand.Parameters.Clear()
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT TOP 1 RefBHID FROM vwReloanPaymentBHFile WHERE RefNum='" & txtRefNum.Text & "'"
                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    refbhid = dr.GetInt32(0)
                End If

                dr.Close()
            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            RefreshPaymentDetailFile()
            Me.Cursor = Cursors.Default
            With frmList
                PublicFilterData(refbhid.ToString, "RefBHID", .cmbColumn, "spSelect_ReloanPaymentbyFilter" _
                              , .dtView, .bsd, .dgList, .dvList)
                .HideColumn()
                .btnFirst.PerformClick()

            End With

            'Me.Close()
            'Me.Dispose()
        End Try
    End Sub

    Private Sub SaveChangesDetail(ByVal status As Integer)
        Dim cnt As Integer = 0
        Try
            Dim i As Integer = dvList.FocusedRowHandle
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspPaymentDTAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@OptionID", status)
                    .AddWithValue("@RefBHID", refbhid)
                    .AddWithValue("@RefTDID", dvList.GetRowCellValue(i, "RefTDID"))
                    .AddWithValue("@CheckRefBHID", dvList.GetRowCellValue(i, "CheckRefBHID"))
                    .AddWithValue("@ReferenceID", dvList.GetRowCellValue(i, "ReferenceID"))
                    .AddWithValue("@TransactionID", dvList.GetRowCellValue(i, "TransactionID"))
                    .AddWithValue("@Amount", dvList.GetRowCellValue(i, "Amount"))
                    .AddWithValue("@CreatedBy", global_PerUserName)
                    .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@ModifiedBy", global_PerUserName)
                    .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                End With

                cnt += .SelectCommand.ExecuteNonQuery()
            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            MsgBox(cnt.ToString & " added.")
            RefreshPaymentDetailFile()
            Me.Cursor = Cursors.Default
            With frmList

                PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_PaymentbyFilter" _
                              , .dtView, .bsd, .dgList, .dvList)
                .HideColumn()
                .btnFirst.PerformClick()

            End With

            'Me.Close()
            'Me.Dispose()
        End Try
    End Sub

    Public Sub SetRowCellValue(ByVal bhid As String, ByVal checkno As String, ByVal checkdate As Date)
        Dim i As Integer = dvList.FocusedRowHandle
        dvList.SetRowCellValue(i, "CheckRefBHID", bhid)

        dvList.SetRowCellValue(i, "CheckNumber", checkno)
        dvList.SetRowCellValue(i, "CheckDate", checkdate)
    End Sub

    Private Sub PaymentEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshCode("PY", "PaymentBH", txtRefNum, Me)
        RefreshPaymentDetailFile()
        DxErrorProvider1.DataSource = bsd.DataSource
        'bsd.DataSource = GetType(MyBonusRecord)
        'Dim cvrec As MyBonusRecord = New MyBonusRecord
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = txtCustomerName.Text
            '.rlvedit = Me
            .frmFocus = Me.Name
            .Show()
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub

    Private Sub txtCustomerName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyDown
        If e.KeyData = Keys.Enter Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub btnSaveChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Or txtCustomerID.Text = "" Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges(isEdit)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub AddRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRowsToolStripMenuItem.Click
        dvList.AddNewRow()
        isEdit = 0
        dvList.FocusedColumn = dvList.VisibleColumns(0)
        dvList.SetFocusedRowCellValue("TranType", "RP")
        dvList.SetFocusedRowCellValue("TranTypeDescription", "RELOAN PAYMENT")
        'MsgBox(dvList.VisibleColumns(0).VisibleIndex)
    End Sub

    Private Sub myLookAddCheckButton_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles myLookAddCheckButton.ButtonClick

        Dim chedit As New Check
        With chedit
            'chedit.MdiParent = Me
            '.pyedit = Me
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .txtCustomerID.Text = txtCustomerID.Text
            .txtCustomerName.Text = txtCustomerName.Text
            .focusedRow = dvList.FocusedRowHandle
            .Show()
            .dvList.AddNewRow()
        End With
    End Sub

    Private Sub myLookAddUnpaidBalance_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles myLookAddUnpaidBalance.ButtonClick
        If txtCustomerID.Text = "" Then Exit Sub

        QuickComputationBalance()
        Dim unpaid As New UnpaidBalanceList
        With unpaid
            .customerid = Integer.Parse(txtCustomerID.Text)
            .customername = txtCustomerName.Text
            .reloan = Me
            .frmList = Me.Name
            .trantype = "LV"
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub dvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dvList.KeyDown
        If e.KeyData = Keys.Enter Then
            Me.Validate()
            If Me.DxErrorProvider1.HasErrors Then Exit Sub
            SaveChanges(isEdit)
        End If
    End Sub

    Private Sub SaveChangesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveChangesToolStripMenuItem.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub
        SaveChanges(isEdit)
    End Sub

    Private Sub myLookAddUnpaidBalance1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles myLookAddUnpaidBalance1.ButtonClick
        If txtCustomerID.Text = "" Then Exit Sub
        QuickComputationBalance()
        Dim unpaid As New UnpaidBalanceList
        With unpaid
            .customerid = Integer.Parse(txtCustomerID.Text)
            .customername = txtCustomerName.Text
            .reloan = Me
            .frmList = Me.Name & "1"
            '.trantype = "LV"
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        With frmList
            .PrintReport(txtCustomerID.Text, Integer.Parse(dvList.GetFocusedRowCellDisplayText("RefBHID").ToString))
        End With
    End Sub
End Class


Public Class MyReloanVoucherRecord
    Implements IDXDataErrorInfo
    Private _RefID As String
    Private _customerID As Integer

    Private _interestRate As Double
    Private _amount As Double
    Public Sub New()
        MyBase.New()
    End Sub
#Region "Initialize"""

    Public Property RefID() As String
        Get
            Return _RefID
        End Get
        Set(ByVal value As String)
            _RefID = value
        End Set
    End Property
    Public Property CustomerID() As Integer
        Get
            Return _customerID
        End Get
        Set(ByVal value As Integer)
            _customerID = value
        End Set
    End Property

    Public Property InterestRate() As Double
        Get
            Return _interestRate
        End Get
        Set(ByVal value As Double)
            _interestRate = value
        End Set
    End Property
    Public Property Amount() As Double
        Get
            Return _amount
        End Get
        Set(ByVal value As Double)
            _amount = value
        End Set
    End Property

#End Region
    Public Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) _
     Implements IDXDataErrorInfo.GetPropertyError
        If ((propertyName = "RefID" AndAlso RefID = "") _
            OrElse (propertyName = "CustomerID" AndAlso CustomerID = 0) _
            OrElse (propertyName = "InterestRate" AndAlso InterestRate = 0) _
             OrElse (propertyName = "Amount" AndAlso Amount = 0)) Then
            info.ErrorText = String.Format("The '{0}' field cannot be empty", propertyName)
        End If
    End Sub

    ' Implements the IDXDataErrorInfo.GetPropertyError method.
    Public Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
    End Sub
End Class