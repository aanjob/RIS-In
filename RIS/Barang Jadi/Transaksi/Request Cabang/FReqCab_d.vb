Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FReqCab_d

    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqCIDD,ReqCID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg,Ditolak,Ket,BtlOrder,SisaKirim,stsKirim From T_ReqCDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ReqCID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqCDtl" & Kode)
        Try
            DsMaster.Tables("T_ReqCDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqCDtl" & Kode)

        DsMaster.Tables("T_ReqCDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqCDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ReqCDtl" & Kode
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

    Private Sub FReqCab_d_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class