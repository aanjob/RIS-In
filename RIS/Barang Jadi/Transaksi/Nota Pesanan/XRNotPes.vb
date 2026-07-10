Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRNotPes
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumHarSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotSbDisc, TotDos, Sisa As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.ArtCode + ' ( ' + B.ArtName + ' )' As Barang, HarSat, DosCab+DosPst As Dos, PsgCab+PsgPst As Psg, HarSbDisc,DiscOB,Ongkir, HarAkhir From T_NotPesDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Bind.Item("Kode").ToString & "' and PsgCab+PsgPst >0", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_NotPesDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Barang", "Barang"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("Dos", "Dos"), New System.Data.Common.DataColumnMapping("Psg", "Psg"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc"), New System.Data.Common.DataColumnMapping("DiscOB", "DiscOB"), New System.Data.Common.DataColumnMapping("Ongkir", "Ongkir"), New System.Data.Common.DataColumnMapping("HarAkhir", "HarAkhir")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_NotPesDtl")

        Me.DataMember = "T_NotPesDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = "Tanggal : " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = ": " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBCust2.Text = Bind.Item("Cust").ToString
        Me.LBAlamat.Text = ": " & Bind.Item("Alamat").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBTotSbDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotSbDisc").ToString))
        Me.LBTotDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotDisc").ToString))
        Me.LBGrandTot.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotAkhir").ToString))

        Me.LBBarang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Barang")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Dos")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Psg")})
        Me.LBOB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.DiscOB", "{0:#}")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Dos", "{0:#}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Psg", "{0:#}")})
        Me.LBHarga.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.HarSat", "{0:n2}")})
        Me.LBHarSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.HarSbDisc", "{0:n2}")})

        Me.LBTotSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.HarSbDisc", "{0:n2}")})
        SumHarSbDisc.FormatString = "{0:n2}"
        SumHarSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotSbDisc.Summary = SumHarSbDisc

        Me.LBTotDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Dos", "{0:#}")})
        SumDos.FormatString = "{0:#}"
        SumDos.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDos.Summary = SumDos

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_NotPesDtl.Psg", "{0:#}")})
        SumPsg.FormatString = "{0:#}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotPsg.Summary = SumPsg

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select URLPic From M_ImageUrl Where ID='" & Bind.Item("Kode").ToString & "'", koneksi)
        Dim namaFile As String

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        namaFile = ""
                    Else
                        namaFile = Reader.Item(0)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        If namaFile <> "" Then
            Me.PFoto.ImageUrl = namaFile
        End If

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
            Me.LBTotDisc.Visible = True
            Me.LBGrandTot.Visible = True

        Else
            Me.LBJdlPromo.Visible = False
            Me.LBJdlGrandTotal.Visible = False
            Me.LBT22.Visible = False
            Me.LBT24.Visible = False
            Me.LBTotDisc.Visible = False
            Me.LBGrandTot.Visible = False
        End If
    End Sub
End Class