Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRBPSpM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection, Gol As String)
        Dim command As New SqlCommand("Select ISOID From M_DocCode Where DocID=8 and CabID='" & Gol & "'", koneksi)

        koneksi.Close()

        With koneksi
            .Open()
            Me.LBIDISO.Text = command.ExecuteScalar()
            .Close()
        End With

        cmsl = New SqlDataAdapter("Select (Select BtNum + ', '  From (Select Distinct BtNum From T_BPBDtl Where BBID=B.BBID and BPBID='" & Bind.Item("Kode").ToString & "') as q order By BtNum FOR XML PATH('')) As BtNum,MesinID,'('+ B.BBID +') ' + Nama As Bahan,D.Sat,Qty From T_BPB H Inner Join T_BPBDtl D On H.BPBID=D.BPBID Inner Join M_BB B On D.BBID=B.BBID Where H.BPBID ='" & Bind.Item("Kode").ToString & "' Order By Nama", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_BPBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BtNum", "BtNum"), New System.Data.Common.DataColumnMapping("MesinID", "MesinID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_BPBDtl")

        Me.DataMember = "T_BPBDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBHeader.Text = Me.LBHeader.Text & " " & Bind.Item("Gol").ToString
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBBagian.Text = ": " & Bind.Item("Bagian").ToString
        Me.LBUnit.Text = ": " & Bind.Item("Unit").ToString
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBtNum.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.BtNum")})
        Me.LBMesinID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.MesinID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Qty", "{0:#,##0.00;(#,##0.00);""}")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
            Me.LBNB.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
            Me.ShowPreview()
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
            Me.ShowPreview()
        End If

    End Sub

    Private Sub XRBPB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class