Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FKetLate
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsNotif3 As New System.Data.DataSet

    Private Sub FKetLate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,TrmID,T.POID,S.Nama As Supp,T.Tanggal,T.TglScK,KetLate From T_TrmBB T Inner Join T_POBB P On T.POID=P.POID Inner Join M_Supp S On T.SuppID=S.SuppID Where T.Tanggal>T.TglScK and T.Jenis='Stock' and KetLate='' Order By TrmID", koneksi)
        cmsl.TableMappings.Add("Table", "T_KetLate")
        cmsl.Fill(DsNotif3, "T_KetLate")
        DsNotif3.Tables("T_KetLate").Clear()
        cmsl.Fill(DsNotif3, "T_KetLate")

        Me.GridControl1.DataSource = DsNotif3
        Me.GridControl1.DataMember = "T_KetLate"

        cmsl = New SqlDataAdapter("Select T.TrmID,B.BBID,B.Nama as Bahan,Qty From T_TrmBB T Inner Join T_TrmBBDtl TD On T.TrmID=TD.TrmID Inner Join M_BB B On TD.BBID=B.BBID Where T.Tanggal>T.TglScK and KetLate='' ", koneksi)
        cmsl.TableMappings.Add("Table", "T_TrmDtlL")
        cmsl.Fill(DsNotif3, "T_TrmDtlL")
        DsNotif3.Tables("T_TrmDtlL").Clear()
        cmsl.Fill(DsNotif3, "T_TrmDtlL")

        Me.GridControl2.DataSource = DsNotif3
        Me.GridControl2.DataMember = "T_TrmDtlL"

        If Me.GridView1.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[TrmID] = '" & Me.GridView1.GetFocusedRowCellValue("TrmID") & "'"
        End If
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer

        For i As Integer = 0 To Me.GridView1.RowCount - 1
            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "Cek")) And Me.GridView1.GetRowCellValue(i, "Cek") = True Then

                Dim cmSPDtl As New SqlCommand("SPUpT_TrmBBKetLate")
                cmSPDtl.CommandType = CommandType.StoredProcedure

                With cmSPDtl
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "TrmID")
                    .Parameters.Add("@KetLate", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetLate")
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

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Cek") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "Cek") = True Then
                Me.GridColumn7.OptionsColumn.AllowEdit = True
            Else
                Me.GridColumn7.OptionsColumn.AllowEdit = False
            End If
        End If

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If Me.GridView1.RowCount > 0 Then

                If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                    Me.GridColumn7.OptionsColumn.AllowEdit = True
                Else
                    Me.GridColumn7.OptionsColumn.AllowEdit = False
                End If

                Me.GridView2.ActiveFilterString = "[TrmID] = '" & Me.GridView1.GetFocusedRowCellValue("TrmID") & "'"

            End If
        End If
    End Sub
End Class