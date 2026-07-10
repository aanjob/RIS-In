Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FNotPes_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Manual, Kode, GdCab, GdPst, PromoID, Paket, Metode, JnsCust, JnsPerhit, Kelipatan, BeliMin, JnsPot, Gol, CustID As String
    Dim Tanggal As Date
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
                Dim cmSl As New SqlCommand("Select JnsPot,Pot,BeliMin From T_PromoDtl where PromoID= '" & PromoID & "' and Paket ='" & Paket & "' and JnsCust In ('" & JnsCust & "','%') and (BeliMin <= " & CType(Me.GridView1.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView1.Columns("QtyPst").SummaryText, Integer) & ") AND (BeliMax >= '" & CType(Me.GridView1.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView1.Columns("QtyPst").SummaryText, Integer) & "') ")

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
                    If BeliMin > 0 Then
                        If (CType(Me.GridView1.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView1.Columns("QtyPst").SummaryText, Integer)) > 0 Then
                            TotPot = Pot * (CType(Me.GridView1.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView1.Columns("QtyPst").SummaryText, Integer) \ BeliMin)
                        Else
                            TotPot = 0
                        End If
                    End If
                End If

                Me.ESINote.Text = "Customer Berhak Mendapatkan Gratis Barang Sebanyak : " & TotPot
            End If
        End If

    End Sub

    Public Sub New(ByVal KodeAktif As String, ByVal JnsCust As String, GdIDCab As String, GdIDPst As String, Gol As String, Tgl As Date, Manuall As Boolean, Promo As String, Metod As String, Pakett As String, JenisCust As String, KodeCust As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        Dim cmsl As SqlDataAdapter
        DsAddDt = New System.Data.DataSet

        Kode = KodeAktif
        Manual = Manuall
        Tanggal = Tgl
        GdCab = GdIDCab
        GdPst = GdIDPst
        PromoID = Promo
        Paket = Pakett
        Metode = Metod
        JnsCust = JenisCust
        CustID = KodeCust

        If Manual = True Then
            cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,B.SatID,B.Isi,'' as stsHarga,0.00 As Harga,0.000 as DiscOB,0 As Qty,0 As QtyCab, 0 As QtyPst,0 As Selisih,0 As SelisihPst,Gol From M_Brg B Where B.Aktif='True' and Gol In ('" & Gol & "','Promosi')", koneksi)

            cmsl.TableMappings.Add("Table", "Harga" & Gol)
            cmsl.Fill(DsAddDt, "Harga" & Gol)

            'Me.GridColumn2.OptionsColumn.AllowEdit = True
            Me.GridColumn5.OptionsColumn.AllowEdit = True
            Me.GridColumn7.OptionsColumn.AllowEdit = True

        Else

            If PromoID = "" Then
                'MsgBox("Masuk 1 OK")
                cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY B.ArtCode)*-1 as Row,B.ArtCode,ArtName,B.SatID,B.Isi,stsHarga,Harga, DiscOB,0 As Qty,0 As QtyCab,0 As QtyPst,0 As Selisih,0 As SelisihPst,Gol From M_Brg B Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True'  Order By B.ArtCode Asc", koneksi)

                cmsl.TableMappings.Add("Table", "Harga" & Gol)
                cmsl.SelectCommand.CommandTimeout = 90000
                cmsl.Fill(DsAddDt, "Harga" & Gol)

            Else
                'MsgBox("Masuk 2 OK")
                If Metode = "Extra Harga Lama" Then
                    'MsgBox("Masuk 3")
                    cmsl = New SqlDataAdapter("Select Distinct ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as Row, D.ArtCode,ArtName,B.SatID,B.Isi, stsHarga,Harga,DiscOB,Harga-(Select Isnull(((Select Top 1 Harga From M_BrgHarga Where Jenis='" & JnsCust & "' and Gol='" & Gol & "' and ArtCode=D.ArtCode and Tanggal <H.Tanggal Order By Tanggal desc)),Harga)) As Selisih,Harga-(Select Isnull(((Select Top 1 Harga From M_BrgHarga Where Jenis='" & JnsCust & "' and Gol='" & Gol & "' and ArtCode=D.ArtCode and Tanggal <H.Tanggal Order By Tanggal desc)),Harga)) As SelisihPst,0 As Qty,0 As QtyCab,0 As QtyPst,Gol From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and PromoID='" & Promo & "' and Paket='" & Paket & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True'  Order By D.ArtCode Asc", koneksi)

                    cmsl.TableMappings.Add("Table", "Harga" & Gol)
                    cmsl.Fill(DsAddDt, "Harga" & Gol)

                Else
                    'MsgBox("Masuk 3")
                    cmsl = New SqlDataAdapter("Select Distinct ROW_NUMBER() over (ORDER BY D.ArtCode)*-1 as Row,D.ArtCode,ArtName,B.SatID,B.Isi, stsHarga,Harga,DiscOB,0 As Qty,0 As QtyCab,0 As QtyPst,0 As Selisih,0 As SelisihPst,Gol From T_PromoDtl2 D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgHarga H On B.ArtCode=H.ArtCode Where Jenis='" & JnsCust & "' and PromoID='" & Promo & "' and Paket='" & Paket & "' and Gol In ('" & Gol & "','Promosi') and B.Aktif='True' and H.Aktif='True' Order By D.ArtCode Asc", koneksi)

                    cmsl.TableMappings.Add("Table", "Harga" & Gol)
                    cmsl.Fill(DsAddDt, "Harga" & Gol)
                End If

                If Metode = "Gratis Barang" Then
                    Me.LCIFree.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    cmsl = New SqlDataAdapter("Select Distinct ROW_NUMBER() over (ORDER BY D.ArtCode) as Row,D.ArtCode,ArtName,B.SatID,B.Isi,0 As Qty,0 As QtyCab,0 As QtyPst,Gol From T_PromoFree D Inner Join M_Brg B On D.ArtCode=B.ArtCode  Where PromoID='" & Promo & "' and Paket='" & Paket & "' and B.Aktif='True' Order By D.ArtCode Asc", koneksi)

                    cmsl.TableMappings.Add("Table", "T_PromoFreeJ" & Gol)
                    cmsl.Fill(DsAddDt, "T_PromoFreeJ" & Gol)

                    Me.GridControl2.DataSource = DsAddDt
                    Me.GridControl2.DataMember = "T_PromoFreeJ" & Gol

                    Me.ESINote.Text = "Customer Berhak Mendapatkan Gratis Barang Sebanyak : 0"

                    If GdCab = "" Then
                        Me.GCQtyCab.OptionsColumn.AllowEdit = False
                        Me.GCQtyPst.OptionsColumn.AllowEdit = True

                        Me.GCQtyCab.Visible = False
                        Me.GCQtyPst.Visible = True
                    Else
                        Me.GCQtyCab.OptionsColumn.AllowEdit = True
                        Me.GCQtyPst.OptionsColumn.AllowEdit = False

                        Me.GCQtyCab.Visible = True
                        Me.GCQtyPst.Visible = False
                    End If
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
        'DtTrans = New System.Data.DataTable
        'DtTrans.Columns.Add("ArtCode")
        'DtTrans.Columns.Add("ArtName")
        'DtTrans.Columns.Add("SatID")
        'DtTrans.Columns.Add("Isi")
        'DtTrans.Columns.Add("stsHarga")
        'DtTrans.Columns.Add("Harga")
        'DtTrans.Columns.Add("DiscOB")
        'DtTrans.Columns.Add("Qty")

        Me.GridView1.ActiveFilter.Clear()
        Me.GridView2.ActiveFilter.Clear()

        CekPromo()

        If Metode = "Gratis Barang" Then
            If CType(Me.GridView2.Columns("QtyCab").SummaryText, Integer) + CType(Me.GridView2.Columns("QtyPst").SummaryText, Integer) <> TotPot Then
                XtraMessageBox.Show("Jumlah Barang Gratis Tidak Sesuai", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            'MsgBox(Me.GridView1.GetRowCellValue(i, "Qty"))
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                'DtTrans.Rows.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), Me.GridView1.GetRowCellValue(i, "ArtName"), Me.GridView1.GetRowCellValue(i, "SatID"), Me.GridView1.GetRowCellValue(i, "Isi"), Me.GridView1.GetRowCellValue(i, "stsHarga"), Me.GridView1.GetRowCellValue(i, "Harga"), Me.GridView1.GetRowCellValue(i, "DiscOB"), Me.GridView1.GetRowCellValue(i, "Qty"))
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Row"), "Row" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "stsHarga"), "stsHarga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Selisih"), "Selisih" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SelisihPst"), "SelisihPst" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Harga"), "Harga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscOB"), "DiscOB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscPst"), "DiscPst" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "QtyCab"), "QtyCab" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "QtyPst"), "QtyPst" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Gol"), "Gol" & x)
                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        dataTrans2 = New Collection
        dataTrans2.Clear()

        Dim y As Integer = 0
        Dim z : For z = 0 To GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(z, "QtyCab") > 0 Or Me.GridView2.GetRowCellValue(z, "QtyPst") > 0 Then
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Row"), "Row" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtCode"), "ArtCode" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "ArtName"), "ArtName" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "SatID"), "SatID" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "Isi"), "Isi" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "QtyCab"), "QtyCab" & y)
                dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "QtyPst"), "QtyPst" & y)
                'dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "DosCab"), "DosCab" & y)
                'dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "DosPst"), "DosPst" & y)
                'dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "PsgCab"), "PsgCab" & y)
                'dataTrans2.Add(Me.GridView2.GetRowCellValue(z, "PsgPst"), "PsgPst" & y)
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
        koneksi.Close()

        Dim StokCab, StokPst, SisaKirim As Integer
        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        If e.Column Is GridView1.Columns("Qty") Then
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & GdCab & "','" & Tanggal & "','" & Kode & "')", koneksi)

            With koneksi
                .Open()
                StokCab = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > StokCab Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", StokCab)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", e.Value)
            End If

            Dim command2 As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & GdPst & "','" & Tanggal & "','" & Kode & "')", koneksi)

            With koneksi
                .Open()
                StokPst = command2.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") > StokPst Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", StokPst)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", e.Value - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Dim command3 As New SqlCommand("Select Isnull(Sum(SisaKirim),0) From T_NotPes H Inner Join T_NotPesDtl D On H.NPID=D.NPID Where CustID='" & CustID & "' And ArtCode='" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "' and SisaKirim>0", koneksi)

            With koneksi
                .Open()
                SisaKirim = command3.ExecuteScalar()
                .Close()
            End With

            RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            'If e.Value > 0 And SisaKirim > 0 Then
            '    If XtraMessageBox.Show("Ada Sisa Kiriman Sebanyak " & SisaKirim & " Yang Belum Dikirim Pada Nota Pesanan Sebelumnya" & vbCrLf & "Apakah Pesanan yang Sekarang Diinput Tetap Diproses?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            '        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", e.Value)
            '    Else

            '        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
            '        Me.GridView1.SetRowCellValue(e.RowHandle, "QtyCab", 0)
            '        Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", 0)
            '    End If
            'End If
            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = GdCab
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Tanggal
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "Row")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab")
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

            Dim cmSPDtl2 As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl2.CommandType = CommandType.StoredProcedure

            With cmSPDtl2
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = GdPst
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Tanggal
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "Row")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst")
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
            End With

            With koneksi
                .Open()
                cmSPDtl2.ExecuteNonQuery()
                x = cmSPDtl2.Parameters("@Return").Value
                .Close()
            End With


        ElseIf e.Column Is GridView1.Columns("QtyCab") Then
            Dim command2 As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & GdPst & "','" & Tanggal & "','" & Kode & "')", koneksi)

            With koneksi
                .Open()
                StokPst = command2.ExecuteScalar()
                .Close()
            End With


            If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab") > StokPst Then
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", StokPst)
            Else
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyPst", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab"))
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = GdCab
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Tanggal
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "Row")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyCab")
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

            Dim cmSPDtl2 As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl2.CommandType = CommandType.StoredProcedure

            With cmSPDtl2
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = GdPst
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Tanggal
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "Row")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(e.RowHandle, "QtyPst")
                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
            End With

            With koneksi
                .Open()
                cmSPDtl2.ExecuteNonQuery()
                x = cmSPDtl2.Parameters("@Return").Value
                .Close()
            End With

        End If

        CekPromo()

        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        If e.Column Is GridView2.Columns("QtyCab") Then
            Dim Stok As Integer
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & GdCab & "','" & Tanggal & "','" & Kode & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView2.GetRowCellValue(e.RowHandle, "QtyCab") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView2.SetRowCellValue(e.RowHandle, "QtyCab", Stok)
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = GdCab
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Tanggal
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(e.RowHandle, "Row")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(e.RowHandle, "QtyCab")
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


        ElseIf e.Column Is GridView2.Columns("QtyPst") Then

            Dim Stok As Integer
            Dim command As New SqlCommand("Select dbo.fcStokBJ('" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "','" & GdPst & "','" & Tanggal & "','" & Kode & "')", koneksi)

            With koneksi
                .Open()
                Stok = command.ExecuteScalar()
                .Close()
            End With

            If Me.GridView2.GetRowCellValue(e.RowHandle, "QtyPst") > Stok Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Stok", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView2.SetRowCellValue(e.RowHandle, "QtyPst", Stok)
            End If

            Dim x As Integer

            Dim cmSPDtl As New SqlCommand("SPInsT_TempStokBJ")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                .Parameters.Add("@TipeDoc", SqlDbType.Int).Value = 1
                .Parameters.Add("@GdID", SqlDbType.VarChar).Value = GdPst
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Kode
                .Parameters.Add("@Tgl", SqlDbType.Date).Value = Tanggal
                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(e.RowHandle, "Row")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode")
                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(e.RowHandle, "QtyPst")
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

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub
End Class