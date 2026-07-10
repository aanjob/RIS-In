Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FSpec_sa
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim SpecID As String

    Private Sub FSpec_sa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SpecID,PeriodID,CodeID,Tanggal,H.CustID,C.Nama As Customer,StyleID,Brand, ArtName,Warna,RangeSize,SampleSize,H.Ket From M_Spec H Inner Join M_Cust C On H.CustID=C.CustID", koneksi)

        cmsl.TableMappings.Add("Table", "M_SpecSa")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_SpecSa")
        DsMaster.Tables("M_SpecSa").Clear()
        cmsl.Fill(DsMaster, "M_SpecSa")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_SpecSa"
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

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Me.GridView1.ActiveFilter.Clear()

        Dim x, i As Integer

        x = 0
        i = 0
        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
                If x = 1 Then
                    SpecID = "'" & Me.GridView1.GetRowCellValue(i, "SpecID") & "'"
                Else
                    SpecID &= ",'" & Me.GridView1.GetRowCellValue(i, "SpecID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(SpecID, "SpecID")

        Dim XR2 As New XRKolomSpec
        XR2.InitializeData(bind)
    End Sub
End Class