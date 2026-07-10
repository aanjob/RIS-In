Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRLPR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumHarSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotSbDisc, TotDos, Sisa As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.ArtCode + ' ( ' + B.ArtName + ' )' As Barang,HarSat,Dos,Psg,HarSbDisc,DiscOB,RpDiscL+RpDiscGlb As DiscL,Ongkir,HarAkhir From T_LPRDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where LPRID='" & Bind.Item("Kode").ToString & "' Order By D.ArtCode Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_RtrBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Barang", "Barang"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("Dos", "Dos"), New System.Data.Common.DataColumnMapping("Psg", "Psg"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc"), New System.Data.Common.DataColumnMapping("DiscOB", "DiscOB")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_RtrBJDtl")

        Me.DataMember = "T_RtrBJDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = "No. LPR : " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = ": " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBAlamat.Text = ": " & Bind.Item("Alamat").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBTotSbDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotSbDisc").ToString))
        Me.LBTotPromo.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotRpDiscOB").ToString))
        Me.LBTotDiscL.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotRpDisc").ToString))
        Me.LBGrandTot.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotAkhir").ToString))

        Me.LBBarang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Barang")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Dos")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Psg")})
        Me.LBOB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.DiscOB", "{0:#}")})
        Me.LBDiscL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.DiscL", "{0:#}")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Dos", "{0:#}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Psg", "{0:#}")})
        Me.LBHarga.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.HarSat", "{0:n2}")})
        Me.LBHarSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.HarSbDisc", "{0:n2}")})

        Me.LBTotSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.HarSbDisc", "{0:n2}")})
        SumHarSbDisc.FormatString = "{0:n2}"
        SumHarSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotSbDisc.Summary = SumHarSbDisc

        Me.LBTotDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Dos", "{0:#}")})
        SumDos.FormatString = "{0:#}"
        SumDos.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDos.Summary = SumDos

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrBJDtl.Psg", "{0:#}")})
        SumPsg.FormatString = "{0:#}"
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
            Me.PageHeight = 2788
            Me.PageWidth = 2159
        End If


        Me.ShowPreview()
    End Sub

    Private Sub XRTrmBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            Me.LBJdlPromo.Visible = True
            Me.LBJdlGrandTotal.Visible = True
            Me.LBT22.Visible = True
            Me.LBT24.Visible = True
            Me.LBTotPromo.Visible = True
            Me.LBGrandTot.Visible = True

        Else
            Me.LBJdlPromo.Visible = False
            Me.LBJdlGrandTotal.Visible = False
            Me.LBT22.Visible = False
            Me.LBT24.Visible = False
            Me.LBTotPromo.Visible = False
            Me.LBGrandTot.Visible = False
        End If
    End Sub
End Class