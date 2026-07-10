Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XROpBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDosG As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsgG As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select M.Nama as Merk,D.ArtCode,ArtName,D.Isi,QtyD,DosD,PsgD From T_OpBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where OpBJID='" & Bind.Item("Kode").ToString & "' and QtyD<>0 Order By ArtCode Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_OpBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Merk", "Merk"), New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Isi", "Isi"), New System.Data.Common.DataColumnMapping("QtyD", "QtyD"), New System.Data.Common.DataColumnMapping("DosD", "DosD"), New System.Data.Common.DataColumnMapping("PsgD", "PsgD")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_OpBJDtl")

        Me.DataMember = "T_OpBJDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBMerk})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Merk", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBMerk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.Merk")})
        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.ArtCode")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.ArtName")})
        Me.LBData.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.QtyD", "{0:n0}")})
        Me.LBIsi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.Isi")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTotDosG, Me.LBTotPsgG})

        Me.LBTotDosG.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.DosD", "{0:n0}")})
        SumDosG.FormatString = ": {0:n0}"
        SumDosG.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotDosG.Summary = SumDosG

        Me.LBTotPsgG.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.PsgD", "{0:n0}")})
        SumPsgG.FormatString = ": {0:n0}"
        SumPsgG.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotPsgG.Summary = SumPsgG

        Me.LBTotDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.DosD", "{0:n0}")})
        SumDos.FormatString = ": {0:n0}"
        SumDos.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDos.Summary = SumDos

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.PsgD", "{0:n0}")})
        SumPsg.FormatString = ": {0:n0}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
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