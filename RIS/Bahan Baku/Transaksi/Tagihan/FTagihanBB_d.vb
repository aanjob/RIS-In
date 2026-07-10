Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTagihanBB_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Jenis, Kode As String

    Public Sub FillNLDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,SJ,Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir From T_TagihanNLDtl Where TagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TagihanNLDtl")
        Try
            DsMaster.Tables("T_TagihanNLDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TagihanNLDtl")

        DsMaster.Tables("T_TagihanNLDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanNLDtl").Columns("Nama")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TagihanNLDtl"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,TrmID,Tanggal,GdId,DiscP,DiscRp From T_TagihanDtl Where TagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TagihanDtl")
        Try
            DsMaster.Tables("T_TagihanDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TagihanDtl")

        DsMaster.Tables("T_TagihanDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl").Columns("TrmID")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_TagihanDtl"

        cmsl = New SqlDataAdapter("Select TagihIDD,TagihID,TagihDtl,TrmID,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRpSat,DiscRp,DiscP, RpDiscP,HarAkhir,DiscRpSatH,DiscRpH,DiscPH,RpDiscPH,HarAkhirH,HarSatDPP,HarBahan From T_TagihanDtl2 D Inner Join M_BB B On D.BBID=B.BBID Where TagihID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TagihanDtl2")
        Try
            DsMaster.Tables("T_TagihanDtl2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TagihanDtl2")

        DsMaster.Tables("T_TagihanDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TagihanDtl2").Columns("TrmIDD")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_TagihanDtl2"
    End Sub

    Public Sub New(ByVal Jns As String, ByVal Kd As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Jenis = Jns
        Kode = Kd
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

    Private Sub FTagihanBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Jenis = "LPB" Then
            Me.XTPLPB.PageVisible = True
            Me.XTPNonLPB.PageVisible = False

            FillDtl(Kode)
            Me.GridView4.Focus()

        Else
            Me.XTPLPB.PageVisible = False
            Me.XTPNonLPB.PageVisible = True

            FillNLDtl(Kode)
            Me.GridView1.Focus()
        End If
    End Sub

    Private Sub GridView3_RowCountChanged(sender As Object, e As EventArgs) Handles GridView3.RowCountChanged
        If Me.GridView3.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView4.ActiveFilterString = "[TrmID] = '" & Me.GridView3.GetFocusedRowCellValue("TrmID") & "'"
            'End If
        End If
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

    Private Sub GridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView4.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class