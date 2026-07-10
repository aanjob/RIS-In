Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FAdjBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID, OpBJIDLm, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=29 and Gol='" & Golongan & "'", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("AdjBJN"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("AdjBJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTAdjBJ_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUGrup.Properties.ReadOnly = True
        Me.SLUOpID.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTAdjBJ_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUOpID.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTAdjBJ_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Aktif='True' and Gol <>'Bahan'", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE" & Gol)
        Try
            DsMaster.Tables("M_GudangLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_GudangLUE" & Gol)

        Me.SLUGd.Properties.DataSource = DsMaster.Tables("M_GudangLUE" & Gol)
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        cmsl = New SqlDataAdapter("Select SatID,Nama,Isi From M_BrgSat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSatLUE")
        Try
            DsMaster.Tables("M_BrgSatLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgSatLUE")

        Me.SLUSat.Properties.DataSource = DsMaster.Tables("M_BrgSatLUE")
        Me.SLUSat.Properties.DisplayMember = "Nama"
        Me.SLUSat.Properties.ValueMember = "SatID"

        DsMaster.Tables("M_BrgSatLUE").Rows.Add("%", "Semua", 0)
        DsMaster.Tables("M_BrgSatLUE").Rows.Add("D%", "Semua Dos", 0)
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AdjBJIDD,AdjBJID,D.ArtCode,ArtName,D.SatID,D.Isi,Qty,Ket From T_AdjBJDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where AdjBJID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_AdjBJDtl" & Gol)
        Try
            DsMaster.Tables("T_AdjBJDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_AdjBJDtl" & Gol)

        DsMaster.Tables("T_AdjBJDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_AdjBJDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_AdjBJDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AdjBJID,PeriodID,CodeID,OpBJID,Tanggal,H.GdID,G.Nama As Gudang,H.SatID,H.Ket,H.Grup,H.Gol,H.InsDate,H.InsBy From T_AdjBJ H Inner Join M_Gudang G On H.GdID=G.GdID and PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,AdjBJID asc", koneksi)
        cmsl.TableMappings.Add("Table", "T_AdjBJ" & Gol)
        Try
            DsMaster.Tables("T_AdjBJ" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_AdjBJ" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_AdjBJ" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_AdjBJ")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@OpBJID", SqlDbType.VarChar).Value = Me.SLUOpID.EditValue
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

    Private Sub FAdjBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Adjustment Barang Jadi"
    End Sub

    Private Sub FAdjBJ_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FAdjBJ_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FAdjBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTAdjBJ_e.Selected = True
    End Sub

    Private Sub BVTAdjBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTAdjBJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Adjustment Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("AdjBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Adjustment Barang Jadi"

        DsMaster.Clear()

        If MainModule.SlstsPeriodNew() = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

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

        Me.SLUGrup.EditValue = ""
        Me.SLUOpID.EditValue = ""
        Me.SLUGd.EditValue = ""
        Me.SLUSat.EditValue = ""
        Me.DTPTanggal.EditValue = Date.Now
        Me.MKet.EditValue = ""
        OpBJIDLm = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_AdjBJDtl" & Gol).Clear()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Adjustment Barang Jadi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("AdjBJID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelT_AdjBJ")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("AdjBJID")
                .Parameters.Add("@OpBJID", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("OpBJID")
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
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("AdjBJID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Gudang"), "Gudang")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")

        Dim XR As New XRAdjBJ
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        If MainModule.SlstsPeriodEdDel(MainModule.periodAktif) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_AdjBJ")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@OpBJID", SqlDbType.VarChar).Value = Me.SLUOpID.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSat.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
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
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_AdjBJDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                                    .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 29
                                    .Parameters.Add("@GdID", SqlDbType.VarChar).Value = Me.SLUGd.EditValue
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                    .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        If Me.SLUGrup.Properties.ReadOnly = False Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select OpBJID,PeriodID,Tanggal,GdID,SatID,Ket From T_OpBJ Where Terproses='False' and Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "T_OpBJLUE" & Gol)
            Try
                DsMaster.Tables("T_OpBJLUE" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "T_OpBJLUE" & Gol)

            Me.SLUOpID.Properties.DataSource = DsMaster.Tables("T_OpBJLUE" & Gol)
            Me.SLUOpID.Properties.DisplayMember = "OpBJID"
            Me.SLUOpID.Properties.ValueMember = "OpBJID"
        End If
    End Sub

    Private Sub SLUOpID_Leave(sender As Object, e As EventArgs) Handles SLUOpID.Leave
        If Me.SLUOpID.EditValue <> "" And Not IsDBNull(Me.SLUOpID.EditValue) And Me.SLUOpID.Properties.ReadOnly = False Then
            DsMaster.Tables("T_AdjBJDtl" & Gol).Clear()
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select '--' As AdjBJID,ROW_NUMBER() over (ORDER BY B.ArtCode)*-1 as AdjBJIDD ,B.ArtCode,B.ArtName, B.SatID,B.Isi,Selisih As Qty,Ket From T_OpBJDtl D Inner Join M_Brg B On B.ArtCode=D.ArtCode Where OpBJID ='" & Me.SLUOpID.EditValue & "' and Selisih <>0", koneksi)
            cmsl.Fill(DsMaster, "T_AdjBJDtl" & Gol)

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_AdjBJDtl" & Gol

            Me.BSave.Focus()
            Me.GridView1.RefreshData()

            If DsMaster.Tables("T_OpBJLUE" & Gol).Select("OpBJID = '" & Me.SLUOpID.EditValue & "'")(0).Item("PeriodID") <> MainModule.periodAktif Then
                Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
            Else
                Me.DTPTanggal.EditValue = DsMaster.Tables("T_OpBJLUE" & Gol).Select("OpBJID = '" & Me.SLUOpID.EditValue & "'")(0).Item("Tanggal")
            End If

            Me.SLUSat.EditValue = DsMaster.Tables("T_OpBJLUE" & Gol).Select("OpBJID = '" & Me.SLUOpID.EditValue & "'")(0).Item("SatID")
            Me.SLUGd.EditValue = DsMaster.Tables("T_OpBJLUE" & Gol).Select("OpBJID = '" & Me.SLUOpID.EditValue & "'")(0).Item("GdID")
            Me.MKet.EditValue = DsMaster.Tables("T_OpBJLUE" & Gol).Select("OpBJID = '" & Me.SLUOpID.EditValue & "'")(0).Item("Ket")
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FAdjBJ_d(Me.GridView2.GetFocusedDataRow.Item("AdjBJID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If GridView1.GetRowCellValue(e.RowHandle, "Qty") < 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Green
                ElseIf GridView1.GetRowCellValue(e.RowHandle, "Qty") = 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, MKet.KeyPress
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