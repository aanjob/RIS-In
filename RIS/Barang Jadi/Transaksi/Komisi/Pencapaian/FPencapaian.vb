Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FPencapaian
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim IdD As Integer

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=52", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PcpN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBExExcel.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTKomisi_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUSubJns.Properties.ReadOnly = True
        Me.SLUTahun.Properties.ReadOnly = True
        Me.CBOBulan.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

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
        Me.BVTKomisi_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.SLUSubJns.Properties.ReadOnly = False
        Me.SLUTahun.Properties.ReadOnly = False
        Me.CBOBulan.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTKomisi_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Tahun From M_ScmKms", koneksi)
        cmsl.TableMappings.Add("Table", "M_ScmKmsLUE")
        Try
            DsMaster.Tables("M_ScmKmsLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ScmKmsLUE")

        Me.SLUTahun.Properties.DataSource = DsMaster.Tables("M_ScmKmsLUE")
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

        Me.CBOBulan.Properties.Items.Clear()

        For i As Integer = 1 To 12
            Me.CBOBulan.Properties.Items.Add(MonthName(i))
        Next
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select KomisiIDD,KomisiID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Nama, PosisiID,stsHarga,Targett,Omset,Pencapaian From T_KomisiDtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where KomisiID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_KomisiDtl")
        Try
            DsMaster.Tables("T_KomisiDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_KomisiDtl")

        DsMaster.Tables("T_KomisiDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_KomisiDtl").Columns("KomisiID"), DsMaster.Tables("T_KomisiDtl").Columns("CabID"), DsMaster.Tables("T_KomisiDtl").Columns("Nama"), DsMaster.Tables("T_KomisiDtl").Columns("PosisiID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_KomisiDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select KomisiID,PeriodID,CodeID,Tanggal,SubJns,Tahun,Bulan,Bulann,SubJns,Ket,InsDate,InsBy,UpdDate,UpdBy From T_Komisi Where Tahun=" & MainModule.periodeTahun & "", koneksi)
        cmsl.TableMappings.Add("Table", "T_Komisi")
        Try
            DsMaster.Tables("T_Komisi").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_Komisi")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_Komisi"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Komisi")
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

    Private Sub FPencapaian_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Komisi"
    End Sub

    Private Sub FPencapaian_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPencapaian_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FPencapaian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTKomisi_e.Selected = True
    End Sub


    Private Sub BVTKomisi_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTKomisi_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Komisi"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PcpEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PcpDel"), Boolean)
        Me.BVBExExcel.Enabled = CType(TcodeCollection.Item("PcpExEc"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Komisi"

        DsMaster.Clear()

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
        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUSubJns.EditValue = ""
        Me.SLUTahun.EditValue = MainModule.periodeTahun
        Me.CBOBulan.EditValue = ""
        Me.MKet.EditValue = ""

        Me.TBInfo.EditValue = ""

        FillDtl(Me.SLUTahun.EditValue)
        DsMaster.Tables("T_KomisiDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Komisi"

        Indicator = "200"
        If MainModule.SlCekHitKms(Me.GridView2.GetFocusedDataRow.Item("Tahun"), Me.GridView2.GetFocusedDataRow.Item("Bulan")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Perhitungan Komisi Tahun " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & " Bulan " & Me.GridView2.GetFocusedDataRow.Item("Bulann") & "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        Else
            LUE()

            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("KomisiID")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.SLUSubJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("SubJns")
            Me.SLUTahun.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tahun")
            Me.CBOBulan.SelectedIndex = Me.GridView2.GetFocusedDataRow.Item("Bulan") - 1
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
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Komisi"

        koneksi.Close()

        If MainModule.SlCekHitKms(Me.GridView2.GetFocusedDataRow.Item("Tahun"), Me.GridView2.GetFocusedDataRow.Item("Bulan")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Perhitungan Komisi Tahun " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & " Bulan " & Me.GridView2.GetFocusedDataRow.Item("Bulann") & "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else

            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Komisi: " & Me.GridView2.GetFocusedDataRow.Item("KomisiID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_Komisi")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("KomisiID")
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
        End If

    End Sub
    Private Sub BVBExExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExExcel.ItemClick
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Pencapaian Tahun " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & " Bulan " & Me.GridView2.GetFocusedDataRow.Item("Bulann") & "")

        FillDtl(Me.GridView2.GetFocusedDataRow.Item("KomisiID"))

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
                Dim cmSP As New SqlCommand("SPInsT_Komisi")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.CBOBulan.SelectedIndex + 1
                    .Parameters.Add("@Bulann", SqlDbType.VarChar).Value = Me.CBOBulan.Text
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_KomisiDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                    .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                    .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Targett")
                                    .Parameters.Add("@Omset", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Omset")
                                    .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
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
                Dim cmSP As New SqlCommand("SPUpT_Komisi")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.CBOBulan.SelectedIndex + 1
                    .Parameters.Add("@Bulann", SqlDbType.VarChar).Value = Me.CBOBulan.Text
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
                            Dim cmSPDel As New SqlCommand("SPDelT_KomisiDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "KomisiIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_KomisiDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Targett")
                                        .Parameters.Add("@Omset", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Omset")
                                        .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
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
                                        Me.GridView1.SetRowCellValue(i, "KomisiIDD", Me.GridView1.GetRowCellValue(i, "KomisiIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Nama")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_KomisiDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "KomisiIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Nama")
                                        .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PosisiID")
                                        .Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "stsHarga")
                                        .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Targett")
                                        .Parameters.Add("@Omset", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Omset")
                                        .Parameters.Add("@Pencapaian", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pencapaian")
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

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        If MainModule.SlCekPencapaian(Me.SLUTahun.EditValue, Me.CBOBulan.SelectedIndex + 1, Me.SLUSubJns.EditValue) > 0 Then
            XtraMessageBox.Show("Data Pencapaian Sudah Ada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("SPProsesKomisi", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.EditValue
            cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = Me.CBOBulan.SelectedIndex + 1
            cmsl.SelectCommand.Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
            cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"

            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "Komisi")

            Try
                DsMaster.Tables("Komisi").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "Komisi")

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "Komisi"
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPencapaian_d(Me.GridView2.GetFocusedDataRow.Item("KomisiID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class