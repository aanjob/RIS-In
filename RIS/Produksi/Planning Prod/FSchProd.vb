Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FSchProd
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,(Select Max(SchIDD) From T_SchPr Where BOMID=SD.BOMID) As MaxSch,SchIDD,SD.BOMID, ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TglCutt,TglJht,TglAss,Case when RealCutt is null Then 'True' Else 'False' End As CekRCutt,RealCutt, Case when RealJht is null Then 'True' Else 'False' End As CekRJht,RealJht,Case when RealAss is null Then 'True' Else 'False' End As CekRAss, RealAss,Case when SD.KetProd='' Then 'True' Else 'False' End As CekKet,SD.KetProd,B.TglKirim,Case when RealKrm is null Then 'True' Else 'False' End As CekRKrm, RealKrm,SD.InsDate,SD.InsBy,SD.UpdDate,SD.UpdBy,convert(bit,'FALSE') As Baru From T_BOM B Inner Join T_SchPr SD On B.BOMID=SD.BOMID Where SD.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False') Union All Select * From (Select Top 1 convert(bit,'FALSE') as Cek,ROW_NUMBER() over (ORDER BY SD.BOMID)*-1 as MaxSch, ROW_NUMBER() over (ORDER BY SD.BOMID)*-1 as SchIDD,SD.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TglCutt,TglJht,TglAss, 'True' As CekRCutt,null as RealCutt,'True' As CekRCJht,null as RealJht,'True' As CekRAss,null as RealAss,'True' As CekKet,'' as KetProd,B.TglKirim, 'True' As CekRKrm,null as RealKrm,null as InsDate,null as InsBy,null as UpdDate,null as UpdBy,convert(bit,'True') As Baru From T_BOM B Left Outer Join T_SchPrPPIC SD On B.BOMID=SD.BOMID Where B.BOMID In (Select BOMID From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPr)) Order By SD.SchIDD desc)as x", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchPr")
        Try
            DsMaster.Tables("T_SchPr").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPr")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPr"

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
                        Dim cmSPDtl As New SqlCommand("SPInsT_SchPr")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TglCutt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglCutt")
                            .Parameters.Add("@TglJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglJht")
                            .Parameters.Add("@TglAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAss")
                            .Parameters.Add("@RealCutt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealCutt")
                            .Parameters.Add("@RealJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealJht")
                            .Parameters.Add("@RealAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealAss")
                            .Parameters.Add("@RealKrm", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealKrm")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetProd")
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
                Else

                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BOMID")) Then
                        Dim cmSPDtl As New SqlCommand("SPUpT_SchPr")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TglCutt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglCutt")
                            .Parameters.Add("@TglJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglJht")
                            .Parameters.Add("@TglAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAss")
                            .Parameters.Add("@RealCutt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealCutt")
                            .Parameters.Add("@RealJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealJht")
                            .Parameters.Add("@RealAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealAss")
                            .Parameters.Add("@RealKrm", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "RealKrm")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetProd")
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
                Me.GridView1.Columns("RealCutt").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekRCutt")
                Me.GridView1.Columns("RealJht").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekRJht")
                Me.GridView1.Columns("RealAss").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekRAss")
                Me.GridView1.Columns("KetProd").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")
            Else
                Me.GridView1.Columns("RealCutt").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("RealJht").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("RealAss").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("KetProd").OptionsColumn.AllowEdit = False
            End If
        Else
            Me.GridView1.Columns("RealCutt").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("RealJht").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("RealAss").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetProd").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Not IsDBNull(Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "Cek")) = True Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(e.FocusedRowHandle, "Baru")) Then
                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Baru") = True Then
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True
                Else
                    Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
                End If

                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "MaxSch") = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SchIDD") Then
                    Me.GridView1.Columns("RealCutt").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekRCutt")
                    Me.GridView1.Columns("RealJht").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekRJht")
                    Me.GridView1.Columns("RealAss").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekRAss")
                    Me.GridView1.Columns("KetProd").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")
                Else
                    Me.GridView1.Columns("RealCutt").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("RealJht").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("RealAss").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KetProd").OptionsColumn.AllowEdit = False
                End If
            End If
        Else
            Me.GridView1.Columns("RealCutt").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("RealJht").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("RealAss").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetProd").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "SchIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MaxSch", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "KetProd", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Baru", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekRCutt", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekRJht", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekRAss", True)
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
            Dim command As New SqlCommand("Select Top 1 (Select Top 1 TglCutt From T_SchPrPPIC Where BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc) As TglCutt,(Select Top 1 TglJht From T_SchPrPPIC Where BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc) As TglJht,(Select Top 1 TglAss From T_SchPrPPIC Where BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc) As TglAss,RealCutt,RealJht,RealAss,Isnull(KetProd,'') As Ket,(Select TglKirim From T_BOM Where BOMID='" & dataTrans.Item("Kode").ToString & "') as TglKirim,RealKrm From T_SchPr SD Right Outer Join T_SchPrPPIC SC On SC.BOMID=SD.BOMID and SC.TglCutt=SD.TglCutt and SC.TglJht=SD.TglJht and SC.TglAss=SD.TglAss Where SC.BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SD.SchIDD desc", koneksi)

            With koneksi
                .Open()
                Reader = command.ExecuteReader

                If Reader.HasRows Then
                    While Reader.Read
                        If IsDBNull(Reader.Item(0)) = True Then

                        Else
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglCutt", Reader.Item(0))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglJht", Reader.Item(1))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglAss", Reader.Item(2))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "RealCutt", Reader.Item(3))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "RealJht", Reader.Item(4))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "RealAss", Reader.Item(5))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KetProd", Reader.Item(6))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglKirim", Reader.Item(7))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "RealKrm", Reader.Item(8))

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