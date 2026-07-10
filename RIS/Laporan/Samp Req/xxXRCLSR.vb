Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO

Public Class xxXRCLSR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        DsLap = New System.Data.DataSet

        cmsl = New SqlDataAdapter("Select H.ReqID,H.CustID,C.Nama As Cust,H.StyleID + '/' + StlName As Style,Warna,SampType,Lastt,Uk,Qty-BtlOrder-(Select Isnull((Select Sum(Qty)+Sum(QtyRj) From T_TrmSR H1 Inner Join T_TrmSRDtl D1 On H1.TrmSRID=D1.TrmSRID Where ReqID=D.ReqID and ReqIDD=D.ReqIDD and stsApp='True'),0)) As Qty,TglKirim From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID Inner Join M_Cust C On H.CustID=C.CustID Where H.CustID Like '" & Bind.Item("CustID").ToString & "' and H.ChaserID Like '" & Bind.Item("ChaserID").ToString & "' and Qty-BtlOrder-(Select Isnull((Select Sum(Qty)+Sum(QtyRj) From T_TrmSR H1 Inner Join T_TrmSRDtl D1 On H1.TrmSRID=D1.TrmSRID Where ReqID=D.ReqID and ReqIDD=D.ReqIDD and stsApp='True'),0))>0", koneksi)

        cmsl.TableMappings.Add("Table", "LSampReq")
        Try
            DsLap.Tables("LSampReq").Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsLap, "LSampReq")

        Dim dtSampReq As New List(Of DsSampReq)

        For Each dr As DataRow In DsLap.Tables("LSampReq").Rows
            Dim ds As New DsSampReq
            ds.ReqID = dr.Item("ReqID").ToString
            ds.CustID = dr.Item("CustID").ToString
            ds.Cust = dr.Item("Cust").ToString
            ds.Style = dr.Item("Style").ToString
            ds.Warna = dr.Item("Warna").ToString
            ds.SampType = dr.Item("SampType").ToString
            ds.Lastt = dr.Item("Lastt").ToString
            ds.Size = dr.Item("Uk").ToString
            ds.Qty = dr.Item("Qty").ToString
            ds.TglKirim = CDate(dr.Item("TglKirim").ToString)

            Dim command As New SqlCommand("Select Picture From M_Image Where ID = '" & ds.ReqID & "'", koneksi)
            command.CommandTimeout = 90000
            With koneksi
                .Open()
                ds.gambar = command.ExecuteScalar()
                .Close()
            End With

            dtSampReq.Add(ds)
        Next
        Me.DataSource = dtSampReq

        Me.ShowPreview()

    End Sub
End Class