Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class FTagihanJsBJ
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

    Public Sub New(Gol As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=36 and Gol='" & Gol & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("InvBNN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("InvBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("InvBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTagihan_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
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
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTagihan_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

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

       cmSl = New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTanggal.EditValue & "') AND (Akhir >= '" & Me.DTPTanggal.EditValue & "') AND (MtUang = '" & Me.SLUMtUang.EditValue & "')ORDER BY Tanggal DESC")

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
        Try
            DsMaster.Tables("T_TagihJsBJDtl").Clear()
            DsMaster.Tables("T_TagihJsBJDtl2").Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,D.TrmID,D.Tanggal From T_TagihJsBJDtl D Inner Join T_TrmBJ T On D.TrmID=T.TrmID Where TagihID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihJsBJDtl")
        cmsl.Fill(DsMaster, "T_TagihJsBJDtl")

        DsMaster.Tables("T_TagihJsBJDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihJsBJDtl").Columns("TrmID")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_TagihJsBJDtl"

        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,TagihDtl,TrmID,D.ArtCode,ArtName,Qty,D.SatID,HarSat,HarAkhir From T_TagihJsBJDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where TagihID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TagihJsBJDtl2")
        cmsl.Fill(DsMaster, "T_TagihJsBJDtl2")

        DsMaster.Tables("T_TagihJsBJDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihJsBJDtl2").Columns("TrmID"), DsMaster.Tables("T_TagihJsBJDtl2").Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_TagihJsBJDtl2"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihID,PeriodID,CodeID,Grup,Tanggal,H.SuppID,S.Nama As Supplier,S.Alamat,K.Nama As Kota,CurrID,MtUang, NilTukarRp,TipePPn,PersenPPn,TotQty,TotDPP,TotPPn,TotAkhir,TotAkhirRp,SisaBayar,H.Ket,stsLunas,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_TagihJsBJ H Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,TagihID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_TagihJsBJ")
        Try
            DsMaster.Tables("T_TagihJsBJ").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TagihJsBJ")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TagihJsBJ"
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue / (((100 + Me.TBPersenPPn.EditValue) / 100) * Me.TBPersenPPn.EditValue / 100), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue + Me.TBTotPPn.EditValue
        End If

        Me.TBTotAkhirRp.EditValue = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)

    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TagihJsBJ")
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
    End Sub

    Private Sub FTagihanBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Tagihan Jasa Barang Jadi"
    End Sub

    Private Sub FTagihanBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FTagihanBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTagihan_e.Selected = True
    End Sub

    Private Sub BVTTagihan_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTagihan_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Tagihan Jasa Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("InvBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Tagihan Jasa Barang Jadi"

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

        Me.SLUSupp.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.RBPPn.EditValue = "Non PPn"
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0.0
        Me.TBTotAkhir.EditValue = 0.0
        Me.TBTotAkhirRp.EditValue = 0.0
        Me.TBTotDPP.EditValue = 0.0
        Me.TBTotPPn.EditValue = 0.0
        Me.TBInfo.EditValue = ""
        Jenis = "LPB"

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TagihJsBJDtl").Clear()
        DsMaster.Tables("T_TagihJsBJDtl2").Clear()
        ReDim arrPar(-1)
        ReDim arrParS(-1)
        ReDim arrParS2(-1)
        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Tagihan Jasa Barang Jadi"

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
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
        Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
        Me.TBTotAkhirRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhirRp")
        Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
        Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

        FillDtl(Me.TBKode.EditValue)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        If Me.GridView1.RowCount > 0 Then
            'If Me.GridView3.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Tagihan Jasa Barang Jadi"

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
            Dim cmSP As New SqlCommand("SPDelT_TagihJsBJ")
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

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        TotQty = Math.Round(CType(Me.GridView3.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView3.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

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
                Dim cmSP As New SqlCommand("SPInsT_TagihJsBJ")
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
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = TotQty
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Me.TBTotSbDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Me.TBTotAkhirRp.EditValue
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


                        Dim i : For i = 0 To GridView3.RowCount - 1

                            If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "TrmID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TagihJsBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TrmID")
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
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


                                Dim z : For z = 0 To GridView3.RowCount - 1
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                        'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView3.GetRowCellValue(z, "TagihDtl") Then
                                        If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetRowCellValue(z, "TrmID") Then

                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihJsBJDtl2")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TrmID")
                                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarSat")
                                                .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarAkhir")
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
                Dim cmSP As New SqlCommand("SPUpT_TagihJsBJ")
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
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = TotQty
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Me.TBTotSbDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Me.TBTotAkhirRp.EditValue
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


                        Dim y : For y = 0 To arrParS.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_TagihJsBJDtl")
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
                            Dim cmSPDel As New SqlCommand("SPDelT_TagihJsBJDtl2")
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
                            If Me.GridView3.GetRowCellValue(i, "TagihIDD") < 0 Then
                                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "TrmID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TagihJsBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TrmID")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
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

                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "TagihIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                                'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView3.GetRowCellValue(z, "TagihDtl") * -1 Then
                                                If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetRowCellValue(z, "TrmID") Then

                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihJsBJDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                        .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                        .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TrmID")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarSat")
                                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarAkhir")
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
                                                        Me.GridView3.SetRowCellValue(i, "TagihIDD", Me.GridView3.GetRowCellValue(i, "TagihIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If

                                                End If
                                            End If

                                        Else
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                                'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView3.GetRowCellValue(z, "TagihDtl") Then
                                                If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetRowCellValue(z, "TrmID") Then

                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_TagihJsBJDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TagihIDD")
                                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                        .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                        .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = IdD
                                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TrmID")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarSat")
                                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarAkhir")
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

                                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TagihJsBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TagihIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "TrmID")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
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

                                Dim z : For z = 0 To GridView3.RowCount - 1
                                    If Me.GridView3.GetRowCellValue(z, "TagihIDD") < 0 Then
                                        If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                            'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView3.GetRowCellValue(z, "TagihDtl") Then
                                            If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetRowCellValue(z, "TrmID") Then

                                                Dim cmSPDtl2 As New SqlCommand("SPInsT_TagihJsBJDtl2")
                                                cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl2
                                                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                    .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                    .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(i, "TagihIDD") 'IdD
                                                    '.Parameters.Add("@TrmIDD", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "TrmIDD")
                                                    .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TrmID")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarSat")
                                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarAkhir")
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
                                                    Me.GridView3.SetRowCellValue(z, "TagihIDD", Me.GridView3.GetRowCellValue(z, "TagihIDD") * -1)
                                                Else
                                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If

                                    Else
                                        If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                            'If Me.GridView3.GetRowCellValue(i, "TagihIDD") = Me.GridView3.GetRowCellValue(z, "TagihDtl") Then
                                            If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetRowCellValue(z, "TrmID") Then

                                                Dim cmSPDtl2 As New SqlCommand("SPUpT_TagihJsBJDtl2")
                                                cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl2
                                                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                                                    .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TagihIDD")
                                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 6
                                                    .Parameters.Add("@GdId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "GdId")
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView3.GetRowCellValue(i, "Tanggal")
                                                    .Parameters.Add("@TagihDtl", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(i, "TagihIDD") 'IdD
                                                    .Parameters.Add("@TrmID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "TrmID")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarSat")
                                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "HarAkhir")
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


    Private Sub RBPPn_EditValueChanged(sender As Object, e As EventArgs) Handles RBPPn.EditValueChanged
        HitPPn()
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        Me.GridView3.Focus()

        If Jenis = "Non LPB" Then
            If Me.GridView1.OptionsBehavior.Editable = True Then
                TotQty = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

                HitPPn()
            End If
        End If

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

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                'If Me.GridView3.GetRowCellValue(i, "TagihDtl") = Me.GridView3.GetFocusedDataRow.Item("TagihIDD") Then
                If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetFocusedDataRow.Item("TrmID") Then
                    ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                    arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("TagihIDD")

                    Me.GridView3.DeleteRow(i)
                End If
            Next
        End If
    End Sub

    Private Sub BEdTrmID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdTrmID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdTrmID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdTrmID.ButtonClick
        Try

            Dim frm As New FSearch("LPB BJ", Me.SLUSupp.EditValue, Me.RBPPn.EditValue, Me.SLUMtUang.EditValue, Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then

                If Me.GridView3.RowCount > 0 Then
                    Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                        If Me.GridView3.GetRowCellValue(i, "TrmID") = Me.GridView3.GetFocusedDataRow.Item("TrmID") Then

                            ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                            arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(i, "TagihIDD")

                            Me.GridView3.DeleteRow(i)
                        End If
                    Next
                End If

                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "POID", dataTrans.Item("POID").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Tanggal", dataTrans.Item("Tgl").ToString)
               

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select *,0 As Urut,'' As NamaLain,Round(HarAkhir-RpDiscPH-DiscRpH,4) As HarAkhirH From (Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 As TagihIDD,'" & Me.TBKode.EditValue & "' as TagihID," & Me.GridView3.GetFocusedDataRow.Item("TagihIDD") & " As TagihDtl,TrmID,D.ArtCode,ArtName,Sum(Qty) As Qty,D.SatID,Isi,HarSat,Sum(HarAkhir) As HarAkhir From T_TrmBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where TrmID='" & dataTrans.Item("Kode").ToString & "' Group By D.ArtCode,ArtName,TrmID,D.SatID,HarSat) As x", koneksi)

                cmsl.TableMappings.Add("Table", "T_TagihJsBJDtl2")
                cmsl.Fill(DsMaster, "T_TagihJsBJDtl2")

                DsMaster.Tables("T_TagihJsBJDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihJsBJDtl2").Columns("TrmID"), DsMaster.Tables("T_TagihJsBJDtl2").Columns("ArtCode")}

                Me.GridView3.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"

            End If
        Catch ex As Exception

        End Try

    End Sub
   
    Private Sub GridView3_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try

            Me.GridView3.SetRowCellValue(e.RowHandle, "TagihIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "TrmID", "")
     
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView3_RowCountChanged(sender As Object, e As EventArgs) Handles GridView3.RowCountChanged
        If Me.GridView3.RowCount > 0 Then
            'If Me.GridView3.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub GridView3_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If Me.GridView3.RowCount > 0 Then
            'If Me.GridView3.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
    End Sub

    Private Sub GridControl3_Leave(sender As Object, e As EventArgs) Handles GridControl3.Leave
        Me.GridView3.Focus()
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

            CekCurr()
        End If
    End Sub

    'Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
    '    Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Tagihan Jasa Barang Jadi " & periodeBulan & " " & periodeTahun & "")
    '    If fileName <> "" Then
    '        ExportTo(New ExportXlsProvider(fileName))
    '        OpenFile(fileName)
    '    End If
    'End Sub

    Private Sub FTagihanBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub


    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class