<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserPermissionEditvb
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.dgTreeList = New DevExpress.XtraGrid.GridControl()
        Me.dvTreeList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cmbNavGroup = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtUserRole = New DevExpress.XtraEditors.TextEdit()
        Me.txtUserGroupName = New DevExpress.XtraEditors.TextEdit()
        Me.txtUserGroupID = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.DxErrorProviderList = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.dgTreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dvTreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbNavGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserRole.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserGroupName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserGroupID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProviderList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.btnCancel)
        Me.LayoutControl1.Controls.Add(Me.btnSave)
        Me.LayoutControl1.Controls.Add(Me.dgTreeList)
        Me.LayoutControl1.Controls.Add(Me.cmbNavGroup)
        Me.LayoutControl1.Controls.Add(Me.txtUserRole)
        Me.LayoutControl1.Controls.Add(Me.txtUserGroupName)
        Me.LayoutControl1.Controls.Add(Me.txtUserGroupID)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1170, 706)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'btnCancel
        '
        Me.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopLeft
        Me.btnCancel.Location = New System.Drawing.Point(18, 656)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(1134, 32)
        Me.btnCancel.StyleController = Me.LayoutControl1
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'btnSave
        '
        Me.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopLeft
        Me.btnSave.Location = New System.Drawing.Point(18, 618)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(1134, 32)
        Me.btnSave.StyleController = Me.LayoutControl1
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save"
        '
        'dgTreeList
        '
        Me.dgTreeList.Location = New System.Drawing.Point(18, 146)
        Me.dgTreeList.MainView = Me.dvTreeList
        Me.dgTreeList.Name = "dgTreeList"
        Me.dgTreeList.Size = New System.Drawing.Size(1134, 466)
        Me.dgTreeList.TabIndex = 8
        Me.dgTreeList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dvTreeList})
        '
        'dvTreeList
        '
        Me.dvTreeList.GridControl = Me.dgTreeList
        Me.dvTreeList.Name = "dvTreeList"
        Me.dvTreeList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvTreeList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        '
        'cmbNavGroup
        '
        Me.cmbNavGroup.Location = New System.Drawing.Point(150, 114)
        Me.cmbNavGroup.Name = "cmbNavGroup"
        Me.cmbNavGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbNavGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbNavGroup.Size = New System.Drawing.Size(1002, 26)
        Me.cmbNavGroup.StyleController = Me.LayoutControl1
        Me.cmbNavGroup.TabIndex = 7
        '
        'txtUserRole
        '
        Me.txtUserRole.Location = New System.Drawing.Point(150, 82)
        Me.txtUserRole.Name = "txtUserRole"
        Me.txtUserRole.Properties.ReadOnly = True
        Me.txtUserRole.Size = New System.Drawing.Size(1002, 26)
        Me.txtUserRole.StyleController = Me.LayoutControl1
        Me.txtUserRole.TabIndex = 6
        '
        'txtUserGroupName
        '
        Me.txtUserGroupName.Location = New System.Drawing.Point(150, 50)
        Me.txtUserGroupName.Name = "txtUserGroupName"
        Me.txtUserGroupName.Properties.ReadOnly = True
        Me.txtUserGroupName.Size = New System.Drawing.Size(1002, 26)
        Me.txtUserGroupName.StyleController = Me.LayoutControl1
        Me.txtUserGroupName.TabIndex = 5
        '
        'txtUserGroupID
        '
        Me.txtUserGroupID.Location = New System.Drawing.Point(150, 18)
        Me.txtUserGroupID.Name = "txtUserGroupID"
        Me.txtUserGroupID.Properties.ReadOnly = True
        Me.txtUserGroupID.Size = New System.Drawing.Size(1002, 26)
        Me.txtUserGroupID.StyleController = Me.LayoutControl1
        Me.txtUserGroupID.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1170, 706)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtUserGroupID
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1140, 32)
        Me.LayoutControlItem1.Text = "User Group ID"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(127, 19)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtUserGroupName
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1140, 32)
        Me.LayoutControlItem2.Text = "User Group Name"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(127, 19)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtUserRole
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1140, 32)
        Me.LayoutControlItem3.Text = "User Role"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(127, 19)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.cmbNavGroup
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1140, 32)
        Me.LayoutControlItem4.Text = "Nav. Group"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(127, 19)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.dgTreeList
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 128)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(1140, 472)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btnSave
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 600)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(1140, 38)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.btnCancel
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 638)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(1140, 38)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'DxErrorProviderList
        '
        Me.DxErrorProviderList.ContainerControl = Me
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1170, 33)
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
        'UserPermissionEditvb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1170, 706)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "UserPermissionEditvb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "UserPermissionEditvb"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.dgTreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dvTreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbNavGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserRole.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserGroupName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserGroupID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProviderList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtUserRole As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUserGroupName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUserGroupID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cmbNavGroup As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents dgTreeList As DevExpress.XtraGrid.GridControl
    Friend WithEvents dvTreeList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents DxErrorProviderList As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
