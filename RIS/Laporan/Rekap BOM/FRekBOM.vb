Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRekBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim BOMID As String
    Dim BBID As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(BOM As String, BB As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        BOMID = BOM
        BBID = BB

        ' Add any initialization after the InitializeComponent() call.
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

    Private Sub FRekBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select B.DivPO,H.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotOrder,D.BBID,B.Nama,D.UkBB,D.Sat,Sum(Keb) As Keb, D.Ket From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.BOMID In (" & BOMID & ") and D.BBID In (" & BBID & ") Group By B.DivPO,H.BOMID,ArtName,Warna,TotPsg,TotPsgPol,D.BBID, B.Nama,D.UkBB,D.Sat,D.Ket Order By B.Nama", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOMDtl")
        Try
            DsLapF.Tables("T_BOMDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "T_BOMDtl")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "T_BOMDtl"

    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap BOM")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class