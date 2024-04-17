<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreeViewList
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
        Me.ComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.dgTreeHeader = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NewParentTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewChildTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewChildTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dvTreeHeader = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ComboBoxEdit2 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.dgTreeDetail = New DevExpress.XtraGrid.GridControl()
        Me.dvTreeDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.DxErrorProviderHeader = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.DxErrorProviderDetail = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgTreeHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.dvTreeHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgTreeDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dvTreeDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProviderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProviderDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.LabelControl1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.ComboBoxEdit1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.TextEdit1)
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.dgTreeHeader)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.LabelControl2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.ComboBoxEdit2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.TextEdit2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.dgTreeDetail)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1052, 514)
        Me.SplitContainerControl1.SplitterPosition = 327
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Location = New System.Drawing.Point(648, 1)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(34, 16)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Filter:"
        '
        'ComboBoxEdit1
        '
        Me.ComboBoxEdit1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxEdit1.Location = New System.Drawing.Point(869, 0)
        Me.ComboBoxEdit1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxEdit1.Name = "ComboBoxEdit1"
        Me.ComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ComboBoxEdit1.Size = New System.Drawing.Size(180, 22)
        Me.ComboBoxEdit1.TabIndex = 2
        '
        'TextEdit1
        '
        Me.TextEdit1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextEdit1.EditValue = ""
        Me.TextEdit1.Location = New System.Drawing.Point(690, 0)
        Me.TextEdit1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(181, 22)
        Me.TextEdit1.TabIndex = 1
        '
        'dgTreeHeader
        '
        Me.dgTreeHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgTreeHeader.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgTreeHeader.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgTreeHeader.Location = New System.Drawing.Point(3, 26)
        Me.dgTreeHeader.MainView = Me.dvTreeHeader
        Me.dgTreeHeader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgTreeHeader.Name = "dgTreeHeader"
        Me.dgTreeHeader.Size = New System.Drawing.Size(1049, 266)
        Me.dgTreeHeader.TabIndex = 5
        Me.dgTreeHeader.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dvTreeHeader, Me.GridView1})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewParentTreeToolStripMenuItem, Me.NewChildTreeToolStripMenuItem, Me.ViewChildTreeToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(186, 76)
        '
        'NewParentTreeToolStripMenuItem
        '
        Me.NewParentTreeToolStripMenuItem.Name = "NewParentTreeToolStripMenuItem"
        Me.NewParentTreeToolStripMenuItem.Size = New System.Drawing.Size(185, 24)
        Me.NewParentTreeToolStripMenuItem.Text = "New Parent Tree"
        '
        'NewChildTreeToolStripMenuItem
        '
        Me.NewChildTreeToolStripMenuItem.Name = "NewChildTreeToolStripMenuItem"
        Me.NewChildTreeToolStripMenuItem.Size = New System.Drawing.Size(185, 24)
        Me.NewChildTreeToolStripMenuItem.Text = "New Child Tree"
        '
        'ViewChildTreeToolStripMenuItem
        '
        Me.ViewChildTreeToolStripMenuItem.Name = "ViewChildTreeToolStripMenuItem"
        Me.ViewChildTreeToolStripMenuItem.Size = New System.Drawing.Size(185, 24)
        Me.ViewChildTreeToolStripMenuItem.Text = "Edit Parent Tree"
        '
        'dvTreeHeader
        '
        Me.dvTreeHeader.GridControl = Me.dgTreeHeader
        Me.dvTreeHeader.Name = "dvTreeHeader"
        Me.dvTreeHeader.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvTreeHeader.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvTreeHeader.OptionsBehavior.Editable = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.dgTreeHeader
        Me.GridView1.Name = "GridView1"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Location = New System.Drawing.Point(648, 2)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(34, 16)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Filter:"
        '
        'ComboBoxEdit2
        '
        Me.ComboBoxEdit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxEdit2.Location = New System.Drawing.Point(869, 1)
        Me.ComboBoxEdit2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxEdit2.Name = "ComboBoxEdit2"
        Me.ComboBoxEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ComboBoxEdit2.Size = New System.Drawing.Size(180, 22)
        Me.ComboBoxEdit2.TabIndex = 5
        '
        'TextEdit2
        '
        Me.TextEdit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextEdit2.EditValue = ""
        Me.TextEdit2.Location = New System.Drawing.Point(690, 1)
        Me.TextEdit2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Size = New System.Drawing.Size(181, 22)
        Me.TextEdit2.TabIndex = 4
        '
        'dgTreeDetail
        '
        Me.dgTreeDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgTreeDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgTreeDetail.Location = New System.Drawing.Point(3, 26)
        Me.dgTreeDetail.MainView = Me.dvTreeDetail
        Me.dgTreeDetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgTreeDetail.Name = "dgTreeDetail"
        Me.dgTreeDetail.Size = New System.Drawing.Size(1049, 180)
        Me.dgTreeDetail.TabIndex = 5
        Me.dgTreeDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dvTreeDetail})
        '
        'dvTreeDetail
        '
        Me.dvTreeDetail.GridControl = Me.dgTreeDetail
        Me.dvTreeDetail.Name = "dvTreeDetail"
        Me.dvTreeDetail.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvTreeDetail.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.dvTreeDetail.OptionsBehavior.Editable = False
        '
        'DxErrorProviderHeader
        '
        Me.DxErrorProviderHeader.ContainerControl = Me
        '
        'DxErrorProviderDetail
        '
        Me.DxErrorProviderDetail.ContainerControl = Me
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.ExitToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1052, 28)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(70, 24)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(45, 24)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(65, 24)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'TreeViewList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1052, 514)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "TreeViewList"
        Me.Text = "TreeViewList"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgTreeHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.dvTreeHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgTreeDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dvTreeDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProviderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProviderDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents dgTreeDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents dvTreeDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ComboBoxEdit2 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NewParentTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewChildTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DxErrorProviderHeader As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents DxErrorProviderDetail As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents dgTreeHeader As DevExpress.XtraGrid.GridControl
    Friend WithEvents dvTreeHeader As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ViewChildTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
