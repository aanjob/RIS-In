Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRFtBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotSbDisc, TotDisc, TotDos, TotPsg As Integer

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select * From (Select D.ArtCode, Concat (D.ArtCode,' ( ',B.ArtName,' )') As Barang,HarSat,Qty,Dos,Psg, HarSbDisc,DiscOB,RpDiscOB+RpDiscCust As DiscDet,HarAkhir From T_JualBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where JualID ='" & Bind.Item("JualID").ToString & "' Union All Select D.ArtCode, Concat (D.ArtCode,' ( ',ArtName,' )') As Barang,0 As HarSat,Qty,Dos,Psg,0 As HarSbDisc,0 As DiscOB,0 As DiscDet,0 As HarAkhir From T_JualBJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where JualID ='" & Bind.Item("JualID").ToString & "') As x Order By ArtCode", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_JualBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Barang", "Barang"), New System.Data.Common.DataColumnMapping("Dos", "Dos"), New System.Data.Common.DataColumnMapping("Psg", "Psg"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("DiscOB", "DiscOB"), New System.Data.Common.DataColumnMapping("DiscDet", "DiscDet"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_JualBJDtl")

        Me.DataMember = "T_JualBJDtl"
        Me.DataSource = DsLap

        Me.LBJualID.Text = Bind.Item("JualID").ToString
        Me.LBKode.Text = Bind.Item("Kode").ToString
        Me.LBTanggal.Text = Format(CDate(Bind.Item("Tanggal")), "dd/MM/yyyy")
        Me.LBDueDate.Text = Format(CDate(Bind.Item("DueDate")), "dd/MM/yyyy")
        Me.LBCust.Text = Bind.Item("Cust").ToString
        Me.LBAlamat.Text = Bind.Item("Alamat").ToString
        Me.LBKet.Text = "Keterangan : " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBTotDiscH.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotDisc").ToString))
        Me.LBTotByExpd.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotByExp").ToString))
        Me.LBTotAkhir.Text = String.Format("{0:#,##0.00;(#,##0.00)}", CDec(Bind.Item("TotAkhir").ToString))

        Me.LBBarang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Barang")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Dos", "{0:n0}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Psg", "{0:n0}")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.HarSat", "{0:n2}")})
        Me.LBDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.DiscOB", "{0:n3}")})
        Me.LBHarAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.HarSbDisc", "{0:n2}")})

        Me.LBTotSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.HarSbDisc", "{0:n2}")})
        SumSbDisc.FormatString = "{0:n2}"
        SumSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotSbDisc.Summary = SumSbDisc

        Me.LBTotDiscDet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.DiscDet", "{0:n2}")})
        SumDisc.FormatString = "{0:n2}"
        SumDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDiscDet.Summary = SumDisc

        Me.LBTotDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Dos", "{0:n0}")})
        SumDos.FormatString = "{0:n0}"
        SumDos.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDos.Summary = SumDos

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Psg", "{0:n0}")})
        SumPsg.FormatString = "{0:n0}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotPsg.Summary = SumPsg

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            'Me.LBTotSbDisc.Visible = True
            'Me.LBTotDiscDet.Visible = True
            Me.LBTotDiscH.Visible = True
            Me.LBTotByExpd.Visible = True
            Me.LBTotAkhir.Visible = True
        Else
            'Me.LBTotSbDisc.Visible = False
            'Me.LBTotDiscDet.Visible = False
            Me.LBTotDiscH.Visible = False
            Me.LBTotByExpd.Visible = False
            Me.LBTotAkhir.Visible = False

        End If
    End Sub

    Private Sub XRFtBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class