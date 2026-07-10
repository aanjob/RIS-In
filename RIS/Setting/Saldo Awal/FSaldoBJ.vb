Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSaldoBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Path As String
    Dim DsUpload As New System.Data.DataSet

    Private Sub FSaldoBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Upload Saldo Barang Jadi"
    End Sub

    Private Sub FSaldoBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,DATENAME(month,TglAwal) as Bulan,Tahun,TglAkhir From M_Period Order By TglAwal Asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_PeriodL")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_PeriodL")

        Me.SLUPeriodID.Properties.DataSource = DsMaster.Tables("M_PeriodL")
        Me.SLUPeriodID.Properties.DisplayMember = "PeriodID"
        Me.SLUPeriodID.Properties.ValueMember = "PeriodID"

        Me.SLUPeriodID.EditValue = periodAktif
        Me.DTPTanggal.EditValue = DsMaster.Tables("M_PeriodL").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("TglAkhir")

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol <>'Bahan' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGudang.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGudang.Properties.DisplayMember = "ArtName"
        Me.SLUGudang.Properties.ValueMember = "GdID"

        cmsl = New SqlDataAdapter("Select ArtCode,ArtName,SatID,Isi From M_Brg Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSal")
        cmsl.Fill(DsMaster, "M_BrgSal")
        DsMaster.Tables("M_BrgSal").Clear()
        cmsl.Fill(DsMaster, "M_BrgSal")
    End Sub

    Private Sub BUpload_Click(sender As Object, e As EventArgs) Handles BUpload.Click
        Me.OpenFileDialog1.ShowDialog()

        'Try
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
        MyConnection = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + Path + "'; Extended Properties=Excel 8.0;")
        MyCommand = New System.Data.OleDb.OleDbDataAdapter("Select * from [Sheet1$]", MyConnection)
        MyCommand.TableMappings.Add("Table", "Excel")
        DsUpload = New System.Data.DataSet
        MyCommand.Fill(DsUpload)

        DtTrans = New System.Data.DataTable
        DtTrans.Columns.Add("ArtCode")
        DtTrans.Columns.Add("ArtName")
        DtTrans.Columns.Add("SatID")
        DtTrans.Columns.Add("Isi")
        DtTrans.Columns.Add("Qty")
        DtTrans.Columns.Add("Psg")

        Dim i : For i = 0 To DsUpload.Tables("Excel").Rows.Count - 1
            DtTrans.Rows.Add(DsUpload.Tables("Excel").Rows(i).Item("ArtCode"), DsMaster.Tables("M_BrgSal").Select("ArtCode = '" & DsUpload.Tables("Excel").Rows(i).Item("ArtCode") & "'")(0).Item("ArtName"), DsMaster.Tables("M_BrgSal").Select("ArtCode = '" & DsUpload.Tables("Excel").Rows(i).Item("ArtCode") & "'")(0).Item("SatID"), DsMaster.Tables("M_BrgSal").Select("ArtCode = '" & DsUpload.Tables("Excel").Rows(i).Item("ArtCode") & "'")(0).Item("Isi"), DsUpload.Tables("Excel").Rows(i).Item("Qty"), DsUpload.Tables("Excel").Rows(i).Item("Qty") * DsMaster.Tables("M_BrgSal").Select("ArtCode = '" & DsUpload.Tables("Excel").Rows(i).Item("ArtCode") & "'")(0).Item("Isi"))
        Next

        'Dim table = DtTrans
        'table.PrimaryKey = New DataColumn() {table.Columns("ArtCode")}
        DtTrans.PrimaryKey = New DataColumn() {DtTrans.Columns("ArtCode")}

        Me.GridControl1.DataSource = DtTrans

        MyConnection.Close()

        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Dim x As Integer

        Dim cmSP As New SqlCommand("SPDelSaldoBJ")
        cmSP.CommandType = CommandType.StoredProcedure

        With cmSP
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Me.SLUPeriodID.EditValue
            .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGudang.EditValue
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

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then

                Dim cmSPDtl As New SqlCommand("SPInsSaldoBJ")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Me.SLUPeriodID.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGudang.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = DsMaster.Tables("M_PeriodL").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("TglAkhir")
                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                    .Parameters.Add("@Stok", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With
                Try

                    With koneksi
                        .Open()
                        cmSPDtl.ExecuteNonQuery()
                        x = cmSPDtl.Parameters("@Return").Value
                        .Close()
                    End With
                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
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

    Private Sub OpenFileDialog1_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim strm As System.IO.Stream
        strm = OpenFileDialog1.OpenFile()
        Path = OpenFileDialog1.FileName.ToString()
    End Sub

    Private Sub SLUPeriodID_Leave(sender As Object, e As EventArgs) Handles SLUPeriodID.Leave
        Me.DTPTanggal.EditValue = DsMaster.Tables("M_PeriodL").Select("PeriodID = '" & Me.SLUPeriodID.EditValue & "'")(0).Item("TglAkhir")
    End Sub
End Class