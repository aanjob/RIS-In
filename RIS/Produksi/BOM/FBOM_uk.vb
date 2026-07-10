Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data

Public Class FBOM_uk
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean

    Public Sub New(ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Kode & "' and Uk Like '%x%'", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select ArtCode, Uk From T_BOMPO Where BOMID='" & Kode & "' Order By Uk Asc", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select ArtCode, Uk From T_BOMPO Where BOMID='" & Kode & "' Order By Uk Asc", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select * From (Select ArtCode,Uk From T_BOMPO Where BOMID='" & Kode & "') as x Order By Cast(Uk as Decimal(18,1))", koneksi)
            End If
        End If

        cmsl.TableMappings.Add("Table", "BOMUk")
        cmsl.Fill(DsMaster, "BOMUk")
        DsMaster.Tables("BOMUk").Clear()
        cmsl.Fill(DsMaster, "BOMUk")

        Me.SLUArtCode.Properties.DataSource = DsMaster.Tables("BOMUk")
        Me.SLUArtCode.Properties.DisplayMember = "ArtCode"
        Me.SLUArtCode.Properties.ValueMember = "Uk"
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub SLUArtCode_Leave(sender As Object, e As EventArgs) Handles SLUArtCode.Leave
        Me.TBUk.EditValue = DsMaster.Tables("BOMUk").Select("ArtCode = '" & Me.SLUArtCode.Text & "'")(0).Item("Uk")
    End Sub

   
    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        dataTrans = New Collection
        dataTrans.Clear()

        dataTrans.Add(Me.SLUArtCode.EditValue, 1)

        Me.Dispose()
    End Sub

    
End Class