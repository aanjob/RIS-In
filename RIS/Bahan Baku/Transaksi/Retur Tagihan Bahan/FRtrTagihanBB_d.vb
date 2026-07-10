Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FRtrTagihanBB_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillNLDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrTagihIDD,RtrTagihID,SJ,Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir From T_RtrTagihNLDtl Where RtrTagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagihNLDtl")
        Try
            DsMaster.Tables("T_RtrTagihNLDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagihNLDtl")

        DsMaster.Tables("T_RtrTagihNLDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrTagihNLDtl").Columns("Nama")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RtrTagihNLDtl"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RtrTagihIDD,RtrTagihID,RtrID,Tanggal,GdId,DiscP,DiscRp From T_RtrTagihDtl Where RtrTagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagihDtl")
        Try
            DsMaster.Tables("T_RtrTagihDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagihDtl")

        DsMaster.Tables("T_RtrTagihDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrTagihDtl").Columns("Nama")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_RtrTagihDtl"

        cmsl = New SqlDataAdapter("Select RtrTagihIDD,RtrTagihID,TagihDtl,RtrIDD,RtrID,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc, DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir,DiscRpSatH,DiscRpH,DiscPH,RpDiscPH,HarAkhirH,HarSatDPP ,HarBahan From T_RtrTagihDtl2 D Inner Join M_BB B On D.BBID=B.BBID Where RtrTagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrTagihDtl2")
        Try
            DsMaster.Tables("T_RtrTagihDtl2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RtrTagihDtl2")

        DsMaster.Tables("T_RtrTagihDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RtrTagihDtl2").Columns("RtrTagihID"), DsMaster.Tables("T_RtrTagihDtl2").Columns("RtrID"), DsMaster.Tables("T_RtrTagihDtl2").Columns("BBID")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_RtrTagihDtl2"
    End Sub

    Public Sub New(ByVal jenis As String, ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        If jenis = "LPB" Then
            Me.XTPLPB.PageVisible = True
            Me.XTPNonLPB.PageVisible = False

            FillDtl(Kode)
        Else
            Me.XTPLPB.PageVisible = False
            Me.XTPNonLPB.PageVisible = True

            FillNLDtl(Kode)
        End If
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

    Private Sub FRtrTagihanBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView3_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView3.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView4_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class