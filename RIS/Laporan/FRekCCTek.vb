Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base

Public Class FRekCCTek
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

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim DsLap As New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLRekCCTek", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Top", SqlDbType.Int).Value = Me.SPTop.EditValue

        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.TableMappings.Add("Table", "RekCCTek")

        Try
            DsLap.Tables("RekCCTek").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsLap, "RekCCTek")

        cmsl = New SqlDataAdapter("Select U.Nama As Teknisi,Tanggal,MesinID,KetJns,Stat,CC.Ket From T_CCTeknisi CC Inner Join M_User U On CC.UserID=U.UserID Inner Join M_BB B On CC.MesinID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "' and MesinID In (Select MesinID From (Select Top " & Me.SPTop.EditValue & "  MesinID,Count(*) As Jml From T_CCTeknisi CC Inner Join M_BB B On CC.MesinID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "' Group By MesinID Order By Jml desc,MesinID asc)as x) Order By Tanggal", koneksi)

        cmsl.TableMappings.Add("Table", "RekCCTekDtl")
        Try
            DsLap.Tables("RekCCTekDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLap, "RekCCTekDtl")

        Dim PK1 As DataColumn = DsLap.Tables("RekCCTek").Columns("MesinID")
        Dim FK1 As DataColumn = DsLap.Tables("RekCCTekDtl").Columns("MesinID")

        'Dim PK2 As DataColumn = DsLap.Tables("SPLSchProdDtl").Columns("BOMID")
        'Dim FK2 As DataColumn = DsLap.Tables("T_SchPrSint").Columns("BOMID")

        DsLap.Relations.Add("HeadDtl1", PK1, FK1)
        'DsLap.Relations.Add("SchProdSint", PK2, FK2)

        Me.GridControl1.DataSource = DsLap.Tables("RekCCTek")
        Me.GridControl1.LevelTree.Nodes.Add("HeadDtl1", GridView2)
        Me.GridView2.ViewCaption = "Detail"
        Me.GridView2.OptionsBehavior.Editable = False
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Rekap Check Clock Teknisi Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")

        Me.GridView1.OptionsPrint.PrintDetails = True
        Me.GridView1.OptionsPrint.ExpandAllDetails = True
        Me.GridView1.ExportToXls(fileName)

    End Sub

End Class