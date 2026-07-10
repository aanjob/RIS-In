Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBOMBtlAw_app
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim DsNotif4 As New System.Data.DataSet

    Private Sub FBOMBtlAw_app_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as stsAppBtl,Rev,BOMID,POID,ArtName,Warna,C.Nama As Cust,BtlAw1 From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where (stsBtlAw1='False' and TotPsgR1>0) or (stsBtlAw2='False' and TotPsgR2>0) or (stsBtlAw3='False' and TotPsgR3>0) and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)

        'cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as stsAppBtl,1 As Rev,BOMID,POID,ArtName,Warna,C.Nama As Cust,BtlAw1 From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where stsBtlAw1='False' and TotPsgR1>0 and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Union All Select convert(bit,'FALSE') as stsAppBtl,2 As Rev,BOMID,POID,ArtName,Warna,C.Nama As Cust,BtlAw2 From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where stsBtlAw2='False' and TotPsgR2>0 and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Union All Select convert(bit,'FALSE') as stsAppBtl,3 As Rev,BOMID,POID,ArtName,Warna,C.Nama As Cust,BtlAw3 From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where stsBtlAw3='False' and TotPsgR3>0 and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMBtlApp")
        Try
            DsNotif4.Tables("T_BOMBtlApp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif4, "T_BOMBtlApp")

        DsNotif4.Tables("T_BOMBtlApp").PrimaryKey = New DataColumn() {DsNotif4.Tables("T_BOMBtlApp").Columns("BOMID")}

        Me.GridControl1.DataSource = DsNotif4
        Me.GridControl1.DataMember = "T_BOMBtlApp"

        cmsl = New SqlDataAdapter("Select H.BOMID,ArtCode,P.BtlAw1 As BtlAw From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Where stsBtlAw1='False' and TotPsgR1>0 and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Union All Select H.BOMID,ArtCode,P.BtlAw2 As BtlAw  From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Where stsBtlAw2='False' and TotPsgR2>0 and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Union All Select H.BOMID,ArtCode,P.BtlAw3 As BtlAw  From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Where stsBtlAw3='False' and TotPsgR3>0 and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMPOBtlApp")
        Try
            DsNotif4.Tables("T_BOMPOBtlApp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif4, "T_BOMPOBtlApp")

        DsNotif4.Tables("T_BOMPOBtlApp").PrimaryKey = New DataColumn() {DsNotif4.Tables("T_BOMPOBtlApp").Columns("BOMID"), DsNotif4.Tables("T_BOMPOBtlApp").Columns("ArtCode")}

        Me.GridControl2.DataSource = DsNotif4
        Me.GridControl2.DataMember = "T_BOMPOBtlApp"

        If Me.GridView1.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"
        End If
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Me.GridView1.ActiveFilterString = "[stsAppBtl]=True"

        Dim x As Integer

        For i As Integer = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "stsAppBtl") = True Then
                koneksi.Close()

                Dim cmSP As New SqlCommand("SPAppBOMRev")
                cmSP.CommandType = CommandType.StoredProcedure

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                    .Parameters.Add("@Rev", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Rev")
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

                        If x <> 0 Then
                            XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If
        Next

        If x = 0 Then
            XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        Me.Dispose()
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "stsAppBtl", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "stsAppBtl", 1)
            Next
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If Me.GridView1.RowCount > 0 Then
                Me.GridView2.ActiveFilterString = "[BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"
            End If
        End If
    End Sub

End Class