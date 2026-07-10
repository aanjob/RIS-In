Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSisaBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim KdBOM As String
    Dim BSTB As Decimal

    Public Sub New(Kode As String, BatalProd As Boolean, Btl As Boolean, LnsMan As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        KdBOM = Kode
        Me.CEBtlProd.EditValue = BatalProd
        Me.CECancel.EditValue = Btl
        Me.CELunasMan.EditValue = LnsMan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FSisaBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol, Tot,BtlOrder,Upp,Hancur,Hilang,LunasMan From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & KdBOM & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOMPO")
        Try
            DsMaster.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMPO")

        DsMaster.Tables("T_BOMPO").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMPO").Columns("POID"), DsMaster.Tables("T_BOMPO").Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BOMPO"
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        If Me.CECancel.EditValue = True And Math.Round(CType(Me.GridView1.Columns("BtlOrder").SummaryText, Decimal), 1) = 0 Then
            XtraMessageBox.Show("Qty Cancel Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CEBtlProd.EditValue = True And Math.Round(CType(Me.GridView1.Columns("Upp").SummaryText, Decimal), 1) + Math.Round(CType(Me.GridView1.Columns("Hancur").SummaryText, Decimal), 1) + Math.Round(CType(Me.GridView1.Columns("Hilang").SummaryText, Decimal), 1) = 0 Then
            XtraMessageBox.Show("Qty Upper/Hilang/Hancur Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CELunasMan.EditValue = True And Math.Round(CType(Me.GridView1.Columns("LunasMan").SummaryText, Decimal), 1) = 0 Then
            XtraMessageBox.Show("Qty Lunas Manual Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim x As Integer

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                Dim cmSPDtl As New SqlCommand("SPBtlBOM")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMIDD")
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                    .Parameters.Add("@BtlOrder", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "BtlOrder")
                    .Parameters.Add("@LunasMan", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "LunasMan")
                    .Parameters.Add("@Upp", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Upp")
                    .Parameters.Add("@Hancur", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Hancur")
                    .Parameters.Add("@Hilang", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Hilang")
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

        x = 0

        Dim cmSP As New SqlCommand("SPBtlBOM2")
        cmSP.CommandType = CommandType.StoredProcedure

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = KdBOM
            .Parameters.Add("@stsBatal", SqlDbType.Bit).Value = Me.CECancel.EditValue
            .Parameters.Add("@stsBtlProd", SqlDbType.Bit).Value = Me.CEBtlProd.EditValue
            .Parameters.Add("@stsLnsMan", SqlDbType.Bit).Value = Me.CELunasMan.EditValue
            .Parameters.Add("@KetLain2", SqlDbType.VarChar).Value = Me.MKet.EditValue
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
                    XtraMessageBox.Show("SPK/BOM Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Dispose()
                ElseIf x = 1 Then
                    XtraMessageBox.Show("SPK/BOM Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception
                XtraMessageBox.Show("SPK/BOM Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub CECancel_EditValueChanged(sender As Object, e As EventArgs) Handles CECancel.EditValueChanged
        If Me.CECancel.EditValue = True Then
            Me.GCCancel.OptionsColumn.AllowEdit = True

            Me.GCUpp.OptionsColumn.AllowEdit = False
            Me.GCHancur.OptionsColumn.AllowEdit = False
            Me.GCHilang.OptionsColumn.AllowEdit = False
            Me.GCLunasMan.OptionsColumn.AllowEdit = False

        Else
            Me.GCCancel.OptionsColumn.AllowEdit = False
            Me.GCLunasMan.OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub CEBtlProd_EditValueChanged(sender As Object, e As EventArgs) Handles CEBtlProd.EditValueChanged
        If Me.CEBtlProd.EditValue = True Then
            Me.GCUpp.OptionsColumn.AllowEdit = True
            Me.GCHancur.OptionsColumn.AllowEdit = True
            Me.GCHilang.OptionsColumn.AllowEdit = True

            Me.GCCancel.OptionsColumn.AllowEdit = False
            Me.GCLunasMan.OptionsColumn.AllowEdit = False
        Else
            Me.GCUpp.OptionsColumn.AllowEdit = False
            Me.GCHancur.OptionsColumn.AllowEdit = False
            Me.GCHilang.OptionsColumn.AllowEdit = False
            Me.GCLunasMan.OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub CELunasMan_EditValueChanged(sender As Object, e As EventArgs) Handles CELunasMan.EditValueChanged
        If Me.CELunasMan.EditValue = True Then
            Me.GCLunasMan.OptionsColumn.AllowEdit = True

            Me.GCUpp.OptionsColumn.AllowEdit = False
            Me.GCHancur.OptionsColumn.AllowEdit = False
            Me.GCHilang.OptionsColumn.AllowEdit = False
            Me.GCCancel.OptionsColumn.AllowEdit = False
        Else
            Me.GCLunasMan.OptionsColumn.AllowEdit = False

            Me.GCUpp.OptionsColumn.AllowEdit = False
            Me.GCHancur.OptionsColumn.AllowEdit = False
            Me.GCHilang.OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        Dim SisaPsg As Decimal

        If e.Column Is GridView1.Columns("BtlOrder") Then
            Dim command As New SqlCommand("Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & KdBOM & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCodeInd") & "'),0)", koneksi)

            With koneksi
                .Open()
                BSTB = command.ExecuteScalar()
                .Close()
            End With

            SisaPsg = Me.GridView1.GetRowCellValue(e.RowHandle, "Tot") - (BSTB * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") > SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Order BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "BtlOrder", SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang"))
            End If


        ElseIf e.Column Is GridView1.Columns("Upp") Then
            Dim command As New SqlCommand("Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & KdBOM & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCodeInd") & "'),0)", koneksi)

            With koneksi
                .Open()
                BSTB = command.ExecuteScalar()
                .Close()
            End With

            SisaPsg = Me.GridView1.GetRowCellValue(e.RowHandle, "Tot") - (BSTB * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos"))


            If Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") > SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Order BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Upp", SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang"))
            End If

        ElseIf e.Column Is GridView1.Columns("Hancur") Then
            Dim command As New SqlCommand("Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & KdBOM & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCodeInd") & "'),0)", koneksi)

            With koneksi
                .Open()
                BSTB = command.ExecuteScalar()
                .Close()
            End With

            SisaPsg = Me.GridView1.GetRowCellValue(e.RowHandle, "Tot") - (BSTB * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") > SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Order BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Hancur", SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang"))
            End If

        ElseIf e.Column Is GridView1.Columns("Hilang") Then
            Dim command As New SqlCommand("Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID='" & KdBOM & "' and ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCodeInd") & "'),0)", koneksi)

            With koneksi
                .Open()
                BSTB = command.ExecuteScalar()
                .Close()
            End With

            SisaPsg = Me.GridView1.GetRowCellValue(e.RowHandle, "Tot") - (BSTB * Me.GridView1.GetRowCellValue(e.RowHandle, "IsiDlmDos"))

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") > SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Order BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Hilang", SisaPsg - Me.GridView1.GetRowCellValue(e.RowHandle, "BtlOrder") - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur"))
            End If

        End If

    End Sub

End Class