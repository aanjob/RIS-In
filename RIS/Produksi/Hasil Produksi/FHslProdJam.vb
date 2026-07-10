Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHslProdJam
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HProdJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HProdJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HProdJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTHslProd_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJam.Properties.ReadOnly = True
        Me.SLUUnit.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BCopy.Enabled = False
        Me.BCopy.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTHslProd_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJam.Properties.ReadOnly = False
        Me.SLUUnit.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTHslProd_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Unit From M_UsUnit Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsUnitLUE")
        Try
            DsMaster.Tables("M_UsUnitLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsUnitLUE")

        Me.SLUUnit.Properties.DataSource = DsMaster.Tables("M_UsUnitLUE")
        Me.SLUUnit.Properties.DisplayMember = "Unit"
        Me.SLUUnit.Properties.ValueMember = "Unit"

    End Sub

    Public Sub FillDtl(Tgl As Date, Jam As Integer, Unit As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HPIDD,D.Tanggal,D.Jam,D.Unit,stsJasa,D.BOMID,D.MerkID,D.JnsID,D.ArtName,D.Warna,D.TotPsg,CuttUpp, CuttBott, Seri,SabUpp,SabIns,JhtKomp,JhtUpp,FinishUpp,Insock,Insole,Outsole,Insertt,Inject,D.Ass,Finish,Pack,Phylon From T_HProdJamDtl D Left Outer Join T_BOM B On D.BOMID=B.BOMID Where D.Tanggal='" & Tgl & "' and Jam=" & Jam & " and D.Unit='" & Unit & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdJamDtl")
        Try
            DsMaster.Tables("T_HProdJamDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HProdJamDtl")

        DsMaster.Tables("T_HProdJamDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdJamDtl").Columns("Tanggal"), DsMaster.Tables("T_HProdJamDtl").Columns("Jam"), DsMaster.Tables("T_HProdJamDtl").Columns("Unit"), DsMaster.Tables("T_HProdJamDtl").Columns("BOMID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HProdJamDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tanggal,Jam,Unit,InsDate,InsBy,UpdDate,UpdBy From T_HProdJam Where Year(Tanggal)=" & MainModule.periodeTahun & " and Month(Tanggal)=" & MainModule.periodeBulan & " and Unit In (Select Unit From M_UsUnit Where UserID=" & MainModule.UserAktif & ") Order By Tanggal", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdJam")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_HProdJam")
        DsMaster.Tables("T_HProdJam").Clear()
        cmsl.Fill(DsMaster, "T_HProdJam")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_HProdJam"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_HProdJam")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
            .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
            .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
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
    End Sub

    Public Sub Print()
        MainModule.PilihAwal = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        MainModule.PilihAkhir = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        MainModule.PilihUnit = Me.GridView2.GetFocusedDataRow.Item("Unit")

        If Me.GridView2.GetFocusedDataRow.Item("Unit") = "1" Or Me.GridView2.GetFocusedDataRow.Item("Unit") = "2" Then
            Dim XR As New XRLapProd1
            XR.InitializeData()
        ElseIf Me.GridView2.GetFocusedDataRow.Item("Unit") = "3" Then
            Dim XR As New XRLapProd3
            XR.InitializeData()
        End If

    End Sub

    Public Sub CekKolom()
        If Me.SLUUnit.EditValue = "1" Or Me.SLUUnit.EditValue = "2" Then
            Me.GridView1.Columns("CuttUpp").VisibleIndex = 5
            Me.GridView1.Columns("CuttBott").VisibleIndex = 6
            Me.GridView1.Columns("Seri").VisibleIndex = 7
            Me.GridView1.Columns("SabUpp").VisibleIndex = 8
            Me.GridView1.Columns("SabIns").VisibleIndex = 9
            Me.GridView1.Columns("JhtKomp").VisibleIndex = 10
            Me.GridView1.Columns("JhtUpp").VisibleIndex = 11
            Me.GridView1.Columns("FinishUpp").VisibleIndex = 12
            Me.GridView1.Columns("Insertt").VisibleIndex = 13
            Me.GridView1.Columns("Inject").VisibleIndex = 14
            Me.GridView1.Columns("Phylon").VisibleIndex = 15
            Me.GridView1.Columns("Insock").VisibleIndex = 16
            Me.GridView1.Columns("Insole").VisibleIndex = 17
            Me.GridView1.Columns("Outsole").VisibleIndex = 18
            Me.GridView1.Columns("Ass").VisibleIndex = 19
            Me.GridView1.Columns("Finish").VisibleIndex = 20
            Me.GridView1.Columns("Pack").VisibleIndex = 21

            Me.GridView1.Columns("CuttUpp").Visible = True
            Me.GridView1.Columns("CuttBott").Visible = True
            Me.GridView1.Columns("Seri").Visible = True
            Me.GridView1.Columns("SabUpp").Visible = True
            Me.GridView1.Columns("SabIns").Visible = True
            Me.GridView1.Columns("JhtKomp").Visible = True
            Me.GridView1.Columns("JhtUpp").Visible = True
            Me.GridView1.Columns("FinishUpp").Visible = False
            Me.GridView1.Columns("Insock").Visible = False
            Me.GridView1.Columns("Insole").Visible = False
            Me.GridView1.Columns("Outsole").Visible = True
            Me.GridView1.Columns("Insertt").Visible = False
            Me.GridView1.Columns("Inject").Visible = False
            Me.GridView1.Columns("Ass").Visible = True
            Me.GridView1.Columns("Finish").Visible = False
            Me.GridView1.Columns("Pack").Visible = True
            Me.GridView1.Columns("Phylon").Visible = False

        ElseIf Me.SLUUnit.EditValue = "3" Then
            Me.GridView1.Columns("CuttUpp").VisibleIndex = 5
            Me.GridView1.Columns("CuttBott").VisibleIndex = 6
            Me.GridView1.Columns("Seri").VisibleIndex = 7
            Me.GridView1.Columns("SabUpp").VisibleIndex = 8
            Me.GridView1.Columns("SabIns").VisibleIndex = 9
            Me.GridView1.Columns("JhtKomp").VisibleIndex = 10
            Me.GridView1.Columns("JhtUpp").VisibleIndex = 11
            Me.GridView1.Columns("FinishUpp").VisibleIndex = 12
            Me.GridView1.Columns("Insertt").VisibleIndex = 13
            Me.GridView1.Columns("Inject").VisibleIndex = 14
            Me.GridView1.Columns("Phylon").VisibleIndex = 15
            Me.GridView1.Columns("Insock").VisibleIndex = 16
            Me.GridView1.Columns("Insole").VisibleIndex = 17
            Me.GridView1.Columns("Outsole").VisibleIndex = 18
            Me.GridView1.Columns("Ass").VisibleIndex = 19
            Me.GridView1.Columns("Finish").VisibleIndex = 20
            Me.GridView1.Columns("Pack").VisibleIndex = 21

            Me.GridView1.Columns("CuttUpp").Visible = True
            Me.GridView1.Columns("CuttBott").Visible = True
            Me.GridView1.Columns("Seri").Visible = True
            Me.GridView1.Columns("SabUpp").Visible = False
            Me.GridView1.Columns("SabIns").Visible = False
            Me.GridView1.Columns("JhtKomp").Visible = False
            Me.GridView1.Columns("JhtUpp").Visible = False
            Me.GridView1.Columns("FinishUpp").Visible = True
            Me.GridView1.Columns("Insock").Visible = True
            Me.GridView1.Columns("Insole").Visible = False
            Me.GridView1.Columns("Outsole").Visible = False
            Me.GridView1.Columns("Insertt").Visible = True
            Me.GridView1.Columns("Inject").Visible = True
            Me.GridView1.Columns("Ass").Visible = False
            Me.GridView1.Columns("Finish").Visible = True
            Me.GridView1.Columns("Pack").Visible = True
            Me.GridView1.Columns("Phylon").Visible = True

        End If
    End Sub

    Private Sub FHslProd_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Hasil Produksi"
    End Sub

    Private Sub FHslProd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FHslProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTHslProd_e.Selected = True
    End Sub

    Private Sub BVTHslProd_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTHslProd_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Hasil Produksi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("HProdJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Hasil Produksi"

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriod() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        LUE()

        CekSave = True

        Indicator = "100"

        Me.SLUUnit.EditValue = ""
        Me.CBOJam.EditValue = 0
        Me.TBInfo.EditValue = ""

        FillDtl(Me.DTPTanggal.EditValue, Me.CBOJam.EditValue, Me.SLUUnit.EditValue)
        DsMaster.Tables("T_HProdJamDtl").Clear()
        ReDim arrPar(-1)

        If Me.SLUUnit.EditValue = "" Or Me.CBOJam.EditValue = 0 Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.GridView1.OptionsBehavior.Editable = False
        Else
            Me.GridView1.OptionsBehavior.Editable = True
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Hasil Produksi"

        If MainModule.SlstsPeriod() = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.CBOJam.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jam")
        Me.SLUUnit.EditValue = Me.GridView2.GetFocusedDataRow.Item("Unit")
        FillDtl(Me.DTPTanggal.EditValue, Me.CBOJam.EditValue, Me.SLUUnit.EditValue)

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True

        If Me.SLUUnit.EditValue = "" Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.GridView1.OptionsBehavior.Editable = False
        Else
            Me.GridView1.OptionsBehavior.Editable = True
        End If

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJam.Properties.ReadOnly = True
        Me.SLUUnit.Properties.ReadOnly = True
        CekKolom()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Hasil Produksi"

        koneksi.Close()

        If MainModule.SlstsPeriod() = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Unit : " & Me.GridView2.GetFocusedDataRow.Item("Unit") & " Tanggal " & Format(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "dd MMMM yyyy") & " Jam " & Me.GridView2.GetFocusedDataRow.Item("Jam") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_HProdJam")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
                .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.GridView2.GetFocusedDataRow.Item("Jam")
                .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Unit")
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
                End Try
            End With
        End If

    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Print()
    End Sub


    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUUnit.EditValue = "" Then
            XtraMessageBox.Show("Unit Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100
                If MainModule.SlHProdJam(Me.DTPTanggal.EditValue, Me.CBOJam.EditValue, Me.SLUUnit.EditValue) > 0 Then
                    XtraMessageBox.Show("Tanggal, Jam Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim cmSP As New SqlCommand("SPInsT_HProdJam")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HProdJamDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MerkID")
                                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsID")
                                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotPsg")
                                    .Parameters.Add("@CuttUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CuttUpp")
                                    .Parameters.Add("@CuttBott", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CuttBott")
                                    .Parameters.Add("@Seri", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Seri")
                                    .Parameters.Add("@SabUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SabUpp")
                                    .Parameters.Add("@SabIns", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SabIns")
                                    .Parameters.Add("@JhtKomp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JhtKomp")
                                    .Parameters.Add("@JhtUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JhtUpp")
                                    .Parameters.Add("@FinishUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "FinishUpp")
                                    .Parameters.Add("@Insock", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insock")
                                    .Parameters.Add("@Insole", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insole")
                                    .Parameters.Add("@Outsole", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Outsole")
                                    .Parameters.Add("@Insertt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insertt")
                                    .Parameters.Add("@Inject", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Inject")
                                    .Parameters.Add("@Ass", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ass")
                                    .Parameters.Add("@Finish", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Finish")
                                    .Parameters.Add("@Pack", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pack")
                                    .Parameters.Add("@Phylon", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Phylon")
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

                                If x <> 0 Then
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpT_HProdJam")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                    .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
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


                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_HProdJamDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tgl", SqlDbType.VarChar).Value = Me.DTPTanggal.EditValue
                                .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
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


                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "HPIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_HProdJamDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                        .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                        .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                        .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MerkID")
                                        .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsID")
                                        .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotPsg")
                                        .Parameters.Add("@CuttUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CuttUpp")
                                        .Parameters.Add("@CuttBott", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CuttBott")
                                        .Parameters.Add("@Seri", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Seri")
                                        .Parameters.Add("@SabUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SabUpp")
                                        .Parameters.Add("@SabIns", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SabIns")
                                        .Parameters.Add("@JhtKomp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JhtKomp")
                                        .Parameters.Add("@JhtUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JhtUpp")
                                        .Parameters.Add("@FinishUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "FinishUpp")
                                        .Parameters.Add("@Insock", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insock")
                                        .Parameters.Add("@Insole", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insole")
                                        .Parameters.Add("@Outsole", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Outsole")
                                        .Parameters.Add("@Insertt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insertt")
                                        .Parameters.Add("@Inject", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Inject")
                                        .Parameters.Add("@Ass", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ass")
                                        .Parameters.Add("@Finish", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Finish")
                                        .Parameters.Add("@Pack", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pack")
                                        .Parameters.Add("@Phylon", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Phylon")
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

                                    If x <> 0 Then
                                        Del()
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_HProdJamDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "HPIDD")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.CBOJam.EditValue
                                        .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtName")
                                        .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                        .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MerkID")
                                        .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsID")
                                        .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotPsg")
                                        .Parameters.Add("@CuttUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CuttUpp")
                                        .Parameters.Add("@CuttBott", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "CuttBott")
                                        .Parameters.Add("@Seri", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Seri")
                                        .Parameters.Add("@SabUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SabUpp")
                                        .Parameters.Add("@SabIns", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SabIns")
                                        .Parameters.Add("@JhtKomp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JhtKomp")
                                        .Parameters.Add("@JhtUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "JhtUpp")
                                        .Parameters.Add("@FinishUpp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "FinishUpp")
                                        .Parameters.Add("@Insock", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insock")
                                        .Parameters.Add("@Insole", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insole")
                                        .Parameters.Add("@Outsole", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Outsole")
                                        .Parameters.Add("@Insertt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Insertt")
                                        .Parameters.Add("@Inject", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Inject")
                                        .Parameters.Add("@Ass", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Ass")
                                        .Parameters.Add("@Finish", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Finish")
                                        .Parameters.Add("@Pack", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pack")
                                        .Parameters.Add("@Phylon", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Phylon")
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

                                    If x <> 0 Then
                                        Del()
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

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("HPIDD")
        End If
    End Sub

    Dim Edit As Boolean = True

    Private Sub BEdBOMID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBOMID.ButtonClick
        If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = True Then
            Dim frm As New FSearch("BOM Jasa", "", "", "", Date.Now, "")
            frm.ShowDialog()
            Try
                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("ArtName").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Warna", dataTrans.Item("Warna").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TotPsg", dataTrans.Item("TotPsg").ToString)

                    Me.BEdBOMID.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
                    Me.GridColumn3.OptionsColumn.AllowEdit = False
                    Me.GridColumn4.OptionsColumn.AllowEdit = False
                    Me.GridColumn5.OptionsColumn.AllowEdit = False
                    Edit = False
                End If

            Catch ex As Exception

            End Try
        Else

            Dim frm As New FSearch("BOM", "", "", "", Date.Now, "")
            frm.ShowDialog()
            Try
                If SlLOHBOM(Me.DTPTanggal.EditValue, dataTrans.Item("MerkID").ToString, dataTrans.Item("JnsID").ToString) > 0 Then
                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                        Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "MerkID", dataTrans.Item("MerkID").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "JnsID", dataTrans.Item("JnsID").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("ArtName").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Warna", dataTrans.Item("Warna").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TotPsg", dataTrans.Item("TotPsg").ToString)
                    End If
                Else
                    XtraMessageBox.Show("Nilai LOH Untuk BOM " & dataTrans.Item("Kode").ToString & " Belum Diinput", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub BEdBOMID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBOMID.KeyPress
        If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = False Or Edit = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FHslProdJam_d(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), Me.GridView2.GetFocusedDataRow.Item("Jam"), Me.GridView2.GetFocusedDataRow.Item("Unit"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("stsJasa") Then

            If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = True Then
                Me.BEdBOMID.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                Me.GridColumn3.OptionsColumn.AllowEdit = True
                Me.GridColumn4.OptionsColumn.AllowEdit = True
                Me.GridColumn5.OptionsColumn.AllowEdit = True
            Else
                Me.BEdBOMID.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
                Me.GridColumn3.OptionsColumn.AllowEdit = False
                Me.GridColumn4.OptionsColumn.AllowEdit = False
                Me.GridColumn5.OptionsColumn.AllowEdit = False
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Warna", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "MerkID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "JnsID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "TotPsg", 0)

        ElseIf e.Column Is GridView1.Columns("CuttUpp") Then

            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(CuttUpp) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "CuttUpp") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "CuttUpp", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("CuttBott") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(CuttBott) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "CuttBott") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "CuttBott", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Seri") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Seri) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Seri") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Seri", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("SabUpp") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(SabUpp) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SabUpp") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SabUpp", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("SabIns") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(SabIns) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SabIns") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SabIns", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("JhtKomp") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(JhtKomp) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "JhtKomp") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "JhtKomp", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("JhtUpp") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(JhtUpp) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "JhtUpp") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "JhtUpp", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("FinishUpp") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(FinishUpp) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "FinishUpp") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "FinishUpp", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Insock") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Insock) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Insock") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Insock", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Insole") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Insole) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Insole") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Insole", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Outsole") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Outsole) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Outsole") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Outsole", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Insertt") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Insertt) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Insertt") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Insertt", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Inject") Then
            Dim Sisa As Decimal

            'Dim command As New SqlCommand("Select Isnull((Select Sum(Inject) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            Dim command As New SqlCommand("Select Case When (Select Isnull(Sum(FU),0) From (Select Isnull(FinishUpp,0) as FU From T_HProdJamDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(FinishUpp,0) as FU From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x)>(Select Isnull((Select Sum(Inject) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)) Then (Select Isnull((Select Sum(Inject) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)) Else (Select Isnull(Sum(FU),0) From (Select Isnull(FinishUpp,0) as FU From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(FinishUpp,0) as FU From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x) End As Qty", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Inject") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM Atau Finish Upper", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Inject", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Ass") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Ass) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Ass") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Ass", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Finish") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Case When (Select Isnull(Sum(Inj),0) From (Select Isnull(Inject,0) as Inj From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Inject,0) as Inj From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x)>(Select Sum(Ins) From (Select Isnull(Insock,0) as Ins From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Insock,0) as Ins From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x) Then Case When (Select Sum(Ins) From (Select Isnull(Insock,0) as Ins From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Insock,0) as Ins From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x)>(Select Isnull((Select Sum(Finish) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>3),0)) Then (Select Isnull((Select Sum(Finish) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>3),0)) Else (Select Sum(Ins) From (Select Isnull(Insock,0) as Ins From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Insock,0) as Ins From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x) End Else Case When  (Select Isnull(Sum(Inj),0) From (Select Isnull(Inject,0) as Inj From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Inject,0) as Inj From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x) >(Select Isnull((Select Sum(Finish) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>3),0)) Then (Select Isnull((Select Sum(Finish) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>3),0)) Else (Select Isnull(Sum(Inj),0) From (Select Isnull(Inject,0) as Inj From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Inject,0) as Inj From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & " ) as x) End End As Qty", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Finish") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM Atau Insock Atau Inject", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Finish", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If

        ElseIf e.Column Is GridView1.Columns("Pack") Then
            Dim Sisa As Decimal
            If Me.CBOJam.EditValue <> 3 Then
                Dim command As New SqlCommand("Select Isnull((Select Sum(Pack) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

                With koneksi
                    .Open()
                    Sisa = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Pack") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM Atau Finishing", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Pack", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
                End If
            Else
                Dim command As New SqlCommand("Select Case When (Select Isnull(Sum(FU),0) From (Select Isnull(Finish,0) as Finish From T_HProdJamDtl Where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Finish,0) as Finish From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x)>(Select Isnull((Select Sum(Pack) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)) Then (Select Isnull((Select Sum(Pack) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)) Else (Select Isnull(Sum(FU),0) From (Select Isnull(Finish,0) as Finish From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<'" & Me.DTPTanggal.EditValue & "' Union All Select Isnull(Finish,0) as Finish From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal='" & Me.DTPTanggal.EditValue & "' and Jam<=" & Me.CBOJam.EditValue & ") as x) End As Qty", koneksi)

                With koneksi
                    .Open()
                    Sisa = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Pack") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM Atau Finishing", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Pack", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
                End If
            End If

        ElseIf e.Column Is GridView1.Columns("Phylon") Then
            Dim Sisa As Decimal

            Dim command As New SqlCommand("Select Isnull((Select Sum(Phylon) From T_HProdJamDtl where BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and Tanggal<>'" & Me.DTPTanggal.EditValue & "' and Jam<>" & Me.CBOJam.EditValue & "),0)", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Phylon") > Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Phylon", Me.GridView1.GetFocusedDataRow.Item("TotPsg") - Sisa)
            End If
        End If
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Indicator = 100 Then
                If MainModule.SlHProdJam(Me.DTPTanggal.EditValue, Me.CBOJam.EditValue, Me.SLUUnit.EditValue) > 0 Then
                    XtraMessageBox.Show("Tanggal, Jam Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.DeleteRow(e.RowHandle)
                    Exit Sub
                End If
            End If

            Me.SLUUnit.Properties.ReadOnly = True
            Me.CBOJam.Properties.ReadOnly = True
            Me.DTPTanggal.Properties.ReadOnly = True

            Me.GridView1.SetRowCellValue(e.RowHandle, "HPIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Jam", Me.CBOJam.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Unit", Me.SLUUnit.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", False)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "MerkID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "JnsID", "")

            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "TotPsg", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CuttUpp", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CuttBott", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Seri", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SabUpp", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SabIns", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "JhtKomp", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "JhtUpp", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "FinishUpp", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Outsole", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Insock", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Insole", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Insertt", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Inject", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ass", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Finish", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Pack", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Phylon", 0.0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FHslProdJam_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub SLUUnit_EditValueChanged(sender As Object, e As EventArgs) Handles SLUUnit.EditValueChanged
        If Me.SLUUnit.EditValue = "" Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.BCopy.Enabled = False

            Me.GridView1.OptionsBehavior.Editable = False
        Else
            Me.BCopy.Enabled = True

            Me.GridView1.OptionsBehavior.Editable = True
        End If

        CekKolom()

    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        Dim frm As New FSearch("Hasil Produksi Jam", Me.SLUUnit.EditValue, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Tanggal").ToString) Then
                If Indicator = 100 Then
                    If MainModule.SlHProdJam(Me.DTPTanggal.EditValue, Me.CBOJam.EditValue, Me.SLUUnit.EditValue) > 0 Then
                        XtraMessageBox.Show("Tanggal, Jam Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "HPIDD")

                    Me.GridView1.DeleteRow(i)
                Next


                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select HPIDD,D.Tanggal,D.Jam,D.Unit,D.stsJasa,D.BOMID,D.MerkID,D.JnsID,B.ArtName,B.Warna,D.TotPsg,0 As CuttUpp,0 As CuttBott,0 As Seri,0 As SabUpp,0 As SabIns,0 As JhtKomp,0 As JhtUpp,0 As FinishUpp,0 As Insock,0 As Insole,0 As Outsole,0 As Insertt,0 As Inject,0 As Ass,0 As Finish,0 As Pack,0 As Phylon From T_HProdJamDtl D Inner Join T_BOM B On D.BOMID=B.BOMID Where D.Tanggal='" & CDate(dataTrans.Item("Tanggal").ToString) & "' and Jam=" & CInt(dataTrans.Item("Jam").ToString) & " and D.Unit='" & Me.SLUUnit.EditValue & "'", koneksi)

                cmsl.TableMappings.Add("Table", "T_HProdJamDtl")
                Try
                    DsMaster.Tables("T_HProdJamDtl").Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "T_HProdJamDtl")

                DsMaster.Tables("T_HProdJamDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdJamDtl").Columns("Tanggal"), DsMaster.Tables("T_HProdJamDtl").Columns("Jam"), DsMaster.Tables("T_HProdJamDtl").Columns("Unit"), DsMaster.Tables("T_HProdJamDtl").Columns("BOMID")}

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_HProdJamDtl"

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class