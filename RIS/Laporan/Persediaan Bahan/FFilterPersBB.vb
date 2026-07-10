Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFilterPersBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll, CekAll2 As Boolean
    Dim Report As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Param As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Report = Param

        If Report = "Qty" Then
            Me.LCGerak.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.ESIStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            Me.LCGerak.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.ESIStatus.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FFilterPersBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,JnsID,Nama As Jenis From M_BBJns", koneksi)
        cmsl.TableMappings.Add("Table", "M_BBJns")
        cmsl.Fill(DsLapF, "M_BBJns")
        DsLapF.Tables("M_BBJns").Clear()
        cmsl.Fill(DsLapF, "M_BBJns")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_BBJns"

        cmsl = New SqlDataAdapter("Select Case When BBID In (Select BBID From M_BBHon) Then convert(bit,'True') Else convert(bit,'FALSE') End as Cek,BBID,Nama,Sat From M_BB", koneksi)
        cmsl.TableMappings.Add("Table", "M_BB")
        cmsl.Fill(DsLapF, "M_BB")
        DsLapF.Tables("M_BB").Clear()
        cmsl.Fill(DsLapF, "M_BB")

        Me.GridControl2.DataSource = DsLapF
        Me.GridControl2.DataMember = "M_BB"

        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol In ('Bahan','Sparepart-Mesin')", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsLapF, "M_GudangLUE")
        DsLapF.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsLapF, "M_GudangLUE")

        Me.SLUGudang.Properties.DataSource = DsLapF.Tables("M_GudangLUE")
        Me.SLUGudang.Properties.DisplayMember = "Nama"
        Me.SLUGudang.Properties.ValueMember = "GdID"
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

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue
        MainModule.PilihGudang = Me.SLUGudang.Text
        MainModule.PilihGudangID = Me.SLUGudang.EditValue

        Dim x, i As Integer
        Dim JnsID As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("M_BBJns").Rows.Count - 1
            If DsLapF.Tables("M_BBJns").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    JnsID = "'" & DsLapF.Tables("M_BBJns").Rows(i).Item("JnsID") & "'"
                Else
                    JnsID &= ",'" & DsLapF.Tables("M_BBJns").Rows(i).Item("JnsID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(JnsID, "JnsID")

        If Report = "Qty" Then
            Dim XR As New XRPersBB
            XR.InitializeData(bind)
        Else
            If Me.CBOStatus.EditValue = "Semua" Then
                bind.Add("%", "stsGerak")
            Else
                bind.Add(Me.CBOStatus.Text, "stsGerak")
            End If

            Dim XR As New XRPersBBNom
            XR.InitializeData(bind)
        End If
    End Sub

    Private Sub BPreview2_Click(sender As Object, e As EventArgs) Handles BPreview2.Click
        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue

        Dim x, i As Integer
        Dim BBID As String = ""

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
        bind.Add(BBID, "BBID")

        Dim XR As New XRRekArusBB
        XR.InitializeData(bind)
    End Sub
End Class