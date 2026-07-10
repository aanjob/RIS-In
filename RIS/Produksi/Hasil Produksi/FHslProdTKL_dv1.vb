Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FHslProdTKL_dv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Tgl As Date, Unit As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HPIDD,Tanggal,Unit,Line,Style,Hasil,TKL,Jam From T_HProdKet Where Tanggal='" & Tgl & "' and Unit='" & Unit & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdKet")
        Try
            DsMaster.Tables("T_HProdKet").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HProdKet")

        DsMaster.Tables("T_HProdKet").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdKet").Columns("Tanggal"), DsMaster.Tables("T_HProdKet").Columns("Unit"), DsMaster.Tables("T_HProdKet").Columns("Line")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_HProdKet"

        cmsl = New SqlDataAdapter("Select HPIDD,Tanggal,Unit,H.KatID,K.Kategori,H.GaJam,H.OTN,H.OTL,JnsJam,CuttUppJam,CuttUppOrg,CuttUppUM, CuttBottJam,CuttBottOrg,CuttBottUM,SeriJam,SeriOrg,SeriUM,SabUppJam,SabUppOrg,SabUppUM,SabInsJam,SabInsOrg,SabInsUM,JhtKompJam, JhtKompOrg,JhtKompUM,JhtUppJam,JhtUppOrg,JhtUppUM,FinishUppJam,FinishUppOrg,FinishUppUM,InsockJam,InsockOrg,InsockUM,InsoleJam,InsoleOrg, InsoleUM,OutsoleJam,OutsoleOrg,OutsoleUM, InserttJam,InserttOrg,InserttUM,InjectJam,InjectOrg,InjectUM,AssJam,AssOrg,AssUM,FinishJam, FinishOrg,FinishUM,PackJam,PackOrg,PackUM,PhylonJam,PhylonOrg,PhylonUM From T_HProdTKL H Inner Join M_KatTKL K On H.KatID=K.KatID Where Tanggal='" & Tgl & "' and Unit='" & Unit & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_HProdTKL")
        Try
            DsMaster.Tables("T_HProdTKL").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_HProdTKL")

        DsMaster.Tables("T_HProdTKL").PrimaryKey = New DataColumn() {DsMaster.Tables("T_HProdTKL").Columns("Unit"), DsMaster.Tables("T_HProdTKL").Columns("KatID"), DsMaster.Tables("T_HProdTKL").Columns("JnsJam"), DsMaster.Tables("T_HProdTKL").Columns("CuttUppUM"), DsMaster.Tables("T_HProdTKL").Columns("CuttBottUM"), DsMaster.Tables("T_HProdTKL").Columns("SeriUM"), DsMaster.Tables("T_HProdTKL").Columns("SabUppUM"), DsMaster.Tables("T_HProdTKL").Columns("SabInsUM"), DsMaster.Tables("T_HProdTKL").Columns("JhtKompUM"), DsMaster.Tables("T_HProdTKL").Columns("JhtUppUM"), DsMaster.Tables("T_HProdTKL").Columns("FinishUppUM"), DsMaster.Tables("T_HProdTKL").Columns("InsockUM"), DsMaster.Tables("T_HProdTKL").Columns("InsoleUM"), DsMaster.Tables("T_HProdTKL").Columns("OutsoleUM"), DsMaster.Tables("T_HProdTKL").Columns("InserttUM"), DsMaster.Tables("T_HProdTKL").Columns("InjectUM"), DsMaster.Tables("T_HProdTKL").Columns("AssUM"), DsMaster.Tables("T_HProdTKL").Columns("FinishUM")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_HProdTKL"
    End Sub

    Public Sub New(Tgl As Date, Unit As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FillDtl(Tgl, Unit)
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

    Private Sub FHslProdTKL_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            'DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub

End Class