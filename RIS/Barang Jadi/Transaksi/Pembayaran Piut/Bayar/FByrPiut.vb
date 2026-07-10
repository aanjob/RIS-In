Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FByrPiut
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, Gol, CurrID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim IdD As Integer
    Dim arrPar(-1) As String
    Dim arrPar2(-1) As String

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        If Gol <> "Own" And Gol <> "Job Order" Then
            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=34 and Gol='" & Gol & "'", koneksi)

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
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("ByrPN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BExExcel.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTBayar_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
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
        Me.BExExcel.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTBayar_s.Enabled = False

        'Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBayar_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        If Gol <> "Own" And Gol <> "Job Order" Then
            cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustL")
            cmsl.Fill(DsMaster, "M_CustL")
            DsMaster.Tables("M_CustL").Clear()
            cmsl.Fill(DsMaster, "M_CustL")

            Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
            Me.SLUCust.Properties.DisplayMember = "Nama"
            Me.SLUCust.Properties.ValueMember = "CustID"

        Else
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
        End If

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"
    End Sub

    Public Sub CekCurr()
        Dim cmSl As New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Me.DTPTanggal.EditValue & "') AND (Akhir >= '" & Me.DTPTanggal.EditValue & "') AND (MtUang = '" & Me.SLUMtUang.EditValue & "')ORDER BY Tanggal DESC")
        cmSl.CommandType = CommandType.Text
        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim Reader As SqlClient.SqlDataReader

        With cmSl
            .Connection = koneksi

            With koneksi
                .Open()
                Reader = cmSl.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        CurrID = Reader.Item(0)
                        Me.TBNilTukarRp.EditValue = Reader.Item(1)
                    End While
                Else
                    XtraMessageBox.Show("Nilai Tukar Rupiah Untuk Tanggal " & Format(Me.DTPTanggal.EditValue, "dd MMMM yyyy") & " Belum Diinput !" & vbCrLf & "Silakan diinput Terlebih Dahulu !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.Dispose()
                End If
                Reader.Close()

                .Close()
            End With
        End With
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_ByrPiutDtl" & Gol).Clear()
            DsMaster.Tables("T_ByrPiutDtl2" & Gol).Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ByrPiutIDD,ByrPiutID,DocID,CaraBayar,(Select Isnull((Select Nama From M_JnsPot Where PotID=T_ByrPiutDtl.DocID),'')) As Deskripsi,SisaPakai,Case When CaraBayar='BG' Then (Select Isnull((Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where DocID=T_BG.BGID and D.ByrPiutID<>'" & Me.TBKode.EditValue & "'),0)) From T_BG Where BGID=T_ByrPiutDtl.DocID),0)) When CaraBayar='Cash' Then	(Select Isnull((Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where DocID=T_Cash.CashID and D.ByrPiutID<>'" & Me.TBKode.EditValue & "'),0)) From T_Cash Where CashID=T_ByrPiutDtl.DocID),0)) When CaraBayar='JM' Then	(Select Isnull((Select TotBayar-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where DocID=T_JMPiut.JMID and D.ByrPiutID<>'" & Me.TBKode.EditValue & "'),0)) From T_JMPiut Where JMID=T_ByrPiutDtl.DocID),0)) When CaraBayar='Retur' Then (Select Isnull((Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where DocID=T_RtrBJ.RtrID and D.ByrPiutID<>'" & Me.TBKode.EditValue & "'),0)) From T_RtrBJ Where RtrID=T_ByrPiutDtl.DocID),0)) Else 0 End As SisaPakai2 From T_ByrPiutDtl Where ByrPiutID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_ByrPiutDtl" & Gol)
        cmsl.Fill(DsMaster, "T_ByrPiutDtl" & Gol)

        DsMaster.Tables("T_ByrPiutDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ByrPiutDtl" & Gol).Columns("DocID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ByrPiutDtl" & Gol

        cmsl = New SqlDataAdapter("Select D2.ByrPiutIDD,ByrPiutDtl,D2.ByrPiutID,D.CaraBayar,JualID,Bayar,(Select Isnull((Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl D Inner Join T_ByrPiutDtl2 D2 On D.ByrPiutIDD=D2.ByrPiutDtl Where JualID=T_JualBJ.JualID and D2.ByrPiutID<>'" & Me.TBKode.EditValue & "'),0)) From T_JualBJ Where JualID=D2.JualID),0)) As SisaBayar From T_ByrPiutDtl2 D2 Inner Join T_ByrPiutDtl D On D2.ByrPiutDtl=D.ByrPiutIDD Where D2.ByrPiutID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_ByrPiutDtl2" & Gol)
        cmsl.Fill(DsMaster, "T_ByrPiutDtl2" & Gol)

        DsMaster.Tables("T_ByrPiutDtl2" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_ByrPiutDtl2" & Gol).Columns("ByrPiutDtl"), DsMaster.Tables("T_ByrPiutDtl2" & Gol).Columns("JualID")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_ByrPiutDtl2" & Gol

        If Me.GridView1.RowCount > 0 Then
            '    If Me.GridView3.RowCount > 0 Then
            Me.GridView3.ActiveFilterString = "[ByrPiutDtl] = '" & Me.GridView1.GetFocusedRowCellValue("ByrPiutIDD") & "'"
            '    End If
        End If

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        If Gol = "Lain-Lain" Or Gol = "Penjualan Bahan" Then
            cmsl = New SqlDataAdapter("Select ByrPiutID,PeriodID,Tanggal,Jenis,H.CabID,Cb.Cabang,H.CustID,C.Nama As Cust,C.Alamat,K.Nama As Kota,CurrID, MtUang,NilTukarRp,TotBayar,TotBayarRp,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_ByrPiut H Left Outer Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "'Order By Tanggal,ByrPiutID asc", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select ByrPiutID,PeriodID,Tanggal,Jenis,H.CabID,Cb.Cabang,H.CustID,C.Nama As Cust,C.Alamat,K.Nama As Kota,CurrID, MtUang,NilTukarRp,TotBayar,TotBayarRp,H.Ket,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From T_ByrPiut H Left Outer Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,ByrPiutID asc", koneksi)
        End If



        cmsl.TableMappings.Add("Table", "T_ByrPiut" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_ByrPiut" & Gol)
        DsMaster.Tables("T_ByrPiut" & Gol).Clear()
        cmsl.Fill(DsMaster, "T_ByrPiut" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_ByrPiut" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_ByrPiut")
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
        If IO.File.Exists("SrJualByr.xml") Then
            System.IO.File.Delete("SrJualByr.xml")
        End If

        If IO.File.Exists("SrByrPiut.xml") Then
            System.IO.File.Delete("SrByrPiut.xml")
        End If

        If IO.File.Exists("SrByrPiutRtr.xml") Then
            System.IO.File.Delete("SrByrPiutRtr.xml")
        End If

        If IO.File.Exists("SrByrPiutJM.xml") Then
            System.IO.File.Delete("SrByrPiutJM.xml")
        End If

        If IO.File.Exists("SrJualLain2Byr.xml") Then
            System.IO.File.Delete("SrJualLain2Byr.xml")
        End If
    End Sub

#Region "Export Excel"

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Apakah Anda Mau Membuka File Ini?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ExportTo(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView2.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub


    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String, ByVal Nama As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Nama
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

#End Region

    Private Sub FByrPiut_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Pembayaran Piutang"
    End Sub

    Private Sub FByrPiut_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FByrPiut_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FByrPiut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBayar_e.Selected = True
    End Sub

    Private Sub BVTBayar_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBayar_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Pembayaran Piutang"
        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("ByrPEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("ByrPDel"), Boolean)
        Me.BExExcel.Enabled = CType(TcodeCollection.Item("ByrPExEc"), Boolean) 'CType(TcodeCollection.Item("ByrPExEc"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Pembayaran Piutang"

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

        If Gol <> "Own" And Gol <> "Job Order" Then
            Me.SLUCab.Properties.ReadOnly = True
        End If

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.CBOJenis.EditValue = ""
        Me.SLUCab.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_ByrPiutDtl" & Gol).Clear()
        DsMaster.Tables("T_ByrPiutDtl2" & Gol).Clear()
        'DsMaster.Tables("M_CustL").Clear()
        'DsMaster.Tables("M_UsCabLUE").Clear()

        ReDim arrPar(-1)

        Me.GridView3.ActiveFilter.Clear()

        CekCurr()

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Pembayaran Piutang"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        DelXml()

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ByrPiutID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")

        If Gol = "Own" Or Gol = "Job Order" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "' ", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustL")
            Try
                DsMaster.Tables("M_CustL").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_CustL")

            Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
            Me.SLUCust.Properties.DisplayMember = "Nama"
            Me.SLUCust.Properties.ValueMember = "CustID"
        End If

        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        CurrID = Me.GridView2.GetFocusedDataRow.Item("CurrID")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        Me.TBNilTukarRp.EditValue = Me.GridView2.GetFocusedDataRow.Item("NilTukarRp")
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
        Me.SLUCab.Properties.ReadOnly = True
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Pembayaran Piutang " & MonthName(MainModule.periodeBulan) & " " & MainModule.periodeTahun & "")

        'FillDtl(Me.GridView2.GetFocusedDataRow.Item("ByrPiutID"))

        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))

            OpenFile(fileName)
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Pembayaran Piutang"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("ByrPiutID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_ByrPiut")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("ByrPiutID")
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
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()

        If Math.Round(CType(Me.GridView3.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) <> Math.Round(CType(Me.GridView1.Columns("SisaPakai").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) Then
            XtraMessageBox.Show("Pembayaran Tidak Balance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim o : For o = 0 To GridView3.RowCount - 1
            Dim SisaBayar As Decimal = 0


            Dim a : For a = 0 To GridView3.RowCount - 1

                If Me.GridView3.GetRowCellValue(o, "JualID") = Me.GridView3.GetRowCellValue(a, "JualID") Then
                    SisaBayar += Me.GridView3.GetRowCellValue(a, "Bayar")
                End If
            Next

            If SisaBayar > Me.GridView3.GetRowCellValue(o, "SisaBayar") Then
                XtraMessageBox.Show("Pembayaran " & Me.GridView3.GetRowCellValue(o, "JualID") & " Melebihi Sisa Bayar Faktur", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        Next

        Dim u : For u = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(u, "CaraBayar") <> "DbCr Note" Then
                Dim SisaPakai As Decimal = 0

                Dim t : For t = 0 To GridView1.RowCount - 1

                    If Me.GridView1.GetRowCellValue(u, "DocID") = Me.GridView1.GetRowCellValue(t, "DocID") Then
                        SisaPakai += Me.GridView1.GetRowCellValue(t, "SisaPakai")
                    End If
                Next

                If SisaPakai > Me.GridView1.GetRowCellValue(u, "SisaPakai") Then
                    XtraMessageBox.Show("Dokumen " & Me.GridView1.GetRowCellValue(u, "DocID") & " Melebihi Sisa Pakai Dokumen", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
        Next

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_ByrPiut")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 34
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    If Gol = "Own" Or Gol = "Job Order" Then
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    Else
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = ""
                    End If
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView3.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Math.Round(CType(Me.GridView3.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "DocID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_ByrPiutDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                    .Parameters.Add("@Cara", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CaraBayar")
                                    .Parameters.Add("@SisaPakai", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SisaPakai")
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

                                Dim z : For z = 0 To GridView3.RowCount - 1
                                    If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JualID")) Then
                                        If Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") = Me.GridView3.GetRowCellValue(z, "ByrPiutDtl") Then
                                            Dim cmSPDtl2 As New SqlCommand("SPInsT_ByrPiutDtl2")
                                            cmSPDtl2.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl2
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@ByrPiutDtl", SqlDbType.VarChar).Value = IdD
                                                .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                                .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "JualID")
                                                .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Bayar")
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
                Dim cmSP As New SqlCommand("SPUpT_ByrPiut")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 34
                    '.Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    If Gol = "Own" Or Gol = "Job Order" Then
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    Else
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = ""
                    End If
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                    .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView3.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotBayarRp", SqlDbType.Decimal).Value = Math.Round(Math.Round(CType(Me.GridView3.Columns("Bayar").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) * Me.TBNilTukarRp.EditValue, 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelT_ByrPiutDtl")
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
                            Dim cmSPDel As New SqlCommand("SPDelT_ByrPiutDtl2")
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
                            If Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "DocID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_ByrPiutDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                        .Parameters.Add("@Cara", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CaraBayar")
                                        .Parameters.Add("@SisaPakai", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SisaPakai")
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
                                        Me.GridView1.SetRowCellValue(i, "ByrPiutIDD", Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "ByrPiutIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JualID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") = Me.GridView3.GetRowCellValue(z, "ByrPiutDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_ByrPiutDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@ByrPiutDtl", SqlDbType.VarChar).Value = IdD
                                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "JualID")
                                                        .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Bayar")
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
                                                        Me.GridView3.SetRowCellValue(i, "ByrPiutIDD", Me.GridView3.GetRowCellValue(i, "ByrPiutIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        Else

                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JualID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") = Me.GridView3.GetRowCellValue(z, "ByrPiutDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_ByrPiutDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ByrPiutIDD")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@ByrPiutDtl", SqlDbType.VarChar).Value = IdD
                                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "JualID")
                                                        .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Bayar")
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
                                        End If
                                    Next
                                End If

                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "DocID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_ByrPiutDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ByrPiutIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                        .Parameters.Add("@Cara", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CaraBayar")
                                        .Parameters.Add("@SisaPakai", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "SisaPakai")
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

                                    Dim z : For z = 0 To GridView3.RowCount - 1
                                        If Me.GridView3.GetRowCellValue(z, "ByrPiutIDD") < 0 Then
                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JualID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") = Me.GridView3.GetRowCellValue(z, "ByrPiutDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPInsT_ByrPiutDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@ByrPiutDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") 'IdD
                                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "JualID")
                                                        .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Bayar")
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
                                                        Me.GridView3.SetRowCellValue(i, "ByrPiutIDD", Me.GridView3.GetRowCellValue(i, "ByrPiutIDD") * -1)
                                                    Else
                                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If

                                        Else

                                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "JualID")) Then
                                                If Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") = Me.GridView3.GetRowCellValue(z, "ByrPiutDtl") Then
                                                    Dim cmSPDtl2 As New SqlCommand("SPUpT_ByrPiutDtl2")
                                                    cmSPDtl2.CommandType = CommandType.StoredProcedure

                                                    With cmSPDtl2
                                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ByrPiutIDD")
                                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                        .Parameters.Add("@ByrPiutDtl", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ByrPiutIDD") 'IdD
                                                        .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DocID")
                                                        .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "JualID")
                                                        .Parameters.Add("@Bayar", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Bayar")
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

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        If Me.SLUCab.EditValue <> "" And Not IsDBNull(Me.SLUCab.EditValue) And Me.SLUCab.Properties.ReadOnly = False Then
            If Gol = "Own" Or Gol = "Job Order" Then
                Dim Reader As SqlClient.SqlDataReader
                Dim command As New SqlCommand("Select Distinct Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=34 and Gol='" & Gol & "' and CabID='" & Me.SLUCab.EditValue & "'", koneksi)

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
            End If

            If Manual = True Then
                Me.TBKode.Properties.ReadOnly = False
                Me.TBKode.EditValue = ""
            Else
                Me.TBKode.Properties.ReadOnly = True
                Me.TBKode.EditValue = "--"
            End If

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where C.Aktif='True' and CabID='" & Me.SLUCab.EditValue & "' ", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustL")
            Try
                DsMaster.Tables("M_CustL").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_CustL")

            Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
            Me.SLUCust.Properties.DisplayMember = "Nama"
            Me.SLUCust.Properties.ValueMember = "CustID"
        End If
    End Sub
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("ByrPiutIDD")

            Dim i : For i = Me.GridView3.RowCount - 1 To 0 Step -1
                If Me.GridView3.GetRowCellValue(i, "ByrPiutDtl") = Me.GridView1.GetFocusedDataRow.Item("ByrPiutIDD") Then
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(i, "ByrPiutIDD")

                    Me.GridView3.DeleteRow(i)
                End If
            Next

        End If

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        koneksi.Close()

        If e.Column Is GridView1.Columns("SisaPakai") Then
            Dim SisaPakai As Decimal

            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CaraBayar") <> "DbCr Note" Then
                Dim command As New SqlCommand("Select Isnull((Select Sum(SisaPakai)-(Select Isnull(Sum(SisaPakai),0) From T_ByrPiutDtl Where DocID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' and ByrPiutID<>'" & Me.TBKode.EditValue & "') From (Select TotBayar As SisaPakai From T_Cash Where CashID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' UNION ALL Select TotBayar As SisaPakai From T_BG Where BGID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' UNION ALL Select TotBayar As SisaPakai From T_JMPiut Where JMID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' UNION ALL Select TotAkhirRp As SisaPakai From T_RtrBJ Where RtrID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' UNION ALL Select TotAkhir As SisaPakai From T_RtrPenjBB Where RtrID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' UNION ALL Select TotAkhir As SisaPakai From T_RtrPenjBebas Where RtrID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "' UNION ALL Select SisaPakai From T_Voucher Where VcrID='" & Me.GridView1.GetFocusedRowCellValue("DocID") & "') as x),0)", koneksi)

                With koneksi
                    .Open()
                    SisaPakai = command.ExecuteScalar()
                    .Close()
                End With

                If e.Value > SisaPakai Then
                    XtraMessageBox.Show("Nominal Melebihi Sisa Pakai Dokumen", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SisaPakai", SisaPakai)
                    Exit Sub
                End If

                'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
                'RemoveHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged
            End If
        End If

    End Sub
    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        Try
            If Me.GridView1.RowCount > 0 Then
                'If Me.GridView3.RowCount > 0 Then
                Me.GridView3.ActiveFilterString = "[ByrPiutDtl] = '" & Me.GridView1.GetFocusedRowCellValue("ByrPiutIDD") & "'"
                'End If
            End If

            If Me.GridView1.RowCount - 1 > 0 Then
                If Not IsDBNull(Me.GridView1.GetFocusedDataRow.Item("CaraBayar")) Then
                    'If Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "DbCr Note" Then
                    '    Me.GridColumn5.OptionsColumn.AllowEdit = True
                    'Else
                    '    Me.GridColumn5.OptionsColumn.AllowEdit = False
                    'End If

                    If Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "BG" Then
                        Me.GridColumn7.OptionsColumn.AllowEdit = False
                        Me.GridColumn9.OptionsColumn.AllowEdit = False
                    Else
                        Me.GridColumn7.OptionsColumn.AllowEdit = True
                        Me.GridColumn9.OptionsColumn.AllowEdit = True
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "ByrPiutIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "DocID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "CaraBayar", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "SisaPakai", 0)

            If Me.GridView1.RowCount > 0 Then
                If Me.GridView3.RowCount > 0 Then
                    Me.GridView3.ActiveFilterString = "[ByrPiutDtl] = '" & Me.GridView1.GetFocusedRowCellValue("ByrPiutIDD") & "'"
                End If
            End If

            AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged
            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdJualID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdJualID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdJualID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdJualID.ButtonClick
        Try
            If Gol = "Lain-Lain" Then
                'MsgBox(Gol)
                Dim frm As New FSearch("Penjualan Lain2 Bayar", Me.SLUCust.EditValue, Gol, Me.TBKode.EditValue, Date.Now, Me.SLUMtUang.EditValue)
                frm.ShowDialog()

            ElseIf Gol = "Penjualan Bahan" Then
                'MsgBox(Gol)
                Dim frm As New FSearch("Penjualan Bahan Bayar", Me.SLUCust.EditValue, Gol, Me.TBKode.EditValue, Date.Now, Me.SLUMtUang.EditValue)
                frm.ShowDialog()

            Else
                'MsgBox(Me.SLUCust.EditValue)
                Dim frm As New FSearch("Penjualan Bayar", Me.SLUCust.EditValue, Gol, Me.TBKode.EditValue, Date.Now, Me.SLUMtUang.EditValue)
                frm.ShowDialog()
            End If

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Bayar", CDec(dataTrans.Item("SisaBayar").ToString))
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "SisaBayar", CDec(dataTrans.Item("SisaBayar").ToString))


                'If Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "DbCr Note" Or Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "Retur" Then
                '    Me.GridColumn5.OptionsColumn.AllowEdit = True
                'Else
                '    Me.GridColumn5.OptionsColumn.AllowEdit = False
                'End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdDocID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdDocID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdDocID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdDocID.ButtonClick
        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged
        'MsgBox("MASUK")
        Try
            If Me.CBOJenis.EditValue = "Non Retur" Then
                Dim frm As New FSearch("ByrPiut", Me.SLUCust.EditValue, Gol, Me.SLUMtUang.EditValue, Me.DTPTanggal.EditValue, "")
                frm.ShowDialog()

            ElseIf Me.CBOJenis.EditValue = "Retur" Then
                If Gol = "Penjualan Bahan" Then
                    Dim frm As New FSearch("ByrPiut Retur Jual Bahan", Me.SLUCust.EditValue, Gol, Me.SLUMtUang.EditValue, Me.DTPTanggal.EditValue, "")
                    frm.ShowDialog()

                ElseIf Gol = "Lain-Lain" Then
                    Dim frm As New FSearch("ByrPiut Retur Lain-Lain", Me.SLUCust.EditValue, Gol, Me.SLUMtUang.EditValue, Me.DTPTanggal.EditValue, "")
                    frm.ShowDialog()
                ElseIf Gol <> "Penjualan Bahan" Then
                    Dim frm As New FSearch("ByrPiut Retur", Me.SLUCust.EditValue, Gol, Me.SLUMtUang.EditValue, Me.DTPTanggal.EditValue, "")
                    frm.ShowDialog()
                End If

            ElseIf Me.CBOJenis.EditValue = "JM" Then
                    Dim frm As New FSearch("ByrPiut JM", Me.SLUCust.EditValue, Gol, Me.SLUMtUang.EditValue, Me.DTPTanggal.EditValue, "")
                    frm.ShowDialog()
                End If

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "CaraBayar", dataTrans.Item("Cara").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Deskripsi", dataTrans.Item("Deskripsi").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DocID", dataTrans.Item("Kode").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SisaPakai2", CDec(dataTrans.Item("SisaPakai").ToString))

                AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "SisaPakai", CDec(dataTrans.Item("SisaPakai").ToString))

                If Me.GridView1.RowCount > 0 Then
                    If Not IsDBNull(Me.GridView1.GetFocusedDataRow.Item("CaraBayar")) Then
                        'If Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "DbCr Note" Or Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "Retur" Then
                        '    Me.GridColumn5.OptionsColumn.AllowEdit = True
                        'Else
                        '    Me.GridColumn5.OptionsColumn.AllowEdit = False
                        'End If

                        If Me.GridView1.GetFocusedDataRow.Item("CaraBayar") = "BG" Then
                            Me.GridColumn7.OptionsColumn.AllowEdit = False
                            Me.GridColumn9.OptionsColumn.AllowEdit = False

                            Dim cmsl As SqlDataAdapter
                            cmsl = New SqlDataAdapter("Select " & Me.GridView3.RowCount + 1 * -1 & " as ByrPiutIDD,'" & Me.TBKode.EditValue & "'," & Me.GridView1.GetFocusedRowCellValue("ByrPiutIDD") & " As ByrPiutDtl,'" & Me.GridView1.GetFocusedRowCellValue("CaraBayar") & "' As CaraBayar,JualID,Bayar,(Select Sum(SisaBayar) From(Select SisaBayar From T_JualBJ where JualID=T_BGDtl.JualID Union All Select SisaBayar From T_JualBebas where JualID=T_BGDtl.JualID Union All Select SisaBayar From T_JualBB where JualID=T_BGDtl.JualID)as x) As SisaBayar From T_BGDtl Where BGID='" & dataTrans.Item("Kode").ToString & "'", koneksi)
                            cmsl.Fill(DsMaster, "T_ByrPiutDtl2" & Gol)

                            Me.GridControl3.DataSource = DsMaster
                            Me.GridControl3.DataMember = "T_ByrPiutDtl2" & Gol

                        Else
                            Me.GridColumn7.OptionsColumn.AllowEdit = True
                            Me.GridColumn9.OptionsColumn.AllowEdit = True
                        End If

                    End If
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("ByrPiutIDD")
        End If
    End Sub

    Private Sub GridView3_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView3.CellValueChanged

        koneksi.Close()

        If e.Column Is GridView3.Columns("Bayar") Then
            Dim SisaBayar As Decimal
            Dim Sisa As Decimal = 0

            RemoveHandler GridControl3.Leave, AddressOf GridControl3_Leave

            'Me.BSave.Focus()
            'Me.GridView3.Focus()

            Dim command As New SqlCommand
            If Gol = "Lain-Lain" Then
                command = New SqlCommand("Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Me.TBKode.EditValue & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Me.TBKode.EditValue & "'),0)) From T_JualBebas J Where JualID='" & Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "JualID") & "'", koneksi)

            ElseIf Gol = "Penjualan Bahan" Then
                command = New SqlCommand("Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Me.TBKode.EditValue & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Me.TBKode.EditValue & "'),0)) From T_JualBB J Where JualID='" & Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "JualID") & "'", koneksi)

            Else
                command = New SqlCommand("Select TotAkhir-(Select Isnull((Select Sum(Bayar) From T_BGDtl Where JualID=J.JualID and BGID Not In (Select Distinct DocID From T_ByrPiutDtl Where ByrPiutID<> '" & Me.TBKode.EditValue & "')),0))-(Select Isnull((Select Sum(Bayar) From T_ByrPiutDtl2 Where JualID=J.JualID and ByrPiutID<> '" & Me.TBKode.EditValue & "'),0)) From T_JualBJ J Where JualID='" & Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "JualID") & "'", koneksi)
            End If


            With koneksi
                .Open()
                SisaBayar = command.ExecuteScalar()
                .Close()
            End With

            Dim i : For i = 0 To DsMaster.Tables("T_ByrPiutDtl2" & Gol).Rows.Count - 1
                If DsMaster.Tables("T_ByrPiutDtl2" & Gol).Rows(i).Item("JualID") = Me.GridView3.GetRowCellValue(Me.GridView3.FocusedRowHandle, "JualID") Then
                    If DsMaster.Tables("T_ByrPiutDtl2" & Gol).Rows(i).Item("CaraBayar") <> "BG" Then
                        Sisa += DsMaster.Tables("T_ByrPiutDtl2" & Gol).Rows(i).Item("Bayar")
                    End If
                End If
            Next

            If Sisa > SisaBayar Then
                XtraMessageBox.Show("Nominal Melebihi Sisa Bayar Faktur", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Bayar", SisaBayar - (Sisa - Me.GridView3.GetFocusedDataRow.Item("Bayar")))
                Exit Sub
                'Me.BSave.Focus()
                'Me.GridView3.Focus()
            End If

            AddHandler GridControl3.Leave, AddressOf GridControl3_Leave

        End If

    End Sub
    Private Sub GridView3_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try

            Me.GridView3.SetRowCellValue(e.RowHandle, "ByrPiutIDD", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "ByrPiutID", Me.TBKode.EditValue)
            Me.GridView3.SetRowCellValue(e.RowHandle, "ByrPiutDtl", Me.GridView1.GetFocusedDataRow.Item("ByrPiutIDD"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "CaraBayar", Me.GridView1.GetFocusedDataRow.Item("CaraBayar"))
            Me.GridView3.SetRowCellValue(e.RowHandle, "JualID", "")

            RemoveHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

            Me.GridView3.SetRowCellValue(e.RowHandle, "Bayar", 0)

            AddHandler GridView3.CellValueChanged, AddressOf GridView3_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FByrPiut_d(Me.GridView2.GetFocusedDataRow.Item("ByrPiutID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.EditValue <> "" And Not IsDBNull(Me.SLUCust.EditValue) And Me.SLUCust.Properties.ReadOnly = False Then
            DelXml()

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ByrPiutIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Dim x : For x = Me.GridView3.RowCount - 1 To 0
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "ByrPiutIDD")

                Me.GridView3.DeleteRow(x)
            Next
        End If
    End Sub

    Private Sub GridControl3_Leave(sender As Object, e As EventArgs) Handles GridControl3.Leave
        Try
            If Me.GridView3.OptionsBehavior.Editable = True Then
                Me.GridView3.RefreshData()

                If CType(Me.GridView3.Columns("Bayar").SummaryText, Decimal) > Me.GridView1.GetFocusedDataRow.Item("SisaPakai") Then
                    XtraMessageBox.Show("Faktur yang Dibayar Dengan " & Me.GridView1.GetFocusedDataRow.Item("DocID") & " Melebihi Nominal Pembayaran", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'Me.GridView3.SetRowCellValue(Me.GridView3.RowCount - 1, "Bayar", 0)
                    Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Bayar", 0)
                    Exit Sub
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CBOJenis_Leave(sender As Object, e As EventArgs) Handles CBOJenis.Leave
        Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "ByrPiutIDD")

            Me.GridView1.DeleteRow(i)
        Next

        Dim x : For x = Me.GridView3.RowCount - 1 To 0
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "ByrPiutIDD")

            Me.GridView3.DeleteRow(x)
        Next
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class