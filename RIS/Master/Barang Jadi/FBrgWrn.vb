Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBrgWrn
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("WrnN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("WrnEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("WrnDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTWarna_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True

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
        Me.BVTWarna_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTWrn_e.Selected = True
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select WrnID,Nama,Aktif,InsDate,InsBy,UpdDate,UpdBy From M_BrgWrn", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgWrn")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgWrn")
        DsMaster.Tables("M_BrgWrn").Clear()
        cmsl.Fill(DsMaster, "M_BrgWrn")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgWrn"
    End Sub

    Private Sub FBrgWrn_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Warna Barang Jadi"
    End Sub

    Private Sub FBrgWrn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTWrn_e.Selected = True
    End Sub

    Private Sub BVTWrn_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTWarna_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Warna Barang Jadi"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Warna Barang Jadi"

        DsMaster.Clear()

        OpenControl()
        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Warna Barang Jadi"

        Me.CEAktif.Properties.ReadOnly = True

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("WrnID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("WrnID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Warna Barang Jadi"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Jenis : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_BrgWrn")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("WrnID")
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
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_BrgWrn")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_BrgWrn")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
   
End Class