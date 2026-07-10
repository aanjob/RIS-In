Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FSupp
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SuppN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("SuppEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("SuppDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTSupp_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.SPTop.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.MAlamat.Properties.ReadOnly = True
        Me.SLUKota.Properties.ReadOnly = True
        Me.TBNegara.Properties.ReadOnly = True
        Me.TBTelp.Properties.ReadOnly = True
        Me.TBHp.Properties.ReadOnly = True
        Me.TBFax.Properties.ReadOnly = True
        Me.TBEmail.Properties.ReadOnly = True
        Me.TBNPWP.Properties.ReadOnly = True
        Me.TBNamaFP.Properties.ReadOnly = True
        Me.MAlamatFP.Properties.ReadOnly = True
        Me.TBKotaFP.Properties.ReadOnly = True
        Me.TBKdPosFP.Properties.ReadOnly = True
        Me.TBPropFP.Properties.ReadOnly = True
        Me.TBNegaraFP.Properties.ReadOnly = True
        Me.SLUBank.Properties.ReadOnly = True
        Me.TBCabang.Properties.ReadOnly = True
        Me.TBNoRek.Properties.ReadOnly = True
        Me.TBAnRek.Properties.ReadOnly = True
        Me.TBNamaCP1.Properties.ReadOnly = True
        Me.TBTelpCP1.Properties.ReadOnly = True
        Me.TBHpCP1.Properties.ReadOnly = True
        Me.TBEmailCP1.Properties.ReadOnly = True
        Me.TBNamaCP2.Properties.ReadOnly = True
        Me.TBTelpCP2.Properties.ReadOnly = True
        Me.TBHpCP2.Properties.ReadOnly = True
        Me.TBEmailCP2.Properties.ReadOnly = True
        Me.CEJasa.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTSupp_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.SPTop.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.MAlamat.Properties.ReadOnly = False
        Me.SLUKota.Properties.ReadOnly = False
        Me.TBNegara.Properties.ReadOnly = False
        Me.TBTelp.Properties.ReadOnly = False
        Me.TBHp.Properties.ReadOnly = False
        Me.TBFax.Properties.ReadOnly = False
        Me.TBEmail.Properties.ReadOnly = False
        Me.TBNPWP.Properties.ReadOnly = False
        Me.TBNamaFP.Properties.ReadOnly = False
        Me.MAlamatFP.Properties.ReadOnly = False
        Me.TBKotaFP.Properties.ReadOnly = False
        Me.TBKdPosFP.Properties.ReadOnly = False
        Me.TBPropFP.Properties.ReadOnly = False
        Me.TBNegaraFP.Properties.ReadOnly = False
        Me.SLUBank.Properties.ReadOnly = False
        Me.TBCabang.Properties.ReadOnly = False
        Me.TBNoRek.Properties.ReadOnly = False
        Me.TBAnRek.Properties.ReadOnly = False
        Me.TBNamaCP1.Properties.ReadOnly = False
        Me.TBTelpCP1.Properties.ReadOnly = False
        Me.TBHpCP1.Properties.ReadOnly = False
        Me.TBEmailCP1.Properties.ReadOnly = False
        Me.TBNamaCP2.Properties.ReadOnly = False
        Me.TBTelpCP2.Properties.ReadOnly = False
        Me.TBHpCP2.Properties.ReadOnly = False
        Me.TBEmailCP2.Properties.ReadOnly = False
        Me.CEJasa.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTSupp_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select KotaID,Nama From M_Kota", koneksi)
        cmsl.TableMappings.Add("Table", "M_KotaLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_KotaLUE")
        DsMaster.Tables("M_KotaLUE").Clear()
        cmsl.Fill(DsMaster, "M_KotaLUE")

        Me.SLUKota.Properties.DataSource = DsMaster.Tables("M_KotaLUE")
        Me.SLUKota.Properties.DisplayMember = "Nama"
        Me.SLUKota.Properties.ValueMember = "KotaID"

        cmsl = New SqlDataAdapter("Select BankID,Nama From M_Bank Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BankLUE")
        cmsl.Fill(DsMaster, "M_BankLUE")
        DsMaster.Tables("M_BankLUE").Clear()
        cmsl.Fill(DsMaster, "M_BankLUE")

        Me.SLUBank.Properties.DataSource = DsMaster.Tables("M_BankLUE")
        Me.SLUBank.Properties.DisplayMember = "Nama"
        Me.SLUBank.Properties.ValueMember = "BankID"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select SuppID,S.Nama,Alamat,S.KotaID,K.Nama As Kota,Negara,NPWP,NamaFP,AlamatFP,KotaFP,KdPosFP,PropFP, NegaraFP,NoTelp,NoHp,Fax,Email,JT,S.BankID,B.Nama as Bank,Cabang,NoRek,AnRek,NamaCP1,NoTelpCP1,NoHPCP1,EmailCP1,NamaCP2, NoTelpCP2,NoHPCP2,EmailCP2,Ket,S.stsOdJasa,S.Aktif,S.InsDate,S.InsBy,S.UpdDate,S.UpdBy From M_Supp S Inner Join M_Kota K On S.KotaID=K.KotaID Left Outer Join M_Bank B On S.BankID=B.BankId", koneksi)

        cmsl.TableMappings.Add("Table", "M_Supp")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_Supp")
        DsMaster.Tables("M_Supp").Clear()
        cmsl.Fill(DsMaster, "M_Supp")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Supp"
    End Sub

    Private Sub FSupp_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Supplier"
    End Sub

    Private Sub FSupp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTSupp_e.Selected = True
    End Sub

    Private Sub BVTCust_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTSupp_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Supplier"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Supplier"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.SPTop.EditValue = 0
        Me.MKet.EditValue = ""
        Me.MAlamat.EditValue = ""
        Me.SLUKota.EditValue = ""
        Me.TBTelp.EditValue = ""
        Me.TBHp.EditValue = ""
        Me.TBFax.EditValue = ""
        Me.TBEmail.EditValue = ""
        Me.TBNPWP.EditValue = ""
        Me.TBNamaFP.EditValue = ""
        Me.MAlamatFP.EditValue = ""
        Me.TBKotaFP.EditValue = ""
        Me.TBKdPosFP.EditValue = ""
        Me.TBPropFP.EditValue = ""
        Me.SLUBank.EditValue = ""
        Me.TBCabang.EditValue = ""
        Me.TBNoRek.EditValue = ""
        Me.TBAnRek.EditValue = ""
        Me.TBNamaCP1.EditValue = ""
        Me.TBTelpCP1.EditValue = ""
        Me.TBHpCP1.EditValue = ""
        Me.TBEmailCP1.EditValue = ""
        Me.TBNamaCP2.EditValue = ""
        Me.TBTelpCP2.EditValue = ""
        Me.TBHpCP2.EditValue = ""
        Me.TBEmailCP2.EditValue = ""
        Me.CEJasa.EditValue = False
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Supplier"

        LUE()
        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("SuppID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("SuppID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.SPTop.EditValue = Me.GridView1.GetFocusedDataRow.Item("JT")
        Me.MKet.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ket")
        Me.MAlamat.EditValue = Me.GridView1.GetFocusedDataRow.Item("Alamat")
        Me.SLUKota.EditValue = Me.GridView1.GetFocusedDataRow.Item("KotaID")
        Me.TBNegara.EditValue = Me.GridView1.GetFocusedDataRow.Item("Negara")
        Me.TBTelp.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoTelp")
        Me.TBHp.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoHp")
        Me.TBFax.EditValue = Me.GridView1.GetFocusedDataRow.Item("Fax")
        Me.TBEmail.EditValue = Me.GridView1.GetFocusedDataRow.Item("Email")
        Me.TBNPWP.EditValue = Me.GridView1.GetFocusedDataRow.Item("NPWP")
        Me.TBNamaFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("NamaFP")
        Me.MAlamatFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlamatFP")
        Me.TBKotaFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("KotaFP")
        Me.TBKdPosFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("KdPosFP")
        Me.TBPropFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("PropFP")
        Me.TBNegaraFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("NegaraFP")
        Me.SLUBank.EditValue = Me.GridView1.GetFocusedDataRow.Item("BankID")
        Me.TBCabang.EditValue = Me.GridView1.GetFocusedDataRow.Item("Cabang")
        Me.TBNoRek.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoRek")
        Me.TBAnRek.EditValue = Me.GridView1.GetFocusedDataRow.Item("AnRek")
        Me.TBNamaCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("NamaCP1")
        Me.TBTelpCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoTelpCP1")
        Me.TBHpCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoHpCP1")
        Me.TBEmailCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("EmailCP1")
        Me.TBNamaCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("NamaCP2")
        Me.TBTelpCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoTelpCP2")
        Me.TBHpCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoHpCP2")
        Me.TBEmailCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("EmailCP2")
        Me.CEJasa.EditValue = Me.GridView1.GetFocusedDataRow.Item("stsOdJasa")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub
    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Supplier"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Customer : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Supp")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("SuppID")
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

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.TBNegara.EditValue = "" Or IsDBNull(Me.TBNegara.EditValue) Then
            XtraMessageBox.Show("Negara Tidak Boleh Kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Supp")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@Negara", SqlDbType.VarChar).Value = Me.TBNegara.EditValue
                    .Parameters.Add("@NPWP", SqlDbType.VarChar).Value = Me.TBNPWP.EditValue
                    .Parameters.Add("@NamaFP", SqlDbType.VarChar).Value = Me.TBNamaFP.EditValue
                    .Parameters.Add("@AlamatFP", SqlDbType.VarChar).Value = Me.MAlamatFP.EditValue
                    .Parameters.Add("@KotaFP", SqlDbType.VarChar).Value = Me.TBKotaFP.EditValue
                    .Parameters.Add("@KdPosFP", SqlDbType.VarChar).Value = Me.TBKdPosFP.EditValue
                    .Parameters.Add("@PropFP", SqlDbType.VarChar).Value = Me.TBPropFP.EditValue
                    .Parameters.Add("@NegaraFP", SqlDbType.VarChar).Value = Me.TBNegaraFP.EditValue
                    .Parameters.Add("@NoTelp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
                    .Parameters.Add("@NoHp", SqlDbType.VarChar).Value = Me.TBHp.EditValue
                    .Parameters.Add("@Fax", SqlDbType.VarChar).Value = Me.TBFax.EditValue
                    .Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TBEmail.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.SPTop.EditValue
                    .Parameters.Add("@BankId", SqlDbType.VarChar).Value = Me.SLUBank.EditValue
                    .Parameters.Add("@Cab", SqlDbType.VarChar).Value = Me.TBCabang.EditValue
                    .Parameters.Add("@NoRek", SqlDbType.VarChar).Value = Me.TBNoRek.EditValue
                    .Parameters.Add("@AnRek", SqlDbType.VarChar).Value = Me.TBAnRek.EditValue
                    .Parameters.Add("@NamaCP1", SqlDbType.VarChar).Value = Me.TBNamaCP1.EditValue
                    .Parameters.Add("@TelpCP1", SqlDbType.VarChar).Value = Me.TBTelpCP1.EditValue
                    .Parameters.Add("@HPCP1", SqlDbType.VarChar).Value = Me.TBHpCP1.EditValue
                    .Parameters.Add("@EmailCP1", SqlDbType.VarChar).Value = Me.TBEmailCP1.EditValue
                    .Parameters.Add("@NamaCP2", SqlDbType.VarChar).Value = Me.TBNamaCP2.EditValue
                    .Parameters.Add("@TelpCP2", SqlDbType.VarChar).Value = Me.TBTelpCP2.EditValue
                    .Parameters.Add("@HPCP2", SqlDbType.VarChar).Value = Me.TBHpCP2.EditValue
                    .Parameters.Add("@EmailCP2", SqlDbType.VarChar).Value = Me.TBEmailCP2.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsOdJasa", SqlDbType.Bit).Value = Me.CEJasa.EditValue
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
            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Supp")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@Negara", SqlDbType.VarChar).Value = Me.TBNegara.EditValue
                    .Parameters.Add("@NPWP", SqlDbType.VarChar).Value = Me.TBNPWP.EditValue
                    .Parameters.Add("@NamaFP", SqlDbType.VarChar).Value = Me.TBNamaFP.EditValue
                    .Parameters.Add("@AlamatFP", SqlDbType.VarChar).Value = Me.MAlamatFP.EditValue
                    .Parameters.Add("@KotaFP", SqlDbType.VarChar).Value = Me.TBKotaFP.EditValue
                    .Parameters.Add("@KdPosFP", SqlDbType.VarChar).Value = Me.TBKdPosFP.EditValue
                    .Parameters.Add("@PropFP", SqlDbType.VarChar).Value = Me.TBPropFP.EditValue
                    .Parameters.Add("@NegaraFP", SqlDbType.VarChar).Value = Me.TBNegaraFP.EditValue
                    .Parameters.Add("@NoTelp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
                    .Parameters.Add("@NoHp", SqlDbType.VarChar).Value = Me.TBHp.EditValue
                    .Parameters.Add("@Fax", SqlDbType.VarChar).Value = Me.TBFax.EditValue
                    .Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TBEmail.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.SPTop.EditValue
                    .Parameters.Add("@BankId", SqlDbType.VarChar).Value = Me.SLUBank.EditValue
                    .Parameters.Add("@Cab", SqlDbType.VarChar).Value = Me.TBCabang.EditValue
                    .Parameters.Add("@NoRek", SqlDbType.VarChar).Value = Me.TBNoRek.EditValue
                    .Parameters.Add("@AnRek", SqlDbType.VarChar).Value = Me.TBAnRek.EditValue
                    .Parameters.Add("@NamaCP1", SqlDbType.VarChar).Value = Me.TBNamaCP1.EditValue
                    .Parameters.Add("@TelpCP1", SqlDbType.VarChar).Value = Me.TBTelpCP1.EditValue
                    .Parameters.Add("@HPCP1", SqlDbType.VarChar).Value = Me.TBHpCP1.EditValue
                    .Parameters.Add("@EmailCP1", SqlDbType.VarChar).Value = Me.TBEmailCP1.EditValue
                    .Parameters.Add("@NamaCP2", SqlDbType.VarChar).Value = Me.TBNamaCP2.EditValue
                    .Parameters.Add("@TelpCP2", SqlDbType.VarChar).Value = Me.TBTelpCP2.EditValue
                    .Parameters.Add("@HPCP2", SqlDbType.VarChar).Value = Me.TBHpCP2.EditValue
                    .Parameters.Add("@EmailCP2", SqlDbType.VarChar).Value = Me.TBEmailCP2.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@stsOdJasa", SqlDbType.Bit).Value = Me.CEJasa.EditValue
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress, MAlamat.KeyPress, TBNegara.KeyPress, TBTelp.KeyPress, TBFax.KeyPress, TBEmail.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBNPWP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNPWP.KeyPress, TBNamaFP.KeyPress, MAlamatFP.KeyPress, TBKotaFP.KeyPress, TBPropFP.KeyPress, TBNegaraFP.KeyPress, TBKdPosFP.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBCabang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBCabang.KeyPress, TBNoRek.KeyPress, TBAnRek.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBNamaCP1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNamaCP1.KeyPress, TBTelpCP1.KeyPress, TBHpCP1.KeyPress, TBEmailCP1.KeyPress, TBNamaCP2.KeyPress, TBTelpCP2.KeyPress, TBHpCP2.KeyPress, TBEmailCP2.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

End Class