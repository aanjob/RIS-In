Imports System.Data.SqlClient
Imports System
Imports System.Collections.Generic
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraReports.UI

Public Class XRBrcdBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData()

    End Sub

    Private Sub XRAss_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Me.BeforePrint
        'cmsl = New SqlDataAdapter("Select BBID,Nama From M_BB Where BBID='" & CStr(BBID.Value) & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Barcode", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Barcode", "Barcode"), New System.Data.Common.DataColumnMapping("Uk", "Uk"), New System.Data.Common.DataColumnMapping("Tot", "Tot")})})

        cmsl.Fill(DsBrcd, "Barcode")

        Me.DataMember = "Barcode"
        Me.DataSource = DsBrcd

        Me.LBBarcode.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LQRCode.Barcode")})
        Me.LBNama.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "LQRCode.Nama")})

    End Sub

    Private Sub BCQrCode_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles BCQrCode.BeforePrint
        Me.BCQrCode.Symbology = New QRCodeGenerator()
        Me.BCQrCode.Text = Me.LBBarcode.Text

        CType(Me.BCQrCode.Symbology, QRCodeGenerator).CompactionMode = QRCodeCompactionMode.Byte
        CType(Me.BCQrCode.Symbology, QRCodeGenerator).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H
        CType(Me.BCQrCode.Symbology, QRCodeGenerator).Version = QRCodeVersion.AutoVersion
    End Sub
End Class