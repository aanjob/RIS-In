Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class FFilterHslProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Lap As String
    Dim DsLapF As New System.Data.DataSet
    Dim CekAll, CekAll2 As Boolean
    Dim Proses As String = ""
    Dim Line As String = ""
    Dim BOMID As String = ""

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
            Dim link As BaseExportLink = BandedGridView1.CreateExportLink(provider)
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

    Public Sub New(ByVal Laporan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Lap = Laporan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FFilterHslProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BExExcel.Enabled = CType(TcodeCollection.Item("LHPBExEc"), Boolean)

        Me.DTPAwal.EditValue = DateTime.Now
        Me.DTPAkhir.EditValue = DateTime.Now
        Me.DTPTanggal.EditValue = DateTime.Now

        Dim cmsl As SqlDataAdapter

        If Lap = "Per Proses" Then
            Me.XTPPros.PageVisible = True
            Me.XTPBOM.PageVisible = False

            cmsl = New SqlDataAdapter("Select Distinct Shiift From T_HslProdTKLDtl Where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_ShiiftLUE")
            Try
                DsLapF.Tables("M_ShiiftLUE").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "M_ShiiftLUE")

            Me.SLUShift.Properties.DataSource = DsLapF.Tables("M_ShiiftLUE")
            Me.SLUShift.Properties.DisplayMember = "Shiift"
            Me.SLUShift.Properties.ValueMember = "Shiift"

            cmsl = New SqlDataAdapter("Select Distinct Jam,Cast(JamAw As varchar(8)) As JamAw From M_Jam", koneksi)
            cmsl.TableMappings.Add("Table", "M_JamL")
            Try
                DsMaster.Tables("M_JamL").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_JamL")

            Me.SLUJamAw.Properties.DataSource = DsMaster.Tables("M_JamL")
            Me.SLUJamAw.Properties.DisplayMember = "Jam"
            Me.SLUJamAw.Properties.ValueMember = "Jam"

            cmsl = New SqlDataAdapter("Select Distinct Jam,Cast(JamAkh As varchar(8)) As JamAkh From M_Jam", koneksi)
            cmsl.TableMappings.Add("Table", "M_JamL2")
            Try
                DsMaster.Tables("M_JamL2").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_JamL2")

            Me.SLUJamAkh.Properties.DataSource = DsMaster.Tables("M_JamL2")
            Me.SLUJamAkh.Properties.DisplayMember = "Jam"
            Me.SLUJamAkh.Properties.ValueMember = "Jam"

            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,Proses,Line From T_HslProd Order By Proses", koneksi)
            cmsl.TableMappings.Add("Table", "M_ProsesL")
            cmsl.Fill(DsLapF, "M_ProsesL")
            DsLapF.Tables("M_ProsesL").Clear()
            cmsl.Fill(DsLapF, "M_ProsesL")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "M_ProsesL"

            If Me.CEPilihJam.EditValue = True Then
                Me.LCIJamAw.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LCIJamAkh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.ESIJam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LCIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.ESIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Else
                Me.LCIJamAw.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LCIJamAkh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.ESIJam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.LCIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.ESIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Else
            Me.XTPPros.PageVisible = False
            Me.XTPBOM.PageVisible = True

            Me.XtraTabControl2.SelectedTabPage = Me.XTPFilter

            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID where stsLunas='False' and stsBatal='False'", koneksi)

            cmsl.TableMappings.Add("Table", "BOML")
            cmsl.Fill(DsLapF, "BOML")
            DsLapF.Tables("BOML").Clear()
            cmsl.Fill(DsLapF, "BOML")

            Me.GridControl3.DataSource = DsLapF
            Me.GridControl3.DataMember = "BOML"

            'cmsl = New SqlDataAdapter("SPLRekHslProdBDef2", koneksi)
            'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            'cmsl.SelectCommand.CommandTimeout = 90000

            'cmsl.TableMappings.Add("Table", "SPLRekHslProdBOM")
            'Try
            '    DsLapF.Tables("SPLRekHslProdBOM").Clear()
            'Catch ex As Exception

            'End Try
            'cmsl.Fill(DsLapF, "SPLRekHslProdBOM")

            'Me.GridControl2.DataSource = DsLapF
            'Me.GridControl2.DataMember = "SPLRekHslProdBOM"
        End If
    End Sub


    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAll2 Then
            CekAll2 = False
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll2 = True
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Dim x, i As Integer
        x = 0
        i = 0

        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1

                If x = 1 Then
                    If Me.GridView1.GetRowCellValue(i, "Proses") = "Kirim" OrElse Me.GridView1.GetRowCellValue(i, "Proses") = "Terima" Then
                        Proses = Me.GridView1.GetRowCellValue(i, "Proses")
                        Line = Me.GridView1.GetRowCellValue(i, "Line")
                    Else
                        Proses = "'" & Me.GridView1.GetRowCellValue(i, "Proses") & Me.GridView1.GetRowCellValue(i, "Line") & "'"
                    End If

                Else
                    Proses &= ",'" & Me.GridView1.GetRowCellValue(i, "Proses") & Me.GridView1.GetRowCellValue(i, "Line") & "'"
                End If
                End If
        Next

        Dim bind As New Collection
        bind = New Collection
        bind.Add(Proses, "Proses")
        bind.Add(Line, "Line")

        If Me.CEPilihJam.EditValue = True Then
            bind.Add("Pilih Jam", "Jam")
            MainModule.PilihJamAw = DsMaster.Tables("M_JamL").Select("Jam = '" & Me.SLUJamAw.EditValue & "'")(0).Item("JamAw")
            MainModule.PilihJamAkh = DsMaster.Tables("M_JamL2").Select("Jam = '" & Me.SLUJamAkh.EditValue & "'")(0).Item("JamAkh")
        Else
            bind.Add("Tidak Pilih Jam", "Jam")
        End If

        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue
        MainModule.PilihJamKeAw = Me.SLUJamAw.EditValue
        MainModule.PilihJamKeAkh = Me.SLUJamAkh.EditValue
        MainModule.PilihShift = Me.SLUShift.EditValue

        Dim XR As New XRLHslProd
        XR.InitializeData(bind)

    End Sub

    Private Sub BPreview2_Click(sender As Object, e As EventArgs) Handles BPreview2.Click
        Me.GridView2.ActiveFilter.Clear()
        Me.XtraTabControl2.SelectedTabPage = Me.XTPPreview

        Dim x, i As Integer
        x = 0
        i = 0

        For i = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(i, "Cek") = True Then
                x += 1

                If x = 1 Then
                    BOMID = "'" & Me.GridView2.GetRowCellValue(i, "BOMID") & "'"
                Else
                    BOMID &= ",'" & Me.GridView2.GetRowCellValue(i, "BOMID") & "'"
                End If
            End If
        Next


        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLRekHslProdBOM", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Tgl", SqlDbType.VarChar).Value = Me.DTPTanggal.EditValue
        cmsl.SelectCommand.Parameters.Add("@BOMID", SqlDbType.VarChar).Value = BOMID
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLRekHslProdBOM")
        Try
            DsLapF.Tables("SPLRekHslProdBOM").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLRekHslProdBOM")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "SPLRekHslProdBOM"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap Hasil Produksi Per BOM Per Tanggal " & Format(Me.DTPAkhir.EditValue, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub CEPilihJam_EditValueChanged(sender As Object, e As EventArgs) Handles CEPilihJam.EditValueChanged
        If Me.CEPilihJam.EditValue = True Then
            Me.LCIJamAw.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.LCIJamAkh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIJam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.LCIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Else
            Me.LCIJamAw.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.LCIJamAkh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIJam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.LCIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIShift.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub DTPAwal_Leave(sender As Object, e As EventArgs) Handles DTPAwal.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct Shiift From T_HslProdTKLDtl Where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_ShiiftLUE")
        Try
            DsLapF.Tables("M_ShiiftLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_ShiiftLUE")

        Me.SLUShift.Properties.DataSource = DsLapF.Tables("M_ShiiftLUE")
        Me.SLUShift.Properties.DisplayMember = "Shiift"
        Me.SLUShift.Properties.ValueMember = "Shiift"
    End Sub

    Private Sub DTPAkhir_Leave(sender As Object, e As EventArgs) Handles DTPAkhir.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct Shiift From T_HslProdTKLDtl Where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_ShiiftLUE")
        Try
            DsLapF.Tables("M_ShiiftLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_ShiiftLUE")

        Me.SLUShift.Properties.DataSource = DsLapF.Tables("M_ShiiftLUE")
        Me.SLUShift.Properties.DisplayMember = "Shiift"
        Me.SLUShift.Properties.ValueMember = "Shiift"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmsl As SqlDataAdapter

        If Me.CKALL.Checked = True Then
            'MsgBox("Tidak")
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID", koneksi)
        Else
            'MsgBox("Semua")
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID where stsLunas='False' and stsBatal='False'", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "BOML")
        cmsl.Fill(DsLapF, "BOML")
        DsLapF.Tables("BOML").Clear()
        cmsl.Fill(DsLapF, "BOML")

        'Me.GridControl3.DataSource = DsLapF
        'Me.GridControl3.DataMember = "BOML"

        'cmsl = New SqlDataAdapter("SPLRekHslProdBDef", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.CommandTimeout = 90000

        'cmsl.TableMappings.Add("Table", "SPLRekHslProdBOM")
        'Try
        '    DsLapF.Tables("SPLRekHslProdBOM").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsLapF, "SPLRekHslProdBOM")

        'Me.GridControl2.DataSource = DsLapF
        'Me.GridControl2.DataMember = "SPLRekHslProdBOM"
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim cmsl As SqlDataAdapter

        If Me.CKALL.Checked = True Then
            'MsgBox("Tidak")
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID", koneksi)
        Else
            'MsgBox("Semua")
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID where stsLunas='False' and stsBatal='False'", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "BOML")
        cmsl.Fill(DsLapF, "BOML")
        DsLapF.Tables("BOML").Clear()
        cmsl.Fill(DsLapF, "BOML")
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub
End Class