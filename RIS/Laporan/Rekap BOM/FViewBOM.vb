Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FViewBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsLapF As New System.Data.DataSet

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ArtCodeInd,ArtCode,Tot From T_BOMPO Where BOMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMPO")
        cmsl.Fill(DsLapF, "T_BOMPO")
        DsLapF.Tables("T_BOMPO").Clear()
        cmsl.Fill(DsLapF, "T_BOMPO")

        Dim table2 = DsLapF.Tables("T_BOMPO")
        table2.PrimaryKey = New DataColumn() {table2.Columns("ArtCodeInd"), table2.Columns("ArtCode")}

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "T_BOMPO"
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


    Private Sub FViewBOM_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsLapF = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class