Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FListSampR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsNotif As New System.Data.DataSet

    Private Sub FListSampR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqID,H.Tanggal,TglKirim,C.Nama As Cust,(Select Nama From M_User Where UserID=H.MktID) As Mkt,(Select Nama From M_User Where UserID=H.ChaserID) As Chaser,StlName,StyleID From T_SampReq H Inner Join M_Cust C On H.CustID=C.CustID where stsApp='False' and stsBatal='False' and (ChaserID=" & MainModule.UserAktif & " or MktID=" & MainModule.UserAktif & ")", koneksi)


        cmsl.TableMappings.Add("Table", "ListSampR")
        Try
            DsNotif.Tables("ListSampR").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif, "ListSampR")

        Me.GridControl1.DataSource = DsNotif
        Me.GridControl1.DataMember = "ListSampR"
    End Sub
End Class