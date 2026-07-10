Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FHslProdTKLv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1) As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HProdTKLN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HProdTKLEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HProdTKLDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTHslProd_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUUnit.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False
        Me.BandedGridView1.OptionsBehavior.Editable = False

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
        Me.SLUUnit.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.BandedGridView1.OptionsBehavior.Editable = True

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

    Public Sub FillDtl(Tgl As Date, Unit As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HPIDD,Tanggal,Unit,Line,Style,Hasil,TKL,Jam From T_HProdKet Where Tanggal='" & Tgl & "' and Unit='" & Unit & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdKet")
        Try
            DsMaster.Tables("T_HProdKet").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HProdKet")

        DsMaster.Tables("T_HProdKet").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdKet").Columns("Tanggal"), DsMaster.Tables("T_HProdKet").Columns("Unit"), DsMaster.Tables("T_HProdKet").Columns("Line"), DsMaster.Tables("T_HProdKet").Columns("Style")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HProdKet"

        cmsl = New SqlDataAdapter("Select HPIDD,Tanggal,Unit,H.KatID,K.Kategori,H.GaJam,H.OTN,H.OTL,JnsJam,CuttUppJam,CuttUppOrg,CuttUppUM, CuttBottJam,CuttBottOrg,CuttBottUM,SeriJam,SeriOrg,SeriUM,SabUppJam,SabUppOrg,SabUppUM,SabInsJam,SabInsOrg,SabInsUM,JhtKompJam, JhtKompOrg,JhtKompUM,JhtUppJam,JhtUppOrg,JhtUppUM,FinishUppJam,FinishUppOrg,FinishUppUM,InsockJam,InsockOrg,InsockUM,InsoleJam,InsoleOrg, InsoleUM,OutsoleJam,OutsoleOrg,OutsoleUM, InserttJam,InserttOrg,InserttUM,InjectJam,InjectOrg,InjectUM,AssJam,AssOrg,AssUM,FinishJam, FinishOrg,FinishUM,PackJam,PackOrg,PackUM,PhylonJam,PhylonOrg,PhylonUM From T_HProdTKL H Inner Join M_KatTKL K On H.KatID=K.KatID Where Tanggal='" & Tgl & "' and Unit='" & Unit & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdTKL")
        Try
            DsMaster.Tables("T_HProdTKL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HProdTKL")

        DsMaster.Tables("T_HProdTKL").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdTKL").Columns("Unit"), DsMaster.Tables("T_HProdTKL").Columns("KatID"), DsMaster.Tables("T_HProdTKL").Columns("JnsJam"), DsMaster.Tables("T_HProdTKL").Columns("CuttUppUM"), DsMaster.Tables("T_HProdTKL").Columns("CuttBottUM"), DsMaster.Tables("T_HProdTKL").Columns("SeriUM"), DsMaster.Tables("T_HProdTKL").Columns("SabUppUM"), DsMaster.Tables("T_HProdTKL").Columns("SabInsUM"), DsMaster.Tables("T_HProdTKL").Columns("JhtKompUM"), DsMaster.Tables("T_HProdTKL").Columns("JhtUppUM"), DsMaster.Tables("T_HProdTKL").Columns("FinishUppUM"), DsMaster.Tables("T_HProdTKL").Columns("InsockUM"), DsMaster.Tables("T_HProdTKL").Columns("InsoleUM"), DsMaster.Tables("T_HProdTKL").Columns("OutsoleUM"), DsMaster.Tables("T_HProdTKL").Columns("InserttUM"), DsMaster.Tables("T_HProdTKL").Columns("InjectUM"), DsMaster.Tables("T_HProdTKL").Columns("AssUM"), DsMaster.Tables("T_HProdTKL").Columns("FinishUM")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_HProdTKL"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tanggal,Unit,InsDate,InsBy,UpdDate,UpdBy From T_HProd Where Year(Tanggal)=" & MainModule.periodeTahun & " and Month(Tanggal)=" & MainModule.periodeBulan & " and Unit In (Select Unit From M_UsUnit Where UserID=" & MainModule.UserAktif & ") Order By Tanggal", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProd")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_HProd")
        DsMaster.Tables("T_HProd").Clear()
        cmsl.Fill(DsMaster, "T_HProd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_HProd"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_HProd")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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

            Me.GridBand1.Visible = True
            Me.gridBand2.Visible = True
            Me.gridBand3.Visible = True
            Me.gridBand4.Visible = True
            Me.gridBand5.Visible = True
            Me.gridBand6.Visible = True
            Me.gridBand7.Visible = True
            Me.gridBand8.Visible = True
            Me.gridBand9.Visible = True
            Me.gridBand10.Visible = False
            Me.gridBand11.Visible = False
            Me.gridBand12.Visible = False
            Me.gridBand13.Visible = False
            Me.gridBand14.Visible = True
            Me.gridBand15.Visible = False
            Me.gridBand16.Visible = True
            Me.gridBand17.Visible = False
            Me.gridBand18.Visible = False

        ElseIf Me.SLUUnit.EditValue = "3" Then

            Me.GridBand1.Visible = True
            Me.gridBand2.Visible = True
            Me.gridBand3.Visible = True
            Me.gridBand4.Visible = True
            Me.gridBand5.Visible = False
            Me.gridBand6.Visible = False
            Me.gridBand7.Visible = False
            Me.gridBand8.Visible = False
            Me.gridBand9.Visible = False
            Me.gridBand10.Visible = True
            Me.gridBand11.Visible = False
            Me.gridBand12.Visible = True
            Me.gridBand13.Visible = True
            Me.gridBand14.Visible = False
            Me.gridBand15.Visible = True
            Me.gridBand16.Visible = True
            Me.gridBand17.Visible = True
            Me.gridBand18.Visible = True
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
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("HProdTKLP"), Boolean)
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
        Me.TBInfo.EditValue = ""

        FillDtl(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue)
        DsMaster.Tables("T_HProdTKL").Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        If Me.SLUUnit.EditValue = "" Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.GridView1.OptionsBehavior.Editable = False
            Me.BandedGridView1.OptionsBehavior.Editable = False
        Else
            Me.GridView1.OptionsBehavior.Editable = True
            Me.BandedGridView1.OptionsBehavior.Editable = True
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
        Me.SLUUnit.EditValue = Me.GridView2.GetFocusedDataRow.Item("Unit")
        FillDtl(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue)

        ReDim arrPar(-1)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True

        If Me.SLUUnit.EditValue = "" Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.GridView1.OptionsBehavior.Editable = False
            Me.BandedGridView1.OptionsBehavior.Editable = False
        Else
            Me.GridView1.OptionsBehavior.Editable = True
            Me.BandedGridView1.OptionsBehavior.Editable = True
        End If

        Me.DTPTanggal.Properties.ReadOnly = True
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

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data Unit : " & Me.GridView2.GetFocusedDataRow.Item("Unit") & " Tanggal " & Format(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "dd MMMM yyyy") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_HProd")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
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
        Me.BandedGridView1.ActiveFilter.Clear()

        If Me.SLUUnit.EditValue = "" Then
            XtraMessageBox.Show("Unit Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100
                If MainModule.SlHProd(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue) > 0 Then
                    XtraMessageBox.Show("Tanggal Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim cmSP As New SqlCommand("SPInsT_HProd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "KatID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HProdKet")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                    .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Line")
                                    .Parameters.Add("@Style", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Style")
                                    .Parameters.Add("@Hasil", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Hasil")
                                    .Parameters.Add("@TKL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TKL")
                                    .Parameters.Add("@Jam", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jam")
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


                        Dim u : For u = 0 To Me.BandedGridView1.RowCount - 1
                            If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(u, "Line")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_HProdTKL")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(u, "KatID")
                                    .Parameters.Add("@GaJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "GaJam")
                                    .Parameters.Add("@OTN", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OTN")
                                    .Parameters.Add("@OTL", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OTL")
                                    .Parameters.Add("@JnsJam", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(u, "JnsJam")
                                    .Parameters.Add("@CuttUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppJam")
                                    .Parameters.Add("@CuttUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppOrg")
                                    .Parameters.Add("@CuttUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppUM")
                                    .Parameters.Add("@CuttBottJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottJam")
                                    .Parameters.Add("@CuttBottOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottOrg")
                                    .Parameters.Add("@CuttBottUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottUM")
                                    .Parameters.Add("@SeriJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriJam")
                                    .Parameters.Add("@SeriOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriOrg")
                                    .Parameters.Add("@SeriUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriUM")
                                    .Parameters.Add("@SabUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppJam")
                                    .Parameters.Add("@SabUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppOrg")
                                    .Parameters.Add("@SabUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppUM")
                                    .Parameters.Add("@SabInsJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsJam")
                                    .Parameters.Add("@SabInsOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsOrg")
                                    .Parameters.Add("@SabInsUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsUM")
                                    .Parameters.Add("@JhtKompJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompJam")
                                    .Parameters.Add("@JhtKompOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompOrg")
                                    .Parameters.Add("@JhtKompUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompUM")
                                    .Parameters.Add("@JhtUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppJam")
                                    .Parameters.Add("@JhtUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppOrg")
                                    .Parameters.Add("@JhtUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppUM")
                                    .Parameters.Add("@FinishUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppJam")
                                    .Parameters.Add("@FinishUppOrg", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppOrg")
                                    .Parameters.Add("@FinishUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppUM")
                                    .Parameters.Add("@InsockJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockJam")
                                    .Parameters.Add("@InsockOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockOrg")
                                    .Parameters.Add("@InsockUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockUM")
                                    .Parameters.Add("@InsoleJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleJam")
                                    .Parameters.Add("@InsoleOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleOrg")
                                    .Parameters.Add("@InsoleUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleUM")
                                    .Parameters.Add("@OutsoleJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleJam")
                                    .Parameters.Add("@OutsoleOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleOrg")
                                    .Parameters.Add("@OutsoleUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleUM")
                                    .Parameters.Add("@InserttJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttJam")
                                    .Parameters.Add("@InserttOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttOrg")
                                    .Parameters.Add("@InserttUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttUM")
                                    .Parameters.Add("@InjectJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectJam")
                                    .Parameters.Add("@InjectOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectOrg")
                                    .Parameters.Add("@InjectUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectUM")
                                    .Parameters.Add("@AssJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "AssJam")
                                    .Parameters.Add("@AssOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "AssOrg")
                                    .Parameters.Add("@AssUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "AssUM")
                                    .Parameters.Add("@FinishJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishJam")
                                    .Parameters.Add("@FinishOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishOrg")
                                    .Parameters.Add("@FinishUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUM")
                                    .Parameters.Add("@PackJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PackJam")
                                    .Parameters.Add("@PackOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "PackOrg")
                                    .Parameters.Add("@PackUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PackUM")
                                    .Parameters.Add("@PhylonJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonJam")
                                    .Parameters.Add("@PhylonOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonOrg")
                                    .Parameters.Add("@PhylonUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonUM")
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
                Dim cmSP As New SqlCommand("SPUpT_HProd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_HProdKet")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tgl", SqlDbType.VarChar).Value = Me.DTPTanggal.EditValue
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

                        Dim z : For z = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_HProdTKL")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(z)
                                .Parameters.Add("@Tgl", SqlDbType.VarChar).Value = Me.DTPTanggal.EditValue
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

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "HPIDD") < 0 Then

                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "KatID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_HProdKet")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                        .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Line")
                                        .Parameters.Add("@Style", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Style")
                                        .Parameters.Add("@Hasil", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Hasil")
                                        .Parameters.Add("@TKL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TKL")
                                        .Parameters.Add("@Jam", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jam")
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

                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "KatID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_HProdKet")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "HPIDD")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                        .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Line")
                                        .Parameters.Add("@Style", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Style")
                                        .Parameters.Add("@Hasil", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Hasil")
                                        .Parameters.Add("@TKL", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TKL")
                                        .Parameters.Add("@Jam", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jam")
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

                        Dim u : For u = 0 To Me.BandedGridView1.RowCount - 1
                            If Me.BandedGridView1.GetRowCellValue(u, "HPIDD") < 0 Then
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(u, "Line")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_HProdTKL")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                        .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(u, "KatID")
                                        .Parameters.Add("@GaJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "GaJam")
                                        .Parameters.Add("@OTN", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OTN")
                                        .Parameters.Add("@OTL", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OTL")
                                        .Parameters.Add("@JnsJam", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(u, "JnsJam")
                                        .Parameters.Add("@CuttUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppJam")
                                        .Parameters.Add("@CuttUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppOrg")
                                        .Parameters.Add("@CuttUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppUM")
                                        .Parameters.Add("@CuttBottJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottJam")
                                        .Parameters.Add("@CuttBottOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottOrg")
                                        .Parameters.Add("@CuttBottUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottUM")
                                        .Parameters.Add("@SeriJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriJam")
                                        .Parameters.Add("@SeriOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriOrg")
                                        .Parameters.Add("@SeriUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriUM")
                                        .Parameters.Add("@SabUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppJam")
                                        .Parameters.Add("@SabUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppOrg")
                                        .Parameters.Add("@SabUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppUM")
                                        .Parameters.Add("@SabInsJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsJam")
                                        .Parameters.Add("@SabInsOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsOrg")
                                        .Parameters.Add("@SabInsUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsUM")
                                        .Parameters.Add("@JhtKompJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompJam")
                                        .Parameters.Add("@JhtKompOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompOrg")
                                        .Parameters.Add("@JhtKompUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompUM")
                                        .Parameters.Add("@JhtUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppJam")
                                        .Parameters.Add("@JhtUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppOrg")
                                        .Parameters.Add("@JhtUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppUM")
                                        .Parameters.Add("@FinishUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppJam")
                                        .Parameters.Add("@FinishUppOrg", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppOrg")
                                        .Parameters.Add("@FinishUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppUM")
                                        .Parameters.Add("@InsockJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockJam")
                                        .Parameters.Add("@InsockOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockOrg")
                                        .Parameters.Add("@InsockUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockUM")
                                        .Parameters.Add("@InsoleJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleJam")
                                        .Parameters.Add("@InsoleOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleOrg")
                                        .Parameters.Add("@InsoleUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleUM")
                                        .Parameters.Add("@OutsoleJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleJam")
                                        .Parameters.Add("@OutsoleOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleOrg")
                                        .Parameters.Add("@OutsoleUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleUM")
                                        .Parameters.Add("@InserttJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttJam")
                                        .Parameters.Add("@InserttOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttOrg")
                                        .Parameters.Add("@InserttUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttUM")
                                        .Parameters.Add("@InjectJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectJam")
                                        .Parameters.Add("@InjectOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectOrg")
                                        .Parameters.Add("@InjectUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectUM")
                                        .Parameters.Add("@AssJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "AssJam")
                                        .Parameters.Add("@AssOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "AssOrg")
                                        .Parameters.Add("@AssUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "AssUM")
                                        .Parameters.Add("@FinishJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishJam")
                                        .Parameters.Add("@FinishOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishOrg")
                                        .Parameters.Add("@FinishUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUM")
                                        .Parameters.Add("@PackJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PackJam")
                                        .Parameters.Add("@PackOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "PackOrg")
                                        .Parameters.Add("@PackUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PackUM")
                                        .Parameters.Add("@PhylonJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonJam")
                                        .Parameters.Add("@PhylonOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonOrg")
                                        .Parameters.Add("@PhylonUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonUM")
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
                                If Not IsDBNull(Me.BandedGridView1.GetRowCellValue(u, "Line")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_HProdTKL")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "HPIDD")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                                        .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(u, "KatID")
                                        .Parameters.Add("@GaJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "GaJam")
                                        .Parameters.Add("@OTN", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OTN")
                                        .Parameters.Add("@OTL", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OTL")
                                        .Parameters.Add("@JnsJam", SqlDbType.VarChar).Value = Me.BandedGridView1.GetRowCellValue(u, "JnsJam")
                                        .Parameters.Add("@CuttUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppJam")
                                        .Parameters.Add("@CuttUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppOrg")
                                        .Parameters.Add("@CuttUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttUppUM")
                                        .Parameters.Add("@CuttBottJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottJam")
                                        .Parameters.Add("@CuttBottOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottOrg")
                                        .Parameters.Add("@CuttBottUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "CuttBottUM")
                                        .Parameters.Add("@SeriJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriJam")
                                        .Parameters.Add("@SeriOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriOrg")
                                        .Parameters.Add("@SeriUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SeriUM")
                                        .Parameters.Add("@SabUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppJam")
                                        .Parameters.Add("@SabUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppOrg")
                                        .Parameters.Add("@SabUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabUppUM")
                                        .Parameters.Add("@SabInsJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsJam")
                                        .Parameters.Add("@SabInsOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsOrg")
                                        .Parameters.Add("@SabInsUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "SabInsUM")
                                        .Parameters.Add("@JhtKompJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompJam")
                                        .Parameters.Add("@JhtKompOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompOrg")
                                        .Parameters.Add("@JhtKompUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtKompUM")
                                        .Parameters.Add("@JhtUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppJam")
                                        .Parameters.Add("@JhtUppOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppOrg")
                                        .Parameters.Add("@JhtUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "JhtUppUM")
                                        .Parameters.Add("@FinishUppJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppJam")
                                        .Parameters.Add("@FinishUppOrg", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppOrg")
                                        .Parameters.Add("@FinishUppUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUppUM")
                                        .Parameters.Add("@InsockJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockJam")
                                        .Parameters.Add("@InsockOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockOrg")
                                        .Parameters.Add("@InsockUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsockUM")
                                        .Parameters.Add("@InsoleJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleJam")
                                        .Parameters.Add("@InsoleOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleOrg")
                                        .Parameters.Add("@InsoleUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InsoleUM")
                                        .Parameters.Add("@OutsoleJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleJam")
                                        .Parameters.Add("@OutsoleOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleOrg")
                                        .Parameters.Add("@OutsoleUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "OutsoleUM")
                                        .Parameters.Add("@InserttJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttJam")
                                        .Parameters.Add("@InserttOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttOrg")
                                        .Parameters.Add("@InserttUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InserttUM")
                                        .Parameters.Add("@InjectJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectJam")
                                        .Parameters.Add("@InjectOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectOrg")
                                        .Parameters.Add("@InjectUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "InjectUM")
                                        .Parameters.Add("@AssJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "AssJam")
                                        .Parameters.Add("@AssOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "AssOrg")
                                        .Parameters.Add("@AssUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "AssUM")
                                        .Parameters.Add("@FinishJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishJam")
                                        .Parameters.Add("@FinishOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishOrg")
                                        .Parameters.Add("@FinishUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "FinishUM")
                                        .Parameters.Add("@PackJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PackJam")
                                        .Parameters.Add("@PackOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "PackOrg")
                                        .Parameters.Add("@PackUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PackUM")
                                        .Parameters.Add("@PhylonJam", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonJam")
                                        .Parameters.Add("@PhylonOrg", SqlDbType.Int).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonOrg")
                                        .Parameters.Add("@PhylonUM", SqlDbType.Decimal).Value = Me.BandedGridView1.GetRowCellValue(u, "PhylonUM")
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

    Private Sub GridControl4_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("HPIDD")
        End If
    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.BandedGridView1.GetFocusedDataRow.Item("HPIDD")
        End If
    End Sub

    Private Sub BEdKatID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdKatID.ButtonClick
        Dim frm As New FSearch("Kategori TKL", "", "", "", Date.Now, "")
        frm.ShowDialog()
        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.BandedGridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "Kategori", dataTrans.Item("Nama").ToString)
                Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "GaJam", CDec(dataTrans.Item("GaJam").ToString))
                Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "OTN", CDec(dataTrans.Item("OTN").ToString))
                Me.BandedGridView1.SetRowCellValue(Me.BandedGridView1.FocusedRowHandle, "OTL", CDec(dataTrans.Item("OTL").ToString))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FHslProdTKL_dv1(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), Me.GridView2.GetFocusedDataRow.Item("Unit"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Indicator = 100 Then
                If MainModule.SlHProd(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue) > 0 Then
                    XtraMessageBox.Show("Tanggal Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.DeleteRow(e.RowHandle)
                    Exit Sub
                End If
            End If

            Me.SLUUnit.Properties.ReadOnly = True
            Me.DTPTanggal.Properties.ReadOnly = True

            Me.GridView1.SetRowCellValue(e.RowHandle, "HPIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Unit", Me.SLUUnit.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Line", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Style", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Hasil", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Jam", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TKL", 0)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BandedGridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles BandedGridView1.InitNewRow
        Try
            If Indicator = 100 Then
                If MainModule.SlHProd(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue) > 0 Then
                    XtraMessageBox.Show("Tanggal Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.BandedGridView1.DeleteRow(e.RowHandle)
                    Exit Sub
                End If
            End If

            Me.SLUUnit.Properties.ReadOnly = True
            Me.DTPTanggal.Properties.ReadOnly = True

            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "HPIDD", Me.BandedGridView1.RowCount * -1)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Tanggal", Me.DTPTanggal.EditValue)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Unit", Me.SLUUnit.EditValue)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "KatID", "")
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "Kategori", "")
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "GaJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OTN", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OTL", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JnsJam", "")
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "CuttUppJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "CuttUppOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "CuttUppUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "CuttBottJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "CuttBottOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "CuttBottUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SeriJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SeriOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SeriUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SabUppJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SabUppOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SabUppUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SabInsJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SabInsOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "SabInsUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JhtKompJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JhtKompOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JhtKompUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JhtUppJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JhtUppOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "JhtUppUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "FinishUppJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "FinishUppOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "FinishUppUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OutsoleJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OutsoleOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "OutsoleUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InsockJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InsockOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InsockUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InsoleJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InsoleOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InsoleUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InserttJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InserttOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InserttUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InjectJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InjectOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "InjectUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "AssJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "AssOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "AssUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "FinishJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "FinishOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "FinishUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PackJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PackOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PackUM", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PhylonJam", 0.0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PhylonOrg", 0)
            Me.BandedGridView1.SetRowCellValue(e.RowHandle, "PhylonUM", 0.0)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FBPB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub SLUUnit_EditValueChanged(sender As Object, e As EventArgs) Handles SLUUnit.EditValueChanged
        If Me.SLUUnit.EditValue = "" Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.BCopy.Enabled = False

            Me.GridView1.OptionsBehavior.Editable = False
            Me.BandedGridView1.OptionsBehavior.Editable = False
        Else
            Me.BCopy.Enabled = True

            Me.GridView1.OptionsBehavior.Editable = True
            Me.BandedGridView1.OptionsBehavior.Editable = True
        End If

        CekKolom()

    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        Dim frm As New FSearch("Hasil Produksi", Me.SLUUnit.EditValue, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Tanggal").ToString) Then
                If Indicator = 100 Then
                    If MainModule.SlHProd(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue) > 0 Then
                        XtraMessageBox.Show("Tanggal Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If

                Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "HPIDD")

                    Me.GridView1.DeleteRow(y)
                Next

                Dim z : For z = Me.BandedGridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.BandedGridView1.GetRowCellValue(z, "HPIDD")

                    Me.BandedGridView1.DeleteRow(z)
                Next

                FillDtl(CDate(dataTrans.Item("Tanggal").ToString), Me.SLUUnit.EditValue)

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Line" Or view.FocusedColumn.FieldName = "Style" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class