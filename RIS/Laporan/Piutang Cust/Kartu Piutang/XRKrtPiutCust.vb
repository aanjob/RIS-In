Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRKrtPiutCust
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumTambah As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumKurang As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim Gol As String
    Dim Saldo As Decimal = 0.0
    Dim Hal As Integer = 0
    Dim Start As Integer = 0

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("SPLKPiutCust", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Bind.Item("Gol").ToString
        cmsl.SelectCommand.Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Bind.Item("MtUang").ToString

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "PiutCust", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("Alamat", "Alamat"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("DocID", "DocID"), New System.Data.Common.DataColumnMapping("Tambah", "Tambah"), New System.Data.Common.DataColumnMapping("Kurang", "Kurang"), New System.Data.Common.DataColumnMapping("SA", "SA"), New System.Data.Common.DataColumnMapping("SAkh", "SAkh")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "PiutCust")

        Me.DataMember = "PiutCust"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = ": " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHCustID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSA, Me.LBCust})
        Me.GHCustID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("CustID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Row")})
        If MainModule.Posisi Like "*Cabang" Then
            Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.CustID", ": {0}")})
            Me.LBAlamat.Visible = False
        Else
            Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Cust", ": {0}")})
            Me.LBAlamat.Visible = True
        End If

        Me.LBAlamat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Alamat", ": {0}")})
        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Row")})
        Me.LBSA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.SA", "{0:n2}")})
        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Tanggal", "{0:dd/MM/yyyy}")})
        Me.LBDocID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.DocID")})
        Me.LBTambah.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Tambah", "{0:n2}")})
        Me.LBKurang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Kurang", "{0:n2}")})
        Me.LBSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.SAkh", "{0:n2}")})

        Me.LBGFTambah.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Tambah", "{0:n2}")})
        SumTambah.FormatString = "{0:n2}"
        SumTambah.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTambah.Summary = SumTambah

        Me.LBGFKurang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PiutCust.Kurang", "{0:n2}")})
        SumKurang.FormatString = "{0:n2}"
        SumKurang.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFKurang.Summary = SumKurang

        Me.FilterString = "[CustID] In (" & Bind.Item("CustID").ToString & ")"

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRKrtPiutCust_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub GHCustID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHCustID.BeforePrint
        Saldo = 0
        Hal = 0
        Start = 0
    End Sub

    Private Sub LBFcSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBFcSAkh.BeforePrint
        If Start = 1 Then
            Saldo += CDec(Me.LBSAkh.Text)
        Else
            Saldo += CDec(Me.LBTambah.Text) - CDec(Me.LBKurang.Text)
        End If
        Me.LBFcSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
    End Sub

    'Private Sub LBTambah_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBTambah.BeforePrint
    '    If Start > 1 Then
    '        Saldo += CDec(Me.LBTambah.Text)
    '    End If
    '    Me.LBFcSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
    'End Sub

    'Private Sub LBKurang_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBKurang.BeforePrint
    '    If Start > 1 Then
    '        Saldo -= CDec(Me.LBKurang.Text)
    '    End If
    '    Me.LBFcSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
    'End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Start += 1
    End Sub

    Private Sub PageHeader_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles PageHeader.BeforePrint
        Hal += 1

        If Hal = 1 Then
            Me.PageHeader.Visible = False
        Else
            Me.PageHeader.Visible = True
        End If
    End Sub

    Private Sub LBGFSA_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSA.BeforePrint
        Me.LBGFSA.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", CDec(Me.LBSA.Text))
    End Sub

    Private Sub LBGFSAkh_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSAkh.BeforePrint
        Me.LBGFSAkh.Text = String.Format("{0:#,##0.00;(#,##0.00);0.00}", Saldo)
    End Sub
End Class