Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FGudang
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("GdN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("GdEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("GdDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTLok_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.MAlamat.Properties.ReadOnly = True
        Me.SLUKota.Properties.ReadOnly = True
        Me.SLUInduk.Properties.ReadOnly = True
        Me.CEPusat.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.CBOGol.Properties.ReadOnly = True
        Me.TBInsBC.Properties.ReadOnly = True

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
        Me.BVTLok_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.MAlamat.Properties.ReadOnly = False
        Me.SLUKota.Properties.ReadOnly = False
        Me.SLUInduk.Properties.ReadOnly = False
        Me.SLUGrup.Properties.ReadOnly = False
        Me.CBOGol.Properties.ReadOnly = False
        Me.CEPusat.Properties.ReadOnly = False
        Me.TBInsBC.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTLok_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        cmsl = New SqlDataAdapter("Select KotaID,Nama From M_Kota", koneksi)
        cmsl.TableMappings.Add("Table", "M_Kota")
        Try
            DsMaster.Tables("M_Kota").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Kota")

        Me.SLUKota.Properties.DataSource = DsMaster.Tables("M_Kota")
        Me.SLUKota.Properties.DisplayMember = "Nama"
        Me.SLUKota.Properties.ValueMember = "KotaID"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        Try
            DsMaster.Tables("M_GudangLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUInduk.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUInduk.Properties.DisplayMember = "Nama"
        Me.SLUInduk.Properties.ValueMember = "GdID"

        DsMaster.Tables("M_GudangLUE").Rows.Add("", "")

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Aktif,Pusat,GdID,G.Nama,Alamat,G.KotaID,K.Nama As Kota, Induk, (Select Isnull((Select Nama From M_Gudang Where GdId=G.Induk),'')) As GdInduk,Grup,Gol,InisialBC,G.InsDate,G.InsBy,G.UpdDate,G.UpdBy From M_Gudang G Inner Join M_Kota K On G.KotaID=K.KotaID", koneksi)

        cmsl.TableMappings.Add("Table", "M_Gudang")
        Try
            DsMaster.Tables("M_Gudang").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Gudang")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Gudang"
    End Sub

    Private Sub FGudang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Gudang"
    End Sub

    Private Sub FGudang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTLok_e.Selected = True
    End Sub

    Private Sub BVTLok_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTLok_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Gudang"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Gudang"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.MAlamat.EditValue = ""
        Me.SLUKota.EditValue = ""
        Me.SLUGrup.EditValue = ""
        Me.CBOGol.EditValue = ""
        Me.CEPusat.EditValue = False 'Untuk Di Rekap Transaksi
        Me.TBInsBC.EditValue = ""
        Me.TBInfo.EditValue = ""

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Gudang"

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("GdID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("GdID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.MAlamat.EditValue = Me.GridView1.GetFocusedDataRow.Item("Alamat")
        Me.SLUKota.EditValue = Me.GridView1.GetFocusedDataRow.Item("KotaID")
        Me.SLUInduk.EditValue = Me.GridView1.GetFocusedDataRow.Item("Induk")
        Me.CEPusat.EditValue = Me.GridView1.GetFocusedDataRow.Item("Pusat")
        Me.TBInsBC.EditValue = Me.GridView1.GetFocusedDataRow.Item("InisialBC")
        Me.SLUGrup.EditValue = Me.GridView1.GetFocusedDataRow.Item("Grup")
        Me.CBOGol.EditValue = Me.GridView1.GetFocusedDataRow.Item("Gol")
        Me.TBInsBC.EditValue = Me.GridView1.GetFocusedDataRow.Item("InisialBC")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Gudang"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Gudang : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Gd")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("GdID")
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

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Gd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@Induk", SqlDbType.VarChar).Value = Me.SLUInduk.EditValue
                    .Parameters.Add("@Pusat", SqlDbType.Bit).Value = Me.CEPusat.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
                    .Parameters.Add("@InsBC", SqlDbType.VarChar).Value = Me.TBInsBC.EditValue
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
                            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Gd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@Induk", SqlDbType.VarChar).Value = Me.SLUInduk.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
                    .Parameters.Add("@Pusat", SqlDbType.Bit).Value = Me.CEPusat.EditValue
                    .Parameters.Add("@InsBC", SqlDbType.VarChar).Value = Me.TBInsBC.EditValue
                    .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.CEAktif.EditValue
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
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress, MAlamat.KeyPress, TBInsBC.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class