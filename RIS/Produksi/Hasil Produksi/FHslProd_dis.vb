Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FHslProd_dis
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim SW As Stopwatch
    Dim Jam As Integer

    Public Sub FillDt()
        Me.DTPTanggal.EditValue = Date.Now

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Jam,JamAw,JamAkh,Proses,Line,Sum(Qty) As Hasil,Sum(Target) As Target From(Select J.Jam,JamAw,JamAkh, J.Proses,Line,B.MerkID+B.KatID+B.JnsID+'-'+Urut As Art,Sum(Qty)-Sum(Batal) As Qty,(Select Isnull((Select Target From T_TargetProdDtl where Proses=J.Proses and Art=B.MerkID+B.KatID+B.JnsID+'-'+Urut),0)) As Target From M_Jam J Inner Join T_HslProdDtl D On J.Proses=D.Proses and J.Jam=D.Jam Inner Join M_Brg B On D.ArtCode=B.ArtCode Where Tanggal='" & Me.DTPTanggal.EditValue & "' and D.Proses Like '" & Me.SLUProses.EditValue & "' and Line Like '" & Me.SLULine.EditValue & "' Group By J.Jam,JamAw,JamAkh,J.Proses,Line,B.MerkID,B.KatID, B.JnsID,Urut Union All Select Distinct J.Jam,J.JamAw,J.JamAkh,J.Proses,Line,B.MerkID+B.KatID+B.JnsID+'-'+Urut As Art,0 As Qty,0 As Target From M_Jam J left outer Join T_HslProdDtl HD On J.Proses=HD.Proses and J.Jam=HD.Jam left outer Join M_Brg B On HD.ArtCode=B.ArtCode Where J.Proses Like '" & Me.SLUProses.EditValue & "' and J.Jam <= (Select Distinct Jam From M_Jam Where Cast(GetDate() as Time(7))>=JamAw and Cast(GetDate() as Time(7))<=JamAkh)) as x Group By Jam,JamAw,JamAkh,Proses,Line,Art Order By Jam,Proses,Line", koneksi)

        cmsl.TableMappings.Add("Table", "T_HslPodDtlDis")
        Try
            DsMaster.Tables("T_HslPodDtlDis").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HslPodDtlDis")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HslPodDtlDis"

        Me.GridView1.FocusedRowHandle = Me.GridView1.RowCount - 1
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim Text As String = ""
        Dim Tick As TimeSpan = SW.Elapsed

        If Tick.Minutes Mod 1 = 0 Then
            FillDt()
        End If
    End Sub


    Private Sub FHslProd_dis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Timer1.Start()

        SW = Stopwatch.StartNew
        SW.Start()

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UP.Proses From M_Proses P Inner Join M_UsProses UP On P.Proses=Up.Proses Where Aktif='True' and UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_ProsesL")
        Try
            DsMaster.Tables("M_ProsesL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ProsesL")
        DsMaster.Tables("M_ProsesL").Rows.Add("")

        Me.SLUProses.Properties.DataSource = DsMaster.Tables("M_ProsesL")
        Me.SLUProses.Properties.DisplayMember = "Proses"
        Me.SLUProses.Properties.ValueMember = "Proses"

        cmsl = New SqlDataAdapter("Select Distinct Line From M_User where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_LineL")
        Try
            DsMaster.Tables("M_LineL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_LineL")
        DsMaster.Tables("M_LineL").Rows.Add("")

        Me.SLULine.Properties.DataSource = DsMaster.Tables("M_LineL")
        Me.SLULine.Properties.DisplayMember = "Line"
        Me.SLULine.Properties.ValueMember = "Line"

        If MainModule.SU = True Then
            Me.SLUProses.Properties.ReadOnly = False
            Me.SLULine.Properties.ReadOnly = False
            Me.DTPTanggal.Properties.ReadOnly = False
        Else
            Me.SLUProses.Properties.ReadOnly = True
            Me.SLULine.Properties.ReadOnly = True
            Me.DTPTanggal.Properties.ReadOnly = True
        End If

        Me.SLUProses.EditValue = MainModule.ProsesAktif
        Me.SLULine.EditValue = MainModule.LineAktif

        FillDt()
    End Sub

    Private Sub SLUDiv_EditValueChanged(sender As Object, e As EventArgs) Handles SLUProses.EditValueChanged
        FillDt()
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If e.RowHandle = Me.GridView1.FocusedRowHandle Then
                    If GridView1.GetRowCellValue(e.RowHandle, "Hasil") < GridView1.GetRowCellValue(e.RowHandle, "Target") Then
                        e.Appearance.ForeColor = Color.Red
                        e.Appearance.BackColor = Color.Yellow
                    Else
                        e.Appearance.ForeColor = Color.White
                        e.Appearance.BackColor = Color.Yellow
                    End If
                Else
                    If GridView1.GetRowCellValue(e.RowHandle, "Hasil") < GridView1.GetRowCellValue(e.RowHandle, "Target") Then
                        e.Appearance.ForeColor = Color.White
                        e.Appearance.BackColor = Color.Red
                    Else
                        e.Appearance.ForeColor = Color.White
                        e.Appearance.BackColor = Color.Green
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class