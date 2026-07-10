Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FSchPPIC
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,(Select Max(SchIDD) From T_SchPrPPIC Where BOMID=SC.BOMID) As MaxSch,SchIDD, SC.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TglPO,Case when TglSpec is null Then 'True' Else 'False' End As CekSpec,TglSpec,Case when SC.TgtUpp is null Then 'True' Else 'False' End As CekUpp,SC.TgtUpp,Case when SC.TgtBott is null Then 'True' Else 'False' End As CekBott, SC.TgtBott,Case when SC.TglCutt is null Then 'True' Else 'False' End As CekCutt,SC.TglCutt,Case when SC.TglJht is null Then 'True' Else 'False' End As CekJht, SC.TglJht,Case when SC.TglAss is null Then 'True' Else 'False' End As CekAss,SC.TglAss,Case when SC.KetPPIC='' Then 'True' Else 'False' End As CekKet,KetPPIC,convert(bit,'FALSE') As Baru,SC.InsDate,SC.InsBy,SC.UpdDate,SC.UpdBy From T_BOM B Inner Join T_SchPrPPIC SC On B.BOMID=SC.BOMID Where SC.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchPrPPIC")
        Try
            DsMaster.Tables("T_SchPrPPIC").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrPPIC")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPrPPIC"

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

        'cmsl = New SqlDataAdapter("Select Distinct SC.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TglPO,TglSpec,SC.TgtUpp,SC.TgtBott,ETALeat, KetLeat,ETASint,KetSint,ETAAcc,KetAcc,ETABott,KetBott,ETAFin,KetFin,TrmLeat,TrmSint,TrmAcc,TrmBott,TrmFin,ETATool,KetTool,TrmTool, SC.TglCutt,SC.TglJht,KetPPIC,SC.TglAss,RealCutt,RealJht,RealAss,SD.KetProd,B.TglKirim,RealKrm From T_BOM B Inner Join T_SchPrPPIC SC On B.BOMID=SC.BOMID Left Outer Join T_SchPrLeat SL On SC.BOMID=SL.BOMID and SC.TgtUpp=SL.TgtUpp Left Outer Join T_SchPrSint SS On SC.BOMID=SS.BOMID and SC.TgtUpp=SS.TgtUpp Left Outer Join T_SchPrAcc SA On SC.BOMID=SA.BOMID and SC.TgtUpp=SA.TgtUpp Left Outer Join T_SchPrBott SB On SC.BOMID=SB.BOMID and SC.TgtBott=SB.TgtBott Left Outer Join T_SchPrFin SF On SC.BOMID=SF.BOMID and SC.TgtBott=SF.TgtBott Left Outer Join T_SchPrTool ST On ST.BOMID=ST.BOMID and SC.TgtUpp=ST.TgtUpp and SC.TgtBott=ST.TgtBott Left Outer Join T_SchPr SD On SC.BOMID=SD.BOMID and SC.TglCutt=SD.TglCutt and SC.TglJht=SD.TglJht and SC.TglAss=SD.TglAss Where SC.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False') Order By SC.BOMID,SC.TgtUpp,SC.TgtBott", koneksi)

        'cmsl.TableMappings.Add("Table", "T_SchPrView")
        'Try
        '    DsMaster.Tables("T_SchPrView").Clear()
        'Catch ex As Exception

        'End Try
        'cmsl.Fill(DsMaster, "T_SchPrView")

        'Me.GridControl2.DataSource = DsMaster
        'Me.GridControl2.DataMember = "T_SchPrView"
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
                        Dim cmSPDtl As New SqlCommand("SPInsT_SchPrPPIC")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TglPO", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglPO")
                            .Parameters.Add("@TglSpec", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglSpec")
                            .Parameters.Add("@TgtUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtUpp")
                            .Parameters.Add("@TgtBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtBott")
                            .Parameters.Add("@TglCutt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglCutt")
                            .Parameters.Add("@TglJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglJht")
                            .Parameters.Add("@TglAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAss")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetPPIC")
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
                        Dim cmSPDtl As New SqlCommand("SPUpT_SchPrPPIC")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TglPO", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglPO")
                            .Parameters.Add("@TglSpec", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglSpec")
                            .Parameters.Add("@TgtUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtUpp")
                            .Parameters.Add("@TgtBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtBott")
                            .Parameters.Add("@TglCutt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglCutt")
                            .Parameters.Add("@TglJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglJht")
                            .Parameters.Add("@TglAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAss")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetPPIC")
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
                Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekSpec")
                Me.GridView1.Columns("TgtUpp").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekUpp")
                Me.GridView1.Columns("TgtBott").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekBott")
                Me.GridView1.Columns("TglCutt").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekCutt")
                Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekJht")
                Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekAss")
                Me.GridView1.Columns("KetPPIC").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")
            Else
                Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TgtUpp").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TgtBott").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglCutt").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("KetPPIC").OptionsColumn.AllowEdit = False
            End If
        Else
            Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TgtUpp").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TgtBott").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglCutt").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetPPIC").OptionsColumn.AllowEdit = False
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
                    Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekSpec")
                    Me.GridView1.Columns("TgtUpp").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekUpp")
                    Me.GridView1.Columns("TgtBott").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekBott")
                    Me.GridView1.Columns("TglCutt").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekCutt")
                    Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekJht")
                    Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekAss")
                    Me.GridView1.Columns("KetPPIC").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")
                Else
                    Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TgtUpp").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TgtBott").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TglCutt").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KetPPIC").OptionsColumn.AllowEdit = False
                End If
            End If
        Else
            Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TgtUpp").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TgtBott").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglCutt").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetPPIC").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "SchIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MaxSch", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "KetPPIC", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Baru", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekSpec", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekUpp", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekBott", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekCutt", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekJht", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekAss", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekKet", True)

            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = True

            'AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdBOMID_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdBOMID.ButtonClick
        Dim frm As New FSearch("BOM", "", "", "", Date.Now, "")
        frm.ShowDialog()

        If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
            GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ArtName", dataTrans.Item("ArtName").ToString)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Warna", dataTrans.Item("Warna").ToString)
            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TotPsg", dataTrans.Item("TotPsg").ToString)

            Dim Reader As SqlClient.SqlDataReader
            Dim command As New SqlCommand("Select Top 1 (Select Tanggal From T_POBJJO Where POID=B.POID Union All Select Tanggal From T_POBJLk Where POID=B.POID) as TglPO,TglSpec,TgtUpp,TgtBott,TglCutt,TglJht,TglAss,Isnull(KetPPIC,'') From T_SchPrPPIC SC Right Outer Join T_BOM B On SC.BOMID=B.BOMID Where B.BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc", koneksi)

            With koneksi
                .Open()
                Reader = command.ExecuteReader

                If Reader.HasRows Then
                    While Reader.Read
                        If IsDBNull(Reader.Item(0)) = True Then
                           
                        Else
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglPO", Reader.Item(0))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglSpec", Reader.Item(1))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TgtUpp", Reader.Item(2))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TgtBott", Reader.Item(3))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglCutt", Reader.Item(4))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglJht", Reader.Item(5))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TglAss", Reader.Item(6))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KetPPIC", Reader.Item(7))
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