Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FPOBJLk
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, Merk, Kat, Jns, Sat, Gol, CurrID, JnsPPn As String
    Dim Manual, MnlInsUpd, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim Isi, IdD As Integer
    Dim arrParS2(-1) As String
    Dim arrParCP(-1) As String

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=1 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBPrintJL.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTPO_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.TBKirim.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

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
        Me.BVBPrintJL.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPO_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.TBKirim.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPO_e.Selected = True
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

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,JT,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE")
        Try
            DsMaster.Tables("M_SuppLUE").Clear()
        Catch ex As Exception

        End Try
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
        Try
            DsMaster.Tables("T_POBJLkDtl" & Gol).Clear()
            DsMaster.Tables("T_POBJLkDtl2" & Gol).Clear()
            DsMaster.Tables("T_POBJCollPO" & Gol).Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POIDD,POID,D.ArtCode,ArtName,Upp,Outs,Variasi,B.AssID,D.SatID,D.Isi,Qty,QtyPol,Psg,PsgPol,HarSat, HCBP,HarAkhir,SisaKirim,BtlOrder,BtlProd,(Select Count(*) From T_BOMPO where POID=D.POID and ArtCodeInd=D.ArtCode) As JmlBOM From T_POBJLkDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJLkDtl" & Gol)
        Try
            DsMaster.Tables("T_POBJLkDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJLkDtl" & Gol)

        DsMaster.Tables("T_POBJLkDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJLkDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBJLkDtl" & Gol

        cmsl = New SqlDataAdapter("Select POIDD,POID,POIDDtl,D.ArtCode,Uk,IsiDlmDos,Qty,QtyPol From T_POBJLkDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where POID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_POBJLkDtl2" & Gol)
        cmsl.Fill(DsMaster, "T_POBJLkDtl2" & Gol)

        DsMaster.Tables("T_POBJLkDtl2" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJLkDtl2" & Gol).Columns("POID"), DsMaster.Tables("T_POBJLkDtl2" & Gol).Columns("POIDD"), DsMaster.Tables("T_POBJLkDtl2" & Gol).Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_POBJLkDtl2" & Gol


        cmsl = New SqlDataAdapter("Select HubID,POID,POIDD,ArtCode,PC.CollPOID,C.Nama As Cust,CollPOIDD,Qty From T_POBJCollPO PC Inner Join T_CollPO CP On PC.CollPOID=CP.CollPOID Inner Join M_Cust C On CP.CustID=C.CustID Where POID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_POBJCollPO" & Gol)
        cmsl.Fill(DsMaster, "T_POBJCollPO" & Gol)

        DsMaster.Tables("T_POBJCollPO" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJCollPO" & Gol).Columns("POIDD"), DsMaster.Tables("T_POBJCollPO" & Gol).Columns("CollPOIDD")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_POBJCollPO" & Gol

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POID,PeriodID,CodeID,Tanggal,TglKrmAw,TglKrmAkh,Jenis,H.JT,H.SuppID,S.Nama As Supp,H.CustID,C.Nama As Cust, KrmKe,MtUang,CurrID,NilTukarRp,TipePPn,PersenPPn,TotQty,TotPsg,TotPsgPol,TotPPn,TotDPP,TotAkhir,SisaKirim,Pembelian,PPIC,H.Ket, H.Grup,H.Gol,H.stsPPn,H.stsLunas,H.stsBatal,H.stsBtlProd,H.InsDate, H.InsBy,H.UpdDate,H.UpdBy From T_POBJLk H Left Outer Join M_Supp S On H.SuppID=S.SuppID Left Outer Join M_Cust C On H.CustID=C.CustID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,POID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJLk" & Gol)
        Try
            DsMaster.Tables("T_POBJLk" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJLk" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_POBJLk" & Gol

        If MainModule.NoHarga = True Then
            Me.GridView2.OptionsMenu.EnableColumnMenu = False

            Me.GridColumn45.Visible = False
        Else
            Me.GridView2.OptionsMenu.EnableColumnMenu = True

            Me.GridColumn45.Visible = True
        End If
    End Sub

    Public Sub HitPPn()
        Try
            Me.TBTotAkhir.EditValue = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

            If Me.RBPPn.EditValue = "Non PPn" Then
                Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
                Me.TBTotPPn.EditValue = 0

            ElseIf Me.RBPPn.EditValue = "Include" Then
                Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
                Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

            ElseIf Me.RBPPn.EditValue = "Exclude" Then
                Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
                Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
                Me.TBTotAkhir.EditValue = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) + Me.TBTotPPn.EditValue
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_POBJLk")
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

    Private Sub FPOBJLk_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Purchase Order Barang Jadi"
    End Sub

    Private Sub FPOBJLk_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FPOBJLk_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPOBJLk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTPO_e.Selected = True
    End Sub

    Private Sub BVTPO_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPO_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Purchase Order Barang Jadi"
        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("POBJEd"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("POBJCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("POBJDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("POBJP"), Boolean)
        Me.BVBPrintJL.Enabled = CType(TcodeCollection.Item("POBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Purchase Order Barang Jadi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
                'If MainModule.BackDate = False Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
                'End If
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPAwal.EditValue = Date.Now
            Me.DTPAkhir.EditValue = Date.Now

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

        Me.TBJT.EditValue = 0
        Me.SLUGrup.EditValue = ""
        Me.SLUSupp.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.CBOJenis.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.TBNilTukarRp.EditValue = 1
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0

        Me.MKet.EditValue = "* Quality harap diperhatikan" & vbCrLf & "* Jaminan Produk 1 tahun dari Delivery " & vbCrLf & "  (Cashback 100% dari barang yang rusak)" & vbCrLf & "* Included Pack Vacuum & Outer Box" & vbCrLf & "* Material Upper Webbing "
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)

        DsMaster.Tables("T_POBJLkDtl" & Gol).Clear()
        DsMaster.Tables("T_POBJLkDtl2" & Gol).Clear()
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

        If Me.GridView2.GetFocusedDataRow.Item("TotPsg") <> SlCek("T_POBJLk", "SisaKirim", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Or SlCek("T_POBJLk", "stsBatal", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Then
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
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.DTPAwal.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKrmAw")
            Me.DTPAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKrmAkh")
            Me.TBJT.EditValue = Me.GridView2.GetFocusedDataRow.Item("JT")
            Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
            Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
            Me.TBKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("KrmKe")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
            Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
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
            stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPPn")

            FillDtl(Me.TBKode.EditValue)
            ReDim arrPar(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            If Me.GridView1.RowCount > 0 Then
                'If Me.GridView3.RowCount > 0 Then
                Me.GridView3.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
                Me.GridView4.ActiveFilterString = "[POIDD] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"

                'End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Purchase Order Barang Jadi"
        koneksi.Close()

        If Me.GridView2.GetFocusedDataRow.Item("stsLunas") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlBOMCancel(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena SPK Belum Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlPOMkt(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            If XtraMessageBox.Show("PO Sudah Ditarik BOM. Apakah Mau Dibatalkan ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPBtlPOBJLkAll")
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
                            XtraMessageBox.Show("Sisa Order PO Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Sisa Order PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            Else
                Exit Sub
            End If
        Else
            If XtraMessageBox.Show("Apakah Anda Mau Membatalkan Sisa : " & Me.GridView2.GetFocusedDataRow.Item("POID") & " Yang Belum Dikirim/Diproduksi ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPBtlPOBJLkAll")
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
        End If
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

        If Me.GridView2.GetFocusedDataRow.Item("TotPsg") <> SlCek("T_POBJLk", "SisaKirim", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Or SlCek("T_POBJLk", "stsBatal", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Penerimaan/Lunas/Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("POID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_POBJLk")
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
        koneksi.Close()

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

        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supp"), "Kpd")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("KrmKe"), "KrmKe")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAw"), "TglKrmAw")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAkh"), "TglKrmAkh")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JT"), "JT")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("PPIC"), "PPIC")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Pembelian"), "Pembelian")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        'If Gol = "Character" Then
        '    bind.Add(MainModule.MMCr, "MM")
        'ElseIf Gol = "Own" Then
        '    bind.Add(MainModule.MMOwn, "MM")
        'ElseIf Gol = "Job Order" Then
        '    bind.Add(MainModule.MMJO, "MM")
        'End If

        Dim XR As New XRPOBJLkR
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBPrintJL_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintJL.ItemClick
        'Dim bind As New Collection
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kpd"), "Kpd")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("KrmKe"), "KrmKe")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAw"), "TglKrmAw")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAkh"), "TglKrmAkh")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("HarSat"), "HarSat")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("JT"), "JT")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtCode"), "ArtCode")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Upp"), "Upp")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Outs"), "Outs")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Variasi"), "Variasi")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDos") + Me.GridView2.GetFocusedDataRow.Item("TotDosPol"), "TotDos")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "TotPsg")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotBayar"), "TotBayar")

        ''If Gol = "Character" Then
        ''    bind.Add(MainModule.MMCr, "MM")
        ''ElseIf Gol = "Own" Then
        ''    bind.Add(MainModule.MMOwn, "MM")
        ''ElseIf Gol = "Job Order" Then
        ''    bind.Add(MainModule.MMJO, "MM")
        ''End If

        'Dim XR As New XRPOBJLkExt
        'XR.InitializeData(bind)
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

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Format(Me.DTPAwal.EditValue, "dd MMMM yyyy") = Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") Or Format(Me.DTPAkhir.EditValue, "dd MMMM yyyy") = Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") Or Me.DTPAwal.EditValue < Me.DTPTanggal.EditValue Or Me.DTPAkhir.EditValue < Me.DTPAwal.EditValue Then
            XtraMessageBox.Show("Tanggal Kirim Harus Diisi Dengan Benar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 1) <> Math.Round(CType(Me.GridView4.Columns("Qty").SummaryText, Decimal), 1) Then
            If XtraMessageBox.Show("Antara Qty dan Collect PO Tidak Balance, Apakah Tetap Mau Menyimpan Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
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
                Dim cmSP As New SqlCommand("SPInsT_POBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKrmAw", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@TglKrmAkh", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.TBJT.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@KrmKe", SqlDbType.VarChar).Value = Me.TBKirim.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotQtyPol", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("QtyPol").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("PsgPol").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Pembelian", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@PPIC", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@PPn", SqlDbType.Bit).Value = stsPPn
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_POBJLkDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@Upp", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Upp")
                                    .Parameters.Add("@Outs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Outs")
                                    .Parameters.Add("@Var", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Variasi")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                    .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                    .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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

                                If x <> 0 Then
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If

                            Dim z : For z = 0 To GridView3.DataRowCount - 1
                                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "ArtCode")) Then
                                    If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_POBJLkDtl2")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@POIDDtl", SqlDbType.VarChar).Value = IdD
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                            .Parameters.Add("@IsiDlmDos", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                            .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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
                                End If
                            Next

                            Dim w : For w = 0 To GridView4.DataRowCount - 1
                                If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "CollPOID")) Then
                                    If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView4.GetRowCellValue(w, "POIDD") Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_POBJCollPO")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@POIDD", SqlDbType.Int).Value = IdD
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "ArtCode")
                                            .Parameters.Add("@CollPOID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CollPOID")
                                            .Parameters.Add("@CollPOIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "CollPOIDD")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Qty")
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
                                End If
                            Next
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
                Dim cmSP As New SqlCommand("SPUpT_POBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKrmAw", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@TglKrmAkh", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.TBJT.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@KrmKe", SqlDbType.VarChar).Value = Me.TBKirim.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotQtyPol", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("QtyPol").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("PsgPol").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Pembelian", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@PPIC", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@PPn", SqlDbType.Bit).Value = stsPPn
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            Dim cmSPDel As New SqlCommand("SPDelT_POBJLkDtl")
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

                        Dim q : For q = 0 To arrParS2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_POBJLkDtl2")
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

                        Dim r : For r = 0 To arrParCP.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_POBJCollPO")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrParCP(r)
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
                                    Dim cmSPDtl As New SqlCommand("SPInsT_POBJLkDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Upp", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Upp")
                                        .Parameters.Add("@Outs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Outs")
                                        .Parameters.Add("@Var", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Variasi")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                        .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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

                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "POIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") * -1 Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_POBJLkDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDDtl", SqlDbType.VarChar).Value = IdD
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                                        .Parameters.Add("@IsiDlmDos", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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
                                                        Me.GridView3.SetRowCellValue(i, "POIDD", Me.GridView3.GetRowCellValue(i, "POIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        Else

                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JualID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_POBJLkDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "POIDD")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDDtl", SqlDbType.VarChar).Value = IdD
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                                        .Parameters.Add("@IsiDlmDos", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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


                                    'POBJCollPO
                                    Dim w : For w = 0 To GridView4.RowCount - 1
                                        If Me.GridView4.GetRowCellValue(w, "HubID") < 0 Then
                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CollPOID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView4.GetRowCellValue(w, "POIDD") * -1 Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_POBJCollPO")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = IdD
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "ArtCode")
                                                        .Parameters.Add("@CollPOID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CollPOID")
                                                        .Parameters.Add("@CollPOIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "CollPOIDD")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Qty")
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
                                                        Me.GridView4.SetRowCellValue(i, "POIDD", Me.GridView4.GetRowCellValue(i, "POIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        Else

                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CollPOID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView4.GetRowCellValue(w, "POIDD") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_POBJCollPO")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "HubID")
                                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = IdD
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "ArtCode")
                                                        .Parameters.Add("@CollPOID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CollPOID")
                                                        .Parameters.Add("@CollPOIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "CollPOIDD")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Qty")
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
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_POBJLkDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Upp", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Upp")
                                        .Parameters.Add("@Outs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Outs")
                                        .Parameters.Add("@Var", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Variasi")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                        .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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

                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "POIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_POBJLkDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                                        .Parameters.Add("@IsiDlmDos", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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
                                                        Me.GridView3.SetRowCellValue(i, "POIDD", Me.GridView3.GetRowCellValue(i, "POIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        Else

                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView3.GetRowCellValue(z, "POIDDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_POBJLkDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "POIDD")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                                        .Parameters.Add("@IsiDlmDos", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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


                                    'POBJCollPO
                                    Dim w : For w = 0 To GridView4.RowCount - 1
                                        If Me.GridView4.GetRowCellValue(w, "HubID") < 0 Then
                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CollPOID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView4.GetRowCellValue(w, "POIDD") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_POBJCollPO")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "ArtCode")
                                                        .Parameters.Add("@CollPOID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CollPOID")
                                                        .Parameters.Add("@CollPOIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "CollPOIDD")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Qty")
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
                                                        Me.GridView4.SetRowCellValue(i, "POIDD", Me.GridView4.GetRowCellValue(i, "POIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        Else

                                            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CollPOID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "POIDD") = Me.GridView4.GetRowCellValue(w, "POIDD") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_POBJCollPO")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "HubID")
                                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@POIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "ArtCode")
                                                        .Parameters.Add("@CollPOID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CollPOID")
                                                        .Parameters.Add("@CollPOIDD", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "CollPOIDD")
                                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Qty")
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

        Dim cmd2 As New SqlCommand("SPAftSPOBJLk")
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

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave
        Try
            If Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.Properties.ReadOnly = False Then
                Me.TBJT.EditValue = DsMaster.Tables("M_SuppLUE").Select("SuppID = '" & Me.SLUSupp.EditValue & "'")(0).Item("JT")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        If Me.GridView1.OptionsBehavior.Editable = True Then
            HitPPn()
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("POIDD")

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "POIDDtl") = Me.GridView1.GetFocusedDataRow.Item("POIDD") Then
                    ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                    arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(i, "POIDD")

                    Me.GridView3.DeleteRow(i)
                End If
            Next

            Dim x : For x = Me.GridView4.RowCount - 1 To 0 Step -1
                If Me.GridView4.GetRowCellValue(x, "POIDD") = Me.GridView1.GetFocusedDataRow.Item("POIDD") Then
                    ReDim Preserve arrParCP(arrParCP.GetUpperBound(0) + 1)
                    arrParCP(arrParCP.GetUpperBound(0)) = Me.GridView4.GetRowCellValue(x, "HubID")

                    Me.GridView4.DeleteRow(x)
                End If
            Next

        End If

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("ArtCode") Then

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "POIDDtl") = Me.GridView1.GetFocusedDataRow.Item("POIDD") Then
                    ReDim Preserve arrParS2(arrParS2.GetUpperBound(0) + 1)
                    arrParS2(arrParS2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(i, "POIDD")

                    Me.GridView3.DeleteRow(i)
                End If
            Next

            Dim x : For x = Me.GridView4.RowCount - 1 To 0 Step -1
                If Me.GridView4.GetRowCellValue(x, "POIDD") = Me.GridView1.GetFocusedDataRow.Item("POIDD") Then
                    ReDim Preserve arrParCP(arrParCP.GetUpperBound(0) + 1)
                    arrParCP(arrParCP.GetUpperBound(0)) = Me.GridView4.GetRowCellValue(x, "HubID")

                    Me.GridView4.DeleteRow(x)
                End If
            Next

            Dim ArtLepas As String
            Dim SplitKdD() As String

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Uk,Qty From M_BrgAssDtl Where AssID='" & dataTrans.Item("AssID").ToString & "'", koneksi)

            cmsl.TableMappings.Add("Table", "M_BrgAssDtlPO" & Gol)
            cmsl.Fill(DsMaster, "M_BrgAssDtlPO" & Gol)
            DsMaster.Tables("M_BrgAssDtlPO" & Gol).Clear()
            cmsl.Fill(DsMaster, "M_BrgAssDtlPO" & Gol)

            SplitKdD = CType(dataTrans.Item("Kode").ToString, String).Split("-")
            ArtLepas = dataTrans.Item("Kode").ToString.Remove(dataTrans.Item("Kode").ToString.Length - (SplitKdD(3).Length), SplitKdD(3).Length)

            Dim y : For y = 0 To DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows.Count - 1
                DsMaster.Tables("T_POBJLkDtl2" & Gol).Rows.Add((y + 1) * -1, Me.TBKode.EditValue, Me.GridView1.GetFocusedRowCellValue("POIDD"), ArtLepas + DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(y).Item("Uk"), DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(y).Item("Uk"), DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(y).Item("Qty"), 0, 0)
            Next

            Dim cmsl2 As SqlDataAdapter
            cmsl2 = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.CollPOID)*-1 as HubID," & Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") & " As POIDD,'" & Me.TBKode.EditValue & "' As POID,CollPOIDD,H.CollPOID,C.Nama As Cust,ArtCode,Qty-(Select Isnull((Select Sum(Qty) From T_POBJCollPO where CollPOIDD=D.CollPOIDD and POID<>'" & Me.TBKode.EditValue & "'),0)) As Qty From T_CollPO H Inner Join T_CollPODtl D On H.CollPOID=D.CollPOID Inner Join M_Cust C On C.CustID=H.CustID Where ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "'", koneksi)

            cmsl2.TableMappings.Add("Table", "T_POBJCollPO" & Gol)
            Try
                DsMaster.Tables("T_POBJCollPO" & Gol).Clear()

            Catch ex As Exception

            End Try
            cmsl2.Fill(DsMaster, "T_POBJCollPO" & Gol)

            Me.GridControl4.DataSource = DsMaster
            Me.GridControl4.DataMember = "T_POBJCollPO" & Gol

            Me.GridView1.RefreshData()

        ElseIf e.Column Is GridView1.Columns("Qty") Then
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "Psg") + Me.GridView1.GetRowCellValue(e.RowHandle, "PsgPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 4))

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "POIDDtl") = Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") Then
                    Me.GridView3.SetRowCellValue(i, "Qty", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView3.GetRowCellValue(i, "IsiDlmDos"))
                End If
            Next

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        ElseIf e.Column Is GridView1.Columns("QtyPol") Then
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPol", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "Psg") + Me.GridView1.GetRowCellValue(e.RowHandle, "PsgPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 4))

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "POIDDtl") = Me.GridView1.GetRowCellValue(e.RowHandle, "POIDD") Then
                    Me.GridView3.SetRowCellValue(i, "QtyPol", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol") * Me.GridView3.GetRowCellValue(i, "IsiDlmDos"))
                End If
            Next
            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        ElseIf e.Column Is GridView1.Columns("HarSat") Then
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "Psg") + Me.GridView1.GetRowCellValue(e.RowHandle, "PsgPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 4))

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        End If

    End Sub
    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            If Me.GridView1.RowCount > 0 Then
                Me.GridView3.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
                Me.GridView4.ActiveFilterString = "[POIDD] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
            End If

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
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            'If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "POIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Upp", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Outs", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Variasi", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPol", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DosPol", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPol", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HCBP", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "JmlBOM", 0)


            AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged
            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            'End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdArtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdArtCode.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdArtCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdArtCode.ButtonClick
        Dim frm As New FSearch("M_Brg", Gol, "", "", Date.Now, "")
        frm.ShowDialog()
        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AssID", dataTrans.Item("AssID").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        If Me.GridView1.RowCount > 0 Then
            If Me.GridView3.RowCount > 0 Then
                Me.GridView3.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"

            End If
        End If
    End Sub

    Private Sub GridControl4_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl4.EmbeddedNavigator.ButtonClick

        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrParCP(arrParCP.GetUpperBound(0) + 1)
            arrParCP(arrParCP.GetUpperBound(0)) = Me.GridView4.GetFocusedDataRow.Item("HubID")
        End If

    End Sub

    Private Sub GridView4_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView4.CellValueChanged

        If e.Column Is GridView4.Columns("Qty") Then
            Dim Stok As Integer
            Dim command As New SqlCommand("Select Qty-(Select Isnull((Select Sum(Qty) From T_POBJCollPO Where CollPOIDD=T_CollPODtl.CollPOIDD and POID <> '" & Me.TBKode.EditValue & "'),0)) From T_CollPODtl Where CollPOID='" & Me.GridView4.GetRowCellValue(e.RowHandle, "CollPOID") & "' and CollPOIDD=" & Me.GridView4.GetRowCellValue(e.RowHandle, "CollPOIDD") & "", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView4.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView4.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If

        End If
    End Sub
    Private Sub BEdCollPOID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdCollPOID.ButtonClick
        RemoveHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

        Dim frm As New FSearch("Collect PO", Me.TBKode.EditValue, Me.GridView1.GetFocusedDataRow.Item("ArtCode"), "", Date.Now, "")
        frm.ShowDialog()
        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView4.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "CollPOIDD", dataTrans.Item("CollPOIDD").ToString)
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Cust", dataTrans.Item("Nama").ToString)
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "ArtCode", dataTrans.Item("ArtCode").ToString)
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Qty", dataTrans.Item("Qty").ToString)
            End If

        Catch ex As Exception

        End Try

        AddHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

    End Sub

    Private Sub GridView4_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView4.InitNewRow
        Try
            RemoveHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

            Me.GridView4.SetRowCellValue(e.RowHandle, "HubID", Me.GridView4.RowCount * -1)
            Me.GridView4.SetRowCellValue(e.RowHandle, "POID", Me.TBKode.EditValue)
            Me.GridView4.SetRowCellValue(e.RowHandle, "POIDD", Me.GridView1.GetFocusedDataRow.Item("POIDD"))
            Me.GridView4.SetRowCellValue(e.RowHandle, "Qty", 0)

            AddHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPOBJLk_d(Me.GridView2.GetFocusedDataRow.Item("POID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBKirim.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub


    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Upp" Or view.FocusedColumn.FieldName = "Outs" Or view.FocusedColumn.FieldName = "Variasi" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
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
End Class