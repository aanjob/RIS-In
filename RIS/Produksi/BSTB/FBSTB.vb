Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBSTB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

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

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=12 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BSTBN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTBSTB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.CBOJns.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUBOM.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.BProses.Enabled = False

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
        Me.BVTBSTB_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJns.Properties.ReadOnly = False
        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUBOM.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.BProses.Enabled = True

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBSTB_e.Selected = True
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
        cmsl.TableMappings.Add("Table", "M_SuppLUE2")
        Try
            DsMaster.Tables("M_SuppLUE2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SuppLUE2")
        DsMaster.Tables("M_SuppLUE2").Rows.Add("", "", 0, "")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE2")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BSTBIDD,BSTBID,BOMID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg From T_BSTBDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where BSTBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BSTBDtl" & Gol)
        cmsl.Fill(DsMaster, "T_BSTBDtl" & Gol)
        DsMaster.Tables("T_BSTBDtl" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_BSTBDtl" & Gol)

        DsMaster.Tables("T_BSTBDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_BSTBDtl" & Gol).Columns("BOMID"), DsMaster.Tables("T_BSTBDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BSTBDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BSTBID,PeriodID,CodeID,Tanggal,JnsDoc,H.GdID,G.Nama As Gudang,H.SuppID,S.Nama As Supp,H.Ket,H.Grup, H.Gol,stsApp,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_BSTB H Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join M_Supp S On H.SuppID=S.SuppID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,BSTBID Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_BSTB" & Gol)
        Try
            DsMaster.Tables("T_BSTB" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BSTB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_BSTB" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_BSTB")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("BSTBID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supp"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRBSTB
        XR.InitializeData(bind)
    End Sub


    Private Sub FBSTB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "BSTB Produksi Barang Jadi"
    End Sub

    Private Sub FBSTB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FBSTB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FBSTB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBSTB_e.Selected = True
    End Sub

    Private Sub BVTBSTB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBSTB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search BSTB Produksi Barang Jadi"
        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BSTBEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("BSTBApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BSTBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("BSTBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create BSTB Produksi Barang Jadi"

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

        Me.CBOJns.EditValue = ""
        Me.SLUGrup.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.SLUSupp.EditValue = ""
        Me.SLUBOM.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_BSTBDtl" & Gol).Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit BSTB Produksi Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If SlCek("T_BSTB", "stsApp", "BSTBID", Me.GridView2.GetFocusedDataRow.Item("BSTBID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"

        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("BSTBID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.CBOJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsDoc")
        If Me.CBOJns.EditValue = "Lain-Lain" Then
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
            Me.SLUBOM.EditValue = ""
            Me.SLUBOM.Properties.ReadOnly = True
            Me.BProses.Enabled = False
        Else
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
            Me.SLUBOM.Properties.ReadOnly = False
            Me.BProses.Enabled = True
        End If

        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Pusat='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")

        If Me.CBOJns.EditValue = "BOM" Then

            cmsl = New SqlDataAdapter("Select BOMID,POID,ArtName,Warna From T_BOM Where stsLunas='False' and Grup='" & Me.SLUGrup.EditValue & "' and Gol='" & Gol & "' and stsApp='True' Union All Select BOMID,POID,ArtName,Warna From T_BOM Where stsLunas='False' and stsApp='True' and POID=''", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOMLUE" & Gol)
            Try
                DsMaster.Tables("T_BOMLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "T_BOMLUE" & Gol)

            Me.SLUBOM.Properties.DataSource = DsMaster.Tables("T_BOMLUE" & Gol)
            Me.SLUBOM.Properties.DisplayMember = "BOMID"
            Me.SLUBOM.Properties.ValueMember = "BOMID"
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
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPAppBSTB")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@BSTBID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BSTBID")
            .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Gol")
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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete BSTB Produksi Barang Jadi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_BSTB", "stsApp", "BSTBID", Me.GridView2.GetFocusedDataRow.Item("BSTBID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("BSTBID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_BSTB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BSTBID")
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
        Print()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGd.EditValue = "" Or IsDBNull(Me.SLUGd.EditValue) Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CBOJns.EditValue = "" Or IsDBNull(Me.CBOJns.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_BSTB")
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
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Integer), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Integer), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Integer), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_BSTBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Dos", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                    .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
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
                Dim cmSP As New SqlCommand("SPUpT_BSTB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@JnsDoc", SqlDbType.VarChar).Value = Me.CBOJns.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Integer), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Integer), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Integer), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_BSTBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "BSTBIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_BSTBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
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
                                        Me.GridView1.SetRowCellValue(i, "BSTBIDD", Me.GridView1.GetRowCellValue(i, "BSTBIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_BSTBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BSTBIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
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

        Me.BVTBSTB_s.Selected = True
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("BSTBP"), Boolean)

        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "BSTBID") = Me.TBKode.EditValue Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Print()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub CBOJns_Leave(sender As Object, e As EventArgs) Handles CBOJns.Leave
        If Me.CBOJns.EditValue = "BOM" Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select BOMID,POID,ArtName,Warna From T_BOM Where stsLunas='False' and Grup='" & Me.SLUGrup.EditValue & "' and Gol='" & Gol & "' and stsApp='True' Union All Select BOMID,POID,ArtName,Warna From T_BOM Where stsLunas='False' and stsApp='True' and POID=''", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOMLUE" & Gol)
            Try
                DsMaster.Tables("T_BOMLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "T_BOMLUE" & Gol)

            Me.SLUBOM.Properties.DataSource = DsMaster.Tables("T_BOMLUE" & Gol)
            Me.SLUBOM.Properties.DisplayMember = "BOMID"
            Me.SLUBOM.Properties.ValueMember = "BOMID"
        End If

        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "BSTBIDD")

            Me.GridView1.DeleteRow(i)
        Next

        If Me.CBOJns.EditValue = "Lain-Lain" Then
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = True
            Me.SLUBOM.EditValue = ""
            Me.SLUBOM.Properties.ReadOnly = True
            Me.BProses.Enabled = False
        Else
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Visible = False
            Me.SLUBOM.Properties.ReadOnly = False
            Me.BProses.Enabled = True
        End If

    End Sub

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Me.SLUGrup.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Pusat='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
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

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        If DsMaster.Tables("T_BOMLUE" & Gol).Select("BOMID = '" & Me.SLUBOM.EditValue & "'")(0).Item("POID") = "" Then
            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BSTBIDD,BOMID,ArtCode,ArtName,SatID,Isi,Qty, Case When SatID='P' Then 0 Else Qty End As Dos, Qty*Isi As Psg From (Select Distinct H.BOMID,BP.ArtCode,B.ArtName,BP.SatID,BP.Isi,Qty+QtyPol-BP.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=BP.ArtCode),0)) As Qty From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Left Outer Join M_Brg B On BP.ArtCode=B.ArtCode Where H.Gol<>'Job Order' and H.BOMID='" & Me.SLUBOM.EditValue & "' and H.POID='') As x Where Qty>0", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BSTBIDD,BOMID,ArtCode,ArtName,SatID,Isi,Qty, Case When SatID='P' Then 0 Else Qty End As Dos, Qty*Isi As Psg From (Select Distinct * From(Select Distinct H.BOMID,P.ArtCode,B.ArtName, P.SatID,P.Isi,Case When BP.ArtCodeInd=BP.ArtCode Then BP.Qty-BP.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=P.ArtCode),0)) Else (H.TotPsg/P.Isi)-BP.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=P.ArtCode),0)) End As Qty  From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Inner Join T_POBJJODtl P On H.POID=P.POID Inner Join M_Brg B On P.ArtCode=B.ArtCode and BP.ArtCodeInd=P.ArtCode Where H.Gol='Job Order' and H.BOMID='" & Me.SLUBOM.EditValue & "' Union All Select Distinct H.BOMID,P.ArtCode,B.ArtName,P.SatID,P.Isi,((H.TotPsg+H.TotPsgPol)/B.Isi)-BP.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=P.ArtCode),0)) As Qty From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Inner Join T_POBJLkDtl P On H.POID=P.POID and BP.ArtCodeInd=P.ArtCode Inner Join M_Brg B On P.ArtCode=B.ArtCode Where H.Gol<>'Job Order' and H.BOMID='" & Me.SLUBOM.EditValue & "' Union All Select Distinct H.BOMID,BP.ArtCode,B.ArtName,BP.SatID,BP.Isi,BP.Qty-BP.BtlOrder-BP.Upp-BP.Hancur-BP.Hilang-(Select Isnull((Select Sum(Qty)*IsiDlmDos From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=BP.ArtCodeInd),0)) As Qty  From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where H.BOMID='" & Me.SLUBOM.EditValue & "')as y) As x Where Qty>0", koneksi)
            'cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BSTBIDD,BOMID,ArtCode,ArtName,SatID,Isi,Qty, Case When SatID='P' Then 0 Else Qty End As Dos, Qty*Isi As Psg From (Select Distinct H.BOMID,P.ArtCode,B.ArtName,P.SatID,P.Isi,Case When BP.ArtCodeInd=BP.ArtCode Then BP.Qty-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=P.ArtCode),0)) Else (H.TotPsg/P.Isi)-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=P.ArtCode),0)) End As Qty  From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Inner Join T_POBJJODtl P On H.POID=P.POID Inner Join M_Brg B On P.ArtCode=B.ArtCode and BP.ArtCodeInd=P.ArtCode Where H.Gol='Job Order' and H.BOMID='" & Me.SLUBOM.EditValue & "' Union All Select Distinct H.BOMID,P.ArtCode,B.ArtName,P.SatID,P.Isi,((H.TotPsg+H.TotPsgPol)/B.Isi)-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=P.ArtCode),0)) As Qty From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Inner Join T_POBJLkDtl P On H.POID=P.POID and BP.ArtCodeInd=P.ArtCode Inner Join M_Brg B On P.ArtCode=B.ArtCode Where H.Gol<>'Job Order' and H.BOMID='" & Me.SLUBOM.EditValue & "' Union All Select Distinct H.BOMID,BP.ArtCode,B.ArtName,BP.SatID,BP.Isi,BP.Qty-BP.Upp-BP.Hancur-BP.Hilang-(Select Isnull((Select Sum(Qty)*IsiDlmDos From T_BSTBDtl Where BOMID=H.BOMID and ArtCode=BP.ArtCodeInd),0)) As Qty  From T_BOM H Inner Join T_BOMPO BP On H.BOMID=BP.BOMID Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where H.Gol<>'Job Order' and H.BOMID='" & Me.SLUBOM.EditValue & "') As x Where Qty>0", koneksi)
        End If

        cmsl.Fill(DsMaster, "T_BSTBDtl" & Gol)

        DsMaster.Tables("T_BSTBDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_BSTBDtl" & Gol).Columns("BOMID"), DsMaster.Tables("T_BSTBDtl" & Gol).Columns("ArtCode")}
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FBSTB_d(Me.GridView2.GetFocusedDataRow.Item("BSTBID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            If Me.CBOJns.EditValue <> "Lain-Lain" Then

                Dim Sisa As Integer
                Dim command As New SqlCommand("Select Distinct Case When P.ArtCodeInd=P.ArtCode Then P.Qty-P.BtlOrder-P.Upp-P.Hancur-P.Hilang-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and BSTBID<>'" & Me.TBKode.EditValue & "'),0)) Else (TotPsg/" & Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") & ")-P.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and BSTBID<>'" & Me.TBKode.EditValue & "'),0)) End As Qty From T_BOMPO P Inner Join T_BOM H On H.BomID=P.BOMID Where H.BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCodeInd='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and H.Gol='Job Order' Union All Select Distinct Case When P.ArtCodeInd=P.ArtCode Then P.Qty-P.BtlOrder-P.Upp-P.hancur-P.Hilang-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and BSTBID<>'" & Me.TBKode.EditValue & "'),0)) Else (TotPsg/" & Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") & ")-P.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and BSTBID<>'" & Me.TBKode.EditValue & "'),0)) End As Qty From T_BOMPO P Inner Join T_BOM H On H.BomID=P.BOMID Where H.BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and H.Gol='Job Order' Union All Select ((H.TotPsg+H.TotPsgPol)/" & Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") & ")-P.BtlOrder-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and BSTBID<>'" & Me.TBKode.EditValue & "'),0)) As Qty From T_BOM H  Inner Join T_POBJLkDtl P On H.POID=P.POID Where H.BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and (ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' Or '" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' In (Select ArtCode From T_POBJLkDtl2 Where POID=H.POID)) and H.Gol<>'Job Order'", koneksi)

                With koneksi
                    .Open()
                    Sisa = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Order BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa)
                End If
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("BSTBIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            Dim frm As New FBSTB_a(Gol, Me.SLUGrup.EditValue)
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

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Me.CBOJns.EditValue = "Lain-Lain" Then
                If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                    Me.GridView1.SetRowCellValue(e.RowHandle, "BSTBID", Me.TBKode.EditValue)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "BSTBIDD", Me.GridView1.RowCount * -1)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "DocIDD", dataTrans.Item("DocIDD" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", dataTrans.Item("BOMID" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", dataTrans.Item("Dos" & rw).ToString)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", dataTrans.Item("Psg" & rw).ToString)

                    DsMaster.Tables("T_BSTBDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_BSTBDtl" & Gol).Columns("BOMID"), DsMaster.Tables("T_BSTBDtl" & Gol).Columns("ArtCode")}

                    rw += 1
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