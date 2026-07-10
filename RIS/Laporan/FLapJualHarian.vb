Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base
Public Class FLapJualHarian
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
        Me.DTPAwal.EditValue = DateAndTime.Now
        Me.DTPAkhir.EditValue = DateAndTime.Now
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tanggal,C.Cabang,M.Nama As Merk,K.Nama As Kat, Sum(Dos) As Dos, Sum(Psg) As Psg From T_SJ H Inner Join T_SJDtl D On H.SJID=D.SJID Inner Join M_Cab C On H.CabID=C.CabID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Inner Join M_BrgKat K On B.KatID=K.KatID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "' Group By Tanggal,C.Cabang,M.Nama,K.Nama", koneksi)
        cmsl.TableMappings.Add("Table", "JualHari")
        cmsl.Fill(DsLapF, "JualHari")
        DsLapF.Tables("JualHari").Clear()
        cmsl.Fill(DsLapF, "JualHari")

        Me.PivotGridControl1.DataSource = DsLapF
        Me.PivotGridControl1.DataMember = "JualHari"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Penjualan Harian Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")

        Me.PivotGridControl1.ExportToXls(fileName)
    End Sub
End Class