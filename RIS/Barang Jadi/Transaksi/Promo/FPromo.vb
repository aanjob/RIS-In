Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPromo
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), arrPar2(-1), arrPar3(-1) As String
    Dim CodeID, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim JT, IdD As Integer
    Dim rw As Integer = 0

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=35 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("PrN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("PrEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("PrDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTPromo_s.Enabled = True

        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJnsPromo.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.CBOPot.Properties.ReadOnly = True
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False
        Me.GridView4.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPromo_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJnsPromo.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.CBOPot.Properties.ReadOnly = False
        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True
        Me.GridView4.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPromo_e.Selected = True
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PromoIDD,PromoID,Paket,JnsCust,JnsPerhit,Kelipatan,BeliMin,BeliMax,JnsPot,Pot From T_PromoDtl Where PromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PromoDtl" & Gol)
        Try
            DsMaster.Tables("T_PromoDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PromoDtl" & Gol)

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_PromoDtl" & Gol

        cmsl = New SqlDataAdapter("Select PromoIDD,PromoIDDtl,PromoID,Paket,D.ArtCode,ArtName From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where PromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PromoDtl2" & Gol)
        Try
            DsMaster.Tables("T_PromoDtl2" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PromoDtl2" & Gol)

        DsMaster.Tables("T_PromoDtl2" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_PromoDtl2" & Gol).Columns("PromoID"), DsMaster.Tables("T_PromoDtl2" & Gol).Columns("Paket"), DsMaster.Tables("T_PromoDtl2" & Gol).Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_PromoDtl2" & Gol

        cmsl = New SqlDataAdapter("Select PromoIDD,PromoIDDtl,PromoID,Paket,D.ArtCode,ArtName From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where PromoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_PromoFree" & Gol)
        Try
            DsMaster.Tables("T_PromoFree" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_PromoFree" & Gol)

        DsMaster.Tables("T_PromoFree" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_PromoFree" & Gol).Columns("PromoID"), DsMaster.Tables("T_PromoFree" & Gol).Columns("Paket"), DsMaster.Tables("T_PromoFree" & Gol).Columns("ArtCode")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_PromoFree" & Gol

         If Me.GridView1.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            Me.GridView4.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
        End If
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PromoID,CodeID,Tanggal,Metod,Nama,TglAwal,TglAkhir,DiperhitSaat,Retur,InsDate,InsBy,UpdDate,UpdBy From T_Promo Where Year (Tanggal)=" & MainModule.periodeTahun & " and Gol='" & Gol & "' Order By Tanggal,PromoID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_Promo" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_Promo" & Gol)
        DsMaster.Tables("T_Promo" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_Promo" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_Promo" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Promo")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
    End Sub

    Private Sub FPromo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Promo"
    End Sub

    Private Sub FPromo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FPromo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FPromo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTPromo_e.Selected = True

        If CBOJnsPromo.EditValue = "Gratis Barang" Then
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub BVTPromo_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPromo_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Promo"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Promo"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPAwal.EditValue = Date.Now
            Me.DTPAkhir.EditValue = Date.Now
        End If

        OpenControl()
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If


        Me.CBOJnsPromo.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.CBOPot.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_PromoDtl" & Gol).Clear()
        DsMaster.Tables("T_PromoDtl2" & Gol).Clear()
        DsMaster.Tables("T_PromoFree" & Gol).Clear()

        ReDim arrPar(-1)
        ReDim arrPar2(-1)
        ReDim arrPar3(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Promo"

        'If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
        '    XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If MainModule.SlPromo(Me.GridView2.GetFocusedDataRow.Item("PromoID")) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Indicator = "200"

        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("PromoID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPAwal.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglAwal")
        Me.DTPAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglAkhir")
        Me.CBOJnsPromo.EditValue = Me.GridView2.GetFocusedDataRow.Item("Metod")
        Me.TBNama.EditValue = Me.GridView2.GetFocusedDataRow.Item("Nama")
        Me.CBOPot.EditValue = Me.GridView2.GetFocusedDataRow.Item("DiperhitSaat")
        Me.CERetur.EditValue = Me.GridView2.GetFocusedDataRow.Item("Retur")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)
        ReDim arrPar3(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        If CBOJnsPromo.EditValue = "Gratis Barang" Then
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Me.GridView1.RowCount > 0 Then
            If Me.GridView3.RowCount > 0 Then
                Me.GridView3.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            End If

            If Me.GridView4.RowCount > 0 Then
                Me.GridView4.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            End If
        End If
        'End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Promo"

        koneksi.Close()

        If MainModule.SlPromo(Me.GridView2.GetFocusedDataRow.Item("PromoID")) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("PromoID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_Promo")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("PromoID")
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

        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()
        Me.GridView4.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_Promo")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Metod", SqlDbType.VarChar).Value = Me.CBOJnsPromo.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@DiperhitSaat", SqlDbType.VarChar).Value = Me.CBOPot.EditValue
                    .Parameters.Add("@Retur", SqlDbType.Bit).Value = Me.CERetur.EditValue
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Paket")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_PromoDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Paket")
                                    .Parameters.Add("@JnsCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsCust")
                                    .Parameters.Add("@JnsPerhit", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPerhit")
                                    .Parameters.Add("@Kelipatan", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "Kelipatan")
                                    .Parameters.Add("@Min", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BeliMin")
                                    .Parameters.Add("@Max", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BeliMax")
                                    .Parameters.Add("@JnsPot", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPot")
                                    .Parameters.Add("@Pot", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pot")
                                    .Parameters.Add("@Return", SqlDbType.Int)
                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                    .Parameters.Add("@IdD", SqlDbType.Int)
                                    .Parameters("@IdD").Direction = ParameterDirection.Output
                                    .Connection = koneksi
                                End With

                                With koneksi
                                    .Open()
                                    cmSPDtl.ExecuteNonQuery()
                                    x = cmSPDtl.Parameters("@Return").Value
                                    IdD = cmSPDtl.Parameters("@IdD").Value

                                    .Close()
                                End With

                                If x <> 0 Then
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If

                                Dim z : For z = 0 To GridView3.RowCount - 1
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "Paket")) Then
                                        If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView3.GetRowCellValue(z, "PromoIDDtl") Then
                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_PromoDtl2")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = IdD
                                                .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Paket")
                                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With

                                            With koneksi
                                                .Open()
                                                cmSPDtl2.ExecuteNonQuery()
                                                x = cmSPDtl2.Parameters("@Return").Value
                                                .Close()
                                            End With

                                            If x <> 0 Then
                                                Del()
                                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If
                                        End If


                                    End If
                                Next

                                If Me.CBOJnsPromo.EditValue = "Gratis Barang" Then
                                    Dim a : For a = 0 To GridView4.RowCount - 1
                                        If Not IsDBNull(Me.GridView4.GetRowCellValue(a, "Paket")) Then
                                            If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView4.GetRowCellValue(a, "PromoIDDtl") Then
                                                Dim cmSPDtl3 As New SqlCommand("SPInsT_PromoFree")
                                                cmSPDtl3.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl3
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = IdD
                                                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "Paket")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "ArtCode")
                                                    .Parameters.Add("@Return", SqlDbType.Int)
                                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                                    .Connection = koneksi
                                                End With

                                                With koneksi
                                                    .Open()
                                                    cmSPDtl3.ExecuteNonQuery()
                                                    x = cmSPDtl3.Parameters("@Return").Value
                                                    .Close()
                                                End With

                                                If x <> 0 Then
                                                    Del()
                                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Next
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
                Dim cmSP As New SqlCommand("SPUpT_Promo")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Metod", SqlDbType.VarChar).Value = Me.CBOJnsPromo.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@DiperhitSaat", SqlDbType.VarChar).Value = Me.CBOPot.EditValue
                    .Parameters.Add("@Retur", SqlDbType.Bit).Value = Me.CERetur.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_PromoDtl")
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_PromoDtl2")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(q)
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
                            If Me.GridView1.GetRowCellValue(i, "PromoIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Paket")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_PromoDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Paket")
                                        .Parameters.Add("@JnsCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsCust")
                                        .Parameters.Add("@JnsPerhit", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPerhit")
                                        .Parameters.Add("@Kelipatan", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "Kelipatan")
                                        .Parameters.Add("@Min", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BeliMin")
                                        .Parameters.Add("@Max", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BeliMax")
                                        .Parameters.Add("@JnsPot", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPot")
                                        .Parameters.Add("@Pot", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pot")
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Parameters.Add("@IdD", SqlDbType.Int)
                                        .Parameters("@IdD").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        IdD = cmSPDtl.Parameters("@IdD").Value
                                        .Close()
                                    End With

                                    If x = 0 Then
                                        Me.GridView1.SetRowCellValue(i, "POIDD", Me.GridView1.GetRowCellValue(i, "POIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "PromoIDD") < 0 Then
                                            If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView3.GetRowCellValue(z, "PromoIDDtl") Then

                                                Dim cmSPDtl2 As New SqlCommand("SPInsT_PromoDtl2")
                                                cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl2
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = IdD
                                                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Paket")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                    .Parameters.Add("@Return", SqlDbType.Int)
                                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                                    .Connection = koneksi
                                                End With

                                                With koneksi
                                                    .Open()
                                                    cmSPDtl2.ExecuteNonQuery()
                                                    x = cmSPDtl2.Parameters("@Return").Value
                                                    .Close()
                                                End With

                                                If x = 0 Then
                                                    Me.GridView3.SetRowCellValue(z, "PromoIDD", Me.GridView3.GetRowCellValue(z, "PromoIDD") * -1)
                                                Else
                                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If

                                            End If
                                        Else
                                            If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView3.GetRowCellValue(z, "PromoIDDtl") Then
                                                Dim cmSPDtl2 As New SqlCommand("SPUpT_PromoDtl2")
                                                cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl2
                                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "PromoIDD")
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = IdD
                                                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Paket")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                    .Parameters.Add("@Return", SqlDbType.Int)
                                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                                    .Connection = koneksi
                                                End With

                                                With koneksi
                                                    .Open()
                                                    cmSPDtl2.ExecuteNonQuery()
                                                    x = cmSPDtl2.Parameters("@Return").Value
                                                    .Close()
                                                End With

                                                If x <> 0 Then
                                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Next

                                    If Me.CBOJnsPromo.EditValue = "Gratis Barang" Then
                                        Dim a : For a = 0 To GridView4.RowCount - 1
                                            If Me.GridView4.GetRowCellValue(a, "PromoIDD") < 0 Then
                                                If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView4.GetRowCellValue(a, "PromoIDDtl") Then

                                                    Dim cmSPDtl3 As New SqlCommand("SPInsT_PromoFree")
                                                    cmSPDtl3.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl3
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "Paket")
                                                        .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = IdD
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "ArtCode")
                                                        .Parameters.Add("@Return", SqlDbType.Int)
                                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                                        .Connection = koneksi
                                                    End With

                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl3.ExecuteNonQuery()
                                                        x = cmSPDtl3.Parameters("@Return").Value
                                                        .Close()
                                                    End With

                                                    If x = 0 Then
                                                        Me.GridView4.SetRowCellValue(a, "PromoIDD", Me.GridView4.GetRowCellValue(a, "PromoIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            Else
                                                If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView4.GetRowCellValue(a, "PromoIDDtl") Then

                                                    Dim cmSPDtl3 As New SqlCommand("SPUpT_PromoFree")
                                                    cmSPDtl3.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl3
                                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(a, "PromoIDD")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = IdD
                                                        .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "Paket")
                                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "ArtCode")
                                                        .Parameters.Add("@Return", SqlDbType.Int)
                                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                                        .Connection = koneksi
                                                    End With

                                                    With koneksi
                                                        .Open()
                                                        cmSPDtl3.ExecuteNonQuery()
                                                        x = cmSPDtl3.Parameters("@Return").Value
                                                        .Close()
                                                    End With

                                                    If x <> 0 Then
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If

                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Paket")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_PromoDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PromoIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Paket")
                                        .Parameters.Add("@JnsCust", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsCust")
                                        .Parameters.Add("@JnsPerhit", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPerhit")
                                        .Parameters.Add("@Kelipatan", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "Kelipatan")
                                        .Parameters.Add("@Min", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BeliMin")
                                        .Parameters.Add("@Max", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BeliMax")
                                        .Parameters.Add("@JnsPot", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JnsPot")
                                        .Parameters.Add("@Pot", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pot")
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
                                        XtraMessageBox.Show("Qty Harus Lebih Besar Dari 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub

                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If

                                Dim z : For z = 0 To GridView3.RowCount - 1
                                    If Me.GridView3.GetRowCellValue(z, "PromoIDD") < 0 Then
                                        If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView3.GetRowCellValue(z, "PromoIDDtl") Then

                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_PromoDtl2")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PromoIDD")
                                                .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Paket")
                                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With

                                            With koneksi
                                                .Open()
                                                cmSPDtl2.ExecuteNonQuery()
                                                x = cmSPDtl2.Parameters("@Return").Value
                                                .Close()
                                            End With

                                            If x = 0 Then
                                                Me.GridView3.SetRowCellValue(z, "PromoIDD", Me.GridView3.GetRowCellValue(z, "PromoIDD") * -1)
                                            Else
                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                        End If
                                    Else
                                        If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView3.GetRowCellValue(z, "PromoIDDtl") Then
                                            Dim cmSPDtl2 As New SqlCommand("SPUpT_PromoDtl2")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "PromoIDD")
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PromoIDD")
                                                .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Paket")
                                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                                .Parameters.Add("@Return", SqlDbType.Int)
                                                .Parameters("@Return").Direction = ParameterDirection.Output
                                                .Connection = koneksi
                                            End With

                                            With koneksi
                                                .Open()
                                                cmSPDtl2.ExecuteNonQuery()
                                                x = cmSPDtl2.Parameters("@Return").Value
                                                .Close()
                                            End With

                                            If x <> 0 Then
                                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                Next

                                If Me.CBOJnsPromo.EditValue = "Gratis Barang" Then
                                    Dim a : For a = 0 To GridView4.RowCount - 1
                                        If Me.GridView4.GetRowCellValue(a, "PromoIDD") < 0 Then
                                            If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView4.GetRowCellValue(a, "PromoIDDtl") Then

                                                Dim cmSPDtl3 As New SqlCommand("SPInsT_PromoFree")
                                                cmSPDtl3.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl3
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "Paket")
                                                    .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PromoIDD")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "ArtCode")
                                                    .Parameters.Add("@Return", SqlDbType.Int)
                                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                                    .Connection = koneksi
                                                End With

                                                With koneksi
                                                    .Open()
                                                    cmSPDtl3.ExecuteNonQuery()
                                                    x = cmSPDtl3.Parameters("@Return").Value
                                                    .Close()
                                                End With

                                                If x = 0 Then
                                                    Me.GridView4.SetRowCellValue(a, "PromoIDD", Me.GridView4.GetRowCellValue(a, "PromoIDD") * -1)
                                                Else
                                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        Else
                                            If Me.GridView1.GetRowCellValue(i, "PromoIDD") = Me.GridView4.GetRowCellValue(a, "PromoIDDtl") Then

                                                Dim cmSPDtl3 As New SqlCommand("SPUpT_PromoFree")
                                                cmSPDtl3.CommandType = CommandType.StoredProcedure

                                                With cmSPDtl3
                                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(a, "PromoIDD")
                                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                    .Parameters.Add("@PromoIDDtl", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "PromoIDD")
                                                    .Parameters.Add("@Paket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "Paket")
                                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(a, "ArtCode")
                                                    .Parameters.Add("@Return", SqlDbType.Int)
                                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                                    .Connection = koneksi
                                                End With

                                                With koneksi
                                                    .Open()
                                                    cmSPDtl3.ExecuteNonQuery()
                                                    x = cmSPDtl3.Parameters("@Return").Value
                                                    .Close()
                                                End With

                                                If x <> 0 Then
                                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    Next

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

    Private Sub BEdArtCode_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdArtCode.ButtonClick
        Dim frm As New FSearch("M_Brg", Gol, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdArtCode2_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdArtCode2.ButtonClick
        Dim frm As New FSearch("M_Brg", Gol, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView4.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "ArtName", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdArtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdArtCode.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdArtCode2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdArtCode2.KeyPress
        e.Handled = True
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("PromoIDD")
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Paket") Then
            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "PromoIDDtl") = Me.GridView1.GetRowCellValue(e.RowHandle, "PromoIDD") Then
                    Me.GridView3.SetRowCellValue(i, "Paket", Me.GridView1.GetRowCellValue(e.RowHandle, "Paket"))
                End If
            Next
        End If
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            Me.GridView1.SetRowCellValue(e.RowHandle, "PromoIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "PromoID", Me.TBKode.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Paket", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Kelipatan", False)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BeliMin", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BeliMax", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Pot", 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged

        If Me.GridView1.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"

            Me.GridView4.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPromo_d(Me.GridView2.GetFocusedDataRow.Item("PromoID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("PromoIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            Dim frm As New FPromo_a(Gol)
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("Baris").ToString) Then
                    Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                        If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                            Me.GridView3.AddNewRow()
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try
            Me.GridView3.SetRowCellValue(e.RowHandle, "PromoIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "PromoID", Me.TBKode.EditValue)
            Me.GridView3.SetRowCellValue(e.RowHandle, "PromoIDDtl", Me.GridView1.GetFocusedDataRow.Item("PromoIDD"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "Paket", Me.GridView1.GetFocusedDataRow.Item("Paket"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
            rw += 1

            'If Me.GridView1.RowCount > 0 Then
            'If Me.GridView3.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            'End If
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridControl4_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl4.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar3(arrPar3.GetUpperBound(0) + 1)
            arrPar3(arrPar3.GetUpperBound(0)) = Me.GridView4.GetFocusedDataRow.Item("PromoIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            Dim frm As New FPromo_a(Gol)
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("Baris").ToString) Then
                    Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                        If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                            Me.GridView4.AddNewRow()
                        End If
                    Next
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub GridView4_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView4.InitNewRow
        Try
            Me.GridView4.SetRowCellValue(e.RowHandle, "PromoIDD", Me.GridView4.RowCount * -1)
            Me.GridView4.SetRowCellValue(e.RowHandle, "PromoID", Me.TBKode.EditValue)
            Me.GridView4.SetRowCellValue(e.RowHandle, "PromoIDDtl", Me.GridView1.GetFocusedDataRow.Item("PromoIDD"))
            Me.GridView4.SetRowCellValue(e.RowHandle, "Paket", Me.GridView1.GetFocusedDataRow.Item("Paket"))
            Me.GridView4.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
            Me.GridView4.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
            rw += 1

            'If Me.GridView1.RowCount > 0 Then
            'If Me.GridView4.RowCount > 0 Then
            Me.GridView4.ActiveFilterString = "[PromoIDDtl] = '" & Me.GridView1.GetFocusedRowCellValue("PromoIDD") & "'"
            'End If
            'End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CBOJnsPromo_Leave(sender As Object, e As EventArgs) Handles CBOJnsPromo.Leave
        If CBOJnsPromo.EditValue = "Gratis Barang" Then
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class