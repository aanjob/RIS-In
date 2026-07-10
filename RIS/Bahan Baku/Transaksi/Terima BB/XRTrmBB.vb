Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRTrmBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection, Jenis As String, Gol As String)
        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            command = New SqlCommand("Select ISOID From M_DocCode Where DocID=6 and CabID='" & Gol & "'", koneksi)

        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select ISOID From M_DocCode Where DocID=57 and CabID='" & Gol & "'", koneksi)
        End If

        koneksi.Close()

        With koneksi
            .Open()
            Me.LBIDISO.Text = command.ExecuteScalar()
            .Close()
        End With

        cmsl = New SqlDataAdapter("Select B.Nama As Bahan,TD.Sat,Sum(Qty) As Qty From T_TrmBBDtl TD Inner Join M_BB B On TD.BBID=B.BBID Where TrmID ='" & Bind.Item("Kode").ToString & "' Group By B.Nama,TD.Sat", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_TrmBBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_TrmBBDtl")

        Me.DataMember = "T_TrmBBDtl"
        Me.DataSource = DsLap

        If CBool(Bind.Item("Manual").ToString) = False Then
            cmsl = New SqlDataAdapter("Select Distinct BOMID From T_TrmBBDtl Where TrmID ='" & Bind.Item("Kode").ToString & "'", koneksi)
            cmsl.Fill(DsLap, "BOM")

            Dim i : For i = 0 To DsLap.Tables("BOM").Rows.Count - 1
                If i = 0 Then
                    Me.LBBOM.Text = DsLap.Tables("BOM").Rows(i).Item(0)

                Else
                    Me.LBBOM.Text &= "," & DsLap.Tables("BOM").Rows(i).Item(0).ToString.Substring(4, DsLap.Tables("BOM").Rows(i).Item(0).ToString.Length - 4)
                End If
            Next
        End If

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan & vbCrLf & MainModule.Alamat & vbCrLf & MainModule.Kota
        ''Me.LBHeader.Text = Me.LBHeader.Text & " " & Bind.Item("Gol").ToString
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBSupp.Text = ": " & Bind.Item("Supp").ToString
        Me.LBAlamat.Text = ": " & Bind.Item("Alamat").ToString & vbCrLf & "  " & Bind.Item("Kota").ToString
        Me.LBTipePPn.Text = ": " & Bind.Item("TipePPn").ToString
        Me.LBTglJT.Text = ": " & Format(CDate(Bind.Item("TglJT")), "dd MMMM yyyy")
        Me.LBPOID.Text = ": " & Bind.Item("POID").ToString
        Me.LBNoSJ.Text = ": " & Bind.Item("SJ").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBBDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBBDtl.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBBDtl.Qty", "{0:#,##0.##}")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
            Me.ShowPreview()
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
            Me.ShowPreview()
        End If
    End Sub

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class