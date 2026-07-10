Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FMenu
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("MnN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("MnEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("MnDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTMenu_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.SPUrut.Properties.ReadOnly = True
        Me.CEParent.Properties.ReadOnly = True
        Me.CEMenu.Properties.ReadOnly = True

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
        Me.BVTMenu_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.SPUrut.Properties.ReadOnly = False
        Me.CEParent.Properties.ReadOnly = False
        Me.CEMenu.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTMenu_e.Selected = True
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MenuID,Nama,Ket,Parent,Menu,Urut,InsDate,InsBy,UpdDate,UpdBy From M_Menu Order By Urut asc", koneksi)

        cmsl.TableMappings.Add("Table", "M_Menu")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_Menu")
        DsMaster.Tables("M_Menu").Clear()
        cmsl.Fill(DsMaster, "M_Menu")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Menu"
    End Sub

    Private Sub FMenu_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Menu"
    End Sub

    Private Sub FMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTMenu_e.Selected = True
    End Sub

    Private Sub BVTMenu_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTMenu_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Menu"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Menu"

        DsMaster.Clear()

        OpenControl()
        Indicator = "100"

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.MKet.EditValue = ""
        Me.SPUrut.EditValue = 0
        Me.CEParent.EditValue = False
        Me.CEMenu.EditValue = False
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Menu"

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("MenuID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("MenuID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.MKet.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ket")
        Me.CEParent.EditValue = Me.GridView1.GetFocusedDataRow.Item("Parent")
        Me.CEMenu.EditValue = Me.GridView1.GetFocusedDataRow.Item("Menu")
        Me.SPUrut.EditValue = Me.GridView1.GetFocusedDataRow.Item("Urut")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Menu"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Menu : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Menu")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("MenuID")
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
                Dim cmSP As New SqlCommand("SPInsM_Menu")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SPUrut.EditValue
                    .Parameters.Add("@Parent", SqlDbType.Bit).Value = Me.CEParent.EditValue
                    .Parameters.Add("@Menu", SqlDbType.Bit).Value = Me.CEMenu.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_Menu")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SPUrut.EditValue
                    .Parameters.Add("@Parent", SqlDbType.Bit).Value = Me.CEParent.EditValue
                    .Parameters.Add("@Menu", SqlDbType.Bit).Value = Me.CEMenu.EditValue
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

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Parent") = "True" Then
                    e.Appearance.BackColor = Color.Yellow
                    e.Appearance.ForeColor = Color.Black

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class