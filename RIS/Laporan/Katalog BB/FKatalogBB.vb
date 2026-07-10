Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKatalogBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Lap As String
    Dim DsLapF As New System.Data.DataSet
    Dim InisialBC As String = ""

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPTanggal.EditValue = System.DateTime.Now

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FKatalogBB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        Try
            DsLapF.Tables("M_CustL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_CustL")
        DsLapF.Tables("M_CustL").Rows.Add("%", "Semua", "")

        Me.SLUCust.Properties.DataSource = DsLapF.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"

        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,JnsID,Nama From M_BBJns", koneksi)
        cmsl.TableMappings.Add("Table", "M_BBJns")
        cmsl.SelectCommand.CommandTimeout = 90000
        Try
            DsLapF.Tables("M_BBJns").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BBJns")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_BBJns"
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

        MainModule.PilihTgl = Me.DTPTanggal.EditValue

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
        bind.Add(Me.SLUCust.EditValue, "CustID")

        Dim XR As New XRKatalogBB
        XR.InitializeData(bind)
    End Sub
End Class