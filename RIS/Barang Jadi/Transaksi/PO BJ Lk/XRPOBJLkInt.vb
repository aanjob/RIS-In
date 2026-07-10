Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPOBJLkInt
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.ArtCode,ArtName,Qty+QtyPol As Qty,Harga,Uk,IsiDlmDos From T_POBJLkDtl D Inner Join M_Brg B On D.ArtCode=B.ArtCode Where POID ='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_POBJLkDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("ArtName", "ArtName"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Harga", "Harga")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_POBJLkDtl")

        Me.DataMember = "T_POBJLkDtl"
        Me.DataSource = DsLap

        Me.LBKode.Text = "Kode PO : " & Bind.Item("Kode").ToString
        Me.LBKpd.Text = ": " & Bind.Item("Kpd").ToString
        Me.LBKirimKe.Text = ": " & Bind.Item("KrmKe").ToString
        Me.LBKrmKeRF.Text = Bind.Item("KrmKe").ToString
        Me.LBTglKirim.Text = ": " & Format(CDate(Bind.Item("TglKrmAw")), "dd MMMM yyyy") & " s/d " & Format(CDate(Bind.Item("TglKrmAkh")), "dd MMMM yyyy")
        Me.LBHBeli.Text = ": " & String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("HBeli").ToString))
        Me.LBHCBP.Text = ": " & String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("HCBP").ToString))
        Me.LBTOP.Text = ": " & Bind.Item("JT").ToString
        Me.LBKet.Text = Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif
        Me.LBTotDos.Text = String.Format("{0:#,##0;(#,##0);""}", CDec(Bind.Item("TotDos").ToString))
        Me.LBTotPsg.Text = String.Format("{0:#,##0;(#,##0);""}", CDec(Bind.Item("TotPsg").ToString))
        Me.LBTotHarga.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", CDec(Bind.Item("TotBayar").ToString))
        'Me.LBPPIC.Text = Bind.Item("PPIC").ToString
        'Me.LBPembelian.Text = Bind.Item("Pembelian").ToString
        'Me.LBMM.Text = Bind.Item("MM").ToString

        Me.LBUpper.Text = Bind.Item("Upp").ToString
        Me.LBOutsole.Text = Bind.Item("Outs").ToString
        Me.LBVariasi.Text = Bind.Item("Variasi").ToString

        Me.LBStyle.Text = ": " & Bind.Item("StyleID").ToString
        Me.LBArtCodeRF.Text = ": " & Bind.Item("ArtCode").ToString
        Me.LBArtNameRF.Text = ": " & Bind.Item("ArtName").ToString
        Me.LBWarnaRF.Text = ": " & Bind.Item("Upp").ToString & " " & Bind.Item("Outs").ToString & " " & Bind.Item("Variasi").ToString

        Me.LBArtCode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.ArtCode")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.ArtName")})
        Me.LBPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Qty", "{0:n0}")})
        Me.LBHarga.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_POBJLkDtl.Harga", "{0:n2}")})

        Dim i : For i = 0 To DsLap.Tables("T_POBJLkDtl").Rows.Count - 1
            If i + 1 = 1 Then
                Me.LBUk1.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("Uk")
                Me.LBQty1.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("IsiDlmDos")
                Me.LBUk1.Visible = True
                Me.LBQty1.Visible = True
            End If

            If i + 1 = 2 Then
                Me.LBUk2.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("Uk")
                Me.LBQty2.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("IsiDlmDos")
                Me.LBUk2.Visible = True
                Me.LBQty2.Visible = True
            End If

            If i + 1 = 3 Then
                Me.LBUk3.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("Uk")
                Me.LBQty3.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("IsiDlmDos")
                Me.LBUk3.Visible = True
                Me.LBQty3.Visible = True
            End If

            If i + 1 = 4 Then
                Me.LBUk4.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("Uk")
                Me.LBQty4.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("IsiDlmDos")
                Me.LBUk4.Visible = True
                Me.LBQty4.Visible = True
            End If

            If i + 1 = 5 Then
                Me.LBUk5.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("Uk")
                Me.LBQty5.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("IsiDlmDos")
                Me.LBUk5.Visible = True
                Me.LBQty5.Visible = True
            End If

            If i + 1 = 6 Then
                Me.LBUk6.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("Uk")
                Me.LBQty6.Text = DsLap.Tables("T_POBJLkDtl").Rows(i).Item("IsiDlmDos")
                Me.LBUk6.Visible = True
                Me.LBQty6.Visible = True
            End If
        Next

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPOBJLkInt_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

End Class