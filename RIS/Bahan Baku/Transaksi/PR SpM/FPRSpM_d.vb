Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPRSpM_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""

    Public Sub FillDtl(Kode As String, Tipe As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PRSMIDD,PRSMID,MesinID,(Select Nama From M_BB Where BBID=MesinID) as Mesin,D.BBID as BBID,B.Nama as Bahan,D.Sat,Qty,D.Ket,BtlOrder,SisaPO,stsPO From T_PRSpMDtl D Inner Join M_BB B On D.BBID=B.BBID Where PRSMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PRSpMDtl")
        Try
            DsMaster.Tables("T_PRSpMDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PRSpMDtl")

        DsMaster.Tables("T_PRSpMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_PRSpMDtl").Columns("MesinID"), DsMaster.Tables("T_PRSpMDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_PRSpMDtl"

        If Tipe = "Sparepart" Then
            Me.GridColumn3.OptionsColumn.AllowEdit = True
            Me.GridColumn3.Visible = True
            Me.GridColumn4.Visible = True
        Else
            Me.GridColumn3.OptionsColumn.AllowEdit = False
            Me.GridColumn3.Visible = False
            Me.GridColumn4.Visible = False
        End If
    End Sub

    Public Sub New(ByVal Kode As String, Tipe As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Kode, Tipe)
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

    Private Sub FPRTool_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class