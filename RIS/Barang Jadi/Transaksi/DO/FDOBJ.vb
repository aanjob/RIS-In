Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FDOBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama, CodeID, CabIDAsLama, CabIDTjLama, UrutAs, UrutTj, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=14 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("DON"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("DOEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("DODel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTDO_s.Enabled = True

        Me.TBID.Properties.ReadOnly = True
        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.SLUCabAs.Properties.ReadOnly = True
        Me.SLUCabTj.Properties.ReadOnly = True
        Me.SLUGdAs.Properties.ReadOnly = True
        Me.SLUGdTj.Properties.ReadOnly = True
        Me.SLUReqID.Properties.ReadOnly = True
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
        Me.BVTDO_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUCabAs.Properties.ReadOnly = False
        Me.SLUCabTj.Properties.ReadOnly = False
        Me.SLUGdAs.Properties.ReadOnly = False
        Me.SLUGdTj.Properties.ReadOnly = False
        Me.SLUReqID.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTDO_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select ReqCID,CabIDAs,GdIDAs,CabIDTj,GdIDTj,Grup From T_ReqC Where stsApp='True' and stsKirim='False' and Gol='" & Gol & "' or ReqCID In (Select ReqCID From T_DO where DOID ='" & Me.TBID.EditValue & "')", koneksi)
        cmsl.TableMappings.Add("Table", "T_ReqCLUE")
        Try
            DsMaster.Tables("T_ReqCLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqCLUE" & Gol)

        Me.SLUReqID.Properties.DataSource = DsMaster.Tables("T_ReqCLUE" & Gol)
        Me.SLUReqID.Properties.DisplayMember = "ReqCID"
        Me.SLUReqID.Properties.ValueMember = "ReqCID"

        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        cmsl = New SqlDataAdapter("Select CabID,Cabang From M_Cab Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CabAsLUE")
        Try
            DsMaster.Tables("M_CabAsLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CabAsLUE")

        Me.SLUCabAs.Properties.DataSource = DsMaster.Tables("M_CabAsLUE")
        Me.SLUCabAs.Properties.DisplayMember = "Cabang"
        Me.SLUCabAs.Properties.ValueMember = "CabID"


        cmsl = New SqlDataAdapter("Select CabID,Cabang From M_Cab Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CabTjLUE")
        Try
            DsMaster.Tables("M_CabTjLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CabTjLUE")

        Me.SLUCabTj.Properties.DataSource = DsMaster.Tables("M_CabTjLUE")
        Me.SLUCabTj.Properties.DisplayMember = "Cabang"
        Me.SLUCabTj.Properties.ValueMember = "CabID"

    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DOIDD,DOID,ReqCIDD,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg From T_DODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where DOID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_DODtl" & Gol)
        Try
            DsMaster.Tables("T_DODtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_DODtl" & Gol)

        DsMaster.Tables("T_DODtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_DODtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_DODtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DOID,PeriodID,CodeID,Kode,ReqCID,H.CabIDAs,C.Cabang As CabAs,H.GdIDAs,(Select Nama From M_Gudang where GdID=H.GdIDAs) as GudangAs,UrutAs,H.CabIDTj,(Select Cabang From M_Cab where CabID=H.CabIDTj) as CabTj,H.GdIDTj,G.Nama as GudangTj, G.Alamat,K.Nama as Kota,UrutTj,Tanggal,TotQty,TotDos,TotPsg,H.Ket,H.Grup,H.Gol,Autoo,stsPrint,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy, H.PrintDate,H.PrintBy From T_DO H Inner Join M_Cab C On H.CabIDAs=C.CabID Inner Join M_Gudang G On H.GdIDTj=G.GdID Inner Join M_Kota K On K.KotaID=G.KotaID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and (H.CabIDAs In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Or H.CabIDTj In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ")) Order By DOID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_DO" & Gol)
        Try
            DsMaster.Tables("T_DO" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_DO" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_DO" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_DO")
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
        If IO.File.Exists("SrStokBJ.xml") Then
            System.IO.File.Delete("SrStokBJ.xml")
        End If
    End Sub

    Private Sub FDOBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delivery Order Barang Jadi"
    End Sub

    Private Sub FDOBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        CekSave = False

        Me.Dispose()
    End Sub

    Private Sub FDOBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FDOBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTDO_e.Selected = True
    End Sub

    Private Sub BVTDO_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTDO_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Delivery Order Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("DOP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Delivery Order Barang Jadi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
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
            Me.TBID.Properties.ReadOnly = False
            Me.TBID.EditValue = ""
        Else
            Me.TBID.Properties.ReadOnly = True
            Me.TBID.EditValue = "--"
        End If

        Me.TBKode.EditValue = "--"
        Me.SLUReqID.EditValue = ""
        Me.SLUGrup.EditValue = ""
        Me.SLUCabAs.EditValue = ""
        Me.SLUGdAs.EditValue = ""
        Me.SLUCabTj.EditValue = ""
        Me.SLUGdTj.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBID.EditValue)
        DsMaster.Tables("T_DODtl" & Gol).Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Delivery Order Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDAs"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDTj"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Autoo") = True Then
            Dim frm As New FUpKet("Delivery Order Barang Jadi", Me.GridView2.GetFocusedDataRow.Item("DOID"), Me.GridView2.GetFocusedDataRow.Item("Ket"))
            frm.ShowDialog()
            FillDt()
            Exit Sub
        End If

        DelXml()


        Indicator = "200"
        Me.TBID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DOID")
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kode")
        LUE()
        Me.SLUReqID.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
        Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
        Me.SLUCabAs.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabIDAs")
        CabIDAsLama = Me.GridView2.GetFocusedDataRow.Item("CabIDAs")
        UrutAs = Me.GridView2.GetFocusedDataRow.Item("UrutAs")
        Me.SLUCabTj.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabIDTj")
        CabIDTjLama = Me.GridView2.GetFocusedDataRow.Item("CabIDTj")
        UrutTj = Me.GridView2.GetFocusedDataRow.Item("UrutTj")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")

        If Not IsDBNull(Me.SLUCabAs.EditValue) Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabAs.EditValue & "' and Gol='" & Gol & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGdAs.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGdAs.Properties.DisplayMember = "Nama"
            Me.SLUGdAs.Properties.ValueMember = "GdID"

            Me.SLUGdAs.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdIDAs")
        End If

        If Not IsDBNull(Me.SLUCabTj.EditValue) Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabTj.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangTj" & Gol)
            cmsl.Fill(DsMaster, "M_GudangTj" & Gol)

            Me.SLUGdTj.Properties.DataSource = DsMaster.Tables("M_GudangTj" & Gol)
            Me.SLUGdTj.Properties.DisplayMember = "Nama"
            Me.SLUGdTj.Properties.ValueMember = "GdID"

            Me.SLUGdTj.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdIDTj")

        End If

        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")


        FillDtl(Me.TBID.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        Me.SLUReqID.Properties.ReadOnly = True

        If Not IsDBNull(Me.SLUReqID.EditValue) And Me.SLUReqID.EditValue = "" Then
            Me.SLUCabAs.Properties.ReadOnly = False
            Me.SLUCabTj.Properties.ReadOnly = False

            Me.SLUGdAs.Properties.ReadOnly = False
            Me.SLUGdTj.Properties.ReadOnly = False
        Else
            Me.SLUCabAs.Properties.ReadOnly = True
            Me.SLUCabTj.Properties.ReadOnly = True

            Me.SLUGdAs.Properties.ReadOnly = True
            Me.SLUGdTj.Properties.ReadOnly = True
        End If

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Delivery Order Barang Jadi"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDAs"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDTj"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Autoo") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Pembuatan Otomatis", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("DOID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_DO")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("DOID")
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
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DOID"), "DOID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kode"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ReqCID"), "SJ")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("GudangTj"), "GudangTj")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat") & vbCrLf & Me.GridView2.GetFocusedDataRow.Item("Kota"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")

        Dim cmSP As New SqlCommand("SPUpstsPrint")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("DOID")
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
                    Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "stsPrint", "True")
                Else
                    XtraMessageBox.Show("Status Print Gagal Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Status Print Gagal Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With


        Dim XR As New XRDOBJ
        XR.InitializeData(bind)

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUCabAs.EditValue = "" Or Me.SLUCabTj.EditValue = "" Or IsDBNull(Me.SLUCabAs.EditValue) Or IsDBNull(Me.SLUCabTj.EditValue) Then
            XtraMessageBox.Show("Cabang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_DO")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@ReqCID", SqlDbType.VarChar).Value = Me.SLUReqID.EditValue
                    .Parameters.Add("@CabIDAs", SqlDbType.VarChar).Value = Me.SLUCabAs.EditValue
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                    .Parameters.Add("@CabIDTj", SqlDbType.VarChar).Value = Me.SLUCabTj.EditValue
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@DOID", SqlDbType.VarChar, 30)
                    .Parameters("@DOID").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 25)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBID.EditValue = cmSP.Parameters("@DOID").Value
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_DODtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 14
                                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@ReqCIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqCIDD")
                                    .Parameters.Add("@ReqCID", SqlDbType.VarChar).Value = Me.SLUReqID.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                    .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
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
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBID.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpT_DO")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@DOID", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@ReqCID", SqlDbType.VarChar).Value = Me.SLUReqID.EditValue
                    .Parameters.Add("@CabIDAs", SqlDbType.VarChar).Value = Me.SLUCabAs.EditValue
                    .Parameters.Add("@CabIDAsLama", SqlDbType.VarChar).Value = CabIDAsLama
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                    .Parameters.Add("@UrutAs", SqlDbType.VarChar).Value = UrutAs
                    .Parameters.Add("@CabIDTj", SqlDbType.VarChar).Value = Me.SLUCabTj.EditValue
                    .Parameters.Add("@CabIDTjLama", SqlDbType.VarChar).Value = CabIDTjLama
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                    .Parameters.Add("@UrutTj", SqlDbType.VarChar).Value = UrutTj
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            Dim cmSPDel As New SqlCommand("SPDelT_DODtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "DOIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_DODtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 14
                                        .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                        .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@ReqCIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqCIDD")
                                        .Parameters.Add("@ReqCID", SqlDbType.VarChar).Value = Me.SLUReqID.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
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
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_DODtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DOIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 14
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                        .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                        .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@ReqCIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqCIDD")
                                        .Parameters.Add("@ReqCID", SqlDbType.VarChar).Value = Me.SLUReqID.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
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

    Private Sub SLUCabAs_Leave(sender As Object, e As EventArgs) Handles SLUCabAs.Leave
        If Me.SLUCabAs.Properties.ReadOnly = False Then
            Try
                If Not IsDBNull(Me.SLUReqID.EditValue) And Me.SLUReqID.EditValue = "" Then
                    If Not IsDBNull(Me.SLUCabAs.EditValue) Then
                        Dim cmsl As SqlDataAdapter
                        'cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabAs.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                        cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabAs.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)

                        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                        Try
                            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
                        Catch ex As Exception

                        End Try
                        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

                        Me.SLUGdAs.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
                        Me.SLUGdAs.Properties.DisplayMember = "Nama"
                        Me.SLUGdAs.Properties.ValueMember = "GdID"

                        If Me.SLUCabAs.EditValue = Me.SLUCabTj.EditValue Then
                            XtraMessageBox.Show("Sales Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.SLUCabAs.EditValue = ""
                        End If
                    End If
                End If


            Catch ex As Exception

            End Try

            Me.SLUGdAs.EditValue = ""

            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DOIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If
        End If
    End Sub

    Private Sub SLUCabTj_Leave(sender As Object, e As EventArgs) Handles SLUCabTj.Leave
        If Me.SLUCabTj.Properties.ReadOnly = False Then
            Try
                If Not IsDBNull(Me.SLUReqID.EditValue) And Me.SLUReqID.EditValue = "" Then
                    If Not IsDBNull(Me.SLUCabTj.EditValue) Then
                        Dim cmsl As SqlDataAdapter
                        cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabTj.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                        cmsl.TableMappings.Add("Table", "M_GudangTj" & Gol)
                        cmsl.Fill(DsMaster, "M_GudangTj" & Gol)
                        DsMaster.Tables("M_GudangTj" & Gol).Clear()
                        cmsl.Fill(DsMaster, "M_GudangTj" & Gol)

                        Me.SLUGdTj.Properties.DataSource = DsMaster.Tables("M_GudangTj" & Gol)
                        Me.SLUGdTj.Properties.DisplayMember = "Nama"
                        Me.SLUGdTj.Properties.ValueMember = "GdID"
                    End If
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub SLUGdTj_Leave(sender As Object, e As EventArgs) Handles SLUGdTj.Leave
        If Me.SLUGdTj.Properties.ReadOnly = False Then
            If Not IsDBNull(Me.SLUReqID.EditValue) And Me.SLUReqID.EditValue = "" Then
                If Me.SLUGdAs.EditValue = Me.SLUGdTj.EditValue Then
                    XtraMessageBox.Show("Gudang Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.SLUGdTj.EditValue = ""
                End If
            End If
        End If

    End Sub

    Private Sub SLUGdAs_Leave(sender As Object, e As EventArgs) Handles SLUGdAs.Leave
        If Me.SLUGdAs.Properties.ReadOnly = False Then
            If Not IsDBNull(Me.SLUReqID.EditValue) And Me.SLUReqID.EditValue = "" Then
                If Manual = False Then
                    Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DOIDD")

                        Me.GridView1.DeleteRow(i)
                    Next
                End If

                If Me.SLUGdAs.EditValue = Me.SLUGdTj.EditValue Then
                    XtraMessageBox.Show("Gudang Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.SLUGdAs.EditValue = ""
                End If
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("DOIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            Dim frm As New FDOBJ_a(Me.SLUGdAs.EditValue, Me.DTPTanggal.EditValue, Me.TBID.EditValue, Gol)
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                    Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                        If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                            Me.GridView1.AddNewRow()
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            Dim Stok As Integer

            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdAs.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBID.EditValue & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
            End If

        End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "DOIDD", Me.GridView1.RowCount * -1)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ReqCIDD", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
                rw += 1
            End If
        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FDOBJ_d(Me.GridView2.GetFocusedDataRow.Item("DOID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        If Manual = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DOIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub


    Private Sub SLUReqID_Leave(sender As Object, e As EventArgs) Handles SLUReqID.Leave
        If Me.SLUReqID.Properties.ReadOnly = False Then
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DOIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If

            LUE()

            Dim cmsl As SqlDataAdapter

            Me.SLUGrup.EditValue = DsMaster.Tables("T_ReqCLUE" & Gol).Select("ReqCID = '" & Me.SLUReqID.EditValue & "'")(0).Item("Grup")
            Me.SLUCabAs.EditValue = DsMaster.Tables("T_ReqCLUE" & Gol).Select("ReqCID = '" & Me.SLUReqID.EditValue & "'")(0).Item("CabIDAs")
            Me.SLUCabTj.EditValue = DsMaster.Tables("T_ReqCLUE" & Gol).Select("ReqCID = '" & Me.SLUReqID.EditValue & "'")(0).Item("CabIDTj")

            If Not IsDBNull(Me.SLUCabAs.EditValue) Then

                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabAs.EditValue & "' and Gol='" & Gol & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

                Me.SLUGdAs.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
                Me.SLUGdAs.Properties.DisplayMember = "Nama"
                Me.SLUGdAs.Properties.ValueMember = "GdID"
            End If

            If Not IsDBNull(Me.SLUCabTj.EditValue) Then

                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabTj.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangTj" & Gol)
                cmsl.Fill(DsMaster, "M_GudangTj" & Gol)

                Me.SLUGdTj.Properties.DataSource = DsMaster.Tables("M_GudangTj" & Gol)
                Me.SLUGdTj.Properties.DisplayMember = "Nama"
                Me.SLUGdTj.Properties.ValueMember = "GdID"
            End If

            Me.SLUGdAs.EditValue = DsMaster.Tables("T_ReqCLUE" & Gol).Select("ReqCID = '" & Me.SLUReqID.EditValue & "'")(0).Item("GdIDAs")
            Me.SLUGdTj.EditValue = DsMaster.Tables("T_ReqCLUE" & Gol).Select("ReqCID = '" & Me.SLUReqID.EditValue & "'")(0).Item("GdIDTj")


            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as DOIDD,'" & Me.TBKode.EditValue & "' As DOID,ReqCIDD,D.ArtCode, D.SatID,D.Isi,Qty,Dos,Psg From T_ReqCDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ReqCID='" & Me.SLUReqID.EditValue & "'", koneksi)

            cmsl.TableMappings.Add("Table", "T_DODtl")
            cmsl.Fill(DsMaster, "T_DODtl")
            DsMaster.Tables("T_DODtl").Clear()
            cmsl.Fill(DsMaster, "T_DODtl")

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_DODtl"
        End If
    End Sub

    Private Sub GridView2_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView2.GetRowCellValue(e.RowHandle, "stsPrint") = False Then
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Yellow
                Else
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class