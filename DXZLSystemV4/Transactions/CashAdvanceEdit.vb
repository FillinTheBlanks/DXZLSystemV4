Imports DevExpress.XtraEditors.DXErrorProvider

Public Class CashAdvanceEdit
    Public bsd As New BindingSource
    Public dtView As New DataSet
    Public refbhid As Integer
    Public frmList As CashAdvance
    Public isEdit As Integer = 0
    Dim refnumpay As String

    Private Sub SaveChanges(ByVal status As Integer)
        Dim cnt As Integer = 0
        Try
            RefreshCode("CA", "CashAdvance", txtRefNum, Me)
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspCashAdvanceAE"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@Option", status)
                    .AddWithValue("@RefBHID", refbhid)
                    .AddWithValue("@RefNum", txtRefNum.Text)
                    .AddWithValue("@CustomerID", txtCustomerID.Text)
                    .AddWithValue("@RefDate", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@Amount", txtAmount.Text)
                    .AddWithValue("@InterestRate", txtInterest.Text)
                    '.AddWithValue("@DateCutOff", "1900-01-01 00:00:00.000")
                    .AddWithValue("@TransactionID", 7)
                    .AddWithValue("@Remarks", txtRemarks.Text)
                    .AddWithValue("@CreatedBy", global_PerUserName)
                    .AddWithValue("@CreatedDateTime", MDIParent1.tsDateTimeChanger.Text)
                    .AddWithValue("@ModifiedBy", global_PerUserName)
                    .AddWithValue("@ModifiedDateTime", MDIParent1.tsDateTimeChanger.Text)
                End With

                cnt += .SelectCommand.ExecuteNonQuery()

                .SelectCommand.Parameters.Clear()
                .SelectCommand.CommandType = CommandType.Text
                .SelectCommand.CommandText = "SELECT TOP 1 RefBHID FROM vwCashAdvanceFile WHERE RefNum='" & txtRefNum.Text & "'"
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
                PublicFilterData(Now.Year.ToString, "RefDate", .cmbColumn, "spSelect_CashAdvancebyFilter" _
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
                    .AddWithValue("@TransactionID", 7)
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

    Private Sub CashAdvanceEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, txtCustomerName.KeyDown, txtAmount.KeyDown, txtRemarks.KeyDown, btnSaveChanges.KeyDown, btnCancel.KeyDown, btnSearch.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        ElseIf e.KeyData = Keys.Enter Then
            btnSaveChanges.PerformClick()
        End If
    End Sub

    Private Sub CashAdvanceEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshCode("CA", "CashAdvance", txtRefNum, Me)
        RefreshCode("PY", "PaymentBH", txtPyRefNum, Me)
        txtInterest.ReadOnly = True
        txtInterest.Text = "5"
        txtPyRefNum.Visible = False
        txtPyRefNum.ReadOnly = True
        DxErrorProvider1.DataSource = bsd.DataSource
        'bsd.DataSource = GetType(MyBonusRecord)
        'Dim cvrec As MyBonusRecord = New MyBonusRecord
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = RTrim(LTrim(txtCustomerName.Text))
            .caedit = Me
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

    Private Sub txtInterest_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtInterest.ButtonClick
        If txtInterest.ReadOnly = False Then
            txtInterest.ReadOnly = True
            txtInterest.Focus()
            txtInterest.SelectAll()
        Else
            txtInterest.ReadOnly = False
            txtAmount.Focus()
            txtAmount.SelectAll()
        End If
    End Sub

    Private Sub txtInterest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInterest.KeyDown
        If e.KeyData = Keys.Escape Then
            txtInterest.ReadOnly = True
            txtAmount.Focus()
            txtAmount.SelectAll()
        ElseIf e.KeyData = Keys.Enter And txtInterest.Text <> "" Then
            txtInterest.ReadOnly = True
            txtAmount.Focus()
            txtAmount.SelectAll()
        End If
    End Sub
End Class


Public Class MyCashAdvanceRecord
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