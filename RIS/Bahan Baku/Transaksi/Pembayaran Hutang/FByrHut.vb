Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FByrHut
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, CurrID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim IdD As Integer
    Dim arrPar(-1) As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=39", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ByrHN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTBayar_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
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
        Me.BVBExit.Enabled = False
        Me.BVTBayar_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBayar_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,JT,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where S.Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE2")
        cmsl.Fill(DsMaster, "M_SuppLUE2")
        DsMaster.Tables("M_SuppLUE2").Clear()
        cmsl.Fill(DsMaster, "M_SuppLUE2")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE2")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"

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
        cmsl = New SqlDataAdapter("Select ByrHutIDD,ByrHutID,TagihID,DocID,Bayar,(Select Isnull((Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl D Where TagihID=T_Tagihan.TagihID and D.ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_Tagihan Where TagihID=T_ByrHutDtl.TagihID),0)) As SisaBayar,(Select Sum(SisaPakai) From(Select Isnull((Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where DocID=T_MetodeByr.MetodeID and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_MetodeByr Where MetodeID=T_ByrHutDtl.DocID),0) As SisaPakai Union All Select Isnull((Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where DocID=T_JMHut.JMID and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_JMHut Where JMID=T_ByrHutDtl.DocID),0) As SisaPakai Union All Select Isnull((Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where DocID=T_RtrTagih.RtrTagihID and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_RtrTagih Where RtrTagihID=T_ByrHutDtl.DocID),0) As SisaPakai) as x) As SisaPakai From T_ByrHutDtl Where ByrHutID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ByrHutDtl")
        Try
            DsMaster.Tables("T_ByrHutDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ByrHutDtl")

        DsMaster.Tables("T_ByrHutDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ByrHutDtl").Columns("TagihID"), DsMaster.Tables("T_ByrHutDtl").Columns("DocID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ByrHutDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ByrHutID,PeriodID,Jenis,Tanggal,H.SuppID,S.Nama As Supp,S.Alamat,K.Nama As Kota,CurrID,MtUang,NilTukarRp, TotBayar,TotBayarRp,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_ByrHut H Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_Kota K On S.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,ByrHutID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_ByrHut")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_ByrHut")
        DsMaster.Tables("T_ByrHut").Clear()
        cmsl.Fill(DsMaster, "T_ByrHut")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ByrHut"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_ByrHut")
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

    Public Sub Delxml()
        If IO.File.Exists("SrTagihan.xml") Then
            System.IO.File.Delete("SrTagihan.xml")
        End If

        If IO.File.Exists("SrByrHutRtr.xml") Then
            System.IO.File.Delete("SrByrHutRtr.xml")
        End If

        If IO.File.Exists("SrByrHutNRtr.xml") Then
            System.IO.File.Delete("SrByrHutNRtr.xml")
        End If

        If IO.File.Exists("SrByrHutJM.xml") Then
            System.IO.File.Delete("SrByrHutJM.xml")
        End If
    End Sub

    Private Sub FByrHut_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Pembayaran Hutang"
    End Sub

    Private Sub FByrHut_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FByrHut_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FByrHut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBayar_e.Selected = True
    End Sub

    Private Sub BVTBayar_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBayar_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Pembayaran Hutang"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ByrHEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ByrHDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Pembayaran Hutang"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        Delxml()
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

        Me.SLUSupp.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOJenis.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_ByrHutDtl").Clear()

        ReDim arrPar(-1)

        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Pembayaran Hutang"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Delxml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ByrHutID")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
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
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Pembayaran Hutang"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("ByrHutID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_ByrHut")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ByrHutID")
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

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim o : For o = 0 To GridView1.RowCount - 1
            Dim SisaBayar As Decimal = 0
            Dim SisaPakai As Decimal = 0
           
            Dim a : For a = 0 To GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(o, "TagihID") = Me.GridView1.GetRowCellValue(a, "TagihID") Then
                    SisaBayar += Me.GridView1.GetRowCellValue(a, "Bayar")
                End If
            Next

            If SisaBayar > Me.GridView1.GetRowCellValue(o, "SisaBayar") Then
                XtraMessageBox.Show("Pembayaran " & Me.GridView1.GetRowCellValue(o, "TagihID") & " Melebihi Sisa Bayar Tagihan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim p : For p = 0 To GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(o, "DocID") = Me.GridView1.GetRowCellValue(p, "DocID") Then
                    SisaPakai += Me.GridView1.GetRowCellValue(p, "Bayar")
                End If
            Next

            If SisaPakai > Me.GridView1.GetRowCellValue(o, "SisaPakai") Then
                XtraMessageBox.Show("Dokumen " & Me.GridView1.GetRowCellValue(o, "DocID") & " Melebihi Sisa Pakai Dokumen", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Next

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_ByrHut")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 39
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Math.Round(CType(Me.GridView1.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "TagihID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_ByrHutDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@TagihID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihID")
                                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                    .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Bayar")
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
                Dim cmSP As New SqlCommand("SPUpT_ByrHut")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 39
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Math.Round(CType(Me.GridView1.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_ByrHutDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "ByrHutIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "TagihID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ByrHutDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@TagihID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihID")
                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                        .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Bayar")
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
                                        Me.GridView1.SetRowCellValue(i, "ByrHutIDD", Me.GridView1.GetRowCellValue(i, "ByrHutIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If

                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "TagihID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_ByrHutDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ByrHutIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@TagihID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TagihID")
                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                        .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Bayar")
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

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ByrHutIDD")
        End If

    End Sub

    Private Sub BEdTagihID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdTagihID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdTagihID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdTagihID.ButtonClick
        Try

            Dim frm As New FSearch("Tagihan", Me.SLUSupp.EditValue, Me.SLUMtUang.EditValue, "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SisaBayar", dataTrans.Item("SisaBayar").ToString)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdDocID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdDocID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdDocID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdDocID.ButtonClick
        Try

            If Me.CBOJenis.EditValue = "Non Retur" Then
                Dim frm As New FSearch("ByrHut Non Retur", Me.SLUSupp.EditValue, Me.SLUMtUang.EditValue, "", Date.Now, "")
                frm.ShowDialog()
            ElseIf Me.CBOJenis.EditValue = "Retur" Then
                Dim frm As New FSearch("ByrHut Retur", Me.SLUSupp.EditValue, Me.SLUMtUang.EditValue, "", Date.Now, "")
                frm.ShowDialog()
            ElseIf Me.CBOJenis.EditValue = "JM" Then
                Dim frm As New FSearch("ByrHut JM", Me.SLUSupp.EditValue, Me.SLUMtUang.EditValue, "", Date.Now, "")
                frm.ShowDialog()
            End If

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SisaPakai", CDec(dataTrans.Item("SisaPakai").ToString))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Bayar") Then

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SisaBayar") < Me.GridView1.GetRowCellValue(e.RowHandle, "SisaPakai") Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Bayar") > Me.GridView1.GetRowCellValue(e.RowHandle, "SisaBayar") Then
                    XtraMessageBox.Show("Pembayaran Melebihi Tagihan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", Me.GridView1.GetRowCellValue(e.RowHandle, "SisaBayar"))
                End If
            Else
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Bayar") > Me.GridView1.GetRowCellValue(e.RowHandle, "SisaPakai") Then
                    XtraMessageBox.Show("Pembayaran Melebihi Nominal Bayar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", Me.GridView1.GetRowCellValue(e.RowHandle, "SisaPakai"))
                End If
            End If

            'Dim SisaBayar, SisaPakai As Decimal

            'Dim command As New SqlCommand("Select TotAkhirRp-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where TagihID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "TagihID") & "' and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_Tagihan Where TagihID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "TagihID") & "'", koneksi)

            'With koneksi
            '    .Open()
            '    SisaBayar = command.ExecuteScalar()
            '    .Close()
            'End With


            'If Me.CBOJenis.EditValue = "Non Retur" Then
            '    command = New SqlCommand("Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where DocID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocID") & "' and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_MetodeByr Where MetodeID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocID") & "'", koneksi)

            '    With koneksi
            '        .Open()
            '        SisaPakai = command.ExecuteScalar()
            '        .Close()
            '    End With

            'ElseIf Me.CBOJenis.EditValue = "Retur" Then

            '    command = New SqlCommand("Select TotAkhirRp-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where DocID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocID") & "' and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_RtrTagih Where RtrTagihID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocID") & "'", koneksi)

            '    With koneksi
            '        .Open()
            '        SisaPakai = command.ExecuteScalar()
            '        .Close()
            '    End With

            'ElseIf Me.CBOJenis.EditValue = "JM" Then
            '    command = New SqlCommand("Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrHutDtl Where DocID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocID") & "' and ByrHutID<>'" & Me.TBKode.EditValue & "'),0)) From T_JMHut Where JMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocID") & "'", koneksi)

            '    With koneksi
            '        .Open()
            '        SisaPakai = command.ExecuteScalar()
            '        .Close()
            '    End With
            'End If

            'If SisaBayar < SisaPakai Then
            '    If Me.GridView1.GetRowCellValue(e.RowHandle, "Bayar") > SisaBayar Then
            '        XtraMessageBox.Show("Pembayaran Melebihi Tagihan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", SisaBayar)
            '    End If
            'Else
            '    If Me.GridView1.GetRowCellValue(e.RowHandle, "Bayar") > SisaPakai Then
            '        XtraMessageBox.Show("Pembayaran Melebihi Nominal Bayar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", SisaPakai)
            '    End If
            'End If
        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "ByrHutIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TagihID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "DocID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FByrHut_d(Me.GridView2.GetFocusedDataRow.Item("ByrHutID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CBOJenis_Leave(sender As Object, e As EventArgs) Handles CBOJenis.Leave
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ByrHutIDD")

            Me.GridView1.DeleteRow(i)
        Next
    End Sub

    Private Sub SLUSupp_Leave(sender As Object, e As EventArgs) Handles SLUSupp.Leave
        Delxml()

        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ByrHutIDD")

            Me.GridView1.DeleteRow(i)
        Next
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class