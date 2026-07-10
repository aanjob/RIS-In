Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTrmBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, Gol, CurrID, MtUang, KdLama As String
    Dim Manual, MnlInsUpd, stsPPn, stsPPnLama As Boolean
    Dim rw As Integer = 0
    Dim NilTukar As Decimal

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=13 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrmBJN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTTrmBJ_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.CBOJns.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUPOID.Properties.ReadOnly = True
        Me.TBSJ.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
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
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTrmBJ_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.SLUGrup.Properties.ReadOnly = False
        Me.CBOJns.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUPOID.Properties.ReadOnly = False
        Me.TBSJ.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTrmBJ_e.Selected = True

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

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE")
        cmsl.Fill(DsMaster, "M_SuppLUE")
        DsMaster.Tables("M_SuppLUE").Clear()
        cmsl.Fill(DsMaster, "M_SuppLUE")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"

    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrmIDD,TrmID,DocIDD,BSTBID,POID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg,HarSat,HarAkhir From T_TrmBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where TrmID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrmBJDtl" & Gol)
        Try
            DsMaster.Tables("T_TrmBJDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrmBJDtl" & Gol)

        DsMaster.Tables("T_TrmBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("DocIDD"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("BSTBID"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("POID"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrmBJDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrmID,PeriodID,CodeID,Tanggal,JnsDoc,H.SuppID,S.Nama As Supp,POID,SJ,H.GdID,G.Nama as Gudang,CurrID, MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotDos,TotPsg,TotDPP,TotPPn,TotAkhir,H.Ket,H.Grup,H.Gol,stsPPn,stsTagih,H.InsDate,H.InsBy, H.UpdDate,H.UpdBy From T_TrmBJ H Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join M_Supp S On H.SuppID=S.SuppID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By TrmID desc,Tanggal desc", koneksi)
        cmsl.TableMappings.Add("Table", "T_TrmBJ" & Gol)
        Try
            DsMaster.Tables("T_TrmBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrmBJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TrmBJ" & Gol
    End Sub

    Public Sub CekCurr()
        Dim cmSl As New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTanggal.EditValue & "') AND (Akhir >= '" & Me.DTPTanggal.EditValue & "') AND (MtUang = '" & MtUang & "')ORDER BY Tanggal DESC")
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
                        NilTukar = Reader.Item(1)
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

    Public Sub HitPPn()
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
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TrmBJ")
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

    Private Sub FTrmBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Terima Barang Jadi"
    End Sub

    Private Sub FTrmBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub FTrmBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FTrmBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTTrmBJ_e.Selected = True

        If Manual = True Then
            Me.GridColumn10.Visible = False
            Me.GridColumn11.Visible = False
            Me.CBOJns.Properties.Items.Clear()
            Me.CBOJns.Properties.Items.Add("Lain-Lain")
        Else
            Me.GridColumn10.Visible = True
            Me.GridColumn11.Visible = True
        End If

    End Sub


    Private Sub BVTTrmBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTrmBJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Terima Barang Jadi"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrmBJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrmBJDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("TrmBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Terima Barang Jadi"

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

        Me.SLUGrup.EditValue = ""
        Me.SLUSupp.EditValue = ""
        Me.SLUPOID.EditValue = ""
        Me.TBSJ.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.RBPPn.EditValue = "Non PPn"
        Me.TBPersenPPn.EditValue = 0
        MtUang = "IDR"

        If Manual = True Then
            Me.CBOJns.EditValue = "Lain-Lain"
            Me.LCIPO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        Else
            Me.CBOJns.EditValue = "PO"
            Me.LCIPO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        Me.TBInfo.EditValue = ""

        CekCurr()

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TrmBJDtl" & Gol).Clear()
        ReDim arrPar(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Terima Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("JnsDoc") = "BSTB" Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Dokumen BSTB", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TrmID")
        KdLama = Me.GridView2.GetFocusedDataRow.Item("TrmID")
        Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        Me.CBOJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsDoc")
        'Me.SLUDocID.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")

        If Me.CBOJns.EditValue = "PO" Then
            Me.SLUPOID.Properties.ReadOnly = False
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False

            cmsl = New SqlDataAdapter("Select POID,MtUang,TipePPn,PersenPPn,stsPPn From T_POBJJO Where SuppID='" & Me.SLUSupp.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and stsKirim='False' Union All Select POID,MtUang,TipePPn,PersenPPn,stsPPn From T_POBJLk Where SuppID='" & Me.SLUSupp.EditValue & "' and stsLunas='False'", koneksi)
            cmsl.TableMappings.Add("Table", "POBJLUE")
            Try
                DsMaster.Tables("POBJLUE").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "POBJLUE")

            Me.SLUPOID.Properties.DataSource = DsMaster.Tables("POBJLUE")
            Me.SLUPOID.Properties.DisplayMember = "POID"
            Me.SLUPOID.Properties.ValueMember = "POID"
        End If

        Me.SLUPOID.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
        Me.TBSJ.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJ")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        stsPPn = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        stsPPnLama = Me.GridView2.GetFocusedDataRow.Item("stsPPn")
        MtUang = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        NilTukar = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
        Me.RBPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("TipePPn")
        Me.TBPersenPPn.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenPPn")
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

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TrmID"), "TrmID")
        bind.Add(Me.LBGols.Text.Substring(4, Me.LBGols.Text.Length - 4), "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJ"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRTrmBJ
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Terima Barang Jadi"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdID"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TrmID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TrmBJ")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TrmID")
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

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        HitPPn()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_TrmBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@JnsDoc", SqlDbType.VarChar).Value = Me.CBOJns.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    If Manual = True Then
                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = ""
                    Else
                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    End If
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = MtUang
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = NilTukar
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Integer), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Integer), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Integer), 2)
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_TrmBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 13
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@DocIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                    .Parameters.Add("@BSTBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BSTBID")
                                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POID")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                    .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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
                Dim cmSP As New SqlCommand("SPUpT_TrmBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@JnsDoc", SqlDbType.VarChar).Value = Me.CBOJns.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    If Manual = True Then
                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = ""
                    Else
                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    End If
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = MtUang
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = NilTukar
                    .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                    .Parameters.Add("@PersenPPn", SqlDbType.Decimal).Value = Me.TBPersenPPn.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                    .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@stsPPn", SqlDbType.Bit).Value = stsPPn
                    .Parameters.Add("@stsPPnLama", SqlDbType.Bit).Value = stsPPnLama
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
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_TrmBJDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TrmIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TrmBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 13
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@DocIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                        .Parameters.Add("@BSTBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BSTBID")
                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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
                                        Me.GridView1.SetRowCellValue(i, "TrmIDD", Me.GridView1.GetRowCellValue(i, "TrmIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TrmBJDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 13
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@DocIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                        .Parameters.Add("@BSTBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BSTBID")
                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub


    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Me.SLUGrup.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            Try
                DsMaster.Tables("M_GudangLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If
        End If
    End Sub

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave
        Me.SLUPOID.EditValue = ""

        If Not IsDBNull(Me.SLUSupp.EditValue) And Me.SLUSupp.Properties.ReadOnly = False Then
            If Me.CBOJns.EditValue = "PO" Then
                Me.SLUPOID.Properties.ReadOnly = False
                Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False

                Dim cmsl As SqlDataAdapter

                If Gol = "Job Order" Then
                    cmsl = New SqlDataAdapter("Select POID,MtUang,TipePPn,PersenPPn,stsPPn From T_POBJJO Where SuppID='" & Me.SLUSupp.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and stsKirim='False'", koneksi)
                Else
                    cmsl = New SqlDataAdapter("Select POID,MtUang,TipePPn,PersenPPn,stsPPn From T_POBJLk Where SuppID='" & Me.SLUSupp.EditValue & "' and Gol='" & Gol & "' and stsLunas='False'", koneksi)
                End If

                cmsl.TableMappings.Add("Table", "POBJLUE")
                Try
                    DsMaster.Tables("POBJLUE").Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "POBJLUE")

                Me.SLUPOID.Properties.DataSource = DsMaster.Tables("POBJLUE")
                Me.SLUPOID.Properties.DisplayMember = "POID"
                Me.SLUPOID.Properties.ValueMember = "POID"
            End If

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If


    End Sub

    Private Sub SLUPOID_Leave(sender As Object, e As EventArgs) Handles SLUPOID.Leave

        If Not IsDBNull(Me.SLUPOID.EditValue) And Me.SLUPOID.Properties.ReadOnly = False Then
            Try

                'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                Me.RBPPn.EditValue = DsMaster.Tables("POBJLUE").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("TipePPn")
                Me.TBPersenPPn.EditValue = DsMaster.Tables("POBJLUE").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("PersenPPn")
                stsPPn = DsMaster.Tables("POBJLUE").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("stsPPn")
                MtUang = DsMaster.Tables("POBJLUE").Select("POID = '" & Me.SLUPOID.EditValue & "'")(0).Item("MtUang")

                CekCurr()

                Dim cmsl As SqlDataAdapter

                'Select TrmIDD,TrmID,DocIDD,BSTBID,POID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg,HarSat,HarAkhir

                cmsl = New SqlDataAdapter("Select *,Case When SatID Like 'D%' Then Qty Else 0 End As Dos,Qty*Isi As Psg, (Qty*Isi)*HarSat As HarAkhir From((Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as TrmIDD,'" & Me.TBKode.EditValue & "' As TrmID,'--' As BSTBID,H.POID,POIDD As DocIDD,D.ArtCode, ArtName,D.SatID,D.Isi,(Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & Me.TBKode.EditValue & "'),0)) As Qty,HarSat From T_POBJLk H Inner Join T_POBJLkDtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Where (Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & Me.TBKode.EditValue & "'),0))>0 and stsBatal='False' and H.Gol='" & Gol & "' and H.Grup Like '" & Me.SLUGrup.EditValue & "' and H.POID='" & Me.SLUPOID.EditValue & "' Union All Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as TrmIDD,'" & Me.TBKode.EditValue & "'  As TrmID,'--' As BSTBID,H.POID,POIDD As DocIDD,D.ArtCode,ArtName,D.SatID,D.Isi,(Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & Me.TBKode.EditValue & "'),0)) As Qty,HarSat From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Where (Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & Me.TBKode.EditValue & "'),0))>0 and stsBatal='False' and H.Grup Like '" & Me.SLUGrup.EditValue & "' and H.POID='" & Me.SLUPOID.EditValue & "'))as x", koneksi)

                cmsl.TableMappings.Add("Table", "T_TrmBJDtl" & Gol)
                cmsl.Fill(DsMaster, "T_TrmBJDtl" & Gol)
                DsMaster.Tables("T_TrmBJDtl" & Gol).Clear()
                cmsl.Fill(DsMaster, "T_TrmBJDtl" & Gol)

                DsMaster.Tables("T_TrmBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("DocIDD"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("BSTBID"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("POID"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("ArtCode")}

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_TrmBJDtl" & Gol

                'AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TrmIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then

            If Me.CBOJns.EditValue = "Lain-Lain" Then
                rw = 0

                Dim frm As New FTrmBJ_a(Me.CBOJns.EditValue, Gol, Me.SLUGrup.EditValue, Me.TBKode.EditValue)
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

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
            End If

            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat")) Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Me.GridView1.GetRowCellValue(e.RowHandle, "Psg") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"))
            End If


        ElseIf e.Column Is GridView1.Columns("HarSat") Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty")) Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Me.GridView1.GetRowCellValue(e.RowHandle, "Psg") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"))
            End If

        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "TrmIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "DocIDD", dataTrans.Item("DocIDD" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BSTBID", dataTrans.Item("BSTBID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "POID", dataTrans.Item("POID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", dataTrans.Item("Dos" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", dataTrans.Item("Psg" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", dataTrans.Item("HarSat" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", dataTrans.Item("HarAkhir" & rw).ToString)

                DsMaster.Tables("T_TrmBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("DocIDD"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("BSTBID"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("POID"), DsMaster.Tables("T_TrmBJDtl" & Gol).Columns("ArtCode")}

                rw += 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTrmBJ_d(Me.GridView2.GetFocusedDataRow.Item("TrmID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBSJ.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class