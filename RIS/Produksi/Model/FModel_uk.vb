Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data

Public Class FModel_uk
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean

    Public Sub New(ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From M_ModelDtl Where MdlID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From M_ModelDtl Where MdlID ='" & Kode & "' and Uk Like '%x%'", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,ArtCode, Uk From M_ModelDtl Where MdlID='" & Kode & "' Order By Uk Asc", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,ArtCode, Uk From M_ModelDtl Where MdlID='" & Kode & "' Order By Uk Asc", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select * From (Select Distinct convert(bit,'FALSE') as Cek,ArtCode,Uk From M_ModelDtl Where MdlID='" & Kode & "') as x Order By Cast(Uk as Decimal(18,1))", koneksi)
            End If
        End If

        cmsl.TableMappings.Add("Table", "M_BrgUk")
        cmsl.Fill(DsMaster, "M_BrgUk")
        DsMaster.Tables("M_BrgUk").Clear()
        cmsl.Fill(DsMaster, "M_BrgUk")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgUk"
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To GridView1.RowCount - 1
                GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To GridView1.RowCount - 1
                GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Uk"), x)
            End If
        Next

        If x <= 12 Then
            Dim y : For y = x + 1 To 12
                dataTrans.Add("", y)
            Next
        End If

        Me.Dispose()
    End Sub
End Class