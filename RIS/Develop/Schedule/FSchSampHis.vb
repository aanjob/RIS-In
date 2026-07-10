Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FSchSampHis
    Dim koneksi As New SqlConnection(GlobalKoneksi)

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

    Private Sub FSchSampHis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct SR.ReqID,SR.Tanggal,SR.TglKirim,SR.CustID,C.Nama As Cust,SR.StyleID +' / '+ SR.StlName As Style, SampType,Warna,Uk,Qty,(SELECT convert(varchar,TglPattern, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglPattern,(SELECT convert(varchar,TglSpec, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglSpec,(SELECT convert(varchar,TglTool, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglTool,(SELECT convert(varchar,TglBhn, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglBhn,(SELECT convert(varchar,TglJht, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglJht,(SELECT convert(varchar,TglAss, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglAss,(SELECT convert(varchar,TglFinProd, 106)+', ' FROM T_SchPrSamp Where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD FOR XML PATH('')) As TglFinProd,SS.Ket From T_SchPrSamp SS Inner Join T_SampReq SR On SS.ReqID=SR.ReqID Inner Join M_Cust C On SS.CustID=C.CustID Where stsKirim='False' Order By ReqID,Style,Warna", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchPrSampHis")
        Try
            DsMaster.Tables("T_SchPrSampHis").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrSampHis")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPrSampHis"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Sample Production Schedule (History)")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class