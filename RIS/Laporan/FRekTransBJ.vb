Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRekTransBJ
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

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = Golongan
        Me.ESIGol.Text = "    " & Golongan

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FRekTransBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SatID,Isi From M_BrgSat", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSatL" & Gol)
        Try
            DsLapF.Tables("M_BrgSatL" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BrgSatL" & Gol)

        Me.SLUSatuan.Properties.DataSource = DsLapF.Tables("M_BrgSatL" & Gol)
        Me.SLUSatuan.Properties.DisplayMember = "SatID"
        Me.SLUSatuan.Properties.ValueMember = "SatID"

        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsLapF.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsLapF.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        DsLapF.Tables("M_BrgSatL" & Gol).Rows.Add("%", 0)
        DsLapF.Tables("M_BrgSatL" & Gol).Rows.Add("D%", 0)
    End Sub

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

        If SlAllCab(MainModule.UserAktif) > 0 Then
            DsMaster.Tables("M_GudangLUE" & Gol).Rows.Add("%", "Semua Gudang")
        End If

        Me.SLUGudang.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
        Me.SLUGudang.Properties.DisplayMember = "Nama"
        Me.SLUGudang.Properties.ValueMember = "GdID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLRekTransBJ", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = Me.SLUGudang.EditValue
        cmsl.SelectCommand.Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSatuan.EditValue
        cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "RekTransBJ" & Gol)
        Try
            DsLapF.Tables("RekTransBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "RekTransBJ" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "RekTransBJ" & Gol

        Dim command As New SqlCommand("Select dbo.fcTotSAwBJPs('" & Me.SLUGudang.EditValue & "','" & Gol & "','" & Me.SLUGrup.EditValue & "','" & Me.SLUSatuan.EditValue & "','" & Me.DTPAwal.EditValue & "') ", koneksi)

        With koneksi
            .Open()
            Me.TBStokAwal.EditValue = command.ExecuteScalar()
            .Close()
        End With

        Me.TBStokAkhir.EditValue = Me.TBStokAwal.EditValue + CType((Me.GridView1.Columns("Psg").SummaryText), Decimal)

    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Rekap Transaksi Barang Jadi Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class