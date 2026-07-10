Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FCabang
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1) As String
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("CabN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("CabEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("CabDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTCabang_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False


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
        Me.BVTCabang_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTCabang_e.Selected = True
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CabID,Cabang,HitKomisi,Aktif,InsDate,InsBy,UpdDate,UpdBy From M_Cab", koneksi)

        cmsl.TableMappings.Add("Table", "M_Cab")
        Try
            DsMaster.Tables("M_Cab").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Cab")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Cab"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID,CG.CabID,G.GdID,G.Nama,CG.Aktif From M_CabGd CG Inner Join M_Gudang G On CG.GdID=G.GdID  Where CabID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_CabGd")
        cmsl.Fill(DsMaster, "M_CabGd")
        DsMaster.Tables("M_CabGd").Clear()
        cmsl.Fill(DsMaster, "M_CabGd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_CabGd"

        cmsl = New SqlDataAdapter("Select HubID,CS.CabID,S.SalID,S.Nama,CS.Aktif From M_CabSal CS Inner Join M_Sales S On CS.SalID=S.SalID Where CabID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_CabSal")
        cmsl.Fill(DsMaster, "M_CabSal")
        DsMaster.Tables("M_CabSal").Clear()
        cmsl.Fill(DsMaster, "M_CabSal")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "M_CabSal"
    End Sub

    Private Sub FCabang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Cabang"
    End Sub

    Private Sub FCabang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTCabang_e.Selected = True
    End Sub

    Private Sub BVTCabang_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTCabang_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Cabang"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Cabang"

        DsMaster.Clear()

        OpenControl()
        Me.CEAktif.Properties.ReadOnly = True

        Indicator = "100"

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.CEAktif.EditValue = True
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Cabang"

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("CabID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("CabID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Cabang")
        Me.CEHitKomisi.EditValue = Me.GridView1.GetFocusedDataRow.Item("HitKomisi")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        FillDtl(Me.TBKode.EditValue)

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubId,CabID,CG.GdID,G.Nama,CG.Aktif From M_CabGd CG Inner Join M_Gudang G On CG.GdID=G.GdID Where CabID = '" & Me.TBKode.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CabGd")
        Try
            DsMaster.Tables("M_CabGd").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "M_CabGd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_CabGd"

        cmsl = New SqlDataAdapter("Select HubId,CabID,CS.SalID,S.Nama,CS.Aktif From M_CabSal CS Inner Join M_Sales S On CS.SalID=S.SalID Where CabID = '" & Me.TBKode.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CabSal")
        Try
            DsMaster.Tables("M_CabSal").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "M_CabSal")

        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "M_CabSal"

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Cabang"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Cabang : " & Me.GridView1.GetFocusedDataRow.Item("Cabang") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Cab")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("CabID")
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
                Dim cmSP As New SqlCommand("SPInsM_Cab")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Cabang", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@HitKms", SqlDbType.Bit).Value = Me.CEHitKomisi.EditValue
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

                        Dim i : For i = 0 To Me.GridView2.RowCount - 1
                            Dim cmSPDtl As New SqlCommand("SPInsM_CabGd")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "GdID")
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
                        Next

                        Dim z : For z = 0 To GridView3.RowCount - 1
                            Dim cmSPDtl As New SqlCommand("SPInsM_CabSal")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SalID")
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
                        Next

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
                Dim cmSP As New SqlCommand("SPUpM_Cab")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Cabang", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@HitKms", SqlDbType.Bit).Value = Me.CEHitKomisi.EditValue
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

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_CabGd")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_CabSal")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(q)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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

                        Dim i : For i = 0 To GridView2.RowCount - 1
                            If Me.GridView2.GetRowCellValue(i, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_CabGd")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "GdID")
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
                                    Me.GridView2.SetRowCellValue(i, "HubID", Me.GridView2.GetRowCellValue(i, "HubID") * -1)
                                End If

                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_CabGd")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(i, "HubID")
                                    .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "GdID")
                                    .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "Aktif")
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
                            End If
                        Next

                        Dim z : For z = 0 To GridView3.RowCount - 1
                            If Me.GridView3.GetRowCellValue(z, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_CabSal")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SalID")
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
                                    Me.GridView3.SetRowCellValue(z, "HubID", Me.GridView3.GetRowCellValue(i, "HubID") * -1)
                                End If
                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_CabSal")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "HubID")
                                    .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SalID")
                                    .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Aktif")
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
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub GridView2_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView2.InitNewRow
        Me.GridView2.SetRowCellValue(e.RowHandle, "HubID", Me.GridView2.RowCount * -1)
        Me.GridView2.SetRowCellValue(e.RowHandle, "GdID", "")
        Me.GridView2.SetRowCellValue(e.RowHandle, "Aktif", "True")
    End Sub

    Private Sub BEdGdID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdGdID.ButtonClick
        Dim frm As New FSearch("Gudang", "", "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            GridView2.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Nama", dataTrans.Item("Nama").ToString)
        End If
    End Sub

    Private Sub GridControl2_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl2.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView2.GetFocusedDataRow.Item("HubId")
        End If
    End Sub

    Private Sub BEdGdID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdGdID.KeyPress
        e.Handled = True
    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Me.GridView3.SetRowCellValue(e.RowHandle, "HubID", Me.GridView1.RowCount * -1)
        Me.GridView3.SetRowCellValue(e.RowHandle, "SalID", "")
        Me.GridView3.SetRowCellValue(e.RowHandle, "Aktif", "True")
    End Sub

    Private Sub BEdSalID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdSalID.ButtonClick
        Dim frm As New FSearch("Sales", "", "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Nama", dataTrans.Item("Nama").ToString)
        End If
    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("HubId")
        End If
    End Sub

    Private Sub BEdSalID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdSalID.KeyPress
        e.Handled = True
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
  
End Class