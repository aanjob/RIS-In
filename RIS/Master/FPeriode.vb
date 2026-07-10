Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPeriode
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim x As Integer

    Sub CekAkses()
        Me.BProses.Enabled = CType(TcodeCollection.Item("PerP"), Boolean)
        Me.BVBClose.Enabled = CType(TcodeCollection.Item("PerClose"), Boolean)
        Me.BVBOpen.Enabled = CType(TcodeCollection.Item("PerOpen"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
    End Sub

    Private Sub OpenControl()
        Me.BVBClose.Enabled = False
        Me.BVBOpen.Enabled = False
        Me.BVBExit.Enabled = False
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PeriodID,DATENAME(month,TglAwal) as Bulan,Tahun,TglAwal,TglAkhir,stsClose,InsDate,InsBy,ClsDate,ClsBy From M_Period Order By TglAwal asc", koneksi)

        cmsl.TableMappings.Add("Table", "M_Period")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_Period")
        DsMaster.Tables("M_Period").Clear()
        cmsl.Fill(DsMaster, "M_Period")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Period"
    End Sub

    Public Sub TransBal(ByVal Period As Integer)
        Dim cmSP As New SqlCommand("SPTransBalStokBB")
        cmSP.CommandType = CommandType.StoredProcedure

        With cmSP
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Period
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

        If x <> 0 Then
            XtraMessageBox.Show("Proses Transfer Balance Bahan Gagal Dilakukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP5 As New SqlCommand("SPTransBalStokBBBtNum")
        cmSP5.CommandType = CommandType.StoredProcedure

        With cmSP5
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Period
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP5.ExecuteNonQuery()
                x = cmSP5.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x <> 0 Then
            XtraMessageBox.Show("Proses Transfer Balance Bahan Gagal Dilakukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP2 As New SqlCommand("SPTransBalStokBJ")
        cmSP2.CommandType = CommandType.StoredProcedure

        With cmSP2
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Period
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

        If x <> 0 Then
            XtraMessageBox.Show("Proses Transfer Balance Barang Jadi Gagal Dilakukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP3 As New SqlCommand("SPTransBalHut")
        cmSP3.CommandType = CommandType.StoredProcedure

        With cmSP3
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Period
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP3.ExecuteNonQuery()
                x = cmSP3.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x <> 0 Then
            XtraMessageBox.Show("Proses Transfer Balance Hutang Gagal Dilakukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP4 As New SqlCommand("SPTransBalPiut")
        cmSP4.CommandType = CommandType.StoredProcedure

        With cmSP4
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = Period
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP4.ExecuteNonQuery()
                x = cmSP4.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x <> 0 Then
            XtraMessageBox.Show("Proses Transfer Balance Piutang Gagal Dilakukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub FPeriode_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Periode"
    End Sub

    Private Sub FPeriode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()

        Me.SPTahun.EditValue = System.DateTime.Now.Year
        FillDt()
    End Sub

    Private Sub BProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BProses.Click
        koneksi.Close()

        Dim x As Integer

        Dim i : For i = 1 To 12
            Dim cmSP As New SqlCommand("SPInsM_Period")
            cmSP.CommandType = CommandType.StoredProcedure

            With cmSP
                .Parameters.Add("@Bulan", SqlDbType.Int).Value = i
                .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SPTahun.EditValue
                .Parameters.Add("@Awal", SqlDbType.DateTime).Value = New Date(Me.SPTahun.EditValue, i, 1, 0, 0, 1)
                .Parameters.Add("@Akhir", SqlDbType.DateTime).Value = New Date(Me.SPTahun.EditValue, i, Date.DaysInMonth(Me.SPTahun.EditValue, i), 23, 59, 59)
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End With
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            FillDt()
        Else
            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub BVBClose_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBClose.ItemClick
        koneksi.Close()

        If MainModule.SlRekalBB2(Me.GridView1.GetFocusedDataRow.Item("PeriodID")) = 0 Then
            XtraMessageBox.Show("Data Belum Direkalkulasi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        TransBal(Me.GridView1.GetFocusedDataRow.Item("PeriodID"))

        Dim jml As Integer = 0
        Dim jml2 As Integer = 0
        Dim jml3 As Integer = 0

        If MainModule.PrintDt = False Then
            Dim command As New SqlCommand("Select dbo.fcCekDocBB(" & Me.GridView1.GetFocusedDataRow.Item("PeriodID") & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With

            Dim command2 As New SqlCommand("Select dbo.fcCekDocBJ(" & Me.GridView1.GetFocusedDataRow.Item("PeriodID") & ")", koneksi)

            With koneksi
                .Open()
                command2.CommandTimeout = 9000
                jml2 = command2.ExecuteScalar()
                .Close()
            End With

        End If

        Dim command3 As New SqlCommand("Select dbo.fcCekDoc(" & Me.GridView1.GetFocusedDataRow.Item("PeriodID") & ")", koneksi)

        With koneksi
            .Open()
            command3.CommandTimeout = 9000
            jml3 = command3.ExecuteScalar()
            .Close()
        End With

        If jml + jml2 + jml3 > 0 Then
            XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim x As Integer

        Dim cmSP As New SqlCommand("SPSetstsPeriod")
        cmSP.CommandType = CommandType.StoredProcedure

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.Int).Value = Me.GridView1.GetFocusedDataRow.Item("PeriodID")
            .Parameters.Add("@sts", SqlDbType.Bit).Value = True
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                    XtraMessageBox.Show("Periode Berhasil Di Close", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    FillDt()
                Else
                    XtraMessageBox.Show("Periode Gagal Di Close", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Periode Gagal Di Close", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With
    End Sub

    Private Sub BVBOpen_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBOpen.ItemClick
        koneksi.Close()

        Dim x As Integer

        Dim cmSP As New SqlCommand("SPSetstsPeriod")
        cmSP.CommandType = CommandType.StoredProcedure

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.Int).Value = Me.GridView1.GetFocusedDataRow.Item("PeriodID")
            .Parameters.Add("@sts", SqlDbType.Bit).Value = False
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                    XtraMessageBox.Show("Periode Berhasil Di Open", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    FillDt()
                Else
                    XtraMessageBox.Show("Periode Gagal Di Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Periode Gagal Di Open", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

End Class