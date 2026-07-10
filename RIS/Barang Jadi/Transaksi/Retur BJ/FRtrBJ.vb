Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FRtrBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama, Area, CodeID, LPRIDLama, JnsLama, UrutArea, UrutCust, CurrID, Gol As String
    Dim Manual, MnlInsUpd, stsPPn As Boolean
    Dim rw As Integer = 0
    Dim JT As Integer
    Dim Ongkir As Decimal

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=16 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("RtrBJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("RtrBJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("RtrBJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTRetur_s.Enabled = True

        Me.TBID.Properties.ReadOnly = True
        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.TBPajak.Properties.ReadOnly = True
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
        Me.SLULPRID.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.TBReImp.Properties.ReadOnly = True
        Me.DTPTglReImp.Properties.ReadOnly = True
        Me.TBBL.Properties.ReadOnly = True
        Me.DTPTglBL.Properties.ReadOnly = True
        Me.TBLC.Properties.ReadOnly = True
        Me.TBTamDiscP.Properties.ReadOnly = True
        Me.TBTamDiscRp.Properties.ReadOnly = True

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
        Me.BVBExit.Enabled = False
        Me.BVTRetur_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.TBPajak.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
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
        Me.SLULPRID.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBReImp.Properties.ReadOnly = False
        Me.DTPTglReImp.Properties.ReadOnly = False
        Me.TBBL.Properties.ReadOnly = False
        Me.DTPTglBL.Properties.ReadOnly = False
        Me.TBLC.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTRetur_e.Selected = True
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
        cmsl = New SqlDataAdapter("Select RtrIDD,RtrID,JualID,JualIDD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,IsiAsDos,HarDPJ,HarSat, Qty,Dos,Psg,stsHarga,HarSbDisc,RpDiscCust,DiscOB,RpDiscOB,RpDiscL,OngkirSat,Ongkir,HarAkhir,NamaLain,DiscGlbSat,RpDiscGlb From T_RtrBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where RtrID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrBJDtl" & Gol)
        Try
            DsMaster.Tables("T_RtrBJDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrBJDtl" & Gol)

        DsMaster.Tables("T_RtrBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrBJDtl" & Gol).Columns("JualID"), DsMaster.Tables("T_RtrBJDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RtrBJDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrID,PeriodID,Kode,LPRID,JnsTrans,JnsRtr,Metode,NoPajak,H.CabID,Cb.Cabang,H.SalID,S.Nama As Sales, UrutArea,H.GdID,G.Nama as Gudang,H.CustID,C.Nama As Customer,C.Alamat,K.Nama As Kota,UrutCust,H.JnsCustID,H.Harga,Tanggal,DueDate,MtUang, CurrID, NilTukarRp,H.DiscCust,Kat,TipePPn,PersenPPn,ReImp,TglReImp,BL,TglBL,LC,TotSbDisc,TotOngkir,TotDPP,TotPPn,TotRpDiscCust, TotRpDiscOB,TotRpDiscL,TotRpDiscGlb,DiscP,RpDiscP,DiscRp,TotAkhir,SisaPakai,TotQty,TotDos,TotPsg,H.Ket,H.Grup,H.Gol,H.stsPPn,H.Autoo, H.stsPakai,stsPrint, H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.PrintDate,H.PrintBy From T_RtrBJ H Inner Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Sales S On H.SalID=S.SalID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Gudang G On H.GdID=G.GdID and PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By RtrID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrBJ" & Gol)
        Try
            DsMaster.Tables("T_RtrBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrBJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_RtrBJ" & Gol
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTotDiscGlb.EditValue - Me.TBTamDiscPRp.EditValue - Me.TBTamDiscRp.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then

            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTotDiscGlb.EditValue - Me.TBTamDiscPRp.EditValue - Me.TBTamDiscRp.EditValue + Me.TBTotPPn.EditValue
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_RtrBJ")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
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
        If IO.File.Exists("SrHarga.xml") Then
            System.IO.File.Delete("SrHarga.xml")
        End If
    End Sub

    Private Sub FRtrBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Retur Penjualan Barang Jadi"
    End Sub

    Private Sub FRtrBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        CekSave = False

        Me.Dispose()
    End Sub

    Private Sub FRtrBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FRtrBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTRetur_e.Selected = True
    End Sub

    Private Sub BVTRetur_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTRetur_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Retur Penjualan Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("RtrBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Retur Penjualan Barang Jadi"

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
            Me.TBID.Properties.ReadOnly = False
            Me.TBID.EditValue = ""
        Else
            Me.TBID.Properties.ReadOnly = True
            Me.TBID.EditValue = "--"
        End If

        Me.TBKode.EditValue = "--"
        Me.SLUGrup.EditValue = ""
        Me.SLULPRID.EditValue = ""
        Me.SLUCab.EditValue = ""
        Me.TBPajak.EditValue = ""
        Me.SLUSales.EditValue = ""
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
        Me.TBReImp.EditValue = ""
        Me.DTPTglReImp.EditValue = Date.Now
        Me.TBBL.EditValue = ""
        Me.DTPTglBL.EditValue = Date.Now
        Me.TBLC.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTotDiscOB.EditValue = 0
        Me.TBTotDiscCust.EditValue = 0
        Me.TBTotDiscL.EditValue = 0
        Me.TBTotDiscGlb.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBID.EditValue)
        DsMaster.Tables("T_RtrBJDtl" & Gol).Clear()
        ReDim arrPar(-1)

        CekCurr()

        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1

        If Gol = "Job Order" Then
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
            Me.GridColumn98.OptionsColumn.AllowEdit = False

        Else
            If Manual = False Then
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
                Me.GridColumn98.OptionsColumn.AllowEdit = False
            Else
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                Me.GridColumn98.OptionsColumn.AllowEdit = True
            End If
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Retur Penjualan Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        If SlCek("T_RtrBJ", "stsPakai", "RtrID", Me.GridView2.GetFocusedDataRow.Item("RtrID")) = False And SlCek("T_RtrBJ", "TotAkhir", "RtrID", Me.GridView2.GetFocusedDataRow.Item("RtrID")) = SlCek("T_RtrBJ", "SisaPakai", "RtrID", Me.GridView2.GetFocusedDataRow.Item("RtrID")) Then
            LUE()

            Indicator = "200"
            Me.TBID.EditValue = Me.GridView2.GetFocusedDataRow.Item("RtrID")
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kode")
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and Pusat='False' and G.Aktif='True' and CG.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            Try
                DsMaster.Tables("M_GudangLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"

            cmsl = New SqlDataAdapter("Select LPRID,JnsTrans,JnsRtr,SalID,GdID,H.CustID,C.Nama As Cust,MtUang,CurrID,Kat,TipePPn,PersenPPn, TotSbDisc,TipePPn,TotSbDisc,TotOngkir,TotDPP,TotPPn,TotRpDiscCust,TotRpDiscOB,DiscP,RpDiscP,DiscRp,TotAkhir,Grup From T_LPR H Inner Join M_Cust C On H.CustID=C.CustID Where CabID='" & Me.SLUCab.EditValue & "' and stsRtr='False'or LPRID = (Select LPRID From T_RtrBJ where RtrID ='" & Me.TBID.EditValue & "')", koneksi)
            cmsl.TableMappings.Add("Table", "T_LPR" & Gol)
            cmsl.Fill(DsMaster, "T_LPR" & Gol)
            DsMaster.Tables("T_LPR" & Gol).Clear()
            cmsl.Fill(DsMaster, "T_LPR" & Gol)

            Me.SLULPRID.Properties.DataSource = DsMaster.Tables("T_LPR" & Gol)
            Me.SLULPRID.Properties.DisplayMember = "LPRID"
            Me.SLULPRID.Properties.ValueMember = "LPRID"

            If Gol = "Job Order" Then
                cmsl = New SqlDataAdapter("Select S.SalID,Area,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
                Try
                    DsMaster.Tables("M_SalesLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
            Else
                cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='Lokal' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
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

            Me.SLULPRID.EditValue = Me.GridView2.GetFocusedDataRow.Item("LPRID")
            LPRIDLama = Me.GridView2.GetFocusedDataRow.Item("LPRID")
            Me.TBPajak.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoPajak")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUSales.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
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

            If Not IsDBNull(Me.SLUSales.EditValue) Then

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

            Me.CBOJnsFt.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            JnsLama = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            Me.CBOJnsRtr.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsRtr")
            Me.CBOMetode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Metode")
            'Area = Me.GridView2.GetFocusedDataRow.Item("Area")
            UrutArea = Me.GridView2.GetFocusedDataRow.Item("UrutArea")
            UrutCust = Me.GridView2.GetFocusedDataRow.Item("UrutCust")
            Me.SLUJnsCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsCustID")
            Me.TBHarga.EditValue = Me.GridView2.GetFocusedDataRow.Item("Harga")
            Me.TBDiscCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscCust")
            Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
            Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
            Me.CBOKat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kat")
            stsPPn = Me.GridView2.GetFocusedDataRow.Item("stsPpn")
            Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
            Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
            Me.TBReImp.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReImp")
            Me.DTPTglReImp.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglReImp")
            Me.TBBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("BL")
            Me.DTPTglBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglBL")
            Me.TBLC.EditValue = Me.GridView2.GetFocusedDataRow.Item("LC")
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

            FillDtl(Me.TBID.EditValue)
            ReDim arrPar(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            Me.SLUSales.Properties.ReadOnly = True
            Me.SLUCust.Properties.ReadOnly = True

            If Me.GridView2.GetFocusedDataRow.Item("Autoo") = False Then
                If Me.RBPPn.EditValue = "Include" Then
                    Me.TBPersenPPn.Properties.ReadOnly = True
                ElseIf Me.RBPPn.EditValue = "Exclude" Then
                    Me.TBPersenPPn.Properties.ReadOnly = False
                ElseIf Me.RBPPn.EditValue = "Non PPn" Then
                    Me.TBPersenPPn.Properties.ReadOnly = True
                End If
            
            Else
                Me.SLULPRID.Properties.ReadOnly = True
                Me.SLUGrup.Properties.ReadOnly = True
                Me.SLUCab.Properties.ReadOnly = True
                Me.SLUCust.Properties.ReadOnly = True
                Me.CBOJnsRtr.Properties.ReadOnly = True
                Me.CBOMetode.Properties.ReadOnly = True
                Me.SLUMtUang.Properties.ReadOnly = True
                Me.CBOJnsFt.Properties.ReadOnly = True

                Me.TBPersenPPn.Properties.ReadOnly = True
                Me.RBPPn.Properties.ReadOnly = True

            End If

            If Gol = "Job Order" Then
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
            Else
                If Manual = False Then
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
                Else
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                End If
            End If
        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Retur Penjualan Barang Jadi"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_RtrBJ", "stsPakai", "RtrID", Me.GridView2.GetFocusedDataRow.Item("RtrID")) = False And SlCek("T_RtrBJ", "TotAkhir", "RtrID", Me.GridView2.GetFocusedDataRow.Item("RtrID")) = SlCek("T_RtrBJ", "SisaPakai", "RtrID", Me.GridView2.GetFocusedDataRow.Item("RtrID")) Then

            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("RtrID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_RtrBJ")
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
        Else
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        koneksi.Close()

        Dim Kode As String
        Kode = Me.GridView2.GetFocusedDataRow.Item("RtrID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "RtrID") = Kode Then
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("RtrID"), "RtrID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kode"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("LPRID") & " " & Me.GridView2.GetFocusedDataRow.Item("CabID"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotSbDisc"), "TotSbDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB"), "TotRpDiscOB")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust"), "TotRpDiscCust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscGlb"), "TotRpDisc")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim cmSP As New SqlCommand("SPUpstsPrint")
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
                    Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "stsPrint", "True")
                Else
                    XtraMessageBox.Show("Status Print Gagal Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Status Print Gagal Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With

        Dim XR As New XRReturBJ
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUCab.EditValue = "" Or IsDBNull(Me.SLUCab.EditValue) Then
            XtraMessageBox.Show("Cabang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUSales.EditValue = "" Or IsDBNull(Me.SLUSales.EditValue) Then
            XtraMessageBox.Show("Sales Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                Dim cmSP As New SqlCommand("SPInsT_RtrBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 16
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@LPRID", SqlDbType.VarChar).Value = Me.SLULPRID.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@JnsRtr", SqlDbType.VarChar).Value = Me.CBOJnsRtr.EditValue
                    .Parameters.Add("@Metode", SqlDbType.VarChar).Value = Me.CBOMetode.EditValue
                    .Parameters.Add("@NoPjk", SqlDbType.VarChar).Value = Me.TBPajak.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    '.Parameters.Add("@Area", SqlDbType.VarChar).Value = Area
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.SLUJnsCust.Text
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Ongkir
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = JT
                    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@ReImp", SqlDbType.VarChar).Value = Me.TBReImp.EditValue
                    .Parameters.Add("@TglReImp", SqlDbType.Date).Value = Me.DTPTglReImp.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
                    .Parameters.Add("@LC", SqlDbType.VarChar).Value = Me.TBLC.EditValue
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
                    .Parameters.Add("@RtrID", SqlDbType.VarChar, 30)
                    .Parameters("@RtrID").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 25)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBID.EditValue = cmSP.Parameters("@RtrID").Value
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_RtrBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 16
                                    .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                    .Parameters.Add("@JualIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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
                                    .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                    .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                    .Parameters.Add("@RpDiscL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscL")
                                    .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@DiscGlbSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscGlbSat")
                                    .Parameters.Add("@RpDiscGlb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscGlb")
                                    .Parameters.Add("@Namalain", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NamaLain")
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
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBID.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpT_RtrBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@RtrID", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 16
                    .Parameters.Add("@LPRID", SqlDbType.VarChar).Value = Me.SLULPRID.EditValue
                    .Parameters.Add("@LPRIDLama", SqlDbType.VarChar).Value = LPRIDLama
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@JnsLama", SqlDbType.VarChar).Value = JnsLama
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@JnsRtr", SqlDbType.VarChar).Value = Me.CBOJnsRtr.EditValue
                    .Parameters.Add("@Metode", SqlDbType.VarChar).Value = Me.CBOMetode.EditValue
                    .Parameters.Add("@NoPjk", SqlDbType.VarChar).Value = Me.TBPajak.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    '.Parameters.Add("@Area", SqlDbType.VarChar).Value = Area
                    .Parameters.Add("@UrutArea", SqlDbType.VarChar).Value = UrutArea
                    .Parameters.Add("@UrutCust", SqlDbType.VarChar).Value = UrutCust
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.SLUJnsCust.Text
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Ongkir
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = JT
                    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@ReImp", SqlDbType.VarChar).Value = Me.TBReImp.EditValue
                    .Parameters.Add("@TglReImp", SqlDbType.Date).Value = Me.DTPTglReImp.EditValue
                    .Parameters.Add("@BL", SqlDbType.VarChar).Value = Me.TBBL.EditValue
                    .Parameters.Add("@TglBL", SqlDbType.Date).Value = Me.DTPTglBL.EditValue
                    .Parameters.Add("@LC", SqlDbType.VarChar).Value = Me.TBLC.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_RtrBJDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
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
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_RtrBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 16
                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                        .Parameters.Add("@JualIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@RpDiscL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscL")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@DiscGlbSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscGlbSat")
                                        .Parameters.Add("@RpDiscGlb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscGlb")
                                        .Parameters.Add("@Namalain", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NamaLain")
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
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_RtrBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "RtrIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 16
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                        .Parameters.Add("@JualIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@RpDiscL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscL")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@DiscGlbSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscGlbSat")
                                        .Parameters.Add("@RpDiscGlb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscGlb")
                                        .Parameters.Add("@Namalain", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NamaLain")
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

        Dim cmd2 As New SqlCommand("SPAftSRtrBJ")
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
            If Me.SLUCab.EditValue <> "" Then
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
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
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.Properties.ReadOnly = False Then
            Try
                If Not IsDBNull(Me.SLUCab.EditValue) Then
                    Dim cmsl As SqlDataAdapter

                    cmsl = New SqlDataAdapter("Select LPRID,JnsTrans,JnsRtr,Metode,SalID,GdID,H.CustID,C.Nama As Cust,MtUang,CurrID,Kat,TipePPn, PersenPPn,TotSbDisc,TotSbDisc,TotOngkir,TotDPP,TotPPn,TotRpDiscCust,TotRpDiscOB,DiscP,RpDiscP,DiscRp,TotAkhir,Grup From T_LPR H Inner Join M_Cust C On H.CustID=C.CustID Where CabID='" & Me.SLUCab.EditValue & "' and stsRtr='False'", koneksi)
                    cmsl.TableMappings.Add("Table", "T_LPR" & Gol)
                    cmsl.Fill(DsMaster, "T_LPR" & Gol)
                    DsMaster.Tables("T_LPR" & Gol).Clear()
                    cmsl.Fill(DsMaster, "T_LPR" & Gol)

                    Me.SLULPRID.Properties.DataSource = DsMaster.Tables("T_LPR" & Gol)
                    Me.SLULPRID.Properties.DisplayMember = "LPRID"
                    Me.SLULPRID.Properties.ValueMember = "LPRID"

                    If Gol = "Job Order" Then
                        cmsl = New SqlDataAdapter("Select S.SalID,Area,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                        cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
                        Try
                            DsMaster.Tables("M_SalesLUE" & Gol).Clear()
                        Catch ex As Exception

                        End Try
                        cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
                    Else
                        cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='Lokal' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
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
                End If
            Catch ex As Exception

            End Try

            Me.SLUCust.EditValue = ""
            Me.SLUGd.EditValue = ""

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                Me.GridView1.DeleteRow(i)
            Next

            If Gol = "Job Order" Then
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                Me.GridColumn98.OptionsColumn.AllowEdit = False

            Else
                If Manual = False Then
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
                    Me.GridColumn98.OptionsColumn.AllowEdit = False
                Else
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                    Me.GridColumn98.OptionsColumn.AllowEdit = True
                End If
            End If
        End If

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
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLULPRID_Leave(sender As Object, e As EventArgs) Handles SLULPRID.Leave
        If Me.SLULPRID.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim cmsl As SqlDataAdapter

            If Me.SLULPRID.EditValue <> "" Then
                Me.SLUGrup.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("Grup")

                cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE")
                cmsl.Fill(DsMaster, "M_GudangLUE")
                DsMaster.Tables("M_GudangLUE").Clear()
                cmsl.Fill(DsMaster, "M_GudangLUE")

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"

                Me.SLUSales.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("SalID")
                Me.SLUGd.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("GdID")
                Me.SLUCust.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("CustID")
                Me.SLUMtUang.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("MtUang")
                Me.CBOJnsFt.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("JnsTrans")
                Me.CBOJnsRtr.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("JnsRtr")
                Me.CBOMetode.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("Metode")
                Me.CBOKat.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("Kat")
                Me.RBPPn.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TipePPn")
                Me.TBPersenPPn.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("PersenPPn")
                Me.TBTotSbDisc.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TotSbDisc")
                Me.TBTotDiscOB.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TotRpDiscOB")
                Me.TBTotDiscCust.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TotRpDiscCust")
                Me.TBTotDPP.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TotDPP")
                Me.TBTotPPn.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TotPPn")
                Me.TBTotAkhir.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("TotAkhir")
                Me.TBTamDiscP.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("DiscP")
                Me.TBTamDiscPRp.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("RpDiscP")
                Me.TBTamDiscRp.EditValue = DsMaster.Tables("T_LPR" & Gol).Select("LPRID = '" & Me.SLULPRID.EditValue & "'")(0).Item("DiscRp")

                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as RtrIDD,'" & Me.TBID.EditValue & "'  As RtrID,JualID, JualIDD,D.ArtCode,ArtName,D.SatID,D.Isi, IsiDlmDos,IsiAsDos,HarDPJ,HarSat,Qty,Dos,Psg,stsHarga,HarSbDisc,RpDiscCust,DiscOB,RpDiscOB, OngkirSat,Ongkir,HarAkhir,'' As NamaLain From T_LPRDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where LPRID='" & Me.SLULPRID.EditValue & "'", koneksi)

                cmsl.TableMappings.Add("Table", "T_RtrBJDtl" & Gol)
                Try
                    DsMaster.Tables("T_RtrBJDtl" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "T_RtrBJDtl" & Gol)

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_RtrBJDtl" & Gol
            End If


            If Not IsDBNull(Me.SLUCust.EditValue) Or Me.SLUCust.EditValue <> "" Then
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
        End If
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub CBOMetode_Leave(sender As Object, e As EventArgs) Handles CBOMetode.Leave
        If Me.CBOMetode.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If

        If Me.CBOMetode.EditValue = "Non Faktur" Then
            Me.RBPPn.Properties.ReadOnly = False
        Else
            Me.RBPPn.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub CBOJnsFt_Leave(sender As Object, e As EventArgs) Handles CBOJnsFt.Leave
        If Me.CBOJnsFt.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RtrIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("RtrIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            'If Gol = "Job Order" Then
            If Not IsDBNull(Me.SLULPRID.EditValue) Or Me.SLULPRID.EditValue = "" Then
                rw = 0
                If Me.CBOMetode.EditValue = "Non Faktur" Then
                    Dim frm As New FRtrBJNF_a(Me.SLUJnsCust.Text, Gol, Me.SLUGrup.EditValue, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue, Ongkir, Manual)
                    frm.ShowDialog()

                ElseIf Me.CBOMetode.EditValue = "With Faktur" Then
                    Dim frm As New FRtrBJWF_a(Me.SLUCust.EditValue, Gol, Me.SLUGrup.EditValue, Me.TBID.EditValue, Me.RBPPn.EditValue, Me.TBPersenPPn.EditValue)
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
            'Else
            '    If Not IsDBNull(Me.SLULPRID.EditValue) Or Me.SLULPRID.EditValue = "" Then
            '        XtraMessageBox.Show("ID LPR Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Exit Sub
            '    End If
            'End If
        End If

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            If Me.CBOMetode.EditValue = "With Faktur" Then
                Dim SdhRtrP, SdhRtrD, QtyJ, QtyP, QtyD, QtyAss, TotRtr, SisaAkh As Integer

                Dim comm As New SqlCommand("Select Isnull(Sum(Qty),0) From T_JualBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & "", koneksi)

                With koneksi
                    .Open()
                    QtyJ = comm.ExecuteScalar()
                    .Close()
                End With

                comm = New SqlCommand("Select Isnull(Sum(Qty),0) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & "  And RtrID <>'" & Me.TBID.EditValue & "' and SatID <> 'P'", koneksi)

                With koneksi
                    .Open()
                    SdhRtrD = comm.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                    comm = New SqlCommand("Select Isnull(Sum(Qty),0) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' And RtrID <>'" & Me.TBID.EditValue & "' and SatID = 'P'", koneksi)

                    With koneksi
                        .Open()
                        SdhRtrP = comm.ExecuteScalar()
                        .Close()
                    End With

                    Dim x : For x = 0 To Me.GridView1.RowCount - 1
                        If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView1.GetRowCellValue(x, "JualIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "RtrIDD") <> Me.GridView1.GetRowCellValue(x, "RtrIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") = Me.GridView1.GetRowCellValue(x, "ArtCode") Then
                            QtyP += Me.GridView1.GetRowCellValue(x, "Qty")
                        End If

                        If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView1.GetRowCellValue(x, "JualIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "RtrIDD") <> Me.GridView1.GetRowCellValue(x, "RtrIDD") And Me.GridView1.GetRowCellValue(x, "SatID") <> "P" Then
                            QtyD += Me.GridView1.GetRowCellValue(x, "Qty")
                        End If

                    Next

                    If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > ((QtyJ - QtyD) * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) - ((SdhRtrD * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) + SdhRtrP + QtyP) Then

                        XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", ((QtyJ - QtyD) * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) - ((SdhRtrD * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos")) + SdhRtrP + QtyP))
                    End If

                Else
                    'Dos
                    QtyAss = 0
                    SisaAkh = 0

                    comm = New SqlCommand("Select Case When Isnull(max(Qty),0) % IsiDlmDos >0 Then Isnull(max(Qty),0)/IsiDlmDos+1 Else Isnull(max(Qty),0)/IsiDlmDos End  From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " And RtrID <>'" & Me.TBID.EditValue & "' and SatID = 'P'  Group By IsiDlmDos", koneksi)

                    With koneksi
                        .Open()
                        SdhRtrP = comm.ExecuteScalar()
                        .Close()
                    End With

                    Dim x : For x = 0 To Me.GridView1.RowCount - 1
                        'If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView1.GetRowCellValue(x, "JualIDD") and  Then
                        If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView1.GetRowCellValue(x, "JualIDD") And Me.GridView1.GetRowCellValue(e.RowHandle, "RtrIDD") <> Me.GridView1.GetRowCellValue(x, "RtrIDD") And Me.GridView1.GetRowCellValue(x, "SatID") = "P" Then

                            If Me.GridView1.GetRowCellValue(x, "Qty") / Me.GridView1.GetRowCellValue(x, "IsiDlmDos") <= 1 Then
                                QtyP = 1

                            Else
                                If Me.GridView1.GetRowCellValue(x, "Qty") Mod Me.GridView1.GetRowCellValue(x, "IsiDlmDos") > 0 Then
                                    QtyP = (Me.GridView1.GetRowCellValue(x, "Qty") / Me.GridView1.GetRowCellValue(x, "IsiDlmDos")) + 1
                                Else
                                    QtyP = Me.GridView1.GetRowCellValue(x, "Qty") / Me.GridView1.GetRowCellValue(x, "IsiDlmDos")
                                End If
                            End If

                            If QtyP > QtyAss Then
                                QtyAss = QtyP
                            End If
                        End If
                    Next

                    If SdhRtrP > QtyAss Then
                        TotRtr = SdhRtrP
                    Else
                        TotRtr = QtyAss
                    End If

                    If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > QtyJ - SdhRtrD - TotRtr Then
                        XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", QtyJ - SdhRtrD - TotRtr)
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
        End If

    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        'If Me.GridView1.OptionsBehavior.Editable = True Then

        '    If Me.CBOJnsRt.EditValue = "Non Faktur" Then
        '        Dim frm As New FSearch("JualBJ", Me.SLUJnsCust.Text, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue)
        '        frm.ShowDialog()

        '        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsHarga", dataTrans.Item("stsHarga").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "HarSat", dataTrans.Item("Harga").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DiscOB", dataTrans.Item("DiscOB").ToString)
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "OngkirSat", Ongkir * Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi"))
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 1)
        '        End If
        '    End If

        'End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow

        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "RtrIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RtrID", Me.TBID.EditValue)
                If Me.CBOMetode.EditValue = "With Faktur" Then
                    Me.GridView1.SetRowCellValue(e.RowHandle, "JualIDD", dataTrans.Item("JualIDD" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "JualID", dataTrans.Item("JualID" & rw).ToString)
                Else
                    Me.GridView1.SetRowCellValue(e.RowHandle, "JualIDD", 0)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "JualID", "--")
                End If
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "IsiDlmDos", dataTrans.Item("IsiDlmDos" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "IsiAsDos", dataTrans.Item("IsiAsDos" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "stsHarga", dataTrans.Item("stsHarga" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarDPJ", dataTrans.Item("HarDPJ" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", dataTrans.Item("Harga" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscOB", dataTrans.Item("DiscOB" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscL", dataTrans.Item("RpDiscL" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "OngkirSat", Ongkir * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscGlbSat", dataTrans.Item("DiscGlbSat" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscGlb", dataTrans.Item("RpDiscGlb" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "NamaLain", "")

                rw += 1
            End If

        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try
    End Sub
    Private Sub RBPPn_Leave(sender As Object, e As EventArgs) Handles RBPPn.Leave
        If Indicator = "100" Then
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
        Else
            If Me.GridView2.GetFocusedDataRow.Item("Autoo") = True Then
                Me.TBPersenPPn.Properties.ReadOnly = True
                Me.RBPPn.Properties.ReadOnly = True
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
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTotDiscGlb.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub

    Private Sub TBTamDiscRp_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscRp.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTotDiscGlb.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub


    Private Sub GridControl2_DoubleClick(sender As Object, e As EventArgs) Handles GridControl2.DoubleClick
        Try
            Dim frm As New FRtrBJ_d(Me.GridView2.GetFocusedDataRow.Item("RtrID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView2.GetRowCellValue(e.RowHandle, "stsPrint") = False Then
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPajak.KeyPress, TBReImp.KeyPress, TBBL.KeyPress, TBLC.KeyPress, MKet.KeyPress
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