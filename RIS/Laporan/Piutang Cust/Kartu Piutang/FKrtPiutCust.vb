Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKrtPiutCust
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FKrtPiutCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UC.CabID,Case When UC.CAbID='%' Then 'Semua Cabang' Else C.Cabang End As Cabang From M_UsCab UC Left Outer Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsMaster.Tables("M_UsCabLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabLUE")

        Me.SLUCab.Properties.DataSource = DsMaster.Tables("M_UsCabLUE")
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"

        cmsl = New SqlDataAdapter("Select Distinct MtUang From M_Curr", koneksi)
        cmsl.TableMappings.Add("Table", "M_CurrLUE")
        cmsl.Fill(DsMaster, "M_CurrLUE")
        DsMaster.Tables("M_CurrLUE").Clear()
        cmsl.Fill(DsMaster, "M_CurrLUE")

        Me.SLUMtUang.Properties.DataSource = DsMaster.Tables("M_CurrLUE")
        Me.SLUMtUang.Properties.DisplayMember = "MtUang"
        Me.SLUMtUang.Properties.ValueMember = "MtUang"
    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        Dim cmsl As SqlDataAdapter

        Try

            If Not IsDBNull(Me.SLUCab.EditValue) Then
                cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,C.CustID,C.Nama As Cust,Alamat,K.Nama As Kota From M_Cust C Inner Join M_CabCust CC On C.CustID=CC.CustID Inner Join M_Kota K On C.KotaID=K.KotaID Where CC.CabID Like '" & Me.SLUCab.EditValue & "' and CC.Aktif='True'", koneksi)
                cmsl.TableMappings.Add("Table", "M_CustL2")
                cmsl.Fill(DsLapF, "M_CustL2")
                DsLapF.Tables("M_CustL2").Clear()
                cmsl.Fill(DsLapF, "M_CustL2")

                Me.GridControl1.DataSource = DsLapF
                Me.GridControl1.DataMember = "M_CustL2"

            End If
        Catch ex As Exception

        End Try
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
        Me.GridView1.ActiveFilter.Clear()

        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue

        Dim x, i As Integer
        Dim CustID As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("M_CustL2").Rows.Count - 1
            If DsLapF.Tables("M_CustL2").Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    CustID = "'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                Else
                    CustID &= ",'" & DsLapF.Tables("M_CustL2").Rows(i).Item("CustID") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(Me.SLUMtUang.EditValue, "MtUang")
        bind.Add(CustID, "CustID")
        bind.Add(Gol, "Gol")

        Dim XR As New XRKrtPiutCust
        XR.InitializeData(bind)

    End Sub
End Class