Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FScmKomisi_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ScmIDD,Tahun,P.PosisiID,JenisCust,stsHarga,SubJns,JnsPerhit,BtsBawah,BtsAtas,TelatBawah,TelatAtas, JnsKomisi,Komisi From M_ScmKmsDtl D Inner Join M_Posisi P On D.PosisiID=P.PosisiID Where Tahun='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ScmKmsDtl")
        Try
            DsMaster.Tables("M_ScmKmsDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ScmKmsDtl")

        DsMaster.Tables("M_ScmKmsDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("M_ScmKmsDtl").Columns("Tahun"), DsMaster.Tables("M_ScmKmsDtl").Columns("PosisiID"), DsMaster.Tables("M_ScmKmsDtl").Columns("JenisCust"), DsMaster.Tables("M_ScmKmsDtl").Columns("stsHarga"), DsMaster.Tables("M_ScmKmsDtl").Columns("SubJns"), DsMaster.Tables("M_ScmKmsDtl").Columns("BtsBawah"), DsMaster.Tables("M_ScmKmsDtl").Columns("BtsAtas"), DsMaster.Tables("M_ScmKmsDtl").Columns("TelatBawah"), DsMaster.Tables("M_ScmKmsDtl").Columns("TelatAtas")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_ScmKmsDtl"
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

    Private Sub FScmKomisi_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class