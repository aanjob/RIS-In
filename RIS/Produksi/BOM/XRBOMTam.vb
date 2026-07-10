Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRBOMTam
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim StdBB As Decimal
    Dim Start As Integer

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.Urut,D.Nama as Divisi,K.Urut,K.Nama As Komponen,B.Nama As Bahan,B.Uk As UkBB, BD.Sat, Sum(Keb)+Sum(Pol) as Keb, Round(Sum(Keb)+Sum(Pol),0) As TotKeb, BD.Ket From T_BOMTamDtl BD Inner Join M_Div D On BD.DivID=D.DivID Inner Join M_Komp K On BD.KompID=K.KompID Inner Join M_BB B On BD.BBID=B.BBID Where TambahanID='" & Bind.Item("Kode").ToString & "' Group By D.Urut,D.Nama,K.Urut,K.Nama,B.Nama,B.Uk, BD.Sat,BD.Ket Order By D.Urut,K.Urut,B.Nama Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_BOMTamDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Divisi", "Divisi"), New System.Data.Common.DataColumnMapping("Komponen", "Komponen"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("UkBB", "UkBB"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Keb", "Keb"), New System.Data.Common.DataColumnMapping("TotKeb", "TotKeb"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_BOMTamDtl")

        Me.DataMember = "T_BOMTamDtl"
        Me.DataSource = DsLap

        Me.LBBOMAsli.Text = ": " & Bind.Item("BOMID").ToString
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBBrand.Text = ": " & Bind.Item("Brand").ToString
        Me.LBStyle.Text = ": " & Bind.Item("StyleID").ToString
        Me.LBArtName.Text = ": " & Bind.Item("ArtName").ToString
        Me.LBSL.Text = ": " & Bind.Item("ShoeLast").ToString
        Me.LBWarna.Text = ": " & Bind.Item("Warna").ToString
        Me.LBHCBP.Text = ": " & String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("HCBP").ToString))
        Me.LBTotOrder.Text = ": " & String.Format("{0:#,#0.#}", CDec(Bind.Item("Tot").ToString))
        Me.LBKet.Text = Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHDiv.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBDivisi})
        Me.GHDiv.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Urut", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBDivisi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.Divisi")})
        Me.LBComponent.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.Komponen")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.Sat")})
        Me.LBUkBB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.UkBB")})
        Me.LBKebBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.Keb")})
        Me.LBTotKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.TotKeb")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMTamDtl.Ket")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSpec_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class