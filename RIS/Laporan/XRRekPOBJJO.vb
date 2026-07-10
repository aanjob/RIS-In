Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRRekPOBJJO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumNil As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select C.Nama As Cust,POID,POCust,TglKirim,TotPsg,MtUang,TotAkhir From T_POBJJO P Inner Join M_Cust C On P.CustID=C.CustID Where POID In (" & Bind.Item("PO").ToString & ") Order By C.Nama,POID", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekPOBJJO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("POCust", "POCust"), New System.Data.Common.DataColumnMapping("TglKirim", "TglKirim"), New System.Data.Common.DataColumnMapping("TotPsg", "TotPsg"), New System.Data.Common.DataColumnMapping("MtUang", "MtUang"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "RekPOBJJO")

        Me.DataMember = "RekPOBJJO"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.Cust")})
        Me.LBKode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.POID")})
        Me.LBPOCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.POCust")})
        Me.LBMtUang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.MtUang")})
        Me.LBTglKirim.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TglKirim", "{0: dd/MM/yyyy}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.TotPsg", "{0:#,##0.0;(#,##0.0);""}")})
        Me.LBSubTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.TotAkhir", "{0:#,##0.00;(#,##0.00);0.00}")})
      
        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.TotPsg", "{0:#,##0.0;(#,##0.0);""}")})
        SumQty.FormatString = "{0:#,##0.0;(#,##0.0);""}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotPsg.Summary = SumQty

        Me.LBTotAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekPOBJJO.TotAkhir", "{0:#,##0.00;(#,##0.00);0.00}")})
        SumNil.FormatString = "{0:#,##0.00;(#,##0.00);0.00}"
        SumNil.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotAkhir.Summary = SumNil

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()

    End Sub

    Private Sub XRBPB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class