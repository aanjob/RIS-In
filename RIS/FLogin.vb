Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices

Public Class FLogin
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Versi As String

    Private Sub FLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TBUser.Focus()
    End Sub

    Private Sub TBPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBPassword.KeyPress
        Dim AsciiCode As Integer = Asc(e.KeyChar)
        Select Case AsciiCode
            Case 13
                TcodeCollection.Clear()

                Dim Reader As SqlClient.SqlDataReader
                Dim cmd As SqlCommand
                cmd = New SqlCommand("Select UserID,[dbo].[fcDecrypt](LoginID) as LoginID,Inisial,PosisiID,Versi,NoHarga,BackDate,Alert,AlertJual, AlertApp,AlertReqC,AlertSpec,AlertBOM,AlertPO,AlertSampR,AlertPass,AlertBSTB,AlertBOMstsPO,AlertTrmPO,AlertSJKBB,AlertBayar,AlertBlmLns,Proses,Line,GdID,FormDef,SU From M_User Where [dbo].[fcDecrypt](LoginID)='" & Me.TBUser.Text & "' and [dbo].[fcDecrypt](Passwordd)= '" & Me.TBPassword.Text & "' and Aktif = 'True'")
                cmd.Connection = koneksi

                With koneksi
                    .Open()
                    Reader = cmd.ExecuteReader
                    If Reader.HasRows Then
                        While Reader.Read
                            MainModule.UserAktif = Reader.Item(0)
                            MainModule.LoginAktif = Reader.Item(1)
                            MainModule.InisialAktif = Reader.Item(2)
                            MainModule.Posisi = Reader.Item(3)
                            Versi = Reader.Item(4)
                            MainModule.NoHarga = Reader.Item(5)
                            MainModule.BackDate = Reader.Item(6)
                            MainModule.stsAlert = Reader.Item(7)
                            MainModule.stsAlertJual = Reader.Item(8)
                            MainModule.stsAlertApp = Reader.Item(9)
                            MainModule.stsAlertReqC = Reader.Item(10)
                            MainModule.stsAlertSpec = Reader.Item(11)
                            MainModule.stsAlertBOM = Reader.Item(12)
                            MainModule.stsAlertPO = Reader.Item(13)
                            MainModule.stsAlertSampR = Reader.Item(14)
                            MainModule.stsAlertPass = Reader.Item(15)
                            MainModule.stsAlertBSTB = Reader.Item(16)
                            MainModule.stsAlertBOMstsPO = Reader.Item(17)
                            MainModule.stsAlertTrmPO = Reader.Item(18)
                            MainModule.stsAlertSJKBB = Reader.Item(19)
                            MainModule.stsAlertBayar = Reader.Item(20)
                            MainModule.stsAlertBlmLns = Reader.Item(21)
                            MainModule.ProsesAktif = Reader.Item(22)
                            MainModule.LineAktif = Reader.Item(23)
                            MainModule.GdAktif = Reader.Item(24)
                            MainModule.FormDef = Reader.Item(25)
                            MainModule.SU = Reader.Item(26)
                            MainModule.PassAktif = Me.TBPassword.EditValue
                        End While
                    Else
                        XtraMessageBox.Show("User Tidak Aktif Atau Tidak Terdaftar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Reader.Close()
                        koneksi.Close()
                        Exit Sub
                    End If
                End With
                Reader.Close()
                koneksi.Close()

                If Me.TBUser.EditValue = Me.TBPassword.EditValue Then
                    XtraMessageBox.Show("Please Change Your Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                If Versi <> Version Then
                    XtraMessageBox.Show("Program Anda Belum Diupdate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'TcodeCollection.Clear()
                    FUtama.Close()
                    Exit Sub
                End If



                'If SlReminder(MainModule.stsAlert) > 0 Then
                '    Dim x : For x = 0 To 100
                '        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                '    Next

                '    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                '    XtraMessageBox.Show("Cek Jadwal Kedatangan Bahan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If

                Me.Dispose()
        End Select
    End Sub

End Class