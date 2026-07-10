Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FFtBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1) As String
    Dim KdLama, CodeID, LPRIDLama, JnsLama, UrutArea, UrutCust, CurrID, Metode, JnsPerhit, Kelipatan, BeliMin, JnsPot, Gol As String
    Dim Manual, MnlInsUpd, stsPPn As Boolean
    Dim RowIdx As Integer
    Dim rw As Integer = 0
    Dim rw2 As Integer = 0
    Dim JT As Integer
    Dim Ongkir, Pot, TotFree As Decimal

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PjBJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PjBJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PjBJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTJualBJ_s.Enabled = True

        Me.TBID.Properties.ReadOnly = True
        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUSJID.Properties.ReadOnly = True
        Me.SLUSales.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.CBOJnsFt.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.SLUPromo.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.TBPajak.Properties.ReadOnly = True
        Me.TBPEB.Properties.ReadOnly = True
        Me.DTPTglPEB.Properties.ReadOnly = True
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
        Me.BVTJualBJ_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUSJID.Properties.ReadOnly = False
        Me.SLUSales.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.CBOJnsFt.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.SLUPromo.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.TBPajak.Properties.ReadOnly = False
        Me.TBPEB.Properties.ReadOnly = False
        Me.DTPTglPEB.Properties.ReadOnly = False
        Me.TBBL.Properties.ReadOnly = False
        Me.DTPTglBL.Properties.ReadOnly = False
        Me.TBLC.Properties.ReadOnly = False
        Me.TBTamDiscP.Properties.ReadOnly = False
        Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTJualBJ_e.Selected = True

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

        'If Gol = "Job Order" Then
        '    cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        '    cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
        '    Try
        '        DsMaster.Tables("M_SalesLUE" & Gol).Clear()
        '    Catch ex As Exception

        '    End Try
        '    cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
        'Else
        '    cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='Lokal'", koneksi)
        '    cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
        '    Try
        '        DsMaster.Tables("M_SalesLUE" & Gol).Clear()
        '    Catch ex As Exception

        '    End Try
        '    cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
        'End If

        'Me.SLUSales.Properties.DataSource = DsMaster.Tables("M_SalesLUE" & Gol)
        'Me.SLUSales.Properties.DisplayMember = "Nama"
        'Me.SLUSales.Properties.ValueMember = "SalID"

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

        'Me.SLUMtUang.EditValue = "IDR"
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

    Public Sub CekPromo()
        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscOB.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscCust.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

        Dim Reader2 As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select JnsPerhit,Kelipatan From T_PromoDtl Where PromoID='" & Me.SLUPromo.EditValue & "' and Paket = '" & Me.TBPaket.EditValue & "' and JnsCust In ('" & Me.SLUJnsCust.Text & "','%') ", koneksi)

        With koneksi
            .Open()
            Reader2 = command.ExecuteReader

            If Reader2.HasRows Then
                While Reader2.Read
                    If IsDBNull(Reader2.Item(0)) = True Then
                        JnsPerhit = ""
                        Kelipatan = False
                    Else
                        JnsPerhit = Reader2.Item(0)
                        Kelipatan = Reader2.Item(1)
                    End If
                End While
            End If
            Reader2.Close()
            .Close()
        End With

        If Me.SLUPromo.EditValue <> "" Then
            If JnsPerhit = "Nominal" Then
                Dim cmSl As New SqlCommand("Select JnsPot,Pot,BeliMin From T_PromoDtl where PromoID= '" & Me.SLUPromo.EditValue & "' and Paket ='" & Me.TBPaket.EditValue & "' and JnsCust In ('" & Me.SLUJnsCust.Text & "','%') and (BeliMin <= " & Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue & ") AND (BeliMax >= " & Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue & ")")

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
                                JnsPot = Reader.Item(0)
                                Pot = Reader.Item(1)
                                BeliMin = Reader.Item(2)
                            End While
                        Else
                            JnsPot = ""
                            Pot = 0
                            BeliMin = 0
                        End If
                        Reader.Close()

                        .Close()
                    End With
                End With

            ElseIf JnsPerhit = "Quantity" Then
                Dim cmSl As New SqlCommand("Select JnsPot,Pot,BeliMin From T_PromoDtl where PromoID= '" & Me.SLUPromo.EditValue & "' and Paket ='" & Me.TBPaket.EditValue & "' and JnsCust In ('" & Me.SLUJnsCust.Text & "','%') and (BeliMin <= " & CType(Me.GridView1.Columns("Qty").SummaryText, Integer) & ") AND (BeliMax >= '" & CType(Me.GridView1.Columns("Qty").SummaryText, Integer) & "') ")

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
                                JnsPot = Reader.Item(0)
                                Pot = Reader.Item(1)
                                BeliMin = Reader.Item(2)
                            End While
                        Else
                            JnsPot = ""
                            Pot = 0
                            BeliMin = 0
                        End If
                        Reader.Close()

                        .Close()
                    End With
                End With
            End If

            If JnsPot = "Nominal" Then
                If Kelipatan = False Then
                    Me.TBTamDiscRp.EditValue = Pot
                Else
                    Me.TBTamDiscRp.EditValue = Pot * ((Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue) \ BeliMin)
                End If

            ElseIf JnsPot = "Persen" Then
                Me.TBTamDiscP.EditValue = Pot
                Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100

            ElseIf JnsPot = "Barang" Then
                If Kelipatan = False Then
                    TotFree = Pot
                Else
                    If CType(Me.GridView1.Columns("Qty").SummaryText, Decimal) > 0 Then
                        TotFree = Pot * (CType(Me.GridView1.Columns("Qty").SummaryText, Decimal) \ BeliMin)
                    Else
                        TotFree = 0
                    End If
                End If

            ElseIf JnsPot = "Harga Lama" Then
                'Khusus Promo Harga Lama yang tidak campur zise dan warna murni per article

                Dim TotPot As Decimal = 0

                If Kelipatan = False Then
                    Dim i : For i = 0 To Me.GridView1.RowCount - 1
                        If Me.GridView1.GetRowCellValue(i, "QtyCab") >= BeliMin Then
                            Me.GridView1.SetRowCellValue(i, "SelisihExtra", Math.Round(BeliMin * Me.GridView1.GetRowCellValue(i, "Selisih"), 2))
                            TotPot += Math.Round(BeliMin * Me.GridView1.GetRowCellValue(i, "Selisih"), 2)
                        Else
                            Me.GridView1.SetRowCellValue(i, "SelisihExtra", 0)
                        End If
                    Next

                    Me.TBTamDiscRp.EditValue = TotPot
                Else

                    Dim i : For i = 0 To Me.GridView1.RowCount - 1
                        If Me.GridView1.GetRowCellValue(i, "QtyCab") >= BeliMin Then
                            Me.GridView1.SetRowCellValue(i, "SelisihExtra", Math.Round((Me.GridView1.GetRowCellValue(i, "QtyCab") \ BeliMin) * Me.GridView1.GetRowCellValue(i, "Selisih"), 2))
                            TotPot += Math.Round((Me.GridView1.GetRowCellValue(i, "QtyCab") \ BeliMin) * Me.GridView1.GetRowCellValue(i, "Selisih"), 2)
                        Else
                            Me.GridView1.SetRowCellValue(i, "SelisihExtra", 0)
                        End If
                    Next

                    Me.TBTamDiscRp.EditValue = TotPot
                End If

            End If

            HitPPn()
        End If
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_JualBJDtl" & Gol).Clear()
            DsMaster.Tables("T_JualBJFree" & Gol).Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualIDD,JualID,SJIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,Qty,Dos,Psg,stsHarga,HarSbDisc,DiscOB, RpDiscOB,RpDiscCust,RpDiscL,OngkirSat,Ongkir,HarAkhir,Selisih,SelisihExtra,D.Urut,NamaLain,B.Gol,D.Ket From T_JualBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where JualID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_JualBJDtl" & Gol)
        cmsl.Fill(DsMaster, "T_JualBJDtl" & Gol)

        DsMaster.Tables("T_JualBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBJDtl" & Gol).Columns("JualID"), DsMaster.Tables("T_JualBJDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_JualBJDtl" & Gol

        cmsl = New SqlDataAdapter("Select JualIDD,JualID,SJIDD,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg From T_JualBJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where JualID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_JualBJFree" & Gol)
        cmsl.Fill(DsMaster, "T_JualBJFree" & Gol)

        DsMaster.Tables("T_JualBJFree" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBJFree" & Gol).Columns("JualID"), DsMaster.Tables("T_JualBJFree" & Gol).Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_JualBJFree" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualID,PeriodID,Kode,JnsTrans,NoPajak,H.PromoID,H.Paket,P.Nama As Promo,H.CabID,Cb.Cabang,H.SJID,H.SalID, S.Nama As Sales,UrutArea,H.GdID,G.Nama as Gudang,H.CustID,C.Nama As Customer,C.Alamat,K.Nama As Kota,UrutCust,H.JnsCustID,H.Jenis, H.Harga,Ongkir,H.Tanggal,DueDate,MtUang,CurrID, NilTukarRp,H.DiscCust,Kat,TipePPn,PersenPPn,PEB,TglPEB,BL,TglBL,LC,TotSbDisc,TotOngkir, TotDPP, TotPPn,TotRpDiscCust,TotRpDiscOB,TotRpDiscL,DiscP,RpDiscP,DiscRp,TotAkhir,TotAkhirRp,SisaBayar,TotQty,TotDos,TotPsg,H.Ket,H.Grup, H.Gol,H.stsPPn,Autoo,H.stsTagih,stsLunas,stsPrint,G.Pusat,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.PrintDate,H.PrintBy From T_JualBJ H Inner Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Sales S On H.SalID=S.SalID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join T_Promo P On H.PromoID=P.PromoID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By JualID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualBJ" & Gol)
        Try
            DsMaster.Tables("T_JualBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_JualBJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_JualBJ" & Gol
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue + Me.TBTotPPn.EditValue
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_JualBJ")
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
        If IO.File.Exists("SrHarga.xml") Then
            System.IO.File.Delete("SrHarga.xml")
        End If

        If IO.File.Exists("SrPromo.xml") Then
            System.IO.File.Delete("SrPromo.xml")
        End If
    End Sub

    Private Sub FFtBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Penjualan Barang Jadi"
    End Sub

    Private Sub FFtBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        dataTrans2 = New Collection
        dataTrans2.Clear()

        CekSave = False

        Me.Dispose()
    End Sub
    Private Sub FFtBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub


    Private Sub FFtBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTJualBJ_e.Selected = True
    End Sub

    Private Sub BVTJualBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTJualBJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Penjualan Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("PjBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Penjualan Barang Jadi"

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

        Dim cmd2 As New SqlCommand("SPBefNJualBJ")
        cmd2.CommandType = CommandType.StoredProcedure

        With cmd2
            .Connection = koneksi

            With koneksi
                .Open()
                cmd2.ExecuteNonQuery()
                .Close()
            End With

        End With

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        Me.TBKode.EditValue = "--"
        Me.SLUGrup.EditValue = ""
        Me.SLUSJID.EditValue = ""
        Me.TBPajak.EditValue = ""
        Me.SLUCab.EditValue = ""
        Me.SLUSales.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.CBOJnsFt.EditValue = "Non Spreading"
        Me.SLUJnsCust.EditValue = ""
        Me.TBHarga.EditValue = ""
        Me.TBDiscCust.EditValue = 0
        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOKat.EditValue = "Lokal"
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        Me.SLUPromo.EditValue = ""
        Me.TBPaket.EditValue = ""
        Metode = ""
        Me.TBPEB.EditValue = ""
        Me.DTPTglPEB.EditValue = Date.Now
        Me.TBBL.EditValue = ""
        Me.DTPTglBL.EditValue = Date.Now
        Me.TBLC.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTotDiscOB.EditValue = 0
        Me.TBTotDiscCust.EditValue = 0
        Me.TBTotDiscL.EditValue = 0
        Me.TBTamDiscP.EditValue = 0.0
        Me.TBTamDiscPRp.EditValue = 0.0
        Me.TBTamDiscRp.EditValue = 0.0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBID.EditValue)
        DsMaster.Tables("T_JualBJDtl" & Gol).Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)
        CekCurr()

        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1

        If Me.SLUPromo.EditValue = "" Then
            Me.TBTamDiscP.Properties.ReadOnly = False
            Me.TBTamDiscRp.Properties.ReadOnly = False

        Else
            Me.TBTamDiscP.Properties.ReadOnly = True
            Me.TBTamDiscRp.Properties.ReadOnly = True

            Me.TBTamDiscP.EditValue = 0
            Me.TBTamDiscRp.EditValue = 0
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Penjualan Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        'MsgBox((Me.GridView2.GetFocusedDataRow.Item("GdID")))
        'MsgBox((Me.GridView2.GetFocusedDataRow.Item("Tanggal")))
        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then

            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Autoo") = True Then
            Me.GridControl1.UseEmbeddedNavigator = False
            Me.GridColumn3.OptionsColumn.AllowEdit = False
            Me.GridColumn7.OptionsColumn.AllowEdit = False

            Me.GridControl3.UseEmbeddedNavigator = False
            Me.GridColumn69.OptionsColumn.AllowEdit = False
        Else
            Me.GridControl1.UseEmbeddedNavigator = True
            Me.GridColumn3.OptionsColumn.AllowEdit = True
            Me.GridColumn7.OptionsColumn.AllowEdit = True

            Me.GridControl3.UseEmbeddedNavigator = True
            Me.GridColumn69.OptionsColumn.AllowEdit = True
        End If

        DelXml()



        If SlCek("T_JualBJ", "stsTagih", "JualID", Me.GridView2.GetFocusedDataRow.Item("JualID")) = False And SlCek("T_JualBJ", "TotAkhir", "JualID", Me.GridView2.GetFocusedDataRow.Item("JualID")) = SlCek("T_JualBJ", "SisaBayar", "JualID", Me.GridView2.GetFocusedDataRow.Item("JualID")) Then
            LUE()

            Dim cmsl As SqlDataAdapter

            Indicator = "200"
            Me.TBID.EditValue = Me.GridView2.GetFocusedDataRow.Item("JualID")
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kode")
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")

            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=15 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

            If Gol <> "Job Order" Then
                cmsl = New SqlDataAdapter("Select SJID,JnsTrans,PromoID,Paket,SalID,CustID,JnsCustID,DiscCust,Harga,Ongkir,MtUang,Kat,stsPPN, TipePPn,PersenPPn,Grup From T_SJ Where CabID='" & Me.SLUCab.EditValue & "' and stsApp='True' or SJID = (Select SJID From T_JualBJ where JualID ='" & Me.TBID.EditValue & "')", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select POID As SJID,POCust,SalID,H.CustID,C.JnsCustID,DiscCust,Harga,0 as Ongkir,MtUang,Kat,Grup From T_POBJJO H Inner Join M_Cust C On H.CustID=C.CustID where stsKirim='False' or POID = (Select SJID From T_JualBJ where JualID ='" & Me.TBID.EditValue & "')", koneksi)

                Me.GridColumn124.Visible = True
            End If
            cmsl.TableMappings.Add("Table", "T_SJ" & Gol)

            Try
                DsMaster.Tables("T_SJ" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "T_SJ" & Gol)

            Me.SLUSJID.Properties.DataSource = DsMaster.Tables("T_SJ" & Gol)
            Me.SLUSJID.Properties.DisplayMember = "SJID"
            Me.SLUSJID.Properties.ValueMember = "SJID"

            If Gol = "Job Order" Then
                cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
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

            Me.SLUSJID.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJID")
            Me.TBPajak.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoPajak")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUSales.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")

            If Not IsDBNull(Me.SLUCust.EditValue) Then
                cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_JnsCustFt" & Gol)
                cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)

                Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustFt" & Gol)
                Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"
            End If

            If Not IsDBNull(Me.SLUCab.EditValue) Then
                cmsl = New SqlDataAdapter("Select C.CustID,Nama,JnsCustID,DiscCust,JT,Harga From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustFt" & Gol)
                cmsl.Fill(DsMaster, "M_CustFt" & Gol)

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustFt" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"

                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Gol='" & Gol & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"
            End If

            Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
            JT = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")
            Me.CBOJnsFt.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            JnsLama = Me.GridView2.GetFocusedDataRow.Item("Jenis")
            UrutArea = Me.GridView2.GetFocusedDataRow.Item("UrutArea")
            UrutCust = Me.GridView2.GetFocusedDataRow.Item("UrutCust")
            Me.SLUJnsCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsCustID")
            Me.TBHarga.EditValue = Me.GridView2.GetFocusedDataRow.Item("Harga")
            Ongkir = Me.GridView2.GetFocusedDataRow.Item("Ongkir")
            Me.TBDiscCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscCust")
            Me.TBPaket.EditValue = Me.GridView2.GetFocusedDataRow.Item("Paket")

            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY PromoID,Paket)As Row,* From(Select Distinct H.PromoID,Nama,Metod,Paket From T_Promo H Inner Join T_PromoDtl D On H.PromoID=D.PromoID Where '" & Me.DTPTanggal.EditValue & "'>=TglAwal and '" & Me.DTPTanggal.EditValue & "' <=TglAkhir and JnsCust in ('" & Me.SLUJnsCust.Text & "','%') and DiperhitSaat='Faktur' and Gol='" & Gol & "') As x", koneksi)
            cmsl.TableMappings.Add("Table", "T_PromoFt" & Gol)
            cmsl.Fill(DsMaster, "T_PromoFt" & Gol)
            DsMaster.Tables("T_PromoFt" & Gol).Clear()
            cmsl.Fill(DsMaster, "T_PromoFt" & Gol)

            Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
            Me.SLUPromo.Properties.DisplayMember = "Nama"
            Me.SLUPromo.Properties.ValueMember = "PromoID"


            Me.SLUPromo.EditValue = Me.GridView2.GetFocusedDataRow.Item("PromoID")
            If Me.SLUPromo.EditValue <> "" Then
                Metode = DsMaster.Tables("T_PromoFt" & Gol).Select("PromoID = '" & Me.SLUPromo.EditValue & "' and Paket='" & Me.TBPaket.EditValue & "'")(0).Item("Metod")
            Else
                Metode = ""
            End If

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
            Me.TBPEB.EditValue = Me.GridView2.GetFocusedDataRow.Item("PEB")
            Me.DTPTglPEB.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglPEB")
            Me.TBBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("BL")
            Me.DTPTglBL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglBL")
            Me.TBLC.EditValue = Me.GridView2.GetFocusedDataRow.Item("LC")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
            Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
            Me.TBTotDiscOB.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB")
            Me.TBTotDiscCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust")
            Me.TBTotDiscL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL")
            Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
            Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
            Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
            Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
            Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
            Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

            FillDtl(Me.TBID.EditValue)
            ReDim arrPar(-1)
            ReDim arrPar2(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            If Me.SLUPromo.EditValue = "" Then
                Me.TBTamDiscP.Properties.ReadOnly = False
                Me.TBTamDiscRp.Properties.ReadOnly = False

            Else
                Me.TBTamDiscP.Properties.ReadOnly = True
                Me.TBTamDiscRp.Properties.ReadOnly = True

                Me.TBTamDiscP.EditValue = 0
                Me.TBTamDiscRp.EditValue = 0
            End If



            Me.SLUCab.Properties.ReadOnly = True
            Me.SLUGrup.Properties.ReadOnly = True
            'Me.SLUSales.Properties.ReadOnly = True
            'Me.SLUGd.Properties.ReadOnly = True
            Me.SLUCust.Properties.ReadOnly = True
            Me.CBOJnsFt.Properties.ReadOnly = True
            Me.SLUPromo.Properties.ReadOnly = True
            'Me.MKet.Properties.ReadOnly = True

            If Gol = "Job Order" Then
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                Me.SLUSJID.Properties.ReadOnly = False
            Else
                Me.SLUSJID.Properties.ReadOnly = True

                If Manual = False Then
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
                    If Me.GridView2.GetFocusedDataRow.Item("Autoo") = True Then
                        Me.DTPTanggal.Properties.ReadOnly = True
                    Else
                        Me.DTPTanggal.Properties.ReadOnly = False
                    End If
                Else
                    Me.DTPTanggal.Properties.ReadOnly = False
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                End If
            End If

        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Penjualan Barang Jadi"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        'If Me.GridView2.GetFocusedDataRow.Item("Autoo") = True Then
        '    XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Pembuatan Otomatis", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If SlCek("T_JualBJ", "stsTagih", "JualID", Me.GridView2.GetFocusedDataRow.Item("JualID")) = False And SlCek("T_JualBJ", "TotAkhir", "JualID", Me.GridView2.GetFocusedDataRow.Item("JualID")) = SlCek("T_JualBJ", "SisaBayar", "JualID", Me.GridView2.GetFocusedDataRow.Item("JualID")) Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("JualID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_JualBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JualID")
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
        Kode = Me.GridView2.GetFocusedDataRow.Item("JualID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "JualID") = Kode Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JualID"), "JualID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kode"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DueDate"), "DueDate")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJID"), "SJID")

        'If Me.GridView2.GetFocusedDataRow.Item("UrutSJ") = 0 Then
        '    bind.Add(Me.GridView2.GetFocusedDataRow.Item("PrefSJ"), "SJ")
        'Else
        '    bind.Add(Me.GridView2.GetFocusedDataRow.Item("PrefSJ") & " " & Me.GridView2.GetFocusedDataRow.Item("UrutSJ"), "SJ")
        'End If
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Promo"), "Promo")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Paket"), "Paket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")

        If Me.GridView2.GetFocusedDataRow.Item("TipePPn") <> "Exclude" Then
            bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir") - (Me.GridView2.GetFocusedDataRow.Item("TotSbDisc") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") - (Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"))), "TotByExp")
        Else
            bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir") - (Me.GridView2.GetFocusedDataRow.Item("TotSbDisc") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") - (Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp")) + Me.GridView2.GetFocusedDataRow.Item("TotPPn")), "TotByExp")
        End If
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")

        Dim cmSP As New SqlCommand("SPUpstsPrint")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JualID")
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
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Status Print Gagal Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Exit Sub
            End Try
        End With

        Dim XR As New XRFtBJ2
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBPrintSJ_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs)
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JualID"), "JualID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJID"), "SJID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DueDate"), "DueDate")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJID"), "SJ")

        'If Me.GridView2.GetFocusedDataRow.Item("UrutSJ") = 0 Then
        '    bind.Add(Me.GridView2.GetFocusedDataRow.Item("PrefSJ"), "SJ")
        'Else
        '    bind.Add(Me.GridView2.GetFocusedDataRow.Item("PrefSJ") & " " & Me.GridView2.GetFocusedDataRow.Item("UrutSJ"), "SJ")
        'End If
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Promo"), "Promo")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Paket"), "Paket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")

        If Me.GridView2.GetFocusedDataRow.Item("TipePPn") <> "Exclude" Then
            bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir") - (Me.GridView2.GetFocusedDataRow.Item("TotSbDisc") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") - (Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"))), "TotByExp")
        Else
            bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir") - (Me.GridView2.GetFocusedDataRow.Item("TotSbDisc") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL") - (Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp")) + Me.GridView2.GetFocusedDataRow.Item("TotPPn")), "TotByExp")
        End If
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")

        Dim XR As New XRFtSJ
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()
        Me.GridView6.ActiveFilter.Clear()

        Me.GridView1.RefreshData()
        Me.GridView6.RefreshData()

        Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscOB.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscCust.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

        CekPromo()

        If Me.RBPPn.EditValue <> "Non PPn" Then
            stsPPn = True
        Else
            stsPPn = False
        End If

        HitPPn()

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

        If Manual = False Then
            If Gol <> "Job Order" Then
                Dim Piut, CL As Decimal

                Dim command As New SqlCommand("Select dbo.fcPiutCust('" & Me.SLUCust.EditValue & "','" & Gol & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBID.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Piut = command.ExecuteScalar()
                    .Close()
                End With

                If Gol = "Character" Then
                    Dim comm As New SqlCommand("Select CLCr From M_Cust Where CustID='" & Me.SLUCust.EditValue & "'", koneksi)

                    With koneksi
                        .Open()
                        CL = comm.ExecuteScalar()
                        .Close()
                    End With

                ElseIf Gol = "Own" Then
                    Dim comm As New SqlCommand("Select CLOwn From M_Cust Where CustID='" & Me.SLUCust.EditValue & "'", koneksi)

                    With koneksi
                        .Open()
                        CL = comm.ExecuteScalar()
                        .Close()
                    End With
                End If

                If Piut + Me.TBTotAkhir.EditValue > CL Then
                    XtraMessageBox.Show("Piutang Melebihi Credit Limit Customer" & vbCrLf & "Konfimasikan kepada Manager Marketing Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        End If


        If Metode = "Gratis Barang" Then
            If CType(Me.GridView6.Columns("Qty").SummaryText, Integer) <> TotFree Then
                XtraMessageBox.Show("Jumlah Barang Gratis Tidak Sesuai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        JT = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")

        If Me.TBID.EditValue = "" Then
            XtraMessageBox.Show("ID Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_JualBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.SLUSJID.EditValue
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@NoPjk", SqlDbType.VarChar).Value = Me.TBPajak.EditValue
                    .Parameters.Add("@PromoID", SqlDbType.VarChar).Value = Me.SLUPromo.EditValue
                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.TBPaket.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.SLUJnsCust.Text
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Ongkir
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = JT
                    'If (Me.DTPTanggal.EditValue).Day <= 15 Then
                    '    Dim Tgl As Date
                    '    Tgl = New Date((Me.DTPTanggal.EditValue).Year, (Me.DTPTanggal.EditValue).Month, 15)
                    '    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Tgl)
                    'Else
                    '    Dim Tgl As Date
                    '    Tgl = New Date((Me.DTPTanggal.EditValue).Year, (Me.DTPTanggal.EditValue).Month, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
                    '    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Tgl)
                    'End If
                    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PEB", SqlDbType.VarChar).Value = Me.TBPEB.EditValue
                    .Parameters.Add("@TglPEB", SqlDbType.Date).Value = Me.DTPTglPEB.EditValue
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
                    .Parameters.Add("@JualID", SqlDbType.VarChar, 30)
                    .Parameters("@JualID").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBID.EditValue = cmSP.Parameters("@JualID").Value
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_JualBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.SLUSJID.EditValue
                                    .Parameters.Add("@SJIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SJIDD")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    '.Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
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
                                    .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                    .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
                                    .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Urut")
                                    .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NamaLain")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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

                        If Metode = "Gratis Barang" Then
                            Dim z : For z = 0 To GridView6.RowCount - 1
                                If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_JualBJFree")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@SJIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "SJIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Psg")
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
                        End If

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
                Dim cmSP As New SqlCommand("SPUpT_JualBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                    .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.SLUSJID.EditValue
                    .Parameters.Add("@JnsTransLama", SqlDbType.VarChar).Value = JnsLama
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@NoPjk", SqlDbType.VarChar).Value = Me.TBPajak.EditValue
                    .Parameters.Add("@PromoID", SqlDbType.VarChar).Value = Me.SLUPromo.EditValue
                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.TBPaket.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
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
                    'If (Me.DTPTanggal.EditValue).Day <= 15 Then
                    '    Dim Tgl As Date
                    '    Tgl = New Date((Me.DTPTanggal.EditValue).Year, (Me.DTPTanggal.EditValue).Month, 15)

                    '    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Tgl)
                    'Else
                    '    Dim Tgl As Date
                    '    Tgl = New Date((Me.DTPTanggal.EditValue).Year, (Me.DTPTanggal.EditValue).Month, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

                    '    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Tgl)
                    'End If
                    .Parameters.Add("@DueDate", SqlDbType.Date).Value = DateAdd(DateInterval.Day, JT, Me.DTPTanggal.EditValue)
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@PEB", SqlDbType.VarChar).Value = Me.TBPEB.EditValue
                    .Parameters.Add("@TglPEB", SqlDbType.Date).Value = Me.DTPTglPEB.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_JualBJDtl")
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_JualBJFree")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(q)
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
                            If Me.GridView1.GetRowCellValue(i, "JualIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_JualBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.SLUSJID.EditValue
                                        .Parameters.Add("@SJIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SJIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        '.Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
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
                                        .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
                                        .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Urut")
                                        .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NamaLain")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                                        Me.GridView1.SetRowCellValue(i, "JualIDD", Me.GridView1.GetRowCellValue(i, "JualIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_JualBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.SLUSJID.EditValue
                                        .Parameters.Add("@SJIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SJIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        '.Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
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
                                        .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
                                        .Parameters.Add("@Urut", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Urut")
                                        .Parameters.Add("@NamaLain", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NamaLain")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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

                                    If x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        If Metode = "Gratis Barang" Then
                            Dim z : For z = 0 To GridView6.RowCount - 1
                                If Me.GridView6.GetRowCellValue(z, "JualIDD") > 0 Then
                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_JualBJFree")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                            .Parameters.Add("@SJIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "SJIDD")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SatID")
                                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Isi")
                                            .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                            .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Dos")
                                            .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Psg")
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
                                            Me.GridView1.SetRowCellValue(i, "JualIDD", Me.GridView1.GetRowCellValue(i, "JualIDD") * -1)
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_JualBJFree")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "JualIDD")
                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 15
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                            .Parameters.Add("@SJIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "SJIDD")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SatID")
                                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Isi")
                                            .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Qty")
                                            .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Dos")
                                            .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Psg")
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

        Dim cmd2 As New SqlCommand("SPAftSJualBJ")
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
            If Not IsDBNull(Me.SLUSJID.EditValue) And Me.SLUSJID.EditValue = "" Then
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

                If Manual = False Then
                    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                        Me.GridView1.DeleteRow(i)
                    Next
                End If
            End If
        End If

    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.Properties.ReadOnly = False Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=15 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

            If Manual = True Then
                Me.TBID.Properties.ReadOnly = False
                Me.TBID.EditValue = ""
                'Me.TBTamDiscP.Properties.ReadOnly = False
                'Me.TBTamDiscRp.Properties.ReadOnly = False
            Else
                Me.TBID.Properties.ReadOnly = True
                Me.TBID.EditValue = "--"

                'Me.TBTamDiscP.Properties.ReadOnly = True
                'Me.TBTamDiscRp.Properties.ReadOnly = True
            End If

            Try
                If Not IsDBNull(Me.SLUCab.EditValue) Then
                    Dim cmsl As SqlDataAdapter

                    If Gol <> "Job Order" Then
                        cmsl = New SqlDataAdapter("Select SJID,JnsTrans,PromoID,Paket,SalID,CustID,JnsCustID,DiscCust,Harga,Ongkir,MtUang,Kat,stsPPN,TipePPn,PersenPPn,Grup From T_SJ Where CabID='" & Me.SLUCab.EditValue & "' and stsApp='True' or SJID = (Select SJID From T_JualBJ where JualID ='" & Me.TBID.EditValue & "')", koneksi)
                        cmsl.TableMappings.Add("Table", "T_SJ" & Gol)
                    Else
                        cmsl = New SqlDataAdapter("Select POID As SJID,POCust,SalID,H.CustID,C.JnsCustID,DiscCust,Harga,0 as Ongkir,MtUang,Kat,Grup From T_POBJJO H Inner Join M_Cust C On H.CustID=C.CustID where stsKirim='False' or POID = (Select SJID From T_JualBJ where JualID ='" & Me.TBID.EditValue & "')", koneksi)
                        cmsl.TableMappings.Add("Table", "T_SJ" & Gol)

                        Me.GridColumn124.Visible = True
                    End If

                    Try
                        DsMaster.Tables("T_SJ" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "T_SJ" & Gol)

                    Me.SLUSJID.Properties.DataSource = DsMaster.Tables("T_SJ" & Gol)
                    Me.SLUSJID.Properties.DisplayMember = "SJID"
                    Me.SLUSJID.Properties.ValueMember = "SJID"

                    If Gol = "Job Order" Then
                        cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where S.Aktif='True' and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
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
                    cmsl.TableMappings.Add("Table", "M_CustFt" & Gol)
                    cmsl.Fill(DsMaster, "M_CustFt" & Gol)
                    DsMaster.Tables("M_CustFt" & Gol).Clear()
                    cmsl.Fill(DsMaster, "M_CustFt" & Gol)

                    Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustFt" & Gol)
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

            Me.SLUGd.EditValue = ""
            Me.CBOKat.EditValue = "Lokal"
            Me.SLUCust.EditValue = ""
            Me.SLUSJID.EditValue = ""

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "JualIDD")

                Me.GridView6.DeleteRow(x)
            Next

            If Gol = "Job Order" Then
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
            Else
                If Manual = False Then
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
                Else
                    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub SLUSJID_Leave(sender As Object, e As EventArgs) Handles SLUSJID.Leave
        If Not IsDBNull(Me.SLUSJID.EditValue) And Me.SLUSJID.EditValue <> "" And Me.SLUSJID.Properties.ReadOnly = False Then
            Try
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "JualIDD")

                    Me.GridView6.DeleteRow(x)
                Next

                Me.SLUGrup.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("Grup")
                Me.SLUSales.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("SalID")

                Dim cmsl As SqlDataAdapter

                If Gol <> "Job Order" Then
                    cmsl = New SqlDataAdapter("Select GdID,Nama,Def From M_Gudang where GdID In (Select GdID From T_SJ where SJID='" & Me.SLUSJID.EditValue & "')", koneksi)
                    cmsl.TableMappings.Add("Table", "M_GudangLUE")
                    Try
                        DsMaster.Tables("M_GudangLUE" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)
                Else
                    cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                    Try
                        DsMaster.Tables("M_GudangLUE" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)
                End If

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"

                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota,JnsCustID,DiscCust,JT,Harga From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustFt" & Gol)
                cmsl.Fill(DsMaster, "M_CustFt" & Gol)
                DsMaster.Tables("M_CustFt" & Gol).Clear()
                cmsl.Fill(DsMaster, "M_CustFt" & Gol)

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustFt" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"

                Me.SLUCust.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("CustID")

                cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_JnsCustFt" & Gol)
                cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)
                DsMaster.Tables("M_JnsCustFt" & Gol).Clear()
                cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)

                Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustFt" & Gol)
                Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"

                Me.SLUJnsCust.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("JnsCustID")
                Me.TBHarga.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("Harga")
                JT = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")
                Ongkir = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("Ongkir")
                Me.TBDiscCust.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("DiscCust")
                Me.CBOKat.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("Kat")

                If Gol <> "Job Order" Then
                    cmsl = New SqlDataAdapter("Select Distinct H.PromoID,Nama,Metod,Paket From T_Promo H Inner Join T_PromoDtl D On H.PromoID=D.PromoID Where '" & Me.DTPTanggal.EditValue & "'>=TglAwal and '" & Me.DTPTanggal.EditValue & "' <=TglAkhir and JnsCust in ('" & Me.SLUJnsCust.Text & "','%') and DiperhitSaat='Faktur' and Gol='" & Gol & "'", koneksi)
                    cmsl.TableMappings.Add("Table", "T_PromoFt" & Gol)
                    cmsl.Fill(DsMaster, "T_PromoFt" & Gol)
                    DsMaster.Tables("T_PromoFt" & Gol).Clear()
                    cmsl.Fill(DsMaster, "T_PromoFt" & Gol)

                    Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
                    Me.SLUPromo.Properties.DisplayMember = "Nama"
                    Me.SLUPromo.Properties.ValueMember = "PromoID"

                    Me.SLUPromo.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("PromoID")

                    If Me.SLUPromo.EditValue = "" Then
                        Me.TBTamDiscP.Properties.ReadOnly = False
                        Me.TBTamDiscRp.Properties.ReadOnly = False
                        Me.TBPaket.EditValue = ""
                        Metode = ""
                    Else
                        Me.TBPaket.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("Paket")
                        Metode = DsMaster.Tables("T_PromoFt" & Gol).Select("PromoID = '" & Me.SLUPromo.EditValue & "' and Paket='" & Me.TBPaket.EditValue & "'")(0).Item("Metod")

                        Me.TBTamDiscP.Properties.ReadOnly = True
                        Me.TBTamDiscRp.Properties.ReadOnly = True

                        Me.TBTamDiscP.EditValue = 0
                        Me.TBTamDiscRp.EditValue = 0
                    End If

                    If Metode = "Gratis Barang" Then
                        Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If

                    Me.RBPPn.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("TipePPn")
                    Me.TBPersenPPn.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("PersenPPn")

                    stsPPn = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("stsPPn")

                    cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as JualIDD,'" & Me.TBID.EditValue & "' As JualID, SJIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,0 as Qty,0 as Dos, 0 as Psg,stsHarga,0.0 As HarSbDisc,DiscOB,0.0 as RpDiscOB,0.0 As RpDiscCust,0.0 As RpDiscL,OngkirSat,Ongkir,0.0 As HarAkhir,0.0 as Selisih,0.0 As SelisihExtra,0 As Urut, '' As NamaLain,'' As Ket From T_SJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where SJID='" & Me.SLUSJID.EditValue & "' and SisaKirim >0 Order By D.ArtCode Asc", koneksi)
                Else
                    cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as JualIDD,'" & Me.TBID.EditValue & "' As JualID, POIDD as SJIDD,D.ArtCode,ArtName,D.SatID,D.Isi,Harga As HarDPJ,Harga As HarSat,0 as Qty,0 as Dos, 0 as Psg,stsHarga,0.0 As HarSbDisc,DiscOB,0.0 as RpDiscOB,0.0 As RpDiscCust,0.0 As RpDiscL,0.0 as OngkirSat,0.0 as Ongkir,0.0 As HarAkhir,0.0 as Selisih,0.0 As SelisihExtra,0 As Urut, '' As NamaLain,'' As Ket  From T_POBJJODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & Me.SLUJnsCust.EditValue & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True' and MtUang='" & Me.SLUMtUang.EditValue & "' and POID='" & Me.SLUSJID.EditValue & "' and SisaKirim >0 Order By D.ArtCode Asc", koneksi)
                End If

                Me.SLUMtUang.EditValue = DsMaster.Tables("T_SJ" & Gol).Select("SJID = '" & Me.SLUSJID.EditValue & "'")(0).Item("MtUang")

                CekCurr()

                cmsl.TableMappings.Add("Table", "T_JualBJDtl")
                cmsl.Fill(DsMaster, "T_JualBJDtl")
                DsMaster.Tables("T_JualBJDtl").Clear()
                cmsl.Fill(DsMaster, "T_JualBJDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_JualBJDtl"

                CekPromo()

            Catch ex As Exception

            End Try

            Me.SLUSales.Properties.ReadOnly = True
            Me.SLUCust.Properties.ReadOnly = True
            Me.CBOJnsFt.Properties.ReadOnly = True
            Me.CBOKat.Properties.ReadOnly = True
            Me.SLUPromo.Properties.ReadOnly = True

        Else
            Me.SLUSales.Properties.ReadOnly = False
            Me.SLUCust.Properties.ReadOnly = False
            Me.CBOJnsFt.Properties.ReadOnly = False
            Me.CBOKat.Properties.ReadOnly = False
            Me.SLUPromo.Properties.ReadOnly = False

        End If
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.Properties.ReadOnly = False Then
            Try
                If Not IsDBNull(Me.SLUSJID.EditValue) And Me.SLUSJID.EditValue = "" Then
                    If Not IsDBNull(Me.SLUCust.EditValue) Then
                        Dim cmsl As SqlDataAdapter
                        cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                        cmsl.TableMappings.Add("Table", "M_JnsCustFt" & Gol)
                        cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)
                        DsMaster.Tables("M_JnsCustFt" & Gol).Clear()
                        cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)

                        Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustFt" & Gol)
                        Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                        Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"

                        Me.SLUJnsCust.EditValue = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JnsCustID")
                        Me.TBHarga.EditValue = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("Harga")
                        JT = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")

                        If Me.TBHarga.EditValue = "DPJ" Or Me.TBHarga.EditValue = "JO" Then
                            Ongkir = 0
                        Else
                            Ongkir = MainModule.OngkosKirim
                        End If

                        Me.TBDiscCust.EditValue = DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("DiscCust")

                        cmsl = New SqlDataAdapter("Select Distinct H.PromoID,Nama,Metod,Paket From T_Promo H Inner Join T_PromoDtl D On H.PromoID=D.PromoID Where '" & Me.DTPTanggal.EditValue & "'>=TglAwal and '" & Me.DTPTanggal.EditValue & "' <=TglAkhir and JnsCust in ('" & Me.SLUJnsCust.Text & "','%') and DiperhitSaat='Faktur' and Gol='" & Gol & "'", koneksi)
                        cmsl.TableMappings.Add("Table", "T_PromoFt" & Gol)
                        cmsl.Fill(DsMaster, "T_PromoFt" & Gol)
                        DsMaster.Tables("T_PromoFt" & Gol).Clear()
                        cmsl.Fill(DsMaster, "T_PromoFt" & Gol)

                        Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
                        Me.SLUPromo.Properties.DisplayMember = "Nama"
                        Me.SLUPromo.Properties.ValueMember = "PromoID"
                    End If
                End If
            Catch ex As Exception

            End Try

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If

    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            If Manual = False Then
                If Not IsDBNull(Me.SLUSJID.EditValue) And Me.SLUSJID.EditValue <> "" Then
                    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                        Me.GridView1.SetRowCellValue(i, "Qty", 0)
                    Next
                Else

                    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                        Me.GridView1.DeleteRow(i)
                    Next

                    Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                        ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                        arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "JualIDD")

                        Me.GridView6.DeleteRow(x)
                    Next
                End If

            End If

            If Me.SLUPromo.EditValue <> "" Then
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as JualIDD,'" & Me.TBID.EditValue & "' As JualID, SJIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,0 as Qty,0 as Dos, 0 as Psg,stsHarga,0 As HarSbDisc,DiscOB,0 as RpDiscOB,0 As RpDiscCust,0 As RpDiscL,OngkirSat,Ongkir,0 As HarAkhir,0 as Selisih,0 As SelisihExtra,0 As Urut, '' As NamaLain From T_SJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where SJID='" & Me.SLUSJID.EditValue & "' and SisaKirim >0 Order By D.ArtCode Asc", koneksi)

                cmsl.TableMappings.Add("Table", "T_JualBJDtl")
                cmsl.Fill(DsMaster, "T_JualBJDtl")
                DsMaster.Tables("T_JualBJDtl").Clear()
                cmsl.Fill(DsMaster, "T_JualBJDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_JualBJDtl"
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("JualIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            rw2 = 0

            'If Gol = "Job Order" Then
            If Not IsDBNull(Me.SLUSJID.EditValue) And Me.SLUSJID.EditValue = "" Then
                Dim frm As New FFtBJ_a(Me.SLUJnsCust.Text, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue, Manual, Me.SLUPromo.EditValue, Metode, Me.TBPaket.EditValue, "Semua", Me.SLUMtUang.EditValue)
                frm.ShowDialog()

                Try
                    If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                        Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                            If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                                Me.GridView1.AddNewRow()
                            End If
                        Next
                    End If

                    If Metode = "Gratis Barang" Then
                        Me.GridView6.AddNewRow()
                        If Not IsDBNull(dataTrans2.Item("Baris").ToString) And CInt(dataTrans2.Item("Baris").ToString) > 0 Then
                            Dim i : For i = 0 To CInt(dataTrans2.Item("Baris").ToString) - 1
                                If i <> CInt(dataTrans2.Item("Baris").ToString) - 1 Then
                                    Me.GridView6.AddNewRow()
                                End If
                            Next
                        End If
                    End If

                Catch ex As Exception

                End Try
            End If
            'Else
            '    If Not IsDBNull(Me.SLUSJID.EditValue) And Me.SLUSJID.EditValue = "" Then
            '        XtraMessageBox.Show("ID Nota Pesanan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Exit Sub
            '    End If
            'End If
        End If
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        koneksi.Close()

        If e.Column Is GridView1.Columns("Qty") Then
            Dim Stok As Integer = 0
            Dim Tot As Integer = 0
            Dim PO As Integer = 0
            Dim Qty As Integer = 0

            Dim i : For i = 0 To Me.GridView6.RowCount - 1
                If Me.GridView6.GetRowCellValue(i, "ArtCode") = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") Then
                    Tot += Me.GridView6.GetRowCellValue(i, "Qty")
                End If
            Next

            If Manual = False Then
                Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBID.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                Dim command2 As New SqlCommand("Select Isnull((Select Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_JualBJ H Inner Join T_JualBJDtl JD On H.JualID=JD.JualID Where SJID='" & Me.SLUSJID.EditValue & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and H.JualID<>'" & Me.TBKode.EditValue & "'),0)) From T_POBJJODtl where POID='" & Me.SLUSJID.EditValue & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "'),0)", koneksi)

                With koneksi
                    .Open()
                    PO = command2.ExecuteScalar()
                    .Close()
                End With

                If Me.SLUSJID.EditValue <> "" Then
                    If PO > Stok Then
                        Qty = Stok
                    Else
                        Qty = PO
                    End If
                Else
                    Qty = Stok
                End If

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Tot > Qty Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok/Qty PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Qty)
                End If
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "Ongkir", Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirSat") * Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Or Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "PCS" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

            Dim HargaSbDisc As Decimal
            HargaSbDisc = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round(HargaSbDisc * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscL") + Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir"), 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtra", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Selisih"), 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir")) * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSbDisc") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB"), 2))

            Me.GridControl1_Leave(sender, e)

            'Me.BSave.Focus()
            'CekPromo()
            'Me.GridView1.Focus()

        ElseIf e.Column Is GridView1.Columns("RpDiscL") Then
            Dim HargaSbDisc As Decimal
            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty")) Then
                HargaSbDisc = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscL") + Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir"), 2))

                Me.GridControl1_Leave(sender, e)
            End If


        End If
    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "JualIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "JualID", Me.TBID.EditValue)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Urut", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SJIDD", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "NamaLain", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "stsHarga", dataTrans.Item("stsHarga" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarDPJ", dataTrans.Item("Harga" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Gol", dataTrans.Item("Gol" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "DiscOB", dataTrans.Item("DiscOB" & rw).ToString)

                If dataTrans.Item("Gol" & rw).ToString = "Promosi" Then
                    Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "OngkirSat", 0)
                Else
                    Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", CDec(dataTrans.Item("Harga" & rw).ToString) + (Ongkir * CInt(dataTrans.Item("Isi" & rw).ToString)))
                    Me.GridView1.SetRowCellValue(e.RowHandle, "OngkirSat", Ongkir * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))
                End If

                Me.GridView1.SetRowCellValue(e.RowHandle, "Selisih", dataTrans.Item("Selisih" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscL", dataTrans.Item("RpDiscL" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                rw += 1

                DsMaster.Tables("T_JualBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBJDtl" & Gol).Columns("JualID"), DsMaster.Tables("T_JualBJDtl" & Gol).Columns("ArtCode")}
            End If
        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try

    End Sub
    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetFocusedDataRow.Item("JualIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            rw2 = 0

            Dim frm As New FFtBJ_a(Me.SLUJnsCust.Text, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue, Manual, Me.SLUPromo.EditValue, Metode, Me.TBPaket.EditValue, "Free", Me.SLUMtUang.EditValue)
            frm.ShowDialog()

            Try
                If Metode = "Gratis Barang" Then
                    Me.GridView6.AddNewRow()
                    If Not IsDBNull(dataTrans2.Item("Baris").ToString) Then
                        Dim i : For i = 0 To CInt(dataTrans2.Item("Baris").ToString) - 1
                            If i <> CInt(dataTrans2.Item("Baris").ToString) - 1 Then
                                Me.GridView6.AddNewRow()
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception

            End Try
        End If

    End Sub
    Private Sub GridView6_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView6.CellValueChanged
        If e.Column Is GridView6.Columns("Qty") Then
            Dim Stok As Integer
            Dim Tot As Integer = 0

            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(i, "ArtCode") = Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") Then
                    Tot += Me.GridView1.GetRowCellValue(i, "Qty")
                End If
            Next

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBID.EditValue & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView6.GetRowCellValue(e.RowHandle, "Qty") + Tot > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView6.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If

            Me.GridView6.SetRowCellValue(e.RowHandle, "Psg", Me.GridView6.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView6.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView6.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView6.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView6.SetRowCellValue(e.RowHandle, "Dos", Me.GridView6.GetRowCellValue(e.RowHandle, "Qty"))
            End If

            Me.BSave.Focus()
            Me.GridView6.Focus()
        End If
    End Sub
    Private Sub GridView6_DoubleClick(sender As Object, e As EventArgs) Handles GridView6.DoubleClick
        If Me.GridView6.OptionsBehavior.Editable = True Then
            Dim frm As New FSearch("JualPromo", Me.SLUPromo.EditValue, Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Paket"), Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue)
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("ArtCode").ToString) Then
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "Qty", 0)
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GridView6_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView6.InitNewRow
        Try
            Me.GridView6.SetRowCellValue(e.RowHandle, "JualIDD", Me.GridView6.RowCount)
            Me.GridView6.SetRowCellValue(e.RowHandle, "JualID", Me.TBKode.EditValue)
            Me.GridView6.SetRowCellValue(e.RowHandle, "SJIDD", 0)
            Me.GridView6.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans2.Item("ArtCode" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "ArtName", dataTrans2.Item("ArtName" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "SatID", dataTrans2.Item("SatID" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "Isi", dataTrans2.Item("Isi" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "Qty", dataTrans2.Item("Qty" & rw2).ToString)
            rw2 += 1
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
        Try
            If Me.GridView1.OptionsBehavior.Editable = True Then
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscOB.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscCust.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                HitPPn()
                CekPromo()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TBTamDiscRp_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscRp.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub

    Private Sub TBTamDiscP_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscP.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTotDiscL.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100
        HitPPn()
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FFtBJ_d(Me.GridView2.GetFocusedDataRow.Item("JualID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct H.PromoID,Nama,Metod,Paket,DiperhitSaat From T_Promo H Inner Join T_PromoDtl D On H.PromoID=D.PromoID Where (TglAwal <= '" & Me.DTPTanggal.EditValue & "') AND (TglAkhir >= '" & Me.DTPTanggal.EditValue & "') and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_PromoFt" & Gol)
        cmsl.Fill(DsMaster, "T_PromoFt" & Gol)
        DsMaster.Tables("T_PromoFt" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_PromoFt" & Gol)

        Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
        Me.SLUPromo.Properties.DisplayMember = "Nama"
        Me.SLUPromo.Properties.ValueMember = "PromoID"

        If Manual = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "JualIDD")

                Me.GridView6.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub SLUPromo_Leave(sender As Object, e As EventArgs) Handles SLUPromo.Leave
        If Me.SLUPromo.Properties.ReadOnly = False Then
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "JualIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "JualIDD")

                Me.GridView6.DeleteRow(x)
            Next

            If Me.SLUPromo.EditValue = "" Then
                Me.TBTamDiscP.Properties.ReadOnly = False
                Me.TBTamDiscRp.Properties.ReadOnly = False
                Me.TBPaket.EditValue = ""
                Metode = ""
            Else
                Me.TBPaket.EditValue = Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Paket")
                Metode = Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Metod")

                Me.TBTamDiscP.Properties.ReadOnly = True
                Me.TBTamDiscRp.Properties.ReadOnly = True

                Me.TBTamDiscP.EditValue = 0
                Me.TBTamDiscRp.EditValue = 0
            End If

            If Metode = "Gratis Barang" Then
                Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView2.GetRowCellValue(e.RowHandle, "stsPrint") = False Then
                    If Me.GridView2.GetRowCellValue(e.RowHandle, "Pusat") = True Then
                        e.Appearance.ForeColor = Color.Black
                        e.Appearance.BackColor = Color.OrangeRed
                    Else
                        e.Appearance.ForeColor = Color.Black
                        e.Appearance.BackColor = Color.Yellow
                    End If
                Else
                    If Me.GridView2.GetRowCellValue(e.RowHandle, "Pusat") = True Then
                        e.Appearance.ForeColor = Color.Black
                        e.Appearance.BackColor = Color.LightGreen
                    Else
                        e.Appearance.ForeColor = Nothing
                        e.Appearance.BackColor = Nothing
                    End If
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPajak.KeyPress, TBPEB.KeyPress, TBBL.KeyPress, TBLC.KeyPress, MKet.KeyPress
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
