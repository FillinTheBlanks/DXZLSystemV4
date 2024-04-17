Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.Utils

Public Class TreeViewList
    Dim bsh As New BindingSource
    Dim bsd As New BindingSource
    Dim dtTreeView As New DataSet
    Dim dtTreeDetail As New DataSet
    Public Sub RefreshDataHeader(ByVal parentChildRel As String, ByVal indxString As String)
        Try
            Dim index As Integer = dvTreeHeader.TopRowIndex
            Dim focusedRow As Integer = dvTreeHeader.FocusedRowHandle
            dgTreeHeader.BeginUpdate()

            dtTreeView.Clear()
            dgTreeHeader.Controls.Clear()
            dgTreeHeader.DataSource = Nothing
            dgTreeHeader.Refresh()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_TreeControl"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@ParentChildRelation", parentChildRel)
                    .AddWithValue("@indexString", indxString)
                End With

                .AcceptChangesDuringFill = True
                .Fill(dtTreeView)
            End With



            bsh.DataSource = dtTreeView.Tables(0)

            dgTreeHeader.DataSource = bsh.DataSource
            dgTreeHeader.Refresh()

            dvTreeHeader.FocusedRowHandle = focusedRow + 1
            dvTreeHeader.TopRowIndex = index + 1
            dgTreeHeader.EndUpdate()
            dvTreeHeader.BestFitColumns()
        Catch ex As Exception
            MsgBox("Error on Form: " & Me.Name & " Contact System Administrator " & ex.ToString, MsgBoxStyle.OkOnly, "Error")
        Finally

        End Try
    End Sub

    Public Sub RefreshDataDetail(ByVal parentChildRel As String, ByVal indxString As String)
        Try
            Dim index As Integer = dvTreeDetail.TopRowIndex
            Dim focusedRow As Integer = dvTreeDetail.FocusedRowHandle
            dgTreeDetail.BeginUpdate()

            dtTreeDetail.Clear()
            dgTreeDetail.Controls.Clear()
            dgTreeDetail.DataSource = Nothing
            dgTreeDetail.Refresh()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_TreeControl"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@ParentChildRelation", parentChildRel)
                    .AddWithValue("@indexString", indxString)
                End With

                .AcceptChangesDuringFill = True
                .Fill(dtTreeDetail)
            End With


            bsd.DataSource = dtTreeDetail.Tables(0)

            dgTreeDetail.DataSource = bsd.DataSource
            dgTreeDetail.Refresh()

            dvTreeDetail.FocusedRowHandle = focusedRow + 1
            dvTreeDetail.TopRowIndex = index + 1
            dgTreeDetail.EndUpdate()
            dvTreeDetail.BestFitColumns()
        Catch ex As Exception
            MsgBox("Error on Form: " & Me.Name & " Contact System Administrator " & ex.ToString, MsgBoxStyle.OkOnly, "Error")
        Finally

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

    Private Sub TreeViewList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        RefreshDataHeader("p", "")
        RefreshDataDetail("c", "")

        DxErrorProviderHeader.DataSource = bsh.DataSource
        DxErrorProviderDetail.DataSource = bsd.DataSource
    End Sub

    Private Sub dvTreeDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvTreeDetail.DoubleClick
        Dim rowname As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        If rowname.SelectedRowsCount > 0 Then
            Dim tedit As New TreeEdit
            With tedit
                .bs = New BindingSource
                .bs.DataSource = GetType(MyTreeRecord)
                Dim rec As MyTreeRecord = New MyTreeRecord
                rec.TreeName = GetSelectedRowsfromGridView(sender, 1)
                rec.NavGroup = GetSelectedRowsfromGridView(sender, 5)
                .bs.Add(rec)

                .txtTreeName.DataBindings.Add(New Binding("EditValue", .bs, "TreeName", True))
                .cmbNavGroup.DataBindings.Add(New Binding("EditValue", .bs, "NavGroup", True))
                .DxErrorProvider1.DataSource = .bs
                .DxErrorProvider1.ContainerControl = tedit

                .frmTreeList = Me
                .txtTreeID.Text = GetSelectedRowsfromGridView(sender, 0)
                .txtTreeName.Text = GetSelectedRowsfromGridView(sender, 1)
                .SRelation = GetSelectedRowsfromGridView(sender, 2)
                .IOrdering = Integer.Parse(GetSelectedRowsfromGridView(sender, 3))
                .IParentTreeID = Integer.Parse(GetSelectedRowsfromGridView(sender, 4))
                .SStatus = GetSelectedRowsfromGridView(sender, 6)
                .isEdit = True
                .Show()
                .TopMost = True
                .cmbNavGroup.SelectedItem = GetSelectedRowsfromGridView(sender, 5)
                .Refresh()
                .txtTreeName.Focus()
                .txtTreeName.SelectAll()
            End With
        End If

    End Sub

    Private Sub dvTreeHeader_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvTreeHeader.DoubleClick
        RefreshDataDetail("c", dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(0)).ToString())
    End Sub

    Private Sub dvTreeDetail_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles dvTreeDetail.RowStyle
        If e.RowHandle >= 0 Then

            Dim cat As String = dvTreeDetail.GetRowCellDisplayText(e.RowHandle, dvTreeDetail.Columns("Status")).ToString
            If cat <> "" Then
                If cat.Substring(0, 1) = "A" Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.SeaGreen
                    e.Appearance.BackColor2 = Color.LightGreen
                ElseIf cat.Substring(0, 1) = "D" Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Tomato
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        End If
    End Sub

    Private Sub NewParentTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewParentTreeToolStripMenuItem.Click 
            Dim tedit As New TreeEdit
            With tedit  
                .frmTreeList = Me
                .txtTreeID.Text = String.Empty
                .txtTreeName.Text = String.Empty
                .SRelation = "p"
                .IOrdering = 0
                .IParentTreeID = 0
                .SStatus = "A"
                .btnDelete.Enabled = False
                .isEdit = False
                .Show()
                .TopMost = True
                .cmbNavGroup.ReadOnly = False
                '.cmbNavGroup.SelectedIndex = 0

                .bs = New BindingSource
            .bs.DataSource = GetType(MyTreeRecord)
            Dim rec As MyTreeRecord = New MyTreeRecord
                rec.TreeName = ""
                rec.NavGroup = ""
                .bs.Add(rec)

                .txtTreeName.DataBindings.Add(New Binding("EditValue", .bs, "TreeName", True))
                .cmbNavGroup.DataBindings.Add(New Binding("EditValue", .bs, "NavGroup", True))
                .DxErrorProvider1.DataSource = .bs
                .DxErrorProvider1.ContainerControl = tedit

                .Refresh()
                .txtTreeName.Focus()
                .txtTreeName.SelectAll()
            End With
    End Sub

    Private Sub NewChildTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewChildTreeToolStripMenuItem.Click
        If dvTreeHeader.SelectedRowsCount > 0 Then
            Dim tedit As New TreeEdit
            With tedit
                .frmTreeList = Me
                .LayoutControlItem1.Text = "Parent Tree:"
                .txtTreeID.Text = String.Format("({0}) {1}", dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(0)).ToString(), dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(1)).ToString())
                .txtTreeName.Text = String.Empty
                .SRelation = "c"
                .IOrdering = 0
                .IParentTreeID = Integer.Parse(dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(0)).ToString())
                .SStatus = "A"
                .btnDelete.Enabled = False
                .isEdit = False
                .Show()
                .TopMost = True
                .cmbNavGroup.ReadOnly = True
                .cmbNavGroup.SelectedItem = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(5)).ToString()

                .bs = New BindingSource
                .bs.DataSource = GetType(MyTreeRecord)
                Dim rec As MyTreeRecord = New MyTreeRecord
                rec.TreeName = ""
                rec.NavGroup = .cmbNavGroup.SelectedItem.ToString
                .bs.Add(rec)

                .txtTreeName.DataBindings.Add(New Binding("EditValue", .bs, "TreeName", True))
                .cmbNavGroup.DataBindings.Add(New Binding("EditValue", .bs, "NavGroup", True))
                .DxErrorProvider1.DataSource = .bs
                .DxErrorProvider1.ContainerControl = tedit

                .Refresh()
                .txtTreeName.Focus()
                .txtTreeName.SelectAll()
            End With
        End If
    End Sub

    Private Sub hot_key(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, dgTreeDetail.KeyDown, dgTreeHeader.KeyDown, _
        dvTreeDetail.KeyDown, dvTreeHeader.KeyDown, TextEdit1.KeyDown, TextEdit2.KeyDown, ComboBoxEdit1.KeyDown, ComboBoxEdit2.KeyDown
        If e.KeyCode = Keys.F5 Then
            RefreshDataDetail("c", "")
            RefreshDataHeader("p", "")
        End If
    End Sub

    Private Sub EditParentTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewChildTreeToolStripMenuItem.Click
        If dvTreeHeader.SelectedRowsCount > 0 Then
            Dim tedit As New TreeEdit
            With tedit
                .bs = New BindingSource
                .bs.DataSource = GetType(MyTreeRecord)
                Dim rec As MyTreeRecord = New MyTreeRecord
                rec.TreeName = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(1)).ToString()
                rec.NavGroup = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(5)).ToString()
                .bs.Add(rec)

                .txtTreeName.DataBindings.Add(New Binding("EditValue", .bs, "TreeName", True))
                .cmbNavGroup.DataBindings.Add(New Binding("EditValue", .bs, "NavGroup", True))
                .DxErrorProvider1.DataSource = .bs
                .DxErrorProvider1.ContainerControl = tedit

                .frmTreeList = Me
                .txtTreeID.Text = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(0)).ToString()
                .txtTreeName.Text = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(1)).ToString()
                .SRelation = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(2)).ToString()
                .IOrdering = Integer.Parse(dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(3)).ToString())
                .IParentTreeID = Integer.Parse(dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(4)).ToString())
                .SStatus = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(6)).ToString()
                .isEdit = True
                .Show()
                .TopMost = True
                .cmbNavGroup.SelectedItem = dvTreeHeader.GetRowCellValue(dvTreeHeader.FocusedRowHandle(), dvTreeHeader.Columns(5)).ToString()
                .Refresh()
                .txtTreeName.Focus()
                .txtTreeName.SelectAll()
            End With
        End If
    End Sub

    Private Sub dvTreeHeader_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles dvTreeHeader.RowStyle
        If e.RowHandle >= 0 Then

            Dim cat As String = dvTreeHeader.GetRowCellDisplayText(e.RowHandle, dvTreeHeader.Columns("Status")).ToString
            If cat <> "" Then
                If cat.Substring(0, 1) = "A" Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.SeaGreen
                    e.Appearance.BackColor2 = Color.LightGreen
                ElseIf cat.Substring(0, 1) = "D" Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Tomato
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshDataHeader("p", "")
        RefreshDataDetail("c", "")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        MDIParent1.tabMain.TabPages.Remove(MDIParent1.GetTabPageByName(Me.Name))
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvTreeDetail.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then Exit Sub
        Dim i As Integer = dvTreeDetail.FocusedRowHandle
        DeleteMasterFileTransaction("tblTreeControl", "TreeID", Integer.Parse(dvTreeDetail.GetRowCellValue(i, dvTreeDetail.Columns("TreeID")).ToString()))
    End Sub
End Class