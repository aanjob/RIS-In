Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FOpenForm
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Versi As String

    Private Sub FLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TBUser.Focus()
    End Sub

    Private Sub TBPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBPassword.KeyPress
        Dim AsciiCode As Integer = Asc(e.KeyChar)
        Select Case AsciiCode
            Case 13

                Dim Reader As SqlClient.SqlDataReader
                Dim cmd As SqlCommand
                cmd = New SqlCommand("Select UserID,[dbo].[fcDecrypt](LoginID) as LoginID,Inisial,SU From M_User Where [dbo].[fcDecrypt](LoginID)='" & Me.TBUser.Text & "' and [dbo].[fcDecrypt](Passwordd)= '" & Me.TBPassword.Text & "' and Aktif = 'True'")
                cmd.Connection = koneksi

                With koneksi
                    .Open()
                    Reader = cmd.ExecuteReader
                    If Reader.HasRows Then
                        While Reader.Read
                            MainModule.UserAktifBtl = Reader.Item(0)
                            MainModule.LoginAktifBtl = Reader.Item(1)
                            MainModule.InisialAktifBtl = Reader.Item(2)
                            MainModule.SU = Reader.Item(3)
                            MainModule.PassAktifBtl = Me.TBPassword.EditValue
                        End While
                    Else
                        FcMsgBox("User Tidak Aktif Atau Tidak Terdaftar", "Error", MessageBoxIcon.Error)

                        Reader.Close()
                        koneksi.Close()
                        Exit Sub
                    End If
                End With
                Reader.Close()
                koneksi.Close()

                If Me.TBUser.EditValue = Me.TBPassword.EditValue Then
                    FcMsgBox("Please Change Your Password", "Warning", MessageBoxIcon.Exclamation)
                End If

                If MainModule.SU = False Then
                    FcMsgBox("Anda Tidak Mempunyai Akses Untuk Merubah Inputan", "Error", MessageBoxIcon.Error)
                    Exit Sub
                End If

                Me.Dispose()

            Case 27
                Me.Dispose()

        End Select
    End Sub

    Private Sub TBUser_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBUser.KeyPress, TBPassword.KeyPress
        Dim AsciiCode As Integer = Asc(e.KeyChar)
        Select Case AsciiCode
            Case 27
                Me.Dispose()
        End Select
    End Sub

End Class