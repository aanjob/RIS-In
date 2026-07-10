Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FListPO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsNotif As New System.Data.DataSet

    Private Sub FListPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi.Close()
        DsNotif.Clear()

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POID,Cust,TglKirim,ArtName,Warna,Sum(Qty) As Qty, Sum(Sisa) as Sisa From(Select H.POID,C.Nama As Cust,TglKirim,D.ArtCode,ArtName,W.Nama as Warna,Sum(Qty*B.Isi) As Qty,Sum(Qty*B.Isi)-(Select Isnull(Sum(P.Tot),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID=H.POID and (ArtCode=D.ArtCode Or ArtCode In (Select replace(ArtCode,Ass,Ad.Uk) From M_Brg B Inner Join M_BrgAssDtl AD  On B.AssID=Ad.AssID where ArtCode=D.ArtCode)) and B.stsBatal='False') As Sisa From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_Cust C On H.CustID=C.CustID  Where H.POID In (Select Distinct POID From T_POBJJODtl where Psg-BtlOrder>(Select Isnull((Select Sum(Tot) From T_BOMPO where ArtCodeInd=T_POBJJODtl.ArtCode and POID=T_POBJJODtl.POID),0))and stsProd='False') and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Group By H.POID,C.Nama,TglKirim,D.ArtCode,ArtName,W.Nama) as x Group By POID,Cust,TglKirim,ArtName,Warna Order By POID", koneksi)

        cmsl.TableMappings.Add("Table", "ListPO")
        Try
            DsNotif.Tables("ListPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif, "ListPO")

        DsNotif.Tables("ListPO").PrimaryKey = New DataColumn() {DsNotif.Tables("ListPO").Columns("BOMID"), DsNotif.Tables("ListPO").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsNotif
        Me.GridControl1.DataMember = "ListPO"
    End Sub
End Class