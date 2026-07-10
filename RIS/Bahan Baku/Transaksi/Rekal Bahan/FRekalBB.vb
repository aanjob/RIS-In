Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FRekalBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID As String
    Dim Manual, MnlInsUpd, Adj As Boolean
    Dim var As String

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RekalIDD,RekalID,GdID,D.BBID, B.Nama As Bahan,Nilai,HarSat From T_RekalBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where RekalID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_RekalBBDtl")
        Try
            DsMaster.Tables("T_RekalBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_RekalBBDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RekalBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RekalID,PeriodID,Bulan,Tahun,Tanggal,InsDate,InsBy,UpdDate,UpdBy From T_RekalBB Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,RekalID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_RekalBB")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_RekalBB")

        If DsMaster.Tables("T_RekalBB").Rows.Count > 0 Then
            Me.TBKode.EditValue = DsMaster.Tables("T_RekalBB").Rows(0).Item("RekalID")
            Me.TBPeriode.EditValue = MonthName(DsMaster.Tables("T_RekalBB").Rows(0).Item("Bulan")) & " " & DsMaster.Tables("T_RekalBB").Rows(0).Item("Tahun")
            Me.DTPTanggal.EditValue = DsMaster.Tables("T_RekalBB").Rows(0).Item("Tanggal")
        End If
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()


        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=31", koneksi)

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        CodeID = ""
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        CodeID = Reader.Item(1)
                        MnlInsUpd = Reader.Item(2)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBProsesSbOp.Enabled = CType(TcodeCollection.Item("RekalBSO"), Boolean)
        Me.BVBProsesStAdj.Enabled = CType(TcodeCollection.Item("RekalBSA"), Boolean)
        Me.BVBCancelRekal.Enabled = CType(TcodeCollection.Item("RekalBC"), Boolean)
    End Sub


    Public Sub Rekal()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim cmRekI As New SqlCommand("SPRekalBB")
        cmRekI.Parameters.Add("@Period", SqlDbType.Int).Value = MainModule.periodAktif
        cmRekI.CommandType = CommandType.StoredProcedure
        cmRekI.Connection = koneksi
        cmRekI.CommandTimeout = 90000

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter(cmRekI)
        cmsl.TableMappings.Add("Table", "SPRekal")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "SPRekal")

        'Dim i As Integer = 0

        'Do While i <= 5
        '    DsMaster.Tables("SPRekal").Clear()

        '    Dim cmRekI2 As New SqlCommand("SPRekalBB")
        '    cmRekI2.Parameters.Add("@Period", SqlDbType.Int).Value = MainModule.periodAktif
        '    cmRekI2.CommandType = CommandType.StoredProcedure
        '    cmRekI2.Connection = koneksi
        '    cmRekI2.CommandTimeout = 90000

        '    Dim cmsl2 As SqlDataAdapter
        '    cmsl2 = New SqlDataAdapter(cmRekI2)
        '    cmsl2.TableMappings.Add("Table", "SPRekal")
        '    DsMaster = New System.Data.DataSet
        '    cmsl2.Fill(DsMaster, "SPRekal")
        'Loop
    End Sub

    Private Sub FRekalBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Rekalkulasi Bahan Baku"
    End Sub

    Private Sub FRekalBB_Load(sender As Object, e As EventArgs) Handles Me.Load
        FillDt()
        FillDtl(Me.TBKode.EditValue)
        CekAkses()
    End Sub
    Private Sub BVBProsesSbOp_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBProsesSbOp.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Proses Rekalkulasi Bahan Baku Sebelum Opname"
        koneksi.Close()

        If Manual = True Then
            Dim jml, jml2 As Integer

            Dim command As New SqlCommand("Select dbo.fcCekDocBB(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With


            Dim command2 As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command2.CommandTimeout = 9000
                jml2 = command2.ExecuteScalar()
                .Close()
            End With
            'MsgBox(jml)
            'MsgBox(jml2)
            If jml + jml2 > 0 Then
                XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

        End If

        If MainModule.SlstsPeriodNew() = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBB > 0 Then
            XtraMessageBox.Show("Rekalkulasi Sebelum Opname Tidak Bisa Dilakukan Karena Sudah Ada Opname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Melakukan Proses Rekalkulasi Nilai Persediaan Bulan " & MonthName(MainModule.periodeBulan) & " Tahun " & MainModule.periodeTahun & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            koneksi.Close()
            Adj = False

            BackgroundWorker1.RunWorkerAsync()
        End If

    End Sub
    Private Sub BVBProsesStAdj_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBProsesStAdj.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Proses Rekalkulasi Bahan Baku Setelah Adjustment"

        koneksi.Close()

        If MainModule.SlstsPeriodNew() = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Manual = False Then
            If MainModule.SlAdjBB < 0 Then
                XtraMessageBox.Show("Rekalkulasi Setelah Adjustment Tidak Bisa Dilakukan Karena Belum Ada Adjustment", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If MainModule.SlRekalBB > 0 Then
            XtraMessageBox.Show("Rekalkulasi Sudah Dilakukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Manual = True Then
            Dim jml, jml2 As Integer

            Dim command As New SqlCommand("Select dbo.fcCekDocBB(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With


            Dim command2 As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command2.CommandTimeout = 9000
                jml2 = command2.ExecuteScalar()
                .Close()
            End With

            If jml + jml2 > 0 Then
                XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

        End If

        If XtraMessageBox.Show("Apakah Anda Mau Melakukan Proses Rekalkulasi Nilai Persediaan Bulan " & MonthName(MainModule.periodeBulan) & " Tahun " & MainModule.periodeTahun & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            koneksi.Close()
            Adj = True

            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Rekal()
            Rekal()
            Rekal()
            Rekal()
            Rekal()

            If Adj = True Then
                Dim cmSP As New SqlCommand("SPInsT_RekalBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.DateTime).Value = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
                    .Parameters.Add("@Bulan", SqlDbType.Int).Value = MainModule.periodeBulan
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = MainModule.periodeTahun
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 31)
                    .Parameters("@Kode").Direction = ParameterDirection.Output

                    .Connection = koneksi
                    .CommandTimeout = 90000
                End With

                Dim Kd As String
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    x = cmSP.Parameters("@Return").Value
                    Kd = cmSP.Parameters("@Kode").Value
                    .Close()
                End With


                Dim cmCek As SqlDataAdapter
                cmCek = New SqlDataAdapter("Select * From (Select GdID,B.BBID,Sum(Masuk)-Sum(Keluar) As Qty,Sum(NilMasuk)-Sum(NilKeluar) As Nilai,(Select Isnull((Select Top 1 HarSat from T_StokBB Where BBID= B.BBID and PeriodID <=" & MainModule.periodAktif & " Order By Tanggal desc,StokID desc),0)) As HarSat From T_StokBB S Inner Join M_BB B On B.BBID=S.BBID where Tanggal <= '" & New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan)) & "' Group By S.GdID,B.BBID) As x Where Qty=0 and Nilai<>0 ORDER BY GdID,BBID", koneksi)

                'cmCek = New SqlDataAdapter("SELECT T.GdID, T.BBID, T.Tanggal,HarSat, (Select dbo.fcSaldoBB (T.BBID,T.GdID,T.Tanggal,'--')) as Nilai FROM T_StokBB AS T INNER JOIN M_BB AS I ON T.BBID = I.BBID WHERE (T.Tanggal =(SELECT MAX(Tanggal) AS Expr1 FROM T_StokBB AS S WHERE (BBID = T.BBID) AND (GdID = T.GdID) AND (Tanggal <= '" & New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan)) & "'))) AND (T.StokID = (SELECT MAX(StokID) FROM T_StokBB AS S WHERE (BBID = T.BBID) AND (GdID = T.GdID) AND (Tanggal = T.Tanggal) )) AND ((Select dbo.fcStokBB (T.BBID,T.GdID,T.Tanggal,'--')) = 0) AND ((Select dbo.fcSaldoBB (T.BBID,T.GdID,T.Tanggal,'--')) <> 0) AND (T.Tanggal <= '" & New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan)) & "') ORDER BY T.GdID,T.BBID", koneksi)

                cmCek.TableMappings.Add("Table", "CekSalAkhir")
                DsMaster = New System.Data.DataSet
                cmCek.SelectCommand.CommandTimeout = 90000
                cmCek.Fill(DsMaster, "CekSalAkhir")

                Dim y : For y = 0 To DsMaster.Tables("CekSalAkhir").Rows.Count - 1
                    Dim cmSPDtl As New SqlCommand("SPInsT_RekalBBDtl")
                    cmSPDtl.CommandType = CommandType.StoredProcedure

                    With cmSPDtl
                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 31
                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kd
                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = DsMaster.Tables("CekSalAkhir").Rows(y).Item("GdID")
                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
                        .Parameters.Add("@BB", SqlDbType.VarChar).Value = DsMaster.Tables("CekSalAkhir").Rows(y).Item("BBID")
                        .Parameters.Add("@Nilai", SqlDbType.Decimal).Value = DsMaster.Tables("CekSalAkhir").Rows(y).Item("Nilai")
                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = DsMaster.Tables("CekSalAkhir").Rows(y).Item("HarSat")
                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                        .Parameters.Add("@Return", SqlDbType.Int)
                        .Parameters("@Return").Direction = ParameterDirection.Output
                        .Connection = koneksi
                        .CommandTimeout = 90000
                    End With

                    With koneksi
                        .Open()
                        cmSPDtl.ExecuteNonQuery()
                        x = cmSPDtl.Parameters("@Return").Value
                        .Close()
                    End With
                Next

                koneksi.Close()
                Dim cmCekGerak As New SqlCommand("SPCekGerakBB")
                cmCekGerak.Parameters.Add("@Batas", SqlDbType.Int).Value = MainModule.BatasBB
                cmCekGerak.Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                cmCekGerak.Parameters.Add("@Tanggal", SqlDbType.Date).Value = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
                cmCekGerak.CommandType = CommandType.StoredProcedure
                cmCekGerak.Connection = koneksi
                cmCekGerak.CommandTimeout = 90000
                With koneksi
                    .Open()
                    cmCekGerak.ExecuteNonQuery()
                    .Close()
                End With

                'FillDt()
                'FillDtl(Me.TBKode.EditValue)
            End If
            var = "Berhasil"
            'Me.Close()
        Catch ex As Exception
            var = "Gagal"
            XtraMessageBox.Show("Proses Rekalkulasi Nilai Persediaan Item Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            XtraMessageBox.Show(ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        LCIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'If MainModule.SlCekHBhnLPB > 0 Then
        '    XtraMessageBox.Show("Masih Ada Nilai Persediaan yang Belum Cocok. Silakan Diproses Kembali", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If var = "Berhasil" Then
            Dim cmSP As New SqlCommand("SPProsesWIP")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Connection = koneksi
                .CommandTimeout = 90000
            End With

            Dim Kd As String
            With koneksi
                .Open()
                cmSP.ExecuteNonQuery()
                .Close()
            End With

            XtraMessageBox.Show("Proses Rekalkulasi Nilai Persediaan Item Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub BVBCancelRekal_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelRekal.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Pembatalan Rekalkulasi Bahan Baku"

        If MainModule.SlstsPeriodNew() = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Rekalkulasi : " & Me.TBKode.EditValue & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_RekalBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
                        XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()

                        Me.Dispose()
                    Else
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

End Class