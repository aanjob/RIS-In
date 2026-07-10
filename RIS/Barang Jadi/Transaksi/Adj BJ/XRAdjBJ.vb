Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Public Class XRAdjBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.ArtCode,ArtName,D.Isi,Qty*D.Isi as Psg,Ket From T_AdjBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where AdjBJID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_AdjBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Isi", "Isi"), New System.Data.Common.DataColumnMapping("Psg", "Psg"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_AdjBJDtl")

        Me.DataMember = "T_AdjBJDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBKeterangan.Text = ": " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_AdjBJDtl.ArtCode")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_AdjBJDtl.ArtName")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_AdjBJDtl.Psg", "{0:n0}")})
        Me.LBIsi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_AdjBJDtl.Isi")})
        Me.LBKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_AdjBJDtl.Ket")})

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_AdjBJDtl.Psg", "{0:n0}")})
        SumPsg.FormatString = "{0:n0}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotPsg.Summary = SumPsg

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XROpBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class