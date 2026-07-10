Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekLPBHargav1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand
    Dim SumGFSuppIDQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSuppIDHAkh As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSuppIDDPP As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFSuppIDPPn As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPPnQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPPnHAkh As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPPnDPP As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPPnPPn As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFMtUangQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFMtUangHAkh As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFMtUangDPP As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFMtUangPPn As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLRekDetLPB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = MainModule.PilihAwal
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = MainModule.PilihAkhir
        cmsl.SelectCommand.Parameters.Add("@PPn", SqlDbType.VarChar).Value = MainModule.PilihPPn
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekLPB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("MtUang", "MtUang"), New System.Data.Common.DataColumnMapping("stsPPn", "stsPPn"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("HarAkhir", "HarAkhir"), New System.Data.Common.DataColumnMapping("DPP", "DPP"), New System.Data.Common.DataColumnMapping("PPn", "PPn")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "RekLPB")

        Me.DataMember = "RekLPB"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHMtUang.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHMtUang})
        Me.GHMtUang.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("MtUang", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHPPn.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHPPn})
        Me.GHPPn.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("stsPPn", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHSuppID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {})
        Me.GHSuppID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("SuppID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHMtUang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.MtUang")})
        Me.LBGHPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.stsPPn")})

        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Tanggal", "{0:dd}")})
        Me.LBSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Supp")})
        Me.LBTrmID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.TrmID")})
        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.POID")})
        Me.LBJenisBB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Jenis")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Qty", "{0:n2}")})
        Me.LBHAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.HarAkhir", "{0:n4}")})
        Me.LBDPP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.DPP", "{0:n4}")})
        Me.LBPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.PPn", "{0:n4}")})

        Me.GFSuppID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFSuppID, Me.LBGFSuppIDQty, Me.LBGFSuppIDHAkh, Me.LBGFSuppIDDPP, Me.LBGFSuppIDPPn})

        Me.LBGFSuppID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Supp", "Total {0}")})

        Me.LBGFSuppIDQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Qty", "{0:n2}")})
        SumGFSuppIDQty.FormatString = "{0:n2}"
        SumGFSuppIDQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSuppIDQty.Summary = SumGFSuppIDQty

        Me.LBGFSuppIDHAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.HarAkhir", "{0:n2}")})
        SumGFSuppIDHAkh.FormatString = "{0:n2}"
        SumGFSuppIDHAkh.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSuppIDHAkh.Summary = SumGFSuppIDHAkh

        Me.LBGFSuppIDDPP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.DPP", "{0:n2}")})
        SumGFSuppIDDPP.FormatString = "{0:n2}"
        SumGFSuppIDDPP.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSuppIDDPP.Summary = SumGFSuppIDDPP

        Me.LBGFSuppIDPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.PPN", "{0:n2}")})
        SumGFSuppIDPPn.FormatString = "{0:n2}"
        SumGFSuppIDPPn.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSuppIDPPn.Summary = SumGFSuppIDPPn

        Me.GFPPn.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFPPn, Me.LBGFPPnQty, Me.LBGFPPnHAkh, Me.LBGFPPnDPP, Me.LBGFPPnPPn})

        Me.LBGFPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.stsPPn", "Total {0}")})

        Me.LBGFPPnQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Qty", "{0:n2}")})
        SumGFPPnQty.FormatString = "{0:n2}"
        SumGFPPnQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPPnQty.Summary = SumGFPPnQty

        Me.LBGFPPnHAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.HarAkhir", "{0:n2}")})
        SumGFPPnHAkh.FormatString = "{0:n2}"
        SumGFPPnHAkh.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPPnHAkh.Summary = SumGFPPnHAkh

        Me.LBGFPPnDPP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.DPP", "{0:n2}")})
        SumGFPPnDPP.FormatString = "{0:n2}"
        SumGFPPnDPP.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPPnDPP.Summary = SumGFPPnDPP

        Me.LBGFPPnPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.PPn", "{0:n2}")})
        SumGFPPnPPn.FormatString = "{0:n2}"
        SumGFPPnPPn.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPPnPPn.Summary = SumGFPPnPPn

        Me.GFMtUang.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFMtUang, Me.LBGFMtUangQty, Me.LBGFMtUangHAkh, Me.LBGFMtUangDPP, Me.LBGFMtUangPPn})

        Me.LBGFMtUang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.MtUang", "Total {0}")})

        Me.LBGFMtUangQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.Qty", "{0:n2}")})
        SumGFMtUangQty.FormatString = "{0:n2}"
        SumGFMtUangQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMtUangQty.Summary = SumGFMtUangQty

        Me.LBGFMtUangHAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.HarAkhir", "{0:n2}")})
        SumGFMtUangHAkh.FormatString = "{0:n2}"
        SumGFMtUangHAkh.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMtUangHAkh.Summary = SumGFMtUangHAkh

        Me.LBGFMtUangDPP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.DPP", "{0:n2}")})
        SumGFMtUangDPP.FormatString = "{0:n2}"
        SumGFMtUangDPP.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMtUangDPP.Summary = SumGFMtUangDPP

        Me.LBGFMtUangPPn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekLPB.PPn", "{0:n2}")})
        SumGFMtUangPPn.FormatString = "{0:n2}"
        SumGFMtUangPPn.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMtUangPPn.Summary = SumGFMtUangPPn

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRLapBrg_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class