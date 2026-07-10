Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSinkronisasi
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAllLPB, CekAllRBL, CekAllBPB, CekAllTAG As Boolean
    Dim jml As Integer

    Private Sub FSinkronisasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LBDB.Text = namaDBSink

        'DsMaster = New System.Data.DataSet
        Me.DTPAwal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAwal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPAkhir.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSaveMaster_Click(sender As Object, e As EventArgs) Handles BSaveMaster.Click
        Dim command As New SqlCommand("SPAmbilMaster")
        command.CommandType = CommandType.StoredProcedure
        With command
            .Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
            .Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
        End With
        command.CommandTimeout = 90000


        Dim koneksi As New SqlConnection(GlobalKoneksi)

        With command
            .Connection = koneksi

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                .Close()
            End With
        End With

        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BProsesPO_Click(sender As Object, e As EventArgs) Handles BProsesPO.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,(Select Count(*) From M_Supp Where SuppID=H.SuppID) As CekSupp,POID, PeriodID,Tanggal,Jenis,Kat,Tipe,Shipment,H.CustID,C.Nama As Customer,H.SuppID,S.Nama As Supplier,S.Alamat,K.Nama As Kota,TglKirim,DueDate, CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotSbDisc,DiscP, RpDiscP,DiscRp,TotDisc,TotDPP,TotPPn,TotAkhir,H.Ket,stsPPn,stsBatal From [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.T_POBB H Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Supp S On H.SuppID=S.SuppID Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Kota K On S.KotaID=K.KotaID Inner Join[" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo. M_Cust C On H.CustID=C.CustID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and  Tanggal<='" & Me.DTPAkhir.EditValue & "' and H.POID Not In (Select POID From T_POBB) Order By POID", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBBSk")
        'DsMaster = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsMaster, "T_POBBSk")
        DsMaster.Tables("T_POBBSk").Clear()
        cmsl.Fill(DsMaster, "T_POBBSk")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBBSk"
    End Sub

    Private Sub BSavePO_Click(sender As Object, e As EventArgs) Handles BSavePO.Click
        Dim x, i As Integer
        x = 0
        i = 0
        Dim POID As String = ""

        For i = 0 To DsMaster.Tables("T_POBBSk").Rows.Count - 1
            If DsMaster.Tables("T_POBBSk").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    POID = DsMaster.Tables("T_POBBSk").Rows(i).Item("POID")
                Else
                    POID &= "," & DsMaster.Tables("T_POBBSk").Rows(i).Item("POID")
                End If
            End If
        Next


        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("SPAmbilPO")
        With command
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
            .Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
            .Parameters.Add("@POIDFill", SqlDbType.VarChar).Value = POID
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
        End With

        With command
            .Connection = koneksi
            .CommandTimeout = 90000

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                x = command.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x = 0 Then
            XtraMessageBox.Show("Data PO Berhasil Diambil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ElseIf x = 1 Then
            XtraMessageBox.Show("Data PO Gagal Diambil", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data PO Gagal Diambil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is Me.GridView1.Columns("Cek") Then
            If e.Value = "True" Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "CekSupp") = 0 Then
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", False)
                End If
            End If
        End If

    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAllLPB Then
            CekAllLPB = False
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAllLPB = True
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView3.DoubleClick
        If CekAllRBL Then
            CekAllRBL = False
            For i As Integer = 0 To Me.GridView3.RowCount - 1
                Me.GridView3.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAllRBL = True
            For i As Integer = 0 To Me.GridView3.RowCount - 1
                Me.GridView3.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView4.DoubleClick
        If CekAllTAG Then
            CekAllTAG = False
            For i As Integer = 0 To Me.GridView4.RowCount - 1
                Me.GridView4.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAllTAG = True
            For i As Integer = 0 To Me.GridView4.RowCount - 1
                Me.GridView4.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView5_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView5.DoubleClick
        If CekAllBPB Then
            CekAllBPB = False
            For i As Integer = 0 To Me.GridView5.RowCount - 1
                Me.GridView5.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAllBPB = True
            For i As Integer = 0 To Me.GridView5.RowCount - 1
                Me.GridView5.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BProsesLPB_Click(sender As Object, e As EventArgs) Handles BProsesLPB.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,POID,TrmID,PeriodID,CodeID,Tanggal,Jenis,H.SuppID,S.Nama as Supplier,S.Alamat,K.Nama As Kota,POID,TglJT,H.GdID,G.Nama as Gudang,SJ, CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRpSat,DiscRp,TotDisc, TotDPP,TotPPn,TotAkhir, H.Ket,stsPPn From [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.T_TrmBB H Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Supp S On H.SuppID=S.SuppID Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Kota K On S.KotaID=K.KotaID Inner Join[" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo. M_Gudang G On H.GdID=G.GdID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and  Tanggal<='" & Me.DTPAkhir.EditValue & "' and H.TrmID Not In (Select TrmID From T_TrmBB) Order By TrmID", koneksi)

        cmsl.TableMappings.Add("Table", "T_TrmBB")
        'DsMaster = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsMaster, "T_TrmBB")
        DsMaster.Tables("T_TrmBB").Clear()
        cmsl.Fill(DsMaster, "T_TrmBB")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_TrmBB"
    End Sub

    Private Sub BSaveLPB_Click(sender As Object, e As EventArgs) Handles BSaveLPB.Click
        Dim x, i As Integer
        x = 0
        i = 0
        Dim TrmID As String = ""

        For i = 0 To DsMaster.Tables("T_TrmBB").Rows.Count - 1
            If DsMaster.Tables("T_TrmBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    TrmID = DsMaster.Tables("T_TrmBB").Rows(i).Item("TrmID")
                Else
                    TrmID &= "," & DsMaster.Tables("T_TrmBB").Rows(i).Item("TrmID")
                End If
            End If
        Next

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("SPAmbilLPB")
        With command
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
            .Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
            .Parameters.Add("@TrmIDFill", SqlDbType.VarChar).Value = TrmID
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
        End With

        With command
            .Connection = koneksi
            .CommandTimeout = 90000

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                x = command.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x = 0 Then
            XtraMessageBox.Show("Data Penerimaan Bahan Berhasil Diambil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ElseIf x = 1 Then
            XtraMessageBox.Show("Data Penerimaan Bahan Gagal Diambil", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data Penerimaan Bahan Gagal Diambil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub BCek_Click(sender As Object, e As EventArgs) Handles BCek.Click
        Dim x, i As Integer
        x = 0
        i = 0
        Dim TrmID As String = ""

        For i = 0 To DsMaster.Tables("T_TrmBB").Rows.Count - 1
            If DsMaster.Tables("T_TrmBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    TrmID = DsMaster.Tables("T_TrmBB").Rows(i).Item("TrmID")
                Else
                    TrmID &= "," & DsMaster.Tables("T_TrmBB").Rows(i).Item("TrmID")
                End If
            End If
        Next

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("SPCekPOIDD", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
        cmsl.SelectCommand.Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
        cmsl.SelectCommand.Parameters.Add("@TrmIDFill", SqlDbType.VarChar).Value = TrmID

        cmsl.TableMappings.Add("Table", "CekPOIDD")
        cmsl.Fill(DsMaster, "CekPOIDD")
        DsMaster.Tables("CekPOIDD").Clear()
        cmsl.Fill(DsMaster, "CekPOIDD")

        Me.GridControl6.DataSource = DsMaster
        Me.GridControl6.DataMember = "CekPOIDD"

    End Sub

    Private Sub BProsesRtr_Click(sender As Object, e As EventArgs) Handles BProsesRtr.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,RtrID,Tanggal,H.SuppID,S.Nama as Supplier,S.Alamat,K.Nama As Kota,POID, H.GdID,G.Nama as Gudang,SJ,CurrID,MtUang,NilTukarRp,TipePPn,PersenPPn,TotQty,TotSbDisc,DiscP,RpDiscP,DiscRpSat,DiscRp,TotDisc,TotDPP, TotPPn,TotAkhir,H.Ket,stsPPn From [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.T_RtrBB H Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Supp S On H.SuppID=S.SuppID Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Kota K On S.KotaID=K.KotaID Inner Join[" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo. M_Gudang G On H.GdID=G.GdID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and  Tanggal<='" & Me.DTPAkhir.EditValue & "' and H.RtrID Not In (Select RtrID From T_RtrBB) Order By RtrID", koneksi)

        cmsl.TableMappings.Add("Table", "T_RtrBB")
        'DsMaster = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsMaster, "T_RtrBB")
        DsMaster.Tables("T_RtrBB").Clear()
        cmsl.Fill(DsMaster, "T_RtrBB")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_RtrBB"
    End Sub

    Private Sub BSaveRtr_Click(sender As Object, e As EventArgs) Handles BSaveRtr.Click
        Dim x, i As Integer
        x = 0
        i = 0
        Dim RtrID As String = ""

        For i = 0 To DsMaster.Tables("T_RtrBB").Rows.Count - 1
            If DsMaster.Tables("T_RtrBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    RtrID = DsMaster.Tables("T_RtrBB").Rows(i).Item("RtrID")
                Else
                    RtrID &= "," & DsMaster.Tables("T_RtrBB").Rows(i).Item("RtrID")
                End If
            End If
        Next

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("SPAmbilRetur")
        With command
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
            .Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
            .Parameters.Add("@RtrIDFill", SqlDbType.VarChar).Value = RtrID
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
        End With

        With command
            .Connection = koneksi
            .CommandTimeout = 90000

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                x = command.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x = 0 Then
            XtraMessageBox.Show("Data Retur Pembelian Berhasil Diambil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ElseIf x = 1 Then
            XtraMessageBox.Show("Data Retur Pembelian Gagal Diambil", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data Retur Pembelian Gagal Diambil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub BProsesTAG_Click(sender As Object, e As EventArgs) Handles BProsesTAG.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,TAGID, PeriodID, CodeID, H.GdIDAs, (Select Nama From [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Gudang where GdID=H.GdIDAs) as GudangAs, H.GdIDTj,G.Nama as GudangTj, G.Alamat, K.Nama as Kota, Tanggal,H.Ket From [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.T_TAG H Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Gudang G On H.GdIDTj=G.GdID Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Kota K On G.KotaID=K.KotaID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and  Tanggal<='" & Me.DTPAkhir.EditValue & "' and H.TAGID Not In (Select TAGID From T_TAG) Order By TAGID", koneksi)

        cmsl.TableMappings.Add("Table", "T_TAG")
        'DsMaster = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsMaster, "T_TAG")
        DsMaster.Tables("T_TAG").Clear()
        cmsl.Fill(DsMaster, "T_TAG")

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_TAG"
    End Sub

    Private Sub BSaveTAG_Click(sender As Object, e As EventArgs) Handles BSaveTAG.Click
        Dim x, i As Integer
        x = 0
        i = 0
        Dim TAGID As String = ""

        For i = 0 To DsMaster.Tables("T_TAG").Rows.Count - 1
            If DsMaster.Tables("T_TAG").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    TAGID = DsMaster.Tables("T_TAG").Rows(i).Item("TAGID")
                Else
                    TAGID &= "," & DsMaster.Tables("T_TAG").Rows(i).Item("TAGID")
                End If
            End If
        Next

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("SPAmbilTAG")
        With command
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
            .Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
            .Parameters.Add("@TAGIDFill", SqlDbType.VarChar).Value = TAGID
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
        End With

        With command
            .Connection = koneksi
            .CommandTimeout = 90000

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                x = command.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x = 0 Then
            XtraMessageBox.Show("Data TAG Berhasil Diambil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ElseIf x = 1 Then
            XtraMessageBox.Show("Data TAG Gagal Diambil", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data TAG Gagal Diambil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub BProsesBPB_Click(sender As Object, e As EventArgs) Handles BProsesBPB.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BPBID,PeriodID,CodeID,Tanggal,DocID,H.GdID,G.Nama as Gudang,Dv.Nama As Bagian, Dv.Unit,H.Ket From [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.T_BPB H Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Gudang G On H.GdID=G.GdID Inner Join [" & namaServerSink & "," & portSink & "].[" & namaDBSink & "].dbo.M_Div Dv On H.DivID=Dv.DivID Where Tanggal>='" & Me.DTPAwal.EditValue & "' and  Tanggal<='" & Me.DTPAkhir.EditValue & "' and H.BPBID Not In (Select BPBID From T_BPB) Order By BPBID", koneksi)

        cmsl.TableMappings.Add("Table", "T_BPB")
        'DsMaster = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsMaster, "T_BPB")
        DsMaster.Tables("T_BPB").Clear()
        cmsl.Fill(DsMaster, "T_BPB")

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "T_BPB"
    End Sub

    Private Sub BSaveBPB_Click(sender As Object, e As EventArgs) Handles BSaveBPB.Click
        Dim x, i As Integer
        x = 0
        i = 0
        Dim BPID As String = ""

        For i = 0 To DsMaster.Tables("T_BPB").Rows.Count - 1
            If DsMaster.Tables("T_BPB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    BPID = DsMaster.Tables("T_BPB").Rows(i).Item("BPBID")
                Else
                    BPID &= "," & DsMaster.Tables("T_BPB").Rows(i).Item("BPBID")
                End If
            End If
        Next

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("SPAmbilBPB")
        With command
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Server", SqlDbType.VarChar).Value = namaServerSink & "," & portSink
            .Parameters.Add("@DB", SqlDbType.VarChar).Value = namaDBSink
            .Parameters.Add("@BPBIDFill", SqlDbType.VarChar).Value = BPID
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
        End With

        With command
            .Connection = koneksi
            .CommandTimeout = 90000

            With koneksi
                .Open()
                command.ExecuteNonQuery()
                x = command.Parameters("@Return").Value
                .Close()
            End With
        End With

        If x = 0 Then
            XtraMessageBox.Show("Data BPB Berhasil Diambil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ElseIf x = 1 Then
            XtraMessageBox.Show("Data BPB Gagal Diambil", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data BPB Gagal Diambil", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

End Class