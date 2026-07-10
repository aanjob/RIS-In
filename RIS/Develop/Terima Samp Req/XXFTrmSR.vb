Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class XXFTrmSR
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=59", koneksi)

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

    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("TrmSN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTTrmSR_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUMkt.Properties.ReadOnly = True
        Me.SLUChaser.Properties.ReadOnly = True
        Me.SLUReqID.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BProses.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTrmSR_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUMkt.Properties.ReadOnly = False
        Me.SLUChaser.Properties.ReadOnly = False
        Me.SLUReqID.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BProses.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTrmSR_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UserID,Nama From M_User Where Aktif='True' and PosisiID <>'Admin Marketing Cabang' and PosisiID Like '%Marketing%'", koneksi)
        cmsl.TableMappings.Add("Table", "MktLUE")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "MktLUE")

        Me.SLUMkt.Properties.DataSource = DsMaster.Tables("MktLUE")
        Me.SLUMkt.Properties.DisplayMember = "Nama"
        Me.SLUMkt.Properties.ValueMember = "UserID"

        cmsl = New SqlDataAdapter("Select UserID,Nama From M_User Where Aktif='True' and PosisiID Like '%Develop%' and PosisiID<>'Developer'", koneksi)
        cmsl.TableMappings.Add("Table", "ChaserLUE")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "ChaserLUE")

        Me.SLUChaser.Properties.DataSource = DsMaster.Tables("ChaserLUE")
        Me.SLUChaser.Properties.DisplayMember = "Nama"
        Me.SLUChaser.Properties.ValueMember = "UserID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrmSRIDD,TrmSRID,ReqIDD,ReqID,CustID,StlName,SampType,Uk,Warna,Qty,QtyRj From T_TrmSRDtl Where TrmSRID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_TrmSRDtl")
        Try
            DsMaster.Tables("T_TrmSRDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_TrmSRDtl")

        DsMaster.Tables("T_TrmSRDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmSRDtl").Columns("ReqID"), DsMaster.Tables("T_TrmSRDtl").Columns("ReqIDD"), DsMaster.Tables("T_TrmSRDtl").Columns("StlName"), DsMaster.Tables("T_TrmSRDtl").Columns("SampType"), DsMaster.Tables("T_TrmSRDtl").Columns("Warna"), DsMaster.Tables("T_TrmSRDtl").Columns("Uk")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrmSRDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TrmSRID,H.PeriodID,CodeID,H.Tanggal,H.UserID,MktID,U.Nama As Mkt,ChaserID,(Select Nama From M_User Where UserID=ChaserID) AS Chaser,TotQty,TotQtyRj,H.Ket,H.stsApp,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_TrmSR H Inner Join M_User U On H.MktID=U.UserID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,TrmSRID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrmSR")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_TrmSR")
        DsMaster.Tables("T_TrmSR").Clear()
        cmsl.Fill(DsMaster, "T_TrmSR")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TrmSR"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_TrmSR")
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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Penerimaan Sample Request"
    End Sub

    Private Sub FSampReq_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FSampReq_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTTrmSR_e.Selected = True
    End Sub

    Private Sub BVTTrmSR_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTrmSR_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Penerimaan Sample Request"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("TrmSEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("TrmSApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("TrmSDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Penerimaan Sample Request"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ("Own") > 0 Or MainModule.SlOpBJ("Job Order") > 0 > 0 Or MainModule.SlstsPeriodNew() = True Then
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

        Me.SLUMkt.EditValue = ""
        Me.SLUChaser.EditValue = ""
        Me.SLUReqID.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_TrmSRDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Penerimaan Sample Request"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Opname Atau Tutup Periode. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If SlCek("T_TrmSR", "stsApp", "TrmSRID", Me.GridView2.GetFocusedDataRow.Item("TrmSRID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Indicator = "200"
        LUE()

        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TrmSRID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUMkt.EditValue = Me.GridView2.GetFocusedDataRow.Item("MktID")
        Me.SLUChaser.EditValue = Me.GridView2.GetFocusedDataRow.Item("ChaserID")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct H.ReqID,C.Nama As Cust,StlName,SampType,Warna,Uk From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID Inner Join M_Cust C On H.CustID=C.CustID Where MktID='" & Me.SLUMkt.EditValue & "' and D.stsKirim='False' or H.ReqID In (Select ReqID From T_SampReqDtl where H.ReqID ='" & Me.TBKode.EditValue & "') ", koneksi)
        cmsl.TableMappings.Add("Table", "T_SampReqLUE")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_SampReqLUE")

        Me.SLUReqID.Properties.DataSource = DsMaster.Tables("T_SampReqLUE")
        Me.SLUReqID.Properties.DisplayMember = "ReqID"
        Me.SLUReqID.Properties.ValueMember = "ReqID"

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
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPAppTrmSR")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TrmSRID")
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
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Penerimaan Sample Request"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_TrmSR", "stsApp", "TrmSRID", Me.GridView2.GetFocusedDataRow.Item("TrmSRID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TrmSRID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_TrmSR")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TrmSRID")
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

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_TrmSR")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@UserID", SqlDbType.Int).Value = MainModule.UserAktif
                    .Parameters.Add("@MktID", SqlDbType.Int).Value = Me.SLUMkt.EditValue
                    .Parameters.Add("@ChaserID", SqlDbType.Int).Value = Me.SLUChaser.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 1)
                    .Parameters.Add("@TotQtyRj", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("QtyRj").SummaryText, Decimal), 1)
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Style")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_TrmSRDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@ReqIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqIDD")
                                    .Parameters.Add("@ReqID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ReqID")
                                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                                    .Parameters.Add("@StlName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StlName")
                                    .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@QtyRj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyRj")
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
                Dim cmSP As New SqlCommand("SPUpT_TrmSR")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@UserID", SqlDbType.Int).Value = MainModule.UserAktif
                    .Parameters.Add("@MktID", SqlDbType.Int).Value = Me.SLUMkt.EditValue
                    .Parameters.Add("@ChaserID", SqlDbType.Int).Value = Me.SLUChaser.EditValue
                    .Parameters.Add("@TotQty", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 1)
                    .Parameters.Add("@TotQtyRj", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("QtyRj").SummaryText, Decimal), 1)
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
                            Dim cmSPDel As New SqlCommand("SPDelT_TrmSRDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TrmSRIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "StlName")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_TrmSRDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ReqIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqIDD")
                                        .Parameters.Add("@ReqID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ReqID")
                                        .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                                        .Parameters.Add("@StlName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StlName")
                                        .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyRj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyRj")
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
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "StlName")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_TrmSRDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmSRIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ReqIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqIDD")
                                        .Parameters.Add("@ReqID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ReqID")
                                        .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                                        .Parameters.Add("@StlName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StlName")
                                        .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyRj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyRj")
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TrmSRIDD")
        End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try

            Me.GridView1.SetRowCellValue(e.RowHandle, "TrmSRIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "TrmSRID", Me.TBKode.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ReqIDD", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ReqID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "StlName", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "SampType", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "QtyRj", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Warna", "")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FTrmSR_d(Me.GridView2.GetFocusedDataRow.Item("TrmSRID"))
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

    Private Sub SLUMkt_Leave(sender As Object, e As EventArgs) Handles SLUMkt.Leave
        If Me.SLUMkt.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct H.ReqID,C.Nama As Cust,StlName,SampType,Warna,Uk From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID  Inner Join M_Cust C On H.CustID=C.CustID Where H.MktID='" & Me.SLUMkt.EditValue & "' and H.ChaserID='" & Me.SLUChaser.EditValue & "' and D.stsKirim='False'", koneksi)
            cmsl.TableMappings.Add("Table", "T_SampReqLUE")
            DsMaster = New System.Data.DataSet
            cmsl.Fill(DsMaster, "T_SampReqLUE")

            Me.SLUReqID.Properties.DataSource = DsMaster.Tables("T_SampReqLUE")
            Me.SLUReqID.Properties.DisplayMember = "ReqID"
            Me.SLUReqID.Properties.ValueMember = "ReqID"

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmSRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUChaser_Leave(sender As Object, e As EventArgs) Handles SLUChaser.Leave
        If Me.SLUChaser.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct H.ReqID,C.Nama As Cust,StlName,SampType,Warna,Uk From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID  Inner Join M_Cust C On H.CustID=C.CustID Where H.MktID='" & Me.SLUMkt.EditValue & "' and H.ChaserID='" & Me.SLUChaser.EditValue & "' and D.stsKirim='False'", koneksi)
            cmsl.TableMappings.Add("Table", "T_SampReqLUE")
            DsMaster = New System.Data.DataSet
            cmsl.Fill(DsMaster, "T_SampReqLUE")

            Me.SLUReqID.Properties.DataSource = DsMaster.Tables("T_SampReqLUE")
            Me.SLUReqID.Properties.DisplayMember = "ReqID"
            Me.SLUReqID.Properties.ValueMember = "ReqID"

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "TrmSRIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select * From (Select ROW_NUMBER() over (ORDER BY StlName)*-1 as TrmSRIDD,'" & Me.TBKode.EditValue & "' as TrmSRID,D.ReqIDD,H.ReqID,CustID,StlName,SampType,Uk,Warna,Qty-(Select Isnull((Select Sum(Qty)+Sum(QtyRj) From T_TrmSRDtl Where ReqID=D.ReqID and ReqIDD=D.ReqIDD and TrmSRID<>'" & Me.TBKode.EditValue & "'),0)) As Qty, 0.0 as QtyRj From T_SampReq H Inner Join T_SampReqDtl D On H.ReqID=D.ReqID Where H.ReqID='" & Me.SLUReqID.EditValue & "') As x Where Qty>0", koneksi)
        cmsl.Fill(DsMaster, "T_TrmSRDtl")

        DsMaster.Tables("T_TrmSRDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmSRDtl").Columns("ReqID"), DsMaster.Tables("T_TrmSRDtl").Columns("ReqIDD"), DsMaster.Tables("T_TrmSRDtl").Columns("StlName"), DsMaster.Tables("T_TrmSRDtl").Columns("SampType"), DsMaster.Tables("T_TrmSRDtl").Columns("Uk")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_TrmSRDtl"

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            Dim Sisa As Decimal
            Dim command As New SqlCommand("Select Qty-(Select Isnull((Select Sum(Qty)+Sum(QtyRj) From T_TrmSRDtl Where ReqID=T_SampReqDtl.ReqID and ReqIDD=T_SampReqDtl.ReqIDD and TrmSRID<>'" & Me.TBKode.EditValue & "'),0)) As Qty From T_SampReqDtl Where ReqIDD='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ReqIDD") & "' and ReqID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ReqID") & "'", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRj") > Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Sisa Qty Request", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Sisa - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRj"))
            End If

        ElseIf e.Column Is GridView1.Columns("QtyRj") Then
            Dim Sisa As Decimal
            Dim command As New SqlCommand("Select Qty-(Select Isnull((Select Sum(Qty)+Sum(QtyRj) From T_TrmSRDtl Where ReqID=T_SampReqDtl.ReqID and ReqIDD=T_SampReqDtl.ReqIDD and TrmSRID<>'" & Me.TBKode.EditValue & "'),0)) As Qty From T_SampReqDtl Where ReqIDD='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ReqIDD") & "' and ReqID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ReqID") & "'", koneksi)

            With koneksi
                .Open()
                Sisa = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRj") + Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Sisa Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Sisa Qty Request", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyRj", Sisa - Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
            End If
        End If

    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class