Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRLapProd3
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl, cmsl2 As SqlDataAdapter
    Dim SumGFQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttUppBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttBot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFCuttBotBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSeri As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSeriBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFFinishUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFFinishUppBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFInsert As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFInsertBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPhylon As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPhylonBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFInject As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFInjectBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFInsock As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFInsockBal As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
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

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SPLHProdDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SPK", "SPK"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("TotPsg", "TotPsg"), New System.Data.Common.DataColumnMapping("CuttUpp", "CuttUpp"), New System.Data.Common.DataColumnMapping("Seri", "Seri"), New System.Data.Common.DataColumnMapping("SeriBal", "SeriBal"), New System.Data.Common.DataColumnMapping("FinishUpp", "FinishUpp"), New System.Data.Common.DataColumnMapping("FinishUppBal", "FinishUppBal"), New System.Data.Common.DataColumnMapping("Insertt", "Insertt"), New System.Data.Common.DataColumnMapping("InserttBal", "InserttBal"), New System.Data.Common.DataColumnMapping("Phylon", "Phylon"), New System.Data.Common.DataColumnMapping("PhylonBal", "PhylonBal"), New System.Data.Common.DataColumnMapping("Inject", "Inject"), New System.Data.Common.DataColumnMapping("InjectBal", "InjectBal"), New System.Data.Common.DataColumnMapping("Insock", "Insock"), New System.Data.Common.DataColumnMapping("InsockBal", "InsockBal"), New System.Data.Common.DataColumnMapping("Ass", "Ass"), New System.Data.Common.DataColumnMapping("AssBal", "AssBal"), New System.Data.Common.DataColumnMapping("Finish", "Finish"), New System.Data.Common.DataColumnMapping("FinishBal", "FinishBal"), New System.Data.Common.DataColumnMapping("Pack", "Pack"), New System.Data.Common.DataColumnMapping("PackBal", "PackBal")})})

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
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.TotPsg", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBCuttUpTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUpp", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBCuttUpBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBCutUpTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBCuttBotTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBott", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBCuttBotBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBCuttBotTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBSeriTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Seri", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBSeriBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBSeriTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBFinishUppTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishUpp", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBFinishUppBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishUppBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBFinishUppTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishUppTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBInsertTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Insertt", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBInsertBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InserttBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBInsertTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InserttTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBPhylonTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Phylon", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBPhylonBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PhylonBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBPhylonTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PhylonTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBInjectTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Inject", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBInjectBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InjectBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBInjectTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InjectTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBInsockTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Insock", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBInsockBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InsockBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBInsockTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InsockTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBFinishTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Finish", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBFinishBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBFinishTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBPackTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Pack", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBPackBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackBal", "{0:#,##0.0;(#,##0.0);0}")})
        Me.LBPackTar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackTar", "{0:#,##0.0;(#,##0.0);""}")})

        Me.LBGFQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.TotPsg", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFQty.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQty.Summary = SumGFQty

        Me.LBGFCuttUpTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUpp", "{0:#,##0.00;(#,##0.00);""}")})
        SumGFCuttUpp.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumGFCuttUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttUpTd.Summary = SumGFCuttUpp

        Me.LBGFCuttUpBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFCuttUppBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFCuttUppBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttUpBal.Summary = SumGFCuttUppBal

        Me.LBGFCuttBotTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBott", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFCuttBot.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFCuttBot.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttBotTd.Summary = SumGFCuttBot

        Me.LBGFCuttBotBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFCuttBotBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFCuttBotBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCuttBotBal.Summary = SumGFCuttBotBal

        Me.LBGFSeriTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Seri", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFSeri.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFSeri.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSeriTd.Summary = SumGFSeri

        Me.LBGFSeriBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFSeriBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFSeriBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSeriBal.Summary = SumGFSeriBal

        Me.LBGFFinishUppTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishUpp", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFFinishUpp.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFFinishUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFFinishUppTd.Summary = SumGFFinishUpp

        Me.LBGFFinishUppBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishUppBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFFinishUppBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFFinishUppBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFFinishUppBal.Summary = SumGFFinishUppBal

        Me.LBGFInsertTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Insertt", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFInsert.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFInsert.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFInsertTd.Summary = SumGFInsert

        Me.LBGFInsertBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InserttBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFInsertBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFInsertBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFInsertBal.Summary = SumGFInsertBal

        Me.LBGFPhylonTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Phylon", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFPhylon.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFPhylon.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPhylonTd.Summary = SumGFPhylon

        Me.LBGFPhylonBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PhylonBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFPhylonBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFPhylonBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPhylonBal.Summary = SumGFPhylonBal

        Me.LBGFInjectTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Inject", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFInject.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFInject.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFInjectTd.Summary = SumGFInject

        Me.LBGFInjectBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InjectBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFInjectBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFInjectBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFInjectBal.Summary = SumGFInjectBal

        Me.LBGFInsockTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Insock", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFInsock.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFInsock.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFInsockTd.Summary = SumGFInsock

        Me.LBGFInsockBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InsockBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFInsockBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFInsockBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFInsockBal.Summary = SumGFInsockBal

        Me.LBGFFinishTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Finish", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFFinish.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFFinish.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFFinishTd.Summary = SumGFFinish

        Me.LBGFFinishBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFFinishBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
        SumGFFinishBal.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFFinishBal.Summary = SumGFFinishBal

        Me.LBGFPackTd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.Pack", "{0:#,##0.0;(#,##0.0);""}")})
        SumGFPack.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumGFPack.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPackTd.Summary = SumGFPack

        Me.LBGFPackBal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackBal", "{0:#,##0.0;(#,##0.0);0}")})
        SumGFPackBal.FormatString = "{0:#,##0.0;(#,##0.0);0}"
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