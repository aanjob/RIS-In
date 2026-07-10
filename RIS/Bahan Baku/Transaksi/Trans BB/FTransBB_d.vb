Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTransBB_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""
    Dim Manual As Boolean

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrIDD,TrID,'" & InisialBC & "'+D.BBIDAs As BBIDAs,BtNumAs,Nama As BahanAs,D.SatAs,QtyAs,'" & InisialBC & "'+D.BBIDTj As BBIDTj,BtNumTj,(Select Nama From M_BB Where BBID=D.BBIDTj) As BahanTj,D.SatTj,QtyTj,D.Ket From T_TrBBDtl D Inner Join M_BB B On D.BBIDAs=B.BBID Where TrID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrBBDtl")
        Try
            DsMaster.Tables("T_TrBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrBBDtl")

        DsMaster.Tables("T_TrBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrBBDtl").Columns("BBIDAs"), DsMaster.Tables("T_TrBBDtl").Columns("BtNumAs"), DsMaster.Tables("T_TrBBDtl").Columns("BBIDTj"), DsMaster.Tables("T_TrBBDtl").Columns("BtNumTj")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrBBDtl"
    End Sub

    Public Sub New(ByVal Kode As String, GdID As String, Manuall As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Manual = Manuall

        If Manual = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & GdID & "'", koneksi)

            With koneksi
                .Open()
                InisialBC = command.ExecuteScalar()
                .Close()
            End With
        End If

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


    Private Sub FTransBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class