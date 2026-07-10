Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FUbahHarga
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama, Gol, CurrID As String
    Dim CekAll As Boolean

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        Gol = Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("UHN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("UHEd"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTUbahHarga_s.Enabled = True

        Me.CBOStatus.Properties.ReadOnly = True
        Me.SLUMtUang.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.TBHarga.Properties.ReadOnly = True
        Me.TBCBP.Properties.ReadOnly = True
        Me.TBOB.Properties.ReadOnly = True
        Me.SPIsi.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView2.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTUbahHarga_s.Enabled = False

        Me.CBOStatus.Properties.ReadOnly = False
        Me.SLUMtUang.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOStatus.Properties.ReadOnly = False
        Me.SPIsi.Properties.ReadOnly = False
        Me.TBHarga.Properties.ReadOnly = False
        Me.TBCBP.Properties.ReadOnly = False
        Me.TBOB.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView2.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTUbahHarga_e.Selected = True

        LUE()
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CurrID,MtUang,NilTukarRp,stsHarga,H.ArtCode,B.ArtName,H.SatID,H.Isi,Jenis,Tanggal,MtUang,HargaCBP, Harga,DiscOB From M_BrgHarga H Inner Join M_Brg B On B.ArtCode=H.ArtCode Where H.Aktif='True' and Gol='" & Gol & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgHarga" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgHarga" & Gol)
        DsMaster.Tables("M_BrgHarga" & Gol).Clear()
        cmsl.Fill(DsMaster, "M_BrgHarga" & Gol)

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "M_BrgHarga" & Gol
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Cast('FALSE' as Bit) As Cek,ArtCode,ArtName,SatID,Isi,(Select Max(Tanggal) From M_BrgHarga where ArtCode=M_Brg.ArtCode and Aktif='True') As Tanggal From M_Brg Where Aktif='True' and Gol='" & Gol & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgL" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgL" & Gol)
        DsMaster.Tables("M_BrgL" & Gol).Clear()
        cmsl.Fill(DsMaster, "M_BrgL" & Gol)

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgL" & Gol

        cmsl = New SqlDataAdapter("Select Distinct Jenis, Cast('FALSE' as Bit) As Cek From M_JnsCust Where Aktif='True'", koneksi)

        cmsl.TableMappings.Add("Table", "M_JnsCust" & Gol)
        cmsl.Fill(DsMaster, "M_JnsCust" & Gol)
        DsMaster.Tables("M_JnsCust" & Gol).Clear()
        cmsl.Fill(DsMaster, "M_JnsCust" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_JnsCust" & Gol

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        Try
            DsMaster.Tables("M_CurrLUE").Clear()
        Catch ex As Exception

        End Try
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

    Private Sub FUbahHarga_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Ubah Harga Barang Jadi"
    End Sub

    Private Sub FUbahHarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTUbahHarga_e.Selected = True
    End Sub

    Private Sub BVTUbahHarga_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTUbahHarga_s.ItemPressed
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Harga Barang Jadi"

        DsMaster.Clear()

        OpenControl()
        Indicator = "100"

        Me.CBOStatus.EditValue = ""
        Me.SLUMtUang.EditValue = "IDR"
        Me.TBNilTukarRp.EditValue = 0.0
        Me.DTPTanggal.EditValue = Date.Now
        Me.TBCBP.EditValue = 0.0
        Me.TBHarga.EditValue = 0.0
        Me.TBOB.EditValue = 0.0
        Me.SPIsi.EditValue = 1
        CekCurr()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Harga Barang Jadi"

        OpenControl()
        Indicator = "200"
        Me.DTPTanggal.Properties.ReadOnly = True

        Me.CBOStatus.EditValue = Me.GridView3.GetFocusedDataRow.Item("stsHarga")
        Me.SLUMtUang.EditValue = Me.GridView3.GetFocusedDataRow.Item("MtUang")
        CurrID = Me.GridView3.GetFocusedDataRow.Item("CurrID")
        Me.TBNilTukarRp.EditValue = Me.GridView3.GetFocusedDataRow.Item("NilTukarRp")
        Me.DTPTanggal.EditValue = Me.GridView3.GetFocusedDataRow.Item("Tanggal")
        Me.TBCBP.EditValue = Me.GridView3.GetFocusedDataRow.Item("HargaCBP")
        Me.TBHarga.EditValue = Me.GridView3.GetFocusedDataRow.Item("Harga")
        Me.TBOB.EditValue = Me.GridView3.GetFocusedDataRow.Item("DiscOB")
        Me.SPIsi.EditValue = Me.GridView3.GetFocusedDataRow.Item("Isi")
    End Sub
    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.CBOStatus.EditValue = "" Or IsDBNull(Me.CBOStatus.EditValue) Then
            XtraMessageBox.Show("Status Harus Diisi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.GridView1.ActiveFilterString = "[Cek] = True"

        Select Case Indicator
            Case 100

                Dim a : For a = 0 To GridView1.RowCount - 1
                    If Me.GridView1.GetRowCellValue(a, "Cek") = True Then
                        If Not IsDBNull(Me.GridView1.GetRowCellValue(a, "Tanggal")) Then
                            If Me.DTPTanggal.EditValue < Me.GridView1.GetRowCellValue(a, "Tanggal") Then
                                XtraMessageBox.Show(Me.GridView1.GetRowCellValue(a, "ArtCode") & ": Tanggal Harus Disetting Melebihi Setting Harga Terakhir", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If
                        End If
                    End If
                Next

                Dim x As Integer

                Dim y : For y = 0 To GridView2.RowCount - 1
                    If Me.GridView2.GetRowCellValue(y, "Cek") = True Then

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then

                                If Me.GridView1.GetRowCellValue(i, "SatID") = "P" Then
                                    'Set Harga Pasangan
                                    Dim cmSP As New SqlCommand("SPInsM_BrgHarga")
                                    cmSP.CommandType = CommandType.StoredProcedure

                                    With cmSP
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = True
                                        .Parameters.Add("@TblIDD", SqlDbType.Int).Value = 0
                                        .Parameters.Add("@Status", SqlDbType.VarChar).Value = Me.CBOStatus.EditValue
                                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(y, "Jenis")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@HargaCBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                                        .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.TBHarga.EditValue / Me.SPIsi.EditValue
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.TBOB.EditValue
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

                                            If x < 0 Then
                                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                        Catch ex As Exception
                                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End Try
                                    End With

                                Else
                                    'Set Harga Sesuai TB

                                    Dim cmSP As New SqlCommand("SPInsM_BrgHarga")
                                    cmSP.CommandType = CommandType.StoredProcedure


                                    With cmSP
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = True
                                        .Parameters.Add("@TblIDD", SqlDbType.Int).Value = 0
                                        .Parameters.Add("@Status", SqlDbType.VarChar).Value = Me.CBOStatus.EditValue
                                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                        .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(y, "Jenis")
                                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                                        .Parameters.Add("@HargaCBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                                        .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.TBHarga.EditValue
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.TBOB.EditValue
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

                                            If x < 0 Then
                                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                        Catch ex As Exception
                                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End Try
                                    End With
                                End If

                            End If
                        Next

                    End If

                Next

                XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Case 200
                Dim x As Integer

                Dim y : For y = 0 To GridView2.RowCount - 1
                    If Me.GridView2.GetRowCellValue(y, "Cek") = True Then

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then

                                If Me.GridView1.GetRowCellValue(i, "SatID") = "P" Then
                                    'Set Harga Pasangan
                                    Dim cmSP As New SqlCommand("SPUpM_BrgHarga")
                                    cmSP.CommandType = CommandType.StoredProcedure

                                    With cmSP
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = True
                                        .Parameters.Add("@TblIDD", SqlDbType.Int).Value = 0
                                        .Parameters.Add("@Status", SqlDbType.VarChar).Value = Me.CBOStatus.EditValue
                                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(y, "Jenis")
                                        .Parameters.Add("@HargaCBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                                        .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.TBHarga.EditValue / Me.SPIsi.EditValue
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.TBOB.EditValue
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

                                            If x < 0 Then
                                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                        Catch ex As Exception
                                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End Try
                                    End With

                                Else
                                    'Set Harga Sesuai TB

                                    Dim cmSP As New SqlCommand("SPUpM_BrgHarga")
                                    cmSP.CommandType = CommandType.StoredProcedure

                                    With cmSP
                                        .Parameters.Add("@Manual", SqlDbType.Bit).Value = True
                                        .Parameters.Add("@TblIDD", SqlDbType.Int).Value = 0
                                        .Parameters.Add("@Status", SqlDbType.VarChar).Value = Me.CBOStatus.EditValue
                                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(y, "Jenis")
                                        .Parameters.Add("@HargaCBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                                        .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.TBHarga.EditValue
                                        .Parameters.Add("@DiscOB", SqlDbType.Decimal).Value = Me.TBOB.EditValue
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

                                            If x < 0 Then
                                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If

                                        Catch ex As Exception
                                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            Exit Sub
                                        End Try
                                    End With
                                End If

                            End If

                        Next

                    End If

                Next
                XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Select

        Me.GridView1.ActiveFilter.Clear()
        LockControl()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub SLUMtUang_Leave(sender As Object, e As EventArgs) Handles SLUMtUang.Leave
        CekCurr()
    End Sub

    Private Sub DTPTanggal_Leave(sender As Object, e As EventArgs) Handles DTPTanggal.Leave
        CekCurr()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Cek") Then
            If Indicator = 100 Then
                If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                    If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue("Tanggal")) Then
                        If Me.DTPTanggal.EditValue < Me.GridView1.GetFocusedRowCellValue("Tanggal") Then
                            Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", False)

                            XtraMessageBox.Show("Tanggal Harus Disetting Melebihi Setting Harga Terakhir", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.IsRowVisible(i) Then
                    GridView1.SetRowCellValue(i, "Cek", 0)
                End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.IsRowVisible(i) Then
                    GridView1.SetRowCellValue(i, "Cek", 1)
                End If
            Next
        End If
    End Sub

    Private Sub CBOStatus_EditValueChanged(sender As Object, e As EventArgs) Handles CBOStatus.EditValueChanged
        If Me.CBOStatus.EditValue = "Normal" Then
            Me.TBCBP.Properties.ReadOnly = False
            Me.TBHarga.Properties.ReadOnly = False
            Me.TBOB.Properties.ReadOnly = True

        ElseIf Me.CBOStatus.EditValue = "OB" Then
            Me.TBCBP.Properties.ReadOnly = True
            Me.TBHarga.Properties.ReadOnly = True
            Me.TBOB.Properties.ReadOnly = False

        ElseIf Me.CBOStatus.EditValue = "SP" Then
            Me.TBOB.EditValue = 0

            Me.TBCBP.Properties.ReadOnly = True
            Me.TBHarga.Properties.ReadOnly = False
            Me.TBOB.Properties.ReadOnly = True
        End If

        If Indicator = "100" Then
            If Me.CBOStatus.EditValue = "Normal" Then
                Me.TBOB.EditValue = 0.0

            ElseIf Me.CBOStatus.EditValue = "OB" Then
                Me.TBCBP.EditValue = 0.0
                Me.TBHarga.EditValue = 0.0

            ElseIf Me.CBOStatus.EditValue = "SP" Then
                Me.TBOB.EditValue = 0.0
                Me.TBCBP.EditValue = 0.0

            End If
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

    Private Sub TBHarga_KeyDown(sender As Object, e As KeyEventArgs) Handles TBHarga.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBHarga_KeyUp(sender As Object, e As KeyEventArgs) Handles TBHarga.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBOB_KeyDown(sender As Object, e As KeyEventArgs) Handles TBOB.KeyDown
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBOB_KeyUp(sender As Object, e As KeyEventArgs) Handles TBOB.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            e.Handled = True
        End If
    End Sub
End Class