Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data

Public Class FModel_tr
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Kd As String

    Public Sub New(ArtName As String, Warna As String, Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Kd = Kode

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,ArtCode,ArtName,Ass From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & ArtName & "' and W.Nama='" & Warna & "' Order By ArtCode Asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_Brg")
        Try
            DsMaster.Tables("M_Brg").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Brg")

        DsMaster.Tables("M_Brg").PrimaryKey = New DataColumn() {DsMaster.Tables("M_Brg").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Brg"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FModel_tr_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim x, i As Integer

        x = 0
        i = 0
        For i = 0 To DsMaster.Tables("M_Brg").Rows.Count - 1
            If DsMaster.Tables("M_Brg").Rows(i).Item("Cek") = True Then
                Dim cmSPDtl As New SqlCommand("SPInsM_ModelDtlTR")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kd
                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ass")
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With

                With koneksi
                    .Open()
                    cmSPDtl.ExecuteNonQuery()
                    x = cmSPDtl.Parameters("@Return").Value
                    .Close()
                End With

               If x <> 0 Then
                    XtraMessageBox.Show("Data Gagal Ditambahkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Ditambahkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Me.Dispose()
    End Sub
End Class