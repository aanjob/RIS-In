Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKatTKL
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("KatTKLN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("KatTKLEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("KatTKLDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTKatTKL_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBKat.Properties.ReadOnly = True
        Me.TBGaJam.Properties.ReadOnly = True
        Me.TBOTN.Properties.ReadOnly = True
        Me.TBOTL.Properties.ReadOnly = True
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
        Me.BVTKatTKL_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBKat.Properties.ReadOnly = False
        Me.TBGaJam.Properties.ReadOnly = False
        Me.TBOTN.Properties.ReadOnly = False
        Me.TBOTL.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTKatTKL_e.Selected = True
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select KatID,Kategori,GaJam,OTN,OTL,Aktif,InsDate,InsBy,UpdDate,UpdBy From M_KatTKL", koneksi)

        cmsl.TableMappings.Add("Table", "M_KatTKL")
        Try
            DsMaster.Tables("M_KatTKL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_KatTKL")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_KatTKL"
    End Sub

    Private Sub FKatTKL_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Kategori TKL"
    End Sub

    Private Sub FKatTKL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTKatTKL_e.Selected = True
    End Sub

    Private Sub BVTKatTKL_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTKatTKL_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Kategori TKL"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Kategori TKL"

        DsMaster.Clear()

        OpenControl()
        Me.CEAktif.Properties.ReadOnly = True

        Indicator = "100"

        Me.TBKode.EditValue = ""
        Me.TBKat.EditValue = ""
        Me.TBGaJam.EditValue = ""
        Me.TBOTN.EditValue = ""
        Me.TBOTL.EditValue = ""
        Me.CEAktif.EditValue = True
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Kategori TKL"

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("KatID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("KatID")
        Me.TBKat.EditValue = Me.GridView1.GetFocusedDataRow.Item("Kategori")
        Me.TBGaJam.EditValue = Me.GridView1.GetFocusedDataRow.Item("GaJam")
        Me.TBOTN.EditValue = Me.GridView1.GetFocusedDataRow.Item("OTN")
        Me.TBOTL.EditValue = Me.GridView1.GetFocusedDataRow.Item("OTL")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        Me.TBKode.Properties.ReadOnly = True
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Kategori TKL"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Kategori Karyawan : " & Me.GridView1.GetFocusedDataRow.Item("Kategori") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_KatTKL")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("KatID")
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
                Dim cmSP As New SqlCommand("SPInsM_KatTKL")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.TBKat.EditValue
                    .Parameters.Add("@GaJam", SqlDbType.Decimal).Value = Me.TBGaJam.EditValue
                    .Parameters.Add("@OTN", SqlDbType.Decimal).Value = Me.TBOTN.EditValue
                    .Parameters.Add("@OTL", SqlDbType.Decimal).Value = Me.TBOTL.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_KatTKL")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.TBKat.EditValue
                    .Parameters.Add("@GaJam", SqlDbType.Decimal).Value = Me.TBGaJam.EditValue
                    .Parameters.Add("@OTN", SqlDbType.Decimal).Value = Me.TBOTN.EditValue
                    .Parameters.Add("@OTL", SqlDbType.Decimal).Value = Me.TBOTL.EditValue
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

    Private Sub TBGaJam_KeyDown(sender As Object, e As KeyEventArgs) Handles TBGaJam.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGaJam_KeyUp(sender As Object, e As KeyEventArgs) Handles TBGaJam.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBOTL_KeyDown(sender As Object, e As KeyEventArgs) Handles TBOTL.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBOTL_KeyUp(sender As Object, e As KeyEventArgs) Handles TBOTL.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBOTN_KeyDown(sender As Object, e As KeyEventArgs) Handles TBOTN.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBOTN_KeyUp(sender As Object, e As KeyEventArgs) Handles TBOTN.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBKat.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class