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
Imports ModeEncryption.ModifiedEncyrption
Imports System.Reflection
Imports System.Data.SqlClient


Public Class LogIn
    Dim mode As New ModeEncryption.ModifiedEncyrption
    Dim cnterror As Integer = 0
    Private Sub SignInFunc()
        Try
            Me.Cursor = Cursors.WaitCursor
        
        Dim isLogIn As Boolean = False

            MDIParent1.lblConnectionSettings.Text = "CONNECTED " & GetSetting("DXZLSystem", "Startup", "ServerName")
            MDIParent1.lblConnectionSettings.ForeColor = Color.ForestGreen

            tAuthentication = GetSetting("DXZLSystem", "Startup", "Authentication")

            SqlDbName = GetSetting("DXZLSystem", "Startup", "ControlDBName")
            SqlDbNameDF = GetSetting("DXZLSystem", "Startup", "DataDBName")
            Server = GetSetting("DXZLSystem", "Startup", "ServerName")
            ServerDF = GetSetting("DXZLSystem", "Startup", "ServerName")

            If tAuthentication = "Windows Authentication" Then
                UseWinAuth = True
                UseWinAuthDF = True
            Else
                UseWinAuth = False
                UseWinAuthDF = False
            End If

            SQLLogin = GetSetting("DXZLSystem", "Startup", "Username")
            SQLLoginDF = SQLLogin
            If GetSetting("DXZLSystem", "Startup", "Password") <> "" Then
                SQLPassword = mode.Decrypt64(GetSetting("DXZLSystem", "Startup", "Password"))
            Else
                SQLPassword = ""
            End If

            SQLPasswordDF = SQLPassword

        If SqlHelper.SqlDbExists(sSqlDbHandle) = False Then
            showChildFormDLG(ServerConnection, FormBorderStyle.FixedToolWindow)
        Else
            cn = New SqlConnection(sSqlDbHandle.ConnectionString)
            dacp = New SqlDataAdapter("", sSqlDbHandle.ConnectionString)
        End If

        If SqlHelper.SqlDbExists(sSqlDbHandleDF) = False Then
            showChildFormDLG(ServerConnection, FormBorderStyle.FixedToolWindow)
        Else
            cn1 = New SqlConnection(sSqlDbHandleDF.ConnectionString)
            dadf = New SqlDataAdapter("", sSqlDbHandleDF.ConnectionString)
        End If


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            'If dr.IsClosed = False Then dr.Close()

            With dacp.SelectCommand
                .CommandType = CommandType.StoredProcedure
                .CommandText = "spSelect_UserAccountbyUsernameandPassword"
                With .Parameters
                    .Clear()
                    .AddWithValue("@Username", txtUsername.Text)
                    If txtPassword.Text.Trim <> "" Then
                        .AddWithValue("@UserPassword", mode.Encrypt64(txtPassword.Text))
                    Else
                        .AddWithValue("@UserPassword", "")
                    End If

                End With
                dr = .ExecuteReader
                If dr.HasRows Then
                    While dr.Read
                        global_PerID = dr("UserID").ToString
                        global_PerName = dr("PersonnelName").ToString
                        global_PerPass = dr("UserPassword").ToString
                        global_PerUserName = dr("Username").ToString
                        global_PerCompanyID = dr("CompanyID").ToString
                        global_PerCompanyInitial = dr("CompanyInitial").ToString
                        global_PerCompanyLong = dr("CompanyName").ToString
                        global_PerStatus = dr("UserStatus").ToString
                        global_UserGroupID = Integer.Parse(dr("UserGroupID").ToString)
                        global_UserGroup = dr("UserGroupName").ToString
                       
                        isLogIn = True
                    End While

                End If
                dr.Close()
            End With
            'isLogIn = True
            'global_PerCompanyID = "61a9be74-62a7-4291-93b3-41c8fa39b44b"
            'global_PerCompanyLong = "ZLS"
            'global_PerID = "dbea6822-aef5-4193-8178-8169eb016ccb"
            'global_PerName = "kevin"
            'global_UserGroup = "Admin"
           

            If isLogIn = True And global_PerStatus = "True" Then

                'LockScreen.Close()
                'LockScreen.Dispose()
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "McSkin"


                'callforms(DashboardDesigner, MDIParent1.tabMain)
                'Dim dd As New DashboardDesigner
                'With dd
                '    .Show()
                '    .BringToFront()
                '    .StartPosition = FormStartPosition.CenterScreen
                '    .WindowState = FormWindowState.Maximized
                'End With
                Dim appversion As String = ""
                If My.Application.IsNetworkDeployed Then
                    appversion = My.Application.Deployment.CurrentVersion.ToString
                End If

                With MDIParent1
                    .Text = global_PerCompanyLong & " V1.0 " & appversion
                    .lblConnectionSettings.Text = "User: [" & global_PerName & "] "
                    .AccordionMenu()
                    .FillAccordionElementfromDB()
                    .DockPanel1.Visibility = Docking.DockVisibility.AutoHide
                    '.Filltreeviews()
                    'For Each nvgrp As NavBarGroup In .NavBarControl1.Groups
                    '    nvgrp.Expanded = False
                    'Next
                    '.nvgConnectionSettings.Expanded = True
                    '.nvg_GeneralSettings.Expanded = True
                    .TopMost = True
                    .TopMost = False

                    .BringToFront()
                End With

                Me.Close()
                Me.Dispose()
            Else
                cnterror += 1
                If cnterror >= loginAttempts Then
                    MsgBox("Please contact System Administrator for your Account.")
                    MDIParent1.Close()
                    MDIParent1.Dispose()

                Else
                    MsgBox(String.Format("Username or Password did not exists. {0} out of {1} Log In Attempts Left.", loginAttempts - cnterror, loginAttempts))
                    txtPassword.Text = String.Empty
                    txtUsername.Focus()
                    txtUsername.SelectAll()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        MDIParent1.Close()
        MDIParent1.Dispose()
    End Sub

    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        If txtUsername.Text = "" Then Exit Sub
        SignInFunc()
    End Sub

    Private Sub BarAndDockingController(ByVal p1 As Object)
        Throw New NotImplementedException
    End Sub

    Private Sub LogIn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim mode As New ModeEncryption.ModifiedEncyrption
        txtUsername.Focus()
        txtUsername.SelectAll()
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown, txtUsername.KeyDown
        If e.KeyData = Keys.Enter Then
            btnLogIn.PerformClick()
        End If
    End Sub

    Private Sub btnAboutUs_Click(sender As System.Object, e As System.EventArgs) Handles btnAboutUs.Click
        Dim au As New AboutUs
        With au
            .labelControl2.Visible = False
            .marqueeProgressBarControl1.Visible = False
            .btnExit.Visible = True
            .ShowDialog()
            .StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub
End Class