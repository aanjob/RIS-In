Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FTransBBv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama, CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim Gol As String

    Public Sub New(BBSpM As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=21 and CabID='" & Gol & "'", koneksi)

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

        Me.BVTTransBB_e.Caption = "Transfer Antar " & Gol
        Me.Text = "Transfer Antar " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrBN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTransBB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True

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
        Me.BVTTransBB_s.Enabled = False

        Me.SLUGd.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTransBB_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrIDD,TrID,'" & InisialBC & "'+D.BBIDAs as BBIDAs,Nama As BahanAs,D.SatAs,QtyAs,'" & InisialBC & "'+D.BBIDTj as BBIDTj,(Select Nama From M_BB Where BBID=D.BBIDTj) As BahanTj,D.SatTj,QtyTj,HarSat,HarAkhir,D.Ket From T_TrBBDtl D Inner Join M_BB B On D.BBIDAs=B.BBID Where TrID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrBBDtl")
        Try
            DsMaster.Tables("T_TrBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrBBDtl")

        DsMaster.Tables("T_TrBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrBBDtl").Columns("BBIDAs"), DsMaster.Tables("T_TrBBDtl").Columns("BBIDTj")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrID,PeriodID,CodeID,Tanggal,H.Gol,H.GdID,G.Nama as Gudang,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_TrBB H Inner Join M_Gudang G On H.GdID=G.GdID and PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,TrID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrBB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_TrBB" & Gol)
        DsMaster.Tables("T_TrBB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_TrBB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TrBB" & Gol
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

        Dim cmSP As New SqlCommand("SPDelT_TrBB")
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

    Public Sub DelXml()
        If IO.File.Exists("SrStokBB.xml") Then
            System.IO.File.Delete("SrStokBB.xml")
        End If
    End Sub

    Private Sub FTransBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Transfer Antar " & Gol
    End Sub

    Private Sub FTransBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FTransBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTTransBB_e.Selected = True
    End Sub
    Private Sub BVTTransBB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTransBB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Transfer Antar " & Gol
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("TrBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Transfer Antar " & Gol

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

        DelXml()

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

        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TrBBDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Transfer Antar " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TrID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        CekInsBC()
        RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        FillDtl(Me.TBKode.EditValue)

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Transfer Antar " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If Manual = False Then
            Dim Hapus As Boolean = True

            FillDtl(Me.GridView2.GetFocusedDataRow.Item("TrID"))

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                Dim Stok2 As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.GridView2.GetFocusedDataRow.Item("GdID") & "','" & Me.GridView2.GetFocusedDataRow.Item("TrID") & "'," & Me.GridView1.GetRowCellValue(i, "TrIDD") & ")", koneksi)

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

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TrID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TrBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TrID")
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TrID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRTransBBv1
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_TrBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 25)
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_TrBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 21
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@BBIDAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDAs")
                                    .Parameters.Add("@SatAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatAs")
                                    .Parameters.Add("@QtyAs", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyAs")
                                    .Parameters.Add("@BBIDTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDTj")
                                    .Parameters.Add("@SatTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatTj")
                                    .Parameters.Add("@QtyTj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyTj")
                                    .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                    .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                Dim cmSP As New SqlCommand("SPUpT_TrBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_TrBBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TrIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TrBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 21
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@BBIDAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDAs")
                                        .Parameters.Add("@SatAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatAs")
                                        .Parameters.Add("@QtyAs", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyAs")
                                        .Parameters.Add("@BBIDTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDTj")
                                        .Parameters.Add("@SatTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatTj")
                                        .Parameters.Add("@QtyTj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyTj")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                                        Me.GridView1.SetRowCellValue(i, "TrIDD", Me.GridView1.GetRowCellValue(i, "TrIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TrBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 21
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@BBIDAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDAs")
                                        .Parameters.Add("@SatAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatAs")
                                        .Parameters.Add("@QtyAs", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyAs")
                                        .Parameters.Add("@BBIDTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDTj")
                                        .Parameters.Add("@SatTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatTj")
                                        .Parameters.Add("@QtyTj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyTj")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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

    Private Sub BEdBBIDAs_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBIDAs.ButtonClick
        Dim frm As New FSearch("M_BB", InisialBC, Gol, "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BahanAs", dataTrans.Item("Nama").ToString)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatAs", dataTrans.Item("Sat").ToString)
        End If
    End Sub

    Private Sub BEdBBIDAs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBIDAs.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdBBIDTj_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBIDTj.ButtonClick
        Try
            Dim frm As New FSearch("M_BB", InisialBC, Gol, "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BahanTj", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatTj", dataTrans.Item("Sat").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBIDTj_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBIDTj.KeyPress
        e.Handled = True
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TrIDD")
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("BBIDTj") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") = Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDTj") Then
                XtraMessageBox.Show("Article Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDTj", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "BahanTj", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatTj", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "IsiTj", 0)
            End If

        ElseIf e.Column Is GridView1.Columns("QtyAs") Then
            Dim Stok, Stok1, Stok2 As Decimal
            Dim command, command2 As New SqlCommand

            Dim koneksi As New SqlConnection(GlobalKoneksi)

            command = New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

            With koneksi
                .Open()
                Stok1 = command.ExecuteScalar()
                .Close()
            End With

            command2 = New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrIDD") & ")", koneksi)

            With koneksi
                .Open()
                Stok2 = command2.ExecuteScalar()
                .Close()
            End With

            If Stok1 > Stok2 Then
                Stok = Stok2
            Else
                Stok = Stok1
            End If

            If Me.GridView1.GetRowCellValue(e.RowHandle, "QtyAs") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyAs", Stok)
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "QtyTj", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyAs"))
        End If

    End Sub
    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        koneksi.Close()

        If Manual = False Then
            If Me.GridView1.RowCount - 1 > 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBIDTj")) Then
                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBIDTj") <> "" Then
                        Dim Stok2 As Decimal

                        Dim commandx As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBIDTj") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TrIDD") & ")", koneksi)

                        With koneksi
                            .Open()
                            Stok2 = commandx.ExecuteScalar()
                            .Close()
                        End With

                        If Stok2 + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <= 0 And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") > 0 Then
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                            Me.GridView1.Columns("BBIDTj").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                            Me.GridView1.Columns("BBIDTj").OptionsColumn.AllowEdit = True
                        End If
                    End If
                End If
            End If
        End If

    End Sub


    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetRowCellValue(e.RowHandle, "TrIDD", Me.GridView1.RowCount * -1)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "QtyAs", 0)
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTransBB_dv1(Me.GridView2.GetFocusedDataRow.Item("TrID"), Me.GridView2.GetFocusedDataRow.Item("GdID"), Manual)
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        koneksi.Close()

        'If Manual = False Then
        '    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
        '        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrIDD")
        '        Me.GridView1.DeleteRow(i)
        '    Next
        'Else
        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Dim Stok As Decimal

            Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBIDAs") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(i, "QtyAs") > Stok Then
                XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(i, "QtyAs", Stok)
            End If

            Me.GridView1.SetRowCellValue(i, "QtyTj", Me.GridView1.GetRowCellValue(i, "QtyAs"))
        Next

        'End If
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        koneksi.Close()
        CekInsBC()

        'If Manual = False Then
        '    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
        '        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrIDD")
        '        Me.GridView1.DeleteRow(i)
        '    Next
        'Else
        If Not IsDBNull(Me.SLUGd.EditValue) And Me.SLUGd.Properties.ReadOnly = False Then
            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                Dim Stok As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBIDAs") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "QtyAs") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "QtyAs", Stok)
                End If

                Me.GridView1.SetRowCellValue(i, "QtyTj", Me.GridView1.GetRowCellValue(i, "QtyAs"))
            Next
        End If

        'End If
    End Sub

    Private Sub FTransBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub


    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class