Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FByrPiutSlg
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, CurrID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim IdD As Integer
    Dim arrPar(-1) As String
    Dim arrParS2(-1) As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ByrPN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ByrPEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ByrPDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTBayar_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUCustAs.Properties.ReadOnly = True
        Me.CBOGolAs.Properties.ReadOnly = True
        Me.SLUCustTj.Properties.ReadOnly = True
        Me.CBOGolTj.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.TBTotBayar.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.BandedGridView1.OptionsBehavior.Editable = False

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
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUCustAs.Properties.ReadOnly = False
        Me.CBOGolAs.Properties.ReadOnly = False
        Me.SLUCustTj.Properties.ReadOnly = False
        Me.CBOGolTj.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.TBTotBayar.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BandedGridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBayar_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
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

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ByrPiutID,PeriodID,CodeID,Tanggal,H.CabID,Cb.Cabang,H.CustIDAs,C.Nama As CustAs,C.Alamat As AlamatAs,K.Nama As KotaAs, GolAs,H.CustIDTj,(Select Nama From M_Cust Where CustID=H.CustIDTj) as CustTj,(Select Alamat From M_Cust Where CustID=H.CustIDTj) as AlamatTj,(Select K.Nama From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where CustID=H.CustIDTj) as KotaTj,GolTj,CurrID,MtUang,NilTukarRp, TotBayar,TotBayarRp,H.Ket, H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_ByrPiutSlg H Inner Join M_Cust C On H.CustIDAs=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Cab Cb On H.CabID=Cb.CabID Where PeriodID=" & MainModule.periodAktif & " and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,ByrPiutID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_ByrPiutSlgSlg")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_ByrPiutSlgSlg")
        DsMaster.Tables("T_ByrPiutSlgSlg").Clear()
        cmsl.Fill(DsMaster, "T_ByrPiutSlgSlg")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ByrPiutSlgSlg"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_ByrPiutSlg")
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


    Private Sub FByrPiutSlg_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Pembayaran Piutang"
    End Sub

    Private Sub FByrPiutSlg_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FByrPiutSlg_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FByrPiutSlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTBayar_e.Selected = True
    End Sub

    Private Sub BVTBayar_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBayar_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Pembayaran Piutang"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Pembayaran Piutang"

        DsMaster.Clear()

        If MainModule.SlstsPeriodNew() = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        Me.SLUCab.EditValue = ""
        Me.SLUCustAs.EditValue = ""
        Me.CBOGolAs.EditValue = ""
        Me.SLUCustTj.EditValue = ""
        Me.CBOGolTj.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.TBTotBayar.EditValue = 0.0
        Me.TBTotBayarRp.EditValue = 0.0
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Pembayaran Piutang"

        If MainModule.SlstsPeriodEdDel(Me.BandedGridView1.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("ByrPiutID")
        Me.DTPTanggal.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Tanggal")
        Me.SLUCab.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CabID")
        CodeID = Me.BandedGridView1.GetFocusedDataRow.Item("CodeID")


        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustAs")
        Try
            DsMaster.Tables("M_CustAs").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CustAs")

        Me.SLUCustAs.Properties.DataSource = DsMaster.Tables("M_CustAs")
        Me.SLUCustAs.Properties.DisplayMember = "Nama"
        Me.SLUCustAs.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustTj")
        Try
            DsMaster.Tables("M_CustTj").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CustTj")

        Me.SLUCustTj.Properties.DataSource = DsMaster.Tables("M_CustTj")
        Me.SLUCustTj.Properties.DisplayMember = "Nama"
        Me.SLUCustTj.Properties.ValueMember = "CustID"

        Me.SLUCustAs.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CustIDAs")
        Me.CBOGolAs.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("GolAs")
        Me.SLUCustTj.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("CustIDTj")
        Me.CBOGolTj.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("GolTj")
        CurrID = Me.BandedGridView1.GetFocusedDataRow.Item("CurrID")
        Me.SLUMtUang.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("MtUang")
        Me.TBNilTukarRp.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("NilTukarRp")
        Me.TBTotBayar.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotBayar")
        Me.TBTotBayarRp.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("TotBayarRp")
        Me.MKet.EditValue = Me.BandedGridView1.GetFocusedDataRow.Item("Ket")

        If IsDBNull(Me.BandedGridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.BandedGridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.BandedGridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Pembayaran Piutang"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.BandedGridView1.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.BandedGridView1.GetFocusedDataRow.Item("ByrPiutID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_ByrPiutSlg")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetFocusedDataRow.Item("ByrPiutID")
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

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.SLUCustTj.EditValue = "" Or IsDBNull(Me.SLUCustTj.EditValue) Then
            XtraMessageBox.Show("Customer Tujuan Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.CBOGolTj.EditValue = "" Or IsDBNull(Me.CBOGolTj.EditValue) Then
            XtraMessageBox.Show("Golongan Tujuan Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.TBTotBayarRp.EditValue = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_ByrPiutSlg")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 43
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@CustIDAs", SqlDbType.VarChar).Value = Me.SLUCustAs.EditValue
                    .Parameters.Add("@GolAs", SqlDbType.VarChar).Value = Me.CBOGolAs.EditValue
                    .Parameters.Add("@CustIDTj", SqlDbType.VarChar).Value = Me.SLUCustTj.EditValue
                    .Parameters.Add("@GolTj", SqlDbType.VarChar).Value = Me.CBOGolTj.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Me.TBTotBayar.EditValue
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
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
                Dim cmSP As New SqlCommand("SPUpT_ByrPiutSlg")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 43
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@CustIDAs", SqlDbType.VarChar).Value = Me.SLUCustAs.EditValue
                    .Parameters.Add("@GolAs", SqlDbType.VarChar).Value = Me.CBOGolAs.EditValue
                    .Parameters.Add("@CustIDTj", SqlDbType.VarChar).Value = Me.SLUCustTj.EditValue
                    .Parameters.Add("@GolTj", SqlDbType.VarChar).Value = Me.CBOGolTj.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Me.TBTotBayar.EditValue
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
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

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.EditValue <> "" And Not IsDBNull(Me.SLUCab.EditValue) And Me.SLUCab.Properties.ReadOnly = False Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=43 and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustAs")
            Try
                DsMaster.Tables("M_CustAs").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_CustAs")

            Me.SLUCustAs.Properties.DataSource = DsMaster.Tables("M_CustAs")
            Me.SLUCustAs.Properties.DisplayMember = "Nama"
            Me.SLUCustAs.Properties.ValueMember = "CustID"

            cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustTj")
            Try
                DsMaster.Tables("M_CustTj").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_CustTj")

            Me.SLUCustTj.Properties.DataSource = DsMaster.Tables("M_CustTj")
            Me.SLUCustTj.Properties.DisplayMember = "Nama"
            Me.SLUCustTj.Properties.ValueMember = "CustID"
        End If
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs)
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub TBTotBayar_Leave(sender As Object, e As EventArgs) Handles TBTotBayar.Leave
        Me.TBTotBayarRp.EditValue = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
    End Sub


    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class