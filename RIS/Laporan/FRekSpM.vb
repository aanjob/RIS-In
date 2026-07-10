Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Public Class FRekSpM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsLapF As New System.Data.DataSet

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

    Private Sub FRekSpM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select BBID,B.Nama From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where J.JnsPers ='Mesin' Order By B.Nama", koneksi)
        cmsl.TableMappings.Add("Table", "MMesinL")
        cmsl.Fill(DsLapF, "MMesinL")
        DsLapF.Tables("MMesinL").Clear()
        cmsl.Fill(DsLapF, "MMesinL")
        DsLapF.Tables("MMesinL").Rows.Add("%", "Semua Bahan/Mesin")

        Me.SLUMesin.Properties.DataSource = DsLapF.Tables("MMesinL")
        Me.SLUMesin.Properties.DisplayMember = "Nama"
        Me.SLUMesin.Properties.ValueMember = "BBID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim Tipe As String

        If Me.CBOTipe.EditValue = "Semua" Then
            Tipe = "%"
        Else
            Tipe = Me.CBOTipe.EditValue
        End If

        If Me.CBOLap.EditValue = "Pemakaian" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("SPLRekPakaiSpM", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.SLUMesin.EditValue
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "RekPakaiSpM")
            cmsl.Fill(DsLapF, "RekPakaiSpM")
            DsLapF.Tables("RekPakaiSpM").Clear()
            cmsl.Fill(DsLapF, "RekPakaiSpM")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "RekPakaiSpM"

        ElseIf Me.CBOLap.EditValue = "Pembelian" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("SPLRekPOSpM", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.SLUMesin.EditValue
            cmsl.SelectCommand.Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Tipe
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLRekPOSpM")
            cmsl.Fill(DsLapF, "SPLRekPOSpM")
            DsLapF.Tables("SPLRekPOSpM").Clear()
            cmsl.Fill(DsLapF, "SPLRekPOSpM")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "SPLRekPOSpM"

        ElseIf Me.CBOLap.EditValue = "Penerimaan" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("SPLRekTrmSpM", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.SLUMesin.EditValue
            cmsl.SelectCommand.Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Tipe
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLRekTrmSpM")
            cmsl.Fill(DsLapF, "SPLRekTrmSpM")
            DsLapF.Tables("SPLRekTrmSpM").Clear()
            cmsl.Fill(DsLapF, "SPLRekTrmSpM")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "SPLRekTrmSpM"
        End If

    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap " & Me.CBOLap.EditValue & " Sparepart-Mesin")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

End Class