Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Public Class XRKet
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()
    End Sub

    Private Sub XRPNPsvMdl_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        cmsl = New SqlDataAdapter("Select Line,Style,Hasil,TKL,Jam From T_HProdKet Where Tanggal ='" & CDate(Tanggal.Value) & "' and Unit ='" & CStr(Unit.Value) & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_HProdKet", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Line", "Line"), New System.Data.Common.DataColumnMapping("Style", "Style"), New System.Data.Common.DataColumnMapping("Hasil", "Hasil"), New System.Data.Common.DataColumnMapping("TKL", "TKL"), New System.Data.Common.DataColumnMapping("Jam", "Jam")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_HProdKet")

        Me.DataMember = "T_HProdKet"
        Me.DataSource = DsLap

        Me.LBLine.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HProdKet.Line")})
        Me.LBStyle.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HProdKet.Style")})
        Me.LBHasil.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HProdKet.Hasil")})
        Me.LBTKL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HProdKet.TKL")})
        Me.LBJam.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_HProdKet.Jam")})
    End Sub


End Class