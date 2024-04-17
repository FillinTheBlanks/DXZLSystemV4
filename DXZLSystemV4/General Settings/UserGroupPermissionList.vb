Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.Utils
Imports DevExpress.XtraTab

Public Class UserGroupPermissionList
    Dim bsh As New BindingSource
    Dim bsd As New BindingSource
    Public dtUserGroup As New DataSet
    Public dtUserPerm As New DataSet

    Public Sub RefreshDataHeader()
        Try
            cmbBox.Properties.Items.Clear()
            Dim index As Integer = dvUserGroup.TopRowIndex
            Dim focusedRow As Integer = dvUserGroup.FocusedRowHandle
            dgUserGroup.BeginUpdate()

            dtUserGroup.Clear()
            dgUserGroup.Controls.Clear()
            dgUserGroup.DataSource = Nothing
            dgUserGroup.Refresh()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_UserGroup"
                With .SelectCommand.Parameters
                    .Clear()
                End With

                .AcceptChangesDuringFill = True
                .Fill(dtUserGroup)
            End With



            bsh.DataSource = dtUserGroup.Tables(0)

            dgUserGroup.DataSource = bsh.DataSource
            dgUserGroup.Refresh()
            For Each col In dtUserGroup.Tables(0).Columns
                cmbBox.Properties.Items.Add(col.ToString)
            Next
            dvUserGroup.FocusedRowHandle = focusedRow + 1
            dvUserGroup.TopRowIndex = index + 1
            dgUserGroup.EndUpdate()
            dvUserGroup.BestFitColumns()
        Catch ex As Exception
            MsgBox("Error on Form: " & Me.Name & " Contact System Administrator " & ex.ToString, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try
    End Sub

    Public Sub RefreshDataDetail(ByVal groupid As String)
        Try
            Dim index As Integer = dvUserPermission.TopRowIndex
            Dim focusedRow As Integer = dvUserPermission.FocusedRowHandle
            dgUserPermission.BeginUpdate()

            dtUserPerm.Clear()
            dgUserPermission.Controls.Clear()
            dgUserPermission.DataSource = Nothing
            dgUserPermission.Refresh()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_UserGroupPermission"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@UserGroupID", groupid)
                End With

                .AcceptChangesDuringFill = True
                .Fill(dtUserPerm)
            End With


            bsd.DataSource = dtUserPerm.Tables(0)

            dgUserPermission.DataSource = bsd.DataSource
            dgUserPermission.Refresh()

            dvUserPermission.FocusedRowHandle = focusedRow + 1
            dvUserPermission.TopRowIndex = index + 1
            dgUserPermission.EndUpdate()
            dvUserPermission.BestFitColumns()
        Catch ex As Exception
            MsgBox("Error on Form: " & Me.Name & " Contact System Administrator " & ex.ToString, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try
    End Sub

    Private Sub AddUserGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddUserGroupToolStripMenuItem.Click
        Dim uge As New UserGroupEdit
        With uge
            .frmList = Me
            .bs = New BindingSource
            .bs.DataSource = GetType(MyGroupRecord)
            Dim rec As MyGroupRecord = New MyGroupRecord
            rec.GroupName = String.Empty
            rec.GroupRole = String.Empty
            .bs.Add(rec)
            .txtUserGroupID.Text = String.Empty

            .txtUserGroupName.DataBindings.Add(New Binding("EditValue", .bs, "GroupName", True))
            .txtUserRole.DataBindings.Add(New Binding("EditValue", .bs, "GroupRole", True))
            .DxErrorProvider1.DataSource = .bs
            .DxErrorProvider1.ContainerControl = uge

            .isEdit = False
            .Show()
            .TopMost = True

            .Refresh()
            .txtUserGroupName.Focus()
            .txtUserGroupName.SelectAll()
        End With
    End Sub

    Private Sub UserGroupPermissionList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefreshDataHeader()
        RefreshDataDetail("")
    End Sub

    Private Sub dvUserGroup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvUserGroup.DoubleClick
        RefreshDataDetail(dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(0)).ToString())
    End Sub

    Private Sub hot_key(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, dgUserGroup.KeyDown, dvUserGroup.KeyDown, _
       dgUserPermission.KeyDown, dvUserPermission.KeyDown, txtFilter.KeyDown, TextEdit2.KeyDown, cmbBox.KeyDown, ComboBoxEdit2.KeyDown
        If e.KeyCode = Keys.F5 Then
            RefreshDataDetail("")
            RefreshDataHeader()
        End If
    End Sub

    Private Sub EditUserGroupToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditUserGroupToolStripMenuItem.Click
        If dvUserGroup.SelectedRowsCount > 0 Then
            Dim uge As New UserGroupEdit
            With uge
                .frmList = Me
                .bs = New BindingSource
                .bs.DataSource = GetType(MyGroupRecord)
                Dim rec As MyGroupRecord = New MyGroupRecord
                rec.GroupName = dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(1)).ToString()
                rec.GroupRole = dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(2)).ToString()
                .bs.Add(rec)
                .txtUserGroupID.Text = dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(0)).ToString()

                .txtUserGroupName.DataBindings.Add(New Binding("EditValue", .bs, "GroupName", True))
                .txtUserRole.DataBindings.Add(New Binding("EditValue", .bs, "GroupRole", True))
                .DxErrorProvider1.DataSource = .bs
                .DxErrorProvider1.ContainerControl = uge

                .isEdit = True
                .Show()
                .TopMost = True

                .Refresh()
                .txtUserGroupName.Focus()
                .txtUserGroupName.SelectAll()
            End With
        End If

    End Sub

    Private Sub AddUserPermissionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddUserPermissionToolStripMenuItem1.Click
        If dvUserPermission.SelectedRowsCount > 0 Then
            Dim upedit As New UserPermissionEditvb
            With upedit
                .frmUserPermList = Me
                .txtUserGroupID.Text = dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(0)).ToString()
                .txtUserGroupName.Text = dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(1)).ToString()
                .txtUserRole.Text = dvUserGroup.GetRowCellValue(dvUserGroup.FocusedRowHandle(), dvUserGroup.Columns(2)).ToString()
                .isEdit = False
                .Show()
                .TopMost = True
                .Refresh()
                .cmbNavGroup.Focus()
            End With
        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshDataHeader()
        RefreshDataDetail("")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        MDIParent1.tabMain.TabPages.Remove(MDIParent1.GetTabPageByName(Me.Name))
    End Sub
End Class