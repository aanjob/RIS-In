Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XROutsSO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim TotQtySO, TotQtyBtl, TotQtySJK, TotQtyRtr, TotSisaQty, SisaQty As Decimal
    Dim Start As Integer = 0
    Dim No As Integer = 0
    Dim Cek As Integer = 0
    Dim cmSP As SqlCommand

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Row,Rn,Rn2,SOID,CustID,Cust,BOMID,Tanggal,BBID,Bahan,QtySO,Case When Rn2=1 Then QtyBatal Else 0 End As QtyBatal,SJKID,KdSOSJK,BOMIDSJK,BBIDSJK,TglSJK,Case When Rn=1 Then QtySJK Else 0 End As QtySJK,RtrID,TglRetur,QtyRetur,SisaQty From (Select ROW_NUMBER() OVER(ORDER BY SOID Asc) AS Row,(DENSE_RANK() OVER (PARTITION BY SJKID,BBID,BOMID ORDER BY SJKID,RtrID,BBIDSJK asc)) As Rn,(DENSE_RANK() OVER (PARTITION BY SOID,BOMID,BBID ORDER BY SOID,BBID,SJKID asc)) As Rn2,*,QtySO-QtyBatal-QtySJK-QtyRetur As SisaQty From (Select O.SOID,O.CustID,C.Nama As Cust,OD.BOMID,O.Tanggal,OD.BBID,B.Nama As Bahan,sum(distinct OD.Qty) as 'QtySO', isnull(sum(distinct OD.BtlOrder),0) as 'QtyBatal',S.SJKID,S.DocID as 'KdSOSJK',OD.BOMID as 'BOMIDSJK',SD.BBID as 'BBIDSJK',S.Tanggal as 'TglSJK',isnull(sum(distinct SD.Qty),0) as 'QtySJK',R.RtrID,R.Tanggal As 'TglRetur', isnull(sum(distinct RD.Qty),0) *-1 as 'QtyRetur' From T_SOBB O inner join T_SOBBDtl OD on O.SOID = OD.SOID left outer join T_SJKBBDtl SD on SD.DocIDD=OD.SOIDD left outer join T_SJKBB S on S.SJKID=SD.SJKID left outer join T_JualBBDtl JD On  SD.SJKIDD=JD.JualIDD left outer join T_RtrPenjBBDtl RD on RD.JualIDD=JD.JualIDD left outer join T_RtrPenjBB R on R.RtrID=RD.RtrID Inner Join M_BB B On B.BBID=OD.BBID Inner Join M_Cust C On O.CustID=C.CustID Where O.Tanggal >='" & MainModule.PilihAwal & "' and O.Tanggal <='" & MainModule.PilihAkhir & "' and O.SOID In (" & Bind.Item("SO").ToString & ") and OD.BBID In (" & Bind.Item("BBID").ToString & ") Group by O.SOID,O.CustID,C.Nama,OD.BOMID,O.Tanggal, OD.BBID,B.Nama,S.SJKID,OD.BOMID,SD.BBID,S.Tanggal,S.DocID,R.RtrID, R.Tanggal) as x) as y order by SOID,BBID asc", koneksi)
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "OutsSO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("SOID", "SOID"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("QtySO", "QtySO"), New System.Data.Common.DataColumnMapping("QtyBatal", "QtyBatal"), New System.Data.Common.DataColumnMapping("SJKID", "SJKID"), New System.Data.Common.DataColumnMapping("KdSOSJK", "KdSOSJK"), New System.Data.Common.DataColumnMapping("BOMIDSJK", "BOMIDSJK"), New System.Data.Common.DataColumnMapping("TglSJK", "TglSJK"), New System.Data.Common.DataColumnMapping("QtySJK", "QtySJK"), New System.Data.Common.DataColumnMapping("RtrID", "RtrID"), New System.Data.Common.DataColumnMapping("TglRetur", "TglRetur"), New System.Data.Common.DataColumnMapping("QtyRetur", "QtyRetur"), New System.Data.Common.DataColumnMapping("SisaQty", "SisaQty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "OutsSO")

        Me.DataMember = "OutsSO"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBUser.Text = MainModule.LoginAktif

        Me.GHCust.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Cust", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHSOID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("SOID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBOMID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BOMID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.Row")})
        Me.LBGHCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.Cust")})
        Me.LBSOID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.SOID")})
        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.BOMID")})
        Me.LBTgl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.Tanggal", "{0: dd/MM/yyyy}")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.Bahan")})
        Me.LBQtySO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.QtySO", "{0:#,##0.##}")})
        Me.LBQtyBtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.QtyBatal", "{0:#,##0.##}")})
        Me.LBLPBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.SJKID")})
        Me.LBReturID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.RtrID")})
        Me.LBTglRetur.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.TglRetur", "{0: dd/MM/yyyy}")})
        Me.LBQtySJK.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.QtySJK", "{0:#,##0.##}")})
        Me.LBQtyRtr.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.QtyRetur", "{0:#,##0.##}")})
        Me.LBSisaQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OutsSO.SisaQty", "{0:#,##0.##}")})

        Me.GFCust.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFQtySO, Me.LBGFQtyBtl, Me.LBGFQtySJK, Me.LBGFQtyBtl, Me.LBGFSisaQty})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XROutsSO_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub GHCust_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHCust.BeforePrint
        No = 0
        TotSisaQty = 0
        TotQtySO = 0
        TotQtyBtl = 0
        TotQtySJK = 0
        TotQtyRtr = 0
    End Sub

    Private Sub GHBBID_BeforePrint(sender As Object, e As EventArgs) Handles GHBBID.BeforePrint
        Start = 0
    End Sub

    Private Sub GHSOID_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GHSOID.BeforePrint
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
            Me.LBSOID.Visible = True
            Me.LBBOMID.Visible = True
            Me.LBTgl.Visible = True
            Me.LBBahan.Visible = True
            Me.LBQtySO.Visible = True
            Me.LBQtyBtl.Visible = True

        Else
            Me.LBSOID.Visible = False
            Me.LBBOMID.Visible = False
            Me.LBTgl.Visible = False
            Me.LBBahan.Visible = False
            Me.LBQtySO.Visible = False
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

    Private Sub LBQtySJK_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBQtySJK.BeforePrint
        If CDec(Me.LBQtySJK.Text) = 0 Then
            Me.LBQtySJK.Visible = False
        Else
            Me.LBQtySJK.Visible = True
        End If

        If Start <> 1 Then
            SisaQty -= CDec(Me.LBQtySJK.Text)

            Me.LBFcSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);""}", SisaQty)
        End If

        TotQtySJK += CDec(Me.LBQtySJK.Text)
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

    Private Sub LBQtySO_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBQtySO.BeforePrint
        If Start = 1 Then
            TotQtySO += CDec(Me.LBQtySO.Text)
        End If
    End Sub

    Private Sub LBGFQtySO_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtySO.BeforePrint
        Me.LBGFQtySO.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtySO)
    End Sub

    Private Sub LBGFQtyBtl_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyBtl.BeforePrint
        Me.LBGFQtyBtl.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyBtl)
    End Sub

    Private Sub LBGFQtyRtr_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtyRtr.BeforePrint
        Me.LBGFQtyRtr.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtyRtr)
    End Sub

    Private Sub LBGFQtySJK_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFQtySJK.BeforePrint
        Me.LBGFQtySJK.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotQtySJK)
    End Sub

    Private Sub LBGFSisaQty_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBGFSisaQty.BeforePrint
        TotSisaQty = TotQtySO - TotQtyBtl - TotQtySJK - TotQtyRtr
        Me.LBGFSisaQty.Text = String.Format("{0:#,##0.##;(#,##0.##);0}", TotSisaQty)
    End Sub
End Class