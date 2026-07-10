Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FReqCab
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama, KodeTemp, CodeID, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim KdAktif As String = ""
    Dim stsReject As Boolean
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ReqCN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ReqCEd"), Boolean)
        Me.BVBReject.Enabled = CType(TcodeCollection.Item("ReqCRj"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ReqCDel"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("ReqCApr"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("ReqCCO"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTReqC_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.SLUCabTj.Properties.ReadOnly = True
        Me.SLUGdTj.Properties.ReadOnly = True
        Me.SLUCabAs.Properties.ReadOnly = True
        Me.SLUGdAs.Properties.ReadOnly = True
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
        Me.BVBReject.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTReqC_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUCabTj.Properties.ReadOnly = False
        Me.SLUGdTj.Properties.ReadOnly = False
        Me.SLUCabAs.Properties.ReadOnly = False
        Me.SLUGdAs.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        'Me.DTPTanggal.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTReqC_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

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

        cmsl = New SqlDataAdapter("Select UC.CabID,C.Cabang From M_UsCab UC Inner Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabMintaLUE")
        Try
            DsMaster.Tables("M_UsCabMintaLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabMintaLUE")
        DsMaster.Tables("M_UsCabMintaLUE").Rows.Add("RAP", "RAJAPAKSI ADYA PERKASA")

        Me.SLUCabTj.Properties.DataSource = DsMaster.Tables("M_UsCabMintaLUE")
        Me.SLUCabTj.Properties.DisplayMember = "Cabang"
        Me.SLUCabTj.Properties.ValueMember = "CabID"

        cmsl = New SqlDataAdapter("Select CabID,Cabang From M_Cab", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabAsLUE")
        Try
            DsMaster.Tables("M_UsCabAsLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabAsLUE")

        Me.SLUCabAs.Properties.DataSource = DsMaster.Tables("M_UsCabAsLUE")
        Me.SLUCabAs.Properties.DisplayMember = "Cabang"
        Me.SLUCabAs.Properties.ValueMember = "CabID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqCIDD,ReqCID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Dos,Psg,Ditolak,Ket,BtlOrder,SisaKirim,stsKirim From T_ReqCDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ReqCID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqCDtl" & Gol)
        Try
            DsMaster.Tables("T_ReqCDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqCDtl" & Gol)

        DsMaster.Tables("T_ReqCDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqCDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ReqCDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqCID,PeriodID,CodeID,Tanggal,H.CabIDAs,C.Cabang As CabAs,H.GdIDAs,(Select Nama From M_Gudang where GdID=H.GdIDAs) as GdAs,H.CabIDTj,(Select Cabang From M_Cab where CabID=H.CabIDTj) as CabTj,H.GdIDTj,G.Nama as GdTj,G.Alamat, K.Nama as Kota,TotQty,TotDos,TotPsg,Ditolak,BtlOrder,SisaKirim,H.Ket,H.Grup,H.Gol,stsApp,stsKirim, stsBatal,H.InsDate,H.InsBy, H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_ReqC H Inner Join M_Cab C On H.CabIDAs=C.CabID Inner Join M_Gudang G On H.GdIDTj=G.GdID Inner Join M_Kota K On K.KotaID=G.KotaID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")  and (H.CabIDAs In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") or H.CabIDTj In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ")) Order By stsApp,ReqCID Asc,Tanggal desc,InsDate desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqC" & Gol)
        Try
            DsMaster.Tables("T_ReqC" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqC" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ReqC" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_ReqC")
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

    Private Sub FReqC_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Request Cabang Barang Jadi"
    End Sub

    Private Sub FReqC_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FReqCab_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        If BSave.Enabled = True Then
            e.Cancel = True
            Me.Focus()
            Exit Sub
        End If
        CekSave = False

        Me.Dispose()
    End Sub

    Private Sub FReqCab_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub


    Private Sub FReqCab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTReqC_e.Selected = True
    End Sub

    Private Sub BVTReqC_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTReqC_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Request Cabang Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("ReqCP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Request Cabang Barang Jadi"

        DsMaster.Clear()

        KodeTemp = MainModule.InisialAktif & Format(System.DateTime.Now, "yyMMddhhmmss")
        KdAktif = KodeTemp

        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TempStokBJCek")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
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
            End Try
        End With

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
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUGrup.EditValue = ""
        Me.SLUCabAs.EditValue = ""
        Me.SLUGdAs.EditValue = ""
        Me.SLUCabTj.EditValue = ""
        Me.SLUGdTj.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_ReqCDtl" & Gol).Clear()
        ReDim arrPar(-1)

        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn42.OptionsColumn.AllowEdit = False
        stsReject = False

        If MainModule.BackDate = True Then
            Me.DTPTanggal.Properties.ReadOnly = False
        Else
            Me.DTPTanggal.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Request Cabang Barang Jadi"

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDAs"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDTj"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        If SlCek("T_ReqC", "stsApp", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False And SlCek("T_ReqC", "stsKirim", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False And SlCek("T_ReqC", "stsBatal", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False Then

            'If Me.GridView2.GetFocusedDataRow.Item("stsApp") = False And Me.GridView2.GetFocusedDataRow.Item("stsKirim") = False And Me.GridView2.GetFocusedDataRow.Item("stsBatal") = False Then

            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
            KdAktif = Me.TBKode.EditValue
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.SLUCabAs.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabIDAs")
            Me.SLUCabTj.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabIDTj")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")

            If Not IsDBNull(Me.SLUCabAs.EditValue) Then
                Dim cmsl As SqlDataAdapter

                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabAs.EditValue & "' and Gol='" & Gol & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                Try
                    DsMaster.Tables("M_GudangLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
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
                Try
                    DsMaster.Tables("M_GudangTj" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GudangTj" & Gol)

                Me.SLUGdTj.Properties.DataSource = DsMaster.Tables("M_GudangTj" & Gol)
                Me.SLUGdTj.Properties.DisplayMember = "Nama"
                Me.SLUGdTj.Properties.ValueMember = "GdID"

                Me.SLUGdTj.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdIDTj")

            End If

            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

            FillDtl(Me.TBKode.EditValue)

            ReDim arrPar(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            Me.BCancel.Enabled = False
            CekSave = True

            Me.SLUCabTj.Properties.ReadOnly = True

            Me.GridColumn7.OptionsColumn.AllowEdit = True

            Me.GridColumn41.OptionsColumn.AllowEdit = False
            Me.GridColumn42.OptionsColumn.AllowEdit = False
            stsReject = False

            If MainModule.BackDate = True Then
                Me.DTPTanggal.Properties.ReadOnly = False
            Else
                Me.DTPTanggal.Properties.ReadOnly = True
            End If
        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove Atau Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBReject_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBReject.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Reject Request Cabang Barang Jadi"

        If MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDAs"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlOpBJGd(Me.GridView2.GetFocusedDataRow.Item("GdIDTj"), Me.GridView2.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        If SlCek("T_ReqC", "stsApp", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False And SlCek("T_ReqC", "stsKirim", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False Then
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
            KdAktif = Me.TBKode.EditValue
            Me.SLUGrup.EditValue = Me.GridView2.GetFocusedDataRow.Item("Grup")
            Me.SLUCabAs.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabIDAs")
            Me.SLUCabTj.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabIDTj")
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


            FillDtl(Me.TBKode.EditValue)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            Me.BCancel.Enabled = False
            CekSave = True

            Me.SLUCabTj.Properties.ReadOnly = True

            Me.GridColumn7.OptionsColumn.AllowEdit = False

            Me.GridColumn41.OptionsColumn.AllowEdit = True
            Me.GridColumn42.OptionsColumn.AllowEdit = True
            stsReject = True

        Else
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        If MainModule.SlAppReqC(Me.GridView2.GetFocusedDataRow.Item("InsDate")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Ada Dokumen Sebelumnya Yang Belum Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_ReqC", "stsBatal", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diapprove Karena Sudah Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            If MainModule.BackDate = True Then
                Dim cmSP As New SqlCommand("SPAppReqCBackDate")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdReqC", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
                    .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
                    .Parameters.Add("@CabIDAs", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabIDAs")
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdIDAs")
                    .Parameters.Add("@CabIDTj", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabIDTj")
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdIDTj")
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Gol")
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
                            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        Else
                            XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            Else
                Dim cmSP As New SqlCommand("SPAppReqC")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdReqC", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
                    .Parameters.Add("@CabIDAs", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabIDAs")
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdIDAs")
                    .Parameters.Add("@CabIDTj", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabIDTj")
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("GdIDTj")
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Gol")
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
                            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        Else
                            XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If


        End If

    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Request Cabang"

        If SlCek("T_ReqC", "stsKirim", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Membatalkan Sisa : " & Me.GridView2.GetFocusedDataRow.Item("ReqCID") & " Yang Belum Dikirim ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPBtlReqC")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
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
                        XtraMessageBox.Show("Sisa Order Request Cabang Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Sisa Order Request Cabang Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Request Cabang Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Request Cabang Barang Jadi"

        koneksi.Close()

        If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_ReqC", "stsApp", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False And SlCek("T_ReqC", "stsKirim", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False And SlCek("T_ReqC", "stsBatal", "ReqCID", Me.GridView2.GetFocusedDataRow.Item("ReqCID")) = False Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("ReqCID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_ReqC")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqCID")
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
        Else
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove Atau Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.LCIJns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ReqCID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("CabAs"), "CabAs")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("GdTj"), "GdTj")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "AlamatTj")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        If MainModule.PilihPrint = "SJ Konsinyasi" Then
            Dim XR2 As New XRReqCabSJ
            XR2.InitializeData(bind)
        Else
            Dim XR As New XRReqCab
            XR.InitializeData(bind)
        End If

    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_ReqC")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer
                'MsgBox(CodeID)
                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_ReqCDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 47
                                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqCIDD")
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KdAktif
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
                Dim cmSP As New SqlCommand("SPUpT_ReqC")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@CabIDAs", SqlDbType.VarChar).Value = Me.SLUCabAs.EditValue
                    .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                    .Parameters.Add("@CabIDTj", SqlDbType.VarChar).Value = Me.SLUCabTj.EditValue
                    .Parameters.Add("@GdIDTj", SqlDbType.VarChar).Value = Me.SLUGdTj.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Dos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Psg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Ditolak", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("Ditolak").SummaryText, Decimal), 2)
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
                            Dim cmSPDel As New SqlCommand("SPDelT_ReqCDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "ReqCIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ReqCDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 47
                                        .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqCIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@KodeTemp", SqlDbType.VarChar).Value = KdAktif
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
                                        Me.GridView1.SetRowCellValue(i, "ReqCIDD", Me.GridView1.GetRowCellValue(i, "ReqCIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_ReqCDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ReqCIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 47
                                        .Parameters.Add("@GdIDAs", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Dos")
                                        .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                        .Parameters.Add("@Ditolak", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Ditolak")
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

    Private Sub SLUCabAs_Leave(sender As Object, e As EventArgs) Handles SLUCabAs.Leave
        If Me.SLUCabAs.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter

            Try
                cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCabAs.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GdCabLUE" & Gol)
                Try
                    DsMaster.Tables("M_GdCabLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GdCabLUE" & Gol)

                Me.SLUGdAs.Properties.DataSource = DsMaster.Tables("M_GdCabLUE" & Gol)
                Me.SLUGdAs.Properties.DisplayMember = "Nama"
                Me.SLUGdAs.Properties.ValueMember = "GdID"

            Catch ex As Exception

            End Try

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ReqCIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If

    End Sub

    Private Sub SLUCabTj_Leave(sender As Object, e As EventArgs) Handles SLUCabTj.Leave
        If Me.SLUCabTj.Properties.ReadOnly = False Then
            Try
                Dim Reader As SqlClient.SqlDataReader
                Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=47 and Gol='" & Gol & "' and CabID='" & Me.SLUCabTj.EditValue & "'", koneksi)

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

                If Manual = True Then
                    Me.TBKode.Properties.ReadOnly = False
                    Me.TBKode.EditValue = ""
                Else
                    Me.TBKode.Properties.ReadOnly = True
                    Me.TBKode.EditValue = "--"
                End If

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
            Catch ex As Exception

            End Try
        End If


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
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ReqCIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If

            If Me.SLUGdAs.EditValue = Me.SLUGdTj.EditValue Then
                XtraMessageBox.Show("Gudang Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.SLUGdAs.EditValue = ""
            End If
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ReqCIDD")
        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            'MsgBox(Me.DTPTanggal.EditValue)
            'MsgBox(Me.SLUGdAs.EditValue)
            'MsgBox(Gol)
            Dim frm As New FReqCab_a(KdAktif, Me.DTPTanggal.EditValue, Me.SLUGdAs.EditValue, Gol)
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
            Dim Stok As Integer = 0

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdAs.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & KdAktif & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            'If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak") > Stok Then
            '    'XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            '    MsgBox(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak"))
            '    MsgBox(Stok)
            'End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ReqCIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak")
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

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak")) * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak"))
            End If

        ElseIf e.Column Is GridView1.Columns("Ditolak") Then
            Dim Stok As Integer = 0

            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Me.SLUGdAs.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & KdAktif & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGdAs.EditValue
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdAktif
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ReqCIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak")
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

            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak")) * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "SatID") = "P" Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", 0)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Ditolak"))
            End If

        End If

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If stsReject = False Then
            If Me.GridView1.OptionsBehavior.Editable = True Then
                Dim frm As New FSearch("StokBJ", Gol, "", "", Me.DTPTanggal.EditValue, "")
                frm.ShowDialog()

                Try
                    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtCode", dataTrans.Item("Kode").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatID", dataTrans.Item("SatID").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Isi", dataTrans.Item("Isi").ToString)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 1)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Ditolak", 0)
                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Ket", "")
                    End If

                Catch ex As Exception

                End Try

            End If
        End If
    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(e.RowHandle, "Ditolak", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")

                AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(e.RowHandle, "ReqCIDD", dataTrans.Item("Row" & rw).ToString)
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
            Dim frm As New FReqCab_d(Me.GridView2.GetFocusedDataRow.Item("ReqCID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        If Manual = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ReqCIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Me.SLUGrup.Properties.ReadOnly = False Then
            If Manual = False Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ReqCIDD")

                    Me.GridView1.DeleteRow(i)
                Next
            End If
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