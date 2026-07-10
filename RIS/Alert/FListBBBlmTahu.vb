Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FListBBBlmTahu
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsNotif As New System.Data.DataSet

    Private Sub FListBBBlmTahu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select H.SpecID,Tanggal,C.Nama As Cust,StyleID,ArtName,Dv.Nama As Divisi,K.Nama As Komponen,B.Nama As Bahan From M_Spec H Inner Join M_SpecDtl D On H.SpecID=D.SpecID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Inner Join M_BB B on D.BBID=B.BBID Where Year(H.Tanggal)>=2020 and D.BBID='00000001'", koneksi)

        cmsl.TableMappings.Add("Table", "BBBlmTahu")
        Try
            DsNotif.Tables("BBBlmTahu").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif, "BBBlmTahu")

        Me.GridControl1.DataSource = DsNotif
        Me.GridControl1.DataMember = "BBBlmTahu"
    End Sub
End Class