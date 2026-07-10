Public Class FPilihUkuran
    Private Sub FPilihUkuran_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.CBOPilihUk.EditValue = "1/2 Halaman"
    End Sub

    Private Sub BOk_Click(sender As Object, e As EventArgs) Handles BOk.Click
        MainModule.PilihUkuran = Me.CBOPilihUk.EditValue
        MainModule.PilihPrint = Me.CBOJenis.EditValue
        Me.Dispose()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        MainModule.PilihUkuran = ""
        Me.Dispose()
    End Sub

End Class