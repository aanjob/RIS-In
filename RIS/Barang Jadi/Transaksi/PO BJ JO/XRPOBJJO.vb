Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPOBJJO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Project,Concat (D.ArtCode,' ( ',ArtName,' ',B.StyleID,' )') As Barang,W.Nama As Warna,D.Uk,Qty+QtyPol As Qty,HarSat,HarAkhir From T_POBJJODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where POID ='" & Bind.Item("Kode").ToString & "' Order By D.ArtCode,D.Uk", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_POBJJODtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Barang", "Barang"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("HarAkhir", "HarAkhir")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_POBJJODtl")

        Me.DataMember = "T_POBJJODtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan & vbCrLf & MainModule.Alamat & vbCrLf & MainModule.Kota
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("TglKirim")), "dd MMMM yyyy")
        Me.LBSuppCust.Text = Bind.Item("SuppCust").ToString
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBAlamat.Text = ": " & Bind.Item("Alamat").ToString & vbCrLf & "  " & Bind.Item("Kota").ToString
        Me.LBPOCust.Text = ": " & Bind.Item("POCust").ToString
        Me.LBKet.Text = Bind.Item("Ket").ToString
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        'Me.LBManager.Text = MainModule.MMJO
       Me.LBUser.Text = MainModule.LoginAktif

        Me.XLBHarga.Text = "Harga (" & Bind.Item("MtUang").ToString & ")"
        Me.XLBJml.Text = "Jumlah (" & Bind.Item("MtUang").ToString & ")"

        Me.LBGrandTot.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotAkhir").ToString))
        Me.LBTotQty.Text = String.Format("{0:#,##0;(#,##0);""}", CDec(Bind.Item("TotQty").ToString))

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBProject})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Project", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBProject.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.Project")})
        Me.LBBarang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.Barang")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.Warna")})
        Me.LBSize.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.Uk")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.Qty", "{0:n0}")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.HarSat", "{0:n2}")})
        Me.LBJml.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.HarAkhir", "{0:n2}")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFTot, Me.LBGFQty})

        Me.LBGFQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.Qty", "{0:n0}")})
        SumQty.FormatString = "{0:n0}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQty.Summary = SumQty

        Me.LBGFTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJJODtl.HarAkhir", "{0:n2}")})
        SumTot.FormatString = "{0:n2}"
        SumTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTot.Summary = SumTot

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPOBJJO_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class