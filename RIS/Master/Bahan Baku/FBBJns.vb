Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBBJns
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("JnsBN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("JnsBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("JnsBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTJns_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.CBOJnsPers.Properties.ReadOnly = True
        Me.TBCOA.Properties.ReadOnly = True
        Me.CBOGol.Properties.ReadOnly = True
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
        Me.BVTJns_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.CBOJnsPers.Properties.ReadOnly = False
        Me.TBCOA.Properties.ReadOnly = False
        Me.CBOGol.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTJns_e.Selected = True
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JnsID,Nama,JnsPers,COA,Gol,Aktif,InsDate,InsBy,UpdDate,UpdBy From M_BBJns", koneksi)

        cmsl.TableMappings.Add("Table", "M_BBJns")
        Try
            DsMaster.Tables("M_BBJns").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BBJns")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BBJns"
    End Sub

    Private Sub FBBJns_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Jenis Bahan Baku"
    End Sub

    Private Sub FBBJns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTJns_e.Selected = True
    End Sub

    Private Sub BVTJns_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTJns_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Jenis Bahan Baku"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Jenis Bahan Baku"

        DsMaster.Clear()

        OpenControl()
        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.TBCOA.EditValue = ""
        Me.CBOGol.EditValue = ""
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Jenis Bahan Baku"

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("JnsID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("JnsID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.CBOJnsPers.EditValue = Me.GridView1.GetFocusedDataRow.Item("JnsPers")
        Me.TBCOA.EditValue = Me.GridView1.GetFocusedDataRow.Item("COA")
        Me.CBOGol.EditValue = Me.GridView1.GetFocusedDataRow.Item("Gol")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Jenis Bahan Baku"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Jenis : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_BBJns")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("JnsID")
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
                Dim cmSP As New SqlCommand("SPInsM_BBJns")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Pers", SqlDbType.VarChar).Value = Me.CBOJnsPers.EditValue
                    .Parameters.Add("@COA", SqlDbType.VarChar).Value = Me.TBCOA.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_BBJns")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Pers", SqlDbType.VarChar).Value = Me.CBOJnsPers.EditValue
                    .Parameters.Add("@COA", SqlDbType.VarChar).Value = Me.TBCOA.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress, TBCOA.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class