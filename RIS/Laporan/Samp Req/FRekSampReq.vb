Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRekSampReq
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsLapF As New System.Data.DataSet

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

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
    Private Sub FRekSampReq_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CustID,Nama As Cust From M_Cust Where Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustSp")
        DsLapF = New System.Data.DataSet
        cmsl.Fill(DsLapF, "M_CustSp")
        DsLapF.Tables("M_CustSp").Rows.Add("%", "Semua Customer")

        Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustSp")
        Me.SLUCust.Properties.DisplayMember = "Cust"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select Cast(UserID as Varchar(6)) As UserID,Nama From M_User Where PosisiID Like '%Develop%' and PosisiID<>'Developer'", koneksi)
        cmsl.TableMappings.Add("Table", "ChaserLUE")
        DsLapF = New System.Data.DataSet
        cmsl.Fill(DsLapF, "ChaserLUE")
        DsLapF.Tables("ChaserLUE").Rows.Add("%", "Semua Chaser")

        Me.SLUChaser.Properties.DataSource = DsLapF.Tables("ChaserLUE")
        Me.SLUChaser.Properties.DisplayMember = "Nama"
        Me.SLUChaser.Properties.ValueMember = "UserID"
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click

        Dim bind As New Collection
        bind.Add(Me.SLUCust.EditValue, "CustID")
        bind.Add(Me.SLUChaser.EditValue, "ChaserID")
      
        Dim XR As New XRCLSR
        XR.InitializeData(bind)

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("SPLRekSR", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
        cmsl.SelectCommand.Parameters.Add("@ChaserID", SqlDbType.VarChar).Value = Me.SLUChaser.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLRekSR")
        Try
            DsLapF.Tables("SPLRekSR").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLRekSR")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLRekSR"


        cmsl = New SqlDataAdapter("SPLDetSR", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
        cmsl.SelectCommand.Parameters.Add("@ChaserID", SqlDbType.VarChar).Value = Me.SLUChaser.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLDetSR")
        Try
            DsLapF.Tables("SPLDetSR").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLDetSR")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "SPLDetSR"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap Sample Request")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            ExportTo2(New ExportXlsProvider(Replace(fileName, ".xls", " (2).xls")))
            OpenFile(fileName)
            OpenFile2(Replace(fileName, ".xls", " (2).xls"))

        End If
    End Sub
End Class