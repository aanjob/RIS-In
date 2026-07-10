Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FBrgAss
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("AssN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("AssEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("AssDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTAss_s.Enabled = True

        Me.TBNama.Properties.ReadOnly = True
        Me.SLUMerk.Properties.ReadOnly = True
        Me.SLUSat.Properties.ReadOnly = True
        Me.CEFixAss.Properties.ReadOnly = True
        Me.TBUkAw.Properties.ReadOnly = True
        Me.TBUkAkh.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.GridView1.OptionsBehavior.Editable = False

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
        Me.BVTAss_s.Enabled = False

        Me.TBNama.Properties.ReadOnly = False
        Me.SLUMerk.Properties.ReadOnly = False
        Me.CEFixAss.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTAss_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MerkID,Nama From M_BrgMerk Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgMerkLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")
        DsMaster.Tables("M_BrgMerkLUE").Clear()
        cmsl.Fill(DsMaster, "M_BrgMerkLUE")

        Me.SLUMerk.Properties.DataSource = DsMaster.Tables("M_BrgMerkLUE")
        Me.SLUMerk.Properties.DisplayMember = "MerkID"
        Me.SLUMerk.Properties.ValueMember = "MerkID"

        cmsl = New SqlDataAdapter("Select SatID,Isi From M_BrgSat Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgSatL")
        cmsl.Fill(DsMaster, "M_BrgSatL")
        DsMaster.Tables("M_BrgSatL").Clear()
        cmsl.Fill(DsMaster, "M_BrgSatL")

        Me.SLUSat.Properties.DataSource = DsMaster.Tables("M_BrgSatL")
        Me.SLUSat.Properties.DisplayMember = "SatID"
        Me.SLUSat.Properties.ValueMember = "SatID"
    End Sub

    Public Sub CekFixAss()
        If Me.CEFixAss.EditValue = True Then
            Me.TBUkAw.Properties.ReadOnly = True
            Me.TBUkAkh.Properties.ReadOnly = True
            Me.SLUSat.Properties.ReadOnly = False
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = True

        Else
            Me.TBUkAw.Properties.ReadOnly = False
            Me.TBUkAkh.Properties.ReadOnly = False
            Me.SLUSat.Properties.ReadOnly = True
            Me.GridControl1.EmbeddedNavigator.Buttons.Append.Enabled = False

            Me.SLUSat.EditValue = "P"
            Me.TBIsi.EditValue = 1
        End If
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AssIDD,AssID,Uk,Qty From M_BrgAssDtl Where AssID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgAssDtl")
        cmsl.Fill(DsMaster, "M_BrgAssDtl")
        DsMaster.Tables("M_BrgAssDtl").Clear()
        cmsl.Fill(DsMaster, "M_BrgAssDtl")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_BrgAssDtl"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select AssID,Ass,H.MerkID,M.Nama As Merk,SatID,Isi,H.Aktif,H.InsDate,H.InsBy,H.UpdDate,H.UpdBy From M_BrgAss H Inner Join M_BrgMerk M On H.MerkID=M.MerkID", koneksi)

        cmsl.TableMappings.Add("Table", "M_BrgAss")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_BrgAss")
        DsMaster.Tables("M_BrgAss").Clear()
        cmsl.Fill(DsMaster, "M_BrgAss")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_BrgAss"
    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelM_BrgAss")
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

    Private Sub FBrgAss_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Assortment"
    End Sub

    Private Sub FBrgAss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTAss_e.Selected = True
    End Sub

    Private Sub BVTAss_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTAss_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Assortment"

        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Assortment"

        DsMaster.Clear()

        OpenControl()
        LUE()
        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.SLUMerk.EditValue = ""
        Me.SLUSat.EditValue = ""
        Me.CEFixAss.EditValue = True
        Me.TBUkAw.EditValue = 0.0
        Me.TBUkAkh.EditValue = 0.0
        Me.TBIsi.EditValue = 0

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("M_BrgAssDtl").Clear()

        CekFixAss()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Assortment"

        Me.TBNama.Properties.ReadOnly = True
        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("AssID")
        Me.TBNama.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ass")
        Me.SLUMerk.EditValue = Me.GridView2.GetFocusedDataRow.Item("MerkID")
        Me.SLUSat.EditValue = Me.GridView2.GetFocusedDataRow.Item("SatID")
        Me.TBIsi.EditValue = Me.GridView2.GetFocusedDataRow.Item("Isi")
        Me.CEAktif.EditValue = Me.GridView2.GetFocusedDataRow.Item("Aktif")

        FillDtl(Me.TBKode.EditValue)

        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        Me.TBNama.Properties.ReadOnly = True
        Me.CEFixAss.Properties.ReadOnly = True

    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Assortment"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Jenis : " & Me.GridView2.GetFocusedDataRow.Item("Ass") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_BrgAss")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("AssID")
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
                    XtraMessageBox.Show(ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
            End With
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Dim x As Integer

        If Me.CEFixAss.EditValue = True Then

            If Me.TBIsi.EditValue <> CType(Me.GridView1.Columns("Qty").SummaryText, Decimal) Then
                XtraMessageBox.Show("Isi Tidak Sama Dengan Isi Dos", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            Else
                Select Case Indicator
                    Case 100
                        Dim cmSP As New SqlCommand("SPInsM_BrgAss")
                        cmSP.CommandType = CommandType.StoredProcedure

                        With cmSP
                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                            .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSat.EditValue
                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.TBIsi.EditValue
                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Parameters.Add("@Kode", SqlDbType.VarChar, 15)
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
                                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Size")) Then
                                        Dim cmSPDtl As New SqlCommand("SPInsM_BrgAssDtl")
                                        cmSPDtl.CommandType = CommandType.StoredProcedure

                                        With cmSPDtl
                                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                            .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                            .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = "P"
                                            .Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                                            .Parameters.Add("@FixAss", SqlDbType.Bit).Value = Me.CEFixAss.EditValue
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

                                If x = 0 Then
                                    XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                ElseIf x = 1 Then
                                    XtraMessageBox.Show("Id Tidak Boleh Sama 2", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Exit Sub
                                Else
                                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Del()
                                    Exit Sub
                                End If

                            Catch ex As Exception
                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Del()
                                Exit Sub
                            End Try
                        End With

                    Case 200
                        Dim cmSP As New SqlCommand("SPUpM_BrgAss")
                        cmSP.CommandType = CommandType.StoredProcedure

                        With cmSP
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                            .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                            .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.SLUSat.EditValue
                            .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.TBIsi.EditValue
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
                                    Dim cmSPDel As New SqlCommand("SPDelM_BrgAssDtl")
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
                                    If Me.GridView1.GetRowCellValue(i, "AssIDD") < 0 Then
                                        If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Uk")) Then
                                            Dim cmSPDtl As New SqlCommand("SPInsM_BrgAssDtl")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                                .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = "P"
                                                .Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                                                .Parameters.Add("@FixAss", SqlDbType.Bit).Value = Me.CEFixAss.EditValue
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

                                            If x = 0 Then
                                                Me.GridView1.SetRowCellValue(i, "AssIDD", Me.GridView1.GetRowCellValue(i, "AssIDD") * -1)
                                            End If
                                        End If
                                    Else
                                        If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Uk")) Then
                                            Dim cmSPDtl As New SqlCommand("SPUpM_BrgAssDtl")
                                            cmSPDtl.CommandType = CommandType.StoredProcedure

                                            With cmSPDtl
                                                .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "AssIDD")
                                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                                .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                                .Parameters.Add("@Qty", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "Qty")
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
            End If

        Else

            Select Case Indicator
                Case 100

                    Dim i : For i = CDec(Me.TBUkAw.EditValue) To CDec(Me.TBUkAkh.EditValue)
                        If Not IsDBNull(i) Then
                            Dim cmSPDtl As New SqlCommand("SPInsM_BrgAssDtl")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@Uk", SqlDbType.VarChar).Value = i
                                .Parameters.Add("@Qty", SqlDbType.Int).Value = 1
                                .Parameters.Add("@MerkID", SqlDbType.VarChar).Value = Me.SLUMerk.EditValue
                                .Parameters.Add("@SatID", SqlDbType.VarChar).Value = "P"
                                .Parameters.Add("@Isi", SqlDbType.Int).Value = 1
                                .Parameters.Add("@FixAss", SqlDbType.Bit).Value = Me.CEFixAss.EditValue
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

                            Catch ex As Exception
                                XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Del()
                                Exit Sub
                            End Try
                        End If
                    Next

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf x = 1 Then
                        XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    Else
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Del()
                        Exit Sub
                    End If
            End Select

        End If



        LockControl()

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("AssIDD")
        End If

    End Sub

    Private Sub GridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetFocusedRowCellValue("AssID", Me.TBKode.EditValue)
        Me.GridView1.SetFocusedRowCellValue("AssIDD", Me.GridView1.RowCount * -1)
        Me.GridView1.SetFocusedRowCellValue("Uk", "")
        Me.GridView1.SetFocusedRowCellValue("Qty", 1)
    End Sub

    Private Sub SLUSat_Leave(sender As Object, e As EventArgs) Handles SLUSat.Leave
        Me.TBIsi.EditValue = DsMaster.Tables("M_BrgSatL").Select("SatID = '" & Me.SLUSat.EditValue & "'")(0).Item("Isi")

        If Me.SLUSat.EditValue = "P" Then
            Me.GridView1.OptionsBehavior.Editable = False
            Me.GridView1.AddNewRow()
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Uk", Me.TBNama.EditValue)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty", 1)
        Else
            Me.GridView1.OptionsBehavior.Editable = True
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Dim frm As New FBrgAss_d(Me.GridView2.GetFocusedDataRow.Item("AssID"))
        frm.ShowDialog()
        frm.Close()
    End Sub

    Private Sub CEFixAss_EditValueChanged(sender As Object, e As EventArgs) Handles CEFixAss.EditValueChanged
        CekFixAss()
    End Sub

    Private Sub TBNama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNama.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.Handled = True
        End If
    End Sub


    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "Uk" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class