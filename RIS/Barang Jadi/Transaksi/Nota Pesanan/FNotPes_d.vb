Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FNotPes_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select NPIDD,NPID,D.ArtCode,ArtName,D.SatID,D.Isi,HarDPJ,HarSat,Qty,QtyCab,DosCab,PsgCab,QtyPst,DosPst,PsgPst, stsHarga,HarSbDisc,DiscOB,RpDiscOB,RpDiscCust,OngkirSat,Ongkir,HarAkhir,Selisih,SelisihExtra,HarSbDiscPst,RpDiscOBPst,RpDiscCustPst, OngkirPst,HarAkhirPst,SelisihPst,SelisihExtraPst,BtlOrder,SisaKirim,stsKirim From T_NotPesDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_NotPesDtl" & Kode)
        Try
            DsMaster.Tables("T_NotPesDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_NotPesDtl" & Kode)

        DsMaster.Tables("T_NotPesDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_NotPesDtl" & Kode).Columns("NPID"), DsMaster.Tables("T_NotPesDtl" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_NotPesDtl" & Kode

        cmsl = New SqlDataAdapter("Select NPIDD,NPID,D.ArtCode,ArtName,D.SatID,D.Isi,QtyCab,DosCab,PsgCab,QtyPst,DosPst,PsgPst From T_NotPesFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where NPID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_NotPesFree" & Kode)
        Try
            DsMaster.Tables("T_NotPesFree" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_NotPesFree" & Kode)

        DsMaster.Tables("T_NotPesFree" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_NotPesFree" & Kode).Columns("NPID"), DsMaster.Tables("T_NotPesFree" & Kode).Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_NotPesFree" & Kode
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

    Private Sub FNotPes_d_Load(sender As Object, e As EventArgs) Handles Me.Load
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