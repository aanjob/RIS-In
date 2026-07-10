Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FHitTrPromo
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

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=55", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HTrPrN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HTrPrEd"), Boolean)
        Me.BVBCair.Enabled = CType(TcodeCollection.Item("HTrPrC"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HTrPrDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTHitTrPromo_e.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUTahun.Properties.ReadOnly = True
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.BProses.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBCair.Enabled = False
        Me.BVBExExcel.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTHitTrPromo_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUTahun.Properties.ReadOnly = False
        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BProses.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTHitTrPromo_e.Selected = True
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HitTrPromoIDD,HitTrPromoID,D.CabID,Cb.Cabang,D.CustID,C.Nama As Cust,J.Jenis As JnsCust,Omset,Pembagi,Poin,Reward,Retur, ProsRtr,RewardRtr,Brand,RewardBrand,TotReward From T_HitTrPromoDtl D Inner Join M_Cust C On D.CustID=C.CustID Inner Join M_Cab Cb On D.CabID=Cb.CabID Inner Join M_JnsCust J On C.JnsCustID=J.JnsCustID Inner Join M_Kota K On C.KotaID=K.KotaID Where HitTrPromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitTrPromoDtl")
        Try
            DsMaster.Tables("T_HitTrPromoDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitTrPromoDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HitTrPromoDtl"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HitTrPromoID,PeriodID,CodeID,Tanggal,Tahun,TglAwal,TglAkhir,TotReward,Ket,stsCair,InsDate,InsBy,UpdDate,UpdBy From T_HitTrPromo Where Tahun=" & MainModule.periodeTahun & "", koneksi)
        cmsl.TableMappings.Add("Table", "T_HitTrPromo")
        Try
            DsMaster.Tables("T_HitTrPromo").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitTrPromo")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_HitTrPromo"
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Tahun From M_ScmTrPromo", koneksi)
        cmsl.TableMappings.Add("Table", "M_TahunLUE")
        Try
            DsMaster.Tables("M_TahunLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_TahunLUE")

        Me.SLUTahun.Properties.DataSource = DsMaster.Tables("M_TahunLUE")
        Me.SLUTahun.Properties.DisplayMember = "Tahun"
        Me.SLUTahun.Properties.ValueMember = "Tahun"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_HitTrPromo")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
    End Sub

#Region "Export Excel"

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Apakah Anda Mau Membuka File Ini?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ExportTo(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView1.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String, ByVal Nama As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Nama
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

#End Region

    Private Sub FHitTrPromo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Perhitungan Trade Promo"
    End Sub

    Private Sub FHitTrPromo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub FHitTrPromo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FHitTrPromo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTHitTrPromo_e.Selected = True
    End Sub


    Private Sub BVTHitTrPromo_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTHitTrPromo_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Perhitungan Trade Promo"
        FillDt()
        Me.BVBExExcel.Enabled = CType(TcodeCollection.Item("HTrPrExEc"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Perhitungan Trade Promo"

        DsMaster.Clear()

        If MainModule.SlCairTrPromo() > 0 Then
            XtraMessageBox.Show("Ada Trade Promo yang Belum Dicairkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

        Me.TBKode.EditValue = ""
        Me.SLUTahun.EditValue = ""
        Me.DTPAwal.EditValue = System.DateTime.Now
        Me.DTPAkhir.EditValue = System.DateTime.Now
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""


        FillDtl("")
        DsMaster.Tables("T_HitTrPromoDtl").Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Perhitungan Trade Promo"

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("HitTrPromoID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUTahun.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tahun")
        Me.DTPAwal.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglAwal")
        Me.DTPAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglAkhir")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True
    End Sub

    Private Sub BVBCair_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCair.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Pencairan Komisi"

        If XtraMessageBox.Show("Apakah Trade Promo Tahun " & Me.GridView1.GetFocusedDataRow.Item("Tahun") & " Sudah Dicairkan ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim cmSP As New SqlCommand("SPCairKmsPr")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("HitTrPromoID")
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
                        XtraMessageBox.Show("Trade Promo Berhasil Dicairkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Trade Promo Gagal Dicairkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Trade Promo Gagal Dicairkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
            End With
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Perhitungan Trade Promo"

        koneksi.Close()

        If MainModule.SlstsCairTrPromo(Me.GridView2.GetFocusedDataRow.Item("HitTrPromoID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dicairkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Komisi: " & Me.GridView2.GetFocusedDataRow.Item("HitTrPromoID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_HitTrPromo")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("HitTrPromoID")
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
                End Try
            End With
        End If
    End Sub

    Private Sub BVBExExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExExcel.ItemClick
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Perhitungan Trade Promo Tahun " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & "")

        FillDtl(Me.GridView2.GetFocusedDataRow.Item("HitTrPromoID"))

        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_HitTrPromo")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@TotReward", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("TotReward").SummaryText, Decimal), 2)
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

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CustID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitTrPromoDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                                    .Parameters.Add("@Omset", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Omset")
                                    .Parameters.Add("@Pembagi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Poin")
                                    .Parameters.Add("@Poin", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Poin")
                                    .Parameters.Add("@Reward", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Reward")
                                    .Parameters.Add("@Retur", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Retur")
                                    .Parameters.Add("@ProsRtr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "ProsRtr")
                                    .Parameters.Add("@RewardRtr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RewardRtr")
                                    .Parameters.Add("@Brand", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Brand")
                                    .Parameters.Add("@RewardBrand", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RewardBrand")
                                    .Parameters.Add("@TotReward", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotReward")
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
                Dim cmSP As New SqlCommand("SPUpT_HitTrPromo")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@TotReward", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("TotReward").SummaryText, Decimal), 2)
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
                            Dim cmSPDel As New SqlCommand("SPDelT_HitTrPromoDtl")
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

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "HitTrPromoIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CustID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_HitTrPromoDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                        .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                                        .Parameters.Add("@Omset", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Omset")
                                        .Parameters.Add("@Pembagi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Poin")
                                        .Parameters.Add("@Poin", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Poin")
                                        .Parameters.Add("@Reward", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Reward")
                                        .Parameters.Add("@Retur", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Retur")
                                        .Parameters.Add("@ProsRtr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "ProsRtr")
                                        .Parameters.Add("@RewardRtr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RewardRtr")
                                        .Parameters.Add("@Brand", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Brand")
                                        .Parameters.Add("@RewardBrand", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RewardBrand")
                                        .Parameters.Add("@TotReward", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotReward")
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

                            Else

                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CustID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_HitTrPromoDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "HitTrPromoIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                        .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                                        .Parameters.Add("@Omset", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Omset")
                                        .Parameters.Add("@Pembagi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Poin")
                                        .Parameters.Add("@Poin", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Poin")
                                        .Parameters.Add("@Reward", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Reward")
                                        .Parameters.Add("@Retur", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Retur")
                                        .Parameters.Add("@ProsRtr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "ProsRtr")
                                        .Parameters.Add("@RewardRtr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RewardRtr")
                                        .Parameters.Add("@Brand", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Brand")
                                        .Parameters.Add("@RewardBrand", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "RewardBrand")
                                        .Parameters.Add("@TotReward", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotReward")
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

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPProsesTrPromo", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = "Own"

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "T_HitTrPromoDtl")
        Try
            DsMaster.Tables("T_HitTrPromoDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitTrPromoDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HitTrPromoDtl"
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FHitTrPromo_d(Me.GridView2.GetFocusedDataRow.Item("HitTrPromoID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SLUTahun_Leave(sender As Object, e As EventArgs) Handles SLUTahun.Leave
        If Me.SLUTahun.Properties.ReadOnly = False Then
            Me.DTPAwal.Properties.MinValue = New Date(Me.SLUTahun.EditValue, 1, 1)
            Me.DTPAwal.Properties.MaxValue = New Date(Me.SLUTahun.EditValue, 12, Date.DaysInMonth(Me.SLUTahun.EditValue, 12))

            Me.DTPAkhir.Properties.MinValue = New Date(Me.SLUTahun.EditValue, 1, 1)
            Me.DTPAkhir.Properties.MaxValue = New Date(Me.SLUTahun.EditValue, 12, Date.DaysInMonth(Me.SLUTahun.EditValue, 12))

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "HitTrPromoIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub MKet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class