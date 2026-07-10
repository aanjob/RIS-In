Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHslProdTKL
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HProdTKLN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTTKL_s.Enabled = True


        Me.DTPTanggal.Properties.ReadOnly = True

        Me.BandedGridView1.OptionsBehavior.Editable = False

        Me.BProses.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTKL_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.BandedGridView1.OptionsBehavior.Editable = True

        Me.BProses.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTKL_e.Selected = True
    End Sub


    Public Sub FillDtl(Tanggal As Date)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TKLIDD,Tanggal,Proses,Line,Shiift,TKN,JamN,TKL,JamL From T_HslProdTKLDtl Where Tanggal='" & Tanggal & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HslProdTKLDtl")
        Try
            DsMaster.Tables("T_HslProdTKLDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HslProdTKLDtl")

        DsMaster.Tables("T_HslProdTKLDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HslProdTKLDtl").Columns("Tanggal"), DsMaster.Tables("T_HslProdTKLDtl").Columns("Proses"), DsMaster.Tables("T_HslProdTKLDtl").Columns("Line"), DsMaster.Tables("T_HslProdTKLDtl").Columns("Shiift")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HslProdTKLDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tanggal,PeriodID,InsDate,InsBy,UpdDate,UpdBy From T_HslProdTKL Where PeriodID=" & MainModule.periodAktif & "", koneksi)

        cmsl.TableMappings.Add("Table", "T_HslProdTKL")

        cmsl.Fill(DsMaster, "T_HslProdTKL")
        DsMaster.Tables("T_HslProdTKL").Clear()
        cmsl.Fill(DsMaster, "T_HslProdTKL")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_HslProdTKL"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_HslProdTKL")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Tgl", SqlDbType.VarChar).Value = Me.DTPTanggal.EditValue
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
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub FHslProdTKL_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Tenaga Kerja Langsung"
    End Sub

    Private Sub FHslProdTKL_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FHslProdTKL_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FHslProdTKL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTKL_e.Selected = True
    End Sub

    Private Sub BVTTKL_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTKL_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Tenaga Kerja Langsung"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HProdTKLEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HProdTKLDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Tenaga Kerja Langsung"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        CekSave = True

        Indicator = "100"

        Me.TBInfo.EditValue = ""

        FillDtl(Me.DTPTanggal.EditValue)
        DsMaster.Tables("T_HslProdTKLDtl").Clear()
        ReDim arrPar(-1)

        Me.BandedGridView1.OptionsBehavior.Editable = False
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Tenaga Kerja Langsung"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Indicator = "200"
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")

        FillDtl(Me.DTPTanggal.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True
        Me.BProses.Enabled = False
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Tenaga Kerja Langsung"

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("Tanggal") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_HslProdTKL")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
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
                    Else
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.BandedGridView1.ActiveFilter.Clear()

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_HslProdTKL")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.DTPTanggal.EditValue = cmSP.Parameters("@Tgl").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To BandedGridView1.RowCount - 1
                            If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "TKN")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HslProdTKLDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Proses")
                                    .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Line")
                                    .Parameters.Add("@Shiift", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Shiift")
                                    .Parameters.Add("@TKN", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "TKN")
                                    .Parameters.Add("@JamN", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "JamN")
                                    .Parameters.Add("@TKL", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "TKL")
                                    .Parameters.Add("@JamL", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "JamL")
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

                                If x = 0 Then

                                ElseIf x = -2 Then
                                    Del()
                                    XtraMessageBox.Show("TKL Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                Else
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.DTPTanggal.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            Del()
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_HslProdTKL")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_HslProdTKLDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tgl", SqlDbType.VarChar).Value = Me.DTPTanggal.EditValue
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi

                                With koneksi
                                    .Open()
                                    cmSPDel.ExecuteNonQuery()
                                    .Close()
                                End With

                            End With
                        Next

                        Dim i : For i = 0 To BandedGridView1.RowCount - 1
                            If Me.BandedGridView1.GetRowCellValue(i, "TKLIDD") < 0 Then
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "TKN")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_HslProdTKLDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Proses")
                                        .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Line")
                                        .Parameters.Add("@Shiift", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Shiift")
                                        .Parameters.Add("@TKN", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "TKN")
                                        .Parameters.Add("@JamN", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "JamN")
                                        .Parameters.Add("@TKL", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "TKL")
                                        .Parameters.Add("@JamL", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "JamL")
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

                                    If x = 0 Then
                                        Me.BandedGridView1.SetRowCellValue(i, "TKLIDD", Me.BandedGridView1.GetRowCellValue(i, "TKLIDD") * -1)
                                    ElseIf x = -2 Then
                                        XtraMessageBox.Show("TKL Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(i, "TKN")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_HslProdTKLDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "TKLIDD")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Proses")
                                        .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Line")
                                        .Parameters.Add("@Shiift", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(i, "Shiift")
                                        .Parameters.Add("@TKN", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "TKN")
                                        .Parameters.Add("@JamN", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "JamN")
                                        .Parameters.Add("@TKL", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(i, "TKL")
                                        .Parameters.Add("@JamL", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(i, "JamL")
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

                                    If x = 0 Then

                                    ElseIf x = -2 Then
                                        XtraMessageBox.Show("TKL Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
        End Select

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FHslProdTKL_d(Me.GridView2.GetFocusedDataRow.Item("Tanggal"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.BandedGridView1.GetFocusedDataRow.Item("TKLIDD")
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click

        If SlCek("T_HslProdTKL", "Count(*)", "Tanggal", Me.DTPTanggal.EditValue) > 0 Then
            Me.BandedGridView1.OptionsBehavior.Editable = False

            XtraMessageBox.Show("Tanggal Sudah Pernah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.BandedGridView1.OptionsBehavior.Editable = True

        DsMaster.Tables("T_HslProdTKLDtl").Clear()

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY Proses)*-1 as TKLIDD,* From(Select Distinct '" & Me.DTPTanggal.EditValue & "' as Tanggal,Proses,Line,1 As Shiift,0 as TKN,7 As JamN,0 as TKL,0 as JamL From T_HslProd Where Tanggal='" & Me.DTPTanggal.EditValue & "') as x Order By Proses,Line", koneksi)
        'cmsl = New SqlDataAdapter("Select Distinct ROW_NUMBER() over (ORDER BY D.Proses)*-1 as TKLIDD,'" & Me.DTPTanggal.EditValue & "' as Tanggal,Proses,Line,1 As Shiift,0 as TKN, 0 As JamN,0 as TKL,0 as JamL From T_HslProd Where Tanggal='" & Me.DTPTanggal.EditValue & "' and Proses+Line Not In (Select Proses+Line From T_HslProdTKL where Tanggal='" & Me.DTPTanggal.EditValue & "')", koneksi)
        cmsl.Fill(DsMaster, "T_HslProdTKLDtl")

    End Sub

    Private Sub BandedGridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles BandedGridView1.InitNewRow
        Try
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "TKLIDD", Me.BandedGridView1.RowCount * -1)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Proses", "")
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Line", "")
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Shiift", "")
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "TKN", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JamN", 7)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "TKL", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JamL", 0)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdProses_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdProses.ButtonClick
        Try
            Dim frm As New FSearch("ProsesLine", "", "", "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                BandedGridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Line", dataTrans.Item("Line").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub


End Class