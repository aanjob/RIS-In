Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class XXFScmKomisi
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim rw As Integer = 0

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ScmKN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ScmKEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ScmKDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTScmKms_s.Enabled = True

        Me.TBTahun.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True

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
        Me.BVTScmKms_s.Enabled = False

        Me.TBTahun.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTScmKms_e.Selected = True
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ScmIDD,Tahun,P.PosisiID,JenisCust,stsHarga,JnsPerhit,BtsBawah,BtsAtas,TelatBawah,TelatAtas,JnsKomisi,Komisi From M_ScmKmsDtl D Inner Join M_Posisi P On D.PosisiID=P.PosisiID Where Tahun='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ScmKmsDtl")
        Try
            DsMaster.Tables("M_ScmKmsDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ScmKmsDtl")

        DsMaster.Tables("M_ScmKmsDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("M_ScmKmsDtl").Columns("Tahun"), DsMaster.Tables("M_ScmKmsDtl").Columns("PosisiID"), DsMaster.Tables("M_ScmKmsDtl").Columns("JenisCust"), DsMaster.Tables("M_ScmKmsDtl").Columns("stsHarga"), DsMaster.Tables("M_ScmKmsDtl").Columns("BtsBawah"), DsMaster.Tables("M_ScmKmsDtl").Columns("BtsAtas"), DsMaster.Tables("M_ScmKmsDtl").Columns("TelatBawah"), DsMaster.Tables("M_ScmKmsDtl").Columns("TelatAtas")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_ScmKmsDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tahun,Tanggal,Ket,Aktif,InsDate,InsBy,UpdDate,UpdBy From M_ScmKms Where Tahun=" & MainModule.periodeTahun & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_ScmKms")
        Try
            DsMaster.Tables("M_ScmKms").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ScmKms")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_ScmKms"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelM_ScmKms")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBTahun.EditValue
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
            End Try
        End With
    End Sub

    Private Sub FScmKomisi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Scheme Komisi"
    End Sub

    Private Sub FScmKomisi_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FScmKomisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTScmKms_e.Selected = True
    End Sub


    Private Sub BVTScmKms_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTScmKms_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Scheme Komisi"
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Scheme Komisi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        CekSave = True

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBTahun.EditValue = MainModule.periodeTahun
        Me.MKet.EditValue = ""
        Me.CEAktif.EditValue = True

        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBTahun.EditValue)
        DsMaster.Tables("M_ScmKmsDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Scheme Komisi"

        Indicator = "200"
        Me.TBTahun.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tahun")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.CEAktif.EditValue = Me.GridView2.GetFocusedDataRow.Item("Aktif")

        FillDtl(Me.TBTahun.EditValue)
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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Scheme Komisi"

        koneksi.Close()

        If MainModule.SlCekPencapaian(Me.GridView2.GetFocusedDataRow.Item("Tahun"), Me.GridView2.GetFocusedDataRow.Item("Bulan"), Me.GridView2.GetFocusedDataRow.Item("SubJns")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_ScmKms")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Tahun")
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
                End Try
            End With
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_ScmKms")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "PosisiID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_ScmKmsDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                    .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                                    .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                    .Parameters.Add("@JnsPerhit", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPerhit")
                                    .Parameters.Add("@Bawah", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtsBawah")
                                    .Parameters.Add("@Atas", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtsAtas")
                                    .Parameters.Add("@TelatBawah", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TelatBawah")
                                    .Parameters.Add("@TelatAtas", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TelatAtas")
                                    .Parameters.Add("@JnsKomisi", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
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
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBTahun.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpM_ScmKms")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Aktif", SqlDbType.Bit).Value = Me.CEAktif.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelM_ScmKmsDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "ScmIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "PosisiID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsM_ScmKmsDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                        .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@JnsPerhit", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPerhit")
                                        .Parameters.Add("@Bawah", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtsBawah")
                                        .Parameters.Add("@Atas", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtsAtas")
                                        .Parameters.Add("@TelatBawah", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TelatBawah")
                                        .Parameters.Add("@TelatAtas", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TelatAtas")
                                        .Parameters.Add("@JnsKomisi", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsKomisi")
                                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
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
                                        Me.GridView1.SetRowCellValue(i, "ScmIDD", Me.GridView1.GetRowCellValue(i, "ScmIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "PosisiID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpM_ScmKmsDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ScmIDD")
                                        .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                        .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@JnsPerhit", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPerhit")
                                        .Parameters.Add("@Bawah", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtsBawah")
                                        .Parameters.Add("@Atas", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtsAtas")
                                        .Parameters.Add("@TelatBawah", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TelatBawah")
                                        .Parameters.Add("@TelatAtas", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TelatAtas")
                                        .Parameters.Add("@JnsKomisi", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsKomisi")
                                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
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


    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ScmIDD")
        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tahun", Me.TBTahun.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ScmIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "PosisiID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "JenisCust", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "stsHarga", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "JnsPerhit", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BtsBawah", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BtsAtas", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Terlambat", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "JnsKomisi", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Komisi", 0.0)

            rw += 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FScmKomisi_d(Me.GridView2.GetFocusedDataRow.Item("Tahun"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTrmBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub BEdPosisiID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdPosisiID.ButtonClick
        Dim frm As New FSearch("Posisi", "", "", "", Me.DTPTanggal.EditValue, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdJnsCust_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdJnsCust.ButtonClick
        Dim frm As New FSearch("Jenis Cust", "", "", "", Me.DTPTanggal.EditValue, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdstsHarga_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdstsHarga.ButtonClick
        Dim frm As New FSearch("Sts Harga", "", "", "", Me.DTPTanggal.EditValue, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdPosisiID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdPosisiID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdJnsCust_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdJnsCust.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdstsHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdstsHarga.KeyPress
        e.Handled = True
    End Sub

    Private Sub MKet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class
