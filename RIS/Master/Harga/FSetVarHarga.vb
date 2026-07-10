Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSetVarHarga
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=50", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("VarHN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("VarHEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("VarHDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTVarHarga_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.SLUKat.Properties.ReadOnly = True
        Me.TBLF.Properties.ReadOnly = True
        Me.TBPr.Properties.ReadOnly = True
        Me.TBMC.Properties.ReadOnly = True
        Me.TBCBP.Properties.ReadOnly = True
        Me.TBAg.Properties.ReadOnly = True
        Me.TBGr.Properties.ReadOnly = True
        Me.TBRt.Properties.ReadOnly = True
        Me.TBKs.Properties.ReadOnly = True
        Me.TBBltNetto.Properties.ReadOnly = True
        Me.TBBltCBP.Properties.ReadOnly = True

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTVarHarga_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.SLUKat.Properties.ReadOnly = False
        Me.TBLF.Properties.ReadOnly = False
        Me.TBPr.Properties.ReadOnly = False
        Me.TBMC.Properties.ReadOnly = False
        Me.TBCBP.Properties.ReadOnly = False
        Me.TBAg.Properties.ReadOnly = False
        Me.TBGr.Properties.ReadOnly = False
        Me.TBRt.Properties.ReadOnly = False
        Me.TBKs.Properties.ReadOnly = False
        Me.TBBltNetto.Properties.ReadOnly = False
        Me.TBBltCBP.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTVarHarga_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select KatID,Nama From M_BrgKat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgKatLUE")
        Try
            DsMaster.Tables("M_BrgKatLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgKatLUE")

        Me.SLUKat.Properties.DataSource = DsMaster.Tables("M_BrgKatLUE")
        Me.SLUKat.Properties.DisplayMember = "Nama"
        Me.SLUKat.Properties.ValueMember = "KatID"
    End Sub
    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select VarID,Tanggal,VH.Nama,VH.KatID,K.Nama As Kat,LF,Pr,MC,Ag,Gr,Rt,Ks,CBP,BulatNetto,BulatCBP,VH.Aktif, VH.InsDate,VH.InsBy,VH.UpdDate,VH.UpdBy From M_VarHarga VH Inner Join M_BrgKat K On VH.KatID=K.KatID", koneksi)

        cmsl.TableMappings.Add("Table", "M_VarHarga")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_VarHarga")
        DsMaster.Tables("M_VarHarga").Clear()
        cmsl.Fill(DsMaster, "M_VarHarga")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_VarHarga"
    End Sub

    Private Sub FSetVarHarga_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Setting Variabel Harga"
    End Sub

    Private Sub FSetVarHarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTVarHarga_e.Selected = True
    End Sub

    Private Sub BVTVarHarga_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTVarHarga_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Setting Variabel Harga"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Setting Variabel Harga"

        DsMaster.Clear()

        OpenControl()
        LUE()
        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.DTPTanggal.EditValue = DateAndTime.Now
        Me.TBNama.EditValue = ""
        Me.SLUKat.EditValue = ""
        Me.TBLF.EditValue = 0.0
        Me.TBPr.EditValue = 0.0
        Me.TBMC.EditValue = 0.0
        Me.TBCBP.EditValue = 0.0
        Me.TBAg.EditValue = 0.0
        Me.TBGr.EditValue = 0.0
        Me.TBRt.EditValue = 0.0
        Me.TBBltNetto.EditValue = 0
        Me.TBBltCBP.EditValue = 0
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Setting Variabel Harga"

        Indicator = "200"
        LUE()

        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("VarID")
        Me.DTPTanggal.EditValue = Me.GridView1.GetFocusedDataRow.Item("Tanggal")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.SLUKat.EditValue = Me.GridView1.GetFocusedDataRow.Item("KatID")
        Me.TBLF.EditValue = Me.GridView1.GetFocusedDataRow.Item("LF")
        Me.TBPr.EditValue = Me.GridView1.GetFocusedDataRow.Item("Pr")
        Me.TBMC.EditValue = Me.GridView1.GetFocusedDataRow.Item("MC")
        Me.TBCBP.EditValue = Me.GridView1.GetFocusedDataRow.Item("CBP")
        Me.TBAg.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ag")
        Me.TBGr.EditValue = Me.GridView1.GetFocusedDataRow.Item("Gr")
        Me.TBRt.EditValue = Me.GridView1.GetFocusedDataRow.Item("Rt")
        Me.TBKs.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ks")
        Me.TBBltNetto.EditValue = Me.GridView1.GetFocusedDataRow.Item("BulatNetto")
        Me.TBBltCBP.EditValue = Me.GridView1.GetFocusedDataRow.Item("BulatCBP")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        Me.CEAktif.Properties.ReadOnly = False
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Setting Variabel Harga"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Variabel Harga Dengan ID : " & Me.GridView1.GetFocusedDataRow.Item("VarID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_VarHarga")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("VarID")
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

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_VarHarga")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                    .Parameters.Add("@LF", SqlDbType.Decimal).Value = Me.TBLF.EditValue
                    .Parameters.Add("@Pr", SqlDbType.Decimal).Value = Me.TBPr.EditValue
                    .Parameters.Add("@MC", SqlDbType.Decimal).Value = Me.TBMC.EditValue
                    .Parameters.Add("@CBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                    .Parameters.Add("@Ag", SqlDbType.Decimal).Value = Me.TBAg.EditValue
                    .Parameters.Add("@Gr", SqlDbType.Decimal).Value = Me.TBGr.EditValue
                    .Parameters.Add("@Rt", SqlDbType.Decimal).Value = Me.TBRt.EditValue
                    .Parameters.Add("@Ks", SqlDbType.Decimal).Value = Me.TBKs.EditValue
                    .Parameters.Add("@BulatNetto", SqlDbType.Int).Value = Me.TBBltNetto.EditValue
                    .Parameters.Add("@BulatCBP", SqlDbType.Int).Value = Me.TBBltCBP.EditValue
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
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With
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
                End With
            Case 200
                Dim cmSP As New SqlCommand("SPUpM_VarHarga")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                    .Parameters.Add("@LF", SqlDbType.Decimal).Value = Me.TBLF.EditValue
                    .Parameters.Add("@Pr", SqlDbType.Decimal).Value = Me.TBPr.EditValue
                    .Parameters.Add("@MC", SqlDbType.Decimal).Value = Me.TBMC.EditValue
                    .Parameters.Add("@CBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                    .Parameters.Add("@Ag", SqlDbType.Decimal).Value = Me.TBAg.EditValue
                    .Parameters.Add("@Gr", SqlDbType.Decimal).Value = Me.TBGr.EditValue
                    .Parameters.Add("@Rt", SqlDbType.Decimal).Value = Me.TBRt.EditValue
                    .Parameters.Add("@Ks", SqlDbType.Decimal).Value = Me.TBKs.EditValue
                    .Parameters.Add("@BulatNetto", SqlDbType.Int).Value = Me.TBBltNetto.EditValue
                    .Parameters.Add("@BulatCBP", SqlDbType.Int).Value = Me.TBBltCBP.EditValue
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
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub


    Private Sub TBLF_KeyDown(sender As Object, e As KeyEventArgs) Handles TBLF.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBLF_KeyUp(sender As Object, e As KeyEventArgs) Handles TBLF.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub


    Private Sub TBPr_KeyDown(sender As Object, e As KeyEventArgs) Handles TBPr.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBPr_KeyUp(sender As Object, e As KeyEventArgs) Handles TBPr.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBMC_KeyDown(sender As Object, e As KeyEventArgs) Handles TBMC.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBMC_KeyUp(sender As Object, e As KeyEventArgs) Handles TBMC.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBCBP_KeyDown(sender As Object, e As KeyEventArgs) Handles TBCBP.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBCBP_KeyUp(sender As Object, e As KeyEventArgs) Handles TBCBP.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBAg_KeyDown(sender As Object, e As KeyEventArgs) Handles TBAg.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBAg_KeyUp(sender As Object, e As KeyEventArgs) Handles TBAg.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGr_KeyDown(sender As Object, e As KeyEventArgs) Handles TBGr.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBGr_KeyUp(sender As Object, e As KeyEventArgs) Handles TBGr.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBRt_KeyDown(sender As Object, e As KeyEventArgs) Handles TBRt.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBRt_KeyUp(sender As Object, e As KeyEventArgs) Handles TBRt.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBKs_KeyDown(sender As Object, e As KeyEventArgs) Handles TBKs.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBKs_KeyUp(sender As Object, e As KeyEventArgs) Handles TBKs.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBBltCBP_KeyDown(sender As Object, e As KeyEventArgs) Handles TBBltCBP.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBBltCBP_KeyUp(sender As Object, e As KeyEventArgs) Handles TBBltCBP.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBBltNetto_KeyDown(sender As Object, e As KeyEventArgs) Handles TBBltNetto.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBBltNetto_KeyUp(sender As Object, e As KeyEventArgs) Handles TBBltNetto.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
   
End Class
