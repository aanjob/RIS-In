Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSJ_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Manual As String
    Dim Gudang, Doc As String
    Dim Tanggal As Date
    Dim Kode, Gd, PromoID, Paket, Metode, JnsCust, JnsPerhit, Kelipatan, BeliMin, JnsPot, Gol, Tampil As String
    Dim Pot, TotPot As Integer

    Public Sub CekPromo()
        Dim Reader2 As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select JnsPerhit,Kelipatan From T_PromoDtl Where PromoID='" & PromoID & "' and Paket = '" & Paket & "' and JnsCust In ('" & JnsCust & "','%') ", koneksi)

        With koneksi
            .Open()
            Reader2 = command.ExecuteReader

            If Reader2.HasRows Then
                While Reader2.Read
                    If IsDBNull(Reader2.Item(0)) = True Then
                        JnsPerhit = ""
                        Kelipatan = False
                    Else
                        JnsPerhit = Reader2.Item(0)
                        Kelipatan = Reader2.Item(1)
                    End If
                End While
            End If
            Reader2.Close()
            .Close()
        End With

        If PromoID <> "" Then
            If Metode = "Gratis Barang" Then
                Dim cmSl As New SqlCommand("Select JnsPot,Pot,BeliMin From T_PromoDtl where PromoID= '" & PromoID & "' and Paket ='" & Paket & "' and JnsCust In ('" & JnsCust & "','%') and (BeliMin <= " & CType(Me.GridView1.Columns("Qty").SummaryText, Integer) & ") AND (BeliMax >= '" & CType(Me.GridView1.Columns("Qty").SummaryText, Integer) & "') ")

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
                                JnsPot = Reader.Item(0)
                                Pot = Reader.Item(1)
                                BeliMin = Reader.Item(2)
                            End While
                        Else
                            JnsPot = ""
                            Pot = 0
                            BeliMin = 0
                        End If
                        Reader.Close()

                        .Close()
                    End With
                End With

                If Kelipatan = False Then
                    TotPot = Pot
                Else
                    If CType(Me.GridView1.Columns("Qty").SummaryText, Integer) > 0 Then
                        TotPot = Pot * (CType(Me.GridView1.Columns("Qty").SummaryText, Integer) \ BeliMin)
                    Else
                        TotPot = 0
                    End If
                End If

                Me.ESINote.Text = "Customer Berhak Mendapatkan Gratis Barang Sebanyak : " & TotPot
            End If
        End If

    End Sub

    Public Sub New(ByVal JenisCust As String, Gol As String, Gd As String, Tgl As Date, DocID As String, Manuall As Boolean, Promo As String, Metod As String, Pakett As String, Ditampilkan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        Dim cmsl As SqlDataAdapter
        DsAddDt = New System.Data.DataSet

        Manual = Manuall
        Gudang = Gd
        Tanggal = Tgl
        Doc = DocID
        Paket = Pakett
        Metode = Metod
        JnsCust = JenisCust
        Tampil = Ditampilkan

        If Tampil = "Semua" Then
            Me.LCIBarang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LCIBarang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Manual = True Then
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,'' as stsHarga,0.00 As Harga,0.000 as DiscOB,0 As RpDiscL,0 As Stok,0 As Qty,0 As Selisih,Gol From M_Brg B Where B.Aktif='True' and Gol In ('" & Gol & "','Promosi')", koneksi)

            cmsl.TableMappings.Add("Table", "Harga" & Gol)
            cmsl.Fill(DsAddDt, "Harga" & Gol)

            Me.GridColumn5.OptionsColumn.AllowEdit = True
            Me.GridColumn7.OptionsColumn.AllowEdit = True

        Else

            If Promo = "" Then
                cmsl = New SqlDataAdapter("Select * From (Select B.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,0 As RpDiscL,0 As Qty,0 As Selisih,(Select Isnull((Select Sum(Masuk)-Sum(Keluar) From T_StokBJ Where ArtCode=B.ArtCode and GdID='" & Gd & "' and Tanggal <='" & Tgl & "'),0))-(Select Isnull((Select Sum(Keluar) From T_TempStokBJ Where ArtCode=B.ArtCode and GdID='" & Gd & "' and Tanggal <='" & Tgl & "'),0)) As Stok,Gol From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True') As x Where Stok >0", koneksi)

                cmsl.TableMappings.Add("Table", "Harga" & Gol)
                cmsl.SelectCommand.CommandTimeout = 90000
                cmsl.Fill(DsAddDt, "Harga" & Gol)

            Else
                If Metode = "Extra Harga Lama" Then
                    cmsl = New SqlDataAdapter("Select Distinct D.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,0 As RpDiscL,Harga-(Select Isnull(((Select Top 1 Harga From M_BrgHarga Where Jenis='" & JnsCust & "' and Gol='" & Gol & "' and ArtCode=D.ArtCode and Tanggal <H.Tanggal Order By Tanggal desc)),Harga)) As Selisih,0 As Qty,Gol From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and PromoID='" & Promo & "' and Paket='" & Paket & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True'", koneksi)

                    cmsl.TableMappings.Add("Table", "Harga" & Gol)
                    cmsl.Fill(DsAddDt, "Harga" & Gol)

                Else
                    cmsl = New SqlDataAdapter("Select Distinct D.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga,DiscOB,0 As RpDiscL,0 As Qty,0 As Selisih,Gol From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and PromoID='" & Promo & "' and Paket='" & Paket & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True'", koneksi)

                    cmsl.TableMappings.Add("Table", "Harga" & Gol)
                    cmsl.Fill(DsAddDt, "Harga" & Gol)
                End If

                If Metode = "Gratis Barang" Then
                    Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    cmsl = New SqlDataAdapter("Select Distinct D.ArtCode,ArtName,B.SatID,B.Isi,0 As Qty,Gol From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where PromoID='" & Promo & "' and Paket='" & Paket & "' and B.Aktif='True'", koneksi)

                    cmsl.TableMappings.Add("Table", "T_PromoFreeJ" & Gol)
                    cmsl.Fill(DsAddDt, "T_PromoFreeJ" & Gol)

                    Me.GridControl2.DataSource = DsAddDt
                    Me.GridControl2.DataMember = "T_PromoFreeJ" & Gol

                    Me.ESINote.Text = "Customer Berhak Mendapatkan Gratis Barang Sebanyak : 0"

                Else
                    Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If

            'Me.GridColumn2.OptionsColumn.AllowEdit = False
            Me.GridColumn5.OptionsColumn.AllowEdit = False
            Me.GridColumn7.OptionsColumn.AllowEdit = False
        End If

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "Harga" & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView2.ActiveFilter.Clear()

        CekPromo()

        Dim x As Integer = 0


        If Metode = "Gratis Barang" Then
            If Tampil = "Semua" Then
                If CType(Me.GridView2.Columns("Qty").SummaryText, Decimal) <> TotPot Then
                    XtraMessageBox.Show("Jumlah Barang Gratis Tidak Sesuai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
        End If

        dataTrans = New Collection
        dataTrans.Clear()

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                'DtTrans.Rows.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), Me.GridView1.GetRowCellValue(i, "ArtName"), Me.GridView1.GetRowCellValue(i, "SatID"), Me.GridView1.GetRowCellValue(i, "Isi"), Me.GridView1.GetRowCellValue(i, "stsHarga"), Me.GridView1.GetRowCellValue(i, "Harga"), Me.GridView1.GetRowCellValue(i, "DiscOB"), Me.GridView1.GetRowCellValue(i, "Qty"))
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "stsHarga"), "stsHarga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Selisih"), "Selisih" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Harga"), "Harga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscOB"), "DiscOB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Gol"), "Gol" & x)
                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        dataTrans2 = New Collection
        dataTrans2.Clear()

        Dim y As Integer = 0
        Dim z : For z = 0 To GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(z, "Qty") > 0 Then
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtCode"), "ArtCode" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtName"), "ArtName" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "SatID"), "SatID" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Isi"), "Isi" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Qty"), "Qty" & y)
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
            If Manual = False Then

                Dim Stok As Integer
                Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Gudang & "','" & Tanggal & "','" & Doc & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                'Me.GridView1.SetRowCellValue(e.RowHandle, "Stok", Stok)

                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If
            End If

            CekPromo()

        End If
    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        If e.Column Is GridView2.Columns("Qty") Then
            If Manual = False Then
                Dim Stok As Integer
                Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & Gudang & "','" & Tanggal & "','" & Doc & "')", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                'Me.GridView2.SetRowCellValue(e.RowHandle, "Stok", Stok)

                If Me.GridView2.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView2.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If
            End If

            CekPromo()

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

    Private Sub GridView1_LostFocus(sender As Object, e As EventArgs) Handles GridView1.LostFocus
        CekPromo()
    End Sub
End Class