Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FUpKet
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Menu, Kode, Ket As String

    Public Sub New(ByVal Form As String, ByVal ID As String, Keterangan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Menu = Form
        Kode = ID
        Ket = Keterangan

        Me.Text = ".:" & Menu & ":."
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FUpKet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MKet.EditValue = Ket
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        If Me.MKet.EditValue = "" Then
            XtraMessageBox.Show("Keterangan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Menu = "Nota Pesanan" Then
            Dim cmSP As New SqlCommand("SPUpT_NotPesKet")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                        XtraMessageBox.Show("Keterangan Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        XtraMessageBox.Show("Keterangan Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Keterangan Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End With

        ElseIf Menu = "Penjualan Barang Jadi" Then

            Dim cmSP As New SqlCommand("SPUpT_JualBJKet")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                        XtraMessageBox.Show("Keterangan Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        XtraMessageBox.Show("Keterangan Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Keterangan Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End With

        ElseIf Menu = "Delivery Order Barang Jadi" Then

            Dim cmSP As New SqlCommand("SPUpT_DOKet")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                        XtraMessageBox.Show("Keterangan Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        XtraMessageBox.Show("Keterangan Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Keterangan Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End With
        End If
        Me.Dispose()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub
End Class