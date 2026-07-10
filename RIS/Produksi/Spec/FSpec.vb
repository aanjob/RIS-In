Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FSpec
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim Kode As Guid
    Dim Pic(), PicLama() As Byte
    Dim ImageLama As Image
    Dim msLama As New MemoryStream()

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=2", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SpecN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SpecEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SpecDel"), Boolean)
        Me.BVBCariAll.Enabled = CType(TcodeCollection.Item("SpecSA"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTSpec_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUStyleID.Properties.ReadOnly = True
        Me.SLUArtName.Properties.ReadOnly = True
        Me.SLUWarna.Properties.ReadOnly = True
        Me.TBBrand.Properties.ReadOnly = True
        Me.TBSL.Properties.ReadOnly = True
        Me.TBRange.Properties.ReadOnly = True
        Me.TBSample.Properties.ReadOnly = True
        Me.TBDibuat.Properties.ReadOnly = True
        Me.TBPattern.Properties.ReadOnly = True
        Me.TBChaser.Properties.ReadOnly = True
        Me.TBPPC.Properties.ReadOnly = True
        Me.TBKulit.Properties.ReadOnly = True
        Me.TBNonKulit.Properties.ReadOnly = True
        Me.TBTeknik.Properties.ReadOnly = True
        Me.TBMengetahui.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.PGambar.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BCopy.Enabled = False
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
        Me.BVBCariAll.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTSpec_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUStyleID.Properties.ReadOnly = False
        Me.SLUArtName.Properties.ReadOnly = False
        Me.TBBrand.Properties.ReadOnly = False
        Me.TBSL.Properties.ReadOnly = False
        Me.SLUWarna.Properties.ReadOnly = False
        Me.TBRange.Properties.ReadOnly = False
        Me.TBSample.Properties.ReadOnly = False
        Me.TBDibuat.Properties.ReadOnly = False
        Me.TBPattern.Properties.ReadOnly = False
        Me.TBChaser.Properties.ReadOnly = False
        Me.TBPPC.Properties.ReadOnly = False
        Me.TBKulit.Properties.ReadOnly = False
        Me.TBNonKulit.Properties.ReadOnly = False
        Me.TBTeknik.Properties.ReadOnly = False
        Me.TBMengetahui.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.PGambar.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BCopy.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSpec_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select CustID,Nama From M_Cust Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustSp")
        cmsl.Fill(DsMaster, "M_CustSp")
        DsMaster.Tables("M_CustSp").Clear()
        cmsl.Fill(DsMaster, "M_CustSp")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustSp")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)
        cmsl.TableMappings.Add("Table", "M_StyleLUE")
        cmsl.Fill(DsMaster, "M_StyleLUE")
        DsMaster.Tables("M_StyleLUE").Clear()
        cmsl.Fill(DsMaster, "M_StyleLUE")

        Me.SLUStyleID.Properties.DataSource = DsMaster.Tables("M_StyleLUE")
        Me.SLUStyleID.Properties.DisplayMember = "StyleID"
        Me.SLUStyleID.Properties.ValueMember = "StyleID"
    End Sub

    Public Sub FillDtl(Kode As String)

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SpecIDD,(Select Count(*) From M_ModelDtl Where SpecIDD=SD.SpecIDD) As Model, SpecID, SD.DivID,D.Nama as Divisi, SD.KompID, K.Nama As Komponen, SD.BBID, B.Nama As BB, B.Uk,B.Wrn,SD.Ket,SD.Sat,SD.stsJasa,stsMentah,BBIDInd From M_SpecDtl SD Inner Join M_Div D On SD.DivID=D.DivID Inner Join M_Komp K On SD.KompID=K.KompID Inner Join M_BB B On SD.BBID=B.BBID Where SpecID='" & Kode & "' Order By D.Urut,K.Urut,D.Nama Asc", koneksi)

        cmsl.TableMappings.Add("Table", "M_SpecDtl")
        Try
            DsMaster.Tables("M_SpecDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SpecDtl")

        DsMaster.Tables("M_SpecDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("M_SpecDtl").Columns("DivID"), DsMaster.Tables("M_SpecDtl").Columns("KompID"), DsMaster.Tables("M_SpecDtl").Columns("BBID"), DsMaster.Tables("M_SpecDtl").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_SpecDtl"

        If DsMaster.Tables("M_SpecDtl").Rows.Count - 1 >= 0 Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Model")) Then
                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Model") > 0 Then
                    Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False

                    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                Else
                    Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                    Else
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                    End If

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    Else
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True

                    End If
                End If
            End If
        End If
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SpecID,PeriodID,CodeID,Revisi,Tanggal,H.CustID,C.Nama As Customer,StyleID,Brand,ArtName,Warna,ShoeLast, RangeSize,SampleSize,H.Ket,Dibuat,Pattern,Chaser,PPC,Teknik,PembKulit,PembNonKulit,Mengetahui,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From M_Spec H Inner Join M_Cust C On H.CustID=C.CustID Where PeriodID=" & MainModule.periodAktif & "", koneksi)

        cmsl.TableMappings.Add("Table", "M_Spec")
        Try
            DsMaster.Tables("M_Spec").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Spec")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_Spec"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelM_Spec")
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


    Private Sub FSpec_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Materials Specification"
    End Sub
    Private Sub FSpec_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub
    Private Sub FSpec_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FSpec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTSpec_e.Selected = True
    End Sub

    Private Sub BVTSpec_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSpec_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Materials Specification"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SpecP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Materials Specification"

        DsMaster.Clear()

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            'If MainModule.SlOpBJ() > 0 Then
            '    If MainModule.BackDate = False Then
            XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If

            'Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            'End If
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

        Me.TBRevisi.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.SLUStyleID.EditValue = ""
        Me.SLUArtName.EditValue = ""
        Me.TBBrand.EditValue = ""
        Me.TBSL.EditValue = ""
        Me.SLUWarna.EditValue = ""
        Me.TBRange.EditValue = ""
        Me.TBSample.EditValue = ""
        Me.TBPattern.EditValue = ""
        Me.TBDibuat.EditValue = ""
        Me.TBPattern.EditValue = ""
        Me.TBChaser.EditValue = ""
        Me.TBPPC.EditValue = ""
        Me.TBKulit.EditValue = ""
        Me.TBNonKulit.EditValue = ""
        Me.TBTeknik.EditValue = ""
        Me.TBMengetahui.EditValue = ""
        Me.MKet.EditValue = ""
        Me.PGambar.Image = Nothing
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("M_SpecDtl").Clear()
        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Materials Specification"

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("SpecID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
        Me.TBRevisi.EditValue = CInt(Me.GridView2.GetFocusedDataRow.Item("Revisi")) + 1
        Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
        Me.SLUStyleID.EditValue = Me.GridView2.GetFocusedDataRow.Item("StyleID")

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct ArtName,Case when B.MerkID='' Then Gol Else M.Nama End As Brand From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where B.Aktif='True' and StyleID='" & Me.SLUStyleID.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSp")
        cmsl.Fill(DsMaster, "M_BrgSp")
        DsMaster.Tables("M_BrgSp").Clear()
        cmsl.Fill(DsMaster, "M_BrgSp")

        Me.SLUArtName.Properties.DataSource = DsMaster.Tables("M_BrgSp")
        Me.SLUArtName.Properties.DisplayMember = "ArtName"
        Me.SLUArtName.Properties.ValueMember = "ArtName"

        Me.SLUArtName.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtName")
        Me.TBBrand.EditValue = Me.GridView2.GetFocusedDataRow.Item("Brand")

        If Not IsDBNull(Me.SLUArtName.EditValue) Then
            cmsl = New SqlDataAdapter("Select Distinct W.Nama As Warna From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and ArtName='" & Me.SLUArtName.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "Warna")
            cmsl.Fill(DsMaster, "Warna")
            DsMaster.Tables("Warna").Clear()
            cmsl.Fill(DsMaster, "Warna")

            Me.SLUWarna.Properties.DataSource = DsMaster.Tables("Warna")
            Me.SLUWarna.Properties.DisplayMember = "Warna"
            Me.SLUWarna.Properties.ValueMember = "Warna"
        End If

        Me.SLUWarna.EditValue = Me.GridView2.GetFocusedDataRow.Item("Warna")
        Me.TBBrand.EditValue = Me.GridView2.GetFocusedDataRow.Item("Brand")
        Me.TBSL.EditValue = Me.GridView2.GetFocusedDataRow.Item("ShoeLast")
        Me.TBRange.EditValue = Me.GridView2.GetFocusedDataRow.Item("RangeSize")
        Me.TBSample.EditValue = Me.GridView2.GetFocusedDataRow.Item("SampleSize")
        'Me.TBDibuat.EditValue = Me.GridView2.GetFocusedDataRow.Item("Dibuat")
        'Me.TBPattern.EditValue = Me.GridView2.GetFocusedDataRow.Item("Pattern")
        Me.TBChaser.EditValue = Me.GridView2.GetFocusedDataRow.Item("Chaser")
        'Me.TBPPC.EditValue = Me.GridView2.GetFocusedDataRow.Item("PPC")
        Me.TBKulit.EditValue = Me.GridView2.GetFocusedDataRow.Item("PembKulit")
        'Me.TBNonKulit.EditValue = Me.GridView2.GetFocusedDataRow.Item("PembNonKulit")
        Me.TBTeknik.EditValue = Me.GridView2.GetFocusedDataRow.Item("Teknik")
        Me.TBMengetahui.EditValue = Me.GridView2.GetFocusedDataRow.Item("Mengetahui")

        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
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
        CekSave = True
        Me.BCopy.Enabled = False
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Materials Specification"

        koneksi.Close()

        If MainModule.SlSpec(Me.GridView2.GetFocusedDataRow.Item("SpecID")) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ditarik Di Model", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("SpecID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            DelImage(Me.GridView2.GetFocusedDataRow.Item("SpecID"))

            Dim cmSP As New SqlCommand("SPDelM_Spec")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("SpecID")
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.UserAktif
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
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SpecID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Brand"), "Brand")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Customer"), "Cust")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Warna"), "Warna")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SampleSize"), "SampleSize")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("RangeSize"), "RangeSize")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Dibuat"), "Dibuat")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Pattern"), "Pattern")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Chaser"), "Chaser")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PPC"), "PPC")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PembKulit"), "PembKulit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("PembNonKulit"), "PembNonKulit")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Teknik"), "Teknik")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Mengetahui"), "Mengetahui")

        Dim XR As New XRSpec
        XR.InitializeData(bind)

        'Dim XR2 As New XRKolomSpec
        'XR2.InitializeData(bind)
    End Sub

    Private Sub BVBCariAll_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCariAll.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Materials Specification"

        Dim frm As New FSpec_sa
        frm.ShowDialog()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()

        'Karena ada spec yang sudah ditarik model bahkan sudah repeat tapi begitu turun lagi ada spec yang berbeda jadi harus buat spec baru dengan artname dan warna yang sama
        If MainModule.SlDoubleSpec(Me.SLUArtName.EditValue, Me.SLUWarna.EditValue) > 0 Then
            If XtraMessageBox.Show("Spec '" & Me.SLUArtName.EditValue & "' dengan warna '" & Me.SLUWarna.EditValue & "' Sudah Pernah Diinput. Apakah Tetap Mau Disimpan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                Me.Dispose()
                Exit Sub
            End If
        End If

        If Me.SLUCust.EditValue = "" Or IsDBNull(Me.SLUCust.EditValue) Then
            XtraMessageBox.Show("Customer Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUStyleID.EditValue = "" Or IsDBNull(Me.SLUStyleID.EditValue) Then
            XtraMessageBox.Show("Style ID Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUArtName.EditValue = "" Or IsDBNull(Me.SLUArtName.EditValue) Then
            XtraMessageBox.Show("Article Name Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUWarna.EditValue = "" Or IsDBNull(Me.SLUWarna.EditValue) Then
            XtraMessageBox.Show("Warna Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBBrand.EditValue = "" Or IsDBNull(Me.TBBrand.EditValue) Then
            XtraMessageBox.Show("Brand Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBSL.EditValue = "" Or IsDBNull(Me.TBSL.EditValue) Then
            XtraMessageBox.Show("Shoe Last Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        Else
            XtraMessageBox.Show("Gambar Belum Dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Spec")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyleID.EditValue
                    .Parameters.Add("@Brand", SqlDbType.VarChar).Value = Me.TBBrand.EditValue
                    .Parameters.Add("@ShoeLast", SqlDbType.VarChar).Value = Me.TBSL.EditValue
                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.SLUArtName.EditValue
                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.SLUWarna.EditValue
                    .Parameters.Add("@Range", SqlDbType.VarChar).Value = Me.TBRange.EditValue
                    .Parameters.Add("@Sample", SqlDbType.VarChar).Value = Me.TBSample.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Dibuat", SqlDbType.VarChar).Value = Me.TBDibuat.EditValue
                    .Parameters.Add("@Pattern", SqlDbType.VarChar).Value = Me.TBPattern.EditValue
                    .Parameters.Add("@Chaser", SqlDbType.VarChar).Value = Me.TBChaser.EditValue
                    .Parameters.Add("@PPC", SqlDbType.VarChar).Value = Me.TBPPC.EditValue
                    .Parameters.Add("@PembKulit", SqlDbType.VarChar).Value = Me.TBKulit.EditValue
                    .Parameters.Add("@PembNonKulit", SqlDbType.VarChar).Value = Me.TBNonKulit.EditValue
                    .Parameters.Add("@Teknik", SqlDbType.VarChar).Value = Me.TBTeknik.EditValue
                    .Parameters.Add("@Mengetahui", SqlDbType.VarChar).Value = Me.TBMengetahui.EditValue
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
                                Dim cmSPDtl As New SqlCommand("SPInsM_SpecDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                    .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                    .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                    .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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

                        If Me.PGambar.Image IsNot Nothing Then
                            InsImage(Me.TBKode.EditValue, "Model", Pic)
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
                Dim cmSP As New SqlCommand("SPUpM_Spec")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Revisi", SqlDbType.Int).Value = Me.TBRevisi.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyleID.EditValue
                    .Parameters.Add("@Brand", SqlDbType.VarChar).Value = Me.TBBrand.EditValue
                    .Parameters.Add("@ShoeLast", SqlDbType.VarChar).Value = Me.TBSL.EditValue
                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.SLUArtName.EditValue
                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.SLUWarna.EditValue
                    .Parameters.Add("@Range", SqlDbType.VarChar).Value = Me.TBRange.EditValue
                    .Parameters.Add("@Sample", SqlDbType.VarChar).Value = Me.TBSample.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Dibuat", SqlDbType.VarChar).Value = Me.TBDibuat.EditValue
                    .Parameters.Add("@Pattern", SqlDbType.VarChar).Value = Me.TBPattern.EditValue
                    .Parameters.Add("@Chaser", SqlDbType.VarChar).Value = Me.TBChaser.EditValue
                    .Parameters.Add("@PPC", SqlDbType.VarChar).Value = Me.TBPPC.EditValue
                    .Parameters.Add("@PembKulit", SqlDbType.VarChar).Value = Me.TBKulit.EditValue
                    .Parameters.Add("@PembNonKulit", SqlDbType.VarChar).Value = Me.TBNonKulit.EditValue
                    .Parameters.Add("@Teknik", SqlDbType.VarChar).Value = Me.TBTeknik.EditValue
                    .Parameters.Add("@Mengetahui", SqlDbType.VarChar).Value = Me.TBMengetahui.EditValue
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
                            Dim cmSPDel As New SqlCommand("SPDelM_SpecDtl")
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
                            If Me.GridView1.GetRowCellValue(i, "SpecIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsM_SpecDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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
                                        Me.GridView1.SetRowCellValue(i, "SpecIDD", Me.GridView1.GetRowCellValue(i, "SpecIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpM_SpecDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SpecIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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

                        If Me.PGambar.Image IsNot Nothing Then
                            If PicLama Is Nothing Then
                                InsImage(Me.TBKode.EditValue, "Model", Pic)
                            Else
                                UpImage(Kode, Me.TBKode.EditValue, "Model", Pic)
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

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetRowCellValue(e.RowHandle, "SpecIDD", Me.GridView1.RowCount * -1)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Model", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "SpecID", Me.TBKode.EditValue)
        Me.GridView1.SetRowCellValue(e.RowHandle, "DivID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "KompID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Wrn", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", False)
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsMentah", False)
        Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDInd", "")

        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True

    End Sub

    'Cek Spec Seperti KHI
    'Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
    '    If DsMaster.Tables("M_SpecDtl").Rows.Count - 1 >= 0 Then
    '        If Not IsDBNull(GridView1.GetRowCellValue(e.FocusedRowHandle, "Model")) Then
    '            If GridView1.GetRowCellValue(e.FocusedRowHandle, "Model") > 0 Then
    '                Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
    '                Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
    '                Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False

    '                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '            Else
    '                Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
    '                Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
    '                Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

    '                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '            End If
    '        End If

    '    End If
    'End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If DsMaster.Tables("M_SpecDtl").Rows.Count - 1 >= 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Model")) Then
                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Model") > 0 Then
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False

                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    Else
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                        'Ibu tidak bisa diganti kodenya jika mau ganti hapus insert baru
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
                            Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                            Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                        Else
                            Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                            Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                        End If

                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                        Else
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True

                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub GridView1_RowCountChanged(sender As Object, e As EventArgs) Handles GridView1.RowCountChanged
        If DsMaster.Tables("M_SpecDtl").Rows.Count - 1 >= 0 Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Model")) Then
                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Model") > 0 Then
                    Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False

                    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                Else
                    Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                    Else
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                    End If

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    Else
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                    End If

                End If
            End If

        End If
    End Sub

    Private Sub BEdDivID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdDivID.ButtonClick

        Dim frm As New FSearch("Divisi", 1, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdKompID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdKompID.ButtonClick
        Dim frm As New FSearch("Komponen", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim BBIDLama As String
    Dim DivHapus, KompHapus, BBIDIndHapus As String
    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        BBIDLama = Me.GridView1.ActiveEditor.EditValue

        Dim frm As New FSpec_a()
        frm.ShowDialog()

        Dim DivID As String = ""
        Dim Div As String = ""
        Dim KompID As String = ""
        Dim Komp As String = ""
        Dim BBID As String = ""

        rw = 0

        If Not IsDBNull(dataTrans.Item("Baris").ToString) And CInt(dataTrans.Item("Baris").ToString) > 0 Then
            Dim i : For i = 0 To CInt(dataTrans.Item("Baris").ToString) - 1

                If i = 0 Then
                    DivID = Me.GridView1.GetFocusedDataRow.Item("DivID")
                    Div = Me.GridView1.GetFocusedDataRow.Item("Divisi")
                    KompID = Me.GridView1.GetFocusedDataRow.Item("KompID")
                    Komp = Me.GridView1.GetFocusedDataRow.Item("Komponen")

                    DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
                    KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
                    BBIDIndHapus = Me.GridView1.GetFocusedDataRow.Item("BBID")

                ElseIf i <> 0 Then
                    Me.GridView1.AddNewRow()
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DivID", DivID)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", Div)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KompID", KompID)
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", Komp)

                End If

                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", dataTrans.Item("Kode" & rw).ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BB", dataTrans.Item("Nama" & rw).ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Wrn", dataTrans.Item("Wrn" & rw).ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", dataTrans.Item("Uk" & rw).ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat" & rw).ToString)
                BBID = Me.GridView1.GetFocusedDataRow.Item("BBID")

                If MainModule.AutoBM = True Then
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", dataTrans.Item("stsJasa" & rw).ToString)

                    If CBool(dataTrans.Item("stsJasa" & rw).ToString) = True Then

                        Dim row As Integer

                        Dim Reader As SqlClient.SqlDataReader
                        koneksi.Close()

                        Dim command As New SqlCommand("Select BBIDM,Nama,Wrn,Uk,Sat,stsJasa From M_BBMentah M Inner Join M_BB B On M.BBIDM=B.BBID Where M.BBID='" & BBID & "'", koneksi)

                        With koneksi
                            .Open()
                            Reader = command.ExecuteReader

                            If Reader.HasRows Then
                                While Reader.Read
                                    If IsDBNull(Reader.Item(0)) = True Then

                                    Else
                                        Me.GridView1.AddNewRow()
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DivID", DivID)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", Div)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KompID", KompID)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", Komp)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", Reader.Item(0))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BB", Reader.Item(1))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Wrn", Reader.Item(2))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", Reader.Item(3))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", Reader.Item(4))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", Reader.Item(5))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah", True)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBIDInd", BBID)

                                    End If
                                End While
                            End If
                            Reader.Close()
                            .Close()
                        End With
                    End If
                Else
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", False)

                End If

                rw += 1

            Next
        End If

        'Try
        '    If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
        '        Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
        '        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", dataTrans.Item("Kode").ToString)
        '        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BB", dataTrans.Item("Nama").ToString)
        '        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Wrn", dataTrans.Item("Wrn").ToString)
        '        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", dataTrans.Item("Uk").ToString)
        '        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
        '        'Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", dataTrans.Item("stsJasa").ToString)


        '        DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
        '        KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
        '        BBIDIndHapus = Me.GridView1.GetFocusedDataRow.Item("BBID")

        '        If MainModule.AutoBM = True Then
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", dataTrans.Item("stsJasa").ToString)

        '            If CBool(dataTrans.Item("stsJasa").ToString) = True Then



        '                Dim row As Integer
        '                DivID = Me.GridView1.GetFocusedDataRow.Item("DivID")
        '                Div = Me.GridView1.GetFocusedDataRow.Item("Divisi")
        '                KompID = Me.GridView1.GetFocusedDataRow.Item("KompID")
        '                Komp = Me.GridView1.GetFocusedDataRow.Item("Komponen")
        '                BBID = Me.GridView1.GetFocusedDataRow.Item("BBID")

        '                Dim Reader As SqlClient.SqlDataReader
        '                koneksi.Close()

        '                Dim command As New SqlCommand("Select BBIDM,Nama,Wrn,Uk,Sat,stsJasa From M_BBMentah M Inner Join M_BB B On M.BBIDM=B.BBID Where M.BBID='" & BBID & "'", koneksi)

        '                With koneksi
        '                    .Open()
        '                    Reader = command.ExecuteReader

        '                    If Reader.HasRows Then
        '                        While Reader.Read
        '                            If IsDBNull(Reader.Item(0)) = True Then

        '                            Else
        '                                Me.GridView1.AddNewRow()
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DivID", DivID)
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", Div)
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KompID", KompID)
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", Komp)
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", Reader.Item(0))
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BB", Reader.Item(1))
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Wrn", Reader.Item(2))
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", Reader.Item(3))
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", Reader.Item(4))
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", Reader.Item(5))
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah", True)
        '                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBIDInd", BBID)

        '                            End If
        '                        End While
        '                    End If
        '                    Reader.Close()
        '                    .Close()
        '                End With
        '            End If
        '        Else
        '            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", False)

        '        End If


        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Dim Hapus As Boolean
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            Me.GridView1.ActiveFilter.Clear()

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("SpecIDD")

            DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
            KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
            BBIDIndHapus = Me.GridView1.GetFocusedDataRow.Item("BBID")

            If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = True Then
                Hapus = True
            Else
                Hapus = False
            End If
        End If

    End Sub
    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        If Hapus = True Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(i, "BBIDInd") <> "" Then
                    If Me.GridView1.GetRowCellValue(i, "DivID") = DivHapus And Me.GridView1.GetRowCellValue(i, "KompID") = KompHapus And Me.GridView1.GetRowCellValue(i, "BBIDInd") = BBIDIndHapus Then

                        'Anak Beranak
                        'Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
                        '    If Me.GridView1.GetRowCellValue(y, "BBIDInd") <> "" Then
                        '        If Me.GridView1.GetRowCellValue(y, "DivID") = DivHapus And Me.GridView1.GetRowCellValue(y, "KompID") = KompHapus And Me.GridView1.GetRowCellValue(y, "BBIDInd") = BBIDIndHapus Then
                        '            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        '            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "SpecIDD")

                        '            Me.GridView1.DeleteRow(y)

                        '        End If
                        '    End If
                        'Next

                        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SpecIDD")

                        Me.GridView1.DeleteRow(i)

                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BEdDivID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdDivID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdKompID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdKompID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub SLUArtName_Leave(sender As Object, e As EventArgs) Handles SLUArtName.Leave
        If Not IsDBNull(Me.SLUArtName.EditValue) And Me.SLUArtName.Properties.ReadOnly = False Then
            Me.SLUWarna.EditValue = ""

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select Distinct W.Nama As Warna From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and ArtName='" & Me.SLUArtName.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "Warna")
            cmsl.Fill(DsMaster, "Warna")
            DsMaster.Tables("Warna").Clear()
            cmsl.Fill(DsMaster, "Warna")

            Me.SLUWarna.Properties.DataSource = DsMaster.Tables("Warna")
            Me.SLUWarna.Properties.DisplayMember = "Warna"
            Me.SLUWarna.Properties.ValueMember = "Warna"
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FSpec_d(Me.GridView2.GetFocusedDataRow.Item("SpecID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        Dim frm As New FSearch("Spec", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.TBKode.EditValue = "--"
                Me.TBRevisi.EditValue = ""
                Me.SLUCust.EditValue = dataTrans.Item("CustID").ToString
                Me.SLUStyleID.EditValue = dataTrans.Item("StyleID").ToString

                If Me.SLUStyleID.Properties.ReadOnly = False Then
                    Dim cmsl As SqlDataAdapter

                    cmsl = New SqlDataAdapter("Select Distinct ArtName,Case when B.MerkID='' Then Gol Else M.Nama End As Brand From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where B.Aktif='True' and StyleID='" & Me.SLUStyleID.EditValue & "'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_BrgSp")
                    cmsl.Fill(DsMaster, "M_BrgSp")
                    DsMaster.Tables("M_BrgSp").Clear()
                    cmsl.Fill(DsMaster, "M_BrgSp")

                    Me.SLUArtName.Properties.DataSource = DsMaster.Tables("M_BrgSp")
                    Me.SLUArtName.Properties.DisplayMember = "ArtName"
                    Me.SLUArtName.Properties.ValueMember = "ArtName"
                End If

                Me.SLUArtName.EditValue = dataTrans.Item("ArtName").ToString

                If Not IsDBNull(Me.SLUArtName.EditValue) Then
                    Dim cmsl As SqlDataAdapter

                    cmsl = New SqlDataAdapter("Select Distinct W.Nama As Warna From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where B.Aktif='True' and ArtName='" & Me.SLUArtName.EditValue & "'", koneksi)
                    cmsl.TableMappings.Add("Table", "Warna")
                    cmsl.Fill(DsMaster, "Warna")

                    Me.SLUWarna.Properties.DataSource = DsMaster.Tables("Warna")
                    Me.SLUWarna.Properties.DisplayMember = "Warna"
                    Me.SLUWarna.Properties.ValueMember = "Warna"
                End If

                Me.TBBrand.EditValue = dataTrans.Item("Brand").ToString
                Me.TBSL.EditValue = dataTrans.Item("ShoeLast").ToString
                Me.SLUWarna.EditValue = dataTrans.Item("Warna").ToString
                Me.TBRange.EditValue = dataTrans.Item("RangeSize").ToString
                Me.TBSample.EditValue = dataTrans.Item("SampleSize").ToString
                'Me.TBDibuat.EditValue = dataTrans.Item("Dibuat").ToString
                'Me.TBPattern.EditValue = dataTrans.Item("Pattern").ToString
                Me.TBChaser.EditValue = dataTrans.Item("Chaser").ToString
                'Me.TBPPC.EditValue = dataTrans.Item("PPC").ToString
                Me.TBKulit.EditValue = dataTrans.Item("PembKulit").ToString
                'Me.TBNonKulit.EditValue = dataTrans.Item("PembNonKulit").ToString
                Me.TBTeknik.EditValue = dataTrans.Item("Teknik").ToString
                Me.TBMengetahui.EditValue = dataTrans.Item("Mengetahui").ToString
                Me.MKet.EditValue = dataTrans.Item("Ket").ToString


                Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(i, "SpecIDD")

                    Me.GridView1.DeleteRow(i)
                Next

                FillDtl(dataTrans.Item("Kode").ToString)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SLUStyleID_Leave(sender As Object, e As EventArgs) Handles SLUStyleID.Leave
        If Me.SLUStyleID.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select Distinct ArtName,Case when B.MerkID='' Then Gol Else M.Nama End As Brand From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where B.Aktif='True' and StyleID='" & Me.SLUStyleID.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_BrgSp")
            cmsl.Fill(DsMaster, "M_BrgSp")
            DsMaster.Tables("M_BrgSp").Clear()
            cmsl.Fill(DsMaster, "M_BrgSp")

            Me.SLUArtName.Properties.DataSource = DsMaster.Tables("M_BrgSp")
            Me.SLUArtName.Properties.DisplayMember = "ArtName"
            Me.SLUArtName.Properties.ValueMember = "ArtName"
        End If
    End Sub

   
    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBBrand.KeyPress, TBSL.KeyPress, TBRange.KeyPress, TBSample.KeyPress, MKet.KeyPress, TBDibuat.KeyPress, TBPattern.KeyPress, TBChaser.KeyPress, TBPPC.KeyPress, TBKulit.KeyPress, TBNonKulit.KeyPress, TBTeknik.KeyPress, TBMengetahui.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Uk" Or view.FocusedColumn.FieldName = "Ket" Then
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
