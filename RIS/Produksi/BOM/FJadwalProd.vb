Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Public Class FJadwalProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillBOM(Period As Integer)
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select BOMID,SPK,Tanggal,ArtName,Warna,TglAwal,TglAkhir,KetProd From T_BOM Where PeriodID=" & Period & "", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOM")
        Try
            DsMaster.Tables("T_BOM").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOM")

        DsMaster.Tables("T_BOM").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOM").Columns("BOMID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BOM"
    End Sub

    Private Sub FJadwalProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,DATENAME(month,TglAwal) as Bulan,Bulan As Bulan1,Tahun From M_Period Order By TglAwal Asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_PeriodL2")
        Try
            DsMaster.Tables("M_PeriodL2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_PeriodL2")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("M_PeriodL2")
        Me.SLUPeriodID.Properties.DisplayMember = "PeriodID"
        Me.SLUPeriodID.Properties.ValueMember = "PeriodID"

        Me.SLUPeriodID.EditValue = periodAktif

        Me.TBTahun.EditValue = periodeTahun
        Me.TBBulan.EditValue = MonthName(periodeBulan)

        FillBOM(Me.SLUPeriodID.EditValue)
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Dim x As Integer

        Dim i : For i = 0 To Me.BandedGridView1.RowCount - 1
            If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "TglAwal")) Then
                Dim cmSPDtl As New SqlCommand("SPUpT_BOM2")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "BOMID")
                    .Parameters.Add("@TglAwal", SqlDbType.Date).Value = Me.BandedGridView1.GetRowCellValue(i, "TglAwal")
                    .Parameters.Add("@TglAkhir", SqlDbType.Date).Value = Me.BandedGridView1.GetRowCellValue(i, "TglAkhir")
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "KetProd")
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With

                With koneksi
                    .Open()
                    cmSPDtl.ExecuteNonQuery()
                    x = cmSPDtl.Parameters("@Return").Value
                    .Close()
                End With
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf x = 1 Then
            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub SLUPeriodID_Leave(sender As Object, e As EventArgs) Handles SLUPeriodID.Leave
        FillBOM(Me.SLUPeriodID.EditValue)

        Me.TBBulan.EditValue = DsMaster.Tables("M_PeriodL2").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Bulan")
        Me.TBTahun.EditValue = DsMaster.Tables("M_PeriodL2").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("Tahun")
    End Sub

    Private Sub BandedGridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles BandedGridView1.CellValueChanged
        If e.Column Is BandedGridView1.Columns("TglAwal") Then
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "TglAkhir", Me.BandedGridView1.GetRowCellValue(e.RowHandle, "TglAwal"))
        End If
    End Sub
End Class