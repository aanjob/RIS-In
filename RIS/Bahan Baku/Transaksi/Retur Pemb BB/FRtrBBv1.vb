Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FRtrBBv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn, KdLama, CurrID As String
    Dim Manual, MnlInsUpd, stsQC, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim Gol As String
    Dim InisialBC As String = ""

    Public Sub New(BBSpM As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = BBSpM
        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,MnlInsUpd From M_DocCode Where DocID=7 and CabID='" & Gol & "'", koneksi)

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

        Me.Text = "Retur Pembelian " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("RBBN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTRtrBB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.TBSJ.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUPOID.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

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
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTRtrBB_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.TBSJ.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUPOID.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTRtrBB_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SuppID,Nama From M_Supp Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppL")
        cmsl.Fill(DsMaster, "M_SuppL")
        DsMaster.Tables("M_SuppL").Clear()
        cmsl.Fill(DsMaster, "M_SuppL")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppL")
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

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrIDD,RtrID,TrmIDD,TrmID,POIDD,BOMID,D.BBID,B.Nama as Bahan,D.Sat,Qty,HarSat,HarSbDisc,DiscRpSat,DiscRp, DiscP,RpDiscP,HarAkhir,HarSatDPP,HarBahan From T_RtrBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where RtrID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_RtrBBDtl")
        Try
            DsMaster.Tables("T_RtrBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrBBDtl")

        DsMaster.Tables("T_RtrBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrBBDtl").Columns("TrmIDD"), DsMaster.Tables("T_RtrBBDtl").Columns("POIDD"), DsMaster.Tables("T_RtrBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RtrBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrID,PeriodID,CodeID,Tanggal,Jenis,H.Gol,Kat,H.SuppID,S.Nama as Supplier,S.Alamat,K.Nama As Kota,POID, H.GdID,G.Nama as Gudang,SJ,CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRpSat,DiscRp,TotDisc,TotDPP, TotPPn,TotAkhir,H.Ket,stsPPn,stsNotaRtr,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_RtrBB H Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,RtrID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrBB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_RtrBB" & Gol)
        DsMaster.Tables("T_RtrBB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_RtrBB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_RtrBB" & Gol
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
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / 1.1, 2, MidpointRounding.AwayFromZero)
            'Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * 10 / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue / 1.1 * 10 / 100, 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * 10 / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue + Me.TBTotPPn.EditValue
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_RtrBB")
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

    Public Sub DelXml()
        If IO.File.Exists("SrBBNBOM" & Gol & ".xml") Then
            System.IO.File.Delete("SrBBNBOM" & Gol & ".xml")
        End If
    End Sub

    Private Sub FRtrBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Retur Pembelian " & Gol
    End Sub

    Private Sub FRtrBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FRtrBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTRtrBB_e.Selected = True

        If Manual = True Then
            Me.GridColumn3.Visible = False
        Else
            Me.GridColumn3.Visible = True
        End If
    End Sub

    Private Sub BVTRtrBB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTRtrBB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Retur Pembelian " & Gol
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("RBBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("RBBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("RBBP"), Boolean)
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("RBBPH"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Retur Pembelian " & Gol

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

        Me.SLUSupp.EditValue = ""
        Me.SLUPOID.EditValue = ""
        Me.CBOJenis.EditValue = "With LPB"
        Me.CBOKat.EditValue = "Lokal"
        Me.SLUGd.EditValue = ""
        Me.TBSJ.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBDiscRpSat.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"

        If Me.CBOJenis.EditValue = "With LPB" Then
            Me.RBPPn.Properties.ReadOnly = True
            Me.SLUMtUang.Properties.ReadOnly = True

            Me.SLUPOID.Properties.ReadOnly = False

            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = False
            Me.GridColumn4.OptionsColumn.AllowEdit = False

            Me.GridColumn8.Visible = False
            Me.GridColumn4.OptionsColumn.AllowEdit = False

        ElseIf Me.CBOJenis.EditValue = "Non LPB" Then
            Me.RBPPn.Properties.ReadOnly = False
            Me.SLUMtUang.Properties.ReadOnly = False

            Me.SLUPOID.Properties.ReadOnly = True

            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = True
            Me.GridColumn4.OptionsColumn.AllowEdit = True

            Me.GridColumn8.Visible = True
            Me.GridColumn4.OptionsColumn.AllowEdit = True
        End If

        CekCurr()

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_RtrBBDtl").Clear()

        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Retur Pembelian " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlRtrTagih(Me.GridView2.GetFocusedDataRow.Item("RtrID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditagihkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("RtrID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("RtrID")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POID,DueDate,TipePPn,stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat From T_POBB Where SuppID='" & Me.SLUSupp.EditValue & "' and stsKirim='False' and stsBatal='False' or POID In (Select POID From T_RtrBB where RtrID ='" & Me.TBKode.EditValue & "')", koneksi)
        cmsl.TableMappings.Add("Table", "T_POBBRtr" & Gol)
        cmsl.Fill(DsMaster, "T_POBBRtr" & Gol)
        DsMaster.Tables("T_POBBRtr" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_POBBRtr" & Gol)

        Me.SLUPOID.Properties.DataSource = DsMaster.Tables("T_POBBRtr" & Gol)
        Me.SLUPOID.Properties.DisplayMember = "POID"
        Me.SLUPOID.Properties.ValueMember = "POID"

        Me.SLUPOID.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.TBSJ.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJ")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        stsPPn = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
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

        RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        FillDtl(Me.TBKode.EditValue)

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        If Me.CBOJenis.EditValue = "With LPB" Then
            Me.RBPPn.Properties.ReadOnly = True
            Me.SLUMtUang.Properties.ReadOnly = True

            Me.SLUPOID.Properties.ReadOnly = False

            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = False
            Me.GridColumn4.OptionsColumn.AllowEdit = False

            Me.GridColumn8.Visible = False
            Me.GridColumn8.OptionsColumn.AllowEdit = False

        ElseIf Me.CBOJenis.EditValue = "Non LPB" Then
            Me.RBPPn.Properties.ReadOnly = False
            Me.SLUMtUang.Properties.ReadOnly = False

            Me.SLUPOID.Properties.ReadOnly = True

            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = True
            Me.GridColumn4.OptionsColumn.AllowEdit = True

            Me.GridColumn8.Visible = True
            Me.GridColumn8.OptionsColumn.AllowEdit = True
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("RtrID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supplier"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJ"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "POID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRRtrPembBBv1
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBPrintH_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintH.ItemClick
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("RtrID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supplier"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJ"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "POID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRRtrPembBBHv1
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Retur Pembelian " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlRtrTagih(Me.GridView2.GetFocusedDataRow.Item("RtrID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditagihkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Manual = False Then
            Dim Hapus As Boolean = True

            FillDtl(Me.GridView2.GetFocusedDataRow.Item("RtrID"))

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                Dim Stok2 As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.GridView2.GetFocusedDataRow.Item("GdID") & "','" & Me.GridView2.GetFocusedDataRow.Item("RtrID") & "'," & Me.GridView1.GetRowCellValue(i, "RtrIDD") & ")", koneksi)

                With koneksi
                    .Open()
                    Stok2 = command.ExecuteScalar()
                    .Close()
                End With

                If Stok2 < 0 Then
                    Hapus = False

                    Exit For
                Else
                    Hapus = True
                End If
            Next

            If Hapus = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("RtrID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_RtrBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("RtrID")
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
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

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

        Dim command As New SqlCommand("Select CodeID From M_DocCode Where DocID=7 and Gol='" & JnsPPn & "' and CabID='" & Gol & "'", koneksi)

        With koneksi
            .Open()
            CodeID = command.ExecuteScalar()
            .Close()
        End With

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_RtrBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_RtrBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmID")
                                    .Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                                    .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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
                Dim cmSP As New SqlCommand("SPUpT_RtrBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_RtrBBDtl")
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "RtrIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_RtrBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmID")
                                        .Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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
                                        Me.GridView1.SetRowCellValue(i, "RtrIDD", Me.GridView1.GetRowCellValue(i, "RtrIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_RtrBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "RtrIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                        .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmID")
                                        .Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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

    Private Sub BEdBBID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Try
            Dim frm As New FSearch("BB No BOM", Me.SLUSupp.EditValue, Gol, "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 0)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))

                AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("RtrIDD")

        End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "RtrIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "POIDD", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TrmIDD", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TrmID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRpSat", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        Dim Stok, Stok1, Stok2 As Decimal
        Dim command, command2 As New SqlCommand

        koneksi.Close()

        If e.Column Is GridView1.Columns("Qty") Then

            If Me.CBOJenis.EditValue = "With LPB" Then
                command = New SqlCommand("Select Case When (Select Qty -(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where TrmIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "TrmIDD") & " and RtrID<>'" & Me.TBKode.EditValue & "'),0)) From T_TrmBBDtl Where TrmID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "TrmID") & "' And TrmIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "TrmIDD") & ") > dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else (Select Qty -(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where TrmIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "TrmIDD") & " and RtrID<>'" & Me.TBKode.EditValue & "'),0)) From T_TrmBBDtl Where TrmID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "TrmID") & "' And TrmIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "TrmIDD") & ") End As Qty ", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok1 Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Penerimaan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok1)
                End If

            Else
                command = New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With
            End If

            command2 = New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "RtrIDD") & ")", koneksi)

            With koneksi
                .Open()
                Stok2 = command2.ExecuteScalar()
                .Close()
            End With

            If Stok1 > Stok2 Then
                Stok = Stok2
            Else
                Stok = Stok1
            End If

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP")) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

        ElseIf e.Column Is GridView1.Columns("HarSat") Then

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP")) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))
        End If

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        koneksi.Close()

        If Manual = False Then
            If Me.GridView1.RowCount - 1 > 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID")) Then
                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") <> "" Then
                        Dim Stok2 As Decimal

                        Dim commandx As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "RtrIDD") & ")", koneksi)

                        With koneksi
                            .Open()
                            Stok2 = commandx.ExecuteScalar()
                            .Close()
                        End With

                        If Stok2 + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <= 0 And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") > 0 Then
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FRtrBB_dv1(Me.GridView2.GetFocusedDataRow.Item("RtrID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave
        If Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.Properties.ReadOnly = False Then
            Try
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select POID,DueDate,TipePPn,stsQC,stsPPn,MtUang,DiscP,Round(DiscRp/TotQty,4) As DiscRpSat From T_POBB Where SuppID='" & Me.SLUSupp.EditValue & "' and POID In (Select POID From T_TrmBB where Year(Tanggal) >= " & MainModule.periodeTahun - 5 & ") and Gol='" & Gol & "'", koneksi)
                cmsl.TableMappings.Add("Table", "T_POBBRtr" & Gol)
                cmsl.Fill(DsMaster, "T_POBBRtr" & Gol)
                DsMaster.Tables("T_POBBRtr" & Gol).Clear()
                cmsl.Fill(DsMaster, "T_POBBRtr" & Gol)

                Me.SLUPOID.Properties.DataSource = DsMaster.Tables("T_POBBRtr" & Gol)
                Me.SLUPOID.Properties.DisplayMember = "POID"
                Me.SLUPOID.Properties.ValueMember = "POID"

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub SLUPOID_Leave(sender As Object, e As EventArgs) Handles SLUPOID.Leave
        If Not IsDBNull(Me.SLUPOID.EditValue) And Me.SLUPOID.Properties.ReadOnly = False Then
            Try

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                Me.RBPPn.EditValue = DsMaster.Tables("T_POBBRtr" & Gol).Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("TipePPn")
                stsPPn = DsMaster.Tables("T_POBBRtr" & Gol).Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("stsPPn")
                Me.SLUMtUang.EditValue = DsMaster.Tables("T_POBBRtr" & Gol).Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("MtUang")
                Me.TBTamDiscP.EditValue = DsMaster.Tables("T_POBBRtr" & Gol).Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("DiscP")
                Me.TBDiscRpSat.EditValue = DsMaster.Tables("T_POBBRtr" & Gol).Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("DiscRpSat")

                CekCurr()

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select *,Round(DiscRpSat*Qty,4) As DiscRp,Qty*HarSat As HarSbDisc,((Qty*HarSat)*DiscP)/100 As RpDiscP,(Qty*HarSat)-(((Qty*HarSat)*DiscP)/100)-(Qty*DiscRpSat) as HarAkhir From (Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as RtrIDD, H.TrmID,TrmIDD,POIDD,BOMID,'" & InisialBC & "'+D.BBID As BBID,B.Nama As Bahan,D.Sat,0 As Qty,HarSat,D.DiscP,Round(D.DiscRp/Qty,4) As DiscRpSat From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Me.SLUPOID.EditValue & "') As x Order By TrmID,Bahan asc", koneksi)
                'cmsl = New SqlDataAdapter("Select *,Round(DiscRpSat*Qty,4) As DiscRp,Qty*HarSat As HarSbDisc,((Qty*HarSat)*DiscP)/100 As RpDiscP,(Qty*HarSat)-(((Qty*HarSat)*DiscP)/100)-(Qty*DiscRpSat) as HarAkhir From (Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as RtrIDD, H.TrmID,TrmIDD,POIDD,BOMID,D.BBID,B.Nama As Bahan,D.Sat,0 As Qty,HarSat,D.DiscP,Round(D.DiscRp/Qty,4) As DiscRpSat From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Inner Join M_BB B On D.BBID=B.BBID Where stsTagih='False' and POID='" & Me.SLUPOID.EditValue & "') As x Order By TrmID,Bahan asc", koneksi)


                cmsl.TableMappings.Add("Table", "T_RtrBBDtl")
                cmsl.Fill(DsMaster, "T_RtrBBDtl")
                DsMaster.Tables("T_RtrBBDtl").Clear()
                cmsl.Fill(DsMaster, "T_RtrBBDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_RtrBBDtl"

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        If Me.GridView1.OptionsBehavior.Editable = True Then
            Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscRp.EditValue = Math.Round(Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBDiscRpSat.EditValue, 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            'Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue
            HitPPn()

        End If
    End Sub

    Private Sub TBDiscRpSat_EditValueChanged(sender As Object, e As EventArgs) Handles TBDiscRpSat.EditValueChanged
        If Me.GridView1.RowCount > 0 Then
            Me.TBTamDiscRp.EditValue = Math.Round(Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBDiscRpSat.EditValue, 2, MidpointRounding.AwayFromZero)
        End If
        Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        'Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue
        HitPPn()
    End Sub

    Private Sub RBPPn_EditValueChanged(sender As Object, e As EventArgs) Handles RBPPn.EditValueChanged
        HitPPn()
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        koneksi.Close()

        CekCurr()

        'If Manual = False Then
        '    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
        '        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")
        '        Me.GridView1.DeleteRow(i)
        '    Next
        'End If
        Dim Stok As Decimal

        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            If Me.CBOJenis.EditValue = "With LPB" Then
                Dim command As New SqlCommand("Select Case When (Select Qty -(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & " and RtrID<>'" & Me.TBKode.EditValue & "'),0)) From T_TrmBBDtl Where TrmID='" & Me.GridView1.GetRowCellValue(i, "TrmID") & "' And TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & ") > dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else (Select Qty -(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & " and RtrID<>'" & Me.TBKode.EditValue & "'),0)) From T_TrmBBDtl Where TrmID='" & Me.GridView1.GetRowCellValue(i, "TrmID") & "' And TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & ") End As Qty ", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Penerimaan", "Peringatan",
MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If

            Else
                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If
            End If
        Next

    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            If Manual = True Then
                CekInsBC()
            End If

            koneksi.Close()
            Dim Stok As Decimal

            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.CBOJenis.EditValue = "With LPB" Then
                    Dim command As New SqlCommand("Select Case When (Select Qty -(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & " and RtrID<>'" & Me.TBKode.EditValue & "'),0)) From T_TrmBBDtl Where TrmID='" & Me.GridView1.GetRowCellValue(i, "TrmID") & "' And TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & ") > dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else (Select Qty -(Select Isnull((Select Sum(Qty) From T_RtrBBDtl Where TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & " and RtrID<>'" & Me.TBKode.EditValue & "'),0)) From T_TrmBBDtl Where TrmID='" & Me.GridView1.GetRowCellValue(i, "TrmID") & "' And TrmIDD=" & Me.GridView1.GetRowCellValue(i, "TrmIDD") & ") End As Qty ", koneksi)

                    With koneksi
                        .Open()
                        Stok = command.ExecuteScalar()
                        .Close()
                    End With

                    If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                        XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Penerimaan", "Peringatan",
    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                    End If

                Else
                    Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

                    With koneksi
                        .Open()
                        Stok = command.ExecuteScalar()
                        .Close()
                    End With

                    If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                        XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub CBOJenis_Leave(sender As Object, e As EventArgs) Handles CBOJenis.Leave
        If Me.CBOJenis.Properties.ReadOnly = False Then
            If Me.CBOJenis.EditValue = "With LPB" Then
                Me.RBPPn.Properties.ReadOnly = True
                Me.SLUMtUang.Properties.ReadOnly = True

                Me.SLUPOID.Properties.ReadOnly = False

                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = False
                Me.GridColumn4.OptionsColumn.AllowEdit = False

                Me.GridColumn8.Visible = False
                Me.GridColumn8.OptionsColumn.AllowEdit = False

            ElseIf Me.CBOJenis.EditValue = "Non LPB" Then
                Me.RBPPn.Properties.ReadOnly = False
                Me.SLUMtUang.Properties.ReadOnly = False

                Me.SLUPOID.Properties.ReadOnly = True
                Me.SLUPOID.EditValue = ""

                Me.SLUMtUang.EditValue = "IDR"
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = True
                Me.GridColumn4.OptionsColumn.AllowEdit = True

                Me.GridColumn8.Visible = True
                Me.GridColumn8.OptionsColumn.AllowEdit = True
            End If

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")
                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub FRtrBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBSJ.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class