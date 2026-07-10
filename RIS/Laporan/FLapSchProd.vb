Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Public Class FLapSchProd
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

    Private Sub OpenFile2(ByVal fileName As String)
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

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        DsLapF = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter

        If Me.CEBOM.EditValue = False Then

            cmsl = New SqlDataAdapter("SPLSchProd", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLSchProd")
            Try
                DsLapF.Tables("SPLSchProd").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "SPLSchProd")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "SPLSchProd"

            cmsl = New SqlDataAdapter("SPLSchProd", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLSchProdDtl")
            Try
                DsLapF.Tables("SPLSchProdDtl").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "SPLSchProdDtl")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrLeat Where BOMID=SL.BOMID) As MaxSch,SchIDD,SL.BOMID,ETALeat,KetLeat, TrmLeat From T_BOM B Inner Join T_SchPrLeat SL On B.BOMID=SL.BOMID Where SL.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrLeat")
            Try
                DsLapF.Tables("T_SchPrLeat").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrLeat")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrSint Where BOMID=SS.BOMID) As MaxSch,SchIDD,SS.BOMID,ETASint,KetSint, TrmSint From T_BOM B Inner Join T_SchPrSint SS On B.BOMID=SS.BOMID Where SS.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrSint")
            Try
                DsLapF.Tables("T_SchPrSint").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrSint")


            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrAcc Where BOMID=SA.BOMID) As MaxSch,SchIDD,SA.BOMID,ETAAcc,KetAcc,TrmAcc From T_BOM B Inner Join T_SchPrAcc SA On B.BOMID=SA.BOMID Where SA.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrAcc")
            Try
                DsLapF.Tables("T_SchPrAcc").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrAcc")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrBott Where BOMID=SB.BOMID) As MaxSch,SchIDD,SB.BOMID,ETABott,KetBott, TrmBott From T_BOM B Inner Join T_SchPrBott SB On B.BOMID=SB.BOMID Where SB.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrBott")
            Try
                DsLapF.Tables("T_SchPrBott").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrBott")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrFin Where BOMID=SF.BOMID) As MaxSch,SchIDD,SF.BOMID,ETAFin,KetFin,TrmFin From T_BOM B Inner Join T_SchPrFin SF On B.BOMID=SF.BOMID Where SF.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrFin")
            Try
                DsLapF.Tables("T_SchPrFin").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrFin")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrTool Where BOMID=ST.BOMID) As MaxSch,SchIDD,ST.BOMID,ETATool,KetTool, TrmTool From T_BOM B Inner Join T_SchPrTool ST On B.BOMID=ST.BOMID Where ST.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrTool")
            Try
                DsLapF.Tables("T_SchPrTool").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrTool")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPr Where BOMID=SD.BOMID) As MaxSch,SchIDD,SD.BOMID,RealCutt,RealJht, RealAss,SD.KetProd,RealKrm From T_BOM B Inner Join T_SchPr SD On B.BOMID=SD.BOMID Where SD.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPr")
            Try
                DsLapF.Tables("T_SchPr").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPr")

        Else

            cmsl = New SqlDataAdapter("SPLSchProdAll", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLSchProd")
            Try
                DsLapF.Tables("SPLSchProd").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "SPLSchProd")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "SPLSchProd"

            cmsl = New SqlDataAdapter("SPLSchProdAll", koneksi)
            cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
            cmsl.SelectCommand.CommandTimeout = 90000

            cmsl.TableMappings.Add("Table", "SPLSchProdDtl")
            Try
                DsLapF.Tables("SPLSchProdDtl").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "SPLSchProdDtl")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrLeat Where BOMID=SL.BOMID) As MaxSch,SchIDD,SL.BOMID,ETALeat,KetLeat, TrmLeat From T_BOM B Inner Join T_SchPrLeat SL On B.BOMID=SL.BOMID Where SL.BOMID In (Select BOMID From T_BOM where stsApp='True')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrLeat")
            Try
                DsLapF.Tables("T_SchPrLeat").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrLeat")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrSint Where BOMID=SS.BOMID) As MaxSch,SchIDD,SS.BOMID,ETASint,KetSint, TrmSint From T_BOM B Inner Join T_SchPrSint SS On B.BOMID=SS.BOMID Where SS.BOMID In (Select BOMID From T_BOM where stsApp='True')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrSint")
            Try
                DsLapF.Tables("T_SchPrSint").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrSint")


            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrAcc Where BOMID=SA.BOMID) As MaxSch,SchIDD,SA.BOMID,ETAAcc,KetAcc,TrmAcc From T_BOM B Inner Join T_SchPrAcc SA On B.BOMID=SA.BOMID Where SA.BOMID In (Select BOMID From T_BOM where stsApp='True')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrAcc")
            Try
                DsLapF.Tables("T_SchPrAcc").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrAcc")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrBott Where BOMID=SB.BOMID) As MaxSch,SchIDD,SB.BOMID,ETABott,KetBott, TrmBott From T_BOM B Inner Join T_SchPrBott SB On B.BOMID=SB.BOMID Where SB.BOMID In (Select BOMID From T_BOM where stsApp='True')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrBott")
            Try
                DsLapF.Tables("T_SchPrBott").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrBott")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrFin Where BOMID=SF.BOMID) As MaxSch,SchIDD,SF.BOMID,ETAFin,KetFin,TrmFin From T_BOM B Inner Join T_SchPrFin SF On B.BOMID=SF.BOMID Where SF.BOMID In (Select BOMID From T_BOM where stsApp='True')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrFin")
            Try
                DsLapF.Tables("T_SchPrFin").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrFin")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPrTool Where BOMID=ST.BOMID) As MaxSch,SchIDD,ST.BOMID,ETATool,KetTool, TrmTool From T_BOM B Inner Join T_SchPrTool ST On B.BOMID=ST.BOMID Where ST.BOMID In (Select BOMID From T_BOM where stsApp='True')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPrTool")
            Try
                DsLapF.Tables("T_SchPrTool").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPrTool")

            cmsl = New SqlDataAdapter("Select (Select Max(SchIDD) From T_SchPr Where BOMID=SD.BOMID) As MaxSch,SchIDD,SD.BOMID,RealCutt,RealJht, RealAss,SD.KetProd,RealKrm From T_BOM B Inner Join T_SchPr SD On B.BOMID=SD.BOMID Where SD.BOMID In (Select BOMID From T_BOM where stsApp='True' and stsLunas='False')", koneksi)

            cmsl.TableMappings.Add("Table", "T_SchPr")
            Try
                DsLapF.Tables("T_SchPr").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "T_SchPr")

        End If

        Dim PK1 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK1 As DataColumn = DsLapF.Tables("T_SchPrLeat").Columns("BOMID")

        Dim PK2 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK2 As DataColumn = DsLapF.Tables("T_SchPrSint").Columns("BOMID")

        Dim PK3 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK3 As DataColumn = DsLapF.Tables("T_SchPrAcc").Columns("BOMID")

        Dim PK4 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK4 As DataColumn = DsLapF.Tables("T_SchPrBott").Columns("BOMID")

        Dim PK5 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK5 As DataColumn = DsLapF.Tables("T_SchPrFin").Columns("BOMID")

        Dim PK6 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK6 As DataColumn = DsLapF.Tables("T_SchPrTool").Columns("BOMID")

        Dim PK7 As DataColumn = DsLapF.Tables("SPLSchProdDtl").Columns("BOMID")
        Dim FK7 As DataColumn = DsLapF.Tables("T_SchPr").Columns("BOMID")

        DsLapF.Relations.Add("SchProdLeat", PK1, FK1)
        DsLapF.Relations.Add("SchProdSint", PK2, FK2)
        DsLapF.Relations.Add("SchProdAcc", PK3, FK3)
        DsLapF.Relations.Add("SchProdBott", PK4, FK4)
        DsLapF.Relations.Add("SchProdFin", PK5, FK5)
        DsLapF.Relations.Add("SchProdTool", PK6, FK6)
        DsLapF.Relations.Add("SchProd", PK7, FK7)


        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProdLeat", Me.GridView3)
        Me.GridView3.ViewCaption = "Leather"
        Me.GridView3.OptionsBehavior.Editable = False

        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProdSint", Me.GridView4)
        Me.GridView4.ViewCaption = "Synthetic"
        Me.GridView4.OptionsBehavior.Editable = False

        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProdAcc", Me.GridView5)
        Me.GridView5.ViewCaption = "Accessories"
        Me.GridView5.OptionsBehavior.Editable = False

        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProdBott", Me.GridView6)
        Me.GridView6.ViewCaption = "Bottom"
        Me.GridView6.OptionsBehavior.Editable = False

        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProdFin", Me.GridView7)
        Me.GridView7.ViewCaption = "Finishing"
        Me.GridView7.OptionsBehavior.Editable = False

        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProdTool", Me.GridView8)
        Me.GridView8.ViewCaption = "Tooling"
        Me.GridView8.OptionsBehavior.Editable = False


        Me.GridControl2.DataSource = DsLapF.Tables("SPLSchProdDtl")
        Me.GridControl2.LevelTree.Nodes.Add("SchProd", Me.GridView9)
        Me.GridView9.ViewCaption = "Production"
        Me.GridView9.OptionsBehavior.Editable = False
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Schedule Produksi")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub

    Private Sub BExExcel2_Click(sender As Object, e As EventArgs) Handles BExExcel2.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Detail Schedule Produksi")

        Me.GridView2.OptionsPrint.PrintDetails = True
        Me.GridView2.OptionsPrint.ExpandAllDetails = True
        Me.GridView2.ExportToXlsx(fileName)
        OpenFile2(fileName)
    End Sub

End Class