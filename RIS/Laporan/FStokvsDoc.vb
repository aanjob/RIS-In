Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Public Class FStokvsDoc
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2 As Boolean
    Dim BBID As String = ""
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
            Dim link As BaseExportLink = GridView4.CreateExportLink(provider)
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

    Private Sub FStokvsDoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAkhir.EditValue = DateTime.Now

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Case When BBID In (Select BBID From M_BBHon) Then convert(bit,'True') Else convert(bit,'FALSE') End as Cek,BBID,B.Nama,Sat From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID where B.Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BBL")
        cmsl.Fill(DsLapF, "M_BBL")
        DsLapF.Tables("M_BBL").Clear()
        cmsl.Fill(DsLapF, "M_BBL")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_BBL"
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        koneksi.Close()

        Me.GridView1.ActiveFilterString = "[Cek] = True"

        Dim x, i As Integer

        x = 0
        i = 0
        For i = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                x += 1
                If x = 1 Then
                    BBID = "'" & Me.GridView1.GetRowCellValue(i, "BBID") & "'"
                Else
                    BBID &= ",'" & Me.GridView1.GetRowCellValue(i, "BBID") & "'"
                End If
            End If
        Next

         Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select *,Stok-OutsBOM As FreeStok From (Select BBID,Nama,Sat,(Select Isnull((Select Sum(QtyBOM) From(Select BOMID,BBID,Round(Sum(Keb)+Sum(Pol),0) As QtyBOM From T_BOMDtl D Where BBID = M_BB.BBID Group By BOMID,BBID) as x),0)) As BOM, (Select Isnull ((Select Sum(Req) From T_ReqP H Inner Join T_ReqPDtl D On H.ReqPID=D.ReqPID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Req,(Select Isnull ((Select Sum(Qty)-Sum(BtlOrder) From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As PO,(Select Isnull((Select Sum(OutsBOM) From(Select BOMID,BBID,Round(Sum(Keb)+Sum(Pol),0)- (Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and Tanggal<='" & Me.DTPAkhir.EditValue & "'),0)) +(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID and Tanggal<='" & Me.DTPAkhir.EditValue & "'),0))As OutsBOM From T_BOMDtl D Where BBID = M_BB.BBID Group By BOMID,BBID) as x),0))+(Select Isnull ((Select Sum(Req) From T_ReqP H Inner Join T_ReqPDtl D On H.ReqPID=D.ReqPID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))  As OutsBOM, (Select Isnull ((Select Sum(Qty) From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Trm, (Select Isnull ((Select Sum(Qty) From T_RtrBB H Inner Join T_RtrBBDtl D On H.RtrID=D.RtrID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Rtr,(Select Isnull ((Select Sum(Qty)-Sum(BtlOrder) From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))-(Select Isnull ((Select Sum(Qty) From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))+(Select Isnull ((Select Sum(Qty) From T_RtrBB H Inner Join T_RtrBBDtl D On H.RtrID=D.RtrID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As OutsPO, (Select Isnull ((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl D On H.BPBID=D.BPBID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As BPB, (Select Isnull ((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl D On H.RPBID=D.RPBID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As RPB, (Select Isnull ((Select Sum(QtyTj) From T_TrBB H Inner Join T_TrBBDtl D On H.TrID=D.TrID where BBIDTj=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) +(Select Isnull ((Select Sum(Qty) From T_AdjBB H Inner Join T_AdjBBDtl D On H.AdjBBID=D.AdjBBID where BBID=M_BB.BBID and Qty>0 and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As DocLM, (Select Isnull ((Select Sum(QtyAs) From T_TrBB H Inner Join T_TrBBDtl D On H.TrID=D.TrID where BBIDAs=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))+(Select Isnull ((Select Sum(Qty)*-1 From T_AdjBB H Inner Join T_AdjBBDtl D On H.AdjBBID=D.AdjBBID where BBID=M_BB.BBID and Qty<0 and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As DocLK,(Select Isnull ((Select Sum(Masuk)-Sum(Keluar) From T_StokBB where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Stok From M_BB Where BBID In (" & BBID & ")) as x", koneksi)
        cmsl.TableMappings.Add("Table", "StokDoc")
        cmsl.SelectCommand.CommandTimeout = 90000

        Try
            DsLapF.Tables("StokDoc").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "StokDoc")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "StokDoc"

        cmsl = New SqlDataAdapter("Select * From(Select B.BBID,H.BOMID,C.Nama As Cust,Tanggal,ArtName,Warna,TotPsg+TotPsgPol As Tot,Sum(Keb) As Keb From M_BB B Left Outer Join T_BOMDtl D On B.BBID=D.BBID Left Outer Join T_BOM H On H.BOMID=D.BOMID Left Outer Join M_Cust C On H.CustID=C.CustID Where B.BBID In (" & BBID & ") Group By B.BBID,H.BOMID,C.Nama,Tanggal,ArtName,Warna,TotPsg,TotPsgPol) as x Where Tanggal <='" & Me.DTPAkhir.EditValue & "' or Tanggal is null", koneksi)

        cmsl.TableMappings.Add("Table", "DocDtl")
        Try
            DsLapF.Tables("DocDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "DocDtl")

        Dim PK1 As DataColumn = DsLapF.Tables("StokDoc").Columns("BBID")
        Dim FK1 As DataColumn = DsLapF.Tables("DocDtl").Columns("BBID")

        Try
            DsLapF.Relations.Add("Detail BOM", PK1, FK1, False)
        Catch ex As Exception

        End Try

        Me.GridControl2.DataSource = DsLapF.Tables("StokDoc")
        Me.GridControl2.LevelTree.Nodes.Add("Detail BOM", GridView2)
        Me.GridView3.ViewCaption = "Detail BOM"
        Me.GridView3.OptionsBehavior.Editable = False

        cmsl = New SqlDataAdapter("Select *,Stok-BOMBlmKirimProd As FreeStok From (Select BBID,Nama,Sat,(Select Isnull((Select Sum(QtyBOM) From(Select BOMID,BBID,Round(Sum(Keb)+Sum(Pol),0) As QtyBOM From T_BOMDtl D Where BBID = M_BB.BBID Group By BOMID,BBID) as x),0)) As QtyBOM, (Select Isnull ((Select Sum(Req) From T_ReqP H Inner Join T_ReqPDtl D On H.ReqPID=D.ReqPID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Req, (Select Isnull ((Select Sum(Qty) From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As PO, (Select Isnull ((Select Sum(Qty) From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))-(Select Isnull ((Select Sum(Qty) From T_RtrBB H Inner Join T_RtrBBDtl D On H.RtrID=D.RtrID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Trm, (Select Isnull ((Select Sum(Qty) From T_POBB H Inner Join T_POBBDtl D On H.POID=D.POID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))- (Select Isnull ((Select Sum(Qty) From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))+(Select Isnull ((Select Sum(Qty) From T_RtrBB H Inner Join T_RtrBBDtl D On H.RtrID=D.RtrID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As OutsPO, (Select Isnull ((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl D On H.BPBID=D.BPBID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))+ (Select Isnull((Select Sum(Qty) From T_SJKBB H Inner Join T_SJKBBDtl D On H.SJKID=D.SJKID Where BBID=M_BB.BBID and Tanggal<='" & Me.DTPAkhir.EditValue & "'),0))- (Select Isnull ((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl D On H.RPBID=D.RPBID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As BPB, (Select Isnull((Select Sum(OutsBOM) From(Select BOMID,BBID,Round(Sum(Keb)+Sum(Pol),0)- (Select Isnull((Select Sum(Qty) From T_BPB H Inner Join T_BPBDtl BD On H.BPBID=BD.BPBID Where DocID=D.BOMID and BBID=D.BBID and Tanggal<='" & Me.DTPAkhir.EditValue & "'),0)) +(Select Isnull((Select Sum(Qty) From T_RPB H Inner Join T_RPBDtl BD On H.RPBID=BD.RPBID Where DocID=D.BOMID and BBID=D.BBID and Tanggal<='" & Me.DTPAkhir.EditValue & "'),0))As OutsBOM From T_BOMDtl D Where BBID = M_BB.BBID  Group By BOMID,BBID) as x),0))+(Select Isnull ((Select Sum(Req) From T_ReqP H Inner Join T_ReqPDtl D On H.ReqPID=D.ReqPID where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As BOMBlmKirimProd, (Select Isnull ((Select Sum(QtyTj) From T_TrBB H Inner Join T_TrBBDtl D On H.TrID=D.TrID where BBIDTj=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))+(Select Isnull ((Select Sum(Qty) From T_AdjBB H Inner Join T_AdjBBDtl D On H.AdjBBID=D.AdjBBID where BBID=M_BB.BBID and Qty>0 and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))-(Select Isnull ((Select Sum(QtyAs) From T_TrBB H Inner Join T_TrBBDtl D On H.TrID=D.TrID where BBIDAs=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0))-(Select Isnull ((Select Sum(Qty)*-1 From T_AdjBB H Inner Join T_AdjBBDtl D On H.AdjBBID=D.AdjBBID where BBID=M_BB.BBID and Qty<0 and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As DocLain2,(Select Isnull ((Select Sum(Masuk)-Sum(Keluar) From T_StokBB where BBID=M_BB.BBID and Tanggal <='" & Me.DTPAkhir.EditValue & "'),0)) As Stok From M_BB Where BBID In (" & BBID & ")) as x", koneksi)
        cmsl.TableMappings.Add("Table", "StokDoc2")
        cmsl.SelectCommand.CommandTimeout = 90000

        Try
            DsLapF.Tables("StokDoc2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "StokDoc2")

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "StokDoc2"

        Me.XtraTabControl1.SelectedTabPage = Me.XTPPreview
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Stok vs Dokumen")

        Me.GridView2.OptionsPrint.PrintDetails = True
        Me.GridView2.OptionsPrint.ExpandAllDetails = True
        Me.GridView2.ExportToXls(fileName)
    End Sub

    Private Sub BExExcel2_Click(sender As Object, e As EventArgs) Handles BExExcel2.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Stok vs Dokumen v2")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class