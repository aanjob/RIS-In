Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPOBJLkR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGFTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim Kode As String

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select POIDD,D.ArtCode As ArtCodeH,ArtName,StyleID,Upp,Outs,Variasi,D.Qty+D.QtyPol As Qty,D.Psg+D.PsgPol As Psg,HarSat,HCBP,HarAkhir From T_POBJLkDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where D.POID ='" & Bind.Item("Kode").ToString & "'", koneksi)

        Kode = Bind.Item("Kode").ToString

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_POBJLkDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("POIDD", "POIDD"), New System.Data.Common.DataColumnMapping("ArtCodeH", "ArtCodeH"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("StyleID", "StyleID"), New System.Data.Common.DataColumnMapping("Upp", "Upp"), New System.Data.Common.DataColumnMapping("Outs", "Outs"), New System.Data.Common.DataColumnMapping("Variasi", "Variasi"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Psg", "Psg"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("HCBP", "HCBP"), New System.Data.Common.DataColumnMapping("HarAkhir", "HarAkhir")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_POBJLkDtl")

        Me.DataMember = "T_POBJLkDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBKpd.Text = ": " & Bind.Item("Kpd").ToString
        Me.LBKirimKe.Text = ": " & Bind.Item("KrmKe").ToString
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("TglKrmAw")), "dd MMMM yyyy") & " s/d " & Format(CDate(Bind.Item("TglKrmAkh")), "dd MMMM yyyy")
        Me.LBTOP.Text = ": " & Bind.Item("JT").ToString
        Me.LBKet.Text = Bind.Item("Ket").ToString
        Me.LBTTPOLk.Text = "(" & MainModule.TTPOLk & ")"
        Me.LBMM1.Text = "(" & MainModule.MM1 & ")"
        Me.LBMM2.Text = "(" & MainModule.MM2 & ")"

        Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ArtCodeH", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.LBGHArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.ArtCodeH", ": {0}")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.ArtName", ": {0}")})
        Me.LBStyle.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.StyleID", ": {0}")})
        Me.LBHCBP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.HCBP", ": {0:n2}")})
        Me.LBQtyD.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Qty", ": {0:n0}")})
        Me.LBUpper.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Upp")})
        Me.LBOutsole.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Outs")})
        Me.LBVariasi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Variasi")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Psg", "{0:n0}")})
        Me.LBHarga.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.HarSat", ": {0:n2}")})

        Me.LBGFTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.HarAkhir", "{0:n2}")})
        SumGFTot.FormatString = "{0:n2}"
        SumGFTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTot.Summary = SumGFTot

        Me.LBRFTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.HarAkhir", "{0:n2}")})
        SumRFTot.FormatString = "{0:n2}"
        SumRFTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTot.Summary = SumRFTot

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
            Me.PageHeight = 2792 '2780
            Me.PageWidth = 2159
            Me.ShowPreview()
        End If
    End Sub

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("POIDDtl").Value = Convert.ToString(GetCurrentColumnValue("POIDD"))
        subRep.Parameters("POID").Value = Convert.ToString(Kode)

    End Sub

    Private Sub XRPOBJLkIntR_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class