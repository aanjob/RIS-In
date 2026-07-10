Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FOutsPOBJJO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim DsLapF As New System.Data.DataSet

#Region "Export Excel"

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Apakah Anda Mau Membuka File Ini?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ExportTo(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView1.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String, ByVal Nama As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Nama
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

#End Region

    Private Sub FOutsPOBJJO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Outstanding PO BJ Per Tanggal " & Format(System.DateTime.Now, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl, cmsl2 As SqlDataAdapter

        If Me.CEAllPO.EditValue = False Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,H.POID,H.Tanggal,H.POCust,c.Nama as CustID,D.ArtCode,B.ArtName, W.Nama as Warna,B.Ass,D.Psg,D.BtlOrder+D.BtlProd As BtlOrder,(Select Isnull((Select Top 1 B.BOMID From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=D.POID and BP.ArtCodeInd =D.ArtCode),'')) As SPK, (Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0)) as Terima, D.Psg+D.PsgPol-D.BtlOrder-D.BtlProd-D.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0)) As Sisa From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On W.WrnID=B.WrnID left join M_Cust c on h.Custid=c.CustID Where D.Psg-D.BtlOrder-D.BtlProd-D.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0))>0 and H.POID NOT IN (SELECT POID FROM T_BOM WHERE stsLunas='1')", koneksi)

            cmsl2 = New SqlDataAdapter("Select J.POID,H.JualID,D.ArtCode,B.ArtName,D.SatID,B.Ass AS Uk,D.Qty As QtyJual From T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join T_POBJJO J On H.SJID=J.POID Inner Join T_POBJJODtl JD On H.SJID=JD.POID and JD.ArtCode=D.ArtCode Inner Join M_Brg B On D.ArtCode=B.ArtCode Where J.stsKirim='False'", koneksi)

            'cmsl2 = New SqlDataAdapter("Select POID,JualID,ArtCode,ArtName,SatID,Case When Rn=1 Then QtyPO Else 0 End As QtyPO, QtyJual,Sisa From(Select (DENSE_RANK() OVER (PARTITION BY POID,ArtCode ORDER BY POID,JualID asc)) As Rn,*,QtyPO-QtyJual As Sisa From (Select J.POID,H.JualID,D.ArtCode,B.ArtName,D.SatID,D.Qty As QtyJual From T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join T_POBJJO J On H.SJID=J.POID Inner Join T_POBJJODtl JD On H.SJID=JD.POID and JD.ArtCode=D.ArtCode Inner Join M_Brg B On D.ArtCode=B.ArtCode Where J.stsKirim='False') as x) as y Order By POID", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,H.POID,H.Tanggal,H.POCust,c.Nama as CustID,D.ArtCode,B.ArtName, W.Nama as Warna,B.Ass,D.Psg,D.BtlOrder+D.BtlProd As BtlOrder,(Select Isnull((Select Top 1 B.BOMID From T_BOM B Inner Join T_BOMPO BP On B.BOMID=BP.BOMID Where B.POID=D.POID and BP.ArtCodeInd =D.ArtCode),'')) As SPK, (Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0)) as Terima, D.Psg+D.PsgPol-D.BtlOrder-D.BtlProd-D.LunasMan-(Select Isnull((Select Sum(Psg) From T_TrmBJDtl Where POID=D.POID and ArtCode=D.ArtCode),0)) As Sisa From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On W.WrnID=B.WrnID left join M_Cust c on h.Custid=c.CustID", koneksi)

            cmsl2 = New SqlDataAdapter("Select J.POID,H.JualID,D.ArtCode,B.ArtName,D.SatID,B.Ass AS Uk,D.Qty As QtyJual From T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join T_POBJJO J On H.SJID=J.POID Inner Join T_POBJJODtl JD On H.SJID=JD.POID and JD.ArtCode=D.ArtCode Inner Join M_Brg B On D.ArtCode=B.ArtCode", koneksi)
        End If

        cmsl.TableMappings.Add("Table", "OutsPO")
        Try
            DsLapF.Tables("OutsPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "OutsPO")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "OutsPO"

        cmsl2.TableMappings.Add("Table", "HisJual")
        Try
            DsLapF.Tables("HisJual").Clear()
        Catch ex As Exception

        End Try
        cmsl2.Fill(DsLapF, "HisJual")

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "HisJual"
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Dim x, i As Integer
        Dim POID, ArtCode As String
        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("OutsPO").Rows.Count - 1
            If DsLapF.Tables("OutsPO").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    POID = "'" & DsLapF.Tables("OutsPO").Rows(i).Item("POID") & "'"
                    ArtCode = "'" & DsLapF.Tables("OutsPO").Rows(i).Item("ArtCode") & "'"

                Else
                    POID &= ",'" & DsLapF.Tables("OutsPO").Rows(i).Item("POID") & "'"
                    ArtCode &= ",'" & DsLapF.Tables("OutsPO").Rows(i).Item("ArtCode") & "'"

                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(POID, "POID")
        bind.Add(ArtCode, "ArtCode")

        Dim XR As New XROutsPOBJJO
        XR.InitializeData(bind)
    End Sub

    Private Sub BHisJual_Click(sender As Object, e As EventArgs)

    End Sub
End Class