Imports System.Data.SqlClient
Imports System.Net
Imports DevExpress.XtraGrid
Imports DevExpress.XtraTab
Imports DevExpress.XtraTab.ViewInfo
Imports System.Windows.Forms
Imports DevExpress.XtraBars.Navigation

Module modFunctions

#Region "Initialize"
    Public tAuthentication As String
    Public tPassword As String
    Public tServername As String
    Public tUsername As String
    Public tYear As String
    Public tPeriod As String
    Public tWeek As String
    Public tApprove As String
    Public tAward As String
    Public global_PerID As String
    Public global_PerName As String
    Public global_PerUserName As String
    Public global_PerPass As String
    Public global_PerCompanyID As String
    Public global_PerCompanyInitial As String
    Public global_PerCompanyLong As String
    Public global_PerLocation As String
    Public global_PerLocationInitial As String
    Public global_PerStatus As String
    Public global_PerDept As String
    Public global_UserGroup As String
    Public global_UserGroupID As Integer
    Public isStartup As Boolean = False
    Public cn As SqlConnection
    Public cmd As SqlCommand
    Public cn1 As SqlConnection
    Public cmd1 As SqlCommand
    Public dr As SqlDataReader
    Public dacp As New SqlDataAdapter("", sSqlDbHandle.ConnectionString)
    Public dadf As New SqlDataAdapter("", sSqlDbHandleDF.ConnectionString)
    Public cnString As String
    Public cnString1 As String
    Public loginAttempts As Integer = 3
    Public dsMasterTrans As DataSet
    Public dsConfig As DataSet
#End Region

#Region "SQLDBHandleControlProgram"
    Private fsqlDbHandle As SqlDbHandle


    Public Property SqlDbName() As String
        Get
            Return fsqlDbHandle.DbName
        End Get
        Set(ByVal value As String)
            fsqlDbHandle.DbName = value
        End Set
    End Property
    Public Property Server() As String
        Get
            Return fsqlDbHandle.Server
        End Get
        Set(ByVal value As String)
            fsqlDbHandle.Server = value
        End Set
    End Property
    Public Property UseWinAuth() As Boolean
        Get
            Return fsqlDbHandle.UseWinAuth
        End Get
        Set(ByVal value As Boolean)
            fsqlDbHandle.UseWinAuth = value
        End Set
    End Property
    Public Property SQLLogin() As String
        Get
            Return fsqlDbHandle.Login
        End Get
        Set(ByVal value As String)
            fsqlDbHandle.Login = value
        End Set
    End Property
    Public Property SQLPassword() As String
        Get
            Return fsqlDbHandle.Password
        End Get
        Set(ByVal value As String)
            fsqlDbHandle.Password = value
        End Set
    End Property

    Public Property sSqlDbHandle() As SqlDbHandle
        Get
            Return fsqlDbHandle
        End Get
        Set(ByVal value As SqlDbHandle)
            fsqlDbHandle = value
        End Set
    End Property

    Public Structure SqlDbHandle
        Public Server As String
        Public DbName As String
        Public UseWinAuth As Boolean
        Public Login As String
        Public Password As String

        Public ReadOnly Property ConnectionString() As String
            Get
                Dim initalCatalog As String
                If String.IsNullOrEmpty(DbName) Then
                    initalCatalog = "User Instance=False"
                Else
                    initalCatalog = String.Format("Initial Catalog={0}", DbName)
                End If
                If UseWinAuth Then
                    Return String.Format("data source={0};integrated security=SSPI;{1}", Server, initalCatalog)
                End If
                Return String.Format("Data Source={0};{3};User ID={1};Password={2}", Server, Login, Password, initalCatalog)
            End Get
        End Property
    End Structure

    Public Class SqlHelper
        Public Shared Function SqlDbExists(ByVal sqlDb As SqlDbHandle) As Boolean
            Dim commandText As String = "select * from master.dbo.sysdatabases where name='" & sqlDb.DbName & "'"
            sqlDb.DbName = "master"
            Dim ret As Boolean = False
            Using sqlConnection As New SqlConnection(sqlDb.ConnectionString)
                Try
                    sqlConnection.Open()
                    Using sqlCommand As New SqlCommand(commandText, sqlConnection)
                        Dim reader As SqlDataReader = sqlCommand.ExecuteReader()
                        ret = reader.HasRows
                        reader.Close()
                    End Using
                    sqlConnection.Close()
                Catch e1 As SqlException
                    ret = False
                End Try
            End Using
            Return ret
        End Function
        Public Shared Sub DeleteSqlDb(ByVal sqlDb As SqlDbHandle)
            Dim dbName As String = sqlDb.DbName
            sqlDb.DbName = Nothing
            Dim sqlConnection As New SqlConnection(sqlDb.ConnectionString)
            sqlConnection.Open()
            CType(New SqlCommand("DROP DATABASE " & dbName, sqlConnection), SqlCommand).ExecuteNonQuery()
        End Sub
        Public Shared Function CanConnect(ByVal sqlDb As SqlDbHandle) As Boolean
            Dim fcanConnect As Boolean = False
            Dim dbName As String = sqlDb.DbName
            sqlDb.DbName = "master"
            Dim sqlConnection As New SqlConnection(sqlDb.ConnectionString)
            Try
                sqlConnection.Open()
                fcanConnect = True
                sqlConnection.Close()
            Catch
            End Try
            If (Not fcanConnect) Then
                Return False
            End If
            sqlDb.DbName = dbName

            If (Not SqlDbExists(sqlDb)) Then
                Return True
            End If
            fcanConnect = False
            sqlConnection = New SqlConnection(sqlDb.ConnectionString)
            Try
                sqlConnection.Open()
                fcanConnect = True
                sqlConnection.Close()
            Catch
            End Try
            Return fcanConnect
        End Function
    End Class
#End Region

#Region "SQLDBHandleDataFile"
    Private fsqlDbHandleDF As SqlDbHandle


    Public Property SqlDbNameDF() As String
        Get
            Return fsqlDbHandleDF.DbName
        End Get
        Set(ByVal value As String)
            fsqlDbHandleDF.DbName = value
        End Set
    End Property
    Public Property ServerDF() As String
        Get
            Return fsqlDbHandleDF.Server
        End Get
        Set(ByVal value As String)
            fsqlDbHandleDF.Server = value
        End Set
    End Property
    Public Property UseWinAuthDF() As Boolean
        Get
            Return fsqlDbHandleDF.UseWinAuth
        End Get
        Set(ByVal value As Boolean)
            fsqlDbHandleDF.UseWinAuth = value
        End Set
    End Property
    Public Property SQLLoginDF() As String
        Get
            Return fsqlDbHandleDF.Login
        End Get
        Set(ByVal value As String)
            fsqlDbHandleDF.Login = value
        End Set
    End Property
    Public Property SQLPasswordDF() As String
        Get
            Return fsqlDbHandleDF.Password
        End Get
        Set(ByVal value As String)
            fsqlDbHandleDF.Password = value
        End Set
    End Property

    Public Property sSqlDbHandleDF() As SqlDbHandle
        Get
            Return fsqlDbHandleDF
        End Get
        Set(ByVal value As SqlDbHandle)
            fsqlDbHandleDF = value
        End Set
    End Property

    Public Structure SqlDbHandleDF
        Public Server As String
        Public DbName As String
        Public UseWinAuth As Boolean
        Public Login As String
        Public Password As String

        Public ReadOnly Property ConnectionString() As String
            Get
                Dim initalCatalog As String
                If String.IsNullOrEmpty(DbName) Then
                    initalCatalog = "User Instance=False"
                Else
                    initalCatalog = String.Format("Initial Catalog={0}", DbName)
                End If
                If UseWinAuth Then
                    Return String.Format("data source={0};integrated security=SSPI;{1}", Server, initalCatalog)
                End If
                Return String.Format("Data Source={0};{3};User ID={1};Password={2}", Server, Login, Password, initalCatalog)
            End Get
        End Property
    End Structure

    Public Class SqlHelperDF
        Public Shared Function SqlDbExists(ByVal sqlDb As SqlDbHandleDF) As Boolean
            Dim commandText As String = "select * from master.dbo.sysdatabases where name='" & sqlDb.DbName & "'"
            sqlDb.DbName = "master"
            Dim ret As Boolean = False
            Using sqlConnection As New SqlConnection(sqlDb.ConnectionString)
                Try
                    sqlConnection.Open()
                    Using sqlCommand As New SqlCommand(commandText, sqlConnection)
                        Dim reader As SqlDataReader = sqlCommand.ExecuteReader()
                        ret = reader.HasRows
                        reader.Close()
                    End Using
                    sqlConnection.Close()
                Catch e1 As SqlException
                    ret = False
                End Try
            End Using
            Return ret
        End Function
        Public Shared Sub DeleteSqlDb(ByVal sqlDb As SqlDbHandleDF)
            Dim dbName As String = sqlDb.DbName
            sqlDb.DbName = Nothing
            Dim sqlConnection As New SqlConnection(sqlDb.ConnectionString)
            sqlConnection.Open()
            CType(New SqlCommand("DROP DATABASE " & dbName, sqlConnection), SqlCommand).ExecuteNonQuery()
        End Sub
        Public Shared Function CanConnect(ByVal sqlDb As SqlDbHandleDF) As Boolean
            Dim fcanConnect As Boolean = False
            Dim dbName As String = sqlDb.DbName
            sqlDb.DbName = "master"
            Dim sqlConnection As New SqlConnection(sqlDb.ConnectionString)
            Try
                sqlConnection.Open()
                fcanConnect = True
                sqlConnection.Close()
            Catch
            End Try
            If (Not fcanConnect) Then
                Return False
            End If
            sqlDb.DbName = dbName

            If (Not SqlDbExists(sqlDb)) Then
                Return True
            End If
            fcanConnect = False
            sqlConnection = New SqlConnection(sqlDb.ConnectionString)
            Try
                sqlConnection.Open()
                fcanConnect = True
                sqlConnection.Close()
            Catch
            End Try
            Return fcanConnect
        End Function
    End Class
#End Region


    Public Function isAllowed(ByVal treename As String, ByVal functxt As String) As String
        Dim isAllow As String = ""

        For Each rw As DataRow In dsConfig.Tables(0).Rows
            If rw("Tree Name").ToString = treename And rw(functxt).ToString = "True" Then
                isAllow = global_PerID
            End If

        Next

        Return isAllow
    End Function

    Public Sub writeDebug(ByVal str As String)
        'If System.IO.File.Exists(Application.StartupPath + "\DXCSP_Logs.txt") Then
        '    System.IO.File.Create(Application.StartupPath + "\DXCSP_Logs.txt").Dispose()
        'End If

        Dim path As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim FILE_NAME As String = Application.StartupPath + "\DXCSP_Logs.txt"

        If System.IO.File.Exists(FILE_NAME) = False Then
            System.IO.File.Create(FILE_NAME).Dispose()
        End If

        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        objWriter.WriteLine(str)
        objWriter.Close()
    End Sub

    Public Sub opencon()
        cn = New SqlConnection(sSqlDbHandle.ConnectionString)
        cn = New SqlConnection(cnString1)
        cn.Open()
        cmd.Connection = cn
    End Sub

    Public Sub opencon1()
        cn1 = New SqlConnection(cnString)
        cn1.Open()

    End Sub

    Public Sub closecon()
        cn.Close()
        cn.ConnectionString = Nothing
    End Sub

    Public Sub closecon1()
        cn1.Close()
        cn1.ConnectionString = Nothing
    End Sub

    Public Sub PublicFilterData(ByVal txtFilter As String, ByVal colname As String, ByVal cmbBox As DevExpress.XtraEditors.ComboBoxEdit _
                          , ByVal selectCmd As String, ByVal dataTable As DataSet, ByVal bs As BindingSource _
                          , ByVal grdControl As DevExpress.XtraGrid.GridControl, ByVal grdView As DevExpress.XtraGrid.Views.Grid.GridView _
                          , Optional ByVal isAddCol As Boolean = True)
        Try


            Dim index As Integer = grdView.TopRowIndex
            Dim focusedRow As Integer = grdView.FocusedRowHandle
            grdControl.BeginUpdate()

            dataTable.Clear()
            grdControl.Controls.Clear()
            grdControl.DataSource = Nothing
            grdControl.Refresh()

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = selectCmd
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", txtFilter)
                    .AddWithValue("@ColumnName", colname)
                End With

                .AcceptChangesDuringFill = True
                .Fill(dataTable)
            End With

            bs.DataSource = dataTable.Tables(0)
            grdControl.DataMember = bs.DataMember
            grdControl.DataSource = bs.DataSource
            grdControl.Refresh()

            grdView.FocusedRowHandle = focusedRow + 1
            grdView.TopRowIndex = index + 1
            grdControl.EndUpdate()
            grdView.OptionsBehavior.ReadOnly = True

            grdView.BestFitColumns()
            If isAddCol Then
                cmbBox.Properties.Items.Clear()
                For Each col In dataTable.Tables(0).Columns
                    cmbBox.Properties.Items.Add(col.ToString)
                Next
            End If

        Catch ex As Exception
            MsgBox("Error on Form: " & " Contact System Administrator " & ex.ToString, MsgBoxStyle.OkOnly, "Error")
        Finally
            If isAddCol Then cmbBox.SelectedIndex = 1
            'isEdit = False
            'Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub PublicFilterDataFile(ByVal txtFilter As String, ByVal colname As String, ByVal cmbBox As DevExpress.XtraEditors.ComboBoxEdit _
                          , ByVal selectCmd As String, ByVal dataTable As DataSet, ByVal bs As BindingSource _
                          , ByVal grdControl As DevExpress.XtraGrid.GridControl, ByVal grdView As DevExpress.XtraGrid.Views.Grid.GridView _
                          , Optional ByVal isAddCol As Boolean = True)
        Try
            cmbBox.Properties.Items.Clear()

            Dim index As Integer = grdView.TopRowIndex
            Dim focusedRow As Integer = grdView.FocusedRowHandle
            grdControl.BeginUpdate()

            dataTable.Clear()
            grdControl.Controls.Clear()
            grdControl.DataSource = Nothing
            grdControl.Refresh()

            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = selectCmd
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", txtFilter)
                    .AddWithValue("@ColumnName", colname)
                End With

                .AcceptChangesDuringFill = True
                .Fill(dataTable)
            End With

            bs.DataSource = dataTable.Tables(0)

            grdControl.DataSource = bs.DataSource
            grdControl.Refresh()

            grdView.FocusedRowHandle = focusedRow + 1
            grdView.TopRowIndex = index + 1
            grdControl.EndUpdate()
            grdView.OptionsBehavior.ReadOnly = True

            grdView.BestFitColumns()

            For Each col In dataTable.Tables(0).Columns
                cmbBox.Properties.Items.Add(col.ToString)
            Next
        Catch ex As Exception
            MsgBox("Error on Form: " & " Contact System Administrator " & ex.ToString, MsgBoxStyle.OkOnly, "Error")
        Finally
            cmbBox.SelectedIndex = 1
            'isEdit = False
            'Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub fillConfigList(ByVal gridview As System.Windows.Forms.DataGridView, ByVal value As String)
        Try
            gridview.Controls.Clear()
            gridview.DataSource = Nothing
            gridview.Refresh()
            Dim dsgroup As New DataSet
            If cn.State = ConnectionState.Closed Then cn.Open()
            'da.SelectCommand.CommandType = CommandType.StoredProcedure
            'da.SelectCommand.CommandText = "SPSM_selectConfigView"
            'da.SelectCommand.Parameters.Clear()
            'da.SelectCommand.Parameters.AddWithValue("@tablename", value)
            'da.AcceptChangesDuringFill = True
            'da.Fill(dsgroup)

            Dim bs As New BindingSource
            bs.DataSource = dsgroup.Tables(0)

            gridview.DataSource = bs.DataSource

            gridview.Columns.Add("", "")
            gridview.Columns(gridview.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Catch ex As Exception
            MsgBox(ex.ToString() & " Error Contact System Administrator", MsgBoxStyle.OkOnly, "Error")
        Finally
        End Try
    End Sub

    Public Function GetSelectedRowsfromGridView(ByVal sender As Object, ByVal columnindex As Integer) As String
        Dim rowvalue As String = ""
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        If view.SelectedRowsCount > 0 Then
            rowvalue = view.GetRowCellValue(view.FocusedRowHandle(), view.Columns(columnindex)).ToString
        End If

        Return rowvalue
    End Function

   

    Public Sub ClearTextBox(ByVal root As Control)

        For Each ctrl As Control In root.Controls

            ClearTextBox(ctrl)

            If TypeOf ctrl Is TextBox Then

                CType(ctrl, TextBox).Text = String.Empty

            End If

        Next ctrl

    End Sub

    Public Sub ChangeFontStyleSize(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            ChangeFontStyleSize(ctrl)
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Font = New Font("Tahoma", 10, FontStyle.Regular)
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).Font = New Font("Tahoma", 10, FontStyle.Regular)
            End If
        Next
    End Sub

    Public Sub ClearListView(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            ClearListView(ctrl)

            If TypeOf ctrl Is ListView Then
                CType(ctrl, ListView).Items.Clear()
            End If
        Next
    End Sub

    Public Sub callforms(ByVal frmname As System.Windows.Forms.Form, ByVal pnlname As DevExpress.XtraTab.XtraTabControl)
        frmname.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frmname.TopLevel = False
        frmname.ShowInTaskbar = False
        frmname.ShowIcon = False
        frmname.Show()
        frmname.Dock = DockStyle.Fill
        Dim tabp As New XtraTabPage

        tabp.Controls.Add(frmname)
        tabp.Text = frmname.Text
        pnlname.TabPages.Add(tabp)
        'pnlname.Controls.Add(frmname)
    End Sub

    Public Sub callnewforms(ByVal frmname As System.Windows.Forms.Form, ByVal pnlname As DevExpress.XtraTab.XtraTabControl)
        frmname.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frmname.TopLevel = False
        frmname.ShowInTaskbar = True
        frmname.ShowIcon = False
        frmname.StartPosition = FormStartPosition.CenterScreen
        frmname.WindowState = FormWindowState.Maximized
        frmname.Show()
        'frmname.Dock = DockStyle.Fill
        'Dim tabp As New XtraTabPage

        'tabp.Controls.Add(frmname)
        'tabp.Text = frmname.Text
        'pnlname.TabPages.Add(tabp)
        'pnlname.Controls.Add(frmname)
    End Sub

    Public Sub SetPayStatusRights(ByVal compcode As String, ByVal groupcode As String, ByVal desname As String, ByVal combobox As Windows.Forms.ComboBox)

        Dim dsTemp As New DataSet
        'With da.SelectCommand
        '    If .Connection.State = ConnectionState.Closed Then .Connection.Open()
        '    .CommandText = "spSelectPayStatusRights"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.Clear()
        '    .Parameters.Add("@compcode", SqlDbType.NVarChar, 50).Value = compcode
        '    .Parameters.Add("@groupcode", SqlDbType.NVarChar, 50).Value = groupcode
        '    .Parameters.Add("@desname", SqlDbType.NVarChar, 50).Value = desname
        '    da.AcceptChangesDuringFill = True
        '    da.Fill(dsTemp)

        combobox.DataSource = dsTemp.Tables(0)
        combobox.DisplayMember = "Description"
        combobox.ValueMember = "CODE"
        If combobox.Items.Count > 0 Then
            combobox.SelectedIndex = 0
        End If
        'End With


    End Sub

    Public Sub PrintReportTemplate(ByVal datagridview As DevExpress.XtraGrid.Views.Grid.GridView, ByVal ReportHeaderTitle As String _
                                   , ByVal PageHeaderTitle As String, ByVal PageFooterTitle As String, ByVal ReportFooterTitle As String)


        datagridview.OptionsPrint.PrintHeader = True
        datagridview.OptionsPrint.RtfReportHeader = ReportHeaderTitle
        datagridview.OptionsPrint.RtfPageHeader = vbNewLine & PageHeaderTitle & vbNewLine
        datagridview.OptionsPrint.PrintFooter = True
        datagridview.OptionsPrint.RtfPageFooter = vbNewLine & PageFooterTitle & vbNewLine
        datagridview.OptionsPrint.RtfReportFooter = ReportFooterTitle
        datagridview.ShowPrintPreview()
    End Sub

    Public Enum FormNameList
        PayStatus
        Deductions
        Payroll
    End Enum

    Public Enum TreeNameList
        Configurations
        GeneralSettings
        Payroll
        Deductions
        Statutory
    End Enum

    Public Enum Gender
        Male
        Female
    End Enum

    Public Sub suggestiontext(ByVal Qry As String, ByVal Txtactive As System.Windows.Forms.TextBox, ByVal da As SqlDataAdapter)
        Try
            Dim dt As New DataTable
            Dim nds As New DataSet
            If da.SelectCommand.Connection.State = ConnectionState.Closed Then da.SelectCommand.Connection.Open()
            With da
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = Qry
                .AcceptChangesDuringFill = True
                .Fill(nds)
            End With

            nds.Tables.Add(dt)

            Dim r As DataRow

            Txtactive.AutoCompleteCustomSource.Clear()
            For Each r In dt.Rows
                Txtactive.AutoCompleteCustomSource.Add(r.Item(0).ToString)
            Next
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub suggestiontextcombo(ByVal Qry As String, ByVal Txtactive As System.Windows.Forms.ComboBox, ByVal da As SqlDataAdapter)
        Try
            Dim dt As New DataTable
            Dim nds As New DataSet
            If da.SelectCommand.Connection.State = ConnectionState.Closed Then da.SelectCommand.Connection.Open()
            With da
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = Qry
                .AcceptChangesDuringFill = True
                .Fill(nds)
            End With
            nds.Tables.Add(dt)

            Dim r As DataRow

            'da.Fill(dt)
            Txtactive.Items.Clear()
            For Each r In dt.Rows
                Txtactive.Items.Add(r.Item(0).ToString)
            Next
            closecon()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub NumberValidate(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 11 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidateDX(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As DevExpress.XtraEditors.TextEdit)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 11 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidateHrs(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 6 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 3 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidate10(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 10 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidatewithDecimal(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As System.Windows.Forms.TextBox)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 9 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Sub NumberValidatewithDecimalDX(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txtNumVal As DevExpress.XtraEditors.TextEdit)
        Dim keyChar = e.KeyChar

        If Char.IsControl(keyChar) Then
        ElseIf Char.IsDigit(keyChar) OrElse keyChar = "."c Then
            Dim text = txtNumVal.Text
            Dim selectionStart = txtNumVal.SelectionStart
            Dim selectionLength = txtNumVal.SelectionLength

            text = text.Substring(0, selectionStart) & keyChar & text.Substring(selectionStart + selectionLength)

            If Integer.TryParse(text, New Integer) AndAlso text.Length > 16 Then
                e.Handled = True
            ElseIf Double.TryParse(text, New Double) AndAlso text.IndexOf("."c) < text.Length - 9 Then
                e.Handled = True
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        Dim allowChrs() As Char = "0123456789".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next

        Return sResult
    End Function

    Public Function FindItem(ByVal LV As ListView, ByVal TextToFind As String) As Integer

        ' Loop through LV’s ListViewItems.
        Dim getlen As Integer = TextToFind.Length
        For i As Integer = 0 To LV.Items.Count - 1
            If Trim(LV.Items(i).Text.ToLower).Length >= Trim(TextToFind.ToLower).Length Then
                If Trim(LV.Items(i).Text.ToLower.Substring(0, getlen)) = Trim(TextToFind.ToLower) Then
                    ' If found, return the row number
                    Return (i)
                End If
            Else
                If Trim(LV.Items(i).Text.ToLower) = Trim(TextToFind.ToLower) Then
                    ' If found, return the row number
                    Return (i)
                End If
            End If
            For subitem As Integer = 0 To LV.Items(i).SubItems.Count - 1
                If Trim(LV.Items(i).SubItems(subitem).Text.ToString.ToLower).Length >= TextToFind.Length Then
                    If Trim(LV.Items(i).SubItems(subitem).Text.ToString.ToLower.Substring(0, getlen)) = Trim(TextToFind.ToLower) Then
                        ' If found, return the row number
                        Return (i)
                    End If
                Else
                    If Trim(LV.Items(i).SubItems(subitem).Text.ToString.ToLower) = Trim(TextToFind.ToLower) Then
                        ' If found, return the row number
                        Return (i)
                    End If
                End If
            Next
        Next
        ' If not found, then return -1.
        Return -1
    End Function

    Public Function FindItemonLV(ByVal LV As ListView, ByVal TextToFind As String, ByVal previndex As Integer) As Integer
        If previndex = -1 Then previndex = 0
        Dim getlen As Integer = TextToFind.Length
        ' Loop through LV’s ListViewItems.
        For i As Integer = previndex + 1 To LV.Items.Count - 1
            If Trim(LV.Items(i).Text.ToLower).Length >= Trim(TextToFind.ToLower).Length Then
                If Trim(LV.Items(i).Text.ToLower.Substring(0, getlen)) = Trim(TextToFind.ToLower) Then
                    ' If found, return the row number
                    Return (i)
                End If
            Else
                If Trim(LV.Items(i).Text.ToLower) = Trim(TextToFind.ToLower) Then
                    ' If found, return the row number
                    Return (i)
                End If
            End If

            For subitem As Integer = 0 To LV.Items(i).SubItems.Count - 1

                If Trim(LV.Items(i).SubItems(subitem).Text.ToString.ToLower).Length >= TextToFind.Length Then
                    If Trim(LV.Items(i).SubItems(subitem).Text.ToString.ToLower.Substring(0, getlen)) = Trim(TextToFind.ToLower) Then
                        ' If found, return the row number
                        Return (i)
                    End If
                Else
                    If Trim(LV.Items(i).SubItems(subitem).Text.ToString.ToLower) = Trim(TextToFind.ToLower) Then
                        ' If found, return the row number
                        Return (i)
                    End If
                End If
            Next
        Next
        ' If not found, then return -1.
        Return -1
    End Function

    Public Sub SelectCommandtoGridControl(ByVal Qry As String, ByVal GridControl1 As DevExpress.XtraGrid.GridControl, ByVal GridView1 As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            opencon1()
            GridControl1.DataSource = Nothing
            GridView1.Columns.Clear()
            GridControl1.Refresh()
            'da = New SqlDataAdapter(Qry, cn1)

            Dim dt As New DataTable
            Dim nds As New DataSet
            nds.Tables.Add(dt)
            'da.Fill(dt)

            GridControl1.DataSource = nds.Tables(0)
            GridControl1.Refresh()
            GridControl1.EndUpdate()

            closecon1()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub SelectCommandtoGridControl1(ByVal Qry As String, ByVal GridControl1 As DevExpress.XtraGrid.GridControl, ByVal GridView1 As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            opencon()
            GridControl1.DataSource = Nothing
            GridView1.Columns.Clear()
            GridControl1.Refresh()
            'da = New SqlDataAdapter(Qry, cn)

            Dim dt As New DataTable
            Dim nds As New DataSet
            nds.Tables.Add(dt)
            'da.Fill(dt)

            GridControl1.DataSource = nds.Tables(0)
            GridControl1.Refresh()
            GridControl1.EndUpdate()

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub SelectCommandtoListView(ByVal Qry As String, ByVal NoCol As Integer, ByVal ListView1 As System.Windows.Forms.ListView)
        Try
            opencon()
            ListView1.Items.Clear()
            cmd = New SqlClient.SqlCommand(Qry, cn)
            cmd.CommandType = CommandType.StoredProcedure
            dr = cmd.ExecuteReader
            While dr.Read
                ListView1.Items.Add(dr.GetValue(0).ToString())

                For x As Integer = 1 To NoCol - 1
                    ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(dr.GetValue(x).ToString())
                Next

            End While

            closecon()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub showChildFormDLG(ByVal frm As Form, Optional ByVal frmBorderstyle As FormBorderStyle = FormBorderStyle.FixedSingle)
        Try
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.StartPosition = FormStartPosition.CenterScreen
            'frm.BringToFront()
            'frm.TopMost = True
            frm.ShowInTaskbar = False
            frm.FormBorderStyle = frmBorderstyle
            frm.MinimumSize = frm.Size
            'ControlProperty()

            'SortOrder(frm)

            frm.ShowDialog()

            frm.TopMost = True
        Catch sh As OutOfMemoryException

            frm.Hide()
            frm.Show()
            frm.Activate()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub showChildFormDLG1(ByVal frm As Form, Optional ByVal frmBorderstyle As FormBorderStyle = FormBorderStyle.FixedSingle)
        Try
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.BringToFront()
            frm.TopMost = True
            frm.ShowInTaskbar = False
            frm.FormBorderStyle = frmBorderstyle
            frm.MinimumSize = frm.Size
            'ControlProperty()

            'SortOrder(frm)

            frm.ShowDialog()

            frm.TopMost = True
        Catch sh As OutOfMemoryException

            frm.Hide()
            frm.Show()
            frm.Activate()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub showChildFormDLGwSize(ByVal frm As Form, Optional ByVal frmBorderstyle As FormBorderStyle = FormBorderStyle.FixedSingle, Optional ByVal height As Integer = 0, Optional ByVal width As Integer = 0)
        Try
            Dim size As System.Drawing.Size
            size.Width = width
            size.Height = height
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.StartPosition = FormStartPosition.CenterScreen
            'frm.BringToFront()
            frm.TopMost = True
            frm.ShowInTaskbar = False
            frm.FormBorderStyle = frmBorderstyle
            frm.MinimumSize = size
            'ControlProperty()

            'SortOrder(frm)

            frm.ShowDialog()

            frm.TopMost = True
        Catch sh As OutOfMemoryException

            frm.Hide()
            frm.Show()
            frm.Activate()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function ExecuteQuery(ByVal s As String, ByVal condb As SqlConnection, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim dt As DataTable = Nothing
        Using da As New System.Data.SqlClient.SqlDataAdapter(s, condb)
            dt = New DataTable

            If params.Length > 0 Then
                da.SelectCommand.Parameters.AddRange(params)
            End If

            If da.SelectCommand.Connection.State <> ConnectionState.Open Then da.SelectCommand.Connection.Open()
            da.Fill(dt)
        End Using
        Return dt
    End Function

    Public Sub GroupListView(ByVal lstV As ListView, ByVal SubItemIndex As Int16)
        Dim flag As Boolean = True
        For Each l As ListViewItem In lstV.Items
            Dim strmyGroupname As String = l.SubItems(SubItemIndex).Text
            For Each lvg As ListViewGroup In lstV.Groups
                If lvg.Name = strmyGroupname Then
                    l.Group = lvg
                    flag = False
                End If
            Next
            If flag = True Then
                Dim lstGrp As New ListViewGroup(strmyGroupname, strmyGroupname)
                lstV.Groups.Add(lstGrp)
                l.Group = lstGrp
            End If
            flag = True
        Next
    End Sub

    Public Sub callformsPanel(ByVal frmname As System.Windows.Forms.Form, ByVal pnlname As System.Windows.Forms.Panel)
        frmname.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        frmname.TopLevel = False
        frmname.ShowInTaskbar = False
        frmname.Show()
        frmname.Dock = DockStyle.Fill
        pnlname.Controls.Clear()
        pnlname.Controls.Add(frmname)
    End Sub

    Public Sub PrintOutstandingReport(ByVal dt As DataTable)
        Try
            Dim paramA As New List(Of Object)

            If System.IO.File.Exists(Application.StartupPath + "\DXZLS_OutstandingRpt.xml") Then
                System.IO.File.Delete(Application.StartupPath + "\DXZLS_OutstandingRpt.xml")
            End If

            If System.IO.File.Exists(Application.StartupPath + "\DXZLS_OutstandingRpt.xsd") Then
                System.IO.File.Delete(Application.StartupPath + "\DXZLS_OutstandingRpt.xsd")
            End If

            dt.WriteXml(Application.StartupPath + "\DXZLS_OutstandingRpt.xml")
            dt.WriteXmlSchema(Application.StartupPath + "\DXZLS_OutstandingRpt.xsd")
            Dim rviewer As New ReportViewer

            rviewer.LoadOutstandingReport(Application.StartupPath + "\DXZLS_OutstandingRpt.xsd", _
                                           Application.StartupPath + "\DXZLS_OutstandingRpt.xml", _
                                           paramA)
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try
    End Sub

    Public Sub PrintSOAReport(ByVal dt As DataTable, ByVal reporttype As String)
        Try

            Dim params As New List(Of Object)

            If System.IO.File.Exists(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xml") Then
                System.IO.File.Delete(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xml")
            End If

            If System.IO.File.Exists(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xsd") Then
                System.IO.File.Delete(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xsd")
            End If

            dt.WriteXml(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xml")
            dt.WriteXmlSchema(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xsd")
            Dim rviewer As New ReportViewer
            If reporttype = "SOASummary" Then
                rviewer.LoadSOAReportSummary(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xsd", _
                                           Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xml", _
                                           params)
            Else
                rviewer.LoadSOAReport(Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xsd", _
                                           Application.StartupPath + "\DXZLS_StmtOfAccntRpt.xml", _
                                           params)
            End If
            
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

        End Try
    End Sub

    Public Sub DeleteMasterFileTransaction(ByVal tableName As String, ByVal fieldname As String, ByVal bhid As Integer)
        Dim cnt As Integer = 0

        Try


            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp

                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspDeleteTransaction"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@tblName", tableName)
                    .AddWithValue("@strField", fieldname)
                    .AddWithValue("@intValue", bhid)
                End With
                'MsgBox(tableName & " " & fieldname & " " & bhid)
                cnt += .SelectCommand.ExecuteNonQuery()

            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If cnt > 0 Then
                MsgBox("Successfully deleted " & cnt & " transactions.")
            End If

        End Try
    End Sub

End Module
