Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FSchAcc
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,(Select Max(SchIDD) From T_SchPrAcc Where BOMID=SA.BOMID) As MaxSch,SchIDD, SA.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TgtUpp,Case when ETAAcc is null Then 'True' Else 'False' End As CekAcc,ETAAcc,Case when KetAcc='' Then 'True' Else 'False' End As CekKet,KetAcc,Case when TrmAcc is null Then 'True' Else 'False' End As CekTAcc,TrmAcc,SA.InsDate, SA.InsBy,SA.UpdDate,SA.UpdBy,convert(bit,'FALSE') As Baru From T_BOM B Inner Join T_SchPrAcc SA On B.BOMID=SA.BOMID Where SA.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False') Union All Select * From (Select Top 1 convert(bit,'FALSE') as Cek,ROW_NUMBER() over (ORDER BY SP.BOMID)*-1 as MaxSch,ROW_NUMBER() over (ORDER BY SP.BOMID)*-1 as SchIDD,SP.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TgtUpp,'True' As CekAcc,null as ETAAcc,'True' As CekKet, '' as KetAcc,'True' As CektAcc,null as tRMAcc,null as InsDate,null as InsBy,null as UpdDate,null as UpdBy,convert(bit,'True') As Baru From T_BOM B Left Outer Join T_SchPrPPIC SP On B.BOMID=SP.BOMID Where B.BOMID In (Select BOMID From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrAcc)) Order By SP.SchIDD desc)as x", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchPrAcc")
        Try
            DsMaster.Tables("T_SchPrAcc").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrAcc")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPrAcc"

        cmsl = New SqlDataAdapter("SPLSchProd", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.Add("Table", "SPLSchProd")
        Try
            DsMaster.Tables("SPLSchProd").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "SPLSchProd")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "SPLSchProd"
    End Sub

    Private Sub FSchProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillDt()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        Me.GridView1.ActiveFilter.Clear()
        Dim x As Integer

        Dim i : For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                If Me.GridView1.GetRowCellValue(i, "SchIDD") < 0 Then
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                        Dim cmSADtl As New SqlCommand("SPInsT_SchPrAcc")
                        cmSADtl.CommandType = CommandType.StoredProcedure

                        With cmSADtl
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TgtUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtUpp")
                            .Parameters.Add("@ETAAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETAAcc")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetAcc")
                            .Parameters.Add("@TrmAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmAcc")
                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                        End With

                        With koneksi
                            .Open()
                            cmSADtl.ExecuteNonQuery()
                            x = cmSADtl.Parameters("@Return").Value
                            .Close()
                        End With
                    End If
                Else

                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                        Dim cmSADtl As New SqlCommand("SPUpT_SchPrAcc")
                        cmSADtl.CommandType = CommandType.StoredProcedure

                        With cmSADtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TgtUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtUpp")
                            .Parameters.Add("@ETAAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETAAcc")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetAcc")
                            .Parameters.Add("@TrmAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmAcc")
                            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                        End With

                        With koneksi
                            .Open()
                            cmSADtl.ExecuteNonQuery()
                            x = cmSADtl.Parameters("@Return").Value
                            .Close()
                        End With
                    End If
                End If
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        ElseIf x = 1 Then
            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Close()
    End Sub

    Private Sub GridView1_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles GridView1.FocusedColumnChanged
        If GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Cek") = True Then
            If GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Baru") = True Then
                Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
            Else
                Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
            End If

            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "MaxSch") = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SchIDD") Then
                Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekAcc")
                Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTAcc")
                Me.GridView1.Columns("KetAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")

            Else
                Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("KetAcc").OptionsColumn.AllowEdit = False
            End If
        Else
            Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        End If

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Not IsDBNull(Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "Cek")) = True Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "Baru")) Then
                If Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "Baru") = True Then
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
                Else
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
                End If

                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "MaxSch") = Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "SchIDD") Then
                    Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "CekAcc")
                    Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "CekTAcc")
                    Me.GridView1.Columns("KetAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "CekKet")

                Else
                    Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KetAcc").OptionsColumn.AllowEdit = False
                End If
            End If
        Else
            Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        End If


    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "SchIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MaxSch", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "KetAcc", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Baru", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekAcc", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekTAcc", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekKet", True)

            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True

            'AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBOMID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBOMID.ButtonClick
        Dim frm As New FSearch("BOM Sch", "", "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("ArtName").ToString)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Warna", dataTrans.Item("Warna").ToString)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TotPsg", dataTrans.Item("TotPsg").ToString)

            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Top 1 (Select Top 1 TgtUpp From T_SchPrPPIC Where BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc) as TgtUpp,ETAAcc,Isnull(KetAcc,'') As Ket,TrmAcc From T_SchPrAcc SA Right Outer Join T_SchPrPPIC SC On SA.BOMID=SC.BOMID and SA.TgtUpp=SC.TgtUpp Where SC.BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SA.SchIDD desc", koneksi)

            With koneksi
                .Open()
                Reader = command.ExecuteReader

                If Reader.HasRows Then
                    While Reader.Read
                        If IsDBNull(Reader.Item(0)) = True Then

                        Else
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TgtUpp", Reader.Item(0))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ETAAcc", Reader.Item(1))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KetAcc", Reader.Item(2))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmAcc", Reader.Item(3))
                          
                        End If
                    End While
                End If
                Reader.Close()
                .Close()
            End With

            Dim i : For i = 0 To GridView1.RowCount - 1
                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                    If Me.GridView1.GetRowCellValue(i, "BOMID") = dataTrans.Item("Kode").ToString Then
                        Me.GridView1.SetRowCellValue(i, "MaxSch", Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SchIDD"))
                    End If
                End If
            Next

        End If
    End Sub

    Private Sub GridView1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If GridView1.GetRowCellValue(e.RowHandle, "MaxSch") = GridView1.GetRowCellValue(e.RowHandle, "SchIDD") Then
                    e.Appearance.ForeColor = Color.Black
                    e.Appearance.BackColor = Color.Yellow
                Else
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class