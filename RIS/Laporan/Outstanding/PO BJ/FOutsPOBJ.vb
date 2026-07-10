Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FOutsPOBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
    Dim DsLapF As New System.Data.DataSet
    Dim CekAll As Boolean

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
        Me.LBGol.Text = "    " & Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOutsPOBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainModule.Posisi Like "*Cabang" Then
            Me.GridColumn5.Visible = False
        Else
            Me.GridColumn5.Visible = True
        End If

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
    End Sub

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Grup Like '" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        Try
            DsLapF.Tables("M_BrgMerkLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BrgMerkLUE")
        DsLapF.Tables("M_BrgMerkLUE").Rows.Add("%", "Semua")

        Me.SLUMerk.Properties.DataSource = DsLapF.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "Nama"
        Me.SLUMerk.Properties.ValueMember = "MerkID"
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Outstanding PO BJ " & Gol & " Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter

        If Me.CEAllPO.EditValue = False Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,P.POID,P.Tanggal,PD.ArtCode,B.ArtName, M.Nama As Merk, K.Nama As Kategori,W.Nama as Warna,B.Ass,J.Nama as Jenis,Qty,PD.BtlOrder+PD.BtlProd As BtlOrder,(Select Isnull((Select Top 1 B.BOMID From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=P.POID and BP.ArtCodeInd =PD.ArtCode),'')) As SPK,Psg,(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=P.POID and ArtCode=PD.ArtCode),0)) as Terima,Psg+PsgPol-PD.BtlOrder-PD.BtlProd-PD.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=P.POID and ArtCode=PD.ArtCode),0)) As Sisa From T_POBJLk P Inner Join T_POBJLkDtl PD On P.POID=PD.POID Inner Join M_Brg B On PD.ArtCode=B.ArtCode Inner Join M_BrgMerk M On M.MerkID=B.MerkID Inner Join M_BrgKat K On K.KatID=B.KatID Inner Join M_BrgJns J On J.JnsID=B.JnsID Inner Join M_BrgWrn W On W.WrnID=B.WrnID Where P.stsBatal='False' and PD.SisaKirim>0 and P.Grup Like '" & Me.SLUGrup.EditValue & "' and P.Gol='" & Gol & "' and M.MerkID Like '" & Me.SLUMerk.EditValue & "'", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,P.POID,P.Tanggal,PD.ArtCode,B.ArtName, M.Nama As Merk, K.Nama As Kategori,W.Nama as Warna,B.Ass,J.Nama as Jenis,Qty,PD.BtlOrder+PD.BtlProd As BtlOrder,(Select Isnull((Select Top 1 B.BOMID From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=P.POID and BP.ArtCodeInd =PD.ArtCode),'')) As SPK,Psg,(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=P.POID and ArtCode=PD.ArtCode),0)) as Terima,Psg+PsgPol-PD.BtlOrder-PD.BtlProd-PD.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=P.POID and ArtCode=PD.ArtCode),0)) As Sisa From T_POBJLk P Inner Join T_POBJLkDtl PD On P.POID=PD.POID Inner Join M_Brg B On PD.ArtCode=B.ArtCode Inner Join M_BrgMerk M On M.MerkID=B.MerkID Inner Join M_BrgKat K On K.KatID=B.KatID Inner Join M_BrgJns J On J.JnsID=B.JnsID Inner Join M_BrgWrn W On W.WrnID=B.WrnID Where P.Grup Like'" & Me.SLUGrup.EditValue & "' and P.Gol='" & Gol & "' and M.MerkID Like '" & Me.SLUMerk.EditValue & "'", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "OutsPO" & Gol)

        Try
            DsLapF.Tables("OutsPO" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "OutsPO" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "OutsPO" & Gol
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
        Dim POID, ArtCode As String
        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("OutsPO" & Gol).Rows.Count - 1
            If DsLapF.Tables("OutsPO" & Gol).Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    POID = "'" & DsLapF.Tables("OutsPO" & Gol).Rows(i).Item("POID") & "'"
                    ArtCode = "'" & DsLapF.Tables("OutsPO" & Gol).Rows(i).Item("ArtCode") & "'"

                Else
                    POID &= ",'" & DsLapF.Tables("OutsPO" & Gol).Rows(i).Item("POID") & "'"
                    ArtCode &= ",'" & DsLapF.Tables("OutsPO" & Gol).Rows(i).Item("ArtCode") & "'"

                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(POID, "POID")
        bind.Add(ArtCode, "ArtCode")

        Dim XR As New XROutsPOBJ
        XR.InitializeData(bind)
    End Sub
End Class