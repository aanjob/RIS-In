Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRDPPBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGFNil As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotAkhir As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Grup,Concat(JualID,' ',Kode) As JualID,Tanggal,DueDate,TotAkhir,Concat(TotDos,' Dos ',TotPsg,' Psg ',SJID,' ',Ket) As Ket From T_DPPBJDtl Where DPPBJID ='" & Bind.Item("DPPBJID").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_DPPBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Grup", "Grup"), New System.Data.Common.DataColumnMapping("JualID", "JualID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("DueDate", "DueDate"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_DPPBJDtl")

        Me.DataMember = "T_DPPBJDtl"
        Me.DataSource = DsLap

        Me.LBJudul.Text = "Daftar Penagihan Piutang " & vbCrLf & Bind.Item("Jenis").ToString
        Me.LBDPPID.Text = ": " & Bind.Item("DPPBJID").ToString
        Me.LBKode.Text = "  " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = Bind.Item("Cust").ToString
        Me.LBStatus.Text = ": " & Bind.Item("Status").ToString
        Me.LBAlamat.Text = Bind.Item("Alamat").ToString & vbCrLf & Bind.Item("Kota").ToString
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        'Me.LBPemilik.Text = MainModule.NmPemilik
        Me.LBTerbilang.Text = Bind.Item("Terbilang").ToString
       Me.LBUser.Text = MainModule.LoginAktif
        TotAkhir = CDec(Bind.Item("TotTagih").ToString)
        Me.LBTotTagih.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotAkhir)

        Me.LBTglDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBNoFT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.JualID")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Ket")})
        Me.LBDueDate.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.DueDate", "{0:dd/MM/yyyy}")})
        Me.LBNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        'Me.LBGHGrup.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Grup", "{0}")})
        'Me.LBGFGrup.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Grup", "Total {0}")})

        'Me.LBGFNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        'SumGFNil.FormatString = "{0:n2}"
        'SumGFNil.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        'Me.LBGFNil.Summary = SumGFNil

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRDPPBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class