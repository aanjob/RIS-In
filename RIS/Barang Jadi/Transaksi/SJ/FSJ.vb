Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI

Public Class FSJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1) As String
    Dim KdLama, CodeID, LPRIDLama, JnsLama, CurrID, Metode, JnsPerhit, Kelipatan, BeliMin, JnsPot, Gol As String
    Dim Manual, MnlInsUpd, stsPPn As Boolean
    Dim RowIdx As Integer
    Dim rw As Integer = 0
    Dim rw2 As Integer = 0
    Dim Ongkir, Pot, TotFree As Decimal

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

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SJN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTSJ_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUNPID.Properties.ReadOnly = True
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
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTSJ_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        'Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUNPID.Properties.ReadOnly = False
        Me.SLUSales.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.CBOJnsFt.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.SLUPromo.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        'Me.TBTamDiscP.Properties.ReadOnly = False
        'Me.TBTamDiscRp.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSJ_e.Selected = True

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

        If Gol = "Job Order" Then
            cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='" & Gol & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_SalesLUE" & Gol)
            Try
                DsMaster.Tables("M_SalesLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_SalesLUE" & Gol)
        Else
            cmsl = New SqlDataAdapter("Select SalID,Area,Nama From M_Sales Where Aktif='True' and Gol='Lokal'", koneksi)
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
        'Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

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
            DsMaster.Tables("T_SJDtl" & Gol).Clear()
            DsMaster.Tables("T_SJFree" & Gol).Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SJIDD,SJID,NPIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,Qty,Dos,Psg,stsHarga,HarSbDisc,DiscOB, RpDiscOB,RpDiscCust,OngkirSat,Ongkir,HarAkhir,Selisih,SelisihExtra From T_SJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where SJID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_SJDtl" & Gol)
        cmsl.Fill(DsMaster, "T_SJDtl" & Gol)

        DsMaster.Tables("T_SJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJDtl" & Gol).Columns("SJID"), DsMaster.Tables("T_SJDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SJDtl" & Gol

        cmsl = New SqlDataAdapter("Select SJIDD,SJID,NPIDD,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg From T_SJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where SJID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_SJFree" & Gol)
        cmsl.Fill(DsMaster, "T_SJFree" & Gol)

        DsMaster.Tables("T_SJFree" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJFree" & Gol).Columns("SJID"), DsMaster.Tables("T_SJFree" & Gol).Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_SJFree" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SJID,PeriodID,H.CodeID,H.Tanggal,H.NPID,JnsTrans,H.PromoID,H.Paket,P.Nama As Promo,H.CabID,Cb.Cabang, H.SalID,S.Nama As Sales,H.GdID,G.Nama as Gudang,H.CustID,C.Nama As Customer,C.Alamat,K.Nama As Kota,H.JnsCustID,H.Jenis,H.Harga,Ongkir, H.DiscCust,MtUang,CurrID,NilTukarRp,Kat,TipePPn,PersenPPn,TotSbDisc,TotOngkir,TotRpDiscCust,TotRpDiscOB,DiscP,RpDiscP,DiscRp,TotDPP, TotPPn,TotAkhir,TotAkhirRp,TotQty,TotDos,TotPsg,H.Ket,H.Grup,H.Gol,H.stsPPn,H.stsApp,H.stsPrint,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy, H.AppDate,H.AppBy,H.PrintDate,H.PrintBy From T_SJ H Inner Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Sales S On H.SalID=S.SalID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join T_Promo P On H.PromoID=P.PromoID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By SJID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJ" & Gol)
        Try
            DsMaster.Tables("T_SJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SJ" & Gol

        If MainModule.NoHarga = True Then
            Me.GridColumn36.Visible = False
            Me.GridColumn39.Visible = False
            Me.GridColumn42.Visible = False
            Me.GridColumn43.Visible = False
            Me.GridColumn44.Visible = False
            Me.GridColumn45.Visible = False
            Me.GridColumn48.Visible = False
            Me.GridColumn49.Visible = False
            Me.GridColumn50.Visible = False
            Me.GridColumn51.Visible = False

        Else
            Me.GridColumn36.Visible = True
            Me.GridColumn39.Visible = True
            Me.GridColumn42.Visible = True
            Me.GridColumn43.Visible = True
            Me.GridColumn44.Visible = True
            Me.GridColumn45.Visible = True
            Me.GridColumn48.Visible = True
            Me.GridColumn49.Visible = True
            Me.GridColumn50.Visible = True
            Me.GridColumn51.Visible = True
        End If
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

        Dim cmSP As New SqlCommand("SPDelT_SJ")
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

    Private Sub FSJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Surat Jalan Barang Jadi"
    End Sub

    Private Sub FSJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        dataTrans2 = New Collection
        dataTrans2.Clear()

        CekSave = False

        Me.Dispose()
    End Sub

    Private Sub FSJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FSJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTSJ_e.Selected = True
    End Sub
    Private Sub BVTSJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSJ_s.ItemPressed, BVTSJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Surat Jalan Barang Jadi"
        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SJEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("SJApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SJDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SJP"), Boolean)
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("SJPH"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Surat Jalan Barang Jadi"

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

        Me.TBKode.EditValue = "--"
        Me.SLUGrup.EditValue = ""
        Me.SLUNPID.EditValue = ""
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
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTotDiscOB.EditValue = 0
        Me.TBTotDiscCust.EditValue = 0
        Me.TBTotDiscL.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTamDiscRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_SJDtl" & Gol).Clear()

        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        CekCurr()


        If Me.SLUPromo.EditValue = "" Then
            Me.TBTamDiscP.Properties.ReadOnly = False
            Me.TBTamDiscRp.Properties.ReadOnly = False

        Else
            Me.TBTamDiscP.Properties.ReadOnly = True
            Me.TBTamDiscRp.Properties.ReadOnly = True

            Me.TBTamDiscP.EditValue = 0
            Me.TBTamDiscRp.EditValue = 0
        End If

        'If Gol = "Job Order" Then
        '    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
        'Else
        '    Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
        'End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Surat Jalan Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        If SlCek("T_SJ", "stsApp", "SJID", Me.GridView2.GetFocusedDataRow.Item("SJID")) = False Then
            LUE()

            Dim cmsl As SqlDataAdapter

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJID")
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")

            cmsl = New SqlDataAdapter("Select NPID From T_NotPes Where CabID='" & Me.SLUCab.EditValue & "' and stsApp='True' and stsKirim='False' or NPID = (Select NPID From T_SJ where SJID ='" & Me.TBKode.EditValue & "')", koneksi)
            cmsl.TableMappings.Add("Table", "T_NotPes" & Gol)
            Try
                DsMaster.Tables("T_NotPes" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "T_NotPes" & Gol)

            Me.SLUNPID.Properties.DataSource = DsMaster.Tables("T_NotPes" & Gol)
            Me.SLUNPID.Properties.DisplayMember = "NPID"
            Me.SLUNPID.Properties.ValueMember = "NPID"

            Me.SLUNPID.EditValue = Me.GridView2.GetFocusedDataRow.Item("NPID")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUSales.EditValue = Me.GridView2.GetFocusedDataRow.Item("SalID")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")

            If Not IsDBNull(Me.SLUCust.EditValue) Then
                cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_JnsCustFt" & Gol)
                Try
                    DsMaster.Tables("M_JnsCustFt" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)

                Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustFt" & Gol)
                Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"
            End If

            If Not IsDBNull(Me.SLUCab.EditValue) Then
                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota,JnsCustID,DiscCust,JT,Harga,stsBlok,stsBlokMnl From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustFt" & Gol)
                Try
                    DsMaster.Tables("M_CustFt" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_CustFt" & Gol)

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustFt" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"

                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Gol='" & Gol & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
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

            Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
            Me.CBOJnsFt.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            JnsLama = Me.GridView2.GetFocusedDataRow.Item("Jenis")
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
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
            Me.TBTotSbDisc.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotSbDisc")
            Me.TBTotDiscOB.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB")
            Me.TBTotDiscCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust")
            'Me.TBTotDiscL.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotRpDiscL")
            Me.TBTamDiscP.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscP")
            Me.TBTamDiscPRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("RpDiscP")
            Me.TBTamDiscRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiscRp")
            Me.TBTotAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
            Me.TBTotDPP.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDPP")
            Me.TBTotPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPPn")

            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=54 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

            FillDtl(Me.TBKode.EditValue)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            ReDim arrPar(-1)
            ReDim arrPar2(-1)

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
            Me.SLUNPID.Properties.ReadOnly = True
            Me.SLUGrup.Properties.ReadOnly = True
            Me.DTPTanggal.Properties.ReadOnly = True
            Me.SLUCust.Properties.ReadOnly = True
            Me.CBOJnsFt.Properties.ReadOnly = True
            Me.SLUPromo.Properties.ReadOnly = True

        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Tagihan Atau Lunas", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        'If MainModule.SlAppSJ(Me.GridView2.GetFocusedDataRow.Item("InsDate")) > 0 Then
        '    XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Ada Dokumen Sebelumnya Yang Belum Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If SlCek("T_SJ", "stsApp", "SJID", Me.GridView2.GetFocusedDataRow.Item("SJID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmSP As New SqlCommand("SPAppSJ")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@KdSJ", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SJID")
            .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabID")
            .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsTrans")
            .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CustID")
            .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("JnsCustID")
            .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MtUang")
            .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Gol")
            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdID")
            '.Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.GridView2.GetFocusedDataRow.Item("TotAkhir")
            '.Parameters.Add("@TotQty", SqlDbType.Int).Value = Me.GridView2.GetFocusedDataRow.Item("TotQty")
            .Parameters.Add("@Posisi", SqlDbType.VarChar).Value = MainModule.Posisi
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi


            Try
                With koneksi
                    .Open()
                    cmSP.CommandTimeout = 90000
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With

                If x = 0 Then
                    XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    FillDt()
                ElseIf x = 3 Then
                    XtraMessageBox.Show("Yang Berhak Approve Pusat", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Surat Jalan Barang Jadi"

        koneksi.Close()

        If SlCek("T_SJ", "stsApp", "SJID", Me.GridView2.GetFocusedDataRow.Item("SJID")) = False Then

            If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
                XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("SJID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_SJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SJID")
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

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("NPID"), "NPID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Promo"), "Promo")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Paket"), "Paket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
   
        Dim XR As New XRSJ
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBPrintH_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintH.ItemClick
        koneksi.Close()

        Dim Kode As String
        Kode = Me.GridView2.GetFocusedDataRow.Item("SJID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "SJID") = Kode Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("NPID"), "NPID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Promo"), "Promo")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Paket"), "Paket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") + Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") + Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"), "TotDisc")

        If Me.GridView2.GetFocusedDataRow.Item("TipePPn") <> "Exclude" Then
            bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir") - (Me.GridView2.GetFocusedDataRow.Item("TotSbDisc") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") - (Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp"))), "TotByExp")
        Else
            bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir") - (Me.GridView2.GetFocusedDataRow.Item("TotSbDisc") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscCust") - Me.GridView2.GetFocusedDataRow.Item("TotRpDiscOB") - (Me.GridView2.GetFocusedDataRow.Item("RpDiscP") + Me.GridView2.GetFocusedDataRow.Item("DiscRp")) + Me.GridView2.GetFocusedDataRow.Item("TotPPn")), "TotByExp")
        End If
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPPn"), "TotPPn")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")

        Dim cmSP As New SqlCommand("SPUpstsPrint")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SJID")
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

        'If DateTime.Now > DateAdd(DateInterval.Day, 7, CDate(bind.Item("Tanggal"))) Then
        '    'Dim XR As New XRSJNew
        '    Dim XR As New XRSJ
        '    XR.InitializeData(bind)

        '    Dim printTool As New ReportPrintTool(XR)

        '    ' Access the Print Tool's Printing System. 
        '    Dim printingSystem As PrintingSystemBase = printTool.PrintingSystem

        '    printingSystem.SetCommandVisibility(New PrintingSystemCommand() {PrintingSystemCommand.PrintDirect}, CommandVisibility.None)

        '    printingSystem.SetCommandVisibility(New PrintingSystemCommand() {PrintingSystemCommand.Print}, CommandVisibility.None)

        '    ' Disable document export to any format. 
        '    printingSystem.SetCommandVisibility(New PrintingSystemCommand() { _
        '        PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, _
        '        PrintingSystemCommand.ExportHtm, PrintingSystemCommand.ExportMht, PrintingSystemCommand.ExportPdf, _
        '        PrintingSystemCommand.ExportRtf, PrintingSystemCommand.ExportXls, PrintingSystemCommand.ExportXlsx, _
        '        PrintingSystemCommand.ExportGraphic}, CommandVisibility.None)

        '    ' Disable export and mailing of documents. 
        '    printingSystem.SetCommandVisibility(New PrintingSystemCommand() { _
        '        PrintingSystemCommand.SendFile}, _
        '        CommandVisibility.None)

        '    printTool.ShowPreviewDialog()

        'Else
        Dim XR As New XRSJH
        XR.InitializeData(bind)

        'End If
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
        'Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

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

        If Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2) = 0 Then
            XtraMessageBox.Show("Stok Barang Di Gudang yang Dipilih Tidak Ada", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Metode = "Gratis Barang" Then
            If CType(Me.GridView6.Columns("Qty").SummaryText, Integer) <> TotFree Then
                XtraMessageBox.Show("Jumlah Barang Gratis Tidak Sesuai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_SJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@NPID", SqlDbType.VarChar).Value = Me.SLUNPID.EditValue
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
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
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotOngkir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Ongkir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscCust", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscOB", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_SJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@NPIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "NPIDD")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                    .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                    .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                    .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                    .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                    .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                    .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
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
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SJFree")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@NPIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "NPIDD")
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
                Dim cmSP As New SqlCommand("SPUpT_SJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@NPID", SqlDbType.VarChar).Value = Me.SLUNPID.EditValue
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
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
                    .Parameters.Add("@DiscCust", SqlDbType.Decimal).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Kat", SqlDbType.VarChar).Value = Me.CBOKat.EditValue
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotSbDisc", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotOngkir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Ongkir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscCust", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscOB", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_SJDtl")
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_SJFree")
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
                            If Me.GridView1.GetRowCellValue(i, "SJIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@NPIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "NPIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
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
                                        Me.GridView1.SetRowCellValue(i, "SJIDD", Me.GridView1.GetRowCellValue(i, "SJIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_SJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@NPIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "NPIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
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
                                If Me.GridView6.GetRowCellValue(z, "SJIDD") > 0 Then
                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_SJFree")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                            .Parameters.Add("@NPIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "NPIDD")
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
                                            Me.GridView1.SetRowCellValue(i, "SJIDD", Me.GridView1.GetRowCellValue(i, "SJIDD") * -1)
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_SJFree")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SJIDD")
                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 54
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                            .Parameters.Add("@NPIDD", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "NPIDD")
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

        Dim cmd2 As New SqlCommand("SPAftSSJBJ")
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
            If Not IsDBNull(Me.SLUNPID.EditValue) Or Me.SLUNPID.EditValue = "" Then
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
                        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")

                        Me.GridView1.DeleteRow(i)
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.Properties.ReadOnly = False Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=54 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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
                Me.TBKode.Properties.ReadOnly = False
                Me.TBKode.EditValue = ""
            Else
                Me.TBKode.Properties.ReadOnly = True
                Me.TBKode.EditValue = "--"
            End If

            Try
                If Not IsDBNull(Me.SLUCab.EditValue) Then
                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select NPID,JnsTrans,PromoID,Paket,SalID,CustID,JnsCustID,DiscCust,Harga,Ongkir,MtUang,Kat,stsPPN, TipePPn,PersenPPn,Grup From T_NotPes Where CabID='" & Me.SLUCab.EditValue & "' and stsApp='True' and stsKirim='False' and Gol='" & Gol & "' or NPID = (Select NPID From T_SJ where SJID ='" & Me.TBKode.EditValue & "')", koneksi)
                    cmsl.TableMappings.Add("Table", "T_NotPes" & Gol)
                    Try
                        DsMaster.Tables("T_NotPes" & Gol).Clear()
                    Catch ex As Exception

                    End Try
                    cmsl.Fill(DsMaster, "T_NotPes" & Gol)

                    Me.SLUNPID.Properties.DataSource = DsMaster.Tables("T_NotPes" & Gol)
                    Me.SLUNPID.Properties.DisplayMember = "NPID"
                    Me.SLUNPID.Properties.ValueMember = "NPID"

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

                    cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota,JnsCustID,DiscCust,JT,Harga,stsBlok,stsBlokMnl From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
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

            Me.SLUCust.EditValue = ""
            Me.SLUGd.EditValue = ""
            Me.SLUNPID.EditValue = ""
            Me.CBOKat.EditValue = "Lokal"

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "SJIDD")

                Me.GridView6.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub SLUNPID_Leave(sender As Object, e As EventArgs) Handles SLUNPID.Leave
        If Not IsDBNull(Me.SLUNPID.EditValue) And Me.SLUNPID.EditValue <> "" And Me.SLUNPID.Properties.ReadOnly = False Then
            Try
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "SJIDD")

                    Me.GridView6.DeleteRow(x)
                Next

                Me.SLUGrup.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("Grup")
                Me.SLUSales.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("SalID")

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select GdID,Nama,Def From M_Gudang where GdID In (Select GdIDCab From T_NotPes where NPID='" & Me.SLUNPID.EditValue & "') Or GdID In (Select GdIDPst From T_NotPes where NPID='" & Me.SLUNPID.EditValue & "')", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE")
                cmsl.Fill(DsMaster, "M_GudangLUE")
                DsMaster.Tables("M_GudangLUE").Clear()
                cmsl.Fill(DsMaster, "M_GudangLUE")

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"

                Me.SLUCust.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("CustID")

                cmsl = New SqlDataAdapter("Select JnsCustID,Jenis,Harga From M_JnsCust Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_JnsCustFt" & Gol)
                cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)
                DsMaster.Tables("M_JnsCustFt" & Gol).Clear()
                cmsl.Fill(DsMaster, "M_JnsCustFt" & Gol)

                Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCustFt" & Gol)
                Me.SLUJnsCust.Properties.DisplayMember = "Jenis"
                Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"

                Me.SLUJnsCust.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("JnsCustID")
                Me.TBHarga.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("Harga")
                Ongkir = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("Ongkir")
                Me.TBDiscCust.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("DiscCust")
                Me.CBOKat.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("Kat")

                cmsl = New SqlDataAdapter("Select Distinct H.PromoID,Nama,Metod,Paket From T_Promo H Inner Join T_PromoDtl D On H.PromoID=D.PromoID Where '" & Me.DTPTanggal.EditValue & "'>=TglAwal and '" & Me.DTPTanggal.EditValue & "' <=TglAkhir and JnsCust in ('" & Me.SLUJnsCust.Text & "','%') and DiperhitSaat='Faktur' and Gol='" & Gol & "'", koneksi)
                cmsl.TableMappings.Add("Table", "T_PromoFt" & Gol)
                cmsl.Fill(DsMaster, "T_PromoFt" & Gol)
                DsMaster.Tables("T_PromoFt" & Gol).Clear()
                cmsl.Fill(DsMaster, "T_PromoFt" & Gol)

                Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
                Me.SLUPromo.Properties.DisplayMember = "Nama"
                Me.SLUPromo.Properties.ValueMember = "PromoID"

                Me.SLUPromo.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("PromoID")

                If Me.SLUPromo.EditValue = "" Then
                    Me.TBTamDiscP.Properties.ReadOnly = False
                    Me.TBTamDiscRp.Properties.ReadOnly = False
                    Me.TBPaket.EditValue = ""
                    Metode = ""
                Else
                    Me.TBPaket.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("Paket")
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

                Me.RBPPn.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("TipePPn")
                Me.TBPersenPPn.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("PersenPPn")
                stsPPn = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("stsPPn")
                Me.SLUMtUang.EditValue = DsMaster.Tables("T_NotPes" & Gol).Select("NPID = '" & Me.SLUNPID.EditValue & "'")(0).Item("MtUang")

                CekCurr()

                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as SJIDD,'" & Me.TBKode.EditValue & "' As SJID, NPIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,0 as Qty,0 as Dos, 0 as Psg,stsHarga,0 As HarSbDisc,DiscOB,0 as RpDiscOB,0 As RpDiscCust,0 As RpDiscL,OngkirSat,Ongkir,0 As HarAkhir,0 as Selisih,0 As SelisihExtra,0 As Urut, '' As NamaLain From T_NotPesDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Me.SLUNPID.EditValue & "' and SisaKirim >0 Order By D.ArtCode Asc", koneksi)

                cmsl.TableMappings.Add("Table", "T_SJDtl")
                cmsl.Fill(DsMaster, "T_SJDtl")
                DsMaster.Tables("T_SJDtl").Clear()
                cmsl.Fill(DsMaster, "T_SJDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_SJDtl"

                CekPromo()

            Catch ex As Exception

            End Try

            Me.SLUSales.Properties.ReadOnly = True
            Me.SLUCust.Properties.ReadOnly = True
            Me.CBOJnsFt.Properties.ReadOnly = True
            Me.CBOKat.Properties.ReadOnly = True
            Me.SLUPromo.Properties.ReadOnly = True
            Me.SLUGrup.Properties.ReadOnly = True

        Else
            Me.SLUSales.Properties.ReadOnly = False
            Me.SLUCust.Properties.ReadOnly = False
            Me.CBOJnsFt.Properties.ReadOnly = False
            Me.CBOKat.Properties.ReadOnly = False
            Me.SLUPromo.Properties.ReadOnly = False
            Me.SLUGrup.Properties.ReadOnly = False

        End If
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.Properties.ReadOnly = False Then
            Try
                If Not IsDBNull(Me.SLUNPID.EditValue) Or Me.SLUNPID.EditValue = "" Then
                    If Not IsDBNull(Me.SLUCust.EditValue) Then
                        If DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("stsBlok") = True Then
                            If DsMaster.Tables("M_CustFt" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("stsBlokMnl") = True Then
                                XtraMessageBox.Show("Customer Sudah Diblok. Silakan Hubungi Manager Marketing Beserta Email Form Release Customer", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                Me.SLUCust.EditValue = ""
                                Me.SLUJnsCust.EditValue = ""
                                Me.TBHarga.EditValue = ""
                                Me.TBDiscCust.EditValue = 0.0

                                Dim a : For a = Me.GridView1.RowCount - 1 To 0 Step -1
                                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(a, "SJIDD")

                                    Me.GridView1.DeleteRow(a)
                                Next

                                Exit Sub
                            End If
                        End If

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
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "SJIDD")

                    Me.GridView6.DeleteRow(x)
                Next
            End If

            If Me.SLUNPID.EditValue <> "" Or Not IsDBNull(Me.SLUNPID.EditValue) Then
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as SJIDD,'" & Me.TBKode.EditValue & "' As SJID, NPIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,0 as Qty,0 as Dos, 0 as Psg,stsHarga,0 As HarSbDisc,DiscOB,0 as RpDiscOB,0 As RpDiscCust,0 As RpDiscL,OngkirSat,Ongkir,0 As HarAkhir,0 as Selisih,0 As SelisihExtra,0 As Urut, '' As NamaLain From T_NotPesDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Me.SLUNPID.EditValue & "' and SisaKirim >0 Order By D.ArtCode Asc", koneksi)

                cmsl.TableMappings.Add("Table", "T_SJDtl")
                cmsl.Fill(DsMaster, "T_SJDtl")
                DsMaster.Tables("T_SJDtl").Clear()
                cmsl.Fill(DsMaster, "T_SJDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_SJDtl"
            End If

            If Me.SLUPromo.EditValue <> "" Then
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as SJIDD,'" & Me.TBKode.EditValue & "' As SJID, NPIDD,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,0 as Qty,0 as Dos, 0 as Psg,stsHarga,0 As HarSbDisc,DiscOB,0 as RpDiscOB,0 As RpDiscCust,0 As RpDiscL,OngkirSat,Ongkir,0 As HarAkhir,0 as Selisih,0 As SelisihExtra,0 As Urut, '' As NamaLain From T_NotPesDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Me.SLUNPID.EditValue & "' and SisaKirim >0 Order By D.ArtCode Asc", koneksi)

                cmsl.TableMappings.Add("Table", "T_SJDtl")
                cmsl.Fill(DsMaster, "T_SJDtl")
                DsMaster.Tables("T_SJDtl").Clear()
                cmsl.Fill(DsMaster, "T_SJDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_SJDtl"
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("SJIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            rw2 = 0

            'If Gol = "Job Order" Then
            Dim frm As New FSJ_a(Me.SLUJnsCust.Text, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue, Manual, Me.SLUPromo.EditValue, Metode, Me.TBPaket.EditValue, "Semua")
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

            'Else
            '    'If Not IsDBNull(Me.SLUNPID.EditValue) Or Me.SLUNPID.EditValue = "" Then
            '    '    XtraMessageBox.Show("Dokumen Sudah Memakai Nota Pesanan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    '    Exit Sub
            '    'End If
            'End If
        End If
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        koneksi.Close()

        If e.Column Is GridView1.Columns("Qty") Then
            Dim Stok As Integer = 0
            Dim Tot As Integer = 0

            Dim i : For i = 0 To Me.GridView6.RowCount - 1
                If Me.GridView6.GetRowCellValue(i, "ArtCode") = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") Then
                    Tot += Me.GridView6.GetRowCellValue(i, "Qty")
                End If
            Next

            If Manual = False Then
                Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Tot > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
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

            Dim HargaSbDisc As Decimal
            HargaSbDisc = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round(HargaSbDisc * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") + Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir"), 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtra", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Selisih"), 2))

            Me.GridControl1_Leave(sender, e)

        End If
    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "SJIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SJID", Me.TBKode.EditValue)
                Me.GridView1.SetRowCellValue(e.RowHandle, "NPIDD", 0)
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
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                rw += 1

                DsMaster.Tables("T_SJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJDtl" & Gol).Columns("SJID"), DsMaster.Tables("T_SJDtl" & Gol).Columns("ArtCode")}
            End If
        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try

    End Sub
    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetFocusedDataRow.Item("SJIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            rw2 = 0

            Dim frm As New FSJ_a(Me.SLUJnsCust.Text, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue, Manual, Me.SLUPromo.EditValue, Metode, Me.TBPaket.EditValue, "Free")
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

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

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
            Dim frm As New FSearch("JualPromo", Me.SLUPromo.EditValue, Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Paket"), Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue)
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
            Me.GridView6.SetRowCellValue(e.RowHandle, "SJIDD", Me.GridView6.RowCount)
            Me.GridView6.SetRowCellValue(e.RowHandle, "SJID", Me.TBKode.EditValue)
            Me.GridView6.SetRowCellValue(e.RowHandle, "NPIDD", 0)
            Me.GridView6.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans2.Item("ArtCode" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "ArtName", dataTrans2.Item("ArtName" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "SatID", dataTrans2.Item("SatID" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "Isi", dataTrans2.Item("Isi" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "Qty", dataTrans2.Item("Qty" & rw2).ToString)
            rw2 += 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RBPPn_EditValueChanged(sender As Object, e As EventArgs) Handles RBPPn.EditValueChanged
        HitPPn()
    End Sub

    Private Sub TBPersenPPn_EditValueChanged(sender As Object, e As EventArgs) Handles TBPersenPPn.EditValueChanged
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
                'Me.TBTotDiscL.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscL").SummaryText, Decimal), 2)
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
            Dim frm As New FSJ_d(Me.GridView2.GetFocusedDataRow.Item("SJID"))
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
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "SJIDD")

                Me.GridView6.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub SLUPromo_Leave(sender As Object, e As EventArgs) Handles SLUPromo.Leave
        If Me.SLUPromo.Properties.ReadOnly = False Then
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "SJIDD")

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
    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class
