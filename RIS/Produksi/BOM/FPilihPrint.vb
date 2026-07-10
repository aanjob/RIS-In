Public Class FPilihPrint

    Private Sub FPilihPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        MainModule.PilihPDok = ""
        Me.CBOPrint.EditValue = "SPK"
    End Sub
    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihPDok = Me.CBOPrint.EditValue
        Me.Dispose()
    End Sub
End Class