Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Public Class FDetKomisi
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
            Dim link As BaseExportLink = GridView2.CreateExportLink(provider)
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

    Private Sub FDetKomisi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct Tahun,Bulan,Bulann + ' ' +  Cast(Tahun as Varchar(5)) As Bulann From T_HitKomisi", koneksi)
        cmsl.TableMappings.Add("Table", "PeriodLUE")
        Try
            DsMaster.Tables("PeriodLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "PeriodLUE")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("PeriodLUE")
        Me.SLUPeriodID.Properties.DisplayMember = "Bulann"
        Me.SLUPeriodID.Properties.ValueMember = "Bulann"


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

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click

        DsLapF = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLDetKomisiN2021", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = DsMaster.Tables("PeriodLUE").Select("Bulann = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Tahun")
        cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = DsMaster.Tables("PeriodLUE").Select("Bulann = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan")
        cmsl.SelectCommand.Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLDetKomisiN2021")
        Try
            DsLapF.Tables("SPLDetKomisiN2021").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLDetKomisiN2021")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLDetKomisiN2021"

        'cmsl = New SqlDataAdapter("SPLDetKomisiN70", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = DsMaster.Tables("PeriodLUE").Select("Bulann = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Tahun")
        'cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = DsMaster.Tables("PeriodLUE").Select("Bulann = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan")
        'cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        'cmsl.SelectCommand.CommandTimeout = 90000

        'cmsl.TableMappings.Add("Table", "SPLDetKomisiN70")
        'Try
        '    DsLapF.Tables("SPLDetKomisiN70").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsLapF, "SPLDetKomisiN70")

        'Me.GridControl1.DataSource = DsLapF
        'Me.GridControl1.DataMember = "SPLDetKomisiN70"


        'cmsl = New SqlDataAdapter("SPLDetKomisiOS", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = DsMaster.Tables("PeriodLUE").Select("Bulann = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Tahun")
        'cmsl.SelectCommand.Parameters.Add("@Bulan", SqlDbType.Int).Value = DsMaster.Tables("PeriodLUE").Select("Bulann = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan")
        'cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"

        'cmsl.SelectCommand.CommandTimeout = 90000

        'cmsl.TableMappings.Add("Table", "SPLDetKomisiOS")
        'Try
        '    DsLapF.Tables("SPLDetKomisiOS").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsLapF, "SPLDetKomisiOS")

        'Me.GridControl2.DataSource = DsLapF
        'Me.GridControl2.DataMember = "SPLDetKomisiOS"
      
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Laporan Detail Komisi Tahun " & Me.SLUPeriodID.Text)
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            ExportTo2(New ExportXlsProvider(Replace(fileName, ".xls", " (2).xls")))
            OpenFile(fileName)
            OpenFile2(Replace(fileName, ".xls", " (2).xls"))
        End If
    End Sub

End Class