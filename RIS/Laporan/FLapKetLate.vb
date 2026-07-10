Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FLapKetLate
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

    Private Sub FLapKetLate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE2")
        Try
            DsLapF.Tables("M_SuppLUE2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_SuppLUE2")
        DsLapF.Tables("M_SuppLUE2").Rows.Add("%", "Semua Supplier", "")

        Me.SLUSupp.Properties.DataSource = DsLapF.Tables("M_SuppLUE2")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
       Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLKetLate", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue

        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.TableMappings.Add("Table", "SPLKetLate")

        Try
            DsLapF.Tables("SPLKetLate").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsLapF, "SPLKetLate")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLKetLate"

        cmsl = New SqlDataAdapter("Select Distinct H.TrmID,D.BBID,B.Nama As Bahan,D.Sat,Sum(Qty) As Qty From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Inner Join M_BB B On D.BBID=B.BBID Where H.Tanggal>H.TglScK and SuppID Like '" & Me.SLUSupp.EditValue & "' and Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "' Group By H.TrmID,D.BBID,B.Nama,D.Sat", koneksi)

        cmsl.TableMappings.Add("Table", "SPLKetLateDtl")
        Try
            DsLapF.Tables("SPLKetLateDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLKetLateDtl")

        Dim PK1 As DataColumn = DsLapF.Tables("SPLKetLate").Columns("TrmID")
        Dim FK1 As DataColumn = DsLapF.Tables("SPLKetLateDtl").Columns("TrmID")

        Try
            DsLapF.Relations.Add("HeadDtl1", PK1, FK1)

        Catch ex As Exception

        End Try

        Me.GridControl1.DataSource = DsLapF.Tables("SPLKetLate")
        Me.GridControl1.LevelTree.Nodes.Add("HeadDtl1", GridView2)
        GridView2.ViewCaption = "Detail"
        GridView2.OptionsBehavior.Editable = False
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Keterangan Terlambat")

        GridView1.OptionsPrint.PrintDetails = True
        GridView1.OptionsPrint.ExpandAllDetails = True
        GridView1.ExportToXls(fileName)
    End Sub
End Class