Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRFtBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotSbDisc, TotDisc As Integer

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRp + RpDiscP As Disc,HarAkhir From T_JualBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where JualID ='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_JualBBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("Disc", "Disc"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_JualBBDtl")

        Me.DataMember = "T_JualBBDtl"
        Me.DataSource = DsLap

        Me.LBHeader.Text = Bind.Item("Gol").ToString
        Me.LBJualID.Text = Bind.Item("Kode").ToString
        Me.LBCust.Text = Bind.Item("Cust").ToString
        Me.LBTanggal.Text = Format(CDate(Bind.Item("Tanggal")), "dd/MM/yyyy")
        Me.LBDueDate.Text = Format(CDate(Bind.Item("DueDate")), "dd/MM/yyyy")
        Me.LBAlamat.Text = Bind.Item("Alamat").ToString
        Me.LBKet.Text = "Keterangan : " & Bind.Item("Ket").ToString

        Me.LBTotDiscH.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotDisc").ToString))
        Me.LBTotAkhir.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotAkhir").ToString))
     
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.Bahan")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.Qty", "{0:#,##0.##}")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.Sat")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.HarSat", "{0:#,##0.#####}")})
        Me.LBDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.Disc", "{0:#,##0.#####}")})
        Me.LBHarAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.HarSbDisc", "{0:#,##0.#####}")})
       
        Me.LBTotSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.HarSbDisc", "{0:n2}")})
        SumSbDisc.FormatString = "{0:n2}"
        SumSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotSbDisc.Summary = SumSbDisc

        Me.LBTotDiscDet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBBDtl.Disc", "{0:n2}")})
        SumDisc.FormatString = "{0:n2}"
        SumDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDiscDet.Summary = SumDisc

        Me.ShowPreview()
    End Sub

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class