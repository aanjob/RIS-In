Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class XXFPOBJLk
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim CodeID, Merk, Kat, Jns, Sat, Gol As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim Isi As Integer

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LBGole.Text = "    " & Golongan
        LBGols.Text = "    " & Golongan
        Gol = Golongan

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=1 and Gol='" & Golongan & "'", koneksi)

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        CodeID = ""
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        CodeID = Reader.Item(1)
                        MnlInsUpd = Reader.Item(2)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("POBJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("POBJEd"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("POBJCO"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("POBJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTPO_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.DTPAwal.Properties.ReadOnly = True
        Me.DTPAkhir.Properties.ReadOnly = True
        Me.SPJT.Properties.ReadOnly = True
        Me.TBSupp.Properties.ReadOnly = True
        Me.TBKirim.Properties.ReadOnly = True
        Me.SLUArtCode.Properties.ReadOnly = True
        Me.TBUpper.Properties.ReadOnly = True
        Me.TBOutsole.Properties.ReadOnly = True
        Me.TBVariasi.Properties.ReadOnly = True
        Me.TBPembelian.Properties.ReadOnly = True
        Me.TBPPIC.Properties.ReadOnly = True
        Me.TBHBeli.Properties.ReadOnly = True
        Me.TBHCBP.Properties.ReadOnly = True
        Me.TBTotDos.Properties.ReadOnly = True
        Me.TBDosPol.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintJL.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTPO_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.DTPAwal.Properties.ReadOnly = False
        Me.DTPAkhir.Properties.ReadOnly = False
        Me.SPJT.Properties.ReadOnly = False
        Me.TBSupp.Properties.ReadOnly = False
        Me.TBKirim.Properties.ReadOnly = False
        Me.SLUArtCode.Properties.ReadOnly = False
        Me.TBUpper.Properties.ReadOnly = False
        Me.TBOutsole.Properties.ReadOnly = False
        Me.TBVariasi.Properties.ReadOnly = False
        Me.TBPembelian.Properties.ReadOnly = False
        Me.TBPPIC.Properties.ReadOnly = False
        Me.TBHBeli.Properties.ReadOnly = False
        Me.TBHCBP.Properties.ReadOnly = False
        Me.TBTotDos.Properties.ReadOnly = False
        Me.TBDosPol.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTPO_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ArtCode,ArtName,MerkID,KatID,JnsID,AssID,SatID,Isi From M_Brg Where Aktif='True' and Gol = '" & Gol & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgPOLk" & Gol)
        cmsl.Fill(DsMaster, "M_BrgPOLk" & Gol)
        DsMaster.Tables("M_BrgPOLk" & Gol).Clear()
        cmsl.Fill(DsMaster, "M_BrgPOLk" & Gol)

        Me.SLUArtCode.Properties.DataSource = DsMaster.Tables("M_BrgPOLk" & Gol)
        Me.SLUArtCode.Properties.DisplayMember = "ArtName"
        Me.SLUArtCode.Properties.ValueMember = "ArtCode"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POIDD,POID,D.ArtCode,ArtName,Uk,IsiDlmDos,Qty,QtyPol,Harga From T_POBJLkDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where POID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJLkDtl" & Gol)
        Try
            DsMaster.Tables("T_POBJLkDtl" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJLkDtl" & Gol)

        DsMaster.Tables("T_POBJLkDtl" & Gol).PrimaryKey = New DataColumn() {DsMaster.Tables("T_POBJLkDtl" & Gol).Columns("ArtCode")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_POBJLkDtl" & Gol
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select POID, PeriodID, CodeID, Tanggal, TglKrmAw, TglKrmAkh, JT, Kpd, KrmKe, H.ArtCode, ArtName,B.StyleID, H.MerkId, H.KatID, H.JnsID, Upp, Outs, Variasi, H.SatID, H.Isi, HBeli, HCBP, TotQty,TotQtyPol,TotPsg,TotPsgPol,TotBayar,SisaKirim,BtlOrder,Pembelian, PPIC, H.Ket, H.Gol, H.stsLunas, H.stsBatal, H.InsDate, H.InsBy, H.UpdDate, H.UpdBy From T_POBJLk H Inner Join M_Brg B On H.ArtCode=B.ArtCode Where PeriodID=" & MainModule.periodAktif & " and H.Gol='" & Gol & "' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Tanggal,POID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_POBJLk" & Gol)
        Try
            DsMaster.Tables("T_POBJLk" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_POBJLk" & Gol)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_POBJLk" & Gol
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_POBJLk")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
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

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub FPOLkBJ_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Purchase Order Barang Jadi"
    End Sub

    Private Sub FPOLkBJ_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        'DsMaster = New System.Data.DataSet
        Me.BVTPO_e.Selected = True
    End Sub

    Private Sub BVTPO_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTPO_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Purchase Order Barang Jadi"
        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("POBJP"), Boolean)
        Me.BVBPrintJL.Enabled = CType(TcodeCollection.Item("POBJP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Purchase Order Barang Jadi"

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlOpBJ(Gol) > 0 Or MainModule.SlstsPeriodNew() = True Then
                'If MainModule.BackDate = False Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
                'End If
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPAwal.EditValue = Date.Now
            Me.DTPAkhir.EditValue = Date.Now

        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If


        Me.SPJT.EditValue = 0
        Me.TBSupp.EditValue = ""
        'Me.TBKirim.EditValue = MainModule.NmPerusahaan
        Me.TBUpper.EditValue = ""
        Me.TBOutsole.EditValue = ""
        Me.TBVariasi.EditValue = ""
        Me.TBPembelian.EditValue = ""
        Me.TBPPIC.EditValue = ""
        Me.SLUArtCode.EditValue = ""
        Me.TBHBeli.EditValue = 0
        Me.TBHCBP.EditValue = 0
        Me.TBTotDos.EditValue = 0
        Me.TBTotPsg.EditValue = 0
        Me.TBDosPol.EditValue = 0
        Me.TBPsgPol.EditValue = 0
        Me.MKet.EditValue = "* Quality harap diperhatikan" & vbCrLf & "* Jaminan Produk 1 tahun dari Delivery " & vbCrLf & "  (Cashback 100% dari barang yang rusak)" & vbCrLf & "* Included Pack Vacuum & Outer Box" & vbCrLf & "* Material Upper Webbing "
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_POBJLkDtl" & Gol).Clear()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Purchase Order Barang Jadi"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlOpBJ(Gol) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Opname", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotQty") <> SlCek("T_POBJLk", "SisaKirim", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Or SlCek("T_POBJLk", "stsBatal", "POID", Me.GridView2.GetFocusedDataRow.Item("POID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Penerimaan/Lunas/Batal", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlPOMkt(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ditarik BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.DTPAwal.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKrmAw")
            Me.DTPAkhir.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKrmAkh")
            Me.SPJT.EditValue = Me.GridView2.GetFocusedDataRow.Item("JT")
            Me.TBSupp.EditValue = Me.GridView2.GetFocusedDataRow.Item("Kpd")
            Me.TBKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("KrmKe")
            Me.SLUArtCode.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtCode")
            Merk = Me.GridView2.GetFocusedDataRow.Item("MerkID")
            Kat = Me.GridView2.GetFocusedDataRow.Item("KatID")
            Jns = Me.GridView2.GetFocusedDataRow.Item("JnsID")
            Sat = Me.GridView2.GetFocusedDataRow.Item("SatID")
            Isi = Me.GridView2.GetFocusedDataRow.Item("Isi")
            Me.TBUpper.EditValue = Me.GridView2.GetFocusedDataRow.Item("Upp")
            Me.TBOutsole.EditValue = Me.GridView2.GetFocusedDataRow.Item("Outs")
            Me.TBVariasi.EditValue = Me.GridView2.GetFocusedDataRow.Item("Variasi")
            Me.TBPembelian.EditValue = Me.GridView2.GetFocusedDataRow.Item("Pembelian")
            Me.TBPPIC.EditValue = Me.GridView2.GetFocusedDataRow.Item("PPIC")
            Me.TBHBeli.EditValue = Me.GridView2.GetFocusedDataRow.Item("HBeli")
            Me.TBHCBP.EditValue = Me.GridView2.GetFocusedDataRow.Item("HCBP")
            Me.TBTotDos.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDos")
            Me.TBTotPsg.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPsg")
            Me.TBDosPol.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotDosPol")
            Me.TBPsgPol.EditValue = Me.GridView2.GetFocusedDataRow.Item("TotPsgPol")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

            FillDtl(Me.TBKode.EditValue)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Purchase Order Barang Jadi"
        koneksi.Close()

        If Me.GridView2.GetFocusedDataRow.Item("stsLunas") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlBOMCancel(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena SPK Belum Dibatalkan", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlPOMkt(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            If XtraMessageBox.Show("PO Sudah Ditarik BOM. Apakah Mau Dibatalkan ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPBtlPOBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("POID")
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
                            XtraMessageBox.Show("Sisa Order PO Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Sisa Order PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            Else
                Exit Sub
            End If
        Else
            If XtraMessageBox.Show("Apakah Anda Mau Membatalkan Sisa : " & Me.GridView2.GetFocusedDataRow.Item("POID") & " Yang Belum Dikirim/Diproduksi ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPBtlPOBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("POID")
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
                            XtraMessageBox.Show("Sisa Order PO Berhasil Dibatalkan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Sisa Order PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("PO Gagal Dibatalkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Purchase Order Barang Jadi"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            'If MainModule.BackDate = False Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            'End If
        End If

        If MainModule.SlPOMkt(Me.GridView2.GetFocusedDataRow.Item("POID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ditarik BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotQty") <> Me.GridView2.GetFocusedDataRow.Item("SisaKirim") Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("POID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_POBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("POID")
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
                            XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        Else
                            XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If
        End If

    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kpd"), "Kpd")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("KrmKe"), "KrmKe")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAw"), "TglKrmAw")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAkh"), "TglKrmAkh")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HBeli"), "HBeli")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JT"), "JT")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtCode"), "ArtCode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Upp"), "Upp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Outs"), "Outs")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Variasi"), "Variasi")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDos") + Me.GridView2.GetFocusedDataRow.Item("TotDosPol"), "TotDos")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "TotPsg")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotBayar"), "TotBayar")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("PPIC"), "PPIC")
        'bind.Add(Me.GridView2.GetFocusedDataRow.Item("Pembelian"), "Pembelian")

        'If Gol = "Character" Then
        '    bind.Add(MainModule.MMCr, "MM")
        'ElseIf Gol = "Own" Then
        '    bind.Add(MainModule.MMOwn, "MM")
        'ElseIf Gol = "Job Order" Then
        '    bind.Add(MainModule.MMJO, "MM")
        'End If

        Dim XR As New XRPOBJLkInt
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBPrintJL_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintJL.ItemClick
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Kpd"), "Kpd")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("KrmKe"), "KrmKe")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAw"), "TglKrmAw")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKrmAkh"), "TglKrmAkh")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HBeli"), "HBeli")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("JT"), "JT")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtCode"), "ArtCode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Upp"), "Upp")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Outs"), "Outs")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Variasi"), "Variasi")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotDos") + Me.GridView2.GetFocusedDataRow.Item("TotDosPol"), "TotDos")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "TotPsg")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotBayar"), "TotBayar")

        'If Gol = "Character" Then
        '    bind.Add(MainModule.MMCr, "MM")
        'ElseIf Gol = "Own" Then
        '    bind.Add(MainModule.MMOwn, "MM")
        'ElseIf Gol = "Job Order" Then
        '    bind.Add(MainModule.MMJO, "MM")
        'End If

        Dim XR As New XRPOBJLkExt
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_POBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKrmAw", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@TglKrmAkh", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.SPJT.EditValue
                    .Parameters.Add("@Kpd", SqlDbType.VarChar).Value = Me.TBSupp.EditValue
                    .Parameters.Add("@KrmKe", SqlDbType.VarChar).Value = Me.TBKirim.EditValue
                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Merk
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Kat
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Jns
                    .Parameters.Add("@Upp", SqlDbType.VarChar).Value = Me.TBUpper.EditValue
                    .Parameters.Add("@Outs", SqlDbType.VarChar).Value = Me.TBOutsole.EditValue
                    .Parameters.Add("@Var", SqlDbType.VarChar).Value = Me.TBVariasi.EditValue
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Sat
                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Isi
                    .Parameters.Add("@HBeli", SqlDbType.Decimal).Value = Me.TBHBeli.EditValue
                    .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.TBHCBP.EditValue
                    .Parameters.Add("@TotDos", SqlDbType.Decimal).Value = Me.TBTotDos.EditValue
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Me.TBTotPsg.EditValue
                    .Parameters.Add("@TotDosPol", SqlDbType.Decimal).Value = Me.TBDosPol.EditValue
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = Me.TBPsgPol.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Harga").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Pembelian", SqlDbType.VarChar).Value = Me.TBPembelian.EditValue
                    .Parameters.Add("@PPIC", SqlDbType.VarChar).Value = Me.TBPPIC.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_POBJLkDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                    .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Harga")
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
                                    Del()
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBKode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            Del()
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_POBJLk")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TglKrmAw", SqlDbType.Date).Value = Me.DTPAwal.EditValue
                    .Parameters.Add("@TglKrmAkh", SqlDbType.Date).Value = Me.DTPAkhir.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.SPJT.EditValue
                    .Parameters.Add("@Kpd", SqlDbType.VarChar).Value = Me.TBSupp.EditValue
                    .Parameters.Add("@KrmKe", SqlDbType.VarChar).Value = Me.TBKirim.EditValue
                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.SLUArtCode.EditValue
                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Merk
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Kat
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Jns
                    .Parameters.Add("@Upp", SqlDbType.VarChar).Value = Me.TBUpper.EditValue
                    .Parameters.Add("@Outs", SqlDbType.VarChar).Value = Me.TBOutsole.EditValue
                    .Parameters.Add("@Var", SqlDbType.VarChar).Value = Me.TBVariasi.EditValue
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Sat
                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Isi
                    .Parameters.Add("@HBeli", SqlDbType.Decimal).Value = Me.TBHBeli.EditValue
                    .Parameters.Add("@HCBP", SqlDbType.Decimal).Value = Me.TBHCBP.EditValue
                    .Parameters.Add("@TotDos", SqlDbType.Decimal).Value = Me.TBTotDos.EditValue
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Me.TBTotPsg.EditValue
                    .Parameters.Add("@TotDosPol", SqlDbType.Decimal).Value = Me.TBDosPol.EditValue
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = Me.TBPsgPol.EditValue
                    .Parameters.Add("@TotBayar", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView1.Columns("Harga").SummaryText, Decimal), 2, MidpointRounding.AwayFromZero)
                    .Parameters.Add("@Pembelian", SqlDbType.VarChar).Value = Me.TBPembelian.EditValue
                    .Parameters.Add("@PPIC", SqlDbType.VarChar).Value = Me.TBPPIC.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_POBJLkDtl")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi

                                With koneksi
                                    .Open()
                                    cmSPDel.ExecuteNonQuery()
                                    .Close()
                                End With

                            End With
                        Next

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "POIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_POBJLkDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                        .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Harga")
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
                                        Me.GridView1.SetRowCellValue(i, "POIDD", Me.GridView1.GetRowCellValue(i, "POIDD") * -1)
                                    Else
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_POBJLkDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "POIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "IsiDlmDos")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "QtyPol")
                                        .Parameters.Add("@Harga", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Harga")
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
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
        End Select

        LockControl()
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUArtCode_Leave(sender As Object, e As EventArgs) Handles SLUArtCode.Leave
        Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "POIDD")

            Me.GridView1.DeleteRow(y)
        Next

        DsMaster.Tables("T_POBJLkDtl" & Gol).Clear()

        Dim ArtLepas As String
        Dim SplitKdD() As String

        If Me.SLUArtCode.EditValue <> "" Then
            Merk = DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("MerkID")
            Kat = DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("KatID")
            Jns = DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("JnsID")
            Sat = DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("SatID")
            Isi = DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("Isi")

            DtTrans = New System.Data.DataTable
            DtTrans.Columns.Add("POID")
            DtTrans.Columns.Add("POIDD")
            DtTrans.Columns.Add("ArtCode")
            DtTrans.Columns.Add("ArtName")
            DtTrans.Columns.Add("Uk")
            DtTrans.Columns.Add("IsiDlmDos", Type.GetType("System.Int32"))
            DtTrans.Columns.Add("Qty", Type.GetType("System.Decimal"))
            DtTrans.Columns.Add("QtyPol", Type.GetType("System.Decimal"))
            DtTrans.Columns.Add("Harga", Type.GetType("System.Decimal"))

            Me.GridControl1.DataSource = DtTrans

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select Uk,Qty From M_BrgAssDtl Where AssID='" & DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("AssID") & "'", koneksi)

            cmsl.TableMappings.Add("Table", "M_BrgAssDtlPO" & Gol)
            cmsl.Fill(DsMaster, "M_BrgAssDtlPO" & Gol)
            DsMaster.Tables("M_BrgAssDtlPO" & Gol).Clear()
            cmsl.Fill(DsMaster, "M_BrgAssDtlPO" & Gol)

            SplitKdD = CType(Me.SLUArtCode.EditValue, String).Split("-")
            ArtLepas = Me.SLUArtCode.EditValue.ToString.Remove(Me.SLUArtCode.EditValue.ToString.Length - (SplitKdD(3).Length), SplitKdD(3).Length)


            Dim i : For i = 0 To DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows.Count - 1
                DtTrans.Rows.Add(Me.TBKode.EditValue, (i + 1) * -1, ArtLepas + DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(i).Item("Uk"), Me.SLUArtCode.Text, DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(i).Item("Uk"), DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(i).Item("Qty"), Me.TBTotDos.EditValue * DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(i).Item("Qty"), Me.TBHBeli.EditValue * DsMaster.Tables("M_BrgAssDtlPO" & Gol).Rows(i).Item("Qty") * (Me.TBTotDos.EditValue + Me.TBDosPol.EditValue))
            Next

        End If
    End Sub

    Private Sub TBTotDos_Leave(sender As Object, e As EventArgs) Handles TBTotDos.Leave
        Me.TBTotPsg.EditValue = Me.TBTotDos.EditValue * DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("Isi")
        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Me.GridView1.SetRowCellValue(i, "Qty", Me.TBTotDos.EditValue * Me.GridView1.GetRowCellValue(i, "IsiDlmDos"))
        Next
    End Sub

    Private Sub TBDosPol_Leave(sender As Object, e As EventArgs) Handles TBDosPol.Leave
        Me.TBPsgPol.EditValue = Me.TBDosPol.EditValue * DsMaster.Tables("M_BrgPOLk" & Gol).Select("ArtCode = '" & Me.SLUArtCode.EditValue & "'")(0).Item("Isi")
        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Me.GridView1.SetRowCellValue(i, "QtyPol", Me.TBDosPol.EditValue * Me.GridView1.GetRowCellValue(i, "IsiDlmDos"))
        Next
    End Sub

    Private Sub TBHBeli_Leave(sender As Object, e As EventArgs) Handles TBHBeli.Leave
        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            Me.GridView1.SetRowCellValue(i, "Harga", Me.GridView1.GetRowCellValue(i, "Qty") * Me.TBHBeli.EditValue)
        Next
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FPOBJLk_d(Me.GridView2.GetFocusedDataRow.Item("POID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FPOBJLk_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

End Class