Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Public Class FTransBal
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim x As Integer


    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim jml, jml2 As Integer

        Dim command As New SqlCommand("Select dbo.fcCekDocBB(" & MainModule.periodAktif & ")", koneksi)

        With koneksi
            .Open()
            command.CommandTimeout = 9000
            jml = command.ExecuteScalar()
            .Close()
        End With


        Dim command2 As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

        With koneksi
            .Open()
            command2.CommandTimeout = 9000
            jml2 = command2.ExecuteScalar()
            .Close()
        End With

        If jml + jml2 > 0 Then
            XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.CBOTrans.EditValue = "" Then
            XtraMessageBox.Show("Jenis Transfer Belum Dipilih", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If XtraMessageBox.Show("Apakah Anda Mau Melakukan Proses Transfer Balance " & Me.CBOTrans.EditValue & "dari Bulan " & MonthName(MainModule.periodeBulan) & " Tahun " & MainModule.periodeTahun & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                koneksi.Close()
                BackgroundWorker1.RunWorkerAsync()
            End If
        End If

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        If Me.CBOTrans.EditValue = "Saldo Bahan" Then
            Dim cmSP As New SqlCommand("SPTransBalStokBB")
            cmSP.CommandType = CommandType.StoredProcedure

            With cmSP
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
                .CommandTimeout = 900000

                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With
            End With

            Dim cmSP2 As New SqlCommand("SPTransBalStokBBBtNum")
            cmSP2.CommandType = CommandType.StoredProcedure

            With cmSP2
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
                .CommandTimeout = 900000

                With koneksi
                    .Open()
                    cmSP2.ExecuteNonQuery()
                    x = cmSP2.Parameters("@Return").Value
                    .Close()
                End With
            End With

        ElseIf Me.CBOTrans.EditValue = "Saldo Barang" Then
            Dim cmSP As New SqlCommand("SPTransBalStokBJ")
            cmSP.CommandType = CommandType.StoredProcedure

            With cmSP
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
                .CommandTimeout = 900000

                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With
            End With
        ElseIf Me.CBOTrans.EditValue = "Saldo Hutang" Then
            Dim cmSP As New SqlCommand("SPTransBalHut")
            cmSP.CommandType = CommandType.StoredProcedure

            With cmSP
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
                .CommandTimeout = 900000

                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With
            End With

        ElseIf Me.CBOTrans.EditValue = "Saldo Piutang" Then
            Dim cmSP As New SqlCommand("SPTransBalPiut")
            cmSP.CommandType = CommandType.StoredProcedure

            With cmSP
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
                .CommandTimeout = 900000

                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    .Close()
                End With
            End With

        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If x = 0 Then
            XtraMessageBox.Show("Trans Balance " & Me.CBOTrans.EditValue & " Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            XtraMessageBox.Show("Trans Balance " & Me.CBOTrans.EditValue & " Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub
End Class