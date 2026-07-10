Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid

Public Class FReqProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim arrPar2(-1) As String
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim rw2 As Integer = 0
    Dim bind As New Collection

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=58", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ReqPN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTReqP_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.CBODok.Properties.ReadOnly = True
        Me.TBBag.Properties.ReadOnly = True
        Me.TBUnit.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
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
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTReqP_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.CBODok.Properties.ReadOnly = False
        Me.TBBag.Properties.ReadOnly = False
        Me.TBUnit.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTReqP_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Clear()
        cmsl.Fill(DsMaster, "M_CustL")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,RD.DivID,D.Nama As Divisi,RD.KompID,K.Nama as Komponen, RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req,KaliQty,RD.KetMdl,RD.stsJasa,RD.stsMentah,RD.BBIDInd,RD.HarSat,RD.HarAkhir,RD.Ket From T_ReqPDtl RD Inner Join M_Div D On RD.DivID=D.DivID Inner Join M_Komp K On RD.KompID=K.KompID Inner Join M_BB B On RD.BBID=B.BBID Inner Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "' and BOMID<>''", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqPDtl")
        Try
            DsMaster.Tables("T_ReqPDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPDtl")

        DsMaster.Tables("T_ReqPDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPDtl").Columns("BOMID"), DsMaster.Tables("T_ReqPDtl").Columns("ArtCode"), DsMaster.Tables("T_ReqPDtl").Columns("DivID"), DsMaster.Tables("T_ReqPDtl").Columns("KompID"), DsMaster.Tables("T_ReqPDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ReqPDtl"

        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,Ket,Upp,Hancur,Hilang From T_ReqPQty RD Inner Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_ReqPQty")
        Try
            DsMaster.Tables("T_ReqPQty").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPQty")

        DsMaster.Tables("T_ReqPQty").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPQty").Columns("BOMID"), DsMaster.Tables("T_ReqPQty").Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_ReqPQty"
    End Sub

    Public Sub FillDtlDModel(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,RD.DivID,D.Nama As Divisi,RD.KompID,K.Nama as Komponen, RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req,KaliQty,RD.KetMdl,RD.stsJasa,RD.stsMentah,RD.BBIDInd,RD.HarSat, RD.HarAkhir,RD.Ket From T_ReqPDtl RD Left Outer Join M_Div D On RD.DivID=D.DivID Left Outer Join M_Komp K On RD.KompID=K.KompID Inner Join M_BB B On RD.BBID=B.BBID Left Outer Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "' and BOMID=''", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqPDtlDModel")
        Try
            DsMaster.Tables("T_ReqPDtlDModel").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPDtlDModel")

        DsMaster.Tables("T_ReqPDtlDModel").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPDtlDModel").Columns("BBID")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_ReqPDtlDModel"
    End Sub

    Public Sub FillDtlBOM(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,RD.DivID,D.Nama As Divisi,RD.KompID,K.Nama as Komponen, RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req,KaliQty,RD.KetMdl,RD.stsJasa,RD.stsMentah,RD.BBIDInd,RD.HarSat, RD.HarAkhir,RD.Ket From T_ReqPDtl RD Left Outer Join M_Div D On RD.DivID=D.DivID Left Outer Join M_Komp K On RD.KompID=K.KompID Inner Join M_BB B On RD.BBID=B.BBID Left Outer Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "' and BOMID<>'' and RD.ArtCode=''", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqPDtlDModel")
        Try
            DsMaster.Tables("T_ReqPDtlDModel").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPDtlDModel")

        DsMaster.Tables("T_ReqPDtlDModel").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPDtlDModel").Columns("BOMID"), DsMaster.Tables("T_ReqPDtlDModel").Columns("BBID")}

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "T_ReqPDtlDModel"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPID,PeriodID,CodeID,Tanggal,Jenis,Dok,R.CustID,C.Nama As Cust,Bagian,Unit,R.Ket,stsApp,stsLunas, R.InsDate,R.InsBy,R.UpdDate,R.UpdBy,R.AppDate,R.AppBy From T_ReqP R Inner Join M_Cust C On R.CustID=C.CustID Where PeriodID=" & MainModule.periodAktif & " Order By Tanggal,ReqPID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqP")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_ReqP")
        DsMaster.Tables("T_ReqP").Clear()
        cmsl.Fill(DsMaster, "T_ReqP")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ReqP"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_ReqP")
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
        If IO.File.Exists("SrBBBOM.xml") Then
            System.IO.File.Delete("SrBBBOM.xml")
        End If

        If IO.File.Exists("SrBBNBOMBahan.xml") Then
            System.IO.File.Delete("SrBBNBOMBahan.xml")
        End If
    End Sub

    Private Sub FReqP_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Request Produksi"
    End Sub

    Private Sub FReqP_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FReqProd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FReqP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTReqP_e.Selected = True
    End Sub

    Private Sub BVTReqP_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTReqP_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Request Produksi"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ReqPEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("ReqPApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ReqPDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("ReqPP"), Boolean)
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
        Me.TBBag.EditValue = ""
        Me.TBUnit.EditValue = ""
        Me.CBOJenis.EditValue = ""
        Me.CBODok.EditValue = "Model"
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        If Me.CBODok.EditValue = "Diluar Model" Then
            Me.XTPDModel.PageVisible = True
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = False

            FillDtlDModel(Me.TBKode.EditValue)

        ElseIf Me.CBODok.EditValue = "Model" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = True
            Me.XTPBOM.PageVisible = False

            FillDtl(Me.TBKode.EditValue)

        ElseIf Me.CBODok.EditValue = "BOM" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = True

            FillDtlBOM(Me.TBKode.EditValue)

        End If

        DsMaster.Tables("T_ReqPQty").Clear()
        DsMaster.Tables("T_ReqPDtl").Clear()

        ReDim arrPar(-1)
        ReDim arrPar2(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Request Produksi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("stsApp") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove atau Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        Indicator = "200"
        LUE()

        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ReqPID")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.CBODok.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dok")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.TBBag.EditValue = Me.GridView2.GetFocusedDataRow.Item("Bagian")
        Me.TBUnit.EditValue = Me.GridView2.GetFocusedDataRow.Item("Unit")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        If Me.CBODok.EditValue = "Diluar Model" Then
            Me.XTPDModel.PageVisible = True
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = False

            FillDtlDModel(Me.TBKode.EditValue)

        ElseIf Me.CBODok.EditValue = "Model" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = True
            Me.XTPBOM.PageVisible = False

            FillDtl(Me.TBKode.EditValue)

        ElseIf Me.CBODok.EditValue = "BOM" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = True

            FillDtlBOM(Me.TBKode.EditValue)
        End If

        ReDim arrPar(-1)
        ReDim arrPar2(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If
        OpenControl()
        CekSave = True

        If Me.GridView3.RowCount > 0 Then

            Me.GridView1.ActiveFilterString = "[BOMID] = '" & Me.GridView3.GetFocusedRowCellValue("BOMID") & "' and [ArtCode]='" & Me.GridView3.GetFocusedRowCellValue("ArtCode") & "'"
        End If
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick

        If SlCek("T_ReqP", "stsApp", "ReqPID", Me.GridView2.GetFocusedDataRow.Item("ReqPID")) = True Then
            XtraMessageBox.Show("Data Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmSP As New SqlCommand("SPAppBOMReq")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqPID")
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

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Request Produksi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_ReqP", "stsApp", "ReqPID", Me.GridView2.GetFocusedDataRow.Item("ReqPID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("ReqPID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_ReqP")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ReqPID")
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
        bind = New Collection

        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ReqPID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Jenis"), "Jenis")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Dok"), "Dok")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Bagian"), "Bagian")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Unit"), "Unit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Manual, "Manual")

        'If SlCek("T_ReqP", "stsApp", "ReqPID", Me.GridView2.GetFocusedDataRow.Item("ReqPID")) = True Then
        If Me.GridView2.GetFocusedDataRow.Item("Jenis") = "Merah" Then
            Dim XR As New XRReqProdHarga
            XR.InitializeData(bind)
        Else
            Dim XR As New XRReqProd
            XR.InitializeData(bind)
        End If

        'Else
        '    Dim XR As New XRReqProd
        '    XR.InitializeData(bind)
        'End If
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

        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBBag.EditValue = "" Or IsDBNull(Me.TBBag.EditValue) Then
            XtraMessageBox.Show("Bagian Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUCust.EditValue = "" Or IsDBNull(Me.SLUCust.EditValue) Then
            XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_ReqP")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@Dok", SqlDbType.VarChar).Value = Me.CBODok.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@Bag", SqlDbType.VarChar).Value = Me.TBBag.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.TBUnit.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue

                    If Me.CBODok.EditValue = "Model" Then
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    ElseIf Me.CBODok.EditValue = "Diluar Model" Then
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView4.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    ElseIf Me.CBODok.EditValue = "BOM" Then
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView5.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    End If

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

                        If Me.CBODok.EditValue = "Model" Then
                            Dim i : For i = 0 To GridView1.RowCount - 1
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ReqPDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Req")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetMdl")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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

                            Dim z : For z = 0 To GridView3.RowCount - 1
                                If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ReqPQty")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Ket")
                                        .Parameters.Add("@Upp", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Upp")
                                        .Parameters.Add("@Hancur", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Hancur")
                                        .Parameters.Add("@Hilang", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Hilang")
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

                        ElseIf Me.CBODok.EditValue = "Diluar Model" Then

                            Dim i : For i = 0 To GridView4.RowCount - 1
                                If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ReqPDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Req")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "KetMdl")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BBIDInd")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Ket")
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

                        ElseIf Me.CBODok.EditValue = "BOM" Then

                            Dim i : For i = 0 To GridView5.RowCount - 1
                                If Not IsDBNull(Me.GridView5.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ReqPDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BOMID")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Req")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "KetMdl")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BBIDInd")
                                        .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "HarSat")
                                        .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "HarAkhir")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Ket")
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
                Dim cmSP As New SqlCommand("SPUpT_ReqP")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@Dok", SqlDbType.VarChar).Value = Me.CBODok.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@Bag", SqlDbType.VarChar).Value = Me.TBBag.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.TBUnit.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    If Me.CBODok.EditValue = "Model" Then
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    ElseIf Me.CBODok.EditValue = "Diluar Model" Then
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView4.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    ElseIf Me.CBODok.EditValue = "BOM" Then
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView5.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    End If
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_ReqPQty")
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

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_ReqPDtl")
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

                        If Me.CBODok.EditValue = "Model" Then
                            Dim i : For i = 0 To GridView1.RowCount - 1
                                If Me.GridView1.GetRowCellValue(i, "ReqPIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_ReqPDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                            .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                            .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                            .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Req")
                                            .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                            .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetMdl")
                                            .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                            .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                            .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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
                                            Me.GridView1.SetRowCellValue(i, "ReqPIDD", Me.GridView1.GetRowCellValue(i, "ReqPIDD") * -1)
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
                                        Dim cmSPDtl As New SqlCommand("SPUpT_ReqPDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqPIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                            .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                            .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                            .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Req")
                                            .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                            .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetMdl")
                                            .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                            .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                            .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
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

                            Dim z : For z = 0 To GridView3.RowCount - 1
                                If Me.GridView3.GetRowCellValue(z, "ReqPIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_ReqPQty")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Ket")
                                            .Parameters.Add("@Upp", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Upp")
                                            .Parameters.Add("@Hancur", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Hancur")
                                            .Parameters.Add("@Hilang", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Hilang")
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
                                            Me.GridView3.SetRowCellValue(i, "ReqPIDD", Me.GridView3.GetRowCellValue(i, "ReqPIDD") * -1)
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_ReqPQty")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "ReqPIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Ket")
                                            .Parameters.Add("@Upp", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Upp")
                                            .Parameters.Add("@Hancur", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Hancur")
                                            .Parameters.Add("@Hilang", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Hilang")
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

                        ElseIf Me.CBODok.EditValue = "Diluar Model" Then

                            Dim i : For i = 0 To GridView4.RowCount - 1
                                If Me.GridView4.GetRowCellValue(i, "ReqPIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_ReqPDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "DivID")
                                            .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "KompID")
                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BBID")
                                            .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "UkBB")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Std")
                                            .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Req")
                                            .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "KaliQty")
                                            .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "KetMdl")
                                            .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "stsJasa")
                                            .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "stsMentah")
                                            .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BBIDInd")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "HarAkhir")
                                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Ket")
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
                                            Me.GridView4.SetRowCellValue(i, "ReqPIDD", Me.GridView4.GetRowCellValue(i, "ReqPIDD") * -1)
                                        ElseIf x = -2 Then
                                            XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    If Not IsDBNull(Me.GridView4.GetRowCellValue(i, "BBID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_ReqPDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView4.GetRowCellValue(i, "ReqPIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "DivID")
                                            .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "KompID")
                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BBID")
                                            .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "UkBB")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Std")
                                            .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "Req")
                                            .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "KaliQty")
                                            .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "KetMdl")
                                            .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "stsJasa")
                                            .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView4.GetRowCellValue(i, "stsMentah")
                                            .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "BBIDInd")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView4.GetRowCellValue(i, "HarAkhir")
                                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView4.GetRowCellValue(i, "Ket")
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

                        ElseIf Me.CBODok.EditValue = "BOM" Then

                            Dim i : For i = 0 To GridView5.RowCount - 1
                                If Me.GridView5.GetRowCellValue(i, "ReqPIDD") < 0 Then
                                    If Not IsDBNull(Me.GridView5.GetRowCellValue(i, "BBID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsT_ReqPDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "DivID")
                                            .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "KompID")
                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BBID")
                                            .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "UkBB")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Std")
                                            .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Req")
                                            .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "KaliQty")
                                            .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "KetMdl")
                                            .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "stsJasa")
                                            .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "stsMentah")
                                            .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BBIDInd")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "HarAkhir")
                                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Ket")
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
                                            Me.GridView5.SetRowCellValue(i, "ReqPIDD", Me.GridView5.GetRowCellValue(i, "ReqPIDD") * -1)
                                        ElseIf x = -2 Then
                                            XtraMessageBox.Show("Bahan Baku Double Input!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        Else
                                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    If Not IsDBNull(Me.GridView5.GetRowCellValue(i, "BBID")) Then
                                        Dim cmSPDtl As New SqlCommand("SPUpT_ReqPDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView5.GetRowCellValue(i, "ReqPIDD")
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BOMID")
                                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "ArtCode")
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "DivID")
                                            .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "KompID")
                                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BBID")
                                            .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "UkBB")
                                            .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Sat")
                                            .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Std")
                                            .Parameters.Add("@Req", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "Req")
                                            .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "KaliQty")
                                            .Parameters.Add("@KetMdl", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "KetMdl")
                                            .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "stsJasa")
                                            .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView5.GetRowCellValue(i, "stsMentah")
                                            .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "BBIDInd")
                                            .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "HarSat")
                                            .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView5.GetRowCellValue(i, "HarAkhir")
                                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "Ket")
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

        Dim cmd2 As New SqlCommand("SPAftSReqP")
        cmd2.CommandType = CommandType.StoredProcedure

        With cmd2
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Connection = koneksi

            With koneksi
                .Open()
                cmd2.ExecuteNonQuery()
                .Close()
            End With

        End With

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        'Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
        '    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ReqPIDD")

        '    Me.GridView1.DeleteRow(i)
        'Next

        'Dim a : For a = Me.GridView4.RowCount - 1 To 0 Step -1
        '    ReDim Preserve arrPar3(arrPar3.GetUpperBound(0) + 1)
        '    arrPar3(arrPar3.GetUpperBound(0)) = Me.GridView4.GetRowCellValue(a, "ReqPIDD")

        '    Me.GridView4.DeleteRow(a)
        'Next
    End Sub

    Private Sub CBOJenis_Leave(sender As Object, e As EventArgs) Handles CBOJenis.Leave
        'Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
        '    Me.GridView1.ActiveFilter.Clear()

        'If Me.CBOJenis.EditValue = "Merah" Then
        '    Dim HarSat As Decimal

        '    Dim command As New SqlCommand("Select Isnull((Select Top 1 Round(HargaBeli+HargaBahan,2) From M_BBHarga Where BBID='" & Me.GridView1.GetRowCellValue(i, "BBID") & "' and Tanggal<='" & Me.DTPTanggal.EditValue & "' Order By Tanggal desc),0) ", koneksi)

        '    koneksi.Close()

        '    With koneksi
        '        .Open()
        '        HarSat = command.ExecuteScalar()
        '        .Close()
        '    End With

        '    Me.GridView1.SetRowCellValue(i, "HarSat", HarSat)
        '    Me.GridView1.SetRowCellValue(i, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(i, "Req") * HarSat, 2))
        'Else
        '    Me.GridView1.SetRowCellValue(i, "HarAkhir", 0)
        '    Me.GridView1.SetRowCellValue(i, "HarSat", 0)
        'End If
        'Next

        Me.GridView1.ActiveFilterString = "[BOMID] = '" & Me.GridView3.GetFocusedRowCellValue("BOMID") & "' and [ArtCode]='" & Me.GridView3.GetFocusedRowCellValue("ArtCode") & "'"
    End Sub

    Private Sub CBODok_Leave(sender As Object, e As EventArgs) Handles CBODok.Leave
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ReqPIDD")

            Me.GridView1.DeleteRow(i)
        Next

        Dim y : For y = Me.GridView3.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(y, "ReqPIDD")

            Me.GridView3.DeleteRow(y)
        Next

        Dim a : For a = Me.GridView4.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView4.GetRowCellValue(a, "ReqPIDD")

            Me.GridView4.DeleteRow(a)
        Next

        Dim b : For b = Me.GridView5.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView5.GetRowCellValue(b, "ReqPIDD")

            Me.GridView5.DeleteRow(b)
        Next

        If Me.CBODok.EditValue = "Diluar Model" Then
            Me.XTPDModel.PageVisible = True
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = False

            FillDtlDModel(Me.TBKode.EditValue)

        ElseIf Me.CBODok.EditValue = "Model" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = True
            Me.XTPBOM.PageVisible = False

            FillDtl(Me.TBKode.EditValue)

        ElseIf Me.CBODok.EditValue = "BOM" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = True

            FillDtlBOM(Me.TBKode.EditValue)
        End If
    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("ReqPIDD")


            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetFocusedDataRow.Item("BOMID") And Me.GridView1.GetRowCellValue(i, "ArtCode") = Me.GridView3.GetFocusedDataRow.Item("ArtCode") Then
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ReqPIDD")

                    Me.GridView1.DeleteRow(i)
                End If
            Next

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0
            rw2 = 0

            Dim frm As New FReqProd_a(Me.CBOJenis.EditValue, Me.SLUCust.EditValue, Me.DTPTanggal.EditValue)
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans2.Item("Baris").ToString) Then
                    Dim i : For i = 0 To CInt(dataTrans2.Item("Baris").ToString) - 1
                        If i <> CInt(dataTrans2.Item("Baris").ToString) - 1 Then
                            Me.GridView3.AddNewRow()
                        End If
                    Next
                End If

                If Not IsDBNull(dataTrans.Item("Baris").ToString) Then

                    Dim x : For x = 0 To CInt(dataTrans.Item("Baris").ToString)
                        If x <> CInt(dataTrans.Item("Baris").ToString) Then
                            Me.GridView1.AddNewRow()
                        End If
                    Next
                End If

                Me.GridView1.RefreshData()

            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub GridView3_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView3.CellValueChanged

        If e.Column Is GridView3.Columns("Qty") Then

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(i, "BOMID") = Me.GridView3.GetRowCellValue(e.RowHandle, "BOMID") And Me.GridView1.GetRowCellValue(i, "ArtCode") = Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") Then
                    Me.GridView1.SetRowCellValue(i, "Qty", Me.GridView3.GetRowCellValue(e.RowHandle, "Qty"))

                    If Me.GridView1.GetRowCellValue(i, "KaliQty") = True Then
                        Me.GridView1.SetRowCellValue(i, "Req", Math.Round(Me.GridView1.GetRowCellValue(i, "Std") * Me.GridView1.GetRowCellValue(i, "Qty"), 2))
                    Else
                        If Me.GridView1.GetRowCellValue(i, "Std") = 0 Then
                            Me.GridView1.SetRowCellValue(i, "Req", 0.0)
                        Else
                            Me.GridView1.SetRowCellValue(i, "Req", Math.Round(Me.GridView1.GetRowCellValue(i, "Qty") / Me.GridView1.GetRowCellValue(i, "Std"), 2))
                        End If

                    End If


                    'If Me.CBOJenis.EditValue = "Merah" Then
                    Me.GridView1.SetRowCellValue(i, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(i, "Req") * Me.GridView1.GetRowCellValue(i, "HarSat"), 2))
                    'Else
                    '    Me.GridView1.SetRowCellValue(i, "HarSat", 0)
                    '    Me.GridView1.SetRowCellValue(i, "HarAkhir", 0)
                    'End If

                End If
            Next

        ElseIf e.Column Is GridView3.Columns("Upp") Then
            If Me.GridView3.GetRowCellValue(e.RowHandle, "Upp") + Me.GridView3.GetRowCellValue(e.RowHandle, "Hancur") + Me.GridView3.GetRowCellValue(e.RowHandle, "Hilang") > Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") Then
                XtraMessageBox.Show("Tidak Boleh Melebihi Qty", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView3.SetRowCellValue(e.RowHandle, "Upp", Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView3.GetRowCellValue(e.RowHandle, "Hancur") - Me.GridView3.GetRowCellValue(e.RowHandle, "Hilang"))
            End If

        ElseIf e.Column Is GridView3.Columns("Hancur") Then
            If Me.GridView3.GetRowCellValue(e.RowHandle, "Upp") + Me.GridView3.GetRowCellValue(e.RowHandle, "Hancur") + Me.GridView3.GetRowCellValue(e.RowHandle, "Hilang") > Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") Then
                XtraMessageBox.Show("Tidak Boleh Melebihi Qty", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView3.SetRowCellValue(e.RowHandle, "Hancur", Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView3.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView3.GetRowCellValue(e.RowHandle, "Hilang"))
            End If

        ElseIf e.Column Is GridView3.Columns("Hilang") Then
            If Me.GridView3.GetRowCellValue(e.RowHandle, "Upp") + Me.GridView3.GetRowCellValue(e.RowHandle, "Hancur") + Me.GridView3.GetRowCellValue(e.RowHandle, "Hilang") > Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") Then
                XtraMessageBox.Show("Tidak Boleh Melebihi Qty", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView3.SetRowCellValue(e.RowHandle, "Hilang", Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView3.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView3.GetRowCellValue(e.RowHandle, "Hancur"))
            End If
        End If

    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try

            Me.GridView3.SetRowCellValue(e.RowHandle, "ReqPIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "ReqPID", Me.TBKode.EditValue)
            Me.GridView3.SetRowCellValue(e.RowHandle, "BOMID", dataTrans2.Item("BOMID" & rw2).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans2.Item("ArtCode" & rw2).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "ArtName", dataTrans2.Item("ArtName" & rw2).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "Qty", CDec(dataTrans2.Item("Qty" & rw2).ToString))
            Me.GridView3.SetRowCellValue(e.RowHandle, "Uk", dataTrans2.Item("Uk" & rw2).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "Ket", dataTrans2.Item("Ket" & rw2).ToString)

            RemoveHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

            Me.GridView3.SetRowCellValue(e.RowHandle, "Upp", dataTrans2.Item("Upp" & rw2).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "Hancur", dataTrans2.Item("Hancur" & rw2).ToString)
            Me.GridView3.SetRowCellValue(e.RowHandle, "Hilang", dataTrans2.Item("Hilang" & rw2).ToString)

            AddHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

            rw2 += 1

            If Me.GridView3.Editable = True Then
                If Me.GridView3.RowCount > 0 Then

                    Me.GridView1.ActiveFilterString = "[BOMID] = '" & Me.GridView3.GetFocusedRowCellValue("BOMID") & "' and [ArtCode]='" & Me.GridView3.GetFocusedRowCellValue("ArtCode") & "'"
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView3_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView3.FocusedRowChanged
        If Me.GridView3.Editable = True Then
            If Me.GridView3.RowCount > 0 Then

                Me.GridView1.ActiveFilterString = "[BOMID] = '" & Me.GridView3.GetFocusedRowCellValue("BOMID") & "' and [ArtCode]='" & Me.GridView3.GetFocusedRowCellValue("ArtCode") & "'"
            End If
        End If
    End Sub

    Private Sub GridView3_RowCountChanged(sender As Object, e As EventArgs) Handles GridView3.RowCountChanged
        If Me.GridView3.RowCount > 0 Then

            Me.GridView1.ActiveFilterString = "[BOMID] = '" & Me.GridView3.GetFocusedRowCellValue("BOMID") & "' and [ArtCode]='" & Me.GridView3.GetFocusedRowCellValue("ArtCode") & "'"
        End If
    End Sub

    Dim DivHapus, KompHapus, BBIDIndHapus As String
    Dim Hapus As Boolean

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Std") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "KaliQty") = True Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "Req", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Std"), 2))
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "Req", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") / Me.GridView1.GetRowCellValue(e.RowHandle, "Std"), 2))
            End If

            'If Me.CBOJenis.EditValue = "Merah" Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Req") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))
            'Else
            '    Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", 0)
            '    Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            'End If
        End If
    End Sub

    'Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
    '    If Me.GridView1.Editable = True Then
    '        If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) Then

    '            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
    '                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '            Else
    '                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ReqPIDD")

            DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
            KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
            BBIDIndHapus = Me.GridView1.GetFocusedDataRow.Item("BBID")

            If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = True Then
                Hapus = True
            Else
                Hapus = False
            End If

            'ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            '    rw = 0

            '    Dim frm As New FReqProd_a(Me.SLUCust.EditValue)
            '    frm.ShowDialog()

            '    Try
            '        If Not IsDBNull(dataTrans.Item("Baris").ToString) Then
            '            Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
            '                If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
            '                    Me.GridView1.AddNewRow()
            '                End If
            '            Next
            '        End If
            '    Catch ex As Exception

            '    End Try
        End If
    End Sub

    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        If Hapus = True Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(i, "BBIDInd") <> "" Then
                    If Me.GridView1.GetRowCellValue(i, "DivID") = DivHapus And Me.GridView1.GetRowCellValue(i, "KompID") = KompHapus And Me.GridView1.GetRowCellValue(i, "BBIDInd") = BBIDIndHapus Then

                        Me.GridView1.DeleteRow(i)

                    End If
                End If
            Next
        End If
    End Sub


    Private Sub GridControl4_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl4.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView4.GetFocusedDataRow.Item("ReqPIDD")
        End If

    End Sub
    Private Sub GridView4_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView4.InitNewRow
        Try
            RemoveHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

            Me.GridView4.SetRowCellValue(e.RowHandle, "ReqPIDD", Me.GridView4.RowCount * -1)
            Me.GridView4.SetRowCellValue(e.RowHandle, "ReqPID", Me.TBKode.EditValue)
            Me.GridView4.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "ArtCode", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "Uk", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "Qty", 0)
            Me.GridView4.SetRowCellValue(e.RowHandle, "DivID", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "KompID", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "UkBB", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "Std", 0)
            Me.GridView4.SetRowCellValue(e.RowHandle, "Req", 0)
            Me.GridView4.SetRowCellValue(e.RowHandle, "KaliQty", False)
            Me.GridView4.SetRowCellValue(e.RowHandle, "KetMdl", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "stsJasa", False)
            Me.GridView4.SetRowCellValue(e.RowHandle, "stsMentah", False)
            Me.GridView4.SetRowCellValue(e.RowHandle, "BBIDInd", "")
            Me.GridView4.SetRowCellValue(e.RowHandle, "HarSat", 0)
            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            Me.GridView4.SetRowCellValue(e.RowHandle, "Ket", "")

            AddHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBBID_ButtonClick1(sender As Object, e As ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Try

            Dim frm As New FSearch("BB No BOM", "%", "Bahan", "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView4.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "UkBB", dataTrans.Item("Uk").ToString)

                RemoveHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "Req", 0)
                Me.GridView4.SetRowCellValue(Me.GridView4.FocusedRowHandle, "HarSat", CDec(dataTrans.Item("Harga").ToString))

                AddHandler GridView4.CellValueChanged, AddressOf GridView4_CellValueChanged

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView4_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles GridView4.CellValueChanged
        If e.Column Is GridView4.Columns("Req") Then
            'If Me.CBOJenis.EditValue = "Merah" Then
            Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView4.GetRowCellValue(e.RowHandle, "Req") * Me.GridView4.GetRowCellValue(e.RowHandle, "HarSat"), 2))
            'Else
            '    Me.GridView4.SetRowCellValue(e.RowHandle, "HarSat", 0)
            '    Me.GridView4.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            'End If
        End If
    End Sub


    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FReqProd_d(Me.GridView2.GetFocusedDataRow.Item("ReqPID"), Me.GridView2.GetFocusedDataRow.Item("Dok"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "ReqPIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ReqPID", Me.TBKode.EditValue)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", dataTrans.Item("BOMID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", dataTrans.Item("ArtCode" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "ArtName", dataTrans.Item("ArtName" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", CDec(dataTrans.Item("Qty" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", dataTrans.Item("Uk" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DivID", dataTrans.Item("DivID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Divisi", dataTrans.Item("Divisi" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "KompID", dataTrans.Item("KompID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Komponen", dataTrans.Item("Komponen" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", dataTrans.Item("BBID" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Bahan", dataTrans.Item("Bahan" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "UkBB", dataTrans.Item("UkBB" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", dataTrans.Item("Sat" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Std", CDec(dataTrans.Item("Std" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "Req", CDec(dataTrans.Item("Req" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "KetMdl", dataTrans.Item("KetMdl" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", CBool(dataTrans.Item("stsJasa" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "stsMentah", CBool(dataTrans.Item("stsMentah" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "KaliQty", CBool(dataTrans.Item("KaliQty" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDInd", dataTrans.Item("BBIDInd" & rw).ToString)
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarSat", CDec(dataTrans.Item("HarSat" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", CDec(dataTrans.Item("HarAkhir" & rw).ToString))
            Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", dataTrans.Item("Ket" & rw).ToString)

            rw += 1

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            'If Me.GridView3.Editable = True Then

            Me.GridView1.ActiveFilterString = "[BOMID] = '" & Me.GridView3.GetFocusedRowCellValue("BOMID") & "' and [ArtCode]='" & Me.GridView3.GetFocusedRowCellValue("ArtCode") & "'"
            'End If

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    'Private Sub GridView1_RowCountChanged(sender As Object, e As EventArgs) Handles GridView1.RowCountChanged
    '    If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) Then

    '        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
    '            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '        Else
    '            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '        End If
    '    End If
    'End Sub

    Private Sub GridControl5_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl5.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView5.GetFocusedDataRow.Item("ReqPIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0

            Dim frm As New FReqProdBOM_a(Me.CBOJenis.EditValue, Me.DTPTanggal.EditValue)
            frm.ShowDialog()

            Try
                If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                    Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1
                        If i <> CInt(dataTrans.Item("Baris").ToString) - 1 Then
                            Me.GridView5.AddNewRow()
                        End If
                    Next
                End If


            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub GridView5_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView5.InitNewRow

        Try
            If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
                RemoveHandler GridView5.CellValueChanged, AddressOf GridView5_CellValueChanged

                Me.GridView5.SetRowCellValue(e.RowHandle, "ReqPIDD", Me.GridView5.RowCount * -1)
                Me.GridView5.SetRowCellValue(e.RowHandle, "ReqPID", Me.TBKode.EditValue)
                Me.GridView5.SetRowCellValue(e.RowHandle, "BOMID", dataTrans.Item("BOMID" & rw).ToString)
                Me.GridView5.SetRowCellValue(e.RowHandle, "ArtCode", "")
                Me.GridView5.SetRowCellValue(e.RowHandle, "Qty", 0)
                Me.GridView5.SetRowCellValue(e.RowHandle, "Uk", "")
                Me.GridView5.SetRowCellValue(e.RowHandle, "DivID", "")
                Me.GridView5.SetRowCellValue(e.RowHandle, "KompID", "")
                Me.GridView5.SetRowCellValue(e.RowHandle, "BBID", dataTrans.Item("BBID" & rw).ToString)
                Me.GridView5.SetRowCellValue(e.RowHandle, "Bahan", dataTrans.Item("Bahan" & rw).ToString)
                Me.GridView5.SetRowCellValue(e.RowHandle, "UkBB", dataTrans.Item("UkBB" & rw).ToString)
                Me.GridView5.SetRowCellValue(e.RowHandle, "Sat", dataTrans.Item("Sat" & rw).ToString)
                Me.GridView5.SetRowCellValue(e.RowHandle, "Std", 0)
                Me.GridView5.SetRowCellValue(e.RowHandle, "Req", 0)
                Me.GridView5.SetRowCellValue(e.RowHandle, "KetMdl", "")
                Me.GridView5.SetRowCellValue(e.RowHandle, "stsJasa", False)
                Me.GridView5.SetRowCellValue(e.RowHandle, "stsMentah", False)
                Me.GridView5.SetRowCellValue(e.RowHandle, "KaliQty", False)
                Me.GridView5.SetRowCellValue(e.RowHandle, "BBIDInd", "")
                Me.GridView5.SetRowCellValue(e.RowHandle, "HarSat", CDec(dataTrans.Item("HarSat" & rw).ToString))
                Me.GridView5.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
                Me.GridView5.SetRowCellValue(e.RowHandle, "Ket", "")

                rw += 1

                AddHandler GridView5.CellValueChanged, AddressOf GridView5_CellValueChanged
            End If

        Catch ex As Exception

            Me.GridView5.DeleteRow(e.RowHandle)
        End Try


    End Sub
    Private Sub GridView5_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles GridView5.CellValueChanged
        If e.Column Is GridView5.Columns("Req") Then
            'If Me.CBOJenis.EditValue = "Merah" Then
            Me.GridView5.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView5.GetRowCellValue(e.RowHandle, "Req") * Me.GridView5.GetRowCellValue(e.RowHandle, "HarSat"), 2))
            'Else
            '    Me.GridView5.SetRowCellValue(e.RowHandle, "HarSat", 0)
            '    Me.GridView5.SetRowCellValue(e.RowHandle, "HarAkhir", 0)
            'End If
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBBag.KeyPress, TBUnit.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub


    Private Sub GridView3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView3.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl3_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl3.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView3_KeyPress(grid.FocusedView, e)
    End Sub


End Class