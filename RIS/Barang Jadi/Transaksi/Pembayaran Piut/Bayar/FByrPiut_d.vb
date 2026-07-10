Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FByrPiut_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ByrPiutIDD,ByrPiutID,DocID,CaraBayar,(Select Isnull((Select Nama From M_JnsPot Where PotID=T_ByrPiutDtl.DocID),'')) As Deskripsi,SisaPakai From T_ByrPiutDtl Where ByrPiutID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ByrPiutDtl" & Kode)
        Try
            DsMaster.Tables("T_ByrPiutDtl" & Kode).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ByrPiutDtl" & Kode)

        DsMaster.Tables("T_ByrPiutDtl" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ByrPiutDtl" & Kode).Columns("DocID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ByrPiutDtl" & Kode


        cmsl = New SqlDataAdapter("Select D2.ByrPiutIDD,ByrPiutDtl,D2.ByrPiutID,D.CaraBayar,JualID,Bayar From T_ByrPiutDtl2 D2 Inner Join T_ByrPiutDtl D On D2.ByrPiutDtl=D.ByrPiutIDD Where D2.ByrPiutID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ByrPiutDtl2" & Kode)
        Try
            DsMaster.Tables("T_ByrPiutDtl2" & Kode).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ByrPiutDtl2" & Kode)

        DsMaster.Tables("T_ByrPiutDtl2" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ByrPiutDtl2" & Kode).Columns("ByrPiutDtl"), DsMaster.Tables("T_ByrPiutDtl2" & Kode).Columns("JualID")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ByrPiutDtl2" & Kode

        If Me.GridView1.RowCount > 0 Then
            '    If Me.GridView3.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[ByrPiutDtl] = '" & Me.GridView1.GetFocusedRowCellValue("ByrPiutIDD") & "'"
            '    End If
        End If

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

    Private Sub FByrPiut_d_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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