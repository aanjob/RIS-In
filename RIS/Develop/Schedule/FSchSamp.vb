Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FSchSamp
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("SchSRN"), Boolean)
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("SchSRP"), Boolean)
        Me.BVBPrintHis.Enabled = CType(TcodeCollection.Item("SchSRPH"), Boolean)
        Me.BVBPrintCust.Enabled = CType(TcodeCollection.Item("SchSRPC"), Boolean)

        Me.BVBExit.Enabled = True

        Me.CEData.Properties.ReadOnly = True
        Me.GridView1.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBPrintHis.Enabled = False
        Me.BVBPrintCust.Enabled = False

        Me.CEData.Properties.ReadOnly = True
        Me.GridView1.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True
    End Sub

    Private Sub FSchSamp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()

    End Sub


    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        OpenControl()

        Dim x As Integer

        Dim cmSPDtl As New SqlCommand("SPInsDataSchPrSamp")
        cmSPDtl.CommandType = CommandType.StoredProcedure

        With cmSPDtl
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
            XtraMessageBox.Show("Schedule Sample Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmsl As SqlDataAdapter

        If Me.CEData.EditValue = False Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SchSRIDD,SR.ReqID,SR.Tanggal,SR.TglKirim,SR.CustID,C.Nama As Cust,SR.StyleID, SR.StlName,ReqIDD, SampType,Warna,Uk,Qty,TglPattern,TglSpec,TglTool,TglBhn,TglJht,TglAss,TglFinProd,SS.Ket From T_SchPrSamp SS Inner Join T_SampReq SR On SS.ReqID=SR.ReqID Inner Join M_Cust C On SS.CustID=C.CustID and SchSRIDD=(Select Top 1 SchSRIDD From T_SchPrSamp where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD order By SchSRIDD desc) Where stsKirim='False' Order By ReqID,StyleID,StlName,Warna", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SchSRIDD,SR.ReqID,SR.Tanggal,SR.TglKirim,SR.CustID,C.Nama As Cust,SR.StyleID, SR.StlName,ReqIDD, SampType,Warna,Uk,Qty,TglPattern,TglSpec,TglTool,TglBhn,TglJht,TglAss,TglFinProd,SS.Ket From T_SchPrSamp SS Inner Join T_SampReq SR On SS.ReqID=SR.ReqID Inner Join M_Cust C On SS.CustID=C.CustID and SchSRIDD=(Select Top 1 SchSRIDD From T_SchPrSamp where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD order By SchSRIDD desc) Order By ReqID,StyleID,StlName,Warna", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "T_SchPrSamp")
        Try
            DsMaster.Tables("T_SchPrSamp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrSamp")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPrSamp"
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick

        Dim XR As New XRSchSampJht
        XR.InitializeData()

        Dim XR2 As New XRSchSampAss
        XR2.InitializeData()
    End Sub

    Private Sub BVBPrintHis_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintHis.ItemClick
        Dim frm As New FSchSampHis()
        frm.MdiParent = FUtama
        frm.Show()
    End Sub

    Private Sub BVBPrintCust_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintCust.ItemClick
        Dim XR As New XRSchSampCust
        XR.InitializeData()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Me.GridView1.ActiveFilterString = "[Cek] = True"

        Dim x As Integer
        Try
            Dim i : For i = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                    If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "ReqID")) Then
                        Dim cmSPDtl As New SqlCommand("SPInsT_SchPrSamp")
                        cmSPDtl.CommandType = CommandType.StoredProcedure

                        With cmSPDtl
                            .Parameters.Add("@ReqID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ReqID")
                            .Parameters.Add("@Tanggal", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "Tanggal")
                            .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglKirim")
                            .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "CustID")
                            .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StyleID")
                            .Parameters.Add("@StlName", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "StlName")
                            .Parameters.Add("@ReqIDD", SqlDbType.Int).Value = Me.GridView1.GetRowCellValue(i, "ReqIDD")
                            .Parameters.Add("@SampType", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SampType")
                            .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Warna")
                            .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                            .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                            .Parameters.Add("@TglPatt", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglPattern")
                            .Parameters.Add("@TglSpec", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglSpec")
                            .Parameters.Add("@TglTool", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglTool")
                            .Parameters.Add("@TglBhn", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglBhn")
                            .Parameters.Add("@TglJht", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglJht")
                            .Parameters.Add("@TglAss", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglAss")
                            .Parameters.Add("@TglFinProd", SqlDbType.Date).Value = Me.GridView1.GetRowCellValue(i, "TglFinProd")
                            .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
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

                        If x <> 0 Then
                            XtraMessageBox.Show("Schedule Sample Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    End If
                End If
            Next

        Catch ex As Exception
            XtraMessageBox.Show("Schedule Sample Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        If x = 0 Then
            XtraMessageBox.Show("Schedule Sample Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            XtraMessageBox.Show("Schedule Sample Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column Is GridView1.Columns("Cek") Then

            If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
                Me.GridView1.Columns("TglPattern").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("TglTool").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("TglBhn").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("TglFinProd").OptionsColumn.AllowEdit = True
                Me.GridView1.Columns("Ket").OptionsColumn.AllowEdit = True

            Else
                Me.GridView1.Columns("TglPattern").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglTool").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglBhn").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("TglFinProd").OptionsColumn.AllowEdit = False
                Me.GridView1.Columns("Ket").OptionsColumn.AllowEdit = False
            End If

        End If
    End Sub
    Private Sub GridView1_FocusedRowChanged(sender As Object, e As Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.GetFocusedRowCellValue("Cek") = True Then
            Me.GridView1.Columns("TglPattern").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TglTool").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TglBhn").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("TglFinProd").OptionsColumn.AllowEdit = True
            Me.GridView1.Columns("Ket").OptionsColumn.AllowEdit = True

        Else
            Me.GridView1.Columns("TglPattern").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglSpec").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglTool").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglBhn").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglJht").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglAss").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("TglFinProd").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Ket").OptionsColumn.AllowEdit = False
        End If
    End Sub

    Private Sub CEData_EditValueChanged(sender As Object, e As EventArgs) Handles CEData.EditValueChanged
        Dim cmsl As SqlDataAdapter

        If Me.CEData.EditValue = False Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SchSRIDD,SR.ReqID,SR.Tanggal,SR.TglKirim,SR.CustID,C.Nama As Cust,SR.StyleID, SR.StlName,ReqIDD, SampType,Warna,Qty,TglPattern,TglSpec,TglTool,TglBhn,TglJht,TglAss,TglFinProd,SS.Ket From T_SchPrSamp SS Inner Join T_SampReq SR On SS.ReqID=SR.ReqID Inner Join M_Cust C On SS.CustID=C.CustID and SchSRIDD=(Select Top 1 SchSRIDD From T_SchPrSamp where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD order By SchSRIDD desc) Where stsKirim='False' Order By ReqID,StyleID,StlName,Warna", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SchSRIDD,SR.ReqID,SR.Tanggal,SR.TglKirim,SR.CustID,C.Nama As Cust,SR.StyleID, SR.StlName,ReqIDD, SampType,Warna,Qty,TglPattern,TglSpec,TglTool,TglBhn,TglJht,TglAss,TglFinProd,SS.Ket From T_SchPrSamp SS Inner Join T_SampReq SR On SS.ReqID=SR.ReqID Inner Join M_Cust C On SS.CustID=C.CustID and SchSRIDD=(Select Top 1 SchSRIDD From T_SchPrSamp where ReqID=SS.ReqID and ReqIDD=SS.ReqIDD order By SchSRIDD desc) Order By ReqID,StyleID,StlName,Warna", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "T_SchPrSamp")
        Try
            DsMaster.Tables("T_SchPrSamp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_SchPrSamp")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_SchPrSamp"
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub
End Class