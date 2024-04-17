Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class ReportViewer
    Dim rpt As OutstandingReport
    Dim rpt1 As PaymentReceiptRpt
    Dim rpt2 As ReloanPaymentRpt
    Dim rpt3 As SOASummaryRpt
    Dim rpt4 As StatementofAccountRpt


    Public Sub LoadOutstandingReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        rpt = New OutstandingReport()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        rpt.SetDataSource(dt)

        For Each obj As Object In rptParams
            rpt.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.Refresh()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ShowDialog()
    End Sub

    Public Sub LoadPaymentReceiptReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        rpt1 = New PaymentReceiptRpt()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        rpt1.SetDataSource(dt)

        For Each obj As Object In rptParams
            rpt1.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = rpt1
        CrystalReportViewer1.Refresh()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ShowDialog()
    End Sub

    Public Sub LoadReloanPaymentReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        rpt2 = New ReloanPaymentRpt()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        rpt2.SetDataSource(dt)

        For Each obj As Object In rptParams
            rpt2.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = rpt2
        CrystalReportViewer1.Refresh()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ShowDialog()
    End Sub

    Public Sub LoadSOAReport(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        rpt3 = New SOASummaryRpt()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        rpt3.SetDataSource(dt)

        For Each obj As Object In rptParams
            rpt3.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = rpt3
        CrystalReportViewer1.Refresh()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ShowDialog()
    End Sub

    Public Sub LoadSOAReportSummary(ByVal schema As String, ByVal data As String, ByVal rptParams As List(Of Object))

        Dim paramCTR As Integer = 0
        rpt4 = New StatementofAccountRpt()

        Dim dt As DataTable = New DataTable()

        dt.ReadXmlSchema(schema)
        dt.ReadXml(data)

        rpt4.SetDataSource(dt)

        For Each obj As Object In rptParams
            rpt4.SetParameterValue(paramCTR, obj)
            paramCTR += 1
        Next

        CrystalReportViewer1.ReportSource = rpt4
        CrystalReportViewer1.Refresh()
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ShowDialog()

    End Sub
End Class