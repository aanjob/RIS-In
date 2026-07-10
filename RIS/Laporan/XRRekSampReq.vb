Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekSampReq
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSample As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumCancel As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("Select Distinct C.Nama As Cust, (Select Isnull(Sum(Qty)-Sum(BtlOrder),0) From T_SampReq Hx Inner Join T_SampReqDtl Dx On Hx.ReqID=Dx.ReqID Where Tanggal >='" & MainModule.PilihAwal & "' and Tanggal <='" & MainModule.PilihAkhir & "' and Hx.CustID=C.CustID) As Samp, (Select Isnull(Sum(BtlOrder),0) From T_SampReq Hx Inner Join T_SampReqDtl Dx On Hx.ReqID=Dx.ReqID Where Tanggal >='" & MainModule.PilihAwal & "' and Tanggal <='" & MainModule.PilihAkhir & "' and Hx.CustID=C.CustID) As Cancel, (Select Isnull(Sum(Qty),0) From T_SampReq Hx Inner Join T_SampReqDtl Dx On Hx.ReqID=Dx.ReqID Where Tanggal >='" & MainModule.PilihAwal & "' and Tanggal <='" & MainModule.PilihAkhir & "' and Hx.CustID=C.CustID) As Tot From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID Inner Join M_Cust C On H.CustID=C.CustID Where Tanggal >='" & MainModule.PilihAwal & "' and Tanggal <='" & MainModule.PilihAkhir & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_SampReqDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("Samp", "Samp"), New System.Data.Common.DataColumnMapping("Cancel", "Cancel"), New System.Data.Common.DataColumnMapping("Tot", "Tot")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_SampReqDtl")

        Me.DataMember = "T_SampReqDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Cust")})
        Me.LBSample.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Samp", "{0:n1}")})
        Me.LBCancel.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Cancel", "{0:n1}")})
        Me.LBTotal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Tot", "{0:n1}")})

        Me.LBTotSample.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Samp", "{0:n1}")})
        SumSample.FormatString = "{0:n1}"
        SumSample.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotSample.Summary = SumSample

        Me.LBTotCancel.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Cancel", "{0:n1}")})
        SumCancel.FormatString = "{0:n1}"
        SumCancel.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotCancel.Summary = SumCancel

        Me.LBGrandTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Tot", "{0:n1}")})
        SumTot.FormatString = "{0:n1}"
        SumTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBGrandTot.Summary = SumTot

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRRekSampReq_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class