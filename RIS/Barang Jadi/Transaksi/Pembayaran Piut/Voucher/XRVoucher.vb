Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRVoucher
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select VcrID,Header,Format(TglAwal, 'dd MMMM yyyy')+ ' - '+Format(TglAkhir, 'dd MMMM yyyy') As Period,+'('+ V.CustID +') '+ C.Nama +' - '+ K.Nama As Cust,Nominal,Replace(Replace(Terbilang,'   ',' '),'  ',' ') as Terbilang,V.Ket From T_Voucher V Inner Join M_Cust C On V.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where V.VcrID In(" & Bind.Item("Kode").ToString & ")", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_Voucher", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("VcrID", "VcrID"), New System.Data.Common.DataColumnMapping("Header", "Header"), New System.Data.Common.DataColumnMapping("TglAwal", "TglAwal"), New System.Data.Common.DataColumnMapping("TglAkhir", "TglAkhir"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("Nominal", "Nominal"), New System.Data.Common.DataColumnMapping("Terbilang", "Terbilang"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_Voucher")

        Me.DataMember = "T_Voucher"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
       
        Me.LBManager.Text = MainModule.MM1

        Me.LBKode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.VcrID")})
        Me.LBHeader.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.Header")})
        Me.LBPeriod.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.Period", ": {0}")})
        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.Cust", ": {0}")})
        Me.LBTerbilang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.Terbilang", "({0})")})
        Me.LBKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.Ket")})
        Me.LBNom.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_Voucher.Nominal", "Rp. {0:n0}")})
      

        Me.ShowPreview()
    End Sub

    Private Sub XRVoucher_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class