Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class FRtrTagihanBB
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
        Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=37", koneksi)

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
        Me.BVBNewNon.Enabled = CType(TcodeCollection.Item("RInvBNN"), Boolean)
        Me.BVBNewLPB.Enabled = CType(TcodeCollection.Item("RInvBNS"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("RInvBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("RInvBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTRtrTagihBB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUTagihan.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.TBPEN.Properties.ReadOnly = True
        Me.DTPTglPEN.Properties.ReadOnly = True
        Me.TBBL.Properties.ReadOnly = True
        Me.DTPTglBL.Properties.ReadOnly = True
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
        Me.BVTRtrTagihBB_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUTagihan.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.TBPEN.Properties.ReadOnly = False
        Me.DTPTglPEN.Properties.ReadOnly = False
        Me.TBBL.Properties.ReadOnly = False
        Me.DTPTglBL.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True
        Me.GridView4.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.GridView4.ActiveFilter.Clear()

        Me.BVTRtrTagihBB_e.Selected = True
    End Sub

    Public Sub LUE(Jenis As String)
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select TagihID,SuppID,MtUang,TipePPn,PersenPPn,DiscP,RpDiscP,DiscRpSat,DiscRp From T_Tagihan Where stsLunas='False' and Jenis='" & Jenis & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihanRtr")
        Try
            DsMaster.Tables("T_TagihanRtr").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TagihanRtr")

        Me.SLUTagihan.Properties.DataSource = DsMaster.Tables("T_TagihanRtr")
        Me.SLUTagihan.Properties.DisplayMember = "TagihID"
        Me.SLUTagihan.Properties.ValueMember = "TagihID"

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,JT,K.Nama As Kota From M_Supp S  Inner Join M_Kota K On S.KotaID=K.KotaID", koneksi)
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

    Public Sub FillNLDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrTagihIDD,RtrTagihID,TagihIDD,Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir,CIF,BM From T_RtrTagihNLDtl Where RtrTagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagihNLDtl")
        Try
            DsMaster.Tables("T_RtrTagihNLDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagihNLDtl")

        DsMaster.Tables("T_RtrTagihNLDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrTagihNLDtl").Columns("Nama")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RtrTagihNLDtl"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrTagihIDD,RtrTagihID,RtrID,Tanggal,GdId,DiscP,DiscRp From T_RtrTagihDtl Where RtrTagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagihDtl")
        Try
            DsMaster.Tables("T_RtrTagihDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagihDtl")

        DsMaster.Tables("T_RtrTagihDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrTagihDtl").Columns("RtrID")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_RtrTagihDtl"

        cmsl = New SqlDataAdapter("Select RtrTagihIDD,RtrTagihID,RtrTagihDtl,RtrID,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc, DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir,DiscRpSatH,DiscRpH,DiscPH,RpDiscPH,HarAkhirH,HarSatDPP,HarBahan,CIF,BM,NamaLain From T_RtrTagihDtl2 D Inner Join M_BB B On D.BBID=B.BBID Where RtrTagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagihDtl2")
        Try
            DsMaster.Tables("T_RtrTagihDtl2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagihDtl2")

        DsMaster.Tables("T_RtrTagihDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrTagihDtl2").Columns("RtrTagihID"), DsMaster.Tables("T_RtrTagihDtl2").Columns("RtrID"), DsMaster.Tables("T_RtrTagihDtl2").Columns("BBID")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_RtrTagihDtl2"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrTagihID,PeriodID,Jenis,Kat,Tanggal,TagihID,H.SuppID,S.Nama As Supplier,S.Alamat,K.Nama As Kota, CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,PEN,TglPEN,BL,TglBL,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRp,TotDisc,TotDPP,TotPPn,TotAkhir,TotBM,TotCIF,H.Ket,H.stsNonPT,stsPakai,H.InsDate, H.InsBy,H.UpdDate, H.UpdBy From T_RtrTagih H Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,RtrTagihID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagih")
        Try
            DsMaster.Tables("T_RtrTagih").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagih")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_RtrTagih"
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

        Me.TBTotAkhirRp.EditValue = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)

    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_RtrTagih")
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
        If IO.File.Exists("SrRBL.xml") Then
            System.IO.File.Delete("SrRBL.xml")
        End If
    End Sub

    Private Sub FRtrTagihBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Retur Tagihan Bahan Baku"
    End Sub

    Private Sub FRtrTagihBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FRtrTagihanBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FRtrTagihBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTRtrTagihBB_e.Selected = True
    End Sub

    Private Sub BVTRtrTagihBB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTRtrTagihBB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Retur Tagihan Bahan Baku"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("RInvBP"), Boolean)
        Me.BExExcel.Enabled = CType(TcodeCollection.Item("RInvExEc"), Boolean)
    End Sub

    Private Sub BVBNewNon_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNewNon.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Retur Tagihan Bahan Baku"

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

        OpenControl()
        LUE("Non LPB")
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUTagihan.EditValue = ""
        Me.CBOKat.EditValue = "Lokal"
        Me.SLUSupp.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""
        Jenis = "Non LPB"
        Me.CENonPT.EditValue = False

        XTPNonLPB.PageVisible = True
        XTPLPB.PageVisible = False

        FillNLDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_RtrTagihNLDtl").Clear()
        ReDim arrPar(-1)
        ReDim arrParS(-1)
        ReDim arrParS2(-1)

        CekCurr()
    End Sub

    Private Sub BVBNewLPB_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNewLPB.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Retur Tagihan Bahan Baku"

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
        LUE("LPB")
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If


        Me.SLUTagihan.EditValue = ""
        Me.CBOKat.EditValue = "Lokal"
        Me.SLUSupp.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""
        Jenis = "LPB"
        Me.CENonPT.EditValue = False

        XTPNonLPB.PageVisible = False
        XTPLPB.PageVisible = True


        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_RtrTagihDtl").Clear()
        DsMaster.Tables("T_RtrTagihDtl2").Clear()
        ReDim arrPar(-1)
        ReDim arrParS(-1)
        ReDim arrParS2(-1)
        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Retur Tagihan Bahan Baku"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlByrHut(Me.GridView2.GetFocusedDataRow.Item("RtrTagihID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Pembayaran", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE(Me.GridView2.GetFocusedDataRow.Item("Jenis"))

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("RtrTagihID")
        Jenis = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUTagihan.EditValue = Me.GridView2.GetFocusedDataRow.Item("TagihID")
        Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
        Me.TBPEN.EditValue = Me.GridView2.GetFocusedDataRow.Item("PEN")
        Me.DTPTglPEN.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglPEN")
        Me.TBBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("BL")
        Me.DTPTglBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglBL")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
        Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
        Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
        Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
        Me.TBTotDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDisc")
        Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
        Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
        Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")
        Me.CENonPT.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsNonPT")

        If Jenis = "LPB" Then
            FillDtl(Me.TBKode.EditValue)

        ElseIf Jenis = "Non LPB" Then
            FillNLDtl(Me.TBKode.EditValue)

        End If

        ReDim arrPar(-1)
        ReDim arrParS(-1)
        ReDim arrParS2(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        If Jenis = "LPB" Then
            Me.XTPLPB.PageVisible = True
            Me.XTPNonLPB.PageVisible = False
        Else
            Me.XTPLPB.PageVisible = False
            Me.XTPNonLPB.PageVisible = True
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Retur Tagihan Bahan Baku"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlByrHut(Me.GridView2.GetFocusedDataRow.Item("RtrTagihID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Pembayaran", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("RtrTagihID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_RtrTagih")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("RtrTagihID")
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

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()
        Me.GridView4.ActiveFilter.Clear()

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Jenis = "LPB" Then
            TotQty = Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)
            Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscP").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscPH").SummaryText, Decimal) + CType(Me.GridView4.Columns("DiscRpH").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)

        ElseIf Jenis = "Non LPB" Then
            TotQty = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
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
                Dim cmSP As New SqlCommand("SPInsT_RtrTagih")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 37
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TagihID", SqlDbType.VarChar).Value = Me.SLUTagihan.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PEN", SqlDbType.VarChar).Value = Me.TBPEN.EditValue
                    .Parameters.Add("@TglPEN", SqlDbType.Date).Value = Me.DTPTglPEN.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
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
                    .Parameters.Add("@stsNonPT", SqlDbType.Bit).Value = Me.CENonPT.EditValue
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
                            'Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        If Jenis = "LPB" Then
                            Dim i : For i = 0 To GridView3.RowCount - 1
                                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "RtrID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_RtrTagihDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "RtrID")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                        .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
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
                                End If

                                Dim z : For z = 0 To GridView4.RowCount - 1
                                    If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                        'If Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(z, "RtrTagihDtl") Then
                                        If Me.GridView3.GetRowCellValue(i, "RtrID") = Me.GridView4.GetRowCellValue(z, "RtrID") Then

                                            Dim cmSPDtl As New SqlCommand("SPInsT_RtrTagihDtl2")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                                .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                .Parameters.Add("@RtrTagihDtl", SqlDbType.Int).Value = IdD
                                                .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "RtrID")
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
                                                cmSPDtl.ExecuteNonQuery()
                                                x = cmSPDtl.Parameters("@Return").Value
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
                            Next


                        ElseIf Jenis = "Non LPB" Then

                            Dim i : For i = 0 To GridView1.RowCount - 1
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_RtrTagihNLDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@TagihIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihIDD")
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
                Dim cmSP As New SqlCommand("SPUpT_RtrTagih")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 37
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TagihID", SqlDbType.VarChar).Value = Me.SLUTagihan.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PEN", SqlDbType.VarChar).Value = Me.TBPEN.EditValue
                    .Parameters.Add("@TglPEN", SqlDbType.Date).Value = Me.DTPTglPEN.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
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
                    .Parameters.Add("@stsNonPT", SqlDbType.Bit).Value = Me.CENonPT.EditValue
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

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        If Jenis = "LPB" Then

                            Dim y : For y = 0 To arrParS.GetUpperBound(0)
                                Dim cmSPDel As New SqlCommand("SPDelT_RtrTagihDtl")
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
                                Dim cmSPDel As New SqlCommand("SPDelT_RtrTagihDtl2")
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

                            Dim i : For i = 0 To GridView3.RowCount - 1
                                If Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "RtrID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_RtrTagihDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "RtrID")
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                            .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
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
                                            Me.GridView3.SetRowCellValue(i, "RtrTagihIDD", Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") * -1)
                                        ElseIf x = -1 Then
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If

                                        Dim z : For z = 0 To GridView4.RowCount - 1
                                            If Me.GridView4.GetRowCellValue(i, "RtrTagihIDD") < 0 Then
                                                If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                                    'If Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(z, "RtrTagihDtl") * -1 Then
                                                    If Me.GridView3.GetRowCellValue(i, "RtrID") = Me.GridView4.GetRowCellValue(z, "RtrID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPInsT_RtrTagihDtl2")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                                            .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                            .Parameters.Add("@RtrTagihDtl", SqlDbType.Int).Value = IdD
                                                            .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "RtrID")
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
                                                            .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
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
                                                            Me.GridView4.SetRowCellValue(z, "RtrTagihIDD", Me.GridView4.GetRowCellValue(z, "RtrTagihIDD") * -1)
                                                        Else
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If

                                                    End If
                                                End If

                                            Else
                                                If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                                    'If Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(z, "RtrTagihDtl") Then
                                                    If Me.GridView3.GetRowCellValue(i, "RtrID") = Me.GridView4.GetRowCellValue(z, "RtrID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPUpT_RtrTagihDtl2")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "RtrTagihIDD")
                                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                                            .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                            .Parameters.Add("@RtrTagihDtl", SqlDbType.Int).Value = IdD
                                                            .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "RtrID")
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
                                                            .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                            .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                            .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
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
                                        Dim cmSPDtl As New SqlCommand("SPUpT_RtrTagihDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "RtrTagihIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "RtrID")
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                            .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
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
                                        If Me.GridView4.GetRowCellValue(i, "RtrTagihIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                                'If Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(z, "RtrTagihDtl") Then
                                                If Me.GridView3.GetRowCellValue(i, "RtrID") = Me.GridView4.GetRowCellValue(z, "RtrID") Then

                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_RtrTagihDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                                        .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                        .Parameters.Add("@RtrTagihDtl", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") 'IdD
                                                        .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "RtrID")
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
                                                        .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                        .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                        .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
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
                                                        Me.GridView4.SetRowCellValue(z, "RtrTagihIDD", Me.GridView4.GetRowCellValue(z, "RtrTagihIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If

                                                End If
                                            End If

                                        Else
                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(z, "BBID")) Then
                                                'If Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(z, "RtrTagihDtl") Then
                                                If Me.GridView3.GetRowCellValue(i, "RtrID") = Me.GridView4.GetRowCellValue(z, "RtrID") Then

                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_RtrTagihDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "RtrTagihIDD")
                                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 7
                                                        .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                        .Parameters.Add("@RtrTagihDtl", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(i, "RtrTagihIDD") 'IdD
                                                        .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "RtrID")
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
                                                        .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(z, "NamaLain")
                                                        .Parameters.Add("@CIF", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "CIF")
                                                        .Parameters.Add("@BM", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(z, "BM")
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
                            Next

                        ElseIf Jenis = "Non LPB" Then

                            Dim y : For y = 0 To arrPar.GetUpperBound(0)
                                Dim cmSPDel As New SqlCommand("SPDelT_RtrTagihNLDtl")
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
                                If Me.GridView1.GetRowCellValue(i, "RtrTagihIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_RtrTagihNLDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@TagihIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihIDD")
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
                                            Me.GridView1.SetRowCellValue(i, "RtrTagihIDD", Me.GridView1.GetRowCellValue(i, "RtrTagihIDD") * -1)
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If

                                Else
                                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_RtrTagihNLDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "RtrTagihIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@TagihIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihIDD")
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

        Dim cmd2 As New SqlCommand("SPAftSRtrTagih")
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

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub


    Private Sub SLUTagihan_Leave(sender As Object, e As EventArgs) Handles SLUTagihan.Leave
        Try
            If Not IsDBNull(Me.SLUTagihan.EditValue) And Me.SLUTagihan.Properties.ReadOnly = False Then
                Me.SLUSupp.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("SuppID")
                Me.SLUMtUang.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("MtUang")
                Me.RBPPn.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("TipePPn")
                Me.TBPersenPPn.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("PersenPPn")
                Me.TBTamDiscP.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("DiscP")
                Me.TBTamDiscPRp.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("RpDiscP")
                Me.TBTamDiscRp.EditValue = DsMaster.Tables("T_TagihanRtr").Select("TagihID = '" & Me.SLUTagihan.EditValue & "'")(0).Item("DiscRp")

                If Jenis = "Non LPB" Then
                    DsMaster.Tables("T_RtrTagihNLDtl").Clear()

                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY Nama)*-1 As RtrTagihIDD,'" & Me.TBKode.EditValue & "' as RtrTagihID,TagihIDD,Nama,Sat,Qty,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir,CIF,BM From T_TagihanNLDtl Where TagihID= '" & Me.SLUTagihan.EditValue & "'", koneksi)

                    cmsl.TableMappings.Add("Table", "T_RtrTagihNLDtl")
                    cmsl.Fill(DsMaster, "T_RtrTagihNLDtl")
                End If

                Me.SLUSupp.Properties.ReadOnly = True
                Me.SLUMtUang.Properties.ReadOnly = True
                Me.RBPPn.Properties.ReadOnly = True
                Me.TBPersenPPn.Properties.ReadOnly = True
                Me.TBTamDiscP.Properties.ReadOnly = True
                Me.TBTamDiscPRp.Properties.ReadOnly = True
                Me.TBTamDiscRp.Properties.ReadOnly = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        If Jenis = "Non LPB" Then
            If Me.GridView1.OptionsBehavior.Editable = True Then
                TotQty = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
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
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView4.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscP").SummaryText, Decimal) + CType(Me.GridView4.Columns("RpDiscPH").SummaryText, Decimal) + CType(Me.GridView4.Columns("DiscRpH").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("RtrTagihIDD")
        End If

    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrParS(arrParS.GetUpperBound(0) + 1)
            arrParS(arrParS.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("RtrTagihIDD")

            Dim i : For i = Me.GridView4.RowCount - 1 To 0 Step -1
                If Me.GridView4.GetRowCellValue(i, "RtrID") = Me.GridView3.GetFocusedDataRow.Item("RtrID") Then
                    Me.GridView4.DeleteRow(i)
                End If
            Next
        End If
    End Sub

    Private Sub BEdRtrID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdRtrID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdRtrID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdRtrID.ButtonClick
        Try

            Dim frm As New FSearch("Retur BB", Me.SLUSupp.EditValue, Me.RBPPn.EditValue, Me.CENonPT.EditValue, Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then

                If Me.GridView4.RowCount > 0 Then
                    Dim i : For i = Me.GridView4.RowCount - 1 To 0 Step -1
                        'If Me.GridView4.GetRowCellValue(i, "RtrTagihDtl") = Me.GridView3.GetFocusedDataRow.Item("RtrTagihIDD") Then
                        If Me.GridView4.GetRowCellValue(i, "RtrID") = Me.GridView3.GetFocusedDataRow.Item("RtrID") Then
                            ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                            arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView4.GetRowCellValue(i, "RtrTagihIDD")

                            Me.GridView4.DeleteRow(i)
                        End If
                    Next
                End If

                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Tanggal", dataTrans.Item("Tgl").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "GdId", dataTrans.Item("GdId").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "DiscP", CDec(dataTrans.Item("DiscP").ToString))
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "DiscRp", CDec(dataTrans.Item("DiscRp").ToString))
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "DiscRpSat", CDec(dataTrans.Item("DiscRpSat").ToString))

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select *,Round(HarAkhir-RpDiscPH-DiscRpH,4) As HarAkhirH,''as NamaLain From (Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 As RtrTagihIDD,'" & Me.TBKode.EditValue & "' as RtrTagihID," & Me.GridView3.GetFocusedDataRow.Item("RtrTagihIDD") & " As RtrTagihDtl,RtrID,D.BBID,B.Nama As Bahan,Sum(Qty) as Qty,D.Sat,HarSat,Sum(HarSbDisc) as HarSbDisc,Sum(DiscRpSat) as DiscRpSat,Sum(DiscRp) as DiscRp,DiscP,Sum(RpDiscP) as RpDiscP,Sum(HarAkhir) as HarAkhir," & CDec(dataTrans.Item("DiscRpSat").ToString) & " as DiscRpSatH,Sum(Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & "*Qty,4)) As DiscRpH," & CDec(dataTrans.Item("DiscP").ToString) & " As DiscPH,Sum(Round((HarAkhir-Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & "*Qty,4))*" & CDec(dataTrans.Item("DiscP").ToString) & "/100,4)) As RpDiscPH,HarSatDPP,HarBahan,0 As CIF,0 AS BM From T_RtrBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where RtrID='" & dataTrans.Item("Kode").ToString & "' Group By RtrID,D.BBID,B.Nama,D.Sat,HarSat,DiscP,HarSatDPP,HarBahan) As x", koneksi)
                'cmsl = New SqlDataAdapter("Select *,Round(HarAkhir-RpDiscPH-DiscRpH,4) As HarAkhirH From (Select ROW_NUMBER() over (ORDER BY B.BBID)*-1 As RtrTagihIDD,'" & Me.TBKode.EditValue & "' as RtrTagihID," & Me.GridView3.GetFocusedDataRow.Item("RtrTagihIDD") & " As RtrTagihDtl,RtrIDD,RtrID,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir," & CDec(dataTrans.Item("DiscRpSat").ToString) & " as DiscRpSatH,Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & "*Qty,4) As DiscRpH," & CDec(dataTrans.Item("DiscP").ToString) & " As DiscPH,Round((HarAkhir-Round(" & CDec(dataTrans.Item("DiscRpSat").ToString) & "*Qty,4))*" & CDec(dataTrans.Item("DiscP").ToString) & "/100,4) As RpDiscPH,HarSatDPP,HarBahan,0 As CIF,0 AS BM From T_RtrBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where RtrID='" & dataTrans.Item("Kode").ToString & "') As x", koneksi)

                cmsl.TableMappings.Add("Table", "T_RtrTagihDtl2")
                cmsl.Fill(DsMaster, "T_RtrTagihDtl2")

                Me.GridView4.ActiveFilterString = "[RtrID] = '" & Me.GridView3.GetFocusedRowCellValue("RtrID") & "'"

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

            'HitPPnDtl(e.RowHandle)

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

            Me.GridView1.SetRowCellValue(e.RowHandle, "RtrTagihIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TagihIDD", "")
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
            Me.GridView1.SetRowCellValue(e.RowHandle, "NamaLain", "")

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridView3_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try
            RemoveHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

            Me.GridView3.SetRowCellValue(e.RowHandle, "RtrTagihIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "RtrID", "")
            Me.GridView3.SetRowCellValue(e.RowHandle, "GdId", "")
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
                'If Me.GridView3.GetRowCellValue(e.RowHandle, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(i, "RtrTagihDtl") Then
                If Me.GridView3.GetRowCellValue(e.RowHandle, "RtrID") = Me.GridView4.GetRowCellValue(i, "RtrID") Then

                    Me.GridView4.SetRowCellValue(i, "DiscPH", Me.GridView3.GetRowCellValue(e.RowHandle, "DiscP"))

                    Me.GridView4.SetRowCellValue(i, "RpDiscPH", Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscP") * (Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))) / 100, 6))

                    Me.GridView4.SetRowCellValue(i, "HarAkhirH", Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "RpDiscPH") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))
                End If
            Next

        ElseIf e.Column Is GridView3.Columns("DiscRp") Then
            Dim i : For i = 0 To Me.GridView4.RowCount - 1
                'If Me.GridView3.GetRowCellValue(e.RowHandle, "RtrTagihIDD") = Me.GridView4.GetRowCellValue(i, "RtrTagihDtl") Then
                If Me.GridView3.GetRowCellValue(e.RowHandle, "RtrID") = Me.GridView4.GetRowCellValue(i, "RtrID") Then

                    Me.GridView4.SetRowCellValue(i, "DiscRpSatH", Math.Round(Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscRp") / Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)), 6), 6))

                    Me.GridView4.SetRowCellValue(i, "DiscRpH", Math.Round(Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscRp") / Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)), 6) * Me.GridView4.GetRowCellValue(i, "Qty"), 6))

                    Me.GridView4.SetRowCellValue(i, "RpDiscPH", Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscP") * (Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))) / 100, 6))

                    Me.GridView4.SetRowCellValue(i, "HarAkhirH", Me.GridView4.GetRowCellValue(i, "HarAkhir") - Me.GridView4.GetRowCellValue(i, "RpDiscPH") - Me.GridView4.GetRowCellValue(i, "DiscRpH"))
                End If
            Next

            'Me.GridView3.SetRowCellValue(e.RowHandle, "DiscRpSatH", Math.Round(Math.Round((Me.GridView3.GetRowCellValue(e.RowHandle, "DiscRp") / Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 2)),6)))
        End If

    End Sub

    Private Sub GridView3_RowCountChanged(sender As Object, e As EventArgs) Handles GridView3.RowCountChanged
        If Me.GridView3.RowCount > 0 Then
            Me.GridView4.ActiveFilterString = "[RtrID] = '" & Me.GridView3.GetFocusedRowCellValue("RtrID") & "'"
        End If
    End Sub

    Private Sub GridView3_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If Me.GridView3.RowCount > 0 Then
            If Me.GridView4.RowCount > 0 Then
                Me.GridView4.ActiveFilterString = "[RtrID] = '" & Me.GridView3.GetFocusedRowCellValue("RtrID") & "'"
            End If
        End If
    End Sub
    Private Sub GridView4_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView4.CellValueChanged

        If e.Column Is GridView4.Columns("Qty") Then
            Me.GridView4.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "DiscRpH", Math.Round((Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpSatH"), 6)), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscPH", Math.Round(Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6)), 6) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscPH"), 3) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhirH", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscPH"), 6))

        ElseIf e.Column Is GridView4.Columns("HarSat") Then

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "DiscRpH", Math.Round((Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpSatH"), 6)), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscPH", Math.Round(Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6)), 6) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscPH"), 3) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhirH", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscPH"), 6))

        ElseIf e.Column Is GridView4.Columns("DiscRp") Then
            If Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRp") = 0 Then
                Me.GridView4.Columns("DiscP").OptionsColumn.AllowEdit = True
                Me.GridView4.Columns("DiscRp").OptionsColumn.AllowEdit = True
            Else
                Me.GridView4.Columns("DiscP").OptionsColumn.AllowEdit = False
            End If

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "DiscRpH", Math.Round((Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpSatH"), 6)), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscPH", Math.Round(Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6)), 6) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscPH"), 3) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhirH", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscPH"), 6))

        ElseIf e.Column Is GridView4.Columns("DiscP") Then
            If Me.GridView4.GetRowCellValue(e.RowHandle, "DiscP") = 0 Then
                Me.GridView4.Columns("DiscP").OptionsColumn.AllowEdit = True
                Me.GridView4.Columns("DiscRp").OptionsColumn.AllowEdit = True
            Else
                Me.GridView4.Columns("DiscRp").OptionsColumn.AllowEdit = False
            End If

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarSbDisc") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRp"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "DiscRpH", Math.Round((Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpSatH"), 6)), 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "RpDiscPH", Math.Round(Math.Round((Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6)), 6) * Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscPH"), 3) / 100, 6))

            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhirH", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "HarAkhir") - Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "DiscRpH"), 6) - Me.GridView4.GetRowCellValue(e.RowHandle, "RpDiscPH"), 6))

        End If

    End Sub

    Private Sub GridControl3_Leave(sender As Object, e As EventArgs) Handles GridControl3.Leave
        Me.GridView4.Focus()
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FRtrTagihanBB_d(Me.GridView2.GetFocusedDataRow.Item("Jenis"), Me.GridView2.GetFocusedDataRow.Item("RtrTagihID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUMtUang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SLUMtUang.KeyPress
        CekCurr()
    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Retur Tagihan Bahan Baku " & periodeBulan & " " & periodeTahun & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPEN.KeyPress, TBBL.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class