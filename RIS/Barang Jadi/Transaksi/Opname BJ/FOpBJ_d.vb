Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FOpBJ_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select OpBJIDD,OpBJID,D.ArtCode,ArtName,D.SatID,D.Isi,QtyD,DosD,PsgD,QtyF,DosF,PsgF,Selisih,Ket From T_OpBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where OpBJID='" & Kode & "' Order By D.ArtCode Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_OpBJDtl" & Kode)
        Try
            DsMaster.Tables("T_OpBJDtl" & Kode).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_OpBJDtl" & Kode)

        DsMaster.Tables("T_OpBJDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_OpBJDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_OpBJDtl" & Kode
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

    Private Sub FOpBJ_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.BandedGridView1.Focus()
    End Sub

    Private Sub BandedGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles BandedGridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub BandedGridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles BandedGridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If BandedGridView1.GetRowCellValue(e.RowHandle, "Selisih") < 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf BandedGridView1.GetRowCellValue(e.RowHandle, "Selisih") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Green
                ElseIf BandedGridView1.GetRowCellValue(e.RowHandle, "Selisih") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class