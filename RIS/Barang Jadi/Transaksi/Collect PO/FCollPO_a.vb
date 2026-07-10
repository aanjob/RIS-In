Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FCollPO_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Gudang, Doc As String
    Dim Tanggal As Date

    Public Sub New(Gol As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,0 As Qty From M_Brg B Where B.Aktif='True' and SatID Like 'D%' and Gol ='" & Gol & "' and ArtCode In (Select ArtCode From M_BrgHarga where stsHarga='Normal' and Aktif='True')", koneksi)

        cmsl.TableMappings.Add("Table", "M_Brg")
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "M_Brg")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "M_Brg"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)

                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            Timer1.Enabled = True
        End If
    End Sub
End Class