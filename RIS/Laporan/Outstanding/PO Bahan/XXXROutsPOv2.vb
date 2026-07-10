Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XXXROutsPOv2
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SisaQty As Decimal
    Dim Start As Integer = 0
    Dim Cek As Integer = 0
    Dim cmSP As SqlCommand

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,POID,SuppID,Supp,SJ,BBID,Bahan,QtyPO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal,KdPOLPB,SJLPB,BBIDLPB,SJ,Case When Rn=1 Then QtyLPB Else 0 End As QtyLPB,SisaQty From (Select ROW_NUMBER() OVER(ORDER BY POID Asc) AS Row,	(DENSE_RANK() OVER (PARTITION BY BBID,SJ ORDER BY BBIDLPB asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY POID,SJ,BBID ORDER BY POid,BBID asc)) As Rn2,*,QtyPO-QtyBatal-QtyLPB As SisaQty From (Select P.POID,P.SuppID,S.Nama As Supp,PD.BBID,B.Nama As Bahan,sum(distinct PD.Qty) as 'QtyPO', isnull(sum(distinct PD.BtlOrder),0) as 'QtyBatal',T.POID as 'KdPOLPB',T.SJ as 'SJLPB',TD.BBID as 'BBIDLPB',T.SJ, isnull(sum(distinct TD.Qty),0) as 'QtyLPB' From T_POBB P inner join T_POBBDtl PD on P.POID = PD.POID left outer join T_TrmBBDtl TD on TD.POIDD=PD.POIDD left outer join T_TrmBB T on T.TrmID=TD.TrmID  Inner Join M_BB B On B.BBID=PD.BBID Inner Join M_Supp S On P.SuppID=S.SuppID Where P.SuppID In ('X02') and P.POID In ('NPOR-1811-0487') Group by P.POID,P.SuppID,S.Nama, PD.BBID, B.Nama,T.SJ,TD.BBID,T.SJ,T.POID) as x ) as y order by POID asc, BBID asc", koneksi)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "OutsPO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("SJ", "SJ"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("QtyPO", "QtyPO"), New System.Data.Common.DataColumnMapping("QtyBatal", "QtyBatal"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("KdPOLPB", "KdPOLPB"), New System.Data.Common.DataColumnMapping("BOMIDLPB", "BOMIDLPB"), New System.Data.Common.DataColumnMapping("QtyLPB", "QtyLPB"), New System.Data.Common.DataColumnMapping("SisaQty", "SisaQty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "OutsPO")

        Me.DataMember = "OutsPO"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHSupp.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Supp", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHPOID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("POID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHInvID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("SJ", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Row")})
        Me.LBGHSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Supp", ": {0}")})
        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.POID", ": {0}")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Bahan", ": {0}")})
        Me.LBQtyPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyPO", "{0:#,##0.##}")})
        Me.LBQtyBtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyBatal", "{0:#,##0.##}")})
        Me.LBSJL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SJ")})
        Me.LBQtyLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyLPB", "{0:#,##0.##}")})
        Me.LBSisaQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SisaQty", "{0:#,##0.##}")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XROutsPO_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub


    Private Sub GHBBID_BeforePrint(sender As Object, e As EventArgs) Handles GHBBID.BeforePrint
        Start = 0
    End Sub

    Private Sub GHPOID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHPOID.BeforePrint
        Start = 0
        Cek = 0
    End Sub

    'Private Sub GHInvID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHInvID.BeforePrint
    '    Start = 0
    'End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Start += 1
        Cek += 1
    End Sub

    Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged
        If Start = 1 Then
            Me.LBPOID.Visible = True
            Me.LBBahan.Visible = True
            Me.LBQtyPO.Visible = True
            Me.LBQtyBtl.Visible = True

        Else
            Me.LBPOID.Visible = False
            Me.LBBahan.Visible = False
            Me.LBQtyPO.Visible = False
            Me.LBQtyBtl.Visible = False

        End If

    End Sub

    Private Sub LBSisaQty_BeforePrint(sender As Object, e As EventArgs) Handles LBSisaQty.BeforePrint
        If Start = 1 Then
            SisaQty = CDec(Me.LBSisaQty.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty)

            If Me.LBFcSisaQty.Text <> "" Then
                If Me.LBFcSisaQty.Text > 0 Then
                    Me.LBFcSisaQty.ForeColor = Color.Black

                Else
                    Me.LBFcSisaQty.ForeColor = Color.Red

                End If
            End If

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

    End Sub

End Class