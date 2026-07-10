Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHslProdJam_d

    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Tgl As Date, Jam As Integer, Unit As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HPIDD,D.Tanggal,D.Jam,D.Unit,stsJasa,D.BOMID,D.MerkID,D.JnsID,D.ArtName,D.Warna,D.TotPsg,CuttUpp,CuttBott,Seri, SabUpp,SabIns,JhtKomp,JhtUpp,FinishUpp,Insock,Insole,Outsole,Insertt,Inject,D.Ass,Finish,Pack,Phylon From T_HProdJamDtl D Left Outer Join T_BOM B On D.BOMID=B.BOMID Where D.Tanggal='" & Tgl & "' and Jam=" & Jam & " and D.Unit='" & Unit & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdJamDtl")
        Try
            DsMaster.Tables("T_HProdJamDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HProdJamDtl")

        DsMaster.Tables("T_HProdJamDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdJamDtl").Columns("Tanggal"), DsMaster.Tables("T_HProdJamDtl").Columns("Jam"), DsMaster.Tables("T_HProdJamDtl").Columns("Unit"), DsMaster.Tables("T_HProdJamDtl").Columns("BOMID")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HProdJamDtl"
    End Sub

    Public Sub CekKolom(Unit As String)
        If Unit = "1" Then
            Me.GridView1.Columns("CuttUpp").VisibleIndex = 5
            Me.GridView1.Columns("CuttBott").VisibleIndex = 6
            Me.GridView1.Columns("Seri").VisibleIndex = 7
            Me.GridView1.Columns("SabUpp").VisibleIndex = 8
            Me.GridView1.Columns("SabIns").VisibleIndex = 9
            Me.GridView1.Columns("JhtKomp").VisibleIndex = 10
            Me.GridView1.Columns("JhtUpp").VisibleIndex = 11
            Me.GridView1.Columns("FinishUpp").VisibleIndex = 12
            Me.GridView1.Columns("Insock").VisibleIndex = 13
            Me.GridView1.Columns("Insole").VisibleIndex = 14
            Me.GridView1.Columns("Outsole").VisibleIndex = 15
            Me.GridView1.Columns("Insertt").VisibleIndex = 16
            Me.GridView1.Columns("Inject").VisibleIndex = 18
            Me.GridView1.Columns("Ass").VisibleIndex = 19
            Me.GridView1.Columns("Finish").VisibleIndex = 20
            Me.GridView1.Columns("Pack").VisibleIndex = 21
            Me.GridView1.Columns("Phylon").VisibleIndex = 17

            Me.GridView1.Columns("CuttUpp").Visible = True
            Me.GridView1.Columns("CuttBott").Visible = True
            Me.GridView1.Columns("Seri").Visible = True
            Me.GridView1.Columns("SabUpp").Visible = True
            Me.GridView1.Columns("SabIns").Visible = True
            Me.GridView1.Columns("JhtKomp").Visible = True
            Me.GridView1.Columns("JhtUpp").Visible = True
            Me.GridView1.Columns("FinishUpp").Visible = False
            Me.GridView1.Columns("Insock").Visible = False
            Me.GridView1.Columns("Insole").Visible = False
            Me.GridView1.Columns("Outsole").Visible = True
            Me.GridView1.Columns("Insertt").Visible = False
            Me.GridView1.Columns("Inject").Visible = False
            Me.GridView1.Columns("Ass").Visible = True
            Me.GridView1.Columns("Finish").Visible = False
            Me.GridView1.Columns("Pack").Visible = True
            Me.GridView1.Columns("Phylon").Visible = False



        ElseIf Unit = "2" Then
            XtraMessageBox.Show("Inputan Untuk Unit Tersebut Belum Tersedia", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Dispose()

        ElseIf Unit = "3" Then
            Me.GridView1.Columns("CuttUpp").VisibleIndex = 5
            Me.GridView1.Columns("CuttBott").VisibleIndex = 6
            Me.GridView1.Columns("Seri").VisibleIndex = 7
            Me.GridView1.Columns("SabUpp").VisibleIndex = 8
            Me.GridView1.Columns("SabIns").VisibleIndex = 9
            Me.GridView1.Columns("JhtKomp").VisibleIndex = 10
            Me.GridView1.Columns("JhtUpp").VisibleIndex = 11
            Me.GridView1.Columns("FinishUpp").VisibleIndex = 12
            Me.GridView1.Columns("Insock").VisibleIndex = 13
            Me.GridView1.Columns("Insole").VisibleIndex = 14
            Me.GridView1.Columns("Outsole").VisibleIndex = 15
            Me.GridView1.Columns("Insertt").VisibleIndex = 16
            Me.GridView1.Columns("Inject").VisibleIndex = 18
            Me.GridView1.Columns("Ass").VisibleIndex = 19
            Me.GridView1.Columns("Finish").VisibleIndex = 20
            Me.GridView1.Columns("Pack").VisibleIndex = 21
            Me.GridView1.Columns("Phylon").VisibleIndex = 17

            Me.GridView1.Columns("CuttUpp").Visible = True
            Me.GridView1.Columns("CuttBott").Visible = True
            Me.GridView1.Columns("Seri").Visible = True
            Me.GridView1.Columns("SabUpp").Visible = False
            Me.GridView1.Columns("SabIns").Visible = False
            Me.GridView1.Columns("JhtKomp").Visible = False
            Me.GridView1.Columns("JhtUpp").Visible = False
            Me.GridView1.Columns("FinishUpp").Visible = True
            Me.GridView1.Columns("Insock").Visible = False
            Me.GridView1.Columns("Insole").Visible = False
            Me.GridView1.Columns("Outsole").Visible = False
            Me.GridView1.Columns("Insertt").Visible = True
            Me.GridView1.Columns("Inject").Visible = True
            Me.GridView1.Columns("Ass").Visible = False
            Me.GridView1.Columns("Finish").Visible = True
            Me.GridView1.Columns("Pack").Visible = True
            Me.GridView1.Columns("Phylon").Visible = True

        End If
    End Sub
    Public Sub New(Tgl As Date, Jam As Integer, Unit As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Tgl, Jam, Unit)
        CekKolom(Unit)
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

    Private Sub FPOBB_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class