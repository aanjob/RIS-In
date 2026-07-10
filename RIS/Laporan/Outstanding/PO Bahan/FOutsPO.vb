Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FOutsPO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2, CekAll3, CekAll4 As Boolean
    Dim DsLapF As New System.Data.DataSet
    Dim Lap As String

    Public Sub New(Laporan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Lap = Laporan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOutsPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SuppID,Nama As Supp From M_Supp", koneksi)
        cmsl.TableMappings.Add("Table", "M_SuppL2")
        cmsl.Fill(DsLapF, "M_SuppL2")
        DsLapF.Tables("M_SuppL2").Clear()
        cmsl.Fill(DsLapF, "M_SuppL2")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_SuppL2"

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,BOMID,Tanggal,ArtName,Warna,TotPsg+TotPsgPol As Tot From T_BOM where stsLunas='False'", koneksi)
        cmsl.TableMappings.Add("Table", "T_BOML")
        cmsl.Fill(DsLapF, "T_BOML")
        DsLapF.Tables("T_BOML").Clear()
        cmsl.Fill(DsLapF, "T_BOML")

        Me.GridControl4.DataSource = DsLapF
        Me.GridControl4.DataMember = "T_BOML"
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
                'End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAll2 Then
            CekAll2 = False
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll2 = True
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                Me.GridView2.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView3.DoubleClick
        If CekAll3 Then
            CekAll3 = False
            For i As Integer = 0 To Me.GridView3.RowCount - 1
                Me.GridView3.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll3 = True
            For i As Integer = 0 To Me.GridView3.RowCount - 1
                Me.GridView3.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub GridView4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView4.DoubleClick
        If CekAll4 Then
            CekAll4 = False
            For i As Integer = 0 To Me.GridView4.RowCount - 1
                Me.GridView4.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll4 = True
            For i As Integer = 0 To Me.GridView4.RowCount - 1
                Me.GridView4.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub BKlik_Click(sender As Object, e As EventArgs) Handles BKlik.Click
        Dim cmsl As SqlDataAdapter
        
        'Dim i : For i = 0 To DsLapF.Tables("M_SuppL2").Rows.Count - 1
        '    If DsLapF.Tables("M_SuppL2").Rows(i).Item("Cek") = True Then
        '        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,POID From T_POBB Where SuppID ='" & DsLapF.Tables("M_SuppL2").Rows(i).Item("SuppID") & "'", koneksi)
        '        cmsl.TableMappings.Add("Table", "T_POBB")
        '        cmsl.Fill(DsLapF, "T_POBB")
        '    End If
        'Next

        Dim x, i As Integer
        Dim Supp As String = ""
        For i = 0 To DsLapF.Tables("M_SuppL2").Rows.Count - 1
            If DsLapF.Tables("M_SuppL2").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    Supp = "'" & DsLapF.Tables("M_SuppL2").Rows(i).Item("SuppID") & "'"
                Else
                    Supp &= ",'" & DsLapF.Tables("M_SuppL2").Rows(i).Item("SuppID") & "'"
                End If
            End If
        Next

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,POID From T_POBB Where SuppID In(" & Supp & ")", koneksi)
        cmsl.TableMappings.Add("Table", "T_POBB")
        cmsl.Fill(DsLapF, "T_POBB")
        DsLapF.Tables("T_POBB").Clear()
        cmsl.Fill(DsLapF, "T_POBB")

        'DsLapF.Tables("T_POBB").DefaultView.ToTable(True, "POID")

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "T_POBB"
    End Sub

    Private Sub BKlik2_Click(sender As Object, e As EventArgs) Handles BKlik2.Click
        Dim cmsl As SqlDataAdapter
        'cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,B.BBID,Nama As Bahan From M_BB B Inner Join M_SuppBB HB On B.BBID=HB.BBID Where SuppID='--' and HB.Aktif='True'", koneksi)
        'cmsl.TableMappings.Add("Table", "M_BB")
        'cmsl.Fill(DsLapF, "M_BB")
        'DsLapF.Tables("M_BB").Clear()

        'Dim i : For i = 0 To DsLapF.Tables("T_POBB").Rows.Count - 1
        '    If DsLapF.Tables("T_POBB").Rows(i).Item("Cek") = True Then
        '        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,B.BBID,Nama As Bahan From M_BB B Inner Join T_POBBDtl PD On B.BBID=PD.BBID Where POID='" & DsLapF.Tables("T_POBB").Rows(i).Item("POID") & "'", koneksi)
        '        cmsl.TableMappings.Add("Table", "M_BB")
        '        cmsl.Fill(DsLapF, "M_BB")
        '    End If
        'Next

        Dim x, i As Integer
        Dim PO As String = ""

        For i = 0 To DsLapF.Tables("T_POBB").Rows.Count - 1
            If DsLapF.Tables("T_POBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    PO = "'" & DsLapF.Tables("T_POBB").Rows(i).Item("POID") & "'"
                Else
                    PO &= ",'" & DsLapF.Tables("T_POBB").Rows(i).Item("POID") & "'"
                End If
            End If
        Next

        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,B.BBID,Nama As Bahan From M_BB B Inner Join T_POBBDtl PD On B.BBID=PD.BBID Where POID In (" & PO & ")", koneksi)
        cmsl.TableMappings.Add("Table", "M_BB")
        cmsl.Fill(DsLapF, "M_BB")
        DsLapF.Tables("M_BB").Clear()
        cmsl.Fill(DsLapF, "M_BB")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "M_BB"
    End Sub


    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue

        Dim x, i As Integer
        Dim Supp As String = ""
        Dim BBID As String = ""
        Dim PO As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("T_POBB").Rows.Count - 1
            If DsLapF.Tables("T_POBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    PO = "'" & DsLapF.Tables("T_POBB").Rows(i).Item("POID") & "'"
                Else
                    PO &= ",'" & DsLapF.Tables("T_POBB").Rows(i).Item("POID") & "'"
                End If
            End If
        Next

        'For i = 0 To DsLapF.Tables("M_SuppL2").Rows.Count - 1
        '    If DsLapF.Tables("M_SuppL2").Rows(i).Item("Cek") = True Then
        '        x += 1
        '        If x = 1 Then
        '            Supp = "'" & DsLapF.Tables("M_SuppL2").Rows(i).Item("SuppID") & "'"
        '        Else
        '            Supp &= ",'" & DsLapF.Tables("M_SuppL2").Rows(i).Item("SuppID") & "'"
        '        End If
        '    End If
        'Next


        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("M_BB").Rows.Count - 1
            If DsLapF.Tables("M_BB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    BBID = "'" & DsLapF.Tables("M_BB").Rows(i).Item("BBID") & "'"
                Else
                    BBID &= ",'" & DsLapF.Tables("M_BB").Rows(i).Item("BBID") & "'"
                End If
            End If
        Next


        Dim bind As New Collection
        'bind.Add(Supp, "Supp")
        bind.Add(PO, "PO")
        bind.Add(BBID, "BBID")

        If Lap = "Qty" Then
            Dim XR As New XROutsPO
            XR.InitializeData(bind)
        ElseIf Lap = "Harga" Then
            Dim XR As New XROutsPOHarga
            XR.InitializeData(bind)
        ElseIf Lap = "Import" Then
            Dim XR As New XROutsPOImp
            XR.InitializeData(bind)
        End If

    End Sub

    Private Sub BPreviewBOM_Click(sender As Object, e As EventArgs) Handles BPreviewBOM.Click
        Dim x, i As Integer
        Dim BOM As String
        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("T_BOML").Rows.Count - 1
            If DsLapF.Tables("T_BOML").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    BOM = "'" & DsLapF.Tables("T_BOML").Rows(i).Item("BOMID") & "'"
                Else
                    BOM &= ",'" & DsLapF.Tables("T_BOML").Rows(i).Item("BOMID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(BOM, "BOMID")

        Dim XR As New XRRekBOMBtl
        XR.InitializeData(bind)

    End Sub
End Class