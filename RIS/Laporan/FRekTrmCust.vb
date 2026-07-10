Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRekTrmCust
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
            Dim link As BaseExportLink = GridView1.CreateExportLink(provider)
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

    Private Sub FRekTrmCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select CustID,C.Nama,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsLapF, "M_CustL")
        DsLapF.Tables("M_CustL").Clear()
        cmsl.Fill(DsLapF, "M_CustL")
        DsLapF.Tables("M_CustL").Rows.Add("%", "Semua Customer", "")

        Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim Tipe As String

        If Me.CBOTipe.EditValue = "Semua" Then
            Tipe = "%"
        Else
            Tipe = Me.CBOTipe.EditValue
        End If

        If Me.CBOLap.EditValue = "Penerimaan" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("SPLDetTrmCust", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Tipe
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "DetTrmCust")
            cmsl.Fill(DsLapF, "DetTrmCust")
            DsLapF.Tables("DetTrmCust").Clear()
            cmsl.Fill(DsLapF, "DetTrmCust")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "DetTrmCust"

            cmsl = New SqlDataAdapter("SPLRekTrmCust", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Tipe
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "RekTrmCust")
            cmsl.Fill(DsLapF, "RekTrmCust")
            DsLapF.Tables("RekTrmCust").Clear()
            cmsl.Fill(DsLapF, "RekTrmCust")

            Me.GridControl2.DataSource = DsLapF
            Me.GridControl2.DataMember = "RekTrmCust"
        Else
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("SPLDetPOCust", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@Outs", SqlDbType.Bit).Value = Me.CEOuts.EditValue
            cmsl.SelectCommand.Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Tipe
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLDetPOCust")
            cmsl.Fill(DsLapF, "SPLDetPOCust")
            DsLapF.Tables("SPLDetPOCust").Clear()
            cmsl.Fill(DsLapF, "SPLDetPOCust")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "SPLDetPOCust"

            cmsl = New SqlDataAdapter("SPLRekPOCust", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@Outs", SqlDbType.Bit).Value = Me.CEOuts.EditValue
            cmsl.SelectCommand.Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Tipe
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLRekPOCust")
            cmsl.Fill(DsLapF, "SPLRekPOCust")
            DsLapF.Tables("SPLRekPOCust").Clear()
            cmsl.Fill(DsLapF, "SPLRekPOCust")

            Me.GridControl2.DataSource = DsLapF
            Me.GridControl2.DataMember = "SPLRekPOCust"
        End If
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Detail Penerimaan Customer")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            ExportTo2(New ExportXlsProvider(Replace(fileName, "Detail Penerimaan Customer.xls", "Rekap Penerimaan Customer.xls")))

            OpenFile(fileName)
            OpenFile2(Replace(fileName, "Detail Penerimaan Customer.xls", "Rekap Penerimaan Customer.xls"))
        End If
    End Sub

    Private Sub CBOLap_Leave(sender As Object, e As EventArgs) Handles CBOLap.Leave
        If Me.CBOLap.EditValue = "PO" Then
            Me.LCIOuts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.GridColumn12.Visible = True
        Else
            Me.LCIOuts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.GridColumn12.Visible = False
        End If
    End Sub
End Class