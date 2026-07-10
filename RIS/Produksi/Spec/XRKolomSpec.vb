Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO

Public Class XRKolomSpec
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Distinct B.Nama As Bahan From M_SpecDtl SD Inner Join M_BB B On SD.BBID=B.BBID Where SpecID In (" & Bind.Item("SpecID").ToString & ") Order By B.Nama Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_SpecDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Bahan", "Bahan")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_SpecDtl")

        Me.DataMember = "M_SpecDtl"
        Me.DataSource = DsLap

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.Bahan")})
       
        Me.ShowPreview()
    End Sub

    Private Sub XRSpec_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class