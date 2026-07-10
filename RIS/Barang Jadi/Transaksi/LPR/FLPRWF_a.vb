Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FLPRWF_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim SplitKdD(), SplitKdP() As String
    Dim NoAssD, NoAssP, ReturID, Gol As String
    Dim QtyP, QtyD, Sisa As Integer
    Dim SdhRtrP, SdhRtrD, QtyJ, QtyAss, TotRtr, SisaAkh As Integer

    Public Sub New(ByVal CustID As String, Golongan As String, Grup As String, RtrID As String, TipePPn As String, PersenPPn As Decimal)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        Gol = Golongan

        ReturID = RtrID
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select * from (Select H.JualID,JualIDD,Tanggal,D.ArtCode,B.ArtName,B.AssID,D.SatID,D.Isi,D.stsHarga,HarDPJ, HarSat,DiscOB,h.DiscCust,RpDiscL/Qty as RpDiscL,Case When TotPsg=0 Then 0 Else Round((RpDiscP+DiscRp)/TotPsg,2) End As DiscGlbSat,0.0 As RpDiscGlb,OngkirSat,HarAkhir, Qty,Psg-(Select Isnull(Sum(Psg),0) From T_RtrBJDtl Where JualIDD=D.JualIDD and RtrID <>'" & RtrID & "')-(Select Isnull(Sum(Psg),0) From T_LPR LH Inner Join T_LPRDtl LD On LH.LPRID=LD.LPRID Where JualIDD=D.JualIDD and LH.LPRID <>'" & RtrID & "' and stsApp='False') As SisaPsg,0 as QtyRtr from T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join M_Brg B On D.ArtCode=B.ArtCode left join M_Cust C on H.CustID=c.CustID Where c.Nama = (select Nama from M_Cust where CustID='" & CustID & "') and TipePPn='" & TipePPn & "' and PersenPPn=" & PersenPPn & ") as x Where SisaPsg >0 and Qty>0 Order By Tanggal desc", koneksi)
        ' and B.Grup='" & Grup & "'
        'cmsl = New SqlDataAdapter("Select * from (Select H.JualID, JualIDD, Tanggal, D.ArtCode, B.ArtName, B.AssID, D.SatID, D.Isi, D.stsHarga, HarDPJ, HarSat, DiscOB, DiscCust, OngkirSat, HarAkhir, Qty, Psg-(Select Isnull(Sum(Psg),0) From T_RtrBJDtl Where JualIDD=D.JualIDD and RtrID <>'" & RtrID & "') As SisaPsg, 0 as QtyRtr from T_JualBJ H Inner Join T_JualBJDtl D On H.JualID=D.JualID Inner Join M_Brg B On D.ArtCode=B.ArtCode Where CustID='" & CustID & "' and H.Gol='" & Gol & "') as x Where SisaPsg >0 Order By Tanggal desc", koneksi)

        cmsl.TableMappings.Add("Table", "T_JualDtlRt" & Gol)
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "T_JualDtlRt" & Gol)

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "T_JualDtlRt" & Gol

        DtTrans = New System.Data.DataTable
        DtTrans.Columns.Add("JualID")
        DtTrans.Columns.Add("JualIDD")
        DtTrans.Columns.Add("Tanggal")
        DtTrans.Columns.Add("ArtCode")
        DtTrans.Columns.Add("ArtName")
        DtTrans.Columns.Add("SatID")
        DtTrans.Columns.Add("Isi")
        DtTrans.Columns.Add("IsiDlmDos")
        DtTrans.Columns.Add("IsiAsDos")
        DtTrans.Columns.Add("stsHarga")
        DtTrans.Columns.Add("HarDPJ")
        DtTrans.Columns.Add("HarSat")
        DtTrans.Columns.Add("DiscOB")
        DtTrans.Columns.Add("DiscCust")
        DtTrans.Columns.Add("RpDiscL")
        DtTrans.Columns.Add("DiscGlbSat")
        DtTrans.Columns.Add("RpDiscGlb")
        DtTrans.Columns.Add("OngkirSat")
        DtTrans.Columns.Add("HarAkhir")
        DtTrans.Columns.Add("Qty")
        DtTrans.Columns.Add("SisaPsg")
        DtTrans.Columns.Add("QtyRtr")

        Me.GridControl2.DataSource = DtTrans

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub FillPsg()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Uk,Qty From M_BrgAssDtl Where AssID='" & Me.GridView1.GetFocusedDataRow.Item("AssID") & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgAssDtlRt" & Gol)
        DsAddDt = New System.Data.DataSet
        cmsl.Fill(DsAddDt, "M_BrgAssDtlRt" & Gol)


        Dim i : For i = 0 To DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows.Count - 1
            Dim comm As New SqlCommand("Select Isnull((Select Sum(Dos)+(Select Isnull((Select Sum(Dos) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetFocusedDataRow.Item("JualIDD") & " and H.LPRID<>'" & ReturID & "' and ArtCode<>'" & NoAssD + DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Uk") & "' and SatID<>'P' and stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetFocusedDataRow.Item("JualIDD") & " and RtrID<>'" & ReturID & "' and ArtCode<>'" & NoAssD + DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Uk") & "' and SatID<>'P'),0)", koneksi)

            With koneksi
                .Open()
                SdhRtrD = comm.ExecuteScalar()
                .Close()
            End With

            comm = New SqlCommand("Select Isnull(Sum(Psg),0)+(Select Isnull(Sum(Psg),0) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetFocusedDataRow.Item("JualIDD") & " and ArtCode='" & NoAssD + DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Uk") & "' And H.LPRID <>'" & ReturID & "' and stsRtr='False') From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetFocusedDataRow.Item("JualIDD") & " and ArtCode='" & NoAssD + DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Uk") & "' And RtrID <>'" & ReturID & "'", koneksi)

            With koneksi
                .Open()
                SdhRtrP = comm.ExecuteScalar()
                .Close()
            End With

            Sisa = (Me.GridView1.GetFocusedDataRow.Item("Qty") * DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty")) - (SdhRtrD * DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty")) - SdhRtrP

            DtTrans.Rows.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), Me.GridView1.GetFocusedDataRow.Item("JualIDD"), Me.GridView1.GetFocusedDataRow.Item("Tanggal"), NoAssD + DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Uk"), Me.GridView1.GetFocusedDataRow.Item("ArtName"), "P", 1, DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty"), Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("stsHarga"), Me.GridView1.GetFocusedDataRow.Item("HarDPJ") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("HarSat") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("DiscOB"), Me.GridView1.GetFocusedDataRow.Item("DiscCust"), Me.GridView1.GetFocusedDataRow.Item("RpDiscL") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("DiscGlbSat"), 0, Me.GridView1.GetFocusedDataRow.Item("OngkirSat"), Me.GridView1.GetFocusedDataRow.Item("HarAkhir") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("Qty") * DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty"), Sisa, 0)

            'DtTrans.Rows.Add(Me.GridView1.GetFocusedDataRow.Item("JualID"), Me.GridView1.GetFocusedDataRow.Item("JualIDD"), Me.GridView1.GetFocusedDataRow.Item("Tanggal"), NoAssD + DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Uk"), Me.GridView1.GetFocusedDataRow.Item("ArtName"), "P", 1, DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty"), Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("stsHarga"), Me.GridView1.GetFocusedDataRow.Item("HarDPJ") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("HarSat") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("DiscOB"), Me.GridView1.GetFocusedDataRow.Item("DiscCust"), Me.GridView1.GetFocusedDataRow.Item("OngkirSat"), Me.GridView1.GetFocusedDataRow.Item("HarAkhir") / Me.GridView1.GetFocusedDataRow.Item("Isi"), Me.GridView1.GetFocusedDataRow.Item("Qty") * DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty"), ((Me.GridView1.GetFocusedDataRow.Item("Qty") - Me.GridView1.GetFocusedDataRow.Item("QtyRtr")) * DsAddDt.Tables("M_BrgAssDtlRt" & Gol).Rows(i).Item("Qty")) - Sisa, 0)
        Next

    End Sub

    Public Function CekPsg(ByVal ArtCodeD As String, ByVal IDD As Integer)
        SplitKdD = CType(ArtCodeD, String).Split("-")
        NoAssD = ArtCodeD.ToString.Remove(ArtCodeD.ToString.Length - (SplitKdD(3).Length), SplitKdD(3).Length)

        QtyP = 0

        Dim x : For x = 0 To Me.GridView2.RowCount - 1
            SplitKdP = CType(Me.GridView2.GetRowCellValue(x, "ArtCode"), String).Split("-")
            NoAssP = Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Remove(Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Length - (SplitKdP(3).Length), SplitKdP(3).Length)

            If IDD = Me.GridView2.GetRowCellValue(x, "JualIDD") And NoAssD = NoAssP Then
                QtyP += Me.GridView2.GetRowCellValue(x, "QtyRtr")
            End If
        Next

        Return QtyP
    End Function

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView2.ActiveFilter.Clear()

        dataTrans = New Collection
        dataTrans.Clear()

        Dim x As Integer = 0
        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "QtyRtr") > 0 Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "JualID"), "JualID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "JualIDD"), "JualIDD" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "Isi" & x)
                dataTrans.Add(1, "IsiDlmDos" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Isi"), "IsiAsDos" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "stsHarga"), "stsHarga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HarDPJ"), "HarDPJ" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HarSat"), "Harga" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscOB"), "DiscOB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "RpDiscL"), "RpDiscL" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "QtyRtr"), "Qty" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "DiscGlbSat"), "DiscGlbSat" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "RpDiscGlb"), "RpDiscGlb" & x)

                x += 1
            End If
        Next

        Dim y : For y = 0 To GridView2.RowCount - 1
            If Me.GridView2.GetRowCellValue(y, "QtyRtr") > 0 Then
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "JualID"), "JualID" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "JualIDD"), "JualIDD" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "ArtCode"), "ArtCode" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "ArtName"), "ArtName" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "SatID"), "SatID" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "Isi"), "Isi" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "IsiDlmDos"), "IsiDlmDos" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "IsiAsDos"), "IsiAsDos" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "stsHarga"), "stsHarga" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "HarDPJ"), "HarDPJ" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "HarSat"), "Harga" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "DiscOB"), "DiscOB" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "RpDiscL"), "RpDiscL" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "QtyRtr"), "Qty" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "DiscGlbSat"), "DiscGlbSat" & x)
                dataTrans.Add(Me.GridView2.GetRowCellValue(y, "RpDiscGlb"), "RpDiscGlb" & x)
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
        If e.Column Is GridView1.Columns("QtyRtr") Then
            Dim SdhRtrP As Integer

            Dim comm As New SqlCommand("Select Isnull((Select Sum(Psg)+(Select Isnull((Select Sum(Psg) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " and H.LPRID<>'" & ReturID & "' and stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & " and RtrID<>'" & ReturID & "'),0)", koneksi)

            With koneksi
                .Open()
                SdhRtrP = comm.ExecuteScalar()
                .Close()
            End With

            If Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRtr") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") > (Me.GridView1.GetRowCellValue(e.RowHandle, "Qty") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi")) - SdhRtrP Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView1.SetRowCellValue(e.RowHandle, "QtyRtr", 0)
            End If

            Dim DiscL As Decimal
            Dim comm2 As New SqlCommand("Select Isnull((Select RpDiscL/Qty From T_JualBJDtl where JualID='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualID") & "' and JualIDD='" & Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") & "'),0)", koneksi)

            With koneksi
                .Open()
                DiscL = comm2.ExecuteScalar()
                .Close()
            End With

            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscL", Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRtr") * DiscL)
            Me.GridView1.SetRowCellValue(e.RowHandle, "RpDiscGlb", Math.Round(Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRtr") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") * Me.GridView1.GetRowCellValue(e.RowHandle, "DiscGlbSat"), 2))

        End If


        'If e.Column Is GridView1.Columns("QtyRtr") Then
        '    Dim SisaP As Integer

        '    SisaP = CekPsg(Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode"), Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD"))

        '    If Me.GridView1.GetRowCellValue(e.RowHandle, "QtyRtr") * Me.GridView1.GetRowCellValue(e.RowHandle, "Isi") > Me.GridView1.GetRowCellValue(e.RowHandle, "SisaPsg") - SisaP Then
        '        XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Me.GridView1.SetRowCellValue(e.RowHandle, "QtyRtr", 0)

        '    Else
        '        SplitKdD = CType(Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode"), String).Split("-")
        '        NoAssD = Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode").ToString.Remove(Me.GridView1.GetRowCellValue(e.RowHandle, "ArtCode").ToString.Length - (SplitKdD(3).Length), SplitKdD(3).Length)

        '        Dim x : For x = 0 To Me.GridView2.RowCount - 1
        '            Dim comm As New SqlCommand("Select Isnull(Sum(Psg),0) From T_RtrBJDtl Where JualIDD=" & Me.GridView1.GetFocusedDataRow.Item("JualIDD") & " and ArtCode='" & Me.GridView2.GetRowCellValue(x, "ArtCode") & "' And RtrID <>'" & ReturID & "'", koneksi)

        '            With koneksi
        '                .Open()
        '                Sisa = comm.ExecuteScalar()
        '                .Close()
        '            End With

        '            SplitKdP = CType(Me.GridView2.GetRowCellValue(x, "ArtCode"), String).Split("-")
        '            NoAssP = Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Remove(Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Length - (SplitKdP(3).Length), SplitKdP(3).Length)

        '            If Me.GridView1.GetRowCellValue(e.RowHandle, "JualIDD") = Me.GridView2.GetRowCellValue(x, "JualIDD") And NoAssD = NoAssP Then
        '                Me.GridView2.SetRowCellValue(x, "SisaPsg", ((Me.GridView1.GetFocusedDataRow.Item("Qty") - Me.GridView1.GetFocusedDataRow.Item("QtyRtr")) * Me.GridView2.GetRowCellValue(x, "IsiDlmDos")) - Sisa)
        '            End If
        '        Next
        '    End If

        'End If
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If Not IsDBNull(Me.GridView1.GetFocusedDataRow.Item("SatID")) Then
            If Me.GridView1.GetFocusedDataRow.Item("SatID") <> "P" Then
                SplitKdD = CType(Me.GridView1.GetFocusedDataRow.Item("ArtCode"), String).Split("-")
                NoAssD = Me.GridView1.GetFocusedDataRow.Item("ArtCode").ToString.Remove(Me.GridView1.GetFocusedDataRow.Item("ArtCode").ToString.Length - (SplitKdD(3).Length), SplitKdD(3).Length)

                Dim St As String = ""

                If Me.GridView2.RowCount > 0 Then
                    Dim x : For x = 0 To Me.GridView2.RowCount - 1
                        SplitKdP = CType(Me.GridView2.GetRowCellValue(x, "ArtCode"), String).Split("-")
                        NoAssP = Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Remove(Me.GridView2.GetRowCellValue(x, "ArtCode").ToString.Length - (SplitKdP(3).Length), SplitKdP(3).Length)

                        If Me.GridView1.GetFocusedDataRow.Item("JualIDD") = Me.GridView2.GetRowCellValue(x, "JualIDD") And NoAssD = NoAssP Then
                            St = "Ada"
                            Exit For
                        Else
                            St = "Tidak"
                        End If
                    Next

                    If St = "Tidak" Then
                        FillPsg()
                    End If

                Else
                    FillPsg()
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

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Escape Then
            dataTrans = New Collection
            dataTrans.Clear()

            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged

        If e.Column Is GridView2.Columns("QtyRtr") Then
            Dim SdhRtrP, SdhRtrD, QtyJ, QtyP, QtyD, QtyAss, TotRtr, SisaAkh As Integer

            Dim comm As New SqlCommand("Select Isnull((Select Sum(Dos)+(Select Isnull((Select Sum(Dos) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView2.GetRowCellValue(e.RowHandle, "JualIDD") & " and H.LPRID<>'" & ReturID & "' and ArtCode<>'" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "' and SatID<>'P' and stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView2.GetRowCellValue(e.RowHandle, "JualIDD") & " and RtrID<>'" & ReturID & "' and ArtCode<>'" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "' and SatID<>'P'),0)", koneksi)

            With koneksi
                .Open()
                SdhRtrD = comm.ExecuteScalar()
                .Close()
            End With

            comm = New SqlCommand("Select Isnull((Select Sum(Psg)+(Select Isnull((Select Sum(Psg) From T_LPR H Inner Join T_LPRDtl D On H.LPRID=D.LPRID Where JualIDD=" & Me.GridView2.GetRowCellValue(e.RowHandle, "JualIDD") & " and H.LPRID<>'" & ReturID & "' and ArtCode='" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "' and H.stsRtr='False'),0)) From T_RtrBJDtl Where JualIDD=" & Me.GridView2.GetRowCellValue(e.RowHandle, "JualIDD") & " and RtrID<>'" & ReturID & "' and ArtCode='" & Me.GridView2.GetRowCellValue(e.RowHandle, "ArtCode") & "'),0)", koneksi)

            With koneksi
                .Open()
                SdhRtrP = comm.ExecuteScalar()
                .Close()
            End With

            If Me.GridView2.GetRowCellValue(e.RowHandle, "QtyRtr") > (Me.GridView1.GetFocusedDataRow.Item("Qty") * Me.GridView2.GetRowCellValue(e.RowHandle, "IsiDlmDos")) - (SdhRtrD * Me.GridView2.GetRowCellValue(e.RowHandle, "IsiDlmDos")) - SdhRtrP Then
                XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.GridView2.SetRowCellValue(e.RowHandle, "QtyRtr", 0)
                Exit Sub
            End If

            Dim DiscL As Decimal
            Dim comm2 As New SqlCommand("Select Isnull((Select RpDiscL/(Qty*Isi) From T_JualBJDtl where JualID='" & Me.GridView2.GetRowCellValue(e.RowHandle, "JualID") & "' and JualIDD='" & Me.GridView2.GetRowCellValue(e.RowHandle, "JualIDD") & "'),0)", koneksi)

            With koneksi
                .Open()
                DiscL = comm2.ExecuteScalar()
                .Close()
            End With

            Me.GridView2.SetRowCellValue(e.RowHandle, "RpDiscL", Me.GridView2.GetRowCellValue(e.RowHandle, "QtyRtr") * DiscL)
            Me.GridView2.SetRowCellValue(e.RowHandle, "RpDiscGlb", Math.Round(Me.GridView2.GetRowCellValue(e.RowHandle, "QtyRtr") * Me.GridView2.GetRowCellValue(e.RowHandle, "DiscGlbSat"), 2))
        End If

        'If e.Column Is GridView2.Columns("QtyRtr") Then
        '    If Me.GridView2.GetRowCellValue(e.RowHandle, "QtyRtr") > Me.GridView2.GetRowCellValue(e.RowHandle, "SisaPsg") Then
        '        XtraMessageBox.Show("Qty Tidak Boleh Melebihi Pembelian", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Me.GridView2.SetRowCellValue(e.RowHandle, "QtyRtr", Me.GridView2.GetRowCellValue(e.RowHandle, "SisaPsg"))
        '    End If
        'End If
    End Sub
End Class