Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class XXFSampReqLama
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim Pic(), PicLama() As Byte
    Dim Kode As Guid
    Dim ImageLama As Image
    Dim msLama As New MemoryStream()

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=41", koneksi)

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
        Me.BVTSampReq_up.Enabled = CType(TcodeCollection.Item("SRUP"), Boolean)
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SRN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SREd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("SRApr"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("SRCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SRDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTSampReq_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUChaser.Properties.ReadOnly = True
        Me.SLUMkt.Properties.ReadOnly = True
        Me.TBStlName.Properties.ReadOnly = True
        Me.DTPTglKirim.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.PGambar.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTSampReq_s.Enabled = False
        Me.BVTSampReq_up.Enabled = False


        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUChaser.Properties.ReadOnly = False
        Me.SLUMkt.Properties.ReadOnly = False
        Me.TBStlName.Properties.ReadOnly = False
        Me.DTPTglKirim.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.PGambar.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSampReq_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CustID,Nama As Cust From M_Cust Where Aktif='True' and Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustSp")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_CustSp")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustSp")
        Me.SLUCust.Properties.DisplayMember = "Cust"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select UserID,Nama From M_User Where Aktif='True' and PosisiID Like '%Develop%' and PosisiID<>'Developer'", koneksi)
        cmsl.TableMappings.Add("Table", "ChaserLUE")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "ChaserLUE")

        Me.SLUChaser.Properties.DataSource = DsMaster.Tables("ChaserLUE")
        Me.SLUChaser.Properties.DisplayMember = "Nama"
        Me.SLUChaser.Properties.ValueMember = "UserID"

        cmsl = New SqlDataAdapter("Select UserID,Nama From M_User Where Aktif='True' and PosisiID <>'Admin Marketing Cabang' and PosisiID Like '%Marketing%'", koneksi)
        cmsl.TableMappings.Add("Table", "MktLUE")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "MktLUE")

        Me.SLUMkt.Properties.DataSource = DsMaster.Tables("MktLUE")
        Me.SLUMkt.Properties.DisplayMember = "Nama"
        Me.SLUMkt.Properties.ValueMember = "UserID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqIDD,ReqID,SampType,Uk,Qty,Material,Ket,BtlOrder,SisaKirim,stsKirim From T_SampReqDtl Where ReqID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_SampReqDtl")
        Try
            DsMaster.Tables("T_SampReqDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SampReqDtl")

        DsMaster.Tables("T_SampReqDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_SampReqDtl").Columns("SampType"), DsMaster.Tables("T_SampReqDtl").Columns("Uk"), DsMaster.Tables("T_SampReqDtl").Columns("Material"), DsMaster.Tables("T_SampReqDtl").Columns("Ket")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SampReqDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqID,H.PeriodID,CodeID,H.Tanggal,TglKirim,H.CustID,C.Nama As Cust,UserID,MktID,(Select Nama From M_User Where UserID=H.MktID) As Mkt,ChaserID,(Select Nama From M_User Where UserID=H.ChaserID) As Chaser,StlName,StyleID,Lastt,Pattern,H.Ket,Stat, stsApp,stsKirim,stsBatal,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy,H.BtlDate,H.BtlBy From T_SampReq H Inner Join M_Cust C On H.CustID=C.CustID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,ReqID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SampReq")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_SampReq")
        DsMaster.Tables("T_SampReq").Clear()
        cmsl.Fill(DsMaster, "T_SampReq")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SampReq"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_SampReq")
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

    Private Sub FSampReq_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Sample Request"
    End Sub

    Private Sub FSampReq_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FSampReq_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTSampReq_e.Selected = True
    End Sub

    Private Sub BVTSampReq_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSampReq_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Sample Request"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SRP"), Boolean)
    End Sub

    Private Sub BVTSampReq_up_SelectedChanged(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSampReq_up.ItemPressed
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqID,C.Nama As Cust,(Select Nama From M_User Where UserID=H.ChaserID) As Chaser,StlName,StyleID,Stat From T_SampReq H Inner Join M_Cust C On H.CustID=C.CustID Where stsKirim='False' Order By Tanggal,ReqID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SampReqsts")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_SampReqsts")
        DsMaster.Tables("T_SampReqsts").Clear()
        cmsl.Fill(DsMaster, "T_SampReqsts")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_SampReqsts"
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Sample Request"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ("Own") > 0 Or MainModule.SlOpBJ("Job Order") > 0 Or MainModule.SlstsPeriodNew() = True Then
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

        Me.SLUCust.EditValue = ""
        Me.SLUChaser.EditValue = ""
        Me.SLUMkt.EditValue = ""
        Me.TBStlName.EditValue = ""
        Me.MKet.EditValue = ""
        Me.PGambar.Image = Nothing
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_SampReqDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Sample Request"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Opname Atau Tutup Periode. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If SlCek("T_SampReq", "stsApp", "ReqID", Me.GridView2.GetFocusedDataRow.Item("ReqID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUChaser.EditValue = Me.GridView2.GetFocusedDataRow.Item("ChaserID")
        Me.SLUMkt.EditValue = Me.GridView2.GetFocusedDataRow.Item("MktID")
        Me.TBStlName.EditValue = Me.GridView2.GetFocusedDataRow.Item("StlName")
        Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmd As New SqlCommand("Select PicID,Picture From M_Image Where ID='" & Me.TBKode.EditValue & "'", koneksi)
        Dim newImage As Image
        Dim Reader As SqlClient.SqlDataReader
        With cmd
            .Connection = koneksi

            With koneksi
                .Open()
                Reader = cmd.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        Kode = Reader.Item(0)
                        Pic = Reader.Item(1)
                        PicLama = Reader.Item(1)
                    End While
                Else
                    Pic = Nothing
                    PicLama = Nothing
                End If
                Reader.Close()
                .Close()
            End With
        End With

        If Pic Is Nothing Then
            Me.PGambar.Image = Nothing
        Else
            Using ms As New MemoryStream(Pic, 0, Pic.Length)
                ms.Write(Pic, 0, Pic.Length)
                newImage = Image.FromStream(ms, True)
                msLama = ms
                ImageLama = newImage
            End Using
            Me.PGambar.Image = newImage
        End If

        OpenControl()
        CekSave = True
    End Sub


    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick

        'If SlCek("T_SampReq", "stsBatal", "ReqID", Me.GridView2.GetFocusedDataRow.Item("ReqID")) = True Then
        '    XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        'Dim frm As New FSampReq_ap(Me.GridView2.GetFocusedDataRow.Item("ReqID"), Me.GridView2.GetFocusedDataRow.Item("StyleID"), Me.GridView2.GetFocusedDataRow.Item("Lastt"), Me.GridView2.GetFocusedDataRow.Item("Pattern"))
        'frm.ShowDialog()
        'frm.Close()

        'Dim frm As New FSampReq_ap()
        'frm.ShowDialog()
        'frm.Close()
    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        If SlCek("T_SampReq", "stsBatal", "ReqID", Me.GridView2.GetFocusedDataRow.Item("ReqID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Membatalkan ID Sample Request : " & Me.GridView2.GetFocusedDataRow.Item("ReqID") & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPBtlSampReq")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqID")
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
                        XtraMessageBox.Show("Sisa Request Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Sisa Request Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Request Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If
    End Sub


    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Sample Request"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_SampReq", "stsApp", "ReqID", Me.GridView2.GetFocusedDataRow.Item("ReqID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlSampReq(Me.GridView2.GetFocusedDataRow.Item("ReqID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Pengiriman", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("ReqID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_SampReq")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqID")
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ReqID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Cust"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Chaser"), "Chaser")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StlName"), "StlName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New xxXRSampReqLama
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUCust.EditValue = "" Or IsDBNull(Me.SLUCust.EditValue) Then
            XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Manual = False Then
            If Format(Me.DTPTglKirim.EditValue, "dd MMMM yyyy") = Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") Or Me.DTPTglKirim.EditValue < Me.DTPTanggal.EditValue Then
                XtraMessageBox.Show("Tanggal Selesai Harus Diisi Dengan Benar", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If


        If Me.PGambar.Image IsNot Nothing Then
            Dim ms As New MemoryStream()

            If Object.ReferenceEquals(ImageLama, Me.PGambar.EditValue) Then
                ms = msLama
            Else
                Me.PGambar.Image.Save(ms, Me.PGambar.Image.RawFormat)
                Pic = ms.GetBuffer
                ms.Close()
            End If
        Else
            XtraMessageBox.Show("Gambar Belum Dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_SampReq")
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
                    .Parameters.Add("@UserID", SqlDbType.Int).Value = MainModule.UserAktif
                    .Parameters.Add("@MktID", SqlDbType.Int).Value = Me.SLUMkt.EditValue
                    .Parameters.Add("@ChaserID", SqlDbType.Int).Value = Me.SLUChaser.EditValue
                    .Parameters.Add("@StlName", SqlDbType.VarChar).Value = Me.TBStlName.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@InputDari", SqlDbType.VarChar).Value = "Desktop"
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "SampType")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_SampReqDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Material", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Material")
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

                                If x <> 0 Then
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If Me.PGambar.Image IsNot Nothing Then
                            InsImage(Me.TBKode.EditValue, "Sample Request", Pic)
                        End If

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
                Dim cmSP As New SqlCommand("SPUpT_SampReq")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@UserID", SqlDbType.Int).Value = MainModule.UserAktif
                    .Parameters.Add("@MktID", SqlDbType.Int).Value = Me.SLUMkt.EditValue
                    .Parameters.Add("@ChaserID", SqlDbType.Int).Value = Me.SLUChaser.EditValue
                    .Parameters.Add("@StlName", SqlDbType.VarChar).Value = Me.TBStlName.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@InputDari", SqlDbType.VarChar).Value = "Desktop"
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            .Close()
                        End With

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_SampReqDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "ReqIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "SampType")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SampReqDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Material", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Material")
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
                                        Me.GridView1.SetRowCellValue(i, "ReqIDD", Me.GridView1.GetRowCellValue(i, "ReqIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "SampType")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_SampReqDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ReqIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Material", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Material")
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

                                    If x <> 0 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        If Me.PGambar.Image IsNot Nothing Then
                            If PicLama Is Nothing Then
                                InsImage(Me.TBKode.EditValue, "Sample Request", Pic)
                            Else
                                UpImage(Kode, Me.TBKode.EditValue, "Sample Request", Pic)
                            End If
                        End If

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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ReqIDD")
        End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try

            Me.GridView1.SetRowCellValue(e.RowHandle, "ReqIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SampType", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Material", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FSampReq_d(Me.GridView2.GetFocusedDataRow.Item("ReqID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FSampReq_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub BSaveUP_Click(sender As Object, e As EventArgs) Handles BSaveUP.Click
        Me.GridView3.ActiveFilter.Clear()
        Dim x As Integer

        Dim i : For i = 0 To Me.GridView3.RowCount - 1
            If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "ReqID")) Then
                Dim cmSPDtl As New SqlCommand("SPUpT_SampReqStat")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "ReqID")
                    .Parameters.Add("@Stat", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "Stat")
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
            End If

            If x <> 0 Then
                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CekAkses()
        ElseIf x = 1 Then
            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancelUP_Click(sender As Object, e As EventArgs) Handles BCancelUP.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBStlName.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "SampType" Or view.FocusedColumn.FieldName = "Uk" Or view.FocusedColumn.FieldName = "Material" Or view.FocusedColumn.FieldName = "Ket" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class