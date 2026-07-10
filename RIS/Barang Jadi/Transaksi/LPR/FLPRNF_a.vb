Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FLPRNF_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim OngkirSatuan As Decimal

    Public Sub New(ByVal JnsCust As String, Gol As String, Grup As String, Gudang As String, Tgl As Date, DocID As String, OngkirSat As Decimal)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        If Gol = "Job Order" Then
            Me.GridColumn5.OptionsColumn.AllowEdit = True
            Me.GridColumn7.OptionsColumn.AllowEdit = True
            Me.GridColumn10.OptionsColumn.AllowEdit = True
        Else
            Me.GridColumn5.OptionsColumn.AllowEdit = False
            Me.GridColumn7.OptionsColumn.AllowEdit = False
            Me.GridColumn10.OptionsColumn.AllowEdit = False
        End If

        OngkirSatuan = OngkirSat

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select *,0 As Qty from(Select B.ArtCode, ArtName, B.SatID, B.Isi, stsHarga,Harga+(" & OngkirSat & "*B.Isi) As Harga,Harga As HarDPJ, DiscOB,0 As RpDiscL From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and Gol In ('" & Gol & "','Promosi') and Grup='" & Grup & "' and B.Aktif='True' and H.Aktif='True') As x", koneksi)

        cmsl.TableMappings.Add("Table", "Barang" & Grup)
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "Barang" & Grup)

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "Barang" & Grup

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                dataTrans.Add("--", "JualID" & x)
                dataTrans.Add(0, "JualIDD" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "IsiDlmDos" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "IsiAsDos" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "stsHarga"), "stsHarga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HarDPJ"), "HarDPJ" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Harga"), "Harga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscOB"), "DiscOB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "RpDiscL"), "RpDiscL" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(0, "DiscGlbSat" & x)
                dataTrans.Add(0, "RpDiscGlb" & x)

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

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Harga") Then
            Me.GridView1.SetRowCellValue(e.RowHandle, "HarDPJ", e.Value - (OngkirSatuan * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi")))
        End If
    End Sub
End Class