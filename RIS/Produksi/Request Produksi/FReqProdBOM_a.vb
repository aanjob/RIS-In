Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FReqProdBOM_a
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim CekAll As Boolean
    Dim Dok, Jns As String
    Dim Tgl As Date

    Public Sub New(JnsBon As String, Tanggal As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.GridView1.Focus()
        Dim cmsl As SqlDataAdapter

        Tgl = Tanggal
        Jns = JnsBon
        DsAddDt = New System.Data.DataSet

        cmsl = New SqlDataAdapter("Select Distinct convert(bit,'FALSE') as Cek,D.BOMID, H.ArtName,D.BBID,B.Nama As Bahan,UkBB,D.Sat,(Select Isnull((Select Top 1 Round((HargaBeli*NilTukarRp)+HargaBahan,2) From M_BBHarga Where BBID=B.BBID and Tanggal<='" & Tgl & "' Order By Tanggal desc),0)) As HarSat From T_BOM H Inner Join T_BOMDtl D On H.BOMID=D.BOMID Inner Join M_BB B On D.BBID=B.BBID Where B.Aktif='True' and H.stsApp='True' and H.stsLunas='False' and H.stsBatal='False' and H.stsBtlProd='False' Order By Bahan", koneksi)

        cmsl.TableMappings.Add("Table", "DokPO")
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsAddDt, "DokPO")

        Me.GridControl1.DataSource = DsAddDt
        Me.GridControl1.DataMember = "DokPO"

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub BFinish_Click(sender As Object, e As EventArgs) Handles BFinish.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()

        Dim x As Integer = 0

        dataTrans = New Collection
        dataTrans.Clear()

        Dim i : For i = 0 To GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(i, "Cek") = True Then
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BOMID"), "BOMID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "BBID"), "BBID" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Bahan"), "Bahan" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "UkBB"), "UkBB" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "Sat"), "Sat" & x)
                dataTrans.Add(Me.GridView1.GetRowCellValue(i, "HarSat"), "HarSat" & x)

                x += 1
            End If
        Next
        dataTrans.Add(x, "Baris")

        Timer1.Enabled = True
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If CekAll Then
            CekAll = False
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 0)
            Next
        Else
            CekAll = True
            For i As Integer = 0 To Me.GridView1.RowCount - 1
                Me.GridView1.SetRowCellValue(i, "Cek", 1)
            Next
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.05
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub


End Class