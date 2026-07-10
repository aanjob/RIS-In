Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBrgAss_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AssID,Uk,Qty From M_BrgAssDtl Where AssID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgAssDtl")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgAssDtl")
        DsMaster.Tables("M_BrgAssDtl").Clear()
        cmsl.Fill(DsMaster, "M_BrgAssDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgAssDtl"
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

    Private Sub FBrgAss_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class