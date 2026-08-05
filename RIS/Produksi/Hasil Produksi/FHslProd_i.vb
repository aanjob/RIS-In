Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FHslProd_i
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Indicator As Int16
    Dim SW As Stopwatch
    Dim Barcode As String

    Public Sub CekJam()
        Me.DTPTanggal.EditValue = Date.Now

        Dim cmSl As New SqlCommand("Select Jam From M_Jam Where '" & Format(Date.Now, "HH:mm:ss") & "'>= JamAw and '" & Format(Date.Now, "HH:mm:ss") & "' <= JamAkh and Proses='" & Me.SLUProses.EditValue & "'")
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
                        Me.SLUJam.EditValue = Reader.Item(0)
                        Me.ESIJam.Text = DsMaster.Tables("M_JamL").Select("Jam = '" & Me.SLUJam.EditValue & "'")(0).Item("JamAw") & " s/d " & DsMaster.Tables("M_JamL").Select("Jam =" & Me.SLUJam.EditValue & "")(0).Item("JamAkh")
                    End While
                End If
                Reader.Close()

                .Close()
            End With
        End With
    End Sub

    Public Sub CekSU()
        Dim frm As New FOpenForm
        frm.ShowDialog()

        If MainModule.SU = True Then
            Me.GridView1.Columns("Cek").Visible = True
            Me.GridView1.Columns("Cek").OptionsColumn.AllowEdit = True

            Me.GridView1.Columns("Cek").VisibleIndex = 7
            Me.GridView1.Columns("Batal").VisibleIndex = 8
            Me.GridView1.Columns("Tot").VisibleIndex = 9

        Else
            Me.GridView1.Columns("Cek").Visible = False
            Me.GridView1.Columns("Cek").OptionsColumn.AllowEdit = False

        End If
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UP.Proses From M_Proses P Inner Join M_UsProses UP On P.Proses=Up.Proses Where Aktif='True' and UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_ProsesL")
        Try
            DsMaster.Tables("M_ProsesL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ProsesL")
        DsMaster.Tables("M_ProsesL").Rows.Add("")

        Me.SLUProses.Properties.DataSource = DsMaster.Tables("M_ProsesL")
        Me.SLUProses.Properties.DisplayMember = "Proses"
        Me.SLUProses.Properties.ValueMember = "Proses"
    End Sub

    Public Sub FillDt(Jam As Integer)
        Dim cmsl As SqlDataAdapter
        'cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,D.*,ArtName,W.Nama As Warna,Ass,SatID,Isi,Batal As BtlBef From T_HslProdDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where Tanggal='" & Me.DTPTanggal.EditValue & "' and Proses='" & Me.SLUProses.EditValue & "' and Line='" & Me.SLULine.EditValue & "' and Jam=" & Jam & "", koneksi)
        If Me.SLUProses.EditValue = "Kirim" OrElse Me.SLUProses.EditValue = "Terima" Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,HslIDD,Tanggal,Proses,Line,Jam,d.Barcode,upper(BOMID) as BOMID,d.ArtCode,Qty,Batal,Tot,BtlDate,BtlBy,ArtName,W.Nama As Warna,Ass,SatID,Isi,Batal As BtlBef  From T_HslProdDtlKirim D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID where Tanggal='" & Me.DTPTanggal.EditValue & "' and Proses='" & Me.SLUProses.EditValue & "' and Line='" & Me.SLULine.EditValue & "' and Jam=" & Jam & "", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,HslIDD,Tanggal,Proses,Line,Jam,d.Barcode,upper(BOMID) as BOMID,d.ArtCode,Qty,Batal,Tot,BtlDate,BtlBy,ArtName,W.Nama As Warna,Ass,SatID,Isi,Batal As BtlBef  From T_HslProdDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID where Tanggal='" & Me.DTPTanggal.EditValue & "' and Proses='" & Me.SLUProses.EditValue & "' and Line='" & Me.SLULine.EditValue & "' and Jam=" & Jam & "", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "T_HslPodDtl")
        Try
            DsMaster.Tables("T_HslPodDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HslPodDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HslPodDtl"

        Dim fc As Integer
        Dim a : For a = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(a, "Barcode") = Barcode Then
                fc = a
                Exit For
            End If
        Next

        Me.GridView1.FocusedRowHandle = fc

    End Sub

    Private Sub FHslProdFns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LUE()

        If MainModule.SU = True Then
            Me.SLUProses.Properties.ReadOnly = False
            Me.SLULine.Properties.ReadOnly = False
            Me.SLUJam.Properties.ReadOnly = False
            Me.DTPTanggal.Properties.ReadOnly = False
        Else
            'If MainModule.ProsesAktif <> "%" And MainModule.ProsesAktif <> "" Then
            Me.SLUProses.Properties.ReadOnly = True
            Me.SLULine.Properties.ReadOnly = True
            Me.SLUJam.Properties.ReadOnly = True
            Me.DTPTanggal.Properties.ReadOnly = True
            'End If
        End If

        Me.SLUProses.EditValue = MainModule.ProsesAktif

        CekJam()

        Dim cmds As New SqlCommand("SPAftSScanBrcd")
        cmds.CommandType = CommandType.StoredProcedure

        With cmds
            .Connection = koneksi

            With koneksi
                .Open()
                cmds.ExecuteNonQuery()
                .Close()
            End With

        End With

        FillDt(Me.SLUJam.EditValue)
        Me.ActiveControl = TBBarcode

    End Sub

    Private Sub TBBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles TBBarcode.KeyDown
        koneksi.Close()


        Select Case e.KeyCode
            Case Keys.Enter

                Me.TBQty.Focus()

            Case Keys.F11
                CekSU()
        End Select

    End Sub

    Private Sub TBQty_KeyDown(sender As Object, e As KeyEventArgs) Handles TBQty.KeyDown
        koneksi.Close()

        Select Case e.KeyCode
            Case Keys.Enter

                If MainModule.SU = False Then
                    CekJam()
                End If

                If Me.SLUProses.EditValue = "" Or IsDBNull(Me.SLUProses.EditValue) Then
                    FcMsgBox("Proses Harus Diisi", "Peringatan", MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If Me.SLULine.EditValue = "" Or IsDBNull(Me.SLULine.EditValue) Then
                    FcMsgBox("Line Harus Diisi", "Peringatan", MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim Cek As Integer = 0

                Dim cmd As New SqlCommand("Select Count(*) From T_BOMPO Where BOMID+'\'+ArtCode='" & Me.TBBarcode.EditValue & "'", koneksi)

                koneksi.Close()

                With koneksi
                    .Open()
                    Cek = cmd.ExecuteScalar()
                    .Close()
                End With

                If Cek > 0 Then
                    Dim str As String = 0
                    Dim strArr() As String
                    str = Me.TBBarcode.EditValue
                    strArr = str.Split("\")

                    Barcode = Me.TBBarcode.EditValue

                    Dim Saldo As Decimal = 0
                    Dim CekDt As Integer = 0
                    Dim CekUrut As Integer = 0

                    'Dim cmd1 As New SqlCommand("Select Count(*) From (Select H.Proses,(Select Isnull((Select Sum(Qty)-Sum(Batal)+" & Me.TBQty.EditValue & " From T_HslProdDtl H1 Inner Join M_Brg B1 On H1.ArtCode=B1.ArtCode Inner Join T_ProsesKrjDtl PD1 On H1.Proses=PD1.Proses and B1.MerkID+B1.KatID+B1.JnsID+'-'+B1.Urut=PD1.Art Where BOMID='" & strArr(0) & "' and H1.ArtCode='" & strArr(1) & "' and PD1.Urut=PD.Urut-1),Case when PD.Urut-1=0 Then (Select Isnull((Select Qty+QtyPol From T_BOMPO where BOMID='" & strArr(0) & "' and ArtCode='" & strArr(1) & "'),0)) Else Sum(Qty)-Sum(Batal) End)) As Bef,(Select Isnull((Select Sum(Qty)-Sum(Batal)+" & Me.TBQty.EditValue & " From T_HslProdDtl H1 Inner Join M_Brg B1 On H1.ArtCode=B1.ArtCode Inner Join T_ProsesKrjDtl PD1 On H1.Proses=PD1.Proses and B1.MerkID+B1.KatID+B1.JnsID+'-'+B1.Urut=PD1.Art where BOMID='" & strArr(0) & "' and H1.ArtCode='" & strArr(1) & "' and PD1.Urut=PD.Urut),Sum(Qty)-Sum(Batal)+" & Me.TBQty.EditValue & ")) As Qty From T_HslProdDtl H Inner Join M_Brg B On H.ArtCode=B.ArtCode Inner Join T_ProsesKrjDtl PD On H.Proses=PD.Proses and B.MerkID+B.KatID+B.JnsID+'-'+B.Urut=PD.Art Where BOMID='" & strArr(0) & "' and H.ArtCode='" & strArr(1) & "' Group By H.Proses,PD.Urut)as x	Where Qty>Bef", koneksi)

                    'koneksi.Close()

                    'With koneksi
                    '    .Open()
                    '    Saldo = cmd1.ExecuteScalar()
                    '    .Close()
                    'End With

                    'If Saldo > 0 Then
                    '    FcMsgBox("Saldo Melebihi Saldo Proses/SPK", "Error", MessageBoxIcon.Error)
                    '    Exit Sub
                    'End If


                    Dim cmd2 As New SqlCommand("Select Count(*) From T_StokProses Where BOMID='" & strArr(0) & "'", koneksi)

                    koneksi.Close()

                    With koneksi
                        .Open()
                        CekDt = cmd2.ExecuteScalar()
                        .Close()
                    End With
                    'Disini()


                    If Me.SLUProses.EditValue = "Kirim" OrElse Me.SLUProses.EditValue = "Terima" Then

                        Dim cmSPDd As New SqlCommand("SPInsUpT_HslProdDtlKirim")
                        cmSPDd.CommandType = CommandType.StoredProcedure
                        Dim x1d As Integer

                        With cmSPDd
                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                            .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                            .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.SLUJam.EditValue
                            .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.SLULine.EditValue
                            .Parameters.Add("@Barcode", SqlDbType.VarChar).Value = Me.TBBarcode.EditValue
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = strArr(0)
                            .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = strArr(1)
                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.TBQty.EditValue
                            '.Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                            '.Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                            'MsgBox(Me.DTPTanggal.EditValue & " - " & Me.SLUProses.EditValue & " - " & Me.SLUJam.EditValue & " - " & Me.SLULine.EditValue & " - " & Me.TBBarcode.EditValue & " - " & strArr(0) & " " & strArr(1) & " - " & Me.TBQty.EditValue)
                            Try
                                With koneksi
                                    .Open()
                                    cmSPDd.ExecuteNonQuery()
                                    x1d = cmSPDd.Parameters("@Return").Value
                                    .Close()
                                End With



                                If x1d <> 0 Then
                                    MsgBox("Salah")
                                    FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                                    Exit Sub
                                End If

                            Catch ex As Exception
                                MsgBox("Salah2")
                                FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                        End With

                        Me.TBBarcode.EditValue = ""
                        Me.TBQty.EditValue = 0

                        FillDt(Me.SLUJam.EditValue)
                    End If


                    ' Disini()




                    If CekDt > 0 AndAlso Me.SLUProses.EditValue <> "Kirim" AndAlso Me.SLUProses.EditValue <> "Terima" Then
                        Dim cmd1 As New SqlCommand("Select Isnull((Select Sum(Masuk)-Sum(Keluar)-" & Me.TBQty.EditValue & " From T_StokProses Where Proses='" & Me.SLUProses.EditValue & "' and BOMID='" & strArr(0) & "' and ArtCode='" & strArr(1) & "'),-" & Me.TBQty.EditValue & ")", koneksi)

                        koneksi.Close()

                        With koneksi
                            .Open()
                            Saldo = cmd1.ExecuteScalar()
                            .Close()
                        End With

                        If Saldo < 0 Then
                            FcMsgBox("Saldo Melebihi Saldo Proses/SPK", "Error", MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Else
                        Dim NoUrut As Integer

                        Dim cmd3 As New SqlCommand("Select K.Urut From M_Brg B Inner Join T_ProsesKrjDtl K On MerkID+KatID+JnsID+'-'+B.Urut=K.Art Where ArtCode='" & strArr(1) & "' and Proses='" & Me.SLUProses.EditValue & "'", koneksi)

                        koneksi.Close()

                        With koneksi
                            .Open()
                            NoUrut = cmd3.ExecuteScalar()
                            .Close()
                        End With

                        If NoUrut = 1 Then

                            Dim SalBOM As Decimal

                            Dim cmd4 As New SqlCommand("Select Isnull((Select Qty+QtyPol From T_BOMPO where BOMID='" & strArr(0) & "' and ArtCode='" & strArr(1) & "'),0)", koneksi)

                            koneksi.Close()

                            With koneksi
                                .Open()
                                SalBOM = cmd4.ExecuteScalar()
                                .Close()
                            End With

                            If SalBOM - Me.TBQty.EditValue < 0 Then
                                FcMsgBox("Qty Melebihi Total BOM", "Error", MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        Else
                            If Me.SLUProses.EditValue <> "Kirim" AndAlso Me.SLUProses.EditValue <> "Terima" Then
                                FcMsgBox("Proses Pertama Belum Ada Saldo", "Error", MessageBoxIcon.Error)
                                Exit Sub
                            Else
                                FcMsgBox("Berhasil Masuk", "Error", MessageBoxIcon.Information)
                                Exit Sub
                            End If

                        End If
                    End If

                    Dim jml As Integer = 0

                    Dim command As New SqlCommand("Select Count(*) From T_HslProd Where Tanggal='" & Me.DTPTanggal.EditValue & "' and Proses='" & Me.SLUProses.EditValue & "' and Line='" & Me.SLULine.EditValue & "' and Jam=" & Me.SLUJam.EditValue & "", koneksi)

                    koneksi.Close()

                    With koneksi
                        .Open()
                        jml = command.ExecuteScalar()
                        .Close()
                    End With

                    If jml = 0 Then
                        Dim cmSP As New SqlCommand("SPInsT_HslProd")
                        cmSP.CommandType = CommandType.StoredProcedure
                        Dim x As Integer

                        With cmSP
                            .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                            .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                            .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.SLUJam.EditValue
                            .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.SLULine.EditValue
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

                                If x <> 0 Then
                                    FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                                    Exit Sub
                                End If

                            Catch ex As Exception
                                FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                        End With
                    End If

                    Dim cmSPD As New SqlCommand("SPInsUpT_HslProdDtl")
                    cmSPD.CommandType = CommandType.StoredProcedure
                    Dim x1 As Integer

                    With cmSPD
                        .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                        .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                        .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.SLUJam.EditValue
                        .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.SLULine.EditValue
                        .Parameters.Add("@Barcode", SqlDbType.VarChar).Value = Me.TBBarcode.EditValue
                        .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = strArr(0)
                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = strArr(1)
                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.TBQty.EditValue
                        '.Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                        '.Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                        .Parameters.Add("@Return", SqlDbType.Int)
                        .Parameters("@Return").Direction = ParameterDirection.Output
                        .Connection = koneksi

                        Try
                            With koneksi
                                .Open()
                                cmSPD.ExecuteNonQuery()
                                x1 = cmSPD.Parameters("@Return").Value
                                .Close()
                            End With

                            If x1 <> 0 Then
                                FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                                Exit Sub
                            End If

                        Catch ex As Exception
                            FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                    End With

                    Me.TBBarcode.EditValue = ""
                    Me.TBQty.EditValue = 0

                    FillDt(Me.SLUJam.EditValue)

                Else
                    FcMsgBox("Barcode Tidak Terdaftar", "Error", MessageBoxIcon.Error)
                    Me.TBBarcode.EditValue = ""
                    Exit Sub
                End If

            Case Keys.F11
                CekSU()
        End Select

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        koneksi.Close()
        '        'update alam
        If Me.SLUProses.EditValue = "Kirim" OrElse Me.SLUProses.EditValue = "Terima" Then
            'update alam
            If e.Column Is GridView1.Columns("Qty") Then

                Me.GridView1.SetRowCellValue(e.RowHandle, "Tot", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Batal"))

            ElseIf e.Column Is GridView1.Columns("Batal") Then

                If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                    If Me.GridView1.GetRowCellValue(e.RowHandle, "Batal") > Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") Then
                        FcMsgBox("Batal Tidak Boleh Melebihi Qty", "Error", MessageBoxIcon.Error)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Batal", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
                    End If

                    Dim CekPros As Integer
                   
                    CekPros = 0
                    If CekPros > 0 Then

                        FcMsgBox("Proses Selanjutnya Ada yang Minus Atau Melebihi Proses Sebelumnya. Silakan Cek Hasil Produksi", "Error", MessageBoxIcon.Error)

                        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        Me.GridView1.SetRowCellValue(e.RowHandle, "Batal", Me.GridView1.GetRowCellValue(e.RowHandle, "BtlBef"))

                        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        FillDt(Me.SLUJam.EditValue)

                        Exit Sub
                    Else
                        Dim cmSPDtl As New SqlCommand("SPUpT_HslProdDtlKirim")
                        cmSPDtl.CommandType = CommandType.StoredProcedure
                        Dim x As Integer

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetFocusedRowCellValue("HslIDD")
                            .Parameters.Add("@Tgl", SqlDbType.DateTime).Value = Me.DTPTanggal.EditValue
                            .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                            .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.SLUJam.EditValue
                            .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.SLULine.EditValue
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedRowCellValue("BOMID")
                            .Parameters.Add("@Barcode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedRowCellValue("Barcode")
                            .Parameters.Add("@Batal", SqlDbType.Decimal).Value = Me.GridView1.GetFocusedRowCellValue("Batal")
                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktifBtl
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

                        If x = 0 Then
                            FcMsgBox("Data Berhasil Diubah", "Informasi", MessageBoxIcon.Information)
                        Else
                            FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                            FillDt(Me.SLUJam.EditValue)
                            Exit Sub
                        End If
                    End If


                    FillDt(Me.SLUJam.EditValue)

                    Me.GridView1.SetRowCellValue(e.RowHandle, "Tot", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Batal"))

                    Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", False)

                    Me.GridView1.Columns("Cek").Visible = False
                    Me.GridView1.Columns("Cek").OptionsColumn.AllowEdit = False
                End If

                Me.TBBarcode.Focus()

            ElseIf e.Column Is GridView1.Columns("Cek") Then

                If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                    Me.GridView1.Columns("Batal").OptionsColumn.AllowEdit = True

                Else
                    Me.GridView1.Columns("Batal").OptionsColumn.AllowEdit = False

                End If
            End If

            'update alam
        Else
            'update alam

            If e.Column Is GridView1.Columns("Qty") Then

                Me.GridView1.SetRowCellValue(e.RowHandle, "Tot", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Batal"))

            ElseIf e.Column Is GridView1.Columns("Batal") Then

                If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                    If Me.GridView1.GetRowCellValue(e.RowHandle, "Batal") > Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") Then
                        FcMsgBox("Batal Tidak Boleh Melebihi Qty", "Error", MessageBoxIcon.Error)
                        Me.GridView1.SetRowCellValue(e.RowHandle, "Batal", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty"))
                    End If

                    Dim CekPros As Integer
                    Dim command As New SqlCommand("Select dbo.fcCekBtlProd('" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "','" & Me.SLUProses.EditValue & "','" & Me.GridView1.GetFocusedRowCellValue("ArtCode") & "','" & Me.GridView1.GetFocusedRowCellValue("Batal") & "','" & Me.SLUJam.EditValue & "','" & Me.DTPTanggal.EditValue & "')", koneksi)

                    With koneksi
                        .Open()
                        command.CommandTimeout = 9000
                        CekPros = command.ExecuteScalar()
                        .Close()
                    End With

                    If CekPros > 0 Then

                        FcMsgBox("Proses Selanjutnya Ada yang Minus Atau Melebihi Proses Sebelumnya. Silakan Cek Hasil Produksi", "Error", MessageBoxIcon.Error)

                        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        Me.GridView1.SetRowCellValue(e.RowHandle, "Batal", Me.GridView1.GetRowCellValue(e.RowHandle, "BtlBef"))

                        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

                        FillDt(Me.SLUJam.EditValue)

                        Exit Sub
                    Else
                        Dim cmSPDtl As New SqlCommand("SPUpT_HslProdDtl")
                        cmSPDtl.CommandType = CommandType.StoredProcedure
                        Dim x As Integer

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetFocusedRowCellValue("HslIDD")
                            .Parameters.Add("@Tgl", SqlDbType.DateTime).Value = Me.DTPTanggal.EditValue
                            .Parameters.Add("@Proses", SqlDbType.VarChar).Value = Me.SLUProses.EditValue
                            .Parameters.Add("@Jam", SqlDbType.Int).Value = Me.SLUJam.EditValue
                            .Parameters.Add("@Line", SqlDbType.VarChar).Value = Me.SLULine.EditValue
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedRowCellValue("BOMID")
                            .Parameters.Add("@Barcode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedRowCellValue("Barcode")
                            .Parameters.Add("@Batal", SqlDbType.Decimal).Value = Me.GridView1.GetFocusedRowCellValue("Batal")
                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktifBtl
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

                        If x = 0 Then
                            FcMsgBox("Data Berhasil Diubah", "Informasi", MessageBoxIcon.Information)
                        Else
                            FcMsgBox("Data Gagal Disimpan", "Error", MessageBoxIcon.Error)
                            FillDt(Me.SLUJam.EditValue)
                            Exit Sub
                        End If
                    End If

                    Dim cmds As New SqlCommand("SPAftSScanBrcd")
                    cmds.CommandType = CommandType.StoredProcedure

                    With cmds
                        .Connection = koneksi

                        With koneksi
                            .Open()
                            cmds.ExecuteNonQuery()
                            .Close()
                        End With

                    End With

                    FillDt(Me.SLUJam.EditValue)

                    Me.GridView1.SetRowCellValue(e.RowHandle, "Tot", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Batal"))

                    Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", False)

                    Me.GridView1.Columns("Cek").Visible = False
                    Me.GridView1.Columns("Cek").OptionsColumn.AllowEdit = False
                End If

                Me.TBBarcode.Focus()

            ElseIf e.Column Is GridView1.Columns("Cek") Then

                If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                    Me.GridView1.Columns("Batal").OptionsColumn.AllowEdit = True

                Else
                    Me.GridView1.Columns("Batal").OptionsColumn.AllowEdit = False

                End If
            End If


            'update alam
        End If
        'update alam

       
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.F11 Then
            CekSU()
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If Me.GridView1.RowCount > 0 Then
                If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue("Cek")) Then
                    If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                        Me.GridView1.Columns("Batal").OptionsColumn.AllowEdit = True
                    Else
                        Me.GridView1.Columns("Batal").OptionsColumn.AllowEdit = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub DTPTanggal_EditValueChanged(sender As Object, e As EventArgs) Handles DTPTanggal.EditValueChanged
        FillDt(Me.SLUJam.EditValue)
    End Sub

    Private Sub SLUJam_EditValueChanged(sender As Object, e As EventArgs) Handles SLUJam.EditValueChanged
        FillDt(Me.SLUJam.EditValue)

        If Me.SLUJam.EditValue <> 0 Then
            Me.ESIJam.Text = DsMaster.Tables("M_JamL").Select("Jam = '" & Me.SLUJam.EditValue & "'")(0).Item("JamAw") & " s/d " & DsMaster.Tables("M_JamL").Select("Jam =" & Me.SLUJam.EditValue & "")(0).Item("JamAkh")
        End If

    End Sub

    Private Sub SLUProses_EditValueChanged(sender As Object, e As EventArgs) Handles SLUProses.EditValueChanged

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Jam,Cast(JamAw As varchar(8)) As JamAw,Cast(JamAkh As varchar(8)) As JamAkh From M_Jam where Proses='" & Me.SLUProses.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_JamL")
        Try
            DsMaster.Tables("M_JamL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_JamL")

        Me.SLUJam.Properties.DataSource = DsMaster.Tables("M_JamL")
        Me.SLUJam.Properties.DisplayMember = "Jam"
        Me.SLUJam.Properties.ValueMember = "Jam"

        cmsl = New SqlDataAdapter("Select Distinct Line From M_User where Aktif='True' and Line<>'' and Line is not null", koneksi)
        cmsl.TableMappings.Add("Table", "M_LineL")
        Try
            DsMaster.Tables("M_LineL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_LineL")
        'DsMaster.Tables("M_LineL").Rows.Add("")

        Me.SLULine.Properties.DataSource = DsMaster.Tables("M_LineL")
        Me.SLULine.Properties.DisplayMember = "Line"
        Me.SLULine.Properties.ValueMember = "Line"

        FillDt(Me.SLUJam.EditValue)

        Me.SLULine.EditValue = MainModule.LineAktif
    End Sub

    Private Sub SLULine_EditValueChanged(sender As Object, e As EventArgs) Handles SLULine.EditValueChanged
        FillDt(Me.SLUJam.EditValue)
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If e.RowHandle = Me.GridView1.FocusedRowHandle Then
                    If GridView1.GetRowCellValue(e.RowHandle, "Barcode") = Barcode Then
                        e.Appearance.ForeColor = Color.Black
                        e.Appearance.BackColor = Color.Yellow
                    Else
                        e.Appearance.ForeColor = Nothing
                        e.Appearance.BackColor = Nothing
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub
End Class