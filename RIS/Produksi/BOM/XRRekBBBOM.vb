Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekBBBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select H.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotOrder,D.BBID,B.Nama,D.Sat,Round(Sum(Keb)+Sum(Pol),2) As Keb From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Where H.BOMID In (" & Bind.Item("BOMID").ToString & ") Group By H.BOMID,ArtName,Warna,TotPsg,TotPsgPol,D.BBID,B.Nama,D.Sat Order By B.Nama", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekBOM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("TotOrder", "TotOrder"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Keb", "Keb")})})

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