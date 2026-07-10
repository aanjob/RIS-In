Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO


Public Class XRTrmSR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQtyRj As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select StlName,SampType,Uk,warna,Sum(Qty) As Qty,Sum(QtyRj) As QtyRj From T_TrmSRDtl Where TrmSRID ='" & Bind.Item("Kode").ToString & "' Group By StlName,SampType,Uk,Warna", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_TrmSRDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("StlName", "StlName"), New System.Data.Common.DataColumnMapping("SampType", "SampType"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("QtyRj", "QtyRj")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_TrmSRDtl")

        Me.DataMember = "T_TrmSRDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBMkt.Text = ": " & Bind.Item("Mkt").ToString
        Me.LBChaser.Text = ": " & Bind.Item("Chaser").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBStlName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.StlName")})
        Me.LBType.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.SampType")})
        Me.LBSize.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.Uk")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.Qty", "{0:n1}")})
        Me.LBQtyRj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.QtyRj", "{0:n1}")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.Warna")})

        Me.LBTotQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.Qty", "{0:n1}")})
        SumQty.FormatString = "{0:n1}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotQty.Summary = SumQty

        Me.LBTotQtyRj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrmSRDtl.QtyRj", "{0:n1}")})
        SumQtyRj.FormatString = "{0:n1}"
        SumQtyRj.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotQtyRj.Summary = SumQtyRj

        Dim command As New SqlCommand("Select Picture From M_Image Where ID = '" & Bind.Item("Kode").ToString & "'", koneksi)
        Dim Pic() As Byte
        Dim newImage As Image

        With koneksi
            .Open()
            Pic = command.ExecuteScalar()
            .Close()
        End With

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