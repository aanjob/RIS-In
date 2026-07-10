Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO

Public Class XRStokBJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand

    Public Sub InitializeData(ByVal Bind As Collection)
        'Dim cmsl As SqlDataAdapter
        'cmsl = New SqlDataAdapter("SPLStokHargaBJ", koneksi)
        'cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        'cmsl.SelectCommand.Parameters.Add("@Tgl", SqlDbType.Date).Value = "2018/03/12"
        'cmsl.SelectCommand.Parameters.Add("@Grup", SqlDbType.VarChar).Value = "Air Max"
        'cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = "Own"
        'cmsl.SelectCommand.CommandTimeout = 90000


        'DsLap = New System.Data.DataSet
        'cmsl.Fill(DsLap, "SPLStokHargaBJ")

        Dim dtsStokBJ As New List(Of DsStokBJ)
        For Each dr As DataRow In DsLap.Tables("StokHargaBJ" & Bind.Item("Gol").ToString).Rows
            Dim ds As New DsStokBJ
            ds.Gudang = dr.Item("Gudang").ToString
            ds.ArtCode = dr.Item("ArtCode").ToString
            ds.ArtName = dr.Item("ArtName").ToString
            ds.SatID = dr.Item("SatID").ToString
            ds.Isi = dr.Item("Isi").ToString
            ds.stsHarga = dr.Item("stsHarga").ToString
            ds.ADPJ = dr.Item("ADPJ").ToString
            ds.AFRK = dr.Item("AFRK").ToString
            ds.GDPJ = dr.Item("GDPJ").ToString
            ds.GFRK = dr.Item("GFRK").ToString
            ds.RDPJ = dr.Item("RDPJ").ToString
            ds.RFRK = dr.Item("RFRK").ToString
            ds.DiscOB = dr.Item("DiscOB").ToString
            ds.SP = dr.Item("SP").ToString
            ds.HargaCBP = dr.Item("HargaCBP").ToString
            ds.Stok = dr.Item("Stok").ToString
            ds.Psg = dr.Item("Psg").ToString

            Dim command As New SqlCommand("Select Picture From M_Image Where ID = '" & ds.ArtCode & "'", koneksi)
            command.CommandTimeout = 90000
            With koneksi
                .Open()
                ds.gambar = command.ExecuteScalar()
                .Close()
            End With

            dtsStokBJ.Add(ds)
        Next
        Me.DataSource = dtsStokBJ

        Me.ShowPreview()
    End Sub

    Private Sub XRStokBJ_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class