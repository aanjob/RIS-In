Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPOBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotDisc, TotPPn, TotAkhir As Decimal

    Public Sub InitializeData(ByVal Bind As Collection, Dok As String)
        If Dok <> "Purchase Request" Then
            cmsl = New SqlDataAdapter("Select B.Nama As Bahan,Sum(Qty) As Qty,D.Sat,HarSat,Sum(HarSbDisc) As HarSbDisc, Sum(HarSbDisc)/1.1 As HarSbDiscInclude From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID Inner Join M_BB B On D.BBID=B.BBID Where H.POID ='" & Bind.Item("Kode").ToString & "' Group By B.Nama,D.Sat,HarSat,TipePPn", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select Bahan,Sum(Qty) As Qty,Sat,HarSat,Sum(HarSbDisc) As HarSbDisc,Sum(HarSbDisc)/1.1 As HarSbDiscInclude From( Select Case When Tipe='Tooling' Then B.Nama +(Select ' ('+ Ket +')' From T_PRToolDtl Where PRTID=D.BOMID and PRTIDD=DocIDD) Else B.Nama +(Select ' ('+ Ket +')' From T_PRSpMDtl Where PRSMID=D.BOMID and PRSMIDD=DocIDD) End As Bahan,Qty,D.Sat,HarSat,HarSbDisc From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID Inner Join M_BB B On D.BBID=B.BBID Where H.POID ='" & Bind.Item("Kode").ToString & "') as x Group By Bahan,Sat,HarSat", koneksi)
        End If

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_POBBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_POBBDtl")

        Me.DataMember = "T_POBBDtl"
        Me.DataSource = DsLap

        If CBool(Bind.Item("Manual").ToString) = False Then
            cmsl = New SqlDataAdapter("Select Distinct BOMID From T_POBBDtl Where POID ='" & Bind.Item("Kode").ToString & "'", koneksi)
            cmsl.Fill(DsLap, "BOM")

            Dim i : For i = 0 To DsLap.Tables("BOM").Rows.Count - 1
                If i = 0 Then
                    Me.LBBOM.Text = DsLap.Tables("BOM").Rows(i).Item(0)

                Else
                    Me.LBBOM.Text &= "," & DsLap.Tables("BOM").Rows(i).Item(0).ToString.Substring(4, DsLap.Tables("BOM").Rows(i).Item(0).ToString.Length - 4)
                End If
            Next
        End If


        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan & vbCrLf & MainModule.Alamat & vbCrLf & MainModule.Kota
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")

        If Bind.Item("Gol").ToString = "Sparepart-Mesin" Then
            Me.LBHeader.Text = "Purchase Order Sparepart/Mesin"
        End If

        If Bind.Item("Tipe").ToString = "Jasa" Then
            Me.LBHeader.Text = "Purchase Order Penjasaan"
        End If

        Me.LBSupp.Text = ": " & Bind.Item("Supp").ToString
        Me.LBAlamat.Text = ": " & Bind.Item("Alamat").ToString & vbCrLf & "  " & Bind.Item("Kota").ToString
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBTipePPn.Text = ": " & Bind.Item("TipePPn").ToString

        If Bind.Item("TipePPn").ToString <> "Non PPn" Then
            Me.XLBPPn.Text = "PPn (" & String.Format("{0:#,##0.##}", CDec(Bind.Item("PersenPPn").ToString)) & " %)"
        End If

        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("TglKirim")), "dd MMMM yyyy")
        Me.LBBayar.Text = ": " & Bind.Item("DueDate").ToString
        Me.LBKet.Text = Bind.Item("Ket").ToString
        'Me.LBNB.Text &= " " & MainModule.Fax
        Me.LBUser.Text = MainModule.LoginAktif
        Me.XLBHarga.Text = "Harga (" & Bind.Item("MtUang").ToString & ")"
        Me.XLBJml.Text = "Jumlah (" & Bind.Item("MtUang").ToString & ")"
        Me.XLBGrandTot.Text = "Grand Total (" & Bind.Item("MtUang").ToString & ")"

        TotDisc = CDec(Bind.Item("TotDisc").ToString)
        TotPPn = CDec(Bind.Item("TotPPn").ToString)
        TotAkhir = CDec(Bind.Item("TotAkhir").ToString)

        Me.LBTotDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotDisc)
        Me.LBPPn.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotPPn)
        Me.LBGrandTot.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotAkhir)

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Qty", "{0:#,##0.##}")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.HarSat", "{0:#,##0.##}")})
        Me.LBJml.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.HarSbDisc", "{0:#,##0.##}")})

        If Bind.Item("TipePPn").ToString = "Include" Then
            Me.LBSubTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.HarSbDiscInclude", "{0:n2}")})
        Else
            Me.LBSubTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.HarSbDisc", "{0:n2}")})
        End If

        SumSbDisc.FormatString = "{0:n2}"
        SumSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBSubTot.Summary = SumSbDisc

        Me.LBTotQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBBDtl.Qty", "{0:#,##0.##}")})
        SumQty.FormatString = "{0:#,##0.##}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBTotQty.Summary = SumQty

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        'If ReportFooter.Visible = True Then
        '    Me.XrLabel3.Visible = True
        '    Me.LBTotQty.Visible = True
        'Else
        'Me.XrLabel3.Visible = False
        'Me.LBTotQty.Visible = False
        'End If
        
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

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        'Me.XrLabel3.Visible = False
        'Me.LBTotQty.Visible = False
        If ReportFooter.Visible = True Then
            Me.LBTotDisc.Visible = True
            Me.LBPPn.Visible = True
            Me.LBGrandTot.Visible = True
            Me.LBSubTot.Visible = True
            Me.XrLabel3.Visible = True
            Me.LBTotQty.Visible = True

            Me.XLBTotDisc.Visible = True
            Me.XLBPPn.Visible = True
            Me.XLBGrandTot.Visible = True
            Me.XrLabel6.Visible = True
            Me.LBKota.Visible = True
            Me.LBTT1.Visible = True
            Me.LBTT2.Visible = True

        Else
            Me.LBTotDisc.Visible = False
            Me.LBPPn.Visible = False
            Me.LBGrandTot.Visible = False
            Me.LBSubTot.Visible = False
            Me.XrLabel3.Visible = False
            Me.LBTotQty.Visible = False

            Me.XLBTotDisc.Visible = False
            Me.XLBPPn.Visible = False
            Me.XLBGrandTot.Visible = False
            Me.XrLabel6.Visible = False

            Me.LBTT1.Visible = False
            Me.LBTT2.Visible = False
        End If
        
    End Sub

End Class