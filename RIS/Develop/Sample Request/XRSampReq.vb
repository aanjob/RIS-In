Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO


Public Class XRSampReq
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select StlName,SampType,Uk,Qty,Warna,Ket From T_SampReqDtl Where ReqID ='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_SampReqDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SampType", "SampType"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_SampReqDtl")

        Me.DataMember = "T_SampReqDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBChaser.Text = ": " & Bind.Item("Chaser").ToString
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("TglKirim")), "dd MMMM yyyy")
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBAdmin.Text = MainModule.LoginAktif
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBStlName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.StlName")})
        Me.LBType.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.SampType")})
        Me.LBSize.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Uk")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Qty", "{0:n1}")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Warna")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Ket")})

        Me.LBTotQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_SampReqDtl.Qty", "{0:n1}")})
        SumQty.FormatString = "{0:n1}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotQty.Summary = SumQty

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
        Else
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
        End If

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSampReq_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class