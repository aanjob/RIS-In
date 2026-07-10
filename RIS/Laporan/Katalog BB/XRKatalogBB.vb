Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRKatalogBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("SPLKatalogBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@CustID", SqlDbType.VarChar).Value = Bind.Item("CustID").ToString
        cmsl.SelectCommand.Parameters.Add("@JnsID", SqlDbType.VarChar).Value = Bind.Item("JnsID").ToString
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.VarChar).Value = MainModule.PilihTgl
        cmsl.SelectCommand.CommandTimeout = 90000

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "KatalogBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("JnsID", "JnsID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "KatalogBB")

        Me.DataMember = "KatalogBB"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBTgl.Text = "Per Tanggal " & Format(CDate(MainModule.PilihTgl), "dd MMMM yyyy") &
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHCust.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBCust})
        Me.GHCust.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Cust", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHJnsBB.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBJnsBB})
        Me.GHJnsBB.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Jenis", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KatalogBB.Cust", "Customer : {0}")})
        Me.LBJnsBB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KatalogBB.Jenis", "Jenis Bahan : {0}")})
        'Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KatalogBB.BBID")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub
    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
        Dim xrSubReport As XRSubreport = DirectCast(sender, XRSubreport)
        Dim subRep As XtraReport = TryCast(xrSubReport.ReportSource, XtraReport)
        subRep.Parameters("JnsID").Value = Convert.ToString(GetCurrentColumnValue("JnsID"))
        subRep.Parameters("CustID").Value = Convert.ToString(GetCurrentColumnValue("CustID"))

    End Sub

    Private Sub XRKrtStokBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class