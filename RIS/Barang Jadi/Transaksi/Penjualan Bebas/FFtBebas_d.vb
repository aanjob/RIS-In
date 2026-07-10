Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFtBebas_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualIDD,JualID,Nama,Size,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir From T_JualBebas_SJDtl Where JualID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualBebasDtl")
        Try
            DsMaster.Tables("T_JualBebasDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_JualBebasDtl")

        DsMaster.Tables("T_JualBebasDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBebasDtl").Columns("Nama")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_JualBebasDtl"

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

    Private Sub FFtBebas_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub
End Class