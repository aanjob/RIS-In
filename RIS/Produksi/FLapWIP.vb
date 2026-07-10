Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FLapWIP
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

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,DATENAME(month,TglAwal) + ' ' + Cast(Tahun As Varchar(20)) as Bulan From M_Period Order By TglAwal Asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_PeriodL2")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_PeriodL2")
        DsMaster.Tables("M_PeriodL2").Clear()
        cmsl.Fill(DsMaster, "M_PeriodL2")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("M_PeriodL2")
        Me.SLUPeriodID.Properties.DisplayMember = "Bulan"
        Me.SLUPeriodID.Properties.ValueMember = "PeriodID"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOmsetBln_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SLUPeriodID.EditValue = periodAktif
    End Sub


    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLWIP", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@PeriodID", SqlDbType.Int).Value = Me.SLUPeriodID.EditValue

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLWIP")
        Try
            DsLapF.Tables("SPLWIP").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLWIP")
        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLWIP"

    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Laporan WIP " & DsMaster.Tables("M_PeriodL2").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan") & "")

        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class