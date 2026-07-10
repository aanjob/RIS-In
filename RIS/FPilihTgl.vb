Public Class FPilihTgl
    Private Sub FPilihUkuran_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.DTPTanggal.EditValue = System.DateTime.Now
    End Sub

    Private Sub BOk_Click(sender As Object, e As EventArgs) Handles BOk.Click
        MainModule.PilihTgl = Me.DTPTanggal.EditValue
        Me.Dispose()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        MainModule.PilihTgl = System.DateTime.Now
        Me.Dispose()
    End Sub
End Class