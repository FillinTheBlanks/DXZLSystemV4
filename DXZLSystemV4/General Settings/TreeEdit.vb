
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class TreeEdit
    Dim dtNavGroup As New DataSet
    Public SRelation As String
    Public IOrdering As Integer
    Public IParentTreeID As Integer
    Public SStatus As String
    Public isEdit As Boolean
    Public frmTreeList As New TreeViewList
    Public bs As BindingSource

    Private Sub RefreshData()
        dtNavGroup.Clear()
        If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
        With dacp
            .SelectCommand.CommandType = CommandType.Text
            .SelectCommand.CommandText = "SELECT  NavGroupDesc FROM vw_NavGroup ORDER BY NavGroupDesc"
            .AcceptChangesDuringFill = True
            .Fill(dtNavGroup)
        End With

        For Each r As DataRow In dtNavGroup.Tables(0).Rows
            cmbNavGroup.Properties.Items.Add(r(0))
        Next
    End Sub

    Private Sub UpdateTransaction(ByVal status As String)
        Dim cnt As Integer = 0
        Try
            If isAllowed(frmTreeList.Text, "Edit") = "" Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spUpdate_TreeControl"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TreeID", txtTreeID.Text)
                    .AddWithValue("@TreeName", txtTreeName.Text)
                    .AddWithValue("@NavGroupDesc", cmbNavGroup.Text)
                    .AddWithValue("@Status", status)
                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            frmTreeList.RefreshDataHeader("p", "")
            frmTreeList.RefreshDataDetail("c", "")
            Me.Close()
            Me.Dispose()
        End Try
    End Sub

    Private Sub AddTransaction()
        Dim cnt As Integer = 0
        Try
            If isAllowed(frmTreeList.Text, "Add") = "" Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spInsert_TreeControl"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TreeName", txtTreeName.Text)
                    .AddWithValue("@ParentChild", SRelation)
                    .AddWithValue("@ParentTreeID", IParentTreeID)
                    .AddWithValue("@NavGroupDesc", cmbNavGroup.Text)
                    .AddWithValue("@Status", SStatus)
                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
            frmTreeList.RefreshDataHeader("p", "")
            frmTreeList.RefreshDataDetail("c", "")
            Me.Close()
            Me.Dispose()
        End Try
    End Sub

    Private Sub TreeEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshData()
        'bs = New BindingSource
        'bs.DataSource = GetType(MyRecord)
        'Dim rec As MyRecord = New MyRecord
        'rec.TreeName = ""
        'rec.NavGroup = cmbNavGroup.Text
        'bs.Add(rec)

        'txtTreeName.DataBindings.Add(New Binding("EditValue", Me.bs, "TreeName", True))
        'cmbNavGroup.DataBindings.Add(New Binding("EditValue", Me.bs, "NavGroup", True))
        'DxErrorProvider1.DataSource = bs
        'DxErrorProvider1.ContainerControl = Me

    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            If isEdit Then
                UpdateTransaction("A")
            Else
                AddTransaction()
            End If
        End If

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MsgBox("Are you sure you want to deactivate the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            If isEdit Then
                UpdateTransaction("D")
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Are you sure you want to cancel the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub
    
End Class

Public Class MyTreeRecord
    Implements IDXDataErrorInfo

    Private _treeName As String
    Private _navgroup As String
    Public Sub New()
        MyBase.New()
    End Sub
    Public Property TreeName() As String
        Get
            Return _treeName
        End Get
        Set(ByVal value As String)
            _treeName = value
        End Set
    End Property
    Public Property NavGroup() As String
        Get
            Return _navgroup
        End Get
        Set(ByVal value As String)
            _navgroup = value
        End Set
    End Property

    Public Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) _
     Implements IDXDataErrorInfo.GetPropertyError
        If ((propertyName = "TreeName" AndAlso TreeName = "") _
        OrElse (propertyName = "NavGroup" AndAlso NavGroup = "")) Then
            info.ErrorText = String.Format("The '{0}' field cannot be empty", propertyName)
        End If
    End Sub

    ' Implements the IDXDataErrorInfo.GetPropertyError method.
    Public Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
    End Sub
End Class
