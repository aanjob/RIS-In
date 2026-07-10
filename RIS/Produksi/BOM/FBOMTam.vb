Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

'Detail BOM yang berwarna merah berarti sudah ditarik PO berarti meskipun di model ada perubahan dan di proses di BOM tidak akan berubah
'Update Model-BOM : Tinggal diklik Kode Modelny

Public Class FBOMTam
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim Ukuran As String = ""
    Dim ArtCenter As String = ""
    Dim centermin, centerplus As Integer
    Dim arrPar(-1), arrPar2(-1) As String
    Dim ModelID As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=76", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BOMTN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BOMTEd"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("BOMTApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BOMTDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTBOM_s.Enabled = True

        Me.SLUBOM.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.TBArtName.Properties.ReadOnly = True
        Me.TBWarna.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True

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
        Me.BVBPrintB.Enabled = False
        Me.BVBPrintS.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTBOM_s.Enabled = False

        Me.SLUBOM.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBOM_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BOMID,CustID,ArtName,Warna,MdlID From T_BOM where stsLunas='False' and stsBatal='False' and stsApp='True'", koneksi)
        cmsl.TableMappings.Add("Table", "BOMLUE")
        cmsl.Fill(DsMaster, "BOMLUE")
        DsMaster.Tables("BOMLUE").Clear()
        cmsl.Fill(DsMaster, "BOMLUE")

        Me.SLUBOM.Properties.DataSource = DsMaster.Tables("BOMLUE")
        Me.SLUBOM.Properties.DisplayMember = "BOMID"
        Me.SLUBOM.Properties.ValueMember = "BOMID"

        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Clear()
        cmsl.Fill(DsMaster, "M_CustL")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Public Sub FillDtl(Kode As String, BOMID As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TambahanIDD,TambahanID,ArtCode, BD.Uk, BD.DivID,D.Nama as Divisi, BD.KompID, K.Nama As Komponen, BD.BBID,B.Nama As Bahan,BD.Uk,BD.UkBB,BD.Sat,Std,Qty,Keb,Pol,BD.Ket,KaliQty,SPK,stsAdd,BD.stsJasa,BD.stsMentah,BD.BBIDInd,(Select Sum (Jml) From(Select Count(*) As Jml From T_POBBDtl D1 Where D1.BOMID= '" & Kode & "' and BBID=BD.BBID Union All Select Count(*) As Jml From T_BPB H1 Inner Join T_BPBDtl D1 On H1.BPBID=D1.BPBID Where H1.DocID= '" & Kode & "' and BBID=BD.BBID Union All Select Count(*) As Jml From T_RPB H1 Inner Join T_RPBDtl D1 On H1.RPBID=D1.RPBID Where H1.DocID= '" & Kode & "' and BBID=BD.BBID)As x) As PO From T_BOMTamDtl BD Inner Join M_Div D On BD.DivID=D.DivID Inner Join M_Komp K On BD.KompID=K.KompID Inner Join M_BB B On BD.BBID=B.BBID Where TambahanID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMTamDtl")
        Try
            DsMaster.Tables("T_BOMTamDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMTamDtl")

        DsMaster.Tables("T_BOMTamDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMTamDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMTamDtl").Columns("DivID"), DsMaster.Tables("T_BOMTamDtl").Columns("KompID"), DsMaster.Tables("T_BOMTamDtl").Columns("BBID"), DsMaster.Tables("T_BOMTamDtl").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BOMTamDtl"

        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & BOMID & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & BOMID & "' and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & BOMID & "' Order By BP.Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & BOMID & "' Order By BP.Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & BOMID & "' Order By Cast(BP.Uk as Decimal(18,2))", koneksi)
            End If
        End If


        cmsl.TableMappings.Add("Table", "T_BOMPO")
        Try
            DsMaster.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMPO")

        DsMaster.Tables("T_BOMPO").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMPO").Columns("POID"), DsMaster.Tables("T_BOMPO").Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_BOMPO"

        'cmsl = New SqlDataAdapter("Select BBID From T_POBBDtl Where BOMID='" & BOMID & "'", koneksi)

        'cmsl.TableMappings.Add("Table", "POBB")
        'cmsl.Fill(DsMaster, "POBB")
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select TambahanID,BT.BOMID,BT.PeriodID,BT.Tanggal,B.TglKirim,B.SPK,S.SpecID,S.Brand,S.StyleID,S.ShoeLast, B.ArtName, B.Warna,M.MdlID,M.SampleSize,B.CustID,C.Nama As Cust,POID,HCBP,B.TotPsg,B.TotPsgPol,B.Ket,BT.stsApp,BT.InsDate, BT.InsBy,BT.UpdDate,BT.UpdBy,BT.AppDate,BT.AppBy From T_BOMTam BT Inner Join T_BOM B On BT.BOMID=B.BOMID Inner Join M_Model M On B.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Left Outer Join M_Cust C on B.CustID=C.CustID Where BT.PeriodID=" & MainModule.periodAktif & " Order By Tanggal,BOMID Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMTam")
        Try
            DsMaster.Tables("T_BOMTam").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMTam")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_BOMTam"
    End Sub

    Public Sub HitKeb()

        'Dim x : For x = Me.GridView1.RowCount - 1 To 0 Step -1
        Try
            Dim i : For i = 0 To DsMaster.Tables("T_BOMPO").Rows.Count - 1
                Dim x : For x = 0 To DsMaster.Tables("T_BOMTamDtl").Rows.Count - 1
                    If DsMaster.Tables("T_BOMTamDtl").Rows(x).RowState <> DataRowState.Deleted Then
                        If DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("stsAdd") = False Then
                            If DsMaster.Tables("T_BOMPO").Rows(i).RowState <> DataRowState.Deleted Then
                                If DsMaster.Tables("T_BOMPO").Rows(i).Item("ArtCode") = DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("ArtCode") Then
                                    DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Qty") = DsMaster.Tables("T_BOMPO").Rows(i).Item("Tot")
                                End If
                            End If

                            If DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("KaliQty") = True Then
                                DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Keb") = Math.Round(DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Qty") * DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Std"), 2, MidpointRounding.AwayFromZero)
                            Else
                                If DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Std") <> 0 Then
                                    DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Keb") = Math.Round(DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Qty") / DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Std"), 2, MidpointRounding.AwayFromZero)
                                Else
                                    DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Keb") = 0
                                End If
                            End If

                            'DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Pol") = Math.Round(DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("Keb") * (DsMaster.Tables("T_BOMTamDtl").Rows(x).Item("PrsPol") / 100), 2, MidpointRounding.AwayFromZero)
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            XtraMessageBox.Show("Standart Model ada yang kosong !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_BOMTam")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.SLUBOM.EditValue
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

    Private Sub FBOM_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Bill Of Materials"
    End Sub

    Private Sub FBOM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FBOM_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBOM_e.Selected = True
    End Sub

    Private Sub BVTBOM_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBOM_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Bill Of Materials Tambahan"
        FillDt()
        Me.BVBPrintB.Enabled = CType(TcodeCollection.Item("BOMTPB"), Boolean)
        Me.BVBPrintS.Enabled = CType(TcodeCollection.Item("BOMTPS"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Bill Of Materials Tambahan"

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

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        'If Manual = True Then
        '    Me.SLUBOM.Properties.ReadOnly = False
        '    Me.SLUBOM.EditValue = ""
        'Else
        '    Me.SLUBOM.Properties.ReadOnly = True
        '    Me.SLUBOM.EditValue = "--"
        'End If

        Me.SLUBOM.EditValue = ""
        Me.TBArtName.EditValue = ""
        Me.TBWarna.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.TBArtName.EditValue = ""
        Me.TBWarna.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue, Me.SLUBOM.EditValue)
        DsMaster.Tables("T_BOMTamDtl").Clear()
        DsMaster.Tables("T_BOMPO").Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Bill Of Materials"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If SlCek("T_BOMTam", "stsApp", "BOMID", Me.GridView2.GetFocusedDataRow.Item("TambahanID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("TambahanID")
        Me.SLUBOM.EditValue = Me.GridView2.GetFocusedDataRow.Item("BOMID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.TBArtName.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtName")
        Me.TBWarna.EditValue = Me.GridView2.GetFocusedDataRow.Item("Warna")
        ModelID = Me.GridView2.GetFocusedDataRow.Item("MdlID")

        FillDtl(Me.TBKode.EditValue, Me.SLUBOM.EditValue)
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

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
        If SlCek("T_BOMTam", "stsApp", "TambahanID", Me.GridView2.GetFocusedDataRow.Item("TambahanID")) = True Then
            XtraMessageBox.Show("Data Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmSP As New SqlCommand("SPAppBOMReq")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TambahanID")
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
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Bill Of Materials"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SlCek("T_BOMTam", "stsApp", "TambahanID", Me.GridView2.GetFocusedDataRow.Item("TambahanID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlCekBOMPO(Me.GridView2.GetFocusedDataRow.Item("TambahanID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Pengiriman Atau Bahan Sudah Dikeluarkan ke Produksi Atau Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("TambahanID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_BOMTam")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("TambahanID")
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

    Private Sub BVBPrintB_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintB.ItemClick
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TambahanID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("BOMID"), "BOMID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Brand"), "Brand")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Warna"), "Warna")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "Tot")

        Dim XR As New XRBOMTam
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBPrintS_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintS.ItemClick
        Dim frm As New FBOM_uk(Me.GridView2.GetFocusedDataRow.Item("BOMID"))

        frm = New FBOM_uk(Me.GridView2.GetFocusedDataRow.Item("BOMID"))
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TambahanID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("BOMID"), "BOMID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SPK"), "SPK")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Cust"), "Customer")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "POID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Brand"), "Brand")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SpecID"), "SpecID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Warna"), "Warna")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SampleSize"), "SampleSize")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "Tot")

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select Uk,Tot,IsiDlmDos From T_BOMPO Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' Order By Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select Uk,Tot,IsiDlmDos From T_BOMPO Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' Order By Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select Uk,Tot,IsiDlmDos From T_BOMPO Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' Order By Cast(Uk as Decimal(18,2))", koneksi)
            End If
        End If

        cmsl.TableMappings.Add("Table", "T_BOMPO")
        DsLap = New System.Data.DataSet
        Try
            DsLap.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLap, "T_BOMPO")

        Dim i : For i = 1 To 8
            If i <= DsLap.Tables("T_BOMPO").Rows.Count Then
                Dim x : For x = 0 To DsLap.Tables("T_BOMPO").Rows.Count - 1
                    If i = x + 1 Then
                        bind.Add(DsLap.Tables("T_BOMPO").Rows(x).Item("Uk"), "Uk" & i)
                        bind.Add(DsLap.Tables("T_BOMPO").Rows(x).Item("Tot"), "Qty" & i)
                        bind.Add(DsLap.Tables("T_BOMPO").Rows(x).Item("IsiDlmDos"), "Isi" & i)
                    End If
                Next
            Else
                bind.Add("", "Uk" & i)
                bind.Add("", "Qty" & i)
                bind.Add("", "Isi" & i)

            End If
        Next

        bind.Add(dataTrans.Item(1).ToString, "UkStd")

        Dim XR As New XRSPKTam
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_BOMTam")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.SLUBOM.EditValue
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
                            Del()
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_BOMTamDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                    .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Keb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Keb")
                                    .Parameters.Add("@Pol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pol")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                    .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                    .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                    .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAdd")
                                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                    .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                    .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.SLUBOM.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            Del()
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
                Dim cmSP As New SqlCommand("SPUpT_BOMTam")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.SLUBOM.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_BOMTamDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "TambahanIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_BOMTamDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Keb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Keb")
                                        '.Parameters.Add("@PrsPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PrsPol")
                                        .Parameters.Add("@Pol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pol")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAdd")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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
                                        Me.GridView1.SetRowCellValue(i, "TambahanIDD", Me.GridView1.GetRowCellValue(i, "TambahanIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_BOMTamDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TambahanIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Keb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Keb")
                                        '.Parameters.Add("@PrsPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PrsPol")
                                        .Parameters.Add("@Pol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pol")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAdd")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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

    Private Sub SLUBOM_Leave(sender As Object, e As EventArgs) Handles SLUBOM.Leave

        If Me.SLUBOM.Properties.ReadOnly = False Then
            Try
                'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

                Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "BOMIDD")

                    Me.GridView3.DeleteRow(x)
                Next

                Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "TambahanIDD")

                    Me.GridView1.DeleteRow(y)
                Next

                Me.TBArtName.EditValue = DsMaster.Tables("BOMLUE").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("ArtName")
                Me.TBWarna.EditValue = DsMaster.Tables("BOMLUE").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("Warna")
                Me.SLUCust.EditValue = DsMaster.Tables("BOMLUE").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("CustID")
                ModelID = DsMaster.Tables("BOMLUE").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("MdlID")

                Select Case Indicator
                    Case 100

                        Try
                            DsMaster.Tables("T_BOMDtl").Clear()
                            DsMaster.Tables("T_BOMPO").Clear()
                        Catch ex As Exception

                        End Try

                        Dim cmsl As SqlDataAdapter
                        Dim jml, assx As Integer

                        Dim command As New SqlCommand("Select Sum(jml) From (Select Isnull(Max(Len(Uk)),0) As Jml From T_BOMPO Where BOMID='" & Me.SLUBOM.EditValue & "') as x", koneksi)

                        With koneksi
                            .Open()
                            jml = command.ExecuteScalar()
                            .Close()
                        End With

                        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID='" & Me.SLUBOM.EditValue & "'  and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

                        With koneksi
                            .Open()
                            assx = command2.ExecuteScalar()
                            .Close()
                        End With

                        If jml > 4 Then
                            cmsl = New SqlDataAdapter("Select BOMIDD,POID,ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,IsiDlmDos,Uk,Qty, QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOM.EditValue & "' Order By Uk", koneksi)
                        Else
                            If assx > 0 Then
                                cmsl = New SqlDataAdapter("Select BOMIDD,POID,ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,IsiDlmDos,Uk, Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOM.EditValue & "' Order By Uk", koneksi)
                            Else
                                cmsl = New SqlDataAdapter("Select BOMIDD,POID,ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,IsiDlmDos,Uk, Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOM.EditValue & "' Order By Cast(Uk as Decimal(18,2))", koneksi)
                            End If
                        End If

                        cmsl.TableMappings.Add("Table", "T_BOMPO")
                        cmsl.SelectCommand.CommandTimeout = 9000
                        cmsl.Fill(DsMaster, "T_BOMPO")

                        Me.GridControl3.DataSource = DsMaster
                        Me.GridControl3.DataMember = "T_BOMPO"

                    Case 200

                        'Dim Hapus As Boolean

                        'Dim a : For a = Me.GridView1.RowCount - 1 To 0 Step -1
                        '    If DsMaster.Tables("T_BOMPO").Rows.Count - 1 > 0 Then
                        '        Dim z : For z = 0 To DsMaster.Tables("T_BOMPO").Rows.Count - 1
                        '            If Me.GridView1.GetRowCellValue(a, "ArtCode") = DsMaster.Tables("T_BOMPO").Rows(z).Item("ArtCode") Then
                        '                Hapus = False
                        '                Exit For
                        '            Else
                        '                Hapus = True
                        '            End If
                        '        Next
                        '    Else
                        '        Hapus = True
                        '    End If

                        '    If Hapus = True Then
                        '        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        '        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(a, "BOMIDD")

                        '        Me.GridView1.DeleteRow(a)
                        '    End If
                        'Next

                        Dim jml, assx As Integer
                        Dim cmsl As SqlDataAdapter

                        Dim command As New SqlCommand("Select Sum(jml) From (Select Isnull(Max(Len(Uk)),0) As Jml From T_BOMPO Where BOMID='" & Me.SLUBOM.EditValue & "') as x", koneksi)

                        With koneksi
                            .Open()
                            jml = command.ExecuteScalar()
                            .Close()
                        End With


                        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID='" & Me.SLUBOM.EditValue & "'  and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

                        With koneksi
                            .Open()
                            assx = command2.ExecuteScalar()
                            .Close()
                        End With

                        If jml > 4 Then
                            cmsl = New SqlDataAdapter("Select BOMIDD,POID,ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,IsiDlmDos,Uk,Qty, QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOM.EditValue & "' Order By Uk", koneksi)
                        Else
                            If assx > 0 Then
                                cmsl = New SqlDataAdapter("Select BOMIDD,POID,ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,IsiDlmDos,Uk, Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOM.EditValue & "' Order By Uk", koneksi)
                            Else
                                cmsl = New SqlDataAdapter("Select BOMIDD,POID,ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,IsiDlmDos,Uk, Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOM.EditValue & "' Order By Cast(Uk as Decimal(18,2))", koneksi)
                            End If
                        End If

                        cmsl.TableMappings.Add("Table", "T_BOMPO")
                        cmsl.SelectCommand.CommandTimeout = 9000
                        cmsl.Fill(DsMaster, "T_BOMPO")

                        Me.GridControl3.DataSource = DsMaster
                        Me.GridControl3.DataMember = "T_BOMPO"

                End Select

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub


    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("TambahanIDD")

        ElseIf (e.Button.ButtonType = NavigatorButtonType.Append) Then
            rw = 0


            Dim frm As New FBOMTam_a(ModelID)
            frm.ShowDialog()
            frm.Close()

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as TambahanIDD,ArtCode,M.Uk, M.DivID,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,M.UkBB,B.Nama As Bahan,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID+M.DivID+M.KompID+M.BBID In (" & BOMTam & ") Order By DivID,K.Urut,KompID,BBID,M.Uk,M.BBIDInd", koneksi)

            cmsl.TableMappings.Add("Table", "T_BOMTamDtl")
            cmsl.SelectCommand.CommandTimeout = 9000
            cmsl.Fill(DsMaster, "T_BOMTamDtl")

            DsMaster.Tables("T_BOMTamDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMTamDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMTamDtl").Columns("DivID"), DsMaster.Tables("T_BOMTamDtl").Columns("KompID"), DsMaster.Tables("T_BOMTamDtl").Columns("BBID"), DsMaster.Tables("T_BOMTamDtl").Columns("BBIDInd")}

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_BOMTamDtl"

            HitKeb()

        End If
    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow

        Me.GridView1.DeleteRow(e.RowHandle)

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FBOMTam_d(Me.GridView2.GetFocusedDataRow.Item("TambahanID"), Me.GridView2.GetFocusedDataRow.Item("BOMID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "UkBB" Or view.FocusedColumn.FieldName = "Ket" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

End Class