Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRAssOrder
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()
    End Sub

    Private Sub XRAss_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        cmsl = New SqlDataAdapter("Select Uk,IsiDlmDos,Qty From T_POBJLKDtl2 Where POIDDtl =" & CInt(POIDDtl.Value) & " and POID ='" & CStr(POID.Value) & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_POBJLKDtl2", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("IsiDlmDos", "IsiDlmDos"), New System.Data.Common.DataColumnMapping("Qty", "Qty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_POBJLKDtl2")

        Me.DataMember = "T_POBJLKDtl2"
        Me.DataSource = DsLap

        Me.LBUk1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLKDtl2.Uk")})
        Me.LBAs1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLKDtl2.IsiDlmDos")})
        Me.LBQtyAs1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLKDtl2.Qty")})
    End Sub

End Class