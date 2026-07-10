Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FOutsSO
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2, CekAll3 As Boolean
    Dim DsLapF As New System.Data.DataSet

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOutsSO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,CustID,C.Nama From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsLapF, "M_CustL")
        DsLapF.Tables("M_CustL").Clear()
        cmsl.Fill(DsLapF, "M_CustL")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_CustL"
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


    Private Sub BKlik_Click(sender As Object, e As EventArgs) Handles BKlik.Click
        Dim cmsl As SqlDataAdapter

        Dim x, i As Integer
        Dim Cust As String = ""
        For i = 0 To DsLapF.Tables("M_CustL").Rows.Count - 1
            If DsLapF.Tables("M_CustL").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    Cust = "'" & DsLapF.Tables("M_CustL").Rows(i).Item("CustID") & "'"
                Else
                    Cust &= ",'" & DsLapF.Tables("M_CustL").Rows(i).Item("CustID") & "'"
                End If
            End If
        Next

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,SOID From T_SOBB Where CustID In(" & Cust & ")", koneksi)
        cmsl.TableMappings.Add("Table", "T_SOBB")
        cmsl.Fill(DsLapF, "T_SOBB")
        DsLapF.Tables("T_SOBB").Clear()
        cmsl.Fill(DsLapF, "T_SOBB")

        'DsLapF.Tables("T_SOBB").DefaultView.ToTable(True, "SOID")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "T_SOBB"
    End Sub

    Private Sub BKlik2_Click(sender As Object, e As EventArgs) Handles BKlik2.Click
        Dim cmsl As SqlDataAdapter

        Dim x, i As Integer
        Dim SO As String = ""

        For i = 0 To DsLapF.Tables("T_SOBB").Rows.Count - 1
            If DsLapF.Tables("T_SOBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    SO = "'" & DsLapF.Tables("T_SOBB").Rows(i).Item("SOID") & "'"
                Else
                    SO &= ",'" & DsLapF.Tables("T_SOBB").Rows(i).Item("SOID") & "'"
                End If
            End If
        Next

        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,B.BBID,Nama As Bahan From M_BB B Inner Join T_SOBBDtl SD On B.BBID=SD.BBID Where SOID In (" & SO & ")", koneksi)
        cmsl.TableMappings.Add("Table", "M_BB")
        cmsl.Fill(DsLapF, "M_BB")
        DsLapF.Tables("M_BB").Clear()
        cmsl.Fill(DsLapF, "M_BB")

        Me.GridControl3.DataSource = DsLapF
        Me.GridControl3.DataMember = "M_BB"
    End Sub


    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue

        Dim x, i As Integer
        Dim Cust As String = ""
        Dim BBID As String = ""
        Dim SO As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("T_SOBB").Rows.Count - 1
            If DsLapF.Tables("T_SOBB").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    SO = "'" & DsLapF.Tables("T_SOBB").Rows(i).Item("SOID") & "'"
                Else
                    SO &= ",'" & DsLapF.Tables("T_SOBB").Rows(i).Item("SOID") & "'"
                End If
            End If
        Next

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
        bind.Add(SO, "SO")
        bind.Add(BBID, "BBID")

        Dim XR As New XROutsSO
        XR.InitializeData(bind)

        'If Lap = "Qty" Then
        '    Dim XR As New XROutsSO
        '    XR.InitializeData(bind)
        'ElseIf Lap = "Harga" Then
        '    Dim XR As New XROutsSOHarga
        '    XR.InitializeData(bind)
        'End If

    End Sub

End Class