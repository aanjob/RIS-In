Imports System.Data.SqlClient
Imports System
Imports System.Collections.Generic
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraReports.UI

Public Class XRLapBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Semua As Boolean, Gol As String, BBID As String)

        If Semua = True Then
            cmsl = New SqlDataAdapter("Select JnsPers,BBID,B.Nama From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where J.Gol='" & Gol & "'", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select JnsPers,BBID,B.Nama From M_BB B Inner Join M_BBJns J On B.JnsID=J.JnsID Where J.Gol='" & Gol & "' and B.BBID In (" & BBID & ")", koneksi)
        End If

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LapBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("JnsPers", "JnsPers"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "LapBB")

        Me.DataMember = "LapBB"
        Me.DataSource = DsLap

        'Me.LBUser.Text = MainModule.LoginAktif

        'Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBJnsPers})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("JnsPers", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LapBB.BBID")})
        'Me.LBNama.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LapBB.Nama")})

        'If MainModule.PrintDt = False Then
        '    Me.LBUser.Visible = False
        '    Me.XrPageInfo2.Visible = False
        'End If

        Me.ShowPreview()
    End Sub

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("BBID").Value = Convert.ToString(GetCurrentColumnValue("BBID"))

    End Sub

    Private Sub XRKaryawan_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class