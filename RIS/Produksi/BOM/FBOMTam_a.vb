Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBOMTam_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Dok As String

    Public Sub New(ByVal MdlID As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        DsAddDt = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select * From(Select Distinct convert(bit,'FALSE') as Cek,MdlID+MD.DivID+MD.KompID+MD.BBID As Data,MdlID,MD.DivID, D.Nama As Div,MD.KompID,K.Nama As Komp,MD.BBID,B.Nama As Bahan From M_ModelDtl MD Inner Join M_Div D On MD.DivID=D.DivID Inner Join M_Komp K on MD.KompID=K.KompID Inner Join M_BB B On MD.BBID=B.BBID Where MdlID='" & MdlID & "') as x Order By Div,Komp,Bahan Asc", koneksi)

        cmsl.TableMappings.Add("Table", "MdlTam")
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsAddDt, "MdlTam")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "MdlTam"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim x, i As Integer
        x = 0
        i = 0

        BOMTam = ""

        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
              
                If x = 1 Then
                    BOMTam = "'" & Me.GridView1.GetRowCellValue(i, "Data") & "'"
                Else
                    BOMTam &= ",'" & Me.GridView1.GetRowCellValue(i, "Data") & "'"
                End If
            End If
        Next

        Timer1.Enabled = True
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub
End Class