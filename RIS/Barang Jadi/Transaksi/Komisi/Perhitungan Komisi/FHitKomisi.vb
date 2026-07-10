Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FHitKomisi
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim IdD As Integer

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=53", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HKmsN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBExExcel.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTHitKomisi_s.Enabled = True

        Me.SLUTahun.Properties.ReadOnly = True
        Me.SLUBulan.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUSubJns.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False
        Me.GridView4.OptionsBehavior.Editable = False

        Me.BProses.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBExExcel.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTHitKomisi_s.Enabled = False

        Me.SLUTahun.Properties.ReadOnly = False
        Me.SLUBulan.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUSubJns.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        'Me.GridView1.OptionsBehavior.Editable = True
        'Me.GridView3.OptionsBehavior.Editable = True
        'Me.GridView4.OptionsBehavior.Editable = True

        Me.BProses.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTHitKomisi_e.Selected = True
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HitKmsIDD,HitKmsID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Bulan, Nama,PosisiID,JenisCust,JmlBayar,Telat,Pencapaian,PersenKomisi,Komisi From T_HitKomisi70Dtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where HitKmsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitKomisi70Dtl")
        Try
            DsMaster.Tables("T_HitKomisi70Dtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisi70Dtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HitKomisi70Dtl"


        cmsl = New SqlDataAdapter("Select HitKmsIDD,HitKmsID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Bulan, Nama,PosisiID,SisaBayar,Telat,Pencapaian,PersenKomisi,Komisi From T_HitKomisi60Dtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where HitKmsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitKomisi60Dtl")
        Try
            DsMaster.Tables("T_HitKomisi60Dtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisi60Dtl")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_HitKomisi60Dtl"


        cmsl = New SqlDataAdapter("Select HitKmsIDD,HitKmsID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Nama, PosisiID,TotPsg,Telat,PersenKomisi,Komisi From T_HitKomisiOSDtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where HitKmsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitKomisiOSDtl")
        Try
            DsMaster.Tables("T_HitKomisiOSDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisiOSDtl")

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_HitKomisiOSDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HitKmsID,PeriodID,CodeID,Tanggal,Tahun,Bulan,Bulann,SubJns,TotKomisi,Ket,InsDate,InsBy,UpdDate,UpdBy From T_HitKomisi Where Tahun=" & MainModule.periodeTahun & "", koneksi)
        cmsl.TableMappings.Add("Table", "T_HitKomisi")
        Try
            DsMaster.Tables("T_HitKomisi").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisi")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_HitKomisi"
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct Tahun From T_Komisi", koneksi)
        cmsl.TableMappings.Add("Table", "M_TahunLUE")
        Try
            DsMaster.Tables("M_TahunLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_TahunLUE")

        Me.SLUTahun.Properties.DataSource = DsMaster.Tables("M_TahunLUE")
        Me.SLUTahun.Properties.DisplayMember = "Tahun"
        Me.SLUTahun.Properties.ValueMember = "Tahun"


        cmsl = New SqlDataAdapter("Select Distinct SubJns From M_Brg Where SubJns<>''", koneksi)
        cmsl.TableMappings.Add("Table", "SubJnsLUE")
        Try
            DsMaster.Tables("SubJnsLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "SubJnsLUE")
        DsMaster.Tables("SubJnsLUE").Rows.Add("%")

        Me.SLUSubJns.Properties.DataSource = DsMaster.Tables("SubJnsLUE")
        Me.SLUSubJns.Properties.DisplayMember = "SubJns"
        Me.SLUSubJns.Properties.ValueMember = "SubJns"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_HitKomisi")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.SLUTahun.EditValue
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

    Private Sub OpenFile2(ByVal fileName As String)
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

    Private Sub OpenFile3(ByVal fileName As String)
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

    Private Sub ExportTo2(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView3.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub

    Private Sub ExportTo3(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView4.CreateExportLink(provider)
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

    Private Sub FHitKomisi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Perhitungan Komisi"
    End Sub

    Private Sub FHitKomisi_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FHitKomisi_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FHitKomisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTHitKomisi_e.Selected = True
    End Sub


    Private Sub BVTHitKomisi_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTHitKomisi_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Perhitungan Komisi"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HKmsEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HKmsDel"), Boolean)
        Me.BVBExExcel.Enabled = CType(TcodeCollection.Item("HKmsExEc"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Perhitungan Komisi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Manual = False And MainModule.BackDate = False Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        Me.SLUTahun.EditValue = MainModule.periodeTahun
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct Bulan,Bulann From T_Komisi Where Tahun=" & Me.SLUTahun.EditValue & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_BulanLUE")
        Try
            DsMaster.Tables("M_BulanLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BulanLUE")

        Me.SLUBulan.Properties.DataSource = DsMaster.Tables("M_BulanLUE")
        Me.SLUBulan.Properties.DisplayMember = "Bulann"
        Me.SLUBulan.Properties.ValueMember = "Bulan"

        Me.TBKode.EditValue = ""
        Me.SLUBulan.EditValue = ""
        Me.SLUSubJns.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl("")
        DsMaster.Tables("T_HitKomisi70Dtl").Clear()
        DsMaster.Tables("T_HitKomisi60Dtl").Clear()
        DsMaster.Tables("T_HitKomisiOSDtl").Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Perhitungan Komisi"

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("HitKmsID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUTahun.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tahun")
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct Bulan,Bulann From T_Komisi Where Tahun=" & Me.SLUTahun.EditValue & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_BulanLUE")
        Try
            DsMaster.Tables("M_BulanLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BulanLUE")

        Me.SLUBulan.Properties.DataSource = DsMaster.Tables("M_BulanLUE")
        Me.SLUBulan.Properties.DisplayMember = "Bulann"
        Me.SLUBulan.Properties.ValueMember = "Bulan"

        Me.SLUSubJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("SubJns")
        Me.SLUBulan.EditValue = Me.GridView2.GetFocusedDataRow.Item("Bulan")
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

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Perhitungan Komisi"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Komisi: " & Me.GridView2.GetFocusedDataRow.Item("HitKmsID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_HitKomisi")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("HitKmsID")
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
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Perhitungan Komisi Tahun " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & " Bulan " & Me.GridView2.GetFocusedDataRow.Item("Bulann") & "")

        FillDtl(Me.GridView2.GetFocusedDataRow.Item("HitKmsID"))

        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            ExportTo2(New ExportXlsProvider(Replace(fileName, ".xls", " (2).xls")))
            ExportTo3(New ExportXlsProvider(Replace(fileName, ".xls", " (3).xls")))

            OpenFile(fileName)
            OpenFile2(Replace(fileName, ".xls", " (2).xls"))
            OpenFile3(Replace(fileName, ".xls", " (3).xls"))
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()
        Me.GridView4.ActiveFilter.Clear()

        Dim cmSPDel As New SqlCommand("SPDelT_HitKomisi70Dtl")
        cmSPDel.CommandType = CommandType.StoredProcedure


        With cmSPDel
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

        Dim cmSPDel2 As New SqlCommand("SPDelT_HitKomisi60Dtl")
        cmSPDel2.CommandType = CommandType.StoredProcedure

        With cmSPDel2
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            With koneksi
                .Open()
                cmSPDel2.ExecuteNonQuery()
                .Close()
            End With

        End With

        Dim cmSPDel3 As New SqlCommand("SPDelT_HitKomisiOSDtl")
        cmSPDel3.CommandType = CommandType.StoredProcedure

        With cmSPDel3
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            With koneksi
                .Open()
                cmSPDel3.ExecuteNonQuery()
                .Close()
            End With

        End With


        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_HitKomisi")
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
                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
                    .Parameters.Add("@Bulann", SqlDbType.VarChar).Value = Me.SLUBulan.Text
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@TotKomisi", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Komisi").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) + Math.Round(CType(Me.GridView3.Columns("Komisi").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) + Math.Round(CType(Me.GridView4.Columns("Komisi").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisi70Dtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Tahun")
                                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Bulan")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                    .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                                    .Parameters.Add("@JmlBayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JmlBayar")
                                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Telat")
                                    .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
                                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PersenKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
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

                        Dim z : For z = 0 To Me.GridView3.DataRowCount - 1
                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisi60Dtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Tahun")
                                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Bulan")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "PosisiID")
                                    .Parameters.Add("@SisaBayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "SisaBayar")
                                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Telat")
                                    .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Pencapaian")
                                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "PersenKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Komisi")
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


                        Dim w : For w = 0 To Me.GridView4.DataRowCount - 1
                            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisiOSDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CabID")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "PosisiID")
                                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "TotPsg")
                                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "Telat")
                                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "PersenKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Komisi")
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
                Dim cmSP As New SqlCommand("SPUpT_HitKomisi")
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
                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
                    .Parameters.Add("@Bulann", SqlDbType.VarChar).Value = Me.SLUBulan.Text
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@TotKomisi", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Komisi").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) + Math.Round(CType(Me.GridView3.Columns("Komisi").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) + Math.Round(CType(Me.GridView4.Columns("Komisi").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
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

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisi70Dtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Tahun")
                                    .Parameters.Add("@Bulan", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Bulan")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                    .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                                    .Parameters.Add("@JmlBayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JmlBayar")
                                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Telat")
                                    .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
                                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PersenKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
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
                        Next

                        Dim z : For z = 0 To Me.GridView3.DataRowCount - 1
                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisi60Dtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Tahun")
                                    .Parameters.Add("@Bulan", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Bulan")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "PosisiID")
                                    .Parameters.Add("@SisaBayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "SisaBayar")
                                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Telat")
                                    .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Pencapaian")
                                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "PersenKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Komisi")
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
                        Next


                        Dim w : For w = 0 To Me.GridView4.DataRowCount - 1
                            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisiOSDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CabID")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "PosisiID")
                                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "TotPsg")
                                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "Telat")
                                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "PersenKomisi")
                                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Komisi")
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

                        'Dim cmSP As New SqlCommand("SPUpT_HitKomisi")
                        'cmSP.CommandType = CommandType.StoredProcedure
                        'Dim x As Integer

                        'With cmSP
                        '    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                        '    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                        '    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                        '    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                        '    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
                        '    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
                        '    .Parameters.Add("@Bulann", SqlDbType.VarChar).Value = Me.SLUBulan.Text
                        '    .Parameters.Add("@TotKomisi", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Komisi").SummaryText, Decimal), 2) + Math.Round(CType(Me.GridView3.Columns("Komisi").SummaryText, Decimal), 2)
                        '    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                        '    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                        '    .Parameters.Add("@Return", SqlDbType.Int)
                        '    .Parameters("@Return").Direction = ParameterDirection.Output
                        '    .Connection = koneksi

                        '    Try
                        '        With koneksi
                        '            .Open()
                        '            cmSP.ExecuteNonQuery()
                        '            x = cmSP.Parameters("@Return").Value
                        '            .Close()
                        '        End With

                        '        If x = -1 Then
                        '            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '            Exit Sub
                        '        End If

                        '        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                        '            Dim cmSPDel As New SqlCommand("SPDelT_HitKomisi70Dtl")
                        '            cmSPDel.CommandType = CommandType.StoredProcedure

                        '            With cmSPDel
                        '                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                        '                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                .Parameters.Add("@Return", SqlDbType.Int)
                        '                .Parameters("@Return").Direction = ParameterDirection.Output
                        '                .Connection = koneksi

                        '                With koneksi
                        '                    .Open()
                        '                    cmSPDel.ExecuteNonQuery()
                        '                    .Close()
                        '                End With

                        '            End With
                        '        Next

                        '        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                        '            Dim cmSPDel As New SqlCommand("SPDelT_HitKomisi60Dtl")
                        '            cmSPDel.CommandType = CommandType.StoredProcedure

                        '            With cmSPDel
                        '                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(q)
                        '                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                .Parameters.Add("@Return", SqlDbType.Int)
                        '                .Parameters("@Return").Direction = ParameterDirection.Output
                        '                .Connection = koneksi

                        '                With koneksi
                        '                    .Open()
                        '                    cmSPDel.ExecuteNonQuery()
                        '                    .Close()
                        '                End With

                        '            End With
                        '        Next

                        '        Dim t : For t = 0 To arrPar2.GetUpperBound(0)
                        '            Dim cmSPDel As New SqlCommand("SPDelT_HitKomisiOSDtl")
                        '            cmSPDel.CommandType = CommandType.StoredProcedure

                        '            With cmSPDel
                        '                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(t)
                        '                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                .Parameters.Add("@Return", SqlDbType.Int)
                        '                .Parameters("@Return").Direction = ParameterDirection.Output
                        '                .Connection = koneksi

                        '                With koneksi
                        '                    .Open()
                        '                    cmSPDel.ExecuteNonQuery()
                        '                    .Close()
                        '                End With

                        '            End With
                        '        Next

                        '        Dim i : For i = 0 To GridView1.RowCount - 1
                        '            If Me.GridView1.GetRowCellValue(i, "HitKmsIDD") < 0 Then
                        '                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                        '                    Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisi70Dtl")
                        '                    cmSPDtl.CommandType = CommandType.StoredProcedure

                        '                    With cmSPDtl
                        '                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                        '                        .Parameters.Add("@Bulan", SqlDbType.VarChar).Value = Me.SLUBulan.EditValue
                        '                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                        '                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                        '                        .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                        '                        .Parameters.Add("@JmlBayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JmlBayar")
                        '                        .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Telat")
                        '                        .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
                        '                        .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PersenKomisi")
                        '                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
                        '                        .Parameters.Add("@Return", SqlDbType.Int)
                        '                        .Parameters("@Return").Direction = ParameterDirection.Output
                        '                        .Connection = koneksi
                        '                    End With

                        '                    With koneksi
                        '                        .Open()
                        '                        cmSPDtl.ExecuteNonQuery()
                        '                        x = cmSPDtl.Parameters("@Return").Value
                        '                        .Close()
                        '                    End With

                        '                    If x = 0 Then
                        '                        Me.GridView1.SetRowCellValue(i, "HitKmsIDD", Me.GridView1.GetRowCellValue(i, "HitKmsIDD") * -1)
                        '                    Else
                        '                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '                        Exit Sub
                        '                    End If

                        '                End If
                        '            Else
                        '                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                        '                    Dim cmSPDtl As New SqlCommand("SPUpT_HitKomisi70Dtl")
                        '                    cmSPDtl.CommandType = CommandType.StoredProcedure

                        '                    With cmSPDtl
                        '                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "HitKmsIDD")
                        '                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                        '                        .Parameters.Add("@Bulan", SqlDbType.VarChar).Value = Me.SLUBulan.EditValue
                        '                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                        '                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                        '                        .Parameters.Add("@JenisCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JenisCust")
                        '                        .Parameters.Add("@JmlBayar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JmlBayar")
                        '                        .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Telat")
                        '                        .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
                        '                        .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PersenKomisi")
                        '                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Komisi")
                        '                        .Parameters.Add("@Return", SqlDbType.Int)
                        '                        .Parameters("@Return").Direction = ParameterDirection.Output
                        '                        .Connection = koneksi
                        '                    End With

                        '                    With koneksi
                        '                        .Open()
                        '                        cmSPDtl.ExecuteNonQuery()
                        '                        x = cmSPDtl.Parameters("@Return").Value
                        '                        .Close()
                        '                    End With

                        '                    If x <> 0 Then
                        '                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '                        Exit Sub
                        '                    End If
                        '                End If
                        '            End If
                        '        Next

                        '        Dim z : For z = 0 To GridView3.RowCount - 1
                        '            If Me.GridView3.GetRowCellValue(z, "HitKmsIDD") < 0 Then
                        '                If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JenisCust")) Then
                        '                    Dim cmSPDtl2 As New SqlCommand("SPInsT_HitKomisi60Dtl")
                        '                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                        '                    With cmSPDtl2
                        '                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                        '                        .Parameters.Add("@Bulan", SqlDbType.VarChar).Value = Me.SLUBulan.EditValue
                        '                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Nama")
                        '                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "PosisiID")
                        '                        .Parameters.Add("@SisaBayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "SisaBayar")
                        '                        .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Telat")
                        '                        .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Pencapaian")
                        '                        .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "PersenKomisi")
                        '                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Komisi")
                        '                        .Parameters.Add("@Return", SqlDbType.Int)
                        '                        .Parameters("@Return").Direction = ParameterDirection.Output
                        '                        .Connection = koneksi
                        '                    End With

                        '                    With koneksi
                        '                        .Open()
                        '                        cmSPDtl2.ExecuteNonQuery()
                        '                        x = cmSPDtl2.Parameters("@Return").Value
                        '                        .Close()
                        '                    End With

                        '                    If x = 0 Then
                        '                        Me.GridView3.SetRowCellValue(z, "HitKmsIDD", Me.GridView3.GetRowCellValue(z, "HitKmsIDD") * -1)
                        '                    Else
                        '                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '                        Exit Sub
                        '                    End If

                        '                End If

                        '            Else

                        '                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "Nama")) Then
                        '                    Dim cmSPDtl2 As New SqlCommand("SPUpT_HitKomisi60Dtl")
                        '                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                        '                    With cmSPDtl2
                        '                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "HitKmsIDD")
                        '                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                        '                        .Parameters.Add("@Bulan", SqlDbType.VarChar).Value = Me.SLUBulan.EditValue
                        '                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Nama")
                        '                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "PosisiID")
                        '                        .Parameters.Add("@SisaBayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "SisaBayar")
                        '                        .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Telat")
                        '                        .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Pencapaian")
                        '                        .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "PersenKomisi")
                        '                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Komisi")
                        '                        .Parameters.Add("@Return", SqlDbType.Int)
                        '                        .Parameters("@Return").Direction = ParameterDirection.Output
                        '                        .Connection = koneksi
                        '                    End With

                        '                    With koneksi
                        '                        .Open()
                        '                        cmSPDtl2.ExecuteNonQuery()
                        '                        x = cmSPDtl2.Parameters("@Return").Value
                        '                        .Close()
                        '                    End With

                        '                    If x <> 0 Then
                        '                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '                        Exit Sub
                        '                    End If

                        '                End If

                        '            End If
                        '        Next


                        '        Dim w : For w = 0 To Me.GridView4.DataRowCount - 1
                        '            If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CabID")) Then
                        '                Dim cmSPDtl As New SqlCommand("SPInsT_HitKomisiOSDtl")
                        '                cmSPDtl.CommandType = CommandType.StoredProcedure

                        '                With cmSPDtl
                        '                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CabID")
                        '                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "Nama")
                        '                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "PosisiID")
                        '                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "TotPsg")
                        '                    .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "Telat")
                        '                    .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "PersenKomisi")
                        '                    .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Komisi")
                        '                    .Parameters.Add("@Return", SqlDbType.Int)
                        '                    .Parameters("@Return").Direction = ParameterDirection.Output
                        '                    .Connection = koneksi
                        '                End With

                        '                With koneksi
                        '                    .Open()
                        '                    cmSPDtl.ExecuteNonQuery()
                        '                    x = cmSPDtl.Parameters("@Return").Value
                        '                    .Close()
                        '                End With

                        '                If x = 0 Then
                        '                    Me.GridView4.SetRowCellValue(w, "HitKmsIDD", Me.GridView4.GetRowCellValue(w, "HitKmsIDD") * -1)
                        '                Else
                        '                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '                    Exit Sub
                        '                End If

                        '            Else

                        '                If Not IsDBNull(Me.GridView4.GetRowCellValue(w, "CabID")) Then
                        '                    Dim cmSPDtl2 As New SqlCommand("SPUpT_HitKomisiOSDtl")
                        '                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                        '                    With cmSPDtl2
                        '                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "HitKmsIDD")
                        '                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "CabID")
                        '                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "Nama")
                        '                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(w, "PosisiID")
                        '                        .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "TotPsg")
                        '                        .Parameters.Add("@Telat", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(w, "Telat")
                        '                        .Parameters.Add("@PersenKomisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "PersenKomisi")
                        '                        .Parameters.Add("@Komisi", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(w, "Komisi")
                        '                        .Parameters.Add("@Return", SqlDbType.Int)
                        '                        .Parameters("@Return").Direction = ParameterDirection.Output
                        '                        .Connection = koneksi
                        '                    End With

                        '                    With koneksi
                        '                        .Open()
                        '                        cmSPDtl2.ExecuteNonQuery()
                        '                        x = cmSPDtl2.Parameters("@Return").Value
                        '                        .Close()
                        '                    End With

                        '                    If x <> 0 Then
                        '                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '                        Exit Sub
                        '                    End If

                        '                End If

                        '            End If
                        '        Next


                        '        If x = 0 Then
                        '            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '        ElseIf x = 1 Then
                        '            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        '            Exit Sub
                        '        Else
                        '            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '            Exit Sub
                        '        End If

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
        cmsl = New SqlDataAdapter("SPProsesKmsN2021", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
        cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
        cmsl.SelectCommand.Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "T_HitKomisi70Dtl")
        Try
            DsMaster.Tables("T_HitKomisi70Dtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisi70Dtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HitKomisi70Dtl"

        'Dim cmsl As SqlDataAdapter
        'cmsl = New SqlDataAdapter("SPProsesKmsN70", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
        'cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
        'cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        'cmsl.SelectCommand.CommandTimeout = 90000

        'cmsl.TableMappings.Add("Table", "T_HitKomisi70Dtl")
        'Try
        '    DsMaster.Tables("T_HitKomisi70Dtl").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsMaster, "T_HitKomisi70Dtl")

        'Me.GridControl1.DataSource = DsMaster
        'Me.GridControl1.DataMember = "T_HitKomisi70Dtl"

        'cmsl = New SqlDataAdapter("SPProsesKmsN60", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
        'cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
        'cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        'cmsl.SelectCommand.CommandTimeout = 90000

        'cmsl.TableMappings.Add("Table", "T_HitKomisi60Dtl")
        'Try
        '    DsMaster.Tables("T_HitKomisi60Dtl").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsMaster, "T_HitKomisi60Dtl")

        'Me.GridControl3.DataSource = DsMaster
        'Me.GridControl3.DataMember = "T_HitKomisi60Dtl"

        'cmsl = New SqlDataAdapter("SPProsesKmsOS", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
        'cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.SLUBulan.EditValue
        'cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        'cmsl.SelectCommand.CommandTimeout = 90000

        'cmsl.TableMappings.Add("Table", "T_HitKomisiOSDtl")
        'Try
        '    DsMaster.Tables("T_HitKomisiOSDtl").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsMaster, "T_HitKomisiOSDtl")

        'Me.GridControl4.DataSource = DsMaster
        'Me.GridControl4.DataMember = "T_HitKomisiOSDtl"
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FHitKomisi_d(Me.GridView2.GetFocusedDataRow.Item("HitKmsID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub SLUTahun_Leave(sender As Object, e As EventArgs) Handles SLUTahun.Leave
        If Me.SLUTahun.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select Distinct Bulan,Bulann From T_Komisi Where Tahun=" & Me.SLUTahun.EditValue & "", koneksi)
            cmsl.TableMappings.Add("Table", "M_BulanLUE")
            Try
                DsMaster.Tables("M_BulanLUE").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_BulanLUE")

            Me.SLUBulan.Properties.DataSource = DsMaster.Tables("M_BulanLUE")
            Me.SLUBulan.Properties.DisplayMember = "Bulann"
            Me.SLUBulan.Properties.ValueMember = "Bulan"
        End If
    End Sub


    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class