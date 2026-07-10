Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FOpBB_dv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select OpBBIDD,OpBBID,AddDt,J.Nama As Jenis,D.BBID,B.Nama As Bahan,D.Sat,QtyD,SalD,QtyF,SalF, SelisihQty, SelisihSal,HarSat, HarSatAs,D.Ket From T_OpBBDtl D Inner Join M_BB B On D.BBID=B.BBID Inner Join M_BBJns J On B.JnsID=J.JnsID Where OpBBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_OpBBDtl")
        Try
            DsMaster.Tables("T_OpBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_OpBBDtl")

        DsMaster.Tables("T_OpBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_OpBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_OpBBDtl"
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

    Private Sub FOpBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                If BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihQty") < 0 Or BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihSal") < 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihQty") > 0 Or BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihSal") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Green
                ElseIf BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihQty") = 0 Or BandedGridView1.GetRowCellValue(e.RowHandle, "SelisihSal") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class