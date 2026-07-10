Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FAdjBJ_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AdjBJIDD,AdjBJID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Ket From T_AdjBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where AdjBJID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_AdjBJDtl" & Kode)
        Try
            DsMaster.Tables("T_AdjBJDtl" & Kode).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_AdjBJDtl" & Kode)

        DsMaster.Tables("T_AdjBJDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_AdjBJDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_AdjBJDtl" & Kode
    End Sub

    Public Sub New(ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Kode)
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.03
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub FAdjBJ_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If GridView1.GetRowCellValue(e.RowHandle, "Qty") < 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Green
                ElseIf GridView1.GetRowCellValue(e.RowHandle, "Qty") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class