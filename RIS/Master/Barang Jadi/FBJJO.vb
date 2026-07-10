Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FBJJO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim stsCari As Boolean = False
    Dim Kode As Guid
    Dim Pic(), PicLama() As Byte

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BJN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BJDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTBJ_s.Enabled = True

        Me.TBHSCode.Properties.ReadOnly = True
        Me.SLUStyle.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.SLUWrn.Properties.ReadOnly = True
        Me.SLUKat.Properties.ReadOnly = True
        Me.TBUkAw.Properties.ReadOnly = True
        Me.TBUkAkh.Properties.ReadOnly = True
        Me.TBArtCust.Properties.ReadOnly = True
        Me.SLUMerk.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.PGambar.Properties.ReadOnly = True

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCopy.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTBJ_s.Enabled = False

        Me.TBHSCode.Properties.ReadOnly = False
        Me.SLUStyle.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.SLUWrn.Properties.ReadOnly = False
        Me.SLUKat.Properties.ReadOnly = False
        Me.TBUkAw.Properties.ReadOnly = False
        Me.TBUkAkh.Properties.ReadOnly = False
        Me.TBArtCust.Properties.ReadOnly = False
        Me.SLUMerk.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.PGambar.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True
        Me.BCopy.Enabled = True

        Me.BVTBJ_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)
        cmsl.TableMappings.Add("Table", "M_StyleLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_StyleLUE")
        DsMaster.Tables("M_StyleLUE").Clear()
        cmsl.Fill(DsMaster, "M_StyleLUE")

        Me.SLUStyle.Properties.DataSource = DsMaster.Tables("M_StyleLUE")
        Me.SLUStyle.Properties.DisplayMember = "StyleID"
        Me.SLUStyle.Properties.ValueMember = "StyleID"

        cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")
        DsMaster.Tables("M_BrgMerkLUE").Clear()
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")

        Me.SLUMerk.Properties.DataSource = DsMaster.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "Nama"
        Me.SLUMerk.Properties.ValueMember = "MerkID"

        cmsl = New SqlDataAdapter("Select KatID,Nama From M_BrgKat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgKatLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgKatLUE")
        DsMaster.Tables("M_BrgKatLUE").Clear()
        cmsl.Fill(DsMaster, "M_BrgKatLUE")

        Me.SLUKat.Properties.DataSource = DsMaster.Tables("M_BrgKatLUE")
        Me.SLUKat.Properties.DisplayMember = "Nama"
        Me.SLUKat.Properties.ValueMember = "KatID"

        cmsl = New SqlDataAdapter("Select WrnID,Nama From M_BrgWrn Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgWrnLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgWrnLUE")
        DsMaster.Tables("M_BrgWrnLUE").Clear()
        cmsl.Fill(DsMaster, "M_BrgWrnLUE")

        Me.SLUWrn.Properties.DataSource = DsMaster.Tables("M_BrgWrnLUE")
        Me.SLUWrn.Properties.DisplayMember = "Nama"
        Me.SLUWrn.Properties.ValueMember = "WrnID"

        cmsl = New SqlDataAdapter("Select SatID,Nama,Isi From M_BrgSat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSatLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgSatLUE")
        DsMaster.Tables("M_BrgSatLUE").Clear()
        cmsl.Fill(DsMaster, "M_BrgSatLUE")

        Me.SLUSatID.Properties.DataSource = DsMaster.Tables("M_BrgSatLUE")
        Me.SLUSatID.Properties.DisplayMember = "Nama"
        Me.SLUSatID.Properties.ValueMember = "SatID"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select B.ArtCode,HSCode,ArtCust,ArtName,StyleID,B.MerkID,M.Nama As Merk,B.KatID,K.Nama As Kategori,B.WrnID,W.Nama As Warna,Ass,SatID,Isi, B.Aktif,B.InsDate,B.InsBy, B.UpdDate,B.UpdBy From M_Brg B Inner Join M_BrgMerk M On B.MerkID=M.MerkID Inner Join M_BrgKat K On B.KatID=K.KatID Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where Gol='Job Order'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgJO")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgJO")
        DsMaster.Tables("M_BrgJO").Clear()
        cmsl.Fill(DsMaster, "M_BrgJO")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgJO"
    End Sub

    Private Sub FBJJO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Barang Jadi"
    End Sub

    Private Sub FBJJO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBJ_e.Selected = True
    End Sub

    Private Sub BVTBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Barang Jadi"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Barang Jadi"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.SLUStyle.EditValue = ""
        Me.TBKode.EditValue = "--"
        Me.TBHSCode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.SLUWrn.EditValue = ""
        Me.SLUKat.EditValue = ""
        Me.TBUkAw.EditValue = 0
        Me.TBUkAkh.EditValue = 0
        Me.TBArtCust.EditValue = ""
        Me.SLUMerk.EditValue = ""
        Me.SLUSatID.EditValue = "P"
        Me.TBIsi.EditValue = 1
        Me.CEAktif.EditValue = True
        Me.PGambar.Image = Nothing

        Me.TBInfo.EditValue = ""

        LCUkAkh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Barang Jadi"

        LUE()

        Indicator = "200"
        Me.SLUStyle.EditValue = Me.GridView1.GetFocusedDataRow.Item("StyleID")
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtCode")
        Me.TBHSCode.EditValue = Me.GridView1.GetFocusedDataRow.Item("HSCode")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtName")
        Me.SLUWrn.EditValue = Me.GridView1.GetFocusedDataRow.Item("WrnID")
        Me.SLUKat.EditValue = Me.GridView1.GetFocusedDataRow.Item("KatID")
        Me.TBUkAw.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ass")
        Me.TBArtCust.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtCust")
        Me.SLUMerk.EditValue = Me.GridView1.GetFocusedDataRow.Item("MerkID")
        Me.SLUSatID.EditValue = Me.GridView1.GetFocusedDataRow.Item("SatID")
        Me.TBIsi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Isi")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmd As New SqlCommand("Select PicID,Picture From M_Image Where ID='" & Me.TBKode.EditValue & "'", koneksi)
        Dim newImage As Image
        Dim Reader As SqlClient.SqlDataReader
        With cmd
            .Connection = koneksi

            With koneksi
                .Open()
                Reader = cmd.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        Kode = Reader.Item(0)
                        Pic = Reader.Item(1)
                        PicLama = Reader.Item(1)
                    End While
                Else
                    Pic = Nothing
                    PicLama = Nothing
                End If
                Reader.Close()
                .Close()
            End With
        End With

        If Pic Is Nothing Then
            Me.PGambar.Image = Nothing
        Else
            Using ms As New MemoryStream(Pic, 0, Pic.Length)
                ms.Write(Pic, 0, Pic.Length)
                newImage = Image.FromStream(ms, True)
            End Using
            Me.PGambar.Image = newImage
        End If

        OpenControl()
        LCUkAkh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BCopy.Enabled = False
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Barang Jadi"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Article Name : " & Me.GridView1.GetFocusedDataRow.Item("ArtName") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            DelImage(Me.GridView1.GetFocusedDataRow.Item("ArtCode"))

            Dim cmSP As New SqlCommand("SPDelM_Brg")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("ArtCode")
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
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.SLUStyle.EditValue = "" Then
            XtraMessageBox.Show("Style Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUKat.EditValue = "" Then
            XtraMessageBox.Show("Kategori Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBNama.EditValue = "" Then
            XtraMessageBox.Show("Art Name Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUWrn.EditValue = "" Then
            XtraMessageBox.Show("Warna Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUSatID.EditValue = "" Then
            XtraMessageBox.Show("Satuan Harus Diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBUkAw.EditValue = 0 Then
            XtraMessageBox.Show("Ukuran Harus Lebih Dari Nol!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.PGambar.Image IsNot Nothing Then
            Dim ms As New MemoryStream()
            Me.PGambar.Image.Save(ms, Me.PGambar.Image.RawFormat)
            Pic = ms.GetBuffer
            ms.Close()
        End If

        Select Case Indicator

            Case 100
                If CDec(Me.TBUkAkh.EditValue) < CDec(Me.TBUkAw.EditValue) Then
                    XtraMessageBox.Show("Ukuran Akhir Harus Lebih Besar Atau Sama dengan Ukuran Awal!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim x As Integer

                Dim i : For i = CDec(Me.TBUkAw.EditValue) To CDec(Me.TBUkAkh.EditValue)

                    Dim cmSP As New SqlCommand("SPInsM_Brg")
                    cmSP.CommandType = CommandType.StoredProcedure

                    With cmSP
                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                        .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                        .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                        .Parameters.Add("@ArtCust", SqlDbType.VarChar).Value = Me.TBArtCust.EditValue
                        .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                        .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                        .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                        .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = "-"
                        .Parameters.Add("@Urut", SqlDbType.VarChar).Value = ""
                        .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue
                        .Parameters.Add("@AssID", SqlDbType.VarChar).Value = ""
                        .Parameters.Add("@Ass", SqlDbType.VarChar).Value = i
                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSatID.EditValue
                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.TBIsi.EditValue
                        .Parameters.Add("@Lcns", SqlDbType.Bit).Value = False
                        .Parameters.Add("@Lux", SqlDbType.Bit).Value = False
                        .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.LBGole.Text.Substring(4, Me.LBGole.Text.Length - 4)
                        .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                        .Parameters.Add("@Return", SqlDbType.Int)
                        .Parameters("@Return").Direction = ParameterDirection.Output
                        .Connection = koneksi

                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If Me.PGambar.Image IsNot Nothing Then
                            InsImage(Me.TBKode.EditValue, "Barang Jadi", Pic)
                        End If
                    End With
                Next

                Try

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    Else
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Brg")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@ArtCust", SqlDbType.VarChar).Value = Me.TBArtCust.EditValue
                    .Parameters.Add("@Brand", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = "-"
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@Urut", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue
                    .Parameters.Add("@AssID", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@Ass", SqlDbType.VarChar).Value = Me.TBUkAw.EditValue
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSatID.EditValue
                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.SLUStyle.EditValue
                    .Parameters.Add("@Lcns", SqlDbType.Bit).Value = False
                    .Parameters.Add("@Lux", SqlDbType.Bit).Value = False
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.LBGole.Text.Substring(4, Me.LBGole.Text.Length - 4)
                    .Parameters.Add("@Aktif", SqlDbType.Bit).Value = Me.CEAktif.EditValue
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

                        If Me.PGambar.Image IsNot Nothing Then
                            If PicLama Is Nothing Then
                                InsImage(Me.TBKode.EditValue, "Barang Jadi", Pic)
                            Else
                                UpImage(Kode, Me.TBKode.EditValue, "Barang Jadi", Pic)
                            End If
                        End If

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
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        stsCari = True

        Dim frm As New FSearch("M_BrgJO", "", "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            Me.TBKode.EditValue = dataTrans.Item("Kode").ToString
            Me.TBHSCode.EditValue = dataTrans.Item("HSCode").ToString
            Me.TBNama.EditValue = dataTrans.Item("Nama").ToString
            Me.SLUStyle.EditValue = dataTrans.Item("StyleID").ToString
            Me.SLUKat.EditValue = dataTrans.Item("KatID").ToString
            Me.SLUWrn.EditValue = dataTrans.Item("WrnID").ToString
            Me.TBUkAw.EditValue = dataTrans.Item("Ass").ToString
            Me.TBArtCust.EditValue = dataTrans.Item("ArtCust").ToString
            Me.SLUMerk.EditValue = dataTrans.Item("MerkID").ToString
            Me.SLUSatID.EditValue = dataTrans.Item("SatID").ToString
            Me.TBIsi.EditValue = dataTrans.Item("Isi").ToString
        End If

    End Sub

    Private Sub SLUSatID_Leave(sender As Object, e As EventArgs) Handles SLUSatID.Leave
        Me.TBIsi.EditValue = DsMaster.Tables("M_BrgSatLUE").Select("SatID = '" & Me.SLUSatID.EditValue & "'")(0).Item("Isi")
    End Sub

    Private Sub TBHSCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBHSCode.KeyPress, TBNama.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
   
End Class