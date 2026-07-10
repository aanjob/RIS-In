Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors.DXErrorProvider

Public Class FNotPes
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1) As String
    Dim KodeTemp, KdLama, Area, CodeID, UrutArea, UrutCust, CurrID, Metode, JnsPerhit, Kelipatan, BeliMin, JnsPot, Gol As String
    Dim Manual, MnlInsUpd, stsPPn As Boolean
    Dim rw As Integer = 0
    Dim rw2 As Integer = 0
    Dim Ongkir, Pot, TotFree, TotPotCab, TotPotPst As Decimal
    Dim KdAktif As String = ""

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("NPN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("NPEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("NPDel"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("NPApr"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("NPCO"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTNotPes_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUGdCab.Properties.ReadOnly = True
        Me.SLUGdPst.Properties.ReadOnly = True
        Me.SLUSales.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.CBOJnsFt.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.CBOKat.Properties.ReadOnly = True
        Me.RBPPn.Properties.ReadOnly = True
        Me.TBPersenPPn.Properties.ReadOnly = True
        Me.SLUPromo.Properties.ReadOnly = True
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
        Me.BVBApprove.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTNotPes_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        'Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUGdCab.Properties.ReadOnly = False
        Me.SLUGdPst.Properties.ReadOnly = False
        Me.SLUSales.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.CBOJnsFt.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOKat.Properties.ReadOnly = False
        Me.RBPPn.Properties.ReadOnly = False
        Me.SLUPromo.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTNotPes_e.Selected = True

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
            DsMaster.Tables("M_UsCabLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabLUE")

        Me.SLUCab.Properties.DataSource = DsMaster.Tables("M_UsCabLUE")
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

        Me.TBTotSbDiscPst.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDiscPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscOBPst.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOBPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscCustPst.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCustPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

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
                If Me.SLUGdCab.EditValue <> "" Then
                    Dim cmSl As New SqlCommand("Select JnsPot,Pot,BeliMin From T_PromoDtl where PromoID= '" & Me.SLUPromo.EditValue & "' and Paket ='" & Me.TBPaket.EditValue & "' and JnsCust In ('" & Me.SLUJnsCust.Text & "','%') and (BeliMin <= " & Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue & ") AND (BeliMax >= '" & Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue & "') ")

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

                If Me.SLUGdPst.EditValue <> "" Then
                    Dim cmSl As New SqlCommand("Select JnsPot,Pot,BeliMin From T_PromoDtl where PromoID= '" & Me.SLUPromo.EditValue & "' and Paket ='" & Me.TBPaket.EditValue & "' and JnsCust In ('" & Me.SLUJnsCust.Text & "','%') and (BeliMin <= " & Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue & ") AND (BeliMax >= '" & Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue & "') ")

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
                If Me.SLUGdCab.EditValue <> "" Then
                    If Kelipatan = False Then
                        Me.TBTamDiscRp.EditValue = Pot
                    Else
                        Me.TBTamDiscRp.EditValue = Pot * ((Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue) \ BeliMin)
                    End If

                    Me.TBTamDiscRpPst.EditValue = 0
                End If

                If Me.SLUGdPst.EditValue <> "" Then
                    If Kelipatan = False Then
                        Me.TBTamDiscRpPst.EditValue = Pot
                    Else
                        Me.TBTamDiscRpPst.EditValue = Pot * ((Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue) \ BeliMin)
                    End If

                    Me.TBTamDiscRp.EditValue = 0
                End If

            ElseIf JnsPot = "Persen" Then
                If Me.SLUGdCab.EditValue <> "" Then
                    Me.TBTamDiscP.EditValue = Pot
                    Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100

                    Me.TBTamDiscPPst.EditValue = 0
                    Me.TBTamDiscPRpPst.EditValue = 0
                End If

                If Me.SLUGdPst.EditValue <> "" Then
                    Me.TBTamDiscPPst.EditValue = Pot
                    Me.TBTamDiscPRpPst.EditValue = (Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue - Me.TBTamDiscRpPst.EditValue) * Me.TBTamDiscPPst.EditValue / 100

                    Me.TBTamDiscP.EditValue = 0
                    Me.TBTamDiscPRp.EditValue = 0
                End If

            ElseIf JnsPot = "Barang" Then
                If Kelipatan = False Then
                    TotFree = Pot
                Else
                    If (CType(Me.GridView6.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView6.Columns("QtyPst").SummaryText, Integer)) > 0 Then
                        TotFree = Pot * (CType(Me.GridView1.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView1.Columns("QtyPst").SummaryText, Integer) \ BeliMin)
                    Else
                        TotFree = 0
                    End If
                End If

            ElseIf JnsPot = "Harga Lama" Then
                'Khusus Promo Harga Lama yang tidak campur zise dan warna murni per article

                Dim TotPotCab As Decimal = 0
                Dim TotPotPst As Decimal = 0

                If Kelipatan = False Then
                    Dim i : For i = 0 To Me.GridView1.RowCount - 1
                        If Me.SLUGdCab.EditValue <> "" Then
                            If Me.GridView1.GetRowCellValue(i, "QtyCab") >= BeliMin Then
                                Me.GridView1.SetRowCellValue(i, "SelisihExtra", Math.Round(BeliMin * Me.GridView1.GetRowCellValue(i, "Selisih"), 2))
                                TotPotCab += Math.Round(BeliMin * Me.GridView1.GetRowCellValue(i, "Selisih"), 2)
                            Else
                                Me.GridView1.SetRowCellValue(i, "SelisihExtra", 0)
                            End If

                            Me.GridView1.SetRowCellValue(i, "SelisihExtraPst", 0)
                        End If

                        If Me.SLUGdPst.EditValue <> "" Then
                            If Me.GridView1.GetRowCellValue(i, "QtyPst") >= BeliMin Then
                                Me.GridView1.SetRowCellValue(i, "SelisihExtraPst", Math.Round(BeliMin * Me.GridView1.GetRowCellValue(i, "SelisihPst"), 2))
                                TotPotPst += Math.Round(BeliMin * Me.GridView1.GetRowCellValue(i, "SelisihPst"), 2)
                            Else
                                Me.GridView1.SetRowCellValue(i, "SelisihExtraPst", 0)
                            End If

                            Me.GridView1.SetRowCellValue(i, "SelisihExtra", 0)
                        End If
                    Next

                    Me.TBTamDiscRp.EditValue = TotPotCab
                    Me.TBTamDiscRpPst.EditValue = TotPotPst

                Else

                    Dim i : For i = 0 To Me.GridView1.RowCount - 1
                        If Me.SLUGdCab.EditValue <> "" Then

                            If Me.GridView1.GetRowCellValue(i, "QtyCab") >= BeliMin Then
                                Me.GridView1.SetRowCellValue(i, "SelisihExtra", Math.Round((Me.GridView1.GetRowCellValue(i, "QtyCab") \ BeliMin) * Me.GridView1.GetRowCellValue(i, "Selisih"), 2))
                                TotPotCab += Math.Round((Me.GridView1.GetRowCellValue(i, "QtyCab") \ BeliMin) * Me.GridView1.GetRowCellValue(i, "Selisih"), 2)
                            Else
                                Me.GridView1.SetRowCellValue(i, "SelisihExtra", 0)
                            End If

                            Me.GridView1.SetRowCellValue(i, "SelisihExtraPst", 0)
                        End If

                        If Me.SLUGdPst.EditValue <> "" Then
                            If Me.GridView1.GetRowCellValue(i, "QtyPst") >= BeliMin Then
                                Me.GridView1.SetRowCellValue(i, "SelisihExtraPst", Math.Round((Me.GridView1.GetRowCellValue(i, "QtyPst") \ BeliMin) * Me.GridView1.GetRowCellValue(i, "SelisihPst"), 2))
                                TotPotPst += Math.Round((Me.GridView1.GetRowCellValue(i, "QtyPst") \ BeliMin) * Me.GridView1.GetRowCellValue(i, "SelisihPst"), 2)
                            Else
                                Me.GridView1.SetRowCellValue(i, "SelisihExtraPst", 0)
                            End If

                            Me.GridView1.SetRowCellValue(i, "SelisihExtra", 0)
                        End If
                    Next

                    Me.TBTamDiscRp.EditValue = TotPotCab
                    Me.TBTamDiscRpPst.EditValue = TotPotPst
                End If

            End If

            HitPPn()
        End If
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_NotPesDtl" & Gol).Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select NPIDD,NPID,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,Qty,QtyCab,DosCab,PsgCab,QtyPst,DosPst,PsgPst, stsHarga,HarSbDisc,DiscOB,RpDiscOB,RpDiscCust,OngkirSat,Ongkir,HarAkhir,Selisih,SelisihExtra,HarSbDiscPst,RpDiscOBPst,RpDiscCustPst, OngkirPst,HarAkhirPst,SelisihPst,SelisihExtraPst,BtlOrder,SisaKirim,stsKirim From T_NotPesDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_NotPesDtl" & Gol)
        cmsl.Fill(DsMaster, "T_NotPesDtl" & Gol)

        DsMaster.Tables("T_NotPesDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_NotPesDtl" & Gol).Columns("NPID"), DsMaster.Tables("T_NotPesDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_NotPesDtl" & Gol

        Try
            DsMaster.Tables("T_NotPesFree" & Gol).Clear()
        Catch ex As Exception


        End Try
        cmsl = New SqlDataAdapter("Select NPIDD,NPID,D.ArtCode,ArtName,D.SatID,D.Isi,QtyCab,DosCab,PsgCab,QtyPst,DosPst,PsgPst From T_NotPesFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_NotPesFree" & Gol)
        cmsl.Fill(DsMaster, "T_NotPesFree" & Gol)

        DsMaster.Tables("T_NotPesFree" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_NotPesFree" & Gol).Columns("NPID"), DsMaster.Tables("T_NotPesFree" & Gol).Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_NotPesFree" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select NPID,PeriodID,H.CodeID,JnsTrans,H.PromoID,H.Paket,P.Nama As Promo,H.CabID,Cb.Cabang,H.SalID,S.Nama As Sales,GdIDCab,G.Nama As GdCab,GdIDPst,(Select Nama From M_Gudang Where GdID=H.GdIDPst) As GdPst,H.CustID,C.Nama As Customer,C.Alamat, K.Nama As Kota,H.JnsCustID,H.Jenis,H.Harga,Ongkir,H.Tanggal,MtUang,CurrID,NilTukarRp,H.DiscCust,Kat,TipePPn,PersenPPn,TotSbDisc, TotOngkir,TotDPP,TotPPn, TotRpDiscCust,TotRpDiscOB,DiscP,RpDiscP,DiscRp,TotAkhir,TotAkhirRp,TotSbDiscPst,TotOngkirPst,TotDPPPst, TotPPnPst, TotRpDiscCustPst,TotRpDiscOBPst,DiscPPst,RpDiscPPst,DiscRpPst,TotAkhirPst,TotAkhirRpPst,TotQty,TotCab,TotPst,CLOwn As CL,(Select Isnull((Select MaxCL From M_JnsCust Where JnsCustID=H.JnsCustID),0)) As MaxCL,(Select Isnull((Select [dbo].[fcSisaCL](H.CustID,H.Gol)),0)) As SisaCL,H.Ket,H.KetApp, InputDari,H.Grup,H.Gol,H.stsPPn,H.stsKirim,stsApp,stsBatal,stsBatalsys, H.InsDate, H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_NotPes H Inner Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Sales S On H.SalID=S.SalID Left Outer Join M_Gudang G On H.GdIDCab=G.GdID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Left Outer Join T_Promo P On H.PromoID=P.PromoID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By stsApp,NPID desc,Tanggal desc,InsDate desc", koneksi)
        cmsl.TableMappings.Add("Table", "T_NotPes" & Gol)
        Try
            DsMaster.Tables("T_NotPes" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_NotPes" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_NotPes" & Gol
    End Sub

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue

        Me.TBTotAkhirPst.EditValue = Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue - Me.TBTamDiscRpPst.EditValue - Me.TBTamDiscPRpPst.EditValue

        If Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

            Me.TBTotDPPPst.EditValue = Me.TBTotAkhirPst.EditValue
            Me.TBTotPPnPst.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue - (Me.TBTotAkhir.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

            Me.TBTotDPPPst.EditValue = Math.Round(Me.TBTotAkhirPst.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100), 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPnPst.EditValue = Math.Round(Me.TBTotAkhirPst.EditValue - (Me.TBTotAkhirPst.EditValue / ((100 + Me.TBPersenPPn.EditValue) / 100)), 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTamDiscRp.EditValue - Me.TBTamDiscPRp.EditValue + Me.TBTotPPn.EditValue

            Me.TBTotDPPPst.EditValue = Me.TBTotAkhirPst.EditValue
            Me.TBTotPPnPst.EditValue = Math.Round(Me.TBTotDPPPst.EditValue * Me.TBPersenPPn.EditValue / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhirPst.EditValue = Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue - Me.TBTamDiscRpPst.EditValue - Me.TBTamDiscPRpPst.EditValue + Me.TBTotPPnPst.EditValue
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_NotPes")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KodeTemp
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

    Private Sub FNotPes_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Nota Pesanan"
    End Sub

    Private Sub FNotPes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        dataTrans2 = New Collection
        dataTrans2.Clear()

        If BSave.Enabled = True Then
            e.Cancel = True
            Me.Focus()
            Exit Sub
        End If

        CekSave = False
    End Sub

    Private Sub FNotPes_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FNotPes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTNotPes_e.Selected = True
    End Sub
    Private Sub BVTNotPes_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTNotPes_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Nota Pesanan"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("NPP"), Boolean)
    End Sub
    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Nota Pesanan"

        DsMaster.Clear()

        KodeTemp = MainModule.InisialAktif & Format(System.DateTime.Now, "yyMMddhhmmss")
        KdAktif = KodeTemp

        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TempStokBJCek")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
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

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
                If Manual = False Then
                    XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
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
        Me.SLUCab.EditValue = ""
        Me.SLUGdCab.EditValue = ""
        Me.SLUGdPst.EditValue = ""
        Me.SLUSales.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.CBOJnsFt.EditValue = "Non Spreading"
        Me.SLUJnsCust.EditValue = ""
        Me.TBHarga.EditValue = ""
        Me.TBDiscCust.EditValue = 0
        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOKat.EditValue = "Lokal"
        Me.RBPPn.EditValue = "Non PPn"
        Me.SLUPromo.EditValue = ""
        Me.TBPaket.EditValue = ""
        Metode = ""
        Me.MKet.EditValue = ""
        Me.TBTotSbDisc.EditValue = 0
        Me.TBTotDiscOB.EditValue = 0
        Me.TBTotDiscCust.EditValue = 0
        Me.TBTamDiscP.EditValue = 0
        Me.TBTamDiscPRp.EditValue = 0
        Me.TBTamDiscRp.EditValue = 0
        Me.TBTotAkhir.EditValue = 0
        Me.TBTotDPP.EditValue = 0
        Me.TBTotPPn.EditValue = 0
        Me.TBTotSbDiscPst.EditValue = 0
        Me.TBTotDiscOBPst.EditValue = 0
        Me.TBTotDiscCustPst.EditValue = 0
        Me.TBTamDiscPPst.EditValue = 0
        Me.TBTamDiscPRpPst.EditValue = 0
        Me.TBTamDiscRpPst.EditValue = 0
        Me.TBTotAkhirPst.EditValue = 0
        Me.TBTotDPPPst.EditValue = 0
        Me.TBTotPPnPst.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_NotPesDtl" & Gol).Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        CekCurr()

        If MainModule.BackDate = True Then
            Me.DTPTanggal.Properties.ReadOnly = False
        Else
            Me.DTPTanggal.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Nota Pesanan"

        If MainModule.SlOpBJGd(Me.BandedGridView1.GetFocusedDataRow.Item("GdIDCab"), Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlOpBJGd(Me.BandedGridView1.GetFocusedDataRow.Item("GdIDPst"), Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.BandedGridView1.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()


        If SlCek("T_NotPes", "stsBatal", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False And SlCek("T_NotPes", "stsBatalsys", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False And SlCek("T_NotPes", "stsApp", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False And SlCek("T_NotPes", "stsKirim", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False Then
            LUE()

            Dim cmsl As SqlDataAdapter

            Indicator = "200"
            Me.TBKode.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("NPID")
            KdAktif = Me.TBKode.EditValue
            Me.SLUGrup.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Grup")
            Me.SLUCab.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CabID")

            cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and Pusat='False' and G.Aktif='True' and CG.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GdCabLUE" & Gol)
            Try
                DsMaster.Tables("M_GdCabLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GdCabLUE" & Gol)

            Me.SLUGdCab.Properties.DataSource = DsMaster.Tables("M_GdCabLUE" & Gol)
            Me.SLUGdCab.Properties.DisplayMember = "Nama"
            Me.SLUGdCab.Properties.ValueMember = "GdID"

            cmsl = New SqlDataAdapter("Select GdID,Nama,Def From M_Gudang Where Pusat='True' and Grup='" & Me.SLUGrup.EditValue & "' and Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GdPstLUE" & Gol)
            Try
                DsMaster.Tables("M_GdPstLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GdPstLUE" & Gol)

            Me.SLUGdPst.Properties.DataSource = DsMaster.Tables("M_GdPstLUE" & Gol)
            Me.SLUGdPst.Properties.DisplayMember = "Nama"
            Me.SLUGdPst.Properties.ValueMember = "GdID"

            Me.SLUGdCab.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("GdIDCab")
            Me.SLUGdPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("GdIDPst")
            Me.DTPTanggal.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")

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

            Me.SLUSales.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("SalID")


            If Not IsDBNull(Me.SLUSales.EditValue) Then
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
            End If

            Me.SLUCust.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CustID")

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

            Me.CBOJnsFt.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("JnsTrans")
            Me.SLUJnsCust.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("JnsCustID")
            Me.TBHarga.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Harga")
            Ongkir = Me.BandedGridView1.GetFocusedDataRow.Item("Ongkir")
            Me.TBDiscCust.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("DiscCust")
            Me.TBPaket.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Paket")

            cmsl = New SqlDataAdapter("Select Distinct H.PromoID,Nama,Metod,Paket,DiperhitSaat From T_Promo H Inner Join T_PromoDtl D On H.PromoID=D.PromoID Where '" & Me.DTPTanggal.EditValue & "'>=TglAwal and '" & Me.DTPTanggal.EditValue & "' <=TglAkhir and JnsCust in ('" & Me.SLUJnsCust.Text & "','%') and Gol='" & Gol & "'", koneksi)
            cmsl.TableMappings.Add("Table", "T_PromoFt" & Gol)
            cmsl.Fill(DsMaster, "T_PromoFt" & Gol)
            DsMaster.Tables("T_PromoFt" & Gol).Clear()
            cmsl.Fill(DsMaster, "T_PromoFt" & Gol)

            Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
            Me.SLUPromo.Properties.DisplayMember = "Nama"
            Me.SLUPromo.Properties.ValueMember = "PromoID"

            Me.SLUPromo.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("PromoID")
            If Me.SLUPromo.EditValue <> "" Then
                Metode = DsMaster.Tables("T_PromoFt" & Gol).Select("PromoID = '" & Me.SLUPromo.EditValue & "' and Paket='" & Me.TBPaket.EditValue & "'")(0).Item("Metod")
            Else
                Metode = ""
            End If

            If Metode = "Gratis Barang" Then
                Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            Me.SLUMtUang.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("MtUang")
            CurrID = Me.BandedGridView1.GetFocusedDataRow.Item("CurrID")
            Me.TBNilTukarRp.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("NilTukarRp")
            Me.CBOKat.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Kat")
            stsPPn = Me.BandedGridView1.GetFocusedDataRow.Item("stsPpn")
            Me.RBPPn.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TipePPn")

            If Me.RBPPn.EditValue = "Include" Then
                Me.TBPersenPPn.Properties.ReadOnly = True
            ElseIf Me.RBPPn.EditValue = "Exclude" Then
                Me.TBPersenPPn.Properties.ReadOnly = False
            ElseIf Me.RBPPn.EditValue = "Non PPn" Then
                Me.TBPersenPPn.Properties.ReadOnly = True
            End If

            Me.TBPersenPPn.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("PersenPPn")
            Me.MKet.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Ket")
            Me.TBTotSbDisc.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotSbDisc")
            Me.TBTotDiscOB.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotRpDiscOB")
            Me.TBTotDiscCust.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotRpDiscCust")
            Me.TBTamDiscP.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("DiscP")
            Me.TBTamDiscPRp.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("RpDiscP")
            Me.TBTamDiscRp.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("DiscRp")
            Me.TBTotAkhir.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhir")
            Me.TBTotDPP.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotDPP")
            Me.TBTotPPn.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotPPn")
            Me.TBTotSbDiscPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotSbDiscPst")
            Me.TBTotDiscOBPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotRpDiscOBPst")
            Me.TBTotDiscCustPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotRpDiscCustPst")
            Me.TBTamDiscPPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("DiscPPst")
            Me.TBTamDiscPRpPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("RpDiscPPst")
            Me.TBTamDiscRpPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("DiscRpPst")
            Me.TBTotAkhirPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhirPst")
            Me.TBTotDPPPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotDPPPst")
            Me.TBTotPPnPst.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotPPnPst")

            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=46 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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
            ReDim arrPar(-1)
            ReDim arrPar2(-1)

            If IsDBNull(Me.BandedGridView1.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            Me.BCancel.Enabled = False
            CekSave = True
            Me.SLUCab.Properties.ReadOnly = True

            If MainModule.BackDate = True Then
                Me.DTPTanggal.Properties.ReadOnly = False
            Else
                Me.DTPTanggal.Properties.ReadOnly = True
            End If

            If Manual = True Then
                Me.TBTamDiscP.Properties.ReadOnly = False
                Me.TBTamDiscRp.Properties.ReadOnly = False

                Me.TBTamDiscPPst.Properties.ReadOnly = False
                Me.TBTamDiscRpPst.Properties.ReadOnly = False
            Else
                Me.TBTamDiscP.Properties.ReadOnly = True
                Me.TBTamDiscRp.Properties.ReadOnly = True

                Me.TBTamDiscPPst.Properties.ReadOnly = True
                Me.TBTamDiscRpPst.Properties.ReadOnly = True
            End If

        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove Atau Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        If MainModule.SlAppNP(MainModule.Posisi, Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Ada Dokumen Sebelumnya Yang Belum Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_NotPes", "stsApp", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = True Or SlCek("T_NotPes", "stsBatal", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = True Or SlCek("T_NotPes", "stsBatalsys", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Sudah Diapprove Atau Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            'MsgBox(MainModule.BackDate)
            If MainModule.BackDate = True Then
                Dim cmSP As New SqlCommand("SPAppNPBackDate")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdNP", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("NPID")
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("CabID")
                    .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("JnsTrans")
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("CustID")
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("JnsCustID")
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("MtUang")
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("Gol")
                    .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("GdIDCab")
                    .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("GdIDPst")
                    '.Parameters.Add("@TotAkhirCab", SqlDbType.Decimal).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhir")
                    '.Parameters.Add("@TotAkhirPst", SqlDbType.Decimal).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhirPst")
                    '.Parameters.Add("@TotQtyCab", SqlDbType.Int).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotCab")
                    '.Parameters.Add("@TotQtyPst", SqlDbType.Int).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotPst")
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
                        MsgBox(x)
                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        ElseIf x = 2 Then
                            XtraMessageBox.Show("Hanya Level Manager Yang Boleh Approve", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Dim frm As New FUpKet("Nota Pesanan", Me.BandedGridView1.GetFocusedDataRow.Item("NPID"), Me.BandedGridView1.GetFocusedDataRow.Item("KetApp"))
                            frm.ShowDialog()
                            FillDt()
                        Else
                            XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            Else
                Dim cmSP As New SqlCommand("SPAppNP")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdNP", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("NPID")
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("CabID")
                    .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("JnsTrans")
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("CustID")
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("JnsCustID")
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("MtUang")
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("Gol")
                    .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("GdIDCab")
                    .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("GdIDPst")
                    '.Parameters.Add("@TotAkhirCab", SqlDbType.Decimal).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhir")
                    '.Parameters.Add("@TotAkhirPst", SqlDbType.Decimal).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhirPst")
                    '.Parameters.Add("@TotQtyCab", SqlDbType.Int).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotCab")
                    '.Parameters.Add("@TotQtyPst", SqlDbType.Int).Value = Me.BandedGridView1.GetFocusedDataRow.Item("TotPst")
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
                        'MsgBox(x)
                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        ElseIf x = 2 Then
                            XtraMessageBox.Show("Hanya Level Manager Yang Boleh Approve", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Dim frm As New FUpKet("Nota Pesanan", Me.BandedGridView1.GetFocusedDataRow.Item("NPID"), Me.BandedGridView1.GetFocusedDataRow.Item("KetApp"))
                            frm.ShowDialog()
                            FillDt()
                        Else
                            MsgBox(x)
                            XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If


        End If

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Nota Pesanan"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.BandedGridView1.GetFocusedDataRow.Item("GdIDCab"), Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlOpBJGd(Me.BandedGridView1.GetFocusedDataRow.Item("GdIDPst"), Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.BandedGridView1.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlKirimNP(Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Pernah Dibuatkan Faktur", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_NotPes", "stsBatal", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False And SlCek("T_NotPes", "stsBatalsys", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False And SlCek("T_NotPes", "stsApp", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False And SlCek("T_NotPes", "stsKirim", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = False Then

            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.BandedGridView1.GetFocusedDataRow.Item("NPID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_NotPes")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("NPID")
                    .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = ""
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
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove Atau Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Nota Pesanan"

        If SlCek("T_NotPes", "stsKirim", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_NotPes", "stsBatalsys", "NPID", Me.BandedGridView1.GetFocusedDataRow.Item("NPID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Dibatalkan System", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Membatalkan Sisa : " & Me.BandedGridView1.GetFocusedDataRow.Item("NPID") & " Yang Belum Dikirim ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPBtlNP")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("NPID")
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
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
            End With
        End If
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        koneksi.Close()

        Dim Kode As String
        Kode = Me.BandedGridView1.GetFocusedDataRow.Item("NPID")

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.BandedGridView1.RowCount - 1
            If Me.BandedGridView1.GetRowCellValue(a, "NPID") = Kode Then
                fc = a
                Exit For
            End If
        Next

        Me.BandedGridView1.FocusedRowHandle = fc

        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("NPID"), "Kode")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.BandedGridView1.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("TotSbDisc"), "TotSbDisc")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("TotRpDiscCust") + Me.BandedGridView1.GetFocusedDataRow.Item("TotRpDiscOB") + +Me.BandedGridView1.GetFocusedDataRow.Item("RpDiscP") + Me.BandedGridView1.GetFocusedDataRow.Item("DiscRp"), "TotDisc")
        bind.Add(Me.BandedGridView1.GetFocusedDataRow.Item("TotAkhir"), "TotAkhir")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRNotPes
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

        Me.TBTotSbDiscPst.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDiscPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscOBPst.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOBPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
        Me.TBTotDiscCustPst.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCustPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

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

        If Math.Round(CType(Me.GridView1.Columns("QtyCab").SummaryText, Decimal), 2) = 0 And Math.Round(CType(Me.GridView1.Columns("QtyPst").SummaryText, Decimal), 2) = 0 Then
            XtraMessageBox.Show("Stok Barang Di Semua Gudang yang Dipilih Tidak Ada", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Metode = "Gratis Barang" Then
            If CType(Me.GridView6.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView6.Columns("QtyPst").SummaryText, Integer) <> TotFree Then
                XtraMessageBox.Show("Jumlah Barang Gratis Tidak Sesuai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        'MsgBox(CodeID)
        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_NotPes")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@PromoID", SqlDbType.VarChar).Value = Me.SLUPromo.EditValue
                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.TBPaket.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                    .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
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
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotSbDiscPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDiscPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotOngkirPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("OngkirPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotDPPPst", SqlDbType.Decimal).Value = Me.TBTotDPPPst.EditValue
                    .Parameters.Add("@TotPPnPst", SqlDbType.Decimal).Value = Me.TBTotPPnPst.EditValue
                    .Parameters.Add("@TotRpDiscCustPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscCustPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscOBPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscOBPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscPPst", SqlDbType.Decimal).Value = Me.TBTamDiscPPst.EditValue
                    .Parameters.Add("@RpDiscPPst", SqlDbType.Decimal).Value = Me.TBTamDiscPRpPst.EditValue
                    .Parameters.Add("@DiscRpPst", SqlDbType.Decimal).Value = Me.TBTamDiscRpPst.EditValue
                    .Parameters.Add("@TotAkhirPst", SqlDbType.Decimal).Value = Me.TBTotAkhirPst.EditValue
                    .Parameters.Add("@TotAkhirRpPst", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhirPst.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotCab", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("QtyCab").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDosCab", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("DosCab").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsgCab", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("PsgCab").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPst", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("QtyPst").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDosPst", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("DosPst").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsgPst", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("PsgPst").SummaryText, Decimal), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@InputDari", SqlDbType.VarChar).Value = "Desktop"
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_NotPesDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                                    .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                                    .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NPIDD")
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KdAktif
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@QtyCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "QtyCab")
                                    .Parameters.Add("@DosCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DosCab")
                                    .Parameters.Add("@PsgCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PsgCab")

                                    .Parameters.Add("@QtyPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "QtyPst")
                                    .Parameters.Add("@DosPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DosPst")
                                    .Parameters.Add("@PsgPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PsgPst")
                                    .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                    .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                    .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                    .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                    .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                    .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                    .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                    .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")

                                    .Parameters.Add("@HarSbDiscPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDiscPst")
                                    .Parameters.Add("@RpDiscCustPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCustPst")
                                    .Parameters.Add("@RpDiscOBPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOBPst")
                                    .Parameters.Add("@OngkirPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirPst")
                                    .Parameters.Add("@HarAkhirPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhirPst")
                                    .Parameters.Add("@SelisihPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihPst")
                                    .Parameters.Add("@SelisihExtraPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtraPst")
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

                        If Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Metod") = "Gratis Barang" Then
                            Dim z : For z = 0 To GridView6.RowCount - 1
                                If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_NotPesFree")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                                        .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                                        .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NPIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KdAktif
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Isi")
                                        .Parameters.Add("@QtyCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "QtyCab")
                                        .Parameters.Add("@DosCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "DosCab")
                                        .Parameters.Add("@PsgCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "PsgCab")
                                        .Parameters.Add("@QtyPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "QtyPst")
                                        .Parameters.Add("@DosPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "DosPst")
                                        .Parameters.Add("@PsgPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "PsgPst")
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
                            'MsgBox("1" & CodeID)
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        'MsgBox("2" & CodeID)
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_NotPes")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@JnsTrans", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@PromoID", SqlDbType.VarChar).Value = Me.SLUPromo.EditValue
                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.TBPaket.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                    .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
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
                    .Parameters.Add("@DiscP", SqlDbType.Decimal).Value = Me.TBTamDiscP.EditValue
                    .Parameters.Add("@RpDiscP", SqlDbType.Decimal).Value = Me.TBTamDiscPRp.EditValue
                    .Parameters.Add("@DiscRp", SqlDbType.Decimal).Value = Me.TBTamDiscRp.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@TotAkhirRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhir.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotSbDiscPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarSbDiscPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotOngkirPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("OngkirPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotDPPPst", SqlDbType.Decimal).Value = Me.TBTotDPPPst.EditValue
                    .Parameters.Add("@TotPPnPst", SqlDbType.Decimal).Value = Me.TBTotPPnPst.EditValue
                    .Parameters.Add("@TotRpDiscCustPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscCustPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotRpDiscOBPst", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("RpDiscOBPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@DiscPPst", SqlDbType.Decimal).Value = Me.TBTamDiscPPst.EditValue
                    .Parameters.Add("@RpDiscPPst", SqlDbType.Decimal).Value = Me.TBTamDiscPRpPst.EditValue
                    .Parameters.Add("@DiscRpPst", SqlDbType.Decimal).Value = Me.TBTamDiscRpPst.EditValue
                    .Parameters.Add("@TotAkhirPst", SqlDbType.Decimal).Value = Me.TBTotAkhirPst.EditValue
                    .Parameters.Add("@TotAkhirRpPst", SqlDbType.Decimal).Value = Math.Round(Me.TBTotAkhirPst.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotCab", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("QtyCab").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDosCab", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("DosCab").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsgCab", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("PsgCab").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPst", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("QtyPst").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDosPst", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("DosPst").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsgPst", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("PsgPst").SummaryText, Decimal), 2)

                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@InputDari", SqlDbType.VarChar).Value = "Desktop"
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
                            Dim cmSPDel As New SqlCommand("SPDelT_NotPesDtl")
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
                            Dim cmSPDel As New SqlCommand("SPDelT_NotPesFree")
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
                            If Me.GridView1.GetRowCellValue(i, "NPIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_NotPesDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                                        .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                                        .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "NPIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KdAktif
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "QtyCab")
                                        .Parameters.Add("@DosCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DosCab")
                                        .Parameters.Add("@PsgCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PsgCab")
                                        .Parameters.Add("@QtyPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "QtyPst")
                                        .Parameters.Add("@DosPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DosPst")
                                        .Parameters.Add("@PsgPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PsgPst")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
                                        .Parameters.Add("@HarSbDiscPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDiscPst")
                                        .Parameters.Add("@RpDiscCustPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCustPst")
                                        .Parameters.Add("@RpDiscOBPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOBPst")
                                        .Parameters.Add("@OngkirPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirPst")
                                        .Parameters.Add("@HarAkhirPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhirPst")
                                        .Parameters.Add("@SelisihPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihPst")
                                        .Parameters.Add("@SelisihExtraPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtraPst")
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
                                        Me.GridView1.SetRowCellValue(i, "NPIDD", Me.GridView1.GetRowCellValue(i, "NPIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_NotPesDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "NPIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                                        .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                                        .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@HarDPJ", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarDPJ")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "QtyCab")
                                        .Parameters.Add("@DosCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DosCab")
                                        .Parameters.Add("@PsgCab", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PsgCab")
                                        .Parameters.Add("@QtyPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "QtyPst")
                                        .Parameters.Add("@DosPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DosPst")
                                        .Parameters.Add("@PsgPst", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PsgPst")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@HarSbDisc", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDisc")
                                        .Parameters.Add("@RpDiscCust", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCust")
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "DiscOB")
                                        .Parameters.Add("@RpDiscOB", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOB")
                                        .Parameters.Add("@OngkirSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirSat")
                                        .Parameters.Add("@Ongkir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ongkir")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Selisih", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Selisih")
                                        .Parameters.Add("@SelisihExtra", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtra")
                                        .Parameters.Add("@HarSbDiscPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSbDiscPst")
                                        .Parameters.Add("@RpDiscCustPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscCustPst")
                                        .Parameters.Add("@RpDiscOBPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RpDiscOBPst")
                                        .Parameters.Add("@OngkirPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "OngkirPst")
                                        .Parameters.Add("@HarAkhirPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhirPst")
                                        .Parameters.Add("@SelisihPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihPst")
                                        .Parameters.Add("@SelisihExtraPst", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SelisihExtraPst")
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
                                If Me.GridView6.GetRowCellValue(z, "NPIDD") > 0 Then
                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_NotPesFree")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                                            .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                                            .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NPIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KdAktif
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SatID")
                                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Isi")
                                            .Parameters.Add("@QtyCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "QtyCab")
                                            .Parameters.Add("@DosCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "DosCab")
                                            .Parameters.Add("@PsgCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "PsgCab")
                                            .Parameters.Add("@QtyPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "QtyPst")
                                            .Parameters.Add("@DosPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "DosPst")
                                            .Parameters.Add("@PsgPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "PsgPst")
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
                                            Me.GridView1.SetRowCellValue(i, "NPIDD", Me.GridView1.GetRowCellValue(i, "NPIDD") * -1)
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    If Not IsDBNull(Me.GridView6.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_NotPesFree")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "NPIDD")
                                            .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                                            .Parameters.Add("@GdIDCab", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                                            .Parameters.Add("@GdIDPst", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(z, "SatID")
                                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "Isi")
                                            .Parameters.Add("@QtyCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "QtyCab")
                                            .Parameters.Add("@DosCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "DosCab")
                                            .Parameters.Add("@PsgCab", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "PsgCab")
                                            .Parameters.Add("@QtyPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "QtyPst")
                                            .Parameters.Add("@DosPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "DosPst")
                                            .Parameters.Add("@PsgPst", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(z, "PsgPst")
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

        Dim cmd2 As New SqlCommand("SPAftSNotPes")
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

    Private Sub SLUCabang_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim jml As Integer

        If Not IsDBNull(Me.SLUCab.EditValue) And Me.SLUCab.EditValue <> "" And Me.SLUCab.EditValue <> "RAP" And Me.SLUCab.EditValue <> "SKB" And Me.SLUCab.EditValue <> "MDN" And Me.SLUCab.EditValue <> "RJH" Then
            Dim command As New SqlCommand("Select [dbo].fcCekTargetCab(" & MainModule.periodeTahun & "," & MainModule.periodeBulan & ",'" & Me.SLUCab.EditValue & "')", koneksi)

            koneksi.Close()

            With koneksi
                .Open()
                jml = command.ExecuteScalar()
                .Close()
            End With

            If jml > 0 Then
                XtraMessageBox.Show("Target Belum Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Dispose()
            End If
        End If

        If Me.SLUCab.Properties.ReadOnly = False Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=46 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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
                'Me.TBTamDiscP.Properties.ReadOnly = False
                'Me.TBTamDiscRp.Properties.ReadOnly = False
            Else
                Me.TBKode.Properties.ReadOnly = True
                Me.TBKode.EditValue = "--"

                'Me.TBTamDiscP.Properties.ReadOnly = True
                'Me.TBTamDiscRp.Properties.ReadOnly = True
            End If

            Dim cmsl As SqlDataAdapter

            Try
                cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and Pusat='False' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GdCabLUE" & Gol)
                Try
                    DsMaster.Tables("M_GdCabLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GdCabLUE" & Gol)
                DsMaster.Tables("M_GdCabLUE" & Gol).Rows.Add("", "", False)

                Me.SLUGdCab.Properties.DataSource = DsMaster.Tables("M_GdCabLUE" & Gol)
                Me.SLUGdCab.Properties.DisplayMember = "Nama"
                Me.SLUGdCab.Properties.ValueMember = "GdID"


                cmsl = New SqlDataAdapter("Select Distinct G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where Pusat='True' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GdPstLUE" & Gol)
                Try
                    DsMaster.Tables("M_GdPstLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GdPstLUE" & Gol)
                DsMaster.Tables("M_GdPstLUE" & Gol).Rows.Add("", "", False)

                Me.SLUGdPst.Properties.DataSource = DsMaster.Tables("M_GdPstLUE" & Gol)
                Me.SLUGdPst.Properties.DisplayMember = "Nama"
                Me.SLUGdPst.Properties.ValueMember = "GdID"

                If Me.SLUPromo.EditValue = "" Then
                    Try
                        Me.SLUGdCab.EditValue = DsMaster.Tables("M_GdCabLUE" & Gol).Select("Def='True'")(0).Item("GdID")

                    Catch ex As Exception

                    End Try
                    Me.SLUGdPst.EditValue = DsMaster.Tables("M_GdPstLUE" & Gol).Select("Def='True'")(0).Item("GdID")
                Else
                    Me.SLUGdCab.EditValue = ""
                    Me.SLUGdPst.EditValue = ""
                End If

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

                If Not IsDBNull(Me.SLUSales.EditValue) Then
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
                End If
            Catch ex As Exception

            End Try

            Me.SLUCust.EditValue = ""

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "NPIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "NPIDD")

                Me.GridView6.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.Properties.ReadOnly = False Then
            Try
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
                                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(a, "NPIDD")

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
                    DsMaster.Tables("T_PromoFt" & Gol).Rows.Add("", "", "", "")

                    Me.SLUPromo.Properties.DataSource = DsMaster.Tables("T_PromoFt" & Gol)
                    Me.SLUPromo.Properties.DisplayMember = "Nama"
                    Me.SLUPromo.Properties.ValueMember = "PromoID"
                End If
            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "NPIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUGdCab_Leave(sender As Object, e As EventArgs) Handles SLUGdCab.Leave
        If Me.SLUGdCab.Properties.ReadOnly = False Then
            If Me.SLUPromo.EditValue <> "" Then
                If Me.SLUGdCab.EditValue = "" Then
                    Me.SLUGdPst.Properties.ReadOnly = False
                    Me.SLUGdCab.Properties.ReadOnly = True
                Else
                    Me.SLUGdPst.EditValue = ""
                    Me.SLUGdPst.Properties.ReadOnly = True
                    Me.SLUGdCab.Properties.ReadOnly = False
                End If
            End If

            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "NPIDD")

                    Me.GridView1.DeleteRow(i)
                Next


                Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "NPIDD")

                    Me.GridView6.DeleteRow(x)
                Next
            End If
        End If
    End Sub

    Private Sub SLUGdPst_Leave(sender As Object, e As EventArgs) Handles SLUGdPst.Leave
        If Me.SLUGdPst.Properties.ReadOnly = False Then
            If Me.SLUPromo.EditValue <> "" Then
                If Me.SLUGdPst.EditValue = "" Then
                    Me.SLUGdPst.Properties.ReadOnly = True
                    Me.SLUGdCab.Properties.ReadOnly = False
                Else
                    Me.SLUGdCab.EditValue = ""
                    Me.SLUGdPst.Properties.ReadOnly = False
                    Me.SLUGdCab.Properties.ReadOnly = True
                End If
            End If

            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "NPIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "NPIDD")

                    Me.GridView6.DeleteRow(x)
                Next
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("NPIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            rw2 = 0

            Dim frm As New FNotPes_a(KdAktif, Me.SLUJnsCust.Text, Me.SLUGdCab.EditValue, Me.SLUGdPst.EditValue, Gol, Me.DTPTanggal.EditValue, Manual, Me.SLUPromo.EditValue, Metode, Me.TBPaket.EditValue, Me.SLUJnsCust.Text, Me.SLUCust.EditValue)
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
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        koneksi.Close()
        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        If e.Column Is GridView1.Columns("Qty") Then
            Dim Stok As Integer = 0
            'Dim Tot As Integer = 0

            'Dim i : For i = 0 To Me.GridView6.RowCount - 1
            '    If Me.GridView6.GetRowCellValue(i, "ArtCode") = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") Then
            '        Tot += Me.GridView6.GetRowCellValue(i, "Qty")
            '    End If
            'Next

            Dim StokCab, StokPst As Integer
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdCab.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & KdAktif & "')", koneksi)

            With koneksi
                .Open()
                StokCab = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > StokCab Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", StokCab)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", e.Value)
            End If

            Dim command2 As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdPst.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & KdAktif & "')", koneksi)

            With koneksi
                .Open()
                StokPst = command2.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") > StokPst Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", StokPst)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", e.Value - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "NPIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab")
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

            Dim cmSPDtl2 As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl2.CommandType = CommandType.StoredProcedure

            With cmSPDtl2
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "NPIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst")
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

            Me.GridView1.SetRowCellValue(e.RowHandle, "Ongkir", Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirSat") * Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgCab", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosCab", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosCab", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

            Dim HargaSbDisc As Decimal
            HargaSbDisc = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round(HargaSbDisc * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") + Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir"), 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtra", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "Selisih"), 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "OngkirPst", Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirSat") * Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPst", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosPst", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosPst", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst"))
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDiscPst", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

            Dim HargaSbDiscPst As Decimal
            HargaSbDiscPst = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOBPst", Math.Round(HargaSbDiscPst * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCustPst", Math.Round((HargaSbDiscPst - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOBPst")) * Me.TBDiscCust.EditValue / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhirPst", Math.Round(HargaSbDiscPst - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCustPst") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOBPst") + Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirPst"), 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtraPst", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "SelisihPst"), 2))

            Me.GridControl1_Leave(sender, e)

        ElseIf e.Column Is GridView1.Columns("QtyCab") Then

            Dim StokCab, StokPst As Integer
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdCab.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & KdAktif & "')", koneksi)

            With koneksi
                .Open()
                StokCab = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") > Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
            Else
                If Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") > StokCab Then
                    Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", StokCab)
                Else
                    Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", e.Value)
                End If
            End If

            Dim command2 As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdPst.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & KdAktif & "')", koneksi)

            With koneksi
                .Open()
                StokPst = command2.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") > StokPst Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", StokPst)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "NPIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab")
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

            Dim cmSPDtl2 As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl2.CommandType = CommandType.StoredProcedure

            With cmSPDtl2
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "NPIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst")
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

            Me.GridView1.SetRowCellValue(e.RowHandle, "Ongkir", Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirSat") * Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgCab", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosCab", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosCab", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDisc", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

            Dim HargaSbDisc As Decimal
            HargaSbDisc = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOB", Math.Round(HargaSbDisc * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCust", Math.Round((HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB")) * Me.TBDiscCust.EditValue / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(HargaSbDisc - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCust") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOB") + Me.GridView1.GetRowCellValue(e.RowHandle, "Ongkir"), 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtra", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView1.GetRowCellValue(e.RowHandle, "Selisih"), 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "OngkirPst", Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirSat") * Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPst", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosPst", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "DosPst", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst"))
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSbDiscPst", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

            Dim HargaSbDiscPst As Decimal
            HargaSbDiscPst = Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarDPJ"), 2)

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscOBPst", Math.Round(HargaSbDiscPst * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscOB") / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscCustPst", Math.Round((HargaSbDiscPst - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOBPst")) * Me.TBDiscCust.EditValue / 100, 2))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhirPst", Math.Round(HargaSbDiscPst - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscCustPst") - Me.GridView1.GetRowCellValue(e.RowHandle, "RpDiscOBPst") + Me.GridView1.GetRowCellValue(e.RowHandle, "OngkirPst"), 2))

            'Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtraPst", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView1.GetRowCellValue(e.RowHandle, "SelisihPst"), 2))

            Me.GridControl1_Leave(sender, e)
        End If

        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) <> 0 Then
                RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(e.RowHandle, "NPIDD", dataTrans.Item("Row" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "NPID", Me.TBKode.EditValue)
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

                Me.GridView1.SetRowCellValue(e.RowHandle, "Selisih", CDec(dataTrans.Item("Selisih" & rw).ToString))
                Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihPst", CDec(dataTrans.Item("SelisihPst" & rw).ToString))

                Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtra", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SelisihExtraPst", 0)

                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", dataTrans.Item("QtyPst" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", dataTrans.Item("QtyCab" & rw).ToString)

                AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)

                rw += 1

                DsMaster.Tables("T_NotPesDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_NotPesDtl" & Gol).Columns("NPID"), DsMaster.Tables("T_NotPesDtl" & Gol).Columns("ArtCode")}
            End If

        Catch ex As Exception
            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try

    End Sub
    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetFocusedDataRow.Item("NPIDD")
        End If
    End Sub

    Private Sub GridView6_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView6.CellValueChanged
        If e.Column Is GridView6.Columns("QtyCab") Then
            Dim Stok As Integer
            Dim Tot As Integer = 0

            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(i, "ArtCode") = Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") Then
                    Tot += Me.GridView1.GetRowCellValue(i, "QtyCab")
                End If
            Next

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdCab.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView6.GetRowCellValue(e.RowHandle, "QtyCab") + Tot > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView6.SetRowCellValue(e.RowHandle, "QtyCab", Stok)
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdCab.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(e.RowHandle, "NPIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(e.RowHandle, "QtyCab")
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

            Me.GridView6.SetRowCellValue(e.RowHandle, "PsgCab", Me.GridView6.GetRowCellValue(e.RowHandle, "QtyCab") * Me.GridView6.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView6.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView6.SetRowCellValue(e.RowHandle, "DosCab", 0)
            Else
                Me.GridView6.SetRowCellValue(e.RowHandle, "DosCab", Me.GridView6.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Me.BSave.Focus()
            Me.GridView6.Focus()

        ElseIf e.Column Is GridView6.Columns("QtyPst") Then
            Dim Stok As Integer
            Dim Tot As Integer = 0

            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(i, "ArtCode") = Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") Then
                    Tot += Me.GridView1.GetRowCellValue(i, "QtyPst")
                End If
            Next

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdPst.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView6.GetRowCellValue(e.RowHandle, "QtyPst") + Tot > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView6.SetRowCellValue(e.RowHandle, "QtyPst", Stok)
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 46
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdPst.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(e.RowHandle, "NPIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView6.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView6.GetRowCellValue(e.RowHandle, "QtyPst")
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


            Me.GridView6.SetRowCellValue(e.RowHandle, "PsgPst", Me.GridView6.GetRowCellValue(e.RowHandle, "QtyPst") * Me.GridView6.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView6.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView6.SetRowCellValue(e.RowHandle, "DosPst", 0)
            Else
                Me.GridView6.SetRowCellValue(e.RowHandle, "DosPst", Me.GridView6.GetRowCellValue(e.RowHandle, "QtyPst"))
            End If

            Me.BSave.Focus()
            Me.GridView6.Focus()
        End If
    End Sub
    Private Sub GridView6_DoubleClick(sender As Object, e As EventArgs) Handles GridView6.DoubleClick
        If Me.GridView6.OptionsBehavior.Editable = True Then
            'Dim frm As New FSearch("JualPromo", Me.SLUPromo.EditValue, Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Paket"), Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue)
            'frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("ArtCode").ToString) Then
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "QtyCab", 0)
                    Me.GridView6.SetRowCellValue(Me.GridView6.FocusedRowHandle, "QtyPst", 0)

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GridView6_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView6.InitNewRow
        Try
            Me.GridView6.SetRowCellValue(e.RowHandle, "NPIDD", dataTrans2.Item("Row" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "NPID", Me.TBKode.EditValue)
            Me.GridView6.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans2.Item("ArtCode" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "ArtName", dataTrans2.Item("ArtName" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "SatID", dataTrans2.Item("SatID" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "Isi", dataTrans2.Item("Isi" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "QtyCab", dataTrans2.Item("QtyCab" & rw2).ToString)
            Me.GridView6.SetRowCellValue(e.RowHandle, "QtyPst", dataTrans2.Item("QtyPst" & rw2).ToString)

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

    Private Sub SLUSales_Leave(sender As Object, e As EventArgs) Handles SLUSales.Leave
       
    End Sub

    Private Sub GridControl1_Leave(sender As Object, e As EventArgs) Handles GridControl1.Leave
        Try
            If Me.GridView1.OptionsBehavior.Editable = True Then
                Me.TBTotSbDisc.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDisc").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscOB.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOB").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscCust.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCust").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

                Me.TBTotSbDiscPst.EditValue = Math.Round(CType(Me.GridView1.Columns("HarSbDiscPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscOBPst.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscOBPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                Me.TBTotDiscCustPst.EditValue = Math.Round(CType(Me.GridView1.Columns("RpDiscCustPst").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

                HitPPn()
                CekPromo()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TBTamDiscRp_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscRp.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100

        Me.TBTamDiscPRpPst.EditValue = (Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue - Me.TBTamDiscRpPst.EditValue) * Me.TBTamDiscPPst.EditValue / 100

        HitPPn()
    End Sub

    Private Sub TBTamDiscP_EditValueChanged(sender As Object, e As EventArgs) Handles TBTamDiscP.EditValueChanged
        Me.TBTamDiscPRp.EditValue = (Me.TBTotSbDisc.EditValue - Me.TBTotDiscOB.EditValue - Me.TBTotDiscCust.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscP.EditValue / 100

        Me.TBTamDiscPRpPst.EditValue = (Me.TBTotSbDiscPst.EditValue - Me.TBTotDiscOBPst.EditValue - Me.TBTotDiscCustPst.EditValue - Me.TBTamDiscRp.EditValue) * Me.TBTamDiscPPst.EditValue / 100

        HitPPn()
    End Sub

    Private Sub BandedGridView1_DoubleClick(sender As Object, e As EventArgs) Handles BandedGridView1.DoubleClick
        Try
            Dim frm As New FNotPes_d(Me.BandedGridView1.GetFocusedDataRow.Item("NPID"))
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
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "NPIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "NPIDD")

                Me.GridView6.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub SLUPromo_Leave(sender As Object, e As EventArgs) Handles SLUPromo.Leave
        If Manual = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "NPIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView6.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView6.GetRowCellValue(x, "NPIDD")

                Me.GridView6.DeleteRow(x)
            Next
        End If

        If Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Metod") = "Gratis Barang" Then
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Me.SLUPromo.EditValue = "" Then
            'Me.TBTamDiscP.Properties.ReadOnly = False
            'Me.TBTamDiscRp.Properties.ReadOnly = False

            'Me.TBTamDiscPPst.Properties.ReadOnly = False
            'Me.TBTamDiscRpPst.Properties.ReadOnly = False
            Try
                Me.SLUGdCab.EditValue = DsMaster.Tables("M_GdCabLUE" & Gol).Select("Def='True'")(0).Item("GdID")
            Catch ex As Exception

            End Try
            Me.SLUGdPst.EditValue = DsMaster.Tables("M_GdPstLUE" & Gol).Select("Def='True'")(0).Item("GdID")

            Me.TBPaket.EditValue = ""
            Metode = ""
        Else
            Me.TBPaket.EditValue = Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Paket")
            Metode = Me.GridView5.GetRowCellValue(Me.GridView5.FocusedRowHandle, "Metod")

            Me.TBTamDiscP.Properties.ReadOnly = True
            Me.TBTamDiscRp.Properties.ReadOnly = True

            Me.TBTamDiscP.EditValue = 0
            Me.TBTamDiscRp.EditValue = 0

            Me.TBTamDiscPPst.Properties.ReadOnly = True
            Me.TBTamDiscRpPst.Properties.ReadOnly = True

            Me.TBTamDiscPPst.EditValue = 0
            Me.TBTamDiscRpPst.EditValue = 0

            Me.SLUGdCab.EditValue = ""
            Me.SLUGdPst.EditValue = ""
        End If
    End Sub

   
    Private Sub BandedGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles BandedGridView1.KeyDown
        If e.KeyCode = Keys.F10 Then
            Dim frm As New FNotPesSP(Me.BandedGridView1.GetFocusedDataRow.Item("CustID"))
            frm.ShowDialog()
            frm.Close()
        End If
    End Sub

    Private Sub BandedGridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles BandedGridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.BandedGridView1.GetRowCellValue(e.RowHandle, "TotAkhir") + Me.BandedGridView1.GetRowCellValue(e.RowHandle, "TotAkhirPst") > Me.BandedGridView1.GetRowCellValue(e.RowHandle, "SisaCL") + Me.BandedGridView1.GetRowCellValue(e.RowHandle, "MaxCL") And Me.BandedGridView1.GetRowCellValue(e.RowHandle, "stsApp") = False And Me.BandedGridView1.GetRowCellValue(e.RowHandle, "stsBatal") = False And Me.BandedGridView1.GetRowCellValue(e.RowHandle, "stsBatalsys") = False Then
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class

