Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Public Class FPOBJJO_r
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Kode As String
    Dim stsK1, stsK2, stsJ1, stsJ2, stsPPn As Boolean
    Dim CurrID, JnsPPn As String
    Dim NilTukarRp As Decimal
    Dim Tgl As Date

    Public Sub CekCurr()
        Dim cmSl As New SqlCommand("SELECT TOP (1) CurrID, NilTukarRp From M_Curr Where (Awal <= '" & Tgl & "') AND (Akhir >= '" & Tgl & "') AND (MtUang = '" & Me.SLUMtUang.EditValue & "')ORDER BY Tanggal DESC")
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

    Public Sub HitPPn()
        Me.TBTotAkhir.EditValue = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)

        If Me.RBPPn.EditValue = "Non PPn" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = 0

        ElseIf Me.RBPPn.EditValue = "Include" Then
            Me.TBTotDPP.EditValue = Math.Round(Me.TBTotAkhir.EditValue / 1.1, 2, MidpointRounding.AwayFromZero)
            'Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * 10 / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotAkhir.EditValue / 1.1 * 10 / 100, 2, MidpointRounding.AwayFromZero)

        ElseIf Me.RBPPn.EditValue = "Exclude" Then
            Me.TBTotDPP.EditValue = Me.TBTotAkhir.EditValue
            Me.TBTotPPn.EditValue = Math.Round(Me.TBTotDPP.EditValue * 10 / 100, 2, MidpointRounding.AwayFromZero)
            Me.TBTotAkhir.EditValue = Math.Round(CType(Me.GridView1.Columns("HarAkhir").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero) + Me.TBTotPPn.EditValue
        End If
    End Sub

    Public Sub New(KodeDoc As String, TglKR1 As String, TglJTR1 As String, K1 As Boolean, K2 As Boolean, J1 As Boolean, J2 As Boolean, Tanggal As Date, JnsPPn As String, KdCurr As String, MtUang As String, NilTukar As Decimal, POCust As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Kode = KodeDoc

        If TglKR1 <> "Kosong" Then
            Me.DTPKirimR1.EditValue = CDate(TglKR1)
        End If

        If TglJTR1 <> "Kosong" Then
            Me.DTPJTR1.EditValue = CDate(TglJTR1)
        End If

        stsK1 = K1
        stsK2 = K2
        stsJ1 = J1
        stsJ2 = J2

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"

        CurrID = KdCurr
        Me.SLUMtUang.EditValue = MtUang
        NilTukarRp = NilTukar
        Me.RBPPn.EditValue = JnsPPn
        Tgl = Tanggal
        Me.TBPOCust.EditValue = POCust

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POIDD,POID,Project,D.ArtCode,ArtName,W.Nama As Warna,D.Uk,D.SatID,D.Isi,Qty,QtyPol,Psg,PsgPol,HarSat, HCBP,HarAkhir,BtlProd,BtlOrder,SisaKirim,stsKirim,SisaProd,stsProd From T_POBJJODtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJJODtl")
        Try
            DsMaster.Tables("T_POBJJODtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJJODtl")

        DsMaster.Tables("T_POBJJODtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJJODtl").Columns("Project"), DsMaster.Tables("T_POBJJODtl").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBJJODtl"
    End Sub

    Private Sub FPOBJJO_r_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.CETglKirim.EditValue = False Then
            Me.DTPKirimR1.Properties.ReadOnly = True
            Me.DTPKirimR2.Properties.ReadOnly = True
        Else
            Me.DTPKirimR1.Properties.ReadOnly = stsK1
            Me.DTPKirimR2.Properties.ReadOnly = stsK2
        End If

        If Me.CETglJT.EditValue = False Then
            Me.DTPJTR1.Properties.ReadOnly = True
            Me.DTPJTR2.Properties.ReadOnly = True
        Else
            Me.DTPJTR1.Properties.ReadOnly = stsJ1
            Me.DTPJTR2.Properties.ReadOnly = stsJ2
        End If

        If Me.CERevHarga.EditValue = False Then
            Me.TBPOCust.Properties.ReadOnly = True
            Me.SLUMtUang.Properties.ReadOnly = True
            Me.RBPPn.Properties.ReadOnly = True

            Me.GridColumn8.OptionsColumn.AllowEdit = False
            Me.GridColumn47.OptionsColumn.AllowEdit = False

        Else
            Me.TBPOCust.Properties.ReadOnly = False
            Me.SLUMtUang.Properties.ReadOnly = False
            Me.RBPPn.Properties.ReadOnly = False

            Me.GridColumn8.OptionsColumn.AllowEdit = True
            Me.GridColumn47.OptionsColumn.AllowEdit = True
        End If

        FillDtl(Kode)
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.RefreshData()
        Me.GridView1.ActiveFilter.Clear()

        Dim cmSP As New SqlCommand("SPUpT_POBJJORvs")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
            .Parameters.Add("@stsK1", SqlDbType.Bit).Value = stsK1
            .Parameters.Add("@stsK2", SqlDbType.Bit).Value = stsK2
            .Parameters.Add("@stsJ1", SqlDbType.Bit).Value = stsJ1
            .Parameters.Add("@stsJ2", SqlDbType.Bit).Value = stsJ2
            .Parameters.Add("@stsK", SqlDbType.Bit).Value = Me.CETglKirim.EditValue
            .Parameters.Add("@stsJ", SqlDbType.Bit).Value = Me.CETglJT.EditValue
            .Parameters.Add("@TglKirimR1", SqlDbType.Date).Value = Me.DTPKirimR1.EditValue
            .Parameters.Add("@TglKirimR2", SqlDbType.Date).Value = Me.DTPKirimR2.EditValue
            .Parameters.Add("@TglJTR1", SqlDbType.Date).Value = Me.DTPJTR1.EditValue
            .Parameters.Add("@TglJTR2", SqlDbType.Date).Value = Me.DTPJTR2.EditValue
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

                If Me.CERevHarga.EditValue = True Then
                    HitPPn()

                    If Me.RBPPn.EditValue <> "Non PPn" Then
                        stsPPn = True
                        JnsPPn = "PPn"
                    Else
                        stsPPn = False
                        JnsPPn = "Non PPn"
                    End If

                    Dim cmSPH As New SqlCommand("SPUpT_POBJJORvsH")
                    cmSPH.CommandType = CommandType.StoredProcedure

                    With cmSPH
                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                        .Parameters.Add("@POCust", SqlDbType.VarChar).Value = Me.TBPOCust.EditValue
                        .Parameters.Add("@MtUang", SqlDbType.VarChar).Value = Me.SLUMtUang.EditValue
                        .Parameters.Add("@CurrID", SqlDbType.VarChar).Value = CurrID
                        .Parameters.Add("@NilTukarRp", SqlDbType.Decimal).Value = Me.TBNilTukarRp.EditValue
                        .Parameters.Add("@TipePPn", SqlDbType.VarChar).Value = Me.RBPPn.EditValue
                        .Parameters.Add("@TotDPP", SqlDbType.Decimal).Value = Me.TBTotDPP.EditValue
                        .Parameters.Add("@TotPPn", SqlDbType.Decimal).Value = Me.TBTotPPn.EditValue
                        .Parameters.Add("@TotAkhir", SqlDbType.Decimal).Value = Me.TBTotAkhir.EditValue
                        .Parameters.Add("@PPn", SqlDbType.Bit).Value = stsPPn
                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                        .Parameters.Add("@Return", SqlDbType.Int)
                        .Parameters("@Return").Direction = ParameterDirection.Output
                        .Connection = koneksi

                        With koneksi
                            .Open()
                            cmSPH.ExecuteNonQuery()
                            x = cmSPH.Parameters("@Return").Value
                            .Close()
                        End With
                    End With

                    Dim i : For i = 0 To GridView1.RowCount - 1
                        If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                            Dim cmSPDtl As New SqlCommand("SPUpT_POBJJODtl")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                                .Parameters.Add("@Project", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Project")
                                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SatID")
                                .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Isi")
                                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                .Parameters.Add("@Psg", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Psg")
                                .Parameters.Add("@PsgPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PsgPol")
                                .Parameters.Add("@HarSat", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarSat")
                                .Parameters.Add("@HarAkhir", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HarAkhir")
                                .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "HCBP")
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
                        End If
                    Next

                End If

                If x = 0 Then
                    XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()

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
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Close()
    End Sub

    Private Sub CETglKirim_EditValueChanged(sender As Object, e As EventArgs) Handles CETglKirim.EditValueChanged
        If Me.CETglKirim.EditValue = False Then
            Me.DTPKirimR1.Properties.ReadOnly = True
            Me.DTPKirimR2.Properties.ReadOnly = True
        Else
            Me.DTPKirimR1.Properties.ReadOnly = stsK1
            Me.DTPKirimR2.Properties.ReadOnly = stsK2
        End If
    End Sub

    Private Sub CETglJT_EditValueChanged(sender As Object, e As EventArgs) Handles CETglJT.EditValueChanged
        If Me.CETglJT.EditValue = False Then
            Me.DTPJTR1.Properties.ReadOnly = True
            Me.DTPJTR2.Properties.ReadOnly = True
        Else
            Me.DTPJTR1.Properties.ReadOnly = stsJ1
            Me.DTPJTR2.Properties.ReadOnly = stsJ2
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("HarSat") Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty")) And Not IsDBNull(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol")) * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"))
            End If

        End If

    End Sub

    Private Sub GridView1_ValidateRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs) Handles GridView1.ValidateRow
        Dim View As GridView = CType(sender, GridView)
        Dim HarSatCol As GridColumn = View.Columns("HarSat")
        Dim CBPCol As GridColumn = View.Columns("HCBP")

        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat") = 0 Then
            e.Valid = False
            View.SetColumnError(HarSatCol, "Harga Harus Diisi")
        End If

        If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > 0 And Me.GridView1.GetRowCellValue(e.RowHandle, "HCBP") = 0 Then
            e.Valid = False
            View.SetColumnError(CBPCol, "Harga CBP Harus Diisi")
        End If
    End Sub

    Private Sub GridView1_InvalidRowException(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles GridView1.InvalidRowException
        'Suppress displaying the error message box
        e.ExceptionMode = ExceptionMode.NoAction
    End Sub

    Private Sub CERevHarga_EditValueChanged(sender As Object, e As EventArgs) Handles CERevHarga.EditValueChanged
        If Me.CERevHarga.EditValue = False Then
            Me.TBPOCust.Properties.ReadOnly = True
            Me.SLUMtUang.Properties.ReadOnly = True
            Me.RBPPn.Properties.ReadOnly = True

            Me.GridColumn8.OptionsColumn.AllowEdit = False
            Me.GridColumn47.OptionsColumn.AllowEdit = False

        Else
            Me.TBPOCust.Properties.ReadOnly = False
            Me.SLUMtUang.Properties.ReadOnly = False
            Me.RBPPn.Properties.ReadOnly = False

            Me.GridColumn8.OptionsColumn.AllowEdit = True
            Me.GridColumn47.OptionsColumn.AllowEdit = True
        End If
    End Sub

    Private Function DTPTanggal() As Object
        Throw New NotImplementedException
    End Function

End Class