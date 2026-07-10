Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FPRTool
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=61", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PRTN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PRTEd"), Boolean)
        'Me.BVBApprove.Enabled = CType(TcodeCollection.Item("PRTApr"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("PRTCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PRTDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTPRT_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUSupp.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
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
        Me.BVTPRT_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.SLUSupp.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPRT_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,JT,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppLUE2")
        Try
            DsMaster.Tables("M_SuppLUE2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SuppLUE2")

        Me.SLUSupp.Properties.DataSource = DsMaster.Tables("M_SuppLUE2")
        Me.SLUSupp.Properties.DisplayMember = "Nama"
        Me.SLUSupp.Properties.ValueMember = "SuppID"

        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Clear()
        cmsl.Fill(DsMaster, "M_CustL")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PRTIDD,PRTID,StyleID,Style,D.BBID as BBID,B.Nama as Bahan,D.Sat,Qty,D.Ket,BtlOrder,SisaPO,stsPO From T_PRToolDtl D Inner Join M_BB B On D.BBID=B.BBID Where PRTID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PRToolDtl")
        Try
            DsMaster.Tables("T_PRToolDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PRToolDtl")

        DsMaster.Tables("T_PRToolDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_PRToolDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_PRToolDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PRTID,PeriodID,CodeID,Tanggal,H.CustID,C.Nama As Cust,H.SuppID,S.Nama As Supp,S.Alamat, K.Nama As Kota,TglKirim,TotQty, H.Ket,stsBatal,stsPO,stsApp,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_PRTool H Inner Join M_Supp S On H.SuppID=S.SuppID Left Outer Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On S.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,PRTID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_PRTool")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_PRTool")
        DsMaster.Tables("T_PRTool").Clear()
        cmsl.Fill(DsMaster, "T_PRTool")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_PRTool"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_PRTool")
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

        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PRTID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Supp"), "Supp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("CustID"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")

        'If Print = "Harga" Then
        '    Dim XR As New XRPRTHarga
        '    XR.InitializeData(bind)
        'Else
        Dim XR As New XRPRTool
        XR.InitializeData(bind)
        'End If
    End Sub

    Public Sub DelXml()
        If IO.File.Exists("SrBBNBOM.xml") Then
            System.IO.File.Delete("SrBBNBOM.xml")
        End If
    End Sub

    Public Sub NewEdit()
        Me.GridColumn27.Visible = False
        Me.GridColumn27.OptionsColumn.AllowEdit = False

        Me.GridColumn3.OptionsColumn.AllowEdit = True
        Me.GridColumn4.OptionsColumn.AllowEdit = True
        'Me.GridColumn5.OptionsColumn.AllowEdit = True
        'Me.GridColumn6.OptionsColumn.AllowEdit = True
        Me.GridColumn7.OptionsColumn.AllowEdit = True
        'Me.GridColumn8.OptionsColumn.AllowEdit = True
        Me.GridColumn9.OptionsColumn.AllowEdit = True
    End Sub

    Public Sub CancelOrder()
        Me.GridColumn27.Visible = True
        Me.GridColumn27.OptionsColumn.AllowEdit = True

        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowEdit = False

    End Sub

    Private Sub FPRTool_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Purchase Request Tooling"
    End Sub

    Private Sub FPRTool_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub


    Private Sub FPRTool_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FPRTool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTPRT_e.Selected = True
    End Sub

    Private Sub BVTPRT_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPRT_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Purchase Request Tooling"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("PRTP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Purchase Request Tooling"

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

        Me.SLUSupp.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_PRToolDtl").Clear()
        ReDim arrPar(-1)

        NewEdit()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Purchase Request Tooling"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If MainModule.SlPR(Me.GridView2.GetFocusedDataRow.Item("PRTID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If SlCek("T_PRTool", "stsApp", "PRTID", Me.GridView2.GetFocusedDataRow.Item("PRTID")) = True Then
        '    XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        DelXml()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("PRTID")
        LUE()
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
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
    End Sub

    'Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
    '    koneksi.Close()
    '    If SlCek("T_PRTool", "stsApp", "PRTID", Me.GridView2.GetFocusedDataRow.Item("PRTID")) = True Then
    '        XtraMessageBox.Show("Data Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    Dim cmSP As New SqlCommand("SPAppPRTool")
    '    cmSP.CommandType = CommandType.StoredProcedure
    '    Dim x As Integer

    '    With cmSP
    '        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("PRTID")
    '        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
    '        .Parameters.Add("@Return", SqlDbType.Int)
    '        .Parameters("@Return").Direction = ParameterDirection.Output
    '        .Connection = koneksi

    '        Try
    '            With koneksi
    '                .Open()
    '                cmSP.ExecuteNonQuery()
    '                x = cmSP.Parameters("@Return").Value
    '                .Close()
    '            End With

    '            If x = 0 Then
    '                XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                FillDt()
    '            Else
    '                XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '        Catch ex As Exception
    '            XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End With
    'End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Purchase Request Tooling"


        If SlCek("T_PRTool", "stsPO", "PRTID", Me.GridView2.GetFocusedDataRow.Item("PRTID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "300"
         Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("PRTID")
        LUE()
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("SuppID")
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
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Purchase Request Tooling"

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlPR(Me.GridView2.GetFocusedDataRow.Item("PRTID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("PRTID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_PRTool")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("PRTID")
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

        If Me.SLUSupp.EditValue = "" Or IsDBNull(Me.SLUSupp.EditValue) Then
            XtraMessageBox.Show("Bagian Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_PRTool")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_PRToolDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StyleID")
                                    .Parameters.Add("@Style", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Style")
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
                Dim cmSP As New SqlCommand("SPUpT_PRTool")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@SuppID", SqlDbType.VarChar).Value = Me.SLUSupp.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_PRToolDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "PRTIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_PRToolDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StyleID")
                                        .Parameters.Add("@Style", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Style")
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
                                        Me.GridView1.SetRowCellValue(i, "PRTIDD", Me.GridView1.GetRowCellValue(i, "PRTIDD") * -1)
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
                                    Dim cmSPDtl As New SqlCommand("SPUpT_PRToolDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "PRTIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StyleID")
                                        .Parameters.Add("@Style", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Style")
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
                        Dim cmSPDtl As New SqlCommand("SPBtlPRTool")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PRTIDD")
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
                                Del()
                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                        Catch ex As Exception
                            XtraMessageBox.Show("Proses Pembatalan PR Tooling Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try

                    End If

                Next

                If x = 0 Then
                    XtraMessageBox.Show("Proses Pembatalan PR Tooling Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    XtraMessageBox.Show("Proses Pembatalan PR Tooling Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

        End Select

        LockControl()
        CekSave = False

        Me.BVTPRT_s.Selected = True
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("PRTP"), Boolean)
        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "PRTID") = Me.TBKode.EditValue Then
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("PRTIDD")
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("BtlOrder") Then
                Dim Sisa As Decimal
                Dim command As New SqlCommand

            command = New SqlCommand("Select Round(Sum(Qty),0)-(Select Isnull((Select Sum(Qty) From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and PRTID='" & Me.TBKode.EditValue & "' and PRTIDD=" & Me.GridView1.GetFocusedDataRow.Item("PRTIDD") & "),0)) From T_PRToolDtl Where PRTID='" & Me.TBKode.EditValue & "' and PRTIDD=" & Me.GridView1.GetFocusedDataRow.Item("PRTIDD") & " and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' Group By PRTID,PRTIDD", koneksi)

                With koneksi
                    .Open()
                    Sisa = command.ExecuteScalar()
                    .Close()
                End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") > Sisa Then
                XtraMessageBox.Show("Batal Order Tidak Boleh Melebihi Qty Purchase Request", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BtlOrder", Sisa)
            End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPRTool_d(Me.GridView2.GetFocusedDataRow.Item("PRTID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try

            Me.GridView1.SetRowCellValue(e.RowHandle, "PRTIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "StyleID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick

        Try
            Dim frm As New FSearch("BB No BOM", Me.SLUSupp.EditValue, "Bahan", "", Date.Now, "")
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

    Private Sub BEdStyleID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdStyleID.ButtonClick

        Try
            Dim frm As New FSearch("Style BJ", "", "", "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Style", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdStyleID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdStyleID.KeyPress
        e.Handled = True
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Ket" Or view.FocusedColumn.FieldName = "Style" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class