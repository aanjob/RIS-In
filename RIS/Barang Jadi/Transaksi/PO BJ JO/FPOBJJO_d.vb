Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPOBJJO_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POIDD,POID,Project,D.ArtCode,ArtName,W.Nama As Warna,D.Uk,D.SatID,D.Isi,Qty,QtyPol,Psg,PsgPol,HarSat,HCBP, HarAkhir,BtlProd,BtlOrder,SisaKirim,stsKirim,SisaProd,stsProd From T_POBJJODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJJODtl")
        Try
            DsMaster.Tables("T_POBJJODtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJJODtl")

        DsMaster.Tables("T_POBJJODtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJJODtl").Columns("Project"), DsMaster.Tables("T_POBJJODtl").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBJJODtl"
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


    Private Sub FPOBJJO_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class