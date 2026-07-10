Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKoneksi
    Private Sub FKoneksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bacaSettingKoneksiDdns()

        Me.TBServerDdns.EditValue = namaServer
        Me.TBPortDdns.EditValue = port
        Me.TBDBDdns.EditValue = namaDB
        Me.TBUserDdns.EditValue = namaUser
        Me.TBPassDdns.EditValue = namaPass

        bacaSettingKoneksi()

        Me.TBServer.EditValue = namaServer
        Me.TBPort.EditValue = port
        Me.TBDB.EditValue = namaDB
        Me.TBUser.EditValue = namaUser
        Me.TBPass.EditValue = namaPass

        Me.TBServerSink.EditValue = namaServerSink
        Me.TBPortSink.EditValue = portSink
        Me.TBDBSink.EditValue = namaDBSink
        Me.TBUserSink.EditValue = namaUserSink
        Me.TBPassSink.EditValue = namaPassSink
    End Sub

    Private Sub BTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTest.Click
        Try
            Dim koneksi As New SqlConnection("Data Source=" & Me.TBServer.EditValue & "," & Me.TBPort.EditValue & ";Initial Catalog=" & Me.TBDB.EditValue & ";Persist Security Info=True;User ID=" & Me.TBUser.EditValue & ";Password=" & Me.TBPass.EditValue & ";Connect Timeout=100")
            koneksi.Open()
            koneksi.Close()
            XtraMessageBox.Show("Sukses", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BSave.Click
        Try
            namaServer = Me.TBServer.EditValue
            port = Me.TBPort.EditValue
            namaDB = Me.TBDB.EditValue
            namaUser = Me.TBUser.EditValue
            namaPass = Me.TBPass.EditValue
            TulisSettingKoneksi()
            GlobalKoneksi = "Data Source=" & namaServer & "," & port & ";Initial Catalog=" & namaDB & ";Persist Security Info=True;User ID=" & namaUser & ";Password=" & namaPass & ";Connect Timeout=9000"

            XtraMessageBox.Show("Setting Database Sukses", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            FUtama.BSIServer.Caption = MainModule.namaServer.Substring(MainModule.namaServer.Length - 4, 4)
            FUtama.BSIPort.Caption = MainModule.port
            FUtama.BSIDB.Caption = MainModule.namaDB

            Me.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub BTestSink_Click(sender As Object, e As EventArgs) Handles BTestSink.Click
        Try
            Dim koneksi As New SqlConnection("Data Source=" & Me.TBServerSink.EditValue & "," & Me.TBPortSink.EditValue & ";Initial Catalog=" & Me.TBDBSink.EditValue & ";Persist Security Info=True;User ID=" & Me.TBUserSink.EditValue & ";Password=" & Me.TBPassSink.EditValue & ";Connect Timeout=100")
            koneksi.Open()
            koneksi.Close()
            XtraMessageBox.Show("Sukses", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BSaveSink_Click(sender As Object, e As EventArgs) Handles BSaveSink.Click
        Try

            namaServerSink = Me.TBServerSink.EditValue
            portSink = Me.TBPortSink.EditValue
            namaDBSink = Me.TBDBSink.EditValue
            namaUserSink = Me.TBUserSink.EditValue
            namaPassSink = Me.TBPassSink.EditValue
            TulisSettingSinkron()

            Dim koneksi As New SqlConnection(GlobalKoneksi)
            Dim command As New SqlCommand("SPCrLinkSvr")
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.Add("@NServer", SqlDbType.VarChar).Value = Me.TBServerSink.EditValue & "," & Me.TBPortSink.EditValue
            command.Parameters.Add("@User", SqlDbType.VarChar).Value = Me.TBUserSink.EditValue
            command.Parameters.Add("@Pass", SqlDbType.VarChar).Value = Me.TBPassSink.EditValue

            With command
                .Connection = koneksi
                With koneksi
                    .Open()
                    command.ExecuteNonQuery()
                    .Close()
                End With
            End With

            XtraMessageBox.Show("Setting Sinkronisasi Sukses", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BCancelSink_Click(sender As Object, e As EventArgs) Handles BCancelSink.Click
        Me.Dispose()
    End Sub

    Private Sub BTestDdns_Click(sender As Object, e As EventArgs) Handles BTestDdns.Click
        Try
            Dim koneksi As New SqlConnection("Data Source=" & Me.TBServerDdns.EditValue & "," & Me.TBPortDdns.EditValue & ";Initial Catalog=" & Me.TBDBDdns.EditValue & ";Persist Security Info=True;User ID=" & Me.TBUserDdns.EditValue & ";Password=" & Me.TBPassDdns.EditValue & ";Connect Timeout=100")
            koneksi.Open()
            koneksi.Close()
            XtraMessageBox.Show("Sukses", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BSaveDdns_Click(sender As Object, e As EventArgs) Handles BSaveDdns.Click
        Try
            namaServer = Me.TBServerDdns.EditValue
            port = Me.TBPortDdns.EditValue
            namaDB = Me.TBDBDdns.EditValue
            namaUser = Me.TBUserDdns.EditValue
            namaPass = Me.TBPassDdns.EditValue
            TulisSettingKoneksiDdns()
            GlobalKoneksi = "Data Source=" & namaServer & "," & port & ";Initial Catalog=" & namaDB & ";Persist Security Info=True;User ID=" & namaUser & ";Password=" & namaPass & ";Connect Timeout=9000"

            XtraMessageBox.Show("Setting Database Sukses", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            FUtama.BSIServer.Caption = MainModule.namaServer.Substring(MainModule.namaServer.Length - 4, 4)
            FUtama.BSIPort.Caption = MainModule.port
            FUtama.BSIDB.Caption = MainModule.namaDB

            Me.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BCancelDdns_Click(sender As Object, e As EventArgs) Handles BCancelDdns.Click
        Me.Dispose()
    End Sub
End Class