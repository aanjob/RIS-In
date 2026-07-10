Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.IO

Public Class FModel_sa
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Private Sub FModel_sa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MdlID,M.PeriodID,M.CodeID,M.Tanggal,M.SpecID,M.StyleID,S.Brand,M.ArtName,M.Warna,M.RangeSize, M.SampleSize,PersenGenerate,M.Ket From M_Model M Inner Join M_Spec S On M.SpecId=S.SpecID", koneksi)

        cmsl.TableMappings.Add("Table", "M_ModelSa")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_ModelSa")
        DsMaster.Tables("M_ModelSa").Clear()
        cmsl.Fill(DsMaster, "M_ModelSa")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_ModelSa"
    End Sub
End Class