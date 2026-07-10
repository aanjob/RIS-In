Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPilihCabang
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        If Golongan = "Job Order" Then
            Gol = Golongan
        Else
            Gol = "Lokal"
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FPilihSales_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select UC.CabID,C.Cabang From M_UsCab UC Inner Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsLapF.Tables("M_UsCabLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_UsCabLUE")

        Me.SLUCab.Properties.DataSource = DsLapF.Tables("M_UsCabLUE")
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"
    End Sub

    Private Sub BPreview_Click(sender As Object, e As EventArgs) Handles BPreview.Click
        MainModule.PilihCab = Me.SLUCab.EditValue
        Me.Dispose()
    End Sub
End Class