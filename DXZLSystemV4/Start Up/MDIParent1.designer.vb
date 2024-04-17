<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIParent1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIParent1))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblConnectionSettings = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsDateTimeChanger = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.AccordionControl1 = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.btnSignOut = New DevExpress.XtraBars.BarButtonItem()
        Me.btnExit = New DevExpress.XtraBars.BarButtonItem()
        Me.btnRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.btnCloseAllTabs = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAboutUs = New DevExpress.XtraBars.BarButtonItem()
        Me.btnUsersManual = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSystemCalculator = New DevExpress.XtraBars.BarButtonItem()
        Me.RibbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.RibbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.RibbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.tabMain = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.AccordionControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblConnectionSettings, Me.tsDateTimeChanger})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 427)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 9, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(745, 22)
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblConnectionSettings
        '
        Me.lblConnectionSettings.Name = "lblConnectionSettings"
        Me.lblConnectionSettings.Size = New System.Drawing.Size(69, 17)
        Me.lblConnectionSettings.Text = "Connection"
        '
        'tsDateTimeChanger
        '
        Me.tsDateTimeChanger.Name = "tsDateTimeChanger"
        Me.tsDateTimeChanger.Size = New System.Drawing.Size(120, 17)
        Me.tsDateTimeChanger.Text = "ToolStripStatusLabel1"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel1.FloatVertical = True
        Me.DockPanel1.ID = New System.Guid("8b6411fd-9544-4d50-ab5c-665b779b9c76")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 141)
        Me.DockPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(238, 200)
        Me.DockPanel1.Size = New System.Drawing.Size(238, 286)
        Me.DockPanel1.Text = "Menu Option"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.AccordionControl1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(2)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(230, 259)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'AccordionControl1
        '
        Me.AccordionControl1.Appearance.AccordionControl.BackColor = System.Drawing.Color.White
        Me.AccordionControl1.Appearance.AccordionControl.Options.UseBackColor = True
        Me.AccordionControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccordionControl1.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple
        Me.AccordionControl1.Location = New System.Drawing.Point(0, 0)
        Me.AccordionControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.AccordionControl1.Name = "AccordionControl1"
        Me.AccordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.[Auto]
        Me.AccordionControl1.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always
        Me.AccordionControl1.Size = New System.Drawing.Size(230, 259)
        Me.AccordionControl1.TabIndex = 0
        Me.AccordionControl1.Text = "AccordionControl1"
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.btnSignOut, Me.btnExit, Me.btnRefresh, Me.btnCloseAllTabs, Me.btnAboutUs, Me.btnUsersManual, Me.btnSystemCalculator})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.RibbonControl1.MaxItemId = 8
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.RibbonPage1, Me.RibbonPage2})
        Me.RibbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.[True]
        Me.RibbonControl1.Size = New System.Drawing.Size(745, 141)
        '
        'btnSignOut
        '
        Me.btnSignOut.Caption = "Sign Out"
        Me.btnSignOut.Glyph = CType(resources.GetObject("btnSignOut.Glyph"), System.Drawing.Image)
        Me.btnSignOut.Id = 1
        Me.btnSignOut.LargeGlyph = CType(resources.GetObject("btnSignOut.LargeGlyph"), System.Drawing.Image)
        Me.btnSignOut.Name = "btnSignOut"
        '
        'btnExit
        '
        Me.btnExit.Caption = "Exit"
        Me.btnExit.Glyph = CType(resources.GetObject("btnExit.Glyph"), System.Drawing.Image)
        Me.btnExit.Id = 2
        Me.btnExit.LargeGlyph = CType(resources.GetObject("btnExit.LargeGlyph"), System.Drawing.Image)
        Me.btnExit.Name = "btnExit"
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "Refresh"
        Me.btnRefresh.Glyph = CType(resources.GetObject("btnRefresh.Glyph"), System.Drawing.Image)
        Me.btnRefresh.Id = 3
        Me.btnRefresh.LargeGlyph = CType(resources.GetObject("btnRefresh.LargeGlyph"), System.Drawing.Image)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'btnCloseAllTabs
        '
        Me.btnCloseAllTabs.Caption = "Close All Tabs"
        Me.btnCloseAllTabs.Glyph = CType(resources.GetObject("btnCloseAllTabs.Glyph"), System.Drawing.Image)
        Me.btnCloseAllTabs.Id = 4
        Me.btnCloseAllTabs.LargeGlyph = CType(resources.GetObject("btnCloseAllTabs.LargeGlyph"), System.Drawing.Image)
        Me.btnCloseAllTabs.Name = "btnCloseAllTabs"
        '
        'btnAboutUs
        '
        Me.btnAboutUs.Caption = "About Us"
        Me.btnAboutUs.Glyph = CType(resources.GetObject("btnAboutUs.Glyph"), System.Drawing.Image)
        Me.btnAboutUs.Id = 5
        Me.btnAboutUs.LargeGlyph = CType(resources.GetObject("btnAboutUs.LargeGlyph"), System.Drawing.Image)
        Me.btnAboutUs.Name = "btnAboutUs"
        '
        'btnUsersManual
        '
        Me.btnUsersManual.Caption = "User's Manual"
        Me.btnUsersManual.Glyph = CType(resources.GetObject("btnUsersManual.Glyph"), System.Drawing.Image)
        Me.btnUsersManual.Id = 6
        Me.btnUsersManual.LargeGlyph = CType(resources.GetObject("btnUsersManual.LargeGlyph"), System.Drawing.Image)
        Me.btnUsersManual.Name = "btnUsersManual"
        '
        'btnSystemCalculator
        '
        Me.btnSystemCalculator.Caption = "Calculator"
        Me.btnSystemCalculator.Glyph = CType(resources.GetObject("btnSystemCalculator.Glyph"), System.Drawing.Image)
        Me.btnSystemCalculator.Id = 7
        Me.btnSystemCalculator.LargeGlyph = CType(resources.GetObject("btnSystemCalculator.LargeGlyph"), System.Drawing.Image)
        Me.btnSystemCalculator.Name = "btnSystemCalculator"
        '
        'RibbonPage1
        '
        Me.RibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup1})
        Me.RibbonPage1.Name = "RibbonPage1"
        Me.RibbonPage1.Text = "File"
        '
        'RibbonPageGroup1
        '
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnSignOut)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnExit)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnRefresh)
        Me.RibbonPageGroup1.ItemLinks.Add(Me.btnCloseAllTabs)
        Me.RibbonPageGroup1.Name = "RibbonPageGroup1"
        Me.RibbonPageGroup1.Text = "File"
        '
        'RibbonPage2
        '
        Me.RibbonPage2.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.RibbonPageGroup2})
        Me.RibbonPage2.Name = "RibbonPage2"
        Me.RibbonPage2.Text = "Tools"
        '
        'RibbonPageGroup2
        '
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnAboutUs)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnUsersManual)
        Me.RibbonPageGroup2.ItemLinks.Add(Me.btnSystemCalculator)
        Me.RibbonPageGroup2.Name = "RibbonPageGroup2"
        Me.RibbonPageGroup2.Text = "Menu"
        '
        'tabMain
        '
        Me.tabMain.AllowDrop = True
        Me.tabMain.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(238, 141)
        Me.tabMain.Margin = New System.Windows.Forms.Padding(2)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedTabPage = Me.XtraTabPage1
        Me.tabMain.Size = New System.Drawing.Size(507, 286)
        Me.tabMain.TabIndex = 10
        Me.tabMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(2)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(501, 258)
        Me.XtraTabPage1.Text = "XtraTabPage1"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(501, 258)
        Me.XtraTabPage2.Text = "XtraTabPage2"
        '
        'MDIParent1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 449)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.DockPanel1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MDIParent1"
        Me.Text = "Lending System V1.0"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.AccordionControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblConnectionSettings As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents AccordionControl1 As DevExpress.XtraBars.Navigation.AccordionControl
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents RibbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents RibbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents tabMain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnSignOut As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnExit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnCloseAllTabs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage
    Friend WithEvents tsDateTimeChanger As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnAboutUs As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnUsersManual As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RibbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents btnSystemCalculator As DevExpress.XtraBars.BarButtonItem
End Class
