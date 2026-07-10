Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FUserv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim arrPar2(-1) As String
    Dim UserID As Integer = 0


    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("UsN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBReset.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTUser_s.Enabled = True

        Me.TBLogin.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.TBInisial.Properties.ReadOnly = True
        Me.SLUPosisi.Properties.ReadOnly = True
        Me.TBVersi.Properties.ReadOnly = True
        Me.CENoHarga.Properties.ReadOnly = True
        Me.CEAlertPemb.Properties.ReadOnly = True
        Me.CEAlertJual.Properties.ReadOnly = True
        Me.CEAlertApp.Properties.ReadOnly = True
        Me.CEAlertReqC.Properties.ReadOnly = True
        Me.CEAlertSpec.Properties.ReadOnly = True
        Me.CEAlertBOM.Properties.ReadOnly = True
        Me.CEAlertPO.Properties.ReadOnly = True
        Me.CEAlertSampR.Properties.ReadOnly = True
        Me.CEAlertPass.Properties.ReadOnly = True
        Me.CEAlertBSTB.Properties.ReadOnly = True
        Me.CEAlertCair.Properties.ReadOnly = True
        Me.CEAlertBOMstsPO.Properties.ReadOnly = True
        Me.CEAlertSJKBB.Properties.ReadOnly = True
        Me.CEAlertBayar.Properties.ReadOnly = True
        Me.CEBackDate.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBReset.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTUser_s.Enabled = False

        Me.TBLogin.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.TBInisial.Properties.ReadOnly = False
        Me.SLUPosisi.Properties.ReadOnly = False
        Me.TBVersi.Properties.ReadOnly = False
        Me.CENoHarga.Properties.ReadOnly = False
        Me.CEAlertPemb.Properties.ReadOnly = False
        Me.CEAlertJual.Properties.ReadOnly = False
        Me.CEAlertApp.Properties.ReadOnly = False
        Me.CEAlertReqC.Properties.ReadOnly = False
        Me.CEAlertSpec.Properties.ReadOnly = False
        Me.CEAlertBOM.Properties.ReadOnly = False
        Me.CEAlertPO.Properties.ReadOnly = False
        Me.CEAlertSampR.Properties.ReadOnly = False
        Me.CEAlertPass.Properties.ReadOnly = False
        Me.CEAlertBSTB.Properties.ReadOnly = False
        Me.CEAlertCair.Properties.ReadOnly = False
        Me.CEAlertBOMstsPO.Properties.ReadOnly = False
        Me.CEAlertSJKBB.Properties.ReadOnly = False
        Me.CEAlertBayar.Properties.ReadOnly = False
        Me.CEBackDate.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.GridView2.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTUser_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select PosisiID From M_Posisi", koneksi)
        cmsl.TableMappings.Add("Table", "M_PosisiL2")
        Try
            DsMaster.Tables("M_PosisiL2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_PosisiL2")

        Me.SLUPosisi.Properties.DataSource = DsMaster.Tables("M_PosisiL2")
        Me.SLUPosisi.Properties.DisplayMember = "PosisiID"
        Me.SLUPosisi.Properties.ValueMember = "PosisiID"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UserID,[dbo].[fcDecrypt](LoginID) as LoginID,Nama,Inisial,[dbo].[fcDecrypt](Passwordd) as Passwordd, PosisiID,DvcID,Versi,NoHarga,Alert,AlertJual,AlertApp,AlertReqC,AlertSpec,AlertBOM,AlertPO,AlertSampR,AlertPass,AlertBSTB,AlertCair, AlertBOMstsPO,AlertTrmPO,AlertSJKBB,AlertBayar,BackDate,Aktif,InsDate,InsBy,UpdDate,UpdBy From M_User Order By Nama Asc", koneksi)

        cmsl.TableMappings.Add("Table", "M_User")
        Try
            DsMaster.Tables("M_User").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_User")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_User"
    End Sub

    Private Sub FUser_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master User"
    End Sub

    Private Sub FUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTUser_e.Selected = True
    End Sub
    Private Sub BVTUser_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTUser_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master User"

        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("UsEd"), Boolean)
        Me.BVBReset.Enabled = CType(TcodeCollection.Item("UsRs"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("UsDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master User"

        OpenControl()
        LUE()

        Me.TBLogin.Font = New Font("Tahoma", 8.25)
        Me.TBLogin.Properties.PasswordChar = ""

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True
        Me.CEBackDate.Properties.ReadOnly = True

        Me.TBLogin.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.TBPassword.EditValue = ""
        Me.TBInisial.EditValue = ""
        Me.SLUPosisi.EditValue = ""
        Me.TBDeviceID.EditValue = ""
        Me.TBVersi.EditValue = MainModule.Version
        Me.CENoHarga.EditValue = False
        Me.CEAlertPemb.EditValue = False
        Me.CEAlertJual.EditValue = False
        Me.CEAlertApp.EditValue = False
        Me.CEAlertReqC.EditValue = False
        Me.CEAlertSpec.EditValue = False
        Me.CEAlertBOM.EditValue = False
        Me.CEAlertPO.EditValue = False
        Me.CEAlertSampR.EditValue = False
        Me.CEAlertPass.EditValue = False
        Me.CEAlertBSTB.EditValue = False
        Me.CEAlertCair.EditValue = False
        Me.CEAlertBOMstsPO.EditValue = False
        Me.CEAlertTrmPO.EditValue = False
        Me.CEAlertSJKBB.EditValue = False
        Me.CEAlertBayar.EditValue = False
        Me.CEBackDate.EditValue = False
        Me.CEAktif.EditValue = True

        Me.TBInfo.EditValue = ""

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID,UserID,NamaLogin,Grup From M_UsGrup Where UserID=0", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrup")
        Try
            DsMaster.Tables("M_UsGrup").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrup")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_UsGrup"

        cmsl = New SqlDataAdapter("Select HubID,UserID,NamaLogin,UC.CabID,Case When UC.CabID='%' Then 'Semua' Else C.Cabang End As Cabang,Def From M_UsCab UC Left Outer Join M_Cab C On UC.CabID=C.CabID Where UserID=0", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCab")
        Try
            DsMaster.Tables("M_UsCab").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "M_UsCab")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "M_UsCab"
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master User"

        Me.TBLogin.Font = New Font("Broadway", 10)
        Me.TBLogin.Properties.PasswordChar = "●"
        LUE()
        Indicator = "200"
        UserID = Me.GridView1.GetFocusedDataRow.Item("UserID")
        Me.TBLogin.EditValue = Me.GridView1.GetFocusedDataRow.Item("LoginID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.TBInisial.EditValue = Me.GridView1.GetFocusedDataRow.Item("Inisial")
        Me.TBPassword.EditValue = Me.GridView1.GetFocusedDataRow.Item("Passwordd")
        Me.SLUPosisi.EditValue = Me.GridView1.GetFocusedDataRow.Item("PosisiID")
        Me.TBDeviceID.EditValue = Me.GridView1.GetFocusedDataRow.Item("DvcID")
        Me.TBVersi.EditValue = Me.GridView1.GetFocusedDataRow.Item("Versi")
        Me.CENoHarga.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoHarga")
        Me.CEAlertPemb.EditValue = Me.GridView1.GetFocusedDataRow.Item("Alert")
        Me.CEAlertJual.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertJual")
        Me.CEAlertApp.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertApp")
        Me.CEAlertReqC.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertReqC")
        Me.CEAlertSpec.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertSpec")
        Me.CEAlertBOM.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertBOM")
        Me.CEAlertPO.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertPO")
        Me.CEAlertSampR.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertSampR")
        Me.CEAlertPass.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertPass")
        Me.CEAlertBSTB.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertBSTB")
        Me.CEAlertCair.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertCair")
        Me.CEAlertBOMstsPO.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertBOMstsPO")
        Me.CEAlertTrmPO.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertTrmPO")
        Me.CEAlertSJKBB.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertSJKBB")
        Me.CEAlertBayar.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlertBayar")
        Me.CEBackDate.EditValue = Me.GridView1.GetFocusedDataRow.Item("BackDate")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID,UserID,NamaLogin,Grup From M_UsGrup Where UserID=" & UserID & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrup")
        Try
            DsMaster.Tables("M_UsGrup").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "M_UsGrup")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_UsGrup"

        cmsl = New SqlDataAdapter("Select HubID,UserID,NamaLogin,UC.CabID,Case When UC.CabID='%' Then 'Semua' Else C.Cabang End As Cabang,Def From M_UsCab UC Left Outer Join M_Cab C On UC.CabID=C.CabID Where UserID=" & UserID & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCab")
        Try
            DsMaster.Tables("M_UsCab").Clear()
        Catch ex As Exception

        End Try

        cmsl.Fill(DsMaster, "M_UsCab")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "M_UsCab"

        OpenControl()
        Me.TBLogin.Properties.ReadOnly = True

    End Sub

    Private Sub BVBResetPassword_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBReset.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Reset Password User"

        If XtraMessageBox.Show("Apakah Anda Mau Mereset Password : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPResetM_User")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@UserID", SqlDbType.Int).Value = Me.GridView1.GetFocusedDataRow.Item("UserID")
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
                        XtraMessageBox.Show("Password Berhasil Direset", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    Else
                        XtraMessageBox.Show("Password Gagal Direset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master User"

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus User : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_User")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@UserID", SqlDbType.Int).Value = Me.GridView1.GetFocusedDataRow.Item("UserID")
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

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_User")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.NVarChar).Value = Me.TBLogin.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Inisial", SqlDbType.VarChar).Value = Me.TBInisial.EditValue
                    .Parameters.Add("@Password", SqlDbType.NVarChar).Value = Me.TBPassword.EditValue
                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.SLUPosisi.EditValue
                    .Parameters.Add("@Versi", SqlDbType.VarChar).Value = Me.TBVersi.EditValue
                    .Parameters.Add("@NoHarga", SqlDbType.Bit).Value = Me.CENoHarga.EditValue
                    .Parameters.Add("@AlertPemb", SqlDbType.Bit).Value = Me.CEAlertPemb.EditValue
                    .Parameters.Add("@AlertJual", SqlDbType.Bit).Value = Me.CEAlertJual.EditValue
                    .Parameters.Add("@AlertApp", SqlDbType.Bit).Value = Me.CEAlertApp.EditValue
                    .Parameters.Add("@AlertReqC", SqlDbType.Bit).Value = Me.CEAlertReqC.EditValue
                    .Parameters.Add("@AlertSpec", SqlDbType.Bit).Value = Me.CEAlertSpec.EditValue
                    .Parameters.Add("@AlertBOM", SqlDbType.Bit).Value = Me.CEAlertBOM.EditValue
                    .Parameters.Add("@AlertPO", SqlDbType.Bit).Value = Me.CEAlertPO.EditValue
                    .Parameters.Add("@AlertSampR", SqlDbType.Bit).Value = Me.CEAlertSampR.EditValue
                    .Parameters.Add("@AlertPass", SqlDbType.Bit).Value = Me.CEAlertPass.EditValue
                    .Parameters.Add("@AlertBSTB", SqlDbType.Bit).Value = Me.CEAlertBSTB.EditValue
                    .Parameters.Add("@AlertCair", SqlDbType.Bit).Value = Me.CEAlertCair.EditValue
                    .Parameters.Add("@AlertBOMstsPO", SqlDbType.Bit).Value = Me.CEAlertBOMstsPO.EditValue
                    .Parameters.Add("@AlertTrmPO", SqlDbType.Bit).Value = Me.CEAlertTrmPO.EditValue
                    .Parameters.Add("@AlertSJKBB", SqlDbType.Bit).Value = Me.CEAlertSJKBB.EditValue
                    .Parameters.Add("@AlertBayar", SqlDbType.Bit).Value = Me.CEAlertBayar.EditValue
                    .Parameters.Add("@BackDate", SqlDbType.Bit).Value = Me.CEBackDate.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@UserID", SqlDbType.Int)
                    .Parameters("@UserID").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            UserID = cmSP.Parameters("@UserID").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        Dim i : For i = 0 To GridView2.RowCount - 1
                            Dim cmSPDtl As New SqlCommand("SPInsM_UsGrup")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                                .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "Grup")
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
                        Next

                        Dim z : For z = 0 To GridView3.RowCount - 1
                            Dim cmSPDtl As New SqlCommand("SPInsM_UsCab")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                                .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                                .Parameters.Add("@Def", SqlDbType.Bit).Value = Me.GridView3.GetRowCellValue(z, "Def")
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
                        Next

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
                Dim cmSP As New SqlCommand("SPUpM_User")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                    .Parameters.Add("@Kode", SqlDbType.NVarChar).Value = Me.TBLogin.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@Inisial", SqlDbType.VarChar).Value = Me.TBInisial.EditValue
                    .Parameters.Add("@Password", SqlDbType.NVarChar).Value = Me.TBPassword.EditValue
                    .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = Me.SLUPosisi.EditValue
                    .Parameters.Add("@Versi", SqlDbType.VarChar).Value = Me.TBVersi.EditValue
                    .Parameters.Add("@NoHarga", SqlDbType.Bit).Value = Me.CENoHarga.EditValue
                    .Parameters.Add("@AlertPemb", SqlDbType.Bit).Value = Me.CEAlertPemb.EditValue
                    .Parameters.Add("@AlertJual", SqlDbType.Bit).Value = Me.CEAlertJual.EditValue
                    .Parameters.Add("@AlertApp", SqlDbType.Bit).Value = Me.CEAlertApp.EditValue
                    .Parameters.Add("@AlertReqC", SqlDbType.Bit).Value = Me.CEAlertReqC.EditValue
                    .Parameters.Add("@AlertSpec", SqlDbType.Bit).Value = Me.CEAlertSpec.EditValue
                    .Parameters.Add("@AlertBOM", SqlDbType.Bit).Value = Me.CEAlertBOM.EditValue
                    .Parameters.Add("@AlertPO", SqlDbType.Bit).Value = Me.CEAlertPO.EditValue
                    .Parameters.Add("@AlertSampR", SqlDbType.Bit).Value = Me.CEAlertSampR.EditValue
                    .Parameters.Add("@AlertPass", SqlDbType.Bit).Value = Me.CEAlertPass.EditValue
                    .Parameters.Add("@AlertBSTB", SqlDbType.Bit).Value = Me.CEAlertBSTB.EditValue
                    .Parameters.Add("@AlertCair", SqlDbType.Bit).Value = Me.CEAlertCair.EditValue
                    .Parameters.Add("@AlertBOMstsPO", SqlDbType.Bit).Value = Me.CEAlertBOMstsPO.EditValue
                    .Parameters.Add("@AlertTrmPO", SqlDbType.Bit).Value = Me.CEAlertTrmPO.EditValue
                    .Parameters.Add("@AlertSJKBB", SqlDbType.Bit).Value = Me.CEAlertSJKBB.EditValue
                    .Parameters.Add("@AlertBayar", SqlDbType.Bit).Value = Me.CEAlertBayar.EditValue
                    .Parameters.Add("@BackDate", SqlDbType.Bit).Value = Me.CEBackDate.EditValue
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


                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_UsGrup")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
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

                        Dim w : For w = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_UsCab")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(w)
                                .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
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

                        Dim i : For i = 0 To GridView2.RowCount - 1
                            If Me.GridView2.GetRowCellValue(i, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_UsGrup")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "Grup")
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
                                    Me.GridView1.SetRowCellValue(i, "HubID", Me.GridView2.GetRowCellValue(i, "HubID") * -1)
                                End If

                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_UsGrup")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(i, "HubID")
                                    .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "Grup")
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

                        Dim z : For z = 0 To GridView3.RowCount - 1
                            If Me.GridView3.GetRowCellValue(z, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_UsCab")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                                    .Parameters.Add("@Def", SqlDbType.Bit).Value = Me.GridView3.GetRowCellValue(z, "Def")
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
                                    Me.GridView3.SetRowCellValue(z, "HubID", Me.GridView3.GetRowCellValue(z, "HubID") * -1)
                                End If

                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_UsCab")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "HubID")
                                    .Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
                                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "CabID")
                                    .Parameters.Add("@Def", SqlDbType.Bit).Value = Me.GridView3.GetRowCellValue(z, "Def")
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

    Private Sub TBLogin_Leave(sender As Object, e As EventArgs) Handles TBLogin.Leave
        Me.TBPassword.EditValue = Me.TBLogin.EditValue
    End Sub

    Private Sub GridControl2_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl2.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView2.GetFocusedDataRow.Item("HubID")
        End If
    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("HubID")
        End If
    End Sub

    Private Sub GridView2_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView2.InitNewRow
        Try
            Me.GridView2.SetRowCellValue(e.RowHandle, "HubID", Me.GridView2.RowCount * -1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView3_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView3.InitNewRow
        Try
            Me.GridView3.SetRowCellValue(e.RowHandle, "HubID", Me.GridView3.RowCount * -1)
            Me.GridView3.SetRowCellValue(e.RowHandle, "Def", False)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdCabID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdCabID.ButtonClick
        Try
            Dim frm As New FSearch("Cabang", "", "", "", Date.Now, "")
            frm.ShowDialog()

            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Cabang", dataTrans.Item("Nama").ToString)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdCabID.KeyPress
        e.Handled = True
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNama.KeyPress, TBInisial.KeyPress, TBVersi.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
End Class