Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRPRSpM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection, Jenis As String, Gol As String)
        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            command = New SqlCommand("Select ISOID From M_DocCode Where DocID=62 and CabID='" & Gol & "'", koneksi)

        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select ISOID From M_DocCode Where DocID=64 and CabID='" & Gol & "'", koneksi)

        End If

        koneksi.Close()

        With koneksi
            .Open()
            Me.LBIDISO.Text = command.ExecuteScalar()
            .Close()
        End With

        cmsl = New SqlDataAdapter("Select MesinID,B.Nama as Bahan,D.Sat,Qty,D.Ket From T_PRSpMDtl D Inner Join M_BB B On D.BBID=B.BBID Where PRSMID ='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_PRSpMDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("MesinID", "MesinID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_PRSpMDtl")

        Me.DataMember = "T_PRSpMDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBHeader.Text = "Purchase Request " & Bind.Item("Jenis").ToString
        Me.LBUnit.Text = ": " & Bind.Item("Unit").ToString
        Me.LBDiv.Text = ": " & Bind.Item("Divisi").ToString
        Me.LBPeminta.Text = ": " & Bind.Item("Peminta").ToString
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("TglKirim")), "dd MMMM yyyy")
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBMesin.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_PRSpMDtl.MesinID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_PRSpMDtl.Bahan")})
        Me.LBKetD.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_PRSpMDtl.Ket")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_PRSpMDtl.Qty", "{0:#,##0.##}")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_PRSpMDtl.Sat")})

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

    Private Sub XRBPB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class