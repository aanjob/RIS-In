Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRModel
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim StdBB As Decimal
    Dim Start As Integer

    Public Sub InitializeData(ByVal Bind As Collection)
        'cmsl = New SqlDataAdapter("Select ROW_NUMBER() OVER(ORDER BY M.BBID Asc) AS Row, D.Urut,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan, UkBB, M.Sat,M.Uk,Isnull(Std,0) As Std From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Bind.Item("Kode").ToString & "' Order By D.Urut,K.Nama,B.Nama,M.Uk Asc", koneksi)

        cmsl = New SqlDataAdapter("Select ROW_NUMBER() OVER(ORDER BY BBID Asc) AS Row,* From(Select D.Urut,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan, UkBB, M.Sat,M.Uk,Isnull(Std,0) As Std,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Bind.Item("Kode").ToString & "' and BBIDInd='' Union All Select D.Urut,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan, UkBB, M.Sat,M.Uk,Isnull(Std,0) As Std,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Bind.Item("Kode").ToString & "' and BBIDInd<>'') as x Order By Urut,Komponen,Bahan,Uk,BBIDInd Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "M_ModelDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Row", "Row"), New System.Data.Common.DataColumnMapping("Divisi", "Divisi"), New System.Data.Common.DataColumnMapping("KompID", "KompID"), New System.Data.Common.DataColumnMapping("Komponen", "Komponen"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("UkBB", "UkBB"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Std", "Std")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "M_ModelDtl")

        Me.DataMember = "M_ModelDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBBrand.Text = ": " & Bind.Item("Brand").ToString
        Me.LBStyle.Text = ": " & Bind.Item("StyleID").ToString
        Me.LBArtName.Text = ": " & Bind.Item("ArtName").ToString
        Me.LBWarna.Text = ": " & Bind.Item("Warna").ToString
        Me.LBSL.Text = ": " & Bind.Item("ShoeLast").ToString
        Me.LBKet.Text = Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBUk1.Text = Bind.Item("Uk1").ToString
        Me.LBUk2.Text = Bind.Item("Uk2").ToString
        Me.LBUk3.Text = Bind.Item("Uk3").ToString
        Me.LBUk4.Text = Bind.Item("Uk4").ToString
        Me.LBUk5.Text = Bind.Item("Uk5").ToString
        Me.LBUk6.Text = Bind.Item("Uk6").ToString
        Me.LBUk7.Text = Bind.Item("Uk7").ToString
        Me.LBUk8.Text = Bind.Item("Uk8").ToString
        Me.LBUk9.Text = Bind.Item("Uk9").ToString
        Me.LBUk10.Text = Bind.Item("Uk10").ToString
        Me.LBUk11.Text = Bind.Item("Uk11").ToString
        Me.LBUk12.Text = Bind.Item("Uk12").ToString

        Me.GHDiv.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBDivisi})
        Me.GHDiv.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Urut", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHKomp.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBKomponen})
        Me.GHKomp.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Komponen", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBBBID})
        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Bahan", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBIDInd.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHBBIDInd})
        Me.GHBBIDInd.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("BBIDInd", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHBBIDInd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.BBIDInd")})
        Me.LBBBIDInd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.BBIDInd")})
        Me.LBRow.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Row")})
        Me.LBDivisi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Divisi")})
        Me.LBKomponen.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Komponen")})
        Me.LBComponent.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Komponen")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Bahan")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Sat")})
        Me.LBUkBB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.UkBB")})
        Me.LBUk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Uk")})
        Me.LBStd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_ModelDtl.Std")})

        Me.ShowPreview()
    End Sub

    Private Sub XRSpec_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub LBUk_TextChanged(sender As Object, e As EventArgs) Handles LBUk.TextChanged
        'ElseIf Me.LBUk.Text = Me.LBUk5.Text And Me.LBKomponen.Text = Me.LBComponent.Text And Me.LBNmBB.Text = Me.LBBahan.Text Then

        If Me.LBUk.Text = Me.LBUk1.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd1.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk2.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd2.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk3.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd3.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk4.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd4.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk5.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd5.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk6.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd6.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk7.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd7.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk8.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd8.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk9.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd9.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk10.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd10.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk11.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd11.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk12.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd12.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)
        End If

    End Sub

    Private Sub LBDivisi_TextChanged(sender As Object, e As EventArgs) Handles LBDivisi.TextChanged
        Start = 0
    End Sub

    Private Sub LBRow_TextChanged(sender As Object, e As EventArgs) Handles LBRow.TextChanged
        Start += 1
        Me.LBNo.Text = Start
    End Sub

    Private Sub LBStd_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBStd.BeforePrint
        If Me.LBUk.Text = Me.LBUk1.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd1.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk2.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd2.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk3.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd3.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk4.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd4.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk5.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd5.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk6.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd6.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk7.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd7.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk8.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd8.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk9.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd9.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk10.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd10.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk11.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd11.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk12.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd12.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)
        End If
    End Sub

    Private Sub LBBBIDInd_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBBBIDInd.BeforePrint
        If Me.LBUk.Text = Me.LBUk1.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd1.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk2.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd2.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk3.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd3.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk4.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd4.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk5.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd5.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk6.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd6.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk7.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd7.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk8.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd8.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk9.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd9.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk10.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd10.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk11.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd11.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk12.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd12.Text = String.Format("{0:#,##0.0000;(#,##0.0000);""}", StdBB)
        End If
    End Sub
End Class