Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering

Public Class XRRekBOMNew
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select B.DivPO,H.BOMID,H.ArtName,H.Warna,S.ShoeLast,TotPsg+TotPsgPol As TotOrder,D.BBID,B.Nama,D.UkBB,D.Sat,Sum(Keb)+Sum(Pol) As Keb, D.Ket From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Model M On H.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where H.BOMID In (" & Bind.Item("BOMID").ToString & ") and D.BBID In (" & Bind.Item("BBID").ToString & ") Group By B.DivPO,H.BOMID,H.ArtName,H.Warna,S.ShoeLast,TotPsg,TotPsgPol,D.BBID, B.Nama,D.UkBB,D.Sat,D.Ket Order By B.Nama", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekBOM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("DivPO", "DivPO")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "RekBOM")

        Me.XrPivotGrid1.DataMember = "RekBOM"
        Me.XrPivotGrid1.DataSource = DsLap

        Me.LBTotOrder.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", Bind.Item("TotOrder").ToString) & " Pasang"
       Me.LBUser.Text = MainModule.LoginAktif

     
        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()

    End Sub

End Class