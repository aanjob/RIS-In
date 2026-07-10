Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBOMTam_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String, BOMID As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TambahanIDD,TambahanID,ArtCode, BD.Uk, BD.DivID,D.Nama as Divisi, BD.KompID, K.Nama As Komponen, BD.BBID,B.Nama As Bahan,BD.Uk,BD.UkBB,BD.Sat,Std,Qty,Keb,Pol,BD.Ket,KaliQty,SPK,stsAdd,BD.stsJasa,BD.stsMentah,BD.BBIDInd,(Select Sum (Jml) From(Select Count(*) As Jml From T_POBBDtl D1 Where D1.BOMID= '" & Kode & "' and BBID=BD.BBID Union All Select Count(*) As Jml From T_BPB H1 Inner Join T_BPBDtl D1 On H1.BPBID=D1.BPBID Where H1.DocID= '" & Kode & "' and BBID=BD.BBID Union All Select Count(*) As Jml From T_RPB H1 Inner Join T_RPBDtl D1 On H1.RPBID=D1.RPBID Where H1.DocID= '" & Kode & "' and BBID=BD.BBID)As x) As PO From T_BOMTamDtl BD Inner Join M_Div D On BD.DivID=D.DivID Inner Join M_Komp K On BD.KompID=K.KompID Inner Join M_BB B On BD.BBID=B.BBID Where TambahanID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMDtl")
        Try
            DsMaster.Tables("T_BOMDtl").Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMDtl")

        DsMaster.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMDtl").Columns("DivID"), DsMaster.Tables("T_BOMDtl").Columns("KompID"), DsMaster.Tables("T_BOMDtl").Columns("BBID"), DsMaster.Tables("T_BOMDtl").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BOMDtl"

        cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & BOMID & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOMPO")
        Try
            DsMaster.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMPO")

        DsMaster.Tables("T_BOMPO").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMPO").Columns("POID"), DsMaster.Tables("T_BOMPO").Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_BOMPO"

        cmsl = New SqlDataAdapter("Select BBID From T_POBBDtl Where BOMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "POBB")
        cmsl.Fill(DsMaster, "POBB")
    End Sub

    Public Sub New(ByVal Kode As String, BOMID As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Kode, BOMID)
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


    Private Sub FBOM_d_Load(sender As Object, e As EventArgs) Handles Me.Load
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