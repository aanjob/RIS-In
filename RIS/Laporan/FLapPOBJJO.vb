Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FLapPOBJJO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
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

    Private Sub FLapPOBJJO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SalID,Nama From M_Sales Where Gol='Job Order'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SalesL")
        Try
            DsLapF.Tables("M_SalesL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_SalesL")

        DsLapF.Tables("M_SalesL").Rows.Add("%", "Semua Sales")

        Me.SLUSales.Properties.DataSource = DsLapF.Tables("M_SalesL")
        Me.SLUSales.Properties.DisplayMember = "Nama"
        Me.SLUSales.Properties.ValueMember = "SalID"


        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True'", koneksi)
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
        Dim DsLap As New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLRekPOBJ", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
        cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue

        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.TableMappings.Add("Table", "SPLRekPOBJ")

        Try
            DsLap.Tables("SPLRekPOBJ").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsLap, "SPLRekPOBJ")

        'Me.GridControl1.DataSource = DsLapF
        'Me.GridControl1.DataMember = "SPLRekPOBJ"

        cmsl = New SqlDataAdapter("Select JualID,SJID,DueDate,TotPsg,TotAkhir From T_JualBJ where SJID In (Select POID From T_POBJJO where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal <='" & Me.DTPAkhir.EditValue & "' and SalID Like '" & Me.SLUSales.EditValue & "' and CustID Like '" & Me.SLUCust.EditValue & "') Union All Select '' As JualID,POID As SJID,null As DueDate,null As TotPsg,null As TotAkhir From T_POBJJO where Tanggal>='" & Me.DTPAwal.EditValue & "' and Tanggal <='" & Me.DTPAkhir.EditValue & "' and SalID Like '" & Me.SLUSales.EditValue & "' and CustID Like '" & Me.SLUCust.EditValue & "' and POID Not In (Select SJID From T_JualBJ)", koneksi)

        cmsl.TableMappings.Add("Table", "DocDtl")

        Try
            DsLap.Tables("DocDtl").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsLap, "DocDtl")

        Dim PK1 As DataColumn = DsLap.Tables("SPLRekPOBJ").Columns("POID")
        Dim FK1 As DataColumn = DsLap.Tables("DocDtl").Columns("SJID")

        DsLap.Relations.Add("Detail Penjualan", PK1, FK1)

        Me.GridControl1.DataSource = DsLap.Tables("SPLRekPOBJ")
        Me.GridControl1.LevelTree.Nodes.Add("Detail Penjualan", GridView2)
        Me.GridView2.ViewCaption = "Detail Penjualan"
        Me.GridView2.OptionsBehavior.Editable = False

    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "PO Job Order Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")

        Me.GridView1.OptionsPrint.PrintDetails = True
        Me.GridView1.OptionsPrint.ExpandAllDetails = True
        Me.GridView1.ExportToXls(fileName)
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
                'End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Dim x, i As Integer
        Dim PO As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("SPLRekPOBJ").Rows.Count - 1
            If DsLapF.Tables("SPLRekPOBJ").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    PO = "'" & DsLapF.Tables("SPLRekPOBJ").Rows(i).Item("POID") & "'"
                Else
                    PO &= ",'" & DsLapF.Tables("SPLRekPOBJ").Rows(i).Item("POID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(PO, "PO")

        Dim XR As New XRRekPOBJJO
        XR.InitializeData(bind)
    End Sub
End Class