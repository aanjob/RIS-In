Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid

Public Class FRPB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim Gol As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(BBSpM As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=11 and CabID='" & Gol & "'", koneksi)

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

        Me.Text = "Retur Pemakaian " & Gol

        If Gol = "Sparepart-Mesin" Then
            Me.GridColumn25.Visible = True
            Me.GridColumn26.Visible = True

            Me.GridColumn25.VisibleIndex = 0
            Me.GridColumn26.VisibleIndex = 1
            Me.GridColumn3.VisibleIndex = 2
            Me.GridColumn4.VisibleIndex = 3
            Me.GridColumn5.VisibleIndex = 4
            Me.GridColumn6.VisibleIndex = 5

        Else
            Me.GridColumn25.Visible = False
            Me.GridColumn26.Visible = False
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("RPBN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTRPB_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.SLUDiv.Properties.ReadOnly = True
        Me.CBODok.Properties.ReadOnly = True
        Me.SLUDocID.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
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
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTRPB_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.SLUDiv.Properties.ReadOnly = False
        Me.CBODok.Properties.ReadOnly = False
        Me.SLUDocID.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUGd.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTRPB_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DivID,Nama,Unit From M_Div Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_DivLUE")
        cmsl.Fill(DsMaster, "M_DivLUE")
        DsMaster.Tables("M_DivLUE").Clear()
        cmsl.Fill(DsMaster, "M_DivLUE")

        Me.SLUDiv.Properties.DataSource = DsMaster.Tables("M_DivLUE")
        Me.SLUDiv.Properties.DisplayMember = "Nama"
        Me.SLUDiv.Properties.ValueMember = "DivID"

        cmsl = New SqlDataAdapter("Select BOMID As DocID,ArtName,Warna,CustID From T_BOM Where stsLunas='False'", koneksi)
        cmsl.TableMappings.Add("Table", "T_DocIDLUE")
        cmsl.Fill(DsMaster, "T_DocIDLUE")
        DsMaster.Tables("T_DocIDLUE").Clear()
        cmsl.Fill(DsMaster, "T_DocIDLUE")

        Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_DocIDLUE")
        Me.SLUDocID.Properties.DisplayMember = "DocID"
        Me.SLUDocID.Properties.ValueMember = "DocID"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsMaster, "M_GudangLUE")
        DsMaster.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsMaster, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

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
        cmsl = New SqlDataAdapter("Select RPBIDD,RPBID,'" & InisialBC & "'+D.BBID As BBID,BtNum,B.Nama as Bahan,MesinID,(Select Nama From M_BB Where BBID=MesinID) as Mesin,D.Sat,Qty From T_RPBDtl D Inner Join M_BB B On D.BBID=B.BBID Where RPBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_RPBDtl")
        cmsl.Fill(DsMaster, "T_RPBDtl")
        DsMaster.Tables("T_RPBDtl").Clear()
        cmsl.Fill(DsMaster, "T_RPBDtl")

        DsMaster.Tables("T_RPBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_RPBDtl").Columns("BBID"), DsMaster.Tables("T_RPBDtl").Columns("BtNum")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_RPBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select RPBID,PeriodID,CodeID,Tanggal,H.Gol,Dok,DocID,H.CustID,C.Nama As Cust,H.GdID,G.Nama as Gudang,H.DivID, Dv.Nama As Bagian,Dv.Unit,H.Ket,H.stsApp,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy,(Select Count(*) From (Select (Select Sum(Masuk)-Sum(Keluar) From T_StokBB where DocID<>T_RPBDtl.RPBID and BBID=T_RPBDtl.BBID and BtNum=T_RPBDtl.BtNum) As stok,* From T_RPBDtl where RPBID=H.RPBID) as x where stok <0) as Cek From T_RPB H Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join M_Cust C On H.CustID=C.CustID Inner Join M_Div Dv On H.DivID=Dv.DivID where PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,RPBID asc", koneksi)

        'cmsl = New SqlDataAdapter("Select RPBID,PeriodID,CodeID,Tanggal,H.Gol,Dok,DocID,H.CustID,C.Nama As Cust,H.GdID,G.Nama as Gudang,H.DivID, Dv.Nama As Bagian,Dv.Unit,H.Ket,H.stsApp,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy,H.AppDate,H.AppBy From T_RPB H Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join M_Cust C On H.CustID=C.CustID Inner Join M_Div Dv On H.DivID=Dv.DivID where PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,RPBID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_RPB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_RPB" & Gol)
        DsMaster.Tables("T_RPB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_RPB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_RPB" & Gol
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

        Dim cmSP As New SqlCommand("SPDelT_RPB")
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
        If IO.File.Exists("SrBBRPBBtNum.xml") Then
            System.IO.File.Delete("SrBBRPBBtNum.xml")
        End If

        If IO.File.Exists("SrBBBtNumNS" & Gol & ".xml") Then
            System.IO.File.Delete("SrBBBtNumNS" & Gol & ".xml")
        End If
    End Sub

    Private Sub FRPB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Retur Pemakaian " & Gol
    End Sub

    Private Sub FRPB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FRPB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FRPB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTRPB_e.Selected = True

        If Manual = True Then
            Me.LCBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.GridColumn11.Visible = False
        Else
            Me.LCBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.GridColumn11.Visible = True
        End If
    End Sub

    Private Sub BVTRPB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTRPB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Retur Pemakaian " & Gol
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("RPBEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("RPBApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("RPBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("RPBP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Retur Pemakaian " & Gol

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
                Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            End If
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

        Me.SLUDiv.EditValue = ""
        Me.TBUnit.EditValue = ""
        Me.CBODok.EditValue = ""
        Me.SLUDocID.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_RPBDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Retur Pemakaian " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_RPB", "stsApp", "RPBID", Me.GridView2.GetFocusedDataRow.Item("RPBID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("RPBID")
        Me.CBODok.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dok")
        Me.SLUDocID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DocID")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.SLUDiv.EditValue = Me.GridView2.GetFocusedDataRow.Item("DivID")
        Me.TBUnit.EditValue = Me.GridView2.GetFocusedDataRow.Item("Unit")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        CekInsBC()
        RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        FillDtl(Me.TBKode.EditValue)

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        ReDim arrPar(-1)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()

        If Manual = False Then
            If Me.GridView1.GetRowCellValue(0, "Cek") < 0 Then
                Me.SLUDocID.Properties.ReadOnly = True
                Me.SLUGd.Properties.ReadOnly = True

                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = False

            Else
                Me.SLUDocID.Properties.ReadOnly = False
                Me.SLUGd.Properties.ReadOnly = False

                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = True
            End If

            'Dim Stok2 As Decimal

            'Dim command As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(0, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(0, "RPBIDD") & "','" & Me.GridView1.GetRowCellValue(0, "BtNum") & "')", koneksi)

            'With koneksi
            '    .Open()
            '    Stok2 = command.ExecuteScalar()
            '    .Close()
            'End With

            'If Stok2 < 0 Then
            '    Me.SLUDocID.Properties.ReadOnly = True
            '    Me.SLUGd.Properties.ReadOnly = True

            '    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
            '    Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = False
            '    Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = False

            'Else
            '    Me.SLUDocID.Properties.ReadOnly = False
            '    Me.SLUGd.Properties.ReadOnly = False

            '    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
            '    Me.GridView1.Columns("Qty").OptionsColumn.AllowEdit = True
            '    Me.GridView1.Columns("BtNum").OptionsColumn.AllowEdit = True
            'End If

        End If

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        CekSave = True
    End Sub

    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPAppRPB")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("RPBID")
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

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Retur Pemakaian " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If SlCek("T_RPB", "stsApp", "RPBID", Me.GridView2.GetFocusedDataRow.Item("RPBID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Manual = False Then
            Dim Hapus As Boolean = True

            FillDtl(Me.GridView2.GetFocusedDataRow.Item("RPBID"))

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                Dim Stok2 As Decimal

                Dim command As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.GridView2.GetFocusedDataRow.Item("GdID") & "','" & Me.GridView2.GetFocusedDataRow.Item("RPBID") & "'," & Me.GridView1.GetRowCellValue(i, "RPBIDD") & ",'" & Me.GridView1.GetRowCellValue(i, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok2 = command.ExecuteScalar()
                    .Close()
                End With

                If Stok2 < 0 Then
                    Hapus = False

                    Exit For
                Else
                    Hapus = True
                End If
            Next

            If Hapus = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Stok Bisa Minus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

        End If


        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("RPBID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_RPB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("RPBID")
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("RPBID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DocID"), "DocID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Bagian"), "Bagian")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Unit"), "Unit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        If Gol = "Bahan" Then
            Dim XR As New XRRPB
            XR.InitializeData(bind, Gol)
        Else
            Dim XR As New XRRPSpM
            XR.InitializeData(bind, Gol)
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.SLUDiv.EditValue = "" Or IsDBNull(Me.SLUDiv.EditValue) Then
            XtraMessageBox.Show("Bagian Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_RPB")
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
                    .Parameters.Add("@Dok", SqlDbType.VarChar).Value = Me.CBODok.EditValue
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUDocID.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_RPBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 11
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MesinID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
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
                Dim cmSP As New SqlCommand("SPUpT_RPB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@Dok", SqlDbType.VarChar).Value = Me.CBODok.EditValue
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUDocID.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.SLUDiv.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_RPBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "RPBIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_RPBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 11
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MesinID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
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
                                        Me.GridView1.SetRowCellValue(i, "RPBIDD", Me.GridView1.GetRowCellValue(i, "RPBIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_RPBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "RPBIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 11
                                        .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@MesinID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "MesinID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@BtNum", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BtNum")
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

    Private Sub CBODok_Leave(sender As Object, e As EventArgs) Handles CBODok.Leave
        If Not IsDBNull(Me.CBODok.EditValue) And Me.CBODok.Properties.ReadOnly = False Then
            Me.SLUDocID.EditValue = ""

            Dim cmsl As SqlDataAdapter
            If Me.CBODok.EditValue = "BOM" Then
                cmsl = New SqlDataAdapter("Select BOMID As DocID,B.CustID,C.Nama As Cust,ArtName,Warna From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Where stsApp='True' and stsLunas='False' or BOMID In (Select DocID From T_RPB where RPBID ='" & Me.TBKode.EditValue & "')", koneksi)
                cmsl.TableMappings.Add("Table", "T_DocIDLUE")
                cmsl.Fill(DsMaster, "T_DocIDLUE")
                DsMaster.Tables("T_DocIDLUE").Clear()
                cmsl.Fill(DsMaster, "T_DocIDLUE")
                DsMaster.Tables("T_DocIDLUE").Rows.Add("", "", "")

                Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_DocIDLUE")
                Me.SLUDocID.Properties.DisplayMember = "DocID"
                Me.SLUDocID.Properties.ValueMember = "DocID"

            ElseIf Me.CBODok.EditValue = "Request" Then
                cmsl = New SqlDataAdapter("Select ReqPID As DocID,R.CustID,C.Nama As Cust From T_ReqP R Left Outer Join M_Cust C On R.CustID=C.CustID Where stsApp='True' and stsLunas='False' or ReqPID In (Select DocID From T_RPB where RPBID ='" & Me.TBKode.EditValue & "')", koneksi)
                cmsl.TableMappings.Add("Table", "T_DocIDLUE")
                cmsl.Fill(DsMaster, "T_DocIDLUE")
                DsMaster.Tables("T_DocIDLUE").Clear()
                cmsl.Fill(DsMaster, "T_DocIDLUE")
                DsMaster.Tables("T_DocIDLUE").Rows.Add("", "", "")

                Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_DocIDLUE")
                Me.SLUDocID.Properties.DisplayMember = "DocID"
                Me.SLUDocID.Properties.ValueMember = "DocID"
            End If


            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RPBIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub SLUDocID_Leave(sender As Object, e As EventArgs) Handles SLUDocID.Leave
        If Me.SLUDocID.EditValue <> "" And Not IsDBNull(Me.SLUDocID.EditValue) And Me.SLUDocID.Properties.ReadOnly = False Then
            Me.SLUCust.Properties.ReadOnly = True
            Me.SLUCust.EditValue = DsMaster.Tables("T_DocIDLUE").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("CustID")

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "RPBIDD")

                Me.GridView1.DeleteRow(i)
            Next
        Else
            Me.SLUCust.Properties.ReadOnly = False
        End If
    End Sub

    Private Sub SLUDiv_Leave(sender As Object, e As EventArgs) Handles SLUDiv.Leave
        Me.TBUnit.EditValue = DsMaster.Tables("M_DivLUE").Select("DivID = '" & Me.SLUDiv.EditValue & "'")(0).Item("Unit")
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("RPBIDD")
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            koneksi.Close()

            If Me.SLUDocID.EditValue <> "" Then
                Dim Stok As Integer

                Dim command As New SqlCommand("Select Isnull(Sum(Qty),0)-(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=B.DocID and BBID=D.BBID and H.RPBID<>'" & Me.TBKode.EditValue & "' and BtNum='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "'),0)) From T_BPB B Inner Join T_BPBDtl D On B.BPBID=D.BPBID Where B.DocID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "' and BtNum='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "' Group By B.DocID,D.BBID", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pemakaian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If
            End If
        End If

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        koneksi.Close()

        If Manual = False Then
            If Me.GridView1.RowCount - 1 > 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID")) Then
                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") <> "" Then
                        Dim Stok2 As Decimal

                        Dim commandx As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "RPBIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)

                        With koneksi
                            .Open()
                            Stok2 = commandx.ExecuteScalar()
                            .Close()
                        End With

                        If Stok2 + Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") <= 0 And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty") > 0 Then
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FRPB_d(Gol, Me.GridView2.GetFocusedDataRow.Item("RPBID"), Me.GridView2.GetFocusedDataRow.Item("GdID"), Manual)
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "RPBIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MesinID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BtNum", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Try
            If Me.SLUDocID.EditValue <> "" Then
                Dim frm As New FSearch("BB RPB BtNum", Me.SLUDocID.EditValue, Me.TBKode.EditValue, InisialBC, Date.Now, "")
                frm.ShowDialog()

                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum", dataTrans.Item("BtNum").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                    RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", CDec(dataTrans.Item("Qty").ToString))
                    AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
                End If

            Else
                Dim frm As New FSearch("Bahan BtNum No Stok", InisialBC, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, "")
                frm.ShowDialog()

                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum", dataTrans.Item("BtNum").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdMesinID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdMesinID.ButtonClick
        Try
            Dim frm As New FSearch("Master Mesin", InisialBC, Gol, "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        If Manual = True Then
            CekInsBC()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView1.ValidateRow
        Dim View As GridView = CType(sender, GridView)
        Dim BtNumCol As GridColumn = View.Columns("BtNum")

        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") = "" Then
            e.Valid = False
            View.SetColumnError(BtNumCol, "Batch Number Harus Diisi")

        ElseIf Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Replace(Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum"), " ", "") = "Rekal" Then
            e.Valid = False
            View.SetColumnError(BtNumCol, "Batch Number Rekal Tidak Boleh Dipakai")
        End If
    End Sub

    
End Class