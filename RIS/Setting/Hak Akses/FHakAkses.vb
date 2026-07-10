Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FHakAkses
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim arrPar(-1) As String
    Dim CekAll As Boolean

    Private Sub FHakAkses_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Hak Akses"
    End Sub

    Private Sub FHakAkses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BVTHakAkses_s.Selected = True

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,PosisiID From M_Posisi Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_PosisiLUE")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_PosisiLUE")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_PosisiLUE"
    End Sub

    Private Sub BSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer
        Try

            Dim i : For i = 0 To DsMaster.Tables("M_Akses").Rows.Count - 1
                Dim y : For y = 0 To DsMaster.Tables("M_PosisiLUE").Rows.Count - 1
                    If DsMaster.Tables("M_PosisiLUE").Rows(y).Item("Cek") = True Then
                        Dim cmSPDtl As New SqlCommand("SPUpM_Akses")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@MenuID", SqlDbType.VarChar).Value = DsMaster.Tables("M_Akses").Rows(i).Item("MenuID")
                            .Parameters.Add("@PosisiID", SqlDbType.VarChar).Value = DsMaster.Tables("M_PosisiLUE").Rows(y).Item("PosisiID")
                            .Parameters.Add("@Akses", SqlDbType.Bit).Value = DsMaster.Tables("M_Akses").Rows(i).Item(DsMaster.Tables("M_PosisiLUE").Rows(y).Item("PosisiID"))
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
            Next

            If x = 0 Then
                XtraMessageBox.Show("Data Successfully Changed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Me.Close()
            ElseIf x = 1 Then
                XtraMessageBox.Show("Id Could Not Be Same", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                XtraMessageBox.Show("Data Failed Changed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            XtraMessageBox.Show("Data Failed Changed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If GridView1.GetRowCellValue(e.RowHandle, "Parent") = "True" Then
                    e.Appearance.BackColor = Color.Yellow
                    e.Appearance.ForeColor = Color.Black

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        cmsl = New SqlDataAdapter("Select MenuID,Nama,Parent From M_Menu Order By Urut asc", koneksi)
        cmsl.TableMappings.Add("Table", "M_Menu")
        cmsl.Fill(DsMaster, "M_Menu")

        Dim DtTable As New DataTable("M_Akses")
        DsMaster.Tables.Add(DtTable)

        DsMaster.Tables("M_Akses").Columns.Add("MenuID")
        DsMaster.Tables("M_Akses").Columns.Add("Nama")
        DsMaster.Tables("M_Akses").Columns.Add("Parent")

        Dim t As Integer = 0
        Dim PosisiID As String

        Dim s : For s = 0 To DsMaster.Tables("M_PosisiLUE").Rows.Count - 1
            If DsMaster.Tables("M_PosisiLUE").Rows(s).Item("Cek") = True Then
                DsMaster.Tables("M_Akses").Columns.Add(DsMaster.Tables("M_PosisiLUE").Rows(s).Item("PosisiID"), GetType(Boolean))
                t += 1
                If t = 1 Then
                    PosisiID = "'" & DsMaster.Tables("M_PosisiLUE").Rows(s).Item("PosisiID") & "'"
                Else
                    PosisiID &= ",'" & DsMaster.Tables("M_PosisiLUE").Rows(s).Item("PosisiID") & "'"
                End If
            End If
        Next

        cmsl = New SqlDataAdapter("Select MenuID,PosisiID,Akses From M_Akses Where PosisiID In (" & PosisiID & ")", koneksi)
        cmsl.TableMappings.Add("Table", "M_AksesTemp")
        cmsl.Fill(DsMaster, "M_AksesTemp")

        Dim x : For x = 0 To DsMaster.Tables("M_Menu").Rows.Count - 1
            DtTable.Rows.Add()

            Dim y : For y = 0 To DsMaster.Tables("M_PosisiLUE").Rows.Count - 1
                Dim z : For z = 0 To DsMaster.Tables("M_AksesTemp").Rows.Count - 1
                    If DsMaster.Tables("M_PosisiLUE").Rows(y).Item("Cek") = True Then
                        If DsMaster.Tables("M_AksesTemp").Rows(z).Item("MenuID") = DsMaster.Tables("M_Menu").Rows(x).Item("MenuID") And DsMaster.Tables("M_AksesTemp").Rows(z).Item("PosisiID") = DsMaster.Tables("M_PosisiLUE").Rows(y).Item("PosisiID") Then
                            DtTable.Rows(x).Item("MenuID") = DsMaster.Tables("M_Menu").Rows(x).Item("MenuID")
                            DtTable.Rows(x).Item("Nama") = DsMaster.Tables("M_Menu").Rows(x).Item("Nama")
                            DtTable.Rows(x).Item("Parent") = DsMaster.Tables("M_Menu").Rows(x).Item("Parent")

                            DtTable.Rows(x).Item(DsMaster.Tables("M_PosisiLUE").Rows(y).Item("PosisiID")) = DsMaster.Tables("M_AksesTemp").Rows(z).Item("Akses")
                        End If
                    End If
                Next
            Next
        Next

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Akses"

        Me.GridView1.Columns("MenuID").Width = 210
        Me.GridView1.Columns("Nama").Width = 270
        Me.GridView1.Columns("Parent").Visible = False

        Me.GridView1.Columns("MenuID").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Nama").OptionsColumn.AllowEdit = False

        Dim a : For a = 0 To DsMaster.Tables("M_PosisiLUE").Rows.Count - 1
            If DsMaster.Tables("M_PosisiLUE").Rows(a).Item("Cek") = True Then

                Me.GridView1.Columns(DsMaster.Tables("M_PosisiLUE").Rows(a).Item("PosisiID")).Width = 140
            End If
        Next

        Me.GridView1.Columns("MenuID").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridView1.Columns("Nama").OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

        Me.BVTHakAkses.Selected = True
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To GridView2.RowCount - 1
                GridView2.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To GridView2.RowCount - 1
                GridView2.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub
End Class