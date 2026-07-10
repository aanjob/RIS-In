Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHitKomisi_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HitKmsIDD,HitKmsID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Tahun,Bulan, Nama,PosisiID,JenisCust,JmlBayar,Telat,Pencapaian,PersenKomisi,Komisi From T_HitKomisi70Dtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where HitKmsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitKomisi70Dtl")
        Try
            DsMaster.Tables("T_HitKomisi70Dtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisi70Dtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HitKomisi70Dtl"


        cmsl = New SqlDataAdapter("Select HitKmsIDD,HitKmsID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Tahun,Bulan, Nama,PosisiID,SisaBayar,Telat,Pencapaian,PersenKomisi,Komisi From T_HitKomisi60Dtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where HitKmsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitKomisi60Dtl")
        Try
            DsMaster.Tables("T_HitKomisi60Dtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisi60Dtl")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_HitKomisi60Dtl"


        cmsl = New SqlDataAdapter("Select HitKmsIDD,HitKmsID,D.CabID,Case When D.CabID='' Then 'Semua Cabang' Else Cabang End As Cabang,Nama, PosisiID,TotPsg,Telat,PersenKomisi,Komisi From T_HitKomisiOSDtl D Left Outer Join M_Cab Cb On D.CabID=Cb.CabID Where HitKmsID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HitKomisiOSDtl")
        Try
            DsMaster.Tables("T_HitKomisiOSDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HitKomisiOSDtl")

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_HitKomisiOSDtl"
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

    Private Sub FHitKomisi_d_Load(sender As Object, e As EventArgs) Handles Me.Load
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

    Private Sub GridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView4.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class