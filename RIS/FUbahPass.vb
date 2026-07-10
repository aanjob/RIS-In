Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FUbahPass
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub Bersih()
        Me.TBPassLama.EditValue = ""
        Me.TBPassBaru.EditValue = ""
        Me.TBKonfirmasi.EditValue = ""
    End Sub

    Private Sub FUbahPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Bersih()
    End Sub

    Private Sub BSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BSave.Click
        koneksi.Close()

        If MainModule.PassAktif = Me.TBPassBaru.EditValue Then
            XtraMessageBox.Show("Password Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.PassAktif = Me.TBPassLama.EditValue Then
            If Me.TBPassBaru.EditValue.Equals(Me.TBKonfirmasi.EditValue) Then
                Dim cmsl As SqlCommand
                cmsl = New SqlCommand("Update M_User Set Passwordd=(Select [dbo].[fcEncrypt]('" & Me.TBPassBaru.EditValue & "')) where UserID=" & MainModule.UserAktif & "")
                cmsl.Connection = koneksi
                With koneksi
                    .Open()
                    cmsl.ExecuteNonQuery()
                    .Close()
                End With
                XtraMessageBox.Show("Password Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                MainModule.PassAktif = Me.TBPassBaru.EditValue
                Bersih()
                Me.Dispose()
            Else
                XtraMessageBox.Show("Password Tidak Sama", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            XtraMessageBox.Show("Password Lama Anda Salah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Bersih()
        End If

    End Sub

    Private Sub BCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCancel.Click
        Bersih()
        Me.Dispose()
    End Sub

    Private Sub TBPassLama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBPassLama.KeyPress, TBPassBaru.KeyPress, TBKonfirmasi.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class