Imports System.Data.SqlClient

Public Class ServerConnection
    Dim mode As ModeEncryption.ModifiedEncyrption
    Dim bs As New BindingSource
    Dim dtTemp As New DataSet
    Private Sub GetDBList()
        Try
            SqlDbName = "master"

            Server = cmbServername.Text
            If cmbAuthentication.Text = "Windows Authentication" Then
                UseWinAuth = True
            Else
                UseWinAuth = False
            End If

            SQLLogin = txtUsername.Text
            SQLPassword = txtPassword.Text

            cmbControlFile.Items.Clear()
            cmbDataFile.Items.Clear()

            If SqlHelper.SqlDbExists(sSqlDbHandle) = False Then
                MsgBox("Server Cannot be Found" & Err.Description, MsgBoxStyle.Critical)
            Else
                cn = New SqlConnection(sSqlDbHandle.ConnectionString)
                dacp = New SqlDataAdapter("", sSqlDbHandle.ConnectionString)


                If cn.State = ConnectionState.Closed Then cn.Open()

                dacp.SelectCommand.CommandType = CommandType.StoredProcedure
                dacp.SelectCommand.CommandText = "sp_databases"
                dacp.SelectCommand.Parameters.Clear()
                dacp.AcceptChangesDuringFill = True
                dacp.Fill(dtTemp)
                Dim cnt As Integer = 0

                While cnt < dtTemp.Tables(0).Rows.Count

                    cmbControlFile.Items.Add(dtTemp.Tables(0).Rows(cnt)(0).ToString)
                    cmbDataFile.Items.Add(dtTemp.Tables(0).Rows(cnt)(0).ToString)

                    cnt += 1
                End While
               
            End If

        Catch ex As Exception
            MsgBox("Databases Cannot be Found" & Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ServerConnection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If GetSetting("DXZLSystem", "Startup", "ServerName") = "" Then
            Application.Exit()
        End If
    End Sub

    Private Sub ServerConnection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        GetDBList()
        cmbAuthentication.SelectedIndex = 0
        cmbServername.SelectedIndex = 0
        cmbAuthentication.Text = GetSetting("DXZLSystem", "Startup", "Authentication")
        cmbServername.Text = GetSetting("DXZLSystem", "Startup", "ServerName")
        txtUsername.Text = GetSetting("DXZLSystem", "Startup", "Username")
        If GetSetting("DXZLSystem", "Startup", "Password") <> "" Then
            txtPassword.Text = mode.Decrypt64(GetSetting("DXZLSystem", "Startup", "Password"))
        Else
            txtPassword.Text = ""
        End If

        cmbControlFile.Text = GetSetting("DXZLSystem", "Startup", "DataDBName")
        cmbDataFile.Text = GetSetting("DXZLSystem", "Startup", "ControlDBName")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If isStartup = True Then
            End
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmbAuthentication_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAuthentication.SelectedIndexChanged
        Select Case cmbAuthentication.Text
            Case "Windows Authentication"
                txtUsername.Enabled = False
                txtPassword.Enabled = False
            Case "SQL Server Authentication"
                txtUsername.Enabled = True
                txtPassword.Enabled = True
        End Select
    End Sub

    Private Sub btnSaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSettings.Click
        On Error GoTo err
        SaveSetting("DXZLSystem", "Startup", "Authentication", cmbAuthentication.Text)
        SaveSetting("DXZLSystem", "Startup", "ServerName", cmbServername.Text)
        SaveSetting("DXZLSystem", "Startup", "Username", txtUsername.Text)
        If txtPassword.Text <> "" Then
            SaveSetting("DXZLSystem", "Startup", "Password", mode.Encrypt64(txtPassword.Text))
        Else
            SaveSetting("DXZLSystem", "Startup", "Password", txtPassword.Text)
        End If

        SaveSetting("DXZLSystem", "Startup", "ControlDBName", cmbControlFile.Text)
        SaveSetting("DXZLSystem", "Startup", "DataDBName", cmbDataFile.Text)

        MsgBox("Settings Saved.", MsgBoxStyle.Information)

        tAuthentication = GetSetting("DXZLSystem", "Startup", "Authentication")
        tServername = GetSetting("DXZLSystem", "Startup", "ServerName")
        tUsername = GetSetting("DXZLSystem", "Startup", "Username")
        If GetSetting("DXZLSystem", "Startup", "Password") <> "" Then
            tPassword = mode.Decrypt64(GetSetting("DXZLSystem", "Startup", "Password"))
        Else
            tPassword = ""
        End If


        Exit Sub
err:
        MsgBox(Err.Number & ": Databases Cannot be Found" & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnTestConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestConnection.Click
        Try

            SqlDbName = GetSetting("DXZLSystem", "Startup", "ControlDBName")


            Server = GetSetting("DXZLSystem", "Startup", "ServerName")
            If cmbAuthentication.Text = "Windows Authentication" Then
                UseWinAuth = True
            Else
                UseWinAuth = False
            End If

            SQLLogin = GetSetting("DXZLSystem", "Startup", "Username")
            SQLPassword = mode.Decrypt64(GetSetting("DXZLSystem", "Startup", "Password"))

            If SqlHelper.SqlDbExists(sSqlDbHandle) = False Then
                MsgBox("Server Cannot be Found" & Err.Description, MsgBoxStyle.Critical)
            Else
                cn = New SqlConnection(sSqlDbHandle.ConnectionString)
                dacp = New SqlDataAdapter("", sSqlDbHandle.ConnectionString)


                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd = New SqlCommand
                cmd.Connection = cn
                cmd.CommandText = "SELECT TOP 1 * FROM tblTreeControl"
                cmd.CommandType = CommandType.Text

                dr = cmd.ExecuteReader
                If dr.HasRows Then
                    MsgBox(sSqlDbHandle.Server, MsgBoxStyle.Information)
                End If
                dr.Close()

            End If
        Catch ex As Exception
            MsgBox("Databases Cannot be Found" & Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsername.KeyDown, txtPassword.KeyDown
        If e.KeyData = Keys.Enter Then
            GetDBList()

        End If
    End Sub
End Class