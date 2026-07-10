Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FJam
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Id As Integer

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("JamN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("JamEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("JamDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTJam_s.Enabled = True

        Me.SLUProses.Properties.ReadOnly = True
        Me.TBJamAw.Properties.ReadOnly = True
        Me.TBJamAkh.Properties.ReadOnly = True
        Me.TBJam.Properties.ReadOnly = True

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
        Me.BVTJam_s.Enabled = False

        Me.SLUProses.Properties.ReadOnly = False
        Me.TBJamAw.Properties.ReadOnly = False
        Me.TBJamAkh.Properties.ReadOnly = False
        Me.TBJam.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTJam_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Proses From M_Proses Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_ProsesLUE")
        cmsl.Fill(DsMaster, "M_ProsesLUE")
        DsMaster.Tables("M_ProsesLUE").Clear()
        cmsl.Fill(DsMaster, "M_ProsesLUE")

        Me.SLUProses.Properties.DataSource = DsMaster.Tables("M_ProsesLUE")
        Me.SLUProses.Properties.DisplayMember = "Proses"
        Me.SLUProses.Properties.ValueMember = "Proses"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JamIDD,Proses,JamAw,JamAkh,Jam,InsDate,InsBy,UpdDate,UpdBy From M_Jam", koneksi)

        cmsl.TableMappings.Add("Table", "M_Jam")
        cmsl.Fill(DsMaster, "M_Jam")
        DsMaster.Tables("M_Jam").Clear()
        cmsl.Fill(DsMaster, "M_Jam")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Jam"
    End Sub

    Private Sub FJam_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Jam"
    End Sub

    Private Sub FJam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTJam_e.Selected = True
    End Sub

    Private Sub BVTJam_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTJam_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Jam"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Jam"
        OpenControl()
        LUE()

        Indicator = "100"

        Me.SLUProses.EditValue = ""
        Me.TBJamAw.EditValue = ""
        Me.TBJamAkh.EditValue = ""
        Me.TBJam.EditValue = 0
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Jam"

        LUE()

        Indicator = "200"
        Id = Me.GridView1.GetFocusedDataRow.Item("JamIDD")
        Me.SLUProses.EditValue = Me.GridView1.GetFocusedDataRow.Item("Proses")
        Me.TBJamAw.EditValue = Me.GridView1.GetFocusedDataRow.Item("JamAw")
        Me.TBJamAkh.EditValue = Me.GridView1.GetFocusedDataRow.Item("JamAkh")
        Me.TBJam.EditValue = Me.GridView1.GetFocusedDataRow.Item("Jam")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Jam"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Jam : " & Me.GridView1.GetFocusedDataRow.Item("Jam") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Jam")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetFocusedDataRow.Item("JamIDD")
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

        If IsDBNull(Me.TBJamAw.EditValue) Or IsDBNull(Me.TBJamAkh.EditValue) Then
            XtraMessageBox.Show("Jam Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If IsDBNull(Me.SLUProses.EditValue) Or Me.SLUProses.EditValue = "" Then
            XtraMessageBox.Show("Proses Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Jam")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                    .Parameters.Add("@JamAw", SqlDbType.Time).Value = Me.TBJamAw.EditValue
                    .Parameters.Add("@JamAkh", SqlDbType.Time).Value = Me.TBJamAkh.EditValue
                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.TBJam.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_Jam")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Id", SqlDbType.Int).Value = Id
                    .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                    .Parameters.Add("@JamAw", SqlDbType.Time).Value = Me.TBJamAw.EditValue
                    .Parameters.Add("@JamAkh", SqlDbType.Time).Value = Me.TBJamAkh.EditValue
                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.TBJam.EditValue
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

    Private Sub TBJamAw_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBJamAw.KeyPress, TBJamAkh.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class