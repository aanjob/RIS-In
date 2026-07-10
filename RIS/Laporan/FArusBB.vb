Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FArusBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
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

    Private Sub FArusBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol In ('Bahan','Sparepart-Mesin')", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsLapF, "M_GudangLUE")
        DsLapF.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsLapF, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsLapF.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        If MainModule.NoHarga = True Then
            Me.BandedGridView1.OptionsMenu.EnableColumnMenu = False
            Me.GridColumn2.Width = 500

            Me.GridColumn5.Visible = False
            Me.GridColumn7.Visible = False
            Me.GridColumn9.Visible = False
            Me.GridColumn11.Visible = False
            Me.BandedGridColumn2.Visible = False
            Me.BandedGridColumn4.Visible = False
        Else
            Me.BandedGridView1.OptionsMenu.EnableColumnMenu = True
            Me.GridColumn2.Width = 250

            Me.GridColumn5.Visible = True
            Me.GridColumn7.Visible = True
            Me.GridColumn9.Visible = True
            Me.GridColumn11.Visible = True
            Me.BandedGridColumn2.Visible = True
            Me.BandedGridColumn4.Visible = True
        End If

    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLArusBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "ArusBB")
        Try
            DsLapF.Tables("ArusBB").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "ArusBB")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "ArusBB"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Arus Bahan Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class