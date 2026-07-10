Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Public Class XRDetTKL1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumRFCuttUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFCuttBot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSeri As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSabUpp As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSabIns As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFJhtK As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFJht As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFSole As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFAss As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
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

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SPLHProdTKL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Kategori", "Kategori"), New System.Data.Common.DataColumnMapping("JnsJam", "JnsJam"), New System.Data.Common.DataColumnMapping("CuttUppJam", "CuttUppJam"), New System.Data.Common.DataColumnMapping("CuttUppOrg", "CuttUppOrg"), New System.Data.Common.DataColumnMapping("CuttUppUM", "CuttUppUM"), New System.Data.Common.DataColumnMapping("CuttUppTot", "CuttUppTot"), New System.Data.Common.DataColumnMapping("CuttBottJam", "CuttBottJam"), New System.Data.Common.DataColumnMapping("CuttBottOrg", "CuttBottOrg"), New System.Data.Common.DataColumnMapping("CuttBottUM", "CuttBottUM"), New System.Data.Common.DataColumnMapping("CuttBottTot", "CuttBottTot"), New System.Data.Common.DataColumnMapping("SeriJam", "SeriJam"), New System.Data.Common.DataColumnMapping("SeriOrg", "SeriOrg"), New System.Data.Common.DataColumnMapping("SeriUM", "SeriUM"), New System.Data.Common.DataColumnMapping("SeriTot", "SeriTot"), New System.Data.Common.DataColumnMapping("SabUppJam", "SabUppJam"), New System.Data.Common.DataColumnMapping("SabUppOrg", "SabUppOrg"), New System.Data.Common.DataColumnMapping("SabUppUM", "SabUppUM"), New System.Data.Common.DataColumnMapping("SabUppTot", "SabUppTot"), New System.Data.Common.DataColumnMapping("SabInsJam", "SabInsJam"), New System.Data.Common.DataColumnMapping("SabInsOrg", "SabInsOrg"), New System.Data.Common.DataColumnMapping("SabInsUM", "SabInsUM"), New System.Data.Common.DataColumnMapping("SabInsTot", "SabInsTot"), New System.Data.Common.DataColumnMapping("JhtKompJam", "JhtKompJam"), New System.Data.Common.DataColumnMapping("JhtKompOrg", "JhtKompOrg"), New System.Data.Common.DataColumnMapping("JhtKompUM", "JhtKompUM"), New System.Data.Common.DataColumnMapping("JhtKompTot", "JhtKompTot"), New System.Data.Common.DataColumnMapping("JhtUppJam", "JhtUppJam"), New System.Data.Common.DataColumnMapping("JhtUppOrg", "JhtUppOrg"), New System.Data.Common.DataColumnMapping("JhtUppUM", "JhtUppUM"), New System.Data.Common.DataColumnMapping("JhtUppTot", "JhtUppTot"), New System.Data.Common.DataColumnMapping("InsockJam", "InsockJam"), New System.Data.Common.DataColumnMapping("InsockOrg", "InsockOrg"), New System.Data.Common.DataColumnMapping("InsockUM", "InsockUM"), New System.Data.Common.DataColumnMapping("InsockTot", "InsockTot"), New System.Data.Common.DataColumnMapping("InsoleJam", "InsoleJam"), New System.Data.Common.DataColumnMapping("InsoleOrg", "InsoleOrg"), New System.Data.Common.DataColumnMapping("InsoleUM", "InsoleUM"), New System.Data.Common.DataColumnMapping("InsoleTot", "InsoleTot"), New System.Data.Common.DataColumnMapping("OutsoleJam", "OutsoleJam"), New System.Data.Common.DataColumnMapping("OutsoleOrg", "OutsoleOrg"), New System.Data.Common.DataColumnMapping("OutsoleUM", "OutsoleUM"), New System.Data.Common.DataColumnMapping("OutsoleTot", "OutsoleTot"), New System.Data.Common.DataColumnMapping("InserttJam", "InserttJam"), New System.Data.Common.DataColumnMapping("InserttOrg", "InserttOrg"), New System.Data.Common.DataColumnMapping("InserttUM", "InserttUM"), New System.Data.Common.DataColumnMapping("InserttTot", "InserttTot"), New System.Data.Common.DataColumnMapping("InjectJam", "InjectJam"), New System.Data.Common.DataColumnMapping("InjectOrg", "InjectOrg"), New System.Data.Common.DataColumnMapping("InjectUM", "InjectUM"), New System.Data.Common.DataColumnMapping("InjectTot", "InjectTot"), New System.Data.Common.DataColumnMapping("AssJam", "AssJam"), New System.Data.Common.DataColumnMapping("AssOrg", "AssOrg"), New System.Data.Common.DataColumnMapping("AssUM", "AssUM"), New System.Data.Common.DataColumnMapping("AssTot", "AssTot"), New System.Data.Common.DataColumnMapping("FinishJam", "FinishJam"), New System.Data.Common.DataColumnMapping("FinishOrg", "FinishOrg"), New System.Data.Common.DataColumnMapping("FinishUM", "FinishUM"), New System.Data.Common.DataColumnMapping("FinishTot", "FinishTot"), New System.Data.Common.DataColumnMapping("PackJam", "PackJam"), New System.Data.Common.DataColumnMapping("PackOrg", "PackOrg"), New System.Data.Common.DataColumnMapping("PackUM", "PackUM"), New System.Data.Common.DataColumnMapping("PackTot", "PackTot"), New System.Data.Common.DataColumnMapping("PhylonJam", "PhylonJam"), New System.Data.Common.DataColumnMapping("PhylonOrg", "PhylonOrg"), New System.Data.Common.DataColumnMapping("PhylonUM", "PhylonUM"), New System.Data.Common.DataColumnMapping("PhylonTot", "PhylonTot"), New System.Data.Common.DataColumnMapping("GrandTot", "GrandTot")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "SPLHProdTKL")

        Me.DataMember = "SPLHProdTKL"
        Me.DataSource = DsLap

        Me.LBKat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.Kategori")})
        Me.LBJnsJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JnsJam")})

        Me.LBCuttUpJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppJam")})
        Me.LBCuttUpOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBCuttUpUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBCuttUpTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttUppTot", "{0:#,##0.##;(#,##0.##);""}")})
        '{0:#,##0.00;(#,##0.00);""}

        Me.LBCuttBotJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottJam")})
        Me.LBCuttBotOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBCuttBotUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBCuttBotTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.CuttBottTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBSeriJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriJam")})
        Me.LBSeriOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBSeriUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBSeriTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SeriTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBSabUpJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabUppJam")})
        Me.LBSabUpOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabUppOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBSabUpUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabUppUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBSabUpTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabUppTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBSabInsJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabInsJam")})
        Me.LBSabInsOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabInsOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBSabInsUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabInsUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBSabInsTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.SabInsTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBJhtKJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtKompJam")})
        Me.LBJhtKOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtKompOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBJhtKUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtKompUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBJhtKTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtKompTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBJhtJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtUppJam")})
        Me.LBJhtOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtUppOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBJhtUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtUppUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBJhtTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.JhtUppTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBSoleJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.OutsoleJam")})
        Me.LBSoleOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.OutsoleOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBSoleUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.OutsoleUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBSoleTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.OutsoleTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBAssJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.AssJam")})
        Me.LBAssOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.AssOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBAssUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.AssUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBAssTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.AssTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBPackJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackJam")})
        Me.LBPackOrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackOrg", "{0:#,##0;(#,##0);""}")})
        Me.LBPackUM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackUM", "{0:#,##0.##;(#,##0.##);""}")})
        Me.LBPackTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.PackTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBGrandTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdTKL.GrandTot", "{0:#,##0.##;(#,##0.##);""}")})

        Me.LBRFCuttUpTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttUppTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFCuttUpp.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFCuttUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFCuttUpTot.Summary = SumRFCuttUpp


        Me.LBRFCuttBotTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.CuttBottTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFCuttBot.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFCuttBot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFCuttBotTot.Summary = SumRFCuttBot

        Me.LBRFSeriTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SeriTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFSeri.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFSeri.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSeriTot.Summary = SumRFSeri

        Me.LBRFSabUpTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabUppTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFSabUpp.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFSabUpp.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSabUpTot.Summary = SumRFSabUpp

        Me.LBRFSabInsTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.SabInsTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFSabIns.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFSabIns.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSabInsTot.Summary = SumRFSabIns

        Me.LBRFJhtKTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtKompTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFJhtK.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFJhtK.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFJhtKTot.Summary = SumRFJhtK

        Me.LBRFJhtTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.JhtUppTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFJht.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFJht.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFJhtTot.Summary = SumRFJht

        Me.LBRFSoleTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.OutsoleTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFSole.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFSole.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSoleTot.Summary = SumRFSole

        Me.LBRFAssTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.AssTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFAss.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFAss.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFAssTot.Summary = SumRFAss

        Me.LBRFPackTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.PackTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFPack.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFPack.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFPackTot.Summary = SumRFPack

        Me.LBRFGrandTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SPLHProdDtl.GrandTot", "{0:#,##0.##;(#,##0.##);""}")})
        SumRFGrandTot.FormatString = "{0:#,##0.##;(#,##0.##);""}"
        SumRFGrandTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFGrandTot.Summary = SumRFGrandTot

    End Sub


End Class