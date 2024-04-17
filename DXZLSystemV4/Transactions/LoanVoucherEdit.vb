Imports DevExpress.XtraEditors.DXErrorProvider

Public Class LoanVoucherEdit
    Public bsd, bsTrans As New BindingSource
    Public dtView, dsTrans As New DataSet
    Public refbhid As Integer
    Public frmList As LoanVoucher
    Public isEdit As Integer = 0
    Dim refnumpay As String

    Public Sub RefreshTransactionFile()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsTrans.Clear()
            cmbCategory.DataSource = Nothing
            cmbCategory.Items.Clear()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_TransactionFilebyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "LV")
                    .AddWithValue("@ColumnName", "TransactionCode")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsTrans)
            End With
            'MsgBox(dsCustomer.Tables(0).Columns.Count)
            For x As Integer = 1 To 2
                dsTrans.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next
            For x As Integer = 10 To 14
                dsTrans.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next

            Dim dv As DataView = dsTrans.Tables(0).DefaultView

            dv.RowFilter = "InUsed = 1"
            bsTrans.DataSource = dv
            'bsTrans.DataSource = dsTrans.Tables(0)
            cmbCategory.DataSource = bsTrans
            cmbCategory.DisplayMember = "TransactionName"
            cmbCategory.ValueMember = "TransactionID"
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            If cmbCategory.Items.Count > 0 Then
                cmbCategory.SelectedIndex = 1
                cmbCategory.SelectedIndex = 0
            End If
        End Try
    End Sub

    Private Sub SaveChanges(ByVal status As Integer)
        Dim cnt As Integer = 0
        Try

            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspLoanVoucherAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@Option", status)
                    .AddWithValue("@RefBHID", refbhid)
                    .AddWithValue("@RefNum", txtRefNum.Text)
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@TransactionID", cmbCategory.SelectedValue.ToString)
                    .AddWithValue("@RefDate", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@Terms", txtTerms.Text)
                    .AddWithValue("@Amount", txtAmount.Text)
                    .AddWithValue("@InterestRate1", txtInterest1.Text)
                    .AddWithValue("@InterestRate2", txtInterest1.Text)
                    .AddWithValue("@Remarks", txtRemarks.Text)
                    .AddWithValue("@CreatedBy", global_PerUserName)
                    .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@ModifiedBy", global_PerUserName)
                    .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                End With

                cnt += .SelectCommand.ExecuteNonQuery()

                .SelectCommand.Parameters.Clear()
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT TOP 1 RefBHID FROM vwLoanVoucherFile WHERE RefNum='" & txtRefNum.Text & "'"
                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    refnumpay = dr(0).ToString
                End If
                dr.Close()
            End With
            If status = 0 Then
                SaveChangesPaymentBH(0)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            With frmList
                PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_LoanVoucherbyFilter" _
                              , .dtView, .bsd, .dgList, .dvList)
                .HideColumn()
                .btnFirst.PerformClick()

            End With

            Me.Close()
            Me.Dispose()
        End Try
    End Sub

    Private Sub SaveChangesPaymentBH(ByVal status As Integer)
        Dim cnt As Integer = 0
        Dim refnumpaybh As String = ""
        Try
            RefreshCode("PY", "PaymentBH", txtPyRefNum, Me)
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspPaymentAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@Option", status)
                    .AddWithValue("@RefBHID", 0)
                    .AddWithValue("@RefNum", txtPyRefNum.Text)
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@TotalAmount", txtAmount.Text)
                    .AddWithValue("@RefDate", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@Remarks", "")
                    .AddWithValue("@CreatedBy", global_PerUserName)
                    .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@ModifiedBy", global_PerUserName)
                    .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                End With

                cnt += .SelectCommand.ExecuteNonQuery()

                .SelectCommand.Parameters.Clear()
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT TOP 1 RefBHID FROM vwPaymentBHFile WHERE RefNum='" & txtPyRefNum.Text & "'"
                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    refnumpaybh = dr(0).ToString
                End If

                dr.Close()

            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            SaveChangesPaymentDetail(0, refnumpaybh)
        End Try
    End Sub

    Private Sub SaveChangesPaymentDetail(ByVal status As Integer, ByVal refnumpaybh As String)
        Dim cnt As Integer = 0
        Try

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspPaymentDTAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@OptionID", status)
                    .AddWithValue("@RefBHID", refnumpaybh)
                    .AddWithValue("@RefTDID", 0)
                    .AddWithValue("@CheckRefBHID", 0)
                    .AddWithValue("@ReferenceID", refnumpay)
                    .AddWithValue("@TransactionID", cmbCategory.SelectedValue.ToString)
                    .AddWithValue("@Amount", 0)
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

            'Me.Close()
            'Me.Dispose()
        End Try
    End Sub

    Private Sub LoanVoucherEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, txtCustomerName.KeyDown, txtAmount.KeyDown, txtRemarks.KeyDown, btnSaveChanges.KeyDown, btnCancel.KeyDown, btnSearch.KeyDown, cmbCategory.KeyDown, txtInterest1.KeyDown, txtInterest2.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If

    End Sub

    Private Sub LoanVoucherEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshCode("LV", "LoanVoucher", txtRefNum, Me)
        RefreshTransactionFile()
        txtPyRefNum.Visible = False
        txtPyRefNum.ReadOnly = True
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = RTrim(LTrim(txtCustomerName.Text))
            .lvedit = Me
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

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        If cmbCategory.SelectedItem.ToString <> "" Then
            txtCategoryID.Text = cmbCategory.SelectedValue.ToString
            For Each rw As DataRow In dsTrans.Tables(0).Rows
                If cmbCategory.SelectedValue.ToString = rw("TransactionID").ToString Then
                    txtInterest1.Text = rw("InterestRate1").ToString
                    txtInterest2.Text = rw("InterestRate2").ToString
                    txtTerms.Text = rw("Terms").ToString
                    Exit For
                End If
            Next
            If txtAmount.Text.Replace(" ", "") = "" Then txtInterestAmt.Text = "0.00" : txtAmmortizationAmt.Text = "0.00" : txtLoanGrantedAmt.Text = "0.00" : Exit Sub
            Dim terms As Double = Double.Parse(txtTerms.Text)
            Dim amount As Double = Double.Parse(txtAmount.Text)
            Dim interest As Double = Double.Parse(txtInterest1.Text) / 100
            Dim interestamt As Double = Math.Round(amount * interest, 2)
            Dim amortamt As Double = Math.Round(amount / terms, 2)
            Dim loangranted As Double = Math.Round(amount - (amount * interest), 2)
            txtInterestAmt.Text = interestamt.ToString("#,##0.00")
            txtAmmortizationAmt.Text = amortamt.ToString("#,##0.00")
            txtLoanGrantedAmt.Text = loangranted.ToString("#,##0.00")
        End If
    End Sub

    Private Sub txtAmount_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyUp
        If txtAmount.Text.Replace(" ", "") = "" Then txtInterestAmt.Text = "0.00" : txtAmmortizationAmt.Text = "0.00" : txtLoanGrantedAmt.Text = "0.00" : Exit Sub
        Dim terms As Double = Double.Parse(txtTerms.Text)
        Dim amount As Double = Double.Parse(txtAmount.Text)
        Dim interest As Double = Double.Parse(txtInterest1.Text) / 100
        Dim interestamt As Double = Math.Round(amount * interest, 2)
        Dim amortamt As Double = Math.Round(amount / terms, 2)
        Dim loangranted As Double = Math.Round(amount - (amount * interest), 2)
        txtInterestAmt.Text = interestamt.ToString("#,##0.00")
        txtAmmortizationAmt.Text = amortamt.ToString("#,##0.00")
        txtLoanGrantedAmt.Text = loangranted.ToString("#,##0.00")
    End Sub
End Class


Public Class MyLoanVoucherRecord
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