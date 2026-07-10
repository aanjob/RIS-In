Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHslProdTKL_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""

    Public Sub FillDtl(Tanggal As Date)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TKLIDD,Tanggal,Proses,Line,Shiift,TKN,JamN,TKL,JamL From T_HslProdTKLDtl Where Tanggal='" & Tanggal & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HslProdTKLDtl")
        Try
            DsMaster.Tables("T_HslProdTKLDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HslProdTKLDtl")

        DsMaster.Tables("T_HslProdTKLDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HslProdTKLDtl").Columns("Tanggal"), DsMaster.Tables("T_HslProdTKLDtl").Columns("Proses"), DsMaster.Tables("T_HslProdTKLDtl").Columns("Line"), DsMaster.Tables("T_HslProdTKLDtl").Columns("Shiift")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HslProdTKLDtl"
    End Sub

    Public Sub New(Tanggal As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        FillDtl(Tanggal)

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
        Me.BandedGridView1.Focus()
    End Sub

    Private Sub BandedGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles BandedGridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class