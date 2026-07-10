Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XROpBJNS
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select M.Nama as Merk,D.ArtCode,ArtName,D.Isi From T_OpBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where OpBJID='" & Bind.Item("Kode").ToString & "' and QtyD<>0 Order By ArtCode Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_OpBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Merk", "Merk"), New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Isi", "Isi")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_OpBJDtl")

        Me.DataMember = "T_OpBJDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBMerk})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Merk", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBMerk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.Merk")})
        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.ArtCode")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.ArtName")})
        Me.LBIsi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_OpBJDtl.Isi")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XROpBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class