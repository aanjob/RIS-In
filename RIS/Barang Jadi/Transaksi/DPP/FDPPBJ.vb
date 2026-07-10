Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FDPPBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, CabLama, CustLama, JnsLama, MtUangLama, UrutSal, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim AwalLama, AkhirLama As Date
    Dim CekAll As Boolean

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

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        LBGolp.Text = "    " & Golongan

        CabLama = ""
        CustLama = ""
        JnsLama = ""
        MtUangLama = ""
        AwalLama = Date.Now
        AkhirLama = Date.Now
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=17 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVTAProses.Enabled = CType(TcodeCollection.Item("DPPBJAP"), Boolean)
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("DPPBJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("DPPBJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("DPPBJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTTagihBJ_s.Enabled = True

        Me.TBID.Properties.ReadOnly = True
        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.CBOJnsFt.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.SLUCab.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUArtCode.Properties.ReadOnly = True
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
        Me.BVBPrint.Enabled = False
        Me.BVBPrintAll.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTTagihBJ_s.Enabled = False
        Me.BVTAProses.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False
        Me.CBOJnsFt.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.SLUCab.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUArtCode.Properties.ReadOnly = False
        Me.BProses.Enabled = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTTagihBJ_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UC.CabID,C.Cabang From M_UsCab UC Inner Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsMaster.Tables("M_UsCabLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabLUE" & Gol)

        Me.SLUCab.Properties.DataSource = DsMaster.Tables("M_UsCabLUE" & Gol)
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE" & Gol)
        cmsl.Fill(DsMaster, "M_CurrLUE" & Gol)
        DsMaster.Tables("M_CurrLUE" & Gol).Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE" & Gol)

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE" & Gol)
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"

        'Me.SLUMtUang.EditValue = "IDR"
    End Sub

    Public Sub FillDtl(Kode As String)
        Try
            DsMaster.Tables("T_DPPBJDtl" & Gol).Clear()
        Catch ex As Exception

        End Try

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DPPBJIDD,DPPBJID,Grup,JualID,Kode,SJID,Tanggal,DueDate,TotAkhir,TotDos,TotPsg,Ket From T_DPPBJDtl Where DPPBJID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_DPPBJDtl" & Gol)
        cmsl.Fill(DsMaster, "T_DPPBJDtl" & Gol)

        DsMaster.Tables("T_DPPBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_DPPBJDtl" & Gol).Columns("JualID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_DPPBJDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DPPBJID,PeriodID,CodeID,Kode,Tanggal,TglAwal,TglAkhir,H.Jenis,H.CabID,Cb.Cabang,UrutCab,H.CustID, C.Nama As Customer,C.Alamat,K.Nama as Kota,J.Nama as 'Status',MtUang,TotTagih,TotDos,TotPsg,Terbilang,H.InsDate,H.InsBy,H.UpdDate, H.UpdBy From T_DPPBJ H Inner Join M_Cab Cb On H.CabID=Cb.CabID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_JnsCust J On C.JnsCustID=J.JnsCustID and PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Order By DPPBJID desc,Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_DPPBJ" & Gol)
        Try
            DsMaster.Tables("T_DPPBJ" & Gol).Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "T_DPPBJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_DPPBJ" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_DPPBJ")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
            .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
            '.Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
            .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
            .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
            .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
            .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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

    Private Sub FDPPBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Tagihan Barang Jadi"
    End Sub

    Private Sub FDPPBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FDPPBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FDPPBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTTagihBJ_e.Selected = True
    End Sub

    Private Sub BVTTagihBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTTagihBJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Tagihan Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("DPPBJP"), Boolean)
        Me.BVBPrintAll.Enabled = CType(TcodeCollection.Item("DPPBJP"), Boolean)
        Me.BExExcel.Enabled = CType(TcodeCollection.Item("DPPBJExEc"), Boolean)
    End Sub

    Private Sub BVTAProses_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTAProses.ItemPressed
        Me.DTPTanggalA.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggalA.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPAwalA.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAwalA.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPAkhirA.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhirA.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
                If MainModule.BackDate = False Then
                    XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.BVTTagihBJ_e.Selected = True
                    Exit Sub
                End If
            End If

            Me.DTPTanggalA.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAwalA.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAkhirA.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggalA.EditValue = Date.Now
            Me.DTPAwalA.EditValue = Date.Now
            Me.DTPAkhirA.EditValue = Date.Now
        End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE" & Gol)
        cmsl.Fill(DsMaster, "M_CurrLUE" & Gol)
        DsMaster.Tables("M_CurrLUE" & Gol).Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE" & Gol)

        ReDim arrPar(-1)

        Me.SLUMtUangA.Properties.DataSource = DsMaster.Tables("M_CurrLUE" & Gol)
        Me.SLUMtUangA.Properties.DisplayMember = "MtUang"
        Me.SLUMtUangA.Properties.ValueMember = "MtUang"

    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Tagihan Barang Jadi"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPAwal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAwal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPAkhir.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            'If MainModule.SlstsPeriod() = True Then
            '    If MainModule.BackDate = False Then
            '        XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Exit Sub
            '    End If
            'End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPAwal.EditValue = Date.Now
            Me.DTPAkhir.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBID.Properties.ReadOnly = False
            Me.TBID.EditValue = ""
        Else
            Me.TBID.Properties.ReadOnly = True
            Me.TBID.EditValue = "--"
        End If

        Me.TBKode.EditValue = "--"

        Me.SLUMtUang.EditValue = "IDR"
        Me.CBOJnsFt.EditValue = "Non Spreading"
        Me.SLUCab.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUArtCode.EditValue = ""
        Me.MTerbilang.EditValue = ""
        Me.TBInfo.EditValue = ""

        AwalLama = Date.Now
        AkhirLama = Date.Now
        JnsLama = ""
        MtUangLama = ""
        CabLama = ""
        CustLama = ""

        FillDtl(Me.TBID.EditValue)
        DsMaster.Tables("T_DPPBJDtl" & Gol).Clear()
        ReDim arrPar(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Tagihan Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJ(Gol) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Opname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        LUE()

        Indicator = "200"

        Me.TBID.EditValue = Me.GridView2.GetFocusedDataRow.Item("DPPBJID")
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kode")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.DTPAwal.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglAwal")
        AwalLama = Me.GridView2.GetFocusedDataRow.Item("TglAwal")
        Me.DTPAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglAkhir")
        AkhirLama = Me.GridView2.GetFocusedDataRow.Item("TglAkhir")
        Me.CBOJnsFt.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        JnsLama = Me.GridView2.GetFocusedDataRow.Item("Jenis")
        Me.SLUMtUang.EditValue = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        MtUangLama = Me.GridView2.GetFocusedDataRow.Item("MtUang")
        Me.SLUCab.EditValue = Me.GridView2.GetFocusedDataRow.Item("CabID")
        CabLama = Me.GridView2.GetFocusedDataRow.Item("CabID")
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        CustLama = Me.GridView2.GetFocusedDataRow.Item("CustID")

        If Not IsDBNull(Me.SLUCab.EditValue) Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select C.CustID,Nama,JnsCustID,DiscCust,JT,Harga From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustDPP" & Gol)
            cmsl.Fill(DsMaster, "M_CustDPP" & Gol)

            Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustDPP" & Gol)
            Me.SLUCust.Properties.DisplayMember = "Nama"
            Me.SLUCust.Properties.ValueMember = "CustID"
        End If

        UrutSal = Me.GridView2.GetFocusedDataRow.Item("UrutCab")
        Me.MTerbilang.EditValue = Me.GridView2.GetFocusedDataRow.Item("Terbilang")

        FillDtl(Me.TBID.EditValue)
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

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Tagihan Barang Jadi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("DPPBJID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_DPPBJ")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("DPPBJID")
                .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CabID")
                .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("CustID")
                '.Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("Jenis")
                .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("TglAwal")
                .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.GridView2.GetFocusedDataRow.Item("TglAkhir")
                .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MtUang")
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
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("DPPBJID"), "DPPBJID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kode"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Status"), "Status")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Alamat"), "Alamat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kota"), "Kota")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Jenis"), "Jenis")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotTagih"), "TotTagih")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Terbilang"), "Terbilang")

        Dim XR As New XRDPPBJ
        XR.InitializeData(bind)
    End Sub

    Private Sub BVTPrintAll_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintAll.ItemClick
        Dim frm As New FPilihTgl

        frm = New FPilihTgl
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add(Me.LBGols.Text.Substring(4, Me.LBGols.Text.Length - 4), "Gol")
        bind.Add(MainModule.PilihTgl, "Tanggal")

        Dim XR As New XRDPPBJAll
        XR.InitializeData(bind)
    End Sub


    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "DPP " & Gol & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim cmSPDel As New SqlCommand("SPDelT_DPPBJDtl")
        cmSPDel.CommandType = CommandType.StoredProcedure
        Dim z As Integer

        With cmSPDel
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
            .Parameters.Add("@CabID", SqlDbType.VarChar).Value = CabLama
            .Parameters.Add("@CustID", SqlDbType.VarChar).Value = CustLama
            '.Parameters.Add("@Jns", SqlDbType.VarChar).Value = JnsLama
            .Parameters.Add("@Awal", SqlDbType.Date).Value = AwalLama
            .Parameters.Add("@Akhir", SqlDbType.Date).Value = AkhirLama
            .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = MtUangLama
            .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            With koneksi
                .Open()
                cmSPDel.ExecuteNonQuery()
                z = cmSPDel.Parameters("@Return").Value
                .Close()
            End With
        End With

        If Me.SLUMtUang.EditValue = "IDR" Then
            Me.MTerbilang.EditValue = StrConv(MainModule.EjaAngka(Me.GridView1.Columns("TotAkhir").SummaryText), VbStrConv.ProperCase) & " Rupiah"
        Else
            Me.MTerbilang.EditValue = StrConv(MainModule.EjaAngka(Me.GridView1.Columns("TotAkhir").SummaryText), VbStrConv.ProperCase) & " " & Me.SLUMtUang.EditValue
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_DPPBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    '.Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = DsMaster.Tables("M_CustDPP" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@TotTagih", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("TotAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("TotDos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("TotPsg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Terbilang", SqlDbType.VarChar).Value = Me.MTerbilang.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@DPPBJID", SqlDbType.VarChar, 30)
                    .Parameters("@DPPBJID").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 25)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBID.EditValue = cmSP.Parameters("@DPPBJID").Value
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "JualID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_DPPBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@DPPBJID", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Grup")
                                    .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Kode")
                                    .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJID")
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Tanggal")
                                    .Parameters.Add("@JT", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "DueDate")
                                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotAkhir")
                                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TotDos")
                                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TotPsg")
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

                                If x <> 0 Then
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBID.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                Dim cmSP As New SqlCommand("SPUpT_DPPBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBID.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    '.Parameters.Add("@Jns", SqlDbType.VarChar).Value = Me.CBOJnsFt.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.SLUCab.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = DsMaster.Tables("M_CustDPP" & Gol).Select("CustID = '" & Me.SLUCust.EditValue & "'")(0).Item("JT")
                    .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                    .Parameters.Add("@TotTagih", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("TotAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("TotDos").SummaryText, Decimal), 2)
                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Me.GridView1.Columns("TotPsg").SummaryText, Decimal), 2)
                    .Parameters.Add("@Terbilang", SqlDbType.VarChar).Value = Me.MTerbilang.EditValue
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "JualID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_DPPBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@DPPBJID", SqlDbType.VarChar).Value = Me.TBID.EditValue
                                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Grup")
                                    .Parameters.Add("@JualID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "JualID")
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Kode")
                                    .Parameters.Add("@SJID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SJID")
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Tanggal")
                                    .Parameters.Add("@JT", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "DueDate")
                                    .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "TotAkhir")
                                    .Parameters.Add("@TotDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TotDos")
                                    .Parameters.Add("@TotPsg", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "TotPsg")
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
                            End If

                            If x = 0 Then
                                Me.GridView1.SetRowCellValue(i, "DPPBJIDD", Me.GridView1.GetRowCellValue(i, "DPPBJIDD") * -1)
                            Else
                                XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
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
        If Me.SLUCab.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DPPBJIDD")

                Me.GridView1.DeleteRow(i)
            Next

            Me.SLUCust.EditValue = ""

            If Not IsDBNull(Me.SLUCab.EditValue) Then
                Try
                    Dim cmsl As SqlDataAdapter

                    cmsl = New SqlDataAdapter("Select C.CustID,C.Nama,C.Alamat,K.Nama As Kota,JT From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_CabCust CC On C.CustID=CC.CustID Where CC.CabID='" & Me.SLUCab.EditValue & "' and C.Aktif='True' and CC.Aktif='True'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_CustDPP" & Gol)
                    cmsl.Fill(DsMaster, "M_CustDPP" & Gol)
                    DsMaster.Tables("M_CustDPP" & Gol).Clear()
                    cmsl.Fill(DsMaster, "M_CustDPP" & Gol)

                    Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustDPP" & Gol)
                    Me.SLUCust.Properties.DisplayMember = "Nama"
                    Me.SLUCust.Properties.ValueMember = "CustID"
                Catch ex As Exception

                End Try

            End If
        End If


    End Sub

    Private Sub SLUCust_Leave(sender As Object, e As EventArgs) Handles SLUCust.Leave
        If Me.SLUCust.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select Distinct ArtName From T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join M_Brg B On D.ArtCode=B.ArtCode Where H.CabID='" & Me.SLUCab.EditValue & "' and CustID='" & Me.SLUCust.EditValue & "' and stsTagih='False'", koneksi)
            cmsl.TableMappings.Add("Table", "T_JualBJDPP" & Gol)
            cmsl.Fill(DsMaster, "T_JualBJDPP" & Gol)
            DsMaster.Tables("T_JualBJDPP" & Gol).Clear()
            cmsl.Fill(DsMaster, "T_JualBJDPP" & Gol)

            Me.SLUArtCode.Properties.DataSource = DsMaster.Tables("T_JualBJDPP" & Gol)
            Me.SLUArtCode.Properties.DisplayMember = "ArtName"
            Me.SLUArtCode.Properties.ValueMember = "ArtName"

            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DPPBJIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If

    End Sub

    Private Sub SLUArtCode_Leave(sender As Object, e As EventArgs) Handles SLUArtCode.Leave
        If Me.SLUArtCode.Properties.ReadOnly = False Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "DPPBJIDD")

                Me.GridView1.DeleteRow(i)
            Next
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim jml, jml2 As Integer

        Dim command2 As New SqlCommand("Select dbo.fcCekDPP(" & MainModule.periodAktif & ")", koneksi)

        With koneksi
            .Open()
            command2.CommandTimeout = 9000
            jml2 = command2.ExecuteScalar()
            .Close()
        End With

        If jml2 > 0 Then
            XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.SLUMtUang.EditValue = "" Then
            XtraMessageBox.Show("Mata Uang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim command As New SqlCommand("Select Count(*) From T_SJ Where CabID='" & Me.SLUCab.EditValue & "' and MtUang ='" & Me.SLUMtUang.EditValue & "' and Tanggal >='" & Me.DTPAwal.EditValue & "' and Tanggal <='" & Me.DTPAkhir.EditValue & "' and Gol = '" & Gol & "' and stsApp='False'", koneksi)

        With koneksi
            .Open()
            Jml = command.ExecuteScalar()
            .Close()
        End With

        If Jml > 0 Then
            XtraMessageBox.Show("Ada Surat Jalan Cabang " & Me.SLUCab.Text & " yang Belum Diapprove !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If SLUArtCode.EditValue = "" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '" & Me.TBID.EditValue & "' As DPPBJID, ROW_NUMBER() over (ORDER BY JualID)*-1 as DPPBJIDD,Grup,JualID, Kode,SJID,Tanggal,DueDate,TotAkhir,TotDos,TotPsg,Ket From T_JualBJ Where CabID ='" & Me.SLUCab.EditValue & "' and CustID = '" & Me.SLUCust.EditValue & "' and MtUang ='" & Me.SLUMtUang.EditValue & "' and Tanggal >='" & Me.DTPAwal.EditValue & "' and Tanggal <='" & Me.DTPAkhir.EditValue & "' and Gol = '" & Gol & "' and stsTagih='False'", koneksi)
            cmsl.Fill(DsMaster, "T_DPPBJDtl" & Gol)
           
        Else
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY JualID)*-1 as DPPBJIDD,* From (Select Distinct '" & Me.TBID.EditValue & "' As DPPBJID,H.Grup,H.JualID,Kode,SJID,Tanggal,DueDate,TotAkhir,TotDos,TotPsg,Ket From T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where CabID ='" & Me.SLUCab.EditValue & "' and CustID = '" & Me.SLUCust.EditValue & "' and MtUang ='" & Me.SLUMtUang.EditValue & "' and Tanggal >='" & Me.DTPAwal.EditValue & "' and Tanggal <='" & Me.DTPAkhir.EditValue & "' and H.Gol = '" & Gol & "' and ArtName = '" & Me.SLUArtCode.EditValue & "' and stsTagih='False') As x", koneksi)
            cmsl.Fill(DsMaster, "T_DPPBJDtl" & Gol)

        End If

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_DPPBJDtl" & Gol

        If Me.SLUMtUang.EditValue = "IDR" Then
            Me.MTerbilang.EditValue = StrConv(MainModule.EjaAngka(Me.GridView1.Columns("TotAkhir").SummaryText), VbStrConv.ProperCase) & " Rupiah"
        Else
            Me.MTerbilang.EditValue = StrConv(MainModule.EjaAngka(Me.GridView1.Columns("TotAkhir").SummaryText), VbStrConv.ProperCase) & " " & Me.SLUMtUang.EditValue
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FDPPBJ_d(Me.GridView2.GetFocusedDataRow.Item("DPPBJID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BProsesA_Click(sender As Object, e As EventArgs) Handles BProsesA.Click
        Dim jml, jml2 As Integer

        Dim command2 As New SqlCommand("Select dbo.fcCekDPP(" & MainModule.periodAktif & ")", koneksi)

        With koneksi
            .Open()
            command2.CommandTimeout = 9000
            jml2 = command2.ExecuteScalar()
            .Close()
        End With

        If jml2 > 0 Then
            XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.SLUMtUangA.EditValue = "" Then
            XtraMessageBox.Show("Mata Uang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim command As New SqlCommand("Select Count(*) From T_SJ Where CabID='" & Me.SLUCab.EditValue & "' and MtUang ='" & Me.SLUMtUang.EditValue & "' and Tanggal >='" & Me.DTPAwal.EditValue & "' and Tanggal <='" & Me.DTPAkhir.EditValue & "' and Gol = '" & Gol & "' and stsApp='False'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        If jml > 0 Then
            XtraMessageBox.Show("Ada Surat Jalan Cabang " & Me.SLUCab.Text & " yang Belum Diapprove !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct H.CabID,convert(bit,'FALSE') as Cek,Cabang From T_JualBJ H Inner Join M_Cab Cb On H.CabID=Cb.CabID Where MtUang ='" & Me.SLUMtUangA.EditValue & "' and Tanggal >='" & Me.DTPAwalA.EditValue & "' and Tanggal <='" & Me.DTPAkhirA.EditValue & "' and H.Gol = '" & Gol & "' and stsTagih='False'", koneksi)
        Try
            DsMaster.Tables("Sales" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "Sales" & Gol)

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "Sales" & Gol
    End Sub

    Private Sub BSaveA_Click(sender As Object, e As EventArgs) Handles BSaveA.Click
        koneksi.Close()
        Dim x As Integer

        Dim i : For i = 0 To Me.GridView5.RowCount - 1
            If Me.GridView5.GetRowCellValue(i, "Cek") = True Then
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select Distinct CustID From T_JualBJ Where MtUang ='" & Me.SLUMtUangA.EditValue & "' and Tanggal >='" & Me.DTPAwalA.EditValue & "' and Tanggal <='" & Me.DTPAkhirA.EditValue & "' and Gol = '" & Gol & "' and stsTagih='False' and CabID='" & Me.GridView5.GetRowCellValue(i, "CabID") & "'", koneksi)

                Try
                    DsMaster.Tables("Cust" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "Cust" & Gol)

                Dim y : For y = 0 To DsMaster.Tables("Cust" & Gol).Rows.Count - 1

                    Dim cmSP As New SqlCommand("SPProsessT_DPPBJ")
                    cmSP.CommandType = CommandType.StoredProcedure

                    With cmSP
                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = False
                        .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = "--"
                        .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                        .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggalA.EditValue
                        .Parameters.Add("@Awal", SqlDbType.Date).Value = Me.DTPAwalA.EditValue
                        .Parameters.Add("@Akhir", SqlDbType.Date).Value = Me.DTPAkhirA.EditValue
                        .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(i, "CabID")
                        .Parameters.Add("@CustID", SqlDbType.VarChar).Value = DsMaster.Tables("Cust" & Gol).Rows(y).Item("CustID")
                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUangA.EditValue

                        Dim Reader As SqlClient.SqlDataReader
                        Dim command As New SqlCommand("Select Sum(TotAkhir),Sum(TotDos),Sum(TotpSG) From T_JualBJ Where MtUang ='" & Me.SLUMtUangA.EditValue & "' and Tanggal >='" & Me.DTPAwalA.EditValue & "' and Tanggal <='" & Me.DTPAkhirA.EditValue & "' and Gol = '" & Gol & "' and stsTagih='False' and CabID='" & Me.GridView5.GetRowCellValue(i, "CabID") & "' and CustID='" & DsMaster.Tables("Cust" & Gol).Rows(y).Item("CustID") & "'", koneksi)

                        With koneksi
                            .Open()
                            Reader = command.ExecuteReader

                            If Reader.HasRows Then
                                While Reader.Read
                                    If IsDBNull(Reader.Item(0)) = True Then
                                        cmSP.Parameters.Add("@TotTagih", SqlDbType.Decimal).Value = 0
                                        cmSP.Parameters.Add("@TotDos", SqlDbType.Int).Value = 0
                                        cmSP.Parameters.Add("@TotPsg", SqlDbType.Int).Value = 0

                                        If Me.SLUMtUangA.EditValue = "IDR" Then
                                            cmSP.Parameters.Add("@Terbilang", SqlDbType.VarChar).Value = "Nol Rupiah"
                                        Else
                                            cmSP.Parameters.Add("@Terbilang", SqlDbType.VarChar).Value = "Nol " & Me.SLUMtUangA.EditValue
                                        End If

                                    Else
                                        cmSP.Parameters.Add("@TotTagih", SqlDbType.Decimal).Value = Math.Round(CType(Reader.Item(0), Decimal), 2)
                                        cmSP.Parameters.Add("@TotDos", SqlDbType.Int).Value = Math.Round(CType(Reader.Item(1), Decimal), 2)
                                        cmSP.Parameters.Add("@TotPsg", SqlDbType.Int).Value = Math.Round(CType(Reader.Item(2), Decimal), 2)

                                        If Me.SLUMtUangA.EditValue = "IDR" Then
                                            cmSP.Parameters.Add("@Terbilang", SqlDbType.VarChar).Value = StrConv(MainModule.EjaAngka(Reader.Item(0)), VbStrConv.ProperCase) & " Rupiah"
                                        Else
                                            cmSP.Parameters.Add("@Terbilang", SqlDbType.VarChar).Value = StrConv(MainModule.EjaAngka(Reader.Item(0)), VbStrConv.ProperCase) & " " & Me.SLUMtUangA.EditValue
                                        End If
                                    End If
                                End While
                            End If
                            Reader.Close()
                            .Close()
                        End With

                        .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                        .Parameters.Add("@Return", SqlDbType.Int)
                        .Parameters("@Return").Direction = ParameterDirection.Output
                        .Parameters.Add("@DPPBJID", SqlDbType.VarChar, 30)
                        .Parameters("@DPPBJID").Direction = ParameterDirection.Output
                        .Parameters.Add("@Kode", SqlDbType.VarChar, 25)
                        .Parameters("@Kode").Direction = ParameterDirection.Output
                        .Connection = koneksi

                        Try
                            With koneksi
                                .Open()
                                cmSP.ExecuteNonQuery()
                                x = cmSP.Parameters("@Return").Value
                                .Close()
                            End With

                        Catch ex As Exception
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                    End With
                Next
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Auto Process Tagihan Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DsMaster.Tables("Sales" & Gol).Clear()
        ElseIf x = 1 Then
            XtraMessageBox.Show("Data Sudah Diproses", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Auto Process Tagihan Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        CekSave = False
    End Sub

    Private Sub GridView5_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView5.CellValueChanged

        If e.Column Is GridView5.Columns("Cek") Then
            RemoveHandler GridView5.CellValueChanged, AddressOf GridView5_CellValueChanged

            Dim Jml As Integer

            Dim command As New SqlCommand("Select Count(*) From T_SJ Where CabID='" & Me.GridView5.GetRowCellValue(e.RowHandle, "CabID") & "' and MtUang ='" & Me.SLUMtUangA.EditValue & "' and Tanggal >='" & Me.DTPAwalA.EditValue & "' and Tanggal <='" & Me.DTPAkhirA.EditValue & "' and Gol = '" & Gol & "' and stsApp='False'", koneksi)

            With koneksi
                .Open()
                Jml = command.ExecuteScalar()
                .Close()
            End With

            If Jml > 0 Then
                Me.GridView5.SetRowCellValue(e.RowHandle, "Cek", False)
                XtraMessageBox.Show("Ada Surat Jalan Cabang " & Me.GridView5.GetRowCellValue(e.RowHandle, "Cabang") & " yang Belum Diapprove !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                AddHandler GridView5.CellValueChanged, AddressOf GridView5_CellValueChanged

                Exit Sub
            End If
        End If
    End Sub

    Private Sub GridView5_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView5.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView5.RowCount - 1
                Me.GridView5.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView5.RowCount - 1
                Me.GridView5.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

End Class