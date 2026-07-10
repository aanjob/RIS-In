Imports DevExpress.XtraEditors
Imports System.Data.SqlClient

Public Class FModel_d
    Dim koneksi As New SqlConnection(GlobalKoneksi)

    Public Sub FillDtl(Kode As String)
        DsMaster = New System.Data.DataSet

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From M_ModelDtl Where MdlID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Ass)),0) From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where MdlID ='" & Kode & "' and Ass Like '%x%'", koneksi)

        With koneksi
            .Open()
            assx = command.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select * From (Select Distinct ArtCode,Uk From M_ModelDtl Where MdlID='" & Kode & "') as x Order By Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select * From (Select Distinct ArtCode,Uk From M_ModelDtl Where MdlID='" & Kode & "') as x Order By Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select * From (Select Distinct ArtCode,Uk From M_ModelDtl Where MdlID='" & Kode & "') as x Order By Cast(Uk as Decimal(18,2))", koneksi)
            End If
        End If

        cmsl.TableMappings.Add("Table", "M_BrgUk")
        Try
            DsMaster.Tables("M_BrgUk").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_BrgUk")

        cmsl = New SqlDataAdapter("Select * From M_ModelDtl Where MdlID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ModelDtlTemp2")
        Try
            DsMaster.Tables("M_ModelDtlTemp2").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ModelDtlTemp2")

        cmsl = New SqlDataAdapter("Select Distinct 0 as stsCopy, MdlID,SpecIDD,M.DivID,D.Nama as Divisi, M.KompID, K.Nama As Komponen, M.BBID, B.Nama As Bahan, B.Uk As UkBB, M.Sat, M.Ket, stsAuto, KaliQty, SPK,(Select Count(*) From T_BOM H1 Inner Join T_BOMDtl D1 On H1.BOMID=D1.BOMID Where H1.MdlID= M.MdlID and DivID=M.DivID and KompID=M.KompID and BBID=M.BBID) As BOM,D.Urut,K.Urut,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "' Order By D.Urut,K.Urut,B.Nama,BBIDInd Asc", koneksi)
        cmsl.SelectCommand.CommandTimeout = 70000

        cmsl.TableMappings.Add("Table", "M_ModelDtl")
        Try
            DsMaster.Tables("M_ModelDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ModelDtl")

        cmsl = New SqlDataAdapter("Select MdlID,SpecIDD, ArtCode, M.DivID,D.Nama as Divisi, M.KompID, K.Nama As Komponen,M.BBID, B.Nama As Bahan, B.Uk As UkBB, M.Sat, Std, M.Ket, stsAuto, KaliQty, SPK,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ModelDtlTemp")
        Try
            DsMaster.Tables("M_ModelDtlTemp").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_ModelDtlTemp")

        Dim table2 = DsMaster.Tables("M_ModelDtl")
        table2.PrimaryKey = New DataColumn() {table2.Columns("DivID"), table2.Columns("KompID"), table2.Columns("BBID"), table2.Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_ModelDtl"


        Dim y : For y = 0 To DsMaster.Tables("M_BrgUk").Rows.Count - 1
            DsMaster.Tables("M_ModelDtl").Columns.Add(DsMaster.Tables("M_BrgUk").Rows(y).Item("ArtCode"), GetType(Decimal)).Caption = DsMaster.Tables("M_BrgUk").Rows(y).Item("Uk")

            Me.GridView1.PopulateColumns(DsMaster.Tables("M_ModelDtl"))

            Dim x : For x = 0 To DsMaster.Tables("M_ModelDtl").Rows.Count - 1
                Dim z : For z = 0 To DsMaster.Tables("M_ModelDtlTemp").Rows.Count - 1

                    If DsMaster.Tables("M_ModelDtl").Rows(x).Item("DivID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("DivID") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("KompID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("KompID") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBID") And DsMaster.Tables("M_BrgUk").Rows(y).Item("ArtCode") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("ArtCode") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBIDInd") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBIDInd") Then

                        Me.GridView1.SetRowCellValue(x, DsMaster.Tables("M_BrgUk").Rows(y).Item("ArtCode"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Std"))
                    End If
                Next
            Next
        Next

        Try
            Me.GridView1.Columns("MdlID").Visible = False
            Me.GridView1.Columns("SpecIDD").Visible = False
            Me.GridView1.Columns("BOM").Visible = False
            Me.GridView1.Columns("stsCopy").Visible = False
            Me.GridView1.Columns("Urut").Visible = False
            Me.GridView1.Columns("Urut1").Visible = False
            Me.GridView1.Columns("BBIDInd").Visible = False

            Me.GridView1.Columns("DivID").Width = 60
            Me.GridView1.Columns("KompID").Width = 60
            Me.GridView1.Columns("Divisi").Width = 100
            Me.GridView1.Columns("Komponen").Width = 100
            Me.GridView1.Columns("BBID").Width = 100
            Me.GridView1.Columns("Bahan").Width = 200
            Me.GridView1.Columns("UkBB").Width = 100
            Me.GridView1.Columns("Sat").Width = 75
            Me.GridView1.Columns("Ket").Width = 150
            Me.GridView1.Columns("KaliQty").Width = 54
            Me.GridView1.Columns("SPK").Width = 40
            Me.GridView1.Columns("stsAuto").Width = 32

            Me.GridView1.Columns("Bahan").Caption = "Deskripsi Bahan"
            Me.GridView1.Columns("DivID").Caption = "Divisi ID"
            Me.GridView1.Columns("KompID").Caption = "Komponen ID"
            Me.GridView1.Columns("BBID").Caption = "Bahan ID"
            Me.GridView1.Columns("stsAuto").Caption = "Auto"
            Me.GridView1.Columns("UkBB").Caption = "Ukuran"

            If DsMaster.Tables("M_ModelDtl").Rows.Count - 1 >= 0 Then
                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD")) Then
                    If GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD") > 0 Then
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                    Else
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub New(ByVal Kode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Private Sub FModel_d_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.GridView1.Focus()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            DsMaster = New System.Data.DataSet
            Timer1.Enabled = True
        End If
    End Sub
End Class