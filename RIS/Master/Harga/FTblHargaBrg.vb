Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FTblHargaBrg
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String
    Dim Id As Integer
    Dim LF, Pr, MC, CBP, Ag, Gr, Rt, Ks As Decimal
    Dim BulatNetto, BulatCBP As Integer

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TblHN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TblHEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TblHDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTblHarga_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CEManual.Properties.ReadOnly = True
        Me.CBOInput.Properties.ReadOnly = True
        Me.SLUVar.Properties.ReadOnly = True
        Me.TBFP.Properties.ReadOnly = True
        Me.TBLF.Properties.ReadOnly = True
        Me.TBPr.Properties.ReadOnly = True
        Me.TBMC.Properties.ReadOnly = True
        Me.TBMP.Properties.ReadOnly = True
        Me.TBCBP.Properties.ReadOnly = True
        Me.TBAg.Properties.ReadOnly = True
        Me.TBGr.Properties.ReadOnly = True
        Me.TBRt.Properties.ReadOnly = True
        Me.TBKs.Properties.ReadOnly = True
        Me.TBCBPBlt.Properties.ReadOnly = True
        Me.TBAgBlt.Properties.ReadOnly = True
        Me.TBGrBlt.Properties.ReadOnly = True
        Me.TBRtBlt.Properties.ReadOnly = True
        Me.TBKsBlt.Properties.ReadOnly = True

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
        Me.BVTblHarga_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CEManual.Properties.ReadOnly = False
        Me.CBOInput.Properties.ReadOnly = False
        Me.SLUVar.Properties.ReadOnly = False
        Me.TBFP.Properties.ReadOnly = False
        Me.TBLF.Properties.ReadOnly = False
        Me.TBPr.Properties.ReadOnly = False
        Me.TBMC.Properties.ReadOnly = False
        Me.TBMP.Properties.ReadOnly = False
        Me.TBCBP.Properties.ReadOnly = False
        Me.TBAg.Properties.ReadOnly = False
        Me.TBGr.Properties.ReadOnly = False
        Me.TBRt.Properties.ReadOnly = False
        Me.TBKs.Properties.ReadOnly = False
        Me.TBCBPBlt.Properties.ReadOnly = False
        Me.TBAgBlt.Properties.ReadOnly = False
        Me.TBGrBlt.Properties.ReadOnly = False
        Me.TBRtBlt.Properties.ReadOnly = False
        Me.TBKsBlt.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTblHarga_e.Selected = True
    End Sub

    Public Sub CekEnableManual()
        If CEManual.EditValue = True Then
            Me.SLUVar.EditValue = ""
            Me.SLUKat.EditValue = ""
            Me.CBOInput.EditValue = ""

            Me.SLUKat.Properties.ReadOnly = False
            Me.CBOInput.Properties.ReadOnly = True
            Me.SLUVar.Properties.ReadOnly = True
            Me.TBFP.Properties.ReadOnly = False
            Me.TBLF.Properties.ReadOnly = False
            Me.TBPr.Properties.ReadOnly = False
            Me.TBMC.Properties.ReadOnly = False
            Me.TBMP.Properties.ReadOnly = False
            Me.TBCBP.Properties.ReadOnly = False
            Me.TBAg.Properties.ReadOnly = False
            Me.TBGr.Properties.ReadOnly = False
            Me.TBRt.Properties.ReadOnly = False
            Me.TBKs.Properties.ReadOnly = False
            Me.TBCBPBlt.Properties.ReadOnly = False
            Me.TBAgBlt.Properties.ReadOnly = False
            Me.TBGrBlt.Properties.ReadOnly = False
            Me.TBRtBlt.Properties.ReadOnly = False
            Me.TBKsBlt.Properties.ReadOnly = False

        Else
            Me.SLUKat.Properties.ReadOnly = True
            Me.CBOInput.Properties.ReadOnly = False
            Me.SLUVar.Properties.ReadOnly = False

            If Me.CBOInput.EditValue = "Factory Price" Then
                Me.TBFP.Properties.ReadOnly = False
                Me.TBCBP.Properties.ReadOnly = True
            Else
                Me.TBCBP.Properties.ReadOnly = False
                Me.TBFP.Properties.ReadOnly = True
            End If

            Me.TBLF.Properties.ReadOnly = True
            Me.TBPr.Properties.ReadOnly = True
            Me.TBMC.Properties.ReadOnly = True
            Me.TBMP.Properties.ReadOnly = True
            Me.TBAg.Properties.ReadOnly = True
            Me.TBGr.Properties.ReadOnly = True
            Me.TBRt.Properties.ReadOnly = True
            Me.TBKs.Properties.ReadOnly = True
            Me.TBKsBlt.Properties.ReadOnly = False
            Me.TBCBPBlt.Properties.ReadOnly = True
            Me.TBAgBlt.Properties.ReadOnly = True
            Me.TBGrBlt.Properties.ReadOnly = True
            Me.TBRtBlt.Properties.ReadOnly = True
            Me.TBKsBlt.Properties.ReadOnly = True
        End If
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select VarID,Nama,KatID,LF,Pr,MC,Ag,Gr,Rt,Ks,CBP,BulatNetto,BulatCBP From M_VarHarga Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_VarHargaLUE")
        Try
            DsMaster.Tables("M_VarHargaLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_VarHargaLUE")

        Me.SLUVar.Properties.DataSource = DsMaster.Tables("M_VarHargaLUE")
        Me.SLUVar.Properties.DisplayMember = "Nama"
        Me.SLUVar.Properties.ValueMember = "VarID"

        cmsl = New SqlDataAdapter("Select KatID,Nama From M_BrgKat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgKatLUE")
        Try
            DsMaster.Tables("M_BrgKatLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgKatLUE")

        Me.SLUKat.Properties.DataSource = DsMaster.Tables("M_BrgKatLUE")
        Me.SLUKat.Properties.DisplayMember = "Nama"
        Me.SLUKat.Properties.ValueMember = "KatID"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TblIDD,Manuall,TH.Tanggal,TH.VarID,VH.Nama,Inputan,TH.KatID,K.Nama As Kat,TH.FP,TH.LF,TH.Pr, TH.MC, TH.MP,TH.CBP,TH.Ag,TH.Gr,TH.Rt,TH.Ks,TH.CBPBlt,TH.AgBlt,TH.GrBlt,TH.RtBlt,TH.KsBlt,TH.Aktif,TH.InsDate,TH.InsBy,TH.UpdDate,TH.UpdBy From M_TblHarga TH Left Outer Join M_BrgKat K On TH.KatID=K.KatID Left Outer Join M_VarHarga VH On TH.VarID=VH.VarID", koneksi)

        cmsl.TableMappings.Add("Table", "M_TblHarga")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_TblHarga")
        DsMaster.Tables("M_TblHarga").Clear()
        cmsl.Fill(DsMaster, "M_TblHarga")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_TblHarga"
    End Sub

    Private Sub FTblHargaBrg_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Tabel Harga"
    End Sub

    Private Sub FTblHargaBrg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTblHarga_e.Selected = True
    End Sub

    Private Sub BVTblHarga_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTblHarga_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Tabel Harga"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Tabel Harga"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"

        Me.CEManual.EditValue = False
        Me.DTPTanggal.EditValue = System.DateTime.Now
        Me.SLUVar.EditValue = ""
        Me.SLUKat.EditValue = ""
        Me.CBOInput.EditValue = "Factory Price"
        Me.TBFP.EditValue = 0
        Me.TBLF.EditValue = 0
        Me.TBPr.EditValue = 0
        Me.TBMC.EditValue = 0
        Me.TBMP.EditValue = 0
        Me.TBCBP.EditValue = 0
        Me.TBAg.EditValue = 0
        Me.TBGr.EditValue = 0
        Me.TBRt.EditValue = 0
        Me.TBKs.EditValue = 0
        Me.TBCBPBlt.EditValue = 0
        Me.TBAgBlt.EditValue = 0
        Me.TBGrBlt.EditValue = 0
        Me.TBRtBlt.EditValue = 0
        Me.TBKsBlt.EditValue = 0
        Me.TBInfo.EditValue = ""

        CekEnableManual()

        If Me.CBOInput.EditValue = "Factory Price" Then
            Me.TBCBP.Properties.ReadOnly = True
            Me.TBFP.Properties.ReadOnly = False
        Else
            Me.TBCBP.Properties.ReadOnly = False
            Me.TBFP.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Tabel Harga"

        Indicator = "200"
        LUE()

        Id = Me.BandedGridView1.GetFocusedDataRow.Item("TblIDD")
        Me.CEManual.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Manuall")
        Me.DTPTanggal.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")
        Me.SLUVar.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("VarID")
        If Me.BandedGridView1.GetFocusedDataRow.Item("Manuall") = False Then
            LF = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("LF")
            Pr = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Pr")
            MC = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("MC")
            CBP = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("CBP")
            Ag = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Ag")
            Gr = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Gr")
            Rt = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Rt")
            Ks = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Ks")
            BulatNetto = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("BulatNetto")
            BulatCBP = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("BulatCBP")
        Else
            LF = 0
            Pr = 0
            MC = 0
            CBP = 0
            Ag = 0
            Gr = 0
            Rt = 0
            Ks = 0
            BulatNetto = 0
            BulatCBP = 0
        End If

        Me.SLUKat.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("KatID")
        Me.CBOInput.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Inputan")

        If Me.CBOInput.EditValue = "Factory Price" Then
            Me.TBCBP.Properties.ReadOnly = True
            Me.TBFP.Properties.ReadOnly = False
        Else
            Me.TBCBP.Properties.ReadOnly = False
            Me.TBFP.Properties.ReadOnly = True
        End If

        Me.TBFP.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("FP")
        Me.TBLF.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("LF")
        Me.TBPr.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Pr")
        Me.TBMC.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("MC")
        Me.TBMP.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("MP")
        Me.TBCBP.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CBP")
        Me.TBAg.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Ag")
        Me.TBGr.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Gr")
        Me.TBRt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Rt")
        Me.TBKs.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Ks")
        Me.TBCBPBlt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CBPBlt")
        Me.TBAgBlt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("AgBlt")
        Me.TBGrBlt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("GrBlt")
        Me.TBRtBlt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("RtBlt")
        Me.TBKsBlt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("KsBlt")

        If IsDBNull(Me.BandedGridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekEnableManual()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Tabel Harga"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Jenis : " & Me.BandedGridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_TblHarga")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TblIDD")
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

        If Me.SLUKat.EditValue = "" Or IsDBNull(Me.SLUKat.EditValue) Then
            XtraMessageBox.Show("Kategori Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_TblHarga")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Me.CEManual.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@VarID", SqlDbType.VarChar).Value = Me.SLUVar.EditValue
                    .Parameters.Add("@Inputan", SqlDbType.VarChar).Value = Me.CBOInput.EditValue
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                    .Parameters.Add("@FP", SqlDbType.Decimal).Value = Me.TBFP.EditValue
                    .Parameters.Add("@LF", SqlDbType.Decimal).Value = Me.TBLF.EditValue
                    .Parameters.Add("@Pr", SqlDbType.Decimal).Value = Me.TBPr.EditValue
                    .Parameters.Add("@MC", SqlDbType.Decimal).Value = Me.TBMC.EditValue
                    .Parameters.Add("@MP", SqlDbType.Decimal).Value = Me.TBMP.EditValue
                    .Parameters.Add("@CBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                    .Parameters.Add("@Ag", SqlDbType.Decimal).Value = Me.TBAg.EditValue
                    .Parameters.Add("@Gr", SqlDbType.Decimal).Value = Me.TBGr.EditValue
                    .Parameters.Add("@Rt", SqlDbType.Decimal).Value = Me.TBRt.EditValue
                    .Parameters.Add("@Ks", SqlDbType.Decimal).Value = Me.TBKs.EditValue
                    .Parameters.Add("@CBPBlt", SqlDbType.Decimal).Value = Me.TBCBPBlt.EditValue
                    .Parameters.Add("@AgBlt", SqlDbType.Decimal).Value = Me.TBAgBlt.EditValue
                    .Parameters.Add("@GrBlt", SqlDbType.Decimal).Value = Me.TBGrBlt.EditValue
                    .Parameters.Add("@RtBlt", SqlDbType.Decimal).Value = Me.TBRtBlt.EditValue
                    .Parameters.Add("@KsBlt", SqlDbType.Decimal).Value = Me.TBKsBlt.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_TblHarga")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Id", SqlDbType.Int).Value = Id
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Me.CEManual.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@VarID", SqlDbType.VarChar).Value = Me.SLUVar.EditValue
                    .Parameters.Add("@Inputan", SqlDbType.VarChar).Value = Me.CBOInput.EditValue
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                    .Parameters.Add("@FP", SqlDbType.Decimal).Value = Me.TBFP.EditValue
                    .Parameters.Add("@LF", SqlDbType.Decimal).Value = Me.TBLF.EditValue
                    .Parameters.Add("@Pr", SqlDbType.Decimal).Value = Me.TBPr.EditValue
                    .Parameters.Add("@MC", SqlDbType.Decimal).Value = Me.TBMC.EditValue
                    .Parameters.Add("@MP", SqlDbType.Decimal).Value = Me.TBMP.EditValue
                    .Parameters.Add("@CBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                    .Parameters.Add("@Ag", SqlDbType.Decimal).Value = Me.TBAg.EditValue
                    .Parameters.Add("@Gr", SqlDbType.Decimal).Value = Me.TBGr.EditValue
                    .Parameters.Add("@Rt", SqlDbType.Decimal).Value = Me.TBRt.EditValue
                    .Parameters.Add("@Ks", SqlDbType.Decimal).Value = Me.TBKs.EditValue
                    .Parameters.Add("@CBPBlt", SqlDbType.Decimal).Value = Me.TBCBPBlt.EditValue
                    .Parameters.Add("@AgBlt", SqlDbType.Decimal).Value = Me.TBAgBlt.EditValue
                    .Parameters.Add("@GrBlt", SqlDbType.Decimal).Value = Me.TBGrBlt.EditValue
                    .Parameters.Add("@RtBlt", SqlDbType.Decimal).Value = Me.TBRtBlt.EditValue
                    .Parameters.Add("@KsBlt", SqlDbType.Decimal).Value = Me.TBKsBlt.EditValue
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

    Private Sub CEManual_EditValueChanged(sender As Object, e As EventArgs) Handles CEManual.EditValueChanged
        CekEnableManual()
    End Sub

    Private Sub CBOInput_Leave(sender As Object, e As EventArgs) Handles CBOInput.Leave
        If Me.CBOInput.EditValue = "Factory Price" Then
            Me.TBCBP.Properties.ReadOnly = True
            Me.TBFP.Properties.ReadOnly = False
        Else
            Me.TBCBP.Properties.ReadOnly = False
            Me.TBFP.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub SLUVar_Leave(sender As Object, e As EventArgs) Handles SLUVar.Leave
        If Not IsDBNull(Me.SLUVar.EditValue) And Me.SLUVar.EditValue <> "" Then
            Me.SLUKat.EditValue = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("KatID")
            LF = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("LF")
            Pr = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Pr")
            MC = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("MC")
            CBP = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("CBP")
            Ag = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Ag")
            Gr = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Gr")
            Rt = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Rt")
            Ks = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("Ks")
            BulatNetto = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("BulatNetto")
            BulatCBP = DsMaster.Tables("M_VarHargaLUE").Select("VarID = '" & Me.SLUVar.EditValue & "'")(0).Item("BulatCBP")
        End If
    End Sub

    Private Sub TBFP_Leave(sender As Object, e As EventArgs) Handles TBFP.Leave
        If Me.CBOInput.EditValue = "Factory Price" And Me.TBFP.EditValue > 0 Then
            Me.TBLF.EditValue = (LF * Me.TBFP.EditValue) / 100
            Me.TBPr.EditValue = (Pr * Me.TBFP.EditValue) / 100
            Me.TBMC.EditValue = (MC * Me.TBFP.EditValue) / 100
            Me.TBMP.EditValue = Me.TBFP.EditValue + Me.TBLF.EditValue + Me.TBPr.EditValue + Me.TBMC.EditValue
            Me.TBCBP.EditValue = CBP * Me.TBMP.EditValue
            Me.TBAg.EditValue = (Me.TBCBP.EditValue * (100 - Ag)) / 100
            Me.TBGr.EditValue = (Me.TBCBP.EditValue * (100 - Gr)) / 100
            Me.TBRt.EditValue = (Me.TBCBP.EditValue * (100 - Rt)) / 100
            Me.TBKs.EditValue = Math.Round(((Me.TBCBP.EditValue * (100 - Ks)) / 100), 2, MidpointRounding.AwayFromZero)

            If Me.TBAg.EditValue Mod BulatNetto > BulatNetto / 2 Then
                Me.TBAgBlt.EditValue = Me.TBAg.EditValue - (Me.TBAg.EditValue Mod BulatNetto) + BulatNetto
            Else
                Me.TBAgBlt.EditValue = Me.TBAg.EditValue - (Me.TBAg.EditValue Mod BulatNetto)
            End If

            If Me.TBGr.EditValue Mod BulatNetto > BulatNetto / 2 Then
                Me.TBGrBlt.EditValue = Me.TBGr.EditValue - (Me.TBGr.EditValue Mod BulatNetto) + BulatNetto
            Else
                Me.TBGrBlt.EditValue = Me.TBGr.EditValue - (Me.TBGr.EditValue Mod BulatNetto)
            End If

            If Me.TBRt.EditValue Mod BulatNetto > BulatNetto / 2 Then
                Me.TBRtBlt.EditValue = Me.TBRt.EditValue - (Me.TBRt.EditValue Mod BulatNetto) + BulatNetto
            Else
                Me.TBRtBlt.EditValue = Me.TBRt.EditValue - (Me.TBRt.EditValue Mod BulatNetto)
            End If

            Dim CBPInt As Integer
            CBPInt = Me.TBCBP.EditValue - (Me.TBCBP.EditValue Mod 1)

            Me.TBCBPBlt.EditValue = CInt(CBPInt.ToString.Substring(0, CBPInt.ToString.Length - 3) & BulatCBP)

            Me.TBKsBlt.EditValue = Math.Round(((Me.TBCBPBlt.EditValue * (100 - Ks)) / 100), 2, MidpointRounding.AwayFromZero)

        End If
    End Sub

    Private Sub TBCBP_Leave(sender As Object, e As EventArgs) Handles TBCBP.Leave
        If Me.CBOInput.EditValue = "CBP" And Me.TBCBP.EditValue > 0 Then

            Me.TBMP.EditValue = Me.TBCBP.EditValue / CBP
            Me.TBFP.EditValue = ((Me.TBMP.EditValue / (100 + LF + Pr + MC)) * 100)
            Me.TBLF.EditValue = (LF * Me.TBFP.EditValue) / 100
            Me.TBPr.EditValue = (Pr * Me.TBFP.EditValue) / 100
            Me.TBMC.EditValue = (MC * Me.TBFP.EditValue) / 100
            Me.TBAg.EditValue = (Me.TBCBP.EditValue * (100 - Ag)) / 100
            Me.TBGr.EditValue = (Me.TBCBP.EditValue * (100 - Gr)) / 100
            Me.TBRt.EditValue = (Me.TBCBP.EditValue * (100 - Rt)) / 100
            Me.TBKs.EditValue = Math.Round(((Me.TBCBP.EditValue * (100 - Ks)) / 100), 2, MidpointRounding.AwayFromZero)

            If Me.TBAg.EditValue Mod BulatNetto > BulatNetto / 2 Then
                Me.TBAgBlt.EditValue = Me.TBAg.EditValue - (Me.TBAg.EditValue Mod BulatNetto) + BulatNetto
            Else
                Me.TBAgBlt.EditValue = Me.TBAg.EditValue - (Me.TBAg.EditValue Mod BulatNetto)
            End If

            If Me.TBGr.EditValue Mod BulatNetto > BulatNetto / 2 Then
                Me.TBGrBlt.EditValue = Me.TBGr.EditValue - (Me.TBGr.EditValue Mod BulatNetto) + BulatNetto
            Else
                Me.TBGrBlt.EditValue = Me.TBGr.EditValue - (Me.TBGr.EditValue Mod BulatNetto)
            End If

            If Me.TBRt.EditValue Mod BulatNetto > BulatNetto / 2 Then
                Me.TBRtBlt.EditValue = Me.TBRt.EditValue - (Me.TBRt.EditValue Mod BulatNetto) + BulatNetto
            Else
                Me.TBRtBlt.EditValue = Me.TBRt.EditValue - (Me.TBRt.EditValue Mod BulatNetto)
            End If

            Dim CBPInt As Integer
            CBPInt = Me.TBCBP.EditValue - (Me.TBCBP.EditValue Mod 1)

            Me.TBCBPBlt.EditValue = CInt(CBPInt.ToString.Substring(0, CBPInt.ToString.Length - 3) & BulatCBP)

            Me.TBKsBlt.EditValue = Math.Round(((Me.TBCBPBlt.EditValue * (100 - Ks)) / 100), 2, MidpointRounding.AwayFromZero)

        End If
    End Sub

    Private Sub TBFP_KeyDown(sender As Object, e As KeyEventArgs) Handles TBFP.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBFP_KeyUp(sender As Object, e As KeyEventArgs) Handles TBFP.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBLF_KeyDown(sender As Object, e As KeyEventArgs) Handles TBLF.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBLF_KeyUp(sender As Object, e As KeyEventArgs) Handles TBLF.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub


    Private Sub TBPr_KeyDown(sender As Object, e As KeyEventArgs) Handles TBPr.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBPr_KeyUp(sender As Object, e As KeyEventArgs) Handles TBPr.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBMC_KeyDown(sender As Object, e As KeyEventArgs) Handles TBMC.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBMC_KeyUp(sender As Object, e As KeyEventArgs) Handles TBMC.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBMP_KeyDown(sender As Object, e As KeyEventArgs) Handles TBMP.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBMP_KeyUp(sender As Object, e As KeyEventArgs) Handles TBMP.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub


    Private Sub TBCBP_KeyDown(sender As Object, e As KeyEventArgs) Handles TBCBP.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBCBP_KeyUp(sender As Object, e As KeyEventArgs) Handles TBCBP.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBAg_KeyDown(sender As Object, e As KeyEventArgs) Handles TBAg.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBAg_KeyUp(sender As Object, e As KeyEventArgs) Handles TBAg.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGr_KeyDown(sender As Object, e As KeyEventArgs) Handles TBGr.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGr_KeyUp(sender As Object, e As KeyEventArgs) Handles TBGr.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBRt_KeyDown(sender As Object, e As KeyEventArgs) Handles TBRt.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBRt_KeyUp(sender As Object, e As KeyEventArgs) Handles TBRt.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBAgBlt_KeyDown(sender As Object, e As KeyEventArgs) Handles TBAgBlt.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBAgBlt_KeyUp(sender As Object, e As KeyEventArgs) Handles TBAgBlt.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGrBlt_KeyDown(sender As Object, e As KeyEventArgs) Handles TBGrBlt.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGrBlt_KeyUp(sender As Object, e As KeyEventArgs) Handles TBGrBlt.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBRtBlt_KeyDown(sender As Object, e As KeyEventArgs) Handles TBRtBlt.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBRtBlt_KeyUp(sender As Object, e As KeyEventArgs) Handles TBRtBlt.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

End Class

