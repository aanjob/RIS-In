Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Public Class XRDetTKL3
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumRFCuttUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFCuttBot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSeri As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSabUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFFinishUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFInsert As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPhylon As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFInject As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFInsock As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFFinish As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPack As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFGrandTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData()
    End Sub

    Private Sub XRPNPsvMdl_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        cmsl = New SqlDataAdapter("SPLHProdTKL", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Tgl", SqlDbType.Date).Value = CDate(Tanggal.Value)
        cmsl.SelectCommand.Parameters.Add("@Unit", SqlDbType.VarChar).Value = CStr(Unit.Value)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SPLHProdTKL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Kategori", "Kategori"), New System.Data.Common.DataColumnMapping("JnsJam", "JnsJam"), New System.Data.Common.DataColumnMapping("CuttUppJam", "CuttUppJam"), New System.Data.Common.DataColumnMapping("CuttUppOrg", "CuttUppOrg"), New System.Data.Common.DataColumnMapping("CuttUppUM", "CuttUppUM"), New System.Data.Common.DataColumnMapping("CuttUppTot", "CuttUppTot"), New System.Data.Common.DataColumnMapping("CuttBottJam", "CuttBottJam"), New System.Data.Common.DataColumnMapping("CuttBottOrg", "CuttBottOrg"), New System.Data.Common.DataColumnMapping("CuttBottUM", "CuttBottUM"), New System.Data.Common.DataColumnMapping("CuttBottTot", "CuttBottTot"), New System.Data.Common.DataColumnMapping("SeriJam", "SeriJam"), New System.Data.Common.DataColumnMapping("SeriOrg", "SeriOrg"), New System.Data.Common.DataColumnMapping("SeriUM", "SeriUM"), New System.Data.Common.DataColumnMapping("SeriTot", "SeriTot"), New System.Data.Common.DataColumnMapping("SabUppJam", "SabUppJam"), New System.Data.Common.DataColumnMapping("SabUppOrg", "SabUppOrg"), New System.Data.Common.DataColumnMapping("SabUppUM", "SabUppUM"), New System.Data.Common.DataColumnMapping("SabUppTot", "SabUppTot"), New System.Data.Common.DataColumnMapping("SabInsJam", "SabInsJam"), New System.Data.Common.DataColumnMapping("SabInsOrg", "SabInsOrg"), New System.Data.Common.DataColumnMapping("SabInsUM", "SabInsUM"), New System.Data.Common.DataColumnMapping("SabInsTot", "SabInsTot"), New System.Data.Common.DataColumnMapping("JhtKompJam", "JhtKompJam"), New System.Data.Common.DataColumnMapping("JhtKompOrg", "JhtKompOrg"), New System.Data.Common.DataColumnMapping("JhtKompUM", "JhtKompUM"), New System.Data.Common.DataColumnMapping("JhtKompTot", "JhtKompTot"), New System.Data.Common.DataColumnMapping("JhtUppJam", "JhtUppJam"), New System.Data.Common.DataColumnMapping("JhtUppOrg", "JhtUppOrg"), New System.Data.Common.DataColumnMapping("JhtUppUM", "JhtUppUM"), New System.Data.Common.DataColumnMapping("JhtUppTot", "JhtUppTot"), New System.Data.Common.DataColumnMapping("FinishUppJam", "FinishUppJam"), New System.Data.Common.DataColumnMapping("FinishUppOrg", "FinishUppOrg"), New System.Data.Common.DataColumnMapping("FinishUppUM", "FinishUppUM"), New System.Data.Common.DataColumnMapping("FinishUppTot", "FinishUppTot"), New System.Data.Common.DataColumnMapping("InsoleJam", "InsoleJam"), New System.Data.Common.DataColumnMapping("InsoleOrg", "InsoleOrg"), New System.Data.Common.DataColumnMapping("InsoleUM", "InsoleUM"), New System.Data.Common.DataColumnMapping("InsoleTot", "InsoleTot"), New System.Data.Common.DataColumnMapping("OutsoleJam", "OutsoleJam"), New System.Data.Common.DataColumnMapping("OutsoleOrg", "OutsoleOrg"), New System.Data.Common.DataColumnMapping("OutsoleUM", "OutsoleUM"), New System.Data.Common.DataColumnMapping("OutsoleTot", "OutsoleTot"), New System.Data.Common.DataColumnMapping("InserttJam", "InserttJam"), New System.Data.Common.DataColumnMapping("InserttOrg", "InserttOrg"), New System.Data.Common.DataColumnMapping("InserttUM", "InserttUM"), New System.Data.Common.DataColumnMapping("InserttTot", "InserttTot"), New System.Data.Common.DataColumnMapping("InjectJam", "InjectJam"), New System.Data.Common.DataColumnMapping("InjectOrg", "InjectOrg"), New System.Data.Common.DataColumnMapping("InjectUM", "InjectUM"), New System.Data.Common.DataColumnMapping("InjectTot", "InjectTot"), New System.Data.Common.DataColumnMapping("InsockJam", "InsockJam"), New System.Data.Common.DataColumnMapping("InsockOrg", "InsockOrg"), New System.Data.Common.DataColumnMapping("InsockUM", "InsockUM"), New System.Data.Common.DataColumnMapping("InsockTot", "InsockTot"), New System.Data.Common.DataColumnMapping("AssJam", "AssJam"), New System.Data.Common.DataColumnMapping("AssOrg", "AssOrg"), New System.Data.Common.DataColumnMapping("AssUM", "AssUM"), New System.Data.Common.DataColumnMapping("AssTot", "AssTot"), New System.Data.Common.DataColumnMapping("FinishJam", "FinishJam"), New System.Data.Common.DataColumnMapping("FinishOrg", "FinishOrg"), New System.Data.Common.DataColumnMapping("FinishUM", "FinishUM"), New System.Data.Common.DataColumnMapping("FinishTot", "FinishTot"), New System.Data.Common.DataColumnMapping("PackJam", "PackJam"), New System.Data.Common.DataColumnMapping("PackOrg", "PackOrg"), New System.Data.Common.DataColumnMapping("PackUM", "PackUM"), New System.Data.Common.DataColumnMapping("PackTot", "PackTot"), New System.Data.Common.DataColumnMapping("PhylonJam", "PhylonJam"), New System.Data.Common.DataColumnMapping("PhylonOrg", "PhylonOrg"), New System.Data.Common.DataColumnMapping("PhylonUM", "PhylonUM"), New System.Data.Common.DataColumnMapping("PhylonTot", "PhylonTot"), New System.Data.Common.DataColumnMapping("GrandTot", "GrandTot")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "SPLHProdTKL")

        Me.DataMember = "SPLHProdTKL"
        Me.DataSource = DsLap

        Me.LBKat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.Kategori")})
        Me.LBJnsJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JnsJam")})

        Me.LBCuttUpJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppJam")})
        Me.LBCuttUpOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBCuttUpUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBCuttUpTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBCuttBotJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottJam")})
        Me.LBCuttBotOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBCuttBotUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBCuttBotTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBSeriJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriJam")})
        Me.LBSeriOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBSeriUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBSeriTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBFinishUppJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishUppJam")})
        Me.LBFinishUppOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishUppOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBFinishUppUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishUppUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBFinishUppTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishUppTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBInsertJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InserttJam")})
        Me.LBInsertOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InserttOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBInsertUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InserttUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBInsertTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InserttTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBPhylonJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PhylonJam")})
        Me.LBPhylonOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PhylonOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBPhylonUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PhylonUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBPhylonTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PhylonTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBInjectJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InjectJam")})
        Me.LBInjectOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InjectOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBInjectUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InjectUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBInjectTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InjectTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBInsockJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InsockJam")})
        Me.LBInsockOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InsockOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBInsockUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InsockUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBInsockTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.InsockTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBFinishJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishJam")})
        Me.LBFinishOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBFinishUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBFinishTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.FinishTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBPackJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackJam")})
        Me.LBPackOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBPackUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackUM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBPackTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBGrandTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.GrandTot", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBRFCuttUpTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFCuttUpp.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFCuttUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFCuttUpTot.Summary = SumRFCuttUpp

        Me.LBRFCuttBotTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFCuttBot.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFCuttBot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFCuttBotTot.Summary = SumRFCuttBot

        Me.LBRFSeriTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFSeri.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFSeri.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSeriTot.Summary = SumRFSeri

        Me.LBRFFinishUppTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishUppTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFFinishUpp.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFFinishUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFFinishUppTot.Summary = SumRFFinishUpp

        Me.LBRFInsertTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InserttTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFInsert.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFInsert.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFInsertTot.Summary = SumRFInsert

        Me.LBRFPhylonTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PhylonTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFPhylon.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFPhylon.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFPhylonTot.Summary = SumRFPhylon

        Me.LBRFInjectTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InjectTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFInject.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFInject.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFInjectTot.Summary = SumRFInject

        Me.LBRFInsockTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.InsockTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFInsock.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFInsock.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFInsockTot.Summary = SumRFInsock

        Me.LBRFFinishTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.FinishTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFFinish.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFFinish.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFFinishTot.Summary = SumRFFinish

        Me.LBRFPackTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFPack.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFPack.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFPackTot.Summary = SumRFPack

        Me.LBRFGrandTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.GrandTot", "{0:#,##0.00;(#,##0.00);""}")})
        SumRFGrandTot.FormatString = "{0:#,##0.00;(#,##0.00);""}"
        SumRFGrandTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFGrandTot.Summary = SumRFGrandTot

    End Sub


End Class