Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRLapProd1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl, cmsl2 As SqlDataAdapter
    Dim SumGFQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttUppBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttBot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttBotBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSeri As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSeriBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSabUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSabUppBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSabIns As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSabInsBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFJhtK As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFJhtKBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFJht As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFJhtBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSole As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSoleBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFAss As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFAssBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFFinish As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFFinishBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPack As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPackBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
  
    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLHProdDtl", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = MainModule.PilihAwal
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = MainModule.PilihAkhir
        cmsl.SelectCommand.Parameters.Add("@Unit", SqlDbType.VarChar).Value = MainModule.PilihUnit

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SPLHProdDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SPK", "SPK"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("TotPsg", "TotPsg"), New System.Data.Common.DataColumnMapping("CuttUpp", "CuttUpp"), New System.Data.Common.DataColumnMapping("CuttUppBal", "CuttUppBal"), New System.Data.Common.DataColumnMapping("CuttBott", "CuttBott"), New System.Data.Common.DataColumnMapping("CuttBottBal", "CuttBottBal"), New System.Data.Common.DataColumnMapping("Seri", "Seri"), New System.Data.Common.DataColumnMapping("SeriBal", "SeriBal"), New System.Data.Common.DataColumnMapping("SabUpp", "SabUpp"), New System.Data.Common.DataColumnMapping("SabUppBal", "SabUppBal"), New System.Data.Common.DataColumnMapping("SabIns", "SabIns"), New System.Data.Common.DataColumnMapping("SabInsBal", "SabInsBal"), New System.Data.Common.DataColumnMapping("JhtKomp", "JhtKomp"), New System.Data.Common.DataColumnMapping("JhtKompBal", "JhtKompBal"), New System.Data.Common.DataColumnMapping("JhtUpp", "JhtUpp"), New System.Data.Common.DataColumnMapping("JhtUppBal", "JhtUppBal"), New System.Data.Common.DataColumnMapping("Insock", "Insock"), New System.Data.Common.DataColumnMapping("InsockBal", "InsockBal"), New System.Data.Common.DataColumnMapping("Insole", "Insole"), New System.Data.Common.DataColumnMapping("InsoleBal", "InsoleBal"), New System.Data.Common.DataColumnMapping("Outsole", "Outsole"), New System.Data.Common.DataColumnMapping("OutsoleBal", "OutsoleBal"), New System.Data.Common.DataColumnMapping("Insertt", "Insertt"), New System.Data.Common.DataColumnMapping("InserttBal", "InserttBal"), New System.Data.Common.DataColumnMapping("Inject", "Inject"), New System.Data.Common.DataColumnMapping("InjectBal", "InjectBal"), New System.Data.Common.DataColumnMapping("Ass", "Ass"), New System.Data.Common.DataColumnMapping("AssBal", "AssBal"), New System.Data.Common.DataColumnMapping("Finish", "Finish"), New System.Data.Common.DataColumnMapping("FinishBal", "FinishBal"), New System.Data.Common.DataColumnMapping("Pack", "Pack"), New System.Data.Common.DataColumnMapping("PackBal", "PackBal"), New System.Data.Common.DataColumnMapping("Phylon", "Phylon"), New System.Data.Common.DataColumnMapping("PhylonBal", "PhylonBal")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "SPLHProdDtl")

        Me.DataMember = "SPLHProdDtl"
        Me.DataSource = DsLap

        Me.LBUnit.Text = "Unit : " & MainModule.PilihUnit

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTanggal})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Tanggal", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Tanggal", ": {0:dd MMMM yyyy}")})
        Me.LBSPK.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SPK")})
        Me.LBStyle.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.ArtName")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Warna")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.TotPsg", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBCuttUpTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUpp", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBCuttUpBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBCutUpTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBCuttBotTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBott", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBCuttBotBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBCuttBotTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBSeriTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Seri", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBSeriBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBSeriTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBSabUpTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabUpp", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBSabUpBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabUppBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBSabUpTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabUppTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBSabInsTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabIns", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBSabInsBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabInsBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBSabInsTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabInsTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBJhtKTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtKomp", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBJhtKBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtKompBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBJhtKTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtKompTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBJhtTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtUpp", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBJhtBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtUppBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBJhtTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtUppTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBSoleTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Outsole", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBSoleBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.OutsoleBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBSoleTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.OutsoleTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBAssTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Ass", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBAssBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.AssBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBAssTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.AssTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBPackTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Pack", "{0:#,#0.#;(#,#0.#);""}")})
        Me.LBPackBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackBal", "{0:#,#0.#;(#,#0.#);0}")})
        Me.LBPackTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackTar", "{0:#,#0.#;(#,#0.#);""}")})

        Me.LBGFQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.TotPsg", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFQty.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQty.Summary = SumGFQty

        Me.LBGFCuttUpTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUpp", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFCuttUpp.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFCuttUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttUpTd.Summary = SumGFCuttUpp

        Me.LBGFCuttUpBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFCuttUppBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFCuttUppBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttUpBal.Summary = SumGFCuttUppBal

        Me.LBGFCuttBotTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBott", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFCuttBot.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFCuttBot.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttBotTd.Summary = SumGFCuttBot

        Me.LBGFCuttBotBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFCuttBotBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFCuttBotBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttBotBal.Summary = SumGFCuttBotBal

        Me.LBGFSeriTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Seri", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFSeri.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFSeri.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSeriTd.Summary = SumGFSeri

        Me.LBGFSeriBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFSeriBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFSeriBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSeriBal.Summary = SumGFSeriBal

        Me.LBGFSabUpTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabUpp", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFSabUpp.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFSabUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSabUpTd.Summary = SumGFSabUpp

        Me.LBGFSabUpBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabUppBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFSabUppBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFSabUppBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSabUpBal.Summary = SumGFSabUppBal

        Me.LBGFSabInsTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabIns", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFSabIns.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFSabIns.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSabInsTd.Summary = SumGFSabIns

        Me.LBGFSabInsBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabInsBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFSabInsBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFSabInsBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSabInsBal.Summary = SumGFSabInsBal

        Me.LBGFJhtKTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtKomp", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFJhtK.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFJhtK.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFJhtKTd.Summary = SumGFJhtK

        Me.LBGFJhtKBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtKompBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFJhtKBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFJhtKBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFJhtKBal.Summary = SumGFJhtKBal

        Me.LBGFJhtTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtUpp", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFJht.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFJht.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFJhtTd.Summary = SumGFJht

        Me.LBGFJhtBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtUppBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFJhtBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFJhtBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFJhtBal.Summary = SumGFJhtBal

        Me.LBGFSoleTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Insole", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFSole.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFSole.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSoleTd.Summary = SumGFSole

        Me.LBGFSoleBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InsoleBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFSoleBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFSoleBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSoleBal.Summary = SumGFSoleBal

        Me.LBGFAssTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Ass", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFAss.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFAss.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFAssTd.Summary = SumGFAss

        Me.LBGFAssBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.AssBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFAssBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFAssBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFAssBal.Summary = SumGFAssBal

        Me.LBGFPackTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Pack", "{0:#,#0.#;(#,#0.#);""}")})
        SumGFPack.FormatString = "{0:#,#0.#;(#,#0.#);""}"
        SumGFPack.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPackTd.Summary = SumGFPack

        Me.LBGFPackBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackBal", "{0:#,#0.#;(#,#0.#);0}")})
        SumGFPackBal.FormatString = "{0:#,#0.#;(#,#0.#);0}"
        SumGFPackBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPackBal.Summary = SumGFPackBal

        Me.ShowPreview()
    End Sub

    Private Sub XRLapProd1_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub XSRTKL_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XSRTKL.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("Tanggal").Value = Convert.ToString(GetCurrentColumnValue("Tanggal"))
        subRep.Parameters("Unit").Value = Convert.ToString(MainModule.PilihUnit)
    End Sub

    Private Sub XSRKet_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XSRKet.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("Tanggal").Value = Convert.ToString(GetCurrentColumnValue("Tanggal"))
        subRep.Parameters("Unit").Value = Convert.ToString(MainModule.PilihUnit)
    End Sub

End Class