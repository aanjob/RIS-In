Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FOutsBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""
    Dim DsLapF As New System.Data.DataSet

    Public Sub New(ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("SPLOutsBOM", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@BOMID", SqlDbType.VarChar).Value = Kode
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.TableMappings.Add("Table", "SPLOutsBOM")
        Try
            DsLapF.Tables("SPLOutsBOM").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLapF, "SPLOutsBOM")

        Me.GridControl1.DataSource = DsLapF
        Me.GridControl1.DataMember = "SPLOutsBOM"


        cmsl = New SqlDataAdapter("Select P.ArtCode,B.ArtName,Warna,Uk,Sum(Qty+QtyPol) As Qty,Sum(P.BtlOrder-P.Upp-P.Hancur-P.Hilang-LunasMan) As Batal,(Select Isnull((Select Sum(Psg) From T_BSTBDtl Where BOMID=P.BOMID and ArtCode=P.ArtCode),0)) As BSTB,Sum(Qty+QtyPol-P.BtlOrder-P.Upp-P.Hancur-P.Hilang-LunasMan)-(Select Isnull((Select Sum(Psg) From T_BSTBDtl Where BOMID=P.BOMID and ArtCode=P.ArtCode),0)) As Sisa From T_BOM H Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner Join M_Brg B On P.ArtCodeInd=B.ArtCode Where P.ArtCodeInd=P.ArtCode and H.BOMID='" & Kode & "' Group By P.BOMID,P.ArtCode,B.ArtName,Warna,Uk Union All Select P.ArtCode,B.ArtName,Warna,Uk,Sum(Qty+QtyPol) As Qty, Sum(P.BtlOrder-P.Upp-P.Hancur-P.Hilang-LunasMan) As Batal, (Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=P.BOMID and ArtCode=P.ArtCodeInd),0))*IsiDlmDos As BSTB,Sum(Qty+QtyPol-P.BtlOrder-P.Upp-P.Hancur-P.Hilang-LunasMan)-(Select Isnull((Select Sum(Qty) From T_BSTBDtl Where BOMID=P.BOMID and ArtCode=P.ArtCodeInd),0))*IsiDlmDos As Sisa From T_BOM H Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner Join M_Brg B On P.ArtCodeInd=B.ArtCode Where P.ArtCodeInd<>P.ArtCode and H.BOMID='" & Kode & "' Group By P.BOMID,P.ArtCode,P.ArtCodeInd,B.ArtName,Warna,P.IsiDlmDos,Uk", koneksi)

        cmsl.TableMappings.Add("Table", "OutsDet")
        Try
            DsMaster.Tables("OutsDet").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "OutsDet")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "OutsDet"


        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FOutsBOM_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.03
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class