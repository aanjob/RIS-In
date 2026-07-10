Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTargetProd_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""

    Public Sub FillDtl(ArtCode As String, Tgl As Date)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TargetIDD,Art,TglBerlaku,Proses,Target From T_TargetProdDtl Where Art='" & ArtCode & "' and TglBerlaku='" & Tgl & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetProdDtl")
        Try
            DsMaster.Tables("T_TargetProdDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TargetProdDtl")

        DsMaster.Tables("T_TargetProdDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TargetProdDtl").Columns("Art"), DsMaster.Tables("T_TargetProdDtl").Columns("Proses")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TargetProdDtl"
    End Sub


    Public Sub New(ByVal ArtCode As String, Tgl As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        FillDtl(ArtCode, Tgl)
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