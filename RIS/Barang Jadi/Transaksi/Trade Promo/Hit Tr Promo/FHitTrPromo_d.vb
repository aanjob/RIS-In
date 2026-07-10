Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHitTrPromo_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HitTrPromoIDD,HitTrPromoID,D.CabID,Cb.Cabang,D.CustID,C.Nama As Cust,J.Jenis As JnsCust,Omset,Pembagi,Poin,Reward,Retur, ProsRtr,RewardRtr,Brand,RewardBrand,TotReward From T_HitTrPromoDtl D Inner Join M_Cust C On D.CustID=C.CustID Inner Join M_Cab Cb On D.CabID=Cb.CabID Inner Join M_JnsCust J On C.JnsCustID=J.JnsCustID Inner Join M_Kota K On C.KotaID=K.KotaID Where HitTrPromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitTrPromoDtl")
        Try
            DsMaster.Tables("T_HitTrPromoDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitTrPromoDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HitTrPromoDtl"

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

    Private Sub FHitTrPromo_d_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class