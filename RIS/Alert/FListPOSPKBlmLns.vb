Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FListPOSPKBlmLns
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
    'Dim DsLapF As New System.Data.DataSet

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

    Private Sub FListPOSPKBlmLns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CekAlert = False Then
            DsLap = New System.Data.DataSet

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select 'SPK' as Doc,c.Nama as CustID,(Select Isnull((Select Top 1 B.BOMID From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=D.POID and BP.ArtCodeInd =D.ArtCode),'')) As BOMID,H.POID,H.POCust,H.Tanggal,(Select Top 1 B.TglKirim From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=D.POID and BP.ArtCodeInd =D.ArtCode) as TglKirim, B.Artcode,B.ArtName, W.Nama as Warna,B.Ass as Size,D.Psg as Qty, (select max(Tanggal) from T_TrmBJ where TrmID in (Select TrmID From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode) ) as TglTrm, (Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0)) as Hari, D.Psg+D.PsgPol-D.BtlOrder-D.BtlProd-D.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0)) As Sisa,'' as Grup From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On W.WrnID=B.WrnID left join M_Cust c on h.Custid=c.CustID Where D.Psg-D.BtlOrder-D.BtlProd-D.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0))>0 and H.POID NOT IN (SELECT POID FROM T_BOM WHERE stsLunas='1')", koneksi)
            cmsl.SelectCommand.CommandTimeout = 90000
            cmsl.TableMappings.Add("Table", "BOMBlmLns")

            Try
                DsLap.Tables("BOMBlmLns").Clear()
            Catch ex As Exception

            End Try

            cmsl.Fill(DsLap, "BOMBlmLns")
        End If


        Me.GridControl1.DataSource = DsLap
        Me.GridControl1.DataMember = "BOMBlmLns"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "List BOM Belum Lunas Per Tanggal " & Format(DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Hari") > 14 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                Else
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class