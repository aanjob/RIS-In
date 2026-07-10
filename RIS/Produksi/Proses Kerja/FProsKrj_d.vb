Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FProsKrj_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Art As String, Tgl As Date)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ProsKIDD,Art,Tanggal,Proses,Urut From T_ProsesKrjDtl D Inner Join M_Brg B On D.Art=B.MerkID+B.KatID+B.JnsID+'-'+B.Urut Where Art='" & Art & "' and Tanggal='" & Tgl & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ProsesKrjDtl")
        Try
            DsMaster.Tables("T_ProsesKrjDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ProsesKrjDtl")

        DsMaster.Tables("T_ProsesKrjDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ProsesKrjDtl").Columns("Art"), DsMaster.Tables("T_ProsesKrjDtl").Columns("Proses")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ProsesKrjDtl"
    End Sub

    Public Sub New(ByVal Art As String, Tgl As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Art,Tgl)
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

    Private Sub FSpec_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub


End Class