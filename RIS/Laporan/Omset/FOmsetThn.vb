Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FOmsetThn
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
            Dim link As BaseExportLink = AdvBandedGridView1.CreateExportLink(provider)
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

        Me.DTPAwal1.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir1.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Me.DTPAwal2.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir2.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan
        Me.LBGol.Text = "    " & Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOmsetThn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Me.GH1.Caption = Format(Me.DTPAwal1.EditValue, "MMMMM yyyy") & " s/d " & Format(Me.DTPAkhir1.EditValue, "MMMMM yyyy")
        Me.GH2.Caption = Format(Me.DTPAwal2.EditValue, "MMMMM yyyy") & " s/d " & Format(Me.DTPAkhir2.EditValue, "MMMMM yyyy")

        Dim cmsl As SqlDataAdapter

        If Gol = "Gabungan" Then
            cmsl = New SqlDataAdapter("SPLOmsetThnGab", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal1", SqlDbType.Date).Value = Me.DTPAwal1.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir1", SqlDbType.Date).Value = Me.DTPAkhir1.EditValue
            cmsl.SelectCommand.Parameters.Add("@Awal2", SqlDbType.Date).Value = Me.DTPAwal2.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir2", SqlDbType.Date).Value = Me.DTPAkhir2.EditValue
            cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
            cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            cmsl.SelectCommand.Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
            cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
           
        Else
            'MsgBox("1")
            'MsgBox(Me.DTPAwal1.EditValue)
            'MsgBox(Me.DTPAkhir1.EditValue)
            'MsgBox(Me.DTPAwal2.EditValue)
            'MsgBox(Me.DTPAkhir2.EditValue)
            'MsgBox(Me.SLUGrup.EditValue)
            'MsgBox(Me.SLUMerk.EditValue)
            'MsgBox(Me.SLUCab.EditValue)
            'MsgBox(Me.SLUSales.EditValue)
            'MsgBox(Me.SLUCust.EditValue)
            'MsgBox(Gol)
            cmsl = New SqlDataAdapter("SPLOmsetThn", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Awal1", SqlDbType.Date).Value = Me.DTPAwal1.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir1", SqlDbType.Date).Value = Me.DTPAkhir1.EditValue
            cmsl.SelectCommand.Parameters.Add("@Awal2", SqlDbType.Date).Value = Me.DTPAwal2.EditValue
            cmsl.SelectCommand.Parameters.Add("@Akhir2", SqlDbType.Date).Value = Me.DTPAkhir2.EditValue
            'cmsl.SelectCommand.Parameters.Add("@Awal1", SqlDbType.Date).Value = Format(Me.DTPAwal1.EditValue, "yyyy-MM-dd")
            'cmsl.SelectCommand.Parameters.Add("@Akhir1", SqlDbType.Date).Value = Format(Me.DTPAkhir1.EditValue, "yyyy-MM-dd")
            'cmsl.SelectCommand.Parameters.Add("@Awal2", SqlDbType.Date).Value = Format(Me.DTPAwal2.EditValue, "yyyy-MM-dd")
            'cmsl.SelectCommand.Parameters.Add("@Akhir2", SqlDbType.Date).Value = Format(Me.DTPAkhir2.EditValue, "yyyy-MM-dd")
            cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
            cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
            cmsl.SelectCommand.Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
            cmsl.SelectCommand.Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            cmsl.SelectCommand.Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.SLUSales.EditValue
            cmsl.SelectCommand.Parameters.Add("@Cust", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
        End If

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "OmsetThn" & Gol)
        Try
            DsLapF.Tables("OmsetThn" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "OmsetThn" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "OmsetThn" & Gol
    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Omset Tahunan")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class