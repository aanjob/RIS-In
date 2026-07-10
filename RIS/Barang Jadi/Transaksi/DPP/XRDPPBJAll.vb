Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRDPPBJAll
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGFNil As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Dim TotAkhir As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select H.DPPBJID,Jenis,H.Kode,H.Tanggal As TglH,C.Nama As Customer,Concat(C.Alamat,CHAR(13),K.Nama) as Alamat,TotTagih,Terbilang,Concat(JualID,' ' ,D.Kode) As JualID,D.Tanggal,DueDate,TotAkhir,Concat(D.TotDos,' Dos ',D.TotPsg,' Psg ',SJID,' ',D.Ket) As Ket From T_DPPBJ H Inner Join T_DPPBJDtl D On H.DPPBJID=D.DPPBJID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where H.Tanggal='" & Bind.Item("Tanggal").ToString & "' and H.Gol='" & Bind.Item("Gol").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_DPPBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DPPBJID", "DPPBJID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("Kode", "Kode"), New System.Data.Common.DataColumnMapping("TglH", "TglH"), New System.Data.Common.DataColumnMapping("Customer", "Customer"), New System.Data.Common.DataColumnMapping("Alamat", "Alamat"), New System.Data.Common.DataColumnMapping("TotTagih", "TotTagih"), New System.Data.Common.DataColumnMapping("Terbilang", "Terbilang"), New System.Data.Common.DataColumnMapping("JualID", "JualID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("DueDate", "DueDate"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_DPPBJDtl")

        Me.DataMember = "T_DPPBJDtl"
        Me.DataSource = DsLap

        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        'Me.LBPemilik.Text = MainModule.NmPemilik
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBDPPID})
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("DPPBJID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})


        Me.LBJudul.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Jenis", "Daftar Penagihan Piutang {0}")})
        Me.LBDPPID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.DPPBJID", ": {0}")})
        Me.LBKode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Kode", "  {0}")})
        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TglH", "Tanggal : {0:dd MMMM yyyy}")})
        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Customer")})
        Me.LBAlamat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Alamat")})
        Me.LBTotTagih.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotTagih", "{0:n2}")})
        Me.LBTerbilang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Terbilang")})
        Me.LBTglDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBJualID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.JualID")})
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

    Private Sub XRDPPBJAll_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class