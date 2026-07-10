Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FConvert_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ConvIDD,ConvID,D.ArtCodeD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,Qty,Dos,Psg From T_ConvAs D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ConvID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ConvAs" & Kode)
        Try
            DsMaster.Tables("T_ConvAs" & Kode).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ConvAs" & Kode)

        DsMaster.Tables("T_ConvAs" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ConvAs" & Kode).Columns("ArtCodeD"), DsMaster.Tables("T_ConvAs" & Kode).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ConvAs" & Kode

        cmsl = New SqlDataAdapter("Select ConvIDD,ConvID,D.ArtCodeD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,Qty,Dos,Psg From T_ConvTj D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ConvID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ConvTj" & Kode)
        Try
            DsMaster.Tables("T_ConvTj" & Kode).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ConvTj" & Kode)

        DsMaster.Tables("T_ConvTj" & Kode).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ConvTj" & Kode).Columns("ArtCodeD"), DsMaster.Tables("T_ConvTj" & Kode).Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ConvTj" & Kode

        If Me.GridView1.RowCount > 0 Then
            If Me.GridView2.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[ArtCodeD] ='" & Me.GridView1.GetFocusedRowCellValue("ArtCodeD") & "'"
            End If

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

    Private Sub FConvert_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.RowCount > 0 Then
            If Me.GridView2.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[ArtCodeD] ='" & Me.GridView1.GetFocusedRowCellValue("ArtCodeD") & "'"
            End If
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