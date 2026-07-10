Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBPB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, JnsPPn, BtNumLama As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim InisialBC As String = ""
    Dim bind As New Collection
    Dim Gol As String

    Public Sub New(BBSpM As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = BBSpM

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=8 and CabID='" & Gol & "'", koneksi)

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

        Me.BVTBPB_e.Caption = "Pemakaian " & Gol
        Me.Text = "Bukti Pemakaian " & Gol


        If Gol = "Sparepart-Mesin" Then
            Me.GridColumn26.Visible = True
            Me.GridColumn27.Visible = True

            Me.GridColumn26.VisibleIndex = 0
            Me.GridColumn27.VisibleIndex = 1
            Me.GridColumn3.VisibleIndex = 2
            Me.GridColumn4.VisibleIndex = 3
            Me.GridColumn5.VisibleIndex = 4
            Me.GridColumn6.VisibleIndex = 5


        Else
            Me.GridColumn26.Visible = False
            Me.GridColumn27.Visible = False
        End If

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BPBN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTBPB_s.Enabled = True

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
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintH.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTBPB_s.Enabled = False

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

        Me.BVTBPB_e.Selected = True
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
        cmsl = New SqlDataAdapter("Select BPBIDD,BPBID,MesinID,(Select Nama From M_BB Where BBID=MesinID) as Mesin,'" & InisialBC & "'+D.BBID as BBID,BtNum,B.Nama as Bahan,D.Sat,Qty From T_BPBDtl D Inner Join M_BB B On D.BBID=B.BBID Where BPBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BPBDtl")
        Try
            DsMaster.Tables("T_BPBDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BPBDtl")

        DsMaster.Tables("T_BPBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BPBDtl").Columns("BBID"), DsMaster.Tables("T_BPBDtl").Columns("BtNum")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BPBDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BPBID,PeriodID,CodeID,Tanggal,H.Gol,Dok,DocID,H.CustID,C.Nama As Cust,H.GdID,G.Nama as Gudang,H.DivID,Dv.Nama As Bagian,Dv.Unit,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_BPB H Inner Join M_Gudang G On H.GdID=G.GdID Left Outer Join M_Cust C On H.CustID=C.CustID Inner Join M_Div Dv On H.DivID=Dv.DivID Where PeriodID=" & MainModule.periodAktif & " and H.Gol ='" & Gol & "' Order By Tanggal,BPBID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_BPB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_BPB" & Gol)
        DsMaster.Tables("T_BPB" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_BPB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_BPB" & Gol
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

        Dim cmSP As New SqlCommand("SPDelT_BPB")
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

    Public Sub Print(Print As String)
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        bind = New Collection
        bind.Add(Gol, "Gol")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("BPBID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DocID"), "DocID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Bagian"), "Bagian")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Unit"), "Unit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")
        bind.Add(Manual, "Manual")

        If Print = "Harga" Then
            If Gol = "Bahan" Then
                Dim XR As New XRBPBHarga
                XR.InitializeData(bind, Gol)
            End If
        Else
            If Gol = "Bahan" Then
                If Manual = False Then
                    Dim XR As New XRBPBBtNum
                    XR.InitializeData(bind, Gol)
                Else
                    Dim XR As New XRBPB
                    XR.InitializeData(bind, Gol)
                End If
            Else
                Dim XR As New XRBPSpM
                XR.InitializeData(bind, Gol)
            End If
        End If
    End Sub

    Public Sub DelXml()
        If IO.File.Exists("SrBBBOMBtNum" & Me.SLUDocID.EditValue & ".xml") Then
            System.IO.File.Delete("SrBBBOMBtNum" & Me.SLUDocID.EditValue & ".xml")
        End If

        If IO.File.Exists("SrBBReqBtNum.xml") Then
            System.IO.File.Delete("SrBBReqBtNum.xml")
        End If

        If IO.File.Exists("SrBBBtNum" & Gol & ".xml") Then
            System.IO.File.Delete("SrBBBtNum" & Gol & ".xml")
        End If
    End Sub

    Private Sub FBPB_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Bukti Pemakaian " & Gol
    End Sub

    Private Sub FBPB_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FBPB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FBPB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBPB_e.Selected = True

        If Manual = True Then
            Me.LCBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.GridColumn11.Visible = False
            Me.GridColumn23.Visible = False
        Else
            Me.LCBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIBOM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.GridColumn11.Visible = True
            Me.GridColumn23.Visible = True
        End If

    End Sub

    Private Sub BVTBPB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBPB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Bukti Pemakaian " & Gol
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BPBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BPBDel"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("BPBP"), Boolean)
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("BPBPH"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Bukti Pemakaian Bahan"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodNew() = True Then
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

        Me.SLUDiv.EditValue = ""
        Me.TBUnit.EditValue = ""
        Me.CBODok.EditValue = ""
        Me.SLUDocID.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_BPBDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Bukti Pemakaian " & Gol

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBBGd(Me.GridView2.GetFocusedDataRow.Item("GdID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Gudang Tersebut Sudah Diopname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("BPBID")
        LUE()
        Me.CBODok.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dok")

        Dim cmsl As SqlDataAdapter
        If Me.CBODok.EditValue = "BOM" Then
            cmsl = New SqlDataAdapter("Select BOMID As DocID,B.CustID,C.Nama As Cust,ArtName,Warna From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Where stsApp='True' and stsLunas='False' or BOMID In (Select DocID From T_BPB where BPBID ='" & Me.TBKode.EditValue & "')", koneksi)
            cmsl.TableMappings.Add("Table", "T_DocIDLUE")
            cmsl.Fill(DsMaster, "T_DocIDLUE")
            DsMaster.Tables("T_DocIDLUE").Clear()
            cmsl.Fill(DsMaster, "T_DocIDLUE")
            DsMaster.Tables("T_DocIDLUE").Rows.Add("", "", "")

            Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_DocIDLUE")
            Me.SLUDocID.Properties.DisplayMember = "DocID"
            Me.SLUDocID.Properties.ValueMember = "DocID"

        ElseIf Me.CBODok.EditValue = "Request" Then
            cmsl = New SqlDataAdapter("Select ReqPID As DocID,R.CustID,C.Nama As Cust From T_ReqP R Left Outer Join M_Cust C On R.CustID=C.CustID Where stsApp='True' and stsLunas='False' or ReqPID In (Select DocID From T_BPB where BPBID ='" & Me.TBKode.EditValue & "')", koneksi)
            cmsl.TableMappings.Add("Table", "T_DocIDLUE")
            cmsl.Fill(DsMaster, "T_DocIDLUE")
            DsMaster.Tables("T_DocIDLUE").Clear()
            cmsl.Fill(DsMaster, "T_DocIDLUE")
            DsMaster.Tables("T_DocIDLUE").Rows.Add("", "", "")

            Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_DocIDLUE")
            Me.SLUDocID.Properties.DisplayMember = "DocID"
            Me.SLUDocID.Properties.ValueMember = "DocID"
        End If

        Me.SLUDocID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DocID")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUGd.EditValue = Me.GridView2.GetFocusedDataRow.Item("GdID")
        Me.SLUDiv.EditValue = Me.GridView2.GetFocusedDataRow.Item("DivID")
        Me.TBUnit.EditValue = Me.GridView2.GetFocusedDataRow.Item("Unit")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        CekInsBC()
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

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Bukti Pemakaian " & Gol

        koneksi.Close()

        If MainModule.SlOpBB() > 0 Or MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("BPBID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_BPB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BPBID")
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
        Print("")
    End Sub

    Private Sub BVBPrintH_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintH.ItemClick
        Print("Harga")
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Manual = False Then
            If Me.SLUCust.EditValue = "" Or IsDBNull(Me.SLUCust.EditValue) Then
                XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If Me.SLUDiv.EditValue = "" Or IsDBNull(Me.SLUDiv.EditValue) Then
            XtraMessageBox.Show("Bagian Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView1.RowCount = 0 Or Math.Round(CType(Me.GridView1.Columns("Qty").SummaryText, Decimal), 2) <= 0 Then
            XtraMessageBox.Show("Data atau Qty Harus Diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Select Case Indicator

            Case 100

                Dim cmSP As New SqlCommand("SPInsT_BPB")
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
                                Dim cmSPDtl As New SqlCommand("SPInsT_BPBDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 8
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
                Dim cmSP As New SqlCommand("SPUpT_BPB")
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
                            Dim cmSPDel As New SqlCommand("SPDelT_BPBDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "BPBIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_BPBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 8
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
                                        Me.GridView1.SetRowCellValue(i, "BPBIDD", Me.GridView1.GetRowCellValue(i, "BPBIDD") * -1)
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
                                    Dim cmSPDtl As New SqlCommand("SPUpT_BPBDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BPBIDD")
                                        .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 8
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
        End Select

        LockControl()
        CekSave = False

        Me.BVTBPB_s.Selected = True
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("BPBP"), Boolean)
        Me.BVBPrintH.Enabled = CType(TcodeCollection.Item("BPBPH"), Boolean)
        FillDt()

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(a, "BPBID") = Me.TBKode.EditValue Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView2.FocusedRowHandle = fc

        Print("")
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub CBODok_Leave(sender As Object, e As EventArgs) Handles CBODok.Leave
        Me.SLUDocID.EditValue = ""

        Dim cmsl As SqlDataAdapter
        If Me.CBODok.EditValue = "BOM" Then
            cmsl = New SqlDataAdapter("Select BOMID As DocID,BOMID,B.CustID,C.Nama As Cust,ArtName,Warna From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Where stsApp='True' and stsLunas='False' or BOMID In (Select DocID From T_BPB where BPBID ='" & Me.TBKode.EditValue & "') Union All Select TambahanID As DocID,B.BOMID,H.CustID,C.Nama As Cust,ArtName,Warna From T_BOMTam B Inner Join T_BOM H On B.BOMID=H.BOMID Left Outer Join M_Cust C On H.CustID=C.CustID Where B.stsApp='True' and stsLunas='False' or TambahanID In (Select DocID From T_BPB where BPBID ='" & Me.TBKode.EditValue & "') Union All Select Distinct MD.MemoID As DocID,MD.BOMID,H.CustID,C.Nama As Cust,ArtName,Warna From T_Memo M Inner Join T_MemoDtl MD On M.MemoID=MD.MemoID Inner Join T_BOM H On MD.BOMID=H.BOMID Left Outer Join M_Cust C On H.CustID=C.CustID Where M.stsApp='True' and stsLunas='False' or MD.MemoID In (Select DocID From T_BPB where BPBID ='" & Me.TBKode.EditValue & "')", koneksi)
            cmsl.TableMappings.Add("Table", "T_DocIDLUE")
            cmsl.Fill(DsMaster, "T_DocIDLUE")
            DsMaster.Tables("T_DocIDLUE").Clear()
            cmsl.Fill(DsMaster, "T_DocIDLUE")
            DsMaster.Tables("T_DocIDLUE").Rows.Add("", "", "")

            Me.SLUDocID.Properties.DataSource = DsMaster.Tables("T_DocIDLUE")
            Me.SLUDocID.Properties.DisplayMember = "DocID"
            Me.SLUDocID.Properties.ValueMember = "DocID"

        ElseIf Me.CBODok.EditValue = "Request" Then
            cmsl = New SqlDataAdapter("Select ReqPID As DocID,R.CustID,C.Nama As Cust From T_ReqP R Left Outer Join M_Cust C On R.CustID=C.CustID Where stsApp='True' and stsLunas='False' or ReqPID In (Select DocID From T_BPB where BPBID ='" & Me.TBKode.EditValue & "')", koneksi)
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
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "BPBIDD")

            Me.GridView1.DeleteRow(i)
        Next
    End Sub

    Private Sub SLUDocID_Leave(sender As Object, e As EventArgs) Handles SLUDocID.Leave
        If Me.SLUDocID.EditValue <> "" Then
            Me.SLUCust.Properties.ReadOnly = True
            Me.SLUCust.EditValue = DsMaster.Tables("T_DocIDLUE").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("CustID")

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "BPBIDD")

                Me.GridView1.DeleteRow(i)
            Next
        Else
            Me.SLUCust.Properties.ReadOnly = False
        End If
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("BPBIDD")
        End If
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            koneksi.Close()
            Dim Stok, Stok1, Stok2, Stok3, StokTemp As Decimal

            'If Manual = False Then
            If Me.SLUDocID.EditValue <> "" Then
                Dim command, command2 As New SqlCommand
                Dim BPBLain As Decimal = 0

                If Me.CBODok.EditValue = "BOM" Then
                    'MsgBox(Me.TBKode.EditValue)
                    'MsgBox(Me.DTPTanggal.EditValue)
                    'MsgBox(Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum"))
                    'MsgBox(Me.SLUGd.EditValue)
                    'MsgBox(Me.SLUDocID.EditValue)
                    'MsgBox(Me.TBKode.EditValue)
                    'MsgBox(Me.TBKode.EditValue)

                    Dim i : For i = 0 To Me.GridView1.RowCount - 1
                        If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView1.GetFocusedRowCellValue("BBID") And Me.GridView1.GetRowCellValue(i, "BPBIDD") <> Me.GridView1.GetFocusedRowCellValue("BPBIDD") Then
                            BPBLain += Me.GridView1.GetRowCellValue(i, "Qty")
                        End If
                    Next

                    command = New SqlCommand("Select Sum(Qty) From (Select Isnull((Select Case When Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0))-(Select Isnull((Select Sum(KebAs) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.BOMID and BBIDAs=D.BBID and stsApp='True'),0))-" & BPBLain & " > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Else Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0))-(Select Isnull((Select Sum(KebAs) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.BOMID and BBIDAs=D.BBID and stsApp='True'),0))-" & BPBLain & " End  As Qty From T_BOMDtl D Where BOMID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' Group By D.BOMID,D.BBID),0) As Qty Union All Select Isnull((Select Case When Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.TambahanID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+ (Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.TambahanID and BBID=D.BBID),0))-(Select Isnull((Select Sum(KebAs) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.TambahanID and BBIDAs=D.BBID and stsApp='True'),0))-" & BPBLain & " > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Else Round(Sum(Keb)+Sum(Pol),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.TambahanID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.TambahanID and BBID=D.BBID),0))-(Select Isnull((Select Sum(KebAs) From T_Memo MH Inner Join T_MemoDtl MD On MH.MemoID=MD.MemoID Where BOMID=D.TambahanID and BBIDAs=D.BBID and stsApp='True'),0))-" & BPBLain & " End  As Qty From T_BOMTamDtl D Where TambahanID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' Group By D.TambahanID,D.BBID),0) As Qty Union All Select Isnull((Select Case When Round(Sum(KebTj),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.MemoID and BBID=D.BBIDTj and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+ (Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.MemoID and BBID=D.BBIDTj),0))-" & BPBLain & " > dbo.fcStokBB(D.BBIDTj,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Then dbo.fcStokBB(D.BBIDTj,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Else Round(Sum(KebTj),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID  Where DocID=D.MemoID and BBID=D.BBIDTj and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.MemoID and BBID=D.BBIDTj),0))-" & BPBLain & " End As Qty From T_Memo MH Inner Join T_MemoDtl D On MH.MemoID=D.MemoID Where MH.MemoID='" & Me.SLUDocID.EditValue & "' and BBIDTj='" & Me.GridView1.GetFocusedDataRow.Item("BBID") & "' and stsApp='True'  Group By D.MemoID,D.BBIDTj),0) As Qty) as x", koneksi)

                    With koneksi
                        .Open()
                        Stok1 = command.ExecuteScalar()
                        .Close()
                    End With

                    command2 = New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BPBIDD") & ",'" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "')", koneksi)

                    With koneksi
                        .Open()
                        Stok2 = command2.ExecuteScalar()
                        .Close()
                    End With

                    Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BPBIDD") & ")", koneksi)

                    With koneksi
                        .Open()
                        Stok3 = command3.ExecuteScalar()
                        .Close()
                    End With

                    If Stok2 > Stok3 Then
                        StokTemp = Stok3
                    Else
                        StokTemp = Stok2
                    End If

                    If Stok1 > StokTemp Then
                        Stok = StokTemp
                    Else
                        Stok = Stok1
                    End If

                    'If Stok1 > Stok2 Then
                    '    Stok = Stok2
                    'Else
                    '    Stok = Stok1
                    'End If
                    ' MsgBox(BPBLain)
                    'MsgBox(Stok) '0
                    MsgBox(Stok1) '0
                    MsgBox(Stok2) '138
                    MsgBox(Stok3) '245
                    MsgBox(StokTemp) '138


                    If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                        XtraMessageBox.Show("Qty Tidak Boleh Melebihi " & Me.CBODok.EditValue & "/Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                    End If

                ElseIf Me.CBODok.EditValue = "Request" Then
                    Dim i : For i = 0 To Me.GridView1.RowCount - 1
                        If Me.GridView1.GetRowCellValue(i, "BBID") = Me.GridView1.GetFocusedRowCellValue("BBID") And Me.GridView1.GetRowCellValue(i, "BPBIDD") <> Me.GridView1.GetFocusedRowCellValue("BPBIDD") Then
                            BPBLain += Me.GridView1.GetRowCellValue(i, "Qty")
                        End If
                    Next

                    command = New SqlCommand("Select Case When Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0)))- " & BPBLain & " > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "') Else Ceiling(Sum(Req)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.ReqPID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.ReqPID and BBID=D.BBID),0)))- " & BPBLain & " End From T_ReqPDtl D Where ReqPID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "' Group By D.ReqPID,D.BBID", koneksi)

                    With koneksi
                        .Open()
                        Stok1 = command.ExecuteScalar()
                        .Close()
                    End With

                    command2 = New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BPBIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)

                    With koneksi
                        .Open()
                        Stok2 = command2.ExecuteScalar()
                        .Close()
                    End With

                    Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BPBIDD") & ")", koneksi)

                    With koneksi
                        .Open()
                        Stok3 = command3.ExecuteScalar()
                        .Close()
                    End With

                    If Stok2 > Stok3 Then
                        StokTemp = Stok3
                    Else
                        StokTemp = Stok2
                    End If

                    If Stok1 > StokTemp Then
                        Stok = StokTemp
                    Else
                        Stok = Stok1
                    End If


                    'If Stok1 > Stok2 Then
                    '    Stok = Stok2
                    'Else
                    '    Stok = Stok1
                    'End If

                    If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                        'If XtraMessageBox.Show("Apakah Qty Yang Diinput Melebihi " & Me.CBODok.EditValue & "/Stok ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        '    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                        'End If

                        XtraMessageBox.Show("Qty Tidak Boleh Melebihi " & Me.CBODok.EditValue & "/Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                    End If
                End If

            Else

                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(e.RowHandle, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok1 = command.ExecuteScalar()
                    .Close()
                End With

                Dim command2 As New SqlCommand("Select dbo.fcStokBBBtNumAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BPBIDD") & ",'" & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok2 = command2.ExecuteScalar()
                    .Close()
                End With

                Dim command3 As New SqlCommand("Select dbo.fcStokBBAll('" & Me.GridView1.GetRowCellValue(e.RowHandle, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.TBKode.EditValue & "'," & Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BPBIDD") & ")", koneksi)

                With koneksi
                    .Open()
                    Stok3 = command3.ExecuteScalar()
                    .Close()
                End With

                If Stok2 > Stok3 Then
                    StokTemp = Stok3
                Else
                    StokTemp = Stok2
                End If

                If Stok1 > StokTemp Then
                    Stok = StokTemp
                Else
                    Stok = Stok1
                End If

                'If Stok1 > Stok2 Then
                '    Stok = Stok2
                'Else
                '    Stok = Stok1
                'End If

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If


            End If
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FBPB_d(Gol, Me.GridView2.GetFocusedDataRow.Item("BPBID"), Me.GridView2.GetFocusedDataRow.Item("GdID"), Manual)
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "BPBIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MesinID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "BtNum", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)

            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging

        BtNumLama = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum")
    End Sub


    Private Sub BEdBBID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        If Me.SLUGd.EditValue = "" Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            If Me.CBODok.EditValue <> "" Then
                If Me.CBODok.EditValue = "BOM" Then
                    MsgBox(Me.SLUDocID.EditValue)

                    MsgBox(Me.TBKode.EditValue)
                    MsgBox(Me.SLUGd.EditValue)
                    MsgBox(Me.DTPTanggal.EditValue)
                    MsgBox(InisialBC)
                    Dim frm As New FSearch("BB BOM BtNum", Me.SLUDocID.EditValue, Me.TBKode.EditValue, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, InisialBC)
                    frm.ShowDialog()

                ElseIf Me.CBODok.EditValue = "Request" Then
                    Dim frm As New FSearch("BB Request BtNum", Me.SLUDocID.EditValue, Me.TBKode.EditValue, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, InisialBC)
                    frm.ShowDialog()
                End If

                If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                    RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BtNum", dataTrans.Item("BtNum").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)

                    Me.BSave.Focus()
                    Me.GridView1.Focus()

                    AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", CDec(dataTrans.Item("Qty").ToString))

                End If

            Else
                Dim frm As New FSearch("Bahan BtNum", InisialBC, Gol, Me.SLUGd.EditValue, Me.DTPTanggal.EditValue, "")
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

    Private Sub BEdMesinID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdMesinID.ButtonClick
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

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        koneksi.Close()

        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Dim Stok As Decimal

            If Me.SLUDocID.EditValue <> "" Then

                Dim command As New SqlCommand("Select Case When Round(Sum(Keb),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "') Else Round(Sum(Keb),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) End From T_BOMDtl D Where BOMID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(i, "BBID") & "' Group By D.BOMID,D.BBID", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If
            Else

                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If

            End If
        Next

    End Sub

    Private Sub SLUDiv_Leave(sender As Object, e As EventArgs) Handles SLUDiv.Leave
        Me.TBUnit.EditValue = DsMaster.Tables("M_DivLUE").Select("DivID = '" & Me.SLUDiv.EditValue & "'")(0).Item("Unit")
    End Sub

    Private Sub SLUGd_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        koneksi.Close()

        CekInsBC()

        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Dim Stok As Decimal

            If Me.SLUDocID.EditValue <> "" Then

                Dim command As New SqlCommand("Select Case When Round(Sum(Keb),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) > dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "') Then dbo.fcStokBB(D.BBID,'" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "') Else Round(Sum(Keb),0)-(Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and H.BPBID<>'" & Me.TBKode.EditValue & "'),0))+(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID),0)) End From T_BOMDtl D Where BOMID='" & Me.SLUDocID.EditValue & "' and BBID='" & Me.GridView1.GetRowCellValue(i, "BBID") & "' Group By D.BOMID,D.BBID", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If
            Else

                Dim command As New SqlCommand("Select dbo.fcStokBB('" & Me.GridView1.GetRowCellValue(i, "BBID") & "','" & Me.SLUGd.EditValue & "','" & Me.DTPTanggal.EditValue & "','" & Me.TBKode.EditValue & "','" & Me.GridView1.GetRowCellValue(i, "BtNum") & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView1.GetRowCellValue(i, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty ID Bahan : " & Me.GridView1.GetRowCellValue(i, "BBID") & " Melebihi Stok Bahan", "Peringatan",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(i, "Qty", Stok)
                End If

            End If
        Next
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class