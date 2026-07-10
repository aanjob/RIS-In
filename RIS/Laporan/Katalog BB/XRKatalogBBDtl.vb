Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRKatalogBBDtl
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()
    End Sub

    Private Sub XRAss_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        cmsl = New SqlDataAdapter("Select * From (Select Distinct '('+ B.BBID +') '+ B.Nama As Bahan,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBB Where BBID=B.BBID and Tanggal<='" & MainModule.PilihTgl & "'),0)) As Stok From T_POBB PH Inner Join T_POBBDtl PD on PH.POID=PD.POID Inner Join M_Cust C On PH.CustID=C.CustID Inner Join M_BB B On PD.BBID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where PH.CustID Like '" & CStr(CustID.Value) & "' and B.JnsID ='" & CStr(JnsID.Value) & "') as x Where Stok>0", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_BB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Stok", "Stok")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_BB")

        Me.DataMember = "M_BB"
        Me.DataSource = DsLap

        Me.LBNama.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_BB.Bahan")})
        Me.LBStok.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_BB.Stok")})
    End Sub

End Class