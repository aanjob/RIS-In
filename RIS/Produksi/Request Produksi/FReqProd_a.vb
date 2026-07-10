Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FReqProd_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Manual As String
    Dim MdlID, Jns As String
    Dim CekAll As Boolean
    Dim Tgl As Date
    Public Sub New(JnsBon As String, CustID As String, Tanggal As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Tgl = Tanggal
        Jns = JnsBon
        DsAddDt = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select BOMID,MdlID,ArtName,Warna From T_BOM B Left Outer Join M_Cust C On B.CustID=C.CustID Where stsApp='True' and stsLunas='False' and B.CustID='" & CustID & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOMLUE")
        cmsl.Fill(DsMaster, "T_BOMLUE")
        DsMaster.Tables("T_BOMLUE").Clear()
        cmsl.Fill(DsMaster, "T_BOMLUE")
        DsMaster.Tables("T_BOMLUE").Rows.Add("", "", "")

        Me.SLUBOMID.Properties.DataSource = DsMaster.Tables("T_BOMLUE")
        Me.SLUBOMID.Properties.DisplayMember = "BOMID"
        Me.SLUBOMID.Properties.ValueMember = "BOMID"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub SLUBOMID_Leave(sender As Object, e As EventArgs) Handles SLUBOMID.Leave
        MdlID = DsMaster.Tables("T_BOMLUE").Select("BOMID = '" & Me.SLUBOMID.EditValue & "'")(0).Item("MdlID")

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select B.ArtCode,ArtName,Uk,0.0 as Qty,0.0 as Upp,0.0 as Hancur,0.0 as Hilang,'' As Ket From T_BOMPO P Inner Join M_Brg B On P.ArtCode=B.ArtCode Where BOMID='" & Me.SLUBOMID.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMPO")
        Try
            DsAddDt.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsAddDt, "T_BOMPO")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "T_BOMPO"

        cmsl = New SqlDataAdapter("Select Distinct MD.KompID,K.Nama As Komponen,convert(bit,'FALSE') as Cek From M_ModelDtl MD Left Outer Join T_BOM B On B.MdlID=MD.MdlID Left Outer Join M_Komp K On MD.KompID=K.KompID Where BOMID='" & Me.SLUBOMID.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_Komp")
        Try
            DsAddDt.Tables("M_Komp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsAddDt, "M_Komp")

        Me.GridControl3.DataSource = DsAddDt
        Me.GridControl3.DataMember = "M_Komp"
    End Sub

    Private Sub BKlik_Click(sender As Object, e As EventArgs) Handles BKlik.Click
        Dim x, i As Integer
        Dim KompID As String = ""
        x = 0
        i = 0

        Dim cmsl As SqlDataAdapter

        For i = 0 To DsAddDt.Tables("T_BOMPO").Rows.Count - 1
            If DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") > 0 Then

                Dim y : For y = Me.GridView4.RowCount - 1 To 0 Step -1
                    If Me.GridView4.GetRowCellValue(y, "BOMID") = Me.SLUBOMID.EditValue And Me.GridView4.GetRowCellValue(y, "ArtCode") = DsAddDt.Tables("T_BOMPO").Rows(i).Item("ArtCode") Then
                        Me.GridView4.DeleteRow(y)
                    End If
                Next

                Dim z : For z = Me.GridView2.RowCount - 1 To 0 Step -1
                    If Me.GridView2.GetRowCellValue(z, "BOMID") = Me.SLUBOMID.EditValue And Me.GridView2.GetRowCellValue(z, "ArtCode") = DsAddDt.Tables("T_BOMPO").Rows(i).Item("ArtCode") Then
                        Me.GridView2.DeleteRow(z)
                    End If
                Next

                cmsl = New SqlDataAdapter("Select '" & Me.SLUBOMID.EditValue & "' as BOMID,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("ArtCode") & "' As ArtCode,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("ArtName") & "' As ArtName,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Uk") & "' as Uk,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") & "' As Qty,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Upp") & "' As Upp,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Hancur") & "' As Hancur,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Hilang") & "' As Hilang,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Ket") & "' as Ket", koneksi)
               
                cmsl.TableMappings.Add("Table", "T_ReqPQty")
                cmsl.Fill(DsAddDt, "T_ReqPQty")

            End If
        Next

        DsAddDt.Tables("T_ReqPQty").PrimaryKey = New DataColumn() {DsAddDt.Tables("T_ReqPQty").Columns("BOMID"), DsAddDt.Tables("T_ReqPQty").Columns("ArtCode")}

        Me.GridControl4.DataSource = DsAddDt
        Me.GridControl4.DataMember = "T_ReqPQty"

        For i = 0 To DsAddDt.Tables("M_Komp").Rows.Count - 1
            If DsAddDt.Tables("M_Komp").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    KompID = "'" & DsAddDt.Tables("M_Komp").Rows(i).Item("KompID") & "'"
                Else
                    KompID &= ",'" & DsAddDt.Tables("M_Komp").Rows(i).Item("KompID") & "'"
                End If
            End If
        Next

        Try
            For i = 0 To DsAddDt.Tables("T_BOMPO").Rows.Count - 1
                If DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") > 0 Then
                    cmsl = New SqlDataAdapter("Select BOMID,ArtCode,ArtName,Uk,DivID,Divisi,KompID,Komponen,BBID,UkBB,Bahan,Sat,Std,KetMdl,Ket,Qty,Req, KaliQty,stsJasa,stsMentah,BBIDInd,HarSat,Round(x.Req*HarSat,2) as HarAkhir From (Select '" & Me.SLUBOMID.EditValue & "' as BOMID,M.ArtCode,ArtName,M.Uk,M.DivID,D.Nama as Divisi, M.KompID,K.Nama As Komponen,M.BBID,M.UkBB,B.Nama As Bahan,M.Sat,M.Std, M.Ket As KetMdl,'' as Ket,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") & "' As Qty,Case when Std=0 Then 0 Else Case When KaliQty='True' Then Round(Std* Cast('" & CDec(DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty")) & "' As Decimal(18,1)),2) Else Round(Cast('" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") & "' As Decimal(18,1))/Std,2) End End As Req,KaliQty,M.stsJasa, stsMentah,BBIDInd,(Select Isnull((Select Top 1 Round((HargaBeli*NilTukarRp)+HargaBahan,2) From M_BBHarga Where BBID=M.BBID and Tanggal<='" & Tgl & "' Order By Tanggal desc),0)) As HarSat,K.Urut From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_Brg BJ On M.ArtCode=BJ.ArtCode Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & MdlID & "' and M.KompID In (" & KompID & ") and M.ArtCode In ('" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("ArtCode") & "')) as x Order By DivID,Urut,KompID,BBID,Uk", koneksi)

                    'cmsl = New SqlDataAdapter("Select BOMID,ArtCode,ArtName,Uk,DivID,Divisi,KompID,Komponen,BBID,UkBB,Bahan,Sat,Std,KetMdl,Ket,Qty,Req, KaliQty,stsJasa,stsMentah,BBIDInd,HarSat,Round(x.Req*HarSat,2) as HarAkhir From (Select '" & Me.SLUBOMID.EditValue & "' as BOMID,M.ArtCode,ArtName,M.Uk,M.DivID,D.Nama as Divisi, M.KompID,K.Nama As Komponen,M.BBID,M.UkBB,B.Nama As Bahan,M.Sat,M.Std, M.Ket As KetMdl,'' as Ket,'" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") & "' As Qty,Case when Std=0 Then 0 Else Case When KaliQty='True' Then Round(Std* Cast('" & CDec(DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty")) & "' As Decimal(18,1)),2) Else Round(Cast('" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("Qty") & "' As Decimal(18,1))/Std,2) End End As Req,KaliQty,M.stsJasa, stsMentah,BBIDInd,Case When '" & Jns & "'='Merah' Then (Select Isnull((Select Top 1 Round(HargaBeli+HargaBahan,2) From M_BBHarga Where BBID=M.BBID and Tanggal<='" & Tgl & "' Order By Tanggal desc),0)) Else 0 End As HarSat,K.Urut From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_Brg BJ On M.ArtCode=BJ.ArtCode Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & MdlID & "' and M.KompID In (" & KompID & ") and M.ArtCode In ('" & DsAddDt.Tables("T_BOMPO").Rows(i).Item("ArtCode") & "')) as x Order By DivID,Urut,KompID,BBID,Uk", koneksi)

                    cmsl.TableMappings.Add("Table", "T_BOMDtl")
                    cmsl.Fill(DsAddDt, "T_BOMDtl")
                End If
            Next
        Catch ex As Exception

        End Try

        'DsAddDt.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsAddDt.Tables("T_BOMDtl").Columns("BOMID"), DsAddDt.Tables("T_BOMDtl").Columns("ArtCode"), DsAddDt.Tables("T_ReqPDtl").Columns("DivID"), DsAddDt.Tables("T_BOMDtl").Columns("KompID"), DsAddDt.Tables("T_BOMDtl").Columns("BBID")}

        Me.GridControl2.DataSource = DsAddDt
        Me.GridControl2.DataMember = "T_BOMDtl"
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView2.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()
        Me.GridView4.ActiveFilter.Clear()


        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0

        Dim i : For i = 0 To GridView2.RowCount - 1
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "BOMID"), "BOMID" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "ArtName"), "ArtName" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Uk"), "Uk" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Qty"), "Qty" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "DivID"), "DivID" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Divisi"), "Divisi" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "KompID"), "KompID" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Komponen"), "Komponen" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "BBID"), "BBID" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Bahan"), "Bahan" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "UkBB"), "UkBB" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Sat"), "Sat" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Std"), "Std" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Req"), "Req" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "KaliQty"), "KaliQty" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "KetMdl"), "KetMdl" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "Ket"), "Ket" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "stsJasa"), "stsJasa" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "stsMentah"), "stsMentah" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "BBIDInd"), "BBIDInd" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "HarSat"), "HarSat" & x)
            dataTrans.Add(Me.GridView2.GetRowCellValue(i, "HarAkhir"), "HarAkhir" & x)

            x += 1
        Next
        dataTrans.Add(x, "Baris")

        dataTrans2 = New Collection
        dataTrans2.Clear()

        Dim y As Integer = 0
        Dim z : For z = 0 To GridView4.RowCount - 1
            If Me.GridView4.GetRowCellValue(z, "Qty") > 0 Then
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "BOMID"), "BOMID" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "ArtCode"), "ArtCode" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "ArtName"), "ArtName" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "Uk"), "Uk" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "Qty"), "Qty" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "Upp"), "Upp" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "Hancur"), "Hancur" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "Hilang"), "Hilang" & y)
                dataTrans2.Add(Me.GridView4.GetRowCellValue(z, "Ket"), "Ket" & y)

                y += 1
            End If
        Next
        dataTrans2.Add(y, "Baris")

        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If e.Column Is GridView1.Columns("Upp") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") + Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") + Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") > Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") Then
                XtraMessageBox.Show("Tidak Boleh Melebihi Qty", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Me.GridView1.SetRowCellValue(e.RowHandle, "Upp", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang"))
            End If

        ElseIf e.Column Is GridView1.Columns("Hancur") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") + Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") + Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") > Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") Then
                XtraMessageBox.Show("Tidak Boleh Melebihi Qty", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Me.GridView1.SetRowCellValue(e.RowHandle, "Hancur", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang"))
            End If

        ElseIf e.Column Is GridView1.Columns("Hilang") Then
            If Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") + Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur") + Me.GridView1.GetRowCellValue(e.RowHandle, "Hilang") > Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") Then
                XtraMessageBox.Show("Tidak Boleh Melebihi Qty", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                Me.GridView1.SetRowCellValue(e.RowHandle, "Hilang", Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") - Me.GridView1.GetRowCellValue(e.RowHandle, "Upp") - Me.GridView1.GetRowCellValue(e.RowHandle, "Hancur"))
            End If
        End If

    End Sub
    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        If e.Column Is GridView2.Columns("Std") Then
            If Me.GridView2.GetRowCellValue(e.RowHandle, "KaliQty") = True Then
                Me.GridView2.SetRowCellValue(e.RowHandle, "Req", Math.Round(Me.GridView2.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView2.GetRowCellValue(e.RowHandle, "Std"), 2))
            Else
                Me.GridView2.SetRowCellValue(e.RowHandle, "Req", Math.Round(Me.GridView2.GetRowCellValue(e.RowHandle, "Qty") / Me.GridView2.GetRowCellValue(e.RowHandle, "Std"), 2))
            End If

            'If Jns = "Merah" Then
            Me.GridView2.SetRowCellValue(e.RowHandle, "HarAkhir", Math.Round(Me.GridView2.GetRowCellValue(e.RowHandle, "Req") * Me.GridView2.GetRowCellValue(e.RowHandle, "HarSat"), 2))
            'End If
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            Timer1.Enabled = True
        End If
    End Sub

 

    'Private Sub GridView2_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
    '    If Me.GridView2.Editable = True Then
    '        If Not IsDBNull(Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "stsMentah")) Then

    '            If Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "stsMentah") = True Then
    '                Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '            Else
    '                Me.GridControl2.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '            End If
    '        End If

    '    End If
    'End Sub

    Dim DivHapus, KompHapus, BBIDIndHapus As String
    Dim Hapus As Boolean

    Private Sub GridControl2_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl2.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            DivHapus = Me.GridView2.GetFocusedDataRow.Item("DivID")
            KompHapus = Me.GridView2.GetFocusedDataRow.Item("KompID")
            BBIDIndHapus = Me.GridView2.GetFocusedDataRow.Item("BBID")

            If Me.GridView2.GetFocusedDataRow.Item("stsJasa") = True Then
                Hapus = True
            Else
                Hapus = False
            End If
        End If
    End Sub

    Private Sub GridView2_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView2.RowDeleted
        If Hapus = True Then
            Dim i : For i = Me.GridView2.RowCount - 1 To 0 Step -1
                If Me.GridView2.GetRowCellValue(i, "BBIDInd") <> "" Then
                    If Me.GridView2.GetRowCellValue(i, "DivID") = DivHapus And Me.GridView2.GetRowCellValue(i, "KompID") = KompHapus And Me.GridView2.GetRowCellValue(i, "BBIDInd") = BBIDIndHapus Then

                        Me.GridView2.DeleteRow(i)

                    End If
                End If
            Next
        End If
    End Sub

    Private Sub GridView3_DoubleClick(sender As Object, e As EventArgs) Handles GridView3.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView3.RowCount - 1
                Me.GridView3.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView3.RowCount - 1
                Me.GridView3.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub


    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "UkBB" Or view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

    Private Sub GridView4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView4.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "UkBB" Or view.FocusedColumn.FieldName = "Ket" And e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridControl4_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl4.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView4_KeyPress(grid.FocusedView, e)
    End Sub

End Class