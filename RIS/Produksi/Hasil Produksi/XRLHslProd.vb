Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRLHslProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        If Bind.Item("Jam").ToString = "Pilih Jam" Then
            cmsl = New SqlDataAdapter("SPLHslProd", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Proses", SqlDbType.VarChar).Value = Bind.Item("Proses").ToString
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
            cmsl.SelectCommand.Parameters.Add("@JamAw", SqlDbType.Int).Value = MainModule.PilihJamKeAw
            cmsl.SelectCommand.Parameters.Add("@JamAkh", SqlDbType.Int).Value = MainModule.PilihJamKeAkh
            cmsl.SelectCommand.Parameters.Add("@Shiift", SqlDbType.VarChar).Value = MainModule.PilihShift

            Me.LBPeriode.Text = ": " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " " & MainModule.PilihJamAw & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy") & " " & MainModule.PilihJamAkh & ""
        Else
            cmsl = New SqlDataAdapter("SPLHslProdHari", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.Parameters.Add("@Proses", SqlDbType.VarChar).Value = Bind.Item("Proses").ToString
            cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.VarChar).Value = CDate(MainModule.PilihAwal)
            cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.VarChar).Value = CDate(MainModule.PilihAkhir)

            Me.LBPeriode.Text = ": " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        End If

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_HslProdDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SPK", "SPK"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Ass", "Ass"), New System.Data.Common.DataColumnMapping("QtyBOM", "QtyBOM"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("QtySelesai", "QtySelesai"), New System.Data.Common.DataColumnMapping("Saldo", "Saldo")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_HslProdDtl")

        Me.DataMember = "T_HslProdDtl"
        Me.DataSource = DsLap


        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHProses.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Proses", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHLine.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Line", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBProses.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Proses", "Proses : {0}")})
        Me.LBLine.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Line", ": {0}")})
        Me.LBBOM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.BOMID")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.ArtName")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Warna")})
        Me.LBUk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Ass")})
        Me.LBQtySPK.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.QtyBOM", "{0:#,##0;(#,##0);0}")})
        Me.LBSelesai.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.QtySelesai", "{0:#,##0;(#,##0);0}")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Qty", "{0:#,##0;(#,##0);0}")})
        Me.LBSaldo.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Saldo", "{0:#,##0;(#,##0);0}")})
        Me.LBTKN.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.TKN", "{0:#,##0;(#,##0);""}")})
        Me.LBJamN.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.JamN", "{0:#,##0;(#,##0);""}")})
        Me.LBTKL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.TKL", "{0:#,##0;(#,##0);""}")})
        Me.LBJamL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.JamL", "{0:#,##0;(#,##0);""}")})


        Me.LBTotQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HslProdDtl.Qty", "{0:#,##0;(#,##0);0}")})
        SumQty.FormatString = "{0:#,##0;(#,##0);0}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotQty.Summary = SumQty

        Me.ShowPreview()
    End Sub

    Private Sub XRBPB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class