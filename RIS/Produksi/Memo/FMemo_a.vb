Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FMemo_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Dok As String

    Public Sub New(Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Dim cmsl As SqlDataAdapter
        DsAddDt = New System.Data.DataSet
        'cmsl = New SqlDataAdapter("Select * From (Select convert(bit,'FALSE') as Cek,H.BOMID,H.MdlID,C.Nama As Cust,ArtName,Warna,D.DivID, Dv.Nama As Div,D.KompID,K.Nama As Komp,D.BBID,B.Nama as Bahan,D.UkBB,D.Sat,Round(Sum(Keb)+Sum(Pol),2) As Keb From T_BOM H inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Where stsLunas='False' and stsBatal='False' and stsBtlProd='False' and Tanggal>=Dateadd(MM,-12,GetDate()) Group By H.BOMID,H.MdlID, C.Nama,ArtName,Warna,D.DivID,Dv.Nama,D.KompID,K.Nama,D.BBID,B.Nama, D.UkBB,D.Sat Union All Select convert(bit,'FALSE') as Cek, H.TambahanID,BH.MdlID,C.Nama As Cust,ArtName,Warna,D.DivID,Dv.Nama As Div,D.KompID,K.Nama As Komp,D.BBID,B.Nama as Bahan,D.UkBB,D.Sat, Round(Sum(Keb)+Sum(Pol),2) As Keb From T_BOMTam H inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join T_BOM BH On H.BOMID=BH.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On BH.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Where stsLunas='False' and stsBatal='False' and stsBtlProd='False' and BH.Tanggal>=Dateadd(MM,-12,GetDate()) Group By H.TambahanID, BH.MdlID,C.Nama,ArtName,Warna,D.DivID,Dv.Nama,D.KompID,K.Nama,D.BBID,B.Nama, D.UkBB,D.Sat Union All Select convert(bit,'FALSE') as Cek, H.MemoID,BH.MdlID,C.Nama As Cust,ArtName,Warna,D.DivID,Dv.Nama As Div,D.KompID,K.Nama As Komp,D.BBIDTj,B.Nama as Bahan,D.UkBBTj,D.SatTj, Round(Sum(KebTj),2) As Keb From T_Memo H inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM BH On D.BOMID=BH.BOMID Inner Join M_BB B On D.BBIDTj=B.BBID Inner Join M_Cust C On BH.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Where stsLunas='False' and stsBatal='False' and stsBtlProd='False' and BH.Tanggal>=Dateadd(MM,-12,GetDate()) Group By H.MemoID, BH.MdlID,C.Nama,ArtName,Warna,D.DivID,Dv.Nama,D.KompID,K.Nama,D.BBIDTj,B.Nama, D.UkBBTj,D.SatTj) as x Order By BOMID,Div,Komp,Bahan", koneksi)

        cmsl = New SqlDataAdapter("Select * From (Select convert(bit,'FALSE') as Cek,'' as MemoIDRef,H.BOMID,H.MdlID,C.Nama As Cust,ArtName,Warna,D.DivID, Dv.Nama As Div,D.KompID,K.Nama As Komp,D.BBID,B.Nama as Bahan,D.UkBB,D.Sat,Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=H.BOMID and DivID=D.DivID and KompID=D.KompID and BBIDAs=D.BBID and MH.MemoID<>'" & Kode & "' and stsApp='True'),0)) As Keb From T_BOM H inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Where stsLunas='False' and stsBatal='False' and stsBtlProd='False' and Tanggal>=Dateadd(MM,-12,GetDate()) Group By H.BOMID,H.MdlID, C.Nama,ArtName,Warna,D.DivID,Dv.Nama,D.KompID,K.Nama,D.BBID,B.Nama, D.UkBB,D.Sat Union All Select convert(bit,'FALSE') as Cek,H.TambahanID as MemoIDRef, H.BOMID,BH.MdlID,C.Nama As Cust,ArtName,Warna,D.DivID,Dv.Nama As Div,D.KompID,K.Nama As Komp,D.BBID,B.Nama as Bahan,D.UkBB,D.Sat, Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=H.TambahanID and DivID=D.DivID and KompID=D.KompID and BBIDAs=D.BBID and MH.MemoID<>'" & Kode & "' and stsApp='True'),0)) As Keb From T_BOMTam H inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join T_BOM BH On H.BOMID=BH.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On BH.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Where stsLunas='False' and stsBatal='False' and stsBtlProd='False' and BH.Tanggal>=Dateadd(MM,-12,GetDate()) Group By H.TambahanID,H.BOMID, BH.MdlID,C.Nama,ArtName,Warna,D.DivID,Dv.Nama,D.KompID,K.Nama,D.BBID,B.Nama, D.UkBB,D.Sat Union All Select convert(bit,'FALSE') as Cek,H.MemoID as MemoIDRef,BH.BOMID,BH.MdlID,C.Nama As Cust,ArtName,Warna,D.DivID,Dv.Nama As Div,D.KompID,K.Nama As Komp,D.BBIDTj,B.Nama as Bahan,D.UkBBTj,D.SatTj, Round(Sum(KebTj),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where MemoIDRef=H.MemoID and DivID=D.DivID and KompID=D.KompID and BBIDAs=D.BBIDTj and MH.MemoID<>'" & Kode & "' and stsApp='True'),0)) As Keb From T_Memo H inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM BH On D.BOMID=BH.BOMID Inner Join M_BB B On D.BBIDTj=B.BBID Inner Join M_Cust C On BH.CustID=C.CustID Inner Join M_Div Dv On D.DivID=Dv.DivID Inner Join M_Komp K On D.KompID=K.KompID Where stsLunas='False' and stsBatal='False' and stsBtlProd='False' and BH.Tanggal>=Dateadd(MM,-12,GetDate()) Group By H.MemoID,BH.BOMID,BH.MdlID,C.Nama,ArtName,Warna,D.DivID,Dv.Nama,D.KompID,K.Nama,D.BBIDTj,B.Nama, D.UkBBTj,D.SatTj) as x Order By BOMID,Div,Komp,Bahan", koneksi)

        cmsl.TableMappings.Add("Table", "MdlMemo")
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsAddDt, "MdlMemo")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "MdlMemo"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilterString = "[Cek] = True"

        Dim x As Integer
        x = 0

        dataTrans = New Collection
        dataTrans.Clear()

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "MemoIDRef"), "MemoIDRef" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BOMID"), "BOMID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "MdlID"), "MdlID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DivID"), "DivID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Div"), "Div" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "KompID"), "KompID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Komp"), "Komp" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BBID"), "BBID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Bahan"), "Bahan" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "UkBB"), "UkBB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Sat"), "Sat" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Keb"), "Keb" & x)
                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        Timer1.Enabled = True
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub
End Class