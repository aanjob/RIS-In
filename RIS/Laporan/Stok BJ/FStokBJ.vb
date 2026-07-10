Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FStokBJ
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

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = Golongan
        Me.ESIGol.Text = "    " & Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FStokBJH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainModule.Posisi Like "*Cabang" Then
            Me.GridColumn5.Visible = False
        Else
            Me.GridColumn5.Visible = True
        End If

        Me.DTPTanggal.EditValue = System.DateTime.Now

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsLapF.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsLapF.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        If Gol = "Gabungan" Then
            cmsl = New SqlDataAdapter("Select Distinct SubGrup From M_Brg where Gol In ('Character','Own')", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select Distinct SubGrup From M_Brg where Gol='" & Gol & "'", koneksi)
        End If


        cmsl.TableMappings.Add("Table", "SubGrupLUE")
        Try
            DsLapF.Tables("SubGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SubGrupLUE")
        DsLapF.Tables("SubGrupLUE").Rows.Add("%")

        Me.SLUSubGrup.Properties.DataSource = DsLapF.Tables("SubGrupLUE")
        Me.SLUSubGrup.Properties.DisplayMember = "SubGrup"
        Me.SLUSubGrup.Properties.ValueMember = "SubGrup"

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
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_CabGd CG Inner Join M_Gudang G On CG.GdID=G.GdID Where Grup='" & Me.SLUGrup.EditValue & "' and CabID = '" & Me.SLUCab.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsLapF.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_GudangLUE" & Gol)

        If SlAllCab(MainModule.UserAktif) > 0 Then
            DsLapF.Tables("M_GudangLUE" & Gol).Rows.Add("%", "Semua Gudang")
        End If

        Me.SLUGd.Properties.DataSource = DsLapF.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"
    End Sub

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsLapF.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_GudangLUE" & Gol)

        If SlAllCab(MainModule.UserAktif) > 0 Then
            DsLapF.Tables("M_GudangLUE" & Gol).Rows.Add("%", "Semua Gudang")
        End If

        Me.SLUGd.Properties.DataSource = DsLapF.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

    End Sub


    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        DsLapF = New System.Data.DataSet

        'MsgBox(Me.DTPTanggal.EditValue & " " & Me.SLUGrup.EditValue & " " & Me.SLUSubGrup.EditValue & " " & Gol)

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLStokHargaBJ", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditorTypeName
        cmsl.SelectCommand.Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
        cmsl.SelectCommand.Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = "%"
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "StokHargaBJ" & Gol)
        Try
            DsLapF.Tables("StokHargaBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "StokHargaBJ" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "StokHargaBJ" & Gol
    End Sub

    'Private Sub BProses2_Click(sender As Object, e As EventArgs) Handles BProses.Click
    '    DsLapF = New System.Data.DataSet

    '    Dim cmsl As SqlDataAdapter
    '    cmsl = New SqlDataAdapter("SPLStokHargaBJ2", koneksi)
    '    cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
    '    cmsl.SelectCommand.Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditorTypeName
    '    cmsl.SelectCommand.Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = "%"
    '    cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
    '    cmsl.SelectCommand.CommandTimeout = 90000

    '    cmsl.TableMappings.Add("Table", "StokHargaBJ" & Gol)
    '    Try
    '        DsLapF.Tables("StokHargaBJ" & Gol).Clear()
    '    Catch ex As Exception

    '    End Try
    '    cmsl.Fill(DsLapF, "StokHargaBJ" & Gol)

    '    Me.GridControl1.DataSource = DsLapF
    '    Me.GridControl1.DataMember = "StokHargaBJ" & Gol
    'End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Stok Barang Jadi " & Gol & " Per Tanggal " & Format(Me.DTPTanggal.EditValue, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    'Private Sub BProses2_Click_1(sender As Object, e As EventArgs)
    '    DsLapF = New System.Data.DataSet

    '    Dim cmsl As SqlDataAdapter
    '    cmsl = New SqlDataAdapter("SPLStokHargaBJ2", koneksi)
    '    cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
    '    cmsl.SelectCommand.Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditorTypeName
    '    cmsl.SelectCommand.Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
    '    cmsl.SelectCommand.Parameters.Add("@stsHarga", SqlDbType.VarChar).Value = "%"
    '    cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
    '    cmsl.SelectCommand.CommandTimeout = 90000

    '    cmsl.TableMappings.Add("Table", "StokHargaBJ" & Gol)
    '    Try
    '        DsLapF.Tables("StokHargaBJ" & Gol).Clear()
    '    Catch ex As Exception

    '    End Try
    '    cmsl.Fill(DsLapF, "StokHargaBJ" & Gol)

    '    Me.GridControl1.DataSource = DsLapF
    '    Me.GridControl1.DataMember = "StokHargaBJ" & Gol
    'End Sub
End Class