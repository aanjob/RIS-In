Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRBukuPemb
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSbDiscRp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPiut As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFDPP As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPPn As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFBM As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Dim SumRFSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSbDiscRp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPiut As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFDPP As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPPn As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFBM As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    'Dim SumTot, SumDPP, SumPPn, SumPiut As Decimal
    Dim No As Integer = 1

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLBukuPemb", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = MainModule.PilihAwal
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = MainModule.PilihAkhir
        cmsl.SelectCommand.Parameters.Add("@PPn", SqlDbType.VarChar).Value = MainModule.PilihPPn
        cmsl.SelectCommand.Parameters.Add("@Kat", SqlDbType.VarChar).Value = MainModule.PilihKat

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "BukuPemb", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Kode", "Kode"), New System.Data.Common.DataColumnMapping("RowNumber", "RowNumber"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("NPWP", "NPWP"), New System.Data.Common.DataColumnMapping("NoPajak", "NoPajak"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("NilTukarRp", "NilTukarRp"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc"), New System.Data.Common.DataColumnMapping("HarSbDiscRp", "HarSbDiscRp"), New System.Data.Common.DataColumnMapping("TotDPP", "TotDPP"), New System.Data.Common.DataColumnMapping("TotPPn", "TotPPn"), New System.Data.Common.DataColumnMapping("TotPiut", "TotPiut"), New System.Data.Common.DataColumnMapping("TotPiutRp", "TotPiutRp"), New System.Data.Common.DataColumnMapping("BM", "BM"), New System.Data.Common.DataColumnMapping("PEN", "PEN"), New System.Data.Common.DataColumnMapping("BL", "BL"), New System.Data.Common.DataColumnMapping("TglBL", "TglBL"), New System.Data.Common.DataColumnMapping("LC", "LC"), New System.Data.Common.DataColumnMapping("COA", "COA"), New System.Data.Common.DataColumnMapping("InisialBC", "InisialBC")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "BukuPemb")

        Me.DataMember = "BukuPemb"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBKode})
        Me.GroupHeader3.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Kode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        'Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() })
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("RowNumber", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBNo, Me.LBTgl, Me.LBNoFP, Me.LBSupp, Me.LBNPWP})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("NoPajak", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBKode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Kode")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Tanggal", "{0:dd}")})
        Me.LBNoFP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.NoPajak")})
        Me.LBSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Supp")})
        Me.LBNPWP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.NPWP")})
        Me.LBNoLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TrmID")})
        Me.LBBJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Bahan")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Qty", "{0:n2}")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Sat")})
        Me.LBHarga.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSat", "{0:n2}")})
        Me.LBNilTukar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.NilTukarRp", "Nilai Tukar : {0:n2}")})
        Me.LBTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSbDisc", "{0:n2}")})
        Me.LBTotRp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSbDiscRp", "{0:n2}")})
        Me.LBBM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.BM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBPEN.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.PEN")})
        Me.LBBL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.BL")})
        Me.LBTglBL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TglBL", "{0:dd/MM}")})
        Me.LBLC.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.LC")})
        Me.LBCOA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.COA")})
        Me.LBFas.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.InisialBC")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFTot, Me.LBGFDPP, Me.LBGFPPn, Me.LBGFTotPiut})

        Me.LBGFTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSbDisc", "{0:n2}")})
        SumGFSbDisc.FormatString = "{0:n2}"
        SumGFSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTot.Summary = SumGFSbDisc

        Me.LBGFTotRp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSbDiscRp", "{0:n2}")})
        SumGFSbDiscRp.FormatString = "{0:n2}"
        SumGFSbDiscRp.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotRp.Summary = SumGFSbDiscRp

        Me.LBGFTotPiut.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TotPiutRp", "{0:n2}")})
        SumGFPiut.FormatString = "{0:n2}"
        SumGFPiut.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotPiut.Summary = SumGFPiut

        Me.LBGFDPP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TotDPP", "{0:n2}")})
        SumGFDPP.FormatString = "{0:n2}"
        SumGFDPP.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFDPP.Summary = SumGFDPP

        Me.LBGFPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TotPPn", "{0:n2}")})
        SumGFPPn.FormatString = "{0:n2}"
        SumGFPPn.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPPn.Summary = SumGFPPn

        Me.LBRFQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.Qty", "{0:n2}")})
        SumQty.FormatString = "{0:n2}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFQty.Summary = SumQty

        Me.LBGFTotBM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.BM", "{0:n2}")})
        SumGFBM.FormatString = "{0:n2}"
        SumGFBM.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotBM.Summary = SumGFBM

        Me.LBRFTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSbDisc", "{0:n2}")})
        SumRFSbDisc.FormatString = "{0:n2}"
        SumRFSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTot.Summary = SumRFSbDisc

        Me.LBRFTotRp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.HarSbDiscRp", "{0:n2}")})
        SumRFSbDiscRp.FormatString = "{0:n2}"
        SumRFSbDiscRp.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTotRp.Summary = SumRFSbDiscRp

        Me.LBRFTotPiut.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TotPiutRp", "{0:n2}")})
        SumRFPiut.FormatString = "{0:n2}"
        SumRFPiut.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTotPiut.Summary = SumRFPiut

        Me.LBRFDPP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TotDPP", "{0:n2}")})
        SumRFDPP.FormatString = "{0:n2}"
        SumRFDPP.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFDPP.Summary = SumRFDPP

        Me.LBRFPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.TotPPn", "{0:n2}")})
        SumRFPPn.FormatString = "{0:n2}"
        SumRFPPn.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFPPn.Summary = SumRFPPn

        Me.LBRFTotBM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BukuPemb.BM", "{0:n2}")})
        SumRFBM.FormatString = "{0:n2}"
        SumRFBM.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTotBM.Summary = SumRFBM

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRBukuPemb_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    'Private Sub ReportHeader_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportHeader.BeforePrint
    '    SumTot = 0
    '    SumDPP = 0
    '    SumPPn = 0
    '    SumPiut = 0
    'End Sub

    'Private Sub LBGFTot_AfterPrint(sender As Object, e As EventArgs) Handles LBGFTot.AfterPrint
    '    SumTot += CDec(Me.LBGFTot.Text)
    'End Sub

    'Private Sub LBGFDPP_AfterPrint(sender As Object, e As EventArgs) Handles LBGFDPP.AfterPrint
    '    Me.LBGFDPP.Text = Math.Round(CDec(Me.LBGFDPP.Text), 2, MidpointRounding.AwayFromZero)
    'End Sub

    'Private Sub LBGFPPn_AfterPrint(sender As Object, e As EventArgs) Handles LBGFPPn.AfterPrint
    '    SumPPn += CDec(Me.LBGFPPn.Text)
    'End Sub

    'Private Sub LBGFTotPiut_AfterPrint(sender As Object, e As EventArgs) Handles LBGFTotPiut.AfterPrint
    '    SumPiut += CDec(Me.LBGFTotPiut.Text)
    'End Sub

    'Private Sub LBPFTot_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBPFTot.BeforePrint
    '    Me.LBPFTot.Text = String.Format("{0:#,##0.00;(#,##0.00);0}", SumTot)
    'End Sub

    'Private Sub LBPFDPP_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBPFDPP.BeforePrint
    '    Me.LBPFDPP.Text = String.Format("{0:#,##0.00;(#,##0.00);0}", SumDPP)
    'End Sub

    'Private Sub LBPFPPn_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBPFPPn.BeforePrint
    '    Me.LBPFPPn.Text = String.Format("{0:#,##0.00;(#,##0.00);0}", SumPPn)
    'End Sub

    'Private Sub LBPFTotPiut_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBPFTotPiut.BeforePrint
    '    Me.LBPFTotPiut.Text = String.Format("{0:#,##0.00;(#,##0.00);0}", SumPiut)
    'End Sub

    Private Sub LBNoFP_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBNoFP.BeforePrint
        No += 1
    End Sub

    Private Sub LBNo_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBNo.BeforePrint
        Me.LBNo.Text = No
    End Sub

    'Private Sub GroupHeader3_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeader3.BeforePrint
    '    No = 1
    'End Sub

End Class