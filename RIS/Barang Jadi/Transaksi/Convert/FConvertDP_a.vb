Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FConvertDP_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim SplitKdD(), SplitKdP() As String
    Dim NoAssD, NoAssP, Gd, NoDok As String
    Dim Dt As Integer
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
        cmsl = New SqlDataAdapter("Select * From (Select ArtCode,ArtName,AssID,SatID,Isi,1 As IsiDlmDos,0 As Qty,0 as Dos, 0 as Psg,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=M_Brg.ArtCode and GdID='" & Gd & "' and Tanggal <='" & Tgl & "'),0)) As Stok From M_Brg Where Aktif='True' and SatID like 'D%' and Gol='" & Gol & "') As x Where Stok >0", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgCv")
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "M_BrgCv")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "M_BrgCv"

        DtTrans = New System.Data.DataTable
        DtTrans.Columns.Add("ArtCodeD")
        DtTrans.Columns.Add("ArtCode")
        DtTrans.Columns.Add("ArtName")
        DtTrans.Columns.Add("SatID")
        DtTrans.Columns.Add("Isi")
        DtTrans.Columns.Add("IsiDlmDos")
        DtTrans.Columns.Add("Qty")
        DtTrans.Columns.Add("Dos")
        DtTrans.Columns.Add("Psg")


        Me.GridControl2.DataSource = DtTrans

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub FillPsg()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Uk,Qty From M_BrgAssDtl Where AssID='" & Me.GridView1.GetFocusedDataRow.Item("AssID") & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgAssDtlCv")
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "M_BrgAssDtlCv")

        Dim i : For i = 0 To DsAddDt.Tables("M_BrgAssDtlCv").Rows.Count - 1
            DtTrans.Rows.Add(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), NoAssD + DsAddDt.Tables("M_BrgAssDtlCv").Rows(i).Item("Uk"), Me.GridView1.GetFocusedDataRow.Item("ArtName"), "P", Me.GridView1.GetFocusedDataRow.Item("Isi"), DsAddDt.Tables("M_BrgAssDtlCv").Rows(i).Item("Qty"), DsAddDt.Tables("M_BrgAssDtlCv").Rows(i).Item("Qty") * Me.GridView1.GetFocusedDataRow.Item("Qty"), 0, DsAddDt.Tables("M_BrgAssDtlCv").Rows(i).Item("Qty") * Me.GridView1.GetFocusedDataRow.Item("Qty"))
        Next
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView2.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        dataTrans2 = New Collection
        dataTrans2.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCodeD" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(1, "IsiDlmDos" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Dos"), "Dos" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Psg"), "Psg" & x)

                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        Dim y As Integer = 0
        Dim z : For z = 0 To Me.GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(z, "Qty") > 0 Then
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtCodeD"), "ArtCodeD" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtCode"), "ArtCode" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtName"), "ArtName" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "SatID"), "SatID" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Isi"), "Isi" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "IsiDlmDos"), "IsiDlmDos" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Qty"), "Qty" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Dos"), "Dos" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Psg"), "Psg" & y)

                y += 1
            End If
        Next
        dataTrans2.Add(y, "Baris")

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
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 0)
            Else
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Dos", Me.GridView1.GetFocusedDataRow.Item("Qty"))
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Psg", Me.GridView1.GetFocusedDataRow.Item("Qty") * Me.GridView1.GetFocusedDataRow.Item("Isi"))

                SplitKdD = CType(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), String).Split("-")
                NoAssD = Me.GridView1.GetFocusedDataRow.Item("ArtCode").ToString.Remove(Me.GridView1.GetFocusedDataRow.Item("ArtCode").ToString.Length - (SplitKdD(3).Length), SplitKdD(3).Length)

                Dim St As String = ""

                If Me.GridView2.RowCount - 1 > 0 Then
                    Dim x : For x = 0 To Me.GridView2.RowCount - 1
                        SplitKdP = CType(Me.GridView2.GetRowCellValue(x, "ArtCode"), String).Split("-")
                        NoAssP = Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Remove(Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Length - (SplitKdP(3).Length), SplitKdP(3).Length)

                        'If NoAssD = NoAssP Then
                        If Me.GridView2.GetRowCellValue(x, "ArtCodeD") = Me.GridView1.GetFocusedDataRow.Item("ArtCode") Then
                            St = "Ada"
                            Exit For
                        Else
                            St = "Tidak"
                        End If

                    Next

                    If St = "Tidak" Then
                        FillPsg()
                    Else
                        Dim i : For i = Me.GridView2.RowCount - 1 To 0 Step -1
                            SplitKdP = CType(Me.GridView2.GetRowCellValue(i, "ArtCode"), String).Split("-")
                            NoAssP = Me.GridView2.GetRowCellValue(i, "ArtCode").ToString.Remove(Me.GridView2.GetRowCellValue(i, "ArtCode").ToString.Length - (SplitKdP(3).Length), SplitKdP(3).Length)

                            'If NoAssD = NoAssP Then
                            If Me.GridView2.GetRowCellValue(i, "ArtCodeD") = Me.GridView1.GetFocusedDataRow.Item("ArtCode") Then

                                Me.GridView2.SetRowCellValue(i, "Qty", Me.GridView1.GetFocusedDataRow.Item("Qty") * (Me.GridView2.GetRowCellValue(i, "IsiDlmDos")))
                                Me.GridView2.SetRowCellValue(i, "Psg", Me.GridView1.GetFocusedDataRow.Item("Qty") * (Me.GridView2.GetRowCellValue(i, "IsiDlmDos")))
                            End If
                        Next
                        'FillPsg()
                    End If

                Else
                    FillPsg()
                End If

            End If
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
End Class