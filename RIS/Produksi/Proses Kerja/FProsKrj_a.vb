Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FProsKrj_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean

    Private Sub FProsKrj_a_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim cmsl As SqlDataAdapter
        DsAddDt = New System.Data.DataSet
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,Proses From M_Proses Where Aktif='True'", koneksi)

        cmsl.TableMappings.Add("Table", "M_Proses")
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsAddDt, "M_Proses")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "M_Proses"
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer = 0
        dataTrans = New Collection
        dataTrans.Clear()

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Proses"), "Proses" & x)
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