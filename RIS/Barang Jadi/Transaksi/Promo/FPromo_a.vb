Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPromo_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Gol, ArtCode As String

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Gol = Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FPromo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DsMaster = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,B.ArtCode,ArtName,B.SatID,B.Isi,stsHarga From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Gol ='" & Gol & "' and B.Aktif='True' and H.Aktif='True' and Jenis='Agen'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgL")
        cmsl.Fill(DsMaster, "M_BrgL")
        DsMaster.Tables("M_BrgL").Clear()
        cmsl.Fill(DsMaster, "M_BrgL")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgL"
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.IsRowVisible(i) Then
                    GridView1.SetRowCellValue(i, "Cek", 0)
                End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.IsRowVisible(i) Then
                    GridView1.SetRowCellValue(i, "Cek", 1)
                End If
            Next
        End If
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        dataTrans = New Collection
        dataTrans.Clear()

        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        Timer1.Enabled = True
    End Sub
    
End Class