Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRLapSupp
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotDos, TotPsg As Integer
    Dim Start As Integer = 0

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("Select SuppID, S.Nama, Alamat, K.Nama As Kota,P.Nama As Propinsi, NPWP, NoTelp,NoHp, Fax, Email, JT From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Inner Join M_Prop P On K.PropID=P.PropID", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_Supp", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Alamat", "Alamat"), New System.Data.Common.DataColumnMapping("Kota", "Kota"), New System.Data.Common.DataColumnMapping("Propinsi", "Propinsi"), New System.Data.Common.DataColumnMapping("NPWP", "NPWP"), New System.Data.Common.DataColumnMapping("NoTelp", "NoTelp"), New System.Data.Common.DataColumnMapping("NoHp", "NoHp"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("Email", "Email"), New System.Data.Common.DataColumnMapping("JT", "JT")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_Supp")

        Me.DataMember = "M_Supp"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBSuppID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.SuppID")})
        Me.LBNPWP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.NPWP")})
        Me.LBSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.Nama")})
        Me.LBTOP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.JT")})
        Me.LBAlamat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.Alamat")})
        Me.LBKota.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.Kota")})
        Me.LBProp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.Propinsi")})
        Me.LBTelp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.NoTelp")})
        Me.LBHp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.NoHp")})
        Me.LBFax.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Supp.Fax")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRLapCust_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class