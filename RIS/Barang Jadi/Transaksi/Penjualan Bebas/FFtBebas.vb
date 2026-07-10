Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FFtBebas
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, CurrID, KdLama, JnsPPn As String
    Dim Manual, MnlInsUpd, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim IdD As Integer

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=30", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PjBsN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PjBsEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PjBsDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTJual_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        'Me.MAlamat.Properties.ReadOnly = True
        'Me.TBKota.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.TBPajak.Properties.ReadOnly = True
        Me.TBPEB.Properties.ReadOnly = True
        Me.DTPTglPEB.Properties.ReadOnly = True
        Me.TBBL.Properties.ReadOnly = True
        Me.DTPTglBL.Properties.ReadOnly = True
        Me.TBLC.Properties.ReadOnly = True
        Me.TBTamDiscP.Properties.ReadOnly = True
        Me.TBTamDiscRp.Properties.ReadOnly = True
        Me.TBTamDiscP.Properties.ReadOnly = True
        Me.TBTamDiscRp.Properties.ReadOnly = True
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
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTJual_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        'Me.MAlamat.Properties.ReadOnly = False
        'Me.TBKota.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBPajak.Properties.ReadOnly = False
        Me.TBPEB.Properties.ReadOnly = False
        Me.DTPTglPEB.Properties.ReadOnly = False
        Me.TBBL.Properties.ReadOnly = False
        Me.DTPTglBL.Properties.ReadOnly = False
        Me.TBLC.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTJual_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"

        cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where  C.Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustFtB")
        cmsl.Fill(DsMaster, "M_CustFtB")
        DsMaster.Tables("M_CustFtB").Clear()
        cmsl.Fill(DsMaster, "M_CustFtB")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustFtB")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Public Sub CekCurr()
        Dim cmSl As New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTanggal.EditValue & "') AND (Akhir >= '" & Me.DTPTanggal.EditValue & "') AND (MtUang = '" & Me.SLUMtUang.EditValue & "')ORDER BY Tanggal DESC")
        cmSl.CommandType = CommandType.Text
        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim Reader As SqlClient.SqlDataReader

        With cmSl
            .Connection = koneksi

            With koneksi
                .Open()
                Reader = cmSl.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        CurrID = Reader.Item(0)
                        Me.TBNilTukarRp.EditValue = Reader.Item(1)
                    End While
                Else
                    XtraMessageBox.Show("Nilai Tukar Rupiah Untuk Tanggal " & Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") & " Belum Diinput !" & vbCrLf & "Silakan diinput Terlebih Dahulu !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.Dispose()
                End If
                Reader.Close()

                .Close()
            End With
        End With
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualIDD,JualID,Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir From T_JualBebasDtl Where JualID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualBebasDtl")
        Try
            DsMaster.Tables("T_JualBebasDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_JualBebasDtl")

        DsMaster.Tables("T_JualBebasDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBebasDtl").Columns("Nama")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_JualBebasDtl"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualID,PeriodID,Tanggal,Jenis,NoPajak,H.CustID,C.Nama,C.Alamat,K.Nama As Kota,CurrID,MtUang,NilTukarRp, Kat,TipePPn,PersenPPn,NoPajak,PEB,TglPEB,BL,TglBL,LC,TotSbDisc,DiscP,RpDiscP,DiscRp,TotDisc,TotDPP,TotPPn,TotQty,TotAkhir,TotAkhirRp,SisaBayar, H.Ket,stsPPn,stsLunas,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_JualBebas H Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,JualID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualBebas")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_JualBebas")
        DsMaster.Tables("T_JualBebas").Clear()
        cmsl.Fill(DsMaster, "T_JualBebas")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_JualBebas"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_JualBebas")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = "DelEr"
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
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue + Me.TBTotPPn.EditValue
        End If
    End Sub

    Private Sub FFtBebas_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Penjualan Bebas"
    End Sub

    Private Sub FFtBebas_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FFtBebas_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FFtBebas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTJual_e.Selected = True
    End Sub

    Private Sub BVTJualBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTJual_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Penjualan Bebas"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("PjBsP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Penjualan Bebas"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ("Own") > 0 Or MainModule.SlOpBJ("Job Order") > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

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

        Me.TBPajak.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.MAlamat.EditValue = ""
        Me.TBKota.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOKat.EditValue = "Lokal"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.TBPEB.EditValue = ""
        Me.DTPTglPEB.EditValue = Date.Now
        Me.TBBL.EditValue = ""
        Me.DTPTglBL.EditValue = Date.Now
        Me.TBLC.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_JualBebasDtl").Clear()
        ReDim arrPar(-1)
        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Penjualan Bebas"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotAkhir") = Me.GridView2.GetFocusedDataRow.Item("SisaBayar") Then
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("JualID")
            KdLama = Me.GridView2.GetFocusedDataRow.Item("JualID")
            Me.TBPajak.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoPajak")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
            Me.MAlamat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Alamat")
            Me.TBKota.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kota")
            Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
            Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
            Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
            Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")

            If Me.RBPPn.EditValue = "Include" Then
                Me.TBPersenPPn.Properties.ReadOnly = True
            ElseIf Me.RBPPn.EditValue = "Exclude" Then
                Me.TBPersenPPn.Properties.ReadOnly = False
            ElseIf Me.RBPPn.EditValue = "Non PPn" Then
                Me.TBPersenPPn.Properties.ReadOnly = True
            End If
            Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
            Me.TBPEB.EditValue = Me.GridView2.GetFocusedDataRow.Item("PEB")
            Me.DTPTglPEB.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglPEB")
            Me.TBBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("BL")
            Me.DTPTglBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglBL")
            Me.TBLC.EditValue = Me.GridView2.GetFocusedDataRow.Item("LC")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
            stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPpn")
            Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
            Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
            Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
            Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
            Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
            Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
            Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

            FillDtl(Me.TBKode.EditValue)
            ReDim arrPar(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True
        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Penjualan Bebas"

        koneksi.Close()

        If MainModule.SlOpBJ("Own") > 0 Or MainModule.SlOpBJ("Job Order") > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotAkhir") = Me.GridView2.GetFocusedDataRow.Item("SisaBayar") Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("JualID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_JualBebas")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JualID")
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
        Else
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        koneksi.Close()

        Dim Kode As String
        Kode = Me.GridView2.GetFocusedDataRow.Item("JualID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "JualID") = Kode Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JualID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Nama"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PersenPPn"), "PersenPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotQty"), "TotQty")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRFtBebas
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 4, MidpointRounding.AwayFromZero)
        'Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue
        HitPPn()

        If Me.RBPPn.EditValue <> "Non PPn" Then
            stsPPn = True
            JnsPPn = "PPn"
        Else
            stsPPn = False
            JnsPPn = "Non PPn"
        End If

        Dim command As New SqlCommand("Select CodeID From M_DocCode Where DocID=30", koneksi)

        With koneksi
            .Open()
            CodeID = command.ExecuteScalar()
            .Close()
        End With


        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_JualBebas")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 30
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@NoPjk", SqlDbType.VarChar).Value = Me.TBPajak.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    '.Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    '.Parameters.Add("@Kota", SqlDbType.VarChar).Value = Me.TBKota.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PEB", SqlDbType.VarChar).Value = Me.TBPEB.EditValue
                    .Parameters.Add("@TglPEB", SqlDbType.Date).Value = Me.DTPTglPEB.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
                    .Parameters.Add("@LC", SqlDbType.VarChar).Value = Me.TBLC.EditValue
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@PPn", SqlDbType.Bit).Value = stsPPn
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_JualBebasDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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
                Dim cmSP As New SqlCommand("SPUpT_JualBebas")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 30
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@NoPjk", SqlDbType.VarChar).Value = Me.TBPajak.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    '.Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    '.Parameters.Add("@Kota", SqlDbType.VarChar).Value = Me.TBKota.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PEB", SqlDbType.VarChar).Value = Me.TBPEB.EditValue
                    .Parameters.Add("@TglPEB", SqlDbType.Date).Value = Me.DTPTglPEB.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
                    .Parameters.Add("@LC", SqlDbType.VarChar).Value = Me.TBLC.EditValue
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@PPn", SqlDbType.Bit).Value = stsPPn
                    .Parameters.Add("@PPnLama", SqlDbType.Bit).Value = stsPPnLama
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
                            x = cmSP.Parameters("@Return").Value
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_JualBebasDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdLama 'Me.TBKode.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "JualIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_JualBebasDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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
                                        Me.GridView1.SetRowCellValue(i, "JualIDD", Me.GridView1.GetRowCellValue(i, "JualIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_JualBebasDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                        .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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

        Dim cmd2 As New SqlCommand("SPAftSJualBebas")
        cmd2.CommandType = CommandType.StoredProcedure

        With cmd2
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Connection = koneksi

            With koneksi
                .Open()
                cmd2.ExecuteNonQuery()
                .Close()
            End With

        End With

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        Try
            If Not IsDBNull(Me.SLUCust.EditValue) And Me.SLUCust.Properties.ReadOnly = False Then
                Me.MAlamat.EditValue = DsMaster.Tables("M_CustFtB").Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("Alamat")
                Me.TBKota.EditValue = DsMaster.Tables("M_CustFtB").Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("Kota")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("JualIDD")
        End If

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Qty") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 4))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP")) / 100, 4))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 4))

        ElseIf e.Column Is GridView1.Columns("HarSat") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 4))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP")) / 100, 4))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 4))

        ElseIf e.Column Is GridView1.Columns("DiscRp") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") = 0 Then
                Me.GridView1.Columns("DiscP").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("DiscRp").OptionsColumn.AllowEdit = True
            Else
                Me.GridView1.Columns("DiscP").OptionsColumn.AllowEdit = False
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 4))

        ElseIf e.Column Is GridView1.Columns("DiscP") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP") = 0 Then
                Me.GridView1.Columns("DiscP").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("DiscRp").OptionsColumn.AllowEdit = True
            Else
                Me.GridView1.Columns("DiscRp").OptionsColumn.AllowEdit = False
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP")) / 100, 4))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 4))
        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "JualIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Nama", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub
    Private Sub RBPPn_Leave(sender As Object, e As EventArgs) Handles RBPPn.Leave
        If Me.RBPPn.EditValue = "Include" Then
            Me.TBPersenPPn.Properties.ReadOnly = True
            Me.TBPersenPPn.EditValue = 0
            Me.RBPPn.EditValue = "Non PPn"

            XtraMessageBox.Show("PPn Tidak Boleh Include", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBPersenPPn.Properties.ReadOnly = False
            Me.TBPersenPPn.EditValue = MainModule.PersenPPn

        ElseIf Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBPersenPPn.Properties.ReadOnly = True
            Me.TBPersenPPn.EditValue = 0

        End If
    End Sub

    Private Sub RBPPn_EditValueChanged(sender As Object, e As EventArgs) Handles RBPPn.EditValueChanged
        HitPPn()
    End Sub

    Private Sub TBPersenPPn_EditValueChanged(sender As Object, e As EventArgs) Handles TBPersenPPn.EditValueChanged
        HitPPn()
    End Sub

    Private errorProvider As New DXErrorProvider()

    Private Sub TBPersenPPn_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TBPersenPPn.Validating
        Dim edit = TryCast(sender, TextEdit)
        If Me.TBPersenPPn.EditValue > 100 Then
            errorProvider.SetError(edit, "PPn Melebihi 100%!!", ErrorType.Critical)
            e.Cancel = True
        Else
            errorProvider.ClearErrors()
        End If
    End Sub


    Private Sub TBPersenPPn_Leave(sender As Object, e As EventArgs) Handles TBPersenPPn.Leave
        If Me.RBPPn.EditValue <> "Non PPn" And Me.TBPersenPPn.EditValue <> MainModule.PersenPPn And Me.TBPersenPPn.EditValue <= 100 Then
            If XtraMessageBox.Show("Persen PPn Tidak Sesuai Dengan Yang Berlaku. Apakah Tetap Mau Diproses?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Else
                Me.RBPPn.EditValue = "Non PPn"
                Me.TBPersenPPn.Properties.ReadOnly = True
                Me.TBPersenPPn.EditValue = 0
            End If
        End If

        HitPPn()
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        If Me.GridView1.OptionsBehavior.Editable = True Then
            Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 4, MidpointRounding.AwayFromZero)
            HitPPn()
        End If
    End Sub

    Private Sub TBTamDiscRp_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscRp.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub

    Private Sub TBTamDiscP_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscP.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FFtBebas_d(Me.GridView2.GetFocusedDataRow.Item("JualID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub TBTamDiscRp_KeyDown(sender As Object, e As KeyEventArgs) Handles TBTamDiscRp.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBTamDiscRp_KeyUp(sender As Object, e As KeyEventArgs) Handles TBTamDiscRp.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBTamDiscP_KeyDown(sender As Object, e As KeyEventArgs) Handles TBTamDiscP.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBTamDiscP_KeyUp(sender As Object, e As KeyEventArgs) Handles TBTamDiscP.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPajak.KeyPress, TBPEB.KeyPress, TBBL.KeyPress, TBLC.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Nama" Or view.FocusedColumn.FieldName = "Sat" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

End Class