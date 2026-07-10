Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FListOutsPO
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

    Private Sub FOutsPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select S.Nama As Supp,H.Tanggal,H.POID,D.BOMID,B.ArtName,B.Warna,BB.Nama As Bahan,D.Sat,SisaKirim,(Select max(Tanggal) From T_TrmBB H1 Inner Join T_TrmBBDtl D1 On H1.TrmID=D1.TrmID where H.POID=H.POID and BBID=D.BBID) As TglTrm From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_BB BB On D.BBID=BB.BBID Left Outer Join T_BOM B On D.BOMID=B.BOMID Where SisaKirim > 0 Order By S.Nama,Tanggal,POID,D.BBID", koneksi)
        cmsl.TableMappings.Add("Table", "SisaPOBB")
        cmsl.Fill(DsLapF, "SisaPOBB")
        DsLapF.Tables("SisaPOBB").Clear()
        cmsl.Fill(DsLapF, "SisaPOBB")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SisaPOBB"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "List Sisa PO Bahan Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

End Class