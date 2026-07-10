Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekBOMv3v1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim No As Integer = 0
    Dim BOMID As String
    Dim DocID As String


    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select * From(Select Distinct B.DivPO,H.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotOrder,D.BBID, B.Nama,D.Sat From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.BOMID In (" & Bind.Item("DocID").ToString & ") and D.BBID In (" & Bind.Item("BBID").ToString & ") Union All Select Distinct B.DivPO,H.TambahanID,ArtName,Warna,0 As TotOrder,D.BBID, B.Nama,D.Sat From T_BOMTam H Inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join T_BOM BH On H.BOMID=BH.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.TambahanID In (" & Bind.Item("DocID").ToString & ") and D.BBID In (" & Bind.Item("BBID").ToString & ") Union All Select B.DivPO,H.MemoID,ArtName, Warna,0 As TotOrder,D.BBIDTj,B.Nama,D.SatTj From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM BH On D.BOMID=BH.BOMID Inner Join M_BB B On D.BBIDTj=B.BBID Where H.MemoID In (" & Bind.Item("DocID").ToString & ") and D.BOMID In (" & Bind.Item("BOMID").ToString & ") and D.BBIDTj In (" & Bind.Item("BBID").ToString & ") and H.stsApp='True') as x Order By Nama", koneksi)

        'cmsl = New SqlDataAdapter("Select * From(Select DivPO,BOMID,ArtName,Warna,TotOrder,BBID,Nama,Sat,Sum(Keb) AS Keb,Ket From(Select B.DivPO,H.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotOrder,D.BBID, B.Nama,D.Sat, Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=H.BOMID and BBIDAs=D.BBID and DivID=D.DivID and KompID=D.KompID and stsApp='True'),0)) As Keb,D.UkBB + ' ' + D.Ket As Ket From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.BOMID In (" & Bind.Item("DocID").ToString & ") and D.BBID In (" & Bind.Item("BBID").ToString & ") Group By B.DivPO,H.BOMID,ArtName,Warna,TotPsg,TotPsgPol,D.BBID,B.Nama,D.UkBB,D.Ket,D.Sat,DivID,KompID) as x Group By DivPO,BOMID, ArtName,Warna,TotOrder,BBID,Nama,Ket,Sat Union All Select B.DivPO,H.TambahanID,ArtName,Warna,0 As TotOrder,D.BBID, B.Nama,D.Sat,Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=H.TambahanID and BBIDAs=D.BBID and DivID=D.DivID and KompID=D.KompID and stsApp='True'),0)) As Keb,D.UkBB + ' ' + D.Ket As Ket From T_BOMTam H Inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join T_BOM BH On H.BOMID=BH.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.TambahanID In (" & Bind.Item("DocID").ToString & ") and D.BBID In (" & Bind.Item("BBID").ToString & ")  Group By B.DivPO,H.TambahanID,ArtName, Warna,D.BBID,B.Nama,D.UkBB,D.Ket,D.Sat,DivID,KompID Union All Select B.DivPO,H.MemoID,ArtName, Warna,0 As TotOrder,D.BBIDTj,B.Nama,D.SatTj, Round(Sum(KebTj),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where MemoIDRef=H.MemoID and BBIDAs=D.BBIDTj and stsApp='True'),0)) As Keb,D.UkBBTj + ' ' + D.Ket As Ket From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM BH On D.BOMID=BH.BOMID Inner Join M_BB B On D.BBIDTj=B.BBID Where H.MemoID In (" & Bind.Item("DocID").ToString & ") and D.BBIDTj In (" & Bind.Item("BBID").ToString & ") and H.stsApp='True' Group By B.DivPO,H.MemoID,ArtName,Warna,TotPsg, TotPsgPol,D.BBIDTj,B.Nama,D.UkBBTj,D.Ket,D.SatTj) as x Order By Nama", koneksi)

        DocID = Bind.Item("DocID").ToString
        BOMID = Bind.Item("BOMID").ToString


        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekBOM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("DivPO", "DivPO"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("TotOrder", "TotOrder"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Keb", "Keb"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        cmsl.SelectCommand.CommandTimeout = 90000

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "RekBOM")

        Me.DataMember = "RekBOM"
        Me.DataSource = DsLap

        Me.LBTotOrder.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", Bind.Item("TotOrder").ToString) & " Pasang"
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("DivPO", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBDivPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.DivPO", "Divisi PO : {0}")})
        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.BOMID")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Nama")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Sat")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "RekBOM.Sat")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()

    End Sub

    Private Sub LBBBID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBBBID.BeforePrint
        No += 1
    End Sub

    Private Sub LBNo_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBNo.BeforePrint
        Me.LBNo.Text = No
    End Sub
    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("BOMID").Value = Convert.ToString(BOMID)
        subRep.Parameters("DocID").Value = Convert.ToString(DocID)
        subRep.Parameters("BBID").Value = Convert.ToString(GetCurrentColumnValue("BBID"))

    End Sub

    Private Sub XRRekBOMv3_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class