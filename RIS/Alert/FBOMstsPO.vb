Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Public Class FBOMstsPO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsNotif2 As New System.Data.DataSet

    Private Sub FBOMstsPO_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            DsNotif2.Tables("DivPO").Clear()
            DsNotif2.Tables("T_BOMstsPO").Clear()
            DsNotif2.Tables("T_BOMstsPOTemp").Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FBOMstsPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DsNotif2 = New System.Data.DataSet

        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim cmSP As New SqlCommand("SPUpstsBOMPO")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim q As Integer

        With cmSP
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            Try
                With koneksi
                    .Open()
                    cmSP.ExecuteNonQuery()
                    q = cmSP.Parameters("@Return").Value
                    .Close()
                End With

            Catch ex As Exception
                XtraMessageBox.Show("Status BOM-PO Gagal Diupdate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With

        RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct B.DivPO From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join T_BOMstsPO P On H.BOMID=P.BOMID and P.DivPO=B.DivPO Where Tanggal>='2020/06/01' and stsLunas=0 and H.BOMID In (Select BOMID From T_BOMstsPO where stsPO='False' and cmPO='False') Order By DivPO", koneksi)

        cmsl.TableMappings.Add("Table", "DivPO")
        cmsl.Fill(DsNotif2, "DivPO")

        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,H.BOMID,C.Nama As Cust,ArtName,TotPsg+TotPsgPol As TotQty From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMstsPO P On H.BOMID=P.BOMID  and P.DivPO=B.DivPO Where Tanggal>='2020/06/01' and stsLunas=0 and H.BOMID In (Select BOMID From T_BOMstsPO where stsPO='False' and cmPO='False') Order By H.BOMID", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMstsPO")
        Try
            DsNotif2.Tables("T_BOMstsPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif2, "T_BOMstsPO")


        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,H.BOMID,C.Nama As Cust,ArtName,TotPsg+TotPsgPol As TotQty,B.DivPO,cmPO From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMstsPO P On H.BOMID=P.BOMID and P.DivPO=B.DivPO Where Tanggal>='2020/06/01' and stsLunas=0 and H.BOMID In (Select BOMID From T_BOMstsPO where stsPO='False' and cmPO='False') Order By H.BOMID,DivPO", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMstsPOTemp")
        cmsl.Fill(DsNotif2, "T_BOMstsPOTemp")

        Me.GridControl1.DataSource = DsNotif2
        Me.GridControl1.DataMember = "T_BOMstsPO"

        Dim y : For y = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1

            DsNotif2.Tables("T_BOMstsPO").Columns.Add(DsNotif2.Tables("DivPO").Rows(y).Item("DivPO"), GetType(Boolean)).Caption = DsNotif2.Tables("T_BOMstsPOTemp").Rows(y).Item("DivPO")

            Me.GridView1.PopulateColumns(DsNotif2.Tables("T_BOMstsPO"))

            Dim x : For x = 0 To DsNotif2.Tables("T_BOMstsPO").Rows.Count - 1
                Dim z : For z = 0 To DsNotif2.Tables("T_BOMstsPOTemp").Rows.Count - 1

                    If DsNotif2.Tables("T_BOMstsPO").Rows(x).Item("BOMID") = DsNotif2.Tables("T_BOMstsPOTemp").Rows(z).Item("BOMID") And DsNotif2.Tables("DivPO").Rows(y).Item("DivPO") = DsNotif2.Tables("T_BOMstsPOTemp").Rows(z).Item("DivPO") Then

                        Me.GridView1.SetRowCellValue(x, DsNotif2.Tables("DivPO").Rows(y).Item("DivPO"), CBool(DsNotif2.Tables("T_BOMstsPOTemp").Rows(z).Item("cmPO")))

                        Exit For
                    End If

                Next
            Next

        Next

        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,H.BOMID,C.Nama As Cust,ArtName,TotPsg+TotPsgPol As TotQty,B.DivPO,cmPO From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMstsPO P On H.BOMID=P.BOMID and P.DivPO=B.DivPO Where Tanggal>='2020/06/01' and stsLunas=0 and H.BOMID In (Select BOMID From T_BOMstsPO where stsPO='False' and cmPO='False') Order By BOMID,DivPO", koneksi)

        Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Cust").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("ArtName").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("TotQty").OptionsColumn.AllowEdit = False

        Dim s : For s = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1
            Me.GridView1.Columns(DsNotif2.Tables("DivPO").Rows(s).Item("DivPO")).OptionsColumn.AllowEdit = False
        Next

        Me.GridView1.Columns("BOMID").Width = 150
        Me.GridView1.Columns("Cust").Width = 180
        Me.GridView1.Columns("ArtName").Width = 120
        Me.GridView1.Columns("TotQty").Width = 80

        AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

    End Sub


    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Me.GridView1.ActiveFilterString = "[Cek] = True"

        Dim x As Integer

        Try
            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                    Dim z : For z = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1
                        If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                            Dim cmSPDtl As New SqlCommand("SPUpT_BOMstsPO")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl

                                .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                                .Parameters.Add("@DivPO", SqlDbType.VarChar).Value = DsNotif2.Tables("DivPO").Rows(z).Item("DivPO")
                                .Parameters.Add("@cmPO", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, DsNotif2.Tables("DivPO").Rows(z).Item("DivPO"))
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
                                XtraMessageBox.Show("Status PO Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            XtraMessageBox.Show("Status PO Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try


        If x = 0 Then
            XtraMessageBox.Show("Status PO Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            XtraMessageBox.Show("Status PO  Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Close()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Cek") Then

            If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                Dim s : For s = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1
                    Me.GridView1.Columns(DsNotif2.Tables("DivPO").Rows(s).Item("DivPO")).OptionsColumn.AllowEdit = True
                Next

            Else
                Dim s : For s = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1
                    Me.GridView1.Columns(DsNotif2.Tables("DivPO").Rows(s).Item("DivPO")).OptionsColumn.AllowEdit = False
                Next
            End If

        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
            Dim s : For s = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1
                Me.GridView1.Columns(DsNotif2.Tables("DivPO").Rows(s).Item("DivPO")).OptionsColumn.AllowEdit = True
            Next

        Else
            Dim s : For s = 0 To DsNotif2.Tables("DivPO").Rows.Count - 1
                Me.GridView1.Columns(DsNotif2.Tables("DivPO").Rows(s).Item("DivPO")).OptionsColumn.AllowEdit = False
            Next

        End If
    End Sub
End Class