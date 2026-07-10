Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports DevExpress.XtraGrid

'Pada saat copy muncul data dari spec yang tidak ada di model yang dicopy dan data tsb tidak muncul stdnya
'Data yang ditarik dari Spec tidak bisa diganti2 hanya bisa dihapus atau diisi stdnya
'Beda range size tidak bisa dicopy (std maupun bahan yang tidak ada tidak muncul)
'Langkah Copy Model: Pilih IDSpec-Proses-Copy

Public Class XXFModel
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim Ukuran As String = ""
    Dim ArtCenter As String = ""
    Dim centermin, centerplus, center As Integer
    Dim stsCopy = False
    Dim arrPar(-1) As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID= 3", koneksi)

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        CodeID = ""
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        CodeID = Reader.Item(1)
                        MnlInsUpd = Reader.Item(2)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("MdlN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("MdlEd"), Boolean)
        Me.BVBTambahRange.Enabled = CType(TcodeCollection.Item("MdlN"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("MdlDel"), Boolean)
        Me.BVBCariAll.Enabled = CType(TcodeCollection.Item("MdlSA"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTMdl_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.SLUSpec.Properties.ReadOnly = True
        Me.TBSize.Properties.ReadOnly = True
        Me.TBRange.Properties.ReadOnly = True
        Me.TBPresentase.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False

        Me.BProses.Enabled = False
        Me.BHitAuto.Enabled = False
        Me.BCopy.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBTambahRange.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrint.Enabled = False
        Me.BVBCariAll.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTMdl_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        Me.SLUSpec.Properties.ReadOnly = False
        Me.TBRange.Properties.ReadOnly = False
        Me.TBSize.Properties.ReadOnly = False
        Me.TBPresentase.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True

        Me.BProses.Enabled = True
        Me.BHitAuto.Enabled = True
        Me.BCopy.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTMdl_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        'PPIC minta 1 Spec bisa ditarik banyak model karena ada size tempuk
        cmsl = New SqlDataAdapter("Select SpecID,StyleID,Brand,ShoeLast,ArtName,Warna,RangeSize,Ket From M_Spec", koneksi)
        'cmsl = New SqlDataAdapter("Select SpecID,StyleID,Brand,ArtName,Warna,RangeSize From M_Spec Where SpecID Not In (Select SpecID From M_Model where MdlID <>'" & Me.TBKode.EditValue & "')", koneksi)
        cmsl.TableMappings.Add("Table", "M_SpecMdl")
        Try
            DsMaster.Tables("M_SpecMdl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_SpecMdl")

        Me.SLUSpec.Properties.DataSource = DsMaster.Tables("M_SpecMdl")
        Me.SLUSpec.Properties.DisplayMember = "SpecID"
        Me.SLUSpec.Properties.ValueMember = "SpecID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim a : For a = 0 To Me.GridView3.RowCount - 1
            Me.GridView1.Columns.Remove(Me.GridView1.Columns(Me.GridView3.GetRowCellValue(a, "ArtCode")))
        Next

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

        DsMaster.Tables("M_BrgUk").PrimaryKey = New DataColumn() {DsMaster.Tables("M_BrgUk").Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "M_BrgUk"

        cmsl = New SqlDataAdapter("Select * From M_ModelDtl Where MdlID='" & Kode & "'", koneksi)
        cmsl.TableMappings.Add("Table", "M_ModelDtlTemp2")
        cmsl.Fill(DsMaster, "M_ModelDtlTemp2")

        'cmsl = New SqlDataAdapter("Select Distinct 0 as stsCopy, MdlID,SpecIDD,M.DivID,D.Nama as Divisi, M.KompID, K.Nama As Komponen, M.BBID, B.Nama As Bahan, B.Uk As UkBB, M.Sat, M.Ket, stsAuto, KaliQty, SPK,(Select Count(*) From T_BOM H1 Inner Join T_BOMDtl D1 On H1.BOMID=D1.BOMID Where H1.MdlID= M.MdlID and DivID=M.DivID and KompID=M.KompID and BBID=M.BBID) As BOM,D.Urut,K.Urut From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "' Order By D.Urut,K.Urut,B.Nama Asc", koneksi)
        cmsl = New SqlDataAdapter("Select Distinct stsCopy, MdlID,SpecIDD,M.DivID,D.Nama as Divisi, M.KompID, K.Nama As Komponen, M.BBID, B.Nama As Bahan, M.UkBB, M.Sat, M.Ket, stsAuto, KaliQty, SPK,D.Urut,K.Urut,M.stsJasa,M.stsMentah,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "' Order By D.Urut,K.Urut,B.Nama,BBIDInd Asc", koneksi)
        cmsl.SelectCommand.CommandTimeout = 70000

        cmsl.TableMappings.Add("Table", "M_ModelDtl")
        cmsl.Fill(DsMaster, "M_ModelDtl")

        cmsl = New SqlDataAdapter("Select MdlID,SpecIDD,ArtCode,M.DivID,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB, M.Sat,Std,M.Ket,stsAuto,KaliQty,SPK,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ModelDtlTemp")
        cmsl.Fill(DsMaster, "M_ModelDtlTemp")

        'Dim table2 = DsMaster.Tables("M_ModelDtl")
        'table2.PrimaryKey = New DataColumn() {table2.Columns("DivID"), table2.Columns("KompID"), table2.Columns("BBID")}
        DsMaster.Tables("M_ModelDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("M_ModelDtl").Columns("DivID"), DsMaster.Tables("M_ModelDtl").Columns("KompID"), DsMaster.Tables("M_ModelDtl").Columns("BBID"), DsMaster.Tables("M_ModelDtl").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_ModelDtl"

        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Dim y : For y = 0 To Me.GridView3.RowCount - 1
            DsMaster.Tables("M_ModelDtl").Columns.Add(Me.GridView3.GetRowCellValue(y, "ArtCode"), GetType(Decimal)).Caption = Me.GridView3.GetRowCellValue(y, "Uk")

            Me.GridView1.PopulateColumns(DsMaster.Tables("M_ModelDtl"))

            Dim x : For x = 0 To DsMaster.Tables("M_ModelDtl").Rows.Count - 1
                Dim z : For z = 0 To DsMaster.Tables("M_ModelDtlTemp").Rows.Count - 1
                    'If DsMaster.Tables("M_ModelDtl").Rows(x).Item("KompID") = "032" And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBID") = "12000005" Then
                    '    MsgBox("Test")
                    'End If
                    If DsMaster.Tables("M_ModelDtl").Rows(x).Item("DivID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("DivID") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("KompID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("KompID") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBID") And Me.GridView3.GetRowCellValue(y, "ArtCode") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("ArtCode") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBIDInd") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBIDInd") Then

                        Me.GridView1.SetRowCellValue(x, Me.GridView3.GetRowCellValue(y, "ArtCode"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Std"))

                        Exit For
                    End If

                Next
            Next
        Next

        Try
            AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

            Me.GridView1.Columns("DivID").ColumnEdit = Me.BEdDivID
            Me.GridView1.Columns("KompID").ColumnEdit = Me.BEdKompID
            Me.GridView1.Columns("BBID").ColumnEdit = Me.BEdBBID

            Me.GridView1.Columns("MdlID").Visible = False
            Me.GridView1.Columns("SpecIDD").Visible = False
            'Me.GridView1.Columns("BOM").Visible = False
            Me.GridView1.Columns("stsCopy").Visible = False
            Me.GridView1.Columns("Urut").Visible = False
            Me.GridView1.Columns("Urut1").Visible = False
            Me.GridView1.Columns("stsJasa").Visible = False
            Me.GridView1.Columns("stsMentah").Visible = False
            Me.GridView1.Columns("BBIDInd").Visible = False

            Me.GridView1.Columns("stsCopy").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Urut").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Urut1").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("MdlID").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Divisi").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Komponen").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Bahan").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("Sat").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("stsJasa").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("stsMentah").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("SPK").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("KaliQty").OptionsColumn.AllowEdit = False
            Me.GridView1.Columns("BBIDInd").OptionsColumn.AllowEdit = False

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
                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD") > 0 Then
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False

                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    Else
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                        If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) And Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa")) Then
                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
                                Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                                Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                                Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                            Else
                                Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                                Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                                Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
                            End If

                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
                                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                            Else
                                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub FillDtlCp(Kode As String)
        Dim cmsl As SqlDataAdapter
        'cmsl = New SqlDataAdapter("Select MdlID,SpecIDD, ArtCode, M.Uk as UkBJ, M.DivID,D.Nama as Divisi, M.KompID, K.Nama As Komponen,M.BBID, B.Nama As Bahan, B.Uk As UkBB, M.Sat, Std, M.Ket, stsAuto, KaliQty, SPK,(Select Count(*) From T_BOM H1 Inner Join T_BOMDtl D1 On H1.BOMID=D1.BOMID Where H1.MdlID= M.MdlID and ArtCode=M.ArtCode and DivID=M.DivID and KompID=M.KompID and BBID=M.BBID) As BOM,D.Urut,K.Urut From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "'", koneksi)
        cmsl = New SqlDataAdapter("Select MdlID,SpecIDD,ArtCode,M.Uk as UkBJ,M.DivID,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,Std,M.Ket,stsAuto,KaliQty,'True' As SPK,D.Urut,K.Urut,M.stsJasa,M.stsMentah,BBIDInd From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_ModelDtlTemp")
        cmsl.Fill(DsMaster, "M_ModelDtlTemp")

        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Dim Masuk As Boolean

        Dim y : For y = 0 To Me.GridView3.RowCount - 1
            Dim z : For z = 0 To DsMaster.Tables("M_ModelDtlTemp").Rows.Count - 1
                Dim x : For x = 0 To DsMaster.Tables("M_ModelDtl").Rows.Count - 1

                    If DsMaster.Tables("M_ModelDtl").Rows(x).Item("DivID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("DivID") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("KompID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("KompID") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBID") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBID") And Me.GridView3.GetRowCellValue(y, "Uk") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("UkBJ") And DsMaster.Tables("M_ModelDtl").Rows(x).Item("BBIDInd") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBIDInd") Then

                        Me.GridView1.SetRowCellValue(x, Me.GridView3.GetRowCellValue(y, "ArtCode"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Std"))
                        Masuk = False
                        Exit For
                    Else
                        Masuk = True
                    End If
                Next

                If Masuk = True Then
                    If Me.GridView3.GetRowCellValue(y, "Uk") = DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("UkBJ") Then
                        DsMaster.Tables("M_ModelDtl").Rows.Add(True, Me.TBKode.EditValue, DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("SpecIDD"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("DivID"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Divisi"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("KompID"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Komponen"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBID"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Bahan"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("UkBB"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Sat"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Ket"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("stsAuto"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("KaliQty"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("SPK"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Urut"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Urut1"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("stsJasa"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("stsMentah"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("BBIDInd"), DsMaster.Tables("M_ModelDtlTemp").Rows(z).Item("Std"))
                    End If
                End If
            Next
        Next

        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Me.GridView1.Columns("DivID").ColumnEdit = Me.BEdDivID
        Me.GridView1.Columns("KompID").ColumnEdit = Me.BEdKompID
        Me.GridView1.Columns("BBID").ColumnEdit = Me.BEdBBID

        Me.GridView1.Columns("MdlID").Visible = False
        Me.GridView1.Columns("SpecIDD").Visible = False
        'Me.GridView1.Columns("BOM").Visible = False
        Me.GridView1.Columns("stsCopy").Visible = False
        Me.GridView1.Columns("Urut").Visible = False
        Me.GridView1.Columns("Urut1").Visible = False
        Me.GridView1.Columns("stsJasa").Visible = False
        Me.GridView1.Columns("stsMentah").Visible = False
        Me.GridView1.Columns("BBIDInd").Visible = False

        Me.GridView1.Columns("stsCopy").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Urut").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Urut1").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("MdlID").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Divisi").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Komponen").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Bahan").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Sat").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("stsJasa").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("stsMentah").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("SPK").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("KaliQty").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("BBIDInd").OptionsColumn.AllowEdit = False

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

        'If DsMaster.Tables("M_ModelDtl").Rows.Count - 1 >= 0 Then
        '    If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD")) Then
        '        If GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD") > 0 Then
        '            Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
        '            Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
        '            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
        '        Else
        '            Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
        '            Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
        '            Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
        '        End If
        '    End If

        'End If

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select MdlID,M.PeriodID,M.CodeID,M.Tanggal,M.SpecID,M.StyleID,S.Brand,S.ShoeLast,M.ArtName,M.Warna,M.RangeSize, M.SampleSize,PersenGenerate,M.Ket,M.InsDate,M.InsBy,M.UpdDate,M.UpdBy From M_Model M Inner Join M_Spec S On M.SpecId=S.SpecID Where M.PeriodID=" & MainModule.periodAktif & "", koneksi)

        cmsl.TableMappings.Add("Table", "M_Model")
        DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_Model")
        'DsMaster.Tables("M_Model").Clear()
        'cmsl.Fill(DsMaster, "M_Model")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_Model"
    End Sub

    Public Sub InsDt()
        koneksi.Close()
        Dim x As Integer

        Dim s : For s = 0 To DsMaster.Tables("M_ModelDtlTemp2").Rows.Count - 1
            Dim cmSPDtl As New SqlCommand("SPInsM_ModelDtl")
            cmSPDtl.CommandType = CommandType.StoredProcedure

            With cmSPDtl
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                .Parameters.Add("@SpecIDD", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("SpecIDD")
                .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("ArtCode")
                .Parameters.Add("@Uk", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("Uk")
                .Parameters.Add("@DivID", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("DivID")
                .Parameters.Add("@KompID", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("KompID")
                .Parameters.Add("@BBID", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("BBID")
                .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("UkBB")
                .Parameters.Add("@Sat", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("Sat")
                .Parameters.Add("@Ket", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("Ket")
                .Parameters.Add("@Auto", SqlDbType.Bit).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("stsAuto")
                .Parameters.Add("@Copy", SqlDbType.Bit).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("stsCopy")
                .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("KaliQty")
                .Parameters.Add("@SPK", SqlDbType.Bit).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("SPK")
                .Parameters.Add("@Std", SqlDbType.Decimal).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("Std")
                .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("stsJasa")
                .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("stsMentah")
                .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = DsMaster.Tables("M_ModelDtlTemp2").Rows(s).Item("BBIDInd")
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi
            End With

            With koneksi
                .Open()
                cmSPDtl.ExecuteNonQuery()
                x = cmSPDtl.Parameters("@Return").Value
                .Close()
            End With

        Next
    End Sub

    Private Sub FModel_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Model"
    End Sub

    Private Sub FModel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FModel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        DsMaster = New System.Data.DataSet
        Me.BVTMdl_e.Selected = True
    End Sub

    Private Sub BVTMdl_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTMdl_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Model"

        FillDt()
        Me.BVBPrint.Enabled = CType(TcodeCollection.Item("MdlP"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Model"

        DsMaster.Clear()

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            'If MainModule.SlOpBJ() > 0 Then
            '    If MainModule.BackDate = False Then
            XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
            '    End If

            '    Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            'End If
        Else
            Me.DTPTanggal.EditValue = Date.Now
        End If

        OpenControl()
        Indicator = "100"
        CekSave = True

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.SLUSpec.EditValue = ""
        Me.TBStyle.EditValue = ""
        Me.TBArtName.EditValue = ""
        Me.TBWarna.EditValue = ""
        Me.TBBrand.EditValue = ""
        Me.TBSL.EditValue = ""
        Me.TBRange.EditValue = ""
        Me.TBSize.EditValue = ""
        Me.TBPresentase.EditValue = 0
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        Me.BHitAuto.Enabled = False

        DsMaster = New System.Data.DataSet

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("M_ModelDtl").Clear()
        DsMaster.Tables("M_ModelDtlTemp").Clear()
        DsMaster.Tables("M_BrgUk").Clear()
        ReDim arrPar(-1)
        LUE()
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Model"

        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("MdlID")
        Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")

        LUE()

        Me.SLUSpec.EditValue = Me.GridView2.GetFocusedDataRow.Item("SpecID")
        Me.TBStyle.EditValue = Me.GridView2.GetFocusedDataRow.Item("StyleID")
        Me.TBArtName.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtName")
        Me.TBWarna.EditValue = Me.GridView2.GetFocusedDataRow.Item("Warna")
        Me.TBBrand.EditValue = Me.GridView2.GetFocusedDataRow.Item("Brand")
        Me.TBSL.EditValue = Me.GridView2.GetFocusedDataRow.Item("ShoeLast")
        Me.TBRange.EditValue = Me.GridView2.GetFocusedDataRow.Item("RangeSize")
        Me.TBSize.EditValue = Me.GridView2.GetFocusedDataRow.Item("SampleSize")
        Me.TBPresentase.EditValue = Me.GridView2.GetFocusedDataRow.Item("PersenGenerate")
        Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

        FillDtl(Me.TBKode.EditValue)
        ReDim arrPar(-1)
        If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
        End If

        OpenControl()
        CekSave = True

        Me.BCopy.Enabled = False
        Me.BHitAuto.Enabled = True
    End Sub


    Private Sub BVBTambahRange_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBTambahRange.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Tambah Range Model"

        Dim frm As New FModel_tr(Me.GridView2.GetFocusedDataRow.Item("ArtName"), Me.GridView2.GetFocusedDataRow.Item("Warna"), Me.GridView2.GetFocusedDataRow.Item("MdlID"))
        frm.ShowDialog()
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Model"

        koneksi.Close()

        If MainModule.SlMdl(Me.GridView2.GetFocusedDataRow.Item("MdlID")) > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ditarik Di BOM", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("MdlID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Model")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("MdlID")
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi

                Try
                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    Else
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If

    End Sub

    Private Sub BVBPrint_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrint.ItemClick
        Dim bind As New Collection

        'Dim cmsl As SqlDataAdapter
        'cmsl = New SqlDataAdapter("Select Distinct ArtCode, Uk From M_ModelDtl Where MdlID='" & Me.GridView2.GetFocusedDataRow.Item("MdlID") & "'", koneksi)

        'cmsl.TableMappings.Add("Table", "M_BrgUk")
        'cmsl.Fill(DsMaster, "M_BrgUk")

        'Dim i : For i = 1 To 12
        '    If i <= DsMaster.Tables("M_BrgUk").Rows.Count Then
        '        Dim x : For x = 0 To DsMaster.Tables("M_BrgUk").Rows.Count - 1
        '            If i = x + 1 Then
        '                bind.Add(DsMaster.Tables("M_BrgUk").Rows(x).Item("Uk"), "Uk" & i)
        '            End If
        '        Next
        '    Else
        '        bind.Add("", "Uk" & i)
        '    End If
        'Next

        Dim frm As New FModel_uk(Me.GridView2.GetFocusedDataRow.Item("MdlID"))

        frm = New FModel_uk(Me.GridView2.GetFocusedDataRow.Item("MdlID"))
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        bind.Add(Me.GridView2.GetFocusedDataRow.Item("MdlID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Brand"), "Brand")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Warna"), "Warna")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")

        Dim i : For i = 1 To 12
            bind.Add(dataTrans.Item(i).ToString, "Uk" & i)
        Next

        Dim XR As New XRModel
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBCariAll_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCariAll.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Model"

        Dim frm As New FModel_sa
        frm.ShowDialog()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()

        If Me.SLUSpec.EditValue = "" Or IsDBNull(Me.SLUSpec.EditValue) Then
            XtraMessageBox.Show("Spec Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        'If MainModule.SlDoubleMdl(Me.SLUSpec.EditValue, Me.TBKode.EditValue) > 0 Then
        '    XtraMessageBox.Show("Spec Sudah Pernah Diinput", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Dim cmSPDel As New SqlCommand("SPDelM_ModelDtl")
        cmSPDel.CommandType = CommandType.StoredProcedure

        With cmSPDel
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 90000

            With koneksi
                .Open()
                cmSPDel.ExecuteNonQuery()
                .Close()
            End With
        End With

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Model")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SpecID", SqlDbType.VarChar).Value = Me.SLUSpec.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.TBStyle.EditValue
                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.TBArtName.EditValue
                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.TBWarna.EditValue
                    .Parameters.Add("@Range", SqlDbType.VarChar).Value = Me.TBRange.EditValue
                    .Parameters.Add("@Sample", SqlDbType.VarChar).Value = Me.TBSize.EditValue
                    .Parameters.Add("@Persen", SqlDbType.Decimal).Value = Me.TBPresentase.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            Dim z : For z = 0 To Me.GridView3.RowCount - 1
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsM_ModelDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@SpecIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SpecIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@Copy", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsCopy")
                                        .Parameters.Add("@Auto", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAuto")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, Me.GridView3.GetRowCellValue(z, "ArtCode"))
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        .Close()
                                    End With
                                End If
                            Next
                        Next

                        If x = 0 Then
                            Try
                                DsMaster.Tables("M_BrgUk").Clear()
                                DsMaster.Tables("M_ModelDtlTemp2").Clear()
                                DsMaster.Tables("M_ModelDtl").Clear()
                                DsMaster.Tables("M_ModelDtlTemp").Clear()

                            Catch ex As Exception

                            End Try

                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBKode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Model")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@SpecID", SqlDbType.VarChar).Value = Me.SLUSpec.EditValue
                    .Parameters.Add("@StyleID", SqlDbType.VarChar).Value = Me.TBStyle.EditValue
                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.TBArtName.EditValue
                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.TBWarna.EditValue
                    .Parameters.Add("@Range", SqlDbType.VarChar).Value = Me.TBRange.EditValue
                    .Parameters.Add("@Sample", SqlDbType.VarChar).Value = Me.TBSize.EditValue
                    .Parameters.Add("@Persen", SqlDbType.Decimal).Value = Me.TBPresentase.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            Dim z : For z = 0 To Me.GridView3.RowCount - 1
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsM_ModelDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@SpecIDD", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "SpecIDD")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@Copy", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsCopy")
                                        .Parameters.Add("@Auto", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAuto")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, Me.GridView3.GetRowCellValue(z, "ArtCode"))
                                        .Parameters.Add("@Return", SqlDbType.Int)
                                        .Parameters("@Return").Direction = ParameterDirection.Output
                                        .Connection = koneksi
                                    End With

                                    With koneksi
                                        .Open()
                                        cmSPDtl.ExecuteNonQuery()
                                        x = cmSPDtl.Parameters("@Return").Value
                                        .Close()
                                    End With

                                    'If x <> 0 Then
                                    '    MsgBox("Error")
                                    'End If
                                End If
                            Next
                        Next

                        If x = 0 Then
                            Try
                                DsMaster.Tables("M_BrgUk").Clear()
                                DsMaster.Tables("M_ModelDtlTemp2").Clear()
                                DsMaster.Tables("M_ModelDtl").Clear()
                                DsMaster.Tables("M_ModelDtlTemp").Clear()

                            Catch ex As Exception

                            End Try

                            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            InsDt()
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        InsDt()
                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
        End Select

        'LockControl()
        'CekSave = False

        Me.Dispose()

        Dim frm As New FModel
        frm.MdiParent = FUtama
        frm.Show()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False

        Try
            DsMaster.Tables("M_BrgUk").Clear()
            DsMaster.Tables("M_ModelDtlTemp2").Clear()
            DsMaster.Tables("M_ModelDtl").Clear()
            DsMaster.Tables("M_ModelDtlTemp").Clear()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        RemoveHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

        Me.GridView1.SetRowCellValue(e.RowHandle, "MdlID", "--")
        Me.GridView1.SetRowCellValue(e.RowHandle, "SpecIDD", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "DivID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "KompID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")
        'Me.GridView1.SetRowCellValue(e.RowHandle, "BOM", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsCopy", False)
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsAuto", True)
        Me.GridView1.SetRowCellValue(e.RowHandle, "KaliQty", False)
        Me.GridView1.SetRowCellValue(e.RowHandle, "SPK", True)
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsJasa", False)
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsMentah", False)
        Me.GridView1.SetRowCellValue(e.RowHandle, "BBIDInd", "")


        Dim i : For i = 0 To Me.GridView3.RowCount - 1
            Me.GridView1.SetRowCellValue(e.RowHandle, Me.GridView3.GetRowCellValue(i, "ArtCode"), 0)
        Next

        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

        'Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True

        AddHandler GridView1.CellValueChanged, AddressOf GridView1_CellValueChanged

    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        If Me.GridView1.Editable = True Then
            If DsMaster.Tables("M_ModelDtl").Rows.Count - 1 >= 0 Then
                If Not IsDBNull(GridView1.GetRowCellValue(e.FocusedRowHandle, "SpecIDD")) Then
                    If GridView1.GetRowCellValue(e.FocusedRowHandle, "SpecIDD") > 0 Then
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
                    Else
                        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                        If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) And Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa")) Then
                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
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

                    If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) Then

                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                        Else
                            Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                        End If
                    End If
                End If



                'Cek Model Seperti KHI

                'Try
                '    If GridView1.GetRowCellValue(e.FocusedRowHandle, "BOM") > 0 Then
                '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                '    Else
                '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                '    End If
                'Catch ex As Exception

                'End Try
            End If

        End If
    End Sub

    Private Sub GridView1_RowCountChanged(sender As Object, e As EventArgs) Handles GridView1.RowCountChanged

        If DsMaster.Tables("M_ModelDtl").Rows.Count - 1 >= 0 Then
            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD")) Then
                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "SpecIDD") > 0 Then
                    Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
                    Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False

                    Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                Else
                    Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
                    Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True

                    If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) And Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa")) Then
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa") = True Then
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

                If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah")) Then

                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah") = True Then
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    Else
                        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
                    End If
                End If

            End If

            'Cek Model Seperti KHI

            'Try
            '    If GridView1.GetRowCellValue(e.FocusedRowHandle, "BOM") > 0 Then
            '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
            '    Else
            '        Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
            '    End If
            'Catch ex As Exception

            'End Try
        End If
    End Sub

    Private Sub BEdDivID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdDivID.ButtonClick
        Dim frm As New FSearch("Divisi", 1, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdKompID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdKompID.ButtonClick
        Dim frm As New FSearch("Komponen", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim BBIDLama As String
    Dim DivHapus, KompHapus, BBIDIndHapus As String

    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Dim frm As New FSearch("M_BB", "", "Bahan", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", dataTrans.Item("Kode").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "UkBB", dataTrans.Item("Uk").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)

                DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
                KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
                KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
                BBIDIndHapus = Me.GridView1.ActiveEditor.EditValue

                If MainModule.AutoBM = True Then
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", dataTrans.Item("stsJasa").ToString)

                    If CBool(dataTrans.Item("stsJasa").ToString) = True Then

                        Dim row As Integer
                        Dim DivID As String = ""
                        Dim Div As String = ""
                        Dim KompID As String = ""
                        Dim Komp As String = ""
                        Dim BBID As String = ""

                        DivID = Me.GridView1.GetFocusedDataRow.Item("DivID")
                        Div = Me.GridView1.GetFocusedDataRow.Item("Divisi")
                        KompID = Me.GridView1.GetFocusedDataRow.Item("KompID")
                        Komp = Me.GridView1.GetFocusedDataRow.Item("Komponen")
                        BBID = Me.GridView1.ActiveEditor.EditValue

                        Dim Reader As SqlClient.SqlDataReader
                        Dim command As New SqlCommand("Select BBIDM,Nama,Uk,Wrn,Sat,stsJasa From M_BBMentah M Inner Join M_BB B On M.BBIDM=B.BBID Where M.BBID='" & BBID & "'", koneksi)

                        With koneksi
                            .Open()
                            Reader = command.ExecuteReader

                            If Reader.HasRows Then
                                While Reader.Read
                                    If IsDBNull(Reader.Item(0)) = True Then

                                    Else
                                        Me.GridView1.AddNewRow()
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "DivID", DivID)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", Div)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "KompID", KompID)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", Komp)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBID", Reader.Item(0))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", Reader.Item(1))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "UkBB", Reader.Item(2))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Wrn", Reader.Item(3))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", Reader.Item(4))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", Reader.Item(5))
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsMentah", True)
                                        Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "BBIDInd", BBID)
                                    End If
                                End While
                            End If
                            Reader.Close()
                            .Close()
                        End With

                    End If
                Else
                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "stsJasa", False)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim Hapus As Boolean
    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            Me.GridView1.ActiveFilter.Clear()
            Me.GridView3.ActiveFilter.Clear()

            DivHapus = Me.GridView1.GetFocusedDataRow.Item("DivID")
            KompHapus = Me.GridView1.GetFocusedDataRow.Item("KompID")
            BBIDIndHapus = Me.GridView1.GetFocusedDataRow.Item("BBID")

            If Me.GridView1.GetFocusedDataRow.Item("stsJasa") = True Then
                Hapus = True
            Else
                Hapus = False
            End If
        End If
    End Sub
    Private Sub GridView1_RowDeleted(sender As Object, e As DevExpress.Data.RowDeletedEventArgs) Handles GridView1.RowDeleted
        If Hapus = True Then
            Dim i : For i = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(i, "BBIDInd") <> "" Then
                    If Me.GridView1.GetRowCellValue(i, "DivID") = DivHapus And Me.GridView1.GetRowCellValue(i, "KompID") = KompHapus And Me.GridView1.GetRowCellValue(i, "BBIDInd") = BBIDIndHapus Then

                        Me.GridView1.DeleteRow(i)

                    End If
                End If
            Next
        End If
    End Sub
    Private Sub GridView1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        Ukuran = ""
        If Ukuran <> Me.TBSize.EditValue Then
            Dim i : For i = 0 To Me.GridView3.RowCount - 1
                If Me.GridView3.GetRowCellValue(i, "Uk") = Me.TBSize.EditValue Then
                    Ukuran = Me.GridView3.GetRowCellValue(i, "Uk")
                    ArtCenter = Me.GridView3.GetRowCellValue(i, "ArtCode")
                    centermin = i - 1
                    centerplus = i + 1
                    Exit For
                End If
            Next
        End If

        If e.Column Is GridView1.Columns(ArtCenter) Then
            Dim x : For x = centermin To 0 Step -1
                If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(x + 1, "ArtCode"))) Then
                    If Me.GridView1.GetFocusedRowCellValue("stsAuto") = True Then
                        Dim val As Decimal

                        If Me.GridView1.GetFocusedRowCellValue("KaliQty") = True Then
                            val = Math.Round(Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(x + 1, "ArtCode")) - (Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(x + 1, "ArtCode")) * Me.TBPresentase.EditValue / 100), 4, MidpointRounding.AwayFromZero)
                        Else
                            val = Math.Round(Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(x + 1, "ArtCode")) * ((100 + Me.TBPresentase.EditValue) / 100), 4, MidpointRounding.AwayFromZero)
                        End If

                        Me.GridView1.SetRowCellValue(e.RowHandle, Me.GridView3.GetRowCellValue(x, "ArtCode"), val)
                    End If
                End If
            Next

            Dim y : For y = centerplus To Me.GridView3.RowCount - 1
                If Not IsDBNull(Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(y - 1, "ArtCode"))) Then
                    If Me.GridView1.GetFocusedRowCellValue("stsAuto") = True Then
                        Dim val As Decimal
                        If Me.GridView1.GetFocusedRowCellValue("KaliQty") = True Then
                            val = Math.Round(Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(y - 1, "ArtCode")) + (Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(y - 1, "ArtCode")) * Me.TBPresentase.EditValue / 100), 4, MidpointRounding.AwayFromZero)
                        Else
                            val = Math.Round(Me.GridView1.GetFocusedRowCellValue(Me.GridView3.GetRowCellValue(y - 1, "ArtCode")) * ((100 - Me.TBPresentase.EditValue) / 100), 4, MidpointRounding.AwayFromZero)
                        End If

                        Me.GridView1.SetRowCellValue(e.RowHandle, Me.GridView3.GetRowCellValue(y, "ArtCode"), val)
                    End If
                End If
            Next
        End If

    End Sub

    Private Sub SLUSpec_Leave(sender As Object, e As EventArgs) Handles SLUSpec.Leave
        If Not IsDBNull(Me.SLUSpec.EditValue) And Me.SLUSpec.Properties.ReadOnly = False Then
            Try
                Me.TBStyle.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("StyleID")
                Me.TBBrand.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("Brand")
                Me.TBArtName.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("ArtName")
                Me.TBWarna.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("Warna")
                Me.TBRange.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("RangeSize")
                Me.TBSL.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("ShoeLast")
                Me.MKet.EditValue = DsMaster.Tables("M_SpecMdl").Select("SpecID = '" & Me.SLUSpec.EditValue & "'")(0).Item("Ket")

                DsMaster.Tables("M_BrgUk").Clear()
                Dim cmsl As SqlDataAdapter
                Dim jml, assx As Integer

                koneksi.Close()

                Dim command As New SqlCommand("Select Isnull(Max(Len(Ass)),0) From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Me.TBArtName.EditValue & "' And W.Nama='" & Me.TBWarna.EditValue & "' And SatID In ('P','PCS')", koneksi)

                With koneksi
                    .Open()
                    jml = command.ExecuteScalar()
                    .Close()
                End With

                Dim command2 As New SqlCommand("Select Isnull(Max(Len(Ass)),0) From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Me.TBArtName.EditValue & "' And W.Nama='" & Me.TBWarna.EditValue & "' And SatID In ('P','PCS') and Ass Like '%x%'", koneksi)

                With koneksi
                    .Open()
                    assx = command.ExecuteScalar()
                    .Close()
                End With


                If jml > 4 Then
                    cmsl = New SqlDataAdapter("Select ArtCode,Ass As Uk From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Me.TBArtName.EditValue & "' And W.Nama='" & Me.TBWarna.EditValue & "' And SatID In ('P','PCS') Order By Uk", koneksi)
                Else
                    If assx > 0 Then
                        cmsl = New SqlDataAdapter("Select ArtCode,Ass As Uk From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Me.TBArtName.EditValue & "' And W.Nama='" & Me.TBWarna.EditValue & "' And SatID In ('P','PCS') Order By Uk", koneksi)
                    Else
                        cmsl = New SqlDataAdapter("Select ArtCode,Ass As Uk From M_Brg B Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where ArtName='" & Me.TBArtName.EditValue & "' And W.Nama='" & Me.TBWarna.EditValue & "' And SatID In ('P','PCS') Order By Cast(Ass as Decimal(18,2))", koneksi)
                    End If

                End If

                cmsl.TableMappings.Add("Table", "M_BrgUk")
                cmsl.Fill(DsMaster, "M_BrgUk")

                Me.GridControl3.DataSource = DsMaster
                Me.GridControl3.DataMember = "M_BrgUk"

                If DsMaster.Tables("M_BrgUk").Rows.Count = 0 Then
                    XtraMessageBox.Show("Data Bermasalah Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    LockControl()
                End If

                Select Case Indicator
                    Case 100
                        DsMaster.Tables("M_ModelDtl").Clear()
                        cmsl = New SqlDataAdapter("Select 'False' As stsCopy, '--' As MdlID,SpecIDD,SD.DivID,D.Nama as Divisi, SD.KompID, K.Nama As Komponen, SD.BBID, B.Nama As Bahan, B.Uk As UkBB, SD.Sat, SD.Ket, 'True' As stsAuto, 'False' As KaliQty,'True' As SPK,SD.stsJasa,SD.stsMentah,SD.BBIDInd From M_SpecDtl SD Inner Join M_Div D On SD.DivID=D.DivID Inner Join M_Komp K On SD.KompID=K.KompID Inner Join M_BB B On B.BBID=SD.BBID Where SpecID='" & Me.SLUSpec.EditValue & "'", koneksi)
                        cmsl.Fill(DsMaster, "M_ModelDtl")

                    Case 200
                        cmsl = New SqlDataAdapter("Select 'False' As stsCopy, '--' As MdlID,SpecIDD,SD.DivID,D.Nama as Divisi, SD.KompID, K.Nama As Komponen, SD.BBID, B.Nama As Bahan, B.Uk As UkBB, SD.Sat, SD.Ket, 'True' As stsAuto, 'False' As KaliQty, 'True' As SPK,SD.stsJasa,SD.stsMentah,SD.BBIDInd From M_SpecDtl SD Inner Join M_Div D On SD.DivID=D.DivID Inner Join M_Komp K On SD.KompID=K.KompID Inner Join M_BB B On B.BBID=SD.BBID Where SpecID='" & Me.SLUSpec.EditValue & "' and SpecIDD Not In (Select SpecIDD From M_ModelDtl Where MdlID='" & Me.TBKode.EditValue & "')", koneksi)
                        cmsl.Fill(DsMaster, "M_ModelDtl")
                End Select

                Me.GridControl1.DataSource = DsMaster
                Me.GridControl1.DataMember = "M_ModelDtl"

                'Me.GridView1.Columns("BOM").Visible = False

                'Me.GridView1.RefreshData()

            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        Dim Masuk As Boolean
        Me.BSave.Focus()
        Me.BCopy.Enabled = True

        Dim z : For z = 0 To arrPar.GetUpperBound(0)
            If Not IsNothing(arrPar(z)) Then

                Try
                    DsMaster.Tables("M_ModelDtl").Columns.Remove(arrPar(z))

                Catch ex As Exception

                End Try
            End If
        Next

        Ukuran = ""
        If Ukuran <> Me.TBSize.EditValue Then
            Dim a : For a = 0 To Me.GridView3.RowCount - 1
                If Me.GridView3.GetRowCellValue(a, "Uk") = Me.TBSize.EditValue Then
                    'Ukuran = Me.GridView3.GetRowCellValue(a, "Uk")
                    'ArtCenter = Me.GridView3.GetRowCellValue(a, "ArtCode")
                    'centermin = i - 1
                    'centerplus = i + 1

                    center = a
                    Exit For
                End If
            Next
        End If

        Dim i : For i = 0 To Me.GridView3.RowCount - 1
            Masuk = False

            Dim x : For x = 0 To DsMaster.Tables("M_ModelDtl").Columns.Count - 1
                If Me.GridView3.GetRowCellValue(i, "ArtCode") = DsMaster.Tables("M_ModelDtl").Columns.Item(x).ToString Then
                    Masuk = False
                    Exit For
                Else
                    Masuk = True
                End If
            Next

            If Masuk = True Then
                DsMaster.Tables("M_ModelDtl").Columns.Add(Me.GridView3.GetRowCellValue(i, "ArtCode"), GetType(Decimal)).Caption = Me.GridView3.GetRowCellValue(i, "Uk")
                Me.GridView1.PopulateColumns(DsMaster.Tables("M_ModelDtl"))

                Dim y : For y = 0 To DsMaster.Tables("M_ModelDtl").Rows.Count - 1
                    Dim Val As Decimal

                    Try
                        If i < center Then
                            If DsMaster.Tables("M_ModelDtl").Rows(y).Item("stsAuto") = True Then
                                If DsMaster.Tables("M_ModelDtl").Rows(y).Item("KaliQty") = True Then
                                    Val = Math.Round(Me.GridView1.GetRowCellValue(y, Me.GridView3.GetRowCellValue(i + 1, "ArtCode")) - (DsMaster.Tables("M_ModelDtl").Rows(y).Item(Me.GridView3.GetRowCellValue(i + 1, "ArtCode")) * Me.TBPresentase.EditValue / 100), 4, MidpointRounding.AwayFromZero)
                                Else
                                    Val = Math.Round(DsMaster.Tables("M_ModelDtl").Rows(y).Item(Me.GridView3.GetRowCellValue(i + 1, "ArtCode")) * ((100 + Me.TBPresentase.EditValue) / 100), 4, MidpointRounding.AwayFromZero)
                                End If
                            Else
                                Val = Me.GridView1.GetRowCellValue(y, Me.GridView3.GetRowCellValue(i + 1, "ArtCode"))
                            End If


                        Else
                            If DsMaster.Tables("M_ModelDtl").Rows(y).Item("stsAuto") = True Then
                                If DsMaster.Tables("M_ModelDtl").Rows(y).Item("KaliQty") = True Then
                                    Val = Math.Round(Me.GridView1.GetRowCellValue(y, Me.GridView3.GetRowCellValue(i - 1, "ArtCode")) + (DsMaster.Tables("M_ModelDtl").Rows(y).Item(Me.GridView3.GetRowCellValue(i - 1, "ArtCode")) * Me.TBPresentase.EditValue / 100), 4, MidpointRounding.AwayFromZero)
                                Else
                                    Val = Math.Round(DsMaster.Tables("M_ModelDtl").Rows(y).Item(Me.GridView3.GetRowCellValue(i - 1, "ArtCode")) * ((100 - Me.TBPresentase.EditValue) / 100), 4, MidpointRounding.AwayFromZero)
                                End If
                            Else
                                Val = Me.GridView1.GetRowCellValue(y, Me.GridView3.GetRowCellValue(i + 1, "ArtCode"))
                            End If
                        End If


                    Catch ex As Exception
                        DsMaster.Tables("M_ModelDtl").Rows(y).Item(Me.GridView3.GetRowCellValue(i, "ArtCode")) = 0
                    End Try

                    DsMaster.Tables("M_ModelDtl").Rows(y).Item(Me.GridView3.GetRowCellValue(i, "ArtCode")) = Val
                Next
            End If
        Next

        Me.GridView1.Columns("DivID").ColumnEdit = Me.BEdDivID
        Me.GridView1.Columns("KompID").ColumnEdit = Me.BEdKompID
        Me.GridView1.Columns("BBID").ColumnEdit = Me.BEdBBID

        Me.GridView1.Columns("MdlID").Visible = False
        Me.GridView1.Columns("SpecIDD").Visible = False
        'Me.GridView1.Columns("BOM").Visible = False
        Me.GridView1.Columns("stsCopy").Visible = False
        Me.GridView1.Columns("Urut").Visible = False
        Me.GridView1.Columns("Urut1").Visible = False
        Me.GridView1.Columns("stsJasa").Visible = False
        Me.GridView1.Columns("stsMentah").Visible = False
        Me.GridView1.Columns("BBIDInd").Visible = False


        Me.GridView1.Columns("stsCopy").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Urut").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Urut1").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("MdlID").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Divisi").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Komponen").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Bahan").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("Sat").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("stsJasa").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("stsMentah").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("SPK").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("KaliQty").OptionsColumn.AllowEdit = False
        Me.GridView1.Columns("BBIDInd").OptionsColumn.AllowEdit = False

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

        Dim c : For c = 0 To arrPar.GetUpperBound(0)
            If Not IsNothing(arrPar(c)) Then
                Try
                    Me.GridView1.Columns(arrPar(c)).Visible = False
                Catch ex As Exception

                End Try
            End If
        Next

        System.Array.Clear(arrPar, 0, arrPar.Length)

    End Sub

    Private Sub BHitAuto_Click(sender As Object, e As EventArgs) Handles BHitAuto.Click
        Ukuran = ""
        If Ukuran <> Me.TBSize.EditValue Then
            Dim i : For i = 0 To Me.GridView3.RowCount - 1
                If Me.GridView3.GetRowCellValue(i, "Uk") = Me.TBSize.EditValue Then
                    Ukuran = Me.GridView3.GetRowCellValue(i, "Uk")
                    ArtCenter = Me.GridView3.GetRowCellValue(i, "ArtCode")
                    centermin = i - 1
                    centerplus = i + 1
                    Exit For
                End If
            Next
        End If

        Dim z : For z = 0 To GridView1.RowCount - 1
            Dim x : For x = centermin To 0 Step -1
                If Not IsDBNull(Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(x + 1, "ArtCode"))) Then
                    If Me.GridView1.GetRowCellValue(z, "stsAuto") = True Then
                        Dim val As Decimal

                        If Me.GridView1.GetFocusedRowCellValue("KaliQty") = True Then
                            val = Math.Round(Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(x + 1, "ArtCode")) - (Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(x + 1, "ArtCode")) * Me.TBPresentase.EditValue / 100), 4, MidpointRounding.AwayFromZero)
                        Else
                            val = Math.Round(Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(x + 1, "ArtCode")) * ((100 + Me.TBPresentase.EditValue) / 100), 4, MidpointRounding.AwayFromZero)
                        End If

                        Me.GridView1.SetRowCellValue(z, Me.GridView3.GetRowCellValue(x, "ArtCode"), val)
                    End If
                End If
            Next

            Dim y : For y = centerplus To Me.GridView3.RowCount - 1
                If Not IsDBNull(Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(y - 1, "ArtCode"))) Then
                    If Me.GridView1.GetRowCellValue(z, "stsAuto") = True Then
                        Dim val As Decimal
                        If Me.GridView1.GetRowCellValue(z, "KaliQty") = True Then
                            val = Math.Round(Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(y - 1, "ArtCode")) + (Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(y - 1, "ArtCode")) * Me.TBPresentase.EditValue / 100), 4, MidpointRounding.AwayFromZero)
                        Else
                            val = Math.Round(Me.GridView1.GetRowCellValue(z, Me.GridView3.GetRowCellValue(y - 1, "ArtCode")) * ((100 - Me.TBPresentase.EditValue) / 100), 4, MidpointRounding.AwayFromZero)
                        End If

                        Me.GridView1.SetRowCellValue(z, Me.GridView3.GetRowCellValue(y, "ArtCode"), val)
                    End If
                End If
            Next
        Next


    End Sub

    Private Sub BEdArtCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdArtCode.ButtonClick
        Dim frm As New FSearch("Brg Model", Me.TBArtName.EditValue, Me.TBWarna.EditValue, "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.GridView3.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView3.SetRowCellValue(Me.GridView3.FocusedRowHandle, "Uk", dataTrans.Item("Uk").ToString)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BEdArtCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdArtCode.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdDivID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdDivID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdKompID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdKompID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FModel_d(Me.GridView2.GetFocusedDataRow.Item("MdlID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BCopy_Click(sender As Object, e As EventArgs) Handles BCopy.Click
        Dim frm As New FSearch("Model", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                Me.TBKode.EditValue = "--"
                'Me.TBStyle.EditValue = dataTrans.Item("StyleID").ToString
                'Me.TBBrand.EditValue = dataTrans.Item("Brand").ToString
                'Me.TBArtName.EditValue = dataTrans.Item("ArtName").ToString
                'Me.TBWarna.EditValue = dataTrans.Item("Warna").ToString
                'Me.TBRange.EditValue = dataTrans.Item("RangeSize").ToString
                Me.TBSize.EditValue = dataTrans.Item("SampleSize").ToString
                Me.TBPresentase.EditValue = dataTrans.Item("PersenGenerate").ToString
                Me.MKet.EditValue = dataTrans.Item("Ket").ToString

                FillDtlCp(dataTrans.Item("Kode").ToString)

                stsCopy = True

                Dim gridView As GridView = GridControl1.FocusedView
                gridView.BeginSort()

                Try
                    gridView.ClearSorting()
                    Me.GridView1.Columns("Divisi").SortOrder = ColumnSortOrder.Ascending
                    Me.GridView1.Columns("Komponen").SortOrder = ColumnSortOrder.Ascending
                    Me.GridView1.Columns("Bahan").SortOrder = ColumnSortOrder.Ascending
                Finally
                    gridView.EndSort()
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "stsCopy") <> False Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf Me.GridView1.GetRowCellValue(e.RowHandle, "stsCopy") = False Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("ArtCode")
        End If
    End Sub

    Private Sub FModel_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBRange.KeyPress, TBPresentase.KeyPress, TBSize.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "UkBB" Or view.FocusedColumn.FieldName = "Ket" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub
End Class