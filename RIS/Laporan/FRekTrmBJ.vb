Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRekTrmBJ
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

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FRekTrmBJ_Load(sender As Object, e As EventArgs) Handles Me.Load
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
    End Sub

   Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select D.POID,(Select Case When H.Gol='Job Order' Then POCust Else '' End As POCust From T_POBJJO Where POID=D.POID) AS POCust,S.Nama As Supp,H.TrmID,SJ,H.Ket,G.Nama As Gudang,H.Tanggal,D.ArtCode,ArtName,D.Isi,Sum(Qty) As Qty,Sum(Psg) As Psg From T_TrmBJ H Inner Join T_TrmBJDtl D On H.TrmID=D.TrmID Inner Join T_BSTB BH On D.BSTBID=BH.BSTBID Left Outer Join M_Supp S On BH.SuppID=S.SuppID Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where H.JnsDoc='BSTB' and H.Tanggal >='" & Me.DTPAwal.EditValue & "' and H.Tanggal <='" & Me.DTPAkhir.EditValue & "' and H.Grup Like '" & Me.SLUGrup.EditValue & "' and B.SubGrup Like '" & Me.SLUSubGrup.EditValue & "' and H.Gol='" & Gol & "' Group By S.Nama,H.TrmID,SJ,H.Ket,G.Nama,H.Tanggal,D.ArtCode,ArtName,D.Isi,D.POID,H.Gol Union All Select D.POID,'' As POCust,S.Nama As Supp,H.TrmID,SJ,H.Ket,G.Nama As Gudang,H.Tanggal,D.ArtCode,ArtName,D.Isi,Sum(Qty) As Qty,Sum(Psg) As Psg From T_TrmBJ H Inner Join T_TrmBJDtl D On H.TrmID=D.TrmID Inner Join T_POBJLk PH On D.POID=PH.POID Left Outer Join M_Supp S On PH.SuppID=S.SuppID Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where JnsDoc='PO' and H.Tanggal >='" & Me.DTPAwal.EditValue & "' and H.Tanggal <='" & Me.DTPAkhir.EditValue & "' and H.Grup Like '" & Me.SLUGrup.EditValue & "' and B.SubGrup Like '" & Me.SLUSubGrup.EditValue & "' and H.Gol='" & Gol & "'  Group By S.Nama,H.TrmID,SJ,H.Ket,G.Nama,H.Tanggal,D.ArtCode,ArtName,D.Isi,D.POID Union All Select D.POID,POCust,S.Nama As Supp,H.TrmID,SJ,H.Ket,G.Nama As Gudang,H.Tanggal,D.ArtCode,ArtName,D.Isi,Sum(Qty) As Qty,Sum(Psg) As Psg From T_TrmBJ H Inner Join T_TrmBJDtl D On H.TrmID=D.TrmID Inner Join T_POBJJO PH On D.POID=PH.POID Left Outer Join M_Supp S On PH.SuppID=S.SuppID Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where JnsDoc='PO' and H.Tanggal >='" & Me.DTPAwal.EditValue & "' and H.Tanggal <='" & Me.DTPAkhir.EditValue & "' and H.Grup Like '" & Me.SLUGrup.EditValue & "' and B.SubGrup Like '" & Me.SLUSubGrup.EditValue & "' and H.Gol='" & Gol & "' Group By S.Nama,H.TrmID,SJ,H.Ket,G.Nama,H.Tanggal,D.ArtCode,ArtName,D.Isi,D.POID,POCust Union All Select D.POID,'' As POCust,'' As Supp,H.TrmID,SJ,H.Ket,G.Nama As Gudang,H.Tanggal,D.ArtCode,ArtName,D.Isi,Sum(Qty) As Qty,Sum(Psg) As Psg From T_TrmBJ H Inner Join T_TrmBJDtl D On H.TrmID=D.TrmID Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where JnsDoc='Lain-Lain' and H.Tanggal >='" & Me.DTPAwal.EditValue & "' and H.Tanggal <='" & Me.DTPAkhir.EditValue & "' and H.Grup Like '" & Me.SLUGrup.EditValue & "' and B.SubGrup Like '" & Me.SLUSubGrup.EditValue & "' and H.Gol='" & Gol & "' Group By H.TrmID,SJ,H.Ket,G.Nama,H.Tanggal,D.ArtCode,ArtName,D.Isi,D.POID", koneksi)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "RekTrmBJ" & Gol)
        Try
            DsLapF.Tables("RekTrmBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "RekTrmBJ" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "RekTrmBJ" & Gol
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Terima Barang Jadi " & Gol & " Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

End Class