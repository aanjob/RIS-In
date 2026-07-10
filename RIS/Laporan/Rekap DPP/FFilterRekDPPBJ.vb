Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FFilterRekDPPBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Lap, Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(ByVal Report As String, ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Lap = Report
        Gol = Golongan

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Me.DTPTanggal.EditValue = System.DateTime.Now
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FFilterRekDPP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UC.CabID,C.Cabang From M_UsCab UC Inner Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsLapF.Tables("M_UsCabLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_UsCabLUE")
        DsLapF.Tables("M_UsCabLUE").Rows.Add("%", "Semua Cabang")

        Me.SLUCab.Properties.DataSource = DsLapF.Tables("M_UsCabLUE")
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"

    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        Dim cmsl As SqlDataAdapter
        If Not IsDBNull(Me.SLUCab.EditValue) Then
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,C.CustID,C.Nama As Cust,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID='" & Me.SLUCab.EditValue & "' and CC.Aktif='True'", koneksi)
            cmsl.TableMappings.Add("Table", "M_CustL2")

            Try
                DsLapF.Tables("M_CustL2").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsLapF, "M_CustL2")

           Me.GridControl1.DataSource = DsLapF
            Me.GridControl1.DataMember = "M_CustL2"
        End If
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To GridView1.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                GridView1.SetRowCellValue(i, "Cek", 0)
                'End If
            Next
        Else
            CekAll = True
            For i As Integer = 0 To GridView1.RowCount - 1
                'If GridView1.IsRowVisible(i) Then
                GridView1.SetRowCellValue(i, "Cek", 1)
                'End If
            Next
        End If
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        'MsgBox(Lap & " " & SLUCab.EditValue)
        If Lap = "Rekap" Then
            Dim x, i As Integer
            Dim Cust As String = ""

            x = 0
            i = 0
            For i = 0 To DsLapF.Tables("M_CustL2").Rows.Count - 1
                If DsLapF.Tables("M_CustL2").Rows(i).Item("Cek") = True Then
                    x += 1
                    If x = 1 Then
                        Cust = "'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                    Else
                        Cust &= ",'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                    End If
                End If
            Next

            Dim bind As New Collection
            bind.Add(Cust, "Cust")
            bind.Add(Me.SLUCab.EditValue, "CabID")
            bind.Add(Me.SLUCab.Text, "Cabang")
            bind.Add(Me.DTPTanggal.EditValue, "Tanggal")
            MainModule.PilihAwal = Me.DTPAwal.EditValue
            MainModule.PilihAkhir = Me.DTPAkhir.EditValue

            Dim XR As New XRRekDPPBJ
            XR.InitializeData(bind)

        ElseIf Lap = "Serah Terima" And SLUCab.EditValue = "RJH" And Gol = "Job Order" Then
            'MsgBox("MASUK RJHimson" & Gol)
            Dim bind As New Collection
            bind.Add(Me.SLUCab.EditValue, "CabID")
            bind.Add(Me.SLUCab.Text, "Cabang")
            bind.Add(Me.DTPTanggal.EditValue, "Tanggal")
            bind.Add(Gol, "Gol")
            MainModule.PilihAwal = Me.DTPAwal.EditValue
            MainModule.PilihAkhir = Me.DTPAkhir.EditValue

            Dim XR As New XRSTDPPJO
            XR.InitializeData(bind)

        ElseIf Lap = "Serah Terima" Then
            'MsgBox("MASUK" & Gol)
            Dim bind As New Collection
            bind.Add(Me.SLUCab.EditValue, "CabID")
            bind.Add(Me.SLUCab.Text, "Cabang")
            bind.Add(Me.DTPTanggal.EditValue, "Tanggal")
            bind.Add(Gol, "Gol")
            MainModule.PilihAwal = Me.DTPAwal.EditValue
            MainModule.PilihAkhir = Me.DTPAkhir.EditValue

            Dim XR As New XRSTDPP
            XR.InitializeData(bind)
        End If
    End Sub

  
End Class