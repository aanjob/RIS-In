Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class XXFSampReq_apLama
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Kd As String

    Public Sub New(ByVal Kode As String, StlID As String, Lastt As String, Pattern As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Kd = Kode
        Me.TBStyleID.EditValue = StlID
        Me.TBLast.EditValue = Lastt
        Me.TBPattern.EditValue = Pattern

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BApprove_Click(sender As Object, e As EventArgs) Handles BApprove.Click
        If Me.TBStyleID.EditValue = "" Or IsDBNull(Me.TBStyleID.EditValue) Then
            XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmSP As New SqlCommand("SPAppSampReq")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kd
            .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.TBStyleID.EditValue
            .Parameters.Add("@Last", SqlDbType.VarChar).Value = Me.TBLast.EditValue
            .Parameters.Add("@Pattern", SqlDbType.VarChar).Value = Me.TBPattern.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            Try
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With

                If x = 0 Then
                    XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Dispose()
                Else
                    XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Close()
    End Sub
End Class