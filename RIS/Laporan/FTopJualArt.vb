Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FTopJualArt
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
            Dim link As BaseExportLink = GridView5.CreateExportLink(provider)
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

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan
        Me.LBGol.Text = "    " & Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FTopJualArt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
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

        cmsl = New SqlDataAdapter("Select Distinct stsHarga From M_BrgHarga", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgHarga")
        Try
            DsLapF.Tables("M_BrgHarga").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BrgHarga")
        DsLapF.Tables("M_BrgHarga").Rows.Add("%")

        Me.SLUStatus.Properties.DataSource = DsLapF.Tables("M_BrgHarga")
        Me.SLUStatus.Properties.DisplayMember = "stsHarga"
        Me.SLUStatus.Properties.ValueMember = "stsHarga"

        cmsl = New SqlDataAdapter("Select JnsID,Nama From M_BrgJns", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgJnsLUE")
        Try
            DsLapF.Tables("M_BrgJnsLUE").Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BrgJnsLUE")
        DsLapF.Tables("M_BrgJnsLUE").Rows.Add("%", "Semua Jenis")

        Me.SLUJns.Properties.DataSource = DsLapF.Tables("M_BrgJnsLUE")
        Me.SLUJns.Properties.DisplayMember = "Nama"
        Me.SLUJns.Properties.ValueMember = "JnsID"

    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        Dim cmsl As SqlDataAdapter

        Try

            If Gol = "Job Order" Then
                cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where Gol='" & Gol & "' and CabID Like'" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_SalesL")
                Try
                    DsLapF.Tables("M_SalesL" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsLapF, "M_SalesL" & Gol)

            Else
                cmsl = New SqlDataAdapter("Select S.SalID,Nama From M_Sales S Inner Join M_CabSal CS On S.SalID=CS.SalID Where Gol='Lokal' and CabID Like'" & Me.SLUCab.EditValue & "' and CS.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_SalesL" & Gol)
                Try
                    DsLapF.Tables("M_SalesL" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsLapF, "M_SalesL" & Gol)
            End If

            DsLapF.Tables("M_SalesL" & Gol).Rows.Add("%", "Semua Sales")


            Me.SLUSales.Properties.DataSource = DsLapF.Tables("M_SalesL" & Gol)
            Me.SLUSales.Properties.DisplayMember = "Nama"
            Me.SLUSales.Properties.ValueMember = "SalID"

            If Not IsDBNull(Me.SLUCab.EditValue) Then
                'Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID Like'" & Me.SLUCab.EditValue & "' and Grup Like'" & Me.SLUGrup.EditValue & "' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangL" & Gol)
                Try
                    DsMaster.Tables("M_GudangL" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsLapF, "M_GudangL" & Gol)
                If MainModule.Posisi Like "*Cabang" Then

                Else
                    DsLapF.Tables("M_GudangL" & Gol).Rows.Add("%", "Semua Gudang")
                End If

                Me.SLUGd.Properties.DataSource = DsLapF.Tables("M_GudangL" & Gol)
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"

            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPLTopJualArt", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
        cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
        cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
        cmsl.SelectCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = Me.SLUStatus.EditValue
        cmsl.SelectCommand.Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.SLUJns.EditValue

        cmsl.SelectCommand.Parameters.Add("@Top", SqlDbType.Int).Value = Me.TBTop.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "TopJualArt" & Gol)
        Try
            DsLapF.Tables("TopJualArt" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "TopJualArt" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "TopJualArt" & Gol


        cmsl = New SqlDataAdapter("SPLTopJualArtDtl", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
        cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
        cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
        cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
        cmsl.SelectCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = Me.SLUStatus.EditValue
        cmsl.SelectCommand.Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
        cmsl.SelectCommand.Parameters.Add("@Top", SqlDbType.Int).Value = Me.TBTop.EditValue
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "TopJualArtDtl" & Gol)
        Try
            DsLapF.Tables("TopJualArtDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "TopJualArtDtl" & Gol)

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "TopJualArtDtl" & Gol

        If Me.GridView1.RowCount > 0 Then
            'If Me.GridView5.RowCount > 0 Then
            Me.GridView5.ActiveFilterString = "[ArtName] = '" & Me.GridView1.GetFocusedRowCellValue("ArtName") & "' and [stsHarga] = '" & Me.GridView1.GetFocusedRowCellValue("stsHarga") & "'"
            'End If
        End If

        cmsl = New SqlDataAdapter("Select Distinct P.POID,TglAwal,TglAkhir,KetProd,D.ArtCode From T_POBJLk P Inner Join T_BOM B On P.POID=B.POID Inner Join T_POBJLkDtl D On P.POID=D.POID Where P.Gol='" & Gol & "' and P.Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)

        'cmsl = New SqlDataAdapter("Select P.POID,P.Tanggal,PD.ArtCode,B.ArtName, M.Nama As Merk, K.Nama As Kategori,W.Nama as Warna,B.Ass,J.Nama as Jenis,Qty,PD.BtlOrder,(Select Isnull((Select Top 1 B.BOMID From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=P.POID and BP.ArtCodeInd =PD.ArtCode),'')) As SPK,Qty,(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=P.POID and ArtCode=PD.ArtCode),0)) as Terima,Qty-PD.BtlOrder-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=P.POID and ArtCode=PD.ArtCode),0)) As Sisa From T_POBJLk P Inner Join T_POBJLkDtl PD On P.POID=PD.POID Inner Join M_Brg B On PD.ArtCode=B.ArtCode Inner Join M_BrgMerk M On M.MerkID=B.MerkID Inner Join M_BrgKat K On K.KatID=B.KatID Inner Join M_BrgJns J On J.JnsID=B.JnsID Inner Join M_BrgWrn W On W.WrnID=B.WrnID Where P.stsBatal='False' and P.stsLunas='False' and PD.stsLunas='False' and P.Gol='" & Gol & "' and Tanggal<='" & Me.DTPAkhir.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "OutsPO" & Gol)

        Try
            DsLapF.Tables("OutsPO" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "OutsPO" & Gol)

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "OutsPO" & Gol

        If Me.GridView5.RowCount > 0 Then
            'If Me.GridView5.RowCount > 0 Then
            Me.GridView6.ActiveFilterString = "[ArtCode] = '" & Me.GridView5.GetFocusedRowCellValue("ArtCode") & "'"
            'End If
        End If
    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Top Penjualan Article Per Tanggal " & Format(Me.DTPAkhir.EditValue, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            ExportTo2(New ExportXlsProvider(Replace(fileName, ".xls", " (2).xls")))
            OpenFile(fileName)
            OpenFile2(Replace(fileName, ".xls", " (2).xls"))

        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.RowCount > 0 Then
            'If Me.GridView5.RowCount > 0 Then
            Me.GridView5.ActiveFilterString = "[ArtName] = '" & Me.GridView1.GetFocusedRowCellValue("ArtName") & "' and [stsHarga] = '" & Me.GridView1.GetFocusedRowCellValue("stsHarga") & "'"
            'End If
        End If
    End Sub

    Private Sub GridView5_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView5.FocusedRowChanged
        If Me.GridView5.RowCount > 0 Then
            'If Me.GridView5.RowCount > 0 Then
            Me.GridView6.ActiveFilterString = "[ArtCode] = '" & Me.GridView5.GetFocusedRowCellValue("ArtCode") & "'"
            'End If
        End If
    End Sub
End Class