Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class FTagihanBBv2
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, CurrID, JnsPPn, Jenis As String
    Dim Manual, MnlInsUpd, stsPPn As Boolean
    Dim rw As Integer = 0
    Dim JT, IdD As Integer
    Dim TotQty As Decimal
    Dim arrPar(-1) As String
    Dim arrParS(-1) As String
    Dim arrParS2(-1) As String

#Region "Export Excel"

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Apakah Anda Mau Membuka File Ini?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ExportTo(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView2.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String, ByVal Nama As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Nama
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

#End Region

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=36", koneksi)

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
        Me.BVBNewNon.Enabled = CType(TcodeCollection.Item("InvBNN"), Boolean)
        Me.BVBNewLPB.Enabled = CType(TcodeCollection.Item("InvBNS"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("InvBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("InvBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTagihan_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPTglFt.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPEN.Properties.ReadOnly = True
        Me.DTPTglPEN.Properties.ReadOnly = True
        Me.TBBL.Properties.ReadOnly = True
        Me.DTPTglBL.Properties.ReadOnly = True
        Me.TBLC.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.TBTamDiscP.Properties.ReadOnly = True
        Me.TBTamDiscRp.Properties.ReadOnly = True
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False
        Me.GridView4.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNewNon.Enabled = False
        Me.BVBNewLPB.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTagihan_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPTglFt.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.TBPEN.Properties.ReadOnly = False
        Me.DTPTglPEN.Properties.ReadOnly = False
        Me.TBBL.Properties.ReadOnly = False
        Me.DTPTglBL.Properties.ReadOnly = False
        Me.TBLC.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True
        Me.GridView4.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.GridView4.ActiveFilter.Clear()

        Me.BVTTagihan_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,JT,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE2")
        Try
            DsMaster.Tables("M_SuppLUE2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SuppLUE2")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE2")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        Try
            DsMaster.Tables("M_CurrLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"
    End Sub

    Public Sub CekCurr()
        Dim cmSl As SqlCommand

        If Manual = True Then
            cmSl = New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTglFt.EditValue & "') AND (Akhir >= '" & Me.DTPTglFt.EditValue & "') AND (MtUang = '" & Me.SLUMtUang.EditValue & "')ORDER BY Tanggal DESC")
        Else
            cmSl = New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTanggal.EditValue & "') AND (Akhir >= '" & Me.DTPTanggal.EditValue & "') AND (MtUang = '" & Me.SLUMtUang.EditValue & "')ORDER BY Tanggal DESC")
        End If

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

    Public Sub FillNLDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,SJ,Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir,CIF,BM From T_TagihanNLDtl Where TagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TagihanNLDtl")
        Try
            DsMaster.Tables("T_TagihanNLDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TagihanNLDtl")

        DsMaster.Tables("T_TagihanNLDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanNLDtl").Columns("Nama")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TagihanNLDtl"
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_TagihanDtl").Clear()
            DsMaster.Tables("T_TagihanDtl2").Clear()
        Catch ex As Exception

        End Try

        'If Me.CBOKat.EditValue = "Barang Jadi" Then
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct TagihIDD,TagihID,D.TrmID,D.Tanggal,D.GdID,D.DiscP,D.DiscRp From T_TagihanDtl D Inner Join T_TrmBJDtl T On D.TrmID=T.TrmID Where TagihID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihanDtl")
        cmsl.Fill(DsMaster, "T_TagihanDtl")

        DsMaster.Tables("T_TagihanDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl").Columns("TrmID")}

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "T_TagihanDtl"

        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,TagihDtl,TrmID,D.Urut,D.BBID,B.ArtName As Bahan,Qty,D.Sat,B.Isi,HarSat,HarSbDisc, DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir,DiscRpSatH,DiscRpH,DiscPH,RpDiscPH,HarAkhirH,HarSatDPP,HarBahan,CIF,BM,NamaLain From T_TagihanDtl2 D Inner Join M_Brg B On D.BBID=B.ArtCode Where TagihID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihanDtl2")
        cmsl.Fill(DsMaster, "T_TagihanDtl2")

        DsMaster.Tables("T_TagihanDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl2").Columns("TrmID"), DsMaster.Tables("T_TagihanDtl2").Columns("BBID"), DsMaster.Tables("T_TagihanDtl2").Columns("HarSatDPP"), DsMaster.Tables("T_TagihanDtl2").Columns("HarBahan")}

        Me.GridControl6.DataSource = DsMaster
        Me.GridControl6.DataMember = "T_TagihanDtl2"

        'Else
        'Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,D.TrmID,T.POID,D.Tanggal,D.GdID,D.DiscP,D.DiscRp From T_TagihanDtl D Inner Join T_TrmBB T On D.TrmID=T.TrmID Where TagihID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihanDtl")
        cmsl.Fill(DsMaster, "T_TagihanDtl")

        DsMaster.Tables("T_TagihanDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl").Columns("TrmID")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_TagihanDtl"

        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,TagihDtl,TrmID,Urut,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc, DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir,DiscRpSatH,DiscRpH,DiscPH,RpDiscPH,HarAkhirH,HarSatDPP,HarBahan,CIF,BM,NamaLain From T_TagihanDtl2 D Inner Join M_BB B On D.BBID=B.BBID Where TagihID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihanDtl2")
        cmsl.Fill(DsMaster, "T_TagihanDtl2")

        DsMaster.Tables("T_TagihanDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl2").Columns("TrmID"), DsMaster.Tables("T_TagihanDtl2").Columns("BBID"), DsMaster.Tables("T_TagihanDtl2").Columns("HarSatDPP"), DsMaster.Tables("T_TagihanDtl2").Columns("HarBahan")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_TagihanDtl2"
        'End If

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihID,PeriodID,Jenis,Kat,Tanggal,TanggalFt,H.SuppID,S.Nama As Supplier,S.Alamat,K.Nama As Kota, CurrID,MtUang,NilTukarRp,TipePPn,PEN,TglPEN,BL,TglBL,LC,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRp,TotDisc,TotDPP,TotPPn,TotAkhir,TotAkhirRp,TotBM,TotCIF,SisaBayar,H.Ket,stsLunas, H.InsDate, H.InsBy,H.UpdDate, H.UpdBy From T_Tagihan H Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,TagihID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_Tagihan")
        Try
            DsMaster.Tables("T_Tagihan").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_Tagihan")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_Tagihan"
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

        Me.TBTotAkhirRp.EditValue = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)

    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Tagihan")
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

    Public Sub DelXml()
        If IO.File.Exists("SrLPB.xml") Then
            System.IO.File.Delete("SrLPB.xml")
        End If

        If IO.File.Exists("SrLPBBJ.xml") Then
            System.IO.File.Delete("SrLPBBJ.xml")
        End If

    End Sub

    Private Sub FTagihanBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Tagihan Bahan Baku"
    End Sub

    Private Sub FTagihanBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FTagihanBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTagihan_e.Selected = True
    End Sub

    Private Sub BVTTagihan_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTagihan_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Tagihan Bahan Baku"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("InvBP"), Boolean)
        Me.BExExcel.Enabled = CType(TcodeCollection.Item("InvExEc"), Boolean)
    End Sub

    Private Sub BVBNewNon_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNewNon.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Tagihan Bahan Baku"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglFt.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglFt.EditValue = Date.Now
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

        Me.SLUSupp.EditValue = ""
        Me.CBOKat.EditValue = "Lokal"
        Me.SLUMtUang.EditValue = "IDR"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPEN.EditValue = ""
        Me.DTPTglPEN.EditValue = Date.Now
        Me.TBBL.EditValue = ""
        Me.DTPTglBL.EditValue = Date.Now
        Me.TBLC.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0.0
        Me.TBTamDiscP.EditValue = 0.0
        Me.TBTamDiscPRp.EditValue = 0.0
        Me.TBTotAkhir.EditValue = 0.0
        Me.TBTotAkhirRp.EditValue = 0.0
        Me.TBTotDPP.EditValue = 0.0
        Me.TBTotPPn.EditValue = 0.0
        Me.TBInfo.EditValue = ""
        Jenis = "Non LPB"

        If Jenis = "LPB" Then
            If Me.CBOKat.EditValue = "Barang Jadi" Then
                Me.XTPLPBBJ.PageVisible = True
                Me.XTPLPB.PageVisible = False

            Else
                Me.XTPLPBBJ.PageVisible = False
                Me.XTPLPB.PageVisible = True

            End If

            Me.XTPNonLPB.PageVisible = False

        ElseIf Jenis = "Non LPB" Then
            Me.XTPLPB.PageVisible = False
            Me.XTPLPBBJ.PageVisible = False
            Me.XTPNonLPB.PageVisible = True
        End If

        FillNLDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TagihanNLDtl").Clear()
        ReDim arrPar(-1)
        ReDim arrParS(-1)
        ReDim arrParS2(-1)
        CekCurr()
    End Sub

    Private Sub BVBNewStock_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNewLPB.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Tagihan Bahan Baku"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglFt.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglFt.EditValue = Date.Now
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
        Me.CBOKat.EditValue = "Lokal"
        Me.SLUMtUang.EditValue = "IDR"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPEN.EditValue = ""
        Me.DTPTglPEN.EditValue = Date.Now
        Me.TBBL.EditValue = ""
        Me.DTPTglBL.EditValue = Date.Now
        Me.TBLC.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0.0
        Me.TBTamDiscP.EditValue = 0.0
        Me.TBTamDiscPRp.EditValue = 0.0
        Me.TBTotAkhir.EditValue = 0.0
        Me.TBTotAkhirRp.EditValue = 0.0
        Me.TBTotDPP.EditValue = 0.0
        Me.TBTotPPn.EditValue = 0.0
        Me.TBInfo.EditValue = ""
        Jenis = "LPB"

        If Jenis = "LPB" Then
            If Me.CBOKat.EditValue = "Barang Jadi" Then
                Me.XTPLPBBJ.PageVisible = True
                Me.XTPLPB.PageVisible = False

            Else
                Me.XTPLPBBJ.PageVisible = False
                Me.XTPLPB.PageVisible = True

            End If

            Me.XTPNonLPB.PageVisible = False

        ElseIf Jenis = "Non LPB" Then
            Me.XTPLPB.PageVisible = False
            Me.XTPLPBBJ.PageVisible = False
            Me.XTPNonLPB.PageVisible = True
        End If

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TagihanDtl").Clear()
        DsMaster.Tables("T_TagihanDtl2").Clear()
        ReDim arrPar(-1)
        ReDim arrParS(-1)
        ReDim arrParS2(-1)
        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Tagihan Bahan Baku"

        DelXml()

        LUE()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlByrHut(Me.GridView2.GetFocusedDataRow.Item("TagihID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Pembayaran", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TagihID")
        Jenis = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglFt.EditValue = Me.GridView2.GetFocusedDataRow.Item("TanggalFT")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.TBPEN.EditValue = Me.GridView2.GetFocusedDataRow.Item("PEN")
        Me.DTPTglPEN.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglPEN")
        Me.TBBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("BL")
        Me.DTPTglBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglBL")
        Me.TBLC.EditValue = Me.GridView2.GetFocusedDataRow.Item("LC")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
        Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
        Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
        Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
        Me.TBTotDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDisc")
        Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
        Me.TBTotAkhirRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhirRp")
        Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
        Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

        If Jenis = "LPB" Then
            FillDtl(Me.TBKode.EditValue)
            If Me.CBOKat.EditValue = "Barang Jadi" Then
                Me.XTPLPBBJ.PageVisible = True
                Me.XTPLPB.PageVisible = False

            Else
                Me.XTPLPBBJ.PageVisible = False
                Me.XTPLPB.PageVisible = True

            End If

            Me.XTPNonLPB.PageVisible = False

        ElseIf Jenis = "Non LPB" Then
            FillNLDtl(Me.TBKode.EditValue)
            Me.XTPLPB.PageVisible = False
            Me.XTPLPBBJ.PageVisible = False
            Me.XTPNonLPB.PageVisible = True
        End If

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        If Me.GridView3.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView4.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Tagihan Bahan Baku"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlByrHut(Me.GridView2.GetFocusedDataRow.Item("TagihID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Pembayaran", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TagihID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_Tagihan")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TagihID")
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

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        'Dim frm As New FPilihUkuran

        'frm = New FPilihUkuran
        'frm.ShowDialog()
        'frm.Dispose()
        'frm.Close()

        'Dim bind As New Collection
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TagihID"), "Kode")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supplier"), "Supp")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("DueDate"), "DueDate")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        'bind.Add(MainModule.PilihUkuran, "Ukuran")

        'Dim XR As New XRPOBB
        'XR.InitializeData(bind)

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()
        Me.GridView4.ActiveFilter.Clear()
        Me.GridView5.ActiveFilter.Clear()
        Me.GridView6.ActiveFilter.Clear()


        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Jenis = "LPB" Then
            If Me.CBOKat.EditValue = "Barang Jadi" Then
                TotQty = Math.Round(CType(Me.GridView6.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView6.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDisc.EditValue = 0.0
                Me.TBTamDiscPRp.EditValue = 0.0

            Else
                TotQty = Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscP").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscPH").SummaryText, Decimal) + CType(Me.GridView4.Columns("DiscRpH").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            End If


        ElseIf Jenis = "Non LPB" Then
            TotQty = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        End If

        HitPPn()

        If Me.RBPPn.EditValue <> "Non PPn" Then
            stsPPn = True
            JnsPPn = "PPn"
        Else
            stsPPn = False
            JnsPPn = "Non PPn"
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_Tagihan")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 36
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglFT", SqlDbType.Date).Value = Me.DTPTglFt.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PEN", SqlDbType.VarChar).Value = Me.TBPEN.EditValue
                    .Parameters.Add("@TglPEN", SqlDbType.Date).Value = Me.DTPTglPEN.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
                    .Parameters.Add("@LC", SqlDbType.VarChar).Value = Me.TBLC.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = TotQty
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Me.TBTotSbDisc.EditValue
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Math.Round(Me.TBTamDiscRp.EditValue / TotQty, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Me.TBTotAkhirRp.EditValue
                    If Jenis = "LPB" Then
                        .Parameters.Add("@TotBM", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView4.Columns("BM").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                        .Parameters.Add("@TotCIF", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView4.Columns("CIF").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    ElseIf Jenis = "Non LPB" Then
                        .Parameters.Add("@TotBM", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("BM").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                        .Parameters.Add("@TotCIF", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("CIF").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    End If
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

                        If Jenis = "LPB" Then
                            If Me.CBOKat.EditValue = "Barang Jadi" Then
                                Dim i : For i = 0 To GridView5.RowCount - 1
                                    If Not IsDBNull(Me.GridView5.GetRowCellValue(i, "TrmID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_TagihanDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "TrmID")
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "DiscRp")
                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "DiscP")
                                            .Parameters.Add("@Return", SqlDbType.Int)
                                            .Parameters("@Return").Direction = ParameterDirection.Output
                                            .Parameters.Add("@IdD", SqlDbType.Int)
                                            .Parameters("@IdD").Direction = ParameterDirection.Output
                                            .Connection = koneksi
                                        End With

                                        With koneksi
                                            .Open()
                                            cmSPDtl.ExecuteNonQuery()
                                            x = cmSPDtl.Parameters("@Return").Value
                                            IdD = cmSPDtl.Parameters("@IdD").Value
                                            .Close()
                                        End With

                                        If x <> 0 Then
                                            Del()
                                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If

                                        Dim z : For z = 0 To GridView6.RowCount - 1
                                            If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "BBID")) Then
                                                'If Me.GridView5.GetRowCellValue(i, "TagihIDD") = Me.GridView6.GetRowCellValue(z, "TagihDtl") Then
                                                If Me.GridView5.GetRowCellValue(i, "TrmID") = Me.GridView6.GetRowCellValue(z, "TrmID") Then

                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihanDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                        .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                        '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "TrmIDD")
                                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TrmID")
                                                        .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Urut")
                                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "BBID")
                                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "Sat")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSat")
                                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSbDisc")
                                                        .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSat")
                                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRp")
                                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscP")
                                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscP")
                                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhir")
                                                        .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSatH")
                                                        .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpH")
                                                        .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscPH")
                                                        .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscPH")
                                                        .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhirH")
                                                        .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "CIF")
                                                        .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "BM")
                                                        .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NamaLain")
                                                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                        .Parameters.Add("@Return", SqlDbType.Int)
                                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                                        .Connection = koneksi
                                                    End With

                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl2.ExecuteNonQuery()
                                                        x = cmSPDtl2.Parameters("@Return").Value
                                                        .Close()
                                                    End With
                                                End If

                                                If x <> 0 Then
                                                    Del()
                                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        Next

                                    End If
                                Next

                            Else
                                Dim i : For i = 0 To GridView3.RowCount - 1
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "TrmID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_TagihanDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TrmID")
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(i, "DiscRp")
                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(i, "DiscP")
                                            .Parameters.Add("@Return", SqlDbType.Int)
                                            .Parameters("@Return").Direction = ParameterDirection.Output
                                            .Parameters.Add("@IdD", SqlDbType.Int)
                                            .Parameters("@IdD").Direction = ParameterDirection.Output
                                            .Connection = koneksi
                                        End With

                                        With koneksi
                                            .Open()
                                            cmSPDtl.ExecuteNonQuery()
                                            x = cmSPDtl.Parameters("@Return").Value
                                            IdD = cmSPDtl.Parameters("@IdD").Value
                                            .Close()
                                        End With

                                        If x <> 0 Then
                                            Del()
                                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If

                                        Dim z : For z = 0 To GridView4.RowCount - 1
                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(z, "BBID")) Then
                                                'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView4.GetRowCellValue(z, "TagihDtl") Then
                                                If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView4.GetRowCellValue(z, "TrmID") Then

                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihanDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                        .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                        '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "TrmIDD")
                                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TrmID")
                                                        .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "Urut")
                                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "BBID")
                                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "Sat")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSat")
                                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSbDisc")
                                                        .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSat")
                                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRp")
                                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscP")
                                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscP")
                                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhir")
                                                        .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSatH")
                                                        .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpH")
                                                        .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscPH")
                                                        .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscPH")
                                                        .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhirH")
                                                        .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                        .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
                                                        .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                        .Parameters.Add("@Return", SqlDbType.Int)
                                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                                        .Connection = koneksi
                                                    End With

                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl2.ExecuteNonQuery()
                                                        x = cmSPDtl2.Parameters("@Return").Value
                                                        .Close()
                                                    End With
                                                End If

                                                If x <> 0 Then
                                                    Del()
                                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        Next

                                    End If
                                Next
                            End If



                        ElseIf Jenis = "Non LPB" Then

                            Dim i : For i = 0 To GridView1.RowCount - 1
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TagihanNLDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJ")
                                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CIF")
                                        .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BM")
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
                        End If

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
                Dim cmSP As New SqlCommand("SPUpT_Tagihan")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 36
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglFT", SqlDbType.Date).Value = Me.DTPTglFt.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PEN", SqlDbType.VarChar).Value = Me.TBPEN.EditValue
                    .Parameters.Add("@TglPEN", SqlDbType.Date).Value = Me.DTPTglPEN.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
                    .Parameters.Add("@LC", SqlDbType.VarChar).Value = Me.TBLC.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = TotQty
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Me.TBTotSbDisc.EditValue
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Math.Round(Me.TBTamDiscRp.EditValue / TotQty, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Me.TBTotAkhirRp.EditValue
                    If Jenis = "LPB" Then
                        .Parameters.Add("@TotBM", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView4.Columns("BM").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                        .Parameters.Add("@TotCIF", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView4.Columns("CIF").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    ElseIf Jenis = "Non LPB" Then
                        .Parameters.Add("@TotBM", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("BM").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                        .Parameters.Add("@TotCIF", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("CIF").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    End If
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@PPn", SqlDbType.Bit).Value = stsPPn
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    '.Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    '.Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            'Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            .Close()
                        End With

                        If Jenis = "LPB" Then

                            Dim y : For y = 0 To arrParS.GetUpperBound(0)
                                Dim cmSPDel As New SqlCommand("SPDelT_TagihanDtl")
                                cmSPDel.CommandType = CommandType.StoredProcedure

                                With cmSPDel
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = arrParS(y)
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

                            Dim q : For q = 0 To arrParS2.GetUpperBound(0)
                                Dim cmSPDel As New SqlCommand("SPDelT_TagihanDtl2")
                                cmSPDel.CommandType = CommandType.StoredProcedure

                                With cmSPDel
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = arrParS2(q)
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

                            If Me.CBOKat.EditValue = "Barang Jadi" Then
                                Dim i : For i = 0 To GridView5.RowCount - 1
                                    If Me.GridView5.GetRowCellValue(i, "TagihIDD") < 0 Then
                                        If Not IsDBNull(Me.GridView5.GetRowCellValue(i, "TrmID")) Then
                                            Dim cmSPDtl As New SqlCommand("SPInsT_TagihanDtl")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "TrmID")
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "DiscRp")
                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "DiscP")
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Parameters.Add("@IdD", SqlDbType.Int)
                                                .Parameters("@IdD").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With

                                            With koneksi
                                                .Open()
                                                cmSPDtl.ExecuteNonQuery()
                                                x = cmSPDtl.Parameters("@Return").Value
                                                IdD = cmSPDtl.Parameters("@IdD").Value
                                                .Close()
                                            End With

                                            If x = 0 Then
                                                Me.GridView5.SetRowCellValue(i, "TagihIDD", Me.GridView5.GetRowCellValue(i, "TagihIDD") * -1)
                                            Else
                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                            Dim z : For z = 0 To GridView6.RowCount - 1
                                                If Me.GridView6.GetRowCellValue(z, "TagihIDD") < 0 Then
                                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "BBID")) Then
                                                        'If Me.GridView5.GetRowCellValue(i, "TagihIDD") = Me.GridView6.GetRowCellValue(z, "TagihDtl") * -1 Then
                                                        If Me.GridView5.GetRowCellValue(i, "TrmID") = Me.GridView6.GetRowCellValue(z, "TrmID") Then

                                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihanDtl2")
                                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                            With cmSPDtl2
                                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                                .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                                '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "TrmIDD")
                                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TrmID")
                                                                .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Urut")
                                                                .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "BBID")
                                                                .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "Sat")
                                                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                                                .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSat")
                                                                .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSbDisc")
                                                                .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSat")
                                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRp")
                                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscP")
                                                                .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscP")
                                                                .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhir")
                                                                .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSatH")
                                                                .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpH")
                                                                .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscPH")
                                                                .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscPH")
                                                                .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhirH")
                                                                .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "CIF")
                                                                .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "BM")
                                                                .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NamaLain")
                                                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                                .Connection = koneksi
                                                            End With

                                                            With koneksi
                                                                .Open()
                                                                cmSPDtl2.ExecuteNonQuery()
                                                                x = cmSPDtl2.Parameters("@Return").Value
                                                                .Close()
                                                            End With

                                                            If x = 0 Then
                                                                Me.GridView6.SetRowCellValue(i, "TagihIDD", Me.GridView6.GetRowCellValue(i, "TagihIDD") * -1)
                                                            Else
                                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                Exit Sub
                                                            End If

                                                        End If
                                                    End If

                                                Else
                                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "BBID")) Then
                                                        'If Me.GridView5.GetRowCellValue(i, "TagihIDD") = Me.GridView6.GetRowCellValue(z, "TagihDtl") Then
                                                        If Me.GridView5.GetRowCellValue(i, "TrmID") = Me.GridView6.GetRowCellValue(z, "TrmID") Then

                                                            Dim cmSPDtl2 As New SqlCommand("SPUpT_TagihanDtl2")
                                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                            With cmSPDtl2
                                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                                .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TagihIDD")
                                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                                .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                                '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "TrmIDD")
                                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TrmID")
                                                                .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Urut")
                                                                .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "BBID")
                                                                .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "Sat")
                                                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                                                .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSat")
                                                                .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSbDisc")
                                                                .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSat")
                                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRp")
                                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscP")
                                                                .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscP")
                                                                .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhir")
                                                                .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSatH")
                                                                .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpH")
                                                                .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscPH")
                                                                .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscPH")
                                                                .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhirH")
                                                                .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "CIF")
                                                                .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "BM")
                                                                .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NamaLain")
                                                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                                .Connection = koneksi
                                                            End With

                                                            With koneksi
                                                                .Open()
                                                                cmSPDtl2.ExecuteNonQuery()
                                                                x = cmSPDtl2.Parameters("@Return").Value
                                                                .Close()
                                                            End With

                                                            If x <> 0 Then
                                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    End If

                                                End If

                                            Next

                                        End If
                                    Else

                                        If Not IsDBNull(Me.GridView5.GetRowCellValue(i, "BBID")) Then
                                            Dim cmSPDtl As New SqlCommand("SPUpT_TagihanDtl")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "TagihIDD")
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "TrmID")
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "DiscRp")
                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "DiscP")
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

                                        Dim z : For z = 0 To GridView6.RowCount - 1
                                            If Me.GridView6.GetRowCellValue(z, "TagihIDD") < 0 Then
                                                If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "BBID")) Then
                                                    'If Me.GridView5.GetRowCellValue(i, "TagihIDD") = Me.GridView6.GetRowCellValue(z, "TagihDtl") Then
                                                    If Me.GridView5.GetRowCellValue(i, "TrmID") = Me.GridView6.GetRowCellValue(z, "TrmID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihanDtl2")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                            .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = Me.GridView5.GetRowCellValue(i, "TagihIDD") 'IdD
                                                            '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "TrmIDD")
                                                            .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TrmID")
                                                            .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Urut")
                                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "BBID")
                                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "Sat")
                                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSat")
                                                            .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSbDisc")
                                                            .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSat")
                                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRp")
                                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscP")
                                                            .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscP")
                                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhir")
                                                            .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSatH")
                                                            .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpH")
                                                            .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscPH")
                                                            .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscPH")
                                                            .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhirH")
                                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "CIF")
                                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "BM")
                                                            .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NamaLain")
                                                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                            .Parameters.Add("@Return", SqlDbType.Int)
                                                            .Parameters("@Return").Direction = ParameterDirection.Output
                                                            .Connection = koneksi
                                                        End With

                                                        With koneksi
                                                            .Open()
                                                            cmSPDtl2.ExecuteNonQuery()
                                                            x = cmSPDtl2.Parameters("@Return").Value
                                                            .Close()
                                                        End With

                                                        If x = 0 Then
                                                            Me.GridView6.SetRowCellValue(z, "TagihIDD", Me.GridView6.GetRowCellValue(z, "TagihIDD") * -1)
                                                        Else
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If
                                                    End If
                                                End If

                                            Else
                                                If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "BBID")) Then
                                                    'If Me.GridView5.GetRowCellValue(i, "TagihIDD") = Me.GridView6.GetRowCellValue(z, "TagihDtl") Then
                                                    If Me.GridView5.GetRowCellValue(i, "TrmID") = Me.GridView6.GetRowCellValue(z, "TrmID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPUpT_TagihanDtl2")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TagihIDD")
                                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "GdID")
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView5.GetRowCellValue(i, "Tanggal")
                                                            .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = Me.GridView5.GetRowCellValue(i, "TagihIDD") 'IdD
                                                            '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "TrmIDD")
                                                            .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "TrmID")
                                                            .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Urut")
                                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "BBID")
                                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "Sat")
                                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSat")
                                                            .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarSbDisc")
                                                            .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSat")
                                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRp")
                                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscP")
                                                            .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscP")
                                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhir")
                                                            .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpSatH")
                                                            .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscRpH")
                                                            .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "DiscPH")
                                                            .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "RpDiscPH")
                                                            .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "HarAkhirH")
                                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "CIF")
                                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView6.GetRowCellValue(z, "BM")
                                                            .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NamaLain")
                                                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                            .Parameters.Add("@Return", SqlDbType.Int)
                                                            .Parameters("@Return").Direction = ParameterDirection.Output
                                                            .Connection = koneksi
                                                        End With

                                                        With koneksi
                                                            .Open()
                                                            cmSPDtl2.ExecuteNonQuery()
                                                            x = cmSPDtl2.Parameters("@Return").Value
                                                            .Close()
                                                        End With

                                                        If x = 0 Then
                                                            Me.GridView1.SetRowCellValue(i, "RPBIDD", Me.GridView1.GetRowCellValue(i, "RPBIDD") * -1)
                                                        Else
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If
                                                    End If

                                                End If

                                            End If

                                        Next
                                    End If
                                Next

                            Else 'CBOKat
                                Dim i : For i = 0 To GridView3.RowCount - 1
                                    If Me.GridView3.GetRowCellValue(i, "TagihIDD") < 0 Then
                                        If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "TrmID")) Then
                                            Dim cmSPDtl As New SqlCommand("SPInsT_TagihanDtl")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TrmID")
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(i, "DiscRp")
                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(i, "DiscP")
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Parameters.Add("@IdD", SqlDbType.Int)
                                                .Parameters("@IdD").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With

                                            With koneksi
                                                .Open()
                                                cmSPDtl.ExecuteNonQuery()
                                                x = cmSPDtl.Parameters("@Return").Value
                                                IdD = cmSPDtl.Parameters("@IdD").Value
                                                .Close()
                                            End With

                                            If x = 0 Then
                                                Me.GridView3.SetRowCellValue(i, "TagihIDD", Me.GridView3.GetRowCellValue(i, "TagihIDD") * -1)
                                            Else
                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                            Dim z : For z = 0 To GridView4.RowCount - 1
                                                If Me.GridView4.GetRowCellValue(z, "TagihIDD") < 0 Then
                                                    If Not IsDBNull(Me.GridView4.GetRowCellValue(z, "BBID")) Then
                                                        'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView4.GetRowCellValue(z, "TagihDtl") * -1 Then
                                                        If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView4.GetRowCellValue(z, "TrmID") Then

                                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihanDtl2")
                                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                            With cmSPDtl2
                                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                                .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                                '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "TrmIDD")
                                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TrmID")
                                                                .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "Urut")
                                                                .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "BBID")
                                                                .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "Sat")
                                                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "Qty")
                                                                .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSat")
                                                                .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSbDisc")
                                                                .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSat")
                                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRp")
                                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscP")
                                                                .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscP")
                                                                .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhir")
                                                                .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSatH")
                                                                .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpH")
                                                                .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscPH")
                                                                .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscPH")
                                                                .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhirH")
                                                                .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                                .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
                                                                .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                                .Connection = koneksi
                                                            End With

                                                            With koneksi
                                                                .Open()
                                                                cmSPDtl2.ExecuteNonQuery()
                                                                x = cmSPDtl2.Parameters("@Return").Value
                                                                .Close()
                                                            End With

                                                            If x = 0 Then
                                                                Me.GridView4.SetRowCellValue(i, "TagihIDD", Me.GridView4.GetRowCellValue(i, "TagihIDD") * -1)
                                                            Else
                                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                Exit Sub
                                                            End If

                                                        End If
                                                    End If

                                                Else
                                                    If Not IsDBNull(Me.GridView4.GetRowCellValue(z, "BBID")) Then
                                                        'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView4.GetRowCellValue(z, "TagihDtl") Then
                                                        If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView4.GetRowCellValue(z, "TrmID") Then

                                                            Dim cmSPDtl2 As New SqlCommand("SPUpT_TagihanDtl2")
                                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                            With cmSPDtl2
                                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                                .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TagihIDD")
                                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                                .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                                '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "TrmIDD")
                                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TrmID")
                                                                .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "Urut")
                                                                .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "BBID")
                                                                .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "Sat")
                                                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "Qty")
                                                                .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSat")
                                                                .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSbDisc")
                                                                .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSat")
                                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRp")
                                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscP")
                                                                .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscP")
                                                                .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhir")
                                                                .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSatH")
                                                                .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpH")
                                                                .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscPH")
                                                                .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscPH")
                                                                .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhirH")
                                                                .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                                .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
                                                                .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                                .Connection = koneksi
                                                            End With

                                                            With koneksi
                                                                .Open()
                                                                cmSPDtl2.ExecuteNonQuery()
                                                                x = cmSPDtl2.Parameters("@Return").Value
                                                                .Close()
                                                            End With

                                                            If x <> 0 Then
                                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                                Exit Sub
                                                            End If
                                                        End If

                                                    End If

                                                End If

                                            Next

                                        End If
                                    Else

                                        If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "BBID")) Then
                                            Dim cmSPDtl As New SqlCommand("SPUpT_TagihanDtl")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TagihIDD")
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TrmID")
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(i, "DiscRp")
                                                .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(i, "DiscP")
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

                                        Dim z : For z = 0 To GridView4.RowCount - 1
                                            If Me.GridView4.GetRowCellValue(z, "TagihIDD") < 0 Then
                                                If Not IsDBNull(Me.GridView4.GetRowCellValue(z, "BBID")) Then
                                                    'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView4.GetRowCellValue(z, "TagihDtl") Then
                                                    If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView4.GetRowCellValue(z, "TrmID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihanDtl2")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                            .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(i, "TagihIDD") 'IdD
                                                            '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "TrmIDD")
                                                            .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TrmID")
                                                            .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "Urut")
                                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "BBID")
                                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "Sat")
                                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "Qty")
                                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSat")
                                                            .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSbDisc")
                                                            .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSat")
                                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRp")
                                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscP")
                                                            .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscP")
                                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhir")
                                                            .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSatH")
                                                            .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpH")
                                                            .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscPH")
                                                            .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscPH")
                                                            .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhirH")
                                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
                                                            .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                            .Parameters.Add("@Return", SqlDbType.Int)
                                                            .Parameters("@Return").Direction = ParameterDirection.Output
                                                            .Connection = koneksi
                                                        End With

                                                        With koneksi
                                                            .Open()
                                                            cmSPDtl2.ExecuteNonQuery()
                                                            x = cmSPDtl2.Parameters("@Return").Value
                                                            .Close()
                                                        End With

                                                        If x = 0 Then
                                                            Me.GridView4.SetRowCellValue(z, "TagihIDD", Me.GridView4.GetRowCellValue(z, "TagihIDD") * -1)
                                                        Else
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If
                                                    End If
                                                End If

                                            Else
                                                If Not IsDBNull(Me.GridView4.GetRowCellValue(z, "BBID")) Then
                                                    'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView4.GetRowCellValue(z, "TagihDtl") Then
                                                    If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView4.GetRowCellValue(z, "TrmID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPUpT_TagihanDtl2")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TagihIDD")
                                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdID")
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                            .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(i, "TagihIDD") 'IdD
                                                            '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "TrmIDD")
                                                            .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "TrmID")
                                                            .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(z, "Urut")
                                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "BBID")
                                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "Sat")
                                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "Qty")
                                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSat")
                                                            .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarSbDisc")
                                                            .Parameters.Add("@DiscRpSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSat")
                                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRp")
                                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscP")
                                                            .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscP")
                                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhir")
                                                            .Parameters.Add("@DiscRpSatH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpSatH")
                                                            .Parameters.Add("@DiscRpH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscRpH")
                                                            .Parameters.Add("@DiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "DiscPH")
                                                            .Parameters.Add("@RpDiscPH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "RpDiscPH")
                                                            .Parameters.Add("@HarAkhirH", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "HarAkhirH")
                                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
                                                            .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                            .Parameters.Add("@Return", SqlDbType.Int)
                                                            .Parameters("@Return").Direction = ParameterDirection.Output
                                                            .Connection = koneksi
                                                        End With

                                                        With koneksi
                                                            .Open()
                                                            cmSPDtl2.ExecuteNonQuery()
                                                            x = cmSPDtl2.Parameters("@Return").Value
                                                            .Close()
                                                        End With

                                                        If x = 0 Then
                                                            Me.GridView1.SetRowCellValue(i, "RPBIDD", Me.GridView1.GetRowCellValue(i, "RPBIDD") * -1)
                                                        Else
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If
                                                    End If

                                                End If

                                            End If

                                        Next
                                    End If
                                Next
                            End If



                        ElseIf Jenis = "Non LPB" Then

                            Dim y : For y = 0 To arrPar.GetUpperBound(0)
                                Dim cmSPDel As New SqlCommand("SPDelT_TagihanNLDtl")
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
                                If Me.GridView1.GetRowCellValue(i, "TagihIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_TagihanNLDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJ")
                                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                            .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CIF")
                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BM")
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
                                            Me.GridView1.SetRowCellValue(i, "TagihIDD", Me.GridView1.GetRowCellValue(i, "TagihIDD") * -1)
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If

                                Else
                                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_TagihanNLDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJ")
                                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                            .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                            .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                            .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CIF")
                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BM")
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
                                End If
                            Next
                        End If

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

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub DTPTglFt_Leave(sender As Object, e As EventArgs) Handles DTPTglFt.Leave
        CekCurr()
    End Sub

    Private Sub RBPPn_EditValueChanged(sender As Object, e As EventArgs) Handles RBPPn.EditValueChanged
        HitPPn()
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        If Jenis = "Non LPB" Then
            If Me.GridView1.OptionsBehavior.Editable = True Then
                TotQty = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
                HitPPn()
            End If
        End If

    End Sub

    Private Sub GridControl4_Leave(sender As Object, e As EventArgs) Handles GridControl4.Leave
        Me.GridView4.ActiveFilter.Clear()

        If Jenis = "LPB" Then
            If Me.GridView4.OptionsBehavior.Editable = True Then
                TotQty = Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("HarSbDisc").SummaryText, Decimal), 2)
                Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscP").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscPH").SummaryText, Decimal) + CType(Me.GridView4.Columns("DiscRpH").SummaryText, Decimal), 2)
                Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2)
                HitPPn()
            End If
        End If
    End Sub

    Private Sub GridControl6_Leave(sender As Object, e As EventArgs) Handles GridControl6.Leave
        If Jenis = "LPB" Then
            If Me.GridView6.OptionsBehavior.Editable = True Then
                TotQty = Math.Round(CType(Me.GridView6.Columns("Qty").SummaryText, Decimal), 2)
                Me.TBTotSbDisc.EditValue = 0
                Me.TBTotDisc.EditValue = 0
                Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2)
                HitPPn()
            End If
        End If

    End Sub

    Private Sub TBTamDiscRp_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscRp.EditValueChanged
        Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        HitPPn()
    End Sub

    Private Sub TBTamDiscP_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscP.EditValueChanged
        Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
        HitPPn()
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TagihIDD")
        End If

    End Sub
    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrParS(arrParS.GetUpperBound(0) + 1)
            arrParS(arrParS.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("TagihIDD")

            Dim i : For i = Me.GridView4.RowCount - 1 To 0 Step -1
                'If Me.GridView4.GetRowCellValue(i, "TagihDtl") = Me.GridView3.GetFocusedDataRow.Item("TagihIDD") Then
                If Me.GridView4.GetRowCellValue(i, "TrmID") = Me.GridView3.GetFocusedDataRow.Item("TrmID") Then
                    ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                    arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView4.GetFocusedDataRow.Item("TagihIDD")

                    Me.GridView4.DeleteRow(i)
                End If
            Next
        End If
    End Sub

    Private Sub CBOKat_Leave(sender As Object, e As EventArgs) Handles CBOKat.Leave
        If Jenis = "LPB" Then
            If Me.CBOKat.EditValue = "Barang Jadi" Then
                Me.XTPLPBBJ.PageVisible = True
                Me.XTPLPB.PageVisible = False

            Else
                Me.XTPLPBBJ.PageVisible = False
                Me.XTPLPB.PageVisible = True

            End If

            Me.XTPNonLPB.PageVisible = False

        ElseIf Jenis = "Non LPB" Then
            Me.XTPLPB.PageVisible = False
            Me.XTPLPBBJ.PageVisible = False
            Me.XTPNonLPB.PageVisible = True
        End If
    End Sub

    Private Sub BEdTrmID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdTrmID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdTrmID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdTrmID.ButtonClick
        Try

            Dim frm As New FSearch("LPB", Me.SLUSupp.EditValue, Me.RBPPn.EditValue, Me.SLUMtUang.EditValue, Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then

                If Me.GridView4.RowCount > 0 Then
                    Dim i : For i = Me.GridView4.RowCount - 1 To 0 Step -1
                        'If Me.GridView4.GetRowCellValue(i, "TagihDtl") = Me.GridView3.GetFocusedDataRow.Item("TagihIDD") Then
                        If Me.GridView4.GetRowCellValue(i, "TrmID") = Me.GridView3.GetFocusedDataRow.Item("TrmID") Then

                            ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                            arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView4.GetRowCellValue(i, "TagihIDD")

                            Me.GridView4.DeleteRow(i)
                        End If
                    Next
                End If

                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "POID", dataTrans.Item("POID").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Tanggal", dataTrans.Item("Tgl").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "GdID", dataTrans.Item("GdID").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "DiscP", CDec(dataTrans.Item("DiscP").ToString))
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "DiscRp", CDec(dataTrans.Item("DiscRp").ToString))
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "DiscRpSat", CDec(dataTrans.Item("DiscRpSat").ToString))

                Dim cmsl As SqlDataAdapter
                'cmsl = New SqlDataAdapter("Select *,Round(HarAkhir-RpDiscPH-DiscRpH,4) As HarAkhirH From (Select ROW_NUMBER() over (ORDER BY B.BBID)*-1 As TagihIDD,'" & Me.TBKode.EditValue & "' as TagihID," & Me.GridView3.GetFocusedDataRow.Item("TagihIDD") & " As TagihDtl,TrmIDD,TrmID,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir," & CDec(dataTrans.Item("DiscRpSat").ToString) & " as DiscRpSatH,Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & "*Qty,4) As DiscRpH," & CDec(dataTrans.Item("DiscP").ToString) & " As DiscPH,Round((HarAkhir-Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & "*Qty,4))*" & CDec(dataTrans.Item("DiscP").ToString) & "/100,4) As RpDiscPH,HarSatDPP,HarBahan,0 as CIF,0 as BM From T_TrmBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where TrmID='" & dataTrans.Item("Kode").ToString & "') As x", koneksi)
                cmsl = New SqlDataAdapter("Select *,0 As Urut,'' As NamaLain,Round(HarAkhir-RpDiscPH-DiscRpH,4) As HarAkhirH From (Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 As TagihIDD,'" & Me.TBKode.EditValue & "' as TagihID," & Me.GridView3.GetFocusedDataRow.Item("TagihIDD") & " As TagihDtl,TrmID,D.BBID,B.Nama As Bahan,Sum(Qty) As Qty,D.Sat,HarSat,Sum(HarSbDisc) As HarSbDisc,Sum(DiscRpSat) As DiscRpSat,Sum(DiscRp) As DiscRp,DiscP,Sum(RpDiscP) As RpDiscP,Sum(HarAkhir) As HarAkhir," & CDec(dataTrans.Item("DiscRpSat").ToString) & " as DiscRpSatH,Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & " * Sum(Qty),4) As DiscRpH," & CDec(dataTrans.Item("DiscP").ToString) & " As DiscPH,Round((Sum(HarAkhir)-Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & " * Sum(Qty),4))*" & CDec(dataTrans.Item("DiscP").ToString) & "/100,4) As RpDiscPH,HarSatDPP,HarBahan,0 as CIF,0 as BM From T_TrmBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where TrmID='" & dataTrans.Item("Kode").ToString & "' Group By D.BBID,B.Nama,TrmID,D.Sat,HarSat,DiscP,HarSatDPP,HarBahan) As x", koneksi)

                cmsl.TableMappings.Add("Table", "T_TagihanDtl2")
                cmsl.Fill(DsMaster, "T_TagihanDtl2")

                DsMaster.Tables("T_TagihanDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl2").Columns("TrmID"), DsMaster.Tables("T_TagihanDtl2").Columns("BBID"), DsMaster.Tables("T_TagihanDtl2").Columns("HarSatDPP"), DsMaster.Tables("T_TagihanDtl2").Columns("HarBahan")}

                Me.GridView4.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Qty") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

        ElseIf e.Column Is GridView1.Columns("HarSat") Then

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

        ElseIf e.Column Is GridView1.Columns("DiscRp") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") = 0 Then
                Me.GridView1.Columns("DiscP").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("DiscRp").OptionsColumn.AllowEdit = True
            Else
                Me.GridView1.Columns("DiscP").OptionsColumn.AllowEdit = False
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

        ElseIf e.Column Is GridView1.Columns("DiscP") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP") = 0 Then
                Me.GridView1.Columns("DiscP").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("DiscRp").OptionsColumn.AllowEdit = True
            Else
                Me.GridView1.Columns("DiscRp").OptionsColumn.AllowEdit = False
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))
        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "TagihIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SJ", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Nama", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CIF", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BM", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridView3_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try
            RemoveHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

            Me.GridView3.SetRowCellValue(e.RowHandle, "TagihIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "TrmID", "")
            Me.GridView3.SetRowCellValue(e.RowHandle, "GdID", "")
            Me.GridView3.SetRowCellValue(e.RowHandle, "DiscRp", 0)
            Me.GridView3.SetRowCellValue(e.RowHandle, "DiscP", 0)

            AddHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridView3_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView3.CellValueChanged
        Me.GridView4.RefreshData()

        If e.Column Is GridView3.Columns("DiscP") Then
            Dim i : For i = 0 To Me.GridView4.RowCount - 1
                'If Me.GridView3.GetRowCellValue(e.RowHandle, "TagihIDD") = Me.GridView4.GetRowCellValue(i, "TagihDtl") Then
                If Me.GridView3.GetRowCellValue(e.RowHandle, "TrmID") = Me.GridView4.GetRowCellValue(i, "TrmID") Then

                    Me.GridView4.SetRowCellValue(i, "DiscPH", Me.GridView3.GetRowCellValue(e.RowHandle, "DiscP"))

                    Me.GridView4.SetRowCellValue(i, "RpDiscPH", Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscP") * (Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))) / 100, 6))

                    Me.GridView4.SetRowCellValue(i, "HarAkhirH", Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "RpDiscPH") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))
                End If
            Next

        ElseIf e.Column Is GridView3.Columns("DiscRp") Then
            Dim i : For i = 0 To Me.GridView4.RowCount - 1
                'If Me.GridView3.GetRowCellValue(e.RowHandle, "TagihIDD") = Me.GridView4.GetRowCellValue(i, "TagihDtl") Then
                If Me.GridView3.GetRowCellValue(e.RowHandle, "TrmID") = Me.GridView4.GetRowCellValue(i, "TrmID") Then

                    Me.GridView4.SetRowCellValue(i, "DiscRpSatH", Math.Round(Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscRp") / Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)), 6), 6))

                    Me.GridView4.SetRowCellValue(i, "DiscRpH", Math.Round(Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscRp") / Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)), 6) * Me.GridView4.GetRowCellValue(i, "Qty"), 6))

                    Me.GridView4.SetRowCellValue(i, "RpDiscPH", Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscP") * (Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))) / 100, 6))

                    Me.GridView4.SetRowCellValue(i, "HarAkhirH", Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "RpDiscPH") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))
                End If
            Next

        End If

    End Sub

    Private Sub GridView3_RowCountChanged(sender As Object, e As EventArgs) Handles GridView3.RowCountChanged
        If Me.GridView3.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView4.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub GridView3_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If Me.GridView3.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView4.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub GridControl3_Leave(sender As Object, e As EventArgs) Handles GridControl3.Leave
        Me.GridView4.Focus()
    End Sub

    Private Sub BEdTrmBJID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdTrmBJID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdTrmBJID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdTrmBJID.ButtonClick
        Try

            Dim frm As New FSearch("LPB BJ", Me.SLUSupp.EditValue, Me.RBPPn.EditValue, Me.SLUMtUang.EditValue, Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then

                If Me.GridView6.RowCount > 0 Then
                    Dim i : For i = Me.GridView6.RowCount - 1 To 0 Step -1
                        If Me.GridView6.GetRowCellValue(i, "TrmID") = Me.GridView5.GetFocusedDataRow.Item("TrmID") Then

                            ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                            arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(i, "TagihIDD")

                            Me.GridView6.DeleteRow(i)
                        End If
                    Next
                End If

                Me.GridView5.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "POID", dataTrans.Item("POID").ToString)
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "Tanggal", dataTrans.Item("Tgl").ToString)
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "GdID", dataTrans.Item("GdID").ToString)
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "DiscP", 0)
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "DiscRp", 0)
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "DiscRpSat", 0)

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select *,0 As Urut,'' As NamaLain,Round(HarAkhir-RpDiscPH-DiscRpH,4) As HarAkhirH From (Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 As TagihIDD,'" & Me.TBKode.EditValue & "' as TagihID," & Me.GridView5.GetFocusedDataRow.Item("TagihIDD") & " As TagihDtl,TrmID,D.ArtCode As BBID,B.ArtName As Bahan,Sum(Qty) As Qty,D.SatID As Sat,D.Isi,HarSat,0 As HarSbDisc,0 As DiscRpSat,0 As DiscRp,0 As DiscP,0 As RpDiscP,Sum(HarAkhir) As HarAkhir,0 as DiscRpSatH,0 As DiscRpH,0 As DiscPH,0 As RpDiscPH,0 As HarSatDPP,0 As HarBahan,0 as CIF,0 as BM  From T_TrmBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where TrmID='" & dataTrans.Item("Kode").ToString & "' Group By D.ArtCode,B.ArtName,TrmID,D.SatID,D.Isi,HarSat) As x", koneksi)

                cmsl.TableMappings.Add("Table", "T_TagihanDtl2")
                cmsl.Fill(DsMaster, "T_TagihanDtl2")

                DsMaster.Tables("T_TagihanDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl2").Columns("TrmID"), DsMaster.Tables("T_TagihanDtl2").Columns("BBID"), DsMaster.Tables("T_TagihanDtl2").Columns("HarSatDPP"), DsMaster.Tables("T_TagihanDtl2").Columns("HarBahan")}

                Me.GridView6.ActiveFilterString = "[TrmID] = '" & dataTrans.Item("Kode").ToString & "'"

            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub GridControl5_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl5.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrParS(arrParS.GetUpperBound(0) + 1)
            arrParS(arrParS.GetUpperBound(0)) = Me.GridView5.GetFocusedDataRow.Item("TagihIDD")

            Dim i : For i = Me.GridView6.RowCount - 1 To 0 Step -1
                'If Me.GridView4.GetRowCellValue(i, "TagihDtl") = Me.GridView3.GetFocusedDataRow.Item("TagihIDD") Then
                If Me.GridView6.GetRowCellValue(i, "TrmID") = Me.GridView5.GetFocusedDataRow.Item("TrmID") Then
                    ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                    arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView6.GetFocusedDataRow.Item("TagihIDD")

                    Me.GridView6.DeleteRow(i)
                End If
            Next
        End If
    End Sub

    Private Sub GridView5_RowCountChanged(sender As Object, e As EventArgs) Handles GridView5.RowCountChanged
        If Me.GridView5.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView6.ActiveFilterString = "[TrmID] = '" & Me.GridView5.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub GridView5_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView5.FocusedRowChanged
        If Me.GridView5.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView6.ActiveFilterString = "[TrmID] = '" & Me.GridView5.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub GridControl5_Leave(sender As Object, e As EventArgs) Handles GridControl5.Leave
        Me.GridView6.Focus()
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTagihanBB_d(Me.GridView2.GetFocusedDataRow.Item("Jenis"), Me.GridView2.GetFocusedDataRow.Item("TagihID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave
        If Me.SLUSupp.EditValue <> "" And Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.Properties.ReadOnly = False Then
            DelXml()

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TagihIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                ReDim Preserve arrParS(arrParS.GetUpperBound(0) + 1)
                arrParS(arrParS.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("TagihIDD")

                Me.GridView3.DeleteRow(x)
            Next

            Dim y : For y = Me.GridView4.RowCount - 1 To 0 Step -1
                ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView4.GetFocusedDataRow.Item("TagihIDD")

                Me.GridView4.DeleteRow(y)
            Next
        End If


    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        If Me.SLUMtUang.EditValue <> "" And Not IsDBNull(Me.SLUMtUang.EditValue) And Me.SLUMtUang.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TagihIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                ReDim Preserve arrParS(arrParS.GetUpperBound(0) + 1)
                arrParS(arrParS.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("TagihIDD")

                Me.GridView3.DeleteRow(x)
            Next

            Dim y : For y = Me.GridView4.RowCount - 1 To 0 Step -1
                ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView4.GetFocusedDataRow.Item("TagihIDD")

                Me.GridView4.DeleteRow(y)
            Next

            CekCurr()
        End If


    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Tagihan Bahan Baku " & periodeBulan & " " & periodeTahun & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub FTagihanBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPEN.KeyPress, TBBL.KeyPress, TBLC.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class