Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class XXFSJKBB
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
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=9 and CabID='" & Gol & "'", koneksi)

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

        Me.BVTSJK_e.Caption = "SJK " & Gol
        Me.Text = "Surat Jalan Keluar " & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SJKBN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SJKBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SJKBDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTSJK_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.SLUDocID.Properties.ReadOnly = True
        Me.SLUKirim.Properties.ReadOnly = True
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
        Me.BVTSJK_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.SLUDocID.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSJK_e.Selected = True
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
        cmsl = New SqlDataAdapter("Select SJKIDD,SJKID,DocIDD,(Select Isnull((Select BOMID From T_SOBBDtl Where SOID='" & Kode & "' and DocIDD=D.DocIDD),'')) as BOMID,'" & InisialBC & "'+D.BBID as BBID,B.Nama as Bahan,D.Sat,Qty,SisaKirim,stsKirim From T_SJKBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where SJKID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJKBBDtl")
        Try
            DsMaster.Tables("T_SJKBBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SJKBBDtl")

        DsMaster.Tables("T_SJKBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_SJKBBDtl").Columns("BOMID"), DsMaster.Tables("T_SJKBBDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SJKBBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select stsFaktur,SJKID,PeriodID,CodeID,Tanggal,H.Gol,JenisSJ,DocID,H.GdID,G.Nama as Gudang,TjID,Case When JenisSJ='Penjasaan Bahan' Then (Select Nama From M_Supp Where SuppID=H.TjID) When JenisSJ='Penjualan Bahan' Or JenisSJ='Penjualan Jasa' Then (Select Nama From M_Cust Where CustID=H.TjID) Else (Select Nama From M_Gudang Where GdID=H.TjID) End As Tujuan,Case When JenisSJ='Penjasaan Bahan' Then (Select Alamat From M_Supp Where SuppID=H.TjID) When JenisSJ='Penjualan Bahan' Or JenisSJ='Penjualan Jasa' Then (Select Alamat From M_Cust Where CustID=H.TjID) Else (Select Alamat From M_Gudang Where GdID=H.TjID) End As Alamat,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,Case When JenisSJ='Penjasaan Bahan' Then (Select POCust From T_SOBB Where SOID=H.DocID) Else '' End as POCust From T_SJKBB H Inner Join M_Gudang G On H.GdID=G.GdID Where PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,SJKID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_SJKBB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_SJKBB" & Gol)
        DsMaster.Tables("T_SJKBB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_SJKBB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SJKBB" & Gol
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

        Dim cmSP As New SqlCommand("SPDelT_SJKBB")
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

    Private Sub FSJKBB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Surat Jalan Keluar " & Gol
    End Sub

    Private Sub FSJKBB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FSJKBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTSJK_e.Selected = True
    End Sub

    Private Sub BVTSJK_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSJK_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Surat Jalan Keluar " & Gol
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SJKBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Surat Jalan Keluar " & Gol

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

        Me.CBOJenis.EditValue = ""
        Me.SLUKirim.EditValue = ""
        Me.SLUDocID.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_SJKBBDtl").Clear()
        ReDim arrPar(-1)

        If Me.CBOJenis.EditValue = "Penjualan Jasa" Then
            Me.GridColumn27.Visible = True
            Me.GridColumn27.VisibleIndex = 0
        Else
            Me.GridColumn27.Visible = False
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Surat Jalan Keluar " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlCek("T_JualBB", "Count(*)", "SOID", Me.GridView2.GetFocusedDataRow.Item("SJKID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Dibuatkan Faktur Penjualan Bahan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("SJKID")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("JenisSJ")
        'Me.SLUDocID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DocID")

        If Me.CBOJenis.EditValue = "Penjasaan Bahan" Then
            Me.SLUDocID.Properties.ReadOnly = False
            Me.SLUKirim.Properties.ReadOnly = False

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select POID As DocID, SuppID As SCID From T_POBB Where Tipe='Jasa' and stsKirim='False' and stsBatal='False'  or POID='" & Me.GridView2.GetFocusedDataRow.Item("DocID") & "'", koneksi)
            cmsl.TableMappings.Add("Table", "T_POBBSJK")
            cmsl.Fill(DsMaster, "T_POBBSJK")
            DsMaster.Tables("T_POBBSJK").Clear()
            cmsl.Fill(DsMaster, "T_POBBSJK")

            Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_POBBSJK")
            Me.SLUDocID.Properties.DisplayMember = "DocID"
            Me.SLUDocID.Properties.ValueMember = "DocID"

            cmsl = New SqlDataAdapter("Select SuppID As SCID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_SuppLUE")
            cmsl.Fill(DsMaster, "M_SuppLUE")
            DsMaster.Tables("M_SuppLUE").Clear()
            cmsl.Fill(DsMaster, "M_SuppLUE")

            Me.SLUKirim.Properties.DataSource = DsMaster.Tables("M_SuppLUE")
            Me.SLUKirim.Properties.DisplayMember = "Nama"
            Me.SLUKirim.Properties.ValueMember = "SCID"

        ElseIf Me.CBOJenis.EditValue = "Penjualan Bahan" Or Me.CBOJenis.EditValue = "Penjualan Jasa" Then
            Me.SLUDocID.Properties.ReadOnly = False
            Me.SLUKirim.Properties.ReadOnly = True

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select SOID As DocID, CustID As SCID From T_SOBB Where stsKirim='False' and stsBatal='False' or SOID='" & Me.GridView2.GetFocusedDataRow.Item("DocID") & "'", koneksi)
            cmsl.TableMappings.Add("Table", "T_SOBBL")
            cmsl.Fill(DsMaster, "T_SOBBL")
            DsMaster.Tables("T_SOBBL").Clear()
            cmsl.Fill(DsMaster, "T_SOBBL")

            Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_SOBBL")
            Me.SLUDocID.Properties.DisplayMember = "DocID"
            Me.SLUDocID.Properties.ValueMember = "DocID"

            cmsl = New SqlDataAdapter("Select CustID As SCID,Nama From M_Cust Where Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustSp")
            cmsl.Fill(DsMaster, "M_CustSp")
            DsMaster.Tables("M_CustSp").Clear()
            cmsl.Fill(DsMaster, "M_CustSp")

            Me.SLUKirim.Properties.DataSource = DsMaster.Tables("M_CustSp")
            Me.SLUKirim.Properties.DisplayMember = "Nama"
            Me.SLUKirim.Properties.ValueMember = "SCID"

        ElseIf Me.CBOJenis.EditValue = "Pindah Gudang" Then
            Me.SLUDocID.Properties.ReadOnly = True
            Me.SLUKirim.Properties.ReadOnly = False

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='Bahan'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE")
            cmsl.Fill(DsMaster, "M_GudangLUE")
            DsMaster.Tables("M_GudangLUE").Clear()
            cmsl.Fill(DsMaster, "M_GudangLUE")

            Me.SLUKirim.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
            Me.SLUKirim.Properties.DisplayMember = "Nama"
            Me.SLUKirim.Properties.ValueMember = "GdID"
        End If

        Me.SLUDocID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DocID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.SLUKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TjID")
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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Surat Jalan Keluar " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If


        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("SJKID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_SJKBB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SJKID")
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
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SJKID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JenisSJ"), "JenisSJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DocID") & "-" & Me.GridView2.GetFocusedDataRow.Item("POCust"), "DocID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tujuan"), "Tujuan")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRSJKBBv1
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUKirim.EditValue = "" Or IsDBNull(Me.SLUKirim.EditValue) Then
            XtraMessageBox.Show("Tujuan Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_SJKBB")
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
                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUDocID.EditValue
                    .Parameters.Add("@TjID", SqlDbType.VarChar).Value = Me.SLUKirim.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_SJKBBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 9
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
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
                Dim cmSP As New SqlCommand("SPUpT_SJKBB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUDocID.EditValue
                    .Parameters.Add("@TjID", SqlDbType.VarChar).Value = Me.SLUKirim.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_SJKBBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "SJKIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_SJKBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 9
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
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
                                        Me.GridView1.SetRowCellValue(i, "SJKIDD", Me.GridView1.GetRowCellValue(i, "SJKIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_SJKBBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJKIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 9
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("SJKIDD")
        End If
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            koneksi.Close()
            Dim Stok, Stok1, Stok2 As Decimal
            Dim command, command2 As New SqlCommand

            If Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue <> "" Then

                command = New SqlCommand("Select Case When Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Then dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Else  Sum(Qty) -(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) End From T_POBBJs Where POID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "' Group By BBID", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With

                command2 = New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SJKIDD") & ")", koneksi)

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


                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi SO/Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If

            ElseIf Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue = "" Then

                command = New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With

                command2 = New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SJKIDD") & ")", koneksi)

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

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If

            ElseIf Me.CBOJenis.EditValue = "Penjualan Bahan" Then

                command = New SqlCommand("Select Case When Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=SOIDD and SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else  Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=SOIDD and SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) End From T_SOBBDtl Where SOID='" & Me.SLUDocID.EditValue & "' and SOIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "DocIDD") & "", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With

                command2 = New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SJKIDD") & ")", koneksi)

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


                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi SO/Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If


            ElseIf Me.CBOJenis.EditValue = "Penjualan Jasa" Then

                command = New SqlCommand("Select Sum(Qty) -(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) From T_SOBBDtl Where SOID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "' Group By BBID", koneksi)


                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi SO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If

            End If

        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FSJKBB_dv1(Me.GridView2.GetFocusedDataRow.Item("JenisSJ"), Me.GridView2.GetFocusedDataRow.Item("SJKID"), Me.GridView2.GetFocusedDataRow.Item("GdID"), Manual)
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "SJKIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DocIDD", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Try
            If Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue = "" Then
                Dim frm As New FSearch("M_BB", InisialBC, Gol, "", Date.Now, "")
                frm.ShowDialog()

                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub SLUDocID_Leave(sender As Object, e As EventArgs) Handles SLUDocID.Leave
        If Not IsDBNull(Me.SLUDocID.EditValue) And Me.SLUDocID.Properties.ReadOnly = False Then
            Try

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJKIDD")
                    Me.GridView1.DeleteRow(i)
                Next

                If Me.CBOJenis.EditValue = "Penjasaan Bahan" Then

                    Me.SLUKirim.EditValue = DsMaster.Tables("T_POBBSJK").Select("DocID = '" & Me.SLUDocID.EditValue & "'")(0).Item("SCID")

                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select * From(Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as SJKIDD,0 As DocIDD,'" & InisialBC & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Case When Sum(D.Qty)-(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Else  Sum(D.Qty) -(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) End As Qty From T_POBB H Inner Join T_POBBJs D On H.POID=D.POID Inner Join M_BB B On D.BBID=B.BBID Where H.POID='" & Me.SLUDocID.EditValue & "' Group By D.BBID,B.Nama,D.Sat)As x Where X.Qty>0", koneksi)

                    cmsl.TableMappings.Add("Table", "T_SJKBBDtl")
                    cmsl.Fill(DsMaster, "T_SJKBBDtl")
                    DsMaster.Tables("T_SJKBBDtl").Clear()
                    cmsl.Fill(DsMaster, "T_SJKBBDtl")

                    Me.GridControl1.DataSource = DsMaster
                    Me.GridControl1.DataMember = "T_SJKBBDtl"

                ElseIf Me.CBOJenis.EditValue = "Penjualan Bahan" Then

                    Me.SLUKirim.EditValue = DsMaster.Tables("T_SOBBL").Select("DocID = '" & Me.SLUDocID.EditValue & "'")(0).Item("SCID")

                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as SJKIDD,BOMID,SOIDD As DocIDD,'" & InisialBC & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Case When Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=D.SOIDD and H.SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else  Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=D.SOIDD and H.SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) End As Qty From T_SOBB H Inner Join T_SOBBDtl D On H.SOID=D.SOID Inner Join M_BB B On D.BBID=B.BBID Where H.SOID='" & Me.SLUDocID.EditValue & "'", koneksi)

                    cmsl.TableMappings.Add("Table", "T_SJKBBDtl")
                    cmsl.Fill(DsMaster, "T_SJKBBDtl")
                    DsMaster.Tables("T_SJKBBDtl").Clear()
                    cmsl.Fill(DsMaster, "T_SJKBBDtl")

                    Me.GridControl1.DataSource = DsMaster
                    Me.GridControl1.DataMember = "T_SJKBBDtl"

                ElseIf Me.CBOJenis.EditValue = "Penjualan Jasa" Then

                    Me.SLUKirim.EditValue = DsMaster.Tables("T_SOBBL").Select("DocID = '" & Me.SLUDocID.EditValue & "'")(0).Item("SCID")

                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.BBID)*-1 as SJKIDD,BOMID,SOIDD As DocIDD,'" & InisialBC & "'+D.BBID as BBID,B.Nama As Bahan,D.Sat,Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=D.SOIDD and H.SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) As Qty From T_SOBB H Inner Join T_SOBBDtl D On H.SOID=D.SOID Inner Join M_BB B On D.BBID=B.BBID Where H.SOID='" & Me.SLUDocID.EditValue & "' and Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=D.SOIDD and H.SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0))>0", koneksi)

                    cmsl.TableMappings.Add("Table", "T_SJKBBDtl")
                    cmsl.Fill(DsMaster, "T_SJKBBDtl")
                    DsMaster.Tables("T_SJKBBDtl").Clear()
                    cmsl.Fill(DsMaster, "T_SJKBBDtl")

                    Me.GridControl1.DataSource = DsMaster
                    Me.GridControl1.DataMember = "T_SJKBBDtl"

                End If

                If Me.SLUDocID.EditValue = "" Or IsDBNull(Me.SLUDocID.EditValue) Then
                    Me.SLUKirim.Properties.ReadOnly = False
                Else
                    Me.SLUKirim.Properties.ReadOnly = True
                End If

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub CBOJenis_Leave(sender As Object, e As EventArgs) Handles CBOJenis.Leave
        If Me.CBOJenis.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SJKIDD")
                Me.GridView1.DeleteRow(i)
            Next

            Me.SLUDocID.EditValue = ""

            If Me.CBOJenis.EditValue = "Penjasaan Bahan" Then
                Me.SLUDocID.Properties.ReadOnly = False
                Me.SLUKirim.Properties.ReadOnly = False

                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select POID As DocID,SuppID As SCID From T_POBB Where Tipe='Jasa' and stsKirim='False' and stsBatal='False' and Gol='" & Gol & "' or POID='" & Me.SLUDocID.EditValue & "'", koneksi)
                cmsl.TableMappings.Add("Table", "T_POBBSJK")
                cmsl.Fill(DsMaster, "T_POBBSJK")
                DsMaster.Tables("T_POBBSJK").Clear()
                cmsl.Fill(DsMaster, "T_POBBSJK")

                Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_POBBSJK")
                Me.SLUDocID.Properties.DisplayMember = "DocID"
                Me.SLUDocID.Properties.ValueMember = "DocID"

                cmsl = New SqlDataAdapter("Select SuppID As SCID,S.Nama,K.Nama As Kota From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_SuppLUE")
                cmsl.Fill(DsMaster, "M_SuppLUE")
                DsMaster.Tables("M_SuppLUE").Clear()
                cmsl.Fill(DsMaster, "M_SuppLUE")

                Me.SLUKirim.Properties.DataSource = DsMaster.Tables("M_SuppLUE")
                Me.SLUKirim.Properties.DisplayMember = "Nama"
                Me.SLUKirim.Properties.ValueMember = "SCID"

            ElseIf Me.CBOJenis.EditValue = "Penjualan Bahan" Or Me.CBOJenis.EditValue = "Penjualan Jasa" Then
                Me.SLUDocID.Properties.ReadOnly = False
                Me.SLUKirim.Properties.ReadOnly = True

                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select SOID As DocID,CustID As SCID From T_SOBB Where Jenis='" & Me.CBOJenis.EditValue & "' and stsKirim='False' and stsBatal='False' and Gol='" & Gol & "' or SOID='" & Me.SLUDocID.EditValue & "'", koneksi)
                cmsl.TableMappings.Add("Table", "T_SOBBL")
                cmsl.Fill(DsMaster, "T_SOBBL")
                DsMaster.Tables("T_SOBBL").Clear()
                cmsl.Fill(DsMaster, "T_SOBBL")

                Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_SOBBL")
                Me.SLUDocID.Properties.DisplayMember = "DocID"
                Me.SLUDocID.Properties.ValueMember = "DocID"

                cmsl = New SqlDataAdapter("Select CustID As SCID,Nama From M_Cust Where Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_Cust")
                cmsl.Fill(DsMaster, "M_Cust")
                DsMaster.Tables("M_Cust").Clear()
                cmsl.Fill(DsMaster, "M_Cust")

                Me.SLUKirim.Properties.DataSource = DsMaster.Tables("M_Cust")
                Me.SLUKirim.Properties.DisplayMember = "Nama"
                Me.SLUKirim.Properties.ValueMember = "SCID"
            End If

            If Me.CBOJenis.EditValue = "Penjualan Jasa" Then
                Me.GridColumn27.Visible = True
                Me.GridColumn27.VisibleIndex = 0
            Else
                Me.GridColumn27.Visible = False
            End If
        End If

    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        koneksi.Close()

        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            If Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue <> "" Then

                Dim Stok As Decimal

                Dim command As New SqlCommand("Select Case When Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Then dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Else  Sum(Qty) -(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) End From T_POBBJs Where POID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(i, "BBID") & "' Group By BBID", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi SO/Stok Bahan", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If

            ElseIf Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue = "" Then
                Dim Stok As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

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

            ElseIf Me.CBOJenis.EditValue = "Penjualan Bahan" Then

                Dim Stok As Decimal

                Dim command As New SqlCommand("Select Case When Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=SOIDD and SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else  Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=SOIDD and SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) End From T_SOBBDtl Where SOID='" & Me.SLUDocID.EditValue & "' and SOIDD=" & Me.GridView1.GetRowCellValue(i, "DocIDD") & "", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi SO/Stok Bahan", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If

            ElseIf Me.CBOJenis.EditValue = "Pindah Gudang" Then

                Dim Stok As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

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

            End If
        Next
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            koneksi.Close()
            CekInsBC()

            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue <> "" Then

                    Dim Stok As Decimal

                    Dim command As New SqlCommand("Select Case When Sum(Qty)-(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Then dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.SLUDocID.EditValue & "') Else  Sum(Qty) -(Select Isnull((Select Sum(Qty) From T_SJKBB H1 Inner Join T_SJKBBDtl D1 On H1.SJKID=D1.SJKID Where H1.DocID='" & Me.SLUDocID.EditValue & "' and H1.SJKID <>'" & Me.TBKode.EditValue & "'),0)) End From T_POBBJs Where POID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(i, "BBID") & "' Group By BBID", koneksi)

                    With koneksi
                        .Open()
                        Stok = command.ExecuteScalar()
                        .Close()
                    End With

                    If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                        XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi SO/Stok Bahan", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                    End If

                ElseIf Me.CBOJenis.EditValue = "Penjasaan Bahan" And Me.SLUDocID.EditValue = "" Then
                    Dim Stok As Decimal

                    Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

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

                ElseIf Me.CBOJenis.EditValue = "Penjualan Bahan" Then

                    Dim Stok As Decimal

                    Dim command As New SqlCommand("Select Case When Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=SOIDD and SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) > dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Then dbo.fcStokBB(BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "') Else  Qty -(Select Isnull((Select Sum(Qty) From T_SJKBBDtl Where DocIDD=SOIDD and SOID='" & Me.SLUDocID.EditValue & "' and SJKID <>'" & Me.TBKode.EditValue & "'),0)) End From T_SOBBDtl Where SOID='" & Me.SLUDocID.EditValue & "' and SOIDD=" & Me.GridView1.GetRowCellValue(i, "DocIDD") & "", koneksi)

                    With koneksi
                        .Open()
                        Stok = command.ExecuteScalar()
                        .Close()
                    End With

                    If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                        XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi SO/Stok Bahan", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                    End If

                ElseIf Me.CBOJenis.EditValue = "Pindah Gudang" Then

                    Dim Stok As Decimal

                    Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "')", koneksi)

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

                End If
            Next
        End If
    End Sub

    Private Sub FSJKBB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class