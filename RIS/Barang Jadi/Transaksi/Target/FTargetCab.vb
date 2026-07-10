Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTargetCab
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim rw As Integer = 0
    'Dim Jan, Feb, Mar, Apr, Mei, Jun, Jul, Agt, Sep, Okt, Nov, Des As Decimal

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrgtCbN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTTarget_s.Enabled = True

        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUSubJns.Properties.ReadOnly = True
        Me.SLUTahun.Properties.ReadOnly = True

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
        Me.BVBExit.Enabled = False
        Me.BVTTarget_s.Enabled = False

        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUSubJns.Properties.ReadOnly = False
        Me.SLUTahun.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTarget_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
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

        cmsl = New SqlDataAdapter("Select Distinct SubJns From M_Brg where SubJns<>'' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SubJnsLUE")
        cmsl.Fill(DsMaster, "M_SubJnsLUE")
        DsMaster.Tables("M_SubJnsLUE").Clear()
        cmsl.Fill(DsMaster, "M_SubJnsLUE")
        DsMaster.Tables("M_SubJnsLUE").Rows.Add("%")

        Me.SLUSubJns.Properties.DataSource = DsMaster.Tables("M_SubJnsLUE")
        Me.SLUSubJns.Properties.DisplayMember = "SubJns"
        Me.SLUSubJns.Properties.ValueMember = "SubJns"
    End Sub

    Public Sub FillDtl(Kode As String, CabID As String, SubJns As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TargetIDD,TargetIDDtl,Tahun,CabID,D.SalID,S.Nama As Sales,SubJns,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov, Dess,D.InsDate,D.InsBy,D.UpdDate,D.UpdBy From T_TargetDtl2 D Inner Join M_Sales S On S.SalID=D.SalID where Tahun='" & Kode & "' and CabID='" & CabID & "' And SubJns='" & SubJns & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetDtl2")
        Try
            DsMaster.Tables("T_TargetDtl2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TargetDtl2")

        DsMaster.Tables("T_TargetDtl2").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TargetDtl2").Columns("Tahun"), DsMaster.Tables("T_TargetDtl2").Columns("CabID"), DsMaster.Tables("T_TargetDtl2").Columns("SalID"), DsMaster.Tables("T_TargetDtl2").Columns("SubJns")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TargetDtl2"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TargetIDD,TargetIDDtl,Tahun,D.CabID,Cabang,D.SalID,S.Nama As Sales,SubJns,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt, Sep, Okt,Nov,Dess,D.InsDate,D.InsBy,D.UpdDate,D.UpdBy From T_TargetDtl2 D Inner Join M_Cab Cb On Cb.CabID=D.CabID Inner Join M_Sales S On S.SalID=D.SalID Where Tahun='" & MainModule.periodeTahun & "' and D.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "TargetCabView")
        Try
            DsMaster.Tables("TargetCabView").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "TargetCabView")

        DsMaster.Tables("TargetCabView").PrimaryKey = New DataColumn() {DsMaster.Tables("TargetCabView").Columns("Tahun"), DsMaster.Tables("TargetCabView").Columns("CabID"), DsMaster.Tables("TargetCabView").Columns("SalID"), DsMaster.Tables("TargetCabView").Columns("SubJns")}

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "TargetCabView"
    End Sub

    Public Sub FillTargetCab(Tahun As Integer, CabID As String, SubJns As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SubJns,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess From T_TargetDtl Where Tahun=" & Tahun & " and CabID='" & CabID & "' and SubJns='" & SubJns & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetDtl")
        cmsl.Fill(DsMaster, "T_TargetDtl")
        DsMaster.Tables("T_TargetDtl").Clear()
        cmsl.Fill(DsMaster, "T_TargetDtl")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_TargetDtl"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TargetDtl2")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.SLUTahun.EditValue
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.SLUTahun.EditValue

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

    Public Sub CekEditKolom()
        Dim Obj As Object

        Dim i : For i = 1 To 12
            If SlTrans(Me.SLUCab.EditValue, i, Me.SLUTahun.Text) > 0 Then
                Me.GridView1.Columns(i + 5).OptionsColumn.AllowEdit = False
            Else
                Me.GridView1.Columns(i + 5).OptionsColumn.AllowEdit = True
            End If
        Next
    End Sub


    Private Sub FTargetCab_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Target Penjualan"
    End Sub

    Private Sub FTargetCab_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub FTargetCab_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FTargetCab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTarget_e.Selected = True
    End Sub


    Private Sub BVTTarget_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTarget_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Target Penjualan"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrgtCbEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrgtCbDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Target Penjualan"

        DsMaster.Clear()

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        Me.SLUTahun.EditValue = MainModule.periodeTahun
        Me.SLUCab.EditValue = ""

        Me.TBInfo.EditValue = ""
        FillDtl(MainModule.periodeTahun, "", "")
        FillTargetCab(MainModule.periodeTahun, "", "")

        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Target Penjualan"

        LUE()

        Indicator = "200"

        Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")
        Me.SLUSubJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("SubJns")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct TargetIDD,H.Tahun From T_Target H Inner Join T_TargetDtl D On H.Tahun=D.Tahun and H.SubJns=D.SubJns Where Aktif ='True' and CabID='" & Me.SLUCab.EditValue & "' and H.SubJns='" & Me.SLUSubJns.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "TargetLUE")
        Try
            DsMaster.Tables("TargetLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "TargetLUE")

        Me.SLUTahun.Properties.DataSource = DsMaster.Tables("TargetLUE")
        Me.SLUTahun.Properties.DisplayMember = "Tahun"
        Me.SLUTahun.Properties.ValueMember = "TargetIDD"

        Me.SLUTahun.EditValue = Me.GridView2.GetFocusedDataRow.Item("TargetIDDtl")

        cmsl = New SqlDataAdapter("Select Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess From T_TargetDtl Where Tahun=" & Me.SLUTahun.Text & " and CabID='" & SLUCab.EditValue & "' and SubJns='" & Me.SLUSubJns.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetDtl")
        cmsl.Fill(DsMaster, "T_TargetDtl")
        DsMaster.Tables("T_TargetDtl").Clear()
        cmsl.Fill(DsMaster, "T_TargetDtl")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_TargetDtl"

        FillDtl(Me.SLUTahun.Text, Me.SLUCab.EditValue, Me.SLUSubJns.EditValue)
        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekEditKolom()

        CekSave = True
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Target Penjualan"

        koneksi.Close()

        If MainModule.SlTarget(Me.GridView2.GetFocusedDataRow.Item("Tahun")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("Tahun") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_Target")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.GridView2.GetFocusedDataRow.Item("Tahun")
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

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        'CekTarget()

        Dim z : For z = 1 To 12
            'MsgBox(i & ":" & GridView1.Columns(i + 5).Caption)

            If Math.Round(CType(Me.GridView1.Columns(z + 5).SummaryText, Decimal), 2) > 0 Then
                If Math.Round(CType(Me.GridView1.Columns(z + 5).SummaryText, Decimal), 2) <> Me.GridView3.GetRowCellValue(0, Me.GridView3.Columns(z - 1)) Then
                    XtraMessageBox.Show("Target " & MonthName(z) & " Tidak Sesuai Dengan yang Sudah Ditetapkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        Next

        Select Case Indicator
            Case 100
                Try
                    Dim x As Integer

                    Dim i : For i = 0 To GridView1.RowCount - 1
                        If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "SalID")) Then
                            Dim cmSPDtl As New SqlCommand("SPInsT_TargetDtl2")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@TargetIDDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TargetIDDtl")
                                .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.Text
                                .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SalID")
                                .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                                .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jan")
                                .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Feb")
                                .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Mar")
                                .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Apr")
                                .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Mei")
                                .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jun")
                                .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jul")
                                .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Agt")
                                .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Sep")
                                .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Okt")
                                .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Nov")
                                .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Dess")
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
                        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            Case 200

                Try

                    Dim y : For y = 0 To arrPar.GetUpperBound(0)
                        Dim cmSPDel As New SqlCommand("SPDelT_TargetDtl2")
                        cmSPDel.CommandType = CommandType.StoredProcedure

                        With cmSPDel
                            .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                            .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.Text
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

                    Dim x As Integer

                    Dim i : For i = 0 To GridView1.RowCount - 1
                        If Me.GridView1.GetRowCellValue(i, "TargetIDD") < 0 Then
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TargetDtl2")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@TargetIDDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TargetIDDtl")
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.Text
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SalID")
                                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                                    .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jan")
                                    .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Feb")
                                    .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Mar")
                                    .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Apr")
                                    .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Mei")
                                    .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jun")
                                    .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jul")
                                    .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Agt")
                                    .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Sep")
                                    .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Okt")
                                    .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Nov")
                                    .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Dess")
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
                                    Me.GridView1.SetRowCellValue(i, "TahunD", Me.GridView1.GetRowCellValue(i, "TahunD") * -1)
                                Else
                                    XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Else
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPUpT_TargetDtl2")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TargetIDD")
                                    .Parameters.Add("@TargetIDDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TargetIDDtl")
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.SLUTahun.Text
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@SalID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SalID")
                                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                                    .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jan")
                                    .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Feb")
                                    .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Mar")
                                    .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Apr")
                                    .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Mei")
                                    .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jun")
                                    .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Jul")
                                    .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Agt")
                                    .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Sep")
                                    .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Okt")
                                    .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Nov")
                                    .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Dess")
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TargetIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            'rw = 0
            'Dim frm As New FSearch("Cabang", "", "", "", Date.Now, "")
            'frm.ShowDialog()

            'Try
            '    If Not IsDBNull(dataTrans.Item("Baris").ToString) Then
            '        Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
            '            If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
            '                Me.GridView1.AddNewRow()
            '            End If
            '        Next
            '    End If
            'Catch ex As Exception

            'End Try

        End If

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tahun", Me.SLUTahun.Text)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TargetIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TargetIDDtl", Me.SLUTahun.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CabID", Me.SLUCab.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SubJns", Me.SLUSubJns.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Jan", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Feb", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Mar", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Apr", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Mei", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Jun", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Jul", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Agt", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sep", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Okt", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Nov", 0.0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Dess", 0.0)

            rw += 1
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
       
    End Sub

    Private Sub SLUSubJns_Leave(sender As Object, e As EventArgs) Handles SLUSubJns.Leave
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct TargetIDD,H.Tahun From T_Target H Inner Join T_TargetDtl D On H.Tahun=D.Tahun and H.SubJns=D.SubJns Where Aktif ='True' and CabID='" & Me.SLUCab.EditValue & "' and H.SubJns='" & Me.SLUSubJns.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "TargetLUE")
        Try
            DsMaster.Tables("TargetLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "TargetLUE")

        Me.SLUTahun.Properties.DataSource = DsMaster.Tables("TargetLUE")
        Me.SLUTahun.Properties.DisplayMember = "Tahun"
        Me.SLUTahun.Properties.ValueMember = "TargetIDD"
    End Sub

    Private Sub SLUTahun_Leave(sender As Object, e As EventArgs) Handles SLUTahun.Leave
        If Not IsDBNull(Me.SLUTahun.EditValue) Then
            FillTargetCab(Me.SLUTahun.Text, Me.SLUCab.EditValue, Me.SLUSubJns.EditValue)

            'Jan = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Jan")
            'Feb = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Feb")
            'Mar = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Mar")
            'Apr = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Apr")
            'Mei = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Mei")
            'Jun = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Jun")
            'Jul = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Jul")
            'Agt = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Agt")
            'Sep = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Sep")
            'Okt = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Okt")
            'Nov = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Nov")
            'Des = DsMaster.Tables("TargetLUE").Select("Tahun = " & Me.SLUTahun.Text & "")(0).Item("Dess")

            CekEditKolom()
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTargetCab_d(Me.GridView2.GetFocusedDataRow.Item("Tahun"), Me.GridView2.GetFocusedDataRow.Item("CabID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdSalID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdSalID.ButtonClick
        If Me.SLUCab.EditValue = "" Then
            XtraMessageBox.Show("Cabang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim frm As New FSearch("SalesCabang", Me.SLUCab.EditValue, "", "", DateAndTime.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sales", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdSalID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdSalID.KeyPress
        e.Handled = True
    End Sub

End Class