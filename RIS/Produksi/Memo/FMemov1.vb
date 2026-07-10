Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid

Public Class FMemov1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim rw2 As Integer = 0
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=77", koneksi)

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

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("MemoN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTMemo_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.CETarikModel.Properties.ReadOnly = True

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
        Me.BVTMemo_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.CETarikModel.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTMemo_e.Selected = True
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_MemoDtl").Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MemoIDD,MemoID,MemoIDRef,Rev,BOMID,MdlID,stsTidakPki,MD.DivID,D.Nama As Div,MD.KompID,K.Nama as Komp, MD.BBIDAs,B.Nama as BahanAs,UkBBAs,KebAs,SatAs,MD.BBIDTj,(Select Nama From M_BB Where BBID=MD.BBIDTj) As BahanTj,UkBBTj,KebTj,KebTjR1, KebTjR2,KebTjR3,SatTj,MD.Ket From T_MemoDtl MD Inner Join M_Div D On MD.DivID=D.DivID Inner Join M_Komp K On MD.KompID=K.KompID Inner Join M_BB B On MD.BBIDAs=B.BBID Where MemoID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_MemoDtl")
        cmsl.Fill(DsMaster, "T_MemoDtl")

        DsMaster.Tables("T_MemoDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_MemoDtl").Columns("BOMID"), DsMaster.Tables("T_MemoDtl").Columns("DivID"), DsMaster.Tables("T_MemoDtl").Columns("KompID"), DsMaster.Tables("T_MemoDtl").Columns("BBIDAs"), DsMaster.Tables("T_MemoDtl").Columns("BBIDTj")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_MemoDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MemoID,PeriodID,CodeID,Tanggal,Ket,stsModel,stsApp,InsDate,InsBy,UpdDate,UpdBy,AppDate,AppBy From T_Memo Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,MemoID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_Memo")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_Memo")
        DsMaster.Tables("T_Memo").Clear()
        cmsl.Fill(DsMaster, "T_Memo")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_Memo"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_Memo")
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
            End Try
        End With
    End Sub


    Public Sub DelXml()
        'If IO.File.Exists("SrBBBOM.xml") Then
        '    System.IO.File.Delete("SrBBBOM.xml")
        'End If

        'If IO.File.Exists("SrBBNBOMBahan.xml") Then
        '    System.IO.File.Delete("SrBBNBOMBahan.xml")
        'End If
    End Sub

    Private Sub FMemo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Request Produksi"
    End Sub

    Private Sub FMemo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FMemorod_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FMemo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTMemo_e.Selected = True
    End Sub

    Private Sub BVTMemo_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTMemo_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Request Produksi"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("MemoEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("MemoApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("MemoDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("MemoP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Request Produksi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        DelXml()

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

        Me.CETarikModel.EditValue = True
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_MemoDtl").Clear()
        ReDim arrPar(-1)

        'If Me.CETarikModel.EditValue = True Then
        '    Me.GridView1.Columns("KebTj").OptionsColumn.AllowEdit = False
        'Else
        '    Me.GridView1.Columns("KebTj").OptionsColumn.AllowEdit = True
        'End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Request Produksi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_Memo", "stsApp", "MemoID", Me.GridView2.GetFocusedDataRow.Item("MemoID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        Indicator = "200"

        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("MemoID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.CETarikModel.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsModel")

        FillDtl(Me.TBKode.EditValue)

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True

        'If Me.CETarikModel.EditValue = True Then
        '    Me.GridView1.Columns("KebTj").OptionsColumn.AllowEdit = False
        'Else
        '    Me.GridView1.Columns("KebTj").OptionsColumn.AllowEdit = True
        'End If
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick

        If SlCek("T_Memo", "stsApp", "MemoID", Me.GridView2.GetFocusedDataRow.Item("MemoID")) = True Then
            XtraMessageBox.Show("Data Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmSP As New SqlCommand("SPAppBOMReq")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MemoID")
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
                    XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub
    Private Sub BVTApprove_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTApprove.ItemPressed
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBCancelAw.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTMemo_s.Enabled = False

        Me.GridView3.OptionsBehavior.Editable = True

        Me.BSaveAppAs.Enabled = True
        Me.BCancelAppAs.Enabled = True

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,MemoID,Tanggal,Ket From T_Memo Where stsapp='False' and PeriodID=" & MainModule.periodAktif & "", koneksi)

        cmsl.TableMappings.Add("Table", "T_MemoApp")
        Try
            DsMaster.Tables("T_MemoApp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_MemoApp")

        DsMaster.Tables("T_MemoApp").PrimaryKey = New DataColumn() {DsMaster.Tables("T_MemoApp").Columns("MemoID")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_MemoApp"

        Dim cmsl2 As SqlDataAdapter
        cmsl2 = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,M.MemoIDD,M.Rev,M.BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol As Tot, TotPsgR1,TotPsgR2,TotPsgR3,BBIDAs, Bn.Nama As BahanAs,KebAs,BBIDTj,(Select Nama From M_BB Where BBID=BBIDTj) As BahanTj,KebTj,KebTjR1,KebTjR2,KebTjR3 From T_MemoDtl M Inner Join T_BOM B On M.BOMID=B.BOMID Inner Join M_Cust C On B.CustID=C.CustID Inner Join M_BB Bn On M.BBIDAs=Bn.BBID Where B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") and PeriodID=" & MainModule.periodAktif & " or (M.Rev='1' and M.stsBtlAw1='False') or (M.Rev='2' and M.stsBtlAw2='False')  or (M.Rev='3' and M.stsBtlAw2='False')", koneksi)

        cmsl2.TableMappings.Add("Table", "T_MemoDApp")
        Try
            DsMaster.Tables("T_MemoDApp").Clear()
        Catch ex As Exception

        End Try
        cmsl2.Fill(DsMaster, "T_MemoDApp")

        DsMaster.Tables("T_MemoDApp").PrimaryKey = New DataColumn() {DsMaster.Tables("T_MemoApp").Columns("MemoID"), DsMaster.Tables("T_MemoDApp").Columns("MemoIDD")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_MemoDApp"
    End Sub


    Private Sub BSaveApp_Click(sender As Object, e As EventArgs) Handles BSaveAppAs.Click
        Me.GridView3.ActiveFilterString = "[Cek]=True"

        Dim x As Integer

        For i As Integer = 0 To Me.GridView3.RowCount - 1
            If Me.GridView3.GetRowCellValue(i, "Cek") = True Then
                koneksi.Close()

                Dim cmSP As New SqlCommand("SPAppBOMReq")
                cmSP.CommandType = CommandType.StoredProcedure

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(i, "MemoID")
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

                        If x <> 0 Then
                            XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BCancelApp_Click(sender As Object, e As EventArgs) Handles BCancelAppAs.Click
        LockControl()
        CekSave = False
    End Sub


    Private Sub BSaveAppRev_Click(sender As Object, e As EventArgs) Handles BSaveAppRev.Click
        Me.GridView4.ActiveFilterString = "[Cek]=True"

        Dim x As Integer

        For i As Integer = 0 To Me.GridView4.RowCount - 1
            If Me.GridView4.GetRowCellValue(i, "Cek") = True Then
                koneksi.Close()

                Dim cmSP As New SqlCommand("SPAppMemoRev")
                cmSP.CommandType = CommandType.StoredProcedure

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "MemoID")
                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(i, "MemoIDD")
                    .Parameters.Add("@Rev", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Rev")
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

                        If x <> 0 Then
                            XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BCancelAppRev_Click(sender As Object, e As EventArgs) Handles BCancelAppRev.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub BVBCancelAw_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelAw.ItemClick
        DelXml()

        Indicator = "300"

        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("MemoID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.CETarikModel.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsModel")

        FillDtl(Me.TBKode.EditValue)

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBCancelAw.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTMemo_s.Enabled = False

        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn34.OptionsColumn.AllowEdit = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.GridColumn35.OptionsColumn.AllowEdit = True
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 2

        CekSave = True
        Me.BVTMemo_e.Selected = True

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Request Produksi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_Memo", "stsApp", "MemoID", Me.GridView2.GetFocusedDataRow.Item("MemoID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("MemoID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_Memo")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MemoID")
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
                End Try
            End With
        End If

    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim frm As New FPilihPrint

        frm = New FPilihPrint
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim frm2 As New FPilihUkuran

        frm2 = New FPilihUkuran
        frm2.ShowDialog()
        frm2.Dispose()
        frm2.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MemoID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Rev"), "Rev")
        bind.Add(Manual, "Manual")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        If MainModule.PilihPDok = "Memo Rev 1" Then
            If SlCek("T_Memo", "stsBtlAw1", "MemoID", Me.GridView2.GetFocusedDataRow.Item("MemoID")) = False Then
                XtraMessageBox.Show("Data Pembatalan Pertama Belum Diapprove Oleh Ka. PPIC!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        ElseIf MainModule.PilihPDok = "Memo Rev 2" Then
            If SlCek("T_Memo", "stsBtlAw2", "MemoID", Me.GridView2.GetFocusedDataRow.Item("MemoID")) = False Then
                XtraMessageBox.Show("Data Pembatalan Pertama Belum Diapprove Oleh Ka. PPIC!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        ElseIf MainModule.PilihPDok = "Memo Rev 3" Then
            If SlCek("T_Memo", "stsBtlAw3", "MemoID", Me.GridView2.GetFocusedDataRow.Item("MemoID")) = False Then
                XtraMessageBox.Show("Data Pembatalan Pertama Belum Diapprove Oleh Ka. PPIC!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Dim XR As New XRMemo
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim o : For o = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(o, "BBIDAs") <> "" And Me.GridView1.GetRowCellValue(o, "stsTidakPki") = False And Me.GridView1.GetRowCellValue(o, "BBIDTj") = "" Then
                XtraMessageBox.Show("Bahan Pengganti Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Next

        Select Case Indicator
            Case 100

                Dim cmSP As New SqlCommand("SPInsT_Memo")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsModel", SqlDbType.VarChar).Value = Me.CETarikModel.EditValue
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBIDAs")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_MemoDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@MemoIDRef", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MemoIDRef")
                                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                    .Parameters.Add("@MdlID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MdlID")
                                    .Parameters.Add("@stsTdkPki", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsTidakPki")
                                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                    .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                    .Parameters.Add("@BBIDAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDAs")
                                    .Parameters.Add("@UkBBAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBBAs")
                                    .Parameters.Add("@SatAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatAs")
                                    .Parameters.Add("@KebAs", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebAs")
                                    .Parameters.Add("@BBIDTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDTj")
                                    .Parameters.Add("@UkBBTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBBTj")
                                    .Parameters.Add("@SatTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatTj")
                                    .Parameters.Add("@KebTj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebTj")
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
                Dim cmSP As New SqlCommand("SPUpT_Memo")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsModel", SqlDbType.VarChar).Value = Me.CETarikModel.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_MemoDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "MemoIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBIDAs")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_MemoDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@MemoIDRef", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MemoIDRef")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@MdlID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MdlID")
                                        .Parameters.Add("@stsTdkPki", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsTidakPki")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBIDAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDAs")
                                        .Parameters.Add("@UkBBAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBBAs")
                                        .Parameters.Add("@SatAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatAs")
                                        .Parameters.Add("@KebAs", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebAs")
                                        .Parameters.Add("@BBIDTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDTj")
                                        .Parameters.Add("@UkBBTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBBTj")
                                        .Parameters.Add("@SatTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatTj")
                                        .Parameters.Add("@KebTj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebTj")
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
                                        Me.GridView1.SetRowCellValue(i, "MemoIDD", Me.GridView1.GetRowCellValue(i, "MemoIDD") * -1)
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
                                    Dim cmSPDtl As New SqlCommand("SPUpT_MemoDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "MemoIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@MemoIDRef", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MemoIDRef")
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@MdlID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MdlID")
                                        .Parameters.Add("@stsTdkPki", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsTidakPki")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBIDAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDAs")
                                        .Parameters.Add("@UkBBAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBBAs")
                                        .Parameters.Add("@SatAs", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatAs")
                                        .Parameters.Add("@KebAs", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebAs")
                                        .Parameters.Add("@BBIDTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDTj")
                                        .Parameters.Add("@UkBBTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBBTj")
                                        .Parameters.Add("@SatTj", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatTj")
                                        .Parameters.Add("@KebTj", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebTj")
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
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Rev")) Then
                        Dim cmSPDtl As New SqlCommand("SPUpT_MemoDtlRev")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                            .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MemoIDD")
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@Rev", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Rev")
                            If Me.GridView1.GetRowCellValue(i, "Rev") = "1" Then
                                .Parameters.Add("@KebR", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebTjR1")

                            ElseIf Me.GridView1.GetRowCellValue(i, "Rev") = "2" Then
                                .Parameters.Add("@KebR", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebTjR2")

                            ElseIf Me.GridView1.GetRowCellValue(i, "Rev") = "3" Then
                                .Parameters.Add("@KebR", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "KebTjR3")
                            End If
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
                Next

                If x = 0 Then
                    XtraMessageBox.Show("Data Berhasil Dicancel", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf x = 1 Then
                    XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    XtraMessageBox.Show("Data Gagal Dicancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
        End Select

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub CETarikModel_Leave(sender As Object, e As EventArgs) Handles CETarikModel.Leave
        'If Me.CETarikModel.EditValue = True Then
        '    Me.GridView1.Columns("KebTj").OptionsColumn.AllowEdit = False
        'Else
        '    Me.GridView1.Columns("KebTj").OptionsColumn.AllowEdit = True
        'End If

        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "MemoIDD")

            Me.GridView1.DeleteRow(i)
        Next
    End Sub
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("MemoIDD")

            'DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
            'KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
            'BBIDIndHapus = Me.GridView1.GetFocusedDataRow.Item("BBID")

            'If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = True Then
            '    Hapus = True
            'Else
            '    Hapus = False
            'End If

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0

            Dim frm As New FMemo_a(Me.TBKode.EditValue)
            frm.ShowDialog()
            frm.Close()

            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                    If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                        Me.GridView1.AddNewRow()
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If Me.GridView1.RowCount > 0 Then
                If Indicator <> 300 Then
                    If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue("stsTidakPki")) Then
                        If Me.GridView1.GetFocusedRowCellValue("stsTidakPki") = True Then
                            Me.GridView1.Columns("BBIDTj").OptionsColumn.AllowEdit = False
                            Me.GridView1.Columns("KebAs").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridView1.Columns("BBIDTj").OptionsColumn.AllowEdit = True
                            Me.GridView1.Columns("KebAs").OptionsColumn.AllowEdit = True
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub RCBORev_EditValueChanged(sender As Object, e As EventArgs) Handles RCBORev.EditValueChanged
        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Rev", Me.GridView1.ActiveEditor.EditValue)

        If Me.GridView1.ActiveEditor.EditValue = "1" Then
            Me.GridView1.RefreshData()

            If SlCek("T_BOM", "stsBtlAw1", "BOMID", Me.GridView1.GetFocusedDataRow.Item("BOMID")) = False Then
                XtraMessageBox.Show("Data Pembatalan Pertama Belum Diapprove Marketing!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Rev", "")
                Exit Sub
            End If

            Me.GridColumn36.Visible = True
            Me.GridColumn36.VisibleIndex = 16

            Me.GridColumn36.OptionsColumn.AllowEdit = True

            Dim KebR As Decimal
            Dim command As New SqlCommand("Select Sum(Keb) As Keb From(Select Case when KaliQty='True' Then Round(Std*TotQtyR1,2) Else Case When Std=0 Then 0 Else Round(TotQtyR1/Std,2) End End As Keb From M_ModelDtl MD Inner Join T_BOM BM On MD.MdlID=BM.MdlID Inner Join T_BOMPO BP On BM.BOMID=BP.BOMID and MD.ArtCode=BP.ArtCode Inner Join M_BB B On MD.BBID=B.BBID Where MD.MdlID='" & Me.GridView1.GetFocusedDataRow.Item("MdlID") & "' and BM.BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and MD.DivID='" & Me.GridView1.GetFocusedDataRow.Item("DivID") & "' and MD.KompID='" & Me.GridView1.GetFocusedDataRow.Item("KompID") & "' and MD.BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBIDTj") & "') as x", koneksi)

            With koneksi
                .Open()
                KebR = command.ExecuteScalar()
                .Close()
            End With

            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KebTjR1", KebR)

        ElseIf Me.GridView1.ActiveEditor.EditValue = "2" Then
            If SlCek("T_BOM", "stsBtlAw2", "BOMID", Me.GridView1.GetFocusedDataRow.Item("BOMID")) = False Then
                XtraMessageBox.Show("Data Pembatalan Kedua Belum Diapprove Marketing!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Rev", "")
                Exit Sub
            End If

            Me.GridColumn37.Visible = True
            Me.GridColumn37.VisibleIndex = 17

            Me.GridColumn37.OptionsColumn.AllowEdit = True

            Dim KebR As Decimal
            Dim command As New SqlCommand("Select Sum(Keb) As Keb From(Select Case when KaliQty='True' Then Round(Std*TotQtyR2,2) Else Case When Std=0 Then 0 Else Round(TotQtyR2/Std,2) End End As Keb From M_ModelDtl MD Inner Join T_BOM BM On MD.MdlID=BM.MdlID Inner Join T_BOMPO BP On BM.BOMID=BP.BOMID and MD.ArtCode=BP.ArtCode Inner Join M_BB B On MD.BBID=B.BBID Where MD.MdlID='" & Me.GridView1.GetFocusedDataRow.Item("MdlID") & "' and BM.BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and MD.DivID='" & Me.GridView1.GetFocusedDataRow.Item("DivID") & "' and MD.KompID='" & Me.GridView1.GetFocusedDataRow.Item("KompID") & "' and MD.BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBIDTj") & "') as x", koneksi)

            With koneksi
                .Open()
                KebR = command.ExecuteScalar()
                .Close()
            End With

            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KebTjR2", KebR)

        ElseIf Me.GridView1.ActiveEditor.EditValue = "3" Then
            If SlCek("T_BOM", "stsBtlAw3", "BOMID", Me.GridView1.GetFocusedDataRow.Item("BOMID")) = False Then
                XtraMessageBox.Show("Data Pembatalan Kedua Belum Diapprove Marketing!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Rev", "")
                Exit Sub
            End If

            Me.GridColumn38.Visible = True
            Me.GridColumn38.VisibleIndex = 18

            Me.GridColumn38.OptionsColumn.AllowEdit = True

            Dim KebR As Decimal
            Dim command As New SqlCommand("Select Sum(Keb) As Keb From(Select Case when KaliQty='True' Then Round(Std*TotQtyR3,2) Else Case When Std=0 Then 0 Else Round(TotQtyR3/Std,2) End End As Keb From M_ModelDtl MD Inner Join T_BOM BM On MD.MdlID=BM.MdlID Inner Join T_BOMPO BP On BM.BOMID=BP.BOMID and MD.ArtCode=BP.ArtCode Inner Join M_BB B On MD.BBID=B.BBID Where MD.MdlID='" & Me.GridView1.GetFocusedDataRow.Item("MdlID") & "' and BM.BOMID='" & Me.GridView1.GetFocusedDataRow.Item("BOMID") & "' and MD.DivID='" & Me.GridView1.GetFocusedDataRow.Item("DivID") & "' and MD.KompID='" & Me.GridView1.GetFocusedDataRow.Item("KompID") & "' and MD.BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBIDTj") & "') as x", koneksi)

            With koneksi
                .Open()
                KebR = command.ExecuteScalar()
                .Close()
            End With

            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KebTjR3", KebR)
        End If

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("stsTidakPki") Then
            If Me.GridView1.GetFocusedRowCellValue("stsTidakPki") = True Then
                Me.GridView1.Columns("BBIDTj").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("KebAs").OptionsColumn.AllowEdit = False

                Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDTj", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "BahanTj", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "UkBBTj", "")
                Me.GridView1.SetRowCellValue(e.RowHandle, "KebTj", 0)
                Me.GridView1.SetRowCellValue(e.RowHandle, "SatTj", "")
            Else
                Me.GridView1.Columns("BBIDTj").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("KebAs").OptionsColumn.AllowEdit = True

            End If

        ElseIf e.Column Is GridView1.Columns("KebAs") Then

            Dim Stok As Decimal
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select Sum(Keb) From (Select Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=T_BOMDtl.BOMID and DivID=T_BOMDtl.DivID and KompID=T_BOMDtl.KompID and BBIDAs=T_BOMDtl.BBID and MH.MemoID<>'" & Me.TBKode.EditValue & "' and stsApp='True'),0)) As Keb From T_BOMDtl Where BOMID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") & "' and DivID ='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DivID") & "' and KompID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "KompID") & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") & "' Group By BOMID,DivID,KompID,BBID Union All Select Round(Sum(Keb)+Sum(Pol),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=T_BOMTamDtl.TambahanID and DivID=T_BOMTamDtl.DivID and KompID=T_BOMTamDtl.KompID and BBIDAs=T_BOMTamDtl.BBID and MH.MemoID<>'" & Me.TBKode.EditValue & "' and stsApp='True'),0)) As Keb From T_BOMTamDtl Where TambahanID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "MemoIDRef") & "' and DivID ='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DivID") & "' and KompID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "KompID") & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") & "' Group By TambahanID,DivID,KompID,BBID Union All Select Round(Sum(KebTj),2)-(Select Isnull((Select Round(Sum(KebAs),2) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where MemoIDRef=T_MemoDtl.MemoID and DivID=T_MemoDtl.DivID and KompID=T_MemoDtl.KompID and BBIDAs=T_MemoDtl.BBIDTj and MH.MemoID<>'" & Me.TBKode.EditValue & "' and stsApp='True'),0)) As Keb From T_MemoDtl Where MemoID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "MemoIDRef") & "' and DivID ='" & Me.GridView1.GetRowCellValue(e.RowHandle, "DivID") & "' and KompID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "KompID") & "' and BBIDTj='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") & "' Group By MemoID,DivID,KompID,BBIDTj) as x", koneksi)

            With koneksi
                .Open()
                Stok = Command.ExecuteScalar()
                .Close()
            End With

            Dim QtyAs As Decimal = 0

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView1.GetRowCellValue(e.RowHandle, "BOMID") And Me.GridView1.GetRowCellValue(i, "DivID") = Me.GridView1.GetRowCellValue(e.RowHandle, "DivID") And Me.GridView1.GetRowCellValue(i, "KompID") = Me.GridView1.GetRowCellValue(e.RowHandle, "KompID") And Me.GridView1.GetRowCellValue(i, "BBIDAs") = Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") And Me.GridView1.GetRowCellValue(i, "MemoIDD") & Me.GridView1.GetRowCellValue(i, "BBIDTj") <> Me.GridView1.GetRowCellValue(e.RowHandle, "MemoIDD") & Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDTj") Then
                    QtyAs += Me.GridView1.GetRowCellValue(i, "KebAs")
                End If
            Next

            If Me.GridView1.GetRowCellValue(e.RowHandle, "KebAs") > Stok - QtyAs Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Kebutuhan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "KebAs", Stok - QtyAs)
            End If

            Me.GridView1.SetRowCellValue(e.RowHandle, "KebTj", Me.GridView1.GetRowCellValue(e.RowHandle, "KebAs"))

            'ElseIf e.Column Is GridView1.Columns("KebTj") Then
            '    If Me.GridView1.GetRowCellValue(e.RowHandle, "KebTj") <> Me.GridView1.GetRowCellValue(e.RowHandle, "KebAs") Then
            '        Me.GridView1.SetRowCellValue(e.RowHandle, "KebTj", Me.GridView1.GetRowCellValue(e.RowHandle, "KebAs"))
            '    End If

        End If

    End Sub
    'Private Sub GridView1_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView1.ValidateRow
    '    Dim View As GridView = CType(sender, GridView)
    '    Dim BBIDTj As GridColumn = View.Columns("BBIDTj")

    '    If Indicator <> 300 Then
    '        If Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDAs") <> "" And Me.GridView1.GetRowCellValue(e.RowHandle, "stsTidakPki") = False And Me.GridView1.GetRowCellValue(e.RowHandle, "BBIDTj") = "" Then
    '            e.Valid = False
    '            View.SetColumnError(BBIDTj, "Bahan Pengganti Harus Diisi")
    '        End If

    '    End If
    'End Sub

    'Private Sub GridView1_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles GridView1.InvalidRowException
    '    'Suppress displaying the error message box
    '    'e.ExceptionMode = ExceptionMode.NoAction
    'End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FMemo_d(Me.GridView2.GetFocusedDataRow.Item("MemoID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            Me.GridView1.SetRowCellValue(e.RowHandle, "MemoIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MemoID", Me.TBKode.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MemoIDRef", dataTrans.Item("MemoIDRef" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", dataTrans.Item("BOMID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MdlID", dataTrans.Item("MdlID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "stsTidakPki", False)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DivID", dataTrans.Item("DivID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Div", dataTrans.Item("Div" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "KompID", dataTrans.Item("KompID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Komp", dataTrans.Item("Komp" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDAs", dataTrans.Item("BBID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BahanAs", dataTrans.Item("Bahan" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "UkBBAs", dataTrans.Item("UkBB" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "SatAs", dataTrans.Item("Sat" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "KebAs", CDec(dataTrans.Item("Keb" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDTj", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BahanTj", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "UkBBTj", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "SatTj", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "KebTj", 0)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")

            rw += 1
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBBTj_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles BEdBBTj.ButtonClick
        If Me.CETarikModel.EditValue = True Then
            Dim frm As New FSearch("BB Model", Me.GridView1.GetFocusedDataRow.Item("MdlID"), Me.GridView1.GetFocusedDataRow.Item("BOMID"), Me.GridView1.GetFocusedDataRow.Item("DivID"), Me.DTPTanggal.EditValue, Me.GridView1.GetFocusedDataRow.Item("KompID"))
            frm.ShowDialog()
        Else
            Dim frm As New FSearch("M_BB", "", "Bahan", "", Date.Now, "")
            frm.ShowDialog()
        End If

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BahanTj", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "UkBBTj", dataTrans.Item("Uk").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SatTj", dataTrans.Item("Sat").ToString)

                If Me.CETarikModel.EditValue = True Then
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KebTj", CDec(dataTrans.Item("Keb").ToString))
                Else
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KebTj", Me.GridView1.GetFocusedDataRow.Item("KebAs"))
                End If

                Me.BSave.Focus()
                Me.GridView1.Focus()

                'AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

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