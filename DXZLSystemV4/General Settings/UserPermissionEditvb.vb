Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class UserPermissionEditvb
    Dim dtNavGroup As New DataSet
    Dim dtTreeList As New DataSet
    Dim bs As New BindingSource
    Dim bsl As New BindingSource
    Public frmUserPermList As New UserGroupPermissionList
    Public isEdit As Boolean

    Private Sub RefreshData()
        dtNavGroup.Clear()
        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
        With dacp
            .SelectCommand.CommandType = CommandType.Text
            .SelectCommand.CommandText = "SELECT  NavGroupDesc FROM vw_NavGroup ORDER BY NavGroupDesc"
            .AcceptChangesDuringFill = True
            .Fill(dtNavGroup)
        End With

        For Each r As DataRow In dtNavGroup.Tables(0).Rows
            cmbNavGroup.Properties.Items.Add(r(0))
        Next

        If cmbNavGroup.Properties.Items.Count > 0 Then
            cmbNavGroup.SelectedIndex = 0
        End If
    End Sub

    Private Sub RefreshTreeListData(ByVal navgrp As String)
        Try
            Me.Cursor = Cursors.WaitCursor

            dtTreeList.Clear()
            dgTreeList.BeginUpdate()

            dtTreeList.Clear()
            dgTreeList.Controls.Clear()
            dgTreeList.DataSource = Nothing
            dgTreeList.Refresh()
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_TreeControlbyNavGroup"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("UserGroupID", txtUserGroupID.Text)
                    .AddWithValue("@NavGroupDesc", navgrp)
                End With
                .AcceptChangesDuringFill = True
                .Fill(dtTreeList)
            End With

            bsl.DataSource = dtTreeList.Tables(0)

            dgTreeList.DataSource = bsl.DataSource
            dgTreeList.Refresh()
            dgTreeList.EndUpdate()
            dvTreeList.BestFitColumns()

            For i As Integer = 0 To dvTreeList.Columns.Count - 6
                dvTreeList.Columns(i).OptionsColumn.ReadOnly = True
            Next

        Catch ex As Exception
        Finally
            DxErrorProviderList.DataSource = bsl
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub SaveChanges()
        Dim cnt As Integer = 0
        Try

            Me.Cursor = Cursors.WaitCursor
            For i As Integer = 0 To dvTreeList.DataRowCount - 1

                If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
                With dacp.SelectCommand
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "spInsert_PermissionSet"
                    With .Parameters
                        .Clear()
                        .AddWithValue("@UserGroupID", txtUserGroupID.Text)
                        .AddWithValue("@TreeID", dvTreeList.GetRowCellValue(i, "Tree ID"))
                        .AddWithValue("@CanAccess", dvTreeList.GetRowCellValue(i, "Access"))
                        .AddWithValue("@CanAdd", dvTreeList.GetRowCellValue(i, "Add"))
                        .AddWithValue("@CanEdit", dvTreeList.GetRowCellValue(i, "Edit"))
                        .AddWithValue("@CanDelete", dvTreeList.GetRowCellValue(i, "Delete"))
                        .AddWithValue("@CanPrint", dvTreeList.GetRowCellValue(i, "Print"))
                    End With
                    cnt = .ExecuteNonQuery()
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MDIParent1.btnRefresh.PerformClick()
                UserGroupPermissionList.RefreshToolStripMenuItem.PerformClick()
                Me.Close()
                Me.Dispose()
            End If
        End Try
    End Sub

    Private Sub SaveChangesperChangeStatAccess(ByVal i As Integer)
        Try
            Me.Cursor = Cursors.WaitCursor


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp.SelectCommand
                .CommandType = CommandType.StoredProcedure
                .CommandText = "spInsert_PermissionSet"
                With .Parameters
                    .Clear()
                    .AddWithValue("@UserGroupID", txtUserGroupID.Text)
                    .AddWithValue("@TreeID", dvTreeList.GetRowCellValue(i, "Tree ID"))
                    .AddWithValue("@CanAccess", dvTreeList.GetRowCellValue(i, "Access"))
                    If dvTreeList.GetRowCellValue(i, "Access").ToString = "True" Then
                        .AddWithValue("@CanAdd", 1)
                        .AddWithValue("@CanEdit", 1)
                        .AddWithValue("@CanDelete", 1)
                        .AddWithValue("@CanPrint", 1)
                    ElseIf dvTreeList.GetRowCellValue(i, "Access").ToString = "False" Then
                        .AddWithValue("@CanAdd", 0)
                        .AddWithValue("@CanEdit", 0)
                        .AddWithValue("@CanDelete", 0)
                        .AddWithValue("@CanPrint", 0)
                    Else
                        .AddWithValue("@CanAdd", dvTreeList.GetRowCellValue(i, "Add"))
                        .AddWithValue("@CanEdit", dvTreeList.GetRowCellValue(i, "Edit"))
                        .AddWithValue("@CanDelete", dvTreeList.GetRowCellValue(i, "Delete"))
                        .AddWithValue("@CanPrint", dvTreeList.GetRowCellValue(i, "Print"))
                    End If

                End With
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            RefreshTreeListData(cmbNavGroup.SelectedItem.ToString)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SaveChangesperChangeStat(ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim i As Integer = e.RowHandle

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp.SelectCommand
                .CommandType = CommandType.StoredProcedure
                .CommandText = "spInsert_PermissionSet"
                With .Parameters
                    .Clear()
                    .AddWithValue("@UserGroupID", txtUserGroupID.Text)
                    .AddWithValue("@TreeID", dvTreeList.GetRowCellValue(i, "Tree ID"))
                    .AddWithValue("@CanAccess", dvTreeList.GetRowCellValue(i, "Access"))
                    .AddWithValue("@CanAdd", dvTreeList.GetRowCellValue(i, "Add"))
                    .AddWithValue("@CanEdit", dvTreeList.GetRowCellValue(i, "Edit"))
                    .AddWithValue("@CanDelete", dvTreeList.GetRowCellValue(i, "Delete"))
                    .AddWithValue("@CanPrint", dvTreeList.GetRowCellValue(i, "Print"))

                End With
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            RefreshTreeListData(cmbNavGroup.SelectedItem.ToString)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Shared Sub DoRowDoubleClick(ByVal view As DevExpress.XtraGrid.Views.Grid.GridView, ByVal pt As Point)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        If info.InRow OrElse info.InRowCell Then
            Dim colCaption As String
            If info.Column Is Nothing Then
                colCaption = "N/A"
            Else
                colCaption = info.Column.GetCaption()
            End If
            Dim indx As Integer = Integer.Parse(info.RowHandle.ToString)
            MessageBox.Show(String.Format("DoubleClick on row: {0}, column: {1}, rowname: {2}.", info.RowHandle, colCaption, view.GetFocusedRowCellValue("Tree Name").ToString))

        End If
    End Sub

    Private Sub cmbNavGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNavGroup.SelectedIndexChanged
        If cmbNavGroup.SelectedItem.ToString <> "" Then
            RefreshTreeListData(cmbNavGroup.SelectedItem.ToString)
        End If
    End Sub

    Private Sub UserPermissionEditvb_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        RefreshData()
        bs = New BindingSource
        bs.DataSource = GetType(MyPermissionRecord)
        Me.txtUserGroupID.DataBindings.Add(New Binding("EditValue", bs, "UserGroupID", True))
        Me.txtUserGroupName.DataBindings.Add(New Binding("EditValue", bs, "UserGroupName", True))
        Me.txtUserRole.DataBindings.Add(New Binding("EditValue", bs, "UserRole", True))

        DxErrorProvider1.DataSource = bs
        DxErrorProvider1.ContainerControl = Me
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If DxErrorProvider1.HasErrors = True OrElse DxErrorProviderList.HasErrors = True Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshData()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub dvTreeList_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles dvTreeList.FocusedRowChanged
        Dim pt As Point = dvTreeList.GridControl.PointToClient(Control.MousePosition)
        Dim colCaption As String = ""
        Dim info As GridHitInfo = dvTreeList.CalcHitInfo(pt)
        If info.InRow OrElse info.InRowCell Then
            Dim indx As Integer = Integer.Parse(info.RowHandle.ToString)

            If info.Column Is Nothing Then
                colCaption = "N/A"
            Else
                colCaption = info.Column.GetCaption()
            End If
        End If
        'MsgBox(info.RowHandle.ToString)
        If info.RowHandle < 0 Then Exit Sub
        If colCaption = "Access" Then
            Dim bool As Boolean = CType(dvTreeList.GetRowCellValue(info.RowHandle, "Access").ToString, Boolean)
            If bool = True Then
                bool = False
            Else
                bool = True
            End If
            dvTreeList.SetRowCellValue(info.RowHandle, "Add", bool)
            dvTreeList.SetRowCellValue(info.RowHandle, "Edit", bool)
            dvTreeList.SetRowCellValue(info.RowHandle, "Delete", bool)
            dvTreeList.SetRowCellValue(info.RowHandle, "Print", bool)
        End If

    End Sub
End Class

Public Class MyPermissionRecord
    Implements IDXDataErrorInfo
    Private _userGroupID As String
    Private _userGroupName As String
    Private _userRole As String
    Private _navgroup As String
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property UserGroupID() As String
        Get
            Return _userGroupID
        End Get
        Set(ByVal value As String)
            _userGroupID = value
        End Set
    End Property
    Public Property UserGroupName() As String
        Get
            Return _userGroupName
        End Get
        Set(ByVal value As String)
            _userGroupName = value
        End Set
    End Property
    Public Property UserRole() As String
        Get
            Return _userRole
        End Get
        Set(ByVal value As String)
            _userRole = value
        End Set
    End Property
    Public Property NavGroup() As String
        Get
            Return _navgroup
        End Get
        Set(ByVal value As String)
            _navgroup = value
        End Set
    End Property

    Public Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) _
     Implements IDXDataErrorInfo.GetPropertyError
        If ((propertyName = "UserGroupID" AndAlso UserGroupID = "") _
            OrElse (propertyName = "UserGroupName" AndAlso UserGroupName = "") _
            OrElse (propertyName = "UserRole" AndAlso UserRole = "") _
            OrElse (propertyName = "NavGroup" AndAlso NavGroup = "")) Then
            info.ErrorText = String.Format("The '{0}' field cannot be empty", propertyName)
        End If
    End Sub

    ' Implements the IDXDataErrorInfo.GetPropertyError method.
    Public Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
    End Sub
End Class