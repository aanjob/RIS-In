Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPOBJLk_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POIDD,POID,D.ArtCode,ArtName,Upp,Outs,Variasi,B.AssID,D.SatID,D.Isi,Qty,QtyPol,Psg,PsgPol,HBeli, HCBP,HAkhir,SisaKirim,BtlOrder From T_POBJLkDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJLkDtl" & Kode)
        Try
            DsMaster.Tables("T_POBJLkDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJLkDtl" & Kode)

        DsMaster.Tables("T_POBJLkDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJLkDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBJLkDtl" & Kode

        cmsl = New SqlDataAdapter("Select POIDD,POID,POIDDtl,D.ArtCode,Uk,IsiDlmDos,Qty,QtyPol From T_POBJLkDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where POID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_POBJLkDtl2" & Kode)
        cmsl.Fill(DsMaster, "T_POBJLkDtl2" & Kode)

        DsMaster.Tables("T_POBJLkDtl2" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJLkDtl2" & Kode).Columns("POID"), DsMaster.Tables("T_POBJLkDtl2" & Kode).Columns("POIDD"), DsMaster.Tables("T_POBJLkDtl2" & Kode).Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_POBJLkDtl2" & Kode
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


    Private Sub FPOBJLk_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class