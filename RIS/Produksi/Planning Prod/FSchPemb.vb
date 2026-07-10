Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class FSchPemb
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,(Select Max(SchIDD) From T_SchPrPemb Where BOMID=SC.BOMID) As MaxSch,SchIDD, SC.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TgtUpp,TgtBott,Case when ETALeat is null Then 'True' Else 'False' End As CekLeat,ETALeat, Case when ETASint is null Then 'True' Else 'False' End As CekSint,ETASint,Case when ETAAcc is null Then 'True' Else 'False' End As CekAcc, ETAAcc,Case when ETABott is null Then 'True' Else 'False' End As CekBott,ETABott,Case when ETAFin is null Then 'True' Else 'False' End As CekFin,ETAFin,Case when KetPemb='' Then 'True' Else 'False' End As CekKet,KetPemb,Case when TrmLeat is null Then 'True' Else 'False' End As CekTLeat,TrmLeat,Case when TrmSint is null Then 'True' Else 'False' End As CekTSint,TrmSint,Case when TrmAcc is null Then 'True' Else 'False' End As CekTAcc,TrmAcc,Case when TrmBott is null Then 'True' Else 'False' End As CekTBott,TrmBott,Case when TrmFin is null Then 'True' Else 'False' End As CekTFin,TrmFin,SC.InsDate,SC.InsBy,SC.UpdDate,SC.UpdBy,convert(bit,'FALSE') As Baru From T_BOM B Inner Join T_SchPrPemb SC On B.BOMID=SC.BOMID Where SC.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False') Union All Select convert(bit,'FALSE') as Cek,ROW_NUMBER() over (ORDER BY SP.BOMID)*-1 as MaxSch,ROW_NUMBER() over (ORDER BY SP.BOMID)*-1 as SchIDD,SP.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TgtUpp,TgtBott,'True' As CekLeat,null as ETALeat,'True' As CekSint,null as ETASint,'True' As CekAcc,null as ETAAcc, 'True' As CekBott,null as ETABott,'True' As CekFin,null as ETAFin,'True' As CekKet, '' as KetPemb,'True' As CekTLeat,null as TrmLeat,'True' As CekTSint,null as TrmSint,'True' As CekTAcc,null as TrmAcc,'True' As CekTBott,null as TrmBott,'True' As CekTFin,null as TrmFin,null as InsDate,null as InsBy,null as UpdDate,null as UpdBy,convert(bit,'True') As Baru From T_BOM B Left Outer Join T_SchPrPPIC SP On B.BOMID=SP.BOMID Where B.BOMID In (Select BOMID From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrPemb))", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchPrPemb")
        Try
            DsMaster.Tables("T_SchPrPemb").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrPemb")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPrPemb"

        cmsl = New SqlDataAdapter("Select Distinct SC.BOMID,ArtName,Warna,TotPsg+TotPsgPol As TotPsg,TglPO,TglSpec,SC.TgtUpp,SC.TgtBott,ETALeat, ETASint,ETAAcc,ETABott,ETAFin,KetPemb,TrmLeat,TrmSint,TrmAcc,TrmBott,TrmFin,ETATool,KetTool,TrmTool,SC.TglCutt,SC.TglJht,KetPPIC,SC.TglAss,RealCutt,RealJht,RealAss,SD.KetProd,B.TglKirim,RealKrm From T_BOM B Inner Join T_SchPrPPIC SC On B.BOMID=SC.BOMID Left Outer Join T_SchPrPemb SP On SC.BOMID=SP.BOMID and SC.TgtUpp=SP.TgtUpp and SC.TgtBott=SP.TgtBott Left Outer Join T_SchPrTool ST On ST.BOMID=SP.BOMID and SC.TgtUpp=SP.TgtUpp and SC.TgtBott=SP.TgtBott Left Outer Join T_SchPr SD On SC.BOMID=SD.BOMID and SC.TglCutt=SD.TglCutt and SC.TglJht=SD.TglJht and SC.TglAss=SD.TglAss Where SC.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False') Order By SC.BOMID,SC.TgtUpp,SC.TgtBott", koneksi)

        cmsl.TableMappings.Add("Table", "T_SchPrView")
        Try
            DsMaster.Tables("T_SchPrView").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrView")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_SchPrView"
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
                        Dim cmSPDtl As New SqlCommand("SPInsT_SchPrPemb")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TgtUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtUpp")
                            .Parameters.Add("@TgtBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtBott")
                            .Parameters.Add("@ETALeat", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETALeat")
                            .Parameters.Add("@ETASint", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETASint")
                            .Parameters.Add("@ETAAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETAAcc")
                            .Parameters.Add("@ETABott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETABott")
                            .Parameters.Add("@ETAFin", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETAFin")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetPemb")
                            .Parameters.Add("@TrmLeat", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmLeat")
                            .Parameters.Add("@TrmSint", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmSint")
                            .Parameters.Add("@TrmAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmAcc")
                            .Parameters.Add("@TrmBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmBott")
                            .Parameters.Add("@TrmFin", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmFin")
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
                        Dim cmSPDtl As New SqlCommand("SPUpT_SchPrPemb")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "SchIDD")
                            .Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                            .Parameters.Add("@TgtUpp", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtUpp")
                            .Parameters.Add("@TgtBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TgtBott")
                            .Parameters.Add("@ETALeat", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETALeat")
                            .Parameters.Add("@ETASint", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETASint")
                            .Parameters.Add("@ETAAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETAAcc")
                            .Parameters.Add("@ETABott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETABott")
                            .Parameters.Add("@ETAFin", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "ETAFin")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KetPemb")
                            .Parameters.Add("@TrmLeat", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmLeat")
                            .Parameters.Add("@TrmSint", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmSint")
                            .Parameters.Add("@TrmAcc", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmAcc")
                            .Parameters.Add("@TrmBott", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmBott")
                            .Parameters.Add("@TrmFin", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TrmFin")
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
                Me.GridView1.Columns("ETALeat").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekLeat")
                Me.GridView1.Columns("ETASint").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekSint")
                Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekAcc")
                Me.GridView1.Columns("ETABott").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekBott")
                Me.GridView1.Columns("ETAFin").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekFin")
                Me.GridView1.Columns("TrmLeat").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTLeat")
                Me.GridView1.Columns("TrmSint").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTSint")
                Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTAcc")
                Me.GridView1.Columns("TrmBott").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTBott")
                Me.GridView1.Columns("TrmFin").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTFin")
                Me.GridView1.Columns("KetPemb").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")

            Else
                Me.GridView1.Columns("ETALeat").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("ETASint").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("ETABott").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("ETAFin").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TrmLeat").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TrmSint").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TrmBott").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TrmFin").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("KetPemb").OptionsColumn.AllowEdit = False
            End If
        Else
            Me.GridView1.Columns("ETALeat").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETASint").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETABott").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETAFin").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmLeat").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmSint").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmBott").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmFin").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetPemb").OptionsColumn.AllowEdit = False
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
                    Me.GridView1.Columns("ETALeat").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekLeat")
                    Me.GridView1.Columns("ETASint").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekSint")
                    Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekAcc")
                    Me.GridView1.Columns("ETABott").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekBott")
                    Me.GridView1.Columns("ETAFin").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekFin")
                    Me.GridView1.Columns("TrmLeat").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTLeat")
                    Me.GridView1.Columns("TrmSint").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTSint")
                    Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTAcc")
                    Me.GridView1.Columns("TrmBott").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTBott")
                    Me.GridView1.Columns("TrmFin").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekTFin")
                    Me.GridView1.Columns("KetPemb").OptionsColumn.AllowEdit = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CekKet")

                Else
                    Me.GridView1.Columns("ETALeat").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("ETASint").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("ETABott").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("ETAFin").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TrmLeat").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TrmSint").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TrmBott").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("TrmFin").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KetPemb").OptionsColumn.AllowEdit = False
                End If
            End If
        Else
            Me.GridView1.Columns("ETALeat").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETASint").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETAAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETABott").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("ETAFin").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmLeat").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmSint").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmAcc").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmBott").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TrmFin").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KetPemb").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BOMID").OptionsColumn.AllowEdit = False
        End If


    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Try
            'RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.SetRowCellValue(e.RowHandle, "SchIDD", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "MaxSch", Me.GridView1.RowCount * -1)
            Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "KetPemb", "")
            Me.GridView1.SetRowCellValue(e.RowHandle, "Cek", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "Baru", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekLeat", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekSint", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekAcc", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekBott", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekFin", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekKet", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekTLeat", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekTSint", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekTAcc", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekTBott", True)
            Me.GridView1.SetRowCellValue(e.RowHandle, "CekTFin", True)
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
            Dim command As New SqlCommand("Select Top 1 (Select Top 1 TgtUpp From T_SchPrPPIC Where BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc) as TgtUpp,(Select Top 1 TgtBott From T_SchPrPPIC Where BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SchIDD desc) as TgtBott,ETALeat,ETASint,ETAAcc,ETABott,ETAFin,Isnull(KetPemb,''),TrmLeat,TrmSint,TrmAcc,TrmBott, TrmFin From T_SchPrPemb SP Right Outer Join T_SchPrPPIC SC On SP.BOMID=SC.BOMID and SP.TgtUpp=SC.TgtUpp and SP.TgtBott=SC.TgtBott Where SC.BOMID='" & dataTrans.Item("Kode").ToString & "' Order By SP.SchIDD desc", koneksi)

            With koneksi
                .Open()
                Reader = command.ExecuteReader

                If Reader.HasRows Then
                    While Reader.Read
                        If IsDBNull(Reader.Item(0)) = True Then

                        Else
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TgtUpp", Reader.Item(0))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TgtBott", Reader.Item(1))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ETALeat", Reader.Item(2))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ETASint", Reader.Item(3))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ETAAcc", Reader.Item(4))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ETABott", Reader.Item(5))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ETAFin", Reader.Item(6))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KetPemb", Reader.Item(7))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmLeat", Reader.Item(8))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmSint", Reader.Item(9))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmAcc", Reader.Item(10))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmBott", Reader.Item(11))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "TrmFin", Reader.Item(12))
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