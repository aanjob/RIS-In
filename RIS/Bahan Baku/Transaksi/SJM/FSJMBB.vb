Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSJMBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim Gol As String

    Public Sub New(BBSpM As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=10 and CabID='" & Gol & "'", koneksi)

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

        Me.Text = "Surat Jalan Masuk " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SJMBN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SJMBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SJMBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTSJM_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUDocID.Properties.ReadOnly = True
        Me.TBSJ.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

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
        Me.BVTSJM_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.SLUDocID.Properties.ReadOnly = False
        Me.TBSJ.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSJM_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select SJKID,GdID From T_SJKBB Where JenisSJ='Penjasaan Bahan' and stsKirim='True' or SJKID='" & Me.SLUDocID.EditValue & "' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_SJKBBLUE")
        cmsl.Fill(DsMaster, "T_SJKBBLUE")
        DsMaster.Tables("T_SJKBBLUE").Clear()
        cmsl.Fill(DsMaster, "T_SJKBBLUE")

        Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_SJKBBLUE")
        Me.SLUDocID.Properties.DisplayMember = "SJKID"
        Me.SLUDocID.Properties.ValueMember = "SJKID"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"


        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangDr")
        cmsl.Fill(DsMaster, "M_GudangDr")
        DsMaster.Tables("M_GudangDr").Clear()
        cmsl.Fill(DsMaster, "M_GudangDr")

        Me.SLUDari.Properties.DataSource = DsMaster.Tables("M_GudangDr")
        Me.SLUDari.Properties.DisplayMember = "Nama"
        Me.SLUDari.Properties.ValueMember = "GdID"

    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SJMIDD,SJMID,DocIDD,'" & InisialBC & "'+D.BBID as BBID,B.Nama as Bahan,D.Sat,Qty From T_SJMBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where SJMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJMBBDtl")
        Try
            DsMaster.Tables("T_SJMBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SJMBBDtl")

        DsMaster.Tables("T_SJMBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJMBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SJMBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SJMID,PeriodID,CodeID,Tanggal,H.Gol,DocID,H.GdID,G.Nama as Gudang,DrID,(Select Nama From M_Gudang Where GdID=DrID) as Dari,NoSJ,H.Ket,H.InsDate,H.InsBy, H.UpdDate,H.UpdBy From T_SJMBB H Inner Join M_Gudang G On H.GdID=G.GdID and PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,SJMID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJMBB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_SJMBB" & Gol)
        DsMaster.Tables("T_SJMBB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_SJMBB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SJMBB" & Gol
    End Sub

    Public Sub CekInsBC()
        If Manual = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & Me.SLUGd.EditValue & "'", koneksi)

            With koneksi
                .Open()
                InisialBC = command.ExecuteScalar()
                .Close()
            End With
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_SJMBB")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = "DelEr"
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

    Private Sub FSJMBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Surat Jalan Masuk " & Gol
    End Sub

    Private Sub FSJMBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FSJMBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FSJMBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTSJM_e.Selected = True
    End Sub


    Private Sub BVTSJM_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSJM_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Surat Jalan Masuk " & Gol
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SJMBP"), Boolean)
    End Sub


    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Surat Jalan Masuk " & Gol

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
        LUE()
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUDari.EditValue = ""
        Me.TBSJ.EditValue = ""
        Me.SLUDocID.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_SJMBBDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Surat Jalan Masuk " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.SLUDocID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DocID")

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJMID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.SLUDari.EditValue = Me.GridView2.GetFocusedDataRow.Item("DrID")
        Me.TBSJ.EditValue = Me.GridView2.GetFocusedDataRow.Item("NoSJ")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        CekInsBC()
        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Surat Jalan Masuk " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If Manual = False Then
            Dim Hapus As Boolean = True

            FillDtl(Me.GridView2.GetFocusedDataRow.Item("SJMID"))

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                Dim Stok2 As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.GridView2.GetFocusedDataRow.Item("GdID") & "','" & Me.GridView2.GetFocusedDataRow.Item("SJMID") & "'," & Me.GridView1.GetRowCellValue(i, "SJMIDD") & ")", koneksi)

                With koneksi
                    .Open()
                    Stok2 = command.ExecuteScalar()
                    .Close()
                End With

                If Stok2 < 0 Then
                    Hapus = False

                    Exit For
                Else
                    Hapus = True
                End If
            Next

            If Hapus = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("SJMID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_SJMBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SJMID")
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

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJMID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DocID"), "DocID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Dari"), "Dari")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("NoSJ"), "NoSJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRSJMBB
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGd.EditValue = "" Or IsDBNull(Me.SLUGd.EditValue) Then
            XtraMessageBox.Show("Gudang Tidak Boleh Kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_SJMBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUDocID.EditValue
                    .Parameters.Add("@DrID", SqlDbType.VarChar).Value = Me.SLUDari.EditValue
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_SJMBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 10
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@DocIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBKode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpT_SJMBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUDocID.EditValue
                    .Parameters.Add("@DrID", SqlDbType.VarChar).Value = Me.SLUDari.EditValue
                    .Parameters.Add("@SJ", SqlDbType.VarChar).Value = Me.TBSJ.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_SJMBBDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "SJMIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SJMBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 10
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@DocIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
                                        Me.GridView1.SetRowCellValue(i, "SJMIDD", Me.GridView1.GetRowCellValue(i, "SJMIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_SJMBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJMIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 10
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@DocIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "DocIDD")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("SJMIDD")

        End If

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            koneksi.Close()

            'If Me.SLUDocID.EditValue <> "" Then
            Dim Stok As Integer

            Dim command As New SqlCommand("Select Qty From T_SJKBBDtl Where SJKID='" & Me.SLUDocID.EditValue & "' and SJKIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocIDD") & "", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi SO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If
            'Else

            '    Dim Stok As Integer

            '    Dim command As New SqlCommand("Select dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

            '    With koneksi
            '        .Open()
            '        Stok = command.ExecuteScalar()
            '        .Close()
            '    End With

            '    If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
            '        XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            '    End If

            'End If
        End If

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        koneksi.Close()

        If Manual = False Then
            If Me.GridView1.RowCount - 1 > 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID")) Then
                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") <> "" Then
                        Dim Stok2 As Decimal

                        Dim commandx As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SJMIDD") & ")", koneksi)

                        With koneksi
                            .Open()
                            Stok2 = commandx.ExecuteScalar()
                            .Close()
                        End With

                        If Stok2 + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <= 0 Then
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FSJMBB_d(Me.GridView2.GetFocusedDataRow.Item("SJMID"), Me.GridView2.GetFocusedDataRow.Item("GdID"), Manual)
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub SLUDocID_Leave(sender As Object, e As EventArgs) Handles SLUDocID.Leave
        If Not IsDBNull(Me.SLUDocID.EditValue) And Me.SLUDocID.Properties.ReadOnly = False Then
            Try
                Me.SLUDari.EditValue = DsMaster.Tables("T_SJKBBLUE").Select("SJKID = '" & Me.SLUDocID.EditValue & "'")(0).Item("GdID")

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJMIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as SJMIDD,SJKIDD As DocIDD,'" & InisialBC & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Qty -(Select Isnull((Select Sum(Qty) From T_SJMBBDtl Where DocIDD=D.SJKIDD and H.SJKID='" & Me.SLUDocID.EditValue & "'),0)) As Qty From T_SJKBB H Inner Join T_SJKBBDtl D On H.SJKID=D.SJKID Inner Join M_BB B On D.BBID=B.BBID Where H.SJKID='" & Me.SLUDocID.EditValue & "'", koneksi)

                cmsl.TableMappings.Add("Table", "T_SJMBBDtl")
                cmsl.Fill(DsMaster, "T_SJMBBDtl")
                DsMaster.Tables("T_SJMBBDtl").Clear()
                cmsl.Fill(DsMaster, "T_SJMBBDtl")

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "T_SJMBBDtl"

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            If Me.SLUDari.EditValue = Me.SLUGd.EditValue Then
                XtraMessageBox.Show("Gudang Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.SLUGd.EditValue = ""
            End If

            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJKIDD")
                    Me.GridView1.DeleteRow(i)
                Next
            Else
                CekInsBC()
            End If
        End If

    End Sub


    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBSJ.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class