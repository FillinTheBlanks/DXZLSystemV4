Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports System.Collections

Public Class PaymentEdit
    Public bsd, bsPaymentd, bsCheck, bsSorted As New BindingSource
    Public dtView, dsPaymentd, dsCheck As New DataSet
    Dim dsSorted As DataTable
    Public refbhid As Integer
    Public refdate As DateTime
    Public frmList As Payment
    Public isEdit As Integer = 0
    Private WithEvents myLookAddCheckButton As New RepositoryItemButtonEdit
    Private WithEvents myLookAddUnpaidBalance As New RepositoryItemButtonEdit

    Public Sub HideColumn()

        If dvList.RowCount > 0 Then

            dvList.OptionsView.ShowFooter = True
            ' dvList.Columns("Amount").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            'dvList.Columns("RefBHID").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            'lblMessage1.Text = "Total Record/s:"
            'lblMessage2.Text = "Total Payment/s:"
            'lblResult1.Text = Double.Parse(dvList.Columns("CustomerID").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
            'lblResult2.Text = Double.Parse(dvList.Columns("TotalAmount").SummaryItem.SummaryValue.ToString).ToString("##,##0.00")
        End If
        With dvList
            .Columns("RefBHID").Visible = False
            '.Columns("RefTDID").Visible = False
            .Columns("RefNum").Visible = False
            .Columns("CreatedBy").Visible = False
            .Columns("CreatedDateTime").Visible = False
            .Columns("ModifiedBy").Visible = False
            .Columns("ModifiedDateTime").Visible = False
            .Columns("ReferenceNumber").BestFit()
            .Columns("Amount").BestFit()
            .Columns("CheckRefBHID").OptionsColumn.ReadOnly = True
            .Columns("CheckDate").OptionsColumn.ReadOnly = True
            .Columns("ReferenceID").OptionsColumn.ReadOnly = True
            .Columns("ReferenceNumber").OptionsColumn.ReadOnly = True
        End With
    End Sub

    Public Sub RefreshPaymentDetailFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsPaymentd.Clear()
            dgList.DataSource = Nothing


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_PaymentDetailbyFilter"
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
            dgList.BeginUpdate()

            dgList.DataSource = bsPaymentd
            dgList.EndUpdate()
            dvList.OptionsBehavior.ReadOnly = False
            dvList.MoveFirst()

            txtbtnCheckEdit.Text = dvList.GetRowCellDisplayText(dvList.FocusedRowHandle, "CheckRefBHID").ToString
            'With myLookAddCheckButton.Buttons(0)
            '    .Caption = " "
            '    .Image = Global.DXZLSystemV4.My.Resources.Zoom_16x16
            '    .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            '    '.Width = 15
            'End With
            'dvList.Columns("CheckNumber").ColumnEdit = myLookAddCheckButton
            'dvList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways

            'With myLookAddUnpaidBalance.Buttons(0)
            '    .Caption = " "
            '    .Image = Global.DXZLSystemV4.My.Resources.Zoom_16x16
            '    .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            '    '.Width = 15
            'End With
            'dvList.Columns("ReferenceNumber").ColumnEdit = myLookAddUnpaidBalance
            'dvList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
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
            Dim fullamt As Double = Double.Parse(txtAmount.Text)
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspPaymentAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@Option", status)
                    .AddWithValue("@RefBHID", refbhid)
                    .AddWithValue("@RefNum", txtRefNum.Text)
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@RefDate", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@Remarks", txtRemarks.Text)
                    .AddWithValue("@TotalAmount", txtAmount.Text)
                    .AddWithValue("@CreatedBy", global_PerUserName)
                    .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@ModifiedBy", global_PerUserName)
                    .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                End With

                cnt += .SelectCommand.ExecuteNonQuery()


                .SelectCommand.Parameters.Clear()
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT TOP 1 isnull(RefBHID,-1),RefDate FROM vwPaymentBHFile WHERE RefNum='" & txtRefNum.Text & "'"
                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    refbhid = dr.GetInt32(0)
                    refdate = dr.GetDateTime(1)
                End If

                dr.Close()

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            'RefreshPaymentDetailFile()
            Me.Cursor = Cursors.Default
            'With frmList
            '    PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_PaymentbyFilter" _
            '                  , .dtView, .bsd, .dgList, .dvList)
            '    .HideColumn()
            '    .btnFirst.PerformClick()

            'End With

            'Me.Close()
            'Me.Dispose()
        End Try
    End Sub

    Private Sub SortCheckedAmount()
        Dim Rows As New ArrayList()
        dsSorted = New DataTable()
        Dim dtSort As New DataSet
        dtSort = dsCheck.Copy

        dsSorted = dtSort.Tables(0).Clone

        Dim I As Integer
        For I = 0 To dvList.SelectedRowsCount() - 1
            If (dvList.GetSelectedRows()(I) >= 0) Then
                Rows.Add(dvList.GetDataRow(dvList.GetSelectedRows()(I)))
            End If
        Next

        dsSorted.Rows.Clear()

        For I = 0 To Rows.Count - 1
            Dim Row As DataRow = CType(Rows(I), DataRow)

            dsSorted.Rows.Add(Row.ItemArray)
        Next
        dsSorted.AcceptChanges()

        Dim dv As New DataView(dsSorted)
        dv.Sort = "TotalBalance ASC"
        dgList.BeginUpdate()
        dgList.DataSource = Nothing
        dgList.DataSource = dv
        dgList.EndUpdate()
        dvList.SelectAll()

    End Sub

    Private Sub SaveChangesDetail(ByVal status As Integer)
        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor
            If refbhid = -1 Then
                SaveChanges(isEdit)
            End If
            If isEdit > 0 Then Exit Sub
            SortCheckedAmount()
            Dim Rows As New ArrayList()
            Dim fullamt As Double = Double.Parse(txtAmount.Text)

            Dim I As Integer
            For I = 0 To dvList.SelectedRowsCount() - 1
                If (dvList.GetSelectedRows()(I) >= 0) Then
                    Rows.Add(dvList.GetDataRow(dvList.GetSelectedRows()(I)))
                End If
            Next

            'LV First to Deduct the Payment Amount before Others
            For I = 0 To Rows.Count - 1
                Dim Row As DataRow = CType(Rows(I), DataRow)

                If fullamt > 0 Then
                    If Row("TransactionCode").ToString = "LV-RS" Then

                        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
                        With dacp
                            Dim balance As Double = Double.Parse(Row("TotalBalance").ToString)
                            Dim amount As Double = Double.Parse(Row("Amortization").ToString)

                            If balance < amount Then
                                amount = balance
                            End If

                            If amount > fullamt Then
                                amount = amount - (amount - fullamt)
                                fullamt = 0
                            Else
                                fullamt = fullamt - amount
                            End If

                            .SelectCommand.CommandType = CommandType.StoredProcedure
                            .SelectCommand.CommandText = "uspPaymentDTAE"
                            With .SelectCommand.Parameters
                                .Clear()
                                .AddWithValue("@OptionID", status)
                                .AddWithValue("@RefBHID", refbhid)
                                .AddWithValue("@RefTDID", 0)
                                .AddWithValue("@CheckRefBHID", txtbtnCheckEdit.Text)
                                .AddWithValue("@ReferenceID", Row("RefBHID").ToString)
                                .AddWithValue("@TransactionID", Row("TransactionID").ToString)
                                .AddWithValue("@Amount", amount)
                                .AddWithValue("@CreatedBy", global_PerUserName)
                                .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                                .AddWithValue("@ModifiedBy", global_PerUserName)
                                .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                            End With

                            cnt += .SelectCommand.ExecuteNonQuery()
                        End With
                    End If

                End If
            Next
            'Other Loans to Deduct the Left Payment Amount
            For I = 0 To Rows.Count - 1
                Dim Row As DataRow = CType(Rows(I), DataRow)

                If fullamt > 0 Then
                    If Row("TransactionCode").ToString <> "LV-RS" Then

                        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
                        With dacp
                            Dim amount As Double = Double.Parse(Row("TotalBalance").ToString)
                            If amount > fullamt Then
                                amount = amount - (amount - fullamt)
                                fullamt = 0
                            Else
                                fullamt = fullamt - amount
                            End If

                            .SelectCommand.CommandType = CommandType.StoredProcedure
                            .SelectCommand.CommandText = "uspPaymentDTAE"
                            With .SelectCommand.Parameters
                                .Clear()
                                .AddWithValue("@OptionID", status)
                                .AddWithValue("@RefBHID", refbhid)
                                .AddWithValue("@RefTDID", 0)
                                .AddWithValue("@CheckRefBHID", txtbtnCheckEdit.Text)
                                .AddWithValue("@ReferenceID", Row("RefBHID").ToString)
                                .AddWithValue("@TransactionID", Row("TransactionID").ToString)
                                .AddWithValue("@Amount", amount)
                                .AddWithValue("@CreatedBy", global_PerUserName)
                                .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                                .AddWithValue("@ModifiedBy", global_PerUserName)
                                .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                            End With

                            cnt += .SelectCommand.ExecuteNonQuery()
                        End With
                    End If

                End If
            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            isEdit = 1
            'RefreshPaymentDetailFile()
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

    Public Sub RefreshUnpaidBalanceFile(ByVal customerid As Integer, Optional ByVal trantype As String = "")
        Try
            Me.Cursor = Cursors.WaitCursor
            dsCheck.Clear()
            dgList.DataSource = Nothing
            dvList.OptionsSelection.CheckBoxSelectorColumnWidth = 10

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

            For x As Integer = 3 To 4
                dsCheck.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next

            Dim dv As DataView = dsCheck.Tables(0).DefaultView

            dv.RowFilter = "TransactionCode LIKE '%" & trantype & "%'"
            bsCheck.DataSource = dv
            'bsCheck.DataSource = dsCheck.Tables(0)
            dgList.DataSource = bsCheck
            dvList.OptionsBehavior.ReadOnly = False
            With dvList
                .Columns("TransactionCode").Visible = False
                '.Columns("Amortization").Visible = False
            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Public Sub SetRowCellValue(ByVal bhid As String, ByVal checkno As String, ByVal checkdate As Date)
        Dim i As Integer = dvList.FocusedRowHandle
        dvList.SetRowCellValue(i, "CheckRefBHID", bhid)
        dvList.SetRowCellValue(i, "CheckNumber", checkno)
        dvList.SetRowCellValue(i, "CheckDate", checkdate)
    End Sub

    Private Sub PaymentEdit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, dvList.KeyDown, txtAmount.KeyDown, _
        txtCustomerName.KeyDown, txtRemarks.KeyDown, dvList.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub PaymentEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshCode("PY", "PaymentBH", txtRefNum, Me)
        If isEdit > 0 Then

            RefreshPaymentDetailFile()
            HideColumn()
        End If
        DxErrorProvider1.DataSource = bsd.DataSource

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = txtCustomerName.Text
            .pyedit = Me
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
            'SimpleButton1.PerformClick()
            SaveChangesDetail(isEdit)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub AddRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        dvList.AddNewRow()
        isEdit = 0
        dvList.FocusedColumn = dvList.VisibleColumns(0)
        dvList.SetFocusedRowCellValue("CheckRefBHID", 0)
        dvList.SetFocusedRowCellValue("RefTDID", 0)
        'MsgBox(dvList.VisibleColumns(0).VisibleIndex)
    End Sub

    Private Sub myLookAddCheckButton_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles myLookAddCheckButton.ButtonClick

        Dim chedit As New Check
        With chedit
            'chedit.MdiParent = Me
            .pyedit = Me
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
            .pyedit = Me
            .frmList = Me.Name
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()

        End With
    End Sub

    Private Sub dvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dvList.KeyDown
        If e.KeyData = Keys.Enter Then
            btnSaveChanges.PerformClick()
        End If
    End Sub

    Private Sub SaveChangesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveChangesToolStripMenuItem.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub
        btnSaveChanges.PerformClick()
    End Sub

    Private Sub btnPrintReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReceipt.Click
        With frmList
            QuickComputationBalance()
            .GenerateReceipt(txtCustomerID.Text, refdate, refbhid, global_PerName, "N")
            .PrintReceipt(txtCustomerID.Text, refbhid)
        End With
    End Sub

    Private Sub txtbtnCheckEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtbtnCheckEdit.ButtonClick
        Dim chedit As New Check
        With chedit
            'chedit.MdiParent = Me
            .pyedit = Me
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .txtCustomerID.Text = txtCustomerID.Text
            .txtCustomerName.Text = txtCustomerName.Text
            .focusedRow = dvList.FocusedRowHandle
            .Show()
            .dvList.AddNewRow()


        End With
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs)
        Dim Rows As New ArrayList()
        dsSorted = New DataTable()
        Dim dtSort As New DataSet
        dtSort = dsCheck.Copy

        dsSorted = dtSort.Tables(0).Clone

        Dim I As Integer
        For I = 0 To dvList.SelectedRowsCount() - 1
            If (dvList.GetSelectedRows()(I) >= 0) Then
                Rows.Add(dvList.GetDataRow(dvList.GetSelectedRows()(I)))
            End If
        Next

        dsSorted.Rows.Clear()

        For I = 0 To Rows.Count - 1
            Dim Row As DataRow = CType(Rows(I), DataRow)

            dsSorted.Rows.Add(Row.ItemArray)
        Next
        dsSorted.AcceptChanges()

        Dim dv As New DataView(dsSorted)
        dv.Sort = "TotalBalance ASC"
        dgList.BeginUpdate()
        dgList.DataSource = Nothing
        dgList.DataSource = dv
        dgList.EndUpdate()
        'System.Threading.Thread.Sleep(3000)
    End Sub

    Private Sub dvList_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles dvList.SelectionChanged
        If dvList.SelectedRowsCount = 0 Then txtTotalAmtChecked.Text = "0.00" : txtRemainingAmt.Text = Double.Parse(txtAmount.Text).ToString("#,##0.00") : Exit Sub
        If isEdit = 0 Then
            Dim Rows As New ArrayList()
            Dim fullamt As Double = Double.Parse(txtAmount.Text)
            Dim totalcheckedamt As Double = 0
            Dim I As Integer
            For I = 0 To dvList.SelectedRowsCount() - 1
                If (dvList.GetSelectedRows()(I) >= 0) Then
                    Rows.Add(dvList.GetDataRow(dvList.GetSelectedRows()(I)))
                End If
            Next

            For I = 0 To Rows.Count - 1
                Dim Row As DataRow = CType(Rows(I), DataRow)
                totalcheckedamt += Double.Parse(Row("TotalBalance").ToString)
            Next

            txtTotalAmtChecked.Text = totalcheckedamt.ToString("#,##0.00")
            If (fullamt - totalcheckedamt) < 0 Then
                txtRemainingAmt.Text = "0.00"
            Else
                txtRemainingAmt.Text = (fullamt - totalcheckedamt).ToString("#,##0.00")
            End If
        Else

        End If
        

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        'RefreshPaymentDetailFile()
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        btnPrintReceipt.PerformClick()
    End Sub

    Private Sub SaveChangesToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles SaveChangesToolStripMenuItem1.Click
        btnSaveChanges.PerformClick()
    End Sub
End Class


Public Class MyPaymentRecord
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