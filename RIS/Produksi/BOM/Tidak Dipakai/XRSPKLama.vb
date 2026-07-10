Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors
Imports System.IO

Public Class XRSPKLama
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim StdBB As Decimal
    Dim SumKeb As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.Urut,D.Nama as Divisi, BD.KompID, K.Nama As Komponen, BD.BBID, B.Nama As Bahan, B.Uk As UkBB, BD.Sat, Round(Std,2) as Std, Keb,BD.Uk,BD.Ket,stsAdd From T_BOMDtl BD Inner Join M_Div D On BD.DivID=D.DivID Inner Join M_Komp K On BD.KompID=K.KompID Inner Join M_BB B On BD.BBID=B.BBID Where BOMID='" & Bind.Item("Kode").ToString & "' and SPK='True' Order By D.Urut, K.Urut,K.Nama,B.Nama,BD.Uk Asc", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_BOMDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Urut", "Urut"), New System.Data.Common.DataColumnMapping("Divisi", "Divisi"), New System.Data.Common.DataColumnMapping("KompID", "KompID"), New System.Data.Common.DataColumnMapping("Komponen", "Komponen"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("UkBB", "UkBB"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Std", "Std"), New System.Data.Common.DataColumnMapping("Keb", "Keb"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Ket", "Ket"), New System.Data.Common.DataColumnMapping("stsAdd", "stsAdd")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_BOMDtl")

        Me.DataMember = "T_BOMDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = ": " & Bind.Item("SPK").ToString
        Me.LBCust.Text = ": " & Bind.Item("Customer").ToString
        Me.LBPOID.Text = ": " & Bind.Item("POID").ToString
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBBrand.Text = ": " & Bind.Item("Brand").ToString
        Me.LBStyle.Text = ": " & Bind.Item("StyleID").ToString
        Me.LBArtName.Text = ": " & Bind.Item("ArtName").ToString
        Me.LBSL.Text = ": " & Bind.Item("ShoeLast").ToString
        Me.LBWarnaH.Text = Bind.Item("Warna").ToString
        Me.LBWarnaF.Text = Bind.Item("Warna").ToString
        Me.LBHCBP.Text = ": " & String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("HCBP").ToString))
        Me.LBKet.Text = Bind.Item("Ket").ToString
        Me.LBTot.Text = Bind.Item("Tot").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBUk1.Text = Bind.Item("Uk1").ToString
        Me.LBUk2.Text = Bind.Item("Uk2").ToString
        Me.LBUk3.Text = Bind.Item("Uk3").ToString
        Me.LBUk4.Text = Bind.Item("Uk4").ToString
        Me.LBUk5.Text = Bind.Item("Uk5").ToString
        Me.LBUk6.Text = Bind.Item("Uk6").ToString
        Me.LBUk7.Text = Bind.Item("Uk7").ToString
        Me.LBUk8.Text = Bind.Item("Uk8").ToString

        Me.LBUkH1.Text = Bind.Item("Uk1").ToString
        Me.LBUkH2.Text = Bind.Item("Uk2").ToString
        Me.LBUkH3.Text = Bind.Item("Uk3").ToString
        Me.LBUkH4.Text = Bind.Item("Uk4").ToString
        Me.LBUkH5.Text = Bind.Item("Uk5").ToString
        Me.LBUkH6.Text = Bind.Item("Uk6").ToString
        Me.LBUkH7.Text = Bind.Item("Uk7").ToString
        Me.LBUkH8.Text = Bind.Item("Uk8").ToString

        Me.LBUkF1.Text = Bind.Item("Uk1").ToString
        Me.LBUkF2.Text = Bind.Item("Uk2").ToString
        Me.LBUkF3.Text = Bind.Item("Uk3").ToString
        Me.LBUkF4.Text = Bind.Item("Uk4").ToString
        Me.LBUkF5.Text = Bind.Item("Uk5").ToString
        Me.LBUkF6.Text = Bind.Item("Uk6").ToString
        Me.LBUkF7.Text = Bind.Item("Uk7").ToString
        Me.LBUkF8.Text = Bind.Item("Uk8").ToString

        Me.LBOrder1.Text = Bind.Item("Qty1").ToString
        Me.LBOrder2.Text = Bind.Item("Qty2").ToString
        Me.LBOrder3.Text = Bind.Item("Qty3").ToString
        Me.LBOrder4.Text = Bind.Item("Qty4").ToString
        Me.LBOrder5.Text = Bind.Item("Qty5").ToString
        Me.LBOrder6.Text = Bind.Item("Qty6").ToString
        Me.LBOrder7.Text = Bind.Item("Qty7").ToString
        Me.LBOrder8.Text = Bind.Item("Qty8").ToString

        Me.LBAss1.Text = Bind.Item("Isi1").ToString
        Me.LBAss2.Text = Bind.Item("Isi2").ToString
        Me.LBAss3.Text = Bind.Item("Isi3").ToString
        Me.LBAss4.Text = Bind.Item("Isi4").ToString
        Me.LBAss5.Text = Bind.Item("Isi5").ToString
        Me.LBAss6.Text = Bind.Item("Isi6").ToString
        Me.LBAss7.Text = Bind.Item("Isi7").ToString
        Me.LBAss8.Text = Bind.Item("Isi8").ToString

        Me.GHDiv.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBDivisi})
        Me.GHDiv.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Urut", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHKomp.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBKomponen})
        Me.GHKomp.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Komponen", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.GHBBID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBBB})
        Me.GHBBID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Bahan", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBDivisi.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Divisi")})
        Me.LBKomponen.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Komponen")})
        Me.LBComponent.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Komponen")})
        Me.LBBB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Bahan")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Sat")})
        Me.LBUkBB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.UkBB")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Ket")})
        Me.LBUk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Uk")})
        Me.LBStd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Std")})
        Me.LBAdd.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.stsAdd")})

        Me.LBKeb.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BOMDtl.Keb", "{0:n2}")})
        SumKeb.FormatString = "{0:n2}"
        SumKeb.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBKeb.Summary = SumKeb

        Dim command As New SqlCommand("Select Picture From M_Image Where ID = '" & Bind.Item("SpecID").ToString & "'", koneksi)
        Dim Pic() As Byte
        Dim newImage As Image

        With koneksi
            .Open()
            Pic = command.ExecuteScalar()
            .Close()
        End With

        If Pic IsNot Nothing Then
            Using ms As New MemoryStream(Pic, 0, Pic.Length)
                ms.Write(Pic, 0, Pic.Length)
                newImage = Image.FromStream(ms, True)
            End Using
            Me.PGambar.Image = newImage
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSpec_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub LBUk_BeforePrint(sender As Object, e As EventArgs) Handles LBUk.BeforePrint
        If Me.LBUk.Text = Me.LBUk1.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd1.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk2.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd2.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk3.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd3.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk4.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd4.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk5.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd5.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk6.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd6.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk7.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd7.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)

        ElseIf Me.LBUk.Text = Me.LBUk8.Text Then
            StdBB = Me.LBStd.Text
            Me.LBStd8.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", StdBB)
        End If
    End Sub

    Private Sub LBAdd_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles LBAdd.BeforePrint
        If Me.LBAdd.Text = "True" Then
            Me.LBStd1.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd2.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd3.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd4.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd5.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd6.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd7.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
            Me.LBStd8.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", 0)
        End If
    End Sub
End Class