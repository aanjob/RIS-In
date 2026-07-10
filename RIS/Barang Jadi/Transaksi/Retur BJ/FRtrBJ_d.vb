Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FRtrBJ_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrIDD,RtrID,JualID,JualIDD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,IsiAsDos,HarDPJ,HarSat,Qty,Dos,Psg, stsHarga,HarSbDisc,RpDiscCust,DiscOB,RpDiscOB,RpDiscL,OngkirSat,Ongkir,HarAkhir,NamaLain From T_RtrBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where RtrID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrBJDtl" & Kode)
        Try
            DsMaster.Tables("T_RtrBJDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrBJDtl" & Kode)

        DsMaster.Tables("T_RtrBJDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrBJDtl" & Kode).Columns("JualID"), DsMaster.Tables("T_RtrBJDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RtrBJDtl" & Kode
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

    Private Sub FRtrBJ_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class