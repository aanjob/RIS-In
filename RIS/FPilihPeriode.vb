Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FPilihPeriode
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Gol As String

    Public Sub New(Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Gol = Golongan
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FPilihPeriode_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        'DsMaster = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter

        If Gol = "Bea Cukai" Then
            cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol <>'" & Gol & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE")
            cmsl.Fill(DsMaster, "M_GudangLUE")
            DsMaster.Tables("M_GudangLUE").Clear()
            cmsl.Fill(DsMaster, "M_GudangLUE")

            Me.SLUGudang.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
            Me.SLUGudang.Properties.DisplayMember = "Nama"
            Me.SLUGudang.Properties.ValueMember = "GdID"

        Else
            cmsl = New SqlDataAdapter("Select GdID,Nama From M_Gudang Where Gol ='" & Gol & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_GudangLUE")
            cmsl.Fill(DsMaster, "M_GudangLUE")
            DsMaster.Tables("M_GudangLUE").Clear()
            cmsl.Fill(DsMaster, "M_GudangLUE")

            Me.SLUGudang.Properties.DataSource = DsMaster.Tables("M_GudangLUE")
            Me.SLUGudang.Properties.DisplayMember = "Nama"
            Me.SLUGudang.Properties.ValueMember = "GdID"
        End If

    End Sub

    Private Sub BOk_Click(sender As Object, e As EventArgs) Handles BOk.Click
        MainModule.PilihAwal = Me.DTPAwal.EditValue
        MainModule.PilihAkhir = Me.DTPAkhir.EditValue
        MainModule.PilihGudangID = Me.SLUGudang.EditValue
        MainModule.PilihGudang = Me.SLUGudang.Text
        MainModule.PilihGudangID = Me.SLUGudang.EditValue
        MainModule.PilihKat = Me.CBOKat.EditValue

        If Me.CBOTipePPn.EditValue = "Semua" Then
            MainModule.PilihPPn = "%"
        ElseIf Me.CBOTipePPn.EditValue = "Non PPn" Then
            MainModule.PilihPPn = "0"
        ElseIf Me.CBOTipePPn.EditValue = "PPn" Then
            MainModule.PilihPPn = "1"
        End If

        If Me.CBOKat.EditValue = "Semua" Then
            MainModule.PilihKat = "%"
        Else
            MainModule.PilihKat = Me.CBOKat.EditValue

        End If

        Me.Dispose()
    End Sub
End Class