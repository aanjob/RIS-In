Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRSTDPP
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumCust As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGrup As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim No As Integer = -1

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select H.DPPBJID,H.Jenis,C.CustID,C.Nama As Cust,J.Jenis as JnsCust,K.Nama as Kota,D.Grup,D.JualID,D.Tanggal, D.SJID +' '+D.Ket As Ket, D.TotAkhir From T_DPPBJ H Inner Join T_DPPBJDtl D On H.DPPBJID=D.DPPBJID Inner Join T_JualBJ J On D.JualID=J.JualID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where H.Tanggal='" & Bind.Item("Tanggal").ToString & "' and H.CabID= '" & Bind.Item("CabID").ToString & "' and H.Gol = '" & Bind.Item("Gol").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_DPPBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DPPBJID", "DPPBJID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("JnsCust", "JnsCust"), New System.Data.Common.DataColumnMapping("Kota", "Kota"), New System.Data.Common.DataColumnMapping("Grup", "Grup"), New System.Data.Common.DataColumnMapping("JualID", "JualID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("Ket", "Ket"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_DPPBJDtl")

        Me.DataMember = "T_DPPBJDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
       Me.LBUser.Text = MainModule.LoginAktif
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")

        'Me.LBDirut.Text = MainModule.NmPemilik
        'If Bind.Item("Gol").ToString = "Character" Then
        '    Me.LBMM.Text = MainModule.MMCr
        'ElseIf Bind.Item("Gol").ToString = "Job Order" Then
        '    Me.LBMM.Text = MainModule.MMJO
        'ElseIf Bind.Item("Gol").ToString = "Own" Then
        '    Me.LBMM.Text = MainModule.MMOwn
        'End If

        Me.LBSales.Text = Bind.Item("Cabang")
        Me.LBCabH.Text = ": " & Bind.Item("Cabang")

        Me.GHCust.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("CustID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GHGrup.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Grup", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHCustID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.CustID")})
        Me.LBGHCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Cust")})
        Me.LBGHGrup.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Grup")})
        Me.LBGFCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Cust", "Total {0}")})
        Me.LBGFGrup.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Grup", "Total {0}")})
        Me.LBDPPID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.DPPBJID")})
        Me.LBCustID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.CustID")})
        Me.LBKota.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Kota")})
        Me.LBJnsCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.JnsCust")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBJualID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.JualID")})
        Me.LBNoSJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.Ket")})
        Me.LBNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})

        Me.LBGFCustNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        SumCust.FormatString = "{0:n2}"
        SumCust.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCustNil.Summary = SumCust

        Me.LBGFGrupNil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        SumGrup.FormatString = "{0:n2}"
        SumGrup.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFGrupNil.Summary = SumGrup

        Me.LBRFNilai.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_DPPBJDtl.TotAkhir", "{0:n2}")})
        SumTot.FormatString = "{0:n2}"
        SumTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFNilai.Summary = SumTot

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSTDPP_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    'Private Sub LBNo_AfterPrint(sender As Object, e As EventArgs) Handles LBNo.AfterPrint
    '    Me.LBNo.Text = No

    'End Sub

    Private Sub LBGHCust_TextChanged(sender As Object, e As EventArgs) Handles LBGHCust.TextChanged

    End Sub

    Private Sub LBGHCustID_TextChanged(sender As Object, e As EventArgs) Handles LBGHCustID.TextChanged
        No += 1
        Me.LBNo.Text = No
    End Sub
End Class