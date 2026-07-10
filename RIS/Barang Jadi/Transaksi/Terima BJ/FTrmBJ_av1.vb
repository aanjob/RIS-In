Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTrmBJ_av1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim Jenis As String

    Public Sub New(ByVal JnsDoc As String, Gol As String, Grup As String, TrmID As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()

        Jenis = JnsDoc

        Dim cmsl As SqlDataAdapter

        If JnsDoc = "BSTB" Then
            cmsl = New SqlDataAdapter("Select BSTBIDD as DocIDD,H.BSTBID,POID,D.ArtCode,B.ArtName,D.SatID,D.Isi,D.Qty-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where DocIDD=D.BSTBIDD and TrmID<>'" & TrmID & "'),0)) as QtyKirim,0 As Qty From T_BSTB H Inner Join T_BSTBDtl D On H.BSTBID=D.BSTBID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join T_BOM BM On D.BOMID=BM.BOMID Where D.Qty-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where DocIDD=D.BSTBIDD and TrmID<>'" & TrmID & "'),0))>0 and H.Grup='" & Grup & "'", koneksi)
            'cmsl = New SqlDataAdapter("Select BSTBIDD as DocIDD,H.BSTBID,POID,D.ArtCode,B.ArtName,D.SatID,D.Isi,D.Qty-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where DocIDD=D.BSTBIDD and TrmID<>'" & TrmID & "'),0)) as QtyKirim,0 As Qty From T_BSTB H Inner Join T_BSTBDtl D On H.BSTBID=D.BSTBID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join T_BOM BM On D.BOMID=BM.BOMID Where D.Qty-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where DocIDD=D.BSTBIDD and TrmID<>'" & TrmID & "'),0))>0 and H.Gol='" & Gol & "'", koneksi)

            cmsl.TableMappings.Add("Table", "TrmBJTemp" & Gol)
            DsAddDt = New System.Data.DataSet
            Try
                DsAddDt.Tables("TrmBJTemp" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsAddDt, "TrmBJTemp" & Gol)

        ElseIf JnsDoc = "PO" Then
            cmsl = New SqlDataAdapter("Select 0 as DocIDD ,'--' As BSTBID,H.POID,D.ArtCode,ArtName,D.SatID,D.Isi,(Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & TrmID & "'),0)) As QtyKirim,0 As Qty From T_POBJLk H Inner Join T_POBJLkDtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Where (Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & TrmID & "'),0))>0 and stsBatal='False' and B.KatID In ('L','D') and H.Gol='" & Gol & "' and H.Grup Like '" & Grup & "' Union All Select 0 as DocIDD ,'--' As BSTBID,H.POID,D.ArtCode,ArtName,D.SatID,D.Isi,(Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & TrmID & "'),0)) As QtyKirim,0 As Qty From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode Where (Qty+QtyPol)-(Select Isnull((Select Sum(Qty) From T_TrmBJDtl Where POID=H.POID and ArtCode=D.ArtCode and TrmID<>'" & TrmID & "'),0))>0 and stsBatal='False' and H.Grup Like '" & Grup & "'", koneksi)

            cmsl.TableMappings.Add("Table", "TrmBJTemp" & Gol)
            DsAddDt = New System.Data.DataSet
            Try
                DsAddDt.Tables("TrmBJTemp" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsAddDt, "TrmBJTemp" & Gol)

        ElseIf JnsDoc = "Lain-Lain" Then
            cmsl = New SqlDataAdapter("Select 0 as DocIDD, '--' As BSTBID,'--' As POID ,ArtCode,ArtName,SatID,Isi,0 As QtyKirim,0 as Qty From M_Brg Where Aktif='True' and Gol In ('" & Gol & "','Promosi') and Grup='" & Grup & "'", koneksi)

            cmsl.TableMappings.Add("Table", "TrmBJTemp" & Gol)
            DsAddDt = New System.Data.DataSet
            Try
                DsAddDt.Tables("TrmBJTemp" & Gol).Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsAddDt, "TrmBJTemp" & Gol)

        End If

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "TrmBJTemp" & Gol

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Qty") > 0 Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DocIDD"), "DocIDD" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BSTBID"), "BSTBID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "POID"), "POID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty") * Me.GridView1.GetRowCellValue(i, "Isi"), "Psg" & x)
                If Me.GridView1.GetRowCellValue(i, "SatID") = "P" Then
                    dataTrans.Add(0, "Dos" & x)
                Else
                    dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Qty"), "Dos" & x)

                End If
                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

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
        If Jenis <> "Lain-Lain" Then
            If e.Column Is GridView1.Columns("Qty") Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") > Me.GridView1.GetRowCellValue(e.RowHandle, "QtyKirim") Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Qty Kirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyKirim"))
                End If
            End If

        End If

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            Timer1.Enabled = True
        End If
    End Sub

End Class