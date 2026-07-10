Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient

Public Class FNotPesSP
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CustID As String
    Public Sub New(ByVal CustomerID As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        CustID = CustomerID

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FNotPesSP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select JualID,Tanggal,DueDate,SisaBayar From T_JualBJ where CustID='" & CustID & "' and stsLunas='False' Order By DueDate Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_NotPesSP")
        Try
            DsMaster.Tables("T_NotPesSP").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_NotPesSP")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_NotPesSP"
    End Sub
End Class