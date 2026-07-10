Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRSubRBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSubTotKeb As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTotKeb As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
    End Sub

    Private Sub XRAss_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        cmsl = New SqlDataAdapter("Select BOMID,BOM,ShoeLast,TotOrder,BBID,Ket,BBIDGroup,Sum(Keb) as Keb From (Select H.BOMID,H.BOMID + ' ('+ H.ArtName +')' As BOM,S.ShoeLast,TotPsg+TotPsgPol As TotOrder, D.BBID,D.UkBB + ' ' + D.Ket As Ket,D.BBID+D.UkBB + ' ' + D.Ket as BBIDGroup,Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=H.BOMID and BBIDAs=D.BBID and DivID=D.DivID and KompID=D.KompID and stsApp='True'),0)) As Keb From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Model M On H.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where H.BOMID In (" & CStr(DocID.Value) & ") and D.BBID ='" & CStr(BBID.Value) & "' Group By H.BOMID,H.ArtName,S.ShoeLast,TotPsg,TotPsgPol,D.BBID,D.UkBB,D.Ket,DivID,KompID Union All Select H.TambahanID,H.BOMID + ' (' + H.TambahanID + ') ('+ BH.ArtName +')' As BOM,S.ShoeLast,0 As TotOrder,D.BBID,D.UkBB + ' ' + D.Ket As Ket,D.BBID+D.UkBB + ' ' + D.Ket as BBIDGroup,Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where MemoIDRef=H.TambahanID and BBIDAs=D.BBID and DivID=D.DivID and KompID=D.KompID and stsApp='True'),0)) As Keb From T_BOMTam H Inner Join T_BOM BH On H.BOMID=BH.BOMID Inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Model M On BH.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where H.TambahanID In (" & CStr(DocID.Value) & ") and D.BBID ='" & CStr(BBID.Value) & "' Group By H.TambahanID,H.BOMID,BH.ArtName,S.ShoeLast, D.BBID,D.UkBB,D.Ket,DivID,KompID Union All Select H.MemoID,D.BOMID + ' (' + H.MemoID + ') ('+ BH.ArtName +')' As BOM,S.ShoeLast,0 As TotOrder,D.BBIDTj, D.UkBBTj + ' ' + D.Ket As Ket,D.BBIDTj+D.UkBBTj + ' ' + D.Ket as BBIDGroup,Round(Sum(KebTj),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where MemoIDRef=H.MemoID and BBIDAs=D.BBIDTj and stsApp='True'),0)) As Keb From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM BH On D.BOMID=BH.BOMID Inner Join M_BB B On D.BBIDTj=B.BBID Inner Join M_Model M On BH.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where H.MemoID In (" & CStr(DocID.Value) & ") and D.BOMID In (" & CStr(BOMID.Value) & ") and BBIDTj ='" & CStr(BBID.Value) & "' Group By H.MemoID,D.BOMID,BH.ArtName, S.ShoeLast,D.BBIDTj,D.UkBBTj,D.Ket)as x  Group By BOMID,BOM,ShoeLast,TotOrder,BBID,Ket,BBIDGroup Order By BOMID", koneksi)

        'cmsl = New SqlDataAdapter("Select * From (Select H.BOMID,H.BOMID + ' ('+ H.ArtName +')' As BOM,S.ShoeLast,TotPsg+TotPsgPol As TotOrder,D.BBID, D.UkBB + ' ' + D.Ket As Ket,D.BBID+D.UkBB + ' ' + D.Ket as BBIDGroup,Round(Sum(Keb)+Sum(Pol),2) As Keb From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Model M On H.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where H.BOMID In (" & CStr(BOMID.Value) & ") Group By H.BOMID,H.ArtName,S.ShoeLast,TotPsg,TotPsgPol,D.BBID,D.UkBB,D.Ket)as x Where BBID ='" & CStr(BBID.Value) & "' Order By BOMID", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ListBOM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOM", "BOM"), New System.Data.Common.DataColumnMapping("ShoeLast", "ShoeLast"), New System.Data.Common.DataColumnMapping, New System.Data.Common.DataColumnMapping("TotOrder", "TotOrder"), New System.Data.Common.DataColumnMapping, New System.Data.Common.DataColumnMapping("Keb", "Keb")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "ListBOM")

        Me.DataMember = "ListBOM"
        Me.DataSource = DsLap


        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBIDGroup", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBUkKet.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.Ket", "Ukuran/Keterangan : {0}")})
        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.BOM")})
        Me.LBSL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.ShoeLast")})
        Me.LBOrder.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.TotOrder", "{0:#,#0.#}")})
        Me.LBKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.Keb", "{0:n2}")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSubTotKeb})

        Me.LBSubTotKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.Keb", "{0:n2}")})
        SumSubTotKeb.FormatString = "{0:n2}"
        SumSubTotKeb.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBSubTotKeb.Summary = SumSubTotKeb

        Me.LBTotKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ListBOM.Keb", "{0:n2}")})
        SumTotKeb.FormatString = "{0:n2}"
        SumTotKeb.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBTotKeb.Summary = SumTotKeb
    End Sub
End Class