Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FCurr
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("CurrN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("CurrEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("CurrDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTCurr_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.TBMtUang.Properties.ReadOnly = True
        Me.TBNilTukarRp.Properties.ReadOnly = True

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
        Me.BVTCurr_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False
        Me.TBMtUang.Properties.ReadOnly = False
        Me.TBNilTukarRp.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTCurr_e.Selected = True
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CurrID,Tanggal,Awal,Akhir,MtUang,NilTukarRp,InsDate,InsBy,UpdDate,UpdBy From M_Curr Where Year(Tanggal)=" & MainModule.periodeTahun & " and Month(Tanggal)=" & MainModule.periodeBulan & "", koneksi)

        cmsl.TableMappings.Add("Table", "M_Curr")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_Curr")
        DsMaster.Tables("M_Curr").Clear()
        cmsl.Fill(DsMaster, "M_Curr")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Curr"
    End Sub

    Private Sub FCurr_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Currency"
    End Sub

    Private Sub FCurr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTCurr_e.Selected = True
    End Sub

    Private Sub BVTCurr_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTCurr_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Currency"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Currency"

        DsMaster.Clear()

        OpenControl()
        Indicator = "100"

        Me.TBKode.EditValue = ""
        Me.DTPTanggal.EditValue = Date.Now
        Me.DTPAwal.EditValue = Date.Now
        Me.DTPAkhir.EditValue = Date.Now
        Me.TBMtUang.EditValue = ""
        Me.TBNilTukarRp.EditValue = 0.0

        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Currency"

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("CurrID")
        Me.DTPTanggal.EditValue = Me.GridView1.GetFocusedDataRow.Item("Tanggal")
        Me.DTPAwal.EditValue = Me.GridView1.GetFocusedDataRow.Item("Awal")
        Me.DTPAkhir.EditValue = Me.GridView1.GetFocusedDataRow.Item("Akhir")
        Me.TBMtUang.EditValue = Me.GridView1.GetFocusedDataRow.Item("MtUang")
        Me.TBNilTukarRp.EditValue = Me.GridView1.GetFocusedDataRow.Item("NilTukarRp")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Currency"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Propinsi : " & Me.GridView1.GetFocusedDataRow.Item("CurrID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Curr")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("CurrID")
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

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Curr")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.TBMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
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
                Dim cmSP As New SqlCommand("SPUpM_Curr")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.TBMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBMtUang.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
   
End Class