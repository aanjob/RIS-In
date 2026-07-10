Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid

Public Class FPOBJJOv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, CurrID, JnsCust As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=1 and Gol='Job Order'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("POBJN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintTH.Enabled = False
        Me.BVBRevisi.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTPO_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
        Me.DTPTglJT.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.SLUSales.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.TBPOCust.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
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
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintTH.Enabled = False
        Me.BVBRevisi.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPO_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.DTPTglJT.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.SLUSales.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.TBPOCust.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridControl1.UseEmbeddedNavigator = True

        Me.GridColumn61.Visible = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPO_e.Selected = True
    End Sub

    Public Sub CancelOrder()
        Me.GridColumn61.Visible = True
        Me.GridColumn61.OptionsColumn.AllowEdit = True

        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn47.OptionsColumn.AllowEdit = False
        Me.GridColumn50.OptionsColumn.AllowEdit = False

        Me.GridControl1.UseEmbeddedNavigator = False
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

        cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='Job Order'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SalesLUE")
        Try
            DsMaster.Tables("M_SalesLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SalesLUE")

        Me.SLUSales.Properties.DataSource = DsMaster.Tables("M_SalesLUE")
        Me.SLUSales.Properties.DisplayMember = "Nama"
        Me.SLUSales.Properties.ValueMember = "SalID"

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE")
        cmsl.Fill(DsMaster, "M_SuppLUE")
        DsMaster.Tables("M_SuppLUE").Clear()
        cmsl.Fill(DsMaster, "M_SuppLUE")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"

        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
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
        cmsl = New SqlDataAdapter("Select POIDD,POID,Project,D.ArtCode,ArtName,W.Nama As Warna,D.Uk,D.SatID,D.Isi,Qty,QtyPol,Psg,PsgPol,HarSat,HCBP, HarAkhir,BtlProd,BtlOrder,SisaKirim,stsKirim,SisaProd,stsProd,(Select Count(*) From T_BOMPO where POID=D.POID and ArtCodeInd=D.ArtCode) As JmlBOM From T_POBJJODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJJODtl")
        Try
            DsMaster.Tables("T_POBJJODtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJJODtl")

        DsMaster.Tables("T_POBJJODtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJJODtl").Columns("Project"), DsMaster.Tables("T_POBJJODtl").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBJJODtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POID,PeriodID,CodeID,Tanggal,TglKirim,TglKirimR1,TglKirimR2,TglJT,TglJTR1,TglJTR2,Jenis,H.SalID,Sl.Nama As Sales,H.SuppID,S.Nama As Supp,S.Alamat As AlamatSupp,(Select Nama From M_Kota Where KotaID=S.KotaID) As KotaSupp,H.CustID,C.Nama As Customer, C.Alamat,K.Nama As Kota,POCust,Kat,MtUang,CurrID,NilTukarRp,TotQty,TotQtyPol,TotPsg,TotPsgPol,TotBayar,SisaKirim,SisaProd,H.Ket,H.Grup, H.stsKirim, H.stsProd,H.stsBtlProd,H.stsBatal,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,TotPsg-(Select Isnull((Select Sum(Tot) From T_BOMPO where POID=H.POID),0)) As BOMPO From T_POBJJO H Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Left Outer Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Sales Sl On H.SalID=Sl.SalID Where PeriodID=" & MainModule.periodAktif & " and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,POID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJJO")
        Try
            DsMaster.Tables("T_POBJJO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJJO")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_POBJJO"

        If MainModule.NoHarga = True Then
            Me.GridColumn19.Visible = False
            Me.GridColumn23.Visible = False
        Else
            Me.GridColumn19.Visible = True
            Me.GridColumn23.Visible = True
        End If

    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_POBJJO")
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

    Private Sub FPOBJJO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Purchase Order Barang Jadi"
    End Sub

    Private Sub FPOBJJO_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPOBJJO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTPO_e.Selected = True
    End Sub

    Private Sub BVTPO_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPO_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Purchase Order Barang Jadi"
        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("POBJEd"), Boolean)
        Me.BVBRevisi.Enabled = CType(TcodeCollection.Item("POBJRv"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("POBJCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("POBJDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("POBJP"), Boolean)
        Me.BVBPrintTH.Enabled = CType(TcodeCollection.Item("POBJPTH"), Boolean)
        Me.BVBRevisi.Enabled = CType(TcodeCollection.Item("POBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Purchase Order Barang Jadi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ("Job Order") > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglKirim.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglJT.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now
            Me.DTPTglJT.EditValue = Date.Now

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

        Me.CBOJenis.EditValue = ""
        Me.SLUSales.EditValue = ""
        Me.SLUSupp.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.TBPOCust.EditValue = ""
        Me.CBOKat.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.TBNilTukarRp.EditValue = 1
        Me.MKet.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_POBJJODtl").Clear()
        ReDim arrPar(-1)
        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Purchase Order Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Opname Atau Tutup Periode. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotPsg") <> SlCek("T_POBJJO", "SisaKirim", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Or SlCek("T_POBJJO", "stsBatal", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Penerimaan/Lunas/Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If MainModule.SlPOMkt(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
        '    XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditarik BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Try
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
            'Me.DTPTglJT.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglJT")
            Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.SLUSales.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
            Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
            Me.TBPOCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("POCust")
            Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
            Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
            Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

            FillDtl(Me.TBKode.EditValue)
            ReDim arrPar(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            Me.DTPTglJT.Properties.ReadOnly = True
            Me.DTPTglKirim.Properties.ReadOnly = True

        Catch ex As Exception

        End Try
    End Sub


    Private Sub BVBRevisi_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBRevisi.ItemClick
        Dim stsK1, stsK2, stsJ1, stsJ2 As Boolean

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglKirimR1")) Then
            stsK1 = False
            stsK2 = True
            'Else
            '    stsK1 = True
            '    stsK2 = True
        End If

        If Not IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglKirimR1")) And IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglKirimR2")) Then
            stsK1 = True
            stsK2 = False
            'Else
            '    stsK1 = True
            '    stsK2 = True
        End If

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglJTR1")) Then
            stsJ1 = False
            stsJ2 = True
            'Else
            '    stsJ1 = True
            '    stsJ2 = True
        End If

        If Not IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglJTR1")) And IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglJTR2")) Then
            stsJ1 = True
            stsJ2 = False
            'Else
            '    stsJ1 = True
            '    stsJ2 = True
        End If

        Dim TglKirim, TglJT As String

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglKirimR1")) Then
            TglKirim = "Kosong"
        Else
            TglKirim = Me.GridView2.GetFocusedDataRow.Item("TglKirimR1")
        End If

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("TglJTR1")) Then
            TglJT = "Kosong"
        Else
            TglJT = Me.GridView2.GetFocusedDataRow.Item("TglJTR1")
        End If

        'Dim frm As New FPOBJJO_r(Me.GridView2.GetFocusedDataRow.Item("POID"), TglKirim, TglJT, stsK1, stsK2, stsJ1, stsJ2)
        'frm.ShowDialog()
    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Purchase Order Barang Jadi"

        If Me.GridView2.GetFocusedDataRow.Item("stsProd") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Produksi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("stsBatal") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Produksi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("stsKirim") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Kirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlBOMCancel(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena SPK Belum Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "300"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        'Me.DTPTglJT.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglJT")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
        Me.SLUSales.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.TBPOCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("POCust")
        Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        Me.DTPTglJT.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True

        CancelOrder()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Purchase Order Barang Jadi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlPOMkt(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ditarik BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotPsg") <> SlCek("T_POBJJO", "SisaKirim", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Or SlCek("T_POBJJO", "stsBatal", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Penerimaan/Lunas/Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("POID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_POBJJO")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("POID")
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
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POCust"), "POCust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add("Customer", "SuppCust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotBayar"), "TotBayar")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotQty") + Me.GridView2.GetFocusedDataRow.Item("TotQtyPol"), "TotQty")

        Dim XR As New XRPOBJJO
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBPrintTH_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintTH.ItemClick
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POCust"), "POCust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add("Customer", "SuppCust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MtUang"), "MtUang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotBayar"), "TotBayar")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotQty") + Me.GridView2.GetFocusedDataRow.Item("TotQtyPol"), "TotQty")

        Dim XR As New XRPOBJJO2
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.RefreshData()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBPOCust.EditValue = "" Or IsDBNull(Me.TBPOCust.EditValue) Then
            XtraMessageBox.Show("PO Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CBOKat.EditValue = "" Or IsDBNull(Me.CBOKat.EditValue) Then
            XtraMessageBox.Show("Kategori Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUSales.EditValue = "" Or IsDBNull(Me.SLUSales.EditValue) Then
            XtraMessageBox.Show("Sales Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Format(Me.DTPTglKirim.EditValue, "dd MMMM yyyy") = Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") Or Me.DTPTglKirim.EditValue < Me.DTPTanggal.EditValue Then
            XtraMessageBox.Show("Tanggal Kirim Harus Diisi Dengan Benar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If Format(Me.DTPTglJT.EditValue, "dd MMMM yyyy") = Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") Or Me.DTPTglJT.EditValue < Me.DTPTanggal.EditValue Then
        '    XtraMessageBox.Show("Tanggal Jatuh Tempo Harus Diisi Dengan Benar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_POBJJO")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    '.Parameters.Add("@TglJT", SqlDbType.Date).Value = Me.DTPTglJT.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@POCust", SqlDbType.VarChar).Value = Me.TBPOCust.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("Qty").SummaryText, Decimal)
                    .Parameters.Add("@TotQtyPol", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("QtyPol").SummaryText, Decimal)
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("Psg").SummaryText, Decimal)
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("PsgPol").SummaryText, Decimal)
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_POBJJODtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Project", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Project")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                    .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                    .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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
                Dim cmSP As New SqlCommand("SPUpT_POBJJO")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    '.Parameters.Add("@TglJT", SqlDbType.Date).Value = Me.DTPTglJT.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@POCust", SqlDbType.VarChar).Value = Me.TBPOCust.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("Qty").SummaryText, Decimal)
                    .Parameters.Add("@TotQtyPol", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("QtyPol").SummaryText, Decimal)
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("Psg").SummaryText, Decimal)
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = CType(Me.GridView1.Columns("PsgPol").SummaryText, Decimal)
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_POBJJODtl")
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
                            If Me.GridView1.GetRowCellValue(i, "POIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_POBJJODtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Project", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Project")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                        .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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
                                        Me.GridView1.SetRowCellValue(i, "POIDD", Me.GridView1.GetRowCellValue(i, "POIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_POBJJODtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Project", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Project")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                        .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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


            Case 300
                Dim x As Integer

                Dim i : For i = 0 To GridView1.RowCount - 1
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                        Dim cmSPDtl As New SqlCommand("SPBtlPOBJJO")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
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

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()

        If Me.SLUMtUang.EditValue <> "" And Not IsDBNull(Me.SLUMtUang.EditValue) And Me.SLUMtUang.Properties.ReadOnly = False Then

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "POIDD")

                Me.GridView1.DeleteRow(i)
            Next

        End If
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("POIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            Dim frm As New FPOBJJO_a(Me.SLUGrup.EditValue, Me.SLUMtUang.EditValue, jnsCust)
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

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat")) And Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"))
            End If

        ElseIf e.Column Is GridView1.Columns("QtyPol") Then

            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPol", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat")) And Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty")) Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"))
            End If

        ElseIf e.Column Is GridView1.Columns("HarSat") Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty")) And Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"))
            End If

        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If Me.GridView1.OptionsBehavior.Editable = True Then
            Dim frm As New FSearch("M_BrgJO", "", "", "", Date.Now, "")
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Warna", dataTrans.Item("Warna").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", dataTrans.Item("Uk").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "POIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Project", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("Nama" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Warna", dataTrans.Item("Warna" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", dataTrans.Item("Uk" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", dataTrans.Item("HarSat" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HCBP", dataTrans.Item("HCBP" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPol", dataTrans.Item("QtyPol" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", dataTrans.Item("Psg" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPol", dataTrans.Item("PsgPol" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "IsiDlmDos", 1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "JmlBOM", 0)

                rw += 1
            End If
        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try

    End Sub

    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView2.GetRowCellValue(e.RowHandle, "TotPsg") <> Me.GridView2.GetRowCellValue(e.RowHandle, "BOMPO") And Me.GridView2.GetRowCellValue(e.RowHandle, "stsBatal") = False And Me.GridView2.GetRowCellValue(e.RowHandle, "stsBtlProd") = False Then
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Yellow
                Else
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPOBJJO_d(Me.GridView2.GetFocusedDataRow.Item("POID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FPOBJJO_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub GridView1_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView1.ValidateRow
        Dim View As GridView = CType(sender, GridView)
        Dim HarSatCol As GridColumn = View.Columns("HarSat")
        Dim CBPCol As GridColumn = View.Columns("HCBP")

        If Indicator <> 300 Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat") = 0 Then
                e.Valid = False
                View.SetColumnError(HarSatCol, "Harga Harus Diisi")
            End If

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Me.GridView1.GetRowCellValue(e.RowHandle, "HCBP") = 0 Then
                e.Valid = False
                View.SetColumnError(CBPCol, "Harga CBP Harus Diisi")
            End If
        End If
    End Sub

    Private Sub GridView1_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles GridView1.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPOCust.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub


    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Project" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "JmlBOM") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf Me.GridView1.GetRowCellValue(e.RowHandle, "JmlBOM") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If Me.GridView1.RowCount > 0 Then
                If (e.FocusedRowHandle >= 0) Then
                    If Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "JmlBOM") > 0 Then
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                        Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("QtyPol").OptionsColumn.AllowEdit = False

                    ElseIf Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "JmlBOM") = 0 Then
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                        Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("QtyPol").OptionsColumn.AllowEdit = True

                    End If
                End If
            End If
        End If
    End Sub

End Class