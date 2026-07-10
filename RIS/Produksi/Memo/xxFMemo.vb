Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class xxFMemo
    'Dim koneksi As New SqlConnection(GlobalKoneksi)
    'Public Indicator As Int16

    'Sub CekAkses()
    '    Me.BVBNew.Enabled = CType(TcodeCollection.Item("GdN"), Boolean)
    '    Me.BVBEdit.Enabled = CType(TcodeCollection.Item("GdEd"), Boolean)
    '    Me.BVBDelete.Enabled = CType(TcodeCollection.Item("GdDel"), Boolean)
    'End Sub

    'Private Sub LockControl()
    '    CekAkses()
    '    Me.BVBExit.Enabled = True
    '    Me.BVTMemo_s.Enabled = True

    '    Me.SLUStyleID.Properties.ReadOnly = True
    '    Me.TBDivisi.Properties.ReadOnly = True
    '    Me.TBPerintah.Properties.ReadOnly = True
    '    Me.MMasalah.Properties.ReadOnly = True
    '    Me.MSolusi.Properties.ReadOnly = True

    '    Me.BSave.Enabled = False
    '    Me.BSave.Enabled = False
    '    Me.BCancel.Enabled = False
    '    Me.BCancel.Enabled = False
    'End Sub

    'Private Sub OpenControl()
    '    Me.BVBNew.Enabled = False
    '    Me.BVBEdit.Enabled = False
    '    Me.BVBDelete.Enabled = False
    '    Me.BVBExit.Enabled = False
    '    Me.BVTMemo_s.Enabled = False

    '    Me.SLUStyleID.Properties.ReadOnly = False
    '    Me.TBDivisi.Properties.ReadOnly = False
    '    Me.TBPerintah.Properties.ReadOnly = False
    '    Me.MMasalah.Properties.ReadOnly = False
    '    Me.MSolusi.Properties.ReadOnly = False

    '    Me.BSave.Enabled = True
    '    Me.BCancel.Enabled = True

    '    Me.BVTMemo_e.Selected = True
    'End Sub

    'Public Sub LUE()
    '    Dim cmsl As SqlDataAdapter

    '    cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)
    '    cmsl.TableMappings.Add("Table", "M_StyleLUE")
    '    Try
    '        DsMaster.Tables("M_StyleLUE").Clear()

    '    Catch ex As Exception

    '    End Try
    '    cmsl.Fill(DsMaster, "M_StyleLUE")

    '    Me.SLUStyleID.Properties.DataSource = DsMaster.Tables("M_StyleLUE")
    '    Me.SLUStyleID.Properties.DisplayMember = "StyleID"
    '    Me.SLUStyleID.Properties.ValueMember = "StyleID"

    'End Sub

    'Public Sub FillDt()
    '    Dim cmsl As SqlDataAdapter
    '    cmsl = New SqlDataAdapter("Select Aktif,Pusat,GdID,G.Nama,Alamat,G.KotaID,K.Nama As Kota, Induk, (Select Isnull((Select Nama From T_Memo Where GdId=G.Induk),'')) As GdInduk,Grup,Gol,InisialBC,G.InsDate,G.InsBy,G.UpdDate,G.UpdBy From T_Memo G Inner Join M_Kota K On G.KotaID=K.KotaID", koneksi)

    '    cmsl.TableMappings.Add("Table", "T_Memo")
    '    Try
    '        DsMaster.Tables("T_Memo").Clear()
    '    Catch ex As Exception

    '    End Try
    '    cmsl.Fill(DsMaster, "T_Memo")

    '    Me.GridControl1.DataSource = DsMaster
    '    Me.GridControl1.DataMember = "T_Memo"
    'End Sub

    'Private Sub FGudang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    '    CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Transaksi Memo"
    'End Sub

    'Private Sub FGudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    LockControl()
    '    Me.BVTMemo_e.Selected = True
    'End Sub

    'Private Sub BVTMemo_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTMemo_s.ItemPressed
    '    CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Transaksi Memo"

    '    FillDt()
    'End Sub

    'Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
    '    CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Transaksi Memo"

    '    OpenControl()
    '    LUE()

    '    Indicator = "100"

    '    Me.SLUStyleID.EditValue = ""
    '    Me.TBDivisi.EditValue = ""
    '    Me.TBPerintah.EditValue = ""
    '    Me.MMasalah.EditValue = ""
    '    Me.MSolusi.EditValue = ""
    '    Me.TBInfo.EditValue = ""

    'End Sub

    'Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
    '    CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Transaksi Memo"

    '    LUE()

    '    Indicator = "200"
    '    Me.SLUStyleID.EditValue = Me.GridView1.GetFocusedDataRow.Item("StyleID")
    '    Me.TBDivisi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Divisi")
    '    Me.TBPerintah.EditValue = Me.GridView1.GetFocusedDataRow.Item("Perintah")
    '    Me.MMasalah.EditValue = Me.GridView1.GetFocusedDataRow.Item("Masalah")
    '    Me.MSolusi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Solusi")

    '    If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
    '        Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
    '    Else
    '        Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
    '    End If

    '    OpenControl()
    'End Sub

    'Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
    '    CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Transaksi Memo"

    '    koneksi.Close()

    '    If XtraMessageBox.Show("Apakah Anda Mau Menghapus Gudang : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '        Dim cmSP As New SqlCommand("SPDelT_Memo")
    '        cmSP.CommandType = CommandType.StoredProcedure
    '        Dim x As Integer

    '        With cmSP
    '            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("StyleID")
    '            .Parameters.Add("@Return", SqlDbType.Int)
    '            .Parameters("@Return").Direction = ParameterDirection.Output
    '            .Connection = koneksi

    '            Try
    '                With koneksi
    '                    .Open()
    '                    cmSP.ExecuteNonQuery()
    '                    x = cmSP.Parameters("@Return").Value
    '                    .Close()
    '                End With

    '                If x = 0 Then
    '                    XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    FillDt()
    '                Else
    '                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                End If

    '            Catch ex As Exception
    '                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try
    '        End With
    '    End If
    'End Sub

    'Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
    '    Me.Dispose()
    'End Sub


    'Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
    '    koneksi.Close()

    '    Select Case Indicator
    '        Case 100
    '            Dim cmSP As New SqlCommand("SPInsT_Memo")
    '            cmSP.CommandType = CommandType.StoredProcedure
    '            Dim x As Integer

    '            With cmSP
    '                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
    '                .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
    '                .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
    '                .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
    '                .Parameters.Add("@Induk", SqlDbType.VarChar).Value = Me.SLUInduk.EditValue
    '                .Parameters.Add("@Pusat", SqlDbType.Bit).Value = Me.CEPusat.EditValue
    '                .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUStyleID.EditValue
    '                .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
    '                .Parameters.Add("@InsBC", SqlDbType.VarChar).Value = Me.TBInsBC.EditValue
    '                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
    '                .Parameters.Add("@Return", SqlDbType.Int)
    '                .Parameters("@Return").Direction = ParameterDirection.Output
    '                .Connection = koneksi

    '                Try
    '                    With koneksi
    '                        .Open()
    '                        cmSP.ExecuteNonQuery()
    '                        x = cmSP.Parameters("@Return").Value
    '                        .Close()
    '                    End With
    '                    If x = 0 Then
    '                        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    ElseIf x = 1 Then
    '                        XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                        Exit Sub
    '                    Else
    '                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                        Exit Sub
    '                    End If

    '                Catch ex As Exception
    '                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Exit Sub
    '                End Try
    '            End With
    '        Case 200
    '            Dim cmSP As New SqlCommand("SPUpT_Memo")
    '            cmSP.CommandType = CommandType.StoredProcedure
    '            Dim x As Integer

    '            With cmSP
    '                .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
    '                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
    '                .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
    '                .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
    '                .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
    '                .Parameters.Add("@Induk", SqlDbType.VarChar).Value = Me.SLUInduk.EditValue
    '                .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUStyleID.EditValue
    '                .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
    '                .Parameters.Add("@Pusat", SqlDbType.Bit).Value = Me.CEPusat.EditValue
    '                .Parameters.Add("@InsBC", SqlDbType.VarChar).Value = Me.TBInsBC.EditValue
    '                .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.CEAktif.EditValue
    '                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
    '                .Parameters.Add("@Return", SqlDbType.Int)
    '                .Parameters("@Return").Direction = ParameterDirection.Output
    '                .Connection = koneksi

    '                Try
    '                    With koneksi
    '                        .Open()
    '                        cmSP.ExecuteNonQuery()
    '                        x = cmSP.Parameters("@Return").Value
    '                        .Close()
    '                    End With
    '                    If x = 0 Then
    '                        XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    ElseIf x = 1 Then
    '                        XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                        Exit Sub
    '                    Else
    '                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                        Exit Sub
    '                    End If

    '                Catch ex As Exception
    '                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Exit Sub
    '                End Try
    '            End With
    '    End Select

    '    LockControl()
    'End Sub

    'Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
    '    LockControl()
    'End Sub
End Class