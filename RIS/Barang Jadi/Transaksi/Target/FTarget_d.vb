Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FTarget_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TargetIDD,Tahun,D.CabID,Cabang,SubJns,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess From T_TargetDtl D Inner Join M_Cab C On C.CabID=D.CabID Where Tahun='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetDtl")
        Try
            DsMaster.Tables("T_TargetDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TargetDtl")

        DsMaster.Tables("T_TargetDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TargetDtl").Columns("Tahun"), DsMaster.Tables("T_TargetDtl").Columns("CabID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TargetDtl"
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

    Private Sub FTrmBJ_d_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class