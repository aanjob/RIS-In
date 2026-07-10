Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FDocCode
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim KdLama As String

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("KdDocN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("KdDocEd"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("KdDocDel"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTKodeDoc_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.SLUTipeDoc.Properties.ReadOnly = True
        Me.CBOFormat.Properties.ReadOnly = True
        Me.TBPj.Properties.ReadOnly = True
        Me.CBOGol.Properties.ReadOnly = True
        Me.TBCab.Properties.ReadOnly = True
        Me.TBISO.Properties.ReadOnly = True

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
        Me.BVTKodeDoc_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.SLUTipeDoc.Properties.ReadOnly = False
        Me.CBOFormat.Properties.ReadOnly = False
        Me.TBPj.Properties.ReadOnly = False
        Me.CBOGol.Properties.ReadOnly = False
        Me.TBCab.Properties.ReadOnly = False
        Me.TBISO.Properties.ReadOnly = False

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTKodeDoc_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select DocID,Ket From M_TipeDoc", koneksi)
        cmsl.TableMappings.Add("Table", "M_TipeDoc")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_TipeDoc")
        DsMaster.Tables("M_TipeDoc").Clear()
        cmsl.Fill(DsMaster, "M_TipeDoc")

        Me.SLUTipeDoc.Properties.DataSource = DsMaster.Tables("M_TipeDoc")
        Me.SLUTipeDoc.Properties.DisplayMember = "Ket"
        Me.SLUTipeDoc.Properties.ValueMember = "DocID"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CodeID,DC.Ket,DC.DocID,TD.Ket As TipeDok,FormDate,Pj,ISOID,Gol,CabID,Manuall,DC.InsDate,DC.InsBy, DC.UpdDate,DC.UpdBy From M_DocCode DC Inner Join M_TipeDoc TD On DC.DocID=TD.DocID", koneksi)

        cmsl.TableMappings.Add("Table", "M_DocCode")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_DocCode")
        DsMaster.Tables("M_DocCode").Clear()
        cmsl.Fill(DsMaster, "M_DocCode")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_DocCode"
    End Sub

    Private Sub FDocCode_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Kode Dokumen"
    End Sub

    Private Sub FDocCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTKodeDoc_e.Selected = True
    End Sub

    Private Sub BVTKodeDoc_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTKodeDoc_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Kode Dokumen"
        FillDt()
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Kode Dokumen"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"

        Me.TBKode.EditValue = ""
        Me.MKet.EditValue = ""
        Me.SLUTipeDoc.EditValue = ""
        Me.CBOFormat.EditValue = ""
        Me.TBPj.EditValue = ""
        Me.TBISO.EditValue = ""
        Me.TBCab.EditValue = ""
        Me.TBInfo.EditValue = ""
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Kode Dokumen"

        LUE()

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("CodeID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("CodeID")
        Me.MKet.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ket")
        Me.SLUTipeDoc.EditValue = Me.GridView1.GetFocusedDataRow.Item("DocID")
        Me.CBOFormat.EditValue = Me.GridView1.GetFocusedDataRow.Item("FormDate")
        Me.TBPj.EditValue = Me.GridView1.GetFocusedDataRow.Item("Pj")
        Me.TBISO.EditValue = Me.GridView1.GetFocusedDataRow.Item("ISOID")
        Me.TBCab.EditValue = Me.GridView1.GetFocusedDataRow.Item("CabID")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Kode Dokumen"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Kode Dokumen : " & Me.GridView1.GetFocusedDataRow.Item("Ket") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_DocCode")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("CodeID")
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
                Dim cmSP As New SqlCommand("SPInsM_DocCode")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUTipeDoc.EditValue
                    .Parameters.Add("@FormDate", SqlDbType.VarChar).Value = Me.CBOFormat.EditValue
                    .Parameters.Add("@Pj", SqlDbType.VarChar).Value = Me.TBPj.EditValue
                    .Parameters.Add("@ISO", SqlDbType.VarChar).Value = Me.TBISO.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.TBCab.EditValue
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
                Dim cmSP As New SqlCommand("SPUpM_DocCode")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@DocID", SqlDbType.VarChar).Value = Me.SLUTipeDoc.EditValue
                    .Parameters.Add("@FormDate", SqlDbType.VarChar).Value = Me.CBOFormat.EditValue
                    .Parameters.Add("@Pj", SqlDbType.VarChar).Value = Me.TBPj.EditValue
                    .Parameters.Add("@ISO", SqlDbType.VarChar).Value = Me.TBISO.EditValue
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Me.CBOGol.EditValue
                    .Parameters.Add("@CabID", SqlDbType.VarChar).Value = Me.TBCab.EditValue
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

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBPj.KeyPress, TBCab.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub
 
End Class