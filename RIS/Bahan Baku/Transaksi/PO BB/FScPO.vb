Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class FScPO
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select H.POID,H.Tanggal,S.Nama As Supp,POIDD,BOMID,D.BBID,B.Nama As Bahan,D.Sat,Qty,SisaKirim,Free,HarSat, Ready,ETD,ETA From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_BB B On D.BBID=B.BBID Where D.stsKirim=0", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBBDtl")
        Try
            DsMaster.Tables("T_POBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBBDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBBDtl"
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

    Private Sub FScPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Input Jadwal Kedatangan"
        Me.BVTJadwalPO.Selected = True

        Me.BVTJadwalPO.Enabled = CType(TcodeCollection.Item("JadKSet"), Boolean)
        Me.BSave.Enabled = CType(TcodeCollection.Item("JadKSave"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("JadKP"), Boolean)
        Me.BVTPrintK.Enabled = CType(TcodeCollection.Item("JadKP"), Boolean)

        FillDt()
    End Sub

    Private Sub BVTPrintK_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPrintK.ItemPressed
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select H.POID,H.Tanggal,S.Nama As Supp,H.CustID,C.Nama As Cust,BOMID,B.Nama As Bahan,D.Sat,Qty,SisaKirim,Ready, ETD,ETA From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_BB B On D.BBID=B.BBID Where H.stsKirim=0 Order By Ready,H.POID,B.Nama Asc", koneksi)

        cmsl.TableMappings.Add("Table", "JadwalK")
        Try
            DsMaster.Tables("JadwalK").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "JadwalK")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "JadwalK"
    End Sub
    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim XR As New XRScPOCust
        XR.InitializeData()
    End Sub


    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Me.GridView1.ActiveFilter.Clear()


        Dim x As Integer

        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Ready")) Then
                Dim cmSPDtl As New SqlCommand("SPUpScT_POBBDtl")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POID")
                    .Parameters.Add("@Ready", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Ready")
                    .Parameters.Add("@ETD", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETD")
                    .Parameters.Add("@ETA", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETA")
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With

                With koneksi
                    .Open()
                    cmSPDtl.ExecuteNonQuery()
                    x = cmSPDtl.Parameters("@Return").Value
                    .Close()
                End With
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf x = 1 Then
            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Ready") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "ETD", e.Value)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ETA", e.Value)
        ElseIf e.Column Is GridView1.Columns("ETD") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "ETA", e.Value)
        End If
    End Sub

    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If System.DateTime.Now > Me.GridView2.GetRowCellValue(e.RowHandle, "ETA") Then
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

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Jadwal Kedatangan Bahan Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

End Class