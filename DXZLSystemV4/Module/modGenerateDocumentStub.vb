Imports System.Data.SqlClient
Imports DevExpress.XtraBars.Navigation
Imports System.Reflection

Module modGenerateDocumentStub
#Region "Create Object Functions"


    Public Function CreateObjectInstance(ByVal objectName As String) As Object
        ' Creates and returns an instance of any object in the assembly by its type name.

        Dim obj As Object

        Try
            If objectName.LastIndexOf(".") = -1 Then
                'Appends the root namespace if not specified.
                objectName = [Assembly].GetEntryAssembly.GetName.Name & "." & objectName
            End If

            obj = [Assembly].GetEntryAssembly.CreateInstance(objectName)

        Catch ex As Exception
            obj = Nothing
        End Try
        Return obj

    End Function

    Public Function CreateForm(ByVal formName As String) As Form
        ' Return the instance of the form by specifying its name.
        Return DirectCast(CreateObjectInstance(formName), Form)
    End Function

    Public Function CreateTreeView(ByVal treeViewName As String) As TreeView
        ' Return the instance of the form by specifying its name.
        Return DirectCast(CreateObjectInstance(treeViewName), TreeView)
    End Function
#End Region

    Public Function AddGroup(ByVal accrodionCtrl As AccordionControl, ByVal txt As String) As AccordionControlElement
        Dim group As New AccordionControlElement() With {.Text = txt, .Image = Global.DXZLSystemV4.My.Resources.AutoExpand_32x32}
        accrodionCtrl.Elements.Add(group)
        Return group
    End Function

    Public Function AddItem(ByVal gp As AccordionControlElement, ByVal txt As String) As AccordionControlElement
        Dim item As New AccordionControlElement() With {.Text = txt, .Style = ElementStyle.Item, .Image = Global.DXZLSystemV4.My.Resources.BOChangeHistory_16x16}
        gp.Elements.Add(item)
        Return item
    End Function

    Public Sub RefreshCode(ByVal transcode As String, ByVal transdesc As String, _
                             ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                             ByVal frm As DevExpress.XtraEditors.XtraForm)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspGenerateRefNum"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TransactionCode", transcode)
                    .AddWithValue("@FileName", "tbl" & transdesc & "File")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then

                txtcode.Text = dr.GetString(0)
            End If
            dr.Close()

            If txtcode.Text = "Error" Then

                frm.Close()
                frm.Dispose()
            End If

            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshCodeWin(ByVal transcode As String, ByVal transdesc As String, _
                             ByVal txtcode As System.Windows.Forms.TextBox, _
                             ByVal frm As DevExpress.XtraEditors.XtraForm)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspGenerateRefNum"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TransactionCode", transcode)
                    .AddWithValue("@FileName", "tbl" & transdesc & "File")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then

                txtcode.Text = dr.GetString(0)
            End If
            dr.Close()

            If txtcode.Text = "Error" Then

                frm.Close()
                frm.Dispose()
            End If

            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshRRCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtrrcode As DevExpress.XtraEditors.TextEdit, _
                             ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableRRCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "RR")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtrrcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtrrcode.Text = txtrrcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshRRAdjCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtrrcode As DevExpress.XtraEditors.TextEdit, _
                             ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableRRCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "RRADJ")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtrrcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtrrcode.Text = txtrrcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshSTCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                             ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableSTCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "ST")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshSTAdjCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                             ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableSTCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "STADJ")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshSRCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                             ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                             ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableSRCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "SR")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshSRAdjCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                            ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableSRCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "SRADJ")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshSWCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                            ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableSWCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "SW")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshSWAdjCode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                            ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableSWCode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "SWADJ")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub RefreshCICode(ByVal txtlocinitial As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtnature As DevExpress.XtraEditors.TextEdit, _
                            ByVal txtcode As DevExpress.XtraEditors.TextEdit, _
                            ByVal frm As System.Windows.Forms.Form)
        Try
            If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()

            With dadf
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_AvailableCICode"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@LocationID", global_PerLocation)
                    .AddWithValue("@Nature", "CI")
                End With
                dr = .SelectCommand.ExecuteReader
            End With

            If dr.Read Then
                txtlocinitial.Text = dr.GetString(0)
                txtnature.Text = dr.GetString(1)
                txtcode.Text = dr.GetString(2)
            End If
            dr.Close()

            If txtnature.Text = "Error" Then
                MsgBox(txtlocinitial.Text)
                frm.Close()
                frm.Dispose()
            End If

            txtlocinitial.Text = txtlocinitial.Text.Replace(" ", "")
            txtnature.Text = txtnature.Text.Replace(" ", "")
            txtcode.Text = txtcode.Text.Replace(" ", "")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    'ByVal da As SqlDataAdapter
    Public Sub InsertTransactionLogs()
        If dadf.SelectCommand.Connection.State = ConnectionState.Closed Then dadf.SelectCommand.Connection.Open()
        Dim FileName As String = System.Net.Dns.GetHostName & "-" & global_PerName
        Dim query As String = dadf.SelectCommand.CommandText + " "
        Dim pval As String = ""
        For Each p As SqlClient.SqlParameter In dadf.SelectCommand.Parameters

            If Double.TryParse(p.Value.ToString, 0) = False Then
                pval = "'" + p.Value.ToString + "'"
            Else
                pval = p.Value.ToString
            End If

            query = query + p.ParameterName + "=" + pval + ","
        Next

        With dadf
            .SelectCommand.Parameters.Clear()
            .SelectCommand.CommandText = "spInsert_TransactionLogs"
            .SelectCommand.Parameters.AddWithValue("@LogDesc", query.Substring(0, query.Length - 1))
            .SelectCommand.Parameters.AddWithValue("@LogUser", FileName)
            .SelectCommand.ExecuteNonQuery()

        End With

    End Sub

End Module
