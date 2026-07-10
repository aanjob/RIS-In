Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XROutsPOImpv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SisaQty, TotFcPO, TotFcTrm, TotInv, TotAct, TotRj As Decimal
    Dim Start As Integer = 0
    Dim StartInv As Integer = 0
    Dim StartHitInv As Integer = 0
    Dim StartPO As Integer = 0
    Dim No As Integer = 0
    Dim cmSP As SqlCommand
    Dim SumPO As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTrm As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumBtl As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,POID,TrmID,SuppID,Supp,SJ,BBID,Bahan,Sat,Case When Rn2=1 Then QtyPO Else 0 End As QtyPO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal, KdPOLPB,SJLPB,BBIDInv,SJ,Case When Rn=1 Then QtyLPB Else 0 End As QtyLPB,Case When Rn=1 Then QtyPL Else 0 End As QtyPL,Case When Rn=1 Then QtyAct Else 0 End As QtyAct,QtyRj, SisaQty From (Select ROW_NUMBER() OVER(ORDER BY POID Asc) AS Row,(DENSE_RANK() OVER (PARTITION BY BBID,SJ ORDER BY BBIDInv asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY POID,BBID ORDER BY POID,TrmID,BBID,SJLPB asc)) As Rn2,*,QtyPO-QtyBatal-QtyLPB As SisaQty From (Select P.POID,T.TrmID,P.SuppID,S.Nama As Supp,PD.BBID,B.Nama As Bahan,B.Sat,T.Tanggal,(Select Isnull((Select Sum(Qty) From T_POBBDtl where POID=P.POID and BBID=PD.BBID),0)) as 'QtyPO', isnull(sum(distinct PD.BtlOrder),0) as 'QtyBatal',T.POID as 'KdPOLPB', T.SJ as 'SJLPB',PD.BBID+T.SJ as 'BBIDInv',T.SJ, isnull(sum(distinct TD.QtyPL),0) as 'QtyPL',isnull(sum(distinct TD.QtyAct),0) as 'QtyAct', isnull(sum(distinct TD.QtyRj),0) as 'QtyRj', isnull(sum(TD.Qty),0) as 'QtyLPB' From T_POBB P inner join T_POBBDtl PD on P.POID = PD.POID left outer join T_TrmBBDtl TD on TD.POIDD=PD.POIDD left outer join T_TrmBB T on T.TrmID=TD.TrmID Inner Join M_BB B On B.BBID=PD.BBID Inner Join M_Supp S On P.SuppID=S.SuppID Where T.POID In (" & Bind.Item("PO").ToString & ")  and PD.BBID In (" & Bind.Item("BBID").ToString & ") Group by P.POID,P.SuppID,S.Nama,PD.BBID,B.Nama,B.Sat, T.SJ,TD.BBID,T.SJ,T.POID,T.TrmID,T.Tanggal) as x ) as y order by POID asc,Tanggal,Bahan asc", koneksi)

        'cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,POID,SuppID,Supp,SJ,BBID,Bahan,Sat,Case When Rn2=1 Then QtyPO Else 0 End As QtyPO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal, KdPOLPB,SJLPB,BBIDInv,SJ,Case When Rn=1 Then QtyLPB Else 0 End As QtyLPB,Case When Rn=1 Then QtyPL Else 0 End As QtyPL,Case When Rn=1 Then QtyAct Else 0 End As QtyAct,QtyRj, SisaQty From (Select ROW_NUMBER() OVER(ORDER BY POID Asc) AS Row,(DENSE_RANK() OVER (PARTITION BY BBID,SJ ORDER BY BBIDInv asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY POID,BBID ORDER BY POID,BBID,SJLPB asc)) As Rn2,*,Case When QtyPO>QtyPL Then QtyPO-QtyBatal-QtyLPB Else QtyPL-QtyBatal-QtyLPB End As SisaQty  From (Select P.POID,P.SuppID, S.Nama As Supp,PD.BBID,B.Nama As Bahan,B.Sat,sum(distinct PD.Qty) as 'QtyPO', isnull(sum(distinct PD.BtlOrder),0) as 'QtyBatal',T.POID as 'KdPOLPB', T.SJ as 'SJLPB',PD.BBID+T.SJ as 'BBIDInv',T.SJ, isnull(sum(distinct TD.QtyPL),0) as 'QtyPL',isnull(sum(distinct TD.QtyAct),0) as 'QtyAct', isnull(sum(distinct TD.QtyRj),0) as 'QtyRj', isnull(sum(distinct TD.Qty),0) as 'QtyLPB' From T_POBB P inner join T_POBBDtl PD on P.POID = PD.POID left outer join T_TrmBBDtl TD on TD.POIDD=PD.POIDD left outer join T_TrmBB T on T.TrmID=TD.TrmID Inner Join M_BB B On B.BBID=PD.BBID Inner Join M_Supp S On P.SuppID=S.SuppID Where T.POID In (" & Bind.Item("PO").ToString & ")  and PD.BBID In (" & Bind.Item("BBID").ToString & ") Group by P.POID,P.SuppID,S.Nama,PD.BBID,B.Nama,B.Sat, T.SJ,TD.BBID,T.SJ,T.POID) as x ) as y order by POID asc, BBID asc", koneksi)

        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "OutsPO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("SuppID", "SuppID"), New System.Data.Common.DataColumnMapping("Supp", "Supp"), New System.Data.Common.DataColumnMapping("SJ", "SJ"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("BBIDInv", "BBIDInv"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("QtyPO", "QtyPO"), New System.Data.Common.DataColumnMapping("QtyPL", "QtyPL"), New System.Data.Common.DataColumnMapping("QtyAct", "QtyAct"), New System.Data.Common.DataColumnMapping("QtyRj", "QtyRj"), New System.Data.Common.DataColumnMapping("QtyBatal", "QtyBatal"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("KdPOLPB", "KdPOLPB"), New System.Data.Common.DataColumnMapping("BOMIDLPB", "BOMIDLPB"), New System.Data.Common.DataColumnMapping("QtyLPB", "QtyLPB"), New System.Data.Common.DataColumnMapping("SisaQty", "SisaQty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "OutsPO")

        Me.DataMember = "OutsPO"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHSupp.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Supp", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Bahan", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHPOID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("POID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHTrmID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("TrmID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHInvID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBIDInv", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBBBIDLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.BBIDInv")})
        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Row")})
        Me.LBGHSupp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Supp", ": {0}")})
        Me.LBPOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.POID", ": {0}")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Bahan", ": {0}")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.Sat", ": {0}")})
        Me.LBQtyPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyPO", "{0:#,##0.##}")})
        Me.LBQtyBtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyBatal", "{0:#,##0.##}")})
        Me.LBSJL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SJ")})
        Me.LBQtyLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyLPB", "{0:#,##0.##}")})
        Me.LBQtyPL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyPL", "{0:#,##0.##}")})
        Me.LBQtyAct.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyAct", "{0:#,##0.##}")})
        Me.LBQtyRj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyRj", "{0:#,##0.##}")})
        Me.LBSisaQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.SisaQty", "{0:#,##0.##}")})

        Me.LBGFPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyPO", "{0:#,##0.##}")})

        SumPO.FormatString = "{0:n2}"
        SumPO.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFPO.Summary = SumPO

        Me.LBGFTrm.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyLPB", "{0:#,##0.##}")})

        SumTrm.FormatString = "{0:n2}"
        SumTrm.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTrm.Summary = SumTrm

        Me.LBGFCancel.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsPO.QtyBatal", "{0:#,##0.##}")})

        SumBtl.FormatString = "{0:n2}"
        SumBtl.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFCancel.Summary = SumBtl

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
        StartPO = 0
    End Sub

    Private Sub GHBBID_BeforePrint(sender As Object, e As EventArgs) Handles GHBBID.BeforePrint
        Start = 0
        No = 0
        StartInv = 0
        StartHitInv = 0
        TotInv = 0
        TotAct = 0
        TotRj = 0
        StartPO = 0

        TotFcPO = 0
        TotFcTrm = 0

    End Sub

    Private Sub GHPOID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHPOID.BeforePrint
        StartPO += 1
    End Sub

    Private Sub GHInvID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHInvID.BeforePrint
        No += 1
        StartInv = 0
        StartHitInv += 1
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Start += 1
        StartInv += 1

        Me.LBNo.Text = No
    End Sub

    Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged
        If StartHitInv = 1 Then
            Me.LBQtyPL.Visible = True
            Me.LBQtyAct.Visible = True
            'Me.LBQtyRj.Visible = True
        Else
            Me.LBQtyPL.Visible = False
            Me.LBQtyAct.Visible = False
            'Me.LBQtyRj.Visible = False
        End If
    End Sub

    Private Sub LBSisaQty_BeforePrint(sender As Object, e As EventArgs) Handles LBSisaQty.BeforePrint
        If Start = 1 Then
            SisaQty = 0
            SisaQty = CDec(Me.LBSisaQty.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty * -1)

        End If

        Me.LBFcSisaQty.Text = SisaQty * -1
    End Sub

    Private Sub LBQtyPO_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBQtyPO.BeforePrint
        If StartPO = 1 Then
            If Start >= 2 Then
                SisaQty += CDec(Me.LBQtyPO.Text)
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
            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty * -1)
        End If

        Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty * -1)

    End Sub

    Private Sub LBQtyBtl_BeforePrint(sender As Object, e As EventArgs) Handles LBQtyBtl.BeforePrint
        If CDec(Me.LBQtyBtl.Text) = 0 Then
            Me.LBQtyBtl.Visible = False
        Else
            Me.LBQtyBtl.Visible = True
        End If

        If Start <> 1 Then
            SisaQty -= CDec(Me.LBQtyBtl.Text)
            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty * -1)
        End If

        Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty * -1)

    End Sub

    Private Sub LBFcSisaQty_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBFcSisaQty.BeforePrint
        Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty * -1)
    End Sub


    Private Sub LBGFInv_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFInv.BeforePrint, LBKet.BeforePrint, LBGFPO.BeforePrint, GroupFooter3.BeforePrint

        Me.LBGFInv.Text = String.Format("{0:#,##0.##;(#,##0.##);0.##}", TotInv)

        If SisaQty * -1 = 0 Then
            If TotFcTrm = TotInv And TotFcTrm = TotFcPO Then
                Me.LBKet.Text = "Qty Pass" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotInv > TotFcTrm And TotFcTrm = TotFcPO Then
                Me.LBKet.Text = "Qty Pass, Kurang Dari Invoice" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm > TotInv And TotFcTrm = TotFcPO Then
                Me.LBKet.Text = "Qty Pass, Lebih Dari Invoice" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm = TotInv And TotFcPO > TotFcTrm Then
                Me.LBKet.Text = "Qty Pass, Lebih Dari PO" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm = TotInv And TotFcTrm > TotFcPO Then
                Me.LBKet.Text = "Qty Pass, Kurang Dari PO" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty
            Else
                Me.LBKet.Text = "Belum Didefiniskan 1" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty
            End If

            Me.LBKet.ForeColor = Color.Black

        ElseIf SisaQty * -1 > 0 Then
            If TotFcTrm > TotFcPO And TotFcTrm > TotInv Then
                Me.LBKet.Text = "Qty Lebih Dari Dari PO & Invoice" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm >= TotFcPO And TotFcTrm <= TotInv Then
                Me.LBKet.Text = "Qty Lebih Dari Dari PO" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm <= TotFcPO And TotFcTrm >= TotInv Then
                Me.LBKet.Text = "Qty Lebih Dari Dari Invoice" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty
            Else
                Me.LBKet.Text = "Belum Didefiniskan 2" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty
            End If

            Me.LBKet.ForeColor = Color.Blue

        Else

            If TotFcTrm < TotFcPO And TotFcTrm < TotInv Then
                Me.LBKet.Text = "Qty Kurang Dari PO & Invoice" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm <= TotFcPO And TotFcTrm >= TotInv Then
                Me.LBKet.Text = "Qty Kurang Dari PO" '& " TotInv : " & " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty

            ElseIf TotFcTrm >= TotFcPO And TotFcTrm <= TotInv Then
                Me.LBKet.Text = "Qty Kurang Dari Invoice" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty
            Else
                Me.LBKet.Text = "Belum Didefiniskan 3" '& " TotInv : " & TotInv & " TotPO : " & TotFcPO & " TotTrm : " & TotFcTrm & " Sisa : " & SisaQty
            End If

            Me.LBKet.ForeColor = Color.Red

        End If

    End Sub

    Private Sub LBGFAct_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFAct.BeforePrint
        Me.LBGFAct.Text = String.Format("{0:#,##0.##;(#,##0.##);0.##}", TotAct)
    End Sub

    Private Sub LBGFRj_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFRj.BeforePrint
        Me.LBGFRj.Text = String.Format("{0:#,##0.##;(#,##0.##);0.##}", TotRj)
    End Sub

    Private Sub LBGFSisa_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSisa.BeforePrint
        Me.LBGFSisa.Text = String.Format("{0:#,##0.##;(#,##0.##);0.##}", SisaQty * -1)

        If Me.LBGFSisa.Text <> "" Then
            If Me.LBGFSisa.Text = 0 Then
                Me.LBGFSisa.ForeColor = Color.Black

            ElseIf Me.LBGFSisa.Text > 0 Then
                Me.LBGFSisa.ForeColor = Color.Blue
            Else
                Me.LBGFSisa.ForeColor = Color.Red

            End If
        End If
    End Sub

    Private Sub LBQtyPO_AfterPrint(sender As Object, e As EventArgs) Handles LBQtyPO.AfterPrint
        TotFcPO += CDec(Me.LBQtyPO.Text)
    End Sub

    Private Sub LBQtyLPB_AfterPrint(sender As Object, e As EventArgs) Handles LBQtyLPB.AfterPrint
        TotFcTrm += CDec(Me.LBQtyLPB.Text)
    End Sub

    Private Sub LBQtyPL_AfterPrint(sender As Object, e As EventArgs) Handles LBQtyPL.AfterPrint
        If StartHitInv = 1 Then
            TotInv += CDec(Me.LBQtyPL.Text)
        End If
    End Sub

    Private Sub LBQtyAct_AfterPrint(sender As Object, e As EventArgs) Handles LBQtyAct.AfterPrint
        If StartHitInv = 1 Then
            TotAct += CDec(Me.LBQtyAct.Text)
        End If
    End Sub

    Private Sub LLBQtyRj_AfterPrint(sender As Object, e As EventArgs) Handles LBQtyRj.AfterPrint
        TotRj += CDec(Me.LBQtyRj.Text)
    End Sub

    Private Sub XrLabel16_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrLabel16.BeforePrint
        Me.XrLabel16.Text = " Inv : " & StartInv & " PO : " & StartPO & " Start : " & Start & " Sisa : " & SisaQty
    End Sub


    Private Sub GroupFooter2_AfterPrint(sender As Object, e As EventArgs) Handles GroupFooter2.AfterPrint
        StartPO = 0
    End Sub
End Class