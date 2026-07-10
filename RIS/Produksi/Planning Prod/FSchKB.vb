Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FSchKB
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Private Sub FSchBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Cek,BOMID,Cust,ArtName,Warna,TotPsg,Case When TglKirimSc is null Then TglKirim Else TglKirimSc End TglKirim, TglUpp,TglSole,TglBott From (Select convert(bit,'FALSE') as Cek,B.BOMID,C.Nama As Cust,ArtName,Warna,TotPsg+TotPsgPol as TotPsg,TglKirim, (Select Top 1 TglKirim From T_SchKB Where BOMID=B.BOMID Order By ScIDD desc) as TglKirimSc,(Select Top 1 TglUpp From T_SchKB Where BOMID=B.BOMID Order By ScIDD desc) as TglUpp,(Select Top 1 TglSole From T_SchKB Where BOMID=B.BOMID Order By ScIDD desc) as TglSole,(Select Top 1 TglBott From T_SchKB Where BOMID=B.BOMID Order By ScIDD desc) as TglBott From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID where stsLunas='False' and stsBatal='False') as x", koneksi)
        cmsl.TableMappings.Add("Table", "T_SchKB")
        cmsl.Fill(DsMaster, "T_SchKB")
        DsMaster.Tables("T_SchKB").Clear()
        cmsl.Fill(DsMaster, "T_SchKB")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchKB"
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer

        For i As Integer = 0 To Me.GridView1.RowCount - 1
            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Cek")) And Me.GridView1.GetRowCellValue(i, "Cek") = True Then

                Dim cmSPDtl As New SqlCommand("SPInsT_SchKB")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglKirim")
                    .Parameters.Add("@TglUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglUpp")
                    .Parameters.Add("@TglSole", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglSole")
                    .Parameters.Add("@TglBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglBott")
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

                    If x <> 0 Then
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Close()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Cek") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "Cek") = True Then
                Me.GridColumn7.OptionsColumn.AllowEdit = True
                Me.GridColumn9.OptionsColumn.AllowEdit = True
                Me.GridColumn10.OptionsColumn.AllowEdit = True
            Else
                Me.GridColumn7.OptionsColumn.AllowEdit = False
                Me.GridColumn9.OptionsColumn.AllowEdit = False
                Me.GridColumn10.OptionsColumn.AllowEdit = False
            End If
        End If

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
            Me.GridColumn7.OptionsColumn.AllowEdit = True
            Me.GridColumn9.OptionsColumn.AllowEdit = True
            Me.GridColumn10.OptionsColumn.AllowEdit = True
        Else
            Me.GridColumn7.OptionsColumn.AllowEdit = False
            Me.GridColumn9.OptionsColumn.AllowEdit = False
            Me.GridColumn10.OptionsColumn.AllowEdit = False
        End If
    End Sub
End Class