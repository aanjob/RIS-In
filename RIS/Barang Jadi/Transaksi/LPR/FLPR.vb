Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FLPR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama, CodeID, JnsLama, CurrID, Gol As String
    Dim Manual, MnlInsUpd, stsPPn As Boolean
    Dim rw As Integer = 0
    Dim JT As Integer
    Dim Ongkir As Decimal

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("LPRN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("LPREd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("LPRApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("LPRDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTLPR_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUSales.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.CBOJnsRtr.Properties.ReadOnly = True
        Me.CBOJnsFt.Properties.ReadOnly = True
        Me.CBOMetode.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        'Me.TBTamDiscP.Properties.ReadOnly = True
        'Me.TBTamDiscRp.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTLPR_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        'Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUSales.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.CBOJnsRtr.Properties.ReadOnly = False
        Me.CBOJnsFt.Properties.ReadOnly = False
        Me.CBOMetode.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        'Me.TBTamDiscP.Properties.ReadOnly = False
        'Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTLPR_e.Selected = True
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

        cmsl = New SqlDataAdapter("Select UC.CabID,C.Cabang From M_UsCab UC Inner Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsMaster.Tables("M_UsCabLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabLUE" & Gol)

        Me.SLUCab.Properties.DataSource = DsMaster.Tables("M_UsCabLUE" & Gol)
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"

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

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select LPRIDD,LPRID,JualID,JualIDD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,IsiAsDos,HarDPJ,HarSat, Qty,Dos,Psg,stsHarga,HarSbDisc,RpDiscCust,DiscOB,RpDiscOB,RpDiscL,OngkirSat,Ongkir,HarAkhir,DiscGlbSat,RpDiscGlb From T_LPRDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where LPRID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_LPRDtl" & Gol)
        Try
            DsMaster.Tables("T_LPRDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_LPRDtl" & Gol)

        DsMaster.Tables("T_LPRDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_LPRDtl" & Gol).Columns("JualID"), DsMaster.Tables("T_LPRDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_LPRDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select LPRID,PeriodID,CodeID,JnsTrans,JnsRtr,Metode,H.CabID,Cb.Cabang,H.SalID,S.Nama As Sales,H.GdID, G.Nama as Gudang,H.CustID,C.Nama As Customer,C.Alamat,K.Nama As Kota,H.JnsCustID,H.Harga,Tanggal,MtUang,CurrID,NilTukarRp,H.DiscCust,Kat,TipePPn, PersenPPn,TotSbDisc,TotOngkir,TotDPP,TotPPn,TotRpDiscCust,TotRpDiscOB,TotRpDiscL,TotRpDiscGlb,DiscP,RpDiscP,DiscRp,TotAkhir,TotQty, TotDos,TotPsg, H.Ket,H.Grup,H.Gol,H.stsPPn,H.stsApp,H.stsRtr,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_LPR H Inner Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Sales S On H.SalID=S.SalID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Gudang G On H.GdID=G.GdID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By LPRID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_LPR" & Gol)
        Try
            DsMaster.Tables("T_LPR" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_LPR" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_LPR" & Gol
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTotDiscGlb.EditValue - Me.TBTamDiscPRp.EditValue - Me.TBTamDiscRp.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then

            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            'Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * 10 / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTotDiscGlb.EditValue - Me.TBTamDiscPRp.EditValue - Me.TBTamDiscRp.EditValue + Me.TBTotPPn.EditValue
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_LPR")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = "DelEr"
            .Parameters.Add("@Kode", SqlDbType.Int)
            .Parameters("@Kode").Direction = ParameterDirection.Output
            .Connection = koneksi

            Try
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Kode").Value
                    .Close()
                End With

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Public Sub DelXml()
        If IO.File.Exists("SrHarga.xml") Then
            System.IO.File.Delete("SrHarga.xml")
        End If
    End Sub

    Private Sub FLPR_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Laporan Penerimaan Retur"
    End Sub

    Private Sub FLPR_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()
        CekSave = False
    End Sub

    Private Sub FLPRBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FLPR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTLPR_e.Selected = True
    End Sub

    Private Sub BVTLPR_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTLPR_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Laporan Penerimaan Retur"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("LPRP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Laporan Penerimaan Retur"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
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

        Me.TBKode.EditValue = "--"
        Me.SLUGrup.EditValue = ""
        Me.SLUCab.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.CBOJnsFt.EditValue = "Non Spreading"
        Me.CBOJnsRtr.EditValue = "Penjualan"
        Me.CBOMetode.EditValue = "With Faktur"
        Me.SLUJnsCust.EditValue = ""
        Me.TBHarga.EditValue = ""
        Me.TBDiscCust.EditValue = 0
        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOKat.EditValue = "Lokal"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTotDiscOB.EditValue = 0.0
        Me.TBTotDiscCust.EditValue = 0.0
        Me.TBTotDiscL.EditValue = 0.0
        Me.TBTotDiscGlb.EditValue = 0.0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0.0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_LPRDtl" & Gol).Clear()
        ReDim arrPar(-1)
        CekCurr()

        If MainModule.BackDate = True Then
            Me.DTPTanggal.Properties.ReadOnly = False
        Else
            Me.DTPTanggal.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Laporan Penerimaan Retur"

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        DelXml()

        If SlCek("T_LPR", "stsApp", "LPRID", Me.GridView2.GetFocusedDataRow.Item("LPRID")) = False Then
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("LPRID")
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and Pusat='False' and G.Aktif='True' and CG.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            Try
                DsMaster.Tables("M_GudangLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"

            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")
            Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")

            If Not IsDBNull(Me.SLUCust.EditValue) Then
                cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_JnsCustRt" & Gol)
                cmsl.Fill(DsMaster, "M_JnsCustRt" & Gol)
                DsMaster.Tables("M_JnsCustRt" & Gol).Clear()
                cmsl.Fill(DsMaster, "M_JnsCustRt" & Gol)

                Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustRt" & Gol)
                Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"

            End If

            If Not IsDBNull(Me.SLUCab.EditValue) Then

                If Gol = "Job Order" Then
                    cmsl = New SqlDataAdapter("Select S.SalID,Area,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
                    Try
                        DsMaster.Tables("M_SalesLUE" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
                Else
                    cmsl = New SqlDataAdapter("Select S.SalID,Area,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='Lokal' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
                    Try
                        DsMaster.Tables("M_SalesLUE" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
                End If

                Me.SLUSales.Properties.DataSource = DsMaster.Tables("M_SalesLUE" & Gol)
                Me.SLUSales.Properties.DisplayMember = "Nama"
                Me.SLUSales.Properties.ValueMember = "SalID"

                cmsl = New SqlDataAdapter("Select C.CustID,Nama,JnsCustID,DiscCust,JT From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustRt" & Gol)
                cmsl.Fill(DsMaster, "M_CustRt" & Gol)

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustRt" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"


                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Gol='" & Gol & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE")
                cmsl.Fill(DsMaster, "M_GudangLUE")

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"
            End If

            Me.SLUSales.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
            Me.CBOJnsFt.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            JnsLama = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            Me.CBOJnsRtr.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsRtr")
            Me.CBOMetode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Metode")
            Me.SLUJnsCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsCustID")
            Me.TBHarga.EditValue = Me.GridView2.GetFocusedDataRow.Item("Harga")
            Me.TBDiscCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscCust")
            Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
            Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
            Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
            stsPPn = Me.GridView2.GetFocusedDataRow.Item("stsPpn")
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
            Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
            Me.TBTotDiscOB.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB")
            Me.TBTotDiscCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust")
            Me.TBTotDiscL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL")
            Me.TBTotDiscGlb.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscGlb")
            Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
            Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
            Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
            Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
            Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
            Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

            Dim Reader As SqlClient.SqlDataReader
            If Not IsDBNull(Me.SLUCab.EditValue) Then
                Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=49 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

                With koneksi
                    .Open()
                    Reader = command.ExecuteReader

                    If Reader.HasRows Then
                        While Reader.Read
                            If IsDBNull(Reader.Item(0)) = True Then
                                Manual = False
                                CodeID = ""
                            Else
                                Manual = Reader.Item(0)
                                CodeID = Reader.Item(1)
                            End If
                        End While
                    End If
                    Reader.Close()
                    .Close()
                End With
            End If

            FillDtl(Me.TBKode.EditValue)
            ReDim arrPar(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            Me.SLUCab.Properties.ReadOnly = True
            Me.SLUCust.Properties.ReadOnly = True

            If MainModule.BackDate = True Then
                Me.DTPTanggal.Properties.ReadOnly = False
            Else
                Me.DTPTanggal.Properties.ReadOnly = True
            End If
        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        If SlCek("T_LPR", "stsApp", "LPRID", Me.GridView2.GetFocusedDataRow.Item("LPRID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.BackDate = True Then
            Dim cmSP As New SqlCommand("SPAppLPRBackDate")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@KdLPR", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("LPRID")
                .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabID")
                .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
                .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
                .Parameters.Add("@JnsRtr", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsRtr")
                .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CustID")
                .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsCustID")
                .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MtUang")
                .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Gol")
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdID")
                .Parameters.Add("@Posisi", SqlDbType.VarChar).Value = MainModule.Posisi
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
                        XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    ElseIf x = 2 Then
                        XtraMessageBox.Show("Anda Tidak Diijinkan Untuk Approve", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        Else
            Dim cmSP As New SqlCommand("SPAppLPR")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@KdLPR", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("LPRID")
                .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabID")
                .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
                .Parameters.Add("@JnsRtr", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsRtr")
                .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CustID")
                .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsCustID")
                .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MtUang")
                .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Gol")
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdID")
                .Parameters.Add("@Posisi", SqlDbType.VarChar).Value = MainModule.Posisi
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
                        XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    ElseIf x = 2 Then
                        XtraMessageBox.Show("Anda Tidak Diijinkan Untuk Approve", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If



    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Laporan Penerimaan Retur"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_LPR", "stsApp", "LPRID", Me.GridView2.GetFocusedDataRow.Item("LPRID")) = False Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("LPRID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_LPR")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("LPRID")
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
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        koneksi.Close()

        Dim Kode As String
        Kode = Me.GridView2.GetFocusedDataRow.Item("LPRID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "LPRID") = Kode Then
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("LPRID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotSbDisc"), "TotSbDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB"), "TotRpDiscOB")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust"), "TotRpDiscCust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscGlb"), "TotRpDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRLPR
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUSales.EditValue = "" Or IsDBNull(Me.SLUSales.EditValue) Then
            XtraMessageBox.Show("Sales Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.MKet.EditValue = "" Or IsDBNull(Me.MKet.EditValue) Then
            XtraMessageBox.Show("Keterangan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscOB.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscCust.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscGlb.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscGlb").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

        If Me.RBPPn.EditValue <> "Non PPn" Then
            stsPPn = True
        Else
            stsPPn = False
        End If

        HitPPn()

        JT = DsMaster.Tables("M_CustRt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_LPR")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 49
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@JnsRtr", SqlDbType.VarChar).Value = Me.CBOJnsRtr.EditValue
                    .Parameters.Add("@Metode", SqlDbType.VarChar).Value = Me.CBOMetode.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.SLUJnsCust.Text
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Ongkir
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    '.Parameters.Add("@JT", SqlDbType.Int).Value = JT
                    '.Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotOngkir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Ongkir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotRpDiscCust", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscOB", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscL", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscGlb", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscGlb").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_LPRDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                    .Parameters.Add("@JualIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
                                    .Parameters.Add("@IsiAsDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiAsDos")
                                    .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                    .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                    .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                    .Parameters.Add("@RpDiscL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscL")
                                    .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                    .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                    .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@DiscGlbSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscGlbSat")
                                    .Parameters.Add("@RpDiscGlb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscGlb")
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
                Dim cmSP As New SqlCommand("SPUpT_LPR")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 49
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@JnsRtr", SqlDbType.VarChar).Value = Me.CBOJnsRtr.EditValue
                    .Parameters.Add("@Metode", SqlDbType.VarChar).Value = Me.CBOMetode.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.SLUJnsCust.Text
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Ongkir
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotOngkir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Ongkir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotRpDiscCust", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscOB", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscL", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscGlb", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscGlb").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@stsPPn", SqlDbType.Bit).Value = stsPPn
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
                            Dim cmSPDel As New SqlCommand("SPDelT_LPRDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "LPRIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_LPRDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                        .Parameters.Add("@JualIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
                                        .Parameters.Add("@IsiAsDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiAsDos")
                                        .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                        .Parameters.Add("@RpDiscL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscL")
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@DiscGlbSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscGlbSat")
                                        .Parameters.Add("@RpDiscGlb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscGlb")
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
                                        Me.GridView1.SetRowCellValue(i, "LPRIDD", Me.GridView1.GetRowCellValue(i, "LPRIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_LPRDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "LPRIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                        .Parameters.Add("@JualIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
                                        .Parameters.Add("@IsiAsDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiAsDos")
                                        .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                        .Parameters.Add("@RpDiscL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscL")
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@DiscGlbSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscGlbSat")
                                        .Parameters.Add("@RpDiscGlb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscGlb")
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

        Dim cmd2 As New SqlCommand("SPAftSLPR")
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

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Me.SLUGrup.Properties.ReadOnly = False Then
            If Me.SLUCab.EditValue <> "" And Not IsDBNull(Me.SLUCab.EditValue) Then
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                Try
                    DsMaster.Tables("M_GudangLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"
            End If

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        Try
            If Not IsDBNull(Me.SLUCab.EditValue) And Me.SLUCab.Properties.ReadOnly = False Then
                Dim Reader As SqlClient.SqlDataReader
                Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=49 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

                Dim cmsl As SqlDataAdapter

                If Gol = "Job Order" Then
                    cmsl = New SqlDataAdapter("Select S.SalID,Area,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
                    Try
                        DsMaster.Tables("M_SalesLUE" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
                Else
                    cmsl = New SqlDataAdapter("Select S.SalID,Area,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='Lokal' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
                    Try
                        DsMaster.Tables("M_SalesLUE" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
                End If

                Me.SLUSales.Properties.DataSource = DsMaster.Tables("M_SalesLUE" & Gol)
                Me.SLUSales.Properties.DisplayMember = "Nama"
                Me.SLUSales.Properties.ValueMember = "SalID"

                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota,JnsCustID,DiscCust,JT,Harga From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustRt" & Gol)
                cmsl.Fill(DsMaster, "M_CustRt" & Gol)
                DsMaster.Tables("M_CustRt" & Gol).Clear()
                cmsl.Fill(DsMaster, "M_CustRt" & Gol)

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustRt" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"

                cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE")
                cmsl.Fill(DsMaster, "M_GudangLUE")
                DsMaster.Tables("M_GudangLUE").Clear()
                cmsl.Fill(DsMaster, "M_GudangLUE")

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"

                Me.SLUCust.EditValue = ""
                Me.SLUGd.EditValue = ""

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.Properties.ReadOnly = False Then
            Try
                If Not IsDBNull(Me.SLUCust.EditValue) Then
                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_JnsCustRt" & Gol)
                    cmsl.Fill(DsMaster, "M_JnsCustRt" & Gol)
                    DsMaster.Tables("M_JnsCustRt" & Gol).Clear()
                    cmsl.Fill(DsMaster, "M_JnsCustRt" & Gol)

                    Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustRt" & Gol)
                    Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                    Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"

                    Me.SLUJnsCust.EditValue = DsMaster.Tables("M_CustRt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JnsCustID")
                    Me.TBHarga.EditValue = DsMaster.Tables("M_CustRt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("Harga")
                    JT = DsMaster.Tables("M_CustRt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")

                    If Me.TBHarga.EditValue = "DPJ" Or Me.TBHarga.EditValue = "JO" Then
                        Ongkir = 0
                    Else
                        Ongkir = MainModule.OngkosKirim
                    End If

                    Me.TBDiscCust.EditValue = DsMaster.Tables("M_CustRt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("DiscCust")

                End If
            Catch ex As Exception

            End Try

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
        'MsgBox(Me.SLUCust.EditValue)
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub CBOMetode_Leave(sender As Object, e As EventArgs) Handles CBOMetode.Leave
        If Me.CBOMetode.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub CBOJnsFt_Leave(sender As Object, e As EventArgs) Handles CBOJnsFt.Leave
        If Me.CBOJnsFt.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("LPRIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            If Me.CBOMetode.EditValue = "Non Faktur" Then
                Dim frm As New FLPRNF_a(Me.SLUJnsCust.Text, Gol, Me.SLUGrup.EditValue, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue, Ongkir)
                frm.ShowDialog()

            ElseIf Me.CBOMetode.EditValue = "With Faktur" Then
                'MsgBox(Me.TBKode.EditValue)
                Dim frm As New FLPRWF_a(Me.SLUCust.EditValue, Gol, Me.SLUGrup.EditValue, Me.TBKode.EditValue, Me.RBPPn.EditValue, Me.TBPersenPPn.EditValue)
                frm.ShowDialog()

            End If

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
        Try
            If e.Column Is GridView1.Columns("Qty") Then
                If Me.CBOMetode.EditValue = "With Faktur" Then
                    Dim SdhLPRP, SdhLPRD, QtyJ, QtyP, QtyD, QtyAss, TotLPR, SisaAkh As Integer

                    Dim comm As New SqlCommand("Select Isnull(Sum(Qty),0) From T_JualBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & "", koneksi)

                    With koneksi
                        .Open()
                        QtyJ = comm.ExecuteScalar()
                        .Close()
                    End With

                    comm = New SqlCommand("Select Isnull(Sum(Qty),0) +(Select Isnull((Select Sum(Qty) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And H.LPRID <>'" & Me.TBKode.EditValue & "' and SatID <> 'P' and H.stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And RtrID <>'" & Me.TBKode.EditValue & "' and SatID <> 'P'", koneksi)

                    'comm = New SqlCommand("Select Isnull(Sum(Qty),0) +(Select Isnull((Select Sum(Qty) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' And H.LPRID <>'" & Me.TBKode.EditValue & "' and SatID <> 'P' and H.stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "'   And RtrID <>'" & Me.TBKode.EditValue & "' and SatID <> 'P'", koneksi)

                    With koneksi
                        .Open()
                        SdhLPRD = comm.ExecuteScalar()
                        .Close()
                    End With

                    If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                        comm = New SqlCommand("Select Isnull(Sum(Qty),0)+(Select Isnull((Select Sum(Qty) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' And H.LPRID <>'" & Me.TBKode.EditValue & "' and SatID= 'P' and H.stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' And RtrID <>'" & Me.TBKode.EditValue & "' and SatID = 'P'", koneksi)

                        With koneksi
                            .Open()
                            SdhLPRP = comm.ExecuteScalar()
                            .Close()
                        End With

                        Dim x : For x = 0 To Me.GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView1.GetRowCellValue(x, "JualIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "LPRIDD") <> Me.GridView1.GetRowCellValue(x, "LPRIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") = Me.GridView1.GetRowCellValue(x, "ArtCode") Then
                                QtyP += Me.GridView1.GetRowCellValue(x, "Qty")
                            End If

                            If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView1.GetRowCellValue(x, "JualIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "LPRIDD") <> Me.GridView1.GetRowCellValue(x, "LPRIDD") And Me.GridView1.GetRowCellValue(x, "SatID") <> "P" Then
                                QtyD += Me.GridView1.GetRowCellValue(x, "Qty")
                            End If

                        Next

                        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > ((QtyJ - QtyD) * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) - ((SdhLPRD * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) + SdhLPRP + QtyP) Then

                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", ((QtyJ - QtyD) * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) - ((SdhLPRD * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) + SdhLPRP + QtyP))
                        End If

                    Else
                        'Dos
                        QtyAss = 0
                        SisaAkh = 0
                        comm = New SqlCommand("Select Case When Isnull(max(Qty),0) % IsiDlmDos >0 Then Isnull(max(Qty),0)/IsiDlmDos+1 Else Isnull(max(Qty),0)/IsiDlmDos End  From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And RtrID <>'" & Me.TBKode.EditValue & "' and SatID = 'P'  Group By IsiDlmDos Union All Select Case When Isnull(max(Qty),0) % IsiDlmDos >0 Then Isnull(max(Qty),0)/IsiDlmDos+1 Else Isnull(max(Qty),0)/IsiDlmDos End  From T_LPRDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And LPRID <>'" & Me.TBKode.EditValue & "' and SatID = 'P'  Group By IsiDlmDos", koneksi)

                        'comm = New SqlCommand("Select Sum(Psg) From(Select Isnull((Select Ceiling((Sum(Psg)*1.0)/max (IsiAsDos)) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And RtrID <>'" & Me.TBKode.EditValue & "'),0) As Psg Union All Select Isnull((Select Ceiling((Sum(Psg)*1.0)/max (IsiAsDos)) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And H.LPRID <>'" & Me.TBKode.EditValue & "' and stsRtr='False'),0) As Psg )As x", koneksi)

                        With koneksi
                            .Open()
                            SdhLPRP = comm.ExecuteScalar()
                            .Close()
                        End With

                        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > QtyJ - SdhLPRD - SdhLPRP - TotLPR Then
                            XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", QtyJ - SdhLPRD - SdhLPRP - TotLPR)
                        End If
                    End If
                End If

                Me.GridView1.SetRowCellValue(e.RowHandle, "Ongkir", Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirSat") * Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))

                Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

                If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
                Else
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
                End If

                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir")) * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

                Dim DiscL As Decimal
                Dim comm2 As New SqlCommand("Select Isnull((Select RpDiscL/(Qty*Isi) From T_JualBJDtl where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and JualIDD='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & "'),0)", koneksi)

                With koneksi
                    .Open()
                    DiscL = comm2.ExecuteScalar()
                    .Close()
                End With

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscL", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") * DiscL)

                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscL"), 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscGlb", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscGlbSat"), 2))


            ElseIf e.Column Is GridView1.Columns("HarSat") Then

                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir")) * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscL"), 2))

                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscGlb", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscGlbSat"), 2))

            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If Me.GridView1.OptionsBehavior.Editable = True Then

            If Me.CBOMetode.EditValue = "Non Faktur" Then
                Dim frm As New FSearch("JualBJ", Me.SLUJnsCust.Text, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue)
                frm.ShowDialog()

                'Try
                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsHarga", dataTrans.Item("stsHarga").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", dataTrans.Item("Harga").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DiscOB", dataTrans.Item("DiscOB").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "OngkirSat", Ongkir * Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi"))
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 1)
                End If

                'Catch ex As Exception

                'End Try
            End If

        End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow

        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then

                Me.GridView1.SetRowCellValue(e.RowHandle, "LPRIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "LPRID", Me.TBKode.EditValue)
                'If Me.CBOJnsRt.EditValue = "With Faktur" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "JualIDD", dataTrans.Item("JualIDD" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "JualID", dataTrans.Item("JualID" & rw).ToString)
                'End If
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "IsiDlmDos", dataTrans.Item("IsiDlmDos" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "IsiAsDos", dataTrans.Item("IsiAsDos" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "stsHarga", dataTrans.Item("stsHarga" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarDPJ", dataTrans.Item("HarDPJ" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscOB", dataTrans.Item("DiscOB" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscL", dataTrans.Item("RpDiscL" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "OngkirSat", Ongkir * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscGlbSat", dataTrans.Item("DiscGlbSat" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscGlb", dataTrans.Item("RpDiscGlb" & rw).ToString)

                'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", dataTrans.Item("Harga" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)

                'AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                rw += 1


            End If

        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
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

        If Me.CBOMetode.EditValue = "With Faktur" Then
            If Me.CBOMetode.Properties.ReadOnly = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If
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
        If Me.CBOMetode.EditValue = "With Faktur" Then
            If Me.CBOMetode.Properties.ReadOnly = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "LPRIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If
        End If

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
        Try
            If Me.GridView1.OptionsBehavior.Editable = True Then
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscOB.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscCust.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscGlb.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscGlb").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

                HitPPn()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TBTamDiscP_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscP.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub

    Private Sub TBTamDiscRp_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscRp.EditValueChanged
        HitPPn()
    End Sub


    Private Sub GridControl2_DoubleClick(sender As Object, e As EventArgs) Handles GridControl2.DoubleClick
        Try
            Dim frm As New FLPR_d(Me.GridView2.GetFocusedDataRow.Item("LPRID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView2.GetRowCellValue(e.RowHandle, "TotAkhir") > 1000000 And Me.GridView2.GetRowCellValue(e.RowHandle, "stsApp") = False And Me.GridView2.GetRowCellValue(e.RowHandle, "JnsRtr") = "Penjualan" Then
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "NamaLain" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

End Class