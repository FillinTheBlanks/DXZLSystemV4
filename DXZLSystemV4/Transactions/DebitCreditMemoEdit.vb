Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Public Class DebitCreditMemoEdit
    Public bsd, bsPaymentd As New BindingSource
    Public dtView, dsPaymentd As New DataSet
    Public refbhid As Integer
    Public refdate As DateTime
    Public frmList As DebitCreditMemo
    Public isEdit As Integer = 0
    Private WithEvents myLookAddCheckButton As New RepositoryItemButtonEdit
    Private WithEvents myLookAddUnpaidBalance As New RepositoryItemButtonEdit

    Private Sub SaveChanges(ByVal status As Integer)
        Dim cnt As Integer = 0
        Try
            RefreshCode(cmbMemoType.SelectedValue.ToString, "DebitCreditMemo", txtRefNum, Me)
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp


                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspDebitCreditMemoAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@Option", status)
                    .AddWithValue("@RefBHID", refbhid)
                    .AddWithValue("@RefNum", txtRefNum.Text)
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@ReferenceID", txtRefID.Text)
                    .AddWithValue("@ReferenceNo", txtbtnRefNo.Text)
                    .AddWithValue("@ReferenceTranID", txtTransID.Text)
                    .AddWithValue("@RefDate", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@Amount", txtAmount.Text)
                    .AddWithValue("@Remarks", txtRemarks.Text)
                    .AddWithValue("@CreatedBy", global_PerUserName)
                    .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@ModifiedBy", global_PerUserName)
                    .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                End With

                cnt += .SelectCommand.ExecuteNonQuery()

                .SelectCommand.Parameters.Clear()
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT TOP 1 RefBHID FROM vwDebitCreditMemoFile WHERE RefNum='" & txtRefNum.Text & "'"
                dr = .SelectCommand.ExecuteReader

                If dr.Read Then
                    refbhid = Integer.Parse(dr(0).ToString)
                End If
                dr.Close()
            End With
           

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            With frmList
                PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_DebitCreditMemobyFilter" _
                              , .dtView, .bsd, .dgList, .dvList)
                .HideColumn()
                .btnFirst.PerformClick()

            End With

            Me.Close()
            Me.Dispose()
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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = RTrim(LTrim(txtCustomerName.Text))
            .dbmedit = Me
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
            SaveChanges(isEdit)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub DebitCreditMemoEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, txtCustomerName.KeyDown, txtAmount.KeyDown, txtRemarks.KeyDown, btnSaveChanges.KeyDown, btnCancel.KeyDown, btnSearch.KeyDown
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

    Private Sub DebitCreditMemoEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dt As New DataTable
        dt.Columns.Add("Text", GetType(String))
        dt.Columns.Add("Value", GetType(String))
        dt.Rows.Add("Debit", "DM")
        dt.Rows.Add("Credit", "CM")
        cmbMemoType.DataSource = dt
        cmbMemoType.DisplayMember = "Text"
        cmbMemoType.ValueMember = "Value"
        cmbMemoType.SelectedIndex = 0
        txtRefID.ReadOnly = True
        txtRefNum.ReadOnly = True
        txtTransID.ReadOnly = True
        txtbtnRefNo.ReadOnly = True
        RefreshCode(cmbMemoType.SelectedValue.ToString, "DebitCreditMemo", txtRefNum, Me)
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub


    Private Sub txtbtnRefNo_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtbtnRefNo.ButtonClick
        QuickComputationBalance()
        Dim unpaid As New UnpaidBalanceList
        With unpaid
            .customerid = Integer.Parse(txtCustomerID.Text)
            .customername = txtCustomerName.Text
            .dbmedit = Me
            .frmList = Me.Name
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub cmbMemoType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMemoType.SelectedValueChanged
        If refbhid > -1 Then Exit Sub

        If cmbMemoType.Items.Count > 0 Then
            RefreshCode(cmbMemoType.SelectedValue.ToString, "DebitCreditMemo", txtRefNum, Me)
        End If
    End Sub
End Class

Public Class MyDebitCreditMemoRecord
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