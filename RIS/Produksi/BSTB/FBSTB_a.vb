Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FBSTB_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Jenis As String

    Public Sub New(Gol As String, Grup As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select 0 as DocIDD, '--' As BOMID ,ArtCode,ArtName,W.Nama As Warna,SatID,Isi,0 As QtyKirim,0 as Qty From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and Gol In ('" & Gol & "','Promosi') and Grup='" & Grup & "'", koneksi)

        cmsl.TableMappings.Add("Table", "TrmBJTemp" & Gol)
        DsAddDt = New System.Data.DataSet
        Try
            DsAddDt.Tables("TrmBJTemp" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsAddDt, "TrmBJTemp" & Gol)

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "TrmBJTemp" & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DocIDD"), "DocIDD" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BOMID"), "BOMID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty") * Me.GridView1.GetRowCellValue(i, "Isi"), "Psg" & x)
                If Me.GridView1.GetRowCellValue(i, "SatID") = "P" Then
                    dataTrans.Add(0, "Dos" & x)
                Else
                    dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Dos" & x)

                End If
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