Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTrialPs_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrlPsIDD,TrlPsID,Div,Masalah,Solusi,TglSelesai,Ket From T_TrlPsDtl Where TrlPsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrlPsDtl")
        Try
            DsMaster.Tables("T_TrlPsDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrlPsDtl")

        DsMaster.Tables("T_TrlPsDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrlPsDtl").Columns("Div")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrlPsDtl"
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

    Private Sub FBPB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class