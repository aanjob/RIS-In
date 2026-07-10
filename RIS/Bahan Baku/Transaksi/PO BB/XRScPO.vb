Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors


Public Class XRScPO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("Select H.POID,H.Tanggal,S.Nama As Supp,H.CustID,C.Nama As Cust,BOMID,B.Nama As Bahan,D.Sat,Qty,Ready,ETD,ETA From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_BB B On D.BBID=B.BBID Where SisaKirim<>Qty and H.stsKirim=0 Order By BOMID,Bahan Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_POBBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Ready", "Ready"), New System.Data.Common.DataColumnMapping("ETD", "ETD"), New System.Data.Common.DataColumnMapping("ETA", "ETA")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_POBBDtl")

        Me.DataMember = "T_POBBDtl"
        Me.DataSource = DsLap

        Me.LBTanggal.Text = "Per Tanggal : " & Format(System.DateTime.Now, "dd MMMM yyyy")

        Me.GHCust.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("CustID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Cust")})
        Me.LBSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Supp")})
        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.POID")})
        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.BOMID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Bahan")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Qty", "{0:#,##0.##}")})
        Me.LBTglPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Tanggal", "{0:dd MMM yy}")})
        Me.LBReady.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Ready", "{0:dd MMM yy}")})
        Me.LBETD.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.ETD", "{0:dd MMM yy}")})
        Me.LBETA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.ETA", "{0:dd MMM yy}")})

        Me.ShowPreview()

    End Sub

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class