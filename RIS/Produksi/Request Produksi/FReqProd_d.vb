Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FReqProd_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,RD.DivID,D.Nama As Divisi,RD.KompID,K.Nama as Komponen, RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req,KaliQty,RD.KetMdl,RD.stsJasa,RD.stsMentah,RD.BBIDInd,RD.HarSat,RD.HarAkhir,RD.Ket From T_ReqPDtl RD Inner Join M_Div D On RD.DivID=D.DivID Inner Join M_Komp K On RD.KompID=K.KompID Inner Join M_BB B On RD.BBID=B.BBID Inner Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "' and BOMID<>''", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqPDtl")
        Try
            DsMaster.Tables("T_ReqPDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPDtl")

        DsMaster.Tables("T_ReqPDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPDtl").Columns("BOMID"), DsMaster.Tables("T_ReqPDtl").Columns("ArtCode"), DsMaster.Tables("T_ReqPDtl").Columns("DivID"), DsMaster.Tables("T_ReqPDtl").Columns("KompID"), DsMaster.Tables("T_ReqPDtl").Columns("BBID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ReqPDtl"

        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,Ket,Upp,Hancur,Hilang From T_ReqPQty RD Inner Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "T_ReqPQty")
        Try
            DsMaster.Tables("T_ReqPQty").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPQty")

        DsMaster.Tables("T_ReqPQty").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPQty").Columns("BOMID"), DsMaster.Tables("T_ReqPQty").Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_ReqPQty"
    End Sub

    Public Sub FillDtlDModel(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,RD.DivID,D.Nama As Divisi,RD.KompID,K.Nama as Komponen, RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req,KaliQty,RD.KetMdl,RD.stsJasa,RD.stsMentah,RD.BBIDInd,RD.HarSat, RD.HarAkhir,RD.Ket From T_ReqPDtl RD Left Outer Join M_Div D On RD.DivID=D.DivID Left Outer Join M_Komp K On RD.KompID=K.KompID Inner Join M_BB B On RD.BBID=B.BBID Left Outer Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "' and BOMID=''", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqPDtlDModel")
        Try
            DsMaster.Tables("T_ReqPDtlDModel").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPDtlDModel")

        DsMaster.Tables("T_ReqPDtlDModel").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPDtlDModel").Columns("BBID")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "T_ReqPDtlDModel"
    End Sub

    Public Sub FillDtlBOM(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqPIDD,ReqPID,BOMID,RD.ArtCode,ArtName,RD.Uk,Qty,RD.DivID,D.Nama As Divisi,RD.KompID,K.Nama as Komponen, RD.BBID as BBID,B.Nama as Bahan,UkBB,RD.Sat,Std,Req,KaliQty,RD.KetMdl,RD.stsJasa,RD.stsMentah,RD.BBIDInd,RD.HarSat, RD.HarAkhir,RD.Ket From T_ReqPDtl RD Left Outer Join M_Div D On RD.DivID=D.DivID Left Outer Join M_Komp K On RD.KompID=K.KompID Inner Join M_BB B On RD.BBID=B.BBID Left Outer Join M_Brg Br On RD.ArtCode=Br.ArtCode Where ReqPID='" & Kode & "' and BOMID<>'' and RD.ArtCode=''", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqPDtlDModel")
        Try
            DsMaster.Tables("T_ReqPDtlDModel").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_ReqPDtlDModel")

        DsMaster.Tables("T_ReqPDtlDModel").PrimaryKey = New DataColumn() {DsMaster.Tables("T_ReqPDtlDModel").Columns("BOMID"), DsMaster.Tables("T_ReqPDtlDModel").Columns("BBID")}

        Me.GridControl5.DataSource = DsMaster
        Me.GridControl5.DataMember = "T_ReqPDtlDModel"
    End Sub

    Public Sub New(ByVal Kode As String, Dokumen As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        If Dokumen = "Diluar Model" Then
            Me.XTPDModel.PageVisible = True
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = False

            FillDtlDModel(Kode)

        ElseIf Dokumen = "Model" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = True
            Me.XTPBOM.PageVisible = False

            FillDtl(Kode)

        ElseIf Dokumen = "BOM" Then
            Me.XTPDModel.PageVisible = False
            Me.XTPModel.PageVisible = False
            Me.XTPBOM.PageVisible = True

            FillDtlBOM(Kode)

        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'decreases opacity in turms of timer interval 
        Me.Opacity -= 0.03
        'when opacity is zero the form is invisible and we dispose it
        If Me.Opacity = 0 Then
            Me.Dispose()
        End If
    End Sub

    Private Sub FBPB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView3_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView3.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView4.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView5_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView5.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class