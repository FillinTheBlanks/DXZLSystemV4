<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReloanPayment
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReloanPayment))
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnNewItem = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.btnFirst = New DevExpress.XtraBars.BarButtonItem()
        Me.btnPrev = New DevExpress.XtraBars.BarButtonItem()
        Me.btnNext = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLast = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRPReport = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup3 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.cmbColumn = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtFilter = New DevExpress.XtraEditors.TextEdit()
        Me.dgList = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PrintReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dvList = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblResult1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblMessage1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.cmbColumn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.dvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.btnNewItem, Me.btnRefresh, Me.btnFirst, Me.btnPrev, Me.btnNext, Me.btnLast, Me.btnRPReport})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.RibbonControl1.MaxItemId = 8
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2})
        Me.RibbonControl1.Size = New System.Drawing.Size(682, 141)
        '
        'btnNewItem
        '
        Me.btnNewItem.Caption = "New Item"
        Me.btnNewItem.Glyph = CType(resources.GetObject("btnNewItem.Glyph"), System.Drawing.Image)
        Me.btnNewItem.Id = 1
        Me.btnNewItem.LargeGlyph = CType(resources.GetObject("btnNewItem.LargeGlyph"), System.Drawing.Image)
        Me.btnNewItem.Name = "btnNewItem"
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "Refresh"
        Me.btnRefresh.Glyph = CType(resources.GetObject("btnRefresh.Glyph"), System.Drawing.Image)
        Me.btnRefresh.Id = 2
        Me.btnRefresh.LargeGlyph = CType(resources.GetObject("btnRefresh.LargeGlyph"), System.Drawing.Image)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'btnFirst
        '
        Me.btnFirst.Caption = "First"
        Me.btnFirst.Glyph = CType(resources.GetObject("btnFirst.Glyph"), System.Drawing.Image)
        Me.btnFirst.Id = 3
        Me.btnFirst.LargeGlyph = CType(resources.GetObject("btnFirst.LargeGlyph"), System.Drawing.Image)
        Me.btnFirst.Name = "btnFirst"
        '
        'btnPrev
        '
        Me.btnPrev.Caption = "Prev"
        Me.btnPrev.Glyph = CType(resources.GetObject("btnPrev.Glyph"), System.Drawing.Image)
        Me.btnPrev.Id = 4
        Me.btnPrev.LargeGlyph = CType(resources.GetObject("btnPrev.LargeGlyph"), System.Drawing.Image)
        Me.btnPrev.Name = "btnPrev"
        '
        'btnNext
        '
        Me.btnNext.Caption = "Next"
        Me.btnNext.Glyph = CType(resources.GetObject("btnNext.Glyph"), System.Drawing.Image)
        Me.btnNext.Id = 5
        Me.btnNext.LargeGlyph = CType(resources.GetObject("btnNext.LargeGlyph"), System.Drawing.Image)
        Me.btnNext.Name = "btnNext"
        '
        'btnLast
        '
        Me.btnLast.Caption = "Last"
        Me.btnLast.Glyph = CType(resources.GetObject("btnLast.Glyph"), System.Drawing.Image)
        Me.btnLast.Id = 6
        Me.btnLast.LargeGlyph = CType(resources.GetObject("btnLast.LargeGlyph"), System.Drawing.Image)
        Me.btnLast.Name = "btnLast"
        '
        'btnRPReport
        '
        Me.btnRPReport.Caption = "Reloan Payment Report"
        Me.btnRPReport.Glyph = CType(resources.GetObject("btnRPReport.Glyph"), System.Drawing.Image)
        Me.btnRPReport.Id = 7
        Me.btnRPReport.LargeGlyph = CType(resources.GetObject("btnRPReport.LargeGlyph"), System.Drawing.Image)
        Me.btnRPReport.Name = "btnRPReport"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1, Me.RibbonPageGroup2})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "File"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnNewItem)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnRefresh)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "Menu"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnFirst)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnPrev)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnNext)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnLast)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Navigation"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup3})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Reports"
        '
        'RibbonPageGroup3
        '
        Me.RibbonPageGroup3.ItemLinks.Add(Me.btnRPReport)
        Me.RibbonPageGroup3.Name = "RibbonPageGroup3"
        Me.RibbonPageGroup3.Text = "Reports"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.cmbColumn)
        Me.LayoutControl1.Controls.Add(Me.txtFilter)
        Me.LayoutControl1.Controls.Add(Me.dgList)
        Me.LayoutControl1.Controls.Add(Me.Panel1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 141)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(682, 303)
        Me.LayoutControl1.TabIndex = 5
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'cmbColumn
        '
        Me.cmbColumn.Location = New System.Drawing.Point(387, 12)
        Me.cmbColumn.MenuManager = Me.RibbonControl1
        Me.cmbColumn.Name = "cmbColumn"
        Me.cmbColumn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbColumn.Size = New System.Drawing.Size(283, 20)
        Me.cmbColumn.StyleController = Me.LayoutControl1
        Me.cmbColumn.TabIndex = 7
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(56, 12)
        Me.txtFilter.MenuManager = Me.RibbonControl1
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(283, 20)
        Me.txtFilter.StyleController = Me.LayoutControl1
        Me.txtFilter.TabIndex = 6
        '
        'dgList
        '
        Me.dgList.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgList.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2)
        Me.dgList.Location = New System.Drawing.Point(12, 36)
        Me.dgList.MainView = Me.dvList
        Me.dgList.MenuManager = Me.RibbonControl1
        Me.dgList.Name = "dgList"
        Me.dgList.Size = New System.Drawing.Size(528, 255)
        Me.dgList.TabIndex = 5
        Me.dgList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.dvList})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintReportToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(138, 26)
        '
        'PrintReportToolStripMenuItem
        '
        Me.PrintReportToolStripMenuItem.Name = "PrintReportToolStripMenuItem"
        Me.PrintReportToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.PrintReportToolStripMenuItem.Text = "Print Report"
        '
        'dvList
        '
        Me.dvList.GridControl = Me.dgList
        Me.dvList.Name = "dvList"
        Me.dvList.OptionsBehavior.Editable = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblResult1)
        Me.Panel1.Controls.Add(Me.lblMessage1)
        Me.Panel1.Controls.Add(Me.LabelControl1)
        Me.Panel1.Location = New System.Drawing.Point(544, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(126, 255)
        Me.Panel1.TabIndex = 4
        '
        'lblResult1
        '
        Me.lblResult1.Location = New System.Drawing.Point(44, 97)
        Me.lblResult1.Name = "lblResult1"
        Me.lblResult1.Size = New System.Drawing.Size(66, 13)
        Me.lblResult1.TabIndex = 2
        Me.lblResult1.Text = "LabelControl2"
        '
        'lblMessage1
        '
        Me.lblMessage1.Location = New System.Drawing.Point(13, 59)
        Me.lblMessage1.Name = "lblMessage1"
        Me.lblMessage1.Size = New System.Drawing.Size(66, 13)
        Me.lblMessage1.TabIndex = 1
        Me.lblMessage1.Text = "LabelControl2"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 14)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Statistics"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(682, 303)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.Panel1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(532, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(130, 259)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dgList
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(532, 259)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtFilter
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(331, 24)
        Me.LayoutControlItem3.Text = "Filter:"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(39, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.cmbColumn
        Me.LayoutControlItem4.Location = New System.Drawing.Point(331, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(331, 24)
        Me.LayoutControlItem4.Text = "Column:"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(39, 13)
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.PrintToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 141)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 1, 0, 1)
        Me.MenuStrip1.Size = New System.Drawing.Size(682, 20)
        Me.MenuStrip1.TabIndex = 22
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(58, 18)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(44, 18)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(52, 18)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ReloanPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(682, 444)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ReloanPayment"
        Me.Text = "Reloan Payment"
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.cmbColumn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.dvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents btnNewItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnFirst As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnPrev As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnNext As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnLast As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents cmbColumn As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtFilter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dgList As DevExpress.XtraGrid.GridControl
    Friend WithEvents dvList As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblResult1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblMessage1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PrintReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRPReport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup3 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
End Class
