Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTarget
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim rw As Integer = 0
    Dim Jan, Feb, Mar, Apr, Mei, Jun, Jul, Agt, Sep, Okt, Nov, Des As Decimal

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrgtN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTTarget_s.Enabled = True

        Me.TBTahun.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUSubJns.Properties.ReadOnly = True
        Me.SLUVarID.Properties.ReadOnly = True
        Me.TBTarget.Properties.ReadOnly = True
        Me.SLUVarID.Properties.ReadOnly = True

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

        Me.TBTahun.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUSubJns.Properties.ReadOnly = False
        Me.SLUVarID.Properties.ReadOnly = False
        Me.TBTarget.Properties.ReadOnly = False
        Me.SLUVarID.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTarget_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
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

    Public Sub FillDtl(Kode As String, SubJns As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TargetIDD,Tahun,D.CabID,Cabang,SubJns,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess From T_TargetDtl D Inner Join M_Cab C On C.CabID=D.CabID Where Tahun='" & Kode & "' and SubJns='" & SubJns & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_TargetDtl")
        Try
            DsMaster.Tables("T_TargetDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TargetDtl")

        DsMaster.Tables("T_TargetDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TargetDtl").Columns("Tahun"), DsMaster.Tables("T_TargetDtl").Columns("CabID"), DsMaster.Tables("T_TargetDtl").Columns("SubJns")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TargetDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Tahun,Tanggal,SubJns,Targett,VarID,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess,InsDate,InsBy,UpdDate, UpdBy From T_Target Where Tahun=" & MainModule.periodeTahun & "", koneksi)
        cmsl.TableMappings.Add("Table", "T_Target")
        Try
            DsMaster.Tables("T_Target").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_Target")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_Target"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Target")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBTahun.EditValue
            .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
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

    Private Sub FTarget_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Target Penjualan"
    End Sub

    Private Sub FTarget_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dataTrans = New Collection
        dataTrans.Clear()
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub FTarget_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FTarget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTarget_e.Selected = True
    End Sub
    Private Sub BVTTarget_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTarget_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Target Penjualan"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrgtEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrgtDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Target Penjualan"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBTahun.EditValue = MainModule.periodeTahun
        Me.TBTarget.EditValue = 0.0
        Me.SLUVarID.EditValue = ""
        Me.TBJan.EditValue = 0.0
        Me.TBFeb.EditValue = 0.0
        Me.TBMar.EditValue = 0.0
        Me.TBApr.EditValue = 0.0
        Me.TBMei.EditValue = 0.0
        Me.TBJun.EditValue = 0.0
        Me.TBJul.EditValue = 0.0
        Me.TBAgt.EditValue = 0.0
        Me.TBSep.EditValue = 0.0
        Me.TBOkt.EditValue = 0.0
        Me.TBNov.EditValue = 0.0
        Me.TBDes.EditValue = 0.0
        Me.CEAktif.EditValue = True

        Me.TBInfo.EditValue = ""

        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Target Penjualan"

        LUE()

        Indicator = "200"
        Me.TBTahun.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tahun")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.TBTarget.EditValue = Me.GridView2.GetFocusedDataRow.Item("Targett")
        Me.SLUSubJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("SubJns")
        Me.SLUVarID.EditValue = Me.GridView2.GetFocusedDataRow.Item("VarID")
        Me.TBJan.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jan")
        Me.TBFeb.EditValue = Me.GridView2.GetFocusedDataRow.Item("Feb")
        Me.TBMar.EditValue = Me.GridView2.GetFocusedDataRow.Item("Mar")
        Me.TBApr.EditValue = Me.GridView2.GetFocusedDataRow.Item("Apr")
        Me.TBMei.EditValue = Me.GridView2.GetFocusedDataRow.Item("Mei")
        Me.TBJun.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jun")
        Me.TBJul.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jul")
        Me.TBAgt.EditValue = Me.GridView2.GetFocusedDataRow.Item("Agt")
        Me.TBSep.EditValue = Me.GridView2.GetFocusedDataRow.Item("Sep")
        Me.TBOkt.EditValue = Me.GridView2.GetFocusedDataRow.Item("Okt")
        Me.TBNov.EditValue = Me.GridView2.GetFocusedDataRow.Item("Nov")
        Me.TBDes.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dess")

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select VarID,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess From M_VarTarget Where Aktif='True' and SubJns='" & Me.SLUSubJns.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_VarTargetLUE")
        Try
            DsMaster.Tables("M_VarTargetLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_VarTargetLUE")

        Me.SLUVarID.Properties.DataSource = DsMaster.Tables("M_VarTargetLUE")
        Me.SLUVarID.Properties.DisplayMember = "VarID"
        Me.SLUVarID.Properties.ValueMember = "VarID"

        FillDtl(Me.TBTahun.EditValue, Me.SLUSubJns.EditValue)

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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Target Penjualan"

        koneksi.Close()

        If MainModule.SlTarget(Me.GridView2.GetFocusedDataRow.Item("Tahun")) > 0 Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Dipakai", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
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

        If Me.SLUSubJns.EditValue = "" Or IsDBNull(Me.SLUSubJns.EditValue) Then
            XtraMessageBox.Show("Sub Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Me.GridView1.ActiveFilter.Clear()

        'If Math.Round(CType(Me.GridView1.Columns("Jan").SummaryText, Decimal), 2) <> Me.TBJan.EditValue Or Math.Round(CType(Me.GridView1.Columns("Feb").SummaryText, Decimal), 2) <> Me.TBFeb.EditValue Or Math.Round(CType(Me.GridView1.Columns("Mar").SummaryText, Decimal), 2) <> Me.TBMar.EditValue Or Math.Round(CType(Me.GridView1.Columns("Apr").SummaryText, Decimal), 2) <> Me.TBApr.EditValue Or Math.Round(CType(Me.GridView1.Columns("Mei").SummaryText, Decimal), 2) <> Me.TBMei.EditValue Or Math.Round(CType(Me.GridView1.Columns("Jun").SummaryText, Decimal), 2) <> Me.TBJun.EditValue Or Math.Round(CType(Me.GridView1.Columns("Jul").SummaryText, Decimal), 2) <> Me.TBJul.EditValue Or Math.Round(CType(Me.GridView1.Columns("Agt").SummaryText, Decimal), 2) <> Me.TBAgt.EditValue Or Math.Round(CType(Me.GridView1.Columns("Sep").SummaryText, Decimal), 2) <> Me.TBSep.EditValue Or Math.Round(CType(Me.GridView1.Columns("Okt").SummaryText, Decimal), 2) <> Me.TBOkt.EditValue Or Math.Round(CType(Me.GridView1.Columns("Nov").SummaryText, Decimal), 2) <> Me.TBNov.EditValue Or Math.Round(CType(Me.GridView1.Columns("Dess").SummaryText, Decimal), 2) <> Me.TBDes.EditValue Then

        '    XtraMessageBox.Show("Ada Target yang Tidak Sesuai Dengan yang Sudah Ditetapkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_Target")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.TBTarget.EditValue
                    .Parameters.Add("@VarID", SqlDbType.VarChar).Value = Me.SLUVarID.EditValue
                    .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.TBJan.EditValue
                    .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.TBFeb.EditValue
                    .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.TBMar.EditValue
                    .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.TBApr.EditValue
                    .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.TBMei.EditValue
                    .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.TBJun.EditValue
                    .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.TBJul.EditValue
                    .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.TBAgt.EditValue
                    .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.TBSep.EditValue
                    .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.TBOkt.EditValue
                    .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.TBNov.EditValue
                    .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.TBDes.EditValue
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

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CabID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TargetDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
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
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBTahun.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpT_Target")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@Target", SqlDbType.Decimal).Value = Me.TBTarget.EditValue
                    .Parameters.Add("@VarID", SqlDbType.VarChar).Value = Me.SLUVarID.EditValue
                    .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.TBJan.EditValue
                    .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.TBFeb.EditValue
                    .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.TBMar.EditValue
                    .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.TBApr.EditValue
                    .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.TBMei.EditValue
                    .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.TBJun.EditValue
                    .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.TBJul.EditValue
                    .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.TBAgt.EditValue
                    .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.TBSep.EditValue
                    .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.TBOkt.EditValue
                    .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.TBNov.EditValue
                    .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.TBDes.EditValue
                    .Parameters.Add("@Aktif", SqlDbType.Bit).Value = Me.CEAktif.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_TargetDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
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
                            If Me.GridView1.GetRowCellValue(i, "TargetIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "CabID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TargetDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
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
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TargetDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TargetIDD")
                                        .Parameters.Add("@Tahun", SqlDbType.Int).Value = Me.TBTahun.EditValue
                                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CabID")
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

    Private Sub SLUSubJns_Leave(sender As Object, e As EventArgs) Handles SLUSubJns.Leave

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select VarID,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess From M_VarTarget Where Aktif='True' and SubJns='" & Me.SLUSubJns.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_VarTargetLUE")
        Try
            DsMaster.Tables("M_VarTargetLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_VarTargetLUE")

        Me.SLUVarID.Properties.DataSource = DsMaster.Tables("M_VarTargetLUE")
        Me.SLUVarID.Properties.DisplayMember = "VarID"
        Me.SLUVarID.Properties.ValueMember = "VarID"
    End Sub


    Private Sub TBTarget_Leave(sender As Object, e As EventArgs) Handles TBTarget.Leave
        If Me.SLUVarID.EditValue <> "" Then
            Me.TBJan.EditValue = Math.Round((Jan * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBFeb.EditValue = Math.Round((Feb * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBMar.EditValue = Math.Round((Mar * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBApr.EditValue = Math.Round((Apr * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBMei.EditValue = Math.Round((Mei * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBJun.EditValue = Math.Round((Jun * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBJul.EditValue = Math.Round((Jul * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBAgt.EditValue = Math.Round((Agt * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBSep.EditValue = Math.Round((Sep * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBOkt.EditValue = Math.Round((Okt * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBNov.EditValue = Math.Round((Nov * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBDes.EditValue = Math.Round((Des * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TargetIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            Dim frm As New FSearch("Cabang", "", "", "", Date.Now, "")
            frm.ShowDialog()

            Try
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
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            Me.GridView1.SetRowCellValue(e.RowHandle, "Tahun", Me.TBTahun.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TargetIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CabID", dataTrans.Item("Kode" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Cabang", dataTrans.Item("Nama" & rw).ToString)
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
            Me.GridView1.SetRowCellValue(e.RowHandle, "Des", 0.0)

            rw += 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUVarID_Leave(sender As Object, e As EventArgs) Handles SLUVarID.Leave
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TargetIDD")

            Me.GridView1.DeleteRow(i)
        Next

        If Not IsDBNull(Me.SLUVarID.EditValue) And Me.SLUVarID.EditValue <> "" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY CabID)*-1 as TargetIDD," & Me.TBTahun.EditValue & " As Tahun,CabID,Cabang,'" & Me.SLUSubJns.EditValue & "' As SubJns,0.0 As Jan,0.0 As Feb,0.0 As Mar,0.0 As Apr,0.0 As Mei,0.0 As Jun,0.0 As Jul,0.0 As Agt,0.0 As Sep,0.0 As Okt,0.0 As Nov,0.0 As Dess From M_Cab Where Aktif='True'", koneksi)

            cmsl.TableMappings.Add("Table", "T_TargetDtl")
            Try
                DsMaster.Tables("T_TargetDtl").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "T_TargetDtl")

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_TargetDtl"

            Jan = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Jan")
            Feb = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Feb")
            Mar = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Mar")
            Apr = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Apr")
            Mei = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Mei")
            Jun = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Jun")
            Jul = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Jul")
            Agt = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Agt")
            Sep = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Sep")
            Okt = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Okt")
            Nov = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Nov")
            Des = DsMaster.Tables("M_VarTargetLUE").Select("VarID = '" & Me.SLUVarID.EditValue & "'")(0).Item("Dess")

            Me.TBJan.EditValue = Math.Round((Jan * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBFeb.EditValue = Math.Round((Feb * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBMar.EditValue = Math.Round((Mar * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBApr.EditValue = Math.Round((Apr * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBMei.EditValue = Math.Round((Mei * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBJun.EditValue = Math.Round((Jun * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBJul.EditValue = Math.Round((Jul * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBAgt.EditValue = Math.Round((Agt * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBSep.EditValue = Math.Round((Sep * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBOkt.EditValue = Math.Round((Okt * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBNov.EditValue = Math.Round((Nov * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBDes.EditValue = Math.Round((Des * Me.TBTarget.EditValue) / 100, 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTarget_d(Me.GridView2.GetFocusedDataRow.Item("Tahun"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try

    End Sub

End Class