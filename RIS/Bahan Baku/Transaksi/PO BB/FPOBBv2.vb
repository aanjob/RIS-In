Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FPOBBv2
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim arrPar2(-1) As String
    Dim CodeID, CurrID, KdLama, JnsPPn As String
    Dim Manual, MnlInsUpd, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim JT, IdD As Integer
    Dim FC As Boolean
    Dim Jenis, Gol As String
    Dim invalid As Boolean = False

    Public Sub New(BBSpM As String, Jns As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Jenis = Jns
        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            command = New SqlCommand("Select Distinct Manuall,MnlInsUpd From M_DocCode Where DocID=5 and CabID='" & Gol & "'", koneksi)

            If Gol = "Bahan" Then
                Me.Text = ".: Purchase Order Bahan Baku Stock :."
            Else
                Me.Text = ".: Purchase Order Sparepart Stock :."
            End If

        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select Distinct Manuall,MnlInsUpd From M_DocCode Where DocID=56 and CabID='" & Gol & "'", koneksi)

            If Gol = "Bahan" Then
                Me.Text = ".: Purchase Order Bahan Baku Non Stock :."
            Else
                Me.Text = ".: Purchase Order Sparepart Non Stock :."
            End If
        End If

        If Gol = "Bahan" Then
            Me.CBOTipe.Properties.Items.AddRange(New Object() {"Bahan", "Jasa", "Jasa Produksi", "Tooling"})
        Else
            Me.CBOTipe.Properties.Items.AddRange(New Object() {"Mesin", "Sparepart", "Umum"})
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

        Me.BVTPOBB_e.Caption = "PO " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub


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
            Dim link As BaseExportLink = GridView4.CreateExportLink(provider)
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

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("POBBN"), Boolean)
        Me.BVTPOBB_trm.Enabled = CType(TcodeCollection.Item("POBBTrm"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTPOBB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.CBOTipe.Properties.ReadOnly = True
        Me.CBODok.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.CBOShipment.Properties.ReadOnly = True
        Me.CENonPT.Properties.ReadOnly = True
        Me.CEKlik.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.TBTamDiscP.Properties.ReadOnly = True
        Me.TBTamDiscRp.Properties.ReadOnly = True
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
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPOBB_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.CBOTipe.Properties.ReadOnly = False
        Me.CBODok.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOShipment.Properties.ReadOnly = False
        Me.CEKlik.Properties.ReadOnly = False
        Me.CENonPT.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPOBB_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        Try
            DsMaster.Tables("M_CustL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Rows.Add("", "", "", "")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,JT,K.Nama As Kota,stsOdJasa From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
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
                    Me.SLUMtUang.EditValue = "IDR"
                    'Me.Dispose()
                End If
                Reader.Close()

                .Close()
            End With
        End With
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_POBBDtl").Clear()
            DsMaster.Tables("T_POBBJs").Clear()
        Catch ex As Exception

        End Try

        Try
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select POIDD,POID,Free,D.BOMID,D.DocIDD,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP, HarAkhir,BtlOrder,SisaKirim,stsKirim From T_POBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Kode & "'", koneksi)
            cmsl.TableMappings.Add("Table", "T_POBBDtl")
            cmsl.Fill(DsMaster, "T_POBBDtl")

            DsMaster.Tables("T_POBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBBDtl").Columns("BOMID"), DsMaster.Tables("T_POBBDtl").Columns("DocIDD"), DsMaster.Tables("T_POBBDtl").Columns("BBID")}

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_POBBDtl"

            cmsl = New SqlDataAdapter("Select POIDD,POID,POIDDtl,D.BOMID,BBIDJs,D.BBID, B.Nama As Bahan,Qty,D.Sat From T_POBBJs D Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Kode & "'", koneksi)
            cmsl.TableMappings.Add("Table", "T_POBBJs")
            cmsl.Fill(DsMaster, "T_POBBJs")

            DsMaster.Tables("T_POBBJs").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBBJs").Columns("BOMID"), DsMaster.Tables("T_POBBJs").Columns("BBIDJs"), DsMaster.Tables("T_POBBJs").Columns("BBID")}

            Me.GridControl3.DataSource = DsMaster
            Me.GridControl3.DataMember = "T_POBBJs"

        Catch ex As Exception
            XtraMessageBox.Show("Data Error Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            FC = True
        End Try
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select POID,PeriodID,Jenis,Gol,Tanggal,Kat,Tipe,Shipment,H.CustID,C.Nama As Customer,H.SuppID,S.Nama As Supplier,S.Alamat,K.Nama As Kota,TglKirim,DueDate,CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRp, TotDisc,TotDPP,TotPPn, TotAkhir,H.Ket,H.stsOdJasa,H.stsNonPT,stsQC,stsPPn,Dok,stsBatal,stsKirim,stsTrm,H.InsDate,H.InsBy,H.UpdDate, H.UpdBy,H.TrmDate,H.TrmBy From T_POBB H Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Left Outer Join M_Cust C On H.CustID=C.CustID Where PeriodID=" & MainModule.periodAktif & " and Jenis='" & Jenis & "' and Gol ='" & Gol & "' Order By POID,Tanggal asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBB" & Gol & Jenis)
        Try
            DsMaster.Tables("T_POBB" & Gol & Jenis).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBB" & Gol & Jenis)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_POBB" & Gol & Jenis
    End Sub

    Public Sub FillDtTrm()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select POID,PeriodID,Jenis,Gol,Tanggal,Kat,Tipe,Shipment,H.CustID,C.Nama As Customer,H.SuppID,S.Nama As Supplier,S.Alamat,K.Nama As Kota,TglKirim,DueDate,CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRp, TotDisc,TotDPP,TotPPn, TotAkhir,H.Ket,H.stsOdJasa,stsQC,stsPPn,Dok,stsBatal,stsKirim,stsTrm,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy, H.TrmDate,H.TrmBy From T_POBB H Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Left Outer Join M_Cust C On H.CustID=C.CustID Where stsTrm='False' and Jenis='" & Jenis & "' and Gol ='" & Gol & "' Order By POID,Tanggal asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBBTrm" & Gol & Jenis)
        Try
            DsMaster.Tables("T_POBBTrm" & Gol & Jenis).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBBTrm" & Gol & Jenis)

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "T_POBBTrm" & Gol & Jenis
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

        Dim cmSP As New SqlCommand("SPDelT_POBB")
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

    Public Sub Print()
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tipe"), "Tipe")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supplier"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TipePPn"), "TipePPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PersenPPn"), "PersenPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DueDate"), "DueDate")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDisc") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")


        Dim Cek As Integer

        Dim command As New SqlCommand("Select Count(*) From(Select POID,BOMID,DocIDD,BBID,Count(*) as Jml From T_POBBDtl where POID='" & Me.GridView2.GetFocusedDataRow.Item("POID") & "' Group By POID,BOMID,DocIDD,BBID) as x Where Jml>1", koneksi)

        With koneksi
            .Open()
            Cek = command.ExecuteScalar()
            .Close()
        End With

        If Cek = 0 Then
            Dim XR As New XRPOBB
            XR.InitializeData(bind, Me.GridView2.GetFocusedDataRow.Item("Dok"))
        Else
            XtraMessageBox.Show("Data Error Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Public Sub DelXml()
        If IO.File.Exists("SrBBWBOM.xml") Then
            System.IO.File.Delete("SrBBWBOM.xml")
        End If

        If IO.File.Exists("SrBBWReq.xml") Then
            System.IO.File.Delete("SrBBWReq.xml")
        End If

        If IO.File.Exists("SrBBKnWBOM.xml") Then
            System.IO.File.Delete("SrBBKnWBOM.xml")
        End If

        If IO.File.Exists("SrBBNBOM" & Gol & ".xml") Then
            System.IO.File.Delete("SrBBNBOM" & Gol & ".xml")
        End If

        If IO.File.Exists("SrPRTool" & Me.SLUSupp.EditValue & ".xml") Then
            System.IO.File.Delete("SrPR" & Me.SLUSupp.EditValue & ".xml")
        End If

        If IO.File.Exists("SrPRSpM" & Me.SLUSupp.EditValue & Me.CBOTipe.EditValue & Jenis & ".xml") Then
            System.IO.File.Delete("SrPRSpM" & Me.SLUSupp.EditValue & Me.CBOTipe.EditValue & Jenis & ".xml")
        End If
    End Sub

    Public Sub NewEdit()
        Me.GridColumn14.Visible = False
        Me.GridColumn14.OptionsColumn.AllowEdit = False

        Me.GridColumn3.OptionsColumn.AllowEdit = True
        Me.GridColumn4.OptionsColumn.AllowEdit = True
        Me.GridColumn7.OptionsColumn.AllowEdit = True
        Me.GridColumn8.OptionsColumn.AllowEdit = True
        Me.GridColumn10.OptionsColumn.AllowEdit = True
        Me.GridColumn11.OptionsColumn.AllowEdit = True
        Me.GridColumn64.OptionsColumn.AllowEdit = True
    End Sub

    Public Sub CancelOrder()
        Me.GridColumn14.Visible = True
        Me.GridColumn14.OptionsColumn.AllowEdit = True

        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn64.OptionsColumn.AllowEdit = False

    End Sub


    Private Sub FPOBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Purchase Order Item"
    End Sub

    Private Sub FPOBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPOBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FPOBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTPOBB_e.Selected = True

        If Manual = True Then
            Me.GridColumn3.Visible = False
            Me.LCDok.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        Else
            Me.GridColumn3.Visible = True
            Me.LCDok.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        End If
    End Sub

    Private Sub BVTPOBB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPOBB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Purchase Order Item"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("POBBEd"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("POBBCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("POBBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("POBBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Purchase Order Item"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Manual = False Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now
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

        Me.CBODok.EditValue = ""
        Me.CBOKat.EditValue = "Lokal"

        If Gol = "Bahan" Then
            Me.CBOTipe.EditValue = "Bahan"
            Me.SLUCust.Properties.ReadOnly = False
        Else
            Me.CBOTipe.EditValue = "Sparepart"
            Me.SLUCust.Properties.ReadOnly = True
        End If

        Me.SLUCust.EditValue = ""
        Me.SLUSupp.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOShipment.EditValue = ""
        Me.CEQC.EditValue = False
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTamDiscRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBPersenPPn.EditValue = 0
        Me.TBInfo.EditValue = ""
        Me.CEKlik.EditValue = True
        Me.CENonPT.EditValue = False
        Me.CEOdJasa.EditValue = False

        If Me.CBOTipe.EditValue = "Jasa" Then
            Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_POBBDtl").Clear()
        DsMaster.Tables("T_POBBJs").Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        CekCurr()

        Me.GridView3.ActiveFilter.Clear()
        NewEdit()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Purchase Order Item"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If MainModule.SlKirim(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Or Me.GridView2.GetFocusedDataRow.Item("stsKirim") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Pengiriman Atau Sudah Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()
        Indicator = "200"
        Me.CEKlik.EditValue = True
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("POID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
        Me.CBOTipe.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tipe")
        Me.CBODok.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dok")
        Me.CEQC.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsQC")
        Me.CBOShipment.EditValue = Me.GridView2.GetFocusedDataRow.Item("Shipment")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
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
        stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
        Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
        Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
        Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
        Me.TBTotDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDisc")
        Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
        Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
        Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")
        Me.CEOdJasa.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsOdJasa")
        Me.CENonPT.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsNonPT")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

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
        CekSave = True

        If Me.CBOTipe.EditValue = "Jasa" Then
            Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Me.GridView1.RowCount > 0 Then
            If Me.GridView3.RowCount > 0 Then
                Me.GridView3.ActiveFilterString = "[BBIDJs] = '" & Me.GridView1.GetFocusedRowCellValue("BBID") & "' and [BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"
            End If
        End If

        NewEdit()

        If Gol = "Bahan" Then
            Me.SLUCust.Properties.ReadOnly = False
        Else
            Me.SLUCust.Properties.ReadOnly = True
        End If

        If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue("Free")) Then
            If Me.GridView1.GetFocusedRowCellValue("Free") = True Then
                Me.GridView1.Columns("HarSat").OptionsColumn.AllowEdit = False
            Else
                Me.GridView1.Columns("HarSat").OptionsColumn.AllowEdit = True
            End If
        End If

    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Purchase Order Item"


        If SlCek("T_POBB", "stsKirim", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "300"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("POID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
        Me.CBOTipe.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tipe")
        Me.CBODok.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dok")
        Me.CEQC.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsQC")
        Me.CBOShipment.EditValue = Me.GridView2.GetFocusedDataRow.Item("Shipment")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPpn")
        Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
        Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
        Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
        Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
        Me.TBTotDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDisc")
        Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
        Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
        Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

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
        CekSave = True

        If Me.CBOTipe.EditValue = "Jasa" Then
            Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        CancelOrder()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Purchase Order Item"

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlKirim(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Or Me.GridView2.GetFocusedDataRow.Item("stsKirim") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Pengiriman Atau Sudah Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("POID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_POBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("POID")
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
        Dim Kode As String
        Kode = Me.GridView2.GetFocusedDataRow.Item("POID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "POID") = Kode Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Print()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView3.ActiveFilter.Clear()
        Me.GridView1.ActiveFilter.Clear()

        If Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2) <= 0 Then
            XtraMessageBox.Show("Qty Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
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

        If Gol = "Bahan" Then
            If Me.SLUCust.EditValue = "" Or IsDBNull(Me.SLUCust.EditValue) Then
                XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If Me.RBPPn.EditValue = "Include" Then
            XtraMessageBox.Show("PPn Tidak Boleh Include", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Manual = False Then
            If Format(Me.DTPTglKirim.EditValue, "dd MMMM yyyy") = Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") Or Me.DTPTglKirim.EditValue < Me.DTPTanggal.EditValue Then
                XtraMessageBox.Show("Tanggal Kirim Harus Diisi Dengan Benar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            command = New SqlCommand("Select CodeID From M_DocCode Where DocID=5 and Gol='" & JnsPPn & "' and CabID='" & Gol & "'", koneksi)
        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select CodeID From M_DocCode Where DocID=56 and Gol='" & JnsPPn & "' and CabID='" & Gol & "'", koneksi)
        End If

        With koneksi
            .Open()
            CodeID = command.ExecuteScalar()
            .Close()
        End With

        DsMaster.Tables("T_POBBJs").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBBJs").Columns("BOMID"), DsMaster.Tables("T_POBBJs").Columns("BBIDJs"), DsMaster.Tables("T_POBBJs").Columns("BBID")}

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_POBB")
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
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                    .Parameters.Add("@Dok", SqlDbType.VarChar).Value = Me.CBODok.EditValue
                    .Parameters.Add("@Ship", SqlDbType.VarChar).Value = Me.CBOShipment.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = JT
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsOdJasa", SqlDbType.Bit).Value = Me.CEOdJasa.EditValue
                    .Parameters.Add("@stsNonPT", SqlDbType.Bit).Value = Me.CENonPT.EditValue
                    .Parameters.Add("@QC", SqlDbType.Bit).Value = Me.CEQC.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_POBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@TipePO", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@DocIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Free", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "Free")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    '.Parameters.Add("@HarSatDPP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSatDPP")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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

                                ElseIf x = -2 Then
                                    Del()
                                    XtraMessageBox.Show("Qty Harus Lebih Besar Dari 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                Else
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If

                            If Me.CBOTipe.EditValue = "Jasa" Then
                                Dim z : For z = 0 To GridView3.DataRowCount - 1
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "BBID")) Then
                                        'If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") 
                                        If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView3.GetRowCellValue(z, "BBIDJs") And Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetRowCellValue(z, "BOMID") Then
                                            Dim cmSPDtl As New SqlCommand("SPInsT_POBBJs")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@POIDD", SqlDbType.VarChar).Value = IdD
                                                .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                                .Parameters.Add("@BBIDJs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                                .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BBID")
                                                .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Sat")
                                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
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
                                                XtraMessageBox.Show("Qty Kanan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                Exit Sub
                                            ElseIf x <> 0 Then
                                                Del()
                                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If
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
                Dim cmSP As New SqlCommand("SPUpT_POBB")
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
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                    .Parameters.Add("@Dok", SqlDbType.VarChar).Value = Me.CBODok.EditValue
                    .Parameters.Add("@Ship", SqlDbType.VarChar).Value = Me.CBOShipment.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = JT
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDisc", SqlDbType.Decimal).Value = Me.TBTotDisc.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsOdJasa", SqlDbType.Bit).Value = Me.CEOdJasa.EditValue
                    .Parameters.Add("@stsNonPT", SqlDbType.Bit).Value = Me.CENonPT.EditValue
                    .Parameters.Add("@QC", SqlDbType.Bit).Value = Me.CEQC.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_POBBDtl")
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_POBBJs")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(q)
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
                            If Me.GridView1.GetRowCellValue(i, "POIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_POBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                                        .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@TipePO", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                        .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@DocIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Free", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "Free")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscRp")
                                        .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscP")
                                        .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscP")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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
                                        Me.GridView1.SetRowCellValue(i, "POIDD", Me.GridView1.GetRowCellValue(i, "POIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                    If Me.CBOTipe.EditValue = "Jasa" Then
                                        Dim z : For z = 0 To GridView3.RowCount - 1
                                            If Me.GridView3.GetRowCellValue(z, "POIDD") < 0 Then
                                                If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "BBID")) Then
                                                    'If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") * -1 Then
                                                    If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView3.GetRowCellValue(z, "BBIDJs") And Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetRowCellValue(z, "BOMID") Then

                                                        Dim cmSPDtl2 As New SqlCommand("SPInsT_POBBJs")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@POIDD", SqlDbType.VarChar).Value = IdD
                                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                                            .Parameters.Add("@BBIDJs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BBID")
                                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Sat")
                                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
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
                                                            Me.GridView3.SetRowCellValue(z, "POIDD", Me.GridView3.GetRowCellValue(z, "POIDD") * -1)
                                                        ElseIf x = -3 Then
                                                            XtraMessageBox.Show("Qty Kanan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                            Exit Sub
                                                        Else
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If

                                                    End If
                                                End If

                                            Else

                                                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "BBID")) Then
                                                    'If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                                    If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView3.GetRowCellValue(z, "BBIDJs") And Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetRowCellValue(z, "BOMID") Then
                                                        Dim cmSPDtl2 As New SqlCommand("SPUpT_POBBJs")
                                                        cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                        With cmSPDtl2
                                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "POIDD")
                                                            .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                                            .Parameters.Add("@POIDDtl", SqlDbType.VarChar).Value = IdD
                                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                                            .Parameters.Add("@BBIDJs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BBID")
                                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Sat")
                                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
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

                                                        If x = -3 Then
                                                            XtraMessageBox.Show("Qty Kanan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                            Exit Sub
                                                        ElseIf x <> 0 Then
                                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                            Exit Sub
                                                        End If

                                                    End If
                                                End If

                                            End If
                                        Next
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_POBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@TipePO", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                                        .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@DocIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Free", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "Free")
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

                                    ElseIf x = -2 Then
                                        XtraMessageBox.Show("Qty Harus Lebih Besar Dari 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub

                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If

                                If Me.CBOTipe.EditValue = "Jasa" Then
                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "POIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "BBID")) Then
                                                'If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                                If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView3.GetRowCellValue(z, "BBIDJs") And Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetRowCellValue(z, "BOMID") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_POBBJs")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                                        .Parameters.Add("@BBIDJs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BBID")
                                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Sat")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
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
                                                        Me.GridView3.SetRowCellValue(z, "POIDD", Me.GridView3.GetRowCellValue(z, "POIDD") * -1)
                                                    ElseIf x = -3 Then
                                                        XtraMessageBox.Show("Qty Kanan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                        Exit Sub
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        Else
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "BBID")) Then
                                                'If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                                If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView3.GetRowCellValue(z, "BBIDJs") And Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetRowCellValue(z, "BOMID") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_POBBJs")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "POIDD")
                                                        .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                                        .Parameters.Add("@POIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                                        .Parameters.Add("@BBIDJs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BBID")
                                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Sat")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
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

                                                    If x = -3 Then
                                                        XtraMessageBox.Show("Qty Kanan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                                        Exit Sub
                                                    ElseIf x <> 0 Then
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        End If
                                    Next
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

            Case 300
                Dim x As Integer

                Dim i : For i = 0 To GridView1.RowCount - 1
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                        Dim cmSPDtl As New SqlCommand("SPBtlPOBB")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                            .Parameters.Add("@Batal", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtlOrder")
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                        End With

                        Try
                            With koneksi
                                .Open()
                                cmSPDtl.ExecuteNonQuery()
                                x = cmSPDtl.Parameters("@Return").Value
                                .Close()
                            End With

                            If x = 0 Then

                            Else
                                Del()
                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                        Catch ex As Exception
                            XtraMessageBox.Show("Proses Pembatalan PO Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                    End If
                Next

                If x = 0 Then
                    XtraMessageBox.Show("Proses Pembatalan PO Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("Proses Pembatalan PO Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

        End Select

        Dim cmd As New SqlCommand("SPHitBahanJs")
        cmd.CommandType = CommandType.StoredProcedure

        With cmd
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Connection = koneksi

            With koneksi
                .Open()
                cmd.ExecuteNonQuery()
                .Close()
            End With

        End With

        LockControl()
        CekSave = False

        If Indicator <> 300 Then
            Me.BVTPOBB_s.Selected = True
            Me.BVBPrint.Enabled = CType(TcodeCollection.Item("POBBP"), Boolean)

            FillDt()

            Dim fcv As Integer
            Dim a : For a = 0 To Me.GridView2.RowCount - 1
                If Me.GridView2.GetRowCellValue(a, "POID") = Me.TBKode.EditValue Then
                    fcv = a
                    Exit For
                End If
            Next

            Me.GridView2.FocusedRowHandle = fcv

            Print()
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub CBOKat_Leave(sender As Object, e As EventArgs) Handles CBOKat.Leave
        If Me.CBOKat.EditValue <> "" And Not IsDBNull(Me.CBOKat.EditValue) And Me.CBOKat.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "POIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                Me.GridView3.DeleteRow(x)
            Next
        End If

    End Sub

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave
        If Me.SLUSupp.EditValue <> "" And Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.Properties.ReadOnly = False Then
            Try
                If IO.File.Exists("SrBBWBOM.xml") Then
                    System.IO.File.Delete("SrBBWBOM.xml")
                End If

                If IO.File.Exists("SrBBWReq.xml") Then
                    System.IO.File.Delete("SrBBWReq.xml")
                End If

                If IO.File.Exists("SrBBNBOM.xml") Then
                    System.IO.File.Delete("SrBBNBOM.xml")
                End If

                If Not IsDBNull(Me.SLUSupp.EditValue) Then
                    JT = DsMaster.Tables("M_SuppLUE2").Select("SuppID = '" & Me.SLUSupp.EditValue & "'")(0).Item("JT")
                    Me.CEOdJasa.EditValue = DsMaster.Tables("M_SuppLUE2").Select("SuppID = '" & Me.SLUSupp.EditValue & "'")(0).Item("stsOdJasa")
                End If

                'If Me.CEOdJasa.EditValue = True Then
                '    Me.GridView1.Columns("Free").OptionsColumn.AllowEdit = False
                'Else
                '    Me.GridView1.Columns("Free").OptionsColumn.AllowEdit = True
                'End If

            Catch ex As Exception

            End Try

            Me.GridView3.ActiveFilter.Clear()
            Me.GridView1.ActiveFilter.Clear()

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "POIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                Me.GridView3.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub CBODok_Leave(sender As Object, e As EventArgs) Handles CBODok.Leave
        If Me.CBODok.EditValue <> "" And Not IsDBNull(Me.CBODok.EditValue) And Me.CBODok.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "POIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                Me.GridView3.DeleteRow(x)
            Next
        End If

    End Sub

    Private Sub CBOTipe_Leave(sender As Object, e As EventArgs) Handles CBOTipe.Leave
        If Me.CBOTipe.Properties.ReadOnly = False Then
            If Me.CBOTipe.EditValue = "Jasa" Then
                Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.LCJasa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If Me.CBOTipe.EditValue <> "" And Not IsDBNull(Me.CBOTipe.EditValue) And Me.CBOTipe.Properties.ReadOnly = False Then

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "POIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                    Me.GridView3.DeleteRow(x)
                Next
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            Me.GridView3.ActiveFilter.Clear()

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("POIDD")

            Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(x, "BBIDJs") = Me.GridView1.GetFocusedDataRow.Item("BBID") And Me.GridView3.GetRowCellValue(x, "BOMID") = Me.GridView1.GetFocusedDataRow.Item("BOMID") Then
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                    Me.GridView3.DeleteRow(x)
                End If
            Next

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            If Me.CEKlik.EditValue = True Then
                rw = 0

                If Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.EditValue <> "" Then
                    Dim frm As New FPOBB_a(Gol, Me.CBODok.EditValue, Me.CBOTipe.EditValue, Me.TBKode.EditValue, Me.SLUSupp.EditValue, Me.SLUCust.EditValue, Jenis)
                    frm.ShowDialog()

                    Try
                        If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                            Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                                If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                                    Me.GridView1.AddNewRow()
                                End If
                            Next
                        End If


                    Catch ex As Exception

                    End Try
                End If
            End If
        End If

    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("POIDD")
        End If
    End Sub

    Private Sub BEdBOMID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBOMID.ButtonClick
        If CBODok.EditValue = "BOM" Then
            Dim frm As New FSearch("BOM", "", "", "", Date.Now, "")
            frm.ShowDialog()

        ElseIf CBODok.EditValue = "Request" Then
            Dim frm As New FSearch("Request", "", "", "", Date.Now, "")
            frm.ShowDialog()
        End If

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", "")
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarAkhir", 0)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        koneksi.Close()
        Try
            If Me.CBOTipe.EditValue = "Jasa" And Me.GridView1.GetFocusedDataRow.Item("BBID") <> "" Then
                XtraMessageBox.Show("Tidak Bisa Diganti Harus Dihapus!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Me.CBOTipe.EditValue <> "Jasa Produksi" Then
                If Me.CBODok.EditValue = "BOM" Then
                    If IO.File.Exists("SrBBWBOM.xml") Then
                        System.IO.File.Delete("SrBBWBOM.xml")
                    End If

                    Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                        'MsgBox("Apakah " & Me.GridView3.GetRowCellValue(x, "POIDDtl") & " = " & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " dan " & Me.GridView3.GetRowCellValue(x, "BOMID") & " = " & Me.GridView1.GetFocusedDataRow.Item("BOMID") & " dan " & Me.GridView3.GetRowCellValue(x, "BBIDJs") & " = " & dataTrans.Item("Kode").ToString)

                        If Me.GridView3.GetRowCellValue(x, "POIDDtl") = Me.GridView1.GetFocusedDataRow.Item("POIDD") And Me.GridView3.GetRowCellValue(x, "BOMID") = Me.GridView1.GetFocusedDataRow.Item("BOMID") And Me.GridView3.GetRowCellValue(x, "BBIDJs") = Me.GridView1.GetFocusedDataRow.Item("BBID") Then
                            'MsgBox("Hapus")

                            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                            Me.GridView3.DeleteRow(x)
                        Else
                            'MsgBox("Tidak")

                        End If
                    Next

                    Dim frm As New FSearch("BB With BOM", Me.GridView1.GetFocusedDataRow.Item("BOMID"), Me.TBKode.EditValue, Me.SLUSupp.EditValue, Date.Now, "")
                    frm.ShowDialog()

                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                        Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DocIDD", 0)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", CDec(dataTrans.Item("Qty").ToString))

                        Dim Sisa As Decimal

                        Dim command As New SqlCommand("Select Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.ActiveEditor.EditValue & "' and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_BOMDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.ActiveEditor.EditValue & "'", koneksi)

                        With koneksi
                            .Open()
                            Sisa = command.ExecuteScalar()
                            .Close()
                        End With

                        If CDec(dataTrans.Item("Qty").ToString) > Sisa Then
                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", Sisa)
                        End If

                        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        If Me.CEOdJasa.EditValue = True Then
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Free", 0)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", 0)
                        Else
                            If Me.GridView1.GetFocusedDataRow.Item("Free") = True Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", 0)

                            Else
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))

                            End If

                        End If


                        If Me.CBOTipe.EditValue = "Jasa" Then
                            If CBool(dataTrans.Item("stsJasa").ToString) = True Then

                                Dim cmsl As SqlDataAdapter
                                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY  x.BBID)*-1 as POIDD,'" & Me.TBKode.EditValue & "' As POID," & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " As POIDDtl,'" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' As BOMID,'" & Me.GridView1.ActiveEditor.EditValue & "' As BBIDJs,* From(Select Distinct D.BBID,Nama AS Bahan," & Me.GridView1.GetFocusedDataRow.Item("Qty") & " As Qty,D.Sat From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where D.BBIDInd='" & Me.GridView1.ActiveEditor.EditValue & "' and BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "') as x", koneksi)

                                cmsl.TableMappings.Add("Table", "T_POBBJsTemp")
                                Try
                                    DsMaster.Tables("T_POBBJsTemp").Clear()
                                Catch ex As Exception

                                End Try
                                cmsl.Fill(DsMaster, "T_POBBJsTemp")

                                Dim i : For i = 0 To DsMaster.Tables("T_POBBJsTemp").Rows.Count - 1
                                    DsMaster.Tables("T_POBBJs").Rows.Add(DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDD"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDDtl"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BOMID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBIDJs"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Bahan"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Qty"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Sat"))
                                Next

                            End If
                        End If
                    End If

                ElseIf Me.CBODok.EditValue = "Request" Then

                    If IO.File.Exists("SrBBWReq.xml") Then
                        System.IO.File.Delete("SrBBWReq.xml")
                    End If

                    Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                        'MsgBox("Apakah " & Me.GridView3.GetRowCellValue(x, "POIDDtl") & " = " & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " dan " & Me.GridView3.GetRowCellValue(x, "BOMID") & " = " & Me.GridView1.GetFocusedDataRow.Item("BOMID") & " dan " & Me.GridView3.GetRowCellValue(x, "BBIDJs") & " = " & dataTrans.Item("Kode").ToString)

                        If Me.GridView3.GetRowCellValue(x, "POIDDtl") = Me.GridView1.GetFocusedDataRow.Item("POIDD") And Me.GridView3.GetRowCellValue(x, "BOMID") = Me.GridView1.GetFocusedDataRow.Item("BOMID") And Me.GridView3.GetRowCellValue(x, "BBIDJs") = Me.GridView1.GetFocusedDataRow.Item("BBID") Then
                            'MsgBox("Hapus")

                            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "POIDD")

                            Me.GridView3.DeleteRow(x)
                        Else
                            'MsgBox("Tidak")

                        End If
                    Next

                    Dim frm As New FSearch("BB With Request", Me.GridView1.GetFocusedDataRow.Item("BOMID"), Me.TBKode.EditValue, Me.SLUSupp.EditValue, Date.Now, "")
                    frm.ShowDialog()

                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                        Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DocIDD", 0)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", CDec(dataTrans.Item("Qty").ToString))

                        Dim Sisa As Decimal

                        Dim command As New SqlCommand("Select Sum(Req)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.ActiveEditor.EditValue & "' and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_ReqPDtl Where ReqPID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.ActiveEditor.EditValue & "'", koneksi)

                        With koneksi
                            .Open()
                            Sisa = command.ExecuteScalar()
                            .Close()
                        End With

                        If CDec(dataTrans.Item("Qty").ToString) > Sisa Then
                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty Request", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", Sisa)
                        End If

                        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        If Me.GridView1.GetFocusedDataRow.Item("Free") = True Then
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", 0)
                        Else
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))
                        End If

                        If Me.CBOTipe.EditValue = "Jasa" Then
                            If CBool(dataTrans.Item("stsJasa").ToString) = True Then

                                Dim cmsl As SqlDataAdapter
                                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY  x.BBID)*-1 as POIDD,'" & Me.TBKode.EditValue & "' As POID," & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " As POIDDtl,'" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' As BOMID,'" & Me.GridView1.ActiveEditor.EditValue & "' As BBIDJs,* From(Select Distinct D.BBID,Nama AS Bahan," & Me.GridView1.GetFocusedDataRow.Item("Qty") & " As Qty,D.Sat From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where D.BBIDInd='" & Me.GridView1.ActiveEditor.EditValue & "' and BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "') as x", koneksi)

                                cmsl.TableMappings.Add("Table", "T_POBBJsTemp")
                                Try
                                    DsMaster.Tables("T_POBBJsTemp").Clear()
                                Catch ex As Exception

                                End Try
                                cmsl.Fill(DsMaster, "T_POBBJsTemp")

                                Dim i : For i = 0 To DsMaster.Tables("T_POBBJsTemp").Rows.Count - 1
                                    DsMaster.Tables("T_POBBJs").Rows.Add(DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDD"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDDtl"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BOMID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBIDJs"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Bahan"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Qty"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Sat"))
                                Next

                            End If
                        End If
                    End If

                ElseIf Me.CBODok.EditValue = "" Then

                    Dim frm As New FSearch("BB No BOM", Me.SLUSupp.EditValue, Gol, "", Date.Now, "")
                    frm.ShowDialog()

                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then

                        Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DocIDD", 0)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 0)

                        If Me.GridView1.GetFocusedDataRow.Item("Free") = True Then
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", 0)
                        Else
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))
                        End If

                        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        If Me.CBOTipe.EditValue = "Jasa" Then
                            If CBool(dataTrans.Item("stsJasa").ToString) = True Then

                                Dim cmsl As SqlDataAdapter
                                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY BBIDM)*-1 as POIDD,'" & Me.TBKode.EditValue & "' As POID," & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " As POIDDtl,'" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' As BOMID,'" & Me.GridView1.ActiveEditor.EditValue & "' As BBIDJs,BBIDM As BBID,Nama AS Bahan," & Me.GridView1.GetFocusedDataRow.Item("Qty") & " As Qty,Sat From M_BBMentah M Inner Join M_BB B On M.BBIDM=B.BBID Where M.BBID='" & Me.GridView1.ActiveEditor.EditValue & "'", koneksi)
                                cmsl.TableMappings.Add("Table", "T_POBBJsTemp")

                                Try
                                    DsMaster.Tables("T_POBBJsTemp").Clear()
                                Catch ex As Exception

                                End Try
                                cmsl.Fill(DsMaster, "T_POBBJsTemp")

                                Dim i : For i = 0 To DsMaster.Tables("T_POBBJsTemp").Rows.Count - 1
                                    DsMaster.Tables("T_POBBJs").Rows.Add(DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDD"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDDtl"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BOMID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBIDJs"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Bahan"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Qty"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Sat"))
                                Next

                            End If
                        End If
                    End If
                End If

            Else
                'Tipe=Jasa Produksi
                Dim frm As New FSearch("BB No BOM", Me.SLUSupp.EditValue, Gol, "", Date.Now, "")
                frm.ShowDialog()

                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then

                    Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DocIDD", 0)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                    RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 0)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))

                    AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                End If
            End If

            If CBODok.EditValue = "Purchase Request" Then

                If Me.CBOTipe.EditValue = "Tooling" Then
                    Dim frm As New FSearch("Purchase Request Tooling", Me.SLUSupp.EditValue, Me.SLUCust.EditValue, "", Date.Now, "")
                    frm.ShowDialog()
                Else
                    Dim frm As New FSearch("Purchase Request Sparepart", Me.SLUSupp.EditValue, Me.CBOTipe.EditValue, Jenis, Date.Now, "")
                    frm.ShowDialog()
                End If

                If Not IsDBNull(dataTrans.Item("BBID").ToString) Then

                    Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("BBID").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DocIDD", dataTrans.Item("DocIDD").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)

                    RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BOMID", dataTrans.Item("Kode").ToString)

                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", dataTrans.Item("Qty").ToString)

                    AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID2_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBID2.ButtonClick
        If Me.CBODok.EditValue = "BOM" Then
            If IO.File.Exists("SrBBKnWBOM.xml") Then
                System.IO.File.Delete("SrBBKnWBOM.xml")
            End If

            Dim frm As New FSearch("BB Kanan With BOM", Me.GridView1.GetFocusedDataRow.Item("BOMID"), Me.TBKode.EditValue, Me.SLUSupp.EditValue, Date.Now, "Tampil All")
            frm.ShowDialog()

        ElseIf Me.CBODok.EditValue = "Request" Then
            If IO.File.Exists("SrBBKnWBOM.xml") Then
                System.IO.File.Delete("SrBBKnWBOM.xml")
            End If

            Dim frm As New FSearch("BB Kanan With Request", Me.GridView1.GetFocusedDataRow.Item("BOMID"), Me.TBKode.EditValue, Me.SLUSupp.EditValue, Date.Now, "Tampil All")
            frm.ShowDialog()

        ElseIf Me.CBODok.EditValue = "" Then

            Dim frm As New FSearch("M_BB", "", "Bahan", "", Date.Now, "")
            frm.ShowDialog()
        End If

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Qty") Then
            If Me.CBOTipe.EditValue <> "Jasa Produksi" Then

                If Manual = False Then
                    Dim Sisa As Decimal
                    Dim command As New SqlCommand

                    If Me.CBODok.EditValue = "BOM" Then
                        command = New SqlCommand("Select Sum(Qty) From(Select Isnull((Select Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and POID<>'" & Me.TBKode.EditValue & "'),0))-(Select Isnull((Select Round(Sum(KebAs),0) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBIDAs='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and stsApp='True'),0)) From T_BOMDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "'),0) As Qty Union All Select Isnull((Select Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and POID<>'" & Me.TBKode.EditValue & "'),0))-(Select Isnull((Select Round(Sum(KebAs),0) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBIDAs='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and stsApp='True'),0)) From T_BOMTamDtl Where TambahanID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "'),0) Union All Select Isnull((Select Round(Sum(KebTj),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Where H.MemoID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBIDTj='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and stsApp='True'),0) As Qty) as x", koneksi)

                        'command = New SqlCommand("Select Isnull((Select Round(Sum(Keb)+Sum(Pol),0)+(Select Isnull((Select Round(Sum(Keb)+Sum(Pol),0) From T_BOMTamDtl Where TambahanID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and POID<>'" & Me.TBKode.EditValue & "'),0))-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_BOMDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "'),0)", koneksi)

                        With koneksi
                            .Open()
                            Sisa = command.ExecuteScalar()
                            .Close()
                        End With

                        If Me.CBOKat.EditValue = "Lokal" Then
                            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa Then
                                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa)
                            End If
                        End If

                    ElseIf Me.CBODok.EditValue = "Purchase Request" Then

                        If Me.CBOTipe.EditValue = "Tooling" Then
                            command = New SqlCommand("Select Sum(Qty)-Sum(BtlOrder)-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and DocIDD=" & Me.GridView1.GetFocusedDataRow.Item("DocIDD") & " and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_PRToolDtl Where PRTID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and PRTIDD=" & Me.GridView1.GetFocusedDataRow.Item("DocIDD") & "", koneksi)

                        Else
                            command = New SqlCommand("Select Sum(Qty)-Sum(BtlOrder)-(Select Isnull((Select Sum(Qty-BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and DocIDD=" & Me.GridView1.GetFocusedDataRow.Item("DocIDD") & " and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_PRSpMDtl Where PRSMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and PRSMIDD=" & Me.GridView1.GetFocusedDataRow.Item("DocIDD") & "", koneksi)
                        End If

                        With koneksi
                            .Open()
                            Sisa = command.ExecuteScalar()
                            .Close()
                        End With

                        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa Then
                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty Purchase Request", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa)
                        End If

                    ElseIf Me.CBODok.EditValue = "Request" Then
                        command = New SqlCommand("Select Sum(Req)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and POID<>'" & Me.TBKode.EditValue & "'),0)) From T_ReqPDtl Where ReqPID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "'", koneksi)

                        With koneksi
                            .Open()
                            Sisa = command.ExecuteScalar()
                            .Close()
                        End With

                        If Me.CBOKat.EditValue = "Lokal" Then
                            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa Then
                                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty Request", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa)
                            End If
                        End If
                    End If

                End If

            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"), 2) * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 6), 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") * Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "DiscP"), 3)) / 100, 6))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "DiscRp") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscP"), 6))

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "POIDDtl") = Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") Then
                    Me.GridView3.SetRowCellValue(i, "Qty", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
                End If
            Next

        ElseIf e.Column Is GridView1.Columns("BtlOrder") Then
            'If Me.CBOTipe.EditValue <> "Jasa Produksi" Then

            'If Manual = False Then
            Dim Sisa As Decimal
            Dim command As New SqlCommand

            command = New SqlCommand("Select Round(Sum(Qty),2)-(Select Isnull((Select Sum(Qty) From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Where H.POID='" & Me.TBKode.EditValue & "' and POIDD=" & Me.GridView1.GetFocusedDataRow.Item("POIDD") & "),0))+(Select Isnull((Select Sum(Qty) From T_RtrBB H Inner Join T_RtrBBDtl D On H.RtrID=D.RtrID Where H.POID='" & Me.TBKode.EditValue & "' and POIDD=" & Me.GridView1.GetFocusedDataRow.Item("POIDD") & "),0)) From T_POBBDtl Where POID='" & Me.TBKode.EditValue & "' and  POIDD=" & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " and BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' Group By POID", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") > Sisa Then
                XtraMessageBox.Show("Batal Order Tidak Boleh Melebihi Qty PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BtlOrder", Sisa)
            End If
            'End If
            'End If

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

        ElseIf e.Column Is GridView1.Columns("BOMID") Then
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Bahan", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        ElseIf e.Column Is GridView1.Columns("Free") Then

            If Me.GridView1.GetFocusedRowCellValue("Free") = True Then
                Me.GridView1.Columns("HarSat").OptionsColumn.AllowEdit = False

                If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue("Free")) And Me.GridView1.GetFocusedRowCellValue("BBID") <> "" Then

                    Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
                End If
            Else
                Me.GridView1.Columns("HarSat").OptionsColumn.AllowEdit = True
            End If
        End If

    End Sub
    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If Me.GridView1.RowCount > 0 Then
                'If Me.GridView3.RowCount > 0 Then
                'Me.GridView3.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
                Me.GridView3.ActiveFilterString = "[BBIDJs] = '" & Me.GridView1.GetFocusedRowCellValue("BBID") & "' and [BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"

                If Indicator <> 300 Then

                    If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue("Free")) Then
                        If Me.GridView1.GetFocusedRowCellValue("Free") = True Then
                            Me.GridView1.Columns("HarSat").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridView1.Columns("HarSat").OptionsColumn.AllowEdit = True
                        End If
                    End If
                End If
                'End If
            End If
        End If
    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        If Me.CEKlik.EditValue = True Then
            Try
                If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                    RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    Me.GridView1.SetRowCellValue(e.RowHandle, "POIDD", Me.GridView1.RowCount * -1)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", dataTrans.Item("DocID" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", dataTrans.Item("BBID" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Bahan", dataTrans.Item("Bahan" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", dataTrans.Item("Sat" & rw).ToString)

                    If Me.CEOdJasa.EditValue = True Then
                        Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Free", True)
                    Else
                        Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", dataTrans.Item("Harga" & rw).ToString)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Free", False)
                    End If

                    Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", dataTrans.Item("stsJasa" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "DocIDD", dataTrans.Item("DocIDD" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", 0)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", 0)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", 0)

                    AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)

                    If Me.CBOTipe.EditValue = "Jasa" Then
                        If CBool(dataTrans.Item("stsJasa" & rw).ToString) = True Then

                            Dim cmsl As SqlDataAdapter
                            If Me.CBODok.EditValue = "BOM" Then
                                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY  x.BBID)*-1 as POIDD,'" & Me.TBKode.EditValue & "' As POID," & Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") & " As POIDDtl,'" & dataTrans.Item("DocID" & rw).ToString & "' As BOMID,'" & dataTrans.Item("BBID" & rw).ToString & "' As BBIDJs,* From(Select Distinct D.BBID,Nama AS Bahan," & Me.GridView1.GetFocusedDataRow.Item("Qty") & " As Qty,D.Sat From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where D.BBIDInd='" & dataTrans.Item("BBID" & rw).ToString & "' and BOMID='" & dataTrans.Item("DocID" & rw).ToString & "') as x", koneksi)

                            ElseIf Me.CBODok.EditValue = "Request" Then
                                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY  x.BBID)*-1 as POIDD,'" & Me.TBKode.EditValue & "' As POID," & Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") & " As POIDDtl,'" & dataTrans.Item("DocID" & rw).ToString & "' As BOMID,'" & dataTrans.Item("BBID" & rw).ToString & "' As BBIDJs,* From(Select Distinct D.BBID,Nama AS Bahan," & Me.GridView1.GetFocusedDataRow.Item("Qty") & " As Qty,D.Sat From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Where D.BBIDInd='" & dataTrans.Item("BBID" & rw).ToString & "' and BOMID='" & dataTrans.Item("BOMID" & rw).ToString & "') as x", koneksi)

                            ElseIf Me.CBODok.EditValue = "" Then
                                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY BBIDM)*-1 as POIDD,'" & Me.TBKode.EditValue & "' As POID," & Me.GridView1.GetFocusedDataRow.Item("POIDD") & " As POIDDtl,'" & dataTrans.Item("DocID" & rw).ToString & "' As BOMID,'" & dataTrans.Item("BBID" & rw).ToString & "' As BBIDJs,BBIDM As BBID,Nama AS Bahan," & Me.GridView1.GetFocusedDataRow.Item("Qty") & " As Qty,Sat From M_BBMentah M Inner Join M_BB B On M.BBIDM=B.BBID Where M.BBID='" & dataTrans.Item("BBID" & rw).ToString & "'", koneksi)
                            End If

                            cmsl.TableMappings.Add("Table", "T_POBBJsTemp")

                            Try
                                DsMaster.Tables("T_POBBJsTemp").Clear()
                            Catch ex As Exception

                            End Try

                            cmsl.Fill(DsMaster, "T_POBBJsTemp")

                            Dim i : For i = 0 To DsMaster.Tables("T_POBBJsTemp").Rows.Count - 1
                                DsMaster.Tables("T_POBBJs").Rows.Add(DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDD"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("POIDDtl"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BOMID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBIDJs"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("BBID"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Bahan"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Qty"), DsMaster.Tables("T_POBBJsTemp").Rows(i).Item("Sat"))
                            Next

                        End If

                        If Me.GridView1.RowCount > 0 Then
                            'If Me.GridView3.RowCount > 0 Then
                            'Me.GridView3.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
                            Me.GridView3.ActiveFilterString = "[BBIDJs] = '" & Me.GridView1.GetFocusedRowCellValue("BBID") & "' and [BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"
                            'End If
                        End If
                    End If

                    rw += 1
                End If

            Catch ex As Exception

                Me.GridView1.DeleteRow(e.RowHandle)
            End Try

        Else

            Try
                RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(e.RowHandle, "POIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Free", False)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscP", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscRp", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscP", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "DocIDD", 0)

                If Me.CBODok.EditValue <> "Purchase Request" Then
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
                Else
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
                End If

                AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Catch ex As Exception
                Me.GridView1.DeleteRow(e.RowHandle)
            End Try

        End If


    End Sub
    Private Sub GridView1_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles GridView1.InvalidRowException
        If Me.CEKlik.EditValue = False Then
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", "")
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSbDisc", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DiscRp", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DiscP", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "RpDiscP", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarAkhir", 0)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SisaKirim", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
        End If

    End Sub

    Private Sub GridView1_RowCountChanged(sender As Object, e As EventArgs) Handles GridView1.RowCountChanged
        If Me.GridView1.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[BBIDJs] = '" & Me.GridView1.GetFocusedRowCellValue("BBID") & "' and [BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"
        End If
    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try
            Me.GridView3.SetRowCellValue(e.RowHandle, "POIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "POID", Me.TBKode.EditValue)
            Me.GridView3.SetRowCellValue(e.RowHandle, "POIDDtl", Me.GridView1.GetFocusedDataRow.Item("POIDD"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "BOMID", Me.GridView1.GetFocusedDataRow.Item("BOMID"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "BBIDJs", Me.GridView1.GetFocusedDataRow.Item("BBID"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView3.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView3.SetRowCellValue(e.RowHandle, "Qty", Me.GridView1.GetFocusedDataRow.Item("Qty"))

            'If Me.GridView1.RowCount > 0 Then
            '    If Me.GridView3.RowCount > 0 Then
            '        Me.GridView3.ActiveFilterString = "[BBIDJs] = '" & Me.GridView1.GetFocusedRowCellValue("BBID") & "' and [BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"

            '    End If
            'End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub GridView3_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView3.ValidateRow
        Dim View As GridView = CType(sender, GridView)
        Dim QtyCol As GridColumn = View.Columns("Qty")

        If Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") = 0 And Me.GridView3.GetRowCellValue(e.RowHandle, "BBID") <> "" Then
            e.Valid = False
            View.SetColumnError(QtyCol, "Qty Harus Diisi")
        End If

    End Sub
    Private Sub GridView3_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles GridView3.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = ExceptionMode.NoAction
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
            Me.TBTotDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("DiscRp").SummaryText, Decimal) + CType(Me.GridView1.Columns("RpDiscP").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
            Me.TBTamDiscPRp.EditValue = Math.Round((Me.TBTotSbDisc.EditValue - Me.TBTotDisc.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            HitPPn()
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

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPOBB_d(Me.GridView2.GetFocusedDataRow.Item("POID"), Manual)
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

    Private Sub BEdBBID2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID2.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdBOMID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBOMID.KeyPress
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

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim cmsl As SqlDataAdapter
        Dim command As New SqlCommand("Select Count(*) From T_POBBDtl Where POID='" & Me.GridView2.GetFocusedDataRow.Item("POID") & "' and HarSat=0 and  Free='False'", koneksi)
        Dim jml As Integer

        koneksi.Close()

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        If jml > 0 Then
            XtraMessageBox.Show("Tidak Bisa Diexport Excel Karena Masih Ada Harga yang Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            cmsl = New SqlDataAdapter("Select H.POID,'" & Me.GridView2.GetFocusedDataRow.Item("MtUang") & "' As MtUang,'" & Me.GridView2.GetFocusedDataRow.Item("TipePPn") & "' As TipePPn,'" & Me.GridView2.GetFocusedDataRow.Item("PersenPPn") & "' As PersenPPn,'" & Me.GridView2.GetFocusedDataRow.Item("PersenPPn") & "' As PersenPPn,'" & MainModule.namaDB & "'As DB,D.BOMID,D.BBID,B.Nama,D.Sat,Qty-BtlOrder As Qty,HarSat, HarSbDisc,D.DiscRp,D.DiscP,D.RpDiscP,HarAkhir,DivPO,B.JnsID,Merk,SubJns,Tbl,Gram,Wrn,Kode,Hard,Uk,Jasa,ThnProd,Case When stsJasa='True' Then 1 Else 0 End As stsJasa From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID Inner Join M_BB B On D.BBID=B.BBID Where H.POID='" & Me.GridView2.GetFocusedDataRow.Item("POID") & "' and Qty-BtlOrder>0", koneksi)
            cmsl.TableMappings.Add("Table", "ExEcPOBB")
            Try
                DsMaster.Tables("ExEcPOBB").Clear()
            Catch ex As Exception

            End Try

            cmsl.Fill(DsMaster, "ExEcPOBB")

            Me.GridControl4.DataSource = DsMaster
            Me.GridControl4.DataMember = "ExEcPOBB"

            Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "PO Jasa Produksi " & Me.GridView2.GetFocusedDataRow.Item("POID") & "")

            If fileName <> "" Then
                ExportTo(New ExportXlsProvider(fileName))
                OpenFile(fileName)
            End If
        End If

    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub BVTPOBB_trm_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPOBB_trm.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Terima Purchase Order Item"
        LockControl()

        FillDtTrm()
    End Sub

    Private Sub BTerima_Click(sender As Object, e As EventArgs) Handles BTerima.Click
        koneksi.Close()

        Me.GridView5.ActiveFilter.Clear()

        Dim POID As String
        Dim y As Integer = 0
        POID = ""

        For i = 0 To Me.GridView5.RowCount - 1
            If Me.GridView5.GetRowCellValue(i, "stsTrm") = True Then
                y += 1

                If y = 1 Then
                    POID = Me.GridView5.GetRowCellValue(i, "POID")
                Else
                    POID &= "," & Me.GridView5.GetRowCellValue(i, "POID")
                End If
            End If
        Next

        Dim cmSP As New SqlCommand("SPUpstsTrmPO")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@POID", SqlDbType.VarChar).Value = POID
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
                    XtraMessageBox.Show("Data PO Berhasil Diterima", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("Data PO Gagal Diterima", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diterima", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With
    End Sub


End Class