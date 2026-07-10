Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSpec_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SpecID,SD.DivID,D.Nama as Divisi,SD.KompID,K.Nama As Komponen,SD.BBID,B.Nama As BB,B.Uk,B.Wrn,SD.Ket,SD.Sat, SD.stsJasa,stsMentah,BBIDInd From M_SpecDtl SD Inner Join M_Div D On SD.DivID=D.DivID Inner Join M_Komp K On SD.KompID=K.KompID Inner Join M_BB B On SD.BBID=B.BBID Where SpecID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_SpecDtl")
        Try
            DsMaster.Tables("M_SpecDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SpecDtl")

        DsMaster.Tables("M_SpecDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("M_SpecDtl").Columns("DivID"), DsMaster.Tables("M_SpecDtl").Columns("KompID"), DsMaster.Tables("M_SpecDtl").Columns("BBID"), DsMaster.Tables("M_SpecDtl").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_SpecDtl"
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

    Private Sub FSpec_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

   
End Class