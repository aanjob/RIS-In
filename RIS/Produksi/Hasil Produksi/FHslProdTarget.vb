Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHslProdTarget
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("HProdTN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("HProdTEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("HProdTDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTHslProd_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUUnit.Properties.ReadOnly = True

        Me.TBCuttUpp.Properties.ReadOnly = True
        Me.TBCuttBott.Properties.ReadOnly = True
        Me.TBSeri.Properties.ReadOnly = True
        Me.TBSabUpp.Properties.ReadOnly = True
        Me.TBSabIns.Properties.ReadOnly = True
        Me.TBJhtKomp.Properties.ReadOnly = True
        Me.TBJhtUpp.Properties.ReadOnly = True
        Me.TBFinishUpp.Properties.ReadOnly = True
        Me.TBOutsole.Properties.ReadOnly = True
        Me.TBInsock.Properties.ReadOnly = True
        Me.TBInsole.Properties.ReadOnly = True
        Me.TBInsert.Properties.ReadOnly = True
        Me.TBInject.Properties.ReadOnly = True
        Me.TBAss.Properties.ReadOnly = True
        Me.TBFinish.Properties.ReadOnly = True
        Me.TBPack.Properties.ReadOnly = True
        Me.TBPhylon.Properties.ReadOnly = True

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

        Me.TBCuttUpp.Properties.ReadOnly = False
        Me.TBCuttBott.Properties.ReadOnly = False
        Me.TBSeri.Properties.ReadOnly = False
        Me.TBSabUpp.Properties.ReadOnly = False
        Me.TBSabIns.Properties.ReadOnly = False
        Me.TBJhtKomp.Properties.ReadOnly = False
        Me.TBJhtUpp.Properties.ReadOnly = False
        Me.TBFinishUpp.Properties.ReadOnly = False
        Me.TBOutsole.Properties.ReadOnly = False
        Me.TBInsock.Properties.ReadOnly = False
        Me.TBInsole.Properties.ReadOnly = False
        Me.TBInsert.Properties.ReadOnly = False
        Me.TBInject.Properties.ReadOnly = False
        Me.TBAss.Properties.ReadOnly = False
        Me.TBFinish.Properties.ReadOnly = False
        Me.TBPack.Properties.ReadOnly = False
        Me.TBPhylon.Properties.ReadOnly = False

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

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tanggal,Unit,CuttUpp,CuttBott,Seri,SabUpp,SabIns,JhtKomp,JhtUpp,FinishUpp,Insock,Insole,Outsole, Insertt,Inject,Ass,Finish,Pack,Phylon,InsDate,InsBy,UpdDate,UpdBy From T_HProdTarget Where Year(Tanggal)=" & MainModule.periodeTahun & " and Month(Tanggal)=" & MainModule.periodeBulan & " and Unit In (Select Unit From M_UsUnit Where UserID=" & MainModule.UserAktif & ") Order By Tanggal", koneksi)

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

        Dim cmSP As New SqlCommand("SPDelT_HProdTarget")
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

            Me.TBCuttUpp.Properties.ReadOnly = False
            Me.TBCuttBott.Properties.ReadOnly = False
            Me.TBSeri.Properties.ReadOnly = False
            Me.TBSabUpp.Properties.ReadOnly = False
            Me.TBSabIns.Properties.ReadOnly = False
            Me.TBJhtKomp.Properties.ReadOnly = False
            Me.TBJhtUpp.Properties.ReadOnly = False
            Me.TBFinishUpp.Properties.ReadOnly = True
            Me.TBInsock.Properties.ReadOnly = True
            Me.TBInsole.Properties.ReadOnly = True
            Me.TBOutsole.Properties.ReadOnly = False
            Me.TBInsert.Properties.ReadOnly = True
            Me.TBInject.Properties.ReadOnly = True
            Me.TBAss.Properties.ReadOnly = False
            Me.TBFinish.Properties.ReadOnly = True
            Me.TBPack.Properties.ReadOnly = False
            Me.TBPhylon.Properties.ReadOnly = True

            Me.TBFinishUpp.EditValue = 0
            Me.TBInsock.EditValue = 0
            Me.TBInsole.EditValue = 0
            Me.TBInsert.EditValue = 0
            Me.TBInject.EditValue = 0
            Me.TBFinish.EditValue = 0
            Me.TBPhylon.EditValue = 0

        ElseIf Me.SLUUnit.EditValue = "3" Then
            Me.TBCuttUpp.Properties.ReadOnly = False
            Me.TBCuttBott.Properties.ReadOnly = False
            Me.TBSeri.Properties.ReadOnly = False
            Me.TBSabUpp.Properties.ReadOnly = True
            Me.TBSabIns.Properties.ReadOnly = True
            Me.TBJhtKomp.Properties.ReadOnly = True
            Me.TBJhtUpp.Properties.ReadOnly = True
            Me.TBFinishUpp.Properties.ReadOnly = False
            Me.TBInsock.Properties.ReadOnly = False
            Me.TBInsole.Properties.ReadOnly = True
            Me.TBOutsole.Properties.ReadOnly = True
            Me.TBInsert.Properties.ReadOnly = False
            Me.TBInject.Properties.ReadOnly = False
            Me.TBAss.Properties.ReadOnly = True
            Me.TBFinish.Properties.ReadOnly = False
            Me.TBPack.Properties.ReadOnly = False
            Me.TBPhylon.Properties.ReadOnly = False


            Me.TBSabUpp.EditValue = 0
            Me.TBSabIns.EditValue = 0
            Me.TBJhtKomp.EditValue = 0
            Me.TBJhtUpp.EditValue = 0
            Me.TBInsole.EditValue = 0
            Me.TBOutsole.EditValue = 0
            Me.TBAss.EditValue = 0
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
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("HProdTP"), Boolean)
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
        Me.TBCuttUpp.EditValue = 0.0
        Me.TBCuttBott.EditValue = 0.0
        Me.TBSeri.EditValue = 0.0
        Me.TBSabUpp.EditValue = 0.0
        Me.TBSabIns.EditValue = 0.0
        Me.TBJhtKomp.EditValue = 0.0
        Me.TBJhtUpp.EditValue = 0.0
        Me.TBFinishUpp.EditValue = 0.0
        Me.TBOutsole.EditValue = 0.0
        Me.TBInsock.EditValue = 0.0
        Me.TBInsole.EditValue = 0.0
        Me.TBInsert.EditValue = 0.0
        Me.TBInject.EditValue = 0.0
        Me.TBAss.EditValue = 0.0
        Me.TBFinish.EditValue = 0.0
        Me.TBPack.EditValue = 0.0
        Me.TBPhylon.EditValue = 0.0

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
        Me.TBCuttUpp.EditValue = Me.GridView2.GetFocusedDataRow.Item("CuttUpp")
        Me.TBCuttBott.EditValue = Me.GridView2.GetFocusedDataRow.Item("CuttBott")
        Me.TBSeri.EditValue = Me.GridView2.GetFocusedDataRow.Item("Seri")
        Me.TBSabUpp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SabUpp")
        Me.TBSabIns.EditValue = Me.GridView2.GetFocusedDataRow.Item("SabIns")
        Me.TBJhtKomp.EditValue = Me.GridView2.GetFocusedDataRow.Item("JhtKomp")
        Me.TBJhtUpp.EditValue = Me.GridView2.GetFocusedDataRow.Item("JhtUpp")
        Me.TBFinishUpp.EditValue = Me.GridView2.GetFocusedDataRow.Item("FinishUpp")
        Me.TBOutsole.EditValue = Me.GridView2.GetFocusedDataRow.Item("Outsole")
        Me.TBInsock.EditValue = Me.GridView2.GetFocusedDataRow.Item("Insock")
        Me.TBInsole.EditValue = Me.GridView2.GetFocusedDataRow.Item("Insole")
        Me.TBInsert.EditValue = Me.GridView2.GetFocusedDataRow.Item("Insertt")
        Me.TBInject.EditValue = Me.GridView2.GetFocusedDataRow.Item("Inject")
        Me.TBAss.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ass")
        Me.TBFinish.EditValue = Me.GridView2.GetFocusedDataRow.Item("Finish")
        Me.TBPack.EditValue = Me.GridView2.GetFocusedDataRow.Item("Pack")
        Me.TBPhylon.EditValue = Me.GridView2.GetFocusedDataRow.Item("Phylon")

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True

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
            Dim cmSP As New SqlCommand("SPDelT_HProdTarget")
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

        If Me.SLUUnit.EditValue = "" Then
            XtraMessageBox.Show("Unit Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100
                If MainModule.SlHProdTarget(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue) > 0 Then
                    XtraMessageBox.Show("Tanggal Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim cmSP As New SqlCommand("SPInsT_HProdTarget")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                    .Parameters.Add("@CuttUpp", SqlDbType.Decimal).Value = Me.TBCuttUpp.EditValue
                    .Parameters.Add("@CuttBott", SqlDbType.Decimal).Value = Me.TBCuttBott.EditValue
                    .Parameters.Add("@Seri", SqlDbType.Decimal).Value = Me.TBSeri.EditValue
                    .Parameters.Add("@SabUpp", SqlDbType.Decimal).Value = Me.TBSabUpp.EditValue
                    .Parameters.Add("@SabIns", SqlDbType.Decimal).Value = Me.TBSabIns.EditValue
                    .Parameters.Add("@JhtKomp", SqlDbType.Decimal).Value = Me.TBJhtKomp.EditValue
                    .Parameters.Add("@JhtUpp", SqlDbType.Decimal).Value = Me.TBJhtUpp.EditValue
                    .Parameters.Add("@FinishUpp", SqlDbType.Decimal).Value = Me.TBFinishUpp.EditValue
                    .Parameters.Add("@Insock", SqlDbType.Decimal).Value = Me.TBInsock.EditValue
                    .Parameters.Add("@Insole", SqlDbType.Decimal).Value = Me.TBInsole.EditValue
                    .Parameters.Add("@Outsole", SqlDbType.Decimal).Value = Me.TBOutsole.EditValue
                    .Parameters.Add("@Insertt", SqlDbType.Decimal).Value = Me.TBInsert.EditValue
                    .Parameters.Add("@Inject", SqlDbType.Decimal).Value = Me.TBInject.EditValue
                    .Parameters.Add("@Ass", SqlDbType.Decimal).Value = Me.TBAss.EditValue
                    .Parameters.Add("@Finish", SqlDbType.Decimal).Value = Me.TBFinish.EditValue
                    .Parameters.Add("@Pack", SqlDbType.Decimal).Value = Me.TBPack.EditValue
                    .Parameters.Add("@Phylon", SqlDbType.Decimal).Value = Me.TBPhylon.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With

                Try

                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

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

            Case 200

                Dim cmSP As New SqlCommand("SPUpT_HProdTarget")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.SLUUnit.EditValue
                    .Parameters.Add("@CuttUpp", SqlDbType.Decimal).Value = Me.TBCuttUpp.EditValue
                    .Parameters.Add("@CuttBott", SqlDbType.Decimal).Value = Me.TBCuttBott.EditValue
                    .Parameters.Add("@Seri", SqlDbType.Decimal).Value = Me.TBSeri.EditValue
                    .Parameters.Add("@SabUpp", SqlDbType.Decimal).Value = Me.TBSabUpp.EditValue
                    .Parameters.Add("@SabIns", SqlDbType.Decimal).Value = Me.TBSabIns.EditValue
                    .Parameters.Add("@JhtKomp", SqlDbType.Decimal).Value = Me.TBJhtKomp.EditValue
                    .Parameters.Add("@JhtUpp", SqlDbType.Decimal).Value = Me.TBJhtUpp.EditValue
                    .Parameters.Add("@FinishUpp", SqlDbType.Decimal).Value = Me.TBFinishUpp.EditValue
                    .Parameters.Add("@Insock", SqlDbType.Decimal).Value = Me.TBInsock.EditValue
                    .Parameters.Add("@Insole", SqlDbType.Decimal).Value = Me.TBInsole.EditValue
                    .Parameters.Add("@Outsole", SqlDbType.Decimal).Value = Me.TBOutsole.EditValue
                    .Parameters.Add("@Insertt", SqlDbType.Decimal).Value = Me.TBInsert.EditValue
                    .Parameters.Add("@Inject", SqlDbType.Decimal).Value = Me.TBInject.EditValue
                    .Parameters.Add("@Ass", SqlDbType.Decimal).Value = Me.TBAss.EditValue
                    .Parameters.Add("@Finish", SqlDbType.Decimal).Value = Me.TBFinish.EditValue
                    .Parameters.Add("@Pack", SqlDbType.Decimal).Value = Me.TBPack.EditValue
                    .Parameters.Add("@Phylon", SqlDbType.Decimal).Value = Me.TBPhylon.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With
                Try

                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

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
        End Select

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub FHProdTarget_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub SLUUnit_EditValueChanged(sender As Object, e As EventArgs) Handles SLUUnit.EditValueChanged
        If Me.SLUUnit.EditValue = "" Or IsDBNull(Me.SLUUnit.EditValue) Then
            Me.BCopy.Enabled = False
        Else
            Me.BCopy.Enabled = True
        End If

        CekKolom()

    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        Dim frm As New FSearch("Hasil Produksi Target", Me.SLUUnit.EditValue, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Tanggal").ToString) Then
                If Indicator = 100 Then
                    If MainModule.SlHProdTarget(Me.DTPTanggal.EditValue, Me.SLUUnit.EditValue) > 0 Then
                        XtraMessageBox.Show("Tanggal Dan Unit Yang Dipilih Telah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                End If

                Me.TBCuttUpp.EditValue = CDec(dataTrans.Item("CuttUpp").ToString)
                Me.TBCuttBott.EditValue = CDec(dataTrans.Item("CuttBott").ToString)
                Me.TBSeri.EditValue = CDec(dataTrans.Item("Seri").ToString)
                Me.TBSabUpp.EditValue = CDec(dataTrans.Item("SabUpp").ToString)
                Me.TBSabIns.EditValue = CDec(dataTrans.Item("SabIns").ToString)
                Me.TBJhtKomp.EditValue = CDec(dataTrans.Item("JhtKomp").ToString)
                Me.TBJhtUpp.EditValue = CDec(dataTrans.Item("JhtUpp").ToString)
                Me.TBFinishUpp.EditValue = CDec(dataTrans.Item("FinishUpp").ToString)
                Me.TBOutsole.EditValue = CDec(dataTrans.Item("Outsole").ToString)
                Me.TBInsock.EditValue = CDec(dataTrans.Item("Insock").ToString)
                Me.TBInsole.EditValue = CDec(dataTrans.Item("Insole").ToString)
                Me.TBInsert.EditValue = CDec(dataTrans.Item("Insertt").ToString)
                Me.TBInject.EditValue = CDec(dataTrans.Item("Inject").ToString)
                Me.TBAss.EditValue = CDec(dataTrans.Item("Ass").ToString)
                Me.TBFinish.EditValue = CDec(dataTrans.Item("Finish").ToString)
                Me.TBPack.EditValue = CDec(dataTrans.Item("Pack").ToString)
                Me.TBPhylon.EditValue = CDec(dataTrans.Item("Phylon").ToString)

            End If
        Catch ex As Exception

        End Try
    End Sub


End Class