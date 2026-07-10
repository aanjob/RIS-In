Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FKrtStokBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Gol, ArtCode As String
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(ByVal Golongan As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPAwal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPAkhir.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Gol = Golongan
        Me.ESIGol.Text = "    " & Golongan

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOutsPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CBOPilihUk.EditValue = "1 Halaman"
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsGrupLUE")
        Try
            DsMaster.Tables("M_UsGrupLUE").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsGrupLUE")

        Me.SLUGrup.Properties.DataSource = DsMaster.Tables("M_UsGrupLUE")
        Me.SLUGrup.Properties.DisplayMember = "Grup"
        Me.SLUGrup.Properties.ValueMember = "Grup"

        cmsl = New SqlDataAdapter("Select UC.CabID,C.Cabang From M_UsCab UC Inner Join M_Cab C On UC.CabID=C.CabID Where UserID=" & MainModule.UserAktif & "", koneksi)
        cmsl.TableMappings.Add("Table", "M_UsCabLUE")
        Try
            DsMaster.Tables("M_UsCabLUE" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_UsCabLUE" & Gol)

        Me.SLUCab.Properties.DataSource = DsMaster.Tables("M_UsCabLUE" & Gol)
        Me.SLUCab.Properties.DisplayMember = "Cabang"
        Me.SLUCab.Properties.ValueMember = "CabID"
    End Sub

    Private Sub SLUCab_Leave(sender As Object, e As EventArgs) Handles SLUCab.Leave
        Try
            If Not IsDBNull(Me.SLUCab.EditValue) Then
                Dim cmsl As SqlDataAdapter
                cmsl = New SqlDataAdapter("Select G.GdID,Nama From M_Gudang G Inner Join M_CabGd CG On G.GdID=CG.GdID Where CG.CabID Like'" & Me.SLUCab.EditValue & "' and Grup Like'" & Me.SLUGrup.EditValue & "'", koneksi)
                cmsl.TableMappings.Add("Table", "M_GudangL" & Gol)
                Try
                    DsMaster.Tables("M_GudangL" & Gol).Clear()
                Catch ex As Exception

                End Try
                cmsl.Fill(DsMaster, "M_GudangL" & Gol)

                Me.SLUGudang.Properties.DataSource = DsMaster.Tables("M_GudangL" & Gol)
                Me.SLUGudang.Properties.DisplayMember = "Nama"
                Me.SLUGudang.Properties.ValueMember = "GdID"

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as Cek,ArtCode,ArtName,Isi From M_Brg B Inner Join M_BrgMerk M On B.MerkID=M.MerkID Where B.Grup Like'" & Me.SLUGrup.EditValue & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_BrgL2" & Gol)
        cmsl.SelectCommand.CommandTimeout = 90000
        Try
            DsLapF.Tables("M_BrgL2" & Gol).Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "M_BrgL2" & Gol)

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "M_BrgL2" & Gol
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
        Dim Supp As String = ""
        Dim BBID As String = ""

        x = 0
        i = 0
        For i = 0 To DsLapF.Tables("M_BrgL2" & Gol).Rows.Count - 1
            If DsLapF.Tables("M_BrgL2" & Gol).Rows(i).Item("Cek") = True Then
                x += 1
                If x = 1 Then
                    ArtCode = "'" & DsLapF.Tables("M_BrgL2" & Gol).Rows(i).Item("ArtCode") & "'"
                Else
                    ArtCode &= ",'" & DsLapF.Tables("M_BrgL2" & Gol).Rows(i).Item("ArtCode") & "'"
                End If
            End If
        Next

        Dim bind As New Collection
        bind.Add(ArtCode, "ArtCode")
        bind.Add(Me.SLUGudang.EditValue, "Gd")
        bind.Add(Me.SLUGudang.Text, "Gudang")
        bind.Add(Me.CBOPilihUk.EditValue, "Ukuran")
        bind.Add(Gol, "Gol")

        Dim XR As New XRKrtStokBJ
        XR.InitializeData(bind)

    End Sub

End Class