Imports System.Data.SqlClient

Public Class FPilihBulan
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Private Sub FPilihBulan_Load(sender As Object, e As EventArgs) Handles Me.Load
        For i As Integer = 1 To 12
            Me.CBOBulan1.Properties.Items.Add(MonthName(i))
            Me.CBOBulan2.Properties.Items.Add(MonthName(i))
            Me.CBOBulan3.Properties.Items.Add(MonthName(i))
            Me.CBOBulan4.Properties.Items.Add(MonthName(i))
        Next

        'DsMaster = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol In ('Bahan','Sparepart-Mesin') and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGudang.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGudang.Properties.DisplayMember = "Nama"
        Me.SLUGudang.Properties.ValueMember = "GdID"
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click

        MainModule.B1 = Me.CBOBulan1.SelectedIndex + 1
        MainModule.B2 = Me.CBOBulan2.SelectedIndex + 1
        MainModule.B3 = Me.CBOBulan3.SelectedIndex + 1
        MainModule.B4 = Me.CBOBulan4.SelectedIndex + 1

        MainModule.NmB1 = Me.CBOBulan1.EditValue
        MainModule.NmB2 = Me.CBOBulan2.EditValue
        MainModule.NmB3 = Me.CBOBulan3.EditValue
        MainModule.NmB4 = Me.CBOBulan4.EditValue



        MainModule.PilihGudangID = Me.SLUGudang.EditValue
        MainModule.PilihGudang = Me.SLUGudang.Text

        Me.Dispose()
    End Sub
End Class