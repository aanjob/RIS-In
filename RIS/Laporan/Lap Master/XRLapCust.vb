Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRLapCust
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim CFAktif As DevExpress.XtraReports.UI.CalculatedField
    Dim CFCab As DevExpress.XtraReports.UI.CalculatedField
    Dim SumAktif As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumCab As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        If Bind.Item("Gol").ToString = "Character" Then
            cmsl = New SqlDataAdapter("Select Case When C.Aktif='True' Then 'Aktif' Else 'Non Aktif' End As Aktif,Cb.Cabang, C.CustID, C.Nama, J.Jenis As JnsCust, J.Harga, C.Alamat, K.Nama As Kota, P.Nama As Propinsi, NPWP, C.NoTelp, Fax, JT,  CLCr As CL, DiscCust,NamaCP1 From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Prop P On K.PropID=P.PropID Inner Join  M_JnsCust J On C.JnsCustId=J.JnsCustId Inner Join M_CabCust H On H.CustID=C.CustID Inner Join M_Cab Cb On H.CabID=Cb.CabID Where Cb.CabID Like '" & MainModule.PilihCab & "'", koneksi)

        ElseIf Bind.Item("Gol").ToString = "Own" Then
            cmsl = New SqlDataAdapter("Select Case When C.Aktif='True' Then 'Aktif' Else 'Non Aktif' End As Aktif,Cb.Cabang, C.CustID, C.Nama, J.Jenis As JnsCust, J.Harga, C.Alamat, K.Nama As Kota, P.Nama As Propinsi, NPWP, C.NoTelp, Fax, JT,  CLOwn As CL, DiscCust,NamaCP1 From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Prop P On K.PropID=P.PropID Inner Join  M_JnsCust J On C.JnsCustId=J.JnsCustId Inner Join M_CabCust H On H.CustID=C.CustID Inner Join M_Cab Cb On H.CabID=Cb.CabID Where Cb.CabID Like '" & MainModule.PilihCab & "' /*AND J.Jenis='GROSIR' and c.Aktif=1*/", koneksi)
        Else
            cmsl = New SqlDataAdapter("Select Case When C.Aktif='True' Then 'Aktif' Else 'Non Aktif' End As Aktif,Cb.Cabang, C.CustID, C.Nama, J.Jenis As JnsCust, J.Harga, C.Alamat, K.Nama As Kota, P.Nama As Propinsi, NPWP, C.NoTelp, Fax, JT,  0 As CL, DiscCust,NamaCP1 From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Prop P On K.PropID=P.PropID Inner Join  M_JnsCust J On C.JnsCustId=J.JnsCustId Inner Join M_CabCust H On H.CustID=C.CustID Inner Join M_Cab Cb On H.CabID=Cb.CabID Where Cb.CabID Like '" & MainModule.PilihCab & "'", koneksi)
        End If

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_Cust", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Aktif", "Aktif"), New System.Data.Common.DataColumnMapping("Cabang", "Cabang"), New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("JnsCust", "JnsCust"), New System.Data.Common.DataColumnMapping("Harga", "Harga"), New System.Data.Common.DataColumnMapping("Alamat", "Alamat"), New System.Data.Common.DataColumnMapping("Kota", "Kota"), New System.Data.Common.DataColumnMapping("Propinsi", "Propinsi"), New System.Data.Common.DataColumnMapping("NPWP", "NPWP"), New System.Data.Common.DataColumnMapping("NoTelp", "NoTelp"), New System.Data.Common.DataColumnMapping("Fax", "Fax"), New System.Data.Common.DataColumnMapping("JT", "JT"), New System.Data.Common.DataColumnMapping("CL", "CL"), New System.Data.Common.DataColumnMapping("DiscCust", "DiscCust"), New System.Data.Common.DataColumnMapping("NamaCP1", "NamaCP1")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_Cust")

        Me.DataMember = "M_Cust"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHAktif.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHAktif})
        Me.GHAktif.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Aktif", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHSales.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHSales})
        Me.GHSales.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Cabang", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        If MainModule.Posisi Like "*Cabang" Then
            Me.LBCustomer.Visible = False
            Me.LBAlamat.Visible = False
            Me.LBKota.Visible = False
            Me.LBProp.Visible = False
            Me.LBTelp.Visible = False

        Else
            Me.LBCustomer.Visible = True
            Me.LBAlamat.Visible = True
            Me.LBKota.Visible = True
            Me.LBProp.Visible = True
            Me.LBTelp.Visible = True
        End If

        Me.LBGHAktif.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Aktif")})
        Me.LBGHSales.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Sales")})
        Me.LBGFAktif.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Aktif", "Jumlah Customer {0}")})
        Me.LBGFSales.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Cabang", "Jumlah Customer {0}")})
        Me.LBCustID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.CustID")})
        Me.LBNPWP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.NPWP")})
        Me.LBCustomer.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Nama")})
        Me.LBJnsCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.JnsCust")})
        Me.LBAlamat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Alamat")})
        Me.LBKota.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Kota")})
        Me.LBProp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Propinsi")})
        Me.LBTelp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.NoTelp")})
        Me.LBCP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.NamaCP1")})
        Me.LBTOP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.JT")})
        Me.LBStatus.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.Harga")})
        Me.LBDisc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.DiscCust")})
        Me.LBCL.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.CL")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFAktif, Me.LBGFTotAktif})

        Me.GroupFooter2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFSales, Me.LBGFTotSales})

        CFAktif = New DevExpress.XtraReports.UI.CalculatedField
        Me.CFAktif.DataMember = "M_Cust"
        Me.CFAktif.DataSource = DsLap
        Me.CFAktif.Expression = "[][JK] > [0].[CustID].Count()"
        Me.CFAktif.FieldType = DevExpress.XtraReports.UI.FieldType.[Int32]
        Me.CFAktif.Name = "CFAktif"

        Me.LBGFTotAktif.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.CFAktif")})
        SumAktif.FormatString = ": {0:#,##0;(#,##0);0}"
        SumAktif.Func = DevExpress.XtraReports.UI.SummaryFunc.Count
        SumAktif.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotAktif.Summary = SumAktif


        CFCab = New DevExpress.XtraReports.UI.CalculatedField
        Me.CFCab.DataMember = "M_Cust"
        Me.CFCab.DataSource = DsLap
        Me.CFCab.Expression = "[][JK] > [0].[CustID].Count()"
        Me.CFCab.FieldType = DevExpress.XtraReports.UI.FieldType.[Int32]
        Me.CFCab.Name = "CFCab"

        Me.LBGFTotSales.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_Cust.CFCab")})
        SumCab.FormatString = ": {0:#,##0;(#,##0);0}"
        SumCab.Func = DevExpress.XtraReports.UI.SummaryFunc.Count
        SumCab.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotSales.Summary = SumCab

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRLapCust_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class