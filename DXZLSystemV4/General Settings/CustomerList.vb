Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.DXErrorProvider
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Camera
Imports DevExpress.Utils
Imports System.Drawing.Imaging
Imports DevExpress.XtraEditors.Controls

Public Class CustomerList
    Dim bsd, bsSchool, bsCategory, bsPaidBy As New BindingSource
    Dim dtView, dsSchool, dsCategory, dsPaidBy As New DataSet
    Dim isEdit As Boolean
    Friend WithEvents myLookSchool, myLookCategory, myLookPaidBy As New RepositoryItemLookUpEdit
    Public WithEvents myLookCustomerImage As New RepositoryItemImageComboBox
    Private WithEvents myLookTakePicture As New RepositoryItemButtonEdit
    Public cvedit As CashValeEdit
    Public bnedit As BonusEdit
    Public caedit As CashAdvanceEdit
    Public lvedit As LoanVoucherEdit
    Public pyedit As PaymentEdit
    Public rptparam As ReportParameterSelector
    Public rlvedit As ReloanPaymentEdit1
    Public dbmedit As DebitCreditMemoEdit
    Public frmFocus As String

    Private Sub RefreshDataSchool()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsSchool.Clear()
            myLookSchool.DataSource = Nothing
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_SchoolbyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "")
                    .AddWithValue("@ColumnName", "SchoolID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsSchool)
            End With
            bsSchool.DataSource = dsSchool.Tables(0)

            dsSchool.Tables(0).Columns(0).ColumnMapping = MappingType.Hidden
            dsSchool.Tables(0).Columns(3).ColumnMapping = MappingType.Hidden
            For x As Integer = 5 To 8
                dsSchool.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next


            Dim ColumnName As String = "SchoolName"
            With myLookSchool
                .SearchMode = SearchMode.AutoFilter
                .TextEditStyle = TextEditStyles.Standard
                .ReadOnly = False
                .DisplayMember = ColumnName
                .ValueMember = ColumnName
                .DataSource = bsSchool.DataSource
            End With
           
            dvList.Columns(ColumnName).ColumnEdit = myLookSchool
            dvList.Columns(ColumnName).ColumnEdit.NullText = ""
            'dvList.Columns(ColumnName).ColumnEdit.NullText = "<Select " & dvList.Columns(ColumnName).FieldName.ToString & ">"


            'With myLookTakePicture.Buttons(0)
            '    .Caption = "Ok"
            '    .Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
            '    '.Width = 15
            'End With
            'dvList.Columns("CustomerImage").ColumnEdit = myLookTakePicture
            'dvList.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RefreshDataCategory()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsCategory.Clear()
            myLookCategory.DataSource = Nothing
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_CategorybyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "")
                    .AddWithValue("@ColumnName", "CategoryID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsCategory)
            End With
            bsCategory.DataSource = dsCategory.Tables(0)

            For x As Integer = 2 To 5
                dsCategory.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next

            Dim ColumnName As String = "CategoryDescription"
          
            With myLookCategory
                .SearchMode = SearchMode.AutoFilter
                .TextEditStyle = TextEditStyles.Standard
                .ReadOnly = False
                .DisplayMember = ColumnName
                .ValueMember = ColumnName
                .DataSource = bsCategory.DataSource
            End With
            dvList.Columns(ColumnName).ColumnEdit = myLookCategory
            dvList.Columns(ColumnName).ColumnEdit.NullText = ""
            'dvList.Columns(ColumnName).ColumnEdit.NullText = "<Select " & dvList.Columns(ColumnName).FieldName.ToString & ">"

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RefreshDataPaidBy()
        Try
            Me.Cursor = Cursors.WaitCursor
            dsPaidBy.Clear()
            myLookPaidBy.DataSource = Nothing
            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "spSelect_PaidBybyFilter"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@TextFilter", "")
                    .AddWithValue("@ColumnName", "PaidByID")
                End With
                .AcceptChangesDuringFill = True
                .Fill(dsPaidBy)
            End With
            bsPaidBy.DataSource = dsPaidBy.Tables(0)

            For x As Integer = 2 To 5
                dsPaidBy.Tables(0).Columns(x).ColumnMapping = MappingType.Hidden
            Next

            Dim ColumnName As String = "PaidByDescription"
            With myLookPaidBy
                .SearchMode = SearchMode.AutoFilter
                .TextEditStyle = TextEditStyles.Standard
                .ReadOnly = False
                .DisplayMember = ColumnName
                .ValueMember = ColumnName
                .DataSource = bsPaidBy.DataSource
            End With
            dvList.Columns(ColumnName).ColumnEdit = myLookPaidBy
            dvList.Columns(ColumnName).ColumnEdit.NullText = ""
            'dvList.Columns(ColumnName).ColumnEdit.NullText = "<Select " & dvList.Columns(ColumnName).FieldName.ToString & ">"

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub HideColumn()
        With dvList
            .Columns("FullName").Visible = False
            .Columns("PhoneNo").Visible = False
            .Columns("MobileNo").Visible = False
            .Columns("CustomerImage").Visible = False
            '.Columns("SchoolID").Visible = False
            .Columns("CategoryID").Visible = False
            .Columns("PaidByID").Visible = False
            .Columns("PaidByDescription").Visible = False
            .Columns("CreatedBy").Visible = False
            .Columns("CreatedDateTime").Visible = False
            .Columns("ModifiedBy").Visible = False
            .Columns("ModifiedDateTime").Visible = False
        End With
    End Sub

    Private Sub SaveChanges()

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()
            If isEdit Then
                Dim i As Integer = dvList.FocusedRowHandle
                If isAllowed(Me.Text, "Edit") = "" And dvList.GetRowCellValue(i, "CustomerID").ToString <> "" Then MsgBox("Editing Features Disabled.") : Exit Sub

                With dacp
                    .SelectCommand.CommandType = CommandType.StoredProcedure
                    .SelectCommand.CommandText = "spInsert_Customer"
                    With .SelectCommand.Parameters
                        .Clear()
                        For Each col In dtView.Tables(0).Columns
                            If col.ToString <> "CustomerImage" Then
                                .AddWithValue("@" & col.ToString, dvList.GetRowCellValue(i, col.ToString))
                            End If

                        Next
                        Dim imgArray As Byte()
                        Dim converter As ImageConverter = New ImageConverter
                        imgArray = CType(converter.ConvertTo(My.Resources.defaultimg, GetType(System.Byte())), Byte())
                        .AddWithValue("@CustomerImage", imgArray)
                        '.AddWithValue("@CompanyID", dvCompanyList.GetRowCellValue(i, "CompanyID"))
                        '.AddWithValue("@CompanyName", dvCompanyList.GetRowCellValue(i, "CompanyName"))
                        '.AddWithValue("@CompanyStatus", dvCompanyList.GetRowCellValue(i, "CompanyStatus"))
                        '.AddWithValue("@CompanyInitial", dvCompanyList.GetRowCellValue(i, "CompanyInitial"))
                        '.AddWithValue("@CompanyAddress", dvCompanyList.GetRowCellValue(i, "CompanyAddress"))
                        '.AddWithValue("@CompanyTelNo", dvCompanyList.GetRowCellValue(i, "CompanyTelNo"))
                        '.AddWithValue("@CompanyTinNo", dvCompanyList.GetRowCellValue(i, "CompanyTinNo"))
                        '.AddWithValue("@CompanyEmailAddress", dvCompanyList.GetRowCellValue(i, "CompanyEmailAddress"))
                    End With
                    cnt = .SelectCommand.ExecuteNonQuery()

                End With
            Else
                For i As Integer = 0 To dvList.DataRowCount - 1
                    If dvList.GetRowCellValue(i, "CustomerID").ToString = "" Then


                        With dacp
                            .SelectCommand.CommandType = CommandType.StoredProcedure
                            .SelectCommand.CommandText = "spInsert_Customer"
                            With .SelectCommand.Parameters
                                .Clear()
                                For Each col In dtView.Tables(0).Columns
                                    If col.ToString <> "CustomerImage" Then
                                        .AddWithValue("@" & col.ToString, dvList.GetRowCellValue(i, col.ToString))
                                    End If
                                Next
                                Dim imgArray As Byte()
                                Dim converter As ImageConverter = New ImageConverter
                                imgArray = CType(converter.ConvertTo(My.Resources.defaultimg, GetType(System.Byte())), Byte())
                                .AddWithValue("@CustomerImage", imgArray)

                                '.AddWithValue("@CompanyID", dvCompanyList.GetRowCellValue(i, "CompanyID"))
                                '.AddWithValue("@CompanyName", dvCompanyList.GetRowCellValue(i, "CompanyName"))
                                '.AddWithValue("@CompanyStatus", dvCompanyList.GetRowCellValue(i, "CompanyStatus"))
                                '.AddWithValue("@CompanyInitial", dvCompanyList.GetRowCellValue(i, "CompanyInitial"))
                                '.AddWithValue("@CompanyAddress", dvCompanyList.GetRowCellValue(i, "CompanyAddress"))
                                '.AddWithValue("@CompanyTelNo", dvCompanyList.GetRowCellValue(i, "CompanyTelNo"))
                                '.AddWithValue("@CompanyTinNo", dvCompanyList.GetRowCellValue(i, "CompanyTinNo"))
                                '.AddWithValue("@CompanyEmailAddress", dvCompanyList.GetRowCellValue(i, "CompanyEmailAddress"))
                            End With
                            cnt = .SelectCommand.ExecuteNonQuery()

                        End With
                    End If
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            Me.Cursor = Cursors.Default
            If cnt > 0 Then
                MsgBox("Successfully Saved.")
                Me.Cursor = Cursors.WaitCursor
                PublicFilterData("", "CustomerID", cmbColumn, "spSelect_CustomerbyFilter" _
                                     , dtView, bsd, dgList, dvList)
                isEdit = False
                Me.Cursor = Cursors.Default
            End If

        End Try
    End Sub

    Public Sub QuickComputationBalance()

        Dim cnt As Integer = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            If dacp.SelectCommand.Connection.State = ConnectionState.Closed Then dacp.SelectCommand.Connection.Open()

            Dim i As Integer = dvList.FocusedRowHandle
            With dacp
                .SelectCommand.CommandType = CommandType.StoredProcedure
                .SelectCommand.CommandText = "uspQuickComputationBalance"
                With .SelectCommand.Parameters
                    .Clear()
                    .AddWithValue("@SearchCustomerID", dvList.GetRowCellDisplayText(i, "CustomerID"))
                    .AddWithValue("@CustomerPaymentDate", My.Computer.Clock.LocalTime)

                End With
                cnt = .SelectCommand.ExecuteNonQuery()
            End With
        Catch ex As Exception

        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SelectItemfromGridView()
        If frmFocus = "" Or dvList.RowCount <= 0 Then Exit Sub

        Dim i As Integer = dvList.FocusedRowHandle
        If frmFocus = "CashValeEdit" Then
            With cvedit

                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "LastName").ToString & ", " & _
                    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                    dvList.GetRowCellValue(i, "MiddleName").ToString
            End With
            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "BonusEdit" Then
            With bnedit
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "LastName").ToString & ", " & _
                    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                    dvList.GetRowCellValue(i, "MiddleName").ToString
            End With
            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "CashAdvanceEdit" Then
            With caedit
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "LastName").ToString & ", " & _
                    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                    dvList.GetRowCellValue(i, "MiddleName").ToString
            End With
            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "LoanVoucherEdit" Then
            With lvedit
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "LastName").ToString & ", " & _
                    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                    dvList.GetRowCellValue(i, "MiddleName").ToString
            End With
            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "PaymentEdit" Then
            With pyedit
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "LastName").ToString & ", " & _
                    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                    dvList.GetRowCellValue(i, "MiddleName").ToString
                .QuickComputationBalance()
                .RefreshUnpaidBalanceFile(Integer.Parse(.txtCustomerID.Text), "-RS")
            End With
            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "ReportParameterSelector" Then
            With rptparam
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "LastName").ToString & ", " & _
                    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                    dvList.GetRowCellValue(i, "MiddleName").ToString
                .txtSchoolID.Text = dvList.GetRowCellValue(i, "SchoolID").ToString
                .txtDistrictID.Text = dvList.GetRowCellValue(i, "DistrictID").ToString
                .QuickComputationBalance()
            End With

            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "ReloanPaymentEdit1" Then
            With rlvedit
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "FullName").ToString '& ", " & _
                '    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                '    dvList.GetRowCellValue(i, "MiddleName").ToString

            End With

            Me.Close()
            Me.Dispose()
        ElseIf frmFocus = "DebitCreditMemoEdit" Then
            With dbmedit
                .txtCustomerID.Text = dvList.GetRowCellValue(i, "CustomerID").ToString
                .txtCustomerName.Text = dvList.GetRowCellValue(i, "FullName").ToString '& ", " & _
                '    dvList.GetRowCellValue(i, "FirstName").ToString & " " & _
                '    dvList.GetRowCellValue(i, "MiddleName").ToString

            End With

            Me.Close()
            Me.Dispose()

        End If
    End Sub

    Private Sub AddCustomerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddCompanyToolStripMenuItem.Click
        dvList.OptionsBehavior.ReadOnly = False
        dvList.AddNewRow()
        With dvList
            .Columns(0).OptionsColumn.ReadOnly = True
            .SetRowCellValue(.FocusedRowHandle, "CreditLimit", 0.0)
            .SetRowCellValue(.FocusedRowHandle, "CreatedBy", global_PerName)
            .SetRowCellValue(.FocusedRowHandle, "CreatedDateTime", Now())
            .FocusedColumn = .VisibleColumns(0)
        End With
    End Sub

    Private Sub EditCustomerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditCompanyToolStripMenuItem.Click
        dvList.OptionsBehavior.ReadOnly = False
        With dvList
            .Columns(0).OptionsColumn.ReadOnly = True
        End With
        isEdit = True
    End Sub

    Private Sub SaveChangesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveChangesToolStripMenuItem.Click
        Me.Validate()
        If Me.DxErrorProvider1.HasErrors Then Exit Sub

        If MsgBox("Are you sure you want to save the transaction?", MsgBoxStyle.YesNo, "PROMPT") = MsgBoxResult.Yes Then
            SaveChanges()
        End If
    End Sub

    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyData = Keys.Enter Then
            If cmbColumn.Text.ToString = String.Empty Then Exit Sub

            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_CustomerbyFilter" _
                                  , dtView, bsd, dgList, dvList, False)
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub CustomerList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData(txtFilter.Text, "LastName", cmbColumn, "spSelect_CustomerbyFilter" _
                             , dtView, bsd, dgList, dvList)
        HideColumn()
    End Sub

    Private Sub CustomerList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        
        RefreshDataSchool()
        RefreshDataCategory()
        RefreshDataPaidBy()
        myLookCustomerImage = TryCast(dgList.RepositoryItems.Add("ImageComboBoxEdit"), RepositoryItemImageComboBox)
        'myLookCustomerImage.PictureChecked = Global.DXZLSystemV4.My.Resources.Index_32x32
        'myLookCustomerImage.PictureUnchecked = Global.DXZLSystemV4.My.Resources.Index_32x32

        myLookCustomerImage.NullText = " "
        'myLookCustomerImage.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        dvList.Columns("CustomerImage").ColumnEdit = myLookCustomerImage
        isEdit = False
        If frmFocus <> "" Then
            HideColumn()
        End If
        'dvList.OptionsBehavior.ReadOnly = True
        Me.Cursor = Cursors.Default
        DxErrorProvider1.DataSource = bsd.DataSource
        dvList.Focus()

        dvList.MoveFirst()
        dvList.FocusedColumn = dvList.Columns(0)
        dvList.FocusedRowHandle = 0
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        PublicFilterData("", "CustomerID", cmbColumn, "spSelect_CustomerbyFilter" _
                             , dtView, bsd, dgList, dvList)
        isEdit = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        If isAllowed(Me.Text, "Print") = "" Then Exit Sub
        PrintReportTemplate(dvList, Me.Text.ToUpper.ToString & " REPORT", "This report is a system generated reports." _
                             , "", "END OF REPORT")
    End Sub

    Private Sub myLookSchool_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookSchool.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.LookUpEdit = TryCast(sender, DevExpress.XtraEditors.LookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsSchool.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
                With dvList
                    .SetRowCellValue(.FocusedRowHandle, "SchoolID", rw(0))
                    .SetRowCellValue(.FocusedRowHandle, "DistrictID", rw(2))
                    .SetRowCellValue(.FocusedRowHandle, "DistrictName", rw(4))
                    .FocusedColumn = .VisibleColumns(0)
                End With
                Exit For
            End If
        Next
    End Sub

    Private Sub myLookCategory_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookCategory.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.LookUpEdit = TryCast(sender, DevExpress.XtraEditors.LookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsCategory.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
                With dvList
                    .SetRowCellValue(.FocusedRowHandle, "CategoryID", rw(0))
                    '.SetRowCellValue(.FocusedRowHandle, "CategoryDescription", rw(1))
                    .FocusedColumn = .VisibleColumns(0)
                End With
                Exit For
            End If
        Next
    End Sub

    Private Sub myLookPaidBy_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles myLookPaidBy.EditValueChanged
        Dim gridLookUpEdit As DevExpress.XtraEditors.LookUpEdit = TryCast(sender, DevExpress.XtraEditors.LookUpEdit)
        If Convert.ToString(gridLookUpEdit.EditValue) = "" Then Exit Sub
        For Each rw As DataRow In dsPaidBy.Tables(0).Rows
            If Convert.ToString(gridLookUpEdit.EditValue) = rw(1).ToString Then
                With dvList
                    .SetRowCellValue(.FocusedRowHandle, "PaidByID", rw(0))
                    '.SetRowCellValue(.FocusedRowHandle, "PaidByDescription", rw(1))
                    .FocusedColumn = .VisibleColumns(0)
                End With
                Exit For
            End If
        Next
    End Sub

    Private Sub TakeAPictureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeAPictureToolStripMenuItem.Click
        'Dim d As New TakePictureDialog()
        'If d.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    Dim images As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
        '    images.ImageSize = New Size(12, 12)
        '    images.AddImage(d.Image)
        '    'i.Save("D:\snapshot.bmp")
        '    myLookCustomerImage.SmallImages = images
        '    myLookCustomerImage.Items.Add(New ImageComboBoxItem("", 1, 0))
        '    myLookCustomerImage.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center

        '    'With dvList
        '    '    .SetRowCellValue(.FocusedRowHandle, "CustomerImage", myLookCustomerImage.SmallImages)
        '    'End With
        '    'myLookCustomerImage.PictureChecked = Image.FromFile("D:\\snapshot.bmp")
        '    'myLookCustomerImage.PictureUnchecked = Image.FromFile("D:\\snapshot.bmp")
        '    'With dvList
        '    '    .SetRowCellValue(.FocusedRowHandle, "CustomerImage", i)
        '    'End With
        'End If
        If dvList.SelectedRowsCount > 0 Then
            Dim tp As New TakePicture
            With tp
                .txtCustomerID.Text = dvList.GetFocusedRowCellValue("CustomerID").ToString
                .txtFullName.Text = dvList.GetFocusedRowCellValue("FullName").ToString
                .txtSchool.Text = dvList.GetFocusedRowCellValue("SchoolName").ToString
                .txtDistrict.Text = dvList.GetFocusedRowCellValue("DistrictName").ToString
                .GetImagefromFile()
            End With
                showChildFormDLG(tp)
        End If
        
    End Sub

    Private Sub myLookTakePicture_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles myLookTakePicture.ButtonClick
        Dim d As New TakePictureDialog()
        If d.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim i As Image = d.Image
            i.Save("D:\snapshot.bmp")
            'myLookCustomerImage.InitialImage = Image.FromFile("D:\snapshot.bmp")
            'With dvList

            '    .SetRowCellValue(.FocusedRowHandle, "CustomerImage", i)
            'End With
        End If
    End Sub

    Private Sub dvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dvList.DoubleClick
        If IsNothing(dvList.GetRowCellValue(dvList.FocusedRowHandle, dvList.Columns("RefBHID"))) = True Then MsgBox("No Record Selected.") : Exit Sub
        SelectItemfromGridView()
    End Sub

    Private Sub cmbColumn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColumn.SelectedIndexChanged
        If cmbColumn.SelectedItem.ToString <> "" Then
            If cmbColumn.Text.ToString = String.Empty Then Exit Sub

            Me.Cursor = Cursors.WaitCursor
            PublicFilterData(txtFilter.Text, cmbColumn.SelectedItem.ToString, cmbColumn, "spSelect_CustomerbyFilter" _
                                  , dtView, bsd, dgList, dvList, False)
            isEdit = False
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub CheckAccountBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAccountBalanceToolStripMenuItem.Click
        QuickComputationBalance()
        Dim i As Integer = dvList.FocusedRowHandle
        Dim unpaid As New UnpaidBalanceList
        With unpaid
            .customerid = Integer.Parse(dvList.GetRowCellDisplayText(i, "CustomerID").ToString)
            .customername = dvList.GetRowCellValue(i, "FullName").ToString
            .school = dvList.GetRowCellValue(i, "SchoolName").ToString
            .district = dvList.GetRowCellValue(i, "DistrictName").ToString
            .dateTrans = MDIParent1.tsDateTimeChanger.Text
            .TopLevel = True
            .StartPosition = FormStartPosition.CenterScreen
            .Show()

        End With
    End Sub

    Private Sub dvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dvList.KeyDown, txtFilter.KeyDown, cmbColumn.KeyDown, Me.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        ElseIf e.KeyData = Keys.Enter Then

            SelectItemfromGridView()
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If dvList.SelectedRowsCount <= 0 Then Exit Sub
        If isAllowed(Me.Text, "Delete") = "" Then Exit Sub
        Dim i As Integer = dvList.FocusedRowHandle
        DeleteMasterFileTransaction("tblCustomerMasterFile", "CustomerID", Integer.Parse(dvList.GetRowCellValue(i, dvList.Columns("CustomerID")).ToString()))
    End Sub
End Class