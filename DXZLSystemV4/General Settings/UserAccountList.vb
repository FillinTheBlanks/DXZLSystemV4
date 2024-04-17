Imports DevExpress.XtraEditors.Repository
Public Class UserAccountList
    Dim bsUserGroup, bsCompany, bsPersonnel, bsLocation As New BindingSource
    Dim dsUserGroup, dsCompany, dsPersonnel, dsLocation As New DataSet
    Public bsd As New BindingSource
    Public dtUserAccntView As New DataSet
    Dim isEdit As Boolean
    Friend WithEvents myLookUserGroup, myLookCompany, myLookPersonnel, myLookLocation As New RepositoryItemGridLookUpEdit

    Private Sub RefreshDataUserGroup()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsUserGroup.Clear()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_UserGroup"
                With .SelectCommand.Parameters
                    .Clear()
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsUserGroup)
            End With
            bsUserGroup.DataSource = dsUserGroup.Tables(0)

            myLookUserGroup.DisplayMember = "UserGroupName"
            myLookUserGroup.ValueMember = "UserGroupName"
            myLookUserGroup.DataSource = bsUserGroup.DataSource
            dvUserAccntList.Columns("UserGroupName").ColumnEdit = myLookUserGroup
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RefreshDataCompany()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsCompany.Clear()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_CompanybyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "1")
                    .AddWithValue("@ColumnName", "CompanyStatus")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsCompany)
            End With
            bsCompany.DataSource = dsCompany.Tables(0)

            myLookCompany.DisplayMember = "CompanyName"
            myLookCompany.ValueMember = "CompanyName"
            myLookCompany.DataSource = bsCompany.DataSource
            dvUserAccntList.Columns("CompanyName").ColumnEdit = myLookCompany
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub RefreshDataLocation(ByVal companyid As String)
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        dsLocation.Clear()

    '        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
    '        With dacp
    '            .SelectCommand.CommandType = CommandType.StoredProcedure
    '            .SelectCommand.CommandText = "spSelect_LocationbyFilter"
    '            With .SelectCommand.Parameters
    '                .Clear()
    '                .AddWithValue("@TextFilter", companyid)
    '                .AddWithValue("@ColumnName", "CompanyID")
    '            End With
    '            .AcceptChangesDuringFill = True
    '            .Fill(dsLocation)
    '        End With
    '        bsLocation.DataSource = dsLocation.Tables(0)

    '        myLookLocation.DisplayMember = "LocationDescription"
    '        myLookLocation.ValueMember = "LocationDescription"
    '        myLookLocation.DataSource = bsLocation.DataSource
    '        dvUserAccntList.Columns("LocationDescription").ColumnEdit = myLookLocation
    '        dvUserAccntList.Columns("LocationDescription").ColumnEdit.NullText = "<Select " & dvUserAccntList.Columns("LocationDescription").FieldName.ToString & ">"
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub

    Private Sub RefreshDataPersonnel(ByVal companyid As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            dsPersonnel.Clear()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_PersonnelCompleteNamebyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", companyid)
                    .AddWithValue("@ColumnName", "CompanyID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsPersonnel)
            End With
            bsPersonnel.DataSource = dsPersonnel.Tables(0)

            myLookPersonnel.DisplayMember = "PersonnelName"
            myLookPersonnel.ValueMember = "PersonnelName"
            myLookPersonnel.DataSource = bsPersonnel.DataSource
            dvUserAccntList.Columns("PersonnelName").ColumnEdit = myLookPersonnel
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub HideColumn()
        With dvUserAccntList
            .Columns("UserID").Visible = False
            .Columns("UserPassword").Visible = False

            .Columns("CompanyID").Visible = False
            .Columns("PersonnelID").Visible = False
            .Columns("UserGroupID").Visible = False
        End With
    End Sub

    Private Sub AddRow(ByVal isEdit As Boolean)
        With Me.dvUserAccntList
            .OptionsBehavior.ReadOnly = False
            .Columns("UserID").OptionsColumn.ReadOnly = True
            .Columns("UserGroupID").OptionsColumn.ReadOnly = True
            .Columns("UserRole").OptionsColumn.ReadOnly = True
            .Columns("CompanyID").OptionsColumn.ReadOnly = True
            .Columns("CompanyInitial").OptionsColumn.ReadOnly = True
            .Columns("PersonnelID").OptionsColumn.ReadOnly = True
            .Columns("UserGroupName").OptionsColumn.ReadOnly = False
            .Columns("CompanyName").OptionsColumn.ReadOnly = False
            .Columns("PersonnelName").OptionsColumn.ReadOnly = False
            .Columns("PersonnelPosition").OptionsColumn.ReadOnly = True
            .Columns("Username").OptionsColumn.ReadOnly = False
            .Columns("UserPassword").OptionsColumn.ReadOnly = False
            .Columns("UserStatus").OptionsColumn.ReadOnly = False
            If isEdit = False Then
                .AddNewRow()
            End If
        End With
    End Sub

    Private Sub SaveChanges(ByVal canedit As Boolean)
        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim mode As New ModeEncryption.ModifiedEncyrption

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            If canedit = False Then
                Dim i As Integer = dvUserAccntList.FocusedRowHandle
                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spInsert_UserAccount"
                    With .SelectCommand.Parameters
                        .Clear()
                        If dvUserAccntList.GetRowCellValue(i, "UserID").ToString = "" Then
                            .AddWithValue("@UserID", DBNull.Value)

                        Else
                            .AddWithValue("@UserID", dvUserAccntList.GetRowCellValue(i, "UserID"))
                        End If
                        If dvUserAccntList.GetRowCellValue(i, "UserGroupID").ToString = "" Then
                            .AddWithValue("@UserGroupID", DBNull.Value)

                        Else
                            .AddWithValue("@UserGroupID", dvUserAccntList.GetRowCellValue(i, "UserGroupID"))
                        End If

                        .AddWithValue("@PersonnelID", dvUserAccntList.GetRowCellValue(i, "PersonnelID"))
                        '.AddWithValue("@LocationID", dvUserAccntList.GetRowCellValue(i, "LocationID"))
                        .AddWithValue("@Username", dvUserAccntList.GetRowCellValue(i, "Username"))
                        .AddWithValue("@UserPassword", "")
                        .AddWithValue("@CompanyID", dvUserAccntList.GetRowCellValue(i, "CompanyID"))
                        .AddWithValue("@UserStatus", dvUserAccntList.GetRowCellValue(i, "UserStatus"))
                    End With
                    cnt = .SelectCommand.ExecuteNonQuery()
                End With
            Else
                'For i As Integer = 0 To dvUserAccntList.DataRowCount - 1
                Dim i As Integer = dvUserAccntList.FocusedRowHandle
                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spInsert_UserAccount"
                    With .SelectCommand.Parameters
                        .Clear()
                        .AddWithValue("@UserID", dvUserAccntList.GetRowCellValue(i, "UserID"))
                        .AddWithValue("@UserGroupID", dvUserAccntList.GetRowCellValue(i, "UserGroupID"))
                        .AddWithValue("@PersonnelID", dvUserAccntList.GetRowCellValue(i, "PersonnelID"))
                        '.AddWithValue("@LocationID", dvUserAccntList.GetRowCellValue(i, "LocationID"))
                        .AddWithValue("@Username", dvUserAccntList.GetRowCellValue(i, "Username"))
                        .AddWithValue("@UserPassword", dvUserAccntList.GetRowCellValue(i, "UserPassword"))
                        .AddWithValue("@CompanyID", dvUserAccntList.GetRowCellValue(i, "CompanyID"))
                        .AddWithValue("@UserStatus", dvUserAccntList.GetRowCellValue(i, "UserStatus"))
                    End With
                    cnt = .SelectCommand.ExecuteNonQuery()
                End With
                'Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MsgBox("Successfully Saved.")
                Me.Cursor = Cursors.WaitCursor
                PublicFilterData("", "UserID", cmbColumn, "spSelect_UserAccountbyFilter" _
                           , dtUserAccntView, bsd, dgUserAccntList, dvUserAccntList)
                Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub

    Private Sub UserAccountList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor

        PublicFilterData("", "UserID", cmbColumn, "spSelect_UserAccountbyFilter" _
                             , dtUserAccntView, bsd, dgUserAccntList, dvUserAccntList)
        RefreshDataCompany()
        RefreshDataUserGroup()
        'RefreshDataLocation("")
        RefreshDataPersonnel("")
        dvUserAccntList.OptionsBehavior.ReadOnly = True
        HideColumn()
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor


        PublicFilterData("", "UserID", cmbColumn, "spSelect_UserAccountbyFilter" _
                             , dtUserAccntView, bsd, dgUserAccntList, dvUserAccntList)
        RefreshDataCompany()
        RefreshDataUserGroup()
        'RefreshDataLocation("")
        RefreshDataPersonnel("")
        dvUserAccntList.OptionsBehavior.ReadOnly = True
        HideColumn()
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If isAllowed(Me.Text, "Print") = "" Then Exit Sub
        PrintReportTemplate(dvUserAccntList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                            , "", "END OF REPORT")
    End Sub

    Private Sub dvUserAccntList_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If dvUserAccntList.RowCount <= 0 Then Exit Sub

        If e.RowHandle >= 0 Then

            Dim cat As String = dvUserAccntList.GetRowCellDisplayText(e.RowHandle, dvUserAccntList.Columns("UserStatus")).ToString
            If cat <> "" Then
                If cat = "Checked" Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.SeaGreen
                    e.Appearance.BackColor2 = Color.LightGreen
                Else
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Tomato
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        End If
    End Sub

    Private Sub myLookCompany_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookCompany.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsCompany.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
                dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "CompanyID", rw(0))
                dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "CompanyInitial", rw("CompanyInitial"))
                'RefreshDataLocation(rw(0).ToString)
                RefreshDataPersonnel(rw(0).ToString)
            End If
        Next
    End Sub

    Private Sub myLookUserGroup_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookUserGroup.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsUserGroup.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
                dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "UserGroupID", rw(0))
                dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "UserRole", rw("UserRole"))
            End If
        Next
    End Sub

    Private Sub myLookPersonnel_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookPersonnel.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        'MsgBox(Convert.ToString(gridLookUpEdit.EditValue))
        For Each rw As DataRow In dsPersonnel.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
                dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "PersonnelID", rw(0))
                dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "PersonnelPosition", rw("PersonnelPosition"))
            End If
        Next
    End Sub

    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyData = Keys.Enter Then
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_UserAccountbyFilter" _
                             , dtUserAccntView, bsd, dgUserAccntList, dvUserAccntList, False)
        End If
    End Sub

    Private Sub SaveChangesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveChangesToolStripMenuItem.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges(isEdit)
        End If
    End Sub

    Private Sub EditStockGroupToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditStockGroupToolStripMenuItem.Click
        isEdit = True
        AddRow(isEdit)
    End Sub

    Private Sub AddGroupListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddGroupListToolStripMenuItem.Click
        isEdit = False
        AddRow(isEdit)
    End Sub

    Private Sub ResetPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetPasswordToolStripMenuItem.Click
        If dvUserAccntList.SelectedRowsCount <= 0 Then Exit Sub
        Dim uedit As New UserAccountEdit

        With uedit
            .bs = New BindingSource
            .bs.DataSource = GetType(MyUserAccountRecord)
            .ulist = Me
            Dim urec As MyUserAccountRecord = New MyUserAccountRecord
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            urec.NewPassword = String.Empty
            urec.RetypePassword = String.Empty

            .txtNewPassword.DataBindings.Add(New Binding("EditValue", .bs, "NewPassword", True))
            .txtRetypePassword.DataBindings.Add(New Binding("EditValue", .bs, "RetypePassword", True))
            .bs.Add(urec)
            .usergroupid = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "UserGroupID").ToString
            .empid = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "PersonnelID").ToString
            .companyid = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "CompanyID").ToString
            '.locationid = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "LocationID").ToString
            .ustatus = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "UserStatus").ToString
            .upassword = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "UserPassword").ToString

            .txtUserAccountID.Text = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "UserID").ToString
            .txtUsername.Text = dvUserAccntList.GetRowCellDisplayText(dvUserAccntList.FocusedRowHandle, "Username").ToString

            .DxErrorProvider1.DataSource = .bs
            .DxErrorProvider1.ContainerControl = uedit
            .Refresh()
            .BringToFront()
            .TopMost = True

            .txtNewPassword.Focus()
            .txtNewPassword.SelectAll()
        End With
    End Sub

    'Private Sub myLookLocation_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookLocation.EditValueChanged
    'Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
    'If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
    'For Each rw As DataRow In dsLocation.Tables(0).Rows
    '    If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
    '        dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "LocationID", rw(0))
    '        dvUserAccntList.SetRowCellValue(dvUserAccntList.FocusedRowHandle, "LocationDescription", rw("LocationDescription"))
    '    End If
    'Next
    'End Sub
End Class