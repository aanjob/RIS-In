Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKrtStokBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Lap As String
    Dim DsLapF As New System.Data.DataSet
    Dim InisialBC As String = ""

    Public Sub New(ByVal Laporan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Lap = Laporan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub CekInsBC()

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & Me.SLUGd.EditValue & "'", koneksi)

        With koneksi
            .Open()
            InisialBC = command.ExecuteScalar()
            .Close()
        End With
    End Sub

    Private Sub FKrtStokBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CBOPilihUk.EditValue = "1 Halaman"

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol In ('Bahan','Sparepart-Mesin')", koneksi)
        cmsl.TableMappings.Add("Table", "M_GudangLUE")
        cmsl.Fill(DsLapF, "M_GudangLUE")
        DsLapF.Tables("M_GudangLUE").Clear()
        cmsl.Fill(DsLapF, "M_GudangLUE")

        Me.SLUGd.Properties.DataSource = DsLapF.Tables("M_GudangLUE")
        Me.SLUGd.Properties.DisplayMember = "Nama"
        Me.SLUGd.Properties.ValueMember = "GdID"

        Me.CEBtNum.EditValue = False

        If Me.CEBtNum.EditValue = True Then
            Me.XTPNoBtNum.PageVisible = False
            Me.XTPWBtNum.PageVisible = True

        Else
            Me.XTPNoBtNum.PageVisible = True
            Me.XTPWBtNum.PageVisible = False
        End If
    End Sub

    Private Sub SLUGudang_Leave(sender As Object, e As EventArgs) Handles SLUGd.Leave
        Dim cmsl As SqlDataAdapter

        If Me.CEBtNum.EditValue = True Then
            cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,'" & InisialBC & "'+B.BBID as BBIDBC,B.BBID,BtNum,Nama As Bahan,Sat From M_BB B Inner Join T_StokBB S On B.BBID=S.BBID", koneksi)
            cmsl.TableMappings.Add("Table", "M_BBBtBum")
            cmsl.SelectCommand.CommandTimeout = 90000
            cmsl.Fill(DsLapF, "M_BBBtBum")
            DsLapF.Tables("M_BBBtBum").Clear()
            cmsl.Fill(DsLapF, "M_BBBtBum")

            Me.GridControl2.DataSource = DsLapF
            Me.GridControl2.DataMember = "M_BBBtBum"
        Else
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,'" & InisialBC & "'+BBID as BBIDBC,BBID,Nama As Bahan,Sat From M_BB", koneksi)
            cmsl.TableMappings.Add("Table", "M_BB")
            cmsl.SelectCommand.CommandTimeout = 90000
            cmsl.Fill(DsLapF, "M_BB")
            DsLapF.Tables("M_BB").Clear()
            cmsl.Fill(DsLapF, "M_BB")

            Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "M_BB"
        End If

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
                'If GridView1.IsRowVisible(i) Then
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
                'End If
            Next
        End If
    End Sub

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                Me.GridView2.SetRowCellValue(i, "Cek", 0)
                'End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView2.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                Me.GridView2.SetRowCellValue(i, "Cek", 1)
                'End If
            Next
        End If
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        Me.GridView1.ActiveFilter.Clear()

        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue

        Dim x, i As Integer
        Dim BBID As String = ""
        Dim BtNum As String = ""


        x = 0
        i = 0

        If Me.CEBtNum.EditValue = True Then
            For i = 0 To DsLapF.Tables("M_BBBtBum").Rows.Count - 1
                If DsLapF.Tables("M_BBBtBum").Rows(i).Item("Cek") = True Then
                    x += 1
                    If x = 1 Then
                        BBID = "'" & DsLapF.Tables("M_BBBtBum").Rows(i).Item("BBID") & "'"
                        BtNum = "'" & DsLapF.Tables("M_BBBtBum").Rows(i).Item("BtNum") & "'"

                    Else
                        BBID &= ",'" & DsLapF.Tables("M_BBBtBum").Rows(i).Item("BBID") & "'"
                        BtNum &= ",'" & DsLapF.Tables("M_BBBtBum").Rows(i).Item("BtNum") & "'"

                    End If
                End If
            Next
        Else
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
        End If

        Dim bind As New Collection
        bind.Add(BBID, "BBID")
        bind.Add(Me.SLUGd.EditValue, "Gd")
        bind.Add(Me.SLUGd.Text, "Gudang")
        bind.Add(Me.CBOPilihUk.EditValue, "Ukuran")

        If Lap = "Nominal" Then
            'MsgBox("Nominal")
            Dim XR As New XRKrtStokBBNom
            XR.InitializeData(bind)
        Else
            'MsgBox("Else")
            If Me.CEBtNum.EditValue = True Then
                'MsgBox("Else include batch")
                bind.Add(BtNum, "BtNum")

                Dim XR As New XRKrtStokBBBtNum
                XR.InitializeData(bind)
            Else
                'MsgBox("Else tanpa batch")
                Dim XR As New XRKrtStokBB
                XR.InitializeData(bind)
            End If


        End If
    End Sub

    Private Sub CEBtNum_EditValueChanged(sender As Object, e As EventArgs) Handles CEBtNum.EditValueChanged
        If Me.CEBtNum.EditValue = True Then
            Me.XTPNoBtNum.PageVisible = False
            Me.XTPWBtNum.PageVisible = True

        Else
            Me.XTPNoBtNum.PageVisible = True
            Me.XTPWBtNum.PageVisible = False
        End If
    End Sub
End Class