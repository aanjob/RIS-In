Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRSchSampAss
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim No As Integer = -1
    Dim SumGFQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("Select SR.ReqID,SR.Tanggal,SR.TglKirim,C.Nama As Cust,SR.StyleID + ' / ' + SR.StlName As Style,SampType,Warna,Uk, Qty,TglPattern,TglSpec,TglTool,TglBhn,TglAss,SS.Ket From T_SchPrSamp SS Inner Join T_SampReq SR On SS.ReqID=SR.ReqID Inner Join M_Cust C On SS.CustID=C.CustID and SchSRIDD=(Select Top 1 SchSRIDD From T_SchPrSamp where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD order By SchSRIDD desc) Where stsKirim='False' Order By ReqID,SR.StyleID,SR.StlName,Warna", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_SchPrSamp", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ReqID", "ReqID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("TglKirim", "TglKirim"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("Style", "Style"), New System.Data.Common.DataColumnMapping("SampType", "SampType"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("TglPattern", "TglPattern"), New System.Data.Common.DataColumnMapping("TglSpec", "TglSpec"), New System.Data.Common.DataColumnMapping("TglTool", "TglTool"), New System.Data.Common.DataColumnMapping("TglBhn", "TglBhn"), New System.Data.Common.DataColumnMapping("TglAss", "TglAss"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_SchPrSamp")

        Me.DataMember = "T_SchPrSamp"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("TglAss", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Cust")})
        Me.LBReqID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.ReqID")})
        Me.LBStyle.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Style")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Tanggal", "{0:dd/MM}")})
        Me.LBTglKrm.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglKirim", "{0:dd/MM}")})
        Me.LBTipe.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.SampType")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Warna")})
        Me.LBSize.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Uk")})
        Me.LBTglPatt.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglPattern", "{0:dd/MM}")})
        Me.LBTglSpec.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglSpec", "{0:dd/MM}")})
        Me.LBTglTool.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglTool", "{0:dd/MM}")})
        Me.LBTglBhn.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglBhn", "{0:dd/MM}")})
        Me.LBGHTglAss.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglAss", "Tanggal Assembling : {0:dd MMMM yyyy}")})
        Me.LBGFTglAss.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.TglAss", "Total Qty Assembling Tanggal {0:dd MMMM yyyy}")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Qty", "{0:#,##0.##}")})
        Me.LBKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Ket")})

        Me.LBGFQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SchPrSamp.Qty", "{0:#,##0.##}")})
        SumGFQty.FormatString = "{0:#,##0.##}"
        SumGFQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQty.Summary = SumGFQty

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSTDPP_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class