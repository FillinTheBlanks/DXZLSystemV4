<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserGroupPermissionList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbBox = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtFilter = New DevExpress.XtraEditors.TextEdit()
        Me.dgUserGroup = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddUserGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditUserGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddUserPermissionToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.dvUserGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ComboBoxEdit2 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.dgUserPermission = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditUserPermissionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dvUserPermission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.cmbBox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgUserGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.dvUserGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgUserPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        CType(Me.dvUserPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LabelControl1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.cmbBox)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.txtFilter)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.dgUserGroup)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.ComboBoxEdit2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TextEdit2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.dgUserPermission)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1179, 643)
        Me.SplitContainerControl1.SplitterPosition = 327
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Location = New System.Drawing.Point(725, 1)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(41, 19)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Filter:"
        '
        'cmbBox
        '
        Me.cmbBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbBox.Location = New System.Drawing.Point(974, 0)
        Me.cmbBox.Name = "cmbBox"
        Me.cmbBox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbBox.Size = New System.Drawing.Size(202, 26)
        Me.cmbBox.TabIndex = 2
        '
        'txtFilter
        '
        Me.txtFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilter.EditValue = ""
        Me.txtFilter.Location = New System.Drawing.Point(772, 0)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(204, 26)
        Me.txtFilter.TabIndex = 1
        '
        'dgUserGroup
        '
        Me.dgUserGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgUserGroup.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgUserGroup.Location = New System.Drawing.Point(3, 32)
        Me.dgUserGroup.MainView = Me.dvUserGroup
        Me.dgUserGroup.Name = "dgUserGroup"
        Me.dgUserGroup.Size = New System.Drawing.Size(1176, 298)
        Me.dgUserGroup.TabIndex = 5
        Me.dgUserGroup.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dvUserGroup, Me.GridView1})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddUserGroupToolStripMenuItem, Me.EditUserGroupToolStripMenuItem, Me.AddUserPermissionToolStripMenuItem1})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(249, 94)
        '
        'AddUserGroupToolStripMenuItem
        '
        Me.AddUserGroupToolStripMenuItem.Name = "AddUserGroupToolStripMenuItem"
        Me.AddUserGroupToolStripMenuItem.Size = New System.Drawing.Size(248, 30)
        Me.AddUserGroupToolStripMenuItem.Text = "Add User Group"
        '
        'EditUserGroupToolStripMenuItem
        '
        Me.EditUserGroupToolStripMenuItem.Name = "EditUserGroupToolStripMenuItem"
        Me.EditUserGroupToolStripMenuItem.Size = New System.Drawing.Size(248, 30)
        Me.EditUserGroupToolStripMenuItem.Text = "Edit User Group"
        '
        'AddUserPermissionToolStripMenuItem1
        '
        Me.AddUserPermissionToolStripMenuItem1.Name = "AddUserPermissionToolStripMenuItem1"
        Me.AddUserPermissionToolStripMenuItem1.Size = New System.Drawing.Size(248, 30)
        Me.AddUserPermissionToolStripMenuItem1.Text = "Add User Permission"
        '
        'dvUserGroup
        '
        Me.dvUserGroup.GridControl = Me.dgUserGroup
        Me.dvUserGroup.Name = "dvUserGroup"
        Me.dvUserGroup.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvUserGroup.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvUserGroup.OptionsBehavior.Editable = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgUserGroup
        Me.GridView1.Name = "GridView1"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Location = New System.Drawing.Point(725, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(41, 19)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Filter:"
        '
        'ComboBoxEdit2
        '
        Me.ComboBoxEdit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxEdit2.Location = New System.Drawing.Point(974, 1)
        Me.ComboBoxEdit2.Name = "ComboBoxEdit2"
        Me.ComboBoxEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ComboBoxEdit2.Size = New System.Drawing.Size(202, 26)
        Me.ComboBoxEdit2.TabIndex = 5
        '
        'TextEdit2
        '
        Me.TextEdit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextEdit2.EditValue = ""
        Me.TextEdit2.Location = New System.Drawing.Point(772, 1)
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Size = New System.Drawing.Size(204, 26)
        Me.TextEdit2.TabIndex = 4
        '
        'dgUserPermission
        '
        Me.dgUserPermission.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgUserPermission.ContextMenuStrip = Me.ContextMenuStrip2
        Me.dgUserPermission.Location = New System.Drawing.Point(3, 33)
        Me.dgUserPermission.MainView = Me.dvUserPermission
        Me.dgUserPermission.Name = "dgUserPermission"
        Me.dgUserPermission.Size = New System.Drawing.Size(1176, 263)
        Me.dgUserPermission.TabIndex = 5
        Me.dgUserPermission.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dvUserPermission})
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditUserPermissionToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(245, 34)
        '
        'EditUserPermissionToolStripMenuItem
        '
        Me.EditUserPermissionToolStripMenuItem.Name = "EditUserPermissionToolStripMenuItem"
        Me.EditUserPermissionToolStripMenuItem.Size = New System.Drawing.Size(244, 30)
        Me.EditUserPermissionToolStripMenuItem.Text = "Edit User Permission"
        '
        'dvUserPermission
        '
        Me.dvUserPermission.GridControl = Me.dgUserPermission
        Me.dvUserPermission.Name = "dvUserPermission"
        Me.dvUserPermission.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvUserPermission.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvUserPermission.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.dvUserPermission.OptionsBehavior.ReadOnly = True
        Me.dvUserPermission.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1179, 33)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(82, 29)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(51, 29)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'UserGroupPermissionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 643)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "UserGroupPermissionList"
        Me.Text = "User Group Permission List"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.cmbBox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgUserGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.dvUserGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgUserPermission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        CType(Me.dvUserPermission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbBox As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtFilter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dgUserGroup As DevExpress.XtraGrid.GridControl
    Friend WithEvents dvUserGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ComboBoxEdit2 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dgUserPermission As DevExpress.XtraGrid.GridControl
    Friend WithEvents dvUserPermission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddUserGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditUserGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditUserPermissionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddUserPermissionToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
