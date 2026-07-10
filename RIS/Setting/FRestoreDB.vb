Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FRestoreDB

    Private Sub BSearchDB_Click(sender As Object, e As EventArgs) Handles BSearchDB.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Backup Files|*.bak"
        OpenFileDialog1.FilterIndex = 1

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.TBPath.EditValue = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub BSearchLoc_Click(sender As Object, e As EventArgs) Handles BSearchLoc.Click
        Dim dialog = New FolderBrowserDialog()
        dialog.SelectedPath = Application.StartupPath

        If DialogResult.OK = dialog.ShowDialog() Then
            Me.TBPathSave.EditValue = dialog.SelectedPath
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        'Dim _DISK As String = fileName
        Try
            With New SqlCommand()
                .Connection = New SqlConnection(GlobalKoneksi)
                .CommandTimeout = 7000
                .CommandType = CommandType.Text
                .CommandText = String.Format("RESTORE DATABASE [" & Me.TBNamaDB.EditValue & "] ", New String() {"Null"})
                .CommandText &= String.Format("FROM  DISK = N'{0}' ", New String() {Me.TBPath.EditValue})
                .CommandText &= String.Format("WITH  FILE = 1,  ", New String() {"Null"})
                .CommandText &= String.Format("MOVE N'PayrollStaff' ", New String() {MainModule.namaDB})
                .CommandText &= String.Format("TO N'{0}',  ", New String() {Me.TBPathSave.EditValue & "\" & Me.TBNamaDB.EditValue & ".mdf"})
                .CommandText &= String.Format("MOVE N'PayrollStaff_log' ", New String() {"Null"})
                .CommandText &= String.Format("TO N'{0}',  ", New String() {Me.TBPathSave.EditValue & "\" & Me.TBNamaDB.EditValue & "_log.ldf"})
                .CommandText &= String.Format("NOUNLOAD,  REPLACE,  STATS = 10 ", New String() {"Null"})
                'Console.WriteLine(.CommandText)
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
            End With

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If Me.CEDeleteDB.EditValue = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("use master alter database " & MainModule.namaDB & " set single_user with rollback immediate Drop Database " & MainModule.namaDB & "", koneksi)

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                .Close()
            End With
        End If

        XtraMessageBox.Show("Proses Restore Database Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)


    End Sub
End Class