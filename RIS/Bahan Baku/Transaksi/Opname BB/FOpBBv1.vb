Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FOpBBv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=26", koneksi)

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

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("OpBN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTStokOp_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUPeriodID.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.BandedGridView1.OptionsBehavior.Editable = False

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

        Me.SLUPeriodID.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BandedGridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTStokOp_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,Bulan,Tahun,TglAkhir From M_Period  Order By PeriodID", koneksi)
        cmsl.TableMappings.Add("Table", "M_PeriodLUE")
        cmsl.Fill(DsMaster, "M_PeriodLUE")
        DsMaster.Tables("M_PeriodLUE").Clear()
        cmsl.Fill(DsMaster, "M_PeriodLUE")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("M_PeriodLUE")
        Me.SLUPeriodID.Properties.DisplayMember = "PeriodID"
        Me.SLUPeriodID.Properties.ValueMember = "PeriodID"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol In ('Bahan','Sparepart-Mesin')", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select OpBBIDD,OpBBID,AddDt,J.Nama As Jenis,D.BBID,B.Nama As Bahan,D.Sat,QtyD,SalD,QtyF,SalF, SelisihQty, SelisihSal,HarSat, HarSatAs,D.Ket From T_OpBBDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where OpBBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_OpBBDtl")
        Try
            DsMaster.Tables("T_OpBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_OpBBDtl")

        DsMaster.Tables("T_OpBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_OpBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_OpBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select OpBBID,PeriodID,Tanggal,H.GdID,G.Nama as Gudang,TotQtyD,TotSalD,TotQtyF,TotSalF,TotSelisihQty,TotSelisihSal, H.Ket,H.Terproses,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_OpBB H Inner Join M_Gudang G On H.GdID=G.GdID and PeriodID=" & MainModule.periodAktif & " Order By Tanggal,OpBBID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_OpBB")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_OpBB")
        DsMaster.Tables("T_OpBB").Clear()
        cmsl.Fill(DsMaster, "T_OpBB")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_OpBB"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_OpBB")
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
        If IO.File.Exists("SrStokBBOp.xml") Then
            System.IO.File.Delete("SrStokBBOp.xml")
        End If
    End Sub

    Private Sub FOpBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Stok Opname Bahan Baku"
    End Sub

    Private Sub FOpBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FOpBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTStokOp_e.Selected = True
    End Sub

    Private Sub BVTStokOp_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTStokOp_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Stok Opname Bahan Baku"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("OpBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("OpBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("OpBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Stok Opname Bahan Baku"

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

        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_OpBBDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Stok Opname Bahan Baku"

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
            Me.TBKode.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("OpBBID")
            Me.SLUPeriodID.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("PeriodID")
            Me.DTPTanggal.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUGd.EditValue = Me.BandedGridView2.GetFocusedDataRow.Item("GdID")
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
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim bind As New Collection
        bind.Add(Me.BandedGridView2.GetFocusedDataRow.Item("OpBBID"), "Kode")
        bind.Add(Me.BandedGridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.BandedGridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")

        Dim XR As New XROpBBv1
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Stok Opname Bahan Baku"

        koneksi.Close()

        'If MainModule.SlAdjBB() > 0 Then
        '    If MainModule.BackDate = False Then
        '        XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Adjustment", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If
        'End If

        If MainModule.SlstsPeriodEdDel(Me.BandedGridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.BandedGridView2.GetFocusedDataRow.Item("Terproses") = False Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.BandedGridView2.GetFocusedDataRow.Item("OpBBID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_OpBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.BandedGridView2.GetFocusedDataRow.Item("OpBBID")
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

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.BandedGridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_OpBB")
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
                    .Parameters.Add("@TotQtyD", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("QtyD").SummaryText, Decimal)
                    .Parameters.Add("@TotSalD", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SalD").SummaryText, Decimal)
                    .Parameters.Add("@TotQtyF", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("QtyF").SummaryText, Decimal)
                    .Parameters.Add("@TotSalF", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SalF").SummaryText, Decimal)
                    .Parameters.Add("@TotSelisihQty", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SelisihQty").SummaryText, Decimal)
                    .Parameters.Add("@TotSelisihSal", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SelisihSal").SummaryText, Decimal)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "BBID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_OpBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.BandedGridView1.GetRowCellValue(i, "AddDt")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@QtyD", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyD")
                                    .Parameters.Add("@SalD", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SalD")
                                    .Parameters.Add("@QtyF", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyF")
                                    .Parameters.Add("@SalF", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SalF")
                                    .Parameters.Add("@SelisihQty", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SelisihQty")
                                    .Parameters.Add("@SelisihSal", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SelisihSal")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarSatAs", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "HarSatAs")
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
                Dim cmSP As New SqlCommand("SPUpT_OpBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@TotQtyD", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("QtyD").SummaryText, Decimal)
                    .Parameters.Add("@TotSalD", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SalD").SummaryText, Decimal)
                    .Parameters.Add("@TotQtyF", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("QtyF").SummaryText, Decimal)
                    .Parameters.Add("@TotSalF", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SalF").SummaryText, Decimal)
                    .Parameters.Add("@TotSelisihQty", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SelisihQty").SummaryText, Decimal)
                    .Parameters.Add("@TotSelisihSal", SqlDbType.Decimal).Value = CType(Me.BandedGridView1.Columns("SelisihSal").SummaryText, Decimal)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_OpBBDtl")
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
                            If Me.BandedGridView1.GetRowCellValue(i, "OpBBIDD") < 0 Then
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_OpBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.BandedGridView1.GetRowCellValue(i, "AddDt")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@QtyD", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyD")
                                        .Parameters.Add("@SalD", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SalD")
                                        .Parameters.Add("@QtyF", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyF")
                                        .Parameters.Add("@SalF", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SalF")
                                        .Parameters.Add("@SelisihQty", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SelisihQty")
                                        .Parameters.Add("@SelisihSal", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SelisihSal")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSatAs", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "HarSatAs")
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
                                        Me.BandedGridView1.SetRowCellValue(i, "OpBBIDD", Me.BandedGridView1.GetRowCellValue(i, "OpBBIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_OpBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "OpBBIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.BandedGridView1.GetRowCellValue(i, "AddDt")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@QtyD", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyD")
                                        .Parameters.Add("@SalD", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SalD")
                                        .Parameters.Add("@QtyF", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "QtyF")
                                        .Parameters.Add("@SalF", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SalF")
                                        .Parameters.Add("@SelisihQty", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SelisihQty")
                                        .Parameters.Add("@SelisihSal", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "SelisihSal")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSatAs", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "HarSatAs")
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

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.EditValue = "" Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim jml As Integer

        Dim command As New SqlCommand("Select dbo.fcCekDocBB(" & MainModule.periodAktif & ")", koneksi)

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
                arrPar(arrPar.GetUpperBound(0)) = Me.BandedGridView1.GetRowCellValue(i, "OpBBIDD")

                Me.BandedGridView1.DeleteRow(i)
            Next

            If Me.SLUGd.EditValue = "" Then
                XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            DsMaster.Tables("T_OpBBDtl").Clear()
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '--' As OpBBID,ROW_NUMBER() over (ORDER BY B.BBID)*-1 as OpBBIDD,'False' As AddDt,J.Nama As Jenis,B.BBID,B.Nama As Bahan, B.Sat, Sum(Masuk)-Sum(Keluar) As QtyD,Sum(Masuk)-Sum(Keluar) As QtyF,Sum(NilMasuk)-Sum(NilKeluar) As SalD,Sum(NilMasuk)-Sum(NilKeluar) As SalF,0 As SelisihQty,0 As SelisihSal,(Select Isnull((Select Top 1 HarSat from T_StokBB Where BBID= B.BBID and PeriodID <=" & Me.SLUPeriodID.EditValue & " Order By Tanggal desc,StokID desc),0)) As HarSat,(Select Isnull((Select Top 1 HarSat from T_StokBB Where BBID= B.BBID and PeriodID <=" & Me.SLUPeriodID.EditValue & " Order By Tanggal desc,StokID desc),0)) As HarSatAs,'' as Ket From T_StokBB S Inner Join M_BB B On B.BBID=S.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where S.GdID ='" & Me.SLUGd.EditValue & "' and Tanggal <='" & Me.DTPTanggal.EditValue & "' Group By S.GdID,J.Nama,B.BBID,B.Nama,B.Sat Order By BBID asc", koneksi)

            'cmsl = New SqlDataAdapter("Select '--' As OpBBID,ROW_NUMBER() over (ORDER BY B.BBID)*-1 as OpBBIDD,'False' As AddDt,J.Nama As Jenis,B.BBID,B.Nama As Bahan, B.Sat, (Select dbo.fcStokOpBB(B.BBID,G.GdID," & Me.SLUPeriodID.EditValue & "))As QtyD,(Select dbo.fcStokOpBB(B.BBID,G.GdID," & Me.SLUPeriodID.EditValue & "))As QtyF,(Select dbo.fcSaldoOpBB(B.BBID,G.GdID," & Me.SLUPeriodID.EditValue & "))As SalD,(Select dbo.fcSaldoOpBB(B.BBID,G.GdID," & Me.SLUPeriodID.EditValue & "))As SalF,0 As SelisihQty,0 As SelisihSal,(Select Isnull((Select Top 1 HarSat from T_StokBB Where BBID= B.BBID and PeriodID <=" & Me.SLUPeriodID.EditValue & " Order By Tanggal desc,StokID desc),0)) As HarSat,(Select Isnull((Select Top 1 HarSat from T_StokBB Where BBID= B.BBID and PeriodID <=" & Me.SLUPeriodID.EditValue & " Order By Tanggal desc,StokID desc),0)) As HarSatAs,'' as Ket From M_BB B Left Outer  Join T_StokBB S On B.BBID=S.BBID Left Outer  Join M_BBJns J On B.JnsID=J.JnsID Outer Apply M_Gudang G Where G.GdID ='" & Me.SLUGd.EditValue & "' and B.BBID In (Select Distinct BBID From SaldoBB Union All Select Distinct BBID From T_StokBB) Group By G.GdID,G.Nama,J.Nama,B.BBID,B.Nama,B.Sat Order By BBID asc", koneksi)
            cmsl.Fill(DsMaster, "T_OpBBDtl")

            Me.BSave.Focus()
            Me.BandedGridView1.RefreshData()
        End If


    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.BandedGridView1.GetFocusedDataRow.Item("OpBBIDD")
        End If
    End Sub

    Private Sub BandedGridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles BandedGridView1.CellValueChanged
        If e.Column Is BandedGridView1.Columns("BBID") Then
            If DsMaster.Tables("T_OpBBDtl").Select("BBID= '" & e.Value & "'").Length > 0 Then
                XtraMessageBox.Show("Data Sudah Ada, Tidak Perlu Menambah Data Baru", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.BandedGridView1.DeleteRow(e.RowHandle)
            End If

        ElseIf e.Column Is BandedGridView1.Columns("QtyF") Then
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SelisihQty", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyF") - Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyD"))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SalF", Math.Round(Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyF") * Me.BandedGridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2, MidpointRounding.AwayFromZero))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SelisihSal", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "SalF") - Me.BandedGridView1.GetRowCellValue(e.RowHandle, "SalD"))

        ElseIf e.Column Is BandedGridView1.Columns("HarSat") Then
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SalF", Math.Round(Me.BandedGridView1.GetRowCellValue(e.RowHandle, "QtyF") * Me.BandedGridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2, MidpointRounding.AwayFromZero))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SelisihSal", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "SalF") - Me.BandedGridView1.GetRowCellValue(e.RowHandle, "SalD"))
        End If
    End Sub

    Private Sub BandedGridView1_DoubleClick(sender As Object, e As EventArgs) Handles BandedGridView1.DoubleClick
        If Me.BandedGridView1.OptionsBehavior.Editable = True Then
            If Me.BandedGridView1.GetFocusedDataRow.Item("AddDt") = True Then
                Dim frm As New FSearch("Stok BB Op", "", "", Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue)
                frm.ShowDialog()

                Try
                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "BBID", dataTrans.Item("Kode").ToString)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "QtyD", CDec(dataTrans.Item("QtyD").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "QtyF", CDec(dataTrans.Item("QtyF").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "SalD", CDec(dataTrans.Item("SalD").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "SalF", CDec(dataTrans.Item("SalF").ToString))
                        Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Ket", "")
                    End If

                Catch ex As Exception

                End Try
            End If


        End If
    End Sub

    Private Sub BandedGridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles BandedGridView1.InitNewRow
        Dim frm As New FSearch("Stok BB Op", "", "", Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue)
        frm.ShowDialog()

        RemoveHandler BandedGridView1.CellValueChanged, AddressOf BandedGridView1_CellValueChanged

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OpBBIDD", Me.BandedGridView1.RowCount * -1)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OpBBID", Me.TBKode.EditValue)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "AddDt", True)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JnsID", dataTrans.Item("JnsID").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Jenis", dataTrans.Item("Jenis").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "BBID", dataTrans.Item("Kode").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Bahan", dataTrans.Item("Nama").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Sat", dataTrans.Item("Sat").ToString)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "QtyD", CDec(dataTrans.Item("Qty").ToString))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "QtyF", CDec(dataTrans.Item("Qty").ToString))
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SalD", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SalF", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "HarSatAs", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SelisihQty", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SelisihSal", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Ket", "")
        End If

        AddHandler BandedGridView1.CellValueChanged, AddressOf BandedGridView1_CellValueChanged

    End Sub
    Private Sub BandedGridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles BandedGridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihQty") < 0 Or BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihSal") < 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihQty") > 0 Or BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihSal") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Green
                ElseIf BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihQty") = 0 Or BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihSal") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BandedGridView2_DoubleClick(sender As Object, e As EventArgs) Handles BandedGridView2.DoubleClick
        Try
            Dim frm As New FOpBB_dv1(Me.BandedGridView2.GetFocusedDataRow.Item("OpBBID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FOpBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub BandedGridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BandedGridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        BandedGridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class