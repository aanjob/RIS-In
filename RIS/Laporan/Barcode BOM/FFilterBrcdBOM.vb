Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFilterBrcdBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2 As Boolean
    Dim BOMID As String = ""
    Dim DsLapF As New System.Data.DataSet

    Private Sub FFilterBrcdBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter

        If Me.CEAllBOM.EditValue = True Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID", koneksi)
            cmsl.TableMappings.Add("Table", "T_BOML")
            cmsl.Fill(DsLapF, "T_BOML")
            DsLapF.Tables("T_BOML").Clear()
            cmsl.Fill(DsLapF, "T_BOML")
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID where stsLunas='False'", koneksi)
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

        Dim x, i As Integer
        x = 0
        i = 0


        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
                If x = 1 Then
                    BOMID = "'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                Else
                    BOMID &= ",'" & Me.GridView1.GetRowCellValue(i, "BOMID") & "'"
                End If
            End If
        Next

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BP.ArtCode,ArtName,W.Nama As Warna,Ass As Uk,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where BOMID In (" & BOMID & ")", koneksi)
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

        DsBrcd = New DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select '' As Barcode,'' As Uk,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where BOMID='--'", koneksi)

        cmsl.TableMappings.Add("Table", "Barcode")
        cmsl.Fill(DsBrcd, "Barcode")
        DsBrcd.Clear()

        For i = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(i, "Cek") = True Then
                DsBrcd.Tables("Barcode").Rows.Add(Me.GridView2.GetRowCellValue(i, "BOMID") & "\" & Me.GridView2.GetRowCellValue(i, "ArtCode"), Me.GridView2.GetRowCellValue(i, "Uk"), Me.GridView2.GetRowCellValue(i, "Tot"))
            End If
        Next

        Dim XR As New XRBrcdBOM
        XR.InitializeData()

    End Sub

End Class