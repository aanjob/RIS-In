Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FCash
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
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
            Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=32 and Gol='" & Gol & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("CsN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("CsEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("CsDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTCash_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUBank.Properties.ReadOnly = True
        Me.TBAN.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.TBTotBayar.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

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
        Me.BVTCash_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUBank.Properties.ReadOnly = False
        Me.TBAN.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.TBTotBayar.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTCash_e.Selected = True
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

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        If Gol = "Lain-Lain" Or Gol = "Penjualan Bahan" Then
            cmsl = New SqlDataAdapter("Select CashID,PeriodID,Tanggal,H.CabID,Cb.Cabang,H.CustID,C.Nama As Cust,C.Alamat,H.BankID,B.Nama As Bank,AN,MtUang, CurrID,NilTukarRp,TotBayar,TotBayarRp,TglKirim,H.Ket,SisaPakai,stsPakai,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_Cash H Left Outer Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Left Outer Join M_Bank B On H.BankID=B.BankID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' Order By Tanggal,CashID asc", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select CashID,PeriodID,Tanggal,H.CabID,Cb.Cabang,H.CustID,C.Nama As Cust,C.Alamat,H.BankID,B.Nama As Bank,AN,MtUang, CurrID,NilTukarRp,TotBayar,TotBayarRp,TglKirim,H.Ket,SisaPakai,stsPakai,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_Cash H Left Outer Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Left Outer Join M_Bank B On H.BankID=B.BankID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,CashID asc", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "T_Cash" & Gol)
        Try
            DsMaster.Tables("T_Cash" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_Cash" & Gol)

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_Cash" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Cash")
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

    Private Sub FCash_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cash"
    End Sub

    Private Sub FCash_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FCash_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTCash_e.Selected = True
    End Sub

    Private Sub BVTCash_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTCash_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Cash"
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Cash"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglKirim.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        If Gol <> "Own" And Gol <> "Job Order" Then
            If Manual = True Then
                Me.TBKode.Properties.ReadOnly = False
                Me.TBKode.EditValue = ""
            Else
                Me.TBKode.Properties.ReadOnly = True
                Me.TBKode.EditValue = "--"
            End If
            Me.SLUCab.Properties.ReadOnly = True
        End If

        Me.TBKode.EditValue = ""
        Me.SLUCab.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUBank.EditValue = ""
        Me.TBAN.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.TBTotBayar.EditValue = 0.0
        Me.TBTotBayarRp.EditValue = 0.0
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Cash"

        If MainModule.SlstsPeriodEdDel(Me.GridView1.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CDec(SlCek("T_Cash", "TotBayar", "CashID", Me.GridView1.GetFocusedDataRow.Item("CashID"))) <> CDec(SlCek("T_Cash", "SisaPakai", "CashID", Me.GridView1.GetFocusedDataRow.Item("CashID"))) Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub

        Else
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("CashID")
            Me.DTPTanggal.EditValue = Me.GridView1.GetFocusedDataRow.Item("Tanggal")
            Me.SLUCab.EditValue = Me.GridView1.GetFocusedDataRow.Item("CabID")

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
            Me.SLUCust.EditValue = Me.GridView1.GetFocusedDataRow.Item("CustID")

            Me.MAlamat.EditValue = Me.GridView1.GetFocusedDataRow.Item("Alamat")
            Me.SLUBank.EditValue = Me.GridView1.GetFocusedDataRow.Item("BankID")
            Me.TBAN.EditValue = Me.GridView1.GetFocusedDataRow.Item("AN")
            Me.SLUMtUang.EditValue = Me.GridView1.GetFocusedDataRow.Item("MtUang")
            CurrID = Me.GridView1.GetFocusedDataRow.Item("CurrID")
            Me.TBNilTukarRp.EditValue = Me.GridView1.GetFocusedDataRow.Item("NilTukarRp")
            Me.TBTotBayar.EditValue = Me.GridView1.GetFocusedDataRow.Item("TotBayar")
            Me.TBTotBayarRp.EditValue = Me.GridView1.GetFocusedDataRow.Item("TotBayarRp")
            Me.DTPTglKirim.EditValue = Me.GridView1.GetFocusedDataRow.Item("TglKirim")
            Me.MKet.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ket")

            If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True
            Me.SLUCab.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Cash"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView1.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CDec(SlCek("T_Cash", "TotBayar", "CashID", Me.GridView1.GetFocusedDataRow.Item("CashID"))) <> CDec(SlCek("T_Cash", "SisaPakai", "CashID", Me.GridView1.GetFocusedDataRow.Item("CashID"))) Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub

        Else
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView1.GetFocusedDataRow.Item("CashID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_Cash")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("CashID")
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

        Me.TBTotBayarRp.EditValue = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_Cash")
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
                    .Parameters.Add("@AN", SqlDbType.VarChar).Value = Me.TBAN.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Me.TBTotBayar.EditValue
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
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

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBKode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            'MsgBox(CodeID)
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
                Dim cmSP As New SqlCommand("SPUpT_Cash")
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
                    .Parameters.Add("@AN", SqlDbType.VarChar).Value = Me.TBAN.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Me.TBTotBayar.EditValue
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
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
                Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=32 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub TBTotBayar_Leave(sender As Object, e As EventArgs) Handles TBTotBayar.Leave
        Me.TBTotBayarRp.EditValue = Math.Round(Me.TBTotBayar.EditValue * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBAN.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class