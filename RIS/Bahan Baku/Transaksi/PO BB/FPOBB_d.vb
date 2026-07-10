Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPOBB_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Manual As Boolean

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POIDD,POID,Free,D.BOMID,D.DocIDD,D.BBID,B.Nama As Bahan,Qty,D.Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP, HarAkhir,BtlOrder,SisaKirim,stsKirim From T_POBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_POBBDtl")
        cmsl.Fill(DsMaster, "T_POBBDtl")
        DsMaster.Tables("T_POBBDtl").Clear()
        cmsl.Fill(DsMaster, "T_POBBDtl")

        DsMaster.Tables("T_POBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBBDtl").Columns("BOMID"), DsMaster.Tables("T_POBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBBDtl"

        cmsl = New SqlDataAdapter("Select POIDD,POID,POIDDtl,D.BOMID,BBIDJs,D.BBID, B.Nama As Bahan,Qty,D.Sat From T_POBBJs D Inner Join M_BB B On D.BBID=B.BBID Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBBJs")
        cmsl.Fill(DsMaster, "T_POBBJs")
        DsMaster.Tables("T_POBBJs").Clear()
        cmsl.Fill(DsMaster, "T_POBBJs")

         DsMaster.Tables("T_POBBJs").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBBJs").Columns("BOMID"), DsMaster.Tables("T_POBBJs").Columns("BBIDJs"), DsMaster.Tables("T_POBBJs").Columns("BBID")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_POBBJs"
    End Sub

    Public Sub New(ByVal Kode As String, Manuall As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Kode)
        Manual = Manuall
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

    Private Sub FPOBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()

        If Me.GridView1.RowCount > 0 Then
            'If Me.GridView2.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
            'End If
        End If

        If Manual = True Then
            Me.GridColumn3.Visible = False
        Else
            Me.GridColumn3.Visible = True
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
       If Me.GridView1.RowCount > 0 Then
            'If Me.GridView2.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[POIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("POIDD") & "'"
            'End If
        End If
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