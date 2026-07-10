Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRLapBrg
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand

    Public Sub InitializeData(ByVal Bind As Collection)

        'cmsl = New SqlDataAdapter("Select * From (Select B.ArtCode,ArtName,B.Isi,M.Nama As Merk, K.Nama As Kategori, J.Nama as Jenis,H.JnsCustID,stsHarga,H.Harga,H.Harga As HargaSP ,HargaCBP,DiscOB From M_Brg B Inner Join M_BrgMerk M On B.MerkID=M.MerkID Inner Join M_BrgKat K On B.KatID=K.KatID Inner Join M_BrgJns J On B.JnsID=J.JnsID Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Inner Join M_JnsCust JC On H.JnsCustID=JC.JnsCustID Where Gol='" & Bind.Item("Sal").ToString & "' and H.Aktif='True' ) x Pivot (Max(Harga) For JnsCustID In (ADPJ,AFRK,GDPJ,GFRK,RDPJ,RFRK)) As Pv1 Pivot (Max(HargaSP) For stsHarga In (SP)) As Pv2", koneksi)

        'Dim cmSP As New SqlCommand("SPLapBrg")
        'cmSP.CommandType = CommandType.StoredProcedure
        'cmSP.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Bind.Item("Gol").ToString
        'cmSP.Connection = koneksi
        'cmsl.SelectCommand = cmSP

        cmsl = New SqlDataAdapter("SPLBrg", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Bind.Item("Gol").ToString

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_Brg", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Isi", "Isi"), New System.Data.Common.DataColumnMapping("Merk", "Merk"), New System.Data.Common.DataColumnMapping("Kategori", "Kategori"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("HargaCBP", "HargaCBP"), New System.Data.Common.DataColumnMapping("DiscOB", "DiscOB"), New System.Data.Common.DataColumnMapping("ADPJ", "ADPJ"), New System.Data.Common.DataColumnMapping("AFRK", "AFRK"), New System.Data.Common.DataColumnMapping("GDPJ", "GDPJ"), New System.Data.Common.DataColumnMapping("GFRK", "GFRK"), New System.Data.Common.DataColumnMapping("RDPJ", "RDPJ"), New System.Data.Common.DataColumnMapping("RFRK", "RFRK"), New System.Data.Common.DataColumnMapping("SP", "SP")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_Brg")

        Me.DataMember = "M_Brg"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHMerk.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHMerk})
        Me.GHMerk.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Merk", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHKategori.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHKategori})
        Me.GHKategori.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Kategori", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHJenis.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHJenis})
        Me.GHJenis.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Jenis", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        If MainModule.Posisi Like "*Cabang" Then
            Me.LBGHMerk.Visible = False
        Else
            Me.LBGHMerk.Visible = True
        End If

        Me.LBGHMerk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.Merk")})
        Me.LBGHKategori.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.Kategori")})
        Me.LBGHJenis.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.Jenis")})
        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.ArtCode")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.ArtName")})
        Me.LBIsi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.Isi")})
        Me.LBAgenDPJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.ADPJ", "{0:n2}")})
        Me.LBAgenLPJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.AFRK", "{0:n2}")})
        Me.LBGrosirDPJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.GDPJ", "{0:n2}")})
        Me.LBGrosirLPJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.GFRK", "{0:n2}")})
        Me.LBRetailDPJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.RDPJ", "{0:n2}")})
        Me.LBRetailLPJ.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.RFRK", "{0:n2}")})
        Me.LBCBP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.HargaCBP", "{0:n2}")})
        Me.LBDiscOB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.DiscOB", "{0:n3}")})
        Me.LBSP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Brg.SP", "{0:n2}")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRLapBrg_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class