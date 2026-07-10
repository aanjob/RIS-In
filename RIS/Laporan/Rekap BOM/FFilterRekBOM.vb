Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFilterRekBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2 As Boolean
    Dim DocID As String = ""
    Dim BBID As String = ""
    Dim BOMID As String = ""
    Dim TotOrder As Decimal = 0
    Dim DsLapF As New System.Data.DataSet

    Private Sub FFilterRekBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter

        If Me.CEAllBOM.EditValue = True Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID As DocID, BOMID,C.Nama As Cust,Tanggal,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Union All Select convert(bit,'FALSE') as Cek,TambahanID as DocID,H.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_BOMTam H Inner Join T_BOM B On H.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID Union All Select Distinct convert(bit,'FALSE') as Cek,H.MemoID as DocID,D.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM B On D.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID Where H.stsApp='True' Union All Select convert(bit,'FALSE') as Cek,a.ReqPID As DocID,b.BOMID,C.Nama As Cust,a.Tanggal,d.ArtName,d.Warna,b.qty As Tot From T_ReqP a Inner Join T_ReqPDtl b On a.ReqPID=b.ReqPID Left Outer Join M_Cust C On a.CustID=C.CustID Inner Join T_BOM D On D.BOMID=B.BOMID where a.stsApp='True'", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOML")
            cmsl.Fill(DsLapF, "T_BOML")
            DsLapF.Tables("T_BOML").Clear()
            cmsl.Fill(DsLapF, "T_BOML")
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID as DocID,BOMID,C.Nama As Cust,Tanggal,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID where stsLunas='False' Union All Select convert(bit,'FALSE') as Cek,TambahanID As DocID,H.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_BOMTam H Inner Join T_BOM B On H.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID where stsLunas='False' Union All Select Distinct convert(bit,'FALSE') as Cek,H.MemoID As DocID,D.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM B On D.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID where H.stsApp='True' and stsLunas='False' Union All Select convert(bit,'FALSE') as Cek,a.ReqPID As DocID,b.BOMID,C.Nama As Cust,a.Tanggal,d.ArtName,d.Warna,b.qty As Tot From T_ReqP a Inner Join T_ReqPDtl b On a.ReqPID=b.ReqPID Left Join M_Cust C On a.CustID=C.CustID Inner Join T_BOM D On D.BOMID=B.BOMID where a.stsApp='True' and A.stsLunas='False'", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOML")
            cmsl.Fill(DsLapF, "T_BOML")
            DsLapF.Tables("T_BOML").Clear()
            cmsl.Fill(DsLapF, "T_BOML")
        End If

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "T_BOML"
    End Sub

    Private Sub CEAllBOM_EditValueChanged(sender As Object, e As EventArgs) Handles CEAllBOM.EditValueChanged
        Dim cmsl As SqlDataAdapter

        If Me.CEAllBOM.EditValue = True Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID As DocID, BOMID,C.Nama As Cust,Tanggal,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Union All Select convert(bit,'FALSE') as Cek,TambahanID as DocID,H.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_BOMTam H Inner Join T_BOM B On H.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID Union All Select Distinct convert(bit,'FALSE') as Cek,H.MemoID as DocID,D.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM B On D.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID Where H.stsApp='True' Union All Select convert(bit,'FALSE') as Cek,a.ReqPID As DocID,b.BOMID,C.Nama As Cust,a.Tanggal,d.ArtName,d.Warna,b.qty As Tot From T_ReqP a Inner Join T_ReqPDtl b On a.ReqPID=b.ReqPID Left Outer Join M_Cust C On a.CustID=C.CustID Inner Join T_BOM D On D.BOMID=B.BOMID where a.stsApp='True'", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOML")
            cmsl.Fill(DsLapF, "T_BOML")
            DsLapF.Tables("T_BOML").Clear()
            cmsl.Fill(DsLapF, "T_BOML")
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID as DocID,BOMID,C.Nama As Cust,Tanggal,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID where stsLunas='False' Union All Select convert(bit,'FALSE') as Cek,TambahanID As DocID,H.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_BOMTam H Inner Join T_BOM B On H.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID where stsLunas='False' Union All Select Distinct convert(bit,'FALSE') as Cek,H.MemoID As DocID,D.BOMID,C.Nama As Cust,H.Tanggal,ArtName,Warna,0 As Tot From T_Memo H Inner Join T_MemoDtl D On H.MemoID=D.MemoID Inner Join T_BOM B On D.BOMID=B.BOMID Left Outer Join M_Cust C On B.CustID=C.CustID where H.stsApp='True' and stsLunas='False' Union All Select convert(bit,'FALSE') as Cek,a.ReqPID As DocID,d.BOMID,C.Nama As Cust,a.Tanggal,d.ArtName,d.Warna,b.qty As Tot From T_ReqP a Inner Join T_ReqPDtl b On a.ReqPID=b.ReqPID Left Outer Join M_Cust C On a.CustID=C.CustID Inner Join T_BOM D On D.BOMID=B.BOMID where a.stsApp='True' and A.stsLunas='False'", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOML")
            cmsl.Fill(DsLapF, "T_BOML")
            DsLapF.Tables("T_BOML").Clear()
            cmsl.Fill(DsLapF, "T_BOML")
        End If

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "T_BOML"
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

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAll2 Then
            CekAll2 = False
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll2 = True
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BKlik_Click(sender As Object, e As EventArgs) Handles BKlik.Click
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
                    DocID = "'" & Me.GridView1.GetRowCellValue(i, "DocID") & "'"
                    BOMID = "'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"

                Else
                    DocID &= ",'" & Me.GridView1.GetRowCellValue(i, "DocID") & "'"
                    BOMID &= ",'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"

                End If
            End If
        Next

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,Jenis,BBID,Nama,Sat From (Select Distinct J.Nama As Jenis,D.BBID,B.Nama,D.Sat From T_BOMDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where BOMID In (" & DocID & ") Union All Select Distinct J.Nama As Jenis,D.BBID,B.Nama,D.Sat From T_BOMTamDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where TambahanID In (" & DocID & ")  Union All Select Distinct J.Nama As Jenis,D.BBIDTj,B.Nama,D.SatTj From T_MemoDtl D Inner Join M_BB B On D.BBIDTj=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where MemoID In (" & DocID & ") Union All Select Distinct J.Nama As Jenis,D.BBID,B.Nama,D.Sat From T_ReqPDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where ReqPID In (" & DocID & "))as x Order By Jenis,Nama ", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOMDtlL")
        cmsl.SelectCommand.CommandTimeout = 90000
        Try
            DsLapF.Tables("T_BOMDtlL").Clear()
        Catch ex As Exception

            End Try
        cmsl.Fill(DsLapF, "T_BOMDtlL")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "T_BOMDtlL"
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Me.GridView2.ActiveFilter.Clear()

        Dim x, i As Integer

        x = 0
        i = 0
        For i = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(i, "Cek") = True Then
                x += 1
                If x = 1 Then
                    BBID = "'" & Me.GridView2.GetRowCellValue(i, "BBID") & "'"
                    'BOMID = "'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                Else
                    BBID &= ",'" & Me.GridView2.GetRowCellValue(i, "BBID") & "'"
                    'BOMID &= ",'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(BBID, "BBID")
        bind.Add(DocID, "DocID")
        bind.Add(BOMID, "BOMID")
        'bind.Add(Me.CBOBOM.EditValue, "JenisBOM")
        bind.Add(TotOrder, "TotOrder")

        Dim XR As New XRRekBOMv3
        XR.InitializeData(bind)

        'Dim XR2 As New XRRekBOMv3v1
        'XR2.InitializeData(bind)

        'Dim XR2 As New XRRekBBBOM
        'XR2.InitializeData(bind)

        'Dim frm As New FRekBOM(BOMID, BBID)
        'frm.MdiParent = FUtama
        'frm.Show()

    End Sub

    'Private Sub BPreviewBOM_Click(sender As Object, e As EventArgs) Handles BPreviewBOM.Click
    '    Try
    '        Dim frm As New FViewBOM(Me.GridView1.GetFocusedDataRow.Item("DocID"))
    '        frm.ShowDialog()
    '        frm.Close()
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class