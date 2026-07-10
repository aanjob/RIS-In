Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FVarTarget
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=51", koneksi)

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
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("VarTrgtN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTVarTarget_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUSubJns.Properties.ReadOnly = True
        Me.TBJan.Properties.ReadOnly = True
        Me.TBFeb.Properties.ReadOnly = True
        Me.TBMar.Properties.ReadOnly = True
        Me.TBApr.Properties.ReadOnly = True
        Me.TBMei.Properties.ReadOnly = True
        Me.TBJun.Properties.ReadOnly = True
        Me.TBJul.Properties.ReadOnly = True
        Me.TBAgt.Properties.ReadOnly = True
        Me.TBSep.Properties.ReadOnly = True
        Me.TBOkt.Properties.ReadOnly = True
        Me.TBNov.Properties.ReadOnly = True
        Me.TBDes.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True

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
        Me.BVTVarTarget_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUSubJns.Properties.ReadOnly = False
        Me.TBJan.Properties.ReadOnly = False
        Me.TBFeb.Properties.ReadOnly = False
        Me.TBMar.Properties.ReadOnly = False
        Me.TBApr.Properties.ReadOnly = False
        Me.TBMei.Properties.ReadOnly = False
        Me.TBJun.Properties.ReadOnly = False
        Me.TBJul.Properties.ReadOnly = False
        Me.TBAgt.Properties.ReadOnly = False
        Me.TBSep.Properties.ReadOnly = False
        Me.TBOkt.Properties.ReadOnly = False
        Me.TBNov.Properties.ReadOnly = False
        Me.TBDes.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTVarTarget_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct SubJns From M_Brg where SubJns<>'' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_SubJnsLUE")
        cmsl.Fill(DsMaster, "M_SubJnsLUE")
        DsMaster.Tables("M_SubJnsLUE").Clear()
        cmsl.Fill(DsMaster, "M_SubJnsLUE")
        DsMaster.Tables("M_SubJnsLUE").Rows.Add("%")

        Me.SLUSubJns.Properties.DataSource = DsMaster.Tables("M_SubJnsLUE")
        Me.SLUSubJns.Properties.DisplayMember = "SubJns"
        Me.SLUSubJns.Properties.ValueMember = "SubJns"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select VarID,PeriodID,CodeID,Tanggal,SubJns,Jan,Feb,Mar,Apr,Mei,Jun,Jul,Agt,Sep,Okt,Nov,Dess,Aktif,InsDate,InsBy, UpdDate,UpdBy From M_VarTarget", koneksi)
        cmsl.TableMappings.Add("Table", "M_VarTarget")
        cmsl.Fill(DsMaster, "M_VarTarget")
        DsMaster.Tables("M_VarTarget").Clear()
        cmsl.Fill(DsMaster, "M_VarTarget")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_VarTarget"
    End Sub

    Private Sub FVarTarget_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Variabel Target"
    End Sub

    Private Sub FVarTarget_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FVarTarget_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FVarTarget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTVarTarget_e.Selected = True
        'AnimateWindow(Handle, 1500, 4 Or 262144)
    End Sub

    Private Sub BVTVarTarget_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTVarTarget_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Variabel Target"

        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("VarTrgtEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("VarTrgtDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Variabel Target"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        Indicator = "100"
        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.SLUSubJns.EditValue = ""
        Me.TBJan.EditValue = 0.0
        Me.TBFeb.EditValue = 0.0
        Me.TBMar.EditValue = 0.0
        Me.TBApr.EditValue = 0.0
        Me.TBMei.EditValue = 0.0
        Me.TBJun.EditValue = 0.0
        Me.TBJul.EditValue = 0.0
        Me.TBAgt.EditValue = 0.0
        Me.TBSep.EditValue = 0.0
        Me.TBOkt.EditValue = 0.0
        Me.TBNov.EditValue = 0.0
        Me.TBDes.EditValue = 0.0
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Variabel Target"

        Indicator = "200"
        LUE()

        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("VarID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("VarID")
        Me.DTPTanggal.EditValue = Me.GridView1.GetFocusedDataRow.Item("Tanggal")
        Me.SLUSubJns.EditValue = Me.GridView1.GetFocusedDataRow.Item("SubJns")
        Me.TBJan.EditValue = Me.GridView1.GetFocusedDataRow.Item("Jan")
        Me.TBFeb.EditValue = Me.GridView1.GetFocusedDataRow.Item("Feb")
        Me.TBMar.EditValue = Me.GridView1.GetFocusedDataRow.Item("Mar")
        Me.TBApr.EditValue = Me.GridView1.GetFocusedDataRow.Item("Apr")
        Me.TBMei.EditValue = Me.GridView1.GetFocusedDataRow.Item("Mei")
        Me.TBJun.EditValue = Me.GridView1.GetFocusedDataRow.Item("Jun")
        Me.TBJul.EditValue = Me.GridView1.GetFocusedDataRow.Item("Jul")
        Me.TBAgt.EditValue = Me.GridView1.GetFocusedDataRow.Item("Agt")
        Me.TBSep.EditValue = Me.GridView1.GetFocusedDataRow.Item("Sep")
        Me.TBOkt.EditValue = Me.GridView1.GetFocusedDataRow.Item("Okt")
        Me.TBNov.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nov")
        Me.TBDes.EditValue = Me.GridView1.GetFocusedDataRow.Item("Dess")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Variabel Target"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Variabel Target : " & Me.GridView1.GetFocusedDataRow.Item("VarID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_VarTarget")
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
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue <> 100 Then
            XtraMessageBox.Show("Prosentase Belum Mencapai 100%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_VarTarget")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.TBJan.EditValue
                    .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.TBFeb.EditValue
                    .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.TBMar.EditValue
                    .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.TBApr.EditValue
                    .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.TBMei.EditValue
                    .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.TBJun.EditValue
                    .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.TBJul.EditValue
                    .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.TBAgt.EditValue
                    .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.TBSep.EditValue
                    .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.TBOkt.EditValue
                    .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.TBNov.EditValue
                    .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.TBDes.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_VarTarget")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.SLUSubJns.EditValue
                    .Parameters.Add("@Jan", SqlDbType.Decimal).Value = Me.TBJan.EditValue
                    .Parameters.Add("@Feb", SqlDbType.Decimal).Value = Me.TBFeb.EditValue
                    .Parameters.Add("@Mar", SqlDbType.Decimal).Value = Me.TBMar.EditValue
                    .Parameters.Add("@Apr", SqlDbType.Decimal).Value = Me.TBApr.EditValue
                    .Parameters.Add("@Mei", SqlDbType.Decimal).Value = Me.TBMei.EditValue
                    .Parameters.Add("@Jun", SqlDbType.Decimal).Value = Me.TBJun.EditValue
                    .Parameters.Add("@Jul", SqlDbType.Decimal).Value = Me.TBJul.EditValue
                    .Parameters.Add("@Agt", SqlDbType.Decimal).Value = Me.TBAgt.EditValue
                    .Parameters.Add("@Sep", SqlDbType.Decimal).Value = Me.TBSep.EditValue
                    .Parameters.Add("@Okt", SqlDbType.Decimal).Value = Me.TBOkt.EditValue
                    .Parameters.Add("@Nov", SqlDbType.Decimal).Value = Me.TBNov.EditValue
                    .Parameters.Add("@Dess", SqlDbType.Decimal).Value = Me.TBDes.EditValue
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
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub TBJan_EditValueChanged(sender As Object, e As EventArgs) Handles TBJan.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBFeb_EditValueChanged(sender As Object, e As EventArgs) Handles TBFeb.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBMar_EditValueChanged(sender As Object, e As EventArgs) Handles TBMar.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBApr_EditValueChanged(sender As Object, e As EventArgs) Handles TBApr.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBMei_EditValueChanged(sender As Object, e As EventArgs) Handles TBMei.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBJun_EditValueChanged(sender As Object, e As EventArgs) Handles TBJun.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBJul_EditValueChanged(sender As Object, e As EventArgs) Handles TBJul.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBAgt_EditValueChanged(sender As Object, e As EventArgs) Handles TBAgt.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBSep_EditValueChanged(sender As Object, e As EventArgs) Handles TBSep.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBOkt_EditValueChanged(sender As Object, e As EventArgs) Handles TBOkt.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBNov_EditValueChanged(sender As Object, e As EventArgs) Handles TBNov.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

    Private Sub TBDes_EditValueChanged(sender As Object, e As EventArgs) Handles TBDes.EditValueChanged
        Me.TBTot.EditValue = Me.TBJan.EditValue + Me.TBFeb.EditValue + Me.TBMar.EditValue + Me.TBApr.EditValue + Me.TBMei.EditValue + Me.TBJun.EditValue + Me.TBJul.EditValue + Me.TBAgt.EditValue + Me.TBSep.EditValue + Me.TBOkt.EditValue + Me.TBNov.EditValue + Me.TBDes.EditValue
    End Sub

End Class