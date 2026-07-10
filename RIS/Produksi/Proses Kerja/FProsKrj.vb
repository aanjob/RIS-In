Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FProsKrj
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim bind As New Collection

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PKPN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTProses_s.Enabled = True

        Me.SLUArtCode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

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
        Me.BVTProses_s.Enabled = False

        Me.SLUArtCode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTProses_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct MerkID+KatID+JnsID+'-'+Urut As ArtCode,(Select ArtName + ', '  From (Select Distinct ArtName From M_Brg B Where B.MerkID+B.KatID+B.JnsID+'-'+B.Urut=M_Brg.MerkID+M_Brg.KatID+M_Brg.JnsID+'-'+M_Brg.Urut) as q order By ArtName FOR XML PATH('')) As ArtName From M_Brg", koneksi)
        cmsl.TableMappings.Add("Table", "M_ArtLUE")
        cmsl.Fill(DsMaster, "M_ArtLUE")
        DsMaster.Tables("M_ArtLUE").Clear()
        cmsl.Fill(DsMaster, "M_ArtLUE")

        Me.SLUArtCode.Properties.DataSource = DsMaster.Tables("M_ArtLUE")
        Me.SLUArtCode.Properties.DisplayMember = "ArtCode"
        Me.SLUArtCode.Properties.ValueMember = "ArtCode"
    End Sub

    Public Sub FillDtl(Art As String, Tgl As Date)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ProsKIDD,Art,Tanggal,Proses,Urut From T_ProsesKrjDtl Where Art='" & Art & "' and Tanggal='" & Tgl & "' Order By Urut", koneksi)

        cmsl.TableMappings.Add("Table", "T_ProsesKrjDtl")
        Try
            DsMaster.Tables("T_ProsesKrjDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ProsesKrjDtl")

        DsMaster.Tables("T_ProsesKrjDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ProsesKrjDtl").Columns("Art"), DsMaster.Tables("T_ProsesKrjDtl").Columns("Proses")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ProsesKrjDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct Art,ArtName,Tanggal,H.Aktif,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_ProsesKrj H Inner Join M_Brg B On H.Art=B.MerkID+B.KatID+B.JnsID+'-'+B.Urut", koneksi)

        cmsl.TableMappings.Add("Table", "T_ProsesKrj")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_ProsesKrj")
        DsMaster.Tables("T_ProsesKrj").Clear()
        cmsl.Fill(DsMaster, "T_ProsesKrj")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ProsesKrj"
    End Sub

    Private Sub FProsKrj_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Proses Kerja"
    End Sub

    Private Sub FProsKrj_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FProsKrj_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FProsKrj_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTProses_e.Selected = True
    End Sub

    Private Sub BVTProses_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTProses_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Proses Kerja"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PKPEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PKPDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Proses Kerja"

        DsMaster.Clear()

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        Me.DTPTanggal.EditValue = DateTime.Now
        Me.SLUArtCode.EditValue = ""
        Me.TBArtName.EditValue = ""
        Me.CEAktif.EditValue = ""
        Me.TBInfo.EditValue = ""
        Me.CEAktif.EditValue = True

        FillDtl(Me.SLUArtCode.EditValue, Me.DTPTanggal.EditValue)
        DsMaster.Tables("T_ProsesKrjDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Proses Kerja"

        Indicator = "200"
        LUE()

        Me.SLUArtCode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Art")
        Me.TBArtName.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtName")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.CEAktif.EditValue = Me.GridView2.GetFocusedDataRow.Item("Aktif")

        FillDtl(Me.SLUArtCode.EditValue, Me.DTPTanggal.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Proses Kerja"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Proses Kerja : " & Me.GridView2.GetFocusedDataRow.Item("Art") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_ProsesKrj")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Art")
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
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
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()


        If Me.SLUArtCode.EditValue = "" Or IsDBNull(Me.SLUArtCode.EditValue) Then
            XtraMessageBox.Show("Article Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_ProsesKrj")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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

                        If x = 1 Then
                            XtraMessageBox.Show("Article Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Art")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_ProsesKrjDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Proses")
                                    .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Urut")
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

                                If x <> 0 Then
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
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
                Dim cmSP As New SqlCommand("SPUpT_ProsesKrj")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_ProsesKrjDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "ProsKIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Art")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ProsesKrjDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Proses")
                                        .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Urut")
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
                                        Me.GridView1.SetRowCellValue(i, "ProsKIDD", Me.GridView1.GetRowCellValue(i, "ProsKIDD") * -1)
                                    ElseIf x = -2 Then
                                        XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Art")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_ProsesKrjDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ProsKIDD")
                                        .Parameters.Add("@Art", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Proses")
                                        .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Urut")
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

                                    If x <> 0 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
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
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUArtCode_Leave(sender As Object, e As EventArgs) Handles SLUArtCode.Leave
        Me.TBArtName.EditValue = DsMaster.Tables("M_ArtLUE").Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("ArtName")
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ProsKIDD")
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FProsKrj_d(Me.GridView2.GetFocusedDataRow.Item("Art"), Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try

            Me.GridView1.SetRowCellValue(e.RowHandle, "ProsKIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Proses", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Art", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Urut", Me.GridView1.RowCount)
        Catch ex As Exception

        End Try

    End Sub


    Private Sub BEdProses_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdProses.ButtonClick
        Try
            Dim frm As New FSearch("Proses Produksi", "", "", "", DateTime.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdProses_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdProses.KeyPress
        e.Handled = True
    End Sub
End Class