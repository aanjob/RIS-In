Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraExport

Public Class FBahan
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim arrPar2(-1) As String
    Dim ColNmBB As New Collection
    Dim Kode As Guid
    Dim Pic(), PicLama() As Byte
    Dim ImageLama As Image
    Dim msLama As New MemoryStream()
    Dim Gol As String
    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Gol = Golongan
        Me.BVTBB_e.Caption = Gol
        If Gol = "Bahan Baku" Then
            Me.BVTBB_e.Glyph = RIS.My.Resources.Resources.Bahan

            'ElseIf Gol = "Mesin" Then
            '    Me.BVTBB_e.Glyph = RIS.My.Resources.Resources.Mesin

            'ElseIf Gol = "Tooling" Then
            '    Me.BVTBB_e.Glyph = RIS.My.Resources.Resources.Tooling

        ElseIf Gol = "Sparepart-Mesin" Then
            Me.BVTBB_e.Glyph = RIS.My.Resources.Resources.Sparepart

            'ElseIf Gol = "Umum" Then
            '    Me.BVTBB_e.Glyph = RIS.My.Resources.Resources.Umum

        End If

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BBN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BBEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BBDel"), Boolean)
        Me.BVTPriceHis.Enabled = CType(TcodeCollection.Item("BBPrHis"), Boolean)
        Me.BVTRekHargaBB.Enabled = CType(TcodeCollection.Item("BBRekHBB"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTBB_s.Enabled = True

        Me.CBODivPO.Properties.ReadOnly = True
        Me.TBHSCode.Properties.ReadOnly = True
        Me.SLUJns.Properties.ReadOnly = True
        Me.LUMerk.Properties.ReadOnly = True
        Me.LUSubJns.Properties.ReadOnly = True
        Me.LUTebal.Properties.ReadOnly = True
        Me.LUGramasi.Properties.ReadOnly = True
        Me.LUWarna.Properties.ReadOnly = True
        Me.LUKode.Properties.ReadOnly = True
        Me.LUHard.Properties.ReadOnly = True
        Me.LUUk.Properties.ReadOnly = True
        Me.LUJasa.Properties.ReadOnly = True
        Me.TBTahun.Properties.ReadOnly = True
        Me.TBSat.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.CEGerak.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.PGambar.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView5.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCopy.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTBB_s.Enabled = False
        Me.BVTPriceHis.Enabled = False
        Me.BVTRekHargaBB.Enabled = False

        If MainModule.Posisi Like "*Pembelian*" Then
            Me.GridView1.OptionsBehavior.Editable = True
            Me.CBODivPO.Properties.ReadOnly = False
        Else
            Me.CBODivPO.Properties.ReadOnly = False
            Me.TBHSCode.Properties.ReadOnly = False
            Me.SLUJns.Properties.ReadOnly = False
            Me.LUMerk.Properties.ReadOnly = False
            Me.LUSubJns.Properties.ReadOnly = False
            Me.LUTebal.Properties.ReadOnly = False
            Me.LUGramasi.Properties.ReadOnly = False
            Me.LUWarna.Properties.ReadOnly = False
            Me.LUKode.Properties.ReadOnly = False
            Me.LUHard.Properties.ReadOnly = False
            Me.LUUk.Properties.ReadOnly = False
            Me.LUJasa.Properties.ReadOnly = False
            Me.TBTahun.Properties.ReadOnly = False
            Me.TBSat.Properties.ReadOnly = False
            Me.MKet.Properties.ReadOnly = False
            Me.CEGerak.Properties.ReadOnly = False
            Me.CEAktif.Properties.ReadOnly = False
            Me.PGambar.Properties.ReadOnly = False

            Me.GridView1.OptionsBehavior.Editable = True
            Me.GridView5.OptionsBehavior.Editable = True
        End If

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True
        Me.BCopy.Enabled = True

        Me.BVTBB_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        Try
            DsMaster.Tables("M_CustLUE").Clear()
            DsMaster.Tables("M_BBJnsLUE").Clear()
            DsMaster.Tables("Merk").Clear()
            DsMaster.Tables("SubJns").Clear()
            DsMaster.Tables("Tbl").Clear()
            DsMaster.Tables("Gram").Clear()
            DsMaster.Tables("Wrn").Clear()
            DsMaster.Tables("Kode").Clear()
            DsMaster.Tables("Hard").Clear()
            DsMaster.Tables("Uk").Clear()
            DsMaster.Tables("Jasa").Clear()
        Catch ex As Exception

        End Try

        cmsl = New SqlDataAdapter("Select JnsID,Nama From M_BBJns Where Gol='" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BBJnsLUE")
        cmsl.Fill(DsMaster, "M_BBJnsLUE")

        Me.SLUJns.Properties.DataSource = DsMaster.Tables("M_BBJnsLUE")
        Me.SLUJns.Properties.DisplayMember = "Nama"
        Me.SLUJns.Properties.ValueMember = "JnsID"

        cmsl = New SqlDataAdapter("Select Distinct Merk From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Merk")
        cmsl.Fill(DsMaster, "Merk")

        Me.LUMerk.Properties.DataSource = DsMaster.Tables("Merk")
        Me.LUMerk.Properties.DisplayMember = "Merk"
        Me.LUMerk.Properties.ValueMember = "Merk"

        cmsl = New SqlDataAdapter("Select Distinct SubJns From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "SubJns")
        cmsl.Fill(DsMaster, "SubJns")

        Me.LUSubJns.Properties.DataSource = DsMaster.Tables("SubJns")
        Me.LUSubJns.Properties.DisplayMember = "SubJns"
        Me.LUSubJns.Properties.ValueMember = "SubJns"

        cmsl = New SqlDataAdapter("Select Distinct Tbl From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Tbl")
        cmsl.Fill(DsMaster, "Tbl")

        Me.LUTebal.Properties.DataSource = DsMaster.Tables("Tbl")
        Me.LUTebal.Properties.DisplayMember = "Tbl"
        Me.LUTebal.Properties.ValueMember = "Tbl"

        cmsl = New SqlDataAdapter("Select Distinct Gram From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Gram")
        cmsl.Fill(DsMaster, "Gram")

        Me.LUGramasi.Properties.DataSource = DsMaster.Tables("Gram")
        Me.LUGramasi.Properties.DisplayMember = "Gram"
        Me.LUGramasi.Properties.ValueMember = "Gram"

        cmsl = New SqlDataAdapter("Select Distinct Wrn From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Wrn")
        cmsl.Fill(DsMaster, "Wrn")

        Me.LUWarna.Properties.DataSource = DsMaster.Tables("Wrn")
        Me.LUWarna.Properties.DisplayMember = "Wrn"
        Me.LUWarna.Properties.ValueMember = "Wrn"

        cmsl = New SqlDataAdapter("Select Distinct Kode From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Kode")
        cmsl.Fill(DsMaster, "Kode")

        Me.LUKode.Properties.DataSource = DsMaster.Tables("Kode")
        Me.LUKode.Properties.DisplayMember = "Kode"
        Me.LUKode.Properties.ValueMember = "Kode"

        cmsl = New SqlDataAdapter("Select Distinct Hard From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Hard")
        cmsl.Fill(DsMaster, "Hard")

        Me.LUHard.Properties.DataSource = DsMaster.Tables("Hard")
        Me.LUHard.Properties.DisplayMember = "Hard"
        Me.LUHard.Properties.ValueMember = "Hard"

        cmsl = New SqlDataAdapter("Select Distinct Uk From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Uk")
        cmsl.Fill(DsMaster, "Uk")

        Me.LUUk.Properties.DataSource = DsMaster.Tables("Uk")
        Me.LUUk.Properties.DisplayMember = "Uk"
        Me.LUUk.Properties.ValueMember = "Uk"

        cmsl = New SqlDataAdapter("Select Distinct Jasa From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "Jasa")
        cmsl.Fill(DsMaster, "Jasa")

        Me.LUJasa.Properties.DataSource = DsMaster.Tables("Jasa")
        Me.LUJasa.Properties.DisplayMember = "Jasa"
        Me.LUJasa.Properties.ValueMember = "Jasa"
    End Sub

    Public Sub FillDt()
        Try
            DsMaster.Tables("M_BB" & Gol).Clear()

        Catch ex As Exception

        End Try
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BBID,HSCode,B.Nama,DivPO,B.JnsID,J.Nama as Jenis,Merk,SubJns,Tbl,Gram,Wrn,Kode,Hard,Uk,Jasa,ThnProd,Sat,Ket, stsJasa,stsGerak,B.Aktif,B.InsDate,B.InsBy,B.UpdDate,B.UpdBy From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where J.Gol='" & Gol & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BB" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BB" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_BB" & Gol
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID,H.SuppID,S.Nama,BBID,H.Aktif From M_SuppBB H Inner Join M_Supp S On H.SuppID=S.SuppID  Where BBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_SuppBB")
        Try
            DsMaster.Tables("M_SuppBB").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SuppBB")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_SuppBB"


        cmsl = New SqlDataAdapter("Select HubID,M.BBID,BBIDM,B.Nama From M_BBMentah M Inner Join M_BB B On M.BBIDM=B.BBID Where M.BBID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BBMentah")
        Try
            DsMaster.Tables("M_BBMentah").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BBMentah")

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "M_BBMentah"

        'cmsl = New SqlDataAdapter("Select HubID,H.DivID,D.Nama,BBID,H.Aktif From M_DivBB H Inner Join M_Div D On H.DivID=D.DivID  Where BBID='" & Kode & "'", koneksi)

        'cmsl.TableMappings.Add("Table", "M_DivBB")
        'cmsl.Fill(DsMaster, "M_DivBB")
        'DsMaster.Tables("M_DivBB").Clear()
        'cmsl.Fill(DsMaster, "M_DivBB")

        'Me.GridControl3.DataSource = DsMaster
        'Me.GridControl3.DataMember = "M_DivBB"
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
            Dim link As BaseExportLink = GridView4.CreateExportLink(provider)
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

    Private Sub FBahan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master " & Gol
    End Sub

    Private Sub FBahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBB_e.Selected = True
    End Sub

    Private Sub BVTBB_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBB_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master " & Gol
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master " & Gol

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBHSCode.EditValue = ""
        Me.MNama.EditValue = ""
        Me.CBODivPO.EditValue = ""
        Me.LUMerk.EditValue = ""
        Me.LUSubJns.EditValue = ""
        Me.LUSubJns.EditValue = ""
        Me.LUTebal.EditValue = ""
        Me.LUGramasi.EditValue = ""
        Me.LUWarna.EditValue = ""
        Me.LUKode.EditValue = ""
        Me.LUHard.EditValue = ""
        Me.LUUk.EditValue = ""
        Me.LUJasa.EditValue = ""
        Me.TBSat.EditValue = ""
        Me.MKet.EditValue = ""
        Me.CEJasa.EditValue = False
        Me.CEGerak.EditValue = True
        Me.CEAktif.EditValue = True
        Me.PGambar.Image = Nothing
        Me.TBInfo.EditValue = ""

        If Me.CEJasa.EditValue = True Then
            Me.GridView5.OptionsBehavior.Editable = True
        Else
            Me.GridView5.OptionsBehavior.Editable = False
        End If

        If Me.ColNmBB.Contains("Jns") Then
            Me.ColNmBB.Remove("Jns")
        End If

        If Me.ColNmBB.Contains("Merk") Then
            Me.ColNmBB.Remove("Merk")
        End If

        If Me.ColNmBB.Contains("SubJns") Then
            Me.ColNmBB.Remove("SubJns")
        End If

        If Me.ColNmBB.Contains("Tbl") Then
            Me.ColNmBB.Remove("Tbl")
        End If

        If Me.ColNmBB.Contains("Gram") Then
            Me.ColNmBB.Remove("Gram")
        End If

        If Me.ColNmBB.Contains("Wrn") Then
            Me.ColNmBB.Remove("Wrn")
        End If

        If Me.ColNmBB.Contains("SubKode") Then
            Me.ColNmBB.Remove("SubKode")
        End If

        If Me.ColNmBB.Contains("Hard") Then
            Me.ColNmBB.Remove("Hard")
        End If

        If Me.ColNmBB.Contains("Uk") Then
            Me.ColNmBB.Remove("Uk")
        End If

        If Me.ColNmBB.Contains("Jasa") Then
            Me.ColNmBB.Remove("Jasa")
        End If

        Me.ColNmBB.Add("", "Jns")
        Me.ColNmBB.Add("", "Merk")
        Me.ColNmBB.Add("", "SubJns")
        Me.ColNmBB.Add("", "Tbl")
        Me.ColNmBB.Add("", "Gram")
        Me.ColNmBB.Add("", "Wrn")
        Me.ColNmBB.Add("", "SubKode")
        Me.ColNmBB.Add("", "Hard")
        Me.ColNmBB.Add("", "Uk")
        Me.ColNmBB.Add("", "Jasa")

        FillDtl("--")
        ReDim arrPar(-1)

        If Gol = "Bahan" Then
            Me.LUMerk.Properties.ReadOnly = True
            Me.TBTahun.Properties.ReadOnly = True
        Else
            Me.LUMerk.Properties.ReadOnly = False
            Me.TBTahun.Properties.ReadOnly = False
        End If
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master " & Gol

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("BBID")
        Me.TBHSCode.EditValue = Me.GridView2.GetFocusedDataRow.Item("HSCode")
        Me.MNama.EditValue = Me.GridView2.GetFocusedDataRow.Item("Nama")
        Me.CBODivPO.EditValue = Me.GridView2.GetFocusedDataRow.Item("DivPO")
        Me.SLUJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("JnsID")
        Me.LUMerk.EditValue = Me.GridView2.GetFocusedDataRow.Item("Merk")
        Me.LUSubJns.EditValue = Me.GridView2.GetFocusedDataRow.Item("SubJns")
        Me.LUTebal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tbl")
        Me.LUGramasi.EditValue = Me.GridView2.GetFocusedDataRow.Item("Gram")
        Me.LUWarna.EditValue = Me.GridView2.GetFocusedDataRow.Item("Wrn")
        Me.LUKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kode")
        Me.LUHard.EditValue = Me.GridView2.GetFocusedDataRow.Item("Hard")
        Me.LUUk.EditValue = Me.GridView2.GetFocusedDataRow.Item("Uk")
        Me.LUJasa.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jasa")
        Me.TBSat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Sat")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")
        Me.CEJasa.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsJasa")
        Me.CEGerak.EditValue = Me.GridView2.GetFocusedDataRow.Item("stsGerak")
        Me.CEAktif.EditValue = Me.GridView2.GetFocusedDataRow.Item("Aktif")

        If Me.CEJasa.EditValue = True Then
            Me.GridView5.OptionsBehavior.Editable = True
        Else
            Me.GridView5.OptionsBehavior.Editable = False
        End If

        If Me.ColNmBB.Contains("Jns") Then
            Me.ColNmBB.Remove("Jns")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Jenis") <> "" Then
            Me.ColNmBB.Add(Me.GridView2.GetFocusedDataRow.Item("Jenis"), "Jns")
        Else
            Me.ColNmBB.Add("", "Jns")
        End If

        If Me.ColNmBB.Contains("Merk") Then
            Me.ColNmBB.Remove("Merk")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Merk") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Merk"), "Merk")
        Else
            Me.ColNmBB.Add("", "Merk")
        End If

        If Me.ColNmBB.Contains("SubJns") Then
            Me.ColNmBB.Remove("SubJns")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("SubJns") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("SubJns"), "SubJns")
        Else
            Me.ColNmBB.Add("", "SubJns")
        End If


        If Me.ColNmBB.Contains("Tbl") Then
            Me.ColNmBB.Remove("Tbl")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Tbl") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Tbl"), "Tbl")
        Else
            Me.ColNmBB.Add("", "Tbl")
        End If


        If Me.ColNmBB.Contains("Gram") Then
            Me.ColNmBB.Remove("Gram")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Gram") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Gram"), "Gram")
        Else
            Me.ColNmBB.Add("", "Gram")
        End If


        If Me.ColNmBB.Contains("Wrn") Then
            Me.ColNmBB.Remove("Wrn")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Wrn") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Wrn"), "Wrn")
        Else
            Me.ColNmBB.Add("", "Wrn")
        End If


        If Me.ColNmBB.Contains("SubKode") Then
            Me.ColNmBB.Remove("SubKode")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Kode") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Kode"), "SubKode")
        Else
            Me.ColNmBB.Add("", "SubKode")
        End If


        If Me.ColNmBB.Contains("Hard") Then
            Me.ColNmBB.Remove("Hard")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Hard") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Hard"), "Hard")
        Else
            Me.ColNmBB.Add("", "Hard")
        End If


        If Me.ColNmBB.Contains("Uk") Then
            Me.ColNmBB.Remove("Uk")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Uk") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Uk"), "Uk")
        Else
            Me.ColNmBB.Add("", "Uk")
        End If


        If Me.ColNmBB.Contains("Jasa") Then
            Me.ColNmBB.Remove("Jasa")
        End If

        If Me.GridView2.GetFocusedDataRow.Item("Jasa") <> "" Then
            Me.ColNmBB.Add(" " & Me.GridView2.GetFocusedDataRow.Item("Jasa"), "Jasa")
        Else
            Me.ColNmBB.Add("", "Jasa")
        End If

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)

        If Gol = "Bahan" Then
            Me.LUMerk.Properties.ReadOnly = True
            Me.TBTahun.Properties.ReadOnly = True
        Else
            Me.LUMerk.Properties.ReadOnly = False
            Me.TBTahun.Properties.ReadOnly = False
        End If

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmd As New SqlCommand("Select PicID,Picture From M_Image Where ID='" & Me.TBKode.EditValue & "'", koneksi)
        Dim newImage As Image
        Dim Reader As SqlClient.SqlDataReader
        With cmd
            .Connection = koneksi

            With koneksi
                .Open()
                Reader = cmd.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        Kode = Reader.Item(0)
                        Pic = Reader.Item(1)
                        PicLama = Reader.Item(1)
                    End While
                Else
                    Pic = Nothing
                    PicLama = Nothing
                End If
                Reader.Close()
                .Close()
            End With
        End With

        If Pic Is Nothing Then
            Me.PGambar.Image = Nothing
        Else
            Using ms As New MemoryStream(Pic, 0, Pic.Length)
                ms.Write(Pic, 0, Pic.Length)
                newImage = Image.FromStream(ms, True)
                msLama = ms
                ImageLama = newImage
            End Using
            Me.PGambar.Image = newImage
        End If

        OpenControl()

        Me.BCopy.Enabled = False
        Me.SLUJns.Properties.ReadOnly = True

        If Me.CEJasa.EditValue = True Then
            Me.GridView5.OptionsBehavior.Editable = True
        Else
            Me.GridView5.OptionsBehavior.Editable = False
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master " & Gol

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Bahan : " & Me.GridView2.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            DelImage(Me.GridView2.GetFocusedDataRow.Item("BBID"))

            Dim cmSP As New SqlCommand("SPDelM_BB")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BBID")
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

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If Me.CBODivPO.EditValue = "" Or IsDBNull(Me.CBODivPO.EditValue) Then
            XtraMessageBox.Show("Divisi PO Harus Diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.SLUJns.EditValue = "" Or IsDBNull(Me.SLUJns.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.TBSat.EditValue = "" Or IsDBNull(Me.TBSat.EditValue) Then
            XtraMessageBox.Show("Satuan Harus Diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.PGambar.Image IsNot Nothing Then
            Dim ms As New MemoryStream()

            If Object.ReferenceEquals(ImageLama, Me.PGambar.EditValue) Then
                ms = msLama
            Else
                Me.PGambar.Image.Save(ms, Me.PGambar.Image.RawFormat)
                Pic = ms.GetBuffer
                ms.Close()
            End If
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_BB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.MNama.EditValue
                    .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                    .Parameters.Add("@DivPO", SqlDbType.VarChar).Value = Me.CBODivPO.EditValue
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                    .Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.LUMerk.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.LUSubJns.EditValue 'Me.LUSubJns.EditValue
                    .Parameters.Add("@Tbl", SqlDbType.VarChar).Value = Me.LUTebal.EditValue
                    .Parameters.Add("@Gram", SqlDbType.VarChar).Value = Me.LUGramasi.EditValue
                    .Parameters.Add("@Wrn", SqlDbType.VarChar).Value = Me.LUWarna.EditValue
                    .Parameters.Add("@SubKode", SqlDbType.VarChar).Value = Me.LUKode.EditValue
                    .Parameters.Add("@Hard", SqlDbType.VarChar).Value = Me.LUHard.EditValue
                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.LUUk.EditValue
                    .Parameters.Add("@Jasa", SqlDbType.VarChar).Value = Me.LUJasa.EditValue
                    .Parameters.Add("@ThnProd", SqlDbType.VarChar).Value = Me.TBTahun.EditValue
                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.TBSat.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.CEJasa.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi
                End With

                Try
                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

                    If x = 1 Then
                        XtraMessageBox.Show("Deskripsi Bahan Sudah Ada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    Dim i : For i = 0 To GridView1.RowCount - 1
                        Dim cmSPDtl As New SqlCommand("SPInsM_SuppBB")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@SuppId", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SuppID")
                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
                    Next

                    Dim z : For z = 0 To GridView5.RowCount - 1
                        Dim cmSPDtl As New SqlCommand("SPInsM_BBMentah")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@BBIDM", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(z, "BBIDM")
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
                    Next

                    'Dim z : For z = 0 To GridView3.RowCount - 1
                    '    Dim cmSPDtl As New SqlCommand("SPInsM_DivBB")
                    '    cmSPDtl.CommandType = CommandType.StoredProcedure

                    '    With cmSPDtl
                    '        .Parameters.Add("@DivId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "DivID")
                    '        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    '        .Parameters.Add("@Return", SqlDbType.Int)
                    '        .Parameters("@Return").Direction = ParameterDirection.Output
                    '        .Connection = koneksi
                    '    End With

                    '    With koneksi
                    '        .Open()
                    '        cmSPDtl.ExecuteNonQuery()
                    '        x = cmSPDtl.Parameters("@Return").Value
                    '        .Close()
                    '    End With
                    'Next

                    If Me.PGambar.Image IsNot Nothing Then
                        InsImage(Me.TBKode.EditValue, "Bahan Baku", Pic)
                    End If

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    Else
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

            Case 200
                Dim cmSP As New SqlCommand("SPUpM_BB")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.MNama.EditValue
                    .Parameters.Add("@DivPO", SqlDbType.VarChar).Value = Me.CBODivPO.EditValue
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                    .Parameters.Add("@Merk", SqlDbType.VarChar).Value = Me.LUMerk.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.LUSubJns.EditValue 'Me.LUSubJns.EditValue
                    .Parameters.Add("@Tbl", SqlDbType.VarChar).Value = Me.LUTebal.EditValue
                    .Parameters.Add("@Gram", SqlDbType.VarChar).Value = Me.LUGramasi.EditValue
                    .Parameters.Add("@Wrn", SqlDbType.VarChar).Value = Me.LUWarna.EditValue
                    .Parameters.Add("@SubKode", SqlDbType.VarChar).Value = Me.LUKode.EditValue
                    .Parameters.Add("@Hard", SqlDbType.VarChar).Value = Me.LUHard.EditValue
                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.LUUk.EditValue
                    .Parameters.Add("@Jasa", SqlDbType.VarChar).Value = Me.LUJasa.EditValue
                    .Parameters.Add("@ThnProd", SqlDbType.VarChar).Value = Me.TBTahun.EditValue
                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.TBSat.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.CEJasa.EditValue
                    .Parameters.Add("@Gerak", SqlDbType.Bit).Value = Me.CEGerak.EditValue
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

                        If x = 1 Then
                            XtraMessageBox.Show("Deskripsi Bahan Sudah Ada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_SuppBB")
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
                            Dim cmSPDel As New SqlCommand("SPDelM_BBMentah")
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
                            If Me.GridView1.GetRowCellValue(i, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_SuppBB")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@SuppId", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SuppID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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
                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_SuppBB")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "HubID")
                                    .Parameters.Add("@SuppId", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SuppID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Aktif")
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

                        Dim z : For z = 0 To GridView5.RowCount - 1
                            If Me.GridView5.GetRowCellValue(z, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_BBMentah")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@BBIDM", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(z, "BBIDM")
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
                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_BBMentah")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView5.GetRowCellValue(z, "HubID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@BBIDM", SqlDbType.VarChar).Value = Me.GridView5.GetRowCellValue(z, "BBIDM")
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
                            End If
                        Next


                        'Dim z : For z = 0 To GridView3.RowCount - 1
                        '    If Me.GridView3.GetRowCellValue(z, "HubID") < 0 Then
                        '        Dim cmSPDtl As New SqlCommand("SPInsM_DivBB")
                        '        cmSPDtl.CommandType = CommandType.StoredProcedure

                        '        With cmSPDtl
                        '            .Parameters.Add("@DivId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "DivID")
                        '            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '            .Parameters.Add("@Return", SqlDbType.Int)
                        '            .Parameters("@Return").Direction = ParameterDirection.Output
                        '            .Connection = koneksi
                        '        End With

                        '        With koneksi
                        '            .Open()
                        '            cmSPDtl.ExecuteNonQuery()
                        '            x = cmSPDtl.Parameters("@Return").Value
                        '            .Close()
                        '        End With
                        '    Else
                        '        Dim cmSPDtl As New SqlCommand("SPUpM_DivBB")
                        '        cmSPDtl.CommandType = CommandType.StoredProcedure

                        '        With cmSPDtl
                        '            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "HubID")
                        '            .Parameters.Add("@DivId", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "DivID")
                        '            .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        '            .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Aktif")
                        '            .Parameters.Add("@Return", SqlDbType.Int)
                        '            .Parameters("@Return").Direction = ParameterDirection.Output
                        '            .Connection = koneksi
                        '        End With

                        '        With koneksi
                        '            .Open()
                        '            cmSPDtl.ExecuteNonQuery()
                        '            x = cmSPDtl.Parameters("@Return").Value
                        '            .Close()
                        '        End With
                        '    End If
                        'Next

                        If Me.PGambar.Image IsNot Nothing Then
                            If PicLama Is Nothing Then
                                InsImage(Me.TBKode.EditValue, "Bahan Baku", Pic)
                            Else
                                UpImage(Kode, Me.TBKode.EditValue, "Bahan Baku", Pic)
                            End If
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

        Me.Dispose()

        Dim frm As New FBahan(Gol)
        frm.MdiParent = FUtama
        frm.Show()
        'LockControl()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()

        Me.Dispose()

        Dim frm As New FBahan(Gol)
        frm.MdiParent = FUtama
        frm.Show()
    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click

        Dim frm As New FSearch("M_BB", "", Gol, "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.TBKode.EditValue = ""
                Me.MNama.EditValue = dataTrans.Item("Nama").ToString
                Me.TBHSCode.EditValue = dataTrans.Item("HSCode").ToString
                Me.CBODivPO.EditValue = dataTrans.Item("DivPO").ToString
                Me.SLUJns.EditValue = dataTrans.Item("JnsID").ToString
                Me.LUMerk.EditValue = dataTrans.Item("Merk").ToString
                Me.LUSubJns.EditValue = dataTrans.Item("SubJns").ToString
                Me.LUTebal.EditValue = dataTrans.Item("Tbl").ToString
                Me.LUGramasi.EditValue = dataTrans.Item("Gram").ToString
                Me.LUWarna.EditValue = dataTrans.Item("Wrn").ToString
                Me.LUKode.EditValue = dataTrans.Item("SubKode").ToString
                Me.LUHard.EditValue = dataTrans.Item("Hard").ToString
                Me.LUUk.EditValue = dataTrans.Item("Uk").ToString
                Me.LUJasa.EditValue = dataTrans.Item("Jasa").ToString
                Me.TBTahun.EditValue = dataTrans.Item("ThnProd").ToString
                Me.TBSat.EditValue = dataTrans.Item("Sat").ToString
                Me.CEJasa.EditValue = CBool(dataTrans.Item("stsJasa").ToString)

                'Me.MKet.EditValue = dataTrans.Item("Ket").ToString
                Me.CEGerak.EditValue = True
                Me.CEAktif.EditValue = True

                FillDtl(dataTrans.Item("Kode").ToString)
            End If

            If Me.ColNmBB.Contains("Jns") Then
                Me.ColNmBB.Remove("Jns")
            End If

            If Me.SLUJns.Text <> "" Then
                Me.ColNmBB.Add(Me.SLUJns.Text, "Jns")
            Else
                Me.ColNmBB.Add("", "Jns")
            End If

            If Me.ColNmBB.Contains("Merk") Then
                Me.ColNmBB.Remove("Merk")
            End If

            If dataTrans.Item("Merk").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Merk").ToString, "Merk")
            Else
                Me.ColNmBB.Add("", "Merk")
            End If

            If Me.ColNmBB.Contains("SubJns") Then
                Me.ColNmBB.Remove("SubJns")
            End If

            If dataTrans.Item("SubJns").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("SubJns").ToString, "SubJns")
            Else
                Me.ColNmBB.Add("", "SubJns")
            End If

            If Me.ColNmBB.Contains("Tbl") Then
                Me.ColNmBB.Remove("Tbl")
            End If

            If dataTrans.Item("Tbl").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Tbl").ToString, "Tbl")
            Else
                Me.ColNmBB.Add("", "Tbl")
            End If


            If Me.ColNmBB.Contains("Gram") Then
                Me.ColNmBB.Remove("Gram")
            End If

            If dataTrans.Item("Gram").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Gram").ToString, "Gram")
            Else
                Me.ColNmBB.Add("", "Gram")
            End If

            If Me.ColNmBB.Contains("Wrn") Then
                Me.ColNmBB.Remove("Wrn")
            End If

            If dataTrans.Item("Wrn").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Wrn").ToString, "Wrn")
            Else
                Me.ColNmBB.Add("", "Wrn")
            End If

            If Me.ColNmBB.Contains("SubKode") Then
                Me.ColNmBB.Remove("SubKode")
            End If

            If dataTrans.Item("SubKode").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("SubKode").ToString, "SubKode")
            Else
                Me.ColNmBB.Add("", "SubKode")
            End If


            If Me.ColNmBB.Contains("Hard") Then
                Me.ColNmBB.Remove("Hard")
            End If

            If dataTrans.Item("Hard").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Hard").ToString, "Hard")
            Else
                Me.ColNmBB.Add("", "Hard")
            End If

            If Me.ColNmBB.Contains("Uk") Then
                Me.ColNmBB.Remove("Uk")
            End If

            If dataTrans.Item("Uk").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Uk").ToString, "Uk")
            Else
                Me.ColNmBB.Add("", "Uk")
            End If


            If Me.ColNmBB.Contains("Jasa") Then
                Me.ColNmBB.Remove("Jasa")
            End If

            If dataTrans.Item("Jasa").ToString <> "" Then
                Me.ColNmBB.Add(" " & dataTrans.Item("Jasa").ToString, "Jasa")
            Else
                Me.ColNmBB.Add("", "Jasa")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUJns_Leave(sender As Object, e As EventArgs) Handles SLUJns.Leave
        If Me.ColNmBB.Contains("Jns") Then
            Me.ColNmBB.Remove("Jns")
        End If

        If Me.SLUJns.EditValue <> "" Then
            Me.ColNmBB.Add(Me.SLUJns.Text, "Jns")
        Else
            Me.ColNmBB.Add("", "Jns")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUMerk_Leave(sender As Object, e As EventArgs) Handles LUMerk.Leave
        If Me.ColNmBB.Contains("Merk") Then
            Me.ColNmBB.Remove("Merk")
        End If

        If Me.LUMerk.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUMerk.EditValue, "Merk")
        Else
            Me.ColNmBB.Add("", "Merk")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUSubJns_Leave(sender As Object, e As EventArgs) Handles LUSubJns.Leave
        If Me.ColNmBB.Contains("SubJns") Then
            Me.ColNmBB.Remove("SubJns")
        End If

        If Me.LUSubJns.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUSubJns.EditValue, "SubJns")
        Else
            Me.ColNmBB.Add("", "SubJns")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUTebal_Leave(sender As Object, e As EventArgs) Handles LUTebal.Leave
        If Me.ColNmBB.Contains("Tbl") Then
            Me.ColNmBB.Remove("Tbl")
        End If

        If Me.LUTebal.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUTebal.EditValue, "Tbl")
        Else
            Me.ColNmBB.Add("", "Tbl")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUGramasi_Leave(sender As Object, e As EventArgs) Handles LUGramasi.Leave
        If Me.ColNmBB.Contains("Gram") Then
            Me.ColNmBB.Remove("Gram")
        End If

        If Me.LUGramasi.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUGramasi.EditValue, "Gram")
        Else
            Me.ColNmBB.Add("", "Gram")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUWarna_Leave(sender As Object, e As EventArgs) Handles LUWarna.Leave
        If Me.ColNmBB.Contains("Wrn") Then
            Me.ColNmBB.Remove("Wrn")
        End If

        If Me.LUWarna.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUWarna.EditValue, "Wrn")
        Else
            Me.ColNmBB.Add("", "Wrn")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUKode_Leave(sender As Object, e As EventArgs) Handles LUKode.Leave
        If Me.ColNmBB.Contains("SubKode") Then
            Me.ColNmBB.Remove("SubKode")
        End If

        If Me.LUKode.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUKode.EditValue, "SubKode")
        Else
            Me.ColNmBB.Add("", "SubKode")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUHard_Leave(sender As Object, e As EventArgs) Handles LUHard.Leave
        If Me.ColNmBB.Contains("Hard") Then
            Me.ColNmBB.Remove("Hard")
        End If

        If Me.LUHard.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUHard.EditValue, "Hard")
        Else
            Me.ColNmBB.Add("", "Hard")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUUk_Leave(sender As Object, e As EventArgs) Handles LUUk.Leave
        If Me.ColNmBB.Contains("Uk") Then
            Me.ColNmBB.Remove("Uk")
        End If

        If Me.LUUk.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUUk.EditValue, "Uk")
        Else
            Me.ColNmBB.Add("", "Uk")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
    End Sub

    Private Sub LUJasa_Leave(sender As Object, e As EventArgs) Handles LUJasa.Leave
        If Me.ColNmBB.Contains("Jasa") Then
            Me.ColNmBB.Remove("Jasa")
        End If

        If Me.LUJasa.EditValue <> "" Then
            Me.ColNmBB.Add(" " & Me.LUJasa.EditValue, "Jasa")
        Else
            Me.ColNmBB.Add("", "Jasa")
        End If

        Me.MNama.EditValue = Me.ColNmBB.Item("Jns") & "" & Me.ColNmBB.Item("Merk") & "" & Me.ColNmBB.Item("SubJns") & "" & Me.ColNmBB.Item("Tbl") & "" & Me.ColNmBB.Item("Gram") & "" & Me.ColNmBB.Item("Wrn") & "" & Me.ColNmBB.Item("SubKode") & "" & Me.ColNmBB.Item("Hard") & "" & Me.ColNmBB.Item("Uk") & "" & Me.ColNmBB.Item("Jasa")
        'Dim Nama As String
        'Nama = Trim(Me.SLUJns.Text & " " & Me.LUSubJns.EditValue & " " & Me.LUTebal.EditValue & " " & Me.LUGramasi.EditValue & " " & Me.LUWarna.EditValue & " " & Me.LUKode.EditValue & " " & Me.LUHard.EditValue & " " & Me.LUUk.EditValue & " " & Me.LUJasa.EditValue)
        'Me.MNama.EditValue = Trim(Nama.Replace("  ", ""))
    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.F9 Then

            Me.GridColumn52.Visible = True
            Me.GridColumn52.VisibleIndex = 0

        ElseIf e.KeyCode = Keys.F11 Then
            Dim x, i As Integer
            Dim BBID As String = ""
            For i = 0 To DsMaster.Tables("M_BB" & Gol).Rows.Count - 1
                If DsMaster.Tables("M_BB" & Gol).Rows(i).Item("Cek") = True Then
                    x += 1
                    If x = 1 Then
                        BBID = "'" & DsMaster.Tables("M_BB" & Gol).Rows(i).Item("BBID") & "'"
                    Else
                        BBID &= ",'" & DsMaster.Tables("M_BB" & Gol).Rows(i).Item("BBID") & "'"
                    End If
                End If
            Next

            Dim XR As New XRLapBB
            XR.InitializeData(False, Gol, BBID)
        End If
    End Sub
    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetRowCellValue(e.RowHandle, "HubID", Me.GridView1.RowCount * -1)
        Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", Me.TBKode.EditValue)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Aktif", "True")
    End Sub

    Private Sub BEdSuppID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdSuppID.ButtonClick
        Dim frm As New FSearch("Supplier", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Nama", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView5_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView5.InitNewRow
        Me.GridView5.SetRowCellValue(e.RowHandle, "HubID", Me.GridView5.RowCount * -1)
        Me.GridView5.SetRowCellValue(e.RowHandle, "BBID", Me.TBKode.EditValue)
    End Sub

    Private Sub GridControl5_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl5.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView5.GetFocusedDataRow.Item("HubID")
        End If

    End Sub

    Private Sub BEdBBIDM_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBBIDM.ButtonClick
        Dim frm As New FSearch("M_BB", "", "Bahan", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView5.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView5.SetRowCellValue(Me.GridView5.FocusedRowHandle, "Nama", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("HubId")
        End If
    End Sub

    Private Sub BEdSuppID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdSuppID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BVTPriceHis_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPriceHis.ItemPressed
        Try
            Me.TBPHBBID.EditValue = Me.GridView2.GetFocusedDataRow.Item("BBID")
            Me.TBPHNama.EditValue = Me.GridView2.GetFocusedDataRow.Item("Nama")
            Me.TBSat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Sat")

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select H.SuppID,S.Nama,Tanggal,DocID As POID,MtUang,TipePPn,PersenPPn,HargaBeli From M_BBHarga H Inner Join M_Supp S On H.SuppID=S.SuppID  Where BBID='" & Me.TBPHBBID.EditValue & "' ORDER BY Tanggal DESC", koneksi)

            cmsl.TableMappings.Add("Table", "PriceHis")
            DsMaster = New System.Data.DataSet
            cmsl.Fill(DsMaster, "PriceHis")

            Me.GridControl3.DataSource = DsMaster
            Me.GridControl3.DataMember = "PriceHis"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BVTRekHargaBB_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTRekHargaBB.ItemPressed
        Try
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Distinct D.BBID,B.Nama,D.Sat,H.Tanggal,H.POID,H.SuppID,S.Nama AS Supp,H.Tipe,H.TipePPn,H.PersenPPn, H.MtUang,D.HarSat From T_POBB AS H INNER JOIN T_POBBDtl AS D ON H.POID = D.POID Inner Join M_Supp S On H.SuppID=S.SuppID Inner Join M_BB B On D.BBID=B.BBID Where (H.Tanggal = (Select MAX(H1.Tanggal) AS Expr1 From T_POBB AS H1 INNER JOIN T_POBBDtl AS D1 ON H1.POID = D1.POID Where (D1.BBID = D.BBID) AND (H1.SuppID = H.SuppID))) ORDER BY B.Nama, H.Tanggal DESC", koneksi)

            cmsl.TableMappings.Add("Table", "RekHargaBB")
            DsMaster = New System.Data.DataSet
            cmsl.Fill(DsMaster, "RekHargaBB")

            Me.GridControl4.DataSource = DsMaster
            Me.GridControl4.DataMember = "RekHargaBB"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Rekap Harga Bahan")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub LUMerk_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUMerk.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Merk?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUMerk.Text <> "" And Not IsDBNull(Me.LUMerk.Text) Then
                DsMaster.Tables("Merk").Rows.Add(Me.LUMerk.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUSubJns_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUSubJns.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Sub Jenis?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUSubJns.Text <> "" And Not IsDBNull(Me.LUSubJns.Text) Then
                DsMaster.Tables("SubJns").Rows.Add(Me.LUSubJns.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUTebal_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUTebal.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Tebal?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUTebal.Text <> "" And Not IsDBNull(Me.LUTebal.Text) Then
                DsMaster.Tables("Tbl").Rows.Add(Me.LUTebal.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUGramasi_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUGramasi.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Gramasi?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUGramasi.Text <> "" And Not IsDBNull(Me.LUGramasi.Text) Then
                DsMaster.Tables("Gram").Rows.Add(Me.LUGramasi.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUWarna_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUWarna.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Warna?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUWarna.Text <> "" And Not IsDBNull(Me.LUWarna.Text) Then
                DsMaster.Tables("Wrn").Rows.Add(Me.LUWarna.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUKode_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUKode.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Kode?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUKode.Text <> "" And Not IsDBNull(Me.LUKode.Text) Then
                DsMaster.Tables("Kode").Rows.Add(Me.LUKode.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUHard_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUHard.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Hardness?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUHard.Text <> "" And Not IsDBNull(Me.LUHard.Text) Then
                DsMaster.Tables("Hard").Rows.Add(Me.LUHard.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUUk_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUUk.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Ukuran?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUUk.Text <> "" And Not IsDBNull(Me.LUUk.Text) Then
                DsMaster.Tables("Uk").Rows.Add(Me.LUUk.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub LUJasa_ProcessNewValue(sender As Object, e As Controls.ProcessNewValueEventArgs) Handles LUJasa.ProcessNewValue
        If CStr(e.DisplayValue) <> String.Empty AndAlso MessageBox.Show(Me, "Apakah Mau Menambahkan '" & e.DisplayValue.ToString() & "' Ke Dalam List Jasa?", "Konfirmasi", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            If Me.LUJasa.Text <> "" And Not IsDBNull(Me.LUJasa.Text) Then
                DsMaster.Tables("Jasa").Rows.Add(Me.LUJasa.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub CEJasa_EditValueChanged(sender As Object, e As EventArgs) Handles CEJasa.EditValueChanged

        If Me.CEJasa.EditValue = True Then
            Me.GridView5.OptionsBehavior.Editable = True
        Else
            Me.GridView5.OptionsBehavior.Editable = False

            Dim i : For i = Me.GridView5.RowCount - 1 To 0 Step -1
                ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView5.GetRowCellValue(i, "HubID")

                Me.GridView5.DeleteRow(i)
            Next
        End If

    End Sub

    Private Sub TBHSCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBHSCode.KeyPress, TBTahun.KeyPress, TBSat.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub LUMerk_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LUMerk.KeyPress, LUSubJns.KeyPress, LUTebal.KeyPress, LUGramasi.KeyPress, LUWarna.KeyPress, LUKode.KeyPress, LUHard.KeyPress, LUUk.KeyPress, LUJasa.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class