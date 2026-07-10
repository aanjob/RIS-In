Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FBOMvsLPBvsBPB5
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsLapF As New System.Data.DataSet
    Dim CekAll, CekAll2 As Boolean

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

    Private Sub FBOMvsLPBvsBPB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.XtraTabControl1.SelectedTabPage = Me.XTPFilter

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,ArtName From T_BOM Order By ArtName", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOML2")
        cmsl.Fill(DsLapF, "T_BOML2")
        DsLapF.Tables("T_BOML2").Clear()
        cmsl.Fill(DsLapF, "T_BOML2")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "T_BOML2"

    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAll2 Then
            CekAll2 = False
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll2 = True
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BKlik_Click(sender As Object, e As EventArgs) Handles BKlik.Click
        Dim x, i As Integer
        Dim ArtName As String = ""
        For i = 0 To DsLapF.Tables("T_BOML2").Rows.Count - 1
            If DsLapF.Tables("T_BOML2").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    ArtName = "'" & DsLapF.Tables("T_BOML2").Rows(i).Item("ArtName") & "'"
                Else
                    ArtName &= ",'" & DsLapF.Tables("T_BOML2").Rows(i).Item("ArtName") & "'"
                End If
            End If
        Next

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,SPK,ArtName,Warna From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where ArtName In (" & ArtName & ")", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOML3")
        cmsl.Fill(DsLapF, "T_BOML3")
        DsLapF.Tables("T_BOML3").Clear()
        cmsl.Fill(DsLapF, "T_BOML3")

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "T_BOML3"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Me.XtraTabControl1.SelectedTabPage = Me.XTPPreview

        Dim x, i As Integer
        Dim BOMID As String = ""
        For i = 0 To DsLapF.Tables("T_BOML3").Rows.Count - 1
            If DsLapF.Tables("T_BOML3").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    BOMID = "'" & DsLapF.Tables("T_BOML3").Rows(i).Item("BOMID") & "'"
                Else
                    BOMID &= ",'" & DsLapF.Tables("T_BOML3").Rows(i).Item("BOMID") & "'"
                End If
            End If
        Next

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLBOMvsLPBvsBPB5", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@BOM", SqlDbType.VarChar).Value = BOMID
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLBOMvsLPBvsBPB")
        Try
            DsLapF.Tables("SPLBOMvsLPBvsBPB").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLBOMvsLPBvsBPB")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLBOMvsLPBvsBPB"
    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "BOM Vs LPB Vs BPB")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

   
End Class