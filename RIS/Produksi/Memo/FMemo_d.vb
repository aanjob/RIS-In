Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FMemo_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MemoIDD,MemoID,MemoIDRef,BOMID,MdlID,stsTidakPki,MD.DivID,D.Nama As Div,MD.KompID,K.Nama as Komp,MD.BBIDAs, B.Nama as BahanAs,UkBBAs,KebAs,SatAs,MD.BBIDTj,(Select Nama From M_BB Where BBID=MD.BBIDTj) As BahanTj,UkBBTj,KebTj,SatTj,MD.Ket From T_MemoDtl MD Inner Join M_Div D On MD.DivID=D.DivID Inner Join M_Komp K On MD.KompID=K.KompID Inner Join M_BB B On MD.BBIDAs=B.BBID Where MemoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_MemoDtl")
        cmsl.Fill(DsMaster, "T_MemoDtl")
        DsMaster.Tables("T_MemoDtl").Clear()
        cmsl.Fill(DsMaster, "T_MemoDtl")

        DsMaster.Tables("T_MemoDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_MemoDtl").Columns("BOMID"), DsMaster.Tables("T_MemoDtl").Columns("DivID"), DsMaster.Tables("T_MemoDtl").Columns("KompID"), DsMaster.Tables("T_MemoDtl").Columns("BBIDAs"), DsMaster.Tables("T_MemoDtl").Columns("BBIDTj")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_MemoDtl"
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

    Private Sub FMemo_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class