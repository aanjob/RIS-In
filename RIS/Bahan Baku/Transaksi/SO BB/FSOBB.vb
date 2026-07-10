Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FSOBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, CurrID, KdLama, JnsPPn As String
    Dim Manual, MnlInsUpd, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim Gol As String
    Dim Path As String
    Dim DsUpd As System.Data.DataSet
    Dim SplitFile() As String
    Dim NamaFile As String
    Dim DBLink As String = ""

    Public Sub New(BBSpM As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Distinct Manuall,MnlInsUpd From M_DocCode Where DocID=22 and CabID='" & Gol & "'", koneksi)

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

        Me.BVTSO_e.Caption = "SO " & Gol
        Me.Text = "Sales Order " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SON"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTSO_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.TBPOCust.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.TBTamDiscP.Properties.ReadOnly = True
        Me.TBTamDiscRp.Properties.ReadOnly = True
        Me.CEUpload.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTSO_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.TBPOCust.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False
        Me.CEUpload.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSO_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Aktif='True' and Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Clear()
        cmsl.Fill(DsMaster, "M_CustL")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
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

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SOIDD,SOID,BOMID,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir, BtlOrder,SisaKirim,stsKirim,DivPO,B.JnsID,Merk,SubJns,Tbl,Gram,Wrn,Kode,Hard,Uk,Jasa,ThnProd,stsJasa From T_SOBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where SOID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_SOBBDtl")
        Try
            DsMaster.Tables("T_SOBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SOBBDtl")

        DsMaster.Tables("T_SOBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_SOBBDtl").Columns("BOMID"), DsMaster.Tables("T_SOBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SOBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SOID,PeriodID,Tanggal,Gol,Jenis,H.CustID,C.Nama As Customer,Alamat,K.Nama As Kota,POCust,CurrID,MtUang, NilTukarRp,TipePPn,PersenPPn,TotSbDisc,DiscP,RpDiscP,DiscRp,TotDisc,TotDPP,TotPPn,TotAkhir,H.Ket,stsPPn,stsBatal,stsKirim,H.InsDate, H.InsBy,H.UpdDate,H.UpdBy From T_SOBB H Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID and PeriodID=" & MainModule.periodAktif & " and Gol ='" & Gol & "' Order By Tanggal,SOID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SOBB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_SOBB" & Gol)
        DsMaster.Tables("T_SOBB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_SOBB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SOBB" & Gol
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

        Dim cmSP As New SqlCommand("SPDelT_SOBB")
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
        If IO.File.Exists("SrBBNBOM" & Gol & ".xml") Then
            System.IO.File.Delete("SrBBNBOM" & Gol & ".xml")
        End If
    End Sub

    Private Sub FSOBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Sales Order " & Gol
    End Sub

    Private Sub FSOBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FSOBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FSOBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTSO_e.Selected = True
    End Sub

    Private Sub BVTSO_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSO_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Sales Order " & Gol
        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SOEd"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("SOCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SODel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SOP"), Boolean)
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("SOPH"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Sales Order " & Gol

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

        Me.SLUCust.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.CBOJenis.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""
        Me.CEUpload.EditValue = False

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_SOBBDtl").Clear()
        ReDim arrPar(-1)

        CekCurr()

        If Me.CEUpload.EditValue = False Then
            Me.BUpload.Enabled = False
            Me.TBPOCust.Properties.ReadOnly = False
            Me.TBPOCust.EditValue = ""
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = True
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True

        Else
            Me.BUpload.Enabled = True
            Me.TBPOCust.Properties.ReadOnly = True
            Me.TBPOCust.EditValue = ""
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = False
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Sales Order " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Opname Atau Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If MainModule.SlSJK(Me.GridView2.GetFocusedDataRow.Item("SOID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Pengiriman", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlFtBB(Me.GridView2.GetFocusedDataRow.Item("SOID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditarik Faktur", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("SOID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("SOID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.TBPOCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("POCust")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        If Me.RBPPn.EditValue = "Include" Then
            Me.TBPersenPPn.Properties.ReadOnly = True
        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBPersenPPn.Properties.ReadOnly = False
        ElseIf Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBPersenPPn.Properties.ReadOnly = True
        End If

        Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
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
    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Sales Order " & Gol

        If Me.GridView2.GetFocusedDataRow.Item("stsKirim") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        koneksi.Close()
        If XtraMessageBox.Show("Apakah Anda Mau Membatalkan Sisa : " & Me.GridView2.GetFocusedDataRow.Item("SOID") & " Yang Belum Dikirim ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPBtlSOBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SOID")
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
                        XtraMessageBox.Show("Sisa Order PO Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Sisa Order PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Sales Order " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlSJK(Me.GridView2.GetFocusedDataRow.Item("SOID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Pengiriman", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlFtBB(Me.GridView2.GetFocusedDataRow.Item("SOID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Dijadikan Faktur", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("SOID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_SOBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SOID")
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
        koneksi.Close()

        Dim Kode As String
        Kode = Me.GridView2.GetFocusedDataRow.Item("SOID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "SOID") = Kode Then
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
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SOID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRSOBB
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SOID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PersenPPn"), "PersenPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRSOBBH
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        'Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue
        HitPPn()

        If Me.RBPPn.EditValue <> "Non PPn" Then
            stsPPn = True
            JnsPPn = "PPn"
        Else
            stsPPn = False
            JnsPPn = "Non PPn"
        End If

        If Me.SLUCust.EditValue = "" Or IsDBNull(Me.SLUCust.EditValue) Then
            XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim command As New SqlCommand("Select CodeID From M_DocCode Where DocID=22 and Gol='" & JnsPPn & "' and CabID='" & Gol & "'", koneksi)

        With koneksi
            .Open()
            CodeID = command.ExecuteScalar()
            .Close()
        End With


        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_SOBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@POCust", SqlDbType.VarChar).Value = Me.TBPOCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_SOBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@Upload", SqlDbType.Bit).Value = Me.CEUpload.EditValue
                                    .Parameters.Add("@Link", SqlDbType.VarChar).Value = DBLink
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Bahan")
                                    .Parameters.Add("@DivPO", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivPO")
                                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsID")
                                    .Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Merk")
                                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SubJns")
                                    .Parameters.Add("@Tbl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Tbl")
                                    .Parameters.Add("@Gram", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Gram")
                                    .Parameters.Add("@Wrn", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Wrn")
                                    .Parameters.Add("@SubKode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Kode")
                                    .Parameters.Add("@Hard", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Hard")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@Jasa", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Jasa")
                                    .Parameters.Add("@ThnProd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ThnProd")
                                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
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
                Dim cmSP As New SqlCommand("SPUpT_SOBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@POCust", SqlDbType.VarChar).Value = Me.TBPOCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
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
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_SOBBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "SOIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SOBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Upload", SqlDbType.Bit).Value = Me.CEUpload.EditValue
                                        .Parameters.Add("@Link", SqlDbType.VarChar).Value = DBLink
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Bahan")
                                        .Parameters.Add("@DivPO", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivPO")
                                        .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsID")
                                        .Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Merk")
                                        .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SubJns")
                                        .Parameters.Add("@Tbl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Tbl")
                                        .Parameters.Add("@Gram", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Gram")
                                        .Parameters.Add("@Wrn", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Wrn")
                                        .Parameters.Add("@SubKode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Kode")
                                        .Parameters.Add("@Hard", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Hard")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Jasa", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Jasa")
                                        .Parameters.Add("@ThnProd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ThnProd")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
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
                                        Me.GridView1.SetRowCellValue(i, "SOIDD", Me.GridView1.GetRowCellValue(i, "SOIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_SOBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SOIDD")
                                        .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
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

        Dim cmd2 As New SqlCommand("SPAftSSOBB")
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


    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("SOIDD")
        End If
    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Try

            'Dim frm As New FSearch("BB No BOM", "%", Gol, "", Date.Now, "")
            Dim frm As New FSearch("Bahan Stok Harga", "%", Gol, "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
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

            Me.GridView1.SetRowCellValue(e.RowHandle, "SOIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)

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
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 6, MidpointRounding.AwayFromZero)
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
            Dim frm As New FSOBB_d(Me.GridView2.GetFocusedDataRow.Item("SOID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
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

    Private Sub OpenFileDialog1_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim strm As System.IO.Stream
        strm = OpenFileDialog1.OpenFile()
        Path = OpenFileDialog1.FileName.ToString()

        SplitFile = Path.Split("\")

        If SplitFile(SplitFile.GetUpperBound(0)).ToString.Contains(".xlsx") Then
            NamaFile = SplitFile(SplitFile.GetUpperBound(0)).ToString.Replace(".xlsx", "")
        Else
            NamaFile = SplitFile(SplitFile.GetUpperBound(0)).ToString.Replace(".xls", "")
        End If

    End Sub

    Private Sub BUpload_Click(sender As Object, e As EventArgs) Handles BUpload.Click

        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 0
        Me.OpenFileDialog1.ShowDialog()

        Try
            Dim MyConnection As System.Data.OleDb.OleDbConnection
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + Path + "'; Extended Properties=Excel 8.0;")
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("Select * from [" & NamaFile & "$]", MyConnection)
            MyCommand.TableMappings.Add("Table", "SOBBExEc")
            DsUpd = New System.Data.DataSet

            MyCommand.Fill(DsUpd)

            Me.TBPOCust.EditValue = Me.DsUpd.Tables("SOBBExEc").Rows(0).Item("ID PO")
            Me.SLUMtUang.EditValue = Me.DsUpd.Tables("SOBBExEc").Rows(0).Item("MtUang")
            Me.RBPPn.EditValue = Me.DsUpd.Tables("SOBBExEc").Rows(0).Item("Tipe PPn")
            DBLink = Me.DsUpd.Tables("SOBBExEc").Rows(0).Item("DB")


            If SlCek("T_SOBB", "Count(*)", "POCust", Me.TBPOCust.EditValue) = 0 Then
                Dim i : For i = 0 To Me.DsUpd.Tables("SOBBExEc").Rows.Count - 2
                    If IsDBNull(Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("BOMID")) = True Then
                        DsMaster.Tables("T_SOBBDtl").Rows.Add(Me.GridView1.RowCount * -1, Me.TBKode.EditValue, "", Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("BBID"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Nama"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Qty"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Satuan"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Harga Satuan"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("HarSbDisc"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("DiscRp"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("DiscP"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("RpDiscP"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Harga Akhir"), 0, Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Qty"), False, Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("DivPO"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("JnsID"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Merk"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("SubJns"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Tbl"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Gram"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Wrn"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Kode"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Hard"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Uk"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Jasa"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("ThnProd"), CBool(CStr(Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("stsJasa"))))
                    Else
                        DsMaster.Tables("T_SOBBDtl").Rows.Add(Me.GridView1.RowCount * -1, Me.TBKode.EditValue, Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("BOMID"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("BBID"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Nama"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Qty"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Satuan"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Harga Satuan"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("HarSbDisc"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("DiscRp"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("DiscP"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("RpDiscP"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Harga Akhir"), 0, Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Qty"), False, Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("DivPO"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("JnsID"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Merk"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("SubJns"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Tbl"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Gram"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Wrn"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Kode"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Hard"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Uk"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("Jasa"), Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("ThnProd"), CBool(CStr(Me.DsUpd.Tables("SOBBExEc").Rows(i).Item("stsJasa"))))
                    End If


                Next

                MyConnection.Close()

                CekCurr()
                HitPPn()

            Else
                XtraMessageBox.Show("PO Customer Sudah Pernah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CEUpload_EditValueChanged(sender As Object, e As EventArgs) Handles CEUpload.EditValueChanged
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SOIDD")

            Me.GridView1.DeleteRow(i)
        Next

        If Me.CEUpload.EditValue = False Then
            Me.BUpload.Enabled = False
            Me.TBPOCust.Properties.ReadOnly = False
            Me.TBPOCust.EditValue = ""
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = True
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True

        Else
            Me.BUpload.Enabled = True
            Me.TBPOCust.Properties.ReadOnly = True
            Me.TBPOCust.EditValue = ""
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = False
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class