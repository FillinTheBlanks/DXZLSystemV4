Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports System.Collections

Public Class ReloanPaymentEdit1
    Public bsd, bsPaymentd As New BindingSource
    Public dtView, dsPaymentd, dsCheck As New DataSet
    Public refbhid, rprefbhid As Integer
    Public refdate As DateTime
    Public frmList As ReloanPayment
    Dim dsSorted As DataTable
    Public isEdit As Integer = 0
    Private WithEvents myLookAddCheckButton As New RepositoryItemButtonEdit
    Private WithEvents myLookAddUnpaidBalance As New RepositoryItemButtonEdit

    Private Sub SortCheckedAmount()
        Dim dv As New DataView(dsCheck.Tables(0))
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

            If status > 0 Then Exit Sub
            'SortCheckedAmount()
            Dim Rows As New ArrayList()
            Dim fullamt As Double = Double.Parse(txtLoanAmountGranted.Text)

            Dim I As Integer
            For I = 0 To dvList.RowCount() - 1
                Rows.Add(dvList.GetDataRow((I)))
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
                            .SelectCommand.CommandText = "uspCreateReloanPayment"
                            With .SelectCommand.Parameters
                                .Clear()
                                .AddWithValue("@LoanVoucherRefBHID", refbhid)
                                .AddWithValue("@TranType", "RP")
                                .AddWithValue("@ReferenceID", dvList.GetRowCellValue(I, "RefBHID"))
                                .AddWithValue("@TransactionID", dvList.GetRowCellValue(I, "TransactionID"))
                                .AddWithValue("@Amount", amount)
                                .AddWithValue("@Interest", dvList.GetRowCellValue(I, "InterestRate1"))
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
                    If Row("TransactionCode").ToString = "CV-RS" Or Row("TransactionCode").ToString = "PC-RS" Then

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
                            .SelectCommand.CommandText = "uspCreateReloanPayment"
                            With .SelectCommand.Parameters
                                .Clear()
                                .AddWithValue("@LoanVoucherRefBHID", refbhid)
                                .AddWithValue("@TranType", "RP")
                                .AddWithValue("@ReferenceID", dvList.GetRowCellValue(I, "RefBHID"))
                                .AddWithValue("@TransactionID", dvList.GetRowCellValue(I, "TransactionID"))
                                .AddWithValue("@Amount", amount)
                                .AddWithValue("@Interest", dvList.GetRowCellValue(I, "InterestRate1"))
                            End With

                            cnt += .SelectCommand.ExecuteNonQuery()
                        End With
                    End If

                End If
            Next

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_ReloanPaymentbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", refbhid)
                    .AddWithValue("@ColumnName", "LoanVoucherRefBHID")
                End With

                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    If dr.HasRows Then
                        rprefbhid = Integer.Parse(dr(1).ToString)
                    End If
                End If
            End With
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)

        Finally
            isEdit = 1
            MsgBox(cnt & " Successfully Added.")
            Me.Cursor = Cursors.Default
            With frmList
                PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_ReloanPaymentbyFilter" _
                              , .dtView, .bsd, .dgList, .dvList)
                .HideColumn()
                .btnFirst.PerformClick()
            End With

            'Me.Close()
            'Me.Dispose()
        End Try
    End Sub

    'Private Sub SaveChanges(ByVal status As Integer)
    '    Dim cnt As Integer = 0
    '    Try

    '        Me.Cursor = Cursors.WaitCursor
    '        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
    '        With dacp


    '            .SelectCommand.CommandType = CommandType.StoredProcedure
    '            .SelectCommand.CommandText = "uspCreateReloanPayment"
    '            With .SelectCommand.Parameters
    '                .Clear()
    '                .AddWithValue("@LoanVoucherRefBHID", refbhid)
    '                .AddWithValue("@TranType", "RP")
    '                .AddWithValue("@ReferenceID", refbhid)
    '                .AddWithValue("@TransactionID", dvList.GetRowCellValue(i, "TransactionID"))
    '                .AddWithValue("@Amount", dvList.GetRowCellValue(i, "Amount"))
    '                .AddWithValue("@Interest", dvList.GetRowCellValue(i, "Interest"))
    '            End With

    '            cnt += .SelectCommand.ExecuteNonQuery()

    '            .SelectCommand.Parameters.Clear()
    '            .SelectCommand.CommandType = CommandType.Text
    '            .SelectCommand.CommandText = "SELECT TOP 1 RefBHID FROM vwDebitCreditMemoFile WHERE RefNum='" & txtRefNum.Text & "'"
    '            dr = .SelectCommand.ExecuteReader

    '            If dr.Read Then
    '                refbhid = Integer.Parse(dr(0).ToString)
    '            End If
    '            dr.Close()
    '        End With


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '        With frmList
    '            PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_DebitCreditMemobyFilter" _
    '                          , .dtView, .bsd, .dgList, .dvList)
    '            .HideColumn()
    '            .btnFirst.PerformClick()

    '        End With

    '        Me.Close()
    '        Me.Dispose()
    '    End Try
    'End Sub

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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = RTrim(LTrim(txtCustomerName.Text))
            .rlvedit = Me
            .frmFocus = Me.Name
            .Show()
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub

    Private Sub btnSaveChanges_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Or txtCustomerID.Text = "" Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChangesDetail(isEdit)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub ReloanPaymentEdit1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, txtCustomerName.KeyDown, txtLVBalance.KeyDown, txtRemarks.KeyDown, btnSaveChanges.KeyDown, btnCancel.KeyDown, btnSearch.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub txtCustomerName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyDown
        If e.KeyData = Keys.Enter Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub ReloanPaymentEdit1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtRefID.ReadOnly = True
        txtLVBalance.ReadOnly = True
        txtCVBalance.ReadOnly = True
        txtRefDate.ReadOnly = True
        txtbtnRefNo.ReadOnly = True
        txtAmortization.ReadOnly = True
        txtInterestAmount.ReadOnly = True
        txtLoanAmount.ReadOnly = True
        txtLoanAmountGranted.ReadOnly = True
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub


    Private Sub txtbtnRefNo_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtbtnRefNo.ButtonClick
        QuickComputationBalance()
        Dim unpaid As New UnpaidBalanceList
        With unpaid
            .customerid = Integer.Parse(txtCustomerID.Text)
            .customername = txtCustomerName.Text
            .rlvedit = Me
            .frmList = Me.Name
            .trantype = "LV"
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        With frmList
            .PrintReport(txtCustomerID.Text, rprefbhid)
        End With
    End Sub
End Class

Public Class MyReloanPaymentRecord
    Implements IDXDataErrorInfo
    Private _RefID As String
    Private _customerID As Integer

    Private _interestRate As Double
    Private _amount As Double
    Private _refnum As String
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

    Public Property ReferenceNo() As String
        Get
            Return _refnum
        End Get
        Set(ByVal value As String)
            _refnum = value
        End Set
    End Property

#End Region
    Public Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) _
     Implements IDXDataErrorInfo.GetPropertyError
        If ((propertyName = "RefID" AndAlso RefID = "") _
            OrElse (propertyName = "CustomerID" AndAlso CustomerID = 0) _
            OrElse (propertyName = "ReferenceNo" AndAlso ReferenceNo = "") _
             OrElse (propertyName = "Amount" AndAlso Amount = 0)) Then
            info.ErrorText = String.Format("The '{0}' field cannot be empty", propertyName)
        End If
    End Sub

    ' Implements the IDXDataErrorInfo.GetPropertyError method.
    Public Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
    End Sub
End Class