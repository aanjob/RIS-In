Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRDOBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotDos, TotPsg As Integer
    Dim Start As Integer = 0
    Dim Kode As String

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select ROW_NUMBER() OVER(ORDER BY D.ArtCode Asc) AS Row, D.ArtCode +' ( '+ ArtName +' )' As Barang ,Dos,Psg From T_DODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where DOID ='" & Bind.Item("DOID").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_DODtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("Barang", "Barang"), New System.Data.Common.DataColumnMapping("Dos", "Dos"), New System.Data.Common.DataColumnMapping("Psg", "Psg")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_DODtl")

        Me.DataMember = "T_DODtl"
        Me.DataSource = DsLap

        Kode = Bind.Item("DOID").ToString
        Me.LBKode.Text = Bind.Item("Kode").ToString
        Me.LBDOID.Text = Bind.Item("DOID").ToString
        Me.LBTanggal.Text = Format(CDate(Bind.Item("Tanggal")), "dd/MM/yyyy")
        Me.LBGudangTj.Text = Bind.Item("GudangTj").ToString
        Me.LBAlamat.Text = Bind.Item("Alamat").ToString
        Me.LBSJ.Text = "Surat Jalan : " & Bind.Item("SJ").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DODtl.Row")})
        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DODtl.Barang")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DODtl.Dos", "{0:n0}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DODtl.Psg", "{0:n0}")})

        Me.LBTotDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DODtl.Dos", "{0:n0}")})
        SumDos.FormatString = "{0:n0}"
        SumDos.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDos.Summary = SumDos

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DODtl.Psg", "{0:n0}")})
        SumPsg.FormatString = "{0:n0}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotPsg.Summary = SumPsg

        Me.ShowPreview()
    End Sub

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    'Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
    '    Start += 1
    'End Sub

    'Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged
    '    Me.LBNo.Text = Start
    'End Sub

    'Private Sub PageHeader_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles PageHeader.BeforePrint
    '    Start = 0
    'End Sub

End Class