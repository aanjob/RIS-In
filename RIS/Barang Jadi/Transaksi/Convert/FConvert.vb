Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FConvert
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim rw2 As Integer = 0
    Dim Masuk As Integer

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


    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ConvN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTConv_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUGd.Properties.ReadOnly = True
        Me.CBOJns.Properties.ReadOnly = True
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
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTConv_s.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.CBOJns.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTConv_e.Selected = True
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
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsMaster.Tables("M_UsCabLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabLUE")

        Me.SLUCab.Properties.DataSource = DsMaster.Tables("M_UsCabLUE")
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ConvIDD,ConvID,D.ArtCodeD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,Qty,Dos,Psg From T_ConvAs D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ConvID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ConvAs" & Gol)
        Try
            DsMaster.Tables("T_ConvAs" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ConvAs" & Gol)

        DsMaster.Tables("T_ConvAs" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ConvAs" & Gol).Columns("ArtCodeD"), DsMaster.Tables("T_ConvAs" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ConvAs" & Gol

        cmsl = New SqlDataAdapter("Select ConvIDD,ConvID,D.ArtCodeD,D.ArtCode,ArtName,D.SatID,D.Isi,IsiDlmDos,Qty,Dos,Psg From T_ConvTj D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where ConvID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_ConvTj" & Gol)
        Try
            DsMaster.Tables("T_ConvTj" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ConvTj" & Gol)

        DsMaster.Tables("T_ConvTj" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ConvTj" & Gol).Columns("ArtCodeD"), DsMaster.Tables("T_ConvTj" & Gol).Columns("ArtCode")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ConvTj" & Gol

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ConvID,PeriodID,CodeID,H.GdID,G.Nama as Gudang,Tanggal,Jns,H.Ket,H.CabID,Cb.Cabang,H.stsApp,H.Grup,H.Gol, H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_Conv H Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Cab Cb On H.CabID=Cb.CabID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")  and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By ConvID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_Conv" & Gol)
        Try
            DsMaster.Tables("T_Conv" & Gol).Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_Conv" & Gol)

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_Conv" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Conv")
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

    Private Sub FConvert_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Convert Barang Jadi"
    End Sub

    Private Sub FConvert_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()

        dataTrans2 = New Collection
        dataTrans2.Clear()
        CekSave = False

        Me.Dispose()
    End Sub

    Private Sub FConvert_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FConvert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTConv_e.Selected = True
    End Sub

    Private Sub BVTConv_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTConv_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Convert Barang Jadi"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ConvEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("ConvApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ConvDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("ConvP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Convert Barang Jadi"

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
        Me.SLUCab.EditValue = ""
        Me.CBOJns.EditValue = "Dos Ke Pasang"
        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        If Me.CBOJns.EditValue = "Dos Ke Pasang" Then
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = True
            Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = False
            Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_ConvAs" & Gol).Clear()
        DsMaster.Tables("T_ConvTj" & Gol).Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Convert Barang Jadi"

        ' Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True

        If MainModule.SlstsPeriodEdDel(Me.GridView3.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJGd(Me.GridView3.GetFocusedDataRow.Item("GdID"), Me.GridView3.GetFocusedDataRow.Item("Tanggal")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView3.GetFocusedDataRow.Item("ConvID")
        Me.SLUCab.EditValue = Me.GridView3.GetFocusedDataRow.Item("CabID")
        Me.SLUGrup.EditValue = Me.GridView3.GetFocusedDataRow.Item("Grup")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        Me.CBOJns.EditValue = Me.GridView3.GetFocusedDataRow.Item("Jns")
        Me.SLUGd.EditValue = Me.GridView3.GetFocusedDataRow.Item("GdID")
        Me.DTPTanggal.EditValue = Me.GridView3.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView3.GetFocusedDataRow.Item("Ket")

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=19 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        CodeID = ""
                    Else
                        Manual = Reader.Item(0)
                        CodeID = Reader.Item(1)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        FillDtl(Me.TBKode.EditValue)

        ReDim arrPar(-1)

        If Me.GridView1.RowCount > 0 Then
            'If Me.GridView2.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[ArtCodeD] ='" & Me.GridView1.GetFocusedRowCellValue("ArtCodeD") & "'"
            'End If

        End If

        If IsDBNull(Me.GridView3.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView3.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView3.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView3.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView3.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView3.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView3.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True
        Me.SLUCab.Properties.ReadOnly = True
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPAppConv")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView3.GetFocusedDataRow.Item("ConvID")
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
                ElseIf x = 1 Then
                    XtraMessageBox.Show("Data Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick

    End Sub
    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Convert Barang Jadi"

        koneksi.Close()

        If MainModule.SlOpBJGd(Me.GridView3.GetFocusedDataRow.Item("GdID"), Me.GridView3.GetFocusedDataRow.Item("Tanggal")) > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView3.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView3.GetFocusedDataRow.Item("ConvID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_Conv")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView3.GetFocusedDataRow.Item("ConvID")
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
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView2.RefreshData()
        Me.GridView2.ActiveFilter.Clear()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_Conv")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJns.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then

                                If i = 0 Then
                                    Masuk = 1
                                Else
                                    If Me.GridView1.GetRowCellValue(i, "ArtCodeD") = Me.GridView1.GetRowCellValue(i - 1, "ArtCodeD") Then
                                        Masuk = 0
                                    Else
                                        Masuk = 1
                                    End If
                                End If

                                Dim cmSPDtl As New SqlCommand("SPInsT_ConvAs")
                                cmSPDtl.CommandType = CommandType.StoredProcedure
                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 19
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@ArtCodeD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCodeD")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
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

                                Dim z : For z = 0 To GridView2.RowCount - 1
                                    If Not IsDBNull(Me.GridView2.GetRowCellValue(z, "ArtCode")) Then
                                        If Me.GridView1.GetRowCellValue(i, "ArtCodeD") = Me.GridView2.GetRowCellValue(z, "ArtCodeD") Then
                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_ConvTj")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 19
                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                                .Parameters.Add("@ArtCodeD", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ArtCodeD")
                                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ArtCode")
                                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "SatID")
                                                .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Isi")
                                                .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "IsiDlmDos")
                                                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Qty")
                                                .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Dos")
                                                .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Psg")
                                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With


                                            If Me.CBOJns.EditValue = "Pasang Ke Dos" Then
                                                If Masuk = 1 Then
                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl2.ExecuteNonQuery()
                                                        x = cmSPDtl2.Parameters("@Return").Value
                                                        .Close()
                                                    End With
                                                End If
                                            Else
                                                With koneksi
                                                    .Open()
                                                    cmSPDtl2.ExecuteNonQuery()
                                                    x = cmSPDtl2.Parameters("@Return").Value
                                                    .Close()
                                                End With
                                            End If

                                        End If
                                    End If
                                Next
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
                Dim cmSP As New SqlCommand("SPUpT_Conv")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJns.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_ConvAs")
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
                            If Me.GridView1.GetRowCellValue(i, "ConvIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then

                                    If i = 0 Then
                                        Masuk = 1
                                    Else
                                        If Me.GridView1.GetRowCellValue(i, "ArtCodeD") = Me.GridView1.GetRowCellValue(i - 1, "ArtCodeD") Then
                                            Masuk = 0
                                        Else
                                            Masuk = 1
                                        End If
                                    End If

                                    Dim cmSPDtl As New SqlCommand("SPInsT_ConvAs")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 19
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@ArtCodeD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCodeD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
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
                                        Me.GridView1.SetRowCellValue(i, "ConvIDD", Me.GridView1.GetRowCellValue(i, "ConvIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                    Dim z : For z = 0 To GridView2.RowCount - 1
                                        If Not IsDBNull(Me.GridView2.GetRowCellValue(z, "ArtCode")) Then
                                            If Me.GridView1.GetRowCellValue(i, "ArtCodeD") = Me.GridView2.GetRowCellValue(z, "ArtCodeD") Then

                                                Dim cmSPDtl2 As New SqlCommand("SPInsT_ConvTj")
                                                cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl2
                                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 19
                                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                                    .Parameters.Add("@ArtCodeD", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ArtCodeD")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ArtCode")
                                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "SatID")
                                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Isi")
                                                    .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "IsiDlmDos")
                                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Qty")
                                                    .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Dos")
                                                    .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Psg")
                                                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                    .Parameters.Add("@Return", SqlDbType.Int)
                                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                                    .Connection = koneksi
                                                End With

                                                If Me.CBOJns.EditValue = "Pasang Ke Dos" Then
                                                    If Masuk = 1 Then
                                                        With koneksi
                                                            .Open()
                                                            cmSPDtl2.ExecuteNonQuery()
                                                            x = cmSPDtl2.Parameters("@Return").Value
                                                            .Close()
                                                        End With
                                                    End If
                                                Else
                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl2.ExecuteNonQuery()
                                                        x = cmSPDtl2.Parameters("@Return").Value
                                                        .Close()
                                                    End With
                                                End If

                                                If x = 0 Then
                                                    Me.GridView2.SetRowCellValue(z, "ConvIDD", Me.GridView2.GetRowCellValue(z, "ConvIDD") * -1)
                                                ElseIf x = -1 Then
                                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Next

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then

                                    If i = 0 Then
                                        Masuk = 1
                                    Else
                                        'If Me.GridView1.GetRowCellValue(i, "ArtCode").ToString.Substring(0, 11) = Me.GridView1.GetRowCellValue(i - 1, "ArtCode").ToString.Substring(0, 11) Then
                                        If Me.GridView1.GetRowCellValue(i, "ArtCodeD") = Me.GridView1.GetRowCellValue(i - 1, "ArtCodeD") Then
                                            Masuk = 0
                                        Else
                                            Masuk = 1
                                        End If
                                    End If

                                    Dim cmSPDtl As New SqlCommand("SPUpT_ConvAs")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ConvIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 19
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@ArtCodeD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCodeD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
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

                                    If x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If

                                Dim z : For z = 0 To GridView2.RowCount - 1
                                    If Not IsDBNull(Me.GridView2.GetRowCellValue(z, "ArtCode")) Then
                                        'If Me.GridView1.GetRowCellValue(i, "ArtCode").ToString.Substring(0, 11) = Me.GridView2.GetRowCellValue(z, "ArtCode").ToString.Substring(0, 11) Then
                                        If Me.GridView1.GetRowCellValue(i, "ArtCodeD") = Me.GridView2.GetRowCellValue(z, "ArtCodeD") Then

                                            Dim cmSPDtl2 As New SqlCommand("SPUpT_ConvTj")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ConvIDD")
                                                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 19
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                                .Parameters.Add("@ArtCodeD", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ArtCodeD")
                                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "ArtCode")
                                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(z, "SatID")
                                                .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Isi")
                                                .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "IsiDlmDos")
                                                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Qty")
                                                .Parameters.Add("@Dos", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Dos")
                                                .Parameters.Add("@Psg", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(z, "Psg")
                                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With

                                            If Me.CBOJns.EditValue = "Pasang Ke Dos" Then
                                                If Masuk = 1 Then
                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl2.ExecuteNonQuery()
                                                        x = cmSPDtl2.Parameters("@Return").Value
                                                        .Close()
                                                    End With
                                                End If
                                            Else
                                                With koneksi
                                                    .Open()
                                                    cmSPDtl2.ExecuteNonQuery()
                                                    x = cmSPDtl2.Parameters("@Return").Value
                                                    .Close()
                                                End With
                                            End If

                                            If x = -1 Then
                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If
                                        End If

                                    End If
                                Next
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

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Me.SLUGrup.Properties.ReadOnly = False Then
            If Me.SLUCab.EditValue <> "" And Not IsDBNull(Me.SLUCab.EditValue) Then
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
                Try
                    DsMaster.Tables("M_GudangLUE" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

                Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
                Me.SLUGd.Properties.DisplayMember = "Nama"
                Me.SLUGd.Properties.ValueMember = "GdID"

                Me.SLUGd.EditValue = DsMaster.Tables("M_GdCabLUE" & Gol).Select("Def='True'")(0).Item("GdID")

                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ConvIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                Dim x : For x = Me.GridView2.RowCount - 1 To 0 Step -1
                    Me.GridView2.DeleteRow(x)
                Next

                Me.GridView2.ActiveFilter.Clear()
            End If
        End If

    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            If Me.CBOJns.EditValue = "Dos Ke Pasang" Then
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ConvIDD")

                Dim i : For i = Me.GridView2.RowCount - 1 To 0 Step -1
                    If Me.GridView2.GetRowCellValue(i, "ArtCodeD") = Me.GridView1.GetFocusedDataRow.Item("ArtCodeD") Then

                        Me.GridView2.DeleteRow(i)
                    End If
                Next
            End If

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            If Me.CBOJns.EditValue = "Dos Ke Pasang" Then
                Dim frm As New FConvertDP_a(Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue, Gol)
                frm.ShowDialog()
            Else

                Dim frm As New FConvertPD_a(Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, Me.TBKode.EditValue, Gol)
                frm.ShowDialog()
            End If

            rw = 0
            rw2 = 0

            Try
                If Not IsDBNull(dataTrans2.Item("Baris").ToString) Then
                    Dim i : For i = 0 To CInt(dataTrans2.Item("Baris").ToString) - 1
                        Me.GridView2.AddNewRow()
                    Next
                End If

                If Not IsDBNull(dataTrans.Item("Baris").ToString) Then
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

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Me.GridView2.RefreshData()
        Try
            If Me.GridView1.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[ArtCodeD] ='" & Me.GridView1.GetFocusedRowCellValue("ArtCodeD") & "'"

                'If Me.GridView2.RowCount > 0 Then
                'Me.GridView2.ActiveFilterString = "[ArtCode] Like '" & Me.GridView1.GetFocusedRowCellValue("ArtCode").ToString.Substring(0, 11) & "%'"
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            Me.GridView1.SetRowCellValue(e.RowHandle, "ConvIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCodeD", dataTrans.Item("ArtCodeD" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SatID", dataTrans.Item("SatID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Isi", dataTrans.Item("Isi" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "IsiDlmDos", dataTrans.Item("IsiDlmDos" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", dataTrans.Item("Qty" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Dos", dataTrans.Item("Dos" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", dataTrans.Item("Psg" & rw).ToString)

            rw += 1

            If Me.GridView1.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[ArtCodeD] ='" & Me.GridView1.GetFocusedRowCellValue("ArtCodeD") & "'"

                'If Me.GridView2.RowCount > 0 Then
                'Me.GridView2.ActiveFilterString = "[ArtCode] Like '" & Me.GridView1.GetFocusedRowCellValue("ArtCode").ToString.Substring(0, 11) & "%'"
                'End If
            End If
        Catch ex As Exception
            Me.GridView1.DeleteRow(e.RowHandle)
        End Try

    End Sub
    Private Sub GridControl2_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl2.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            If Me.CBOJns.EditValue = "Pasang Ke Dos" Then
                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    'If Me.GridView1.GetRowCellValue(i, "ArtCode") Like Me.GridView2.GetFocusedDataRow.Item("ArtCode").ToString.Substring(0, 11) & "%'" Then
                    If Me.GridView2.GetRowCellValue(i, "ArtCodeD") = Me.GridView1.GetFocusedDataRow.Item("ArtCodeD") & "'" Then

                        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ConvIDD")

                        Me.GridView1.DeleteRow(i)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub GridView2_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView2.InitNewRow
        Try
            Me.GridView2.SetRowCellValue(e.RowHandle, "ConvIDD", Me.GridView2.RowCount * 1)
            Me.GridView2.SetRowCellValue(e.RowHandle, "ArtCodeD", dataTrans2.Item("ArtCodeD" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans2.Item("ArtCode" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "ArtName", dataTrans2.Item("ArtName" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "SatID", dataTrans2.Item("SatID" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "Isi", dataTrans2.Item("Isi" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "IsiDlmDos", dataTrans2.Item("IsiDlmDos" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "Qty", dataTrans2.Item("Qty" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "Dos", dataTrans2.Item("Dos" & rw2).ToString)
            Me.GridView2.SetRowCellValue(e.RowHandle, "Psg", dataTrans2.Item("Psg" & rw2).ToString)
            rw2 += 1

            If Me.GridView1.RowCount > 0 Then
                'If Me.GridView2.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[ArtCodeD] ='" & Me.GridView1.GetFocusedRowCellValue("ArtCodeD") & "'"
                'Me.GridView2.ActiveFilterString = "[ArtCode] Like '" & Me.GridView1.GetFocusedRowCellValue("ArtCode").ToString.Substring(0, 11) & "%'"
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView3_DoubleClick(sender As Object, e As EventArgs) Handles GridView3.DoubleClick
        Try
            Dim frm As New FConvert_d(Me.GridView3.GetFocusedDataRow.Item("ConvID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CBOJns_Leave(sender As Object, e As EventArgs) Handles CBOJns.Leave
        If Me.CBOJns.Properties.ReadOnly = False Then
            If Me.CBOJns.EditValue = "Dos Ke Pasang" Then
                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = True
                Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Visible = False
            Else
                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Visible = False
                Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Visible = True
            End If

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ConvIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView2.RowCount - 1 To 0 Step -1
                Me.GridView2.DeleteRow(x)
            Next

            Me.GridView2.ActiveFilter.Clear()
        End If
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Me.SLUGd.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ConvIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView2.RowCount - 1 To 0 Step -1
                Me.GridView2.DeleteRow(x)
            Next

            Me.GridView2.ActiveFilter.Clear()
        End If

    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ConvIDD")

            Me.GridView1.DeleteRow(i)
        Next

        Dim x : For x = Me.GridView2.RowCount - 1 To 0 Step -1
            Me.GridView2.DeleteRow(x)
        Next

        Me.GridView2.ActiveFilter.Clear()
    End Sub


    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.Properties.ReadOnly = False Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=19 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select G.GdID,Nama,Def From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID='" & Me.SLUCab.EditValue & "' and Grup='" & Me.SLUGrup.EditValue & "' and G.Aktif='True' and CG.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
            Try
                DsMaster.Tables("M_GudangLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

            Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
            Me.SLUGd.Properties.DisplayMember = "Nama"
            Me.SLUGd.Properties.ValueMember = "GdID"

            Me.SLUGd.EditValue = DsMaster.Tables("M_GudangLUE" & Gol).Select("Def='True'")(0).Item("GdID")

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ConvIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView2.RowCount - 1 To 0 Step -1
                Me.GridView2.DeleteRow(x)
            Next

            Me.GridView2.ActiveFilter.Clear()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class