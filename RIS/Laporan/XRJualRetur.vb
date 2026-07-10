Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO

Public Class XRJualRetur
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand
    Dim SumBruto As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumOngkir As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumAkhir As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Dim SumRFBruto As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFOngkir As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFAkhir As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(Gol As String)
        cmsl = New SqlDataAdapter("SPLJualRetur", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = MainModule.PilihAwal
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = MainModule.PilihAkhir
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "JualRetur", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Kode", "Kode"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("Kota", "Kota"), New System.Data.Common.DataColumnMapping("TotSbDisc", "TotSbDisc"), New System.Data.Common.DataColumnMapping("TotOngkir", "TotOngkir"), New System.Data.Common.DataColumnMapping("TotDisc", "TotDisc"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir"), New System.Data.Common.DataColumnMapping("TotPsg", "TotPsg")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "JualRetur")

        Me.DataMember = "JualRetur"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHCust})

        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("CustID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBKode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.Kode")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.Tanggal", "{0:dd}")})
        Me.LBGHCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.Cust")})
        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.Cust")})
        Me.LBKota.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.Kota")})
        Me.LBBruto.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotSbDisc", "{0:n2}")})
        Me.LBDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotDisc", "{0:n2}")})
        Me.LBOngkir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotOngkir", "{0:n2}")})
        Me.LBAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotAkhir", "{0:n2}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotPsg", "{0:n1}")})
        'Me.LBGFBruto.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotSbDisc", "{0:n2}")})
        'Me.LBGFDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotDisc", "{0:n2}")})
        'Me.LBGFOngkir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotOngkir", "{0:n2}")})
        'Me.LBGFAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotAkhir", "{0:n2}")})
        'Me.LBGFPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotPsg", "{0:n1}")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFBruto, Me.LBGFDisc, Me.LBGFOngkir, Me.LBGFAkhir, Me.LBGFPsg})

        Me.LBGFBruto.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotSbDisc", "{0:n2}")})
        SumBruto.FormatString = "{0:n2}"
        SumBruto.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFBruto.Summary = SumBruto

        Me.LBGFDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotDisc", "{0:n2}")})
        SumDisc.FormatString = "{0:n2}"
        SumDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFDisc.Summary = SumDisc

        Me.LBGFOngkir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotOngkir", "{0:n2}")})
        SumOngkir.FormatString = "{0:n2}"
        SumOngkir.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFOngkir.Summary = SumOngkir

        Me.LBGFAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotAkhir", "{0:n2}")})
        SumAkhir.FormatString = "{0:n2}"
        SumAkhir.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFAkhir.Summary = SumAkhir

        Me.LBGFPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotPsg", "{0:n1}")})
        SumPsg.FormatString = "{0:n1}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPsg.Summary = SumPsg


        Me.LBRFBruto.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotSbDisc", "{0:n2}")})
        SumRFBruto.FormatString = "{0:n2}"
        SumRFBruto.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFBruto.Summary = SumRFBruto

        Me.LBRFDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotDisc", "{0:n2}")})
        SumRFDisc.FormatString = "{0:n2}"
        SumRFDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFDisc.Summary = SumRFDisc

        Me.LBRFOngkir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotOngkir", "{0:n2}")})
        SumRFOngkir.FormatString = "{0:n2}"
        SumRFOngkir.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFOngkir.Summary = SumRFOngkir

        Me.LBRFAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotAkhir", "{0:n2}")})
        SumRFAkhir.FormatString = "{0:n2}"
        SumRFAkhir.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFAkhir.Summary = SumRFAkhir

        Me.LBRFPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "JualRetur.TotPsg", "{0:n1}")})
        SumRFPsg.FormatString = "{0:n1}"
        SumRFPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFPsg.Summary = SumRFPsg

        Dim command As New SqlCommand("Select Picture From M_Image Where ID = 'P01'", koneksi)
        Dim Pic() As Byte
        Dim newImage As Image

        With koneksi
            .Open()
            Pic = command.ExecuteScalar()
            .Close()
        End With

        'If Pic IsNot Nothing Then
        '    Using ms As New MemoryStream(Pic, 0, Pic.Length)
        '        ms.Write(Pic, 0, Pic.Length)
        '        newImage = Image.FromStream(ms, True)
        '    End Using
        '    Me.PGambar.Image = newImage
        'End If

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRJualRetur_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        'If CDec(Me.LBPsg.Text) < 0 Then
        '    Me.LBTgl.ForeColor = Color.Red
        'Else
        '    Me.LBTgl.ForeColor = Color.Black
        'End If
    End Sub
End Class