Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FOpBJv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=27 and Gol='" & Golongan & "'", koneksi)

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        CodeID = ""
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        CodeID = Reader.Item(1)
                        MnlInsUpd = Reader.Item(2)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("OpBJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("OpBJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("OpBJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTStokOp_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.SLUPeriodID.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.SLUSat.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.BandedGridView2.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTStokOp_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUPeriodID.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.SLUSat.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BandedGridView2.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTStokOp_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        cmsl = New SqlDataAdapter("Select PeriodID,Bulan,Tahun,TglAkhir From M_Period Order By PeriodID", koneksi)
        cmsl.TableMappings.Add("Table", "M_PeriodLUE")
        Try
            DsMaster.Tables("M_PeriodLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_PeriodLUE")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("M_PeriodLUE")
        Me.SLUPeriodID.Properties.DisplayMember = "PeriodID"
        Me.SLUPeriodID.Properties.ValueMember = "PeriodID"

        cmsl = New SqlDataAdapter("Select SatID,Nama,Isi From M_BrgSat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSatLUE")
        Try
            DsMaster.Tables("M_BrgSatLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgSatLUE")

        Me.SLUSat.Properties.DataSource = DsMaster.Tables("M_BrgSatLUE")
        Me.SLUSat.Properties.DisplayMember = "Nama"
        Me.SLUSat.Properties.ValueMember = "SatID"

        DsMaster.Tables("M_BrgSatLUE").Rows.Add("%", "Semua", 0)
        DsMaster.Tables("M_BrgSatLUE").Rows.Add("D%", "Semua Dos", 0)
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select OpBJIDD,OpBJID,AddDt,D.ArtCode,ArtName,D.SatID,D.Isi,QtyD,DosD,PsgD,QtyF,DosF,PsgF,Selisih,Ket From T_OpBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where OpBJID='" & Kode & "' Order By ArtCode Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_OpBJDtl" & Gol)
        Try
            DsMaster.Tables("T_OpBJDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_OpBJDtl" & Gol)

        DsMaster.Tables("T_OpBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_OpBJDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_OpBJDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select OpBJID,PeriodID,Tanggal,H.GdID,G.Nama as Gudang, H.SatID,TotQtyD,TotDosD,TotPsgD,TotQtyF, TotDosF,TotPsgF,TotSelisih,H.Ket,H.Grup,H.Gol,H.Terproses,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_OpBJ H Inner Join M_Gudang G On H.GdID=G.GdID and PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,OpBJID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_OpBJ" & Gol)
        Try
            DsMaster.Tables("T_OpBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_OpBJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_OpBJ" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_OpBJ")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Public Sub DelXml()
        If IO.File.Exists("SrStokBJOp.xml") Then
            System.IO.File.Delete("SrStokBJOp.xml")
        End If
    End Sub

    Private Sub FOpBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Stok Opname Barang Jadi"
    End Sub

    Private Sub FOpBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FOpBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTStokOp_e.Selected = True
    End Sub

    Private Sub BVTStokOp_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTStokOp_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Stok Opname Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("OpBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Stok Opname Barang Jadi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If
        DelXml()

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUPeriodID.EditValue = MainModule.periodAktif

        Me.SLUGrup.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.SLUSat.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_OpBJDtl" & Gol).Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Stok Opname Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.BandedGridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        DelXml()

        If Me.BandedGridView2.GetFocusedDataRow.Item("Terproses") = False Then
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("OpBJID")
            Me.SLUGrup.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("Grup")

            Dim cmsl As SqlDataAdapter
            If Gol = "Promosi" Then
                cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True'", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
            End If

            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            Try
                DsMaster.Tables("M_GudangLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"
            Me.SLUPeriodID.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("PeriodID")
            Me.DTPTanggal.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUGd.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("GdID")
            Me.SLUSat.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("SatID")
            Me.MKet.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("Ket")

            FillDtl(Me.TBKode.EditValue)
            ReDim arrPar(-1)

            If IsDBNull(Me.BandedGridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.BandedGridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True
        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diadjustment", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        'End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Stok Opname Barang Jadi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.BandedGridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If MainModule.SlAdjBJ() > 0 Then
        '    If MainModule.BackDate = False Then
        '        XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Adjustment", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If
        'End If

        If Me.BandedGridView2.GetFocusedDataRow.Item("Terproses") = False Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.BandedGridView2.GetFocusedDataRow.Item("OpBJID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_OpBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.BandedGridView2.GetFocusedDataRow.Item("OpBJID")
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
        Else
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Terproses", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim bind As New Collection
        bind.Add(Me.BandedGridView2.GetFocusedDataRow.Item("OpBJID"), "Kode")
        bind.Add(Me.BandedGridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.BandedGridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")

        Dim XR As New XROpBJ
        XR.InitializeData(bind)

        Dim XR2 As New XROpBJNS
        XR2.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.BandedGridView1.ActiveFilter.Clear()

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_OpBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSat.EditValue
                    .Parameters.Add("@TotQtyD", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("QtyD").SummaryText, Integer)
                    .Parameters.Add("@TotDosD", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("DosD").SummaryText, Integer)
                    .Parameters.Add("@TotPsgD", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("PsgD").SummaryText, Integer)
                    .Parameters.Add("@TotQtyF", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("QtyF").SummaryText, Integer)
                    .Parameters.Add("@TotDosF", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("DosF").SummaryText, Integer)
                    .Parameters.Add("@TotPsgF", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("PsgF").SummaryText, Integer)
                    .Parameters.Add("@TotSelisih", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("Selisih").SummaryText, Integer)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To BandedGridView1.RowCount - 1
                            If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_OpBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.BandedGridView1.GetRowCellValue(i, "AddDt")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@QtyD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyD")
                                    .Parameters.Add("@DosD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "DosD")
                                    .Parameters.Add("@PsgD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "PsgD")
                                    .Parameters.Add("@QtyF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyF")
                                    .Parameters.Add("@DosF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "DosF")
                                    .Parameters.Add("@PsgF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "PsgF")
                                    .Parameters.Add("@Selisih", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "Selisih")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Ket")
                                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBKode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            Del()
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_OpBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSat.EditValue
                    .Parameters.Add("@TotQtyD", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("QtyD").SummaryText, Integer)
                    .Parameters.Add("@TotDosD", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("DosD").SummaryText, Integer)
                    .Parameters.Add("@TotPsgD", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("PsgD").SummaryText, Integer)
                    .Parameters.Add("@TotQtyF", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("QtyF").SummaryText, Integer)
                    .Parameters.Add("@TotDosF", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("DosF").SummaryText, Integer)
                    .Parameters.Add("@TotPsgF", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("PsgF").SummaryText, Integer)
                    .Parameters.Add("@TotSelisih", SqlDbType.Int).Value = CType(Me.BandedGridView1.Columns("Selisih").SummaryText, Integer)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            Dim cmSPDel As New SqlCommand("SPDelT_OpBJDtl")
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

                        Dim i : For i = 0 To BandedGridView1.RowCount - 1
                            If Me.BandedGridView1.GetRowCellValue(i, "OpBJIDD") < 0 Then
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_OpBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.BandedGridView1.GetRowCellValue(i, "AddDt")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@QtyD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyD")
                                        .Parameters.Add("@DosD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "DosD")
                                        .Parameters.Add("@PsgD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "PsgD")
                                        .Parameters.Add("@QtyF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyF")
                                        .Parameters.Add("@DosF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "DosF")
                                        .Parameters.Add("@PsgF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "PsgF")
                                        .Parameters.Add("@Selisih", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                                        Me.BandedGridView1.SetRowCellValue(i, "OpBJIDD", Me.BandedGridView1.GetRowCellValue(i, "OpBJIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_OpBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "OpBJIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.BandedGridView1.GetRowCellValue(i, "AddDt")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@QtyD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyD")
                                        .Parameters.Add("@DosD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "DosD")
                                        .Parameters.Add("@PsgD", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "PsgD")
                                        .Parameters.Add("@QtyF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyF")
                                        .Parameters.Add("@DosF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "DosF")
                                        .Parameters.Add("@PsgF", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "PsgF")
                                        .Parameters.Add("@Selisih", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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

    Private Sub SLUPeriodID_Leave(sender As Object, e As EventArgs) Handles SLUPeriodID.Leave
        If CStr(Me.SLUPeriodID.EditValue) <> "" Then
            Me.DTPTanggal.EditValue = DsMaster.Tables("M_PeriodLUE").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("TglAkhir")
        End If
    End Sub

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Not IsDBNull(Me.SLUGrup.EditValue) And Me.SLUGrup.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter
            If Gol = "Promosi" Then
                cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True'", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
            End If

            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            Try
                DsMaster.Tables("M_GudangLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"

            Dim i : For i = Me.BandedGridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.BandedGridView1.GetRowCellValue(i, "OpBJIDD")

                Me.BandedGridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim jml As Integer

        Dim command As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

        With koneksi
            .Open()
            command.CommandTimeout = 9000
            jml = command.ExecuteScalar()
            .Close()
        End With

        If jml > 0 Then
            XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Dim i : For i = Me.BandedGridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.BandedGridView1.GetRowCellValue(i, "OpBJIDD")

                Me.BandedGridView1.DeleteRow(i)
            Next

            If Me.SLUGd.EditValue = "" Then
                XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Me.SLUSat.EditValue = "" Then
                XtraMessageBox.Show("Satuan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            DsMaster.Tables("T_OpBJDtl" & Gol).Clear()
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '--' As OpBjID,OpBJIDD,'False' as AddDt,ArtCode,ArtName,SatID,Isi,QtyD,Case When SatID ='P' Then 0 Else QtyD End As DosD,QtyD*Isi As PsgD,QtyD as QtyF, Case When SatID ='P' Then 0 Else QtyD End As DosF,QtyD*Isi As PsgF,Selisih,Ket From ( Select '--' As OpBjID,ROW_NUMBER() over (ORDER BY B.ArtCode)*-1 as OpBJIDD ,B.ArtCode,B.ArtName, B.SatID,B.Isi, Sum(masuk)-Sum(Keluar) As QtyD,0 As Selisih, '' as Ket From T_StokBJ S Inner Join M_Brg B On B.ArtCode=S.ArtCode Where S.GdID ='" & Me.SLUGd.EditValue & "' and B.SatID Like '" & Me.SLUSat.EditValue & "' and Tanggal<='" & Me.DTPTanggal.EditValue & "' and B.Grup='" & Me.SLUGrup.EditValue & "' Group By S.GdID,B.ArtCode,B.ArtName,B.SatID,B.Isi) as x Order By ArtCode asc", koneksi)
            cmsl.Fill(DsMaster, "T_OpBJDtl" & Gol)

            Me.BSave.Focus()
            Me.BandedGridView1.RefreshData()
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.BandedGridView1.GetFocusedDataRow.Item("OpBJIDD")
        End If
    End Sub

    Private Sub BandedGridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles BandedGridView1.CellValueChanged
        If e.Column Is BandedGridView1.Columns("ArtCode") Then
            If DsMaster.Tables("T_OpBJDtl" & Gol).Select("ArtCode= '" & e.Value & "'").Length > 0 Then
                XtraMessageBox.Show("Article Sudah Ada, Tidak Perlu Menambah Article Baru", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.BandedGridView1.DeleteRow(Me.BandedGridView1.FocusedRowHandle)
            End If

        ElseIf e.Column Is BandedGridView1.Columns("QtyF") Then
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Selisih", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyF") - Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyD"))

            If Me.BandedGridView1.GetRowCellValue(e.RowHandle, "SatID") <> "P" Then
                Me.BandedGridView1.SetRowCellValue(e.RowHandle, "DosF", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyF"))
            End If

            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PsgF", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyF") * Me.BandedGridView1.GetRowCellValue(e.RowHandle, "Isi"))
        End If
    End Sub

    Private Sub BandedGridView1_DoubleClick(sender As Object, e As EventArgs) Handles BandedGridView1.DoubleClick
        If Me.BandedGridView1.OptionsBehavior.Editable = True Then
            If Me.BandedGridView1.GetFocusedDataRow.Item("AddDt") = True Then
                Dim frm As New FSearch("Stok BJ Op", Me.SLUSat.EditValue, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue)
                frm.ShowDialog()

                Try
                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "OpBJIDD", Me.BandedGridView1.RowCount * -1)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "OpBJID", Me.TBKode.EditValue)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Isi", CInt(dataTrans.Item("Isi").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "QtyD", CInt(dataTrans.Item("Qty").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "QtyF", CInt(dataTrans.Item("Qty").ToString))

                        If dataTrans.Item("SatID").ToString <> "P" Then
                            Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DosD", CInt(dataTrans.Item("Qty").ToString))
                            Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DosF", CInt(dataTrans.Item("Qty").ToString))
                        Else
                            Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DosD", 0)
                            Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "DosF", 0)
                        End If

                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PsgD", CInt(dataTrans.Item("Qty").ToString) * CInt(dataTrans.Item("Isi").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "PsgF", CInt(dataTrans.Item("Qty").ToString) * CInt(dataTrans.Item("Isi").ToString))

                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Selisih", 0)

                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Ket", "")
                    End If

                Catch ex As Exception

                End Try
            End If

        End If
    End Sub

    Private Sub BandedGridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles BandedGridView1.InitNewRow
        Dim frm As New FSearch("Stok BJ Op", Me.SLUSat.EditValue, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue)
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OpBJIDD", Me.BandedGridView1.RowCount * -1)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OpBJID", Me.TBKode.EditValue)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "AddDt", True)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("Nama").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Isi", CInt(dataTrans.Item("Isi").ToString))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "QtyD", CInt(dataTrans.Item("Qty").ToString))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "QtyF", CInt(dataTrans.Item("Qty").ToString))

            If dataTrans.Item("SatID").ToString <> "P" Then
                Me.BandedGridView1.SetRowCellValue(e.RowHandle, "DosD", CInt(dataTrans.Item("Qty").ToString))
                Me.BandedGridView1.SetRowCellValue(e.RowHandle, "DosF", CInt(dataTrans.Item("Qty").ToString))
            Else
                Me.BandedGridView1.SetRowCellValue(e.RowHandle, "DosD", 0)
                Me.BandedGridView1.SetRowCellValue(e.RowHandle, "DosF", 0)
            End If

            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PsgD", CInt(dataTrans.Item("Qty").ToString) * CInt(dataTrans.Item("Isi").ToString))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PsgF", CInt(dataTrans.Item("Qty").ToString) * CInt(dataTrans.Item("Isi").ToString))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Selisih", 0)

            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Ket", "")
        End If

    End Sub
    Private Sub BandedGridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles BandedGridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.BandedGridView1.GetRowCellValue(e.RowHandle, "Selisih") < 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf Me.BandedGridView1.GetRowCellValue(e.RowHandle, "Selisih") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Green
                ElseIf Me.BandedGridView1.GetRowCellValue(e.RowHandle, "Selisih") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BandedGridView2_DoubleClick(sender As Object, e As EventArgs) Handles BandedGridView2.DoubleClick
        Try
            Dim frm As New FOpBJ_d(Me.BandedGridView2.GetFocusedDataRow.Item("OpBJID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FOpBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class