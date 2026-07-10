Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XROutsPOBJJO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim TotQtyPO, TotQtyBtl, TotQtyLPB, TotQtyRtr, TotSisaQty, SisaQty As Decimal
    Dim Start As Integer = 0
    Dim No As Integer = 0
    Dim Cek As Integer = 0
    Dim cmSP As SqlCommand

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,POID,SuppID,Supp,Tanggal,TglKirim,ArtCode,ArtCode+' ('+ArtName+')' As ArtName,QtyPO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal,TrmID,KdPOLPB,ArtCodeLPB,SJ,TglLPB,Case When Rn=1 Then QtyLPB Else 0 End As QtyLPB,SisaQty From (Select ROW_NUMBER() OVER(ORDER BY POID Asc) AS Row,(DENSE_RANK() OVER (PARTITION BY TrmID,ArtCode ORDER BY TrmID,ArtCodeLPB asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY POID,ArtCode ORDER BY POid,ArtCode,trmid asc)) As Rn2,*,QtyPO-QtyBatal-QtyLPB As SisaQty From (Select P.POID,P.SuppID, S.Nama As Supp,P.Tanggal,TglKirim,PD.ArtCode,B.ArtName As ArtName,sum(distinct PD.Qty) as 'QtyPO', isnull(sum(distinct PD.BtlOrder),0) as 'QtyBatal',T.TrmID,T.POID as 'KdPOLPB',TD.ArtCode as 'ArtCodeLPB',T.SJ, T.Tanggal as 'TglLPB', isnull(sum(distinct TD.Qty),0) as 'QtyLPB' From T_POBJJO P inner join T_POBJJODtl PD on P.POID = PD.POID left outer join T_TrmBJDtl TD on TD.ArtCode=PD.ArtCode and P.POID=TD.POID left outer join T_TrmBJ T on T.TrmID=TD.TrmID Inner Join M_Brg B On B.ArtCode=PD.ArtCode left outer Join M_Supp S On P.SuppID=S.SuppID Where P.POID In (" & Bind.Item("POID").ToString & ") and PD.ArtCode In (" & Bind.Item("ArtCode").ToString & ") Group by P.POID,P.SuppID,S.Nama,P.Tanggal, TglKirim,PD.ArtCode,B.ArtName,T.TrmID,TD.ArtCode,T.SJ,T.Tanggal, T.POID)as x)as y order by POID asc, ArtCode asc", koneksi)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "OutsPO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("TglKirim", "TglKirim"), New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("QtyPO", "QtyPO"), New System.Data.Common.DataColumnMapping("QtyBatal", "QtyBatal"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("KdPOLPB", "KdPOLPB"), New System.Data.Common.DataColumnMapping("SJ", "SJ"), New System.Data.Common.DataColumnMapping("TglLPB", "TglLPB"), New System.Data.Common.DataColumnMapping("QtyLPB", "QtyLPB"), New System.Data.Common.DataColumnMapping("SisaQty", "SisaQty")})})


        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "OutsPO")

        Me.DataMember = "OutsPO"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHSupp.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Supp", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHPOID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("POID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHArtCode.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ArtCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Row")})
        Me.LBGHSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Supp")})
        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.POID")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Tanggal", "{0: dd/MM/yyyy}")})
        Me.LBTglKirim.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TglKirim", "{0: dd/MM/yyyy}")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.ArtName")})
        Me.LBQtyPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyPO", "{0:#,##0.##}")})
        Me.LBQtyBtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyBatal", "{0:#,##0.##}")})
        Me.LBLPBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TrmID")})
        Me.LBSJL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SJ")})
        Me.LBTglLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TglLPB", "{0: dd/MM/yyyy}")})
        Me.LBQtyLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyLPB", "{0:#,##0.##}")})
        Me.LBSisaQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SisaQty", "{0:#,##0.##}")})

        Me.GFSupp.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFQtyPO, Me.LBGFQtyBtl, Me.LBGFQtyLPB, Me.LBGFQtyBtl, Me.LBGFSisaQty})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XROutsPO_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub GHSupp_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHSupp.BeforePrint
        No = 0
        TotSisaQty = 0
        TotQtyPO = 0
        TotQtyBtl = 0
        TotQtyLPB = 0
        TotQtyRtr = 0
    End Sub

    Private Sub GHArtCode_BeforePrint(sender As Object, e As EventArgs) Handles GHArtCode.BeforePrint
        Start = 0
    End Sub

    Private Sub GHPOID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHPOID.BeforePrint
        Start = 0
        Cek = 0
        No += 1
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Start += 1
        Cek += 1
        LBNo.Text = No
    End Sub

    Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged
        If Start = 1 Then
            Me.LBPOID.Visible = True
            Me.LBTgl.Visible = True
            Me.LBTglKirim.Visible = True
            Me.LBBahan.Visible = True
            Me.LBQtyPO.Visible = True
            Me.LBQtyBtl.Visible = True

        Else
            Me.LBPOID.Visible = False
            Me.LBTgl.Visible = False
            Me.LBTglKirim.Visible = False
            Me.LBBahan.Visible = False
            Me.LBQtyPO.Visible = False
            Me.LBQtyBtl.Visible = False

        End If

        If Cek = 1 Then
            Me.LBNo.Visible = True
        Else
            Me.LBNo.Visible = False
        End If

    End Sub

    Private Sub LBSisaQty_BeforePrint(sender As Object, e As EventArgs) Handles LBSisaQty.BeforePrint
        If Start = 1 Then
            SisaQty = CDec(Me.LBSisaQty.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty)
        End If
    End Sub

    Private Sub LBQtyLPB_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBQtyLPB.BeforePrint
        If CDec(Me.LBQtyLPB.Text) = 0 Then
            Me.LBQtyLPB.Visible = False
        Else
            Me.LBQtyLPB.Visible = True
        End If

        If Start <> 1 Then
            SisaQty -= CDec(Me.LBQtyLPB.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty)
        End If

        TotQtyLPB += CDec(Me.LBQtyLPB.Text)
    End Sub

    Private Sub LBQtyBtl_BeforePrint(sender As Object, e As EventArgs) Handles LBQtyBtl.BeforePrint
        If CDec(Me.LBQtyBtl.Text) = 0 Then
            Me.LBQtyBtl.Visible = False
        Else
            Me.LBQtyBtl.Visible = True
        End If

        If Start <> 1 Then
            SisaQty -= CDec(Me.LBQtyBtl.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty)
        End If

        If Start = 1 Then
            TotQtyBtl += CDec(Me.LBQtyBtl.Text)
        End If

    End Sub

    Private Sub LBQtyPO_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBQtyPO.BeforePrint
        If Start = 1 Then
            TotQtyPO += CDec(Me.LBQtyPO.Text)
        End If
    End Sub

    Private Sub LBGFQtyPO_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyPO.BeforePrint
        Me.LBGFQtyPO.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyPO)
    End Sub

    Private Sub LBGFQtyBtl_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyBtl.BeforePrint
        Me.LBGFQtyBtl.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyBtl)
    End Sub

    Private Sub LBGFQtyLPB_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyLPB.BeforePrint
        Me.LBGFQtyLPB.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyLPB)
    End Sub

    Private Sub LBGFSisaQty_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSisaQty.BeforePrint
        TotSisaQty = TotQtyPO - TotQtyBtl - TotQtyLPB - TotQtyRtr
        Me.LBGFSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotSisaQty)
    End Sub
End Class