Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Public Class FTAG
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
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=40 and CabID='" & Gol & "'", koneksi)

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

        Me.BVTTAG_e.Caption = "TAG " & Gol
        Me.Text = "TAG " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TAGN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TAGEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TAGDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTAG_s.Enabled = True

        Me.SLUGdAs.Properties.ReadOnly = True
        Me.SLUGdTj.Properties.ReadOnly = True
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
        Me.BVTTAG_s.Enabled = False

        Me.SLUGdAs.Properties.ReadOnly = False
        Me.SLUGdTj.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTAG_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGdAs.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGdAs.Properties.DisplayMember = "Nama"
        Me.SLUGdAs.Properties.ValueMember = "GdID"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangTj")
        cmsl.Fill(DsMaster, "M_GudangTj")
        DsMaster.Tables("M_GudangTj").Clear()
        cmsl.Fill(DsMaster, "M_GudangTj")

        Me.SLUGdTj.Properties.DataSource = DsMaster.Tables("M_GudangTj")
        Me.SLUGdTj.Properties.DisplayMember = "Nama"
        Me.SLUGdTj.Properties.ValueMember = "GdID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TAGIDD,TAGID,'" & InisialBC & "'+D.BBID as BBID,BtNum,B.Nama as Bahan,D.Sat,Qty,HarSat,HarAkhir,D.Ket From T_TAGDtl D Inner Join M_BB B On D.BBID=B.BBID Where TAGID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TAGDtl")
        cmsl.Fill(DsMaster, "T_TAGDtl")
        DsMaster.Tables("T_TAGDtl").Clear()
        cmsl.Fill(DsMaster, "T_TAGDtl")

        DsMaster.Tables("T_TAGDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TAGDtl").Columns("BBID"), DsMaster.Tables("T_TAGDtl").Columns("BtNum")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TAGDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TAGID,PeriodID,CodeID,H.Gol,H.GdIDAs,(Select Nama From M_Gudang where GdID=H.GdIDAs) as GudangAs, H.GdIDTj,G.Nama as GudangTj, G.Alamat,K.Nama as Kota,Tanggal,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_TAG H Inner Join M_Gudang G On H.GdIDTj=G.GdID Inner Join M_Kota K On G.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,TAGID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_TAG")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_TAG")
        DsMaster.Tables("T_TAG").Clear()
        cmsl.Fill(DsMaster, "T_TAG")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TAG"
    End Sub

    Public Sub CekInsBC()
        If Manual = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & Me.SLUGdAs.EditValue & "'", koneksi)

            With koneksi
                .Open()
                InisialBC = command.ExecuteScalar()
                .Close()
            End With
        End If
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TAG")
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

    Public Sub Print()
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TAGID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("GudangAs"), "GudangAs")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("GudangTj"), "GudangTj")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRTAG
        XR.InitializeData(bind)
    End Sub

    Private Sub FTAG_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Transfer Antar Gudang"
    End Sub

    Private Sub FTAG_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub FTAG_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FTAG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTAG_e.Selected = True
    End Sub

    Private Sub BVTTAG_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTAG_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Transfer Antar Gudang"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("TAGP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Transfer Antar Gudang"

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

        Me.SLUGdAs.EditValue = ""
        Me.SLUGdTj.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TAGDtl").Clear()
        ReDim arrPar(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Transfer Antar Gudang"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdIDAs")) > 0 Or MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdIDTj")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TAGID")
        Me.SLUGdAs.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdIDAs")
        Me.SLUGdTj.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdIDTj")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        CekInsBC()
        'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        FillDtl(Me.TBKode.EditValue)

        'AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Transfer Antar Gudang"

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        'If Manual = False Then
        '    Dim Hapus As Boolean = True

        '    FillDtl(Me.GridView2.GetFocusedDataRow.Item("TAGID"))

        '    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '        Dim Stok2 As Decimal

        '        Dim command As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.GridView2.GetFocusedDataRow.Item("GdIDTj") & "','" & Me.GridView2.GetFocusedDataRow.Item("TAGID") & "'," & Me.GridView1.GetRowCellValue(i, "TAGIDD") & ",'" & Me.GridView1.GetRowCellValue(i, "BtNum") & "')", koneksi)

        '        With koneksi
        '            .Open()
        '            Stok2 = command.ExecuteScalar()
        '            .Close()
        '        End With

        '        If Stok2 < 0 Then
        '            Hapus = False

        '            Exit For
        '        Else
        '            Hapus = True
        '        End If
        '    Next

        '    If Hapus = False Then
        '        XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If

        'End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TAGID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TAG")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TAGID")
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
        Print()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_TAG")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_TAGDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 40
                                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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
                Dim cmSP As New SqlCommand("SPUpT_TAG")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_TAGDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TAGIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TAGDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 40
                                        .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                        .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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
                                        Me.GridView1.SetRowCellValue(i, "DOIDD", Me.GridView1.GetRowCellValue(i, "DOIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TAGDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TAGIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 40
                                        .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                        .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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

        Me.BVTTAG_s.Selected = True
        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "TAGID") = Me.TBKode.EditValue Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Print()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUGdTj_Leave(sender As Object, e As EventArgs) Handles SLUGdTj.Leave
        If Me.SLUGdTj.Properties.ReadOnly = False Then
            If Me.SLUGdAs.EditValue = Me.SLUGdTj.EditValue Then
                XtraMessageBox.Show("Gudang Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.SLUGdTj.EditValue = ""
            End If
        End If

    End Sub

    Private Sub SLUGdAs_Leave(sender As Object, e As EventArgs) Handles SLUGdAs.Leave
        If Me.SLUGdAs.Properties.ReadOnly = False Then
            CekInsBC()

            If Me.SLUGdAs.EditValue <> "" And Not IsDBNull(Me.SLUGdAs.EditValue) And Me.SLUGdAs.Properties.ReadOnly = False Then
                Dim i : For i = 0 To Me.GridView1.RowCount - 1
                    Dim Stok As Decimal

                    Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGdAs.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "')", koneksi)

                    With koneksi
                        .Open()
                        Stok = command.ExecuteScalar()
                        .Close()
                    End With

                    If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                        XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                    End If
                Next

                If Me.SLUGdAs.EditValue = Me.SLUGdTj.EditValue Then
                    XtraMessageBox.Show("Gudang Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.SLUGdAs.EditValue = ""
                End If
            End If
        End If
    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Try
            Dim frm As New FSearch("Bahan BtNum", InisialBC, Gol, Me.SLUGdAs.EditValue, Me.DTPTanggal.EditValue, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum", dataTrans.Item("BtNum").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TAGIDD")
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        koneksi.Close()

        If e.Column Is GridView1.Columns("Qty") Then
            If Manual = False Then
                Dim Stok, Stok1, Stok2, Stok3, StokTemp As Decimal
                Dim command, command2 As New SqlCommand

                command = New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGdAs.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With

                command2 = New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGdAs.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TAGIDD") & ",'" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok2 = command2.ExecuteScalar()
                    .Close()
                End With

                Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGdAs.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TAGIDD") & ")", koneksi)

                With koneksi
                    .Open()
                    Stok3 = command3.ExecuteScalar()
                    .Close()
                End With

                If Stok2 > Stok3 Then
                    StokTemp = Stok3
                Else
                    StokTemp = Stok2
                End If

                If Stok1 > StokTemp Then
                    Stok = StokTemp
                Else
                    Stok = Stok1
                End If

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If
            End If
        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "TAGIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BtNum", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
    '    koneksi.Close()

    '    If Manual = False Then
    '        If Me.GridView1.RowCount - 1 > 0 Then
    '            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID")) Then
    '                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") <> "" Then
    '                    Dim Stok2 As Decimal

    '                    Dim commandx As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGdTj.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "TAGIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)

    '                    With koneksi
    '                        .Open()
    '                        Stok2 = commandx.ExecuteScalar()
    '                        .Close()
    '                    End With

    '                    If Stok2 + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <= 0 And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") > 0 Then
    '                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
    '                    Else
    '                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    'End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTAG_d(Me.GridView2.GetFocusedDataRow.Item("TAGID"), Me.GridView2.GetFocusedDataRow.Item("GdIDAs"), Manual)
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
        '        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TAGIDD")

        '        Me.GridView1.DeleteRow(i)
        '    Next
        'Else
        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Dim Stok As Decimal

            Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGdAs.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(i, "Qty", Stok)
            End If
        Next


        'End If
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

    Private Sub GridView1_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView1.ValidateRow
        Dim View As GridView = CType(sender, GridView)
        Dim BtNumCol As GridColumn = View.Columns("BtNum")

        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") = "" Then
            e.Valid = False
            View.SetColumnError(BtNumCol, "Batch Number Harus Diisi")

        ElseIf Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Replace(Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum"), " ", "") = "Rekal" Then
            e.Valid = False
            View.SetColumnError(BtNumCol, "Batch Number Rekal Tidak Boleh Dipakai")
        End If
    End Sub
End Class