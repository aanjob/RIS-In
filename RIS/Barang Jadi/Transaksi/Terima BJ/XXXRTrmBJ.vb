Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XXXRTrmBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotQty, Sisa As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select BSTBID,POID,D.ArtCode,ArtName,D.Isi,Qty,Psg From T_TrmBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where TrmID='" & Bind.Item("TrmID").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_TrmBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("BSTBID", "BSTBID"), New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Isi", "Isi"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Psg", "Psg")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_TrmBJDtl")

        Me.DataMember = "T_TrmBJDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBJudul.Text = "Penerimaan Barang Jadi " & Bind.Item("Gol").ToString
        Me.LBKode.Text = ": " & Bind.Item("TrmID").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBSupp.Text = ": " & Bind.Item("Supp").ToString
        Me.LBSJ.Text = ": " & Bind.Item("SJ").ToString
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.POID")})
        Me.LBBSTBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.BSTBID")})
        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.ArtCode")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.ArtName")})
        Me.LBIsi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.Isi")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.Qty", "{0:n1}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.Psg", "{0:n1}")})

        Me.LBTotQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.Qty", "{0:n1}")})
        SumQty.FormatString = "{0:n1}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotQty.Summary = SumQty

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmBJDtl.Psg", "{0:n1}")})
        SumPsg.FormatString = "{0:n1}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotPsg.Summary = SumPsg

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRTrmBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            Me.LBKota.Visible = True
            Me.LBTT1.Visible = True
            Me.LBTT2.Visible = True
            Me.LBTT3.Visible = True
            Me.LBTT4.Visible = True
        Else
            Me.LBTT1.Visible = False
            Me.LBTT2.Visible = False
            Me.LBTT3.Visible = False
            Me.LBTT4.Visible = False
        End If
    End Sub
End Class