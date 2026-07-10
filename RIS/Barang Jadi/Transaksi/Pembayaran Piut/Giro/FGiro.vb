Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FGiro
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, CurrID, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        If Gol <> "Own" And Gol <> "Job Order" Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=33 and Gol='" & Gol & "'", koneksi)

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
        End If

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BGN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BGEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BGDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTBG_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUBank.Properties.ReadOnly = True
        Me.TBNoSeri.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.DTPTglCair.Properties.ReadOnly = True
        Me.TBTotBayar.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
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
        Me.BVTBG_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUBank.Properties.ReadOnly = False
        Me.TBNoSeri.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.DTPTglCair.Properties.ReadOnly = False
        Me.TBTotBayar.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBG_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        If Gol <> "Own" And Gol <> "Job Order" Then
            cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustL")
            cmsl.Fill(DsMaster, "M_CustL")
            DsMaster.Tables("M_CustL").Clear()
            cmsl.Fill(DsMaster, "M_CustL")

            Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
            Me.SLUCust.Properties.DisplayMember = "Nama"
            Me.SLUCust.Properties.ValueMember = "CustID"
        Else
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
        End If

        cmsl = New SqlDataAdapter("Select BankID,Nama From M_Bank Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BankLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BankLUE")
        DsMaster.Tables("M_BankLUE").Clear()
        cmsl.Fill(DsMaster, "M_BankLUE")

        Me.SLUBank.Properties.DataSource = DsMaster.Tables("M_BankLUE")
        Me.SLUBank.Properties.DisplayMember = "Nama"
        Me.SLUBank.Properties.ValueMember = "BankID"

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
        cmsl = New SqlDataAdapter("Select BGIDD,BGID,JualID,Bayar From T_BGDtl Where BGID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BGDtl" & Gol)
        Try
            DsMaster.Tables("T_BGDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BGDtl" & Gol)

        DsMaster.Tables("T_BGDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_BGDtl" & Gol).Columns("JualID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BGDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        If Gol = "Lain-Lain" Or Gol = "Penjualan Bahan" Then
            cmsl = New SqlDataAdapter("Select BGID,PeriodID,Tanggal,H.CabID,Cb.Cabang,H.CustID,C.Nama As Cust,C.Alamat,H.BankID,B.Nama As Bank,NoSeri,MtUang,CurrID, NilTukarRp,TotBayar,TotBayarRp,TglCair,TglKirim,H.Ket,SisaPakai,stsPakai,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_BG H Left Outer Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Bank B On H.BankID=B.BankID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' Order By Tanggal,BGID asc", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select BGID,PeriodID,Tanggal,H.CabID,Cb.Cabang,H.CustID,C.Nama As Cust,C.Alamat,H.BankID,B.Nama As Bank,NoSeri,MtUang,CurrID, NilTukarRp,TotBayar,TotBayarRp,TglCair,TglKirim,H.Ket,SisaPakai,stsPakai,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_BG H Left Outer Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Bank B On H.BankID=B.BankID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,BGID asc", koneksi)
        End If



        cmsl.TableMappings.Add("Table", "T_BG" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_BG" & Gol)
        DsMaster.Tables("T_BG" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_BG" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_BG" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_BG")
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
        If IO.File.Exists("SrJualGiro.xml") Then
            System.IO.File.Delete("SrJualGiro.xml")
        End If
    End Sub

    Private Sub FGiro_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Bilyet Giro"
    End Sub

    Private Sub FGiro_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FGiro_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FGiro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBG_e.Selected = True
    End Sub

    Private Sub BVTBG_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBG_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Bilyet Giro"
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Bilyet Giro"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglCair.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglKirim.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglCair.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now
        End If

        DelXml()

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        If Gol <> "Own" And Gol <> "Job Order" Then
            Me.SLUCab.Properties.ReadOnly = True
        End If

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUCab.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUBank.EditValue = ""
        Me.TBNoSeri.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.TBTotBayar.EditValue = 0.0
        Me.TBTotBayarRp.EditValue = 0.0
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_BGDtl" & Gol).Clear()
        ReDim arrPar(-1)

        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Bilyet Giro"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        If CDec(SlCek("T_BG", "TotBayar", "BGID", Me.GridView2.GetFocusedDataRow.Item("BGID"))) <> CDec(SlCek("T_BG", "SisaPakai", "BGID", Me.GridView2.GetFocusedDataRow.Item("BGID"))) Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub

        Else
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("BGID")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")

            If Gol = "Own" Or Gol = "Job Order" Then
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "' ", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustL")
                Try
                    DsMaster.Tables("M_CustL").Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_CustL")

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"
            End If

            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
            Me.MAlamat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Alamat")
            Me.SLUBank.EditValue = Me.GridView2.GetFocusedDataRow.Item("BankID")
            Me.TBNoSeri.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoSeri")
            Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
            CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
            Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
            Me.TBTotBayar.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotBayar")
            Me.TBTotBayarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotBayarRp")
            Me.DTPTglCair.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglCair")
            Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
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
            Me.SLUCab.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Bilyet Giro"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CDec(SlCek("T_BG", "TotBayar", "BGID", Me.GridView2.GetFocusedDataRow.Item("BGID"))) <> CDec(SlCek("T_BG", "SisaPakai", "BGID", Me.GridView2.GetFocusedDataRow.Item("BGID"))) Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub

        Else
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("BGID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_BG")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BGID")
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

        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.TBNoSeri.EditValue = "" Or IsDBNull(Me.TBNoSeri.EditValue) Then
            XtraMessageBox.Show("No Seri Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUBank.EditValue = "" Or IsDBNull(Me.SLUBank.EditValue) Then
            XtraMessageBox.Show("Bank Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CType(Me.GridView1.Columns("Bayar").SummaryText, Decimal) > Me.TBTotBayar.EditValue Then
            XtraMessageBox.Show("Pembayaran Melebihi Nominal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.TBTotBayarRp.EditValue = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_BG")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    If Gol = "Own" Or Gol = "Job Order" Then
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    Else
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = ""
                    End If
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@BankID", SqlDbType.VarChar).Value = Me.SLUBank.EditValue
                    .Parameters.Add("@NoSeri", SqlDbType.VarChar).Value = Me.TBNoSeri.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Me.TBTotBayar.EditValue
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Me.TBTotBayarRp.EditValue
                    .Parameters.Add("@TglCair", SqlDbType.Date).Value = Me.DTPTglCair.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@InputDari", SqlDbType.VarChar).Value = "Desktop"
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "JualID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_BGDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
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
                Dim cmSP As New SqlCommand("SPUpT_BG")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    '.Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    If Gol = "Own" Or Gol = "Job Order" Then
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    Else
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = ""
                    End If
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@BankID", SqlDbType.VarChar).Value = Me.SLUBank.EditValue
                    .Parameters.Add("@NoSeri", SqlDbType.VarChar).Value = Me.TBNoSeri.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Me.TBTotBayar.EditValue
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Me.TBTotBayarRp.EditValue
                    .Parameters.Add("@TglCair", SqlDbType.Date).Value = Me.DTPTglCair.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@InputDari", SqlDbType.VarChar).Value = "Desktop"
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
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_BGDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "BGIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "JualID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_BGDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
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
                                        Me.GridView1.SetRowCellValue(i, "BGIDD", Me.GridView1.GetRowCellValue(i, "BGIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "JualID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_BGDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BGIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
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

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.EditValue <> "" And Not IsDBNull(Me.SLUCab.EditValue) And Me.SLUCab.Properties.ReadOnly = False Then
            If Gol = "Own" Or Gol = "Job Order" Then

                Dim Reader As SqlClient.SqlDataReader
                Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=33 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

                If Manual = True Then
                    Me.TBKode.Properties.ReadOnly = False
                    Me.TBKode.EditValue = ""
                Else
                    Me.TBKode.Properties.ReadOnly = True
                    Me.TBKode.EditValue = "--"
                End If

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "' ", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustL")
                Try
                    DsMaster.Tables("M_CustL").Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_CustL")

                Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"
            End If
        End If
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.EditValue <> "" And Not IsDBNull(Me.SLUCust.EditValue) And Me.SLUCust.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "BGIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub BEdJualID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdJualID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdJualID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdJualID.ButtonClick
        If Gol = "Lain-Lain" Then
            Dim frm As New FSearch("Penjualan Lain2", Me.SLUCust.EditValue, "Lain-Lain", Me.TBKode.EditValue, Date.Now, "")
            frm.ShowDialog()

        ElseIf Gol = "Penjualan Bahan" Then
            Dim frm As New FSearch("Penjualan Bahan", Me.SLUCust.EditValue, "Penjualan Bahan", Me.TBKode.EditValue, Date.Now, "")
            frm.ShowDialog()

        Else
            Dim frm As New FSearch("Penjualan Giro", Me.SLUCust.EditValue, Gol, Me.TBKode.EditValue, Date.Now, "")
            frm.ShowDialog()
        End If

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bayar", dataTrans.Item("SisaBayar").ToString)

                AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "BGIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "JualID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("BGIDD")

        End If

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Bayar") Then
            Dim Sisa As Decimal
            Dim command As SqlCommand

            If Gol = "Lain-Lain" Then
                command = New SqlCommand("Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and DocID<>'" & Me.TBKode.EditValue & "'),0))-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and BGID<>'" & Me.TBKode.EditValue & "' and BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0)) From T_JualBebas Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "'", koneksi)

            ElseIf Gol = "Penjualan Bahan" Then
                command = New SqlCommand("Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and DocID<>'" & Me.TBKode.EditValue & "'),0))-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and BGID<>'" & Me.TBKode.EditValue & "' and BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0)) From T_JualBB Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "'", koneksi)

            Else
                command = New SqlCommand("Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and DocID<>'" & Me.TBKode.EditValue & "'),0))-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and BGID<>'" & Me.TBKode.EditValue & "' and BGID Not In (Select Distinct DocID From T_ByrPiutDtl)),0)) From T_JualBJ Where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "'", koneksi)
            End If

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With


            If Me.GridView1.GetRowCellValue(e.RowHandle, "Bayar") > Sisa Then
                XtraMessageBox.Show("Pembayaran Melebihi Sisa Faktur", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Bayar", Sisa)
            End If
        End If

    End Sub
    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FGiro_d(Me.GridView2.GetFocusedDataRow.Item("BGID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TBTotBayar_Leave(sender As Object, e As EventArgs) Handles TBTotBayar.Leave
        Me.TBTotBayarRp.EditValue = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNoSeri.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class