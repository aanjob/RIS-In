Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FModel_cp
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Private Sub FModel_cp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MdlID,ArtName,Warna,Ket From M_Model", koneksi)
        cmsl.TableMappings.Add("Table", "M_ModelLUE")
        cmsl.Fill(DsMaster, "M_ModelLUE")
        DsMaster.Tables("M_ModelLUE").Clear()
        cmsl.Fill(DsMaster, "M_ModelLUE")

        Me.SLUMdlID.Properties.DataSource = DsMaster.Tables("M_ModelLUE")
        Me.SLUMdlID.Properties.DisplayMember = "MdlID"
        Me.SLUMdlID.Properties.ValueMember = "MdlID"
    End Sub
    Private Sub SLUMdlID_Leave(sender As Object, e As EventArgs) Handles SLUMdlID.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,MdlID+MD.DivID+MD.KompID+MD.BBID As Data,MD.DiviD,D.Nama As Div,MD.KompID,K.Nama As Komp,MD.BBID,B.Nama As Bahan,MD.UkBB,MD.Sat From M_ModelDtl MD Inner Join M_Div D On MD.DivID=D.DivID Inner Join M_Komp K On MD.KompID=K.KompID Inner Join M_BB B On MD.BBID=B.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ModelCp")
        Try
            DsMaster.Tables("M_ModelCp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ModelCp")

        DsMaster.Tables("M_ModelCp").PrimaryKey = New DataColumn() {DsMaster.Tables("M_ModelCp").Columns("ArtCode"), DsMaster.Tables("M_ModelCp").Columns("DivID"), DsMaster.Tables("M_ModelCp").Columns("KompID"), DsMaster.Tables("M_ModelCp").Columns("BBID"), DsMaster.Tables("M_ModelCp").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_ModelCp"
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Dim x, i As Integer

        x = 0
        i = 0

        MdlCp = ""

        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
                If x = 1 Then
                    MdlCp = "'" & Me.GridView1.GetRowCellValue(i, "Data") & "'"
                Else
                    MdlCp &= ",'" & Me.GridView1.GetRowCellValue(i, "Data") & "'"
                End If
            End If
        Next

        Timer1.Enabled = True

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