Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTAG_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TAGIDD,TAGID,'" & InisialBC & "'+D.BBID as BBID,BtNum,B.Nama as Bahan,D.Sat,Qty,HarSat,HarAkhir,D.Ket From T_TAGDtl D Inner Join M_BB B On D.BBID=B.BBID Where TAGID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TAGDtl")
        cmsl.Fill(DsMaster, "T_TAGDtl")
        DsMaster.Tables("T_TAGDtl").Clear()
        cmsl.Fill(DsMaster, "T_TAGDtl")

        DsMaster.Tables("T_TAGDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TAGDtl").Columns("BBID"), DsMaster.Tables("T_TAGDtl").Columns("BtNum")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TAGDtl"
    End Sub

    Public Sub New(ByVal Kode As String, GdID As String, Manuall As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        If Manuall = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & GdID & "'", koneksi)

            With koneksi
                .Open()
                InisialBC = command.ExecuteScalar()
                .Close()
            End With
        End If

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

    Private Sub FTAG_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class