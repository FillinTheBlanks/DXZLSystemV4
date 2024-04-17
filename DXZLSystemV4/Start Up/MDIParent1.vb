Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.Utils.Frames
Imports DevExpress.LookAndFeel
Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports System.Collections.Generic
Imports ModeEncryption
Imports System.Reflection
Imports DevExpress.Skins
Imports DevExpress.XtraTab
Imports DevExpress.XtraTab.ViewInfo
Imports DevExpress.XtraBars.Navigation
Partial Public Class MDIParent1

    Inherits DevExpress.XtraEditors.XtraForm
    Public ActiveForms As String = ""
    Private fprogress As frmProgress1 = Nothing
    Public m_CountTo As Integer = 0
    Public f_cnt As Integer = 0
    Dim isExists As Boolean
    Dim frmFocus As New frmNew


    Private ReadOnly Property Progress() As frmProgress1
        Get
            If Object.Equals(fprogress, Nothing) Then
                fprogress = New frmProgress1(Me)
            End If
            Return fprogress
        End Get
    End Property

    Shared Sub New()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

#Region "TabPageSettings"
    Public Function GetTabPageByName(ByVal name As String) As XtraTabPage
        For Each page As XtraTabPage In tabMain.TabPages
            If page.Text = name Then
                Return page
            End If
        Next page
        Return Nothing
    End Function

    Public Function GetTabPageIndexByName(ByVal name As String) As Integer
        Dim indxpage As Integer
        For Each page As XtraTabPage In tabMain.TabPages
            If page.Text = name Then
                indxpage = tabMain.TabPages.IndexOf(page)
                Return indxpage
            End If
        Next page
        Return Nothing
    End Function

    Private Sub tabMain_CloseButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMain.CloseButtonClick
        Dim arg As ClosePageButtonEventArgs = TryCast(e, ClosePageButtonEventArgs)
        TryCast(arg.Page, XtraTabPage).PageVisible = False
        tabMain.TabPages.Remove(TryCast(arg.Page, XtraTabPage))
    End Sub

    Private Sub tabMain_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabMain.SelectedPageChanged

    End Sub
#End Region

#Region "AccordionControl"
    Dim gsetting As AccordionControlElement
    Dim custom As AccordionControlElement
    Dim trans As AccordionControlElement
    Dim maint As AccordionControlElement
    Private Sub AccordionControl1_ElementClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.Navigation.ElementClickEventArgs) Handles AccordionControl1.ElementClick
        Dim tview As String = e.Element.Text

        For Each cnt As SkinContainer In SkinManager.Default.Skins
            If cnt.SkinName = tview Then
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = tview
                Me.LookAndFeel.SetSkinStyle(tview)
                SaveSetting("DXZLSystem", "Startup", "DefaultSkin", Me.LookAndFeel.ActiveSkinName.ToString)
                'MsgBox(Me.LookAndFeel.ActiveSkinName.ToString)
                Exit For
            End If
        Next cnt

        Try
            If tabMain.TabPages.Count > 0 Then
                For x As Integer = 0 To tabMain.TabPages.Count - 1
                    If tabMain.TabPages(x).Text = tview Then
                        isExists = True
                    End If
                Next
            End If

            If isExists = False Then
                Dim frm As New Form

                frm = CreateForm(tview.Replace(" ", ""))

                frm.MdiParent = Me
                frm.Text = tview

                callforms(frm, tabMain)

                'frm.MaximizeBox = True
                'frm.MinimizeBox = True
                'frm.WindowState = FormWindowState.Maximized
                'frm.Show()
            Else
                tabMain.TabPages(GetTabPageIndexByName(tview)).PageVisible = True
            End If
            tabMain.SelectedTabPage = GetTabPageByName(tview)
            isExists = False
            
        Catch ex As Exception

        End Try
    End Sub

    Public Sub AccordionMenu()
        AccordionControl1.Elements.Clear()
        gsetting = AddGroup(AccordionControl1, "General Settings")
        trans = AddGroup(AccordionControl1, "Transactions")
        maint = AddGroup(AccordionControl1, "Maintenance")
        custom = AddGroup(AccordionControl1, "Skins")

        For Each cnt As SkinContainer In SkinManager.Default.Skins
            'Dim item As New AccordionControlElement() With {.Text = cnt.SkinName, .Style = ElementStyle.Item, .Image = Global.RecordMngtSystem.My.Resources.Example_16x16}
            AddItem(custom, cnt.SkinName)
            'custom.Elements.Insert(0, item)
        Next cnt
        AccordionControl1.ExpandElement(gsetting)
        AccordionControl1.ExpandElement(trans)

    End Sub

    Public Sub FillAccordionElementfromDB()
        dsConfig = New DataSet


        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
        dacp.SelectCommand.CommandType = CommandType.StoredProcedure
        dacp.SelectCommand.CommandText = "spSelect_TreeControlbyUserGroup"
        dacp.SelectCommand.Parameters.Clear()
        dacp.SelectCommand.Parameters.AddWithValue("@UserGroupName", global_UserGroup)
        dacp.AcceptChangesDuringFill = True
        dacp.Fill(dsConfig)

        For Each rw As DataRow In dsConfig.Tables(0).Rows

            If rw("NavGroup").ToString = "General Settings" Then
                With gsetting
                    If rw("Relation").ToString.Trim = "c" Then
                        '.Nodes.Add(rw("Tree Name").ToString)
                        AddItem(gsetting, rw("Tree Name").ToString)
                    End If
                End With
            End If

            If rw("NavGroup").ToString = "Transactions" Then
                With trans
                    If rw("Relation").ToString.Trim = "c" Then
                        '.Nodes.Add(rw("Tree Name").ToString)
                        AddItem(trans, rw("Tree Name").ToString)
                    End If
                End With
            End If

            If rw("NavGroup").ToString = "Maintenance" Then
                With maint
                    If rw("Relation").ToString.Trim = "c" Then
                        '.Nodes.Add(rw("Tree Name").ToString)
                        AddItem(maint, rw("Tree Name").ToString)
                    End If
                End With
            End If
        Next
    End Sub
#End Region

    Private Sub MDIParent1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetBounds(4, 10, 1366, 768)

        Me.WindowState = FormWindowState.Maximized
        Me.StartPosition = FormStartPosition.CenterScreen
        If GetSetting("DXZLSystem", "Startup", "DefaultSkin") = "" Then
            Me.LookAndFeel.SetSkinStyle("McSkin")
            SaveSetting("DXZLSystem", "Startup", "DefaultSkin", Me.LookAndFeel.ActiveSkinName.ToString)
        Else
            Me.LookAndFeel.SetSkinStyle(GetSetting("DXZLSystem", "Startup", "DefaultSkin"))
        End If


        DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = New Font("Tahoma", 10, FontStyle.Regular)
        tabMain.TabPages.Clear()
        AccordionMenu()
        tsDateTimeChanger.Text = My.Computer.Clock.LocalTime.ToString
    End Sub

    Private Sub MDIParent1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblConnectionSettings.Text = String.Empty
        Me.RibbonControl1.Minimized = True
        Me.RibbonControl1.ShowFullScreenButton = DefaultBoolean.True

        tabMain.TabPages.Clear()
        Dim lin As New LogIn
        lin.Height = 198

        lin.Width = Me.Width
        lin.StartPosition = Me.StartPosition
        lin.BringToFront()
        'lin.MdiParent = Me
        'lin.TopMost = True
        lin.TopLevel = True
        lin.ShowDialog()
        'lin.Show()
    End Sub

    Private Sub btnSignOut_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSignOut.ItemClick
        tabMain.TabPages.Clear()
        Dim lin As New LogIn
        lin.Height = 198
        dsConfig.Clear()
        lin.Width = Me.Width
        lin.StartPosition = FormStartPosition.CenterScreen


        lin.BringToFront()

        lin.TopMost = True
        lin.ShowDialog()
    End Sub

    Private Sub btnExit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExit.ItemClick
        If MsgBox("Are you sure you want to exit this application?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub btnRefresh_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        AccordionMenu()
        FillAccordionElementfromDB()
    End Sub

    Private Sub btnCloseAllTabs_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCloseAllTabs.ItemClick
        If MsgBox("Are you sure you want to close ALL Tabs?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            tabMain.TabPages.Clear()
        End If
    End Sub

    Private Sub tsDateTimeChanger_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsDateTimeChanger.DoubleClick, StatusStrip1.DoubleClick
        Dim dtc As New DateTimeChanger
        With dtc
            .frmList = Me
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
            .TopMost = True
            .dtDateTime.EditValue = Date.Parse(tsDateTimeChanger.Text)
        End With
    End Sub

    Private Sub btnAboutUs_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAboutUs.ItemClick
        Dim au As New AboutUs
        With au
            .labelControl2.Visible = False
            .marqueeProgressBarControl1.Visible = False
            .btnExit.Visible = True
            .ShowDialog()
            .StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub

    Private Sub btnSystemCalculator_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSystemCalculator.ItemClick
        System.Diagnostics.Process.Start("C:\Windows\system32\calc.exe")
    End Sub

    Private Sub btnUsersManual_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnUsersManual.ItemClick
        Dim um As New UsersManual
        With um
            .ShowDialog()
            .StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub
End Class