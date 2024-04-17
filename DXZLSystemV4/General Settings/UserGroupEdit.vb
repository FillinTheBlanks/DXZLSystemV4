Imports DevExpress.XtraEditors.DXErrorProvider

Public Class UserGroupEdit
    Public frmList As New UserGroupPermissionList
    Public bs As BindingSource
    Public isEdit As Boolean

    Private Sub AddTransaction()
        Dim cnt As Integer = 0
        Try

            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spInsert_UserGroup"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@UserGroupID", txtUserGroupID.Text)
                    .AddWithValue("@UserGroupName", txtUserGroupName.Text)
                    .AddWithValue("@UserRole", txtUserRole.Text)

                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            frmList.RefreshDataHeader()
            frmList.RefreshDataDetail("")
            Me.Close()
            Me.Dispose()
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            
                AddTransaction()

        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub
End Class

Public Class MyGroupRecord
    Implements IDXDataErrorInfo

    Private _groupname As String
    Private _grouprole As String
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property GroupName() As String
        Get
            Return _groupname
        End Get
        Set(ByVal value As String)
            _groupname = value
        End Set
    End Property
    Public Property GroupRole() As String
        Get
            Return _grouprole
        End Get
        Set(ByVal value As String)
            _grouprole = value
        End Set
    End Property

    Public Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) _
     Implements IDXDataErrorInfo.GetPropertyError
        If ((propertyName = "GroupName" AndAlso GroupName = "") _
        OrElse (propertyName = "GroupRole" AndAlso GroupRole = "")) Then
            info.ErrorText = String.Format("The '{0}' field cannot be empty", propertyName)
        End If
    End Sub

    ' Implements the IDXDataErrorInfo.GetPropertyError method.
    Public Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
    End Sub
End Class