Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO

Public Class XRSpec
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select SD.DivID,D.Urut,D.Nama as Divisi, K.Nama As Komponen, SD.BBID, B.Nama As Bahan,SD.Ket, SD.Sat From M_SpecDtl SD Inner Join M_Div D On SD.DivID=D.DivID Inner Join M_Komp K On SD.KompID=K.KompID Inner Join M_BB B On SD.BBID=B.BBID Where SpecID ='" & Bind.Item("Kode").ToString & "' Order By D.Urut,K.Nama,B.Nama,BBIDInd Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_SpecDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Divisi", "Divisi"), New System.Data.Common.DataColumnMapping("Komponen", "Komponen"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Ket", "Ket"), New System.Data.Common.DataColumnMapping("Sat", "Sat")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_SpecDtl")

        Me.DataMember = "M_SpecDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBBrand.Text = ": " & Bind.Item("Brand").ToString
        Me.LBKet.Text = Bind.Item("Ket").ToString
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBStyle.Text = ": " & Bind.Item("StyleID").ToString
        Me.LBArtName.Text = ": " & Bind.Item("ArtName").ToString
        Me.LBWarna.Text = ": " & Bind.Item("Warna").ToString
        Me.LBSL.Text = ": " & Bind.Item("ShoeLast").ToString
        Me.LBSample.Text = ": " & Bind.Item("SampleSize").ToString
        Me.LBRange.Text = ": " & Bind.Item("RangeSize").ToString
        'Me.LBDibuat.Text = Bind.Item("Dibuat").ToString
        'Me.LBPattern.Text = Bind.Item("Pattern").ToString
        Me.LBChaser.Text = Bind.Item("Chaser").ToString
        'Me.LBPPC.Text = Bind.Item("PPC").ToString
        Me.LBPembKulit.Text = Bind.Item("PembKulit").ToString
        'Me.LBPembNonKulit.Text = Bind.Item("PembNonKulit").ToString
        Me.LBTechnical.Text = Bind.Item("Teknik").ToString
        Me.LBMengetahui.Text = Bind.Item("Mengetahui").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBDivisi})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Urut", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBDivisi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.Divisi")})
        Me.LBComponent.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.Komponen")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.Sat")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_SpecDtl.Ket")})

        Dim command As New SqlCommand("Select Picture From M_Image Where ID = '" & Bind.Item("Kode").ToString & "'", koneksi)
        Dim Pic() As Byte
        Dim newImage As Image

        With koneksi
            .Open()
            Pic = command.ExecuteScalar()
            .Close()
        End With

        If Pic IsNot Nothing Then
            Using ms As New MemoryStream(Pic, 0, Pic.Length)
                ms.Write(Pic, 0, Pic.Length)
                newImage = Image.FromStream(ms, True)
            End Using
            Me.PGambar.Image = newImage
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSpec_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class