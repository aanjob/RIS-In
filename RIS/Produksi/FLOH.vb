Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FLOH
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("LOHN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("LOHEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("LOHDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTLOH_s.Enabled = True

        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.SLUMerk.Properties.ReadOnly = True
        Me.SLUJns.Properties.ReadOnly = True
        Me.TBCutt.Properties.ReadOnly = True
        Me.TBSeri.Properties.ReadOnly = True
        Me.TBJahit.Properties.ReadOnly = True
        Me.TBPack.Properties.ReadOnly = True
        Me.TBFinishUpp.Properties.ReadOnly = True
        Me.TBFinish.Properties.ReadOnly = True

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
        Me.BVTLOH_s.Enabled = False

        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False
        Me.SLUMerk.Properties.ReadOnly = False
        Me.SLUJns.Properties.ReadOnly = False
        Me.TBCutt.Properties.ReadOnly = False
        Me.TBSeri.Properties.ReadOnly = False
        Me.TBJahit.Properties.ReadOnly = False
        Me.TBPack.Properties.ReadOnly = False
        Me.TBFinishUpp.Properties.ReadOnly = False
        Me.TBFinish.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTLOH_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        Try
            DsMaster.Tables("M_BrgMerkLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")

        Me.SLUMerk.Properties.DataSource = DsMaster.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "Nama"
        Me.SLUMerk.Properties.ValueMember = "MerkID"

        cmsl = New SqlDataAdapter("Select JnsID,Nama From M_BrgJns Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgJnsLUE")
        Try
            DsMaster.Tables("M_BrgJnsLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgJnsLUE")

        Me.SLUJns.Properties.DataSource = DsMaster.Tables("M_BrgJnsLUE")
        Me.SLUJns.Properties.DisplayMember = "Nama"
        Me.SLUJns.Properties.ValueMember = "JnsID"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Awal,Akhir,L.MerkID,M.Nama As Merk,L.JnsID,J.Nama as Jenis,LOHCutt,LOHSeri,LOHJht,LOHPack,LOHFinishUpp, LOHFinish,L.InsDate,L.InsBy,L.UpdDate,L.UpdBy From M_LOH L Inner Join M_BrgMerk M On L.MerkID=M.MerkID Inner Join M_BrgJns J On L.JnsID=J.JnsID", koneksi)

        cmsl.TableMappings.Add("Table", "M_LOH")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_LOH")
        DsMaster.Tables("M_LOH").Clear()
        cmsl.Fill(DsMaster, "M_LOH")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_LOH"
    End Sub

    Private Sub FLOH_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master LOH"
    End Sub

    Private Sub FLOH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTLOH_e.Selected = True
    End Sub

    Private Sub BVTLOH_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTLOH_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master LOH"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master LOH"

        OpenControl()
        LUE()
        Indicator = "100"

        Me.DTPAwal.EditValue = Date.Now
        Me.DTPAkhir.EditValue = Date.Now
        Me.SLUMerk.EditValue = ""
        Me.SLUJns.EditValue = ""
        Me.TBCutt.EditValue = 0.0
        Me.TBSeri.EditValue = 0.0
        Me.TBJahit.EditValue = 0.0
        Me.TBPack.EditValue = 0.0
        Me.TBFinishUpp.EditValue = 0.0
        Me.TBFinish.EditValue = 0.0

        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master LOH"

        LUE()

        Indicator = "200"
        Me.DTPAwal.EditValue = Me.GridView1.GetFocusedDataRow.Item("Awal")
        Me.DTPAkhir.EditValue = Me.GridView1.GetFocusedDataRow.Item("Akhir")
        Me.SLUMerk.EditValue = Me.GridView1.GetFocusedDataRow.Item("MerkID")
        Me.SLUJns.EditValue = Me.GridView1.GetFocusedDataRow.Item("JnsID")
        Me.TBCutt.EditValue = Me.GridView1.GetFocusedDataRow.Item("LOHCutt")
        Me.TBSeri.EditValue = Me.GridView1.GetFocusedDataRow.Item("LOHSeri")
        Me.TBJahit.EditValue = Me.GridView1.GetFocusedDataRow.Item("LOHJht")
        Me.TBPack.EditValue = Me.GridView1.GetFocusedDataRow.Item("LOHPack")
        Me.TBFinishUpp.EditValue = Me.GridView1.GetFocusedDataRow.Item("LOHFinishUpp")
        Me.TBFinish.EditValue = Me.GridView1.GetFocusedDataRow.Item("LOHFinish")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.SLUMerk.Properties.ReadOnly = True
        Me.SLUJns.Properties.ReadOnly = True
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master LOH"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus LOH Merk : " & Me.GridView1.GetFocusedDataRow.Item("Merk") & " Dan Jenis : " & Me.GridView1.GetFocusedDataRow.Item("Jenis") & "  Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_LOH")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.GridView1.GetFocusedDataRow.Item("Awal")
                .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.GridView1.GetFocusedDataRow.Item("Akhir")
                .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("MerkID")
                .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("JnsID")
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
                Dim cmSP As New SqlCommand("SPInsM_LOH")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                    .Parameters.Add("@LOHCutt", SqlDbType.Decimal).Value = Me.TBCutt.EditValue
                    .Parameters.Add("@LOHSeri", SqlDbType.Decimal).Value = Me.TBSeri.EditValue
                    .Parameters.Add("@LOHJht", SqlDbType.Decimal).Value = Me.TBJahit.EditValue
                    .Parameters.Add("@LOHPack", SqlDbType.Decimal).Value = Me.TBPack.EditValue
                    .Parameters.Add("@LOHFinishUpp", SqlDbType.Decimal).Value = Me.TBFinishUpp.EditValue
                    .Parameters.Add("@LOHFinish", SqlDbType.Decimal).Value = Me.TBFinish.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_LOH")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                    .Parameters.Add("@LOHCutt", SqlDbType.Decimal).Value = Me.TBCutt.EditValue
                    .Parameters.Add("@LOHSeri", SqlDbType.Decimal).Value = Me.TBSeri.EditValue
                    .Parameters.Add("@LOHJht", SqlDbType.Decimal).Value = Me.TBJahit.EditValue
                    .Parameters.Add("@LOHPack", SqlDbType.Decimal).Value = Me.TBPack.EditValue
                    .Parameters.Add("@LOHFinishUpp", SqlDbType.Decimal).Value = Me.TBFinishUpp.EditValue
                    .Parameters.Add("@LOHFinish", SqlDbType.Decimal).Value = Me.TBFinish.EditValue
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

    Private Sub TBCutt_KeyDown(sender As Object, e As KeyEventArgs) Handles TBCutt.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBCutt_KeyUp(sender As Object, e As KeyEventArgs) Handles TBCutt.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBSeri_KeyDown(sender As Object, e As KeyEventArgs) Handles TBSeri.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBSeri_KeyUp(sender As Object, e As KeyEventArgs) Handles TBSeri.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBJahit_KeyDown(sender As Object, e As KeyEventArgs) Handles TBJahit.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBJahit_KeyUp(sender As Object, e As KeyEventArgs) Handles TBJahit.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBPack_KeyDown(sender As Object, e As KeyEventArgs) Handles TBPack.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBPack_KeyUp(sender As Object, e As KeyEventArgs) Handles TBPack.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBFinishUpp_KeyDown(sender As Object, e As KeyEventArgs) Handles TBFinishUpp.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBFinishUpp_KeyUp(sender As Object, e As KeyEventArgs) Handles TBFinishUpp.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBFinish_KeyDown(sender As Object, e As KeyEventArgs) Handles TBFinish.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBFinish_KeyUp(sender As Object, e As KeyEventArgs) Handles TBFinish.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub
End Class