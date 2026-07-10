Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FOmsetBln
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
            Dim link As BaseExportLink = BandedGridView1.CreateExportLink(provider)
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
            Dim link As BaseExportLink = BandedGridView2.CreateExportLink(provider)
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

        Me.SPTahun.EditValue = MainModule.periodeTahun
        Gol = Golongan
        Me.LBGol.Text = "    " & Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOmsetBln_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MainModule.Posisi Like "*Cabang" Then
            Me.GridColumn2.Visible = False
            Me.GridColumn30.Visible = False
            Me.GridColumn31.Visible = False
        Else
            Me.GridColumn2.Visible = True
            Me.GridColumn30.Visible = True
            Me.GridColumn31.Visible = True
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

    Private Sub SLUSubGrup_Leave(sender As Object, e As EventArgs) Handles SLUSubGrup.Leave
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct B.MerkID,Nama From M_Brg B Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where SubGrup Like '" & Me.SLUSubGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        Try
            DsLapF.Tables("M_BrgMerkLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BrgMerkLUE")
        DsLapF.Tables("M_BrgMerkLUE").Rows.Add("%", "Semua Merk")

        Me.SLUMerk.Properties.DataSource = DsLapF.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "Nama"
        Me.SLUMerk.Properties.ValueMember = "MerkID"
    End Sub

    'Private Sub SLUSubGrup_Leave(sender As Object, e As EventArgs) Handles SLUSubGrup.Leave
    '    Dim cmsl As SqlDataAdapter

    '    cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where SubGrup Like '" & Me.SLUGrup.EditValue & "' and Aktif='True'", koneksi)
    '    cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
    '    Try
    '        DsLapF.Tables("M_BrgMerkLUE").Clear()
    '    Catch ex As Exception

    '    End Try
    '    cmsl.Fill(DsLapF, "M_BrgMerkLUE")
    '    DsLapF.Tables("M_BrgMerkLUE").Rows.Add("%", "Semua Merk")

    '    Me.SLUMerk.Properties.DataSource = DsLapF.Tables("M_BrgMerkLUE")
    '    Me.SLUMerk.Properties.DisplayMember = "Nama"
    '    Me.SLUMerk.Properties.ValueMember = "MerkID"
    'End Sub


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
                cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustL" & Gol)
                Try
                    DsLapF.Tables("M_CustL" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsLapF, "M_CustL" & Gol)
                DsLapF.Tables("M_CustL" & Gol).Rows.Add("%", "Semua Customer", "")

                Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustL" & Gol)
                Me.SLUCust.Properties.DisplayMember = "Nama"
                Me.SLUCust.Properties.ValueMember = "CustID"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter

        If Gol = "Gabungan" Then
            cmsl = New SqlDataAdapter("SPLOmsetBlnGab", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SPTahun.EditValue
            cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
            cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
            cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@JnsOmset", SqlDbType.VarChar).Value = Me.CBOJnsOms.EditValue

            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "OmsetBln" & Gol)
            Try
                DsLapF.Tables("OmsetBln" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "OmsetBln" & Gol)

        Else
            cmsl = New SqlDataAdapter("SPLOmsetBln", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SPTahun.EditValue
            cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
            cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
            cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
            cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@JnsOmset", SqlDbType.VarChar).Value = Me.CBOJnsOms.EditValue

            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "OmsetBln" & Gol)

            Try
                DsLapF.Tables("OmsetBln" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "OmsetBln" & Gol)
        End If

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "OmsetBln" & Gol


        If Gol = "Gabungan" Then
            cmsl = New SqlDataAdapter("SPLOmsetBlnSalGab", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SPTahun.EditValue
            cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
            cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
            cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@JnsOmset", SqlDbType.VarChar).Value = Me.CBOJnsOms.EditValue

            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "OmsetBlnSal" & Gol)
            Try
                DsLapF.Tables("OmsetBlnSal" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "OmsetBlnSal" & Gol)

        Else
            cmsl = New SqlDataAdapter("SPLOmsetBlnSal", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SPTahun.EditValue
            cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
            cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
            cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            cmsl.SelectCommand.Parameters.Add("@Sal", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
            cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            cmsl.SelectCommand.Parameters.Add("@JnsOmset", SqlDbType.VarChar).Value = Me.CBOJnsOms.EditValue

            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "OmsetBlnSal" & Gol)

            Try
                DsLapF.Tables("OmsetBlnSal" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "OmsetBlnSal" & Gol)
        End If

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "OmsetBlnSal" & Gol
    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Omset Bulanan " & Gol & " Tahun " & Me.SPTahun.EditValue & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            ExportTo2(New ExportXlsProvider(Replace(fileName, ".xls", " (2).xls")))
            OpenFile(fileName)
            OpenFile2(Replace(fileName, ".xls", " (2).xls"))

        End If
    End Sub

End Class