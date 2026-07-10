Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

'Khusus PO Jasa Produksi Tidak Menambah Stok karena ribet (harus mengikuti semua pergerakan bahan di dalam produksi)

Public Class FTrmBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim TglScK As Date
    Dim CodeID, JnsPPn, KdLama, CurrID, MtUang, BtNumLama As String
    Dim Manual, MnlInsUpd, stsQC, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim NilTukar As Decimal
    Dim DueDate As Integer
    Dim InisialBC As String = ""
    Dim Jenis, Gol, Kat As String
    Dim StokSbEd As Decimal
    Dim FC As Boolean
    Dim stsOdJasa, stsNonPT As Boolean
    Dim POIDDCp, BOMIDCp, BBIDCp, BahanCp, SatCp As String
    Dim QtyPLCp, QtyActCp, QtyRjCp, HarSatCp, DiscRpSatCp, DiscPCp As Decimal

    Public Sub New(BBSpM As String, Jns As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand

        Jenis = Jns
        Gol = BBSpM

        If Jenis = "Stock" Then
            command = New SqlCommand("Select Distinct Manuall,MnlInsUpd From M_DocCode Where DocID=6 and CabID='" & Gol & "'", koneksi)

        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select Distinct Manuall,MnlInsUpd From M_DocCode Where DocID=57 and CabID='" & Gol & "'", koneksi)
        End If

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        MnlInsUpd = Reader.Item(1)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        Me.BVTTrmBB_e.Caption = "Penerimaan " & Gol
        Me.Text = ".: Penerimaan " & Gol & " " & Jenis & " :."

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrmBN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTTrmBB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBSJ.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUPOID.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.TBPENID.Properties.ReadOnly = True
        Me.DTPTglPEN.Properties.ReadOnly = True
        Me.TBJnsDok.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.SLUCurr.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled =
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTrmBB_s.Enabled = False

        Me.TBSJ.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUPOID.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.TBPENID.Properties.ReadOnly = False
        Me.DTPTglPEN.Properties.ReadOnly = False
        Me.TBJnsDok.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.SLUCurr.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTrmBB_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE")
        cmsl.Fill(DsMaster, "M_SuppLUE")
        DsMaster.Tables("M_SuppLUE").Clear()
        cmsl.Fill(DsMaster, "M_SuppLUE")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select TrmIDD,TrmID,POIDD,BOMID,'" & InisialBC & "'+D.BBID as BBID,B.Nama as Bahan,BtNum,D.Sat,Qty,QtyPL,QtyAct, QtyRj,HarSat,HarSbDisc,DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir,HarSatDPP,HarBahan From T_TrmBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where TrmID='" & Kode & "'", koneksi)

            cmsl.TableMappings.Add("Table", "T_TrmBBDtl")
            cmsl.Fill(DsMaster, "T_TrmBBDtl")
            DsMaster.Tables("T_TrmBBDtl").Clear()
            cmsl.Fill(DsMaster, "T_TrmBBDtl")

            If Manual = True Then
                DsMaster.Tables("T_TrmBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBBDtl").Columns("POIDD"), DsMaster.Tables("T_TrmBBDtl").Columns("BBID"), DsMaster.Tables("T_TrmBBDtl").Columns("BtNum")}
            Else
                DsMaster.Tables("T_TrmBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBBDtl").Columns("BOMID"), DsMaster.Tables("T_TrmBBDtl").Columns("BBID"), DsMaster.Tables("T_TrmBBDtl").Columns("BtNum")}
            End If

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_TrmBBDtl"

        Catch ex As Exception
            XtraMessageBox.Show("Data Error Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FC = True
        End Try


    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrmID,PeriodID,CodeID,Jenis,H.Gol,Tanggal,TglScK,H.SuppID,S.Nama as Supplier,S.Alamat,K.Nama As Kota, POID,TglJT,H.GdID,G.Nama as Gudang,SJ,CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,PENID,TglPEN,JnsDoc,TotQty,TotSbDisc,DiscP,RpDiscP, DiscRpSat,DiscRp,TotDisc,TotDPP,TotPPn,TotAkhir,H.Ket,H.stsOdJasa,H.stsNonPT,stsQC,stsHasilQC,stsPPn,stsTagih,H.InsDate,H.InsBy, H.UpdDate,H.UpdBy,(Select Count(*) From (Select (Select Sum(Masuk)-Sum(Keluar) From T_StokBB where DocID<>T_TrmBBDtl.TrmID and BBID=T_TrmBBDtl.BBID and BtNum=T_TrmBBDtl.BtNum) As stok,* From T_TrmBBDtl where TrmID=H.TrmID) as x where stok <0) as Cek From T_TrmBB H Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID and PeriodID=" & MainModule.periodAktif & " and Jenis='" & Jenis & "' and H.Gol ='" & Gol & "' Order By TrmID,Tanggal asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrmBB" & Jenis & Gol)
        cmsl.Fill(DsMaster, "T_TrmBB" & Jenis & Gol)
        DsMaster.Tables("T_TrmBB" & Jenis & Gol).Clear()
        cmsl.Fill(DsMaster, "T_TrmBB" & Jenis & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TrmBB" & Jenis & Gol
    End Sub

    Public Sub CekCurr()
        Dim cmSl As New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTanggal.EditValue & "') AND (Akhir >= '" & Me.DTPTanggal.EditValue & "') AND (MtUang = '" & MtUang & "')ORDER BY Tanggal DESC")
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
                        NilTukar = Reader.Item(1)
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

    Public Sub CekInsBC()
        If Manual = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & Me.SLUGd.EditValue & "'", koneksi)

            With koneksi
                .Open()
                InisialBC = command.ExecuteScalar()
                .Close()
            End With
        End If
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue + Me.TBTotPPn.EditValue
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TrmBB")
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
            End Try
        End With
    End Sub

    Public Sub Print()
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TrmID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supplier"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "POID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJ"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglJT"), "TglJT")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")

        Dim XR As New XRTrmBB
        XR.InitializeData(bind, Me.GridView2.GetFocusedDataRow.Item("Jenis"), Me.GridView2.GetFocusedDataRow.Item("Gol"))

    End Sub

    Public Sub PrintH()
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TrmID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supplier"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "POID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PersenPPn"), "PersenPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJ"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglJT"), "TglJT")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")

        Dim XR As New XRTrmBBH
        XR.InitializeData(bind, Me.GridView2.GetFocusedDataRow.Item("Jenis"), Me.GridView2.GetFocusedDataRow.Item("Gol"))
    End Sub

    Private Sub FTrmBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Penerimaan Item"
    End Sub

    Private Sub FTrmBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FTrmBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FTrmBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTrmBB_e.Selected = True

        If Manual = True Then
            Me.GridColumn3.Visible = False
            Me.LCIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Else
            Me.GridColumn3.Visible = True
            Me.LCIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub BVTTrmBB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTrmBB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Penerimaan Item"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrmBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrmBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("TrmBP"), Boolean)
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("TrmBPH"), Boolean)

    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Penerimaan Item"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
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
            Me.LCIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
            Me.LCIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIKurs.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        Me.SLUSupp.EditValue = ""
        Me.SLUPOID.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.TBSJ.EditValue = ""
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.TBPENID.EditValue = ""
        Me.DTPTglPEN.EditValue = Date.Now
        Me.TBJnsDok.EditValue = ""
        Me.MKet.EditValue = ""
        Me.SLUCurr.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBDiscRpSat.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTamDiscRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBPersenPPn.EditValue = 0
        Me.TBInfo.EditValue = ""
        MtUang = "IDR"

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TrmBBDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Penerimaan Item"

        'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlTagih(Me.GridView2.GetFocusedDataRow.Item("TrmID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditagihkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TrmID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("TrmID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")

        Dim cmsl As SqlDataAdapter

        If Jenis = "Stock" Then
            cmsl = New SqlDataAdapter("Select *,DATEDIFF(dd,TglScK,GetDate()) as Hari From (Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn, MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket,Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='Stock' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and Gol='" & Gol & "' or POID In (Select POID From T_TrmBB where TrmID ='" & Me.TBKode.EditValue & "')) as x Union All Select *,DATEDIFF(dd,TglScK,GetDate()) as Hari From (Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn, MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket,Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='Stock' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='True' and Gol='" & Gol & "' and stsOdJasa='True') as x", koneksi)

        ElseIf Jenis = "Non Stock" Then
            cmsl = New SqlDataAdapter("Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket, Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='Non Stock' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and Gol='" & Gol & "' or POID In (Select POID From T_TrmBB where TrmID ='" & Me.TBKode.EditValue & "') Union All Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket, Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='Non Stock' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and Gol='" & Gol & "' or POID In (Select POID From T_TrmBB where TrmID ='" & Me.TBKode.EditValue & "') and stsOdJasa='True'", koneksi)
        End If
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.TableMappings.Add("Table", "T_POBBTrm")

        Try
            DsMaster.Tables("T_POBBTrm").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBBTrm")

        Me.SLUPOID.Properties.DataSource = DsMaster.Tables("T_POBBTrm")
        Me.SLUPOID.Properties.DisplayMember = "POID"
        Me.SLUPOID.Properties.ValueMember = "POID"

        Me.SLUPOID.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.TBSJ.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJ")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        stsQC = Me.GridView2.GetFocusedDataRow.Item("stsQC")
        stsPPn = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        MtUang = Me.GridView2.GetFocusedDataRow.Item("MtUang")

        If Manual = False Then
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Else
            Dim cmsl2 As SqlDataAdapter

            cmsl2 = New SqlDataAdapter("Select CurrID,Awal,Akhir,NilTukarRp From M_Curr where MtUang='" & MtUang & "' and MtUang<>'IDR' and Awal>=DATEADD(Month, -2, '" & Me.DTPTanggal.EditValue & "') Union All Select CurrID,Awal,Akhir,NilTukarRp From M_Curr where MtUang='" & MtUang & "' and MtUang='IDR'", koneksi)
            cmsl2.TableMappings.Add("Table", "M_CurrLUE")
            cmsl2.Fill(DsMaster, "M_CurrLUE")
            DsMaster.Tables("M_CurrLUE").Clear()
            cmsl2.Fill(DsMaster, "M_CurrLUE")

            Me.SLUCurr.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
            Me.SLUCurr.Properties.DisplayMember = "NilTukarRp"
            Me.SLUCurr.Properties.ValueMember = "CurrID"

            Me.SLUCurr.EditValue = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        End If

        NilTukar = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        TglScK = Me.GridView2.GetFocusedDataRow.Item("TglScK")
        Kat = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("Kat")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
        Me.TBPENID.EditValue = Me.GridView2.GetFocusedDataRow.Item("PENID")
        Me.DTPTglPEN.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglPEN")
        Me.TBJnsDok.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsDoc")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
        Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
        Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
        Me.TBDiscRpSat.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRpSat")
        Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
        Me.TBTotDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDisc")
        Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
        Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
        Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")
        Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
        stsOdJasa = Me.GridView2.GetFocusedDataRow.Item("stsOdJasa")
        stsNonPT = Me.GridView2.GetFocusedDataRow.Item("stsNonPT")


        CekInsBC()
        FillDtl(Me.TBKode.EditValue)

        ReDim arrPar(-1)

        If FC = True Then
            Me.Dispose()
            Me.Dispose()
            Exit Sub
        End If

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()

        'If Manual = False Then
        '    If Me.GridView2.GetFocusedDataRow.Item("Cek") < 0 Then
        '        Me.SLUPOID.Properties.ReadOnly = True
        '        Me.SLUSupp.Properties.ReadOnly = True
        '        Me.SLUGd.Properties.ReadOnly = True

        '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
        '        Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = False
        '        Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = False

        '    Else
        '        Me.SLUPOID.Properties.ReadOnly = False
        '        Me.SLUSupp.Properties.ReadOnly = False
        '        Me.SLUGd.Properties.ReadOnly = False

        '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
        '        Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
        '        Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = True
        '    End If

        '    Dim Stok2, Stok3, Stok As Decimal

        '    Dim command As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(0, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(0, "TrmIDD") & "','" & Me.GridView1.GetRowCellValue(0, "BtNum") & "')", koneksi)

        '    With koneksi
        '        .Open()
        '        Stok2 = command.ExecuteScalar()
        '        .Close()
        '    End With

        '    Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(0, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(0, "TrmIDD") & ")", koneksi)

        '    With koneksi
        '        .Open()
        '        Stok3 = command3.ExecuteScalar()
        '        .Close()
        '    End With

        '    If Stok2 > Stok3 Then
        '        Stok = Stok3
        '    Else
        '        Stok = Stok2
        '    End If


        '    If Stok < 0 Then
        '        Me.SLUPOID.Properties.ReadOnly = True
        '        Me.SLUSupp.Properties.ReadOnly = True
        '        Me.SLUGd.Properties.ReadOnly = True

        '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
        '        'Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = False
        '        'Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = False

        '    Else
        '        Me.SLUPOID.Properties.ReadOnly = False
        '        Me.SLUSupp.Properties.ReadOnly = False
        '        Me.SLUGd.Properties.ReadOnly = False

        '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
        '        'Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
        '        'Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = True
        '    End If

        'End If

        'AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        CekSave = True
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Print()
    End Sub

    Private Sub BVBPrintH_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintH.ItemClick
        PrintH()
    End Sub


    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Penerimaan Item"

        koneksi.Close()
        'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlTagih(Me.GridView2.GetFocusedDataRow.Item("TrmID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ditagihkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If Manual = False Then
        '    Dim Hapus As Boolean = True

        '    FillDtl(Me.GridView2.GetFocusedDataRow.Item("TrmID"))

        '    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '        Dim Stok2, Stok3, Stok As Decimal

        '        Dim command As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.GridView2.GetFocusedDataRow.Item("GdID") & "','" & Me.GridView2.GetFocusedDataRow.Item("TrmID") & "'," & Me.GridView1.GetRowCellValue(i, "TrmIDD") & ",'" & Me.GridView1.GetFocusedDataRow.Item("BtNum") & "')", koneksi)

        '        With koneksi
        '            .Open()
        '            Stok2 = command.ExecuteScalar()
        '            .Close()
        '        End With

        '        Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(i, "TrmIDD") & ")", koneksi)

        '        With koneksi
        '            .Open()
        '            Stok3 = command3.ExecuteScalar()
        '            .Close()
        '        End With

        '        If Stok2 > Stok3 Then
        '            Stok = Stok3
        '        Else
        '            Stok = Stok2
        '        End If

        '        If Stok < 0 Then
        '            Hapus = False

        '            Exit For
        '        Else
        '            Hapus = True
        '        End If
        '    Next

        'If Hapus = False Then
        '    XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        'End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TrmID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TrmBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TrmID")
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

        'AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGd.EditValue = "" Or IsDBNull(Me.SLUGd.EditValue) Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBSJ.EditValue = "" Or IsDBNull(Me.TBSJ.EditValue) Then
            XtraMessageBox.Show("Nomer Surat Jalan / PL / Invoice Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTamDiscRp.EditValue = Math.Round(Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBDiscRpSat.EditValue, 2, MidpointRounding.AwayFromZero)
        Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        'Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue

        HitPPn()

        If Me.RBPPn.EditValue <> "Non PPn" Then
            stsPPn = True
            JnsPPn = "PPn"
        Else
            stsPPn = False
            JnsPPn = "Non PPn"
        End If

        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            command = New SqlCommand("Select CodeID From M_DocCode Where DocID=6 and Gol='" & JnsPPn & "' and CabID='" & Gol & "'", koneksi)
        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select CodeID From M_DocCode Where DocID=57 and Gol='" & JnsPPn & "' and CabID='" & Gol & "'", koneksi)
        End If

        With koneksi
            .Open()
            CodeID = command.ExecuteScalar()
            .Close()
        End With

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_TrmBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglScK", SqlDbType.Date).Value = TglScK
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    .Parameters.Add("@TglJT", SqlDbType.Date).Value = DateAdd(DateInterval.Day, DueDate, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = MtUang

                    If Manual = False Then
                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = NilTukar
                    Else
                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = Me.SLUCurr.EditValue
                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.SLUCurr.Text
                    End If

                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PENID", SqlDbType.VarChar).Value = Me.TBPENID.EditValue
                    .Parameters.Add("@TglPEN", SqlDbType.Date).Value = Me.DTPTglPEN.EditValue
                    .Parameters.Add("@JnsDoc", SqlDbType.VarChar).Value = Me.TBJnsDok.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.TBDiscRpSat.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsOdJasa", SqlDbType.Bit).Value = stsOdJasa
                    .Parameters.Add("@stsNonPT", SqlDbType.Bit).Value = stsNonPT
                    .Parameters.Add("@stsQC", SqlDbType.Bit).Value = stsQC
                    .Parameters.Add("@stsPPn", SqlDbType.Bit).Value = stsPPn
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TrmBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                                    .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@QtyPL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPL")
                                    .Parameters.Add("@QtyAct", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyAct")
                                    .Parameters.Add("@QtyRj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyRj")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRpSat")
                                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.TBDiscRpSat.EditValue
                                    .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
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

                                If x = -3 Then
                                    Del()
                                    XtraMessageBox.Show("Batch Number Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit Sub
                                ElseIf x <> 0 Then
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
                        ElseIf x = -3 Then
                            Del()
                            XtraMessageBox.Show("Batch Number Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                Dim cmSP As New SqlCommand("SPUpT_TrmBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglScK", SqlDbType.Date).Value = TglScK
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    .Parameters.Add("@TglJT", SqlDbType.Date).Value = DateAdd(DateInterval.Day, DueDate, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = MtUang
                    If Manual = False Then
                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = NilTukar
                    Else
                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = Me.SLUCurr.EditValue
                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.SLUCurr.Text
                    End If
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PENID", SqlDbType.VarChar).Value = Me.TBPENID.EditValue
                    .Parameters.Add("@TglPEN", SqlDbType.Date).Value = Me.DTPTglPEN.EditValue
                    .Parameters.Add("@JnsDoc", SqlDbType.VarChar).Value = Me.TBJnsDok.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.TBDiscRpSat.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsOdJasa", SqlDbType.Bit).Value = stsOdJasa
                    .Parameters.Add("@stsNonPT", SqlDbType.Bit).Value = stsNonPT
                    .Parameters.Add("@stsQC", SqlDbType.Bit).Value = stsQC
                    .Parameters.Add("@stsPPn", SqlDbType.Bit).Value = stsPPn
                    .Parameters.Add("@stsPPnLama", SqlDbType.Bit).Value = stsPPnLama
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

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_TrmBBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TrmIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TrmBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                        .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                        .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPL")
                                        .Parameters.Add("@QtyAct", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyAct")
                                        .Parameters.Add("@QtyRj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyRj")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRpSat")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.TBDiscRpSat.EditValue
                                        .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
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
                                        Me.GridView1.SetRowCellValue(i, "TrmIDD", Me.GridView1.GetRowCellValue(i, "TrmIDD") * -1)
                                    ElseIf x = -3 Then
                                        XtraMessageBox.Show("Batch Number Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TrmBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                        .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                        .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                                        .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPL")
                                        .Parameters.Add("@QtyAct", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyAct")
                                        .Parameters.Add("@QtyRj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyRj")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRpSat")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.TBDiscRpSat.EditValue
                                        .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
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

                                    If x = -3 Then
                                        XtraMessageBox.Show("Batch Number Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        Exit Sub
                                    ElseIf x <> 0 Then
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
                        ElseIf x = -3 Then
                            XtraMessageBox.Show("Batch Number Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

        Dim cmd2 As New SqlCommand("SPAftSTrmBB")
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

        Me.BVTTrmBB_s.Selected = True
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("TrmBP"), Boolean)
        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "TrmID") = Me.TBKode.EditValue Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Print()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TrmIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            POIDDCp = Me.GridView1.GetFocusedDataRow.Item("POIDD")
            BOMIDCp = Me.GridView1.GetFocusedDataRow.Item("BOMID")
            BBIDCp = Me.GridView1.GetFocusedDataRow.Item("BBID")
            BahanCp = Me.GridView1.GetFocusedDataRow.Item("Bahan")
            SatCp = Me.GridView1.GetFocusedDataRow.Item("Sat")
            QtyPLCp = Me.GridView1.GetFocusedDataRow.Item("QtyPL")
            QtyActCp = Me.GridView1.GetFocusedDataRow.Item("QtyAct")
            QtyRjCp = Me.GridView1.GetFocusedDataRow.Item("QtyRj")
            HarSatCp = Me.GridView1.GetFocusedDataRow.Item("HarSat")
            DiscRpSatCp = Me.GridView1.GetFocusedDataRow.Item("DiscRpSat")
            DiscPCp = Me.GridView1.GetFocusedDataRow.Item("DiscP")
        End If
    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "TrmIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TrmID", Me.TBKode.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "POIDD", POIDDCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", BOMIDCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", BBIDCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BtNum", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Bahan", BahanCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", SatCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPL", QtyPLCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "QtyAct", QtyActCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "QtyRj", QtyRjCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", HarSatCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRpSat", DiscRpSatCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", DiscPCp)
            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            Dim stsLebih As Boolean

            Dim command2 As New SqlCommand("Select Lebih From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where BBID=Right('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "',Len('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "')-Len('" & InisialBC & "'))", koneksi)

            With koneksi
                .Open()
                stsLebih = command2.ExecuteScalar()
                .Close()
            End With

            Dim Sisa, PO, Pol As Decimal
            Dim TrmLain As Decimal = 0

            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView1.GetFocusedRowCellValue("POIDD") And Me.GridView1.GetRowCellValue(i, "TrmIDD") <> Me.GridView1.GetFocusedRowCellValue("TrmIDD") Then
                    TrmLain += Me.GridView1.GetRowCellValue(i, "Qty")
                End If
            Next

            Dim command As New SqlCommand("Select Qty-BtlOrder-(Select Isnull((Select Sum(Qty) From T_TrmBBDtl Where POIDD=T_POBBDtl.POIDD and TrmID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where POIDD=T_POBBDtl.POIDD),0)) From T_POBBDtl Where POID='" & Me.SLUPOID.EditValue & "' and POIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") & "", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If stsOdJasa = False Then
                If stsLebih = False Then
                    'MsgBox("1" & stsLebih)
                    If Kat = "Lokal" Then
                        'MsgBox("1.1" & Kat)
                        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa - TrmLain Then
                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa - TrmLain)
                        End If
                    End If
                    'MsgBox("1.1.1" & Kat)

                Else
                    'MsgBox("2" & stsLebih)
                    Dim command3 As New SqlCommand("Select Qty-BtlOrder From T_POBBDtl Where POID='" & Me.SLUPOID.EditValue & "' and POIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") & "", koneksi)

                    With koneksi
                        .Open()
                        PO = command3.ExecuteScalar()
                        .Close()
                    End With

                    Pol = PO * PersenLbh / 100

                    If Kat = "Lokal" Then
                        'MsgBox("2.1" & Kat & " " & Sisa & " " & Pol & " " & TrmLain & " PersenLbh" & PersenLbh)
                        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa + Pol - TrmLain Then
                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa + Pol - TrmLain)
                        End If
                    End If
                    MsgBox("2.2" & Kat)
                End If
            End If

            'If Indicator <> 100 Then
            '    If Manual = False Then
            '        Dim Stok2, Stok3, Stok As Decimal

            '        Dim commands As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)


            '        With koneksi
            '            .Open()
            '            Stok2 = commands.ExecuteScalar()
            '            .Close()
            '        End With

            '        Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmIDD") & ")", koneksi)

            '        With koneksi
            '            .Open()
            '            Stok3 = command3.ExecuteScalar()
            '            .Close()
            '        End With

            '        If Stok2 > Stok3 Then
            '            Stok = Stok3
            '        Else
            '            Stok = Stok2
            '        End If

            '        If Stok + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") < 0 Then
            '            XtraMessageBox.Show("Qty Tidak Boleh Dirubah Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok * -1)

            '            'Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", StokSbEd)
            '        End If
            '    End If
            'End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRpSat"), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

            'ElseIf e.Column Is GridView1.Columns("BtNum") Then
            'If Indicator <> 100 Then
            '    If Manual = False Then
            '        Dim Stok2, Stok3, Stok As Decimal

            '        Dim commands As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)


            '        With koneksi
            '            .Open()
            '            Stok2 = commands.ExecuteScalar()
            '            .Close()
            '        End With

            '        Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmIDD") & ")", koneksi)

            '        With koneksi
            '            .Open()
            '            Stok3 = command3.ExecuteScalar()
            '            .Close()
            '        End With

            '        If Stok2 > Stok3 Then
            '            Stok = Stok3
            '        Else
            '            Stok = Stok2
            '        End If

            '        If Stok + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") < 0 Then
            '            XtraMessageBox.Show("Batch Number Tidak Boleh Dirubah Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Me.GridView1.SetRowCellValue(e.RowHandle, "BtNum", BtNumLama)
            '        End If
            '    End If
            'End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTrmBB_d(Me.GridView2.GetFocusedDataRow.Item("TrmID"), Me.GridView2.GetFocusedDataRow.Item("GdID"), Manual)
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave

        If Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.Properties.ReadOnly = False Then
            Try
                Me.SLUPOID.EditValue = ""

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                Dim cmsl As SqlDataAdapter

                If Jenis = "Stock" Then
                    cmsl = New SqlDataAdapter("Select Distinct POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn,MtUang,DiscP,DiscRpSat,Ket, TglScK, stsOdJasa, stsNonPT, Hari From (Select *,DATEDIFF(dd,TglScK,GetDate()) as Hari From (Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn, stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket,Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='" & Jenis & "' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and Gol='" & Gol & "' or POID In (Select POID From T_TrmBB where TrmID ='" & Me.TBKode.EditValue & "')) as x Union All Select *,DATEDIFF(dd,TglScK,GetDate()) as Hari From (Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn, MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket,Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='" & Jenis & "' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='True' and Gol='" & Gol & "' and stsOdJasa='True') as x Union All Select *,DATEDIFF(dd,TglScK,GetDate()) as Hari From (Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn, MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket,Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='" & Jenis & "' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='True' and Gol='" & Gol & "' and Kat='Import' and DATEDIFF(mm,Tanggal,GetDate())<=12) as x) as y Order By POID", koneksi)

                ElseIf Jenis = "Non Stock" Then
                    cmsl = New SqlDataAdapter("Select Distinct * From(Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket, Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='" & Jenis & "' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and Gol='" & Gol & "' or POID In (Select POID From T_TrmBB where TrmID ='" & Me.TBKode.EditValue & "') Union All Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket, Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='" & Jenis & "' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and Gol='" & Gol & "' or POID In (Select POID From T_TrmBB where TrmID ='" & Me.TBKode.EditValue & "') and stsOdJasa='True' Union All Select POID,Kat,Tipe,DueDate,TipePPn,PersenPPn,stsQC,stsPPn, MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat,Ket,Case When TglScK is null Then TglKirim Else TglScK End as TglScK,stsOdJasa,stsNonPT From T_POBB Where Jenis='" & Jenis & "' and POID Not In (Select Distinct POID From T_POBBDtl where HarSat=0 and Free=0) and SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='True' and Gol='" & Gol & "' and Kat='Import' and DATEDIFF(mm,Tanggal,GetDate())<=12) as x Order By POID", koneksi)
                End If
                cmsl.SelectCommand.CommandTimeout = 90000
                cmsl.TableMappings.Add("Table", "T_POBBTrm")

                Try
                    DsMaster.Tables("T_POBBTrm").Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "T_POBBTrm")

                Me.SLUPOID.Properties.DataSource = DsMaster.Tables("T_POBBTrm")
                Me.SLUPOID.Properties.DisplayMember = "POID"
                Me.SLUPOID.Properties.ValueMember = "POID"

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub SLUPOID_Leave(sender As Object, e As EventArgs) Handles SLUPOID.Leave

        If Not IsDBNull(Me.SLUPOID.EditValue) And Me.SLUPOID.Properties.ReadOnly = False Then
            Try

                'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                If Manual = False Then
                    If Jenis = "Stock" And DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("Kat") = "Lokal" Then
                        If DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("Hari") < -7 Then
                            XtraMessageBox.Show("Barang Baru Bisa Diterima Dari Tanggal " & Format(DateAdd(DateInterval.Day, -7, DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("TglScK")), "dd MMMM yyyy") & " Ke atas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            Me.SLUPOID.EditValue = ""

                            Exit Sub
                        End If
                    End If
                End If


                Me.RBPPn.EditValue = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("TipePPn")
                Me.TBPersenPPn.EditValue = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("PersenPPn")
                stsPPn = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("stsPPn")
                MtUang = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("MtUang")
                DueDate = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("DueDate")
                TglScK = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("TglScK")
                Kat = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("Kat")
                Me.TBTamDiscP.EditValue = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("DiscP")
                Me.TBDiscRpSat.EditValue = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("DiscRpSat")
                Me.MKet.EditValue = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("Ket")
                stsOdJasa = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("stsOdJasa")
                stsNonPT = DsMaster.Tables("T_POBBTrm").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("stsNonPT")
                CekCurr()

                If Manual = True Then
                    Dim cmsl2 As SqlDataAdapter

                    cmsl2 = New SqlDataAdapter("Select CurrID,Awal,Akhir,NilTukarRp From M_Curr where MtUang='" & MtUang & "' and MtUang<>'IDR' and Awal>=DATEADD(Month, -2, '" & Me.DTPTanggal.EditValue & "') Union All Select CurrID,Awal,Akhir,NilTukarRp From M_Curr where MtUang='" & MtUang & "' and MtUang='IDR'", koneksi)
                    cmsl2.TableMappings.Add("Table", "M_CurrLUE")
                    cmsl2.Fill(DsMaster, "M_CurrLUE")
                    DsMaster.Tables("M_CurrLUE").Clear()
                    cmsl2.Fill(DsMaster, "M_CurrLUE")

                    Me.SLUCurr.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
                    Me.SLUCurr.Properties.DisplayMember = "NilTukarRp"
                    Me.SLUCurr.Properties.ValueMember = "CurrID"

                    Me.SLUCurr.EditValue = CurrID
                End If

                Dim cmsl As SqlDataAdapter
                If stsOdJasa = True Or Kat = "Import" Then
                    'MsgBox("1")
                    cmsl = New SqlDataAdapter("Select *,Round(DiscRpSat*Qty,4) As DiscRp,Qty*HarSat As HarSbDisc,((Qty*HarSat)*DiscP)/100 As RpDiscP,(Qty*HarSat)-(((Qty*HarSat)*DiscP)/100)-(Qty*DiscRpSat) as HarAkhir,0 As QtyPL, 0 As QtyAct, 0 As QtyRj From (Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as TrmIDD, POIDD,BOMID,'" & InisialBC & "'+D.BBID as BBID,B.Nama As Bahan,'' as BtNum,D.Sat,Qty-(Select Isnull((Select Sum(Qty) From T_TrmBBDtl Where POIDD=D.POIDD and TrmID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where POIDD=D.POIDD and RtrID<>'" & Me.TBKode.EditValue & "'),0)) As Qty,HarSat,DiscP,Round(DiscRp/Qty,4) As DiscRpSat From T_POBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Me.SLUPOID.EditValue & "' and ((Harsat<>0 and Free='False') or (Harsat=0 and Free='True'))) As x", koneksi)
                Else
                    'MsgBox("2")
                    cmsl = New SqlDataAdapter("Select *,Round(DiscRpSat*Qty,4) As DiscRp,Qty*HarSat As HarSbDisc,((Qty*HarSat)*DiscP)/100 As RpDiscP,(Qty*HarSat)-(((Qty*HarSat)*DiscP)/100)-(Qty*DiscRpSat) as HarAkhir,0 As QtyPL, 0 As QtyAct, 0 As QtyRj From (Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as TrmIDD, POIDD,BOMID,'" & InisialBC & "'+D.BBID as BBID,B.Nama As Bahan,'' as BtNum,D.Sat,Qty-(Select Isnull((Select Sum(Qty) From T_TrmBBDtl Where POIDD=D.POIDD and TrmID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where POIDD=D.POIDD and RtrID<>'" & Me.TBKode.EditValue & "'),0)) As Qty,HarSat,DiscP,Round(DiscRp/Qty,4) As DiscRpSat From T_POBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Me.SLUPOID.EditValue & "' and ((Harsat<>0 and Free='False') or (Harsat=0 and Free='True'))) As x Where Qty>0", koneksi)
                End If

                cmsl.TableMappings.Add("Table", "T_TrmBBDtl")
                cmsl.Fill(DsMaster, "T_TrmBBDtl")
                DsMaster.Tables("T_TrmBBDtl").Clear()
                cmsl.Fill(DsMaster, "T_TrmBBDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_TrmBBDtl"

                'AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Manual = True Then
            CekInsBC()
        End If
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        If Me.GridView1.OptionsBehavior.Editable = True Then
            Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscRp.EditValue = Math.Round(Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBDiscRpSat.EditValue, 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            HitPPn()
        End If
    End Sub

    Private Sub TBDiscRpSat_EditValueChanged(sender As Object, e As EventArgs) Handles TBDiscRpSat.EditValueChanged
        If Me.GridView1.RowCount > 0 Then
            Me.TBTamDiscRp.EditValue = Math.Round(Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBDiscRpSat.EditValue, 2, MidpointRounding.AwayFromZero)
        End If
        Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        HitPPn()
    End Sub

    Private Sub TBTamDiscP_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscP.EditValueChanged
        Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        HitPPn()
    End Sub


    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging

        StokSbEd = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty")
        BtNumLama = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum")
    End Sub

    'Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
    '    koneksi.Close()

    '    If Indicator <> 100 Then
    '        If Manual = False Then
    '            If Me.GridView1.RowCount - 1 > 0 Then
    '                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID")) Then
    '                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") <> "" Then
    '                        Dim Stok2, Stok3, Stok As Decimal

    '                        Dim commandx As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)

    '                        With koneksi
    '                            .Open()
    '                            Stok2 = commandx.ExecuteScalar()
    '                            .Close()
    '                        End With

    '                        Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmIDD") & ")", koneksi)

    '                        With koneksi
    '                            .Open()
    '                            Stok3 = command3.ExecuteScalar()
    '                            .Close()
    '                        End With

    '                        If Stok2 > Stok3 Then
    '                            Stok = Stok3
    '                        Else
    '                            Stok = Stok2
    '                        End If


    '                        If Stok + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <= 0 And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <> 0 Then
    '                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '                            Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = False
    '                            Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = False
    '                        Else
    '                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '                            Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
    '                            Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = True
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBSJ.KeyPress, TBPENID.KeyPress, TBJnsDok.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
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
End Class