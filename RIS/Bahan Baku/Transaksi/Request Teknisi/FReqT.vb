Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Public Class FReqT
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim bind As New Collection

    Public Sub Print(Print As String)
        Dim frm As New FPilihUkuran

        frm = New FPilihUkuran
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        bind = New Collection
        bind.Add(Me.GridView1.GetFocusedDataRow.Item("ReqTID"), "Kode")
        bind.Add(Me.GridView1.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView1.GetFocusedDataRow.Item("Teknisi"), "Teknisi")
        bind.Add(Me.GridView1.GetFocusedDataRow.Item("MesinID"), "MesinID")
        bind.Add(Me.GridView1.GetFocusedDataRow.Item("Mesin"), "Mesin")
        bind.Add(Me.GridView1.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(MainModule.PilihUkuran, "Ukuran")

        Dim XR As New XRReqT
        XR.InitializeData(bind)
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select ReqTID,PeriodID,CodeID,Tanggal,H.UserID,U.Nama As Teknisi,H.MesinID,B.Nama As Mesin,H.Ket,stsPR,H.InsDate, H.InsBy,H.UpdDate,H.UpdBy From T_ReqT H Inner Join M_BB B On H.MesinID=B.BBID Inner Join M_User U On H.UserID=U.UserID Where PeriodID=" & MainModule.periodAktif & "Order By Tanggal,ReqTID asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_ReqT")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "T_ReqT")
        DsMaster.Tables("T_ReqT").Clear()
        cmsl.Fill(DsMaster, "T_ReqT")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_ReqT"
    End Sub

    Private Sub FReqT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("ReqTP"), Boolean)

        FillDt()
    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Print("")
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Try
            Dim frm As New FReqT_d(Me.GridView1.GetFocusedDataRow.Item("ReqTID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class