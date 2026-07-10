Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRKrtStokBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumMasuk As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumKeluar As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim Gol As String
    Dim Saldo As Integer = 0
    Dim Hal As Integer = 0
    Dim Start As Integer = 0

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("SPLKStokBJ", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = Bind.Item("Gd").ToString
        cmsl.SelectCommand.Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Bind.Item("ArtCode").ToString
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "StokBJ", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("SatID", "SatID"), New System.Data.Common.DataColumnMapping("Isi", "Isi"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("DocID", "DocID"), New System.Data.Common.DataColumnMapping("Masuk", "Masuk"), New System.Data.Common.DataColumnMapping("Keluar", "Keluar"), New System.Data.Common.DataColumnMapping("Ket", "Ket"), New System.Data.Common.DataColumnMapping("SA", "SA"), New System.Data.Common.DataColumnMapping("SAkh", "SAkh")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "StokBJ")

        Me.DataMember = "StokBJ"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = ": " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHArtCode.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSA, Me.LBArtCode, Me.LBArtName, Me.LBSatuan, Me.LBIsi})
        Me.GHArtCode.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ArtCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Row")})
        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.ArtCode", ": {0}")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.ArtName", ": {0}")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.SatID", ": {0}")})
        Me.LBIsi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Isi", ": {0}")})
        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Row")})
        Me.LBSA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.SA", "{0:n0}")})
        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBDocID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.DocID")})
        Me.LBKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Ket")})
        Me.LBMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Masuk", "{0:n0}")})
        Me.LBKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Keluar", "{0:n0}")})
        Me.LBSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.SAkh", "{0:n0}")})

        Me.LBGFMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Masuk", "{0:n0}")})
        SumMasuk.FormatString = "{0:n0}"
        SumMasuk.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMasuk.Summary = SumMasuk

        Me.LBGFKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "StokBJ.Keluar", "{0:n0}")})
        SumKeluar.FormatString = "{0:n0}"
        SumKeluar.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFKeluar.Summary = SumKeluar

        'Me.FilterString = "[ArtCode] In (" & Bind.Item("ArtCode").ToString & ")"

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1650
            Me.PageWidth = 2159
            Me.ShowPreview()
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 3300
            Me.PageWidth = 2159
            Me.ShowPreview()
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRKrtStokBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub GHArtCode_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHArtCode.BeforePrint
        Saldo = 0
        Hal = 0
        Start = 0
    End Sub

    Private Sub LBFcSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBFcSAkh.BeforePrint
        If Start = 1 Then
            If Me.LBSAkh.Text <> "" Then
                Saldo += CInt(Me.LBSAkh.Text)
            End If
        End If

        Me.LBFcSAkh.Text = String.Format("{0:#,##0;(#,##0);0}", Saldo)
    End Sub

    Private Sub LBMasuk_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBMasuk.BeforePrint
        If Start <> 1 Then
            Saldo += CInt(Me.LBMasuk.Text)
        End If
    End Sub

    Private Sub LBKeluar_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBKeluar.BeforePrint
        If Start <> 1 Then
            Saldo -= CInt(Me.LBKeluar.Text)
        End If
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
            Me.LBGFSA.Text = String.Format("{0:#,##0;(#,##0);0}", CDec(Me.LBSA.Text))
        End If
    End Sub

    Private Sub LBGFSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSAkh.BeforePrint
        Me.LBGFSAkh.Text = String.Format("{0:#,##0;(#,##0);0}", Saldo)
    End Sub

End Class