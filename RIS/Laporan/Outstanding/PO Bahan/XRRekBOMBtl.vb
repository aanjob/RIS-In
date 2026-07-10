Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Public Class XRRekBOMBtl
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumPO As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTrm As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select BOMID,D.BBID,B.Nama,D.Sat,Sum(Qty) as TotPO,(Select Isnull((Select Sum(Qty) From T_TrmBB Hx Inner Join T_TrmBBDtl Dx On Hx.TrmID=Dx.TrmID Where BBID=D.BBID and BOMID=D.BOMID  Group By BOMID,BBID),0)) as TotTrm From T_POBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where BOMID In (" & Bind.Item("BOMID").ToString & ") Group By BOMID,D.BBID,B.Nama,D.Sat", koneksi)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "BOMBtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("TotPO", "TotPO"), New System.Data.Common.DataColumnMapping("TotTrm", "TotTrm")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "BOMBtl")

        Me.DataMember = "BOMBtl"
        Me.DataSource = DsLap

       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHBahan.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.Bahan")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.Sat")})
        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.BOMID")})
        Me.LBPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.TotPO", "{0:n2}")})
        Me.LBTrm.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.TotTrm", "{0:n2}")})

        Me.LBTotPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.TotPO", "{0:n2}")})
        SumPO.FormatString = "{0:n2}"
        SumPO.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotPO.Summary = SumPO

        Me.LBTotTrm.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BOMBtl.TotTrm", "{0:n2}")})
        SumTrm.FormatString = "{0:n2}"
        SumTrm.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotTrm.Summary = SumTrm

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRRekBOMBtl_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class