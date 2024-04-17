Imports DevExpress.XtraEditors.DXErrorProvider

Public Class UserAccountEdit
    Public bs As New BindingSource
    Public ulist As UserAccountList
    Public usergroupid, empid, companyid, ustatus, upassword, locationid As String
    Dim mode As New ModeEncryption.ModifiedEncyrption

    Private Sub SaveChanges(ByVal userpassword As String)
        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor
            If isAllowed(ulist.Text, "Edit") = "" Then Exit Sub

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()


            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spInsert_UserAccount"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@UserGroupID", usergroupid)
                    .AddWithValue("@PersonnelID", empid)

                    .AddWithValue("UserID", txtUserAccountID.Text)
                    .AddWithValue("@Username", txtUsername.Text)
                    .AddWithValue("@UserPassword", mode.Encrypt64(userpassword))
                    .AddWithValue("@CompanyID", companyid)
                    If ustatus = "Checked" Then
                        .AddWithValue("@UserStatus", 1)
                    Else
                        .AddWithValue("@UserStatus", 0)
                    End If

                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With


        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MsgBox("Successfully Saved.")
                Me.Cursor = Cursors.WaitCursor
                With ulist
                    PublicFilterData("", "UserID", .cmbColumn, "spSelect_UserAccountbyFilter" _
                           , .dtUserAccntView, .bsd, .dgUserAccntList, .dvUserAccntList)
                End With

                Me.Cursor = Cursors.Default
                Me.Close()
                Me.Dispose()
            End If

        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub
        If txtNewPassword.Text.Equals(txtRetypePassword.Text) Then
            If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
                SaveChanges(txtNewPassword.Text)
            End If
        End If
    End Sub

    Private Sub UserAccountEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUserAccountID.Visible = False
        If ustatus = "Checked" Then
            btnDeactivate.Text = "Deactivate"
        Else
            btnDeactivate.Text = "Activate"
        End If
    End Sub

    Private Sub btnDeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeactivate.Click
        If MsgBox("Are you sure you want to " & btnDeactivate.Text.ToLower & " this record?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            If btnDeactivate.Text = "Deactivate" Then
                ustatus = "Unchecked"
            Else
                ustatus = "Checked"
            End If
            SaveChanges(mode.Decrypt64(upassword))
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub
End Class

Public Class MyUserAccountRecord
    Implements IDXDataErrorInfo
    Private _newPassword As String
    Private _RetypePassword As String

    Public Sub New()
        MyBase.New()
    End Sub
#Region "Initialize"

    Public Property NewPassword() As String
        Get
            Return _newPassword
        End Get
        Set(ByVal value As String)
            _newPassword = value
        End Set
    End Property
    Public Property RetypePassword() As String
        Get
            Return _RetypePassword
        End Get
        Set(ByVal value As String)
            _RetypePassword = value
        End Set
    End Property

#End Region
    Public Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) _
     Implements IDXDataErrorInfo.GetPropertyError
        If (propertyName = "RetypePassword" AndAlso NewPassword.Equals(RetypePassword) = False) Then
            info.ErrorText = String.Format("The 'New Password' field must be equal to 'Retype Password' field")
        End If
    End Sub

    ' Implements the IDXDataErrorInfo.GetPropertyError method.
    Public Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
    End Sub
End Class