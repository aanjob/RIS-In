Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Public Class FBOM_sa
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim BOMID As String
    Dim CekAll As Boolean
    Dim TotOrder As Decimal = 0

    Private Sub FBOM_sa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,*,(Select Top 1 Tanggal From T_BSTB H Inner Join T_BSTBDtl D On H.BSTBID=D.BSTBID where D.BOMID=x.BOMID order By Tanggal desc) As TglBSTB,(Select Isnull((Select Sum(Psg) From T_BSTBDtl Where BOMID=x.BOMID),0)) As TotProd From (Select BOMID, B.PeriodID,B.CodeID,B.Tanggal,B.TglAwal,B.TglAkhir,B.Jenis,Unit,SPK,S.SpecID,B.MdlID,B.POID,S.Brand,S.StyleID, B.ArtName,B.Warna, C.Nama As Cust,B.HCBP,B.Ket,B.KetProd,B.KetLain2,B.TotPsg,B.TotPsgPol,B.SisaPsg,B.stsBatal,B.stsBtlProd,B.stsLunas,B.Grup,'Job Order' As Gol, B.InsDate,B.InsBy,B.UpdDate,B.UpdBy From T_BOM B Left Outer Join T_POBJJO PJ On B.POID=PJ.POID Left Outer Join M_Cust C on B.CustID=C.CustID Left Outer Join M_Model M On B.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where Gol='Job Order' Union All Select Distinct BOMID,B.PeriodID,B.CodeID, B.Tanggal,B.TglAwal,B.TglAkhir,B.Jenis,Unit,SPK,S.SpecID,B.MdlID,B.POID,S.Brand,S.StyleID,B.ArtName,B.Warna, C.Nama As Customer,B.HCBP,B.Ket, B.KetProd,B.KetLain2,B.TotPsg,B.TotPsgPol,B.SisaPsg,B.stsBatal,B.stsBtlProd, B.stsLunas,B.Grup, B.Gol,B.InsDate,B.InsBy,B.UpdDate,B.UpdBy From T_BOM B Left Outer Join T_POBJLk H  On B.POID=H.POID Left Outer Join T_POBJLkDtl PJ On H.POID=PJ.POID Left Outer Join M_Model M On B.MdlID=M.MdlID Left Outer Join M_Spec S On S.SpecID=M.SpecID Left Outer Join M_Cust C on B.CustID=C.CustID Where B.Gol In ('Own','Character','Air Max') Union All Select BOMID, B.PeriodID,B.CodeID,B.Tanggal, B.TglAwal,B.TglAkhir,B.Jenis,Unit,SPK,S.SpecID, B.MdlID,B.POID,S.Brand,S.StyleID,B.ArtName,B.Warna,C.Nama As Customer,B.HCBP,B.Ket,B.KetProd, B.KetLain2,B.TotPsg,B.TotPsgPol,B.SisaPsg,B.stsBatal, B.stsBtlProd,B.stsLunas,B.Grup,B.Gol,B.InsDate,B.InsBy,B.UpdDate,B.UpdBy From T_BOM B Left Outer Join M_Model M On B.MdlID=M.MdlID Left Outer Join M_Spec S On S.SpecID=M.SpecID Left Outer Join M_Cust C on B.CustID=C.CustID Where Gol='') as x Order By Tanggal,BOMID Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMSa")
        Try
            DsMaster.Tables("T_BOMSa").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMSa")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BOMSa"
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
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

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Me.GridView1.ActiveFilter.Clear()
        TotOrder = 0

        Dim x, i As Integer

        x = 0
        i = 0
        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
                TotOrder += Me.GridView1.GetRowCellValue(i, "Tot")

                If x = 1 Then
                    BOMID = "'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                Else
                    BOMID &= ",'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(BOMID, "BOMID")
        bind.Add(TotOrder, "TotOrder")

        Dim XR As New XRRekBBBOM
        XR.InitializeData(bind)
    End Sub

    Private Sub BCekOutsBOM_Click(sender As Object, e As EventArgs) Handles BCekOutsBOM.Click
        Try
            Dim frm As New FOutsBOM(Me.GridView1.GetFocusedDataRow.Item("BOMID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class