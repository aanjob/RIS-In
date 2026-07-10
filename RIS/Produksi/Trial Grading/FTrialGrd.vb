Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTrialGrd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=66", koneksi)

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
        'Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrlGrdN"), Boolean)
        'Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrlGrdEd"), Boolean)
        'Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrlHGrdDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTrialGrd_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUStyleID.Properties.ReadOnly = True
        Me.TBSL.Properties.ReadOnly = True
        Me.TBOuts.Properties.ReadOnly = True
        Me.TBSize.Properties.ReadOnly = True
        Me.TBTotOrder.Properties.ReadOnly = True

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
        Me.BVTTrialGrd_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUStyleID.Properties.ReadOnly = False
        Me.TBSL.Properties.ReadOnly = False
        Me.TBOuts.Properties.ReadOnly = False
        Me.TBSize.Properties.ReadOnly = False
        Me.TBTotOrder.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTrialGrd_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        Try
            DsMaster.Tables("M_CustL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Rows.Add("", "", "", "")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)
        cmsl.TableMappings.Add("Table", "M_StyleLUE")
        cmsl.Fill(DsMaster, "M_StyleLUE")
        DsMaster.Tables("M_StyleLUE").Clear()
        cmsl.Fill(DsMaster, "M_StyleLUE")

        Me.SLUStyleID.Properties.DataSource = DsMaster.Tables("M_StyleLUE")
        Me.SLUStyleID.Properties.DisplayMember = "Nama"
        Me.SLUStyleID.Properties.ValueMember = "StyleID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrlGrdIDD,TrlGrdID,Div,Masalah,Solusi,Tgl,TglAwal,TglEnd,CekBy From T_TrlGrdDtl Where TrlGrdID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrlGrdDtl")
        Try
            DsMaster.Tables("T_TrlGrdDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrlGrdDtl")

        DsMaster.Tables("T_TrlGrdDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrlGrdDtl").Columns("Div")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrlGrdDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrlGrdID,PeriodID,CodeID,Tanggal,TglKirim,H.CustID,C.Nama As Cust,StyleID,ShoeLast,Outsole,Size,TotOrder, H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_TrlGrd H Inner Join M_Cust C On H.CustID=C.CustID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,TrlGrdID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrlGrd")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_TrlGrd")
        DsMaster.Tables("T_TrlGrd").Clear()
        cmsl.Fill(DsMaster, "T_TrlGrd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TrlGrd"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TrlGrd")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Public Sub Print(Print As String)
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        bind = New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TrlGrdID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DocID"), "DocID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Bagian"), "Bagian")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Unit"), "Unit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")

        'Dim XR As New XRTrialGrdHarga
        'XR.InitializeData(bind)
    End Sub

    Private Sub FTrialGrd_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Trial Grading"
    End Sub

    Private Sub FTrialGrd_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FTrialGrd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTTrialGrd_e.Selected = True
    End Sub

    Private Sub BVTTrialGrd_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTrialGrd_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Trial Grading"
        FillDt()
        'Me.BVBPrint.Enabled = CType(TcodeCollection.Item("TrlPsP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Trial Grading"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglKirim.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now

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

        Me.SLUStyleID.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUStyleID.EditValue = ""
        Me.TBSL.EditValue = ""
        Me.TBOuts.EditValue = ""
        Me.TBSize.EditValue = ""
        Me.TBTotOrder.EditValue = 0

        Me.TBInfo.EditValue = ""

        'FillDtl(Me.TBKode.EditValue)
        'DsMaster.Tables("T_TrlGrdDtl").Clear()
        ReDim arrPar(-1)

        DtTrans = New System.Data.DataTable
        DtTrans.Columns.Add("TrlGrdIDD")
        DtTrans.Columns.Add("TrlGrdID")
        DtTrans.Columns.Add("Div")
        DtTrans.Columns.Add("Masalah")
        DtTrans.Columns.Add("Solusi")
        DtTrans.Columns.Add("Tgl")
        DtTrans.Columns.Add("TglAwal")
        DtTrans.Columns.Add("TglEnd")
        DtTrans.Columns.Add("CekBy")

        Me.GridControl1.DataSource = DtTrans

        DtTrans.Rows.Add(-1, Me.TBKode.EditValue, "Cutting", "", "", Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, "")
        DtTrans.Rows.Add(-2, Me.TBKode.EditValue, "Stitching", "", "", Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, "")
        DtTrans.Rows.Add(-3, Me.TBKode.EditValue, "Bottom", "", "", Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, "")
        DtTrans.Rows.Add(-4, Me.TBKode.EditValue, "Assembling", "", "", Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, Me.DTPTanggal.EditValue, "")
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Trial Grading"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TrlGrdID")
        LUE()
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUStyleID.EditValue = Me.GridView2.GetFocusedDataRow.Item("StyleID")
        Me.TBSL.EditValue = Me.GridView2.GetFocusedDataRow.Item("ShoeLast")
        Me.TBOuts.EditValue = Me.GridView2.GetFocusedDataRow.Item("Outsole")
        Me.TBSize.EditValue = Me.GridView2.GetFocusedDataRow.Item("Size")
        Me.TBTotOrder.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotOrder")

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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Trial Grading"

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TrlGrdID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TrlGrd")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TrlGrdID")
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
        Print("")
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUStyleID.EditValue = "" Or IsDBNull(Me.SLUStyleID.EditValue) Then
            XtraMessageBox.Show("Style Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_TrlGrd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyleID.EditValue
                    .Parameters.Add("@ShoeLast", SqlDbType.VarChar).Value = Me.TBSL.EditValue
                    .Parameters.Add("@Outsole", SqlDbType.VarChar).Value = Me.TBOuts.EditValue
                    .Parameters.Add("@Size", SqlDbType.VarChar).Value = Me.TBSize.EditValue
                    .Parameters.Add("@TotOrder", SqlDbType.Decimal).Value = Me.TBTotOrder.EditValue
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Div")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TrlGrdDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Div", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Div")
                                    .Parameters.Add("@Masalah", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Masalah")
                                    .Parameters.Add("@Solusi", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Solusi")
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Tgl")
                                    .Parameters.Add("@TglAw", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAwal")
                                    .Parameters.Add("@TglEnd", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglEnd")
                                    .Parameters.Add("@CekBy", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CekBy")
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
                                    XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                Else
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
                Dim cmSP As New SqlCommand("SPUpT_TrlGrd")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyleID.EditValue
                    .Parameters.Add("@ShoeLast", SqlDbType.VarChar).Value = Me.TBSL.EditValue
                    .Parameters.Add("@Outsole", SqlDbType.VarChar).Value = Me.TBOuts.EditValue
                    .Parameters.Add("@Size", SqlDbType.VarChar).Value = Me.TBSize.EditValue
                    .Parameters.Add("@TotOrder", SqlDbType.Decimal).Value = Me.TBTotOrder.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_TrlGrdDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TrlGrdIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Div")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TrlGrdDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Div", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Div")
                                        .Parameters.Add("@Masalah", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Masalah")
                                        .Parameters.Add("@Solusi", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Solusi")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Tgl")
                                        .Parameters.Add("@TglAw", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAwal")
                                        .Parameters.Add("@TglEnd", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglEnd")
                                        .Parameters.Add("@CekBy", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CekBy")
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
                                        Me.GridView1.SetRowCellValue(i, "TrlGrdIDD", Me.GridView1.GetRowCellValue(i, "TrlGrdIDD") * -1)
                                    ElseIf x = -2 Then
                                        XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Div")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TrlGrdDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrlGrdIDD")
                                        .Parameters.Add("@Div", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Div")
                                        .Parameters.Add("@Masalah", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Masalah")
                                        .Parameters.Add("@Solusi", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Solusi")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Tgl")
                                        .Parameters.Add("@TglAw", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAwal")
                                        .Parameters.Add("@TglEnd", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglEnd")
                                        .Parameters.Add("@CekBy", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CekBy")
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
                                        XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        Me.BVTTrialGrd_s.Selected = True
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("TrlGrdP"), Boolean)
        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "TrlGrdID") = Me.TBKode.EditValue Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Print("")
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TrlGrdIDD")
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTrialGrd_d(Me.GridView2.GetFocusedDataRow.Item("TrlGrdID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTrialGrd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

End Class