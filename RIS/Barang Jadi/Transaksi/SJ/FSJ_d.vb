Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSJ_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SJIDD,SJID,NPIDD,D.ArtCode,B.ArtName,D.SatID,D.Isi,HarDPJ,HarSat,Qty,Dos,Psg,stsHarga, HarSbDisc,DiscOB,RpDiscOB,RpDiscCust,OngkirSat,Ongkir,HarAkhir,Selisih,SelisihExtra From T_SJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where SJID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJDtl" & Kode)
        Try
            DsMaster.Tables("T_SJDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SJDtl" & Kode)

        DsMaster.Tables("T_SJDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJDtl" & Kode).Columns("SJID"), DsMaster.Tables("T_SJDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SJDtl" & Kode

        cmsl = New SqlDataAdapter("Select SJIDD,SJID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg From T_SJFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where SJID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJFree" & Kode)
        Try
            DsMaster.Tables("T_SJFree" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SJFree" & Kode)

        DsMaster.Tables("T_SJFree" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJFree" & Kode).Columns("SJID"), DsMaster.Tables("T_SJFree" & Kode).Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SJFree" & Kode
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

    Private Sub FSJ_d_Load(sender As Object, e As EventArgs) Handles Me.Load
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
End Class