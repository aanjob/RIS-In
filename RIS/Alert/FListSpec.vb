Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FListSpec
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsNotif As New System.Data.DataSet

    Private Sub FListSpec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SH.SpecID,StyleID,Brand,ArtName,Warna,SD.DivID,D.Nama as Divisi, SD.KompID, K.Nama As Komponen, SD.BBID, B.Nama As BB, B.Uk,B.Wrn,SD.Ket,SD.Sat From M_Spec SH Inner Join M_SpecDtl SD On SH.SpecID=SD.SpecID Inner Join M_Div D On SD.DivID=D.DivID Inner Join M_Komp K On SD.KompID=K.KompID Inner Join M_BB B On SD.BBID=B.BBID Where SpecIDD Not In (Select SpecIDD From M_Model h inner Join M_ModelDtl d On h.MdlID=d.MdlID where H.SpecID=SH.SpecID)and SH.SpecID in (Select SpecID From M_Model) and Year(SH.Tanggal)>=2019 Order By SH.SpecID,SpecIDD", koneksi)

        cmsl.TableMappings.Add("Table", "M_SpecDtl")
        Try
            DsNotif.Tables("M_SpecDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif, "M_SpecDtl")

        DsNotif.Tables("M_SpecDtl").PrimaryKey = New DataColumn() {DsNotif.Tables("M_SpecDtl").Columns("BOMID"), DsNotif.Tables("M_SpecDtl").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsNotif
        Me.GridControl1.DataMember = "M_SpecDtl"
    End Sub
End Class