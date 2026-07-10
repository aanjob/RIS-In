Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FBJLk

    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1), Gol As String
    Dim stsCari As Boolean = False
    Dim Kode As Guid
    Dim Pic(), PicLama() As Byte
    Dim ImageLama As Image
    Dim msLama As New MemoryStream()
    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.LBGole.Text = "    " & Golongan
        Me.LBGols.Text = "    " & Golongan
        Gol = Golongan

        If Gol = "Own" Then
            Me.LESubJns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESISubJns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.LESubJns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESISubJns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BJN"), Boolean)
        Me.BVTDOHis.Enabled = CType(TcodeCollection.Item("BJDOHis"), Boolean)
        Me.BVTHS.Enabled = CType(TcodeCollection.Item("BJHarStok"), Boolean)
        Me.BVTJualHis.Enabled = CType(TcodeCollection.Item("BJJualHis"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTBJ_s.Enabled = True

        Me.SLUGrup.Properties.ReadOnly = True
        Me.SLUSubGrup.Properties.ReadOnly = True
        Me.SLUStyle.Properties.ReadOnly = True
        Me.TBHSCode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.SLUMerk.Properties.ReadOnly = True
        Me.SLUKat.Properties.ReadOnly = True
        Me.SLUJns.Properties.ReadOnly = True
        Me.CBOSubJns.Properties.ReadOnly = True
        Me.SLUUrut.Properties.ReadOnly = True
        Me.SLUWrn.Properties.ReadOnly = True
        Me.SLUAss.Properties.ReadOnly = True
        Me.CELicense.Properties.ReadOnly = True
        Me.CELux.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.PGambar.Properties.ReadOnly = True
        Me.TBUkAw.Properties.ReadOnly = True
        Me.TBUkAkh.Properties.ReadOnly = True

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
        Me.BVTDOHis.Enabled = False
        Me.BVTHS.Enabled = False
        Me.BVTJualHis.Enabled = False

        Me.SLUGrup.Properties.ReadOnly = False
        Me.SLUSubGrup.Properties.ReadOnly = False
        Me.SLUStyle.Properties.ReadOnly = False
        Me.TBHSCode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.SLUMerk.Properties.ReadOnly = False
        Me.SLUKat.Properties.ReadOnly = False
        Me.SLUJns.Properties.ReadOnly = False
        Me.CBOSubJns.Properties.ReadOnly = False
        Me.SLUUrut.Properties.ReadOnly = False
        Me.SLUWrn.Properties.ReadOnly = False
        Me.CELicense.Properties.ReadOnly = False
        Me.CELux.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.PGambar.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True
        Me.BCopy.Enabled = True

        Me.CEFixAss.EditValue = True
        Me.BVTBJ_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()

        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        cmsl = New SqlDataAdapter("Select StyleID,Nama From M_Style", koneksi)
        cmsl.TableMappings.Add("Table", "M_StyleLUE")
        Try
            DsMaster.Tables("M_StyleLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_StyleLUE")

        Me.SLUStyle.Properties.DataSource = DsMaster.Tables("M_StyleLUE")
        Me.SLUStyle.Properties.DisplayMember = "StyleID"
        Me.SLUStyle.Properties.ValueMember = "StyleID"

        cmsl = New SqlDataAdapter("Select KatID,Nama From M_BrgKat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgKatLUE")
        Try
            DsMaster.Tables("M_BrgKatLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgKatLUE")

        Me.SLUKat.Properties.DataSource = DsMaster.Tables("M_BrgKatLUE")
        Me.SLUKat.Properties.DisplayMember = "Nama"
        Me.SLUKat.Properties.ValueMember = "KatID"

        cmsl = New SqlDataAdapter("Select JnsID,Nama From M_BrgJns Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgJnsLUE")
        Try
            DsMaster.Tables("M_BrgJnsLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgJnsLUE")

        Me.SLUJns.Properties.DataSource = DsMaster.Tables("M_BrgJnsLUE")
        Me.SLUJns.Properties.DisplayMember = "Nama"
        Me.SLUJns.Properties.ValueMember = "JnsID"

        cmsl = New SqlDataAdapter("Select WrnID,Nama From M_BrgWrn Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgWrnLUE")
        Try
            DsMaster.Tables("M_BrgWrnLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgWrnLUE")

        Me.SLUWrn.Properties.DataSource = DsMaster.Tables("M_BrgWrnLUE")
        Me.SLUWrn.Properties.DisplayMember = "Nama"
        Me.SLUWrn.Properties.ValueMember = "WrnID"

    End Sub

    Public Sub CekFixAss()
        If Me.CEFixAss.EditValue = True Then
            Me.TBUkAw.Properties.ReadOnly = True
            Me.TBUkAkh.Properties.ReadOnly = True
            Me.SLUAss.Properties.ReadOnly = False
            Me.GridControl2.EmbeddedNavigator.Buttons.Append.Enabled = True

        Else
            Me.TBUkAw.Properties.ReadOnly = False
            Me.TBUkAkh.Properties.ReadOnly = False
            Me.SLUAss.Properties.ReadOnly = True
            Me.GridControl2.EmbeddedNavigator.Buttons.Append.Enabled = False

            Me.TBSat.EditValue = "P"
            Me.TBIsi.EditValue = 1
        End If
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ArtCode,HSCode,ArtName,StyleID,B.MerkID,M.Nama As Merk,B.KatID,K.Nama As Kat,B.JnsID,J.Nama As Jns, SubJns,Urut,B.WrnID,W.Nama As Warna,B.AssID,A.Ass,B.SatID,B.Isi,License,Luxury,SubGrup,B.Grup,Gol,B.Aktif,B.InsDate,B.InsBy,B.UpdDate, B.UpdBy From M_Brg B Inner Join M_BrgMerk M On B.MerkID=M.MerkID Inner Join M_BrgKat K On B.KatID=K.KatID Inner Join M_BrgJns J On B.JnsID=J.JnsID Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_BrgAss A On B.AssID=A.AssID Where Gol='" & Gol & "' and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "M_Brg" & Gol)
        Try
            DsMaster.Tables("M_Brg" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Brg" & Gol)

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Brg" & Gol
    End Sub

    Public Sub Del()
        Dim cmSP1 As New SqlCommand("SPDelM_Brg")
        cmSP1.CommandType = CommandType.StoredProcedure
        'Dim x As Integer

        With cmSP1
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            With koneksi
                .Open()
                cmSP1.ExecuteNonQuery()
                'x = cmSP.Parameters("@Return").Value
                .Close()
            End With
        End With
    End Sub

    Public Sub FillAss()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AssID,Ass,SatID,Isi From M_BrgAss Where MerkID='" & Me.SLUMerk.EditValue & "' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgAssLUE")
        Try
            DsMaster.Tables("M_BrgAssLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgAssLUE")

        Me.SLUAss.Properties.DataSource = DsMaster.Tables("M_BrgAssLUE")
        Me.SLUAss.Properties.DisplayMember = "Ass"
        Me.SLUAss.Properties.ValueMember = "AssID"
    End Sub

    Public Sub FillAssDtl()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Uk,Qty From M_BrgAssDtl Where AssID='" & Me.SLUAss.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgAssDtlLUE")
        Try
            DsMaster.Tables("M_BrgAssDtlLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgAssDtlLUE")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_BrgAssDtlLUE"
    End Sub

    Public Sub FillUrut(Urut As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Urut From M_BrgUrut Where Urut Not In (Select Urut From M_Brg Where MerkID='" & Me.SLUMerk.EditValue & "' and KatID = '" & Me.SLUKat.EditValue & "' and JnsID ='" & Me.SLUJns.EditValue & "' and Urut <>'" & Urut & "') Order By Urut asc", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgUrut")
        Try
            DsMaster.Tables("M_BrgUrut").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgUrut")

        Me.SLUUrut.Properties.DataSource = DsMaster.Tables("M_BrgUrut")
        Me.SLUUrut.Properties.DisplayMember = "Urut"
        Me.SLUUrut.Properties.ValueMember = "Urut"
    End Sub

    Private Sub FBJLk_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Barang Jadi"
    End Sub

    Private Sub FBJLk_Load(sender As Object, e As EventArgs) Handles Me.Load
        LockControl()
        Me.BVTBJ_e.Selected = True
    End Sub

    Private Sub BVTBJ_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBJ_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Barang Jadi"

        FillDt()
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BJEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BJDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Barang Jadi"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.SLUGrup.EditValue = ""
        Me.SLUSubGrup.EditValue = ""
        Me.SLUStyle.EditValue = ""
        Me.TBKode.EditValue = ""
        Me.TBHSCode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.SLUAss.EditValue = ""
        Me.SLUMerk.EditValue = ""
        Me.SLUKat.EditValue = ""
        Me.SLUJns.EditValue = ""
        Me.CBOSubJns.EditValue = ""
        Me.SLUUrut.EditValue = ""
        Me.SLUWrn.EditValue = ""
        Me.TBSat.EditValue = ""
        Me.TBIsi.EditValue = ""
        Me.PGambar.Image = Nothing
        Me.CELux.EditValue = False
        Me.CELicense.EditValue = False
        Me.CEAktif.EditValue = True
        Me.CEFixAss.EditValue = True
        Me.TBInfo.EditValue = ""
        Me.TBUkAw.EditValue = 0.0
        Me.TBUkAkh.EditValue = 0.0
        stsCari = False

        Try
            DsMaster.Tables("M_BrgMerkLUE").Clear()

        Catch ex As Exception

        End Try
        CekFixAss()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Barang Jadi"

        LUE()

        Indicator = "200"
        Me.SLUGrup.EditValue = Me.GridView1.GetFocusedDataRow.Item("Grup")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SubGrup From M_BrgSubGrup Where Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "SubGrupLUE")
        cmsl.Fill(DsMaster, "SubGrupLUE")
        Try
            DsMaster.Tables("SubGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "SubGrupLUE")

        Me.SLUSubGrup.Properties.DataSource = DsMaster.Tables("SubGrupLUE")
        Me.SLUSubGrup.Properties.DisplayMember = "SubGrup"
        Me.SLUSubGrup.Properties.ValueMember = "SubGrup"

        Me.SLUSubGrup.EditValue = Me.GridView1.GetFocusedDataRow.Item("SubGrup")

        Me.TBHSCode.EditValue = Me.GridView1.GetFocusedDataRow.Item("HSCode")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtName")

        cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Grup = '" & Me.SLUGrup.EditValue & "' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        Try
            DsMaster.Tables("M_BrgMerkLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")

        Me.SLUMerk.Properties.DataSource = DsMaster.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "Nama"
        Me.SLUMerk.Properties.ValueMember = "MerkID"


        Me.SLUMerk.EditValue = Me.GridView1.GetFocusedDataRow.Item("MerkID")

        FillAss()

        Me.SLUStyle.EditValue = Me.GridView1.GetFocusedDataRow.Item("StyleID")
        Me.SLUKat.EditValue = Me.GridView1.GetFocusedDataRow.Item("KatID")
        Me.SLUJns.EditValue = Me.GridView1.GetFocusedDataRow.Item("JnsID")
        Me.CBOSubJns.EditValue = Me.GridView1.GetFocusedDataRow.Item("SubJns")

        If stsCari = False Then
            FillUrut(Me.GridView1.GetFocusedDataRow.Item("Urut"))
        End If

        Me.SLUUrut.EditValue = Me.GridView1.GetFocusedDataRow.Item("Urut")
        Me.SLUWrn.EditValue = Me.GridView1.GetFocusedDataRow.Item("WrnID")
        Me.SLUAss.EditValue = Me.GridView1.GetFocusedDataRow.Item("AssID")

        FillAssDtl()

        Me.TBSat.EditValue = Me.GridView1.GetFocusedDataRow.Item("SatID")
        Me.TBIsi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Isi")
        Me.CELux.EditValue = Me.GridView1.GetFocusedDataRow.Item("Luxury")
        Me.CELicense.EditValue = Me.GridView1.GetFocusedDataRow.Item("License")
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
                msLama = ms
                ImageLama = newImage
            End Using
            Me.PGambar.Image = newImage
        End If

        OpenControl()
        Me.BCopy.Enabled = False

        Me.SLUMerk.Properties.ReadOnly = True
        Me.SLUKat.Properties.ReadOnly = True
        Me.SLUJns.Properties.ReadOnly = True
        Me.SLUUrut.Properties.ReadOnly = True
        Me.SLUWrn.Properties.ReadOnly = True
        Me.SLUAss.Properties.ReadOnly = True

        CekFixAss()
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtCode")

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

        If Gol = "Own" Then
            If Me.CBOSubJns.EditValue = "" Or IsDBNull(Me.CBOSubJns.EditValue) Then
                XtraMessageBox.Show("Sub Jenis Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Dim Art As String = Me.SLUMerk.EditValue & Me.SLUKat.EditValue & Me.SLUJns.EditValue & "-" & Me.SLUUrut.EditValue & "-" & Me.SLUWrn.EditValue & "-"
        If Me.SLUGrup.EditValue = "" Or IsDBNull(Me.SLUGrup.EditValue) Then
            XtraMessageBox.Show("Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If Me.SLUSubGrup.EditValue = "" Or IsDBNull(Me.SLUSubGrup.EditValue) Then
        '    XtraMessageBox.Show("Sub Group Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If Me.SLUStyle.EditValue = "" Or IsDBNull(Me.SLUStyle.EditValue) Then
            XtraMessageBox.Show("Style Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUUrut.EditValue = "" Or IsDBNull(Me.SLUUrut.EditValue) Then
            XtraMessageBox.Show("No Urut Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.PGambar.Image IsNot Nothing Then
            Dim ms As New MemoryStream()

            If Object.ReferenceEquals(ImageLama, Me.PGambar.EditValue) Then
                ms = msLama
            Else
                Me.PGambar.Image.Save(ms, Me.PGambar.Image.RawFormat)
                Pic = ms.GetBuffer
                ms.Close()
            End If
        End If

        Dim x As Integer

        Select Case Indicator
            Case 100
                If Me.CEFixAss.EditValue = True Then
                    If Me.TBSat.EditValue = "P" Then
                        Dim cmSP As New SqlCommand("SPInsM_Brg")
                        cmSP.CommandType = CommandType.StoredProcedure
                        x = 0

                        With cmSP
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                            .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                            .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                            .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                            .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                            .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.CBOSubJns.EditValue
                            .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SLUUrut.EditValue
                            .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue
                            .Parameters.Add("@AssID", SqlDbType.VarChar).Value = Me.SLUAss.EditValue
                            .Parameters.Add("@Ass", SqlDbType.VarChar).Value = Me.SLUAss.Text
                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.TBSat.EditValue
                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.TBIsi.EditValue
                            .Parameters.Add("@Lcns", SqlDbType.Bit).Value = Me.CELicense.EditValue
                            .Parameters.Add("@Lux", SqlDbType.Bit).Value = Me.CELux.EditValue
                            .Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
                            .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
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

                                If Me.PGambar.Image IsNot Nothing Then
                                    InsImage(Me.TBKode.EditValue, "Barang Jadi", Pic)
                                End If

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
                        End With

                    Else
                        'Input Dos

                        Dim cmSP As New SqlCommand("SPInsM_Brg")
                        cmSP.CommandType = CommandType.StoredProcedure
                        x = 0

                        With cmSP
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                            .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                            .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                            .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                            .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                            .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.CBOSubJns.EditValue
                            .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SLUUrut.EditValue
                            .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue
                            .Parameters.Add("@AssID", SqlDbType.VarChar).Value = Me.SLUAss.EditValue
                            .Parameters.Add("@Ass", SqlDbType.VarChar).Value = Me.SLUAss.Text
                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.TBSat.EditValue
                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.TBIsi.EditValue
                            .Parameters.Add("@Lcns", SqlDbType.Bit).Value = Me.CELicense.EditValue
                            .Parameters.Add("@Lux", SqlDbType.Bit).Value = Me.CELux.EditValue
                            .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                            .Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
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

                                Dim i : For i = 0 To GridView2.RowCount - 1
                                    If Not IsDBNull(Me.GridView2.GetRowCellValue(i, "Size")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsM_Brg")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Art & Me.GridView2.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                            .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                                            .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                                            .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                                            .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                                            .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.CBOSubJns.EditValue
                                            .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SLUUrut.EditValue
                                            .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue

                                            Dim AssID As String
                                            Dim comm As New SqlCommand("Select AssID From M_BrgAss Where Ass='" & Me.GridView2.GetRowCellValue(i, "Uk") & "' and MerkID='" & Me.SLUMerk.EditValue & "'", koneksi)

                                            With koneksi
                                                .Open()
                                                AssID = comm.ExecuteScalar()
                                                .Close()
                                            End With
                                            .Parameters.Add("@AssID", SqlDbType.VarChar).Value = AssID
                                            .Parameters.Add("@Ass", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = "P"
                                            .Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                                            .Parameters.Add("@Lcns", SqlDbType.Bit).Value = Me.CELicense.EditValue
                                            .Parameters.Add("@Lux", SqlDbType.Bit).Value = Me.CELux.EditValue
                                            .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                                            .Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
                                            .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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
                                    End If
                                Next

                                If Me.PGambar.Image IsNot Nothing Then
                                    InsImage(Me.TBKode.EditValue, "Barang Jadi", Pic)
                                End If

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
                        End With
                    End If

                Else
                    'Input UnFixAss

                    Dim i : For i = CDec(Me.TBUkAw.EditValue) To CDec(Me.TBUkAkh.EditValue)
                        If Not IsDBNull(i) Then
                            Dim cmSPDtl As New SqlCommand("SPInsM_Brg")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Art & i
                                .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                                .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                                .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                                .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                                .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                                .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.CBOSubJns.EditValue
                                .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SLUUrut.EditValue
                                .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue

                                Dim AssID As String
                                Dim comm As New SqlCommand("Select AssID From M_BrgAss Where Ass='" & i & "' and MerkID='" & Me.SLUMerk.EditValue & "'", koneksi)

                                With koneksi
                                    .Open()
                                    AssID = comm.ExecuteScalar()
                                    .Close()
                                End With

                                .Parameters.Add("@AssID", SqlDbType.VarChar).Value = AssID
                                .Parameters.Add("@Ass", SqlDbType.VarChar).Value = i
                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = "P"
                                .Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                                .Parameters.Add("@Lcns", SqlDbType.Bit).Value = Me.CELicense.EditValue
                                .Parameters.Add("@Lux", SqlDbType.Bit).Value = Me.CELux.EditValue
                                .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                                .Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
                                .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                                .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi
                            End With

                            Try
                                With koneksi
                                    .Open()
                                    cmSPDtl.ExecuteNonQuery()
                                    x = cmSPDtl.Parameters("@Return").Value
                                    .Close()
                                End With

                                'MsgBox(Art & i & " Status : " & x)
                            Catch ex As Exception
                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try

                        End If
                    Next

                    If Me.PGambar.Image IsNot Nothing Then
                        InsImage(Me.TBKode.EditValue, "Barang Jadi", Pic)
                    End If

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    Else
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                End If

            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Brg")
                cmSP.CommandType = CommandType.StoredProcedure
                x = 0

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@HSCode", SqlDbType.VarChar).Value = Me.TBHSCode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.SLUStyle.EditValue
                    .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                    .Parameters.Add("@KatID", SqlDbType.VarChar).Value = Me.SLUKat.EditValue
                    .Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Me.SLUJns.EditValue
                    .Parameters.Add("@SubJns", SqlDbType.VarChar).Value = Me.CBOSubJns.EditValue
                    .Parameters.Add("@Urut", SqlDbType.VarChar).Value = Me.SLUUrut.EditValue
                    .Parameters.Add("@WrnID", SqlDbType.VarChar).Value = Me.SLUWrn.EditValue
                    .Parameters.Add("@AssID", SqlDbType.VarChar).Value = Me.SLUAss.EditValue
                    .Parameters.Add("@Ass", SqlDbType.VarChar).Value = Me.SLUAss.Text
                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.TBSat.EditValue
                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.TBIsi.EditValue
                    .Parameters.Add("@Lcns", SqlDbType.Bit).Value = Me.CELicense.EditValue
                    .Parameters.Add("@Lux", SqlDbType.Bit).Value = Me.CELux.EditValue
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.SLUGrup.EditValue
                    .Parameters.Add("@SubGrup", SqlDbType.VarChar).Value = Me.SLUSubGrup.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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

    Dim ColArticleCode As New Collection

    Private Sub SLUGrup_Leave(sender As Object, e As EventArgs) Handles SLUGrup.Leave
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Grup = '" & Me.SLUGrup.EditValue & "' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        Try
            DsMaster.Tables("M_BrgMerkLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")

        Me.SLUMerk.Properties.DataSource = DsMaster.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "Nama"
        Me.SLUMerk.Properties.ValueMember = "MerkID"

        cmsl = New SqlDataAdapter("Select SubGrup From M_BrgSubGrup Where Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "SubGrupLUE")
        cmsl.Fill(DsMaster, "SubGrupLUE")
        Try
            DsMaster.Tables("SubGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "SubGrupLUE")

        Me.SLUSubGrup.Properties.DataSource = DsMaster.Tables("SubGrupLUE")
        Me.SLUSubGrup.Properties.DisplayMember = "SubGrup"
        Me.SLUSubGrup.Properties.ValueMember = "SubGrup"
    End Sub

    Private Sub SLUMerk_EditValueChanged(sender As Object, e As EventArgs) Handles SLUMerk.EditValueChanged
        If Me.ColArticleCode.Contains("Merk") Then
            Me.ColArticleCode.Remove("Merk")
        End If
        Me.ColArticleCode.Add(Me.SLUMerk.EditValue, "Merk")

        Me.SLUAss_Leave(sender, e)
    End Sub

    Private Sub SLUKat_EditValueChanged(sender As Object, e As EventArgs) Handles SLUKat.EditValueChanged
        If Me.ColArticleCode.Contains("Kategori") Then
            Me.ColArticleCode.Remove("Kategori")
        End If
        Me.ColArticleCode.Add(Me.SLUKat.EditValue, "Kategori")

        Me.SLUAss_Leave(sender, e)
    End Sub

    Private Sub SLUJns_EditValueChanged(sender As Object, e As EventArgs) Handles SLUJns.EditValueChanged
        If Me.ColArticleCode.Contains("Jenis") Then
            Me.ColArticleCode.Remove("Jenis")
        End If
        Me.ColArticleCode.Add(Me.SLUJns.EditValue, "Jenis")

        Me.SLUAss_Leave(sender, e)
    End Sub

    Private Sub SLUJns_Leave(sender As Object, e As EventArgs) Handles SLUJns.Leave
        If stsCari = False Then
            FillUrut("")
        End If
    End Sub

    Private Sub SLUUrut_EditValueChanged(sender As Object, e As EventArgs) Handles SLUUrut.EditValueChanged
        If Me.ColArticleCode.Contains("Urut") Then
            Me.ColArticleCode.Remove("Urut")
        End If
        Me.ColArticleCode.Add(Me.SLUUrut.EditValue, "Urut")

        Me.SLUAss_Leave(sender, e)
    End Sub

    Private Sub SLUWrn_EditValueChanged(sender As Object, e As EventArgs) Handles SLUWrn.EditValueChanged
        If Me.ColArticleCode.Contains("Warna") Then
            Me.ColArticleCode.Remove("Warna")
        End If
        Me.ColArticleCode.Add(Me.SLUWrn.EditValue, "Warna")

        Me.SLUAss_Leave(sender, e)
    End Sub

    Private Sub SLUAss_EditValueChanged(sender As Object, e As EventArgs) Handles SLUAss.EditValueChanged
        If Me.ColArticleCode.Contains("Ass") Then
            Me.ColArticleCode.Remove("Ass")
        End If
        Me.ColArticleCode.Add(Me.SLUAss.Text, "Ass")

        Me.SLUAss_Leave(sender, e)
    End Sub

    Private Sub SLUMerk_Leave(sender As Object, e As EventArgs) Handles SLUMerk.Leave
        FillAss()
    End Sub

    Private Sub SLUAss_Leave(sender As Object, e As EventArgs) Handles SLUAss.Leave
        Try
            If Me.SLUAss.EditValue <> "" Then
                FillAssDtl()

                Me.TBSat.EditValue = DsMaster.Tables("M_BrgAssLUE").Select("AssID = '" & Me.SLUAss.EditValue & "'")(0).Item("SatID")
                Me.TBIsi.EditValue = DsMaster.Tables("M_BrgAssLUE").Select("AssID = '" & Me.SLUAss.EditValue & "'")(0).Item("Isi")
            End If

            Me.TBKode.EditValue = Me.ColArticleCode.Item("Merk") & "" & Me.ColArticleCode.Item("Kategori") & "" & Me.ColArticleCode.Item("Jenis") & "-" & Me.ColArticleCode.Item("Urut") & "-" & Me.ColArticleCode.Item("Warna") & "-" & Me.ColArticleCode.Item("Ass")
        Catch ex As Exception

        End Try
    End Sub


    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        stsCari = True

        Me.SLUUrut.Properties.ReadOnly = True
        Me.SLUMerk.Properties.ReadOnly = True
        Me.SLUKat.Properties.ReadOnly = True
        Me.SLUJns.Properties.ReadOnly = True

        Dim frm As New FSearch("M_Brg", Gol, "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            Me.TBKode.EditValue = dataTrans.Item("Kode").ToString
            Me.TBNama.EditValue = dataTrans.Item("Nama").ToString
            Me.SLUStyle.EditValue = dataTrans.Item("StyleID").ToString
            Me.SLUGrup.EditValue = dataTrans.Item("Grup").ToString

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select SubGrup From M_BrgSubGrup Where Grup='" & Me.SLUGrup.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "SubGrupLUE")
            cmsl.Fill(DsMaster, "SubGrupLUE")
            Try
                DsMaster.Tables("SubGrupLUE").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "SubGrupLUE")

            Me.SLUSubGrup.Properties.DataSource = DsMaster.Tables("SubGrupLUE")
            Me.SLUSubGrup.Properties.DisplayMember = "SubGrup"
            Me.SLUSubGrup.Properties.ValueMember = "SubGrup"

            Me.SLUSubGrup.EditValue = dataTrans.Item("SubGrup").ToString

            cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
            Try
                DsMaster.Tables("M_BrgMerkLUE").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_BrgMerkLUE")

            Me.SLUMerk.Properties.DataSource = DsMaster.Tables("M_BrgMerkLUE")
            Me.SLUMerk.Properties.DisplayMember = "Nama"
            Me.SLUMerk.Properties.ValueMember = "MerkID"

            Me.SLUMerk.EditValue = dataTrans.Item("MerkID").ToString
            Me.SLUKat.EditValue = dataTrans.Item("KatID").ToString
            Me.SLUJns.EditValue = dataTrans.Item("JnsID").ToString
            Me.CBOSubJns.EditValue = dataTrans.Item("SubJns").ToString

            cmsl = New SqlDataAdapter("Select '" & dataTrans.Item("Urut").ToString & "' As Urut", koneksi)

            cmsl.TableMappings.Add("Table", "M_BrgUrut")
            Try
                DsMaster.Tables("M_BrgUrut").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_BrgUrut")

            Me.SLUUrut.Properties.DataSource = DsMaster.Tables("M_BrgUrut")
            Me.SLUUrut.Properties.DisplayMember = "Urut"
            Me.SLUUrut.Properties.ValueMember = "Urut"

            Me.SLUUrut.EditValue = dataTrans.Item("Urut").ToString
            Me.SLUWrn.EditValue = dataTrans.Item("WrnID").ToString

            FillAss()

            Me.SLUAss.EditValue = dataTrans.Item("AssID").ToString
            FillAssDtl()

            Me.TBSat.EditValue = dataTrans.Item("SatID").ToString
            Me.TBIsi.EditValue = dataTrans.Item("Isi").ToString
            Me.CELicense.EditValue = CBool(dataTrans.Item("License").ToString)
            Me.CELux.EditValue = CBool(dataTrans.Item("Luxury").ToString)
        End If

    End Sub

    Private Sub BVTDOHis_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTDOHis.ItemPressed
        Me.TBDOArtCode.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtCode")
        Me.TBDOArtName.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtName")
        Me.TBDOIsi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Isi")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select H.DOID,Tanggal, C.Cabang As CabAs, G.Nama as GudangAs,(Select Cabang From M_Cab where CabID=H.CabIDTj) as CabTj,(Select Nama From M_Gudang where GdID=H.GdIDTj) as GudangTj,Dos, Psg From T_DO H Inner Join T_DODtl D On H.DOID=D.DOID Inner Join M_Cab C On H.CabIDAs=C.CabID Inner Join M_Gudang G On H.GdIDAs=G.GdID Where D.ArtCode='" & Me.TBDOArtCode.EditValue & "' and (H.CabIDAs In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") Or H.CabIDTj In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & "))", koneksi)

        cmsl.TableMappings.Add("Table", "DOHis")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "DOHis")
        DsMaster.Tables("DOHis").Clear()
        cmsl.Fill(DsMaster, "DOHis")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "DOHis"
    End Sub
    Private Sub BVTHS_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTHS.ItemPressed
        Me.TBHSArtCode.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtCode")
        Me.TBHSArtName.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtName")
        Me.TBHSIsi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Isi")

        Dim cmsl As SqlDataAdapter
        If Gol = "Promosi" Then
            cmsl = New SqlDataAdapter("Select * From (Select G.GdID, Nama As Gd,[dbo].[fcGetStokBJ]('" & Me.TBHSArtCode.EditValue & "',G.GdID,'" & System.DateTime.Now & "') As Stok From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ")) as x Where Stok>0", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select * From (Select G.GdID, Nama As Gd,[dbo].[fcGetStokBJ]('" & Me.TBHSArtCode.EditValue & "',G.GdID,'" & System.DateTime.Now & "') As Stok From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ") and Gol='" & Gol & "') as x Where Stok>0", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "Stock" & Gol)
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "Stock" & Gol)
        DsMaster.Tables("Stock" & Gol).Clear()
        cmsl.Fill(DsMaster, "Stock" & Gol)

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "Stock" & Gol


        cmsl = New SqlDataAdapter("Select stsHarga,Jenis,'DPJ' As JnsHarga,Tanggal,Harga,HargaCBP,DiscOB From M_BrgHarga where Aktif='TRUE' and ArtCode='" & Me.TBHSArtCode.EditValue & "' Union All Select stsHarga,Jenis,'FRANGKO' As JnsHarga,Tanggal,Harga+((Select Ongkir From M_Perusahaan)*Isi),HargaCBP,DiscOB From M_BrgHarga where Aktif='TRUE' and ArtCode='" & Me.TBHSArtCode.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "Harga")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "Harga")
        DsMaster.Tables("Harga").Clear()
        cmsl.Fill(DsMaster, "Harga")

        Me.GridControl6.DataSource = DsMaster
        Me.GridControl6.DataMember = "Harga"

        cmsl = New SqlDataAdapter("Select Uk,Qty From M_BrgAssDtl where AssID='" & Me.GridView1.GetFocusedDataRow.Item("AssID") & "'", koneksi)

        cmsl.TableMappings.Add("Table", "Ass")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "Ass")
        DsMaster.Tables("Ass").Clear()
        cmsl.Fill(DsMaster, "Ass")

        Me.GridControl7.DataSource = DsMaster
        Me.GridControl7.DataMember = "Ass"
    End Sub

    Private Sub BVTJualHis_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTJualHis.ItemPressed
        Me.TBJualArtCode.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtCode")
        Me.TBJualArtName.EditValue = Me.GridView1.GetFocusedDataRow.Item("ArtName")
        Me.TBJualIsi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Isi")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select H.JualID,Kode,Tanggal, S.Nama As Sal, G.Nama as Gd,C.Nama As Cust,Dos,Psg From T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join M_Sales S On H.SalID=S.SalID Inner Join M_Gudang G On H.GdID=G.GdID Inner Join M_Cust C On H.CustID=C.CustID Where D.ArtCode='" & Me.TBJualArtCode.EditValue & "' and H.CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "JualHis")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "JualHis")
        DsMaster.Tables("JualHis").Clear()
        cmsl.Fill(DsMaster, "JualHis")

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "JualHis"
    End Sub

    Private Sub CEFixAss_EditValueChanged(sender As Object, e As EventArgs) Handles CEFixAss.EditValueChanged
        CekFixAss()
    End Sub

    Private Sub TBHSCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBHSCode.KeyPress, TBNama.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.Handled = True
        End If
    End Sub

    Private Sub BVTBJ_s_SelectedChanged(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBJ_s.SelectedChanged

    End Sub
End Class