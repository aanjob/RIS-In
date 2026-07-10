Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPOBB_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Dok As String

    Public Sub New(ByVal Gol As String, ByVal Dokumen As String, Tipe As String, ByVal POID As String, SuppID As String, CustID As String, Jenis As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        Dim cmsl As SqlDataAdapter
        DsAddDt = New System.Data.DataSet

        Dok = Dokumen

        If Dok = "BOM" Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,* From (Select D.BOMID As DocID,D.BOMID,0 as DocIDD, H.ArtName As Style, D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & POID & "'),0))-(Select Isnull((Select Round(Sum(KebAs),0) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.BOMID and BBIDAs=D.BBID and stsApp='True'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID and HargaBeli<>0 Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and H.stsLunas='False' and H.stsBatal='False' and H.stsBtlProd='False' Group By D.BBID,B.Nama,D.Sat,D.BOMID, H.ArtName ,SuppID,D.stsJasa Union All Select D.TambahanID As DocID,H.BOMID,0 as DocIDD, BH.ArtName As Style,D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.TambahanID and BBID=D.BBID and POID<>'" & POID & "'),0))-(Select Isnull((Select Round(Sum(KebAs),0) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.TambahanID and BBIDAs=D.BBID and stsApp='True'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID and HargaBeli<>0 Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMTam H Inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Inner Join T_BOM BH On H.BOMID=BH.BOMID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and BH.stsLunas='False' and BH.stsBatal='False' and BH.stsBtlProd='False' Group By D.BBID,B.Nama,D.Sat, D.TambahanID,H.BOMID,BH.ArtName ,SuppID,D.stsJasa Union All	Select DocID,(SELECT substring ((Select Distinct ', ' + BOMID From (Select Distinct MemoID,B1.BOMID From T_MemoDtl D1 Inner Join T_BOM B1 On D1.BOMID=B1.BOMID) as x where MemoID=DocID FOR XML PATH('')), 3, 500)) As BOMID,DocIDD,(SELECT substring ((Select Distinct ', ' + ArtName From (Select Distinct MemoID,B1.ArtName From T_MemoDtl D1 Inner Join T_BOM B1 On D1.BOMID=B1.BOMID) as x where MemoID=DocID FOR XML PATH('')), 3, 500)) As Style,BBIDTj,Bahan,SatTj,Qty,Harga,stsJasa From(Select D.MemoID As DocID,0 as DocIDD,D.BBIDTj,B.Nama As Bahan,D.SatTj,Round(Sum(KebTj),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.MemoID and BBID=D.BBIDTj and POID<>'" & POID & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBIDTj and SuppID=S.SuppID and HargaBeli<>0 Order By Tanggal desc),0)) As Harga,'False' As stsJasa From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join M_BB B On D.BBIDTj=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Inner Join T_BOM BH On D.BOMID=BH.BOMID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and BH.stsLunas='False' and BH.stsBatal='False' and BH.stsBtlProd='False' Group By D.BBIDTj,B.Nama,D.SatTj,D.MemoID,SuppID) as y) as x Order By Bahan", koneksi)

            'cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,* From (Select D.BOMID As DocID,0 as DocIDD, H.ArtName As Style, D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & POID & "'),0))-(Select Isnull((Select Round(Sum(KebAs),0) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.BOMID and BBIDAs=D.BBID and stsApp='True'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and H.stsLunas='False' and H.stsBatal='False' and H.stsBtlProd='False' Group By D.BBID,B.Nama,D.Sat,D.BOMID, H.ArtName ,SuppID,D.stsJasa Union All Select D.TambahanID As DocID,0 as DocIDD, BH.ArtName As Style,D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.TambahanID and BBID=D.BBID and POID<>'" & POID & "'),0))-(Select Isnull((Select Round(Sum(KebAs),0) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.TambahanID and BBIDAs=D.BBID and stsApp='True'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOMTam H Inner Join T_BOMTamDtl D On H.TambahanID=D.TambahanID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Inner Join T_BOM BH On H.BOMID=BH.BOMID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and BH.stsLunas='False' and BH.stsBatal='False' and BH.stsBtlProd='False' Group By D.BBID,B.Nama,D.Sat, D.TambahanID,BH.ArtName ,SuppID,D.stsJasa Union All Select D.MemoID As DocID,0 as DocIDD,BH.ArtName As Style,D.BBIDTj,B.Nama As Bahan,D.SatTj,Round(Sum(KebTj),0)-(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.MemoID and BBID=D.BBIDTj and POID<>'" & POID & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBIDTj and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,'False' As stsJasa From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join M_BB B On D.BBIDTj=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Inner Join T_BOM BH On D.BOMID=BH.BOMID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and BH.stsLunas='False' and BH.stsBatal='False' and BH.stsBtlProd='False' Group By D.BBIDTj,B.Nama,D.SatTj,D.MemoID,BH.ArtName, SuppID) as x Order By Bahan", koneksi)

            'cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,D.BOMID As DocID,0 as DocIDD, H.ArtName As Style,D.BBID,B.Nama As Bahan,D.Sat,Round(Sum(Keb)+Sum(Pol),0) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.BOMID and BBID=D.BBID and POID<>'" & POID & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID Order By Tanggal desc),0)) As Harga,D.stsJasa From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and H.stsLunas='False' and H.stsBatal='False' and H.stsBtlProd='False' Group By D.BBID,B.Nama,D.Sat,D.BOMID, H.ArtName ,SuppID,D.stsJasa Order By Bahan", koneksi)

        ElseIf Dok = "Request" Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,D.ReqPID As DocID,0 as DocIDD,D.BBID,B.Nama As Bahan,D.Sat,Sum(Req) -(Select Isnull((Select Sum(Qty)-Sum(BtlOrder) From T_POBBDtl Where BOMID=D.ReqPID and BBID=D.BBID and POID<>'" & POID & "'),0)) As Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=S.SuppID and HargaBeli<>0 Order By Tanggal desc),0)) As Harga,D.stsJasa From T_ReqP H Inner Join T_ReqPDtl D On H.ReqPID=D.ReqPID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID='" & SuppID & "' And S.Aktif='True' and B.Aktif='True' and H.stsApp='True' and H.stsLunas='False' Group By D.BBID,B.Nama,D.Sat,D.ReqPID,SuppID,D.stsJasa Order By Bahan", koneksi)

        ElseIf Dok = "Purchase Request" Then

            If Tipe = "Tooling" Then
                cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,* From (Select H.PRTID As DocID,D.PRTIDD As DocIDD,StyleID,Style,D.BBID, Case When D.Ket='' Then B.Nama Else B.Nama +' ('+ D.Ket +')' End As Bahan,Qty-BtlOrder-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID=H.PRTID and DocIDD=D.PRTIDD),0)) as Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID=H.SuppID and HargaBeli<>0 Order By Tanggal desc),0)) As Harga,'False' as stsJasa,D.Sat From T_PRTool H Inner Join T_PRToolDtl D On H.PRTID=D.PRTID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_SuppBB SB On SB.BBID=D.BBID Where H.SuppID='" & SuppID & "' and SB.SuppID='" & SuppID & "' and H.CustID='" & CustID & "') As x Where Qty>0", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,* From (Select H.PRSMID As DocID,PRSMIDD As DocIDD,D.BBID,Case When D.Ket='' Then B.Nama Else B.Nama +' ('+ D.Ket +')' End As Bahan,Qty-BtlOrder-(Select Isnull((Select Sum(Qty-BtlOrder) From T_POBBDtl Where BOMID=H.PRSMID and DocIDD=D.PRSMIDD),0)) as Qty,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=D.BBID and SuppID='" & SuppID & "' and HargaBeli<>0 Order By Tanggal desc),0)) As Harga,'False' as stsJasa,D.Sat From T_PRSpM H Inner Join T_PRSpMDtl D On H.PRSMID=D.PRSMID Inner Join M_BB B On D.BBID=B.BBID Where Tipe='" & Tipe & "' and Jenis='" & Jenis & "') as x Where Qty>0", koneksi)
            End If

        ElseIf Dok = "" Then
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,'' As DocID,0 as DocIDD,B.BBID,B.Nama As Bahan,0 As Qty,B.Sat,(Select Isnull((Select Top 1 HargaBeli From M_BBHarga Where BBID=B.BBID and SuppID=S.SuppID and HargaBeli<>0 Order By Tanggal desc,HargaIDD desc),0)) As Harga,stsJasa From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Inner Join M_SuppBB S On B.BBID=S.BBID Where S.SuppID Like '" & SuppID & "' and J.Gol Like'" & Gol & "' and B.Aktif='True' Order By B.Nama", koneksi)

        End If

        cmsl.TableMappings.Add("Table", "DokPO")
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsAddDt, "DokPO")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "DokPO"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer = 0

        dataTrans = New Collection
        dataTrans.Clear()

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DocID"), "DocID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BBID"), "BBID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Bahan"), "Bahan" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Sat"), "Sat" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Harga"), "Harga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "stsJasa"), "stsJasa" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DocIDD"), "DocIDD" & x)

                If Dok = "Request" Then
                    dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BOMID"), "BOMID" & x)
                End If

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