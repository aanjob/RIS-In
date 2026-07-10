Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFtBJ_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualIDD,JualID,D.ArtCode,B.ArtName,D.SatID,D.Isi,HarDPJ,HarSat,Qty,Dos,Psg,stsHarga,HarSbDisc, DiscOB,RpDiscOB,RpDiscCust,RpDiscL,OngkirSat,Ongkir,HarAkhir,Selisih,SelisihExtra,NamaLain,D.Ket From T_JualBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where JualID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualBJDtl" & Kode)
        Try
            DsMaster.Tables("T_JualBJDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_JualBJDtl" & Kode)

        DsMaster.Tables("T_JualBJDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBJDtl" & Kode).Columns("JualID"), DsMaster.Tables("T_JualBJDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_JualBJDtl" & Kode

        cmsl = New SqlDataAdapter("Select JualIDD,JualID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg From T_JualBJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where JualID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualBJFree" & Kode)
        Try
            DsMaster.Tables("T_JualBJFree" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_JualBJFree" & Kode)

        DsMaster.Tables("T_JualBJFree" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_JualBJFree" & Kode).Columns("JualID"), DsMaster.Tables("T_JualBJFree" & Kode).Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_JualBJFree" & Kode
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

    Private Sub FFtBJ_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub
End Class