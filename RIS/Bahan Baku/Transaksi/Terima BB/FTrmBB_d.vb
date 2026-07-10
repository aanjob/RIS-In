Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FTrmBB_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim InisialBC As String = ""
    Dim Manual, MnlInsUpd As Boolean

    Public Sub FillDtl(Kode As String)
        Try
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select TrmIDD,TrmID,POIDD,BOMID,'" & InisialBC & "'+D.BBID as BBID,B.Nama as Bahan,BtNum,D.Sat,Qty,QtyPL,QtyAct, QtyRj,HarSat,HarSbDisc,DiscRpSat,DiscRp,DiscP,RpDiscP,HarAkhir,HarSatDPP,HarBahan From T_TrmBBDtl D Inner Join M_BB B On D.BBID=B.BBID Where TrmID='" & Kode & "'", koneksi)

            cmsl.TableMappings.Add("Table", "T_TrmBBDtl")
            cmsl.Fill(DsMaster, "T_TrmBBDtl")
            DsMaster.Tables("T_TrmBBDtl").Clear()
            cmsl.Fill(DsMaster, "T_TrmBBDtl")

            If Manual = True Then
                DsMaster.Tables("T_TrmBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBBDtl").Columns("POIDD"), DsMaster.Tables("T_TrmBBDtl").Columns("BBID"), DsMaster.Tables("T_TrmBBDtl").Columns("BtNum")}
            Else
                DsMaster.Tables("T_TrmBBDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_TrmBBDtl").Columns("BOMID"), DsMaster.Tables("T_TrmBBDtl").Columns("BBID"), DsMaster.Tables("T_TrmBBDtl").Columns("BtNum")}
            End If

            Me.GridControl1.DataSource = DsMaster
            Me.GridControl1.DataMember = "T_TrmBBDtl"

        Catch ex As Exception
            XtraMessageBox.Show("Data Error Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Public Sub New(ByVal Kode As String, GdID As String, Manuall As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Manual = Manuall

        If Manuall = True Then
            Dim koneksi As New SqlConnection(GlobalKoneksi)

            Dim command As New SqlCommand("Select InisialBC From M_Gudang Where GdId ='" & GdID & "'", koneksi)

            With koneksi
                .Open()
                InisialBC = command.ExecuteScalar()
                .Close()
            End With
        End If

        FillDtl(Kode)

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

    Private Sub FTrmBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()

        If Manual = True Then
            Me.GridColumn3.Visible = False
        Else
            Me.GridColumn3.Visible = True
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class