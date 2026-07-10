Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPilihBahan
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2 As Boolean
    Dim BBID As String = ""
    Dim Gol As String = ""
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub FPilihBahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BBID,B.Nama As Bahan, Sat From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where J.Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BBL")
        cmsl.Fill(DsLapF, "M_BBL")
        DsLapF.Tables("M_BBL").Clear()
        cmsl.Fill(DsLapF, "M_BBL")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_BBL"
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
                    BBID = "'" & Me.GridView1.GetRowCellValue(i, "BBID") & "'"
                Else
                    BBID &= ",'" & Me.GridView1.GetRowCellValue(i, "BBID") & "'"
                End If
            End If
        Next

        Dim XR As New XRLapBB
        XR.InitializeData(False, Gol, BBID)
    End Sub
End Class