Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FSetPeriode
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Private Sub FSetPeriode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If CekSave = True Then
        '    Me.BSet.Enabled = False
        'End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,DATENAME(month,TglAwal) as Bulan,Bulan As Bulan1,Tahun From M_Period Order By TglAwal Asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_PeriodL2")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_PeriodL2")
        DsMaster.Tables("M_PeriodL2").Clear()
        cmsl.Fill(DsMaster, "M_PeriodL2")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("M_PeriodL2")
        Me.SLUPeriodID.Properties.DisplayMember = "PeriodID"
        Me.SLUPeriodID.Properties.ValueMember = "PeriodID"

        Me.SLUPeriodID.EditValue = periodAktif

        Me.TBTahun.EditValue = periodeTahun
        Me.TBBulan.EditValue = MonthName(periodeBulan)
    End Sub

    Private Sub BSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BSet.Click
        Try
            MainModule.periodAktif = Me.SLUPeriodID.EditValue
            MainModule.periodeTahun = Me.TBTahun.EditValue
            MainModule.periodeBulan = DsMaster.Tables("M_PeriodL2").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan1")
            TulisSettingPeriode()
            XtraMessageBox.Show("Periode Berhasil Disetting", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.Dispose()
        Catch ex As Exception
            XtraMessageBox.Show("Periode Gagal Disetting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub SLUPeriodID_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles SLUPeriodID.Leave
        Me.TBBulan.EditValue = DsMaster.Tables("M_PeriodL2").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan")
        Me.TBTahun.EditValue = DsMaster.Tables("M_PeriodL2").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Tahun")
    End Sub
End Class