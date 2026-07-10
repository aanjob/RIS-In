Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekDPPBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGrup As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumCust As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select H.DPPBJID,TglAwal,TglAkhir,Jenis,Grup,C.CustID,C.Nama As Cust,Concat(C.Alamat,CHAR(13),K.Nama) as Alamat,JualID,D.Tanggal,TotAkhir From T_DPPBJ H Inner Join T_DPPBJDtl D On H.DPPBJID=D.DPPBJID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where H.Tanggal='" & Bind.Item("Tanggal").ToString & "' and H.CabID= '" & Bind.Item("CabID").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_DPPBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DPPBJID", "DPPBJID"), New System.Data.Common.DataColumnMapping("TglAwal", "TglAwal"), New System.Data.Common.DataColumnMapping("TglAkhir", "TglAkhir"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("Grup", "Grup"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("Alamat", "Alamat"), New System.Data.Common.DataColumnMapping("JualID", "JualID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_DPPBJDtl")

        Me.DataMember = "T_DPPBJDtl"
        Me.DataSource = DsLap

        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif
        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        'Me.LBPerusahaan2.Text = MainModule.NmPerusahaan
        Me.LBCabang.Text = ": " & Bind.Item("Cabang")
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")

        Me.GHCust.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBCabang, Me.LBCust, Me.LBAlamat})
        Me.GHCust.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("CustID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHGrup.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHGrup})
        Me.GHGrup.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Grup", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHDPP.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHDPPID})
        Me.GHDPP.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("DPPBJID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Cust", ": {0}")})
        Me.LBAlamat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Alamat", ": {0}")})
        Me.LBGHGrup.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Grup")})
        Me.LBGFGrup.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Grup", "Total {0}")})
        Me.LBGHDPPID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.DPPBJID")})
        Me.LBJualID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.JualID")})
        Me.LBJns.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Jenis")})
        Me.LBTglFT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})

        Me.GFGrup.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFGrup, Me.LBGFGrupNil})

        Me.LBGFGrupNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        SumGrup.FormatString = "{0:n2}"
        SumGrup.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFGrupNil.Summary = SumGrup

        Me.GFCust.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFCustNil})

        Me.LBGFCustNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        SumCust.FormatString = "{0:n2}"
        SumCust.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCustNil.Summary = SumCust

        Me.FilterString = "[CustID] In (" & Bind.Item("Cust").ToString & ")"

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRRekDPPBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class