Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FDOBJ_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Gudang, Doc As String
    Dim Tanggal As Date

    Public Sub New(Gd As String, Tgl As Date, DocID As String, Gol As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Gudang = Gd
        Tanggal = Tgl
        Doc = DocID

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select * From (Select B.ArtCode,ArtName,B.SatID,B.Isi,0 As Qty,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=B.ArtCode and GdID='" & Gd & "' and Tanggal <='" & Tgl & "'),0)) As Stok  From M_Brg B Where B.Aktif='True' and Gol In ('" & Gol & "','Promosi')) As x Where Stok >0", koneksi)

        cmsl.TableMappings.Add("Table", "BrgStok")
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "BrgStok")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "BrgStok"

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
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
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

            Dim Stok As Integer
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Gudang & "','" & Tanggal & "','" & Doc & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
            End If
        End If

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            Timer1.Enabled = True
        End If
    End Sub
End Class