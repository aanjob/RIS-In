Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XROutsPO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim TotQtyPO, TotQtyBtl, TotQtyLPB, TotQtyRtr, TotSisaQty, SisaQty As Decimal
    Dim Start As Integer = 0
    Dim No As Integer = 0
    Dim Cek As Integer = 0
    Dim cmSP As SqlCommand

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,POID,SuppID,Supp,BOMID,Tanggal,TglKirim,BBID,Bahan,QtyPO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal,TrmID,KdPOLPB,BOMIDLPB,BBIDLPB,SJ,TglLPB,Case When Rn=1 Then QtyLPB Else 0 End As QtyLPB,RtrID,TglRetur,QtyRetur,SisaQty From (Select ROW_NUMBER() OVER(ORDER BY POID Asc) AS Row,(DENSE_RANK() OVER (PARTITION BY TrmID,BBID,BOMID ORDER BY TrmID,RtrID,BBIDLPB asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY POID,BOMID,BBID ORDER BY POid,BBID,TrmID asc)) As Rn2,*,QtyPO-QtyBatal-QtyLPB-QtyRetur As SisaQty From (Select POID,SuppID,Supp,BOMID,Tanggal,TglKirim,BBID,Bahan,Sum(QtyPO) As QtyPO,sum(BtlOrder) As QtyBatal,TrmID,KdPOLPB, BOMIDLPB,BBIDLPB,SJ,TglLPB,Sum(QtyLPB) As QtyLPB,RtrID,TglRetur,Sum(QtyRetur) as QtyRetur From(Select P.POID,P.SuppID,S.Nama As Supp, PD.BOMID,P.Tanggal,TglKirim,PD.BBID,B.Nama As Bahan,sum(distinct PD.Qty) as 'QtyPO', isnull(sum(distinct PD.BtlOrder),0) as 'BtlOrder', T.TrmID,T.POID as 'KdPOLPB',TD.BOMID as 'BOMIDLPB',TD.BBID as 'BBIDLPB',T.SJ,T.Tanggal as 'TglLPB',isnull(sum(distinct TD.Qty),0) as 'QtyLPB',R.RtrID,R.Tanggal As 'TglRetur', isnull(sum(distinct RD.Qty),0) *-1 as 'QtyRetur' From T_POBB P inner join T_POBBDtl PD on P.POID = PD.POID left outer join T_TrmBBDtl TD on TD.POIDD=PD.POIDD left outer join T_TrmBB T on T.TrmID=TD.TrmID left outer join T_RtrBBDtl RD on RD.TrmIDD=TD.TrmIDD left outer join T_RtrBB R on R.RtrID =RD.RtrID and RD.TrmIDD=TD.TrmIDD Inner Join M_BB B On B.BBID=PD.BBID Inner Join M_Supp S On P.SuppID=S.SuppID Left Outer Join T_PRSpMDtl PT On PD.BOMID=PT.PRSMID and PD.DocIDD=PT.PRSMIDD Where P.Tanggal >='" & MainModule.PilihAwal & "' and P.Tanggal <='" & MainModule.PilihAkhir & "' and P.POID In (" & Bind.Item("PO").ToString & ") and PD.BBID In (" & Bind.Item("BBID").ToString & ") Group by P.POID,P.SuppID,S.Nama,PD.BOMID,P.Tanggal,TglKirim, PD.BBID,B.Nama,T.TrmID,TD.BOMID,TD.BBID,T.SJ,T.Tanggal,T.POID,R.RtrID, R.Tanggal,MesinID) as z Group By POID,SuppID,Supp,BOMID, Tanggal,TglKirim,BBID,Bahan,TrmID,BOMID,KdPOLPB,BOMIDLPB,BBIDLPB,SJ,TglLPB,RtrID,TglRetur) as x) as y order by POID asc, BBID asc", koneksi)

        'cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,POID,SuppID,Supp,BOMID,Tanggal,TglKirim,BBID,Bahan,QtyPO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal,TrmID,KdPOLPB,BOMIDLPB,BBIDLPB,SJ,TglLPB,Case When Rn=1 Then QtyLPB Else 0 End As QtyLPB,RtrID,TglRetur,QtyRetur,SisaQty From (Select ROW_NUMBER() OVER(ORDER BY POID Asc) AS Row,(DENSE_RANK() OVER (PARTITION BY TrmID,BBID,BOMID ORDER BY TrmID,RtrID,BBIDLPB asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY POID,BOMID,BBID ORDER BY POid,BBID,TrmID asc)) As Rn2,*,QtyPO-QtyBatal-QtyLPB-QtyRetur As SisaQty From (Select P.POID,P.SuppID,S.Nama As Supp, PD.BOMID, P.Tanggal , TglKirim , PD.BBID,B.Nama As Bahan,sum(distinct PD.Qty) as 'QtyPO', isnull(sum(distinct PD.BtlOrder),0) as 'QtyBatal', T.TrmID,T.POID as 'KdPOLPB',TD.BOMID as 'BOMIDLPB',TD.BBID as 'BBIDLPB',T.SJ, T.Tanggal as 'TglLPB', isnull(sum(distinct TD.Qty),0) as 'QtyLPB',R.RtrID,R.Tanggal As 'TglRetur', isnull(sum(distinct RD.Qty),0) *-1 as 'QtyRetur' From T_POBB P inner join T_POBBDtl PD on P.POID = PD.POID left outer join T_TrmBBDtl TD on TD.POIDD=PD.POIDD left outer join T_TrmBB T on T.TrmID=TD.TrmID left outer join T_RtrBBDtl RD on RD.TrmIDD=TD.TrmIDD left outer join T_RtrBB R on R.RtrID =RD.RtrID and RD.TrmIDD=TD.TrmIDD Inner Join M_BB B On B.BBID=PD.BBID Inner Join M_Supp S On P.SuppID=S.SuppID Where P.Tanggal >='" & MainModule.PilihAwal & "' and P.Tanggal <='" & MainModule.PilihAkhir & "' and P.POID In (" & Bind.Item("PO").ToString & ") and PD.BBID In (" & Bind.Item("BBID").ToString & ") Group by P.POID,P.SuppID,S.Nama, PD.BOMID, P.Tanggal , TglKirim, PD.BBID, B.Nama,T.TrmID,TD.BOMID,TD.BBID,T.SJ,T.Tanggal,T.POID, R.RtrID, R.Tanggal) as x) as y order by POID asc, BBID asc", koneksi)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "OutsPO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("TglKirim", "TglKirim"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("QtyPO", "QtyPO"), New System.Data.Common.DataColumnMapping("QtyBatal", "QtyBatal"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("KdPOLPB", "KdPOLPB"), New System.Data.Common.DataColumnMapping("BOMIDLPB", "BOMIDLPB"), New System.Data.Common.DataColumnMapping("SJ", "SJ"), New System.Data.Common.DataColumnMapping("TglLPB", "TglLPB"), New System.Data.Common.DataColumnMapping("QtyLPB", "QtyLPB"), New System.Data.Common.DataColumnMapping("RtrID", "RtrID"), New System.Data.Common.DataColumnMapping("TglRetur", "TglRetur"), New System.Data.Common.DataColumnMapping("QtyRetur", "QtyRetur"), New System.Data.Common.DataColumnMapping("SisaQty", "SisaQty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "OutsPO")

        Me.DataMember = "OutsPO"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHSupp.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Supp", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHPOID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("POID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBOMID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BOMID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Row")})
        Me.LBGHSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Supp")})
        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.POID")})
        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.BOMID")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Tanggal", "{0: dd/MM/yyyy}")})
        Me.LBTglKirim.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TglKirim", "{0: dd/MM/yyyy}")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Bahan")})
        Me.LBQtyPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyPO", "{0:#,##0.##}")})
        Me.LBQtyBtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyBatal", "{0:#,##0.##}")})
        Me.LBLPBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TrmID")})
        Me.LBReturID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.RtrID")})
        Me.LBSJL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SJ")})
        Me.LBTglLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TglLPB", "{0: dd/MM/yyyy}")})
        Me.LBTglRetur.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.TglRetur", "{0: dd/MM/yyyy}")})
        Me.LBQtyLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyLPB", "{0:#,##0.##}")})
        Me.LBQtyRtr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyRetur", "{0:#,##0.##}")})
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

    Private Sub GHBBID_BeforePrint(sender As Object, e As EventArgs) Handles GHBBID.BeforePrint
        Start = 0
    End Sub

    Private Sub GHPOID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHPOID.BeforePrint
        Start = 0
        Cek = 0
        No += 1
    End Sub

    Private Sub GHBOMID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHBOMID.BeforePrint
        Start = 0
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Start += 1
        Cek += 1
        LBNo.Text = No
    End Sub

    Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged
        If Start = 1 Then
            Me.LBPOID.Visible = True
            Me.LBBOMID.Visible = True
            Me.LBTgl.Visible = True
            Me.LBTglKirim.Visible = True
            Me.LBBahan.Visible = True
            Me.LBQtyPO.Visible = True
            Me.LBQtyBtl.Visible = True

        Else
            Me.LBPOID.Visible = False
            Me.LBBOMID.Visible = False
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

    Private Sub LBQtyRtr_BeforePrint(sender As Object, e As EventArgs) Handles LBQtyRtr.BeforePrint
        If CDec(Me.LBQtyRtr.Text) = 0 Then
            Me.LBQtyRtr.Visible = False
        Else
            Me.LBQtyRtr.Visible = True
        End If

        If Start <> 1 Then
            SisaQty -= CDec(Me.LBQtyRtr.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty)
        End If

        TotQtyRtr += CDec(Me.LBQtyRtr.Text)
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

    Private Sub LBGFQtyRtr_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyRtr.BeforePrint
        Me.LBGFQtyRtr.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyRtr)
    End Sub

    Private Sub LBGFQtyLPB_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyLPB.BeforePrint
        Me.LBGFQtyLPB.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyLPB)
    End Sub

    Private Sub LBGFSisaQty_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSisaQty.BeforePrint
        TotSisaQty = TotQtyPO - TotQtyBtl - TotQtyLPB - TotQtyRtr
        Me.LBGFSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotSisaQty)
    End Sub
End Class