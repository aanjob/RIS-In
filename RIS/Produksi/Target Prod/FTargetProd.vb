Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FTargetProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim rw As Integer = 0

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrBN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTargetProd_s.Enabled = True

        Me.SLUArtCode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTargetProd_s.Enabled = False

        Me.SLUArtCode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTargetProd_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct MerkID+KatID+JnsID+'-'+Urut As ArtCode,(Select ArtName + ', '  From (Select Distinct ArtName From M_Brg B Where B.MerkID+B.KatID+B.JnsID+'-'+B.Urut=M_Brg.MerkID+M_Brg.KatID+M_Brg.JnsID+'-'+M_Brg.Urut) as q order By ArtName FOR XML PATH('')) As ArtName From M_Brg", koneksi)
        cmsl.TableMappings.Add("Table", "M_ArtLUE")
        cmsl.Fill(DsMaster, "M_ArtLUE")
        DsMaster.Tables("M_ArtLUE").Clear()
        cmsl.Fill(DsMaster, "M_ArtLUE")

        Me.SLUArtCode.Properties.DataSource = DsMaster.Tables("M_ArtLUE")
        Me.SLUArtCode.Properties.DisplayMember = "ArtName"
        Me.SLUArtCode.Properties.ValueMember = "ArtCode"
    End Sub

    Public Sub FillDtl(ArtCode As String, Tgl As Date)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TargetIDD,Art,TglBerlaku,Proses,Target From T_TargetProdDtl Where Art='" & ArtCode & "' and TglBerlaku='" & Tgl & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetProdDtl")
        Try
            DsMaster.Tables("T_TargetProdDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TargetProdDtl")

        DsMaster.Tables("T_TargetProdDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TargetProdDtl").Columns("Art"), DsMaster.Tables("T_TargetProdDtl").Columns("Proses")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TargetProdDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Art,(Select ArtName + ', '  From (Select Distinct ArtName From M_Brg B Where B.MerkID+B.KatID+B.JnsID+'-'+B.Urut=Art) as q order By ArtName FOR XML PATH('')) As ArtName,TglBerlaku,Aktif,InsDate,InsBy,UpdDate,UpdBy From T_TargetProd", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetProd")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_TargetProd")
        DsMaster.Tables("T_TargetProd").Clear()
        cmsl.Fill(DsMaster, "T_TargetProd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TargetProd"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TargetProd")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
            .Parameters.Add("@Tgl", SqlDbType.DateTime).Value = Me.DTPTanggal.EditValue
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            Try
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub FTargetProd_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Target Produksi"
    End Sub

    Private Sub FTargetProd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FTargetProd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FTargetProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTargetProd_e.Selected = True
    End Sub
    Private Sub BVTTargetProd_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTargetProd_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Target Produksi"
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Target Produksi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        Me.SLUArtCode.EditValue = ""
        Me.TBArtName.EditValue = ""
        Me.DTPTanggal.EditValue = DateTime.Now
        Me.TBInfo.EditValue = ""

        FillDtl(Me.SLUArtCode.EditValue, Me.DTPTanggal.EditValue)
        DsMaster.Tables("T_TargetProdDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Target Produksi"

        LUE()

        Indicator = "200"
        Me.SLUArtCode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Art")
        Me.TBArtName.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtName")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglBerlaku")

        FillDtl(Me.SLUArtCode.EditValue, Me.DTPTanggal.EditValue)

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Target Produksi"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("Art") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TargetProd")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Art")
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi

                Try
                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    Else
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If
    End Sub


    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_TargetProd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Art")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TargetProdDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Proses")
                                    .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Target")
                                    .Parameters.Add("@Return", SqlDbType.Int)
                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                    .Connection = koneksi
                                End With

                                With koneksi
                                    .Open()
                                    cmSPDtl.ExecuteNonQuery()
                                    x = cmSPDtl.Parameters("@Return").Value
                                    .Close()
                                End With

                                If x <> 0 Then
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.SLUArtCode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            Del()
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_TargetProd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_TargetProdDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi

                                With koneksi
                                    .Open()
                                    cmSPDel.ExecuteNonQuery()
                                    .Close()
                                End With

                            End With
                        Next

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "TargetIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Art")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TargetProdDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Proses")
                                        .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Target")
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        .Close()
                                    End With

                                    If x = 0 Then
                                        Me.GridView1.SetRowCellValue(i, "TargetIDD", Me.GridView1.GetRowCellValue(i, "TargetIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Art")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TargetProdDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TargetIDD")
                                        .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Proses")
                                        .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Target")
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        .Close()
                                    End With

                                    If x <> 0 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
        End Select

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUArtCode_Leave(sender As Object, e As EventArgs) Handles SLUArtCode.Leave
        Me.TBArtName.EditValue = DsMaster.Tables("M_ArtLUE").Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("ArtName")
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TargetIDD")
        End If
    End Sub
    
    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetRowCellValue(e.RowHandle, "TargetIDD", Me.GridView1.RowCount * -1)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Art", Me.SLUArtCode.EditValue)
        Me.GridView1.SetRowCellValue(e.RowHandle, "TglBerlaku", Me.DTPTanggal.EditValue)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Proses", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Target", 0)
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTargetProd_d(Me.GridView2.GetFocusedDataRow.Item("Art"), Me.GridView2.GetFocusedDataRow.Item("GdID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdProses_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdProses.ButtonClick
        Try
            Dim frm As New FSearch("Proses Produksi", "", "", "", DateTime.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdProses_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdProses.KeyPress
        e.Handled = True
    End Sub

End Class