Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPromo_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PromoIDD,PromoID,Paket,JnsCust,JnsPerhit,Kelipatan,BeliMin,BeliMax,JnsPot,Pot From T_PromoDtl Where PromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PromoDtl" & Kode)
        Try
            DsMaster.Tables("T_PromoDtl" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PromoDtl" & Kode)

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_PromoDtl" & Kode

        cmsl = New SqlDataAdapter("Select PromoIDD,PromoIDDtl,PromoID,Paket,D.ArtCode,ArtName From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where PromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PromoDtl2" & Kode)
        Try
            DsMaster.Tables("T_PromoDtl2" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PromoDtl2" & Kode)

        DsMaster.Tables("T_PromoDtl2" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_PromoDtl2" & Kode).Columns("PromoID"), DsMaster.Tables("T_PromoDtl2" & Kode).Columns("Paket"), DsMaster.Tables("T_PromoDtl2" & Kode).Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_PromoDtl2" & Kode

        cmsl = New SqlDataAdapter("Select PromoIDD,PromoIDDtl,PromoID,Paket,D.ArtCode,ArtName From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where PromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PromoFree" & Kode)
        Try
            DsMaster.Tables("T_PromoFree" & Kode).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PromoFree" & Kode)

        DsMaster.Tables("T_PromoFree" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_PromoFree" & Kode).Columns("PromoID"), DsMaster.Tables("T_PromoFree" & Kode).Columns("Paket"), DsMaster.Tables("T_PromoFree" & Kode).Columns("ArtCode")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_PromoFree" & Kode

        If Me.GridView1.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            Me.GridView4.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
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


    Private Sub FPromo_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        'Try
        If Me.GridView1.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            Me.GridView4.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
        End If
        'Catch ex As Exception

        'End Try

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