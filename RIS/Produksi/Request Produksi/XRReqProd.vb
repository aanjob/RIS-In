Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering

Public Class XRReqProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim Kode As String

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,K.Nama as Komponen,RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req, KaliQty,RD.KetMdl From T_ReqPDtl RD Left Outer Join M_Div D On RD.DivID=D.DivID Left Outer Join M_Komp K On RD.KompID=K.KompID Left Outer Join M_BB B On RD.BBID=B.BBID Left Outer Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Bind.Item("Kode").ToString & "'", koneksi)

        Kode = Bind.Item("Kode").ToString

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "RekReq", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "RekReq")

        Me.XrPivotGrid1.DataMember = "RekReq"
        Me.XrPivotGrid1.DataSource = DsLap

        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBBagian.Text = ": " & Bind.Item("Bagian").ToString
        Me.LBUnit.Text = ": " & Bind.Item("Unit").ToString
        Me.LBJenis.Text = ": " & Bind.Item("Jenis").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString

        Me.ShowPreview()

    End Sub

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("ReqID").Value = Convert.ToString(Kode)
    End Sub

End Class