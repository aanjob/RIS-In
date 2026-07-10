Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Public Class XRReqQty
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Public Sub InitializeData()
    End Sub

    Private Sub XRAss_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        cmsl = New SqlDataAdapter("Select *,Round((TotQty/TotBOM)*100,2) As Persen From (Select Distinct BOMID,RD.ArtCode,ArtName,W.Nama As Warna,Uk,Ket,Qty, (Select Isnull((Select Sum(Qty) From T_ReqPQty Where BOMID=RD.BOMID),0)) As TotQty, (Select Isnull((Select TotPsg From T_BOM Where BOMID=RD.BOMID),0)) As TotBOM From T_ReqPQty RD Inner Join M_Brg Br On RD.ArtCode=Br.ArtCode Inner Join M_BrgWrn W On Br.WrnID=W.WrnID Where ReqPID='" & CStr(ReqID.Value) & "' ) as x", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_ReqPDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_ReqPDtl")

        Me.DataMember = "T_ReqPDtl"
        Me.DataSource = DsLap

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBBOMID})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BOMID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.BOMID", ": {0}")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.ArtName", ": {0}")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.Warna", ": {0}")})
        Me.LBPersen.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.Persen", ": {0:n2} %")})
        Me.LBUk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.Uk")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.Qty")})
        Me.LBKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_ReqPDtl.Ket", "{0} ")})
    End Sub
End Class