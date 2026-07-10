Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Public Class FRekByrPiutCust
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan
        Me.LBGol.Text = "    " & Golongan

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

    Private Sub FRekDetPiutCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UC.CabID,Case When UC.CAbID='%' Then 'Semua Cabang' Else C.Cabang End As Cabang From M_UsCab UC Left Outer Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsLapF.Tables("M_UsCabLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_UsCabLUE")

        Me.SLUCab.Properties.DataSource = DsLapF.Tables("M_UsCabLUE")
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"
    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Not IsDBNull(Me.SLUCab.EditValue) Then
            Try
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustL")
                cmsl.Fill(DsLapF, "M_CustL")
                DsLapF.Tables("M_CustL").Clear()
                cmsl.Fill(DsLapF, "M_CustL")
                DsLapF.Tables("M_CustL").Rows.Add("%", "Semua Customer", "")

                Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustL")
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLRekByrCust", koneksi)

        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
        cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
        cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue

        cmsl.SelectCommand.CommandTimeout = 9000
        cmsl.TableMappings.Add("Table", "RekDetBayar" & Gol)
        Try
            DsLapF.Tables("RekDetBayar" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "RekDetBayar" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "RekDetBayar" & Gol
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap Pembayaran")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class