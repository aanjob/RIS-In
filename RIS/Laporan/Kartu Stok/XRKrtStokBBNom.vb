Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRKrtStokBBNom
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumMasuk As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumKeluar As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumNomMasuk As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumNomKeluar As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim Gol As String
    Dim Saldo As Decimal = 0
    Dim Nominal As Decimal = 0
    Dim Hal As Decimal = 0
    Dim Start As Decimal = 0

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("SPLKStokBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = Bind.Item("Gd").ToString
        cmsl.SelectCommand.Parameters.Add("@BBID", SqlDbType.VarChar).Value = Bind.Item("BBID").ToString
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "StokBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("DocID", "DocID"), New System.Data.Common.DataColumnMapping("Masuk", "Masuk"), New System.Data.Common.DataColumnMapping("Keluar", "Keluar"), New System.Data.Common.DataColumnMapping("NilMasuk", "NilMasuk"), New System.Data.Common.DataColumnMapping("NilKeluar", "NilKeluar"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("Ket", "Ket"), New System.Data.Common.DataColumnMapping("SA", "SA"), New System.Data.Common.DataColumnMapping("SAkh", "SAkh")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "StokBB")

        Me.DataMember = "StokBB"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = ": " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHBBID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSA, Me.LBBBID, Me.LBBahan, Me.LBSatuan})
        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Row")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.BBID", ": {0}")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Bahan", ": {0}")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Sat", ": {0}")})
        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Row")})
        Me.LBSA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.SA", "{0:n2}")})
        Me.LBSalAw.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.SalAw", "{0:n2}")})
        Me.LBHarSatAw.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSatAw", "{0:n2}")})
        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBDocID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.DocID")})
        Me.LBKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Ket")})
        Me.LBMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Masuk", "{0:n2}")})
        Me.LBKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Keluar", "{0:n2}")})
        Me.LBNomMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.NilMasuk", "{0:n2}")})
        Me.LBNomKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.NilKeluar", "{0:n2}")})
        Me.LBHarSatMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSat", "{0:n2}")})
        Me.LBHarSatKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSat", "{0:n2}")})
        Me.LBSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.SAkh", "{0:n2}")})
        Me.LBNomSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.SalAkh", "{0:n2}")})
        Me.LBGFHarSatSA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSatAw", "{0:n2}")})
        Me.LBGFHarSatMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSat", "{0:n2}")})
        Me.LBGFHarSatKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSat", "{0:n2}")})
        Me.LBGFHarSatSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HarSat", "{0:n2}")})
        Me.LBHarSatSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.HSNP", "{0:n2}")})

        Me.LBGFMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Masuk", "{0:n2}")})
        SumMasuk.FormatString = "{0:n2}"
        SumMasuk.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMasuk.Summary = SumMasuk

        Me.LBGFKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.Keluar", "{0:n2}")})
        SumKeluar.FormatString = "{0:n2}"
        SumKeluar.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFKeluar.Summary = SumKeluar

        Me.LBGFNomMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.NilMasuk", "{0:n2}")})
        SumNomMasuk.FormatString = "{0:n2}"
        SumNomMasuk.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMasuk.Summary = SumNomMasuk

        Me.LBGFNomKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBB.NilKeluar", "{0:n2}")})
        SumNomKeluar.FormatString = "{0:n2}"
        SumNomKeluar.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFKeluar.Summary = SumNomKeluar

        'Me.FilterString = "[BBID] In (" & Bind.Item("BBID").ToString & ")"

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRKrtStokBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub GHBBID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHBBID.BeforePrint
        Saldo = 0
        Nominal = 0
        Hal = 0
        Start = 0
    End Sub

    Private Sub LBFcSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBFcSAkh.BeforePrint
        If Start = 1 Then
            If Me.LBSAkh.Text <> "" Then
                Saldo += CDec(Me.LBSAkh.Text)
            End If
        End If
        Me.LBFcSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
    End Sub

    Private Sub LBFcNomSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBFcNomSAkh.BeforePrint
        If Start = 1 Then
            If Me.LBNomSAkh.Text <> "" Then
                Nominal += CDec(Me.LBNomSAkh.Text)
            End If
        End If
        Me.LBFcNomSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Nominal)
    End Sub

    Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged

        If Start <> 1 Then
            'If Me.LBNomKeluar.Text <> "" Then
            '    Nominal += CDec(Me.LBNomMasuk.Text) - CDec(Me.LBNomKeluar.Text)
            'End If

            If Me.LBMasuk.Text <> "" Then
                Saldo += CDec(Me.LBMasuk.Text) - CDec(Me.LBKeluar.Text)
            End If
        End If

        'Me.LBFcSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
        'Me.LBFcNomSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Nominal)
    End Sub


    Private Sub LBNomMasuk_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBNomMasuk.BeforePrint
        If Start <> 1 Then
            Nominal += CDec(Me.LBNomMasuk.Text)
        End If
    End Sub

    Private Sub LBNomKeluar_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBNomKeluar.BeforePrint
        If Start <> 1 Then
            Nominal -= CDec(Me.LBNomKeluar.Text)
        End If
    End Sub

    Private Sub LBMasuk_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBMasuk.BeforePrint
        'If Start <> 1 Then
        '    Saldo += CDec(Me.LBMasuk.Text)
        '    Nominal += CDec(Me.LBNomMasuk.Text)
        'End If
    End Sub

    Private Sub LBKeluar_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBKeluar.BeforePrint
        'If Start <> 1 Then
        '    Saldo -= CDec(Me.LBKeluar.Text)
        '    Nominal -= CDec(Me.LBNomKeluar.Text)
        'End If
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Start += 1
    End Sub

    Private Sub PageHeader_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles PageHeader.BeforePrint
        Hal += 1

        If Hal = 1 Then
            Me.PageHeader.Visible = False
        Else
            Me.PageHeader.Visible = True
        End If
    End Sub

    Private Sub LBGFSA_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSA.BeforePrint
        If Me.LBSA.Text <> "" Then
            Me.LBGFSA.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", CDec(Me.LBSA.Text))
        End If
    End Sub

    Private Sub LBGFNomSA_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFNomSA.BeforePrint
        If Me.LBSalAw.Text <> "" Then
            Me.LBGFNomSA.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", CDec(Me.LBSalAw.Text))
        End If
    End Sub


    Private Sub LBGFSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSAkh.BeforePrint
        Me.LBGFSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
    End Sub

    Private Sub LBGFNomSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFNomSAkh.BeforePrint
        Me.LBGFNomSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Nominal)
    End Sub

End Class