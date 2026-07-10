Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FBOM_app
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim DsNotif4 As New System.Data.DataSet

    Private Sub FBOM_app_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as stsAppMkt,BOMID,POID,ArtName,Warna,C.Nama As Cust,KetLain2,stsBatal,stsBtlProd,stsLunasMan From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where (stsbtlProd='True' or stsBatal='True' or stsLunasMan='True') and stsAppMkt='False' and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMApp")
        Try
            DsNotif4.Tables("T_BOMApp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif4, "T_BOMApp")

        DsNotif4.Tables("T_BOMApp").PrimaryKey = New DataColumn() {DsNotif4.Tables("T_BOMApp").Columns("BOMID")}

        Me.GridControl1.DataSource = DsNotif4
        Me.GridControl1.DataMember = "T_BOMApp"

        cmsl = New SqlDataAdapter("Select H.BOMID,ArtCode,P.BtlOrder,P.Upp,P.Hancur,P.Hilang From T_BOM H Inner Join T_BOMPO P On H.BOMID=P.BOMID Where (stsBtlProd='True' or stsBatal='True' or stsLunasMan='True') and stsAppMkt='False' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMPOApp")
        Try
            DsNotif4.Tables("T_BOMPOApp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsNotif4, "T_BOMPOApp")

        DsNotif4.Tables("T_BOMPOApp").PrimaryKey = New DataColumn() {DsNotif4.Tables("T_BOMPOApp").Columns("BOMID"), DsNotif4.Tables("T_BOMPOApp").Columns("ArtCode")}

        Me.GridControl2.DataSource = DsNotif4
        Me.GridControl2.DataMember = "T_BOMPOApp"

        If Me.GridView1.RowCount > 0 Then
            Me.GridView2.ActiveFilterString = "[BOMID]='" & Me.GridView1.GetFocusedRowCellValue("BOMID") & "'"
        End If
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        Me.GridView1.ActiveFilterString = "[stsAppMkt]=True"

        Dim x As Integer

        For i As Integer = 0 To Me.GridView1.RowCount - 1
            If SlCek("T_BOM", "stsAppMkt", "BOMID", Me.GridView1.GetRowCellValue(i, "BOMID")) = True Then
                XtraMessageBox.Show("ID BOM : " & Me.GridView1.GetRowCellValue(i, "BOMID") & " Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Me.GridView1.GetRowCellValue(i, "stsAppMkt") = True Then
                koneksi.Close()

                Dim cmSP As New SqlCommand("SPAppBOMMkt")
                cmSP.CommandType = CommandType.StoredProcedure

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMID")
                    .Parameters.Add("@stsBatal", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsBatal")
                    .Parameters.Add("@stsBtlProd", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsBtlProd")
                    .Parameters.Add("@stsLnsMan", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsLunasMan")
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
                Me.GridView1.SetRowCellValue(i, "stsAppMkt", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "stsAppMkt", 1)
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