Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting

Public Class XXXRSJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumDos As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotSbDisc, TotDisc, TotDos, TotPsg As Integer

    Public Sub InitializeData(ByVal Bind As Collection)
        'cmsl = New SqlDataAdapter("Select * From (Select D.ArtCode, Concat (D.ArtCode,' ( ',B.ArtName,' ',W.Nama,' )') As Barang,HarSat,Qty,Dos,Psg, HarSbDisc,DiscOB,RpDiscOB+RpDiscCust+RpDiscL As DiscDet,HarAkhir From T_JualBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where JualID ='" & Bind.Item("JualID").ToString & "' Union All Select D.ArtCode, Concat (D.ArtCode,' ( ',ArtName,' ',W.Nama,' )') As Barang,0 As HarSat,Qty,Dos,Psg,0 As HarSbDisc,0 As DiscOB,0 As DiscDet,0 As HarAkhir From T_JualBJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where JualID ='" & Bind.Item("JualID").ToString & "') As x Order By ArtCode", koneksi)

        cmsl = New SqlDataAdapter("Select *,Concat (ArtCode,' ',Ass,' ( ',ArtName,' ',Nama,' )') As Barang From (Select *, Case When SatID='P' Then '' Else CONCAT('(',Uk1,'-',Uk2,')') End As Ass  From (Select D.ArtCode,B.ArtName,W.Nama,D.SatID,(Select Min (Uk) From M_BrgAssDtl where AssID=B.AssID) As Uk1,(Select Max (Uk) From M_BrgAssDtl where AssID=B.AssID) As Uk2,HarSat,Qty,Dos,Psg, HarSbDisc,DiscOB,RpDiscOB+RpDiscCust As DiscDet,HarAkhir From T_SJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where SJID ='" & Bind.Item("Kode").ToString & "') as x Union All Select *, Case When SatID='P' Then '' Else CONCAT('(',Uk1,'-',Uk2,')') End As Ass  From (Select D.ArtCode,ArtName,W.Nama,D.SatID,(Select min (Uk) From M_BrgAssDtl where AssID=B.AssID) As Uk1,(Select Max (Uk) From M_BrgAssDtl where AssID=B.AssID) As Uk2,0 As HarSat,Qty,Dos,Psg,0 As HarSbDisc,0 As DiscOB,0 As DiscDet,0 As HarAkhir From T_SJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where SJID ='" & Bind.Item("Kode").ToString & "') as x) as xyz Order By ArtCode", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_JualBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Barang", "Barang"), New System.Data.Common.DataColumnMapping("Dos", "Dos"), New System.Data.Common.DataColumnMapping("Psg", "Psg"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("DiscOB", "DiscOB"), New System.Data.Common.DataColumnMapping("DiscDet", "DiscDet"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_JualBJDtl")

        Me.DataMember = "T_JualBJDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = Bind.Item("Kode").ToString
        Me.LBIDNotPes.Text = ": " & Bind.Item("NPID").ToString
        Me.LBTanggal.Text = ": " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBAlamat.Text = ": " & Bind.Item("Alamat").ToString
        If Bind.Item("Promo").ToString = "" Then
            Me.LBPromo.Text = ": "
        Else
            Me.LBPromo.Text = ": " & Bind.Item("Promo").ToString & " Paket : " & Bind.Item("Paket").ToString
        End If
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString

        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBTotDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotDisc").ToString))
        Me.LBTotByExpd.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotByExp").ToString))
        Me.LBTotPPn.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotPPn").ToString))
        Me.LBTotAkhir.Text = String.Format("{0:#,##0.00;(#,##0.00)}", CDec(Bind.Item("TotAkhir").ToString))

        Me.LBBarang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Barang")})
        Me.LBDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Dos", "{0:n0}")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Psg", "{0:n0}")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.HarSat", "{0:n2}")})
        Me.LBDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.DiscOB", "{0:n3}")})
        Me.LBHarAkhir.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.HarSbDisc", "{0:n2}")})

        Me.LBTotSbDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.HarSbDisc", "{0:n2}")})
        SumSbDisc.FormatString = "{0:n2}"
        SumSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotSbDisc.Summary = SumSbDisc

        'Me.LBTotDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.DiscDet", "{0:n2}")})
        'SumDisc.FormatString = "{0:n2}"
        'SumDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        'Me.LBTotDisc.Summary = SumDisc

        Me.LBTotDos.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Dos", "{0:n0}")})
        SumDos.FormatString = "{0:n0}"
        SumDos.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotDos.Summary = SumDos

        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBJDtl.Psg", "{0:n0}")})
        SumPsg.FormatString = "{0:n0}"
        SumPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotPsg.Summary = SumPsg

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select URLPic From M_ImageUrl Where ID='" & Bind.Item("NPID").ToString & "'", koneksi)
        Dim namaFile As String

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        namaFile = ""
                    Else
                        namaFile = Reader.Item(0)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        If namaFile <> "" Then
            Me.PFoto.ImageUrl = namaFile
        End If

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        'If DateTime.Now < DateAdd(DateInterval.Day, 7, CDate(Bind.Item("Tanggal"))) Then
        Me.ShowPreview()
        'End If
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            'Me.LBTotSbDisc.Visible = True
            'Me.LBTotDiscDet.Visible = True
            Me.LBTotDisc.Visible = True
            Me.LBTotByExpd.Visible = True
            Me.LBTotPPn.Visible = True
            Me.LBTotAkhir.Visible = True

            Me.LBTxTotDisc.Visible = True
            Me.LBTxTotByExpd.Visible = True
            Me.LBTxTotPPn.Visible = True
            Me.LBTxTotAkhir.Visible = True

        Else
            'Me.LBTotSbDisc.Visible = False
            'Me.LBTotDiscDet.Visible = False
            Me.LBTotDisc.Visible = False
            Me.LBTotByExpd.Visible = False
            Me.LBTotPPn.Visible = False
            Me.LBTotAkhir.Visible = False

            Me.LBTxTotDisc.Visible = False
            Me.LBTxTotByExpd.Visible = False
            Me.LBTxTotPPn.Visible = False
            Me.LBTxTotAkhir.Visible = False

        End If
    End Sub

    Private Sub XRFtBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class