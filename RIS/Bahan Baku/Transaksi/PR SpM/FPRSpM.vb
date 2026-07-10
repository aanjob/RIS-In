Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FPRSpM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim bind As New Collection
    Dim Jenis, Gol As String

    Public Sub New(BBSpM As String, Jns As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Jenis = Jns
        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            Me.Text = ".: Purchase Request Sparepart Stock :."
            command = New SqlCommand("Select Manuall,MnlInsUpd From M_DocCode Where DocID=62 and CabID='" & Gol & "'", koneksi)

        ElseIf Jenis = "Non Stock" Then
            Me.Text = ".: Purchase Request Sparepart Non Stock :."
            command = New SqlCommand("Select Manuall,MnlInsUpd From M_DocCode Where DocID=64 and CabID='" & Gol & "'", koneksi)

        End If

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        MnlInsUpd = Reader.Item(1)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PRSMN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PRSMEd"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("PRSMCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PRSMDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTPRS_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUReqT.Properties.ReadOnly = True
        Me.SLUDiv.Properties.ReadOnly = True
        Me.TBPeminta.Properties.ReadOnly = True
        Me.CBOTipe.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
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
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPRS_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.SLUReqT.Properties.ReadOnly = False
        Me.SLUDiv.Properties.ReadOnly = False
        Me.TBPeminta.Properties.ReadOnly = False
        Me.CBOTipe.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPRS_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DivID,Nama From M_Div Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_DivLUE")
        Try
            DsMaster.Tables("M_DivLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_DivLUE")

        Me.SLUDiv.Properties.DataSource = DsMaster.Tables("M_DivLUE")
        Me.SLUDiv.Properties.DisplayMember = "Nama"
        Me.SLUDiv.Properties.ValueMember = "DivID"

        cmsl = New SqlDataAdapter("Select ReqTID,U.Nama As Teknisi From T_ReqT H Inner Join M_User U On H.UserID=U.UserID Where stsPR='False'", koneksi)
        cmsl.TableMappings.Add("Table", "M_ReqTLUE")
        Try
            DsMaster.Tables("M_ReqTLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ReqTLUE")

        Me.SLUReqT.Properties.DataSource = DsMaster.Tables("M_ReqTLUE")
        Me.SLUReqT.Properties.DisplayMember = "ReqTID"
        Me.SLUReqT.Properties.ValueMember = "ReqTID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PRSMIDD,PRSMID,MesinID,(Select Nama From M_BB Where BBID=MesinID) as Mesin,D.BBID as BBID,B.Nama as Bahan,D.Sat,Qty,D.Ket,BtlOrder,SisaPO,stsPO From T_PRSpMDtl D Inner Join M_BB B On D.BBID=B.BBID Where PRSMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PRSpMDtl")
        Try
            DsMaster.Tables("T_PRSpMDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PRSpMDtl")

        DsMaster.Tables("T_PRSpMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_PRSpMDtl").Columns("MesinID"), DsMaster.Tables("T_PRSpMDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_PRSpMDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PRSMID,PeriodID,CodeID,Tanggal,Jenis,Tipe,ReqTID,Dv.Unit,H.DivID,Dv.Nama As Divisi,Peminta,TglKirim, TotQty,H.Ket,stsBatal,stsPO,H.Gol,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_PRSpM H Inner Join M_Div Dv On H.DivID=Dv.DivID Where PeriodID=" & MainModule.periodAktif & " and Jenis='" & Jenis & "' Order By Tanggal,PRSMID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_PRSpM" & Jenis)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_PRSpM" & Jenis)
        DsMaster.Tables("T_PRSpM" & Jenis).Clear()
        cmsl.Fill(DsMaster, "T_PRSpM" & Jenis)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_PRSpM" & Jenis
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_PRSpM")
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

    Public Sub Print(Print As String)
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        bind = New Collection

        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PRSMID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Jenis"), "Jenis")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Unit"), "Unit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Divisi"), "Divisi")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Peminta"), "Peminta")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")

        If Me.GridView2.GetFocusedDataRow.Item("Tipe") = "Sparepart" Then
            Dim XR As New XRPRSpM
            XR.InitializeData(bind, Me.GridView2.GetFocusedDataRow.Item("Jenis"), Me.GridView2.GetFocusedDataRow.Item("Gol"))
        Else
            Dim XR As New XRPRSpM2
            XR.InitializeData(bind, Me.GridView2.GetFocusedDataRow.Item("Jenis"), Me.GridView2.GetFocusedDataRow.Item("Gol"))
        End If

    End Sub

    Public Sub NewEdit()
        Me.GridColumn10.Visible = False
        Me.GridColumn10.OptionsColumn.AllowEdit = False

        Me.GridColumn3.OptionsColumn.AllowEdit = True
        Me.GridColumn5.OptionsColumn.AllowEdit = True
        Me.GridColumn7.OptionsColumn.AllowEdit = True
        Me.GridColumn9.OptionsColumn.AllowEdit = True
    End Sub

    Public Sub CancelOrder()
        Me.GridColumn10.Visible = True
        Me.GridColumn10.OptionsColumn.AllowEdit = True

        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowEdit = False
    End Sub

    Private Sub FPRS_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Purchase Request Sparepart"
    End Sub

    Private Sub FPRS_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPRSpM_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FPRS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTPRS_e.Selected = True
    End Sub

    Private Sub BVTPRS_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPRS_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Purchase Request Sparepart"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("PRSMP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Purchase Request Sparepart"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Manual = False And MainModule.BackDate = False Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now
        End If

        'DelXml()

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

        Me.CBOTipe.EditValue = "Sparepart"
        Me.SLUReqT.EditValue = ""
        Me.SLUDiv.EditValue = ""
        Me.TBPeminta.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_PRSpMDtl").Clear()
        ReDim arrPar(-1)

        NewEdit()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Purchase Request Sparepart"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlPR(Me.GridView2.GetFocusedDataRow.Item("PRSMID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'DelXml()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("PRSMID")
        LUE()
        Me.TBPeminta.EditValue = Me.GridView2.GetFocusedDataRow.Item("Peminta")
        Me.CBOTipe.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tipe")
        Me.SLUReqT.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqTID")
        Me.SLUDiv.EditValue = Me.GridView2.GetFocusedDataRow.Item("DivID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True

        NewEdit()

        If Me.CBOTipe.EditValue = "Sparepart" Then
            Me.GridColumn3.OptionsColumn.AllowEdit = True
            Me.GridColumn3.Visible = True
            Me.GridColumn4.Visible = True
        Else
            Me.GridColumn3.OptionsColumn.AllowEdit = False
            Me.GridColumn3.Visible = False
            Me.GridColumn4.Visible = False
        End If
    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Purchase Request Sparepart"


        If SlCek("T_PRSpM", "stsPO", "PRSMID", Me.GridView2.GetFocusedDataRow.Item("PRSMID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'DelXml()

        LUE()

        Indicator = "300"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("PRSMID")
        LUE()
        Me.SLUDiv.EditValue = Me.GridView2.GetFocusedDataRow.Item("DivID")
        Me.SLUReqT.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqTID")
        Me.TBPeminta.EditValue = Me.GridView2.GetFocusedDataRow.Item("Peminta")
        Me.CBOTipe.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tipe")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        CancelOrder()

        If Me.CBOTipe.EditValue = "Sparepart" Then
            Me.GridColumn3.OptionsColumn.AllowEdit = True
            Me.GridColumn3.Visible = True
            Me.GridColumn4.Visible = True
        Else
            Me.GridColumn3.OptionsColumn.AllowEdit = False
            Me.GridColumn3.Visible = False
            Me.GridColumn4.Visible = False
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Purchase Request Sparepart"

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlPR(Me.GridView2.GetFocusedDataRow.Item("PRSMID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("PRSMID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_PRSpM")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("PRSMID")
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

        If Me.CBOTipe.EditValue = "" Then
            XtraMessageBox.Show("Tipe Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUDiv.EditValue = "" Or IsDBNull(Me.SLUDiv.EditValue) Then
            XtraMessageBox.Show("Bagian Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim command As New SqlCommand

        If Jenis = "Stock" Then
            command = New SqlCommand("Select CodeID From M_DocCode Where DocID=62 and CabID='" & Gol & "'", koneksi)
        ElseIf Jenis = "Non Stock" Then
            command = New SqlCommand("Select CodeID From M_DocCode Where DocID=64 and CabID='" & Gol & "'", koneksi)
        End If

        With koneksi
            .Open()
            CodeID = command.ExecuteScalar()
            .Close()
        End With

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_PRSpM")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                    .Parameters.Add("@ReqTID", SqlDbType.VarChar).Value = Me.SLUReqT.EditValue
                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
                    .Parameters.Add("@Peminta", SqlDbType.VarChar).Value = Me.TBPeminta.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_PRSpMDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MesinID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                Dim cmSP As New SqlCommand("SPUpT_PRSpM")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jenis
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Tipe", SqlDbType.VarChar).Value = Me.CBOTipe.EditValue
                    .Parameters.Add("@ReqTID", SqlDbType.VarChar).Value = Me.SLUReqT.EditValue
                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
                    .Parameters.Add("@Peminta", SqlDbType.VarChar).Value = Me.TBPeminta.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
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
                            Dim cmSPDel As New SqlCommand("SPDelT_PRSpMDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "PRSMIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_PRSpMDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MesinID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                                        Me.GridView1.SetRowCellValue(i, "PRSMIDD", Me.GridView1.GetRowCellValue(i, "PRSMIDD") * -1)
                                    ElseIf x = -2 Then
                                        XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_PRSpMDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PRSMIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MesinID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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

            Case 300
                Dim x As Integer

                Dim i : For i = 0 To GridView1.RowCount - 1
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                        Dim cmSPDtl As New SqlCommand("SPBtlPRSpM")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PRSMIDD")
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                            .Parameters.Add("@Batal", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtlOrder")
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                        End With

                        Try
                            With koneksi
                                .Open()
                                cmSPDtl.ExecuteNonQuery()
                                x = cmSPDtl.Parameters("@Return").Value
                                .Close()
                            End With

                            If x = 0 Then

                            Else
                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                        Catch ex As Exception
                            XtraMessageBox.Show("Proses Pembatalan PR Sparepart/Mesin Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    End If

                Next

                If x = 0 Then
                    XtraMessageBox.Show("Proses Pembatalan PR Sparepart/Mesin Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("Proses Pembatalan PR Sparepart/Mesin Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

        End Select

        LockControl()
        CekSave = False

        Me.BVTPRS_s.Selected = True
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("PRSMP"), Boolean)
        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "PRSMID") = Me.TBKode.EditValue Then
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("PRSMIDD")
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("BtlOrder") Then
            Dim Sisa As Decimal
            Dim command As New SqlCommand

            command = New SqlCommand("Select Round(Sum(Qty),0)-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and PRSMID='" & Me.TBKode.EditValue & "' and PRSMIDD=" & Me.GridView1.GetFocusedDataRow.Item("PRSMIDD") & "),0)) From T_PRSpMDtl Where PRSMID='" & Me.TBKode.EditValue & "' and PRSMIDD=" & Me.GridView1.GetFocusedDataRow.Item("PRSMIDD") & " and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' Group By PRSMID,PRSMIDD", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") > Sisa Then
                XtraMessageBox.Show("Batal Order Tidak Boleh Melebihi Qty Purchase Request Sparepart", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BtlOrder", Sisa)
            End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPRSpM_d(Me.GridView2.GetFocusedDataRow.Item("PRSMID"), Me.GridView2.GetFocusedDataRow.Item("Tipe"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try

            Me.GridView1.SetRowCellValue(e.RowHandle, "PRSMIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MesinID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick

        Try
            Dim frm As New FSearch("M_BB", "", "Sparepart-Mesin", "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdMesinID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdMesinID.ButtonClick

        Try
            Dim frm As New FSearch("M_BB", "", "Sparepart-Mesin", "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Mesin", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdMesinID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdMesinID.KeyPress
        e.Handled = True
    End Sub

    Private Sub CBOTipe_Leave(sender As Object, e As EventArgs) Handles CBOTipe.Leave
        If Me.CBOTipe.Properties.ReadOnly = False Then
            If Me.CBOTipe.EditValue = "Sparepart" Then
                Me.GridColumn3.OptionsColumn.AllowEdit = True
                Me.GridColumn3.Visible = True
                Me.GridColumn4.Visible = True

                Me.GridColumn3.VisibleIndex = 0
            Else
                Me.GridColumn3.OptionsColumn.AllowEdit = False
                Me.GridColumn3.Visible = False
                Me.GridColumn4.Visible = False
            End If


            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "PRSMIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUReqT_Leave(sender As Object, e As EventArgs) Handles SLUReqT.Leave
        Me.TBPeminta.EditValue = DsMaster.Tables("M_ReqTLUE").Select("ReqTID = '" & Me.SLUReqT.EditValue & "'")(0).Item("Teknisi")
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPeminta.KeyPress, MKet.KeyPress
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