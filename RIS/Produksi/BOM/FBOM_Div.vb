Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data

Public Class FBOM_Div
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean

    Public Sub New(ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim cmsl As SqlDataAdapter
        'Dim jml, assx As Integer

        'Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Kode & "'", koneksi)

        'With koneksi
        '    .Open()
        '    jml = command.ExecuteScalar()
        '    .Close()
        'End With

        'Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Kode & "' and Uk Like '%x%'", koneksi)

        'With koneksi
        '    .Open()
        '    assx = command2.ExecuteScalar()
        '    .Close()
        'End With

        'If jml > 4 Then
        '    'MsgBox("masuk1")
        '    cmsl = New SqlDataAdapter("Select ArtCode, Uk From T_BOMPO Where BOMID='" & Kode & "' Order By Uk Asc", koneksi)
        'Else
        '    If assx > 0 Then
        '        'MsgBox("masuk2")
        '        cmsl = New SqlDataAdapter("Select ArtCode, Uk From T_BOMPO Where BOMID='" & Kode & "' Order By Uk Asc", koneksi)
        '    Else
        '        'MsgBox("masuk3")
        '        'cmsl = New SqlDataAdapter("Select * From (Select ArtCode,Uk From T_BOMPO Where BOMID='" & Kode & "') as x Order By Cast(Uk as Decimal(18,1))", koneksi)
        '        cmsl = New SqlDataAdapter("Select * From (Select ArtCode,Uk From T_BOMPO Where BOMID='" & Kode & "') as x Order By Cast(Uk as Varchar(18))", koneksi)
        '    End If
        'End If

        cmsl = New SqlDataAdapter("Select Distinct Gol from M_DIv where Gol<>'' union all select Distinct '%' from M_DIv where Gol<>''", koneksi)

        cmsl.TableMappings.Add("Table", "BOMDiv")
        cmsl.Fill(DsMaster, "BOMDiv")
        DsMaster.Tables("BOMDiv").Clear()
        cmsl.Fill(DsMaster, "BOMDiv")

        Me.SLUArtCode.Properties.DataSource = DsMaster.Tables("BOMDiv")
        Me.SLUArtCode.Properties.DisplayMember = "Gol"
        Me.SLUArtCode.Properties.ValueMember = "Gol"
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub SLUArtCode_Leave(sender As Object, e As EventArgs) Handles SLUArtCode.Leave
        'Me.TBUk.EditValue = DsMaster.Tables("BOMUk").Select("ArtCode = '" & Me.SLUArtCode.Text & "'")(0).Item("Uk")
    End Sub


    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        'dataTrans = New Collection
        'dataTrans.Clear()

        'dataTrans.Add(Me.SLUArtCode.EditValue, 1)
        dataTrans.Add(Me.SLUArtCode.EditValue, 2)
        Me.Dispose()
    End Sub


End Class