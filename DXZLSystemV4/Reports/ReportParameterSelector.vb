Public Class ReportParameterSelector 
    Public rpttype As String = ""
    Dim hasRecord As Boolean
    Public RptName As String = ""
    Private dsLoanType As New DataSet
    Private bsLoanType As New BindingSource

    Public Sub QuickComputationBalance()

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()


            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspQuickComputationBalance"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@SearchCustomerID", txtCustomerID.Text)
                    .AddWithValue("@CustomerPaymentDate", My.Computer.Clock.LocalTime)

                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PrintReportOutstanding()
        Dim dt As New DataTable
        Dim ds As New DataSet

        dt.Clear()
        dt.TableName = "OutstandingRptTable"

        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

        With dacp
            .SelectCommand.CommandType = CommandType.StoredProcedure
            .SelectCommand.CommandText = "uspReport" & cmbLoanType.SelectedValue.ToString.Replace(" ", "") & "OutStanding"
            With .SelectCommand.Parameters
                .Clear()
                .AddWithValue("@CustomerID", txtCustomerID.Text)
                .AddWithValue("@DistrictID", txtDistrictID.Text)
                .AddWithValue("@SchoolID", txtSchoolID.Text)
            End With
            dr = .SelectCommand.ExecuteReader
            ds.Tables.Add(dt)
            dt.Load(dr)
            'If dr.Read Then
            '    If dr.HasRows Then
            '        hasRecord = True
            '    End If
            'End If

            dr.Close()
        End With
        If ds.Tables(0).Rows.Count > 0 Then
            PrintOutstandingReport(dt)
            Me.Close()
            Me.Dispose()
        Else
            MsgBox("No records available.")
        End If

    End Sub

    Private Sub PrintReportSOA(Optional ByVal rpt As String = "")
        Dim dt As New DataTable
        Dim ds As New DataSet

        dt.Clear()
        dt.TableName = "SOARptTable"

        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

        With dacp
            .SelectCommand.CommandType = CommandType.StoredProcedure
            .SelectCommand.CommandText = "spReport_StatementofAccountDT"
            With .SelectCommand.Parameters
                .Clear()
                .AddWithValue("@CustomerID", txtCustomerID.Text)
                .AddWithValue("@RefDate", MDIParent1.tsDateTimeChanger.Text)
                .AddWithValue("@PreparedBy", global_PerName)
                .AddWithValue("@ReportType", cmbLoanType.SelectedValue.ToString)
            End With
            dr = .SelectCommand.ExecuteReader
            ds.Tables.Add(dt)
            dt.Load(dr)
            'If dr.Read Then
            '    If dr.HasRows Then
            '        hasRecord = True
            '    End If
            'End If

            dr.Close()
        End With
        If ds.Tables(0).Rows.Count > 0 Then
            PrintSOAReport(dt, rpt)
            Me.Close()
            Me.Dispose()
        Else
            MsgBox("No records available.")
        End If

    End Sub

    Private Sub GenerateLoanType()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsLoanType.Clear()
            cmbLoanType.DataSource = Nothing
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()


            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_TransactionGroup"
                With .SelectCommand.Parameters
                    .Clear()
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsLoanType)
            End With
            Dim dv As DataView = dsLoanType.Tables(0).DefaultView

            dv.Sort = "TransactionGroupName ASC"
            bsLoanType.DataSource = dv
            cmbLoanType.DataSource = bsLoanType
            cmbLoanType.DisplayMember = "ReportType"
            cmbLoanType.ValueMember = "TransactionGroupName"
            'If cmbLoanType.Items.Count > 0 Then
            '    Me.cmbLoanType.SelectedValue = rpttype
            '    'cmbLoanType.SelectedValue = rpttype
            '    MsgBox(Me.cmbLoanType.SelectedValue.ToString)
            'End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim clist As New CustomerList
        With clist
            .txtFilter.Text = RTrim(LTrim(txtCustomerName.Text))
            .rptparam = Me
            .frmFocus = Me.Name
            .Show()
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
        End With
    End Sub

    Private Sub txtCustomerName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyDown
        If e.KeyData = Keys.Enter Then
            btnSearch.PerformClick()
        End If
    End Sub

    Private Sub btnGenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Dim wf As New WaitForm
        Try

            wf.ShowOnTopMode = DevExpress.XtraWaitForm.ShowFormOnTopMode.AboveAll
            wf.ShowIcon = False
            wf.ShowInTaskbar = False
            wf.Show()
            Me.Cursor = Cursors.WaitCursor

            If RptName = "OutstandingReport" Then
                PrintReportOutstanding()
            ElseIf RptName = "SOADetailed" Then
                PrintReportSOA(RptName)
            ElseIf RptName = "SOASummary" Then
                PrintReportSOA(RptName)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Me.Cursor = Cursors.Default
            wf.Close()
        End Try


        'System.Threading.Thread.Sleep(2000)
        'QuickComputationBalance()

        Me.Cursor = Cursors.Default
        wf.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel printing?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub ReportParameterSelector_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hasRecord = False
        GenerateLoanType()
    End Sub

    Private Sub cmbLoanType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbLoanType.SelectedIndexChanged
       
    End Sub

    Private Sub ReportParameterSelector_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If cmbLoanType.Items.Count > 0 Then
            cmbLoanType.SelectedValue = rpttype.ToUpper
        End If
    End Sub
End Class