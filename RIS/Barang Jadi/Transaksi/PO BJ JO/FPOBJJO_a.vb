Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls

Public Class FPOBJJO_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16

    Public Sub New(Grup As String, MtUang As String, JnsHarga As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select B.StyleID,B.ArtCode,ArtName,W.Nama As Warna,Ass As Uk,B.SatID,B.Isi,0.0 As Qty,0.0 As QtyPol,0.0 As Psg,0.0 As PsgPol,Harga As HarSat,HargaCBP As HCBP,0.0 As HarAkhir From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where B.Aktif='True' and H.Aktif='True' and Grup='" & Grup & "' and Gol='Job Order' and MtUang='" & MtUang & "' and Jenis='" & JnsHarga & "' Order By ArtName,Warna,Cast(dbo.fcRmvNonNumeric(Ass) as decimal)", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgPO")
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "M_BrgPO")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "M_BrgPO"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "Nama" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Warna"), "Warna" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Uk"), "Uk" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HarSat"), "HarSat" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HCBP"), "HCBP" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "QtyPol"), "QtyPol" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Psg"), "Psg" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "PsgPol"), "PsgPol" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HarAkhir"), "HarAkhir" & x)

                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Qty") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "Psg", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))

        ElseIf e.Column Is GridView1.Columns("QtyPol") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "PsgPol", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPol") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi"))

        ElseIf e.Column Is GridView1.Columns("HarSat") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "HarSat"), 2))
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            dataTrans2 = New Collection
            dataTrans2.Clear()

            Timer1.Enabled = True
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

End Class