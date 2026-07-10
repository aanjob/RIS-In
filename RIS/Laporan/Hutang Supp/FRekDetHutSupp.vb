Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRekDetHutSupp
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

    Private Sub FArusHutSupp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SuppID,Nama From M_Supp", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE")
        Try
            DsLapF.Tables("M_SuppLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_SuppLUE")

        DsLapF.Tables("M_SuppLUE").Rows.Add("%", "Semua Supplier")

        Me.SLUSupp.Properties.DataSource = DsLapF.Tables("M_SuppLUE")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLRekDetByrHut", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLRekDetByrHut")
        Try
            DsLapF.Tables("SPLRekDetByrHut").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLRekDetByrHut")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLRekDetByrHut"


        cmsl = New SqlDataAdapter("Select TH.TagihID,TrmID From T_Tagihan TH Inner Join T_TagihanDtl TD On TH.TagihID=TD.TagihID Where SuppID Like '" & Me.SLUSupp.EditValue & "' and TH.Tanggal >='" & Me.DTPAwal.EditValue & "' and TH.Tanggal <='" & Me.DTPAkhir.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_Tagihan")
        Try
            DsLapF.Tables("T_Tagihan").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "T_Tagihan")

        DsMaster.Tables("T_Tagihan").PrimaryKey = New DataColumn() {DsMaster.Tables("T_Tagihan").Columns("TagihID"), DsMaster.Tables("T_Tagihan").Columns("TrmID")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_Tagihan"

        If Me.GridView1.RowCount > 0 Then
            If Me.GridView2.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[TagihID] = '" & Me.GridView1.GetFocusedRowCellValue("TagihID") & "'"
            End If
        End If
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap Detail Pembayaran Hutang Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[TagihID] = '" & Me.GridView1.GetFocusedRowCellValue("TagihID") & "'"
        End If
    End Sub
End Class