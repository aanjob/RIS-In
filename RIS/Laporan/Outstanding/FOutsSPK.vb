Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FOutsSPK
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim DsLapF As New System.Data.DataSet
    Dim DocID As String = ""

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
            Dim link As BaseExportLink = GridView3.CreateExportLink(provider)
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

    Private Sub FOutsSPK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = DateTime.Now
        Me.DTPAkhir.EditValue = DateTime.Now

        Me.XtraTabControl1.SelectedTabPage = Me.XTPFilter

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        Try
            DsLapF.Tables("M_CustL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_CustL")
        DsLapF.Tables("M_CustL").Rows.Add("%", "Semua Customer", "")

        Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        If Me.CEAllBOM.EditValue = True Then
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,Tanggal,C.Nama As Cust,ArtName,Warna From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Where B.CustID Like '" & Me.SLUCust.EditValue & "' and Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,BOMID,Tanggal,C.Nama As Cust,ArtName,Warna From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Where B.CustID Like '" & Me.SLUCust.EditValue & "' and Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "' and stsLunas='False'", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "OutsSPK")
        cmsl.Fill(DsLapF, "OutsSPK")
        DsLapF.Tables("OutsSPK").Clear()
        cmsl.Fill(DsLapF, "OutsSPK")
        cmsl.SelectCommand.CommandTimeout = 90000

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "OutsSPK"


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

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Me.GridView1.ActiveFilter.Clear()

        Dim x, i As Integer
        x = 0
        i = 0

        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1

                If x = 1 Then
                    DocID = "'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                Else
                    DocID &= ",'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                End If
            End If
        Next

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tanggal,BOMID,D.BSTBID,D.ArtCode,ArtName,Psg From T_BSTB H Inner Join T_BSTBDtl D On H.BSTBID=D.BSTBID Inner join M_Brg B On D.ArtCode=B.ArtCode Where BOMID In (" & DocID & ") and Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "OutsSPKDet")
        cmsl.Fill(DsLapF, "OutsSPKDet")
        DsLapF.Tables("OutsSPKDet").Clear()
        cmsl.Fill(DsLapF, "OutsSPKDet")
        cmsl.SelectCommand.CommandTimeout = 90000

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "OutsSPKDet"

        cmsl = New SqlDataAdapter("Select Cust,BOMID,ArtName,Warna,Sum(TotSPK) As TotSPK,Sum(Batal) As Batal,Sum(Trm) As TotTrm,Sum(TotSPK)-Sum(Batal)-Sum(Trm) As Sisa From(Select C.Nama As Cust,P.BOMID,P.ArtCodeInd,P.ArtCode,B.ArtName,Warna,Sum(Qty+QtyPol) As TotSPK,Sum(P.BtlOrder+P.Upp+P.Hancur+P.Hilang) As Batal,Sum(P.LunasMan)+(Select Isnull((Select Sum(Qty)*IsiDlmDos From T_BSTBDtl where BOMID=P.BOMID and ArtCode=P.ArtCodeInd),0)) As Trm From T_BOM H Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner Join M_Brg B On P.ArtCodeInd=B.ArtCode Where H.BOMID In (" & DocID & ") and Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "' Group By C.Nama,P.BOMID,P.ArtCodeInd,P.ArtCode,B.ArtName,Warna,IsiDlmDos) as x Group By Cust,BOMID,ArtName,Warna Order By Cust,ArtName,BOMID", koneksi)

        cmsl.TableMappings.Add("Table", "OutsSPKRek")
        cmsl.Fill(DsLapF, "OutsSPKRek")
        DsLapF.Tables("OutsSPKRek").Clear()
        cmsl.Fill(DsLapF, "OutsSPKRek")
        cmsl.SelectCommand.CommandTimeout = 90000

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "OutsSPKRek"

        Me.XtraTabControl1.SelectedTabPage = Me.XTPDet

    End Sub

    Private Sub BExExcelDet_Click(sender As Object, e As EventArgs) Handles BExExcelDet.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Outstanding SPK Detail Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo2(New ExportXlsProvider(fileName))
            OpenFile2(fileName)
        End If
    End Sub

    Private Sub BExExcelRek_Click(sender As Object, e As EventArgs) Handles BExExcelRek.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap Outstanding SPK Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

End Class