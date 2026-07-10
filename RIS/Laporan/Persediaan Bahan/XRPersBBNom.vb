Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPersBBNom
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQtystsGerak As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumSaldostsGerak As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQtyJnsID As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumSaldoJnsID As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("SPLPersBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = MainModule.PilihGudangID
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "PersBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("JnsID", "JnsID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("stsGerak", "stsGerak"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("SA", "SA"), New System.Data.Common.DataColumnMapping("Masuk", "Masuk"), New System.Data.Common.DataColumnMapping("Keluar", "Keluar"), New System.Data.Common.DataColumnMapping("SAkh", "SAkh"), New System.Data.Common.DataColumnMapping("SalAw", "SalAw"), New System.Data.Common.DataColumnMapping("NilMasuk", "NilMasuk"), New System.Data.Common.DataColumnMapping("NilKeluar", "NilKeluar"), New System.Data.Common.DataColumnMapping("SalAkh", "SalAkh"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "PersBB")

        Me.DataMember = "PersBB"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBGudang.Text = "Gudang : " & MainModule.PilihGudang
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHJnsID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHJnsID})
        Me.GHJnsID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("JnsID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHstsGerak.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHstsGerak})
        Me.GHstsGerak.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("stsGerak", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHJnsID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Jenis")})
        Me.LBGHstsGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.stsGerak")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SAkh", "{0:n2}")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.HarSat", "{0:n2}")})
        Me.LBSaldo.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SalAkh", "{0:n2}")})
        Me.LBGFJenisID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Jenis", "Total {0}")})

        Me.GFstsGerak.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFstsGerak, Me.LBGFStsGerakQty, Me.LBGFStsGerakSaldo})
        Me.GFJnsID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFJenisID, Me.LBGFJnsIDQty, Me.LBGFJnsIDSaldo})

        Me.LBGFStsGerakQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SAkh", "{0:n2}")})
        SumQtystsGerak.FormatString = "{0:n2}"
        SumQtystsGerak.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFStsGerakQty.Summary = SumQtystsGerak

        Me.LBGFStsGerakSaldo.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SalAkh", "{0:n2}")})
        SumSaldostsGerak.FormatString = "{0:n2}"
        SumSaldostsGerak.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFStsGerakSaldo.Summary = SumSaldostsGerak

        Me.LBGFJnsIDQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SAkh", "{0:n2}")})
        SumQtyJnsID.FormatString = "{0:n2}"
        SumQtyJnsID.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFJnsIDQty.Summary = SumQtyJnsID

        Me.LBGFJnsIDSaldo.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SalAkh", "{0:n2}")})
        SumSaldoJnsID.FormatString = "{0:n2}"
        SumSaldoJnsID.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFJnsIDSaldo.Summary = SumSaldoJnsID

        Me.FilterString = "[JnsID] In (" & Bind.Item("JnsID").ToString & ") and ([stsGerak] Like '" & Bind.Item("stsGerak").ToString & "' or [stsGerak]=null)"

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPersBBNom_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub LBGHstsGerak_TextChanged(sender As Object, e As EventArgs) Handles LBGHstsGerak.TextChanged
        Me.LBGFstsGerak.Text = "Total " & Me.LBGHJnsID.Text & " " & Me.LBGHstsGerak.Text
    End Sub
End Class